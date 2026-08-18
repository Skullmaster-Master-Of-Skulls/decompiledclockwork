using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AjaxControlToolkit
{
	// Token: 0x02000029 RID: 41
	public class MultipartFormDataParser
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00006148 File Offset: 0x00004348
		public static FileHeaderInfo ParseHeaderInfo(byte[] bytes, Encoding encoding)
		{
			MultipartFormDataParser.MultipartFormData multipartFormData = MultipartFormDataParser.Parse(bytes, encoding);
			FileHeaderInfo result = null;
			foreach (string text in multipartFormData.Boundaries)
			{
				if (text.Contains("Content-Disposition"))
				{
					Match match = new Regex("(?<=name\\=\\\")(.*?)(?=\\\")").Match(text);
					Match match2 = new Regex("(?<=filename\\=\\\")(.*?)(?=\\\")").Match(text);
					if (match.Success && match2.Success && match.Value == "act-file-data")
					{
						Regex regex = new Regex("(?<=Content\\-Type:)(.*?)(?=\r\n\r\n)");
						Match match3 = regex.Match(multipartFormData.Source);
						if (!match3.Success)
						{
							return null;
						}
						int index = match3.Index;
						int length = match3.Length;
						int length2 = "\r\n\r\n".Length;
						int startIndex = MultipartFormDataParser.GetContentTypeIndex(bytes, encoding, match3.Value) + match3.Length + "\r\n\r\n".Length;
						result = new FileHeaderInfo
						{
							StartIndex = startIndex,
							BoundaryDelimiterLength = ("\r\n" + multipartFormData.Delimiter + "\r\n\r\n").Length,
							FileName = match2.Value.Trim(),
							ContentType = match3.Value.Trim()
						};
					}
				}
			}
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000062B0 File Offset: 0x000044B0
		private static int GetContentTypeIndex(byte[] bytes, Encoding encoding, string contentTypeString)
		{
			byte[] bytes2 = encoding.GetBytes(contentTypeString);
			return bytes.StartingIndex(bytes2).First<int>();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000062D4 File Offset: 0x000044D4
		private static MultipartFormDataParser.MultipartFormData Parse(byte[] bytes, Encoding encoding)
		{
			string @string = encoding.GetString(bytes);
			int num = @string.IndexOf("\r\n");
			if (num < 0)
			{
				return null;
			}
			string text = @string.Substring(0, num);
			string[] boundaries = @string.Split(new string[]
			{
				text
			}, StringSplitOptions.RemoveEmptyEntries);
			return new MultipartFormDataParser.MultipartFormData
			{
				Boundaries = boundaries,
				Delimiter = text,
				Source = @string
			};
		}

		// Token: 0x04000076 RID: 118
		private const string EOF = "\r\n";

		// Token: 0x0200002A RID: 42
		private class MultipartFormData
		{
			// Token: 0x1700008B RID: 139
			// (get) Token: 0x0600019A RID: 410 RVA: 0x00006344 File Offset: 0x00004544
			// (set) Token: 0x0600019B RID: 411 RVA: 0x0000634C File Offset: 0x0000454C
			internal string[] Boundaries { get; set; }

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x0600019C RID: 412 RVA: 0x00006355 File Offset: 0x00004555
			// (set) Token: 0x0600019D RID: 413 RVA: 0x0000635D File Offset: 0x0000455D
			internal string Source { get; set; }

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x0600019E RID: 414 RVA: 0x00006366 File Offset: 0x00004566
			// (set) Token: 0x0600019F RID: 415 RVA: 0x0000636E File Offset: 0x0000456E
			internal string Delimiter { get; set; }
		}
	}
}
