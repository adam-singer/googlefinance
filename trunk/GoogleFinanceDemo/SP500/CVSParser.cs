using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace SP500
{
    public class CsvParseException : Exception
    {
        public CsvParseException(string message)
            : base(message)
        {
        }
    }

    public static class MyExtensions
    {
        private enum State
        {
            AtBeginningOfToken,
            InNonQuotedToken,
            InQuotedToken,
            ExpectingComma,
            InEscapedCharacter
        };

        public static string[] CsvSplit(this String source)
        {
            List<string> splitString = new List<string>();
            List<int> slashesToRemove = null;
            State state = State.AtBeginningOfToken;
            char[] sourceCharArray = source.ToCharArray();
            int tokenStart = 0;
            int len = sourceCharArray.Length;
            for (int i = 0; i < len; ++i)
            {
                switch (state)
                {
                    case State.AtBeginningOfToken:
                        if (sourceCharArray[i] == '"')
                        {
                            state = State.InQuotedToken;
                            slashesToRemove = new List<int>();
                            continue;
                        }
                        if (sourceCharArray[i] == ',')
                        {
                            splitString.Add("");
                            tokenStart = i + 1;
                            continue;
                        }
                        state = State.InNonQuotedToken;
                        continue;
                    case State.InNonQuotedToken:
                        if (sourceCharArray[i] == ',')
                        {
                            splitString.Add(
                                source.Substring(tokenStart, i - tokenStart));
                            state = State.AtBeginningOfToken;
                            tokenStart = i + 1;
                        }
                        continue;
                    case State.InQuotedToken:
                        if (sourceCharArray[i] == '"')
                        {
                            state = State.ExpectingComma;
                            continue;
                        }
                        if (sourceCharArray[i] == '\\')
                        {
                            state = State.InEscapedCharacter;
                            slashesToRemove.Add(i - tokenStart);
                            continue;
                        }
                        continue;
                    case State.ExpectingComma:
                        if (sourceCharArray[i] != ',')
                            throw new CsvParseException("Expecting comma");
                        string stringWithSlashes =
                            source.Substring(tokenStart, i - tokenStart);
                        foreach (int item in slashesToRemove.Reverse<int>())
                            stringWithSlashes =
                                stringWithSlashes.Remove(item, 1);
                        splitString.Add(
                            stringWithSlashes.Substring(1,
                                stringWithSlashes.Length - 2));
                        state = State.AtBeginningOfToken;
                        tokenStart = i + 1;
                        continue;
                    case State.InEscapedCharacter:
                        state = State.InQuotedToken;
                        continue;
                }
            }
            switch (state)
            {
                case State.AtBeginningOfToken:
                    splitString.Add("");
                    return splitString.ToArray();
                case State.InNonQuotedToken:
                    splitString.Add(
                        source.Substring(tokenStart,
                            source.Length - tokenStart));
                    return splitString.ToArray();
                case State.InQuotedToken:
                    throw new CsvParseException("Expecting ending quote");
                case State.ExpectingComma:
                    string stringWithSlashes =
                        source.Substring(tokenStart, source.Length - tokenStart);
                    foreach (int item in slashesToRemove.Reverse<int>())
                        stringWithSlashes = stringWithSlashes.Remove(item, 1);
                    splitString.Add(
                        stringWithSlashes.Substring(1,
                            stringWithSlashes.Length - 2));
                    return splitString.ToArray();
                case State.InEscapedCharacter:
                    throw new CsvParseException("Expecting escaped character");
            }
            throw new CsvParseException("Unexpected error");
        }
    }

    class xxProgramxx
    {
        //static bool Validate(string[] results, string[] expectedResults)
        //{
        //    if (results.Length != expectedResults.Length)
        //    {
        //        Console.WriteLine("  Validation error");
        //        return false;
        //    }
        //    for (int i = 0; i < results.Length; i++)
        //    {
        //        if (results[i] != expectedResults[i])
        //        {
        //            Console.WriteLine("  Validation error");
        //            return false;
        //        }
        //    }
        //    Console.WriteLine("  Validated");
        //    return true;
        //}

        //static void ValidateAll()
        //{
        //    string[] split;

        //    Console.WriteLine("Test1");
        //    split = "\"12\\\"3\",\"456\",\"789\"".CsvSplit();
        //    Validate(split, new[] { "12\"3", "456", "789" });

        //    Console.WriteLine("Test2");
        //    split = "\"123\",\"456\",\"789\"".CsvSplit();
        //    Validate(split, new[] { "123", "456", "789" });

        //    Console.WriteLine("Test3");
        //    split = "\"aaa,bbb\",\"ccc,ddd\",ghi".CsvSplit();
        //    Validate(split, new[] { "aaa,bbb", "ccc,ddd", "ghi" });

        //    Console.WriteLine("Test4");
        //    split = "aaa,,bbb".CsvSplit();
        //    Validate(split, new[] { "aaa", "", "bbb" });

        //    Console.WriteLine("Test5");
        //    try
        //    {
        //        split = "\"aaa\\bbb\",ccc,ddd".CsvSplit();
        //        Console.WriteLine("  Validation error");
        //    }
        //    catch (CsvParseException)
        //    {
        //        Console.WriteLine("  Validated");
        //    }

        //    Console.WriteLine("Test6");
        //    try
        //    {
        //        split = "\"aaabbb\"bbb,ccc,ddd".CsvSplit();
        //        Console.WriteLine("  Validation error");
        //    }
        //    catch (CsvParseException)
        //    {
        //        Console.WriteLine("  Validated");
        //    }

        //    Console.WriteLine("Test7");
        //    split = "aaa,,bbb,".CsvSplit();
        //    Validate(split, new[] { "aaa", "", "bbb", "" });

        //    Console.WriteLine("Test8");
        //    try
        //    {
        //        split = "\"aaabbb\",ccc,\"ddd".CsvSplit();
        //        Console.WriteLine("  Validation error");
        //    }
        //    catch (CsvParseException)
        //    {
        //        Console.WriteLine("  Validated");
        //    }

        //    Console.WriteLine("Test9");
        //    try
        //    {
        //        split = "aaa,ccc,\"ddd\\".CsvSplit();
        //        Console.WriteLine("  Validation error");
        //    }
        //    catch (CsvParseException)
        //    {
        //        Console.WriteLine("  Validated");
        //    }

        //    Console.WriteLine("Test10");
        //    split = "\"a\\\\aa\",,bbb,".CsvSplit();
        //    Validate(split, new[] { "a\\aa", "", "bbb", "" });

        //    Console.WriteLine("Test11");
        //    split = "\"a\\aa\",,bbb,".CsvSplit();
        //    Validate(split, new[] { "aaa", "", "bbb", "" });
        //}

        //static void Main(string[] args)
        //{
        //    XElement xmlDoc = new XElement("Root",
        //        File.ReadAllLines("TextFile.txt")
        //            .Select
        //            (
        //                line =>
        //                {
        //                    var split = line.CsvSplit();
        //                    return new XElement("Quote",
        //                        new XElement("Person", split[0]),
        //                        new XElement("Text", split[1])
        //                    );
        //                }
        //            )
        //    );
        //    Console.WriteLine(xmlDoc);
        //    ValidateAll();
        //}
    }
}



namespace SP500
{
    class CVSParser
    {
    }
}
