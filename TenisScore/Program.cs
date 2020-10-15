using System;
using System.Collections.Generic;
using System.IO;

namespace TenisScore
{
    class Program
    {

        public class TenisScore
        {
            public List<string> sets;
            public int gemCntA = 0;
            public int gemCntB = 0;
            public int scoreA = 0;
            public int scoreB = 0;
            public int gemCounter = 0;
        }
        public static List<string> scores = new List<string> { "0", "15", "30", "40", "A" };
        static void Main(string[] args)
        {
            var listOfStrings = new List<string>();
            Console.WriteLine("[Please enter the input file name with exstension: example input.txt]");
            var inputFileName = Console.ReadLine();
            Console.WriteLine("[Please enter the output file name with exstension: example something.txt]");
            var outputFileName = Console.ReadLine();
            string inputFileText = "";
            string readPath = Path.Combine("../../../", inputFileName);
            string writePath = Path.Combine("../../../", outputFileName);

            using (StreamReader r = new StreamReader(readPath))
            {
                while((inputFileText = r.ReadLine()) != null)
                {
                    listOfStrings.Add(GetAndWriteScore(inputFileText, outputFileName));
                }        
            }

            using (StreamWriter w = new StreamWriter(writePath))
            {
                foreach(var line in listOfStrings)
                {
                    w.WriteLine(line);
                }
                Console.WriteLine("[Writing done!]");
            }
            Console.WriteLine("[Press enter to finish!]");
            Console.ReadLine();
        }

        public static string GetAndWriteScore(string inputFileText, string outputFileName)
        {
            var score = new TenisScore();
            score.sets = new List<string>();
            foreach(var c in inputFileText)
            {
                if(c == 'A')
                {
                    score.scoreA++;
                }
                else if(c == 'B')
                {
                    score.scoreB++;
                }
                if(score.scoreA == 4 && score.scoreB < 3)
                {
                    score.gemCntA++;
                    score.gemCounter++;
                    score.scoreA = 0;
                    score.scoreB = 0;
                }
                else if(score.scoreB == 4 && score.scoreA < 3)
                {
                    score.gemCntB++;
                    score.gemCounter++;
                    score.scoreA = 0;
                    score.scoreB = 0;
                }
                if (score.scoreA == 5 && score.scoreB == 3)
                {
                    score.gemCntA++;
                    score.gemCounter++;
                    score.scoreA = 0;
                    score.scoreB = 0;
                }
                else if(score.scoreB == 5 && score.scoreA ==3)
                {
                    score.gemCntB++;
                    score.gemCounter++;
                    score.scoreA = 0;
                    score.scoreB = 0;
                }
                if(score.scoreA == 4 && score.scoreB == 4)
                {
                    score.scoreA = 3;
                    score.scoreB = 3;
                }
                if ((score.gemCntA >=6 && score.gemCntA-score.gemCntB > 1) || (score.gemCntB >=6 && score.gemCntB -score.gemCntA > 1))
                { 
                    score.sets.Add(score.gemCntA.ToString() + '-' + score.gemCntB.ToString() + " ");
                    score.gemCntA = 0;
                    score.gemCntB = 0;
                }
            }
            var stringBuilder = "";
            if(score.gemCntA != 0 || score.gemCntB != 0)
            {
                score.sets.Add(score.gemCntA.ToString() + '-' + score.gemCntB.ToString() + " ");
            }
            else if(score.scoreA != 0 || score.scoreB != 0 || score.sets.Count != 0)
            {
                score.sets.Add(score.gemCntA.ToString() + '-' + score.gemCntB.ToString() + " ");
            }
            foreach(var set in score.sets)
            {
                if(score.gemCounter% 2 == 1)
                {
                    stringBuilder += Reverse(set);
                }
                else
                {
                    stringBuilder += set;
                }          
            }

            if (score.scoreA != 0 || score.scoreB != 0 || (score.sets.Count == 0 && score.scoreA == 0 && score.scoreB == 0 && score.gemCntA == 0 && score.gemCntB == 0))
            {
                if (score.gemCounter % 2 == 0)
                {
                    stringBuilder += scores[score.scoreA] + "-" + scores[score.scoreB];
                }
                else
                {
                    stringBuilder += scores[score.scoreB] + "-" + scores[score.scoreA];
                }
                
            }
            return stringBuilder;
        }
        public static string Reverse(string set)
        {
            var newString = "";
            for (int i = (set.Length -2); i >= 0; i--)
            {
                newString += set[i];
            }
            return newString + " ";
        }
    }
}
