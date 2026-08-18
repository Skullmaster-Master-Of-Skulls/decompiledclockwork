using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x020005D4 RID: 1492
	public class PdfChunk
	{
		// Token: 0x06003364 RID: 13156 RVA: 0x0013EB58 File Offset: 0x0013DB58
		static PdfChunk()
		{
			PdfChunk.keysAttributes.Add("ACTION", null);
			PdfChunk.keysAttributes.Add("UNDERLINE", null);
			PdfChunk.keysAttributes.Add("REMOTEGOTO", null);
			PdfChunk.keysAttributes.Add("LOCALGOTO", null);
			PdfChunk.keysAttributes.Add("LOCALDESTINATION", null);
			PdfChunk.keysAttributes.Add("GENERICTAG", null);
			PdfChunk.keysAttributes.Add("NEWPAGE", null);
			PdfChunk.keysAttributes.Add("IMAGE", null);
			PdfChunk.keysAttributes.Add("BACKGROUND", null);
			PdfChunk.keysAttributes.Add("PDFANNOTATION", null);
			PdfChunk.keysAttributes.Add("SKEW", null);
			PdfChunk.keysAttributes.Add("HSCALE", null);
			PdfChunk.keysAttributes.Add("SEPARATOR", null);
			PdfChunk.keysAttributes.Add("TAB", null);
			PdfChunk.keysAttributes.Add("CHAR_SPACING", null);
			PdfChunk.keysNoStroke.Add("SUBSUPSCRIPT", null);
			PdfChunk.keysNoStroke.Add("SPLITCHARACTER", null);
			PdfChunk.keysNoStroke.Add("HYPHENATION", null);
			PdfChunk.keysNoStroke.Add("TEXTRENDERMODE", null);
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x0013ECC8 File Offset: 0x0013DCC8
		internal PdfChunk(string str, PdfChunk other)
		{
			PdfChunk.thisChunk[0] = this;
			this.value = str;
			this.font = other.font;
			this.attributes = other.attributes;
			this.noStroke = other.noStroke;
			this.baseFont = other.baseFont;
			object[] array = null;
			if (this.attributes.ContainsKey("IMAGE"))
			{
				array = (object[])this.attributes["IMAGE"];
			}
			if (array == null)
			{
				this.image = null;
			}
			else
			{
				this.image = (Image)array[0];
				this.offsetX = (float)array[1];
				this.offsetY = (float)array[2];
				this.changeLeading = (bool)array[3];
			}
			this.encoding = this.font.Font.Encoding;
			if (this.noStroke.ContainsKey("SPLITCHARACTER"))
			{
				this.splitCharacter = (ISplitCharacter)this.noStroke["SPLITCHARACTER"];
				return;
			}
			this.splitCharacter = DefaultSplitCharacter.DEFAULT;
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x0013EE04 File Offset: 0x0013DE04
		internal PdfChunk(Chunk chunk, PdfAction action)
		{
			PdfChunk.thisChunk[0] = this;
			this.value = chunk.Content;
			Font font = chunk.Font;
			float num = font.Size;
			if (num == -1f)
			{
				num = 12f;
			}
			this.baseFont = font.BaseFont;
			BaseFont baseFont = font.BaseFont;
			int num2 = font.Style;
			if (num2 == -1)
			{
				num2 = 0;
			}
			if (this.baseFont == null)
			{
				this.baseFont = font.GetCalculatedBaseFont(false);
			}
			else
			{
				if ((num2 & 1) != 0)
				{
					Dictionary<string, object> dictionary = this.attributes;
					string key = "TEXTRENDERMODE";
					object[] array = new object[3];
					array[0] = 2;
					array[1] = num / 30f;
					dictionary[key] = array;
				}
				if ((num2 & 2) != 0)
				{
					this.attributes["SKEW"] = new float[]
					{
						0f,
						0.21256f
					};
				}
			}
			this.font = new PdfFont(this.baseFont, num);
			Dictionary<string, object> dictionary2 = chunk.Attributes;
			if (dictionary2 != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in dictionary2)
				{
					string key2 = keyValuePair.Key;
					if (PdfChunk.keysAttributes.ContainsKey(key2))
					{
						this.attributes[key2] = keyValuePair.Value;
					}
					else if (PdfChunk.keysNoStroke.ContainsKey(key2))
					{
						this.noStroke[key2] = keyValuePair.Value;
					}
				}
				if (dictionary2.ContainsKey("GENERICTAG") && "".Equals(dictionary2["GENERICTAG"]))
				{
					this.attributes["GENERICTAG"] = chunk.Content;
				}
			}
			if (font.IsUnderlined())
			{
				object[] array2 = new object[2];
				object[] array3 = array2;
				int num3 = 1;
				float[] array4 = new float[5];
				array4[1] = 0.06666667f;
				array4[3] = -0.33333334f;
				array3[num3] = array4;
				object[] item = array2;
				object[][] original = null;
				if (this.attributes.ContainsKey("UNDERLINE"))
				{
					original = (object[][])this.attributes["UNDERLINE"];
				}
				object[][] array5 = Utilities.AddToArray(original, item);
				this.attributes["UNDERLINE"] = array5;
			}
			if (font.IsStrikethru())
			{
				object[] array6 = new object[2];
				object[] array7 = array6;
				int num4 = 1;
				float[] array8 = new float[5];
				array8[1] = 0.06666667f;
				array8[3] = 0.33333334f;
				array7[num4] = array8;
				object[] item2 = array6;
				object[][] original2 = null;
				if (this.attributes.ContainsKey("UNDERLINE"))
				{
					original2 = (object[][])this.attributes["UNDERLINE"];
				}
				object[][] array9 = Utilities.AddToArray(original2, item2);
				this.attributes["UNDERLINE"] = array9;
			}
			if (action != null)
			{
				this.attributes["ACTION"] = action;
			}
			this.noStroke["COLOR"] = font.Color;
			this.noStroke["ENCODING"] = this.font.Font.Encoding;
			object[] array10 = null;
			if (this.attributes.ContainsKey("IMAGE"))
			{
				array10 = (object[])this.attributes["IMAGE"];
			}
			if (array10 == null)
			{
				this.image = null;
			}
			else
			{
				this.attributes.Remove("HSCALE");
				this.image = (Image)array10[0];
				this.offsetX = (float)array10[1];
				this.offsetY = (float)array10[2];
				this.changeLeading = (bool)array10[3];
			}
			this.font.Image = this.image;
			object obj;
			this.attributes.TryGetValue("HSCALE", out obj);
			if (obj != null)
			{
				this.font.HorizontalScaling = (float)obj;
			}
			this.encoding = this.font.Font.Encoding;
			if (this.noStroke.ContainsKey("SPLITCHARACTER"))
			{
				this.splitCharacter = (ISplitCharacter)this.noStroke["SPLITCHARACTER"];
				return;
			}
			this.splitCharacter = DefaultSplitCharacter.DEFAULT;
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x0013F244 File Offset: 0x0013E244
		public int GetUnicodeEquivalent(int c)
		{
			return this.baseFont.GetUnicodeEquivalent(c);
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x0013F254 File Offset: 0x0013E254
		protected int GetWord(string text, int start)
		{
			int length = text.Length;
			while (start < length && char.IsLetter(text[start]))
			{
				start++;
			}
			return start;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x0013F284 File Offset: 0x0013E284
		internal PdfChunk Split(float width)
		{
			this.newlineSplit = false;
			if (this.image != null)
			{
				if (this.image.ScaledWidth > width)
				{
					PdfChunk result = new PdfChunk("￼", this);
					this.value = "";
					this.attributes = new Dictionary<string, object>();
					this.image = null;
					this.font = PdfFont.DefaultFont;
					return result;
				}
				return null;
			}
			else
			{
				IHyphenationEvent hyphenationEvent = null;
				if (this.noStroke.ContainsKey("HYPHENATION"))
				{
					hyphenationEvent = (IHyphenationEvent)this.noStroke["HYPHENATION"];
				}
				int i = 0;
				int num = -1;
				float num2 = 0f;
				int num3 = -1;
				float num4 = 0f;
				int length = this.value.Length;
				char[] array = this.value.ToCharArray();
				BaseFont baseFont = this.font.Font;
				if (baseFont.FontType == 2 && baseFont.GetUnicodeEquivalent(32) != 32)
				{
					while (i < length)
					{
						char c = array[i];
						char c2 = (char)baseFont.GetUnicodeEquivalent((int)c);
						if (c2 == '\n')
						{
							this.newlineSplit = true;
							string str = this.value.Substring(i + 1);
							this.value = this.value.Substring(0, i);
							if (this.value.Length < 1)
							{
								this.value = "\u0001";
							}
							return new PdfChunk(str, this);
						}
						num2 += this.GetCharWidth((int)c);
						if (c2 == ' ')
						{
							num3 = i + 1;
							num4 = num2;
						}
						if (num2 > width)
						{
							break;
						}
						if (this.splitCharacter.IsSplitCharacter(0, i, length, array, PdfChunk.thisChunk))
						{
							num = i + 1;
						}
						i++;
					}
				}
				else
				{
					while (i < length)
					{
						char c2 = array[i];
						if (c2 == '\r' || c2 == '\n')
						{
							this.newlineSplit = true;
							int num5 = 1;
							if (c2 == '\r' && i + 1 < length && array[i + 1] == '\n')
							{
								num5 = 2;
							}
							string str2 = this.value.Substring(i + num5);
							this.value = this.value.Substring(0, i);
							if (this.value.Length < 1)
							{
								this.value = " ";
							}
							return new PdfChunk(str2, this);
						}
						bool flag = Utilities.IsSurrogatePair(array, i);
						if (flag)
						{
							num2 += this.GetCharWidth(Utilities.ConvertToUtf32(array[i], array[i + 1]));
						}
						else
						{
							num2 += this.GetCharWidth((int)c2);
						}
						if (c2 == ' ')
						{
							num3 = i + 1;
							num4 = num2;
						}
						if (flag)
						{
							i++;
						}
						if (num2 > width)
						{
							break;
						}
						if (this.splitCharacter.IsSplitCharacter(0, i, length, array, null))
						{
							num = i + 1;
						}
						i++;
					}
				}
				if (i == length)
				{
					return null;
				}
				if (num < 0)
				{
					string str3 = this.value;
					this.value = "";
					return new PdfChunk(str3, this);
				}
				if (num3 > num && this.splitCharacter.IsSplitCharacter(0, 0, 1, PdfChunk.singleSpace, null))
				{
					num = num3;
				}
				if (hyphenationEvent != null && num3 >= 0 && num3 < i)
				{
					int word = this.GetWord(this.value, num3);
					if (word > num3)
					{
						string hyphenatedWordPre = hyphenationEvent.GetHyphenatedWordPre(this.value.Substring(num3, word - num3), this.font.Font, this.font.Size, width - num4);
						string hyphenatedWordPost = hyphenationEvent.HyphenatedWordPost;
						if (hyphenatedWordPre.Length > 0)
						{
							string str4 = hyphenatedWordPost + this.value.Substring(word);
							this.value = this.Trim(this.value.Substring(0, num3) + hyphenatedWordPre);
							return new PdfChunk(str4, this);
						}
					}
				}
				string str5 = this.value.Substring(num);
				this.value = this.Trim(this.value.Substring(0, num));
				return new PdfChunk(str5, this);
			}
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x0013F65C File Offset: 0x0013E65C
		internal PdfChunk Truncate(float width)
		{
			if (this.image != null)
			{
				if (this.image.ScaledWidth > width)
				{
					PdfChunk result = new PdfChunk("", this);
					this.value = "";
					this.attributes.Remove("IMAGE");
					this.image = null;
					this.font = PdfFont.DefaultFont;
					return result;
				}
				return null;
			}
			else
			{
				int i = 0;
				float num = 0f;
				if (width < this.font.Width())
				{
					string str = this.value.Substring(1);
					this.value = this.value.Substring(0, 1);
					return new PdfChunk(str, this);
				}
				int length = this.value.Length;
				bool flag = false;
				while (i < length)
				{
					flag = Utilities.IsSurrogatePair(this.value, i);
					if (flag)
					{
						num += this.GetCharWidth(Utilities.ConvertToUtf32(this.value, i));
					}
					else
					{
						num += this.GetCharWidth((int)this.value[i]);
					}
					if (num > width)
					{
						break;
					}
					if (flag)
					{
						i++;
					}
					i++;
				}
				if (i == length)
				{
					return null;
				}
				if (i == 0)
				{
					i = 1;
					if (flag)
					{
						i++;
					}
				}
				string str2 = this.value.Substring(i);
				this.value = this.value.Substring(0, i);
				return new PdfChunk(str2, this);
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x0013F7A5 File Offset: 0x0013E7A5
		internal PdfFont Font
		{
			get
			{
				return this.font;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x0013F7AD File Offset: 0x0013E7AD
		internal BaseColor Color
		{
			get
			{
				if (this.noStroke.ContainsKey("COLOR"))
				{
					return (BaseColor)this.noStroke["COLOR"];
				}
				return null;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x0013F7D8 File Offset: 0x0013E7D8
		internal float Width
		{
			get
			{
				if (this.IsAttribute("CHAR_SPACING"))
				{
					float num = (float)this.GetAttribute("CHAR_SPACING");
					return this.font.Width(this.value) + (float)this.value.Length * num;
				}
				if (this.IsAttribute("SEPARATOR"))
				{
					return 0f;
				}
				return this.font.Width(this.value);
			}
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x0013F848 File Offset: 0x0013E848
		public bool IsNewlineSplit()
		{
			return this.newlineSplit;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x0013F850 File Offset: 0x0013E850
		public float GetWidthCorrected(float charSpacing, float wordSpacing)
		{
			if (this.image != null)
			{
				return this.image.ScaledWidth + charSpacing;
			}
			int num = 0;
			int num2 = -1;
			while ((num2 = this.value.IndexOf(' ', num2 + 1)) >= 0)
			{
				num++;
			}
			return this.Width + ((float)this.value.Length * charSpacing + (float)num * wordSpacing);
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x0013F8AC File Offset: 0x0013E8AC
		public float TextRise
		{
			get
			{
				object attribute = this.GetAttribute("SUBSUPSCRIPT");
				if (attribute != null)
				{
					return (float)attribute;
				}
				return 0f;
			}
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x0013F8D4 File Offset: 0x0013E8D4
		public float TrimLastSpace()
		{
			BaseFont baseFont = this.font.Font;
			if (baseFont.FontType == 2 && baseFont.GetUnicodeEquivalent(32) != 32)
			{
				if (this.value.Length > 1 && this.value.EndsWith("\u0001"))
				{
					this.value = this.value.Substring(0, this.value.Length - 1);
					return this.font.Width(1);
				}
			}
			else if (this.value.Length > 1 && this.value.EndsWith(" "))
			{
				this.value = this.value.Substring(0, this.value.Length - 1);
				return this.font.Width(32);
			}
			return 0f;
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x0013F9A4 File Offset: 0x0013E9A4
		public float TrimFirstSpace()
		{
			BaseFont baseFont = this.font.Font;
			if (baseFont.FontType == 2 && baseFont.GetUnicodeEquivalent(32) != 32)
			{
				if (this.value.Length > 1 && this.value.StartsWith("\u0001"))
				{
					this.value = this.value.Substring(1);
					return this.font.Width(1);
				}
			}
			else if (this.value.Length > 1 && this.value.StartsWith(" "))
			{
				this.value = this.value.Substring(1);
				return this.font.Width(32);
			}
			return 0f;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x0013FA56 File Offset: 0x0013EA56
		internal object GetAttribute(string name)
		{
			if (this.attributes.ContainsKey(name))
			{
				return this.attributes[name];
			}
			if (this.noStroke.ContainsKey(name))
			{
				return this.noStroke[name];
			}
			return null;
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x0013FA8F File Offset: 0x0013EA8F
		internal bool IsAttribute(string name)
		{
			return this.attributes.ContainsKey(name) || this.noStroke.ContainsKey(name);
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x0013FAAD File Offset: 0x0013EAAD
		internal bool IsStroked()
		{
			return this.attributes.Count > 0;
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x0013FABD File Offset: 0x0013EABD
		internal bool IsSeparator()
		{
			return this.IsAttribute("SEPARATOR");
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x0013FACC File Offset: 0x0013EACC
		internal bool IsHorizontalSeparator()
		{
			if (this.IsAttribute("SEPARATOR"))
			{
				object[] array = (object[])this.GetAttribute("SEPARATOR");
				return !(bool)array[1];
			}
			return false;
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x0013FB04 File Offset: 0x0013EB04
		internal bool IsTab()
		{
			return this.IsAttribute("TAB");
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x0013FB14 File Offset: 0x0013EB14
		internal void AdjustLeft(float newValue)
		{
			if (this.attributes.ContainsKey("TAB"))
			{
				object[] array = (object[])this.attributes["TAB"];
				this.attributes["TAB"] = new object[]
				{
					array[0],
					array[1],
					array[2],
					newValue
				};
			}
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x0013FB7C File Offset: 0x0013EB7C
		internal bool IsImage()
		{
			return this.image != null;
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x0013FB8A File Offset: 0x0013EB8A
		internal Image Image
		{
			get
			{
				return this.image;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x0013FB92 File Offset: 0x0013EB92
		// (set) Token: 0x0600337D RID: 13181 RVA: 0x0013FB9A File Offset: 0x0013EB9A
		internal float ImageOffsetX
		{
			get
			{
				return this.offsetX;
			}
			set
			{
				this.offsetX = value;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x0013FBA3 File Offset: 0x0013EBA3
		// (set) Token: 0x0600337F RID: 13183 RVA: 0x0013FBAB File Offset: 0x0013EBAB
		internal float ImageOffsetY
		{
			get
			{
				return this.offsetY;
			}
			set
			{
				this.offsetY = value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (set) Token: 0x06003380 RID: 13184 RVA: 0x0013FBB4 File Offset: 0x0013EBB4
		internal string Value
		{
			set
			{
				this.value = value;
			}
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x0013FBBD File Offset: 0x0013EBBD
		public override string ToString()
		{
			return this.value;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x0013FBC5 File Offset: 0x0013EBC5
		internal bool IsSpecialEncoding()
		{
			return this.encoding.Equals("UNICODEBIGUNMARKED") || this.encoding.Equals("Identity-H");
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x0013FBEB File Offset: 0x0013EBEB
		internal string Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x0013FBF3 File Offset: 0x0013EBF3
		internal int Length
		{
			get
			{
				return this.value.Length;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x0013FC00 File Offset: 0x0013EC00
		internal int LengthUtf32
		{
			get
			{
				if (!"Identity-H".Equals(this.encoding))
				{
					return this.value.Length;
				}
				int num = 0;
				int length = this.value.Length;
				for (int i = 0; i < length; i++)
				{
					if (Utilities.IsSurrogateHigh(this.value[i]))
					{
						i++;
					}
					num++;
				}
				return num;
			}
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x0013FC61 File Offset: 0x0013EC61
		internal bool IsExtSplitCharacter(int start, int current, int end, char[] cc, PdfChunk[] ck)
		{
			return this.splitCharacter.IsSplitCharacter(start, current, end, cc, ck);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x0013FC78 File Offset: 0x0013EC78
		internal string Trim(string str)
		{
			BaseFont baseFont = this.font.Font;
			if (baseFont.FontType == 2 && baseFont.GetUnicodeEquivalent(32) != 32)
			{
				while (str.EndsWith("\u0001"))
				{
					str = str.Substring(0, str.Length - 1);
				}
			}
			else
			{
				while (str.EndsWith(" ") || str.EndsWith("\t"))
				{
					str = str.Substring(0, str.Length - 1);
				}
			}
			return str;
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x0013FCF4 File Offset: 0x0013ECF4
		public bool ChangeLeading
		{
			get
			{
				return this.changeLeading;
			}
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x0013FCFC File Offset: 0x0013ECFC
		internal float GetCharWidth(int c)
		{
			if (PdfChunk.NoPrint(c))
			{
				return 0f;
			}
			if (this.IsAttribute("CHAR_SPACING"))
			{
				float num = (float)this.GetAttribute("CHAR_SPACING");
				return this.font.Width(c) + num;
			}
			return this.font.Width(c);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x0013FD50 File Offset: 0x0013ED50
		public static bool NoPrint(int c)
		{
			return (c >= 8203 && c <= 8207) || (c >= 8234 && c <= 8238);
		}

		// Token: 0x040022D1 RID: 8913
		private const float ITALIC_ANGLE = 0.21256f;

		// Token: 0x040022D2 RID: 8914
		private static char[] singleSpace = new char[]
		{
			' '
		};

		// Token: 0x040022D3 RID: 8915
		private static PdfChunk[] thisChunk = new PdfChunk[1];

		// Token: 0x040022D4 RID: 8916
		private static Dictionary<string, object> keysAttributes = new Dictionary<string, object>();

		// Token: 0x040022D5 RID: 8917
		private static Dictionary<string, object> keysNoStroke = new Dictionary<string, object>();

		// Token: 0x040022D6 RID: 8918
		protected string value = "";

		// Token: 0x040022D7 RID: 8919
		protected string encoding = "Cp1252";

		// Token: 0x040022D8 RID: 8920
		protected PdfFont font;

		// Token: 0x040022D9 RID: 8921
		protected BaseFont baseFont;

		// Token: 0x040022DA RID: 8922
		protected ISplitCharacter splitCharacter;

		// Token: 0x040022DB RID: 8923
		protected Dictionary<string, object> attributes = new Dictionary<string, object>();

		// Token: 0x040022DC RID: 8924
		protected Dictionary<string, object> noStroke = new Dictionary<string, object>();

		// Token: 0x040022DD RID: 8925
		protected bool newlineSplit;

		// Token: 0x040022DE RID: 8926
		protected Image image;

		// Token: 0x040022DF RID: 8927
		protected float offsetX;

		// Token: 0x040022E0 RID: 8928
		protected float offsetY;

		// Token: 0x040022E1 RID: 8929
		protected bool changeLeading;
	}
}
