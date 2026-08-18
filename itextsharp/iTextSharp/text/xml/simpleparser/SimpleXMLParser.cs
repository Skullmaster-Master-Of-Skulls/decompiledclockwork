using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.xml.simpleparser
{
	// Token: 0x02000634 RID: 1588
	public sealed class SimpleXMLParser
	{
		// Token: 0x060035B3 RID: 13747 RVA: 0x0014C6A4 File Offset: 0x0014B6A4
		private SimpleXMLParser(ISimpleXMLDocHandler doc, ISimpleXMLDocHandlerComment comment, bool html)
		{
			this.doc = doc;
			this.comment = comment;
			this.html = html;
			this.stack = new Stack<int>();
			this.state = (html ? 1 : 0);
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x0014C710 File Offset: 0x0014B710
		private void Go(TextReader reader)
		{
			this.doc.StartDocument();
			for (;;)
			{
				if (this.previousCharacter == -1)
				{
					this.character = reader.Read();
				}
				else
				{
					this.character = this.previousCharacter;
					this.previousCharacter = -1;
				}
				if (this.character == -1)
				{
					break;
				}
				if (this.character == 10 && this.eol)
				{
					this.eol = false;
				}
				else
				{
					if (this.eol)
					{
						this.eol = false;
					}
					else if (this.character == 10)
					{
						this.lines++;
						this.columns = 0;
					}
					else if (this.character == 13)
					{
						this.eol = true;
						this.character = 10;
						this.lines++;
						this.columns = 0;
					}
					else
					{
						this.columns++;
					}
					switch (this.state)
					{
					case 0:
						if (this.character == 60)
						{
							this.SaveState(1);
							this.state = 2;
						}
						break;
					case 1:
						if (this.character == 60)
						{
							this.Flush();
							this.SaveState(this.state);
							this.state = 2;
						}
						else if (this.character == 38)
						{
							this.SaveState(this.state);
							this.entity.Length = 0;
							this.state = 10;
							this.nowhite = true;
						}
						else if (char.IsWhiteSpace((char)this.character))
						{
							if (this.nowhite)
							{
								this.text.Append((char)this.character);
							}
							this.nowhite = false;
						}
						else
						{
							this.text.Append((char)this.character);
							this.nowhite = true;
						}
						break;
					case 2:
						this.InitTag();
						if (this.character == 47)
						{
							this.state = 5;
						}
						else if (this.character == 63)
						{
							this.RestoreState();
							this.state = 9;
						}
						else
						{
							this.text.Append((char)this.character);
							this.state = 3;
						}
						break;
					case 3:
						if (this.character == 62)
						{
							this.DoTag();
							this.ProcessTag(true);
							this.InitTag();
							this.state = this.RestoreState();
						}
						else if (this.character == 47)
						{
							this.state = 6;
						}
						else if (this.character == 45 && this.text.ToString().Equals("!-"))
						{
							this.Flush();
							this.state = 8;
						}
						else if (this.character == 91 && this.text.ToString().Equals("![CDATA"))
						{
							this.Flush();
							this.state = 7;
						}
						else if (this.character == 69 && this.text.ToString().Equals("!DOCTYP"))
						{
							this.Flush();
							this.state = 9;
						}
						else if (char.IsWhiteSpace((char)this.character))
						{
							this.DoTag();
							this.state = 4;
						}
						else
						{
							this.text.Append((char)this.character);
						}
						break;
					case 4:
						if (this.character == 62)
						{
							this.ProcessTag(true);
							this.InitTag();
							this.state = this.RestoreState();
						}
						else if (this.character == 47)
						{
							this.state = 6;
						}
						else if (!char.IsWhiteSpace((char)this.character))
						{
							this.text.Append((char)this.character);
							this.state = 12;
						}
						break;
					case 5:
						if (this.character == 62)
						{
							this.DoTag();
							this.ProcessTag(false);
							if (!this.html && this.nested == 0)
							{
								return;
							}
							this.state = this.RestoreState();
						}
						else if (!char.IsWhiteSpace((char)this.character))
						{
							this.text.Append((char)this.character);
						}
						break;
					case 6:
						if (this.character != 62)
						{
							this.ThrowException(MessageLocalization.GetComposedMessage("expected.gt.for.tag.lt.1.gt", this.tag));
						}
						this.DoTag();
						this.ProcessTag(true);
						this.ProcessTag(false);
						this.InitTag();
						if (!this.html && this.nested == 0)
						{
							goto Block_37;
						}
						this.state = this.RestoreState();
						break;
					case 7:
						if (this.character == 62 && this.text.ToString().EndsWith("]]"))
						{
							this.text.Length = this.text.Length - 2;
							this.Flush();
							this.state = this.RestoreState();
						}
						else
						{
							this.text.Append((char)this.character);
						}
						break;
					case 8:
						if (this.character == 62 && this.text.ToString().EndsWith("--"))
						{
							this.text.Length = this.text.Length - 2;
							this.Flush();
							this.state = this.RestoreState();
						}
						else
						{
							this.text.Append((char)this.character);
						}
						break;
					case 9:
						if (this.character == 62)
						{
							this.state = this.RestoreState();
							if (this.state == 1)
							{
								this.state = 0;
							}
						}
						break;
					case 10:
						if (this.character == 59)
						{
							this.state = this.RestoreState();
							string text = this.entity.ToString();
							this.entity.Length = 0;
							char c = EntitiesToUnicode.DecodeEntity(text);
							if (c == '\0')
							{
								this.text.Append('&').Append(text).Append(';');
							}
							else
							{
								this.text.Append(c);
							}
						}
						else if ((this.character != 35 && (this.character < 48 || this.character > 57) && (this.character < 97 || this.character > 122) && (this.character < 65 || this.character > 90)) || this.entity.Length >= 7)
						{
							this.state = this.RestoreState();
							this.previousCharacter = this.character;
							this.text.Append('&').Append(this.entity.ToString());
							this.entity.Length = 0;
						}
						else
						{
							this.entity.Append((char)this.character);
						}
						break;
					case 11:
						if (this.html && this.quoteCharacter == 32 && this.character == 62)
						{
							this.Flush();
							this.ProcessTag(true);
							this.InitTag();
							this.state = this.RestoreState();
						}
						else if (this.html && this.quoteCharacter == 32 && char.IsWhiteSpace((char)this.character))
						{
							this.Flush();
							this.state = 4;
						}
						else if (this.html && this.quoteCharacter == 32)
						{
							this.text.Append((char)this.character);
						}
						else if (this.character == this.quoteCharacter)
						{
							this.Flush();
							this.state = 4;
						}
						else if (" \r\n\t".IndexOf((char)this.character) >= 0)
						{
							this.text.Append(' ');
						}
						else if (this.character == 38)
						{
							this.SaveState(this.state);
							this.state = 10;
							this.entity.Length = 0;
						}
						else
						{
							this.text.Append((char)this.character);
						}
						break;
					case 12:
						if (char.IsWhiteSpace((char)this.character))
						{
							this.Flush();
							this.state = 13;
						}
						else if (this.character == 61)
						{
							this.Flush();
							this.state = 14;
						}
						else if (this.html && this.character == 62)
						{
							this.text.Length = 0;
							this.ProcessTag(true);
							this.InitTag();
							this.state = this.RestoreState();
						}
						else
						{
							this.text.Append((char)this.character);
						}
						break;
					case 13:
						if (this.character == 61)
						{
							this.state = 14;
						}
						else if (!char.IsWhiteSpace((char)this.character))
						{
							if (this.html && this.character == 62)
							{
								this.text.Length = 0;
								this.ProcessTag(true);
								this.InitTag();
								this.state = this.RestoreState();
							}
							else if (this.html && this.character == 47)
							{
								this.Flush();
								this.state = 6;
							}
							else if (this.html)
							{
								this.Flush();
								this.text.Append((char)this.character);
								this.state = 12;
							}
							else
							{
								this.ThrowException(MessageLocalization.GetComposedMessage("error.in.attribute.processing"));
							}
						}
						break;
					case 14:
						if (this.character == 34 || this.character == 39)
						{
							this.quoteCharacter = this.character;
							this.state = 11;
						}
						else if (!char.IsWhiteSpace((char)this.character))
						{
							if (this.html && this.character == 62)
							{
								this.Flush();
								this.ProcessTag(true);
								this.InitTag();
								this.state = this.RestoreState();
							}
							else if (this.html)
							{
								this.text.Append((char)this.character);
								this.quoteCharacter = 32;
								this.state = 11;
							}
							else
							{
								this.ThrowException(MessageLocalization.GetComposedMessage("error.in.attribute.processing"));
							}
						}
						break;
					}
				}
			}
			if (this.html)
			{
				if (this.html && this.state == 1)
				{
					this.Flush();
				}
				this.doc.EndDocument();
				return;
			}
			this.ThrowException(MessageLocalization.GetComposedMessage("missing.end.tag"));
			return;
			Block_37:
			this.doc.EndDocument();
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x0014D149 File Offset: 0x0014C149
		private int RestoreState()
		{
			if (this.stack.Count != 0)
			{
				return this.stack.Pop();
			}
			return 0;
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x0014D165 File Offset: 0x0014C165
		private void SaveState(int s)
		{
			this.stack.Push(s);
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x0014D174 File Offset: 0x0014C174
		private void Flush()
		{
			int num = this.state;
			if (num != 1)
			{
				switch (num)
				{
				case 7:
					break;
				case 8:
					if (this.comment != null)
					{
						this.comment.Comment(this.text.ToString());
						goto IL_DA;
					}
					goto IL_DA;
				case 9:
				case 10:
				case 13:
					goto IL_DA;
				case 11:
				case 14:
					this.attributevalue = this.text.ToString();
					this.attributes[this.attributekey] = this.attributevalue;
					goto IL_DA;
				case 12:
					this.attributekey = this.text.ToString();
					if (this.html)
					{
						this.attributekey = this.attributekey.ToLower(CultureInfo.InvariantCulture);
						goto IL_DA;
					}
					goto IL_DA;
				default:
					goto IL_DA;
				}
			}
			if (this.text.Length > 0)
			{
				this.doc.Text(this.text.ToString());
			}
			IL_DA:
			this.text.Length = 0;
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x0014D267 File Offset: 0x0014C267
		private void InitTag()
		{
			this.tag = null;
			this.attributes = new Dictionary<string, string>();
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x0014D27C File Offset: 0x0014C27C
		private void DoTag()
		{
			if (this.tag == null)
			{
				this.tag = this.text.ToString();
			}
			if (this.html)
			{
				this.tag = this.tag.ToLower(CultureInfo.InvariantCulture);
			}
			this.text.Length = 0;
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x0014D2CC File Offset: 0x0014C2CC
		private void ProcessTag(bool start)
		{
			if (start)
			{
				this.nested++;
				this.doc.StartElement(this.tag, this.attributes);
				return;
			}
			this.nested--;
			this.doc.EndElement(this.tag);
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x0014D321 File Offset: 0x0014C321
		private void ThrowException(string s)
		{
			throw new IOException(MessageLocalization.GetComposedMessage("1.near.line.2.column.3", s, this.lines, this.columns));
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x0014D34C File Offset: 0x0014C34C
		public static void Parse(ISimpleXMLDocHandler doc, ISimpleXMLDocHandlerComment comment, TextReader r, bool html)
		{
			SimpleXMLParser simpleXMLParser = new SimpleXMLParser(doc, comment, html);
			simpleXMLParser.Go(r);
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x0014D36C File Offset: 0x0014C36C
		public static void Parse(ISimpleXMLDocHandler doc, Stream inp)
		{
			byte[] array = new byte[4];
			int num = inp.Read(array, 0, array.Length);
			if (num != 4)
			{
				throw new IOException(MessageLocalization.GetComposedMessage("insufficient.length"));
			}
			string text = SimpleXMLParser.GetEncodingName(array);
			string text2 = null;
			if (text.Equals("UTF-8"))
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num2;
				while ((num2 = inp.ReadByte()) != -1 && num2 != 62)
				{
					stringBuilder.Append((char)num2);
				}
				text2 = stringBuilder.ToString();
			}
			else if (text.Equals("CP037"))
			{
				MemoryStream memoryStream = new MemoryStream();
				int num3;
				while ((num3 = inp.ReadByte()) != -1 && num3 != 110)
				{
					memoryStream.WriteByte((byte)num3);
				}
				text2 = Encoding.GetEncoding(37).GetString(memoryStream.ToArray());
			}
			if (text2 != null)
			{
				text2 = SimpleXMLParser.GetDeclaredEncoding(text2);
				if (text2 != null)
				{
					text = text2;
				}
			}
			SimpleXMLParser.Parse(doc, new StreamReader(inp, IanaEncodings.GetEncodingEncoding(text)));
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x0014D450 File Offset: 0x0014C450
		private static string GetDeclaredEncoding(string decl)
		{
			if (decl == null)
			{
				return null;
			}
			int num = decl.IndexOf("encoding");
			if (num < 0)
			{
				return null;
			}
			int num2 = decl.IndexOf('"', num);
			int num3 = decl.IndexOf('\'', num);
			if (num2 == num3)
			{
				return null;
			}
			if ((num2 < 0 && num3 > 0) || (num3 > 0 && num3 < num2))
			{
				int num4 = decl.IndexOf('\'', num3 + 1);
				if (num4 < 0)
				{
					return null;
				}
				return decl.Substring(num3 + 1, num4 - (num3 + 1));
			}
			else
			{
				if ((num3 >= 0 || num2 <= 0) && (num2 <= 0 || num2 >= num3))
				{
					return null;
				}
				int num5 = decl.IndexOf('"', num2 + 1);
				if (num5 < 0)
				{
					return null;
				}
				return decl.Substring(num2 + 1, num5 - (num2 + 1));
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x0014D4F4 File Offset: 0x0014C4F4
		public static void Parse(ISimpleXMLDocHandler doc, TextReader r)
		{
			SimpleXMLParser.Parse(doc, null, r, false);
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x0014D500 File Offset: 0x0014C500
		public static string EscapeXML(string s, bool onlyASCII)
		{
			char[] array = s.ToCharArray();
			int num = array.Length;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)array[i];
				int num3 = num2;
				if (num3 != 34)
				{
					switch (num3)
					{
					case 38:
						stringBuilder.Append("&amp;");
						break;
					case 39:
						stringBuilder.Append("&apos;");
						break;
					default:
						switch (num3)
						{
						case 60:
							stringBuilder.Append("&lt;");
							goto IL_117;
						case 62:
							stringBuilder.Append("&gt;");
							goto IL_117;
						}
						if (num2 == 9 || num2 == 10 || num2 == 13 || (num2 >= 32 && num2 <= 55295) || (num2 >= 57344 && num2 <= 65533) || (num2 >= 65536 && num2 <= 1114111))
						{
							if (onlyASCII && num2 > 127)
							{
								stringBuilder.Append("&#").Append(num2).Append(';');
							}
							else
							{
								stringBuilder.Append((char)num2);
							}
						}
						break;
					}
				}
				else
				{
					stringBuilder.Append("&quot;");
				}
				IL_117:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x0014D638 File Offset: 0x0014C638
		private static string GetEncodingName(byte[] b4)
		{
			int num = (int)(b4[0] & byte.MaxValue);
			int num2 = (int)(b4[1] & byte.MaxValue);
			if (num == 254 && num2 == 255)
			{
				return "UTF-16BE";
			}
			if (num == 255 && num2 == 254)
			{
				return "UTF-16LE";
			}
			int num3 = (int)(b4[2] & byte.MaxValue);
			if (num == 239 && num2 == 187 && num3 == 191)
			{
				return "UTF-8";
			}
			int num4 = (int)(b4[3] & byte.MaxValue);
			if (num == 0 && num2 == 0 && num3 == 0 && num4 == 60)
			{
				return "ISO-10646-UCS-4";
			}
			if (num == 60 && num2 == 0 && num3 == 0 && num4 == 0)
			{
				return "ISO-10646-UCS-4";
			}
			if (num == 0 && num2 == 0 && num3 == 60 && num4 == 0)
			{
				return "ISO-10646-UCS-4";
			}
			if (num == 0 && num2 == 60 && num3 == 0 && num4 == 0)
			{
				return "ISO-10646-UCS-4";
			}
			if (num == 0 && num2 == 60 && num3 == 0 && num4 == 63)
			{
				return "UTF-16BE";
			}
			if (num == 60 && num2 == 0 && num3 == 63 && num4 == 0)
			{
				return "UTF-16LE";
			}
			if (num == 76 && num2 == 111 && num3 == 167 && num4 == 148)
			{
				return "CP037";
			}
			return "UTF-8";
		}

		// Token: 0x0400240E RID: 9230
		private const int UNKNOWN = 0;

		// Token: 0x0400240F RID: 9231
		private const int TEXT = 1;

		// Token: 0x04002410 RID: 9232
		private const int TAG_ENCOUNTERED = 2;

		// Token: 0x04002411 RID: 9233
		private const int EXAMIN_TAG = 3;

		// Token: 0x04002412 RID: 9234
		private const int TAG_EXAMINED = 4;

		// Token: 0x04002413 RID: 9235
		private const int IN_CLOSETAG = 5;

		// Token: 0x04002414 RID: 9236
		private const int SINGLE_TAG = 6;

		// Token: 0x04002415 RID: 9237
		private const int CDATA = 7;

		// Token: 0x04002416 RID: 9238
		private const int COMMENT = 8;

		// Token: 0x04002417 RID: 9239
		private const int PI = 9;

		// Token: 0x04002418 RID: 9240
		private const int ENTITY = 10;

		// Token: 0x04002419 RID: 9241
		private const int QUOTE = 11;

		// Token: 0x0400241A RID: 9242
		private const int ATTRIBUTE_KEY = 12;

		// Token: 0x0400241B RID: 9243
		private const int ATTRIBUTE_EQUAL = 13;

		// Token: 0x0400241C RID: 9244
		private const int ATTRIBUTE_VALUE = 14;

		// Token: 0x0400241D RID: 9245
		internal Stack<int> stack;

		// Token: 0x0400241E RID: 9246
		internal int character;

		// Token: 0x0400241F RID: 9247
		internal int previousCharacter = -1;

		// Token: 0x04002420 RID: 9248
		internal int lines = 1;

		// Token: 0x04002421 RID: 9249
		internal int columns;

		// Token: 0x04002422 RID: 9250
		internal bool eol;

		// Token: 0x04002423 RID: 9251
		internal bool nowhite;

		// Token: 0x04002424 RID: 9252
		internal int state;

		// Token: 0x04002425 RID: 9253
		internal bool html;

		// Token: 0x04002426 RID: 9254
		internal StringBuilder text = new StringBuilder();

		// Token: 0x04002427 RID: 9255
		internal StringBuilder entity = new StringBuilder();

		// Token: 0x04002428 RID: 9256
		internal string tag;

		// Token: 0x04002429 RID: 9257
		internal Dictionary<string, string> attributes;

		// Token: 0x0400242A RID: 9258
		internal ISimpleXMLDocHandler doc;

		// Token: 0x0400242B RID: 9259
		internal ISimpleXMLDocHandlerComment comment;

		// Token: 0x0400242C RID: 9260
		internal int nested;

		// Token: 0x0400242D RID: 9261
		internal int quoteCharacter = 34;

		// Token: 0x0400242E RID: 9262
		internal string attributekey;

		// Token: 0x0400242F RID: 9263
		internal string attributevalue;
	}
}
