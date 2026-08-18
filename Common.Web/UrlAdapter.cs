using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Common.Web
{
	// Token: 0x02000003 RID: 3
	public static class UrlAdapter
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020B8 File Offset: 0x000002B8
		public static bool AvailableUrl(string url)
		{
			bool result;
			try
			{
				using (MyClient myClient = new MyClient())
				{
					myClient.HeadOnly = true;
					myClient.DownloadString(url);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002114 File Offset: 0x00000314
		public static Image GetImageFromUrl(this string url)
		{
			Image result2;
			try
			{
				HttpClient httpClient = new HttpClient();
				byte[] result = httpClient.GetByteArrayAsync(url).Result;
				bool flag = result == null || result.Length == 0;
				if (flag)
				{
					result2 = null;
				}
				else
				{
					result2 = Image.FromStream(new MemoryStream(result));
				}
			}
			catch (Exception)
			{
				result2 = null;
			}
			return result2;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002170 File Offset: 0x00000370
		[DebuggerStepThrough]
		public static Task<Image> GetImageFromUrlAsync(this string url)
		{
			UrlAdapter.<GetImageFromUrlAsync>d__2 <GetImageFromUrlAsync>d__ = new UrlAdapter.<GetImageFromUrlAsync>d__2();
			<GetImageFromUrlAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Image>.Create();
			<GetImageFromUrlAsync>d__.url = url;
			<GetImageFromUrlAsync>d__.<>1__state = -1;
			<GetImageFromUrlAsync>d__.<>t__builder.Start<UrlAdapter.<GetImageFromUrlAsync>d__2>(ref <GetImageFromUrlAsync>d__);
			return <GetImageFromUrlAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B4 File Offset: 0x000003B4
		public static T GetObjectFromWeb<T>(this string uri) where T : class
		{
			UrlAdapter.<>c__DisplayClass3_0<T> CS$<>8__locals1 = new UrlAdapter.<>c__DisplayClass3_0<T>();
			CS$<>8__locals1.uri = uri;
			return Task.Run<T>(delegate()
			{
				UrlAdapter.<>c__DisplayClass3_0<T>.<<GetObjectFromWeb>b__0>d <<GetObjectFromWeb>b__0>d = new UrlAdapter.<>c__DisplayClass3_0<T>.<<GetObjectFromWeb>b__0>d();
				<<GetObjectFromWeb>b__0>d.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
				<<GetObjectFromWeb>b__0>d.<>4__this = CS$<>8__locals1;
				<<GetObjectFromWeb>b__0>d.<>1__state = -1;
				<<GetObjectFromWeb>b__0>d.<>t__builder.Start<UrlAdapter.<>c__DisplayClass3_0<T>.<<GetObjectFromWeb>b__0>d>(ref <<GetObjectFromWeb>b__0>d);
				return <<GetObjectFromWeb>b__0>d.<>t__builder.Task;
			}).Result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021EC File Offset: 0x000003EC
		[DebuggerStepThrough]
		public static Task<T> GetObjectFromWebAsync<T>(this string uri) where T : class
		{
			UrlAdapter.<GetObjectFromWebAsync>d__4<T> <GetObjectFromWebAsync>d__ = new UrlAdapter.<GetObjectFromWebAsync>d__4<T>();
			<GetObjectFromWebAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<GetObjectFromWebAsync>d__.uri = uri;
			<GetObjectFromWebAsync>d__.<>1__state = -1;
			<GetObjectFromWebAsync>d__.<>t__builder.Start<UrlAdapter.<GetObjectFromWebAsync>d__4<T>>(ref <GetObjectFromWebAsync>d__);
			return <GetObjectFromWebAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002230 File Offset: 0x00000430
		public static byte[] GetImageBytes(this string url)
		{
			byte[] result2;
			try
			{
				HttpClient httpClient = new HttpClient();
				byte[] result = httpClient.GetByteArrayAsync(url).Result;
				result2 = ((result == null || result.Length == 0) ? null : result);
			}
			catch
			{
				result2 = null;
			}
			return result2;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002278 File Offset: 0x00000478
		[DebuggerStepThrough]
		public static Task<byte[]> GetImageBytesAsync(this string url)
		{
			UrlAdapter.<GetImageBytesAsync>d__6 <GetImageBytesAsync>d__ = new UrlAdapter.<GetImageBytesAsync>d__6();
			<GetImageBytesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			<GetImageBytesAsync>d__.url = url;
			<GetImageBytesAsync>d__.<>1__state = -1;
			<GetImageBytesAsync>d__.<>t__builder.Start<UrlAdapter.<GetImageBytesAsync>d__6>(ref <GetImageBytesAsync>d__);
			return <GetImageBytesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022BC File Offset: 0x000004BC
		private static string GetTempFileName(string extension)
		{
			string path = string.Format("{0}_{1}{2}", Guid.NewGuid().ToString(), DateTime.Now.Millisecond.ToString(), extension);
			string text = Path.GetTempPath();
			text = Path.Combine(text, "TechnoPro");
			text = Path.Combine(text, "ClockWork");
			bool flag = !Directory.Exists(text);
			if (flag)
			{
				Directory.CreateDirectory(text);
			}
			return Path.Combine(text, path);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002340 File Offset: 0x00000540
		public static string UrlEncode(this string s)
		{
			return WebUtility.UrlEncode(s);
		}
	}
}
