using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.SqlClient
{
	// Token: 0x02000335 RID: 821
	internal sealed class TdsParserStaticMethods
	{
		// Token: 0x06002ADE RID: 10974 RVA: 0x002C1918 File Offset: 0x002C0D18
		private TdsParserStaticMethods()
		{
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x002C1938 File Offset: 0x002C0D38
		internal static void AliasRegistryLookup(ref string host, ref string protocol)
		{
			if (!ADP.IsEmpty(host))
			{
				string text = (string)ADP.LocalMachineRegistryValue("SOFTWARE\\Microsoft\\MSSQLServer\\Client\\ConnectTo", host);
				if (!ADP.IsEmpty(text))
				{
					int num = text.IndexOf(',');
					if (-1 != num)
					{
						string text2 = text.Substring(0, num).ToLower(CultureInfo.InvariantCulture);
						if (num + 1 < text.Length)
						{
							string text3 = text.Substring(num + 1);
							if ("dbnetlib" == text2)
							{
								num = text3.IndexOf(':');
								if (-1 != num && num + 1 < text3.Length)
								{
									text2 = text3.Substring(0, num);
									if (SqlConnectionString.ValidProtocal(text2))
									{
										protocol = text2;
										host = text3.Substring(num + 1);
										return;
									}
								}
							}
							else
							{
								protocol = (string)SqlConnectionString.NetlibMapping()[text2];
								if (protocol != null)
								{
									host = text3;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x002C1A08 File Offset: 0x002C0E08
		internal static byte[] EncryptPassword(string password)
		{
			byte[] array = new byte[password.Length << 1];
			for (int i = 0; i < password.Length; i++)
			{
				int num = (int)password[i];
				byte b = (byte)(num & 255);
				byte b2 = (byte)(num >> 8 & 255);
				array[i << 1] = (byte)(((int)(b & 15) << 4 | b >> 4) ^ 165);
				array[(i << 1) + 1] = (byte)(((int)(b2 & 15) << 4 | b2 >> 4) ^ 165);
			}
			return array;
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x002C1A88 File Offset: 0x002C0E88
		internal static int GetCurrentProcessId()
		{
			return SafeNativeMethods.GetCurrentProcessId();
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x002C1AA8 File Offset: 0x002C0EA8
		internal static byte[] GetNIC()
		{
			int num = 0;
			byte[] array = null;
			object obj = ADP.LocalMachineRegistryValue("SOFTWARE\\Description\\Microsoft\\Rpc\\UuidTemporaryData", "NetworkAddressLocal");
			if (obj is int)
			{
				num = (int)obj;
			}
			if (num <= 0)
			{
				obj = ADP.LocalMachineRegistryValue("SOFTWARE\\Description\\Microsoft\\Rpc\\UuidTemporaryData", "NetworkAddress");
				if (obj is byte[])
				{
					array = (byte[])obj;
				}
			}
			if (array == null)
			{
				array = new byte[6];
				Random random = new Random();
				random.NextBytes(array);
			}
			return array;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x002C1B18 File Offset: 0x002C0F18
		internal static int GetTimeoutMilliseconds(long timeoutTime)
		{
			if (9223372036854775807L == timeoutTime)
			{
				return -1;
			}
			long num = ADP.TimerRemainingMilliseconds(timeoutTime);
			if (num < 0L)
			{
				return 0;
			}
			if (num > 2147483647L)
			{
				return int.MaxValue;
			}
			return (int)num;
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x002C1B58 File Offset: 0x002C0F58
		internal static long GetTimeoutSeconds(int timeoutSeconds)
		{
			long result;
			if (timeoutSeconds == 0)
			{
				result = long.MaxValue;
			}
			else
			{
				long num = ADP.TimerCurrent();
				result = num + ADP.TimerFromSeconds(timeoutSeconds);
			}
			return result;
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x002C1B88 File Offset: 0x002C0F88
		internal static bool TimeoutHasExpired(long timeoutTime)
		{
			bool result = false;
			if (0L != timeoutTime && 9223372036854775807L != timeoutTime)
			{
				result = ADP.TimerHasExpired(timeoutTime);
			}
			return result;
		}
	}
}
