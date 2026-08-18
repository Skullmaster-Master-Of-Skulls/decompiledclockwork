using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoComboBox
{
	// Token: 0x0200005D RID: 93
	public class SqlLocator
	{
		// Token: 0x0600033E RID: 830
		[DllImport("odbc32.dll")]
		private static extern short SQLAllocHandle(short hType, IntPtr inputHandle, out IntPtr outputHandle);

		// Token: 0x0600033F RID: 831
		[DllImport("odbc32.dll")]
		private static extern short SQLSetEnvAttr(IntPtr henv, int attribute, IntPtr valuePtr, int strLength);

		// Token: 0x06000340 RID: 832
		[DllImport("odbc32.dll")]
		private static extern short SQLFreeHandle(short hType, IntPtr handle);

		// Token: 0x06000341 RID: 833
		[DllImport("odbc32.dll", CharSet = CharSet.Ansi)]
		private static extern short SQLBrowseConnect(IntPtr hconn, StringBuilder inString, short inStringLength, StringBuilder outString, short outStringLength, out short outLengthNeeded);

		// Token: 0x06000342 RID: 834 RVA: 0x0001A0B6 File Offset: 0x000190B6
		private SqlLocator()
		{
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001A0C4 File Offset: 0x000190C4
		public static string[] GetServers()
		{
			string[] result = null;
			string text = string.Empty;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			StringBuilder stringBuilder = new StringBuilder("DRIVER=SQL SERVER");
			StringBuilder stringBuilder2 = new StringBuilder(1024);
			short inStringLength = (short)stringBuilder.Length;
			short num = 0;
			try
			{
				if (0 == SqlLocator.SQLAllocHandle(1, zero, out zero))
				{
					if (0 == SqlLocator.SQLSetEnvAttr(zero, 200, (IntPtr)3, 0))
					{
						if (0 == SqlLocator.SQLAllocHandle(2, zero, out zero2))
						{
							if (99 == SqlLocator.SQLBrowseConnect(zero2, stringBuilder, inStringLength, stringBuilder2, 1024, out num))
							{
								if (1024 < num)
								{
									stringBuilder2.Capacity = (int)num;
									if (99 != SqlLocator.SQLBrowseConnect(zero2, stringBuilder, inStringLength, stringBuilder2, num, out num))
									{
										throw new ApplicationException("Unabled to aquire SQL Servers from ODBC driver.");
									}
								}
								text = stringBuilder2.ToString();
								int num2 = text.IndexOf("{") + 1;
								int num3 = text.IndexOf("}") - num2;
								if (num2 > 0 && num3 > 0)
								{
									text = text.Substring(num2, num3);
								}
								else
								{
									text = string.Empty;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				text = string.Empty;
			}
			finally
			{
				if (zero2 != IntPtr.Zero)
				{
					SqlLocator.SQLFreeHandle(2, zero2);
				}
				if (zero != IntPtr.Zero)
				{
					SqlLocator.SQLFreeHandle(1, zero2);
				}
			}
			if (text.Length > 0)
			{
				result = text.Split(",".ToCharArray());
			}
			return result;
		}

		// Token: 0x04000328 RID: 808
		private const short SQL_HANDLE_ENV = 1;

		// Token: 0x04000329 RID: 809
		private const short SQL_HANDLE_DBC = 2;

		// Token: 0x0400032A RID: 810
		private const int SQL_ATTR_ODBC_VERSION = 200;

		// Token: 0x0400032B RID: 811
		private const int SQL_OV_ODBC3 = 3;

		// Token: 0x0400032C RID: 812
		private const short SQL_SUCCESS = 0;

		// Token: 0x0400032D RID: 813
		private const short SQL_NEED_DATA = 99;

		// Token: 0x0400032E RID: 814
		private const short DEFAULT_RESULT_SIZE = 1024;

		// Token: 0x0400032F RID: 815
		private const string SQL_DRIVER_STR = "DRIVER=SQL SERVER";
	}
}
