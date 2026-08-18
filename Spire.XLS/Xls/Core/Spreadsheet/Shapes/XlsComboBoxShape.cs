using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x02000220 RID: 544
	public class XlsComboBoxShape : XlsShape, IComboBoxShape
	{
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x00125954 File Offset: 0x00124954
		// (set) Token: 0x0600209B RID: 8347 RVA: 0x00125998 File Offset: 0x00124998
		public IXLSRange ListFillRange
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
				return this.ᜁ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_66;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_68;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					case 2:
						this.ᜁ = value;
						num = 0;
						continue;
					}
					if (value == null)
					{
						break;
					}
					num = 2;
				}
				IL_66:
				IL_68:
				(this.ᜁ as spr\u1A8B).ᜀ();
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x00125A20 File Offset: 0x00124A20
		// (set) Token: 0x0600209D RID: 8349 RVA: 0x00125A64 File Offset: 0x00124A64
		public IXLSRange LinkedCell
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
				return this.ᜂ;
			}
			set
			{
				int a_ = 11;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value.Row == value.LastRow)
						{
							num = 1;
							continue;
						}
						goto IL_73;
					case 1:
						num = 2;
						continue;
					case 2:
						if (value.Column == value.LastColumn)
						{
							goto IL_4A;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_51;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 4:
						num = 0;
						continue;
					case 5:
						if (value != null)
						{
							num = 7;
							continue;
						}
						return;
					case 6:
						goto IL_C6;
					case 7:
						(this.ᜂ as spr\u1A8B).ᜀ();
						num = 6;
						continue;
					case 8:
						goto IL_113;
					}
					if (value != null)
					{
						num = 4;
						continue;
					}
					goto IL_4A;
					IL_51:
					num = 5;
					continue;
					IL_4A:
					this.ᜂ = value;
					goto IL_51;
				}
				IL_73:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ɀ♂⥄⭆Ո≊⍌⑎煐㹒⁔⑖ⵘ筚㽜㩞䅠ၢ౤०๨ݪ࡬佮ተᙲᥴ᭶坸", a_));
				IL_C6:
				return;
				IL_113:
				goto IL_73;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x00125B8C File Offset: 0x00124B8C
		// (set) Token: 0x0600209F RID: 8351 RVA: 0x00125C8C File Offset: 0x00124C8C
		public int SelectedIndex
		{
			get
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_80;
					case 1:
						if (true)
						{
						}
						num = 6;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_80;
						default:
							if (false)
							{
							}
							if (this.ᜂ.IsBlank)
							{
								num = 4;
								continue;
							}
							goto IL_EA;
						}
						break;
					case 3:
						goto IL_9D;
					case 4:
						this.ᜃ = 0;
						num = 7;
						continue;
					case 6:
						if (this.ᜂ.HasNumber)
						{
							num = 0;
							continue;
						}
						num = 2;
						continue;
					case 7:
						goto IL_DE;
					}
					if (this.ᜂ != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_80:
					this.ᜃ = (int)this.ᜂ.NumberValue;
					num = 3;
				}
				IL_9D:
				IL_DE:
				IL_EA:
				return this.ᜃ;
			}
			set
			{
				for (;;)
				{
					this.ᜃ = value;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜂ != null)
							{
								if (true)
								{
								}
								num = 4;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
							if (this.ᜁ != null)
							{
								num = 3;
								continue;
							}
							return;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6B;
							default:
								if (false)
								{
								}
								this.ᜂ.NumberValue = (double)this.ᜃ;
								num = 1;
								continue;
							}
							break;
						case 4:
							goto IL_6B;
						}
						break;
						IL_6B:
						num = 2;
					}
				}
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x00125D40 File Offset: 0x00124D40
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x00125D84 File Offset: 0x00124D84
		public int DropDownLines
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
				int a_ = 7;
				if (value < 0)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("礼䴾⹀㍂ॄ⹆❈⹊㹌", a_));
					}
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x00125DEC File Offset: 0x00124DEC
		public ExcelComboType ComboType
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
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x00125E30 File Offset: 0x00124E30
		// (set) Token: 0x060020A4 RID: 8356 RVA: 0x00125E74 File Offset: 0x00124E74
		public bool Display3DShading
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
				return this.ᜆ;
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
				this.ᜆ = value;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x00125EB8 File Offset: 0x00124EB8
		public string SelectedValue
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex <= 0)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3D;
					}
					if (false)
					{
					}
					return null;
				}
				IL_3D:
				return this.ᜁ.Cells[selectedIndex - 1].Value;
			}
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00125F18 File Offset: 0x00124F18
		internal XlsComboBoxShape(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			base.VmlShape = true;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00125F3C File Offset: 0x00124F3C
		internal XlsComboBoxShape(spr\u1DF5 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3, List<spr\u25AD> A_4) : base(A_0, A_1, A_2, A_3)
		{
			base.VmlShape = true;
			this.ᜀ(A_4);
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x00125F6C File Offset: 0x00124F6C
		private void ᜀ(List<spr\u25AD> A_0)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 0:
						return;
					case 2:
						goto IL_109;
					case 3:
						goto IL_109;
					case 4:
						goto IL_C9;
					case 5:
						goto IL_C9;
					case 6:
						num = 12;
						continue;
					case 7:
					{
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						spr\u25AD spr_u25AD = A_0[num2];
						TObjSubRecordType tobjSubRecordType = spr_u25AD.ᜏ();
						num = 11;
						continue;
					}
					case 8:
						goto IL_A6;
					case 9:
						goto IL_C9;
					case 10:
						goto IL_6D;
					case 11:
					{
						TObjSubRecordType tobjSubRecordType;
						switch (tobjSubRecordType)
						{
						case TObjSubRecordType.ftSbs:
						{
							spr\u25AD spr_u25AD;
							this.ᜀ((sprᢛ)spr_u25AD);
							if (true)
							{
							}
							num = 8;
							continue;
						}
						case TObjSubRecordType.ftNts:
							goto IL_C9;
						case TObjSubRecordType.ftSbsFormula:
						{
							spr\u25AD spr_u25AD;
							this.ᜀ((sprḵ)spr_u25AD);
							num = 4;
							continue;
						}
						default:
							num = 6;
							continue;
						}
						break;
					}
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A6;
						default:
						{
							if (false)
							{
							}
							TObjSubRecordType tobjSubRecordType;
							if (tobjSubRecordType != TObjSubRecordType.ftLbsData)
							{
								num = 13;
								continue;
							}
							spr\u25AD spr_u25AD;
							this.ᜀ((spr\u2471)spr_u25AD);
							num = 9;
							continue;
						}
						}
						break;
					case 13:
						num = 5;
						continue;
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					num2 = 0;
					count = A_0.Count;
					num = 2;
					continue;
					IL_C9:
					num2++;
					num = 3;
					continue;
					IL_A6:
					goto IL_C9;
					IL_109:
					num = 7;
				}
				IL_6D:
				throw new ArgumentNullException(RecordTableEnumerator.b("䠺䠼崾ፀ♂♄⡆㭈⽊㹌", a_));
			}
			}
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x00126130 File Offset: 0x00125130
		private void ᜀ(sprḵ A_0)
		{
			int a_ = 11;
			if (A_0 == null)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_50;
				}
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("❀㝂ᙄ╆㩈ൊ≌㵎㱐♒㥔㙖", a_));
			}
			IL_50:
			sprỜ sprỜ = A_0.ᜀ()[0] as sprỜ;
			this.ᜂ = sprỜ.ᜀ(base.Workbook, base.Worksheet as IWorksheet);
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x001261B8 File Offset: 0x001251B8
		private void ᜀ(spr\u2471 A_0)
		{
			int a_ = 16;
			int num = 0;
			for (;;)
			{
				Ptg[] array;
				int num2;
				int num3;
				switch (num)
				{
				case 1:
					num = 8;
					continue;
				case 2:
					num2 = array.Length;
					goto IL_A9;
				case 3:
					if (array != null)
					{
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_73;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 4:
					if (num3 > 0)
					{
						num = 6;
						continue;
					}
					goto IL_122;
				case 5:
					goto IL_4B;
				case 6:
				{
					sprỜ sprỜ = array[0] as sprỜ;
					this.ᜁ = sprỜ.ᜀ(base.Workbook, base.Worksheet as IWorksheet);
					goto IL_73;
				}
				case 7:
					goto IL_7B;
				case 8:
					num2 = 0;
					goto IL_A9;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				array = A_0.ᜌ();
				num = 3;
				continue;
				IL_73:
				num = 7;
				continue;
				IL_A9:
				num3 = num2;
				if (true)
				{
				}
				num = 4;
			}
			IL_4B:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁅㱇ى⹋㵍ᑏ㍑⁓㝕", a_));
			IL_7B:
			IL_122:
			this.ᜃ = A_0.ᜄ();
			this.ᜅ = A_0.ᜉ();
			this.ᜆ = !A_0.ᜃ();
			this.ᜄ = (int)A_0.ᜇ().ᜂ();
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00126320 File Offset: 0x00125320
		private void ᜀ(sprᢛ A_0)
		{
			int a_ = 2;
			if (true)
			{
			}
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_50;
				}
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("帷丹漻尽㌿", a_));
			}
			IL_50:
			this.ᜃ = A_0.ᜇ();
			this.ᜄ = A_0.ᜄ();
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00126398 File Offset: 0x00125398
		protected override void OnPrepareForSerialization()
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
			this.ᜏ = (sprἼ)spr\u231F.ᜀ(MsoRecords.msofbtSp);
			this.ᜏ.ᜉ(2);
			this.ᜏ.ᜈ(201);
			this.ᜏ.ᜆ(true);
			this.ᜏ.ᜇ(true);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0012641C File Offset: 0x0012541C
		internal override void SerializeShape(spr\u21EB spgrContainer)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 6;
				sprὙ sprὙ;
				spr᪙ spr᪙;
				for (;;)
				{
					spr\u23E7 spr_u23E;
					bool flag;
					spr\u2003 spr_u;
					switch (num)
					{
					case 0:
						goto IL_83;
					case 1:
						goto IL_61;
					case 2:
						sprὙ.ᜀ(spr_u23E);
						num = 10;
						continue;
					case 3:
						if (true)
						{
						}
						if (!flag)
						{
							num = 9;
							continue;
						}
						goto IL_14B;
					case 4:
						goto IL_14B;
					case 5:
					{
						int selectedIndex = this.SelectedIndex;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_58;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					case 7:
						if (this.ᜂ == null)
						{
							num = 5;
							continue;
						}
						goto IL_83;
					case 8:
						if (spr_u23E.ᜀ().Length > 0)
						{
							num = 2;
							continue;
						}
						goto IL_1CD;
					case 9:
					{
						sprទ a_2 = new sprទ();
						spr_u.ᜀ(a_2);
						num = 4;
						continue;
					}
					case 10:
						goto IL_132;
					}
					goto IL_55;
					IL_58:
					num = 1;
					continue;
					IL_55:
					if (spgrContainer == null)
					{
						goto IL_58;
					}
					sprὙ = (sprὙ)spr\u231F.ᜀ(MsoRecords.msofbtSpContainer);
					spr᪙ = (spr᪙)spr\u231F.ᜀ(MsoRecords.msofbtClientData);
					spr_u = base.Obj;
					flag = (spr_u != null);
					this.ᜀ(ref spr_u, flag, spr᪙);
					this.ᜀ(spr_u, flag);
					this.ᜁ(spr_u, flag);
					this.ᜂ(spr_u, flag);
					num = 7;
					continue;
					IL_83:
					num = 3;
					continue;
					IL_14B:
					spr᪙.ᜀ(spr_u);
					sprὙ.ᜀ(this.ᜏ);
					spr_u23E = this.SerializeOptions(sprὙ);
					num = 8;
				}
				IL_61:
				throw new ArgumentNullException(RecordTableEnumerator.b("㑆㥈ⱊ㽌౎㹐㵒⅔㙖じ㕚㡜ⵞ", a_));
				IL_132:
				IL_1CD:
				sprὙ.ᜀ(base.ClientAnchor);
				sprὙ.ᜀ(spr᪙);
				spgrContainer.ᜀ(sprὙ);
				return;
			}
			}
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x00126610 File Offset: 0x00125610
		private new void ᜂ(spr\u2003 A_0, bool A_1)
		{
			for (;;)
			{
				spr\u2471 spr_u = null;
				int num = 11;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num)
					{
					case 0:
						goto IL_119;
					case 1:
						goto IL_1A6;
					case 2:
						if (flag)
						{
							num = 13;
							continue;
						}
						goto IL_C9;
					case 3:
						spr_u.ᜀ((this.ᜁ != null) ? (this.ᜁ as spr\u1A8B).ᜀ() : null);
						spr_u.ᜁ(this.SelectedIndex);
						spr_u.ᜂ(0);
						num = 5;
						continue;
					case 4:
						flag2 = ((spr_u = (spr\u2471)A_0.ᜀ(TObjSubRecordType.ftLbsData)) == null);
						goto IL_8B;
					case 5:
						if (this.ᜅ != ExcelComboType.Regular)
						{
							num = 6;
							continue;
						}
						goto IL_119;
					case 6:
						spr_u.ᜀ(this.ᜅ);
						num = 0;
						continue;
					case 7:
						if (flag)
						{
							num = 12;
							continue;
						}
						goto IL_1A8;
					case 8:
						goto IL_C9;
					case 9:
						num = 4;
						continue;
					case 10:
						flag2 = true;
						goto IL_8B;
					case 11:
						if (A_1)
						{
							num = 9;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1A6;
						default:
							if (false)
							{
							}
							num = 10;
							continue;
						}
						break;
					case 12:
						A_0.ᜀ(spr_u);
						num = 1;
						continue;
					case 13:
						spr_u = new spr\u2471();
						num = 8;
						continue;
					}
					break;
					IL_8B:
					flag = flag2;
					num = 2;
					continue;
					IL_C9:
					spr_u.ᜃ(this.ᜄ);
					num = 3;
					continue;
					IL_119:
					spr_u.ᜂ(!this.ᜆ);
					num = 7;
				}
			}
			IL_1A6:
			IL_1A8:
			if (true)
			{
			}
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x001267D0 File Offset: 0x001257D0
		private new void ᜁ(spr\u2003 A_0, bool A_1)
		{
			sprḵ sprḵ;
			for (;;)
			{
				sprḵ = null;
				int num = 13;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num)
					{
					case 0:
						if (A_1)
						{
							num = 11;
							continue;
						}
						num = 5;
						continue;
					case 1:
						flag = ((sprḵ = (sprḵ)A_0.ᜀ(TObjSubRecordType.ftSbsFormula)) == null);
						goto IL_160;
					case 2:
					{
						int index;
						if ((index = A_0.ᜁ(TObjSubRecordType.ftSbsFormula)) < 0)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_18A;
						default:
							if (false)
							{
							}
							num = 12;
							continue;
						}
						break;
					}
					case 3:
						sprḵ = new sprḵ();
						num = 8;
						continue;
					case 4:
						if (flag2)
						{
							num = 3;
							continue;
						}
						goto IL_113;
					case 5:
						goto IL_18A;
					case 6:
						return;
					case 7:
						if (flag2)
						{
							num = 10;
							continue;
						}
						return;
					case 8:
						goto IL_113;
					case 9:
						num = 0;
						continue;
					case 10:
						goto IL_142;
					case 11:
						num = 1;
						continue;
					case 12:
					{
						int index;
						A_0.ᜃ().RemoveAt(index);
						num = 6;
						continue;
					}
					case 13:
						if (this.ᜂ != null)
						{
							if (true)
							{
							}
							num = 9;
							continue;
						}
						num = 2;
						continue;
					}
					break;
					IL_113:
					sprḵ.ᜀ((this.ᜂ as spr\u1A8B).ᜀ());
					num = 7;
					continue;
					IL_160:
					flag2 = flag;
					num = 4;
					continue;
					IL_18A:
					flag = true;
					goto IL_160;
				}
			}
			IL_142:
			A_0.ᜀ(sprḵ);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0012696C File Offset: 0x0012596C
		private void ᜀ(spr\u2003 A_0, bool A_1)
		{
			int num = 4;
			sprᢛ sprᢛ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_71;
				case 1:
					num = 3;
					continue;
				case 2:
					goto IL_31;
				case 3:
					if (true)
					{
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
						if ((sprᢛ = (sprᢛ)A_0.ᜀ(TObjSubRecordType.ftSbs)) != null)
						{
							goto IL_CA;
						}
						break;
					}
					num = 2;
					continue;
				}
				if (A_1)
				{
					num = 1;
					continue;
				}
				IL_31:
				sprᢛ = new sprᢛ();
				sprᢛ.ᜃ(2);
				sprᢛ.ᜆ(16);
				sprᢛ.ᜄ(0);
				sprᢛ.ᜅ(2);
				sprᢛ.ᜀ(1);
				sprᢛ.ᜂ(0);
				A_0.ᜀ(sprᢛ);
				num = 0;
			}
			IL_71:
			IL_CA:
			sprᢛ.ᜁ(this.ᜄ);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00126A50 File Offset: 0x00125A50
		private void ᜀ(ref spr\u2003 A_0, bool A_1, spr᪙ A_2)
		{
			if (true)
			{
			}
			int num = 2;
			spr\u2223 spr_u;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0 = (spr\u2003)spr\u175E.ᜀ(TBIFFRecord.OBJ);
					spr_u = new spr\u2223();
					spr_u.ᜀ(TObjType.otComboBox);
					spr_u.ᜀ(true);
					spr_u.ᜂ(true);
					spr_u.ᜃ(false);
					A_0.ᜀ(spr_u);
					num = 3;
					continue;
				case 1:
					goto IL_C4;
				case 3:
					goto IL_C6;
				case 4:
					goto IL_D1;
				}
				if (!A_1)
				{
					num = 0;
					continue;
				}
				spr_u = (base.Obj.ᜃ()[0] as spr\u2223);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				IL_C6:
				num = 4;
				continue;
				IL_C4:
				goto IL_C6;
			}
			IL_D1:
			spr_u.ᜁ((base.OldObjId > 0U) ? ((ushort)base.OldObjId) : ((ushort)base.ParentWorkbook.CurrentObjectId));
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x00126B58 File Offset: 0x00125B58
		public override IShape Clone(object parent, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes, bool addToCollections)
		{
			XlsComboBoxShape xlsComboBoxShape;
			for (;;)
			{
				xlsComboBoxShape = (XlsComboBoxShape)base.Clone(parent, hashNewNames, dicFontIndexes, addToCollections);
				XlsWorksheetBase worksheet = xlsComboBoxShape.Worksheet;
				XlsWorkbook parentWorkbook = worksheet.ParentWorkbook;
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
						(xlsComboBoxShape.Worksheet.ComboBoxes as ComboBoxCollection).AddComboBox(xlsComboBoxShape);
						num = 5;
						continue;
					case 2:
						if (this.ᜁ != null)
						{
							num = 3;
							continue;
						}
						goto IL_105;
					case 3:
						xlsComboBoxShape.ᜁ = (this.ᜁ as ICombinedRange).Clone(worksheet, hashNewNames, parentWorkbook);
						num = 7;
						continue;
					case 4:
						goto IL_110;
					case 5:
						return xlsComboBoxShape;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_110;
						default:
							if (false)
							{
							}
							xlsComboBoxShape.ᜂ = (this.ᜂ as ICombinedRange).Clone(worksheet, hashNewNames, parentWorkbook);
							num = 0;
							continue;
						}
						break;
					case 7:
						goto IL_105;
					case 8:
						if (addToCollections)
						{
							num = 1;
							continue;
						}
						return xlsComboBoxShape;
					}
					break;
					IL_70:
					num = 8;
					continue;
					IL_110:
					if (this.ᜂ != null)
					{
						num = 6;
						continue;
					}
					goto IL_70;
					IL_105:
					num = 4;
				}
			}
			return xlsComboBoxShape;
		}

		// Token: 0x04001145 RID: 4421
		public const int ShapeInstance = 201;

		// Token: 0x04001146 RID: 4422
		public const int ShapeVersion = 2;

		// Token: 0x04001147 RID: 4423
		private new const int ᜀ = 8;

		// Token: 0x04001148 RID: 4424
		private bool[] \u25D8\u009B\u00A6\u008A;

		// Token: 0x04001149 RID: 4425
		private new IXLSRange ᜁ;

		// Token: 0x0400114A RID: 4426
		private new IXLSRange ᜂ;

		// Token: 0x0400114B RID: 4427
		private new int ᜃ;

		// Token: 0x0400114C RID: 4428
		private float[] \u25D9\u009B\u0099\u0082;

		// Token: 0x0400114D RID: 4429
		private new int ᜄ = 8;

		// Token: 0x0400114E RID: 4430
		private new ExcelComboType ᜅ;

		// Token: 0x0400114F RID: 4431
		private byte \u2593\u0091\u0083\u008F;

		// Token: 0x04001150 RID: 4432
		private new bool ᜆ;
	}
}
