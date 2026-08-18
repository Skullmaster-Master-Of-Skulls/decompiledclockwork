using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x020000D7 RID: 215
	internal class XmlTextEncoder
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x00024E6F File Offset: 0x0002306F
		internal XmlTextEncoder(TextWriter textWriter)
		{
			this.textWriter = textWriter;
			this.quoteChar = '"';
			this.xmlCharType = XmlCharType.Instance;
		}

		// Token: 0x170001E9 RID: 489
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x00024E91 File Offset: 0x00023091
		internal char QuoteChar
		{
			set
			{
				this.quoteChar = value;
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00024E9A File Offset: 0x0002309A
		internal void StartAttribute(bool cacheAttrValue)
		{
			this.inAttribute = true;
			this.cacheAttrValue = cacheAttrValue;
			if (cacheAttrValue)
			{
				if (this.attrValue == null)
				{
					this.attrValue = new StringBuilder();
					return;
				}
				this.attrValue.Length = 0;
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00024ECD File Offset: 0x000230CD
		internal void EndAttribute()
		{
			if (this.cacheAttrValue)
			{
				this.attrValue.Length = 0;
			}
			this.inAttribute = false;
			this.cacheAttrValue = false;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00024EF1 File Offset: 0x000230F1
		internal string AttributeValue
		{
			get
			{
				if (this.cacheAttrValue)
				{
					return this.attrValue.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00024F0C File Offset: 0x0002310C
		internal void WriteSurrogateChar(char lowChar, char highChar)
		{
			if (!XmlCharType.IsLowSurrogate((int)lowChar) || !XmlCharType.IsHighSurrogate((int)highChar))
			{
				throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
			}
			this.textWriter.Write(highChar);
			this.textWriter.Write(lowChar);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00024F40 File Offset: 0x00023140
		internal unsafe void Write(char[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (0 > offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (0 > count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > array.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(array, offset, count);
			}
			int num = offset + count;
			int num2 = offset;
			char c = '\0';
			for (;;)
			{
				int num3 = num2;
				while (num2 < num && (this.xmlCharType.charProperties[c = array[num2]] & 128) != 0)
				{
					num2++;
				}
				if (num3 < num2)
				{
					this.textWriter.Write(array, num3, num2 - num3);
				}
				if (num2 == num)
				{
					return;
				}
				if (c <= '&')
				{
					switch (c)
					{
					case '\t':
						this.textWriter.Write(c);
						break;
					case '\n':
					case '\r':
						if (this.inAttribute)
						{
							this.WriteCharEntityImpl(c);
						}
						else
						{
							this.textWriter.Write(c);
						}
						break;
					case '\v':
					case '\f':
						goto IL_1AA;
					default:
						if (c != '"')
						{
							if (c != '&')
							{
								goto IL_1AA;
							}
							this.WriteEntityRefImpl("amp");
						}
						else if (this.inAttribute && this.quoteChar == c)
						{
							this.WriteEntityRefImpl("quot");
						}
						else
						{
							this.textWriter.Write('"');
						}
						break;
					}
				}
				else if (c != '\'')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							goto IL_1AA;
						}
						this.WriteEntityRefImpl("gt");
					}
					else
					{
						this.WriteEntityRefImpl("lt");
					}
				}
				else if (this.inAttribute && this.quoteChar == c)
				{
					this.WriteEntityRefImpl("apos");
				}
				else
				{
					this.textWriter.Write('\'');
				}
				IL_1EE:
				num2++;
				continue;
				IL_1AA:
				if (XmlCharType.IsHighSurrogate((int)c))
				{
					if (num2 + 1 < num)
					{
						this.WriteSurrogateChar(array[++num2], c);
						goto IL_1EE;
					}
					break;
				}
				else
				{
					if (XmlCharType.IsLowSurrogate((int)c))
					{
						goto Block_23;
					}
					this.WriteCharEntityImpl(c);
					goto IL_1EE;
				}
			}
			throw new ArgumentException(Res.GetString("Xml_SurrogatePairSplit"));
			Block_23:
			throw XmlConvert.CreateInvalidHighSurrogateCharException(c);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00025144 File Offset: 0x00023344
		internal void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			if (!XmlCharType.IsLowSurrogate((int)lowChar) || !XmlCharType.IsHighSurrogate((int)highChar))
			{
				throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
			}
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(highChar);
				this.attrValue.Append(lowChar);
			}
			this.textWriter.Write("&#x");
			this.textWriter.Write(num.ToString("X", NumberFormatInfo.InvariantInfo));
			this.textWriter.Write(';');
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000251CC File Offset: 0x000233CC
		internal unsafe void Write(string text)
		{
			if (text == null)
			{
				return;
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(text);
			}
			int length = text.Length;
			int i = 0;
			int num = 0;
			char c = '\0';
			for (;;)
			{
				if (i >= length || (this.xmlCharType.charProperties[c = text[i]] & 128) == 0)
				{
					if (i == length)
					{
						break;
					}
					if (this.inAttribute)
					{
						if (c != '\t')
						{
							goto IL_91;
						}
						i++;
					}
					else
					{
						if (c != '\t' && c != '\n' && c != '\r' && c != '"' && c != '\'')
						{
							goto IL_91;
						}
						i++;
					}
				}
				else
				{
					i++;
				}
			}
			this.textWriter.Write(text);
			return;
			IL_91:
			char[] helperBuffer = new char[256];
			for (;;)
			{
				if (num < i)
				{
					this.WriteStringFragment(text, num, i - num, helperBuffer);
				}
				if (i == length)
				{
					return;
				}
				if (c <= '&')
				{
					switch (c)
					{
					case '\t':
						this.textWriter.Write(c);
						break;
					case '\n':
					case '\r':
						if (this.inAttribute)
						{
							this.WriteCharEntityImpl(c);
						}
						else
						{
							this.textWriter.Write(c);
						}
						break;
					case '\v':
					case '\f':
						goto IL_1C0;
					default:
						if (c != '"')
						{
							if (c != '&')
							{
								goto IL_1C0;
							}
							this.WriteEntityRefImpl("amp");
						}
						else if (this.inAttribute && this.quoteChar == c)
						{
							this.WriteEntityRefImpl("quot");
						}
						else
						{
							this.textWriter.Write('"');
						}
						break;
					}
				}
				else if (c != '\'')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							goto IL_1C0;
						}
						this.WriteEntityRefImpl("gt");
					}
					else
					{
						this.WriteEntityRefImpl("lt");
					}
				}
				else if (this.inAttribute && this.quoteChar == c)
				{
					this.WriteEntityRefImpl("apos");
				}
				else
				{
					this.textWriter.Write('\'');
				}
				IL_206:
				i++;
				num = i;
				while (i < length)
				{
					if ((this.xmlCharType.charProperties[c = text[i]] & 128) == 0)
					{
						break;
					}
					i++;
				}
				continue;
				IL_1C0:
				if (XmlCharType.IsHighSurrogate((int)c))
				{
					if (i + 1 < length)
					{
						this.WriteSurrogateChar(text[++i], c);
						goto IL_206;
					}
					break;
				}
				else
				{
					if (XmlCharType.IsLowSurrogate((int)c))
					{
						goto Block_27;
					}
					this.WriteCharEntityImpl(c);
					goto IL_206;
				}
			}
			throw XmlConvert.CreateInvalidSurrogatePairException(text[i], c);
			Block_27:
			throw XmlConvert.CreateInvalidHighSurrogateCharException(c);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00025418 File Offset: 0x00023618
		internal unsafe void WriteRawWithSurrogateChecking(string text)
		{
			if (text == null)
			{
				return;
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(text);
			}
			int length = text.Length;
			int num = 0;
			char c = '\0';
			char c2;
			for (;;)
			{
				if (num >= length || ((this.xmlCharType.charProperties[c = text[num]] & 16) == 0 && c >= ' '))
				{
					if (num == length)
					{
						goto IL_A5;
					}
					if (XmlCharType.IsHighSurrogate((int)c))
					{
						if (num + 1 >= length)
						{
							goto IL_80;
						}
						c2 = text[num + 1];
						if (!XmlCharType.IsLowSurrogate((int)c2))
						{
							break;
						}
						num += 2;
					}
					else
					{
						if (XmlCharType.IsLowSurrogate((int)c))
						{
							goto Block_9;
						}
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			throw XmlConvert.CreateInvalidSurrogatePairException(c2, c);
			IL_80:
			throw new ArgumentException(Res.GetString("Xml_InvalidSurrogateMissingLowChar"));
			Block_9:
			throw XmlConvert.CreateInvalidHighSurrogateCharException(c);
			IL_A5:
			this.textWriter.Write(text);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000254D6 File Offset: 0x000236D6
		internal void WriteRaw(string value)
		{
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(value);
			}
			this.textWriter.Write(value);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000254FC File Offset: 0x000236FC
		internal void WriteRaw(char[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (0 > count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (0 > offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count > array.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(array, offset, count);
			}
			this.textWriter.Write(array, offset, count);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00025570 File Offset: 0x00023770
		internal void WriteCharEntity(char ch)
		{
			if (XmlCharType.IsSurrogate((int)ch))
			{
				throw new ArgumentException(Res.GetString("Xml_InvalidSurrogateMissingLowChar"));
			}
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.cacheAttrValue)
			{
				this.attrValue.Append("&#x");
				this.attrValue.Append(text);
				this.attrValue.Append(';');
			}
			this.WriteCharEntityImpl(text);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000255E4 File Offset: 0x000237E4
		internal void WriteEntityRef(string name)
		{
			if (this.cacheAttrValue)
			{
				this.attrValue.Append('&');
				this.attrValue.Append(name);
				this.attrValue.Append(';');
			}
			this.WriteEntityRefImpl(name);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002561E File Offset: 0x0002381E
		internal void Flush()
		{
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00025620 File Offset: 0x00023820
		private void WriteStringFragment(string str, int offset, int count, char[] helperBuffer)
		{
			int num = helperBuffer.Length;
			while (count > 0)
			{
				int num2 = count;
				if (num2 > num)
				{
					num2 = num;
				}
				str.CopyTo(offset, helperBuffer, 0, num2);
				this.textWriter.Write(helperBuffer, 0, num2);
				offset += num2;
				count -= num2;
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00025664 File Offset: 0x00023864
		private void WriteCharEntityImpl(char ch)
		{
			int num = (int)ch;
			this.WriteCharEntityImpl(num.ToString("X", NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002568A File Offset: 0x0002388A
		private void WriteCharEntityImpl(string strVal)
		{
			this.textWriter.Write("&#x");
			this.textWriter.Write(strVal);
			this.textWriter.Write(';');
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000256B5 File Offset: 0x000238B5
		private void WriteEntityRefImpl(string name)
		{
			this.textWriter.Write('&');
			this.textWriter.Write(name);
			this.textWriter.Write(';');
		}

		// Token: 0x04000364 RID: 868
		private TextWriter textWriter;

		// Token: 0x04000365 RID: 869
		private bool inAttribute;

		// Token: 0x04000366 RID: 870
		private char quoteChar;

		// Token: 0x04000367 RID: 871
		private StringBuilder attrValue;

		// Token: 0x04000368 RID: 872
		private bool cacheAttrValue;

		// Token: 0x04000369 RID: 873
		private XmlCharType xmlCharType;
	}
}
