using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Web.Hosting;
using Microsoft.Win32;

namespace System.Web.Util
{
	// Token: 0x02000209 RID: 521
	internal sealed class Misc
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0004FBAE File Offset: 0x0004DDAE
		internal static StringComparer CaseInsensitiveInvariantKeyComparer
		{
			get
			{
				if (Misc.s_caseInsensitiveInvariantKeyComparer == null)
				{
					Misc.s_caseInsensitiveInvariantKeyComparer = StringComparer.Create(CultureInfo.InvariantCulture, true);
				}
				return Misc.s_caseInsensitiveInvariantKeyComparer;
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0004FBCC File Offset: 0x0004DDCC
		internal static void WriteUnhandledExceptionToEventLog(AppDomain appDomain, Exception exception)
		{
			if (appDomain == null || exception == null)
			{
				return;
			}
			ProcessImpersonationContext processImpersonationContext = null;
			try
			{
				processImpersonationContext = new ProcessImpersonationContext();
				string text = appDomain.GetData(".appId") as string;
				if (text == null)
				{
					text = appDomain.FriendlyName;
				}
				string text2 = SafeNativeMethods.GetCurrentProcessId().ToString(CultureInfo.InstalledUICulture);
				string @string = SR.Resources.GetString("Unhandled_Exception", CultureInfo.InstalledUICulture);
				Misc.ReportUnhandledException(exception, new string[]
				{
					@string,
					"\r\n\r\nApplication ID: ",
					text,
					"\r\n\r\nProcess ID: ",
					text2
				});
			}
			catch
			{
			}
			finally
			{
				if (processImpersonationContext != null)
				{
					processImpersonationContext.Undo();
				}
			}
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0004FC80 File Offset: 0x0004DE80
		internal static void ReportUnhandledException(Exception e, string[] strings)
		{
			UnsafeNativeMethods.ReportUnhandledException(Misc.FormatExceptionMessage(e, strings));
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0004FC90 File Offset: 0x0004DE90
		internal static string FormatExceptionMessage(Exception e, string[] strings)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			for (int i = 0; i < strings.Length; i++)
			{
				stringBuilder.Append(strings[i]);
			}
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				if (ex == e)
				{
					stringBuilder.Append("\r\n\r\nException: ");
				}
				else
				{
					stringBuilder.Append("\r\n\r\nInnerException: ");
				}
				stringBuilder.Append(ex.GetType().FullName);
				stringBuilder.Append("\r\n\r\nMessage: ");
				stringBuilder.Append(ex.Message);
				stringBuilder.Append("\r\n\r\nStackTrace: ");
				stringBuilder.Append(ex.StackTrace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0004FD36 File Offset: 0x0004DF36
		internal static void CopyMemory(IntPtr src, int srcOffset, byte[] dest, int destOffset, int size)
		{
			Marshal.Copy(new IntPtr(src.ToInt64() + (long)srcOffset), dest, destOffset, size);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0004FD50 File Offset: 0x0004DF50
		internal static void CopyMemory(byte[] src, int srcOffset, IntPtr dest, int destOffset, int size)
		{
			Marshal.Copy(src, srcOffset, new IntPtr(dest.ToInt64() + (long)destOffset), size);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0004FD6C File Offset: 0x0004DF6C
		internal unsafe static void CopyMemory(IntPtr src, int srcOffset, IntPtr dest, int destOffset, int size)
		{
			byte* src2 = (byte*)((void*)src) + srcOffset;
			byte* dest2 = (byte*)((void*)dest) + destOffset;
			StringUtil.memcpyimpl(src2, dest2, size);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0004FD94 File Offset: 0x0004DF94
		internal static void ThrowIfFailedHr(int hresult)
		{
			if (hresult < 0)
			{
				Marshal.ThrowExceptionForHR(hresult);
			}
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0004FDA0 File Offset: 0x0004DFA0
		internal static IProcessHostSupportFunctions CreateLocalSupportFunctions(IProcessHostSupportFunctions proxyFunctions)
		{
			IProcessHostSupportFunctions result = null;
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(proxyFunctions);
			if (IntPtr.Zero == iunknownForObject)
			{
				return null;
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				Guid guid = typeof(IProcessHostSupportFunctions).GUID;
				int num = Marshal.QueryInterface(iunknownForObject, ref guid, out zero);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				result = (IProcessHostSupportFunctions)Marshal.GetObjectForIUnknown(zero);
			}
			finally
			{
				if (IntPtr.Zero != zero)
				{
					Marshal.Release(zero);
				}
				if (IntPtr.Zero != iunknownForObject)
				{
					Marshal.Release(iunknownForObject);
				}
			}
			return result;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0004FE3C File Offset: 0x0004E03C
		internal static RegistryKey OpenAspNetRegKey(string subKey)
		{
			string text = VersionInfo.SystemWebVersion;
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.LastIndexOf('.');
				if (num > -1)
				{
					text = text.Substring(0, num + 1) + "0";
				}
			}
			string text2 = "Software\\Microsoft\\ASP.NET\\" + text;
			if (subKey != null)
			{
				text2 = text2 + "\\" + subKey;
			}
			return Registry.LocalMachine.OpenSubKey(text2);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0004FEA0 File Offset: 0x0004E0A0
		[RegistryPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static object GetAspNetRegValue(string subKey, string valueName, object defaultValue)
		{
			object result;
			try
			{
				using (RegistryKey registryKey = Misc.OpenAspNetRegKey(subKey))
				{
					if (registryKey == null)
					{
						result = defaultValue;
					}
					else
					{
						result = registryKey.GetValue(valueName, defaultValue);
					}
				}
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x040017D2 RID: 6098
		private const string APPLICATION_ID = "\r\n\r\nApplication ID: ";

		// Token: 0x040017D3 RID: 6099
		private const string PROCESS_ID = "\r\n\r\nProcess ID: ";

		// Token: 0x040017D4 RID: 6100
		private const string EXCEPTION = "\r\n\r\nException: ";

		// Token: 0x040017D5 RID: 6101
		private const string INNER_EXCEPTION = "\r\n\r\nInnerException: ";

		// Token: 0x040017D6 RID: 6102
		private const string MESSAGE = "\r\n\r\nMessage: ";

		// Token: 0x040017D7 RID: 6103
		private const string STACK_TRACE = "\r\n\r\nStackTrace: ";

		// Token: 0x040017D8 RID: 6104
		private static StringComparer s_caseInsensitiveInvariantKeyComparer;
	}
}
