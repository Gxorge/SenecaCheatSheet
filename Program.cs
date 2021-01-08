using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SenecaCheatSheet
{
    class Program
    {
        /*
         * TODO:
         * - Exact List
         * - Multi Select
         */

        static string _course = "";
        static string _section = "";
        static string _fileName = "";

        static void Main(string[] args)
        {
            Console.WriteLine("Seneca Cheat Sheet - by Gabriella Kaitlyn");
            Console.WriteLine(" ");

            Console.WriteLine("Please enter the course id: ");
            _course = Console.ReadLine();
            Console.WriteLine("Please enter the section id: ");
            _section = Console.ReadLine();

            string data = Get(GenerateUrl(_course, _section));
            if (data == null)
                return;
            ParseAndWrite(data);
        }

        public static void ParseAndWrite(string data)
        {
            Console.WriteLine("Data recieved. Parsing json data...");
            Answers answers = null;
            try
            {
                answers = JsonConvert.DeserializeObject<Answers>(data);
            } catch (Exception ex)
            {
                Console.WriteLine("Error json data: " + ex.Message);
                Console.WriteLine("Please contact Gabriella from her github page: https://github.com/IsGabriellaCurious");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Parsed. Writing file...");
            _fileName = "" + answers.title + " Answers.txt";
            List<string> lines = new List<string>();

            lines.Add("Seneca Cheat Sheet - by Gabriella Kaitlyn");
            lines.Add(" ");
            lines.Add("Abbreviation meanings:");
            lines.Add("MC - Multiple Choice");
            lines.Add("WF - Word Fill");
            lines.Add("ID - Image Description (formatting is a bit gross but you can figure it out)");
            lines.Add("WW - Wrong Word (for this, see where the \"WW starts with\" ends and the wrong word will be there)");
            lines.Add("LT - List (there will be an arrow that points to the correct word)");
            lines.Add(" ");
            lines.Add("Answers are split into sections, if you can't find the question in a section, check other ones as Seneca could be recapping.");
            lines.Add("Some answers may be incorrect due to Seneca's weird JSON formatting, though this shouldn't happen often.");
            lines.Add(" ");

            foreach (Content c in answers.contents)
            {
                lines.Add("=== " + c.tags[0] + " ===");
                lines.Add(" ");
                foreach (ContentModule cm in c.contentModules)
                {
                    try
                    {
                        Content2 cont = cm.content;
                        switch (cm.moduleType)
                        {
                            case "multiple-choice":
                                {
                                    lines.Add("MC Question: " + cont.question);
                                    lines.Add("Answer: " + cont.correctAnswer);
                                    lines.Add(" ");
                                    break;
                                }
                            case "wordfill":
                                {
                                    string q = (string)cont.words[0];
                                    JObject j = cont.words[1] as JObject;
                                    WordsJson w = j.ToObject<WordsJson>();

                                    lines.Add("WF Question: " + q);
                                    lines.Add("Answer: " + w.word);
                                    lines.Add(" ");
                                    break;
                                }
                            case "toggles":
                                {
                                    lines.Add("Toggle Statement: " + cont.statement);
                                    StringBuilder sb = new StringBuilder();
                                    foreach (Toggle t in cont.toggles)
                                    {
                                        sb.Append((sb.ToString() == "" ? "" : ", ") + t.correctToggle);
                                    }
                                    lines.Add("Toggle Answers: " + sb.ToString());
                                    lines.Add(" ");
                                    break;
                                }
                            case "image-description":
                                {
                                    StringBuilder ans = new StringBuilder();
                                    int curr = 1;
                                    while (curr < cont.words.Count)
                                    {
                                        JObject j = cont.words[curr] as JObject;
                                        WordsJson w = j.ToObject<WordsJson>();
                                        ans.Append((ans.ToString() == "" ? "" : ", ") + w.word);
                                        curr += 2;
                                    }
                                    string sw = (string)cont.words[0];



                                    lines.Add("ID starts with: " + sw);
                                    lines.Add("ID possible answers: " + ans.ToString());
                                    lines.Add(" ");
                                    break;
                                }
                            case "wrong-word":
                                {
                                    StringBuilder ans = new StringBuilder();
                                    int curr = 1;
                                    while (curr < cont.sentence.Count)
                                    {
                                        JObject j = cont.sentence[curr] as JObject;
                                        WordsJson w = j.ToObject<WordsJson>();
                                        ans.Append((ans.ToString() == "" ? "" : ", ") + w.word);
                                        curr += 2;
                                    }
                                    string sw = (string)cont.sentence[0];



                                    lines.Add("WW starts with: " + sw);
                                    lines.Add("WW possible answers: " + ans.ToString());
                                    lines.Add(" ");
                                    break;
                                }
                            case "mindmap":
                                {
                                    StringBuilder ans = new StringBuilder();
                                    foreach (Value v in cont.values)
                                    {
                                        int curr = 1;
                                        while (curr < v.value.Count)
                                        {
                                            JObject j = v.value[curr] as JObject;
                                            WordsJson w = j.ToObject<WordsJson>();
                                            ans.Append((ans.ToString() == "" ? "" : ", ") + w.word);
                                            curr += 2;
                                        }
                                    }

                                    lines.Add("MM Statement: " + cont.statement);
                                    lines.Add("MM possible answers: " + ans.ToString());
                                    lines.Add(" ");
                                    break;
                                }
                            case "list":
                                {
                                    lines.Add("LT Statement: " + cont.statement);
                                    foreach (Value v in cont.values)
                                    {
                                        bool after = false;
                                        JObject j = v.value[1] as JObject;
                                        if (j == null)
                                        {
                                            j = v.value[0] as JObject;
                                            after = true;
                                        }
                                            
                                        WordsJson w = j.ToObject<WordsJson>();
                                        if (!after)
                                            lines.Add("LT answer: " + v.value[0].ToString().Trim() + " -> " + w.word.Trim());
                                        else
                                            lines.Add("LT answer: " + w.word.Trim() + " <- " + v.value[1].ToString().Trim());
                                    }
                                    lines.Add(" ");

                                    break;
                                }
                            default:
                                break;
                        }
                    } catch (Exception ex)
                    {
                        continue;
                    }
                }
            }

            File.WriteAllLines(_fileName, lines);

            Console.WriteLine("Written answers to " + _fileName);
            Process.Start(_fileName);
            Console.WriteLine("File opened. Press enter to close the application.");
            Console.ReadLine();
        }

        public static string Get(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                if (uri.Contains("signed-url"))
                    request.Headers["correlationid"] = "1610108125764::0a953a252cc60181648be7dbfa8d9f01";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error getting answers from Seneca: " + ex.Message);
                Console.WriteLine("If the error contains \"403\", please contact me.");
                Console.WriteLine("Else, make sure you typed everything correctly and try again.");
                Console.ReadLine();
                return null;
            }
        }

        public static string GenerateSignedUrl(string course, string section)
        {
            return "https://course.app.senecalearning.com/api/courses/" + course + "/signed-url?sectionId=" + section;
        }

        public static string GenerateUrl(string course, string section)
        {
            Console.WriteLine("Requesting signed url from Seneca...");
            SignedUrl signed = JsonConvert.DeserializeObject<SignedUrl>(Get(GenerateSignedUrl(course, section)));
            Console.WriteLine("Sending request to answer CDN...");
            return signed.url;
        }
    }
}
