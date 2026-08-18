using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.util;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200063E RID: 1598
	public class GlyphList
	{
		// Token: 0x0600360F RID: 13839 RVA: 0x0014F85C File Offset: 0x0014E85C
		static GlyphList()
		{
			Stream stream = null;
			try
			{
				stream = BaseFont.GetResourceStream("iTextSharp.text.pdf.fonts.glyphlist.txt");
				if (stream == null)
				{
					string message = "glyphlist.txt not found as resource.";
					throw new Exception(message);
				}
				byte[] array = new byte[1024];
				MemoryStream memoryStream = new MemoryStream();
				for (;;)
				{
					int num = stream.Read(array, 0, array.Length);
					if (num == 0)
					{
						break;
					}
					memoryStream.Write(array, 0, num);
				}
				stream.Close();
				stream = null;
				string str = PdfEncodings.ConvertToString(memoryStream.ToArray(), null);
				StringTokenizer stringTokenizer = new StringTokenizer(str, "\r\n");
				while (stringTokenizer.HasMoreTokens())
				{
					string text = stringTokenizer.NextToken();
					if (!text.StartsWith("#"))
					{
						StringTokenizer stringTokenizer2 = new StringTokenizer(text, " ;\r\n\t\f");
						if (stringTokenizer2.HasMoreTokens())
						{
							string text2 = stringTokenizer2.NextToken();
							if (stringTokenizer2.HasMoreTokens())
							{
								string s = stringTokenizer2.NextToken();
								int num2 = int.Parse(s, NumberStyles.HexNumber);
								GlyphList.unicode2names[num2] = text2;
								GlyphList.names2unicode[text2] = new int[]
								{
									num2
								};
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("glyphlist.txt loading error: " + ex.Message);
			}
			finally
			{
				if (stream != null)
				{
					try
					{
						stream.Close();
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x0014FA04 File Offset: 0x0014EA04
		public static int[] NameToUnicode(string name)
		{
			int[] result;
			GlyphList.names2unicode.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x0014FA20 File Offset: 0x0014EA20
		public static string UnicodeToName(int num)
		{
			string result;
			GlyphList.unicode2names.TryGetValue(num, out result);
			return result;
		}

		// Token: 0x04002450 RID: 9296
		private static Dictionary<int, string> unicode2names = new Dictionary<int, string>();

		// Token: 0x04002451 RID: 9297
		private static Dictionary<string, int[]> names2unicode = new Dictionary<string, int[]>();
	}
}
