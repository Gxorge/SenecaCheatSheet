# Seneca Cheat Sheet
This is a little tool that gets all the answers for an assignment and formats it so you can read it.

## Bugs
This currently doesn't work on 2 question types (to do with exact answer questions).
I also noticed that this doesn't work on some courses, I'm currently working on a way around this to do with intercepting the data.

## Prerequisites
Make sure you are <b>logged into Seneca</b> on a suitable browser (this guide will have Chromium-based browsers in-mind) and have .NET Framework 4.8
<br>
This will probably only work on Windows.

## Setup
For this you will just need your identifier token. But first let's download the program.

- Download the ZIP from the releases tab of this GitHub repo.
- Once downloaded, create a new folder and extract the contents into it.
<br>
You've now got the program. Let's get some answers!


## Program Usage 
Now go find a seneca course to do. Once you have got to the start session screen (there should be a button at the bottom with that text):
- Take a look at the URL, it should look something like this: https://app.senecalearning.com/classroom/course/14f685aa-dcf1-4815-aafc-cf7d49aba313/section/4ffa572f-b8ba-4e49-a89c-4d43e75e1078/session . You will need 2 values from this
- Open the SenecaCheatSheet.exe file, ignore any anti-virus warnings that may pop up, it is not a virus.
- It will start by asking you for a course id, this is found in the URL right after it says "course/" up until "/section". So it should be "14f685aa-dcf1-4815-aafc-cf7d49aba313"
- Copy that then pase it into the window (by either right clicking or doing CTRL+V) and hit enter!
- Next it will asking for the section id, this is found in the URL right after it says "section/" up until "/session". So it should be "4ffa572f-b8ba-4e49-a89c-4d43e75e1078".
- Copy that then pase it into the window (by either right clicking or doing CTRL+V) and hit enter!
- It will then parse the data and open a text file with your answers in them, it should be pretty self explanitory from there.

If something is not working, feel free to add me on discord or email me. Those details can be found on my [account page](https://github.com/IsGabriellaCurious).
