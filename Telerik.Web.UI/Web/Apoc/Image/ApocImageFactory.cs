using System;
using System.IO;
using System.Net;
using System.Security;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Web.Apoc.Configuration;

namespace Telerik.Web.Apoc.Image
{
	// Token: 0x020015CE RID: 5582
	internal class ApocImageFactory
	{
		// Token: 0x0600D98A RID: 55690 RVA: 0x002FBA14 File Offset: 0x002F9C14
		public static string GetTemporaryDir()
		{
			string text = Path.GetTempPath();
			if (string.IsNullOrEmpty(text))
			{
				foreach (string text2 in ApocImageFactory.tempDirEnvVars)
				{
					text = Environment.GetEnvironmentVariable(text2);
					if (!string.IsNullOrEmpty(text2))
					{
						break;
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = "/tmp";
				}
			}
			return text;
		}

		// Token: 0x0600D98B RID: 55691 RVA: 0x002FBA68 File Offset: 0x002F9C68
		public static ApocImage Make(string href)
		{
			if (ApocDriver.ActiveDriver.ImageHandler != null)
			{
				byte[] array = ApocDriver.ActiveDriver.ImageHandler(href);
				if (array != null)
				{
					return new ApocImage(href, array);
				}
			}
			Uri uri = null;
			UriSpecificationParser uriSpecificationParser = new UriSpecificationParser(href);
			string text = uriSpecificationParser.Uri;
			HttpContext httpContext = HttpContext.Current;
			string pattern = "^data:image/.*;base64,";
			string value = Regex.Match(text, pattern).Value;
			if (value != string.Empty)
			{
				byte[] imageData = new byte[text.Length];
				imageData = Convert.FromBase64String(text.Replace(value, string.Empty));
				return new ApocImage(href, imageData);
			}
			if ((text.Contains(".axd") || text.Contains(".ashx")) && !Regex.IsMatch(text, "^https?://"))
			{
				string text2 = httpContext.Request.Url.ToString();
				string text3 = text2.Substring(0, text2.LastIndexOf("/") + 1);
				text = ((text.StartsWith("~/") || text.StartsWith("..")) ? text.Substring(2) : text);
				text = ((text3.EndsWith("/") && text.StartsWith("/")) ? (text3 + text.Substring(1)) : (text3 + text));
			}
			if (httpContext != null && Regex.IsMatch(text, "^file://"))
			{
				uri = new Uri(text);
				return new ApocImage(uri.AbsoluteUri, ApocImageFactory.ExtractImageData(uri));
			}
			string arg;
			if (httpContext != null && !Regex.IsMatch(text, "^https?://") && ApocImageFactory.TryMapPath(text, out arg))
			{
				uri = new Uri(string.Format("file://{0}", arg));
				return new ApocImage(uri.AbsoluteUri, ApocImageFactory.ExtractImageData(uri));
			}
			try
			{
				uri = new Uri(text);
			}
			catch
			{
				if (File.Exists(text))
				{
					uri = new Uri("file://" + Path.Combine(Directory.GetCurrentDirectory(), text));
				}
				else
				{
					string stringValue = Configuration.GetStringValue("baseDir");
					if (stringValue == null)
					{
						throw new ApocImageException("Unable to locate " + text + " : no base directory is specified");
					}
					string text4 = Path.Combine(stringValue, text);
					if (!File.Exists(text4))
					{
						throw new ApocImageException("Unable to retrieve graphic from " + text);
					}
					uri = new Uri("file://" + Path.Combine(Directory.GetCurrentDirectory(), text4));
				}
			}
			return new ApocImage(uri.AbsoluteUri, ApocImageFactory.ExtractImageData(uri));
		}

		// Token: 0x0600D98C RID: 55692 RVA: 0x002FBCDC File Offset: 0x002F9EDC
		private static bool TryMapPath(string path, out string physPath)
		{
			try
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					physPath = httpContext.Server.MapPath(path);
				}
				else
				{
					physPath = string.Empty;
				}
			}
			catch (InvalidOperationException)
			{
				if (path.StartsWith("/"))
				{
					return ApocImageFactory.TryMapPath("~" + path, out physPath);
				}
				physPath = string.Empty;
			}
			return physPath != string.Empty;
		}

		// Token: 0x0600D98D RID: 55693 RVA: 0x002FBD54 File Offset: 0x002F9F54
		private static Stream GetImageStream(Uri uri)
		{
			Stream responseStream;
			try
			{
				WebRequest webRequest = WebRequest.CreateDefault(uri);
				int intValue = Configuration.GetIntValue("timeout");
				if (intValue != -1)
				{
					webRequest.Timeout = intValue;
				}
				if (!ApocDriver.ActiveDriver.Credentials.GetEnumerator().MoveNext())
				{
					webRequest.Credentials = CredentialCache.DefaultCredentials;
				}
				else
				{
					webRequest.Credentials = ApocDriver.ActiveDriver.Credentials;
				}
				WebResponse response = webRequest.GetResponse();
				responseStream = response.GetResponseStream();
			}
			catch (SecurityException ex)
			{
				throw new ApocImageException(string.Format("Detected security exception while fetching image from {0}: {1}", uri, ex.Message));
			}
			catch (UriFormatException ex2)
			{
				throw new ApocImageException(string.Format("Badly formed Uri {0}: {1}", uri, ex2.Message));
			}
			catch (WebException ex3)
			{
				throw new ApocImageException(string.Format("Encountered web exception while fetching image from {0}: {1}", uri, ex3.Message));
			}
			catch (Exception ex4)
			{
				throw new ApocImageException(string.Format("Encountered unexpected exception while fetching image from {0}: {1}", uri, ex4.Message));
			}
			return responseStream;
		}

		// Token: 0x0600D98E RID: 55694 RVA: 0x002FBE60 File Offset: 0x002FA060
		private static byte[] ExtractImageData(Uri absoluteURL)
		{
			Stream stream = ApocImageFactory.GetImageStream(absoluteURL);
			byte[] result;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] buffer = new byte[4096];
				int count;
				while ((count = stream.Read(buffer, 0, 4096)) != 0)
				{
					memoryStream.Write(buffer, 0, count);
				}
				memoryStream.Flush();
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			finally
			{
				stream.Flush();
				stream.Close();
				stream = null;
			}
			return result;
		}

		// Token: 0x04003C2B RID: 15403
		private static string[] tempDirEnvVars = new string[]
		{
			"Temp",
			"TMP",
			"TEMP"
		};
	}
}
