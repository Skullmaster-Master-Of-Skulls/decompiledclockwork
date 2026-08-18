using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Hosting;
using Microsoft.Win32;

namespace System.Web.Util
{
	// Token: 0x0200076D RID: 1901
	internal sealed class Misc
	{
		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x06005C42 RID: 23618 RVA: 0x0017214C File Offset: 0x0017114C
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

		// Token: 0x06005C43 RID: 23619 RVA: 0x0017216C File Offset: 0x0017116C
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

		// Token: 0x06005C44 RID: 23620 RVA: 0x0017222C File Offset: 0x0017122C
		internal static void ReportUnhandledException(Exception e, string[] strings)
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
			UnsafeNativeMethods.ReportUnhandledException(stringBuilder.ToString());
		}

		// Token: 0x06005C45 RID: 23621 RVA: 0x001722D7 File Offset: 0x001712D7
		internal static void CopyMemory(IntPtr src, int srcOffset, byte[] dest, int destOffset, int size)
		{
			Marshal.Copy(new IntPtr(src.ToInt64() + (long)srcOffset), dest, destOffset, size);
		}

		// Token: 0x06005C46 RID: 23622 RVA: 0x001722F1 File Offset: 0x001712F1
		internal static void CopyMemory(byte[] src, int srcOffset, IntPtr dest, int destOffset, int size)
		{
			Marshal.Copy(src, srcOffset, new IntPtr(dest.ToInt64() + (long)destOffset), size);
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x0017230C File Offset: 0x0017130C
		internal unsafe static void CopyMemory(IntPtr src, int srcOffset, IntPtr dest, int destOffset, int size)
		{
			byte* src2 = (byte*)((void*)src) + srcOffset;
			byte* dest2 = (byte*)((void*)dest) + destOffset;
			StringUtil.memcpyimpl(src2, dest2, size);
		}

		// Token: 0x06005C48 RID: 23624 RVA: 0x00172334 File Offset: 0x00171334
		internal static void ThrowIfFailedHr(int hresult)
		{
			if (hresult < 0)
			{
				Marshal.ThrowExceptionForHR(hresult);
			}
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x00172340 File Offset: 0x00171340
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

		// Token: 0x06005C4A RID: 23626 RVA: 0x001723DC File Offset: 0x001713DC
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

		// Token: 0x06005C4B RID: 23627 RVA: 0x00172440 File Offset: 0x00171440
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

		// Token: 0x04003159 RID: 12633
		private const string APPLICATION_ID = "\r\n\r\nApplication ID: ";

		// Token: 0x0400315A RID: 12634
		private const string PROCESS_ID = "\r\n\r\nProcess ID: ";

		// Token: 0x0400315B RID: 12635
		private const string EXCEPTION = "\r\n\r\nException: ";

		// Token: 0x0400315C RID: 12636
		private const string INNER_EXCEPTION = "\r\n\r\nInnerException: ";

		// Token: 0x0400315D RID: 12637
		private const string MESSAGE = "\r\n\r\nMessage: ";

		// Token: 0x0400315E RID: 12638
		private const string STACK_TRACE = "\r\n\r\nStackTrace: ";

		// Token: 0x0400315F RID: 12639
		private static StringComparer s_caseInsensitiveInvariantKeyComparer;
	}
}
