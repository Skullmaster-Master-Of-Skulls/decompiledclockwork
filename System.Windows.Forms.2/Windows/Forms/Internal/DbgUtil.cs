using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004EF RID: 1263
	[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
	[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
	[UIPermission(SecurityAction.Assert, Unrestricted = true)]
	internal sealed class DbgUtil
	{
		// Token: 0x06005238 RID: 21048
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetUserDefaultLCID();

		// Token: 0x06005239 RID: 21049
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int FormatMessage(int dwFlags, HandleRef lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, HandleRef arguments);

		// Token: 0x0600523A RID: 21050 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		public static void AssertFinalization(object obj, bool disposing)
		{
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string message)
		{
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string format, object arg1)
		{
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string format, object arg1, object arg2)
		{
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string format, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string format, object arg1, object arg2, object arg3, object arg4)
		{
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string format, object arg1, object arg2, object arg3, object arg4, object arg5)
		{
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		private static void AssertWin32Impl(bool expression, string format, object[] args)
		{
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x00155710 File Offset: 0x00153910
		public static string GetLastErrorStr()
		{
			int num = 255;
			StringBuilder stringBuilder = new StringBuilder(num);
			string text = string.Empty;
			int num2 = 0;
			try
			{
				num2 = Marshal.GetLastWin32Error();
				text = ((DbgUtil.FormatMessage(4608, new HandleRef(null, IntPtr.Zero), num2, DbgUtil.GetUserDefaultLCID(), stringBuilder, num, new HandleRef(null, IntPtr.Zero)) != 0) ? stringBuilder.ToString() : "<error returned>");
			}
			catch (Exception ex)
			{
				if (DbgUtil.IsCriticalException(ex))
				{
					throw;
				}
				text = ex.ToString();
			}
			return string.Format(CultureInfo.CurrentCulture, "0x{0:x8} - {1}", new object[]
			{
				num2,
				text
			});
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x001557C0 File Offset: 0x001539C0
		private static bool IsCriticalException(Exception ex)
		{
			return ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException;
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06005244 RID: 21060 RVA: 0x001557DD File Offset: 0x001539DD
		public static string StackTrace
		{
			get
			{
				return Environment.StackTrace;
			}
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x001557E4 File Offset: 0x001539E4
		public static string StackFramesToStr(int maxFrameCount)
		{
			string text = string.Empty;
			try
			{
				StackTrace stackTrace = new StackTrace(true);
				int i;
				for (i = 0; i < stackTrace.FrameCount; i++)
				{
					StackFrame frame = stackTrace.GetFrame(i);
					if (frame == null || frame.GetMethod().DeclaringType != typeof(DbgUtil))
					{
						break;
					}
				}
				maxFrameCount += i;
				if (maxFrameCount > stackTrace.FrameCount)
				{
					maxFrameCount = stackTrace.FrameCount;
				}
				for (int j = i; j < maxFrameCount; j++)
				{
					StackFrame frame2 = stackTrace.GetFrame(j);
					if (frame2 != null)
					{
						MethodBase method = frame2.GetMethod();
						if (!(method == null))
						{
							string text2 = string.Empty;
							string text3 = frame2.GetFileName();
							int num = (text3 == null) ? -1 : text3.LastIndexOf('\\');
							if (num != -1)
							{
								text3 = text3.Substring(num + 1, text3.Length - num - 1);
							}
							foreach (ParameterInfo parameterInfo in method.GetParameters())
							{
								text2 = text2 + parameterInfo.ParameterType.Name + ", ";
							}
							if (text2.Length > 0)
							{
								text2 = text2.Substring(0, text2.Length - 2);
							}
							text += string.Format(CultureInfo.CurrentCulture, "at {0} {1}.{2}({3})\r\n", new object[]
							{
								text3,
								method.DeclaringType,
								method.Name,
								text2
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (DbgUtil.IsCriticalException(ex))
				{
					throw;
				}
				text += ex.ToString();
			}
			return text.ToString();
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x001559A0 File Offset: 0x00153BA0
		public static string StackFramesToStr()
		{
			return DbgUtil.StackFramesToStr(DbgUtil.gdipInitMaxFrameCount);
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x001559AC File Offset: 0x00153BAC
		public static string StackTraceToStr(string message, int frameCount)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}\r\nTop Stack Trace:\r\n{1}", new object[]
			{
				message,
				DbgUtil.StackFramesToStr(frameCount)
			});
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x001559D0 File Offset: 0x00153BD0
		public static string StackTraceToStr(string message)
		{
			return DbgUtil.StackTraceToStr(message, DbgUtil.gdipInitMaxFrameCount);
		}

		// Token: 0x04003624 RID: 13860
		public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;

		// Token: 0x04003625 RID: 13861
		public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x04003626 RID: 13862
		public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x04003627 RID: 13863
		public const int FORMAT_MESSAGE_DEFAULT = 4608;

		// Token: 0x04003628 RID: 13864
		public static int gdipInitMaxFrameCount = 8;

		// Token: 0x04003629 RID: 13865
		public static int gdiUseMaxFrameCount = 8;

		// Token: 0x0400362A RID: 13866
		public static int finalizeMaxFrameCount = 5;
	}
}
