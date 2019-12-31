using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogFactorial
{
	class Program
	{

		static void Main(string[] args)
		{
			int value = 7;

            Console.WriteLine("Value: " + value);
            Console.WriteLine();
			Console.WriteLine("Standard factorial: " + standardFactorial(value));
			Console.WriteLine("Log factorial: " + Math.Exp(logFactorial(value)));
			Console.WriteLine("Stirling's approximation factorial: " + stirlingApproxFactorial(value));
			Console.WriteLine();

            // tes withi Binomial Theorem
			int n = 100;
			int k = 10;

            Console.WriteLine("------------------------------------------------------------------------");
			Console.WriteLine("n: " + n);
			Console.WriteLine("k: " + k);
			Console.WriteLine();

			binomialTheoremTest(n, k, 0, "Standard factorial 1");
			binomialTheoremTest(n, k, 1, "Standard factorial 2");
			binomialTheoremTest(n, k, 2, "LogFactorial");
			binomialTheoremTest(n, k, 3, "Stirling's approximation factorial");

			Console.ReadLine();
		}

		static void binomialTheoremTest(int n, int k, int method, string msg)
		{
			try
			{
				double res = binomialTheorem(n, k, method);
				Console.WriteLine(msg + ": " + res);
			}
			catch (Exception ex)
			{
				Console.WriteLine(msg + " - error: " + ex.Message);
			}
		}

		/// <summary>
		/// FATTORIAL(n) / (FATTORIAL(n-k) * FATTORIAL(k))
		/// </summary>
		/// <param name="n"></param>
		/// <param name="k"></param>
		/// <param name="standard"></param>
		/// <returns></returns>
		static double binomialTheorem(int n, int k, int method)
		{
			Func<int, double> factorialFunc = num => num <= 0 ? 1.0 : Enumerable.Range(1, num).Select(_ => (double)_).Aggregate((acc, x) => acc * x);

			double res = 0;
			double fN, fNK, fK;
			switch (method)
			{
				case 0:
					fN = standardFactorial(n);
					fNK = standardFactorial(n - k);
					fK = standardFactorial(k);
					res = fN / (fNK * fK);
					break;

				case 1:
					fN = factorialFunc(n);
					fNK = factorialFunc(n - k);
					fK = factorialFunc(k);
					res = fN / (fNK * fK);
					break;

				case 2:
					fN = logFactorial(n);
					fNK = logFactorial(n - k);
					fK = logFactorial(k);
					double logRes = fN - fNK - fK;
					res = Math.Exp(logRes);
					break;

				case 3:
					fN = stirlingApproxFactorial(n);
					fNK = stirlingApproxFactorial(n - k);
					fK = stirlingApproxFactorial(k);
					res = fN / (fNK * fK);
					break;
			}
			
			return res;
		}

		static double logFactorial(int n)
		{
			double res = 0;
			for (int i = 2; i <= n; i++)
			{
				res += Math.Log(i);
			}
			return res;
		}

		static double standardFactorial(int n)
		{
			if (n <= 1) return 1;
			return n * standardFactorial(n - 1);
		}

		//Stirling's approximation
		static double stirlingApproxFactorial(int n)
		{
			double res = Math.Sqrt(2 * Math.PI * n) * Math.Pow(n / Math.E, n);
			return res;
		}
	}
}
