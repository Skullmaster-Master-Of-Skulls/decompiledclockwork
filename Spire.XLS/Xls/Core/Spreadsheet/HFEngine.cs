using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200063C RID: 1596
	public class HFEngine : RichTextString, IHFEngine
	{
		// Token: 0x06006169 RID: 24937 RVA: 0x003D9B94 File Offset: 0x003D8B94
		internal HFEngine(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜁ = new spr\u223A();
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x003D9BC0 File Offset: 0x003D8BC0
		public void Parse(string strText)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					int num2;
					int num3;
					StringBuilder stringBuilder;
					XlsFont defaultFont;
					switch (num)
					{
					case 0:
						goto IL_10C;
					case 1:
						goto IL_10C;
					case 2:
						if (true)
						{
						}
						goto IL_10C;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_73;
						default:
						{
							if (false)
							{
							}
							if (num2 >= 0)
							{
								num = 7;
								continue;
							}
							string value = strText.Substring(num3);
							stringBuilder.Append(value);
							this.ᜀ(stringBuilder, defaultFont);
							int length;
							num3 = length;
							num = 0;
							continue;
						}
						}
						break;
					case 4:
						goto IL_14D;
					case 5:
						return;
					case 7:
						goto IL_73;
					case 8:
					{
						if (strText.Length == 0)
						{
							num = 4;
							continue;
						}
						num3 = 0;
						int length = strText.Length;
						this.ᜁ = new spr\u223A();
						this.ᜌ.Clear();
						this.ᜌ.Add(this.DefaultFont);
						defaultFont = this.DefaultFont;
						stringBuilder = new StringBuilder(length);
						num = 1;
						continue;
					}
					case 9:
						num = 8;
						continue;
					case 10:
					{
						int length;
						if (num3 >= length)
						{
							num = 5;
							continue;
						}
						num2 = strText.IndexOf(RecordTableEnumerator.b("ᬼ", a_), num3);
						num = 3;
						continue;
					}
					}
					if (strText != null)
					{
						num = 9;
						continue;
					}
					break;
					IL_73:
					string value2 = strText.Substring(num3, num2 - num3);
					stringBuilder.Append(value2);
					num3 = num2 + 1;
					this.ᜀ(strText, stringBuilder, ref num3, ref defaultFont);
					num = 2;
					continue;
					IL_10C:
					num = 10;
				}
				IL_105:
				this.Clear();
				return;
				IL_14D:
				goto IL_105;
			}
			}
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x003D9D98 File Offset: 0x003D8D98
		public string GetHeaderFooterString()
		{
			StringBuilder stringBuilder;
			int a_2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					for (;;)
					{
						stringBuilder = new StringBuilder();
						SortedList<int, int> sortedList = this.ᜁ.ᜇ();
						IList<int> keys = sortedList.Keys;
						IList<int> values = sortedList.Values;
						XlsFont a_ = null;
						a_2 = 0;
						int num = 0;
						int num2 = this.ᜁ.ᜆ();
						int num3 = 2;
						for (;;)
						{
							if (true)
							{
							}
							switch (num3)
							{
							case 0:
								goto IL_8F;
							case 1:
								goto IL_B4;
							case 2:
								goto IL_8F;
							case 3:
							{
								if (num >= num2)
								{
									num3 = 1;
									continue;
								}
								int index = values[num];
								int num4 = keys[num];
								XlsFont xlsFont = this.ᜌ[index];
								this.ᜀ(stringBuilder, a_2, num4);
								this.ᜅ(stringBuilder, a_, xlsFont);
								a_2 = num4;
								a_ = xlsFont;
								num++;
								num3 = 0;
								continue;
							}
							}
							break;
							IL_8F:
							num3 = 3;
						}
					}
					break;
				}
				break;
			}
			IL_B4:
			this.ᜀ(stringBuilder, a_2, this.ᜁ.ᜏ().Length);
			return stringBuilder.ToString();
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x003D9ED0 File Offset: 0x003D8ED0
		private void ᜀ(string A_0, StringBuilder A_1, ref int A_2, ref XlsFont A_3)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 14;
				int num2;
				char c;
				FontUnderlineType underline;
				FontUnderlineType underline2;
				for (;;)
				{
					char c2;
					int length;
					switch (num)
					{
					case 0:
						goto IL_2A6;
					case 1:
						goto IL_2C9;
					case 2:
						return;
					case 3:
						goto IL_2A6;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num2 = 0;
							num = 3;
							continue;
						}
						break;
					case 5:
						c = A_0[A_2];
						num = 0;
						continue;
					case 6:
						if (c2 != 'E')
						{
							num = 8;
							continue;
						}
						this.ᜀ(A_1, A_3);
						A_3 = A_3.TypedClone();
						underline = A_3.Underline;
						num = 15;
						continue;
					case 7:
						num = 6;
						continue;
					case 8:
						num = 9;
						continue;
					case 9:
						switch (c2)
						{
						case 'S':
							goto IL_300;
						case 'T':
						case 'V':
						case 'W':
							goto IL_35E;
						case 'U':
							this.ᜀ(A_1, A_3);
							A_3 = A_3.TypedClone();
							underline2 = A_3.Underline;
							num = 10;
							continue;
						case 'X':
							goto IL_32F;
						case 'Y':
							goto IL_1A6;
						default:
							num = 13;
							continue;
						}
						break;
					case 10:
						goto IL_FE;
					case 11:
						goto IL_2E6;
					case 12:
						if (char.IsDigit(c))
						{
							if (true)
							{
							}
							num = 4;
							continue;
						}
						goto IL_40A;
					case 13:
						num = 16;
						continue;
					case 15:
						goto IL_138;
					case 16:
						goto IL_35E;
					case 17:
					{
						int num3;
						int num4;
						string a_2 = A_0.Substring(num3, num4 - num3);
						this.ᜀ(A_1, A_3);
						A_3 = A_3.TypedClone();
						this.ᜀ(A_3, a_2);
						A_2 = num4 + 1;
						num = 2;
						continue;
					}
					case 18:
					{
						int num4;
						if (num4 != -1)
						{
							num = 17;
							continue;
						}
						goto IL_35E;
					}
					case 19:
					{
						if (c2 != '"')
						{
							num = 7;
							continue;
						}
						int num3 = A_2 + 1;
						int num4 = A_0.IndexOf('"', num3);
						num = 18;
						continue;
					}
					case 20:
						num = 11;
						continue;
					case 21:
						if (A_2 < length)
						{
							num = 5;
							continue;
						}
						goto IL_B3;
					case 22:
						if (!char.IsDigit(c))
						{
							num = 1;
							continue;
						}
						num2 = 10 * num2 + (int)c - 48;
						A_2++;
						num = 21;
						continue;
					}
					if (A_0 == null)
					{
						num = 20;
						continue;
					}
					length = A_0.Length;
					c = A_0[A_2];
					c2 = c;
					num = 19;
					continue;
					IL_2A6:
					num = 22;
					continue;
					IL_35E:
					num = 12;
				}
				IL_B3:
				this.ᜀ(A_1, A_3);
				A_3 = A_3.TypedClone();
				A_3.Size = (double)num2;
				return;
				IL_FE:
				A_3.Underline = ((underline2 == FontUnderlineType.SingleAccounting) ? FontUnderlineType.None : FontUnderlineType.SingleAccounting);
				A_2++;
				return;
				IL_138:
				A_3.Underline = ((underline == FontUnderlineType.DoubleAccounting) ? FontUnderlineType.None : FontUnderlineType.DoubleAccounting);
				A_2++;
				return;
				IL_1A6:
				this.ᜀ(A_1, A_3);
				A_3 = A_3.TypedClone();
				A_3.IsSubscript = !A_3.IsSubscript;
				A_2++;
				return;
				IL_2C9:
				goto IL_B3;
				IL_2E6:
				throw new ArgumentNullException(RecordTableEnumerator.b("あㅄ㕆ᵈ⹊㕌㭎", a_));
				IL_300:
				this.ᜀ(A_1, A_3);
				A_3 = A_3.TypedClone();
				A_3.IsStrikethrough = !A_3.IsStrikethrough;
				A_2++;
				return;
				IL_32F:
				this.ᜀ(A_1, A_3);
				A_3 = A_3.TypedClone();
				A_3.IsSuperscript = !A_3.IsSuperscript;
				A_2++;
				return;
				IL_40A:
				A_1.Append(RecordTableEnumerator.b("敂", a_));
				A_1.Append(c);
				A_2++;
				return;
			}
			}
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x003DA30C File Offset: 0x003D930C
		private void ᜀ(StringBuilder A_0, XlsFont A_1)
		{
			for (;;)
			{
				for (;;)
				{
					int length = this.ᜁ.ᜏ().Length;
					spr\u223A spr_u223A = this.ᜁ;
					spr_u223A.ᜁ(spr_u223A.ᜏ() + A_0.ToString());
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								int a_ = this.AddFont(A_1);
								this.ᜁ.ᜀ(length, length + A_0.Length - 1, a_);
								A_0.Length = 0;
								if (true)
								{
								}
								num = 0;
								continue;
							}
							}
							break;
						case 2:
							if (A_0.Length > 0)
							{
								num = 1;
								continue;
							}
							return;
						}
						break;
					}
				}
			}
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x003DA3D8 File Offset: 0x003D93D8
		protected override int AddFont(IFont fontToAdd)
		{
			int a_ = 19;
			int num = 7;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_91;
				case 1:
					goto IL_47;
				case 2:
					goto IL_91;
				case 3:
				{
					XlsFont xlsFont;
					if (xlsFont == fontToAdd)
					{
						num = 5;
						continue;
					}
					num2++;
					num = 2;
					continue;
				}
				case 4:
					goto IL_D9;
				case 5:
					return num2;
				case 6:
					if (num2 < count)
					{
						XlsFont xlsFont = this.ᜌ[num2];
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_47;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				if (fontToAdd == null)
				{
					num = 1;
					continue;
				}
				fontToAdd = ((XlsFont)fontToAdd).Font;
				num2 = 0;
				count = this.ᜌ.Count;
				num = 0;
				continue;
				IL_91:
				num = 6;
			}
			IL_47:
			throw new ArgumentNullException(RecordTableEnumerator.b("⽈⑊⍌㭎Ր㱒ᑔ㍖㵘", a_));
			IL_D9:
			this.ᜌ.Add((XlsFont)fontToAdd);
			return this.ᜌ.Count - 1;
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x003DA508 File Offset: 0x003D9508
		private void ᜀ(XlsFont A_0, string A_1)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 9;
				string text;
				ENUMLOGFONTEX enumlogfontex;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (A_1.Length == 0)
						{
							num = 1;
							continue;
						}
						int num2 = A_1.IndexOf(',');
						text = null;
						num = 10;
						continue;
					}
					case 1:
						goto IL_173;
					case 2:
						goto IL_14D;
					case 3:
						goto IL_7D;
					case 4:
						if (enumlogfontex != null)
						{
							num = 2;
							continue;
						}
						goto IL_175;
					case 5:
						if (A_1 != null)
						{
							num = 6;
							continue;
						}
						return;
					case 6:
						num = 0;
						continue;
					case 7:
						goto IL_122;
					case 8:
					{
						if (true)
						{
						}
						int num2;
						text = A_1.Substring(num2 + 1);
						A_1 = A_1.Substring(0, num2);
						num = 7;
						continue;
					}
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 10:
					{
						int num2;
						if (num2 >= 0)
						{
							num = 8;
							continue;
						}
						goto IL_122;
					}
					}
					if (A_0 == null)
					{
						num = 3;
						continue;
					}
					num = 5;
					continue;
					IL_122:
					enumlogfontex = this.ᜀ(A_1, text);
					A_0.FontName = A_1;
					num = 4;
				}
				IL_7D:
				throw new ArgumentNullException(RecordTableEnumerator.b("⹇╉≋㩍", a_));
				IL_14D:
				LOGFONT logFont = enumlogfontex.LogFont;
				Font a_2 = Font.FromLogFont(logFont);
				HFEngine.ᜀ(a_2, A_0);
				return;
				IL_173:
				return;
				IL_175:
				text = text.ToLower();
				A_0.IsBold = (text.IndexOf(RecordTableEnumerator.b("⩇╉⁋⩍", a_)) >= 0);
				A_0.IsItalic = (text.IndexOf(RecordTableEnumerator.b("ⅇ㹉ⵋ≍㥏ㅑ", a_)) >= 0);
				return;
			}
			}
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x003DA6D4 File Offset: 0x003D96D4
		internal ENUMLOGFONTEX ᜀ(string A_0, string A_1)
		{
			switch (0)
			{
			default:
			{
				LOGFONT logfont;
				for (;;)
				{
					if (true)
					{
					}
					logfont = new LOGFONT();
					int length = A_0.Length;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_94;
						case 1:
							goto IL_7D;
						case 2:
							if (A_0[length - 1] == '\0')
							{
								goto IL_96;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7D;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						}
						break;
						IL_7D:
						A_0 += '\0';
						num = 0;
					}
				}
				IL_94:
				IL_96:
				logfont.lfFaceName = A_0;
				logfont.lfCharSet = 1;
				Graphics innerGraphics = this.ᜂ.InnerGraphics;
				IntPtr hdc = innerGraphics.GetHdc();
				HFEngine.ᜀ ᜀ = new HFEngine.ᜀ();
				ᜀ.ᜀ = A_1;
				object obj = ᜀ;
				API.EnumFontFamiliesEx(hdc, logfont, new EnumFontFamExProc(HFEngine.ᜀ), ref obj, 0);
				innerGraphics.ReleaseHdc(hdc);
				return ᜀ.ᜁ;
			}
			}
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x003DA7D0 File Offset: 0x003D97D0
		private static int ᜀ(ENUMLOGFONTEX A_0, IntPtr A_1, int A_2, ref object A_3)
		{
			HFEngine.ᜀ ᜀ;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_74:
				if (string.Compare(A_0.Style, ᜀ.ᜀ, StringComparison.CurrentCultureIgnoreCase) != 0)
				{
					return 1;
				}
				num = 3;
				break;
			default:
				if (false)
				{
				}
				goto IL_34;
			}
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					return 0;
				case 1:
					goto IL_74;
				case 2:
					if (ᜀ == null)
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				case 3:
					goto IL_90;
				}
				goto IL_34;
			}
			return 0;
			IL_90:
			if (true)
			{
			}
			ᜀ.ᜁ = A_0;
			return 0;
			IL_34:
			ᜀ = (A_3 as HFEngine.ᜀ);
			num = 2;
			goto IL_1E;
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x003DA874 File Offset: 0x003D9874
		private static void ᜀ(Font A_0, XlsFont A_1)
		{
			int a_ = 17;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_76;
					default:
						goto IL_54;
					}
					break;
				case 2:
					goto IL_3C;
				case 3:
					goto IL_76;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 3;
				continue;
				IL_76:
				if (A_1 != null)
				{
					goto IL_A1;
				}
				num = 0;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆♈㹊㽌ⱎ㑐ᕒ㩔㥖ⵘ", a_));
			IL_54:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⍆ⱈ㡊㥌ॎ㹐㵒⅔", a_));
			IL_A1:
			A_1.IsBold = A_0.Bold;
			A_1.IsItalic = A_0.Italic;
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x003DA93C File Offset: 0x003D993C
		private void ᜀ(StringBuilder A_0, int A_1, int A_2)
		{
			int a_ = 9;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_34;
				case 1:
					goto IL_5B;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5B;
					default:
						goto IL_4C;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 1;
				continue;
				IL_5B:
				if (A_1 < A_2)
				{
					goto IL_8F;
				}
				if (true)
				{
				}
				num = 2;
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("崾㑀⩂⥄⍆ⱈ㥊", a_));
			IL_4C:
			if (false)
			{
			}
			return;
			IL_8F:
			A_0.Append(this.ᜁ.ᜏ(), A_1, A_2 - A_1);
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x003DA9F0 File Offset: 0x003D99F0
		private void ᜅ(StringBuilder A_0, XlsFont A_1, XlsFont A_2)
		{
			int a_ = 6;
			if (true)
			{
			}
			if (A_0 != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.ᜄ(A_0, A_1, A_2);
					this.ᜀ(A_0, A_1, A_2);
					this.ᜃ(A_0, A_1, A_2);
					this.ᜂ(A_0, A_1, A_2);
					this.ᜁ(A_0, A_1, A_2);
					return;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("帻䬽⤿⹁⁃⍅㩇", a_));
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x003DAA7C File Offset: 0x003D9A7C
		private void ᜄ(StringBuilder A_0, XlsFont A_1, XlsFont A_2)
		{
			int a_ = 11;
			int num = 32;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					if (A_1.IsItalic)
					{
						num = 6;
						continue;
					}
					goto IL_40F;
				case 1:
					if (!A_1.IsBold)
					{
						num = 13;
						continue;
					}
					goto IL_263;
				case 2:
					if (!flag)
					{
						num = 34;
						continue;
					}
					goto IL_1DB;
				case 3:
					A_0.Append(RecordTableEnumerator.b("⍀ⱂ⥄⍆", a_));
					num = 33;
					continue;
				case 4:
					if (!A_2.IsBold)
					{
						goto IL_32B;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_340;
					default:
						if (false)
						{
						}
						num = 23;
						continue;
					}
					break;
				case 5:
					goto IL_BD;
				case 6:
					goto IL_263;
				case 7:
					num = 8;
					continue;
				case 8:
					if (A_1.IsBold == A_2.IsBold)
					{
						num = 9;
						continue;
					}
					goto IL_1DB;
				case 9:
					num = 10;
					continue;
				case 10:
					if (A_1.IsItalic == A_2.IsItalic)
					{
						num = 24;
						continue;
					}
					goto IL_1DB;
				case 11:
					if (flag)
					{
						num = 18;
						continue;
					}
					goto IL_127;
				case 12:
					goto IL_34B;
				case 13:
					num = 0;
					continue;
				case 14:
					num = 4;
					continue;
				case 15:
					if (A_2.IsItalic)
					{
						num = 14;
						continue;
					}
					goto IL_40F;
				case 16:
					if (A_1.FontName == A_2.FontName)
					{
						num = 7;
						continue;
					}
					goto IL_1DB;
				case 17:
					return;
				case 18:
					num = 25;
					continue;
				case 19:
					if (flag2)
					{
						num = 22;
						continue;
					}
					num = 2;
					continue;
				case 20:
					if (!flag)
					{
						num = 29;
						continue;
					}
					goto IL_40F;
				case 21:
					goto IL_32B;
				case 22:
					goto IL_143;
				case 23:
					A_0.Append(' ');
					num = 21;
					continue;
				case 24:
					return;
				case 25:
					if (flag2)
					{
						num = 17;
						continue;
					}
					goto IL_127;
				case 26:
					if (!A_2.IsBold)
					{
						num = 35;
						continue;
					}
					goto IL_39F;
				case 27:
					if (A_2.IsItalic)
					{
						num = 28;
						continue;
					}
					num = 20;
					continue;
				case 28:
					goto IL_39F;
				case 29:
					num = 1;
					continue;
				case 30:
					if (A_2.IsBold)
					{
						num = 3;
						continue;
					}
					goto IL_187;
				case 31:
					goto IL_28C;
				case 33:
					goto IL_187;
				case 34:
					num = 16;
					continue;
				case 35:
					num = 27;
					continue;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				flag = (A_1 == null);
				flag2 = (A_2 == null);
				num = 11;
				continue;
				IL_127:
				num = 19;
				continue;
				IL_187:
				if (true)
				{
				}
				num = 15;
				continue;
				IL_1DB:
				A_0.Append(RecordTableEnumerator.b("杀", a_));
				A_0.Append('"');
				A_0.Append(A_2.FontName);
				num = 26;
				continue;
				IL_263:
				A_0.Append(',');
				A_0.Append(RecordTableEnumerator.b("㍀♂≄㉆╈⩊㽌", a_));
				num = 31;
				continue;
				IL_340:
				num = 12;
				continue;
				IL_32B:
				A_0.Append(RecordTableEnumerator.b("⡀㝂⑄⭆⁈⡊", a_));
				goto IL_340;
				IL_39F:
				A_0.Append(',');
				num = 30;
			}
			IL_BD:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍀㙂ⱄ⭆ⵈ⹊㽌", a_));
			IL_143:
			throw new ArgumentNullException(RecordTableEnumerator.b("≀㙂㝄ņ♈╊㥌", a_));
			IL_28C:
			IL_34B:
			IL_40F:
			A_0.Append('"');
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x003DAEA4 File Offset: 0x003D9EA4
		private void ᜃ(StringBuilder A_0, XlsFont A_1, XlsFont A_2)
		{
			int a_ = 5;
			int num = 6;
			for (;;)
			{
				bool flag;
				bool flag2;
				FontUnderlineType underline;
				switch (num)
				{
				case 0:
					if (flag)
					{
						num = 15;
						continue;
					}
					goto IL_1B6;
				case 1:
					num = 13;
					continue;
				case 2:
					if (flag2)
					{
						num = 10;
						continue;
					}
					goto IL_1B6;
				case 3:
					if (flag)
					{
						num = 5;
						continue;
					}
					num = 4;
					continue;
				case 4:
					if (!flag2)
					{
						num = 9;
						continue;
					}
					goto IL_11B;
				case 5:
					goto IL_1CF;
				case 7:
					goto IL_6B;
				case 8:
					if (A_1.Underline == A_2.Underline)
					{
						num = 11;
						continue;
					}
					goto IL_11B;
				case 9:
					num = 8;
					continue;
				case 10:
					num = 0;
					continue;
				case 11:
					goto IL_220;
				case 12:
					num = 14;
					continue;
				case 13:
					goto IL_1B4;
				case 14:
					for (;;)
					{
						switch (underline)
						{
						case FontUnderlineType.SingleAccounting:
							goto IL_14F;
						case FontUnderlineType.DoubleAccounting:
							goto IL_18A;
						default:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_9F;
							}
							break;
						}
					}
					IL_9F:
					if (false)
					{
					}
					num = 1;
					continue;
				case 15:
					return;
				case 16:
					switch (underline)
					{
					case FontUnderlineType.None:
						goto IL_E9;
					case FontUnderlineType.Single:
						goto IL_14F;
					case FontUnderlineType.Double:
						goto IL_18A;
					default:
						num = 12;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				flag2 = (A_1 == null);
				flag = (A_2 == null);
				num = 2;
				continue;
				IL_11B:
				underline = A_2.Underline;
				num = 16;
				continue;
				IL_1B6:
				num = 3;
			}
			IL_6B:
			throw new ArgumentNullException(RecordTableEnumerator.b("夺䠼嘾ⵀ❂⁄㕆", a_));
			IL_E9:
			this.ᜃ(A_0, null, A_1);
			return;
			IL_14F:
			A_0.Append(RecordTableEnumerator.b("ᴺ", a_));
			A_0.Append('U');
			return;
			IL_18A:
			A_0.Append(RecordTableEnumerator.b("ᴺ", a_));
			A_0.Append('E');
			return;
			IL_1B4:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("为匼嬾⑀ㅂ⥄⹆❈⹊", a_));
			IL_1CF:
			throw new ArgumentNullException(RecordTableEnumerator.b("堺䠼䴾݀ⱂ⭄㍆", a_));
			IL_220:
			if (true)
			{
			}
		}

		// Token: 0x06006177 RID: 24951 RVA: 0x003DB0EC File Offset: 0x003DA0EC
		private new void ᜂ(StringBuilder A_0, XlsFont A_1, XlsFont A_2)
		{
			int a_ = 2;
			int num = 2;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (flag)
					{
						num = 17;
						continue;
					}
					goto IL_1DE;
				case 1:
					goto IL_152;
				case 3:
					goto IL_1F7;
				case 4:
					num = 13;
					continue;
				case 5:
					return;
				case 6:
					goto IL_1B0;
				case 7:
					if (flag)
					{
						num = 3;
						continue;
					}
					num = 16;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_75;
					default:
						if (false)
						{
						}
						if (A_1.IsSuperscript == A_2.IsSuperscript)
						{
							num = 4;
							continue;
						}
						goto IL_134;
					}
					break;
				case 9:
					goto IL_6F;
				case 10:
					goto IL_75;
				case 11:
					if (flag2)
					{
						num = 15;
						continue;
					}
					goto IL_1DE;
				case 12:
					if (A_2.IsSuperscript)
					{
						num = 1;
						continue;
					}
					num = 14;
					continue;
				case 13:
					if (A_1.IsSubscript == A_2.IsSubscript)
					{
						num = 5;
						continue;
					}
					goto IL_134;
				case 14:
					if (A_2.IsSubscript)
					{
						num = 6;
						continue;
					}
					goto IL_221;
				case 15:
					num = 0;
					continue;
				case 16:
					if (!flag2)
					{
						num = 10;
						continue;
					}
					goto IL_134;
				case 17:
					return;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				flag2 = (A_1 == null);
				flag = (A_2 == null);
				num = 11;
				continue;
				IL_75:
				num = 8;
				continue;
				IL_134:
				num = 12;
				continue;
				IL_1DE:
				num = 7;
			}
			IL_6F:
			throw new ArgumentNullException(RecordTableEnumerator.b("娷伹唻刽␿❁㙃", a_));
			IL_152:
			A_0.Append(RecordTableEnumerator.b("ḷ", a_));
			A_0.Append('X');
			return;
			IL_1B0:
			A_0.Append(RecordTableEnumerator.b("ḷ", a_));
			A_0.Append('Y');
			return;
			IL_1F7:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬷伹主砽⼿ⱁぃ", a_));
			IL_221:
			this.ᜂ(A_0, null, A_1);
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x003DB324 File Offset: 0x003DA324
		private new void ᜁ(StringBuilder A_0, XlsFont A_1, XlsFont A_2)
		{
			int a_ = 7;
			int num = 9;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					num = 12;
					continue;
				case 1:
					if (A_1.IsStrikethrough != A_2.IsStrikethrough)
					{
						num = 4;
						continue;
					}
					return;
				case 2:
					goto IL_E6;
				case 3:
					goto IL_13C;
				case 4:
					goto IL_104;
				case 5:
					if (flag)
					{
						num = 14;
						continue;
					}
					goto IL_123;
				case 6:
					if (flag2)
					{
						num = 0;
						continue;
					}
					goto IL_197;
				case 7:
					goto IL_64;
				case 8:
					if (flag2)
					{
						num = 11;
						continue;
					}
					goto IL_123;
				case 10:
					if (flag)
					{
						num = 3;
						continue;
					}
					num = 15;
					continue;
				case 11:
					goto IL_C3;
				case 12:
					if (!A_2.IsStrikethrough)
					{
						num = 2;
						continue;
					}
					goto IL_197;
				case 13:
					num = 1;
					continue;
				case 14:
					return;
				case 15:
					if (flag2)
					{
						goto IL_104;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C3;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				flag2 = (A_1 == null);
				flag = (A_2 == null);
				num = 8;
				continue;
				IL_C3:
				num = 5;
				continue;
				IL_104:
				num = 6;
				continue;
				IL_123:
				num = 10;
			}
			IL_64:
			throw new ArgumentNullException(RecordTableEnumerator.b("弼䨾⡀⽂⅄≆㭈", a_));
			IL_E6:
			if (true)
			{
			}
			return;
			IL_13C:
			throw new ArgumentNullException(RecordTableEnumerator.b("帼䨾㍀Ղ⩄⥆㵈", a_));
			IL_197:
			A_0.Append(RecordTableEnumerator.b("ᬼ", a_));
			A_0.Append('S');
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x003DB4F0 File Offset: 0x003DA4F0
		private void ᜀ(StringBuilder A_0, XlsFont A_1, XlsFont A_2)
		{
			int a_ = 18;
			int num = 9;
			for (;;)
			{
				if (true)
				{
				}
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					if (A_1.Size == A_2.Size)
					{
						num = 3;
						continue;
					}
					goto IL_147;
				case 1:
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
						goto IL_A2;
					}
					break;
				case 3:
					return;
				case 4:
					num = 7;
					continue;
				case 5:
					if (!flag)
					{
						num = 1;
						continue;
					}
					goto IL_147;
				case 6:
					return;
				case 7:
					if (flag2)
					{
						num = 6;
						continue;
					}
					goto IL_79;
				case 8:
					if (flag2)
					{
						num = 2;
						continue;
					}
					num = 5;
					continue;
				case 10:
					if (flag)
					{
						num = 4;
						continue;
					}
					goto IL_79;
				case 11:
					goto IL_5C;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				flag = (A_1 == null);
				flag2 = (A_2 == null);
				goto IL_B4;
				IL_79:
				num = 8;
				continue;
				IL_B4:
				num = 10;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩇㽉╋≍㑏㝑♓", a_));
			IL_A2:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⭇㽉㹋ࡍ㽏㱑⁓", a_));
			IL_147:
			A_0.Append(RecordTableEnumerator.b("湇", a_));
			A_0.Append(A_2.Size);
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x003DB668 File Offset: 0x003DA668
		protected override XlsFont GetFontByIndex(int iFontIndex)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.ᜌ[iFontIndex];
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x003DB6B0 File Offset: 0x003DA6B0
		public override void Clear()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.Clear();
			this.ᜌ.Clear();
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x003DB6FC File Offset: 0x003DA6FC
		public override void EndUpdate()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int length = this.ᜁ.ᜏ().Length;
					goto IL_70;
				}
				case 1:
				{
					int length;
					if (length > 0)
					{
						num = 2;
						continue;
					}
					goto IL_A2;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
					{
						if (false)
						{
						}
						int length;
						base.SetRichTextFont(0, length - 1, this.DefaultFont);
						num = 3;
						continue;
					}
					}
					break;
				case 3:
					goto IL_5D;
				}
				if (this.ᜁ.ᜆ() == 0)
				{
					num = 0;
					continue;
				}
				break;
				IL_70:
				num = 1;
			}
			IL_5D:
			IL_A2:
			if (true)
			{
			}
		}

		// Token: 0x04002E9A RID: 11930
		private new const string ᜀ = "&";

		// Token: 0x04002E9B RID: 11931
		private new const char ᜁ = 'U';

		// Token: 0x04002E9C RID: 11932
		private new const char ᜂ = 'E';

		// Token: 0x04002E9D RID: 11933
		private new const char ᜃ = 'S';

		// Token: 0x04002E9E RID: 11934
		private const char ᜄ = 'Y';

		// Token: 0x04002E9F RID: 11935
		private const char ᜅ = 'X';

		// Token: 0x04002EA0 RID: 11936
		private const char ᜆ = '"';

		// Token: 0x04002EA1 RID: 11937
		private const char ᜇ = ',';

		// Token: 0x04002EA2 RID: 11938
		private const string ᜈ = "bold";

		// Token: 0x04002EA3 RID: 11939
		private new const string ᜉ = "italic";

		// Token: 0x04002EA4 RID: 11940
		private const string ᜊ = "regular";

		// Token: 0x04002EA5 RID: 11941
		private const char ᜋ = ' ';

		// Token: 0x04002EA6 RID: 11942
		private List<XlsFont> ᜌ = new List<XlsFont>();

		// Token: 0x0200063D RID: 1597
		private new class ᜀ
		{
			// Token: 0x04002EA7 RID: 11943
			public string ᜀ;

			// Token: 0x04002EA8 RID: 11944
			public ENUMLOGFONTEX ᜁ;
		}
	}
}
