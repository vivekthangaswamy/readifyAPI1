using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace readify.Controllers
{
    [RoutePrefix("api")]
    public class ReadifyController : ApiController
    {
        private static int Max = 92;

        [Route("Token")]
        public Guid GetToken()
        {
            return Guid.Parse("6f4d6811-2b42-4198-96c8-8b0f9af21752");
        }

        [Route("")]
        public string Get()
        {
            return "My web api implementation, Robert Rozas Navarro";
        }

        [Route("ReverseWords")]
        public string GetReverseWords(string sentence)
        {

            if (sentence != null)
            {
                var reversedWords = string.Join(" ",
                sentence.Split(' ')
                .Select(x => new String(x.Reverse().ToArray())));
                return reversedWords;
            }
            else
                throw new ArgumentNullException("string is null.");
        }

        [Route("Fibonacci")]
        public long GetFibonacci(long n)
        {
            if (n > Max || n < -Max)
            {
                throw new ArgumentException("Fib number beyond range.");
            }
            else
            {
                long a = 0;
                long b = 1;
                long result = 0;
                if (n == 0)
                {
                    result = 0;
                }
                else if (n > 0)
                {
                    result = FibonacciPositive(n, a, b);
                }
                else // in case of the input n is minus value
                {
                    result = FibonacciPositive(n * -1, a, b);

                    //shift the positive (n*-1) 1 bit right, then 1 bit left, to check is odd or even
                    //if the n is even, return the minus value.
                    if ((((n * -1) >> 1) << 1) == (n * -1))
                    {
                        result = result * -1;
                    }
                }

                return result;
            }
        }


        private long FibonacciPositive(long n, long a, long b)
        {
            long data = a;
            for (int i = 0; i < n; i++)
            {
                long temp = data;
                data = b;
                b = temp + b;
            }
            return data;
        }

        [Route("TriangleType")]
        public string GetTriangleType(int a, int b, int c)
        {
            if (a <= 0 || b <= 0 || c <= 0 || ((b + c) <= a) || ((a + c) <= b) || ((a + b) <= c))
            {
                return "Error";
            }
            else
            {
                if (AllSidesAreEqual(a, b, c))
                {
                    return ("Equilateral");
                }
                else if (AtLeastTwoSideAreEqual(a, b, c))
                {
                    return ("Isoceles");
                }
                else
                {
                    return ("Scalene");
                }
            }
        }

        private static bool AllSidesAreEqual(int side1, int side2, int side3)
        {
            return (side1 == side2)
                && (side2 == side3);
        }

        private static bool AtLeastTwoSideAreEqual(int side1, int side2, int side3)
        {
            return (side1 == side2)
                || (side2 == side3)
                || (side1 == side3);
        }
    }
}
