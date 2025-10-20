using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace BoxOfCoins
{
    public class BoxOfCoins
    {
        public static int Solve(int[] boxes)
        {
            int n = boxes.Length;
            int[,] dp = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                dp[i, i] = boxes[i];
            }

            for (int length = 2; length <= n; length++)
            {
                for (int i = 0; i <= n - length; i++)
                {
                    int j = i + length - 1;
                    int pickLeft = boxes[i] - dp[i + 1, j];
                    int pickRight = boxes[j] - dp[i, j - 1];
                    dp[i, j] = Math.Max(pickLeft, pickRight);
                }
            }

            return dp[0, n - 1];
        }
    }
}