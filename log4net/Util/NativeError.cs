using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace log4net.Util
{
	// Token: 0x02000107 RID: 263
	public sealed class NativeError
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x00017AC8 File Offset: 0x00015CC8
		private NativeError(int number, string message)
		{
			this.m_number = number;
			this.m_message = message;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00017ADE File Offset: 0x00015CDE
		public int Number
		{
			get
			{
				return this.m_number;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00017AE6 File Offset: 0x00015CE6
		public string Message
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00017AF0 File Offset: 0x00015CF0
		[SecuritySafeCritical]
		public static NativeError GetLastError()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			return new NativeError(lastWin32Error, NativeError.GetErrorMessage(lastWin32Error));
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00017B0F File Offset: 0x00015D0F
		public static NativeError GetError(int number)
		{
			return new NativeError(number, NativeError.GetErrorMessage(number));
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00017B20 File Offset: 0x00015D20
		[SecuritySafeCritical]
		public static string GetErrorMessage(int messageId)
		{
			int num = 256;
			int num2 = 512;
			int num3 = 4096;
			string text = "";
			IntPtr intPtr = 0;
			IntPtr arguments = 0;
			if (messageId != 0)
			{
				int num4 = NativeError.FormatMessage(num | num3 | num2, ref intPtr, messageId, 0, ref text, 255, arguments);
				if (num4 > 0)
				{
					text = text.TrimEnd(new char[]
					{
						'\r',
						'\n'
					});
				}
				else
				{
					text = null;
				}
			}
			else
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00017B9C File Offset: 0x00015D9C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "0x{0:x8}", this.Number) + ((this.Message != null) ? (": " + this.Message) : "");
		}

		// Token: 0x06000798 RID: 1944
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref string lpBuffer, int nSize, IntPtr Arguments);

		// Token: 0x040002D1 RID: 721
		private int m_number;

		// Token: 0x040002D2 RID: 722
		private string m_message;
	}
}
