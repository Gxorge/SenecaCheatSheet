using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenecaCheatSheet
{
    public class Answers
    {
        public string id { get; set; }
        public string parentId { get; set; }
        public string title { get; set; }
        public List<string> moduleIds { get; set; }
        public List<Content> contents { get; set; }
        public List<object> sections { get; set; }
    }

    public class Example
    {
        public string title { get; set; }
        public string url { get; set; }
        public string example { get; set; }
    }

    public class Child2
    {
        public string parent { get; set; }
        public string value { get; set; }
        public string text { get; set; }
        //public List<Child> children { get; set; }
    }

    public class Tree
    {
        public string value { get; set; }
        //public List<Child> children { get; set; }
        public string text { get; set; }
    }

    public class Toggle
    {
        public string incorrectToggle { get; set; }
        public string correctToggle { get; set; }
    }

    public class Definition
    {
        public List<object> word { get; set; }
        public string text { get; set; }
    }

    public class Value
    {
        public List<object> value { get; set; }
    }

    public class Content2
    {
        public bool randomExampleOrder { get; set; }
        public string title { get; set; }
        public string explanation { get; set; }
        public List<Example> examples { get; set; }
        public List<Tree> tree { get; set; }
        public List<object> words { get; set; }
        public string description { get; set; }
        public string key { get; set; }
        public object wrongAnswers { get; set; }
        public string question { get; set; }
        public string correctAnswer { get; set; }
        public string imageURL { get; set; }
        public string statement { get; set; }
        public List<Toggle> toggles { get; set; }
        public List<object> sentence { get; set; }
        //public List<Definition> definitions { get; set; }
        public string pretest { get; set; }
        public List<Value> values { get; set; }
    }

    public class ContentModule
    {
        public string courseId { get; set; }
        public string id { get; set; }
        public string parentId { get; set; }
        public string moduleType { get; set; }
        public Content2 content { get; set; }
        public int moduleDifficulty { get; set; }
    }

    public class Content
    {
        public string courseId { get; set; }
        public string id { get; set; }
        public string parentId { get; set; }
        public List<string> tags { get; set; }
        public List<object> prereqTags { get; set; }
        public List<ContentModule> contentModules { get; set; }
    }

    public class WordsJson
    {
        public List<object> otherPermittedWords { get; set; }
        public string word;
        public string caps;
    }
}
