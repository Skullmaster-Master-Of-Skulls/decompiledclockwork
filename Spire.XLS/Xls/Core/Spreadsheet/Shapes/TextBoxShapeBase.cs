using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x02000052 RID: 82
	public class TextBoxShapeBase : XlsShape
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x000546AC File Offset: 0x000536AC
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x000546F0 File Offset: 0x000536F0
		public CommentHAlignType HAlignment
		{
			get
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
				return this.ᜂ;
			}
			set
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
				this.ᜂ = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00054734 File Offset: 0x00053734
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00054778 File Offset: 0x00053778
		public CommentVAlignType VAlignment
		{
			get
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
				return this.ᜃ;
			}
			set
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
				this.ᜃ = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x000547BC File Offset: 0x000537BC
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00054800 File Offset: 0x00053800
		public TextRotationType TextRotation
		{
			get
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
				return this.ᜄ;
			}
			set
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00054844 File Offset: 0x00053844
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00054888 File Offset: 0x00053888
		public bool IsTextLocked
		{
			get
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
				return this.ᜅ;
			}
			set
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
				this.ᜅ = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000548CC File Offset: 0x000538CC
		public IRichTextString RichText
		{
			get
			{
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
							goto IL_3E;
						default:
							goto IL_64;
						}
						break;
					case 2:
						goto IL_3E;
					}
					if (true)
					{
					}
					if (this.ᜆ == null)
					{
						num = 2;
						continue;
					}
					goto IL_6C;
					IL_3E:
					this.InitializeVariables();
					num = 0;
				}
				IL_64:
				if (false)
				{
				}
				IL_6C:
				return this.ᜆ;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0005494C File Offset: 0x0005394C
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x00054994 File Offset: 0x00053994
		public string Text
		{
			get
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
				return this.RichText.Text;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.RichText.Text = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x000549DC File Offset: 0x000539DC
		internal RichTextString InnerRichText
		{
			get
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
				return this.ᜆ;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00054A20 File Offset: 0x00053A20
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x00054A64 File Offset: 0x00053A64
		public Color FillColor
		{
			get
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
				return this.ᜉ;
			}
			set
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
				this.ᜉ = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00054AA8 File Offset: 0x00053AA8
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x00054AEC File Offset: 0x00053AEC
		public Dictionary<string, string> UnknownBodyProperties
		{
			get
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
				return this.ᜊ;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ = value;
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00054B30 File Offset: 0x00053B30
		internal TextBoxShapeBase(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.InitializeVariables();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00054B6C File Offset: 0x00053B6C
		internal TextBoxShapeBase(spr\u1DF5 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00054BA4 File Offset: 0x00053BA4
		public override IShape Clone(object parent, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes, bool addToCollections)
		{
			switch (0)
			{
			default:
			{
				TextBoxShapeBase textBoxShapeBase;
				for (;;)
				{
					textBoxShapeBase = (TextBoxShapeBase)base.Clone(parent, hashNewNames, dicFontIndexes, addToCollections);
					textBoxShapeBase.ᜆ = (RichTextString)this.ᜆ.Clone(textBoxShapeBase);
					int num = textBoxShapeBase.ᜆ.TextObject.ᜆ();
					XlsWorkbook xlsWorkbook = textBoxShapeBase.Workbook as XlsWorkbook;
					int num2 = 0;
					int num3 = 4;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (num2 < this.ᜆ.Text.Length)
							{
								num3 = 9;
								continue;
							}
							goto IL_242;
						case 1:
							goto IL_242;
						case 2:
							try
							{
								num3 = 1;
								for (;;)
								{
									IL_173:
									switch (num3)
									{
									case 0:
										num3 = 4;
										continue;
									case 3:
									{
										Dictionary<string, string>.Enumerator enumerator;
										while (enumerator.MoveNext())
										{
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
												KeyValuePair<string, string> keyValuePair = enumerator.Current;
												textBoxShapeBase.ᜊ.Add(keyValuePair.Key, keyValuePair.Value);
												num3 = 2;
												goto IL_173;
											}
											}
										}
										num3 = 0;
										continue;
									}
									case 4:
										goto IL_205;
									}
									IL_1D9:
									num3 = 3;
									continue;
									goto IL_1D9;
								}
								IL_205:
								return textBoxShapeBase;
							}
							finally
							{
								Dictionary<string, string>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							goto IL_215;
						case 3:
							goto IL_215;
						case 4:
							goto IL_215;
						case 5:
						{
							textBoxShapeBase.ᜊ = new Dictionary<string, string>();
							Dictionary<string, string>.Enumerator enumerator = this.ᜊ.GetEnumerator();
							num3 = 2;
							continue;
						}
						case 6:
							num3 = 8;
							continue;
						case 7:
							if (num2 >= num)
							{
								if (true)
								{
								}
								num3 = 6;
								continue;
							}
							num3 = 0;
							continue;
						case 8:
							if (this.ᜊ != null)
							{
								num3 = 5;
								continue;
							}
							return textBoxShapeBase;
						case 9:
						{
							IFont font = this.ᜆ.GetFont(num2);
							FontWrapper fontWrapper = xlsWorkbook.AddFont(font) as FontWrapper;
							textBoxShapeBase.ᜆ.TextObject.ᜂ(0, fontWrapper.FontIndex);
							num3 = 1;
							continue;
						}
						}
						break;
						IL_215:
						num3 = 7;
						continue;
						IL_242:
						num2++;
						num3 = 3;
					}
				}
				return textBoxShapeBase;
			}
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00054E18 File Offset: 0x00053E18
		internal void ᜀ(spr\u223A A_0)
		{
			int a_ = 0;
			if (A_0 == null)
			{
				for (;;)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_36;
					}
				}
				IL_36:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("唵圷圹儻嬽⸿㙁၃⍅ぇ㹉", a_));
			}
			RichTextString richTextString = (RichTextString)this.RichText;
			richTextString.ᜀ(A_0);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00054E88 File Offset: 0x00053E88
		internal new spr\u2016 ᜁ(spr\u1D3B A_0)
		{
			spr\u2016 spr_u;
			for (;;)
			{
				spr_u = (spr\u2016)spr\u231F.ᜀ(MsoRecords.msofbtClientTextbox);
				spr\u1FF0 spr_u1FF = (spr\u1FF0)spr\u175E.ᜀ(TBIFFRecord.TextObject);
				int num = 3;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4C;
						default:
							if (false)
							{
							}
							num2 = this.ᜆ.Text.Length;
							goto IL_8B;
						}
						break;
					case 1:
						num = 2;
						continue;
					case 2:
						num2 = 0;
						goto IL_8B;
					case 3:
						goto IL_4C;
					case 4:
						goto IL_86;
					case 5:
						this.ᜀ(spr_u);
						this.ᜀ(spr_u, spr_u1FF);
						num = 4;
						continue;
					case 6:
						if (num3 > 0)
						{
							num = 5;
							continue;
						}
						goto IL_137;
					}
					break;
					IL_4C:
					if (this.ᜆ == null)
					{
						num = 1;
						continue;
					}
					num = 0;
					continue;
					IL_8B:
					num3 = num2;
					spr_u1FF.ᜀ(this.HAlignment);
					spr_u1FF.ᜀ(this.VAlignment);
					spr_u1FF.ᜁ((ushort)num3);
					spr_u1FF.ᜀ(0);
					spr_u1FF.ᜀ(this.IsTextLocked);
					spr_u1FF.ᜀ(this.TextRotation);
					spr_u.ᜀ(spr_u1FF);
					num = 6;
				}
			}
			IL_86:
			IL_137:
			if (true)
			{
			}
			return spr_u;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00054FD8 File Offset: 0x00053FD8
		private void ᜀ(spr\u2016 A_0, spr\u1FF0 A_1)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				int num = 8;
				int num3;
				for (;;)
				{
					byte[] array;
					int num5;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_179;
						default:
							if (false)
							{
							}
							num = 13;
							continue;
						}
						break;
					case 1:
						goto IL_107;
					case 2:
						goto IL_1A5;
					case 3:
						goto IL_7A;
					case 4:
						if (array == null)
						{
							num = 0;
							continue;
						}
						num = 12;
						continue;
					case 5:
						goto IL_75;
					case 6:
						num = 1;
						continue;
					case 7:
					{
						if (array != null)
						{
							num = 11;
							continue;
						}
						spr\u2553 spr_u = (spr\u2553)spr\u175E.ᜀ(TBIFFRecord.Continue);
						spr_u.ᜀ(0);
						A_0.ᜀ(spr_u);
						num = 2;
						continue;
					}
					case 9:
					{
						int num2;
						if (num2 >= num3)
						{
							num = 6;
							continue;
						}
						int num4 = Math.Min(num3 - num2, 8224);
						spr\u2553 spr_u = (spr\u2553)spr\u175E.ᜀ(TBIFFRecord.Continue);
						byte[] array2 = new byte[num4];
						Buffer.BlockCopy(array, num2, array2, 0, num4);
						spr_u.ᜀ(array2);
						spr_u.ᜀ(num4);
						A_0.ᜀ(spr_u);
						num2 += num4;
						num = 10;
						continue;
					}
					case 10:
						goto IL_179;
					case 11:
					{
						int num2 = 0;
						num = 3;
						continue;
					}
					case 12:
						num5 = array.Length;
						goto IL_1C7;
					case 13:
						num5 = 0;
						goto IL_1C7;
					}
					if (A_0 == null)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					array = this.ᜀ(this.ᜀ());
					num = 4;
					continue;
					IL_7A:
					num = 9;
					continue;
					IL_179:
					goto IL_7A;
					IL_1C7:
					num3 = num5;
					num = 7;
				}
				IL_75:
				throw new ArgumentNullException(RecordTableEnumerator.b("似娾㉀㙂⥄㍆", a_));
				IL_107:
				IL_1A5:
				A_1.ᜀ((ushort)num3);
				return;
			}
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000551D8 File Offset: 0x000541D8
		private void ᜀ(spr\u2016 A_0)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num2;
					int length;
					string text;
					switch (num)
					{
					case 0:
						goto IL_EE;
					case 2:
					{
						if (num2 >= length)
						{
							num = 4;
							continue;
						}
						int num3 = Math.Min(length - num2, 4111);
						spr\u2553 spr_u = (spr\u2553)spr\u175E.ᜀ(TBIFFRecord.Continue);
						spr_u.AutoGrowData = true;
						string a_2 = text.Substring(num2, num3);
						int a_3 = spr_u.ᜂ(0, a_2);
						spr_u.ᜀ(a_3);
						num2 += num3;
						A_0.ᜀ(spr_u);
						num = 5;
						continue;
					}
					case 3:
						goto IL_4D;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41;
						}
						goto Block_3;
					case 5:
						goto IL_EE;
					}
					IL_41:
					if (A_0 == null)
					{
						num = 3;
						continue;
					}
					text = this.ᜆ.Text;
					length = text.Length;
					num2 = 0;
					num = 0;
					continue;
					IL_EE:
					num = 2;
				}
				IL_4D:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄㑆㱈❊㥌", a_));
				Block_3:
				if (false)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0005531C File Offset: 0x0005431C
		private void ᜀ(spr\u1FF0 A_0)
		{
			int a_ = 10;
			if (A_0 == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㐿❁㱃㉅݇⡉♋⭍㍏♑", a_));
			}
			this.ᜂ = A_0.ᜇ();
			this.ᜃ = A_0.ᜊ();
			this.ᜄ = A_0.ᜀ();
			this.ᜅ = A_0.ᜆ();
			this.ᜇ = (int)A_0.ᜈ();
			this.ᜈ = (int)A_0.ᜉ();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000553C4 File Offset: 0x000543C4
		private void ᜀ(string A_0, byte[] A_1, ExcelParseOptions A_2)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				spr\u223A spr_u223A;
				for (;;)
				{
					spr_u223A = new spr\u223A();
					spr_u223A.ᜁ(A_0);
					int num = 1;
					for (;;)
					{
						byte[] array;
						switch (num)
						{
						case 0:
							goto IL_F5;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_E2;
							default:
								if (false)
								{
								}
								if (A_1 != null)
								{
									num = 3;
									continue;
								}
								goto IL_F7;
							}
							break;
						case 2:
							goto IL_E2;
						case 3:
						{
							int num2 = A_1.Length / 8;
							array = new byte[num2 * 4];
							int num3 = 0;
							num = 5;
							continue;
						}
						case 4:
						{
							int num2;
							int num3;
							if (num3 >= num2)
							{
								num = 2;
								continue;
							}
							Buffer.BlockCopy(A_1, num3 * 8, array, num3 * 4, 4);
							num3++;
							num = 6;
							continue;
						}
						case 5:
							goto IL_88;
						case 6:
							goto IL_88;
						}
						break;
						IL_88:
						num = 4;
						continue;
						IL_E2:
						spr_u223A.ᜀ(array);
						num = 0;
					}
				}
				IL_F5:
				IL_F7:
				this.ᜆ.Parse(spr_u223A, null, A_2);
				return;
			}
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000554D8 File Offset: 0x000544D8
		private byte[] ᜀ()
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				for (;;)
				{
					IL_63:
					array = this.ᜆ.TextObject.ᜃ();
					int num = 4;
					for (;;)
					{
						int num2;
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
							switch (num)
							{
							case 0:
								if (array[0] == 0)
								{
									num = 2;
									continue;
								}
								goto IL_8E;
							case 1:
								goto IL_17B;
							case 2:
								num = 7;
								continue;
							case 3:
								goto IL_19D;
							case 4:
								if (array != null)
								{
									num = 8;
									continue;
								}
								goto IL_F0;
							case 5:
								goto IL_8E;
							case 6:
								goto IL_1A2;
							case 7:
								if (array[1] != 0)
								{
									num = 5;
									continue;
								}
								goto IL_1A2;
							case 8:
								num = 9;
								continue;
							case 9:
								if (array.Length == 0)
								{
									num = 3;
									continue;
								}
								num = 0;
								continue;
							case 10:
							{
								byte[] array2 = array;
								array = new byte[array.Length + 4];
								array2.CopyTo(array, 0);
								BitConverter.GetBytes((ushort)this.ᜆ.Text.Length).CopyTo(array, array2.Length);
								BitConverter.GetBytes(0).CopyTo(array, array2.Length + 2);
								num = 1;
								continue;
							}
							case 11:
								if ((int)BitConverter.ToUInt16(array, num2 - 4) != this.ᜆ.Text.Length)
								{
									num = 10;
									continue;
								}
								return array;
							}
							goto IL_63;
							IL_8E:
							byte[] array3 = array;
							array = new byte[array.Length + 4];
							array3.CopyTo(array, 4);
							BitConverter.GetBytes(0).CopyTo(array, 0);
							BitConverter.GetBytes(0).CopyTo(array, 1);
							num = 6;
							continue;
						}
						}
						IL_1A2:
						num2 = array.Length;
						if (true)
						{
						}
						num = 11;
					}
				}
				IL_F0:
				byte[] array4 = new byte[8];
				byte[] bytes = BitConverter.GetBytes((ushort)this.ᜆ.Text.Length);
				array4[4] = bytes[0];
				array4[5] = bytes[1];
				return array4;
				IL_17B:
				return array;
				IL_19D:
				goto IL_F0;
			}
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000556F4 File Offset: 0x000546F4
		private byte[] ᜀ(byte[] A_0)
		{
			int num = 3;
			byte[] array;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_33;
				case 1:
					goto IL_97;
				case 2:
					goto IL_97;
				case 4:
					if (num2 >= num3)
					{
						num = 5;
						continue;
					}
					Buffer.BlockCopy(A_0, num2 * 4, array, num2 * 8, 4);
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B1;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 5:
					goto IL_B1;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num3 = A_0.Length / 4;
				array = new byte[num3 * 8];
				num2 = 0;
				num = 1;
				continue;
				IL_97:
				num = 4;
			}
			IL_33:
			return null;
			IL_B1:
			if (true)
			{
			}
			return array;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000557C0 File Offset: 0x000547C0
		protected virtual void InitializeVariables()
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
			this.ᜆ = new RichTextString(base.AppImplementation, base.ParentWorkbook, false, true);
			this.m_bSupportOptions = true;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0005581C File Offset: 0x0005481C
		[CLSCompliant(false)]
		internal virtual void ParseClientTextBoxRecord(spr\u2016 textBox, ExcelParseOptions options)
		{
			int a_ = 4;
			int num = 1;
			for (;;)
			{
				string text;
				byte[] array;
				switch (num)
				{
				case 0:
					if (text != null)
					{
						num = 6;
						continue;
					}
					return;
				case 2:
					goto IL_E1;
				case 3:
					goto IL_40;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E1;
					default:
						if (false)
						{
						}
						if (array != null)
						{
							num = 2;
							continue;
						}
						return;
					}
					break;
				case 5:
					return;
				case 6:
					num = 4;
					continue;
				}
				if (textBox == null)
				{
					num = 3;
					continue;
				}
				this.RichText.Text = string.Empty;
				this.ᜀ(textBox.ᜄ());
				text = textBox.ᜃ();
				array = textBox.ᜀ();
				num = 0;
				continue;
				IL_E1:
				this.ᜀ(text, array, options);
				num = 5;
			}
			IL_40:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("丹夻䘽㐿A⭃㹅", a_));
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00055920 File Offset: 0x00054920
		public void CopyFrom(TextBoxShapeBase source, Dictionary<int, int> dicFontIndexes)
		{
			int a_ = 16;
			if (source == null)
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
					if (true)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("㕅❇㽉㹋ⵍ㕏", a_));
				}
			}
			this.ᜆ.CopyFrom(source.ᜆ, dicFontIndexes);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00055990 File Offset: 0x00054990
		[CLSCompliant(false)]
		internal override spr\u23E7 CreateDefaultOptions()
		{
			spr\u23E7 spr_u23E;
			for (;;)
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
					spr_u23E = base.CreateDefaultOptions();
					spr_u23E.ᜉ(3);
					spr_u23E.ᜈ(2);
					break;
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						base.ᜀ(spr_u23E, MsoOptions.TextId, 19990000);
						if (true)
						{
						}
						num = 2;
						continue;
					case 1:
						if (this.Text.Length != 0)
						{
							num = 0;
							continue;
						}
						goto IL_94;
					case 2:
						goto IL_92;
					}
					break;
				}
			}
			IL_92:
			IL_94:
			this.ᜂ(spr_u23E);
			base.ᜄ(spr_u23E);
			return spr_u23E;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00055A40 File Offset: 0x00054A40
		internal new void ᜂ(spr\u23E7 A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.ᜁ(A_0, MsoOptions.TextDirection, 2U);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00055A8C File Offset: 0x00054A8C
		[CLSCompliant(false)]
		internal override spr\u23E7 SerializeOptions(spr\u1D3B parent)
		{
			int num = 2;
			spr\u23E7 a_;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_104;
				case 1:
					if (this.\u1712 == null)
					{
						num = 5;
						continue;
					}
					goto IL_10D;
				case 2:
					if (true)
					{
					}
					break;
				case 3:
					num = 7;
					continue;
				case 4:
					if (!this.m_bUpdateLineFill)
					{
						num = 6;
						continue;
					}
					goto IL_C0;
				case 5:
					goto IL_C0;
				case 6:
					IL_97:
					num = 1;
					continue;
				case 7:
					if (this.\u1712.ᜀ().Length != 0)
					{
						num = 8;
						continue;
					}
					goto IL_6B;
				case 8:
					goto IL_BE;
				}
				if (this.\u1712 != null)
				{
					num = 3;
					continue;
				}
				IL_6B:
				a_ = this.\u1712;
				num = 4;
				continue;
				IL_C0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_97;
				default:
					if (false)
					{
					}
					a_ = (this.\u1712 = this.CreateDefaultOptions());
					a_ = base.ᜆ(this.\u1712);
					num = 0;
					break;
				}
			}
			IL_BE:
			return this.\u1712;
			IL_104:
			IL_10D:
			base.ᜇ(a_);
			base.ᜀ(a_, MsoOptions.AlternativeText, base.AlternativeText);
			return this.\u1712;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00055BC8 File Offset: 0x00054BC8
		[CLSCompliant(false)]
		internal override void ParseOtherRecords(spr\u1D3B subRecord, ExcelParseOptions options)
		{
			int a_ = 8;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					MsoRecords msoRecords;
					if (msoRecords != MsoRecords.msofbtClientTextbox)
					{
						num = 2;
						continue;
					}
					goto IL_9A;
				}
				case 2:
					return;
				case 3:
					goto IL_5A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (subRecord != null)
					{
						if (true)
						{
						}
						MsoRecords msoRecords = subRecord.\u1717();
						num = 0;
						continue;
					}
					break;
				}
				num = 3;
			}
			IL_5A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽㔿⁁ᙃ⍅⭇╉㹋⩍", a_));
			IL_9A:
			this.ParseClientTextBoxRecord(subRecord as spr\u2016, options);
		}

		// Token: 0x0400015E RID: 350
		private new const int ᜀ = 8;

		// Token: 0x0400015F RID: 351
		private new const uint ᜁ = 2U;

		// Token: 0x04000160 RID: 352
		private new CommentHAlignType ᜂ = CommentHAlignType.Left;

		// Token: 0x04000161 RID: 353
		private new CommentVAlignType ᜃ = CommentVAlignType.Top;

		// Token: 0x04000162 RID: 354
		private new TextRotationType ᜄ;

		// Token: 0x04000163 RID: 355
		private new bool ᜅ = true;

		// Token: 0x04000164 RID: 356
		private bool \u2609\u00A3\u0097\u009C;

		// Token: 0x04000165 RID: 357
		private bool[] \u25D9\u007F\u009A\u0080;

		// Token: 0x04000166 RID: 358
		private new RichTextString ᜆ;

		// Token: 0x04000167 RID: 359
		private bool[] \u25D8\u00AB\u009D\u0098;

		// Token: 0x04000168 RID: 360
		private bool[] \u2460\u00B0\u0094\u0097;

		// Token: 0x04000169 RID: 361
		private long \u2609ª\u008D\u008B;

		// Token: 0x0400016A RID: 362
		private new int ᜇ;

		// Token: 0x0400016B RID: 363
		private bool[] \u25D8\u00AE\u0089\u0081;

		// Token: 0x0400016C RID: 364
		private int ᜈ;

		// Token: 0x0400016D RID: 365
		private Color ᜉ = spr\u1D39.ᜂ;

		// Token: 0x0400016E RID: 366
		private Dictionary<string, string> ᜊ;
	}
}
