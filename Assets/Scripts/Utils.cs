using System;
using System.Collections.Generic;

public static class Utils {
    private static  Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static int[] CreateRandomIntArray(int size) {
        int[] array = new int[size];

        for (int i = 0; i < size; i++)
            array[i] = i;

        array.Shuffle();

        return array;
    }
}
