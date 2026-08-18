using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters
{
	// Token: 0x0200059B RID: 1435
	public static class ISBNAdapter
	{
		// Token: 0x06002EB1 RID: 11953 RVA: 0x00033388 File Offset: 0x00031588
		public static bool IsValidISBN(this string isbn)
		{
			return isbn.ValidateISBN10() || isbn.ValidateISBN13();
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000333AC File Offset: 0x000315AC
		public static string DisplayISBNFormat(this string isbn)
		{
			bool flag = isbn.Length == 13;
			string result;
			if (flag)
			{
				result = string.Concat(new string[]
				{
					isbn.Substring(0, 3),
					"-",
					isbn.Substring(3, 1),
					"-",
					isbn.Substring(4, 4),
					"-",
					isbn.Substring(8, 4),
					"-",
					isbn.Substring(12, 1)
				});
			}
			else
			{
				bool flag2 = isbn.Length == 10;
				if (flag2)
				{
					result = string.Concat(new string[]
					{
						isbn.Substring(0, 1),
						"-",
						isbn.Substring(1, 4),
						"-",
						isbn.Substring(5, 4),
						"-",
						isbn.Substring(9, 1)
					});
				}
				else
				{
					result = isbn;
				}
			}
			return result;
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x00033498 File Offset: 0x00031698
		private static bool ValidateISBN13(this string isbn)
		{
			bool flag = string.IsNullOrEmpty(isbn) || isbn.Length != 13;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					int num = 0;
					for (int i = 0; i < 12; i += 2)
					{
						num += int.Parse(isbn.Substring(i, 1));
					}
					for (int j = 1; j < 12; j += 2)
					{
						num += int.Parse(isbn.Substring(j, 1)) * 3;
					}
					num += int.Parse(isbn.Substring(12));
					result = (num % 10 == 0);
				}
				catch
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x00033548 File Offset: 0x00031748
		private static bool ValidateISBN10(this string isbn)
		{
			bool flag = string.IsNullOrEmpty(isbn) || isbn.Length != 10;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					int num = 0;
					for (int i = 0; i < 10; i++)
					{
						num += int.Parse(isbn.Substring(i, 1)) * (10 - i);
					}
					result = (num % 11 == 0);
				}
				catch
				{
					result = false;
				}
			}
			return result;
		}
	}
}
