using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace System.EnterpriseServices.Internal
{
	// Token: 0x020000F7 RID: 247
	internal static class SoapServerInfo
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x000134C0 File Offset: 0x000124C0
		internal static bool BoolFromString(string inVal, bool inDefault)
		{
			if (inVal == null)
			{
				return inDefault;
			}
			string a = inVal.ToLower(CultureInfo.InvariantCulture);
			bool result = inDefault;
			if (a == "true")
			{
				result = true;
			}
			if (a == "false")
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00013500 File Offset: 0x00012500
		internal static string ServerPhysicalPath(string rootWebServer, string inBaseUrl, string inVirtualRoot, bool createDir)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			SoapServerInfo.ParseUrl(inBaseUrl, inVirtualRoot, "", out text2, out text3);
			if (text3.Length <= 0)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(1024, 1024);
			uint uSize = 1024U;
			if (SoapServerInfo.GetSystemDirectory(stringBuilder, uSize) == 0U)
			{
				throw new ServicedComponentException(Resource.FormatString("Soap_GetSystemDirectoryFailure"));
			}
			if (stringBuilder.ToString().Length <= 0)
			{
				return text;
			}
			text = stringBuilder.ToString() + "\\com\\SoapVRoots\\" + text3;
			if (createDir)
			{
				string path = text + "\\bin";
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			}
			return text;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000135B0 File Offset: 0x000125B0
		internal static void CheckUrl(string inBaseUrl, string inVirtualRoot, string inProtocol)
		{
			string text = inBaseUrl;
			if (text.Length <= 0)
			{
				text = inProtocol + "://";
				text += Dns.GetHostName();
				text += "/";
			}
			Uri uri = new Uri(text);
			int upperBound = uri.Segments.GetUpperBound(0);
			Uri uri2 = new Uri(uri, inVirtualRoot);
			if (uri2.Segments.GetUpperBound(0) > upperBound + 1)
			{
				throw new NonRootVRootException();
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00013620 File Offset: 0x00012620
		internal static void ParseUrl(string inBaseUrl, string inVirtualRoot, string inProtocol, out string baseUrl, out string virtualRoot)
		{
			string text = "https";
			if (inProtocol.ToLower(CultureInfo.InvariantCulture) == "http")
			{
				text = inProtocol;
			}
			baseUrl = inBaseUrl;
			if (baseUrl.Length <= 0)
			{
				baseUrl = text + "://";
				baseUrl += Dns.GetHostName();
				baseUrl += "/";
			}
			Uri baseUri = new Uri(baseUrl);
			Uri uri = new Uri(baseUri, inVirtualRoot);
			if (uri.Scheme != text)
			{
				UriBuilder uriBuilder = new UriBuilder(uri.AbsoluteUri);
				uriBuilder.Scheme = text;
				if (text == "https" && uriBuilder.Port == 80)
				{
					uriBuilder.Port = 443;
				}
				if (text == "http" && uriBuilder.Port == 443)
				{
					uriBuilder.Port = 80;
				}
				uri = uriBuilder.Uri;
			}
			string[] segments = uri.Segments;
			virtualRoot = segments[segments.GetUpperBound(0)];
			baseUrl = uri.AbsoluteUri.Substring(0, uri.AbsoluteUri.Length - virtualRoot.Length);
			char[] trimChars = new char[]
			{
				'/'
			};
			virtualRoot = virtualRoot.TrimEnd(trimChars);
		}

		// Token: 0x0600057F RID: 1407
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint GetSystemDirectory(StringBuilder lpBuf, uint uSize);
	}
}
