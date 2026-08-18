using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.util;
using iTextSharp.text.xml.simpleparser;

namespace iTextSharp.text.pdf.hyphenation
{
	// Token: 0x0200021D RID: 541
	public class SimplePatternParser : ISimpleXMLDocHandler
	{
		// Token: 0x0600150A RID: 5386 RVA: 0x00076462 File Offset: 0x00075462
		public SimplePatternParser()
		{
			this.token = new StringBuilder();
			this.hyphenChar = '-';
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00076480 File Offset: 0x00075480
		public void Parse(Stream stream, IPatternConsumer consumer)
		{
			this.consumer = consumer;
			try
			{
				SimpleXMLParser.Parse(this, stream);
			}
			finally
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

		// Token: 0x0600150C RID: 5388 RVA: 0x000764C8 File Offset: 0x000754C8
		protected static string GetPattern(string word)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int length = word.Length;
			for (int i = 0; i < length; i++)
			{
				if (!char.IsDigit(word[i]))
				{
					stringBuilder.Append(word[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00076510 File Offset: 0x00075510
		protected List<object> NormalizeException(List<object> ex)
		{
			List<object> list = new List<object>();
			for (int i = 0; i < ex.Count; i++)
			{
				object obj = ex[i];
				if (obj is string)
				{
					string text = (string)obj;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (char c in text)
					{
						if (c != this.hyphenChar)
						{
							stringBuilder.Append(c);
						}
						else
						{
							list.Add(stringBuilder.ToString());
							stringBuilder.Length = 0;
							list.Add(new Hyphen(new string(new char[]
							{
								this.hyphenChar
							}), null, null));
						}
					}
					if (stringBuilder.Length > 0)
					{
						list.Add(stringBuilder.ToString());
					}
				}
				else
				{
					list.Add(obj);
				}
			}
			return list;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x000765EC File Offset: 0x000755EC
		protected string GetExceptionWord(List<object> ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < ex.Count; i++)
			{
				object obj = ex[i];
				if (obj is string)
				{
					stringBuilder.Append((string)obj);
				}
				else if (((Hyphen)obj).noBreak != null)
				{
					stringBuilder.Append(((Hyphen)obj).noBreak);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00076654 File Offset: 0x00075654
		protected static string GetInterletterValues(string pat)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = pat + "a";
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (char.IsDigit(c))
				{
					stringBuilder.Append(c);
					i++;
				}
				else
				{
					stringBuilder.Append('0');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x000766B6 File Offset: 0x000756B6
		public void EndDocument()
		{
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000766B8 File Offset: 0x000756B8
		public void EndElement(string tag)
		{
			if (this.token.Length > 0)
			{
				string text = this.token.ToString();
				switch (this.currElement)
				{
				case 1:
					this.consumer.AddClass(text);
					break;
				case 2:
					this.exception.Add(text);
					this.exception = this.NormalizeException(this.exception);
					this.consumer.AddException(this.GetExceptionWord(this.exception), new List<object>(this.exception));
					break;
				case 3:
					this.consumer.AddPattern(SimplePatternParser.GetPattern(text), SimplePatternParser.GetInterletterValues(text));
					break;
				}
				if (this.currElement != 4)
				{
					this.token.Length = 0;
				}
			}
			if (this.currElement == 4)
			{
				this.currElement = 2;
				return;
			}
			this.currElement = 0;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00076797 File Offset: 0x00075797
		public void StartDocument()
		{
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0007679C File Offset: 0x0007579C
		public void StartElement(string tag, Dictionary<string, string> h)
		{
			if (tag.Equals("hyphen-char"))
			{
				string text;
				h.TryGetValue("value", out text);
				if (text != null && text.Length == 1)
				{
					this.hyphenChar = text[0];
				}
			}
			else if (tag.Equals("classes"))
			{
				this.currElement = 1;
			}
			else if (tag.Equals("patterns"))
			{
				this.currElement = 3;
			}
			else if (tag.Equals("exceptions"))
			{
				this.currElement = 2;
				this.exception = new List<object>();
			}
			else if (tag.Equals("hyphen"))
			{
				if (this.token.Length > 0)
				{
					this.exception.Add(this.token.ToString());
				}
				this.exception.Add(new Hyphen(h["pre"], h["no"], h["post"]));
				this.currElement = 4;
			}
			this.token.Length = 0;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x000768B0 File Offset: 0x000758B0
		public void Text(string str)
		{
			StringTokenizer stringTokenizer = new StringTokenizer(str);
			while (stringTokenizer.HasMoreTokens())
			{
				string text = stringTokenizer.NextToken();
				switch (this.currElement)
				{
				case 1:
					this.consumer.AddClass(text);
					break;
				case 2:
					this.exception.Add(text);
					this.exception = this.NormalizeException(this.exception);
					this.consumer.AddException(this.GetExceptionWord(this.exception), new List<object>(this.exception));
					this.exception.Clear();
					break;
				case 3:
					this.consumer.AddPattern(SimplePatternParser.GetPattern(text), SimplePatternParser.GetInterletterValues(text));
					break;
				}
			}
		}

		// Token: 0x04000E39 RID: 3641
		internal const int ELEM_CLASSES = 1;

		// Token: 0x04000E3A RID: 3642
		internal const int ELEM_EXCEPTIONS = 2;

		// Token: 0x04000E3B RID: 3643
		internal const int ELEM_PATTERNS = 3;

		// Token: 0x04000E3C RID: 3644
		internal const int ELEM_HYPHEN = 4;

		// Token: 0x04000E3D RID: 3645
		internal int currElement;

		// Token: 0x04000E3E RID: 3646
		internal IPatternConsumer consumer;

		// Token: 0x04000E3F RID: 3647
		internal StringBuilder token;

		// Token: 0x04000E40 RID: 3648
		internal List<object> exception;

		// Token: 0x04000E41 RID: 3649
		internal char hyphenChar;
	}
}
