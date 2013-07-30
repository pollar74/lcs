using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//* Write a program that reads an array of integers and removes from it a minimal number
//of elements in such way that the remaining array is sorted in increasing order.
//Print the remaining sorted array. Example:
//    {6, 1, 4, 3, 0, 3, 6, 4, 5}  {1, 3, 3, 4, 5}

//test

namespace SortThrouRemoval
{
    class SortThrouRemoval
    {
        static int MaximalArrayElement(int[,] arrayForCheck)
        {
            int maxeElment = arrayForCheck[0, 0];
            for (int i = 0; i < arrayForCheck.GetLength(0); i++)
            {
                for (int j = 0; j < arrayForCheck.GetLength(1); j++)
                {
                    if (arrayForCheck[i, j] > maxeElment)
                    {
                        maxeElment = arrayForCheck[i, j];
                    }

                }
            }
            return maxeElment;
        }
        static void Main(string[] args)
        {
            int length = -1;
            while (length < 1)
            {
                Console.WriteLine("Please, enter array length:");
                length = Convert.ToInt32(Console.ReadLine());
            }
            int[] inputArray = new int[length];
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine("Please, enter element {0} from {1}", i + 1, length);
                inputArray[i] = Convert.ToInt32(Console.ReadLine());
            }
            int[] sortedArray = (int[])inputArray.Clone();
            Array.Sort(sortedArray);
            int[,] lcs = new int[inputArray.Length + 1, inputArray.Length + 1];
            for (int row = 0; row <= sortedArray.Length; row++)
            {
                for (int column = 0; column <= inputArray.Length; column++)
                {
                    lcs[0, column] = 0;
                    lcs[row, 0] = 0;
                }
            }
            for (int row = 1; row <= sortedArray.Length; row++)
            {
                for (int column = 1; column <= inputArray.Length; column++)
                {
                    if (sortedArray[column - 1] == inputArray[row - 1])
                    {
                        lcs[row, column] = (lcs[row - 1, column - 1]) + 1;
                    }
                    else
                    {
                        if (lcs[row - 1, column] > lcs[row, column - 1])
                        {
                            lcs[row, column] = lcs[row - 1, column];

                        }
                        else if (lcs[row - 1, column] < lcs[row, column - 1])
                        {
                            lcs[row, column] = lcs[row, column - 1];
                        }
                        else
                        {
                            lcs[row, column] = lcs[row - 1, column];
                        }
                    }
                }
            }

            string longestCommonSequence = "";
            int maxLcsLength = MaximalArrayElement(lcs);
            for (int i = lcs.GetLength(0) - 1; i > 0; i--)
            {
                for (int j = lcs.GetLength(1) - 1; j > 0; j--)
                {
                    if (i > 0 && j > 0)
                    {
                        if ((lcs[i - 1, j - 1] < lcs[i, j]) &&
                            (lcs[i - 1, j] < lcs[i, j]) &&
                            lcs[i, j - 1] < lcs[i, j] &&
                            lcs[i, j] == maxLcsLength)
                        {
                            longestCommonSequence = longestCommonSequence + " " + inputArray[i - 1].ToString();
                            maxLcsLength--;
                        }
                    }
                }

            }

            char[] elements = longestCommonSequence.ToArray();
            List<string> listLCS = new List<string>();
            string result = "";
            for (int i = 1; i < longestCommonSequence.Length; i++)
            {
                if (elements[i] == 32)
                {
                    listLCS.Add(result);
                    result = "";
                    continue;
                }
                else
                {
                    result += elements[i];
                }
            }
            listLCS.Add(result);
            listLCS.Reverse();
            foreach (var item in listLCS)
            {
                Console.Write(item + " ");
            }
        }
    }
}
