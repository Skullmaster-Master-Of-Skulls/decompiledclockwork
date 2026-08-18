using System;
using System.Data.Common;
using System.Globalization;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x0200022F RID: 559
	internal sealed class TdsParserStaticMethods
	{
		// Token: 0x060022CF RID: 8911 RVA: 0x000F1688 File Offset: 0x000F0A88
		private TdsParserStaticMethods()
		{
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000F169C File Offset: 0x000F0A9C
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

		// Token: 0x060022D1 RID: 8913 RVA: 0x000F1768 File Offset: 0x000F0B68
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

		// Token: 0x060022D2 RID: 8914 RVA: 0x000F17E4 File Offset: 0x000F0BE4
		internal static int GetCurrentProcessIdForTdsLoginOnly()
		{
			return SafeNativeMethods.GetCurrentProcessId();
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000F17F8 File Offset: 0x000F0BF8
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal static int GetCurrentThreadIdForTdsLoginOnly()
		{
			return AppDomain.GetCurrentThreadId();
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000F180C File Offset: 0x000F0C0C
		internal static byte[] GetNetworkPhysicalAddressForTdsLoginOnly()
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

		// Token: 0x060022D5 RID: 8917 RVA: 0x000F1878 File Offset: 0x000F0C78
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

		// Token: 0x060022D6 RID: 8918 RVA: 0x000F18B4 File Offset: 0x000F0CB4
		internal static long GetTimeoutSeconds(int timeout)
		{
			return TdsParserStaticMethods.GetTimeout((long)timeout * 1000L);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000F18D0 File Offset: 0x000F0CD0
		internal static long GetTimeout(long timeoutMilliseconds)
		{
			long result;
			if (timeoutMilliseconds <= 0L)
			{
				result = long.MaxValue;
			}
			else
			{
				try
				{
					result = checked(ADP.TimerCurrent() + ADP.TimerFromMilliseconds(timeoutMilliseconds));
				}
				catch (OverflowException)
				{
					result = long.MaxValue;
				}
			}
			return result;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000F1928 File Offset: 0x000F0D28
		internal static bool TimeoutHasExpired(long timeoutTime)
		{
			bool result = false;
			if (timeoutTime != 0L && 9223372036854775807L != timeoutTime)
			{
				result = ADP.TimerHasExpired(timeoutTime);
			}
			return result;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x000F1950 File Offset: 0x000F0D50
		internal static int NullAwareStringLength(string str)
		{
			if (str == null)
			{
				return 0;
			}
			return str.Length;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000F1968 File Offset: 0x000F0D68
		internal static int GetRemainingTimeout(int timeout, long start)
		{
			if (timeout <= 0)
			{
				return timeout;
			}
			long num = ADP.TimerRemainingSeconds(start + ADP.TimerFromSeconds(timeout));
			if (num <= 0L)
			{
				return 1;
			}
			return checked((int)num);
		}
	}
}
