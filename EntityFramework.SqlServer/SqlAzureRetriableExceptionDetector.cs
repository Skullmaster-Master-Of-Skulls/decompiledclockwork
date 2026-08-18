using System;
using System.Data.SqlClient;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000019 RID: 25
	internal static class SqlAzureRetriableExceptionDetector
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00004C7C File Offset: 0x00002E7C
		public static bool ShouldRetryOn(Exception ex)
		{
			SqlException ex2 = ex as SqlException;
			if (ex2 != null)
			{
				foreach (object obj in ex2.Errors)
				{
					SqlError sqlError = (SqlError)obj;
					int number = sqlError.Number;
					if (number <= 10060)
					{
						if (number <= 64)
						{
							if (number != 20 && number != 64)
							{
								continue;
							}
						}
						else if (number != 233)
						{
							switch (number)
							{
							case 10053:
							case 10054:
								break;
							default:
								if (number != 10060)
								{
									continue;
								}
								break;
							}
						}
					}
					else if (number <= 40501)
					{
						switch (number)
						{
						case 10928:
						case 10929:
							break;
						default:
							if (number != 40197 && number != 40501)
							{
								continue;
							}
							break;
						}
					}
					else if (number != 40613)
					{
						switch (number)
						{
						case 41301:
						case 41302:
						case 41305:
							break;
						case 41303:
						case 41304:
							continue;
						default:
							if (number != 41325)
							{
								continue;
							}
							break;
						}
					}
					return true;
				}
				return false;
			}
			return ex is TimeoutException;
		}
	}
}
