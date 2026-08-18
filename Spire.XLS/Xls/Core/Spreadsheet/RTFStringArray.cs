using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000609 RID: 1545
	public class RTFStringArray : XlsObject, IRTFWrapper
	{
		// Token: 0x06005B65 RID: 23397 RVA: 0x0038FA54 File Offset: 0x0038EA54
		internal RTFStringArray(spr\u1DF5 A_0, object A_1, IXLSRange A_2)
		{
			int a_ = 3;
			base..ctor(A_0, A_1);
			if (A_2 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("䬸娺匼堾⑀", a_));
			}
			this.ᜀ = A_2;
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x0038FA94 File Offset: 0x0038EA94
		public IFont GetFont(int iPosition)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_63:
					IXLSRange[] cells = this.ᜀ.Cells;
					int num = cells.Length;
					int num2 = 4;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14D;
						default:
							if (false)
							{
							}
							switch (num2)
							{
							case 0:
							{
								int num3;
								if (num3 >= num)
								{
									num2 = 6;
									continue;
								}
								num2 = 2;
								continue;
							}
							case 1:
								goto IL_10B;
							case 2:
							{
								int num3;
								if (!cells[num3].HasRichText)
								{
									num2 = 11;
									continue;
								}
								num2 = 10;
								continue;
							}
							case 3:
							{
								if (!cells[0].HasRichText)
								{
									num2 = 5;
									continue;
								}
								IFont font = ((RichTextString)cells[0].RichText).GetFont(iPosition);
								int num3 = 1;
								num2 = 1;
								continue;
							}
							case 4:
								if (num == 0)
								{
									num2 = 9;
									continue;
								}
								num2 = 3;
								continue;
							case 5:
								goto IL_B8;
							case 6:
							{
								IFont font;
								return font;
							}
							case 7:
								goto IL_109;
							case 8:
								goto IL_10B;
							case 9:
								goto IL_88;
							case 10:
							{
								int num3;
								IFont font;
								if (font != ((RichTextString)cells[num3].RichText).GetFont(iPosition))
								{
									num2 = 7;
									continue;
								}
								num3++;
								num2 = 8;
								continue;
							}
							case 11:
								goto IL_174;
							}
							goto IL_63;
							IL_10B:
							num2 = 0;
							break;
						}
					}
				}
				IL_88:
				if (true)
				{
				}
				return null;
				IL_B8:
				goto IL_14D;
				IL_109:
				return null;
				IL_14D:
				return null;
				IL_174:
				return null;
			}
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0038FC1C File Offset: 0x0038EC1C
		public void SetFont(int iStartPos, int iEndPos, IFont font)
		{
			for (;;)
			{
				IL_18:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_38;
				default:
					goto IL_38;
				}
				int num;
				int num2;
				int num3;
				IXLSRange[] cells;
				for (;;)
				{
					IL_02:
					switch (num)
					{
					case 0:
						goto IL_62;
					case 1:
						if (num2 >= num3)
						{
							num = 2;
							continue;
						}
						((RichTextString)cells[num2].RichText).SetRichTextFont(iStartPos, iEndPos, font);
						num2++;
						num = 3;
						continue;
					case 2:
						return;
					case 3:
						goto IL_62;
					}
					goto IL_18;
					IL_62:
					num = 1;
				}
				IL_38:
				if (true)
				{
				}
				if (false)
				{
				}
				cells = this.ᜀ.Cells;
				num2 = 0;
				num3 = cells.Length;
				num = 0;
				goto IL_02;
			}
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x0038FCC8 File Offset: 0x0038ECC8
		public void ClearFormatting()
		{
			for (;;)
			{
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 1;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_40;
					case 1:
						goto IL_8E;
					case 2:
						goto IL_8E;
					case 3:
						if (num >= num2)
						{
							num3 = 4;
							continue;
						}
						goto IL_6F;
					case 4:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							goto IL_C6;
						}
						break;
					case 5:
						cells[num].RichText.ClearFormatting();
						num3 = 0;
						continue;
					case 6:
						if (cells[num].HasRichText)
						{
							num3 = 5;
							continue;
						}
						goto IL_40;
					}
					break;
					IL_40:
					num++;
					num3 = 2;
					continue;
					IL_6F:
					num3 = 6;
					continue;
					IL_8E:
					num3 = 3;
				}
			}
			IL_C6:
			if (false)
			{
			}
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x0038FDA4 File Offset: 0x0038EDA4
		public void Append(string text, IFont font)
		{
			for (;;)
			{
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 6;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (num >= num2)
						{
							num3 = 5;
							continue;
						}
						goto IL_71;
					case 1:
						goto IL_40;
					case 2:
						cells[num].RichText.Append(text, font);
						num3 = 1;
						continue;
					case 3:
						goto IL_90;
					case 4:
						if (cells[num].HasRichText)
						{
							num3 = 2;
							continue;
						}
						goto IL_40;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_71;
						default:
							goto IL_C0;
						}
						break;
					case 6:
						goto IL_90;
					}
					break;
					IL_40:
					num++;
					num3 = 3;
					continue;
					IL_71:
					num3 = 4;
					continue;
					IL_90:
					num3 = 0;
				}
			}
			IL_C0:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06005B6A RID: 23402 RVA: 0x0038FE84 File Offset: 0x0038EE84
		// (set) Token: 0x06005B6B RID: 23403 RVA: 0x0038FFD4 File Offset: 0x0038EFD4
		public string Text
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						int num = cells.Length;
						int num2 = 6;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_12E;
							case 1:
								goto IL_F6;
							case 2:
							{
								string text;
								if (text != null)
								{
									num2 = 7;
									continue;
								}
								return text;
							}
							case 3:
							{
								string text = null;
								num2 = 5;
								continue;
							}
							case 4:
							{
								int num3;
								if (num3 >= num)
								{
									num2 = 8;
									continue;
								}
								num2 = 10;
								continue;
							}
							case 5:
							{
								string text;
								return text;
							}
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_12E;
								default:
								{
									if (false)
									{
									}
									if (num == 0)
									{
										num2 = 9;
										continue;
									}
									string text = cells[0].Text;
									num2 = 2;
									continue;
								}
								}
								break;
							case 7:
							{
								int num3 = 1;
								num2 = 1;
								continue;
							}
							case 8:
							{
								string text;
								return text;
							}
							case 9:
								goto IL_8E;
							case 10:
							{
								string text;
								int num3;
								if (text != cells[num3].Text)
								{
									num2 = 3;
									continue;
								}
								num3++;
								num2 = 0;
								continue;
							}
							}
							break;
							IL_F6:
							if (true)
							{
							}
							num2 = 4;
							continue;
							IL_12E:
							goto IL_F6;
						}
					}
					IL_8E:
					return null;
				}
			}
			set
			{
				for (;;)
				{
					IL_18:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					case 1:
						goto IL_38;
					default:
						goto IL_38;
					}
					int num;
					int num2;
					int num3;
					IXLSRange[] cells;
					for (;;)
					{
						IL_02:
						switch (num)
						{
						case 0:
							goto IL_62;
						case 1:
							return;
						case 2:
							goto IL_62;
						case 3:
							if (num2 >= num3)
							{
								num = 1;
								continue;
							}
							cells[num2].RichText.Text = value;
							num2++;
							num = 2;
							continue;
						}
						goto IL_18;
						IL_62:
						num = 3;
					}
					IL_38:
					if (true)
					{
					}
					if (false)
					{
					}
					cells = this.ᜀ.Cells;
					num2 = 0;
					num3 = cells.Length;
					num = 0;
					goto IL_02;
				}
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06005B6C RID: 23404 RVA: 0x00390078 File Offset: 0x0038F078
		public string RtfText
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_63:
						IXLSRange[] cells = this.ᜀ.Cells;
						int num = cells.Length;
						int num2 = 10;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_146;
							default:
								if (false)
								{
								}
								switch (num2)
								{
								case 0:
									goto IL_B8;
								case 1:
									goto IL_10A;
								case 2:
									goto IL_10A;
								case 3:
								{
									int num3;
									if (num3 >= num)
									{
										num2 = 8;
										continue;
									}
									num2 = 5;
									continue;
								}
								case 4:
									goto IL_16D;
								case 5:
								{
									int num3;
									if (!cells[num3].HasRichText)
									{
										num2 = 4;
										continue;
									}
									num2 = 7;
									continue;
								}
								case 6:
									goto IL_88;
								case 7:
								{
									int num3;
									string rtfText;
									if (rtfText != cells[num3].RichText.RtfText)
									{
										num2 = 9;
										continue;
									}
									num3++;
									num2 = 2;
									continue;
								}
								case 8:
								{
									string rtfText;
									return rtfText;
								}
								case 9:
									goto IL_108;
								case 10:
									if (num == 0)
									{
										num2 = 6;
										continue;
									}
									num2 = 11;
									continue;
								case 11:
								{
									if (!cells[0].HasRichText)
									{
										num2 = 0;
										continue;
									}
									string rtfText = cells[0].RichText.RtfText;
									int num3 = 1;
									num2 = 1;
									continue;
								}
								}
								goto IL_63;
								IL_10A:
								num2 = 3;
								break;
							}
						}
					}
					IL_88:
					if (true)
					{
					}
					return null;
					IL_B8:
					goto IL_146;
					IL_108:
					return null;
					IL_146:
					return null;
					IL_16D:
					return null;
				}
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06005B6D RID: 23405 RVA: 0x003901F8 File Offset: 0x0038F1F8
		public bool IsFormatted
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_A0:
					num = 8;
					break;
				default:
					if (false)
					{
					}
					goto IL_4C;
				}
				IXLSRange[] cells;
				int num2;
				int num3;
				for (;;)
				{
					IL_1E:
					switch (num)
					{
					case 0:
						if (!cells[num2].HasRichText)
						{
							num = 4;
							continue;
						}
						num = 9;
						continue;
					case 1:
						if (num3 == 0)
						{
							num = 2;
							continue;
						}
						goto IL_9E;
					case 2:
						return false;
					case 3:
						if (true)
						{
						}
						if (num2 >= num3)
						{
							num = 7;
							continue;
						}
						num = 0;
						continue;
					case 4:
						return false;
					case 5:
						return false;
					case 6:
						goto IL_AD;
					case 7:
						return true;
					case 8:
						goto IL_AD;
					case 9:
						if (!cells[num2].RichText.IsFormatted)
						{
							num = 5;
							continue;
						}
						num2++;
						num = 6;
						continue;
					}
					goto IL_4C;
					IL_AD:
					num = 3;
				}
				return false;
				IL_9E:
				num2 = 0;
				goto IL_A0;
				IL_4C:
				cells = this.ᜀ.Cells;
				num3 = cells.Length;
				num = 1;
				goto IL_1E;
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06005B6E RID: 23406 RVA: 0x00390310 File Offset: 0x0038F310
		internal new spr\u1DF5 ReservedHandle
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
				return ((XlsRange)this.ᜀ).Application;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06005B6F RID: 23407 RVA: 0x0039035C File Offset: 0x0038F35C
		public new object Parent
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
				return this.ᜀ;
			}
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x003903A0 File Offset: 0x0038F3A0
		public void BeginUpdate()
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
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x003903DC File Offset: 0x0038F3DC
		public void EndUpdate()
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
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x00390418 File Offset: 0x0038F418
		public new void Dispose()
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
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x00390454 File Offset: 0x0038F454
		public void Clear()
		{
			for (;;)
			{
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 1;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7C;
						default:
							goto IL_CE;
						}
						break;
					case 1:
						goto IL_9E;
					case 2:
						if (cells[num].HasRichText)
						{
							num3 = 6;
							continue;
						}
						goto IL_40;
					case 3:
						goto IL_9E;
					case 4:
						goto IL_40;
					case 5:
						if (num >= num2)
						{
							num3 = 0;
							continue;
						}
						goto IL_7C;
					case 6:
						((RangeRichTextString)cells[num].RichText).Clear();
						num3 = 4;
						continue;
					}
					break;
					IL_40:
					num++;
					if (true)
					{
					}
					num3 = 3;
					continue;
					IL_7C:
					num3 = 2;
					continue;
					IL_9E:
					num3 = 5;
				}
			}
			IL_CE:
			if (false)
			{
			}
		}

		// Token: 0x04002C9D RID: 11421
		private float \u2609\u0096\u008F\u00AC;

		// Token: 0x04002C9E RID: 11422
		private IXLSRange ᜀ;
	}
}
