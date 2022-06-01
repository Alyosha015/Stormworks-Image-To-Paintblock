using System;

namespace ImageToPaintblockConverter {
    class Util {
        public static int ReadNumber(String message) {
            while (true) {
                try {
                    Console.Write(message);
                    int input = int.Parse(Console.ReadLine());
                    return input;
                }
                catch (Exception e) {
                    Console.WriteLine("The input was not a number!");
                }
            }
        }

        public static bool ReadBool(String message) {
            while (true) {
                Console.Write(message);
                String input = Console.ReadLine().ToLower();
                if (String.Equals(input, "y")) return true;
                else if (String.Equals(input, "n")) return false;
                Console.WriteLine("The input was not a y or n!");
            }
        }
    }
}
