using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200060A RID: 1546
	public class RTFCommentArray : XlsObject, IRichTextString
	{
		// Token: 0x06005B74 RID: 23412 RVA: 0x00390538 File Offset: 0x0038F538
		internal RTFCommentArray(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x00390554 File Offset: 0x0038F554
		public IFont GetFont(int iPosition)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IXLSRange[] cells = this.ᜀ.Cells;
					bool flag = true;
					IFont font = null;
					int num = 0;
					int num2 = cells.Length;
					int num3 = 9;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (flag)
							{
								num3 = 2;
								continue;
							}
							num3 = 1;
							continue;
						case 1:
							goto IL_73;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_73;
							default:
								if (false)
								{
								}
								font = cells[num].Comment.RichText.GetFont(iPosition);
								num3 = 5;
								continue;
							}
							break;
						case 3:
							num3 = 0;
							continue;
						case 4:
							return font;
						case 5:
							goto IL_130;
						case 6:
							goto IL_A5;
						case 7:
							if (cells[num].Comment != null)
							{
								num3 = 3;
								continue;
							}
							goto IL_130;
						case 8:
							goto IL_EA;
						case 9:
							goto IL_EA;
						case 10:
							if (num >= num2)
							{
								num3 = 4;
								continue;
							}
							num3 = 7;
							continue;
						}
						break;
						IL_73:
						if (!font.Equals(cells[num].Comment.RichText.GetFont(iPosition)))
						{
							if (true)
							{
							}
							num3 = 6;
							continue;
						}
						goto IL_130;
						IL_EA:
						num3 = 10;
						continue;
						IL_130:
						num++;
						num3 = 8;
					}
				}
				IL_A5:
				return null;
			}
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x003906C8 File Offset: 0x0038F6C8
		public void SetFont(int iStartPos, int iEndPos, IFont font)
		{
			for (;;)
			{
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 5;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							goto IL_4A;
						}
						break;
					case 1:
						if (num >= num2)
						{
							num3 = 6;
							continue;
						}
						num3 = 4;
						continue;
					case 2:
						goto IL_BD;
					case 3:
						cells[num].Comment.RichText.SetFont(iStartPos, iEndPos, font);
						num3 = 0;
						continue;
					case 4:
						if (cells[num].Comment != null)
						{
							num3 = 3;
							continue;
						}
						goto IL_4A;
					case 5:
						goto IL_BD;
					case 6:
						return;
					}
					break;
					IL_4A:
					num++;
					if (true)
					{
					}
					num3 = 2;
					continue;
					IL_BD:
					num3 = 1;
				}
			}
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x003907B0 File Offset: 0x0038F7B0
		public void ClearFormatting()
		{
			for (;;)
			{
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_B2;
					case 1:
						if (num >= num2)
						{
							num3 = 5;
							continue;
						}
						num3 = 4;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							goto IL_4A;
						}
						break;
					case 3:
						goto IL_B2;
					case 4:
						if (cells[num].Comment != null)
						{
							num3 = 6;
							continue;
						}
						goto IL_4A;
					case 5:
						return;
					case 6:
						cells[num].Comment.RichText.ClearFormatting();
						num3 = 2;
						continue;
					}
					break;
					IL_4A:
					num++;
					num3 = 0;
					continue;
					IL_B2:
					if (true)
					{
					}
					num3 = 1;
				}
			}
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x00390894 File Offset: 0x0038F894
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
						if (true)
						{
						}
						if (num >= num2)
						{
							num3 = 5;
							continue;
						}
						num3 = 3;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							goto IL_4A;
						}
						break;
					case 2:
						goto IL_B4;
					case 3:
						if (cells[num].Comment != null)
						{
							num3 = 4;
							continue;
						}
						goto IL_4A;
					case 4:
						cells[num].Comment.RichText.Append(text, font);
						num3 = 1;
						continue;
					case 5:
						return;
					case 6:
						goto IL_B4;
					}
					break;
					IL_4A:
					num++;
					num3 = 2;
					continue;
					IL_B4:
					num3 = 0;
				}
			}
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x0039097C File Offset: 0x0038F97C
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
						return;
					case 1:
						goto IL_BA;
					case 2:
						goto IL_BA;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							goto IL_4A;
						}
						break;
					case 4:
						cells[num].Comment.RichText.Clear();
						num3 = 3;
						continue;
					case 5:
						if (true)
						{
						}
						if (cells[num].Comment != null)
						{
							num3 = 4;
							continue;
						}
						goto IL_4A;
					case 6:
						if (num >= num2)
						{
							num3 = 0;
							continue;
						}
						num3 = 5;
						continue;
					}
					break;
					IL_4A:
					num++;
					num3 = 2;
					continue;
					IL_BA:
					num3 = 6;
				}
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06005B7A RID: 23418 RVA: 0x00390A60 File Offset: 0x0038FA60
		// (set) Token: 0x06005B7B RID: 23419 RVA: 0x00390BD0 File Offset: 0x0038FBD0
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
						bool flag = true;
						string text = null;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 4;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (flag)
								{
									num3 = 5;
									continue;
								}
								num3 = 3;
								continue;
							case 1:
								return text;
							case 2:
								if (num >= num2)
								{
									num3 = 1;
									continue;
								}
								num3 = 6;
								continue;
							case 3:
								goto IL_73;
							case 4:
								goto IL_E9;
							case 5:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_73;
								default:
									if (false)
									{
									}
									text = cells[num].Comment.RichText.Text;
									num3 = 7;
									continue;
								}
								break;
							case 6:
								if (cells[num].Comment != null)
								{
									num3 = 9;
									continue;
								}
								goto IL_12F;
							case 7:
								goto IL_12F;
							case 8:
								goto IL_9C;
							case 9:
								num3 = 0;
								continue;
							case 10:
								goto IL_E9;
							}
							break;
							IL_73:
							if (text != cells[num].Comment.RichText.Text)
							{
								num3 = 8;
								continue;
							}
							goto IL_12F;
							IL_E9:
							num3 = 2;
							continue;
							IL_12F:
							num++;
							num3 = 10;
						}
					}
					IL_9C:
					if (true)
					{
					}
					return null;
				}
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 3;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (num >= num2)
								{
									num3 = 2;
									continue;
								}
								cells[num].AddComment().RichText.Text = value;
								num++;
								num3 = 1;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									goto IL_3C;
								}
								break;
							case 2:
								return;
							case 3:
								if (true)
								{
								}
								goto IL_3C;
							}
							break;
							IL_3C:
							num3 = 0;
						}
					}
				}
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06005B7C RID: 23420 RVA: 0x00390C78 File Offset: 0x0038FC78
		public string RtfText
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						bool flag = true;
						string text = null;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 3;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (num >= num2)
								{
									if (true)
									{
									}
									num3 = 8;
									continue;
								}
								num3 = 6;
								continue;
							case 1:
								goto IL_70;
							case 2:
								num3 = 9;
								continue;
							case 3:
								goto IL_DE;
							case 4:
								goto IL_DE;
							case 5:
								goto IL_99;
							case 6:
								if (cells[num].Comment != null)
								{
									num3 = 2;
									continue;
								}
								goto IL_12C;
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_70;
								default:
									if (false)
									{
									}
									text = cells[num].Comment.RichText.RtfText;
									num3 = 10;
									continue;
								}
								break;
							case 8:
								return text;
							case 9:
								if (flag)
								{
									num3 = 7;
									continue;
								}
								num3 = 1;
								continue;
							case 10:
								goto IL_12C;
							}
							break;
							IL_70:
							if (text != cells[num].Comment.RichText.RtfText)
							{
								num3 = 5;
								continue;
							}
							goto IL_12C;
							IL_DE:
							num3 = 0;
							continue;
							IL_12C:
							num++;
							num3 = 4;
						}
					}
					IL_99:
					return null;
				}
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06005B7D RID: 23421 RVA: 0x00390DE8 File Offset: 0x0038FDE8
		public bool IsFormatted
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						bool flag = true;
						bool flag2 = false;
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
									goto IL_70;
								default:
									if (false)
									{
									}
									flag2 = cells[num].Comment.RichText.IsFormatted;
									num3 = 5;
									continue;
								}
								break;
							case 1:
								goto IL_E1;
							case 2:
								num3 = 7;
								continue;
							case 3:
								return flag2;
							case 4:
								if (num >= num2)
								{
									num3 = 3;
									continue;
								}
								num3 = 9;
								continue;
							case 5:
								goto IL_127;
							case 6:
								return false;
							case 7:
								if (flag)
								{
									num3 = 0;
									continue;
								}
								num3 = 8;
								continue;
							case 8:
								goto IL_70;
							case 9:
								if (cells[num].Comment != null)
								{
									num3 = 2;
									continue;
								}
								goto IL_127;
							case 10:
								goto IL_E1;
							}
							break;
							IL_70:
							if (true)
							{
							}
							if (flag2 != cells[num].Comment.RichText.IsFormatted)
							{
								num3 = 6;
								continue;
							}
							goto IL_127;
							IL_E1:
							num3 = 4;
							continue;
							IL_127:
							num++;
							num3 = 10;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x00390F50 File Offset: 0x0038FF50
		private void ᜀ()
		{
			int a_ = 1;
			for (;;)
			{
				this.ᜀ = (base.FindParent(typeof(IXLSRange)) as IXLSRange);
				if (this.ᜀ != null)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_44;
				}
			}
			IL_44:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("朶堸䤺堼儾㕀捂⩄╆⍈⹊⹌㭎煐げ㑔㥖㝘㑚⥜罞͠٢䕤Ŧ٨Ṫͬ୮彰", a_));
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x00390FD0 File Offset: 0x0038FFD0
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

		// Token: 0x06005B80 RID: 23424 RVA: 0x0039100C File Offset: 0x0039000C
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

		// Token: 0x04002C9F RID: 11423
		private int \u2609\u008E\u00A6\u0089;

		// Token: 0x04002CA0 RID: 11424
		private IXLSRange ᜀ;
	}
}
