using System;
using System.Collections.Generic;
using System.IO;

namespace Telerik.Web.UI
{
	// Token: 0x02000B0F RID: 2831
	internal class WebResourceWriter
	{
		// Token: 0x060069EA RID: 27114 RVA: 0x0018DDAC File Offset: 0x0018BFAC
		public WebResourceWriter()
		{
			this.ReadBufferSize = 4096;
		}

		// Token: 0x060069EB RID: 27115 RVA: 0x0018DDBF File Offset: 0x0018BFBF
		public WebResourceWriter(int readBufferSize)
		{
			this.ReadBufferSize = readBufferSize;
		}

		// Token: 0x060069EC RID: 27116 RVA: 0x0018DDD0 File Offset: 0x0018BFD0
		public void WriteResource(TextReader inputReader, TextWriter outputWriter, WebResourceNameEvaluator nameEvaluator)
		{
			char[] array = new char[this.ReadBufferSize];
			WebResourceRegex webResourceRegex = new WebResourceRegex();
			List<char[]> list = new List<char[]>();
			int num = 0;
			bool flag = false;
			int num2 = 0;
			int num3 = num2;
			for (;;)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					num = inputReader.Read(array, 0, this.ReadBufferSize);
					num2 = 0;
				}
				if (num == 0)
				{
					break;
				}
				CharArrayRegexMatchResult charArrayRegexMatchResult = webResourceRegex.Match(array, num2, num - 1);
				if (charArrayRegexMatchResult == CharArrayRegexMatchResult.InProgress)
				{
					char[] array2 = new char[num];
					Array.Copy(array, array2, num);
					list.Add(array2);
				}
				else if (charArrayRegexMatchResult == CharArrayRegexMatchResult.Success)
				{
					char[] array3 = new char[num];
					Array.Copy(array, array3, num);
					list.Add(array3);
					string webResourceName = new string(webResourceRegex.Name);
					string text = nameEvaluator(webResourceName);
					char[] array4 = text.ToCharArray();
					outputWriter.Write(list[0], num3, webResourceRegex.MatchStartIndex - num3);
					outputWriter.Write(array4, 0, array4.Length);
					flag = true;
					num2 = webResourceRegex.MatchEndIndex + 1;
					num3 = num2;
					list.Clear();
				}
				else if (charArrayRegexMatchResult == CharArrayRegexMatchResult.Fail)
				{
					for (int i = 0; i < list.Count; i++)
					{
						outputWriter.Write(list[i], num3, list[i].Length - num3);
						if (i == 0)
						{
							num3 = 0;
						}
					}
					list.Clear();
					outputWriter.Write(array, num3, webResourceRegex.MatchEndIndex - num3 + 1);
					flag = true;
					num2 = webResourceRegex.MatchEndIndex + 1;
					num3 = num2;
				}
				else if (charArrayRegexMatchResult == CharArrayRegexMatchResult.Pass)
				{
					outputWriter.Write(array, num3, num - num3);
					num3 = 0;
				}
			}
		}

		// Token: 0x04001CAF RID: 7343
		private readonly int ReadBufferSize;
	}
}
