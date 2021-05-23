using System;
using System.IO;

namespace ex01
{
    class Program
    {
        static int NoOccurence()
        {
            Console.WriteLine("Your name was not found.");
            return (0);
        }

        static int findMin(int one, int two, int three)
        {
            int res;

            res = one < two ? one : two;
            res = res < three ? res : three;
            return (res);
        }
        static int countLevensteinDistance(string str1, string str2) 
        {
            int res = 0;
            int i = 0;
            int j = 0;
            int[,] levArr = new int[str1.Length + 1, str2.Length + 1];

            for (i = 0; i < str1.Length + 1; i++)
            {
                for (j = 0; j < str2.Length + 1; j++)
                {
                    if (i == 0){
                        levArr[i,j] = j;
                        continue;
                    }
                    if (j == 0){
                        levArr[i,j] = i;
                        continue;
                    }
                    if (str1[i - 1] == str2[j - 1])
                    {
                        levArr[i, j] = findMin(levArr[i-1,j], levArr[i,j-1], levArr[i-1,j-1]);
                    }
                    else {
                        levArr[i, j] = findMin(levArr[i-1,j], levArr[i,j-1], levArr[i-1,j-1]) + 1;
                    }
                }
            }
            res = levArr[i - 1, j - 1];

            return (res);
        } 
        static int guessName(string n) 
        {
            Console.WriteLine($"Did you mean {n}? Y/N");
            string response = Console.ReadLine();
            if (response == "Y") {
               Console.WriteLine($"Hello, {n}");
               return (1);
            }
            else if (response != "Y" && response != "N")
            {
               return (NoOccurence());
            }
            return (-1);
        }
        static int Main(string[] args)
        {
            string[] allNames;
            int levensteinDist = 0;
            string name;
            int[] allLevensteinDists;

            try{
                allNames = File.ReadAllLines("us.txt");
            }
            catch {
                Console.WriteLine($"The file doesn't exist");
                return (0);
            }
            allLevensteinDists = new int[allNames.Length];

            Console.WriteLine("Enter name:");
            try {
                name = Console.ReadLine();
            }
            catch {
                return (NoOccurence());
            }
            
            int i = 0;
            foreach (string n in allNames)
            {
                 levensteinDist = countLevensteinDistance(n, name);
                 if (levensteinDist == 0)
                 {
                     Console.WriteLine($"Hello, {n}");
                     return (1);
                 }
                 allLevensteinDists[i] = levensteinDist;
                 /*
                if (n[0] == name[0] && n[n.Length - 1] == name[name.Length - 1]) {
                 if (levensteinDist == 0){
                     Console.WriteLine($"Hello, {n}");
                     return (1);
                 }
                 if (levensteinDist <= 3) {
                     Console.WriteLine($"Did you mean {n}? Y/N");
                     string response = Console.ReadLine();
                     if (response == "Y") {
                        Console.WriteLine($"Hello, {n}");
                        return (1);
                     }
                     else if (response != "Y" && response != "N")
                     {
                        return (NoOccurence());
                     }
                 }
                }
                 */
                 i++;
            }
            int answer = -1;
            for (int j = 0; j < allLevensteinDists.Length; j++)
            {
                int l = allLevensteinDists[j];
                if (l == 1) {
                    if ((answer = guessName(allNames[j])) == 1) {
                        return (1);
                    } 
                    else if(answer == 0){
                        return (0);
                    }
                } 
            }
            for (int j = 0; j < allLevensteinDists.Length; j++)
            {
                int l = allLevensteinDists[j];
                if (l == 2) {
                    if ((answer = guessName(allNames[j])) == 1) {
                        return (1);
                    } 
                    else if(answer == 0){
                        return (0);
                    }
                } 
            }
            for (int j = 0; j < allLevensteinDists.Length; j++)
            {
                int l = allLevensteinDists[j];
                if (l == 3) {
                    if ((answer = guessName(allNames[j])) == 1) {
                        return (1);
                    } 
                    else if(answer == 0){
                        return (0);
                    }
                } 
            }
            return (NoOccurence());
        }
    }
}
