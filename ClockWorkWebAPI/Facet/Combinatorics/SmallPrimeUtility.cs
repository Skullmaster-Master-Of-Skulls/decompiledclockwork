using System;
using System.Collections;
using System.Collections.Generic;

namespace Facet.Combinatorics
{
	// Token: 0x02000005 RID: 5
	public class SmallPrimeUtility
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002050 File Offset: 0x00000250
		private SmallPrimeUtility()
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002324 File Offset: 0x00000524
		public static List<int> Factor(int i)
		{
			int num = 0;
			int num2 = SmallPrimeUtility.PrimeTable[num];
			List<int> list = new List<int>();
			while (i > 1)
			{
				bool flag = i % num2 == 0;
				if (flag)
				{
					list.Add(num2);
					i /= num2;
				}
				else
				{
					num++;
					num2 = SmallPrimeUtility.PrimeTable[num];
				}
			}
			return list;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002388 File Offset: 0x00000588
		public static List<int> MultiplyPrimeFactors(IList<int> lhs, IList<int> rhs)
		{
			List<int> list = new List<int>();
			foreach (int item in lhs)
			{
				list.Add(item);
			}
			foreach (int item2 in rhs)
			{
				list.Add(item2);
			}
			list.Sort();
			return list;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002428 File Offset: 0x00000628
		public static List<int> DividePrimeFactors(IList<int> numerator, IList<int> denominator)
		{
			List<int> list = new List<int>();
			foreach (int item in numerator)
			{
				list.Add(item);
			}
			foreach (int item2 in denominator)
			{
				list.Remove(item2);
			}
			return list;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024C4 File Offset: 0x000006C4
		public static long EvaluatePrimeFactors(IList<int> value)
		{
			long num = 1L;
			foreach (int num2 in value)
			{
				num *= (long)num2;
			}
			return num;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002518 File Offset: 0x00000718
		static SmallPrimeUtility()
		{
			SmallPrimeUtility.CalculatePrimes();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000252C File Offset: 0x0000072C
		private static void CalculatePrimes()
		{
			BitArray bitArray = new BitArray(65536, true);
			for (int i = 2; i <= 256; i++)
			{
				bool flag = bitArray[i];
				if (flag)
				{
					for (int j = 2 * i; j < 65536; j += i)
					{
						bitArray[j] = false;
					}
				}
			}
			SmallPrimeUtility.myPrimes = new List<int>();
			for (int k = 2; k < 65536; k++)
			{
				bool flag2 = bitArray[k];
				if (flag2)
				{
					SmallPrimeUtility.myPrimes.Add(k);
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000025D4 File Offset: 0x000007D4
		public static IList<int> PrimeTable
		{
			get
			{
				return SmallPrimeUtility.myPrimes;
			}
		}

		// Token: 0x04000008 RID: 8
		private static List<int> myPrimes = new List<int>();
	}
}
