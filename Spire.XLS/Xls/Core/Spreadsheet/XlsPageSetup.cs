using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Spire.Xls.Collections;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200016D RID: 365
	public class XlsPageSetup : XlsPageSetupBase, IPageSetup
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x000AA37C File Offset: 0x000A937C
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x000AA3C0 File Offset: 0x000A93C0
		public bool IsPrintGridlines
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
				return this.ᜆ == 1;
			}
			set
			{
				int num = 5;
				for (;;)
				{
					ushort num2;
					ushort num3;
					switch (num)
					{
					case 0:
						num2 = 1;
						goto IL_96;
					case 1:
						num2 = 0;
						goto IL_96;
					case 2:
						return;
					case 3:
						this.ᜆ = num3;
						this.ᜇ = 1;
						base.SetChanged();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 4:
						if (this.ᜆ != num3)
						{
							num = 3;
							continue;
						}
						return;
					case 6:
						if (true)
						{
						}
						num = 1;
						continue;
					}
					if (!value)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
					IL_96:
					num3 = num2;
					num = 4;
				}
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x000AA494 File Offset: 0x000A9494
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x000AA4DC File Offset: 0x000A94DC
		public bool IsPrintHeadings
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
				return this.ᜅ != 0;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					ushort num2;
					ushort num3;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						this.ᜅ = num2;
						base.SetChanged();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 1:
						if (this.ᜅ != num2)
						{
							num = 0;
							continue;
						}
						return;
					case 3:
						num = 5;
						continue;
					case 4:
						num3 = 1;
						goto IL_81;
					case 5:
						num3 = 0;
						goto IL_81;
					case 6:
						return;
					}
					if (!value)
					{
						num = 3;
						continue;
					}
					num = 4;
					continue;
					IL_81:
					num2 = num3;
					num = 1;
				}
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000AA5A0 File Offset: 0x000A95A0
		protected internal XlsHPageBreaksCollection HPageBreaks
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
							break;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜌ = new HPageBreaksCollection((spr\u2158)base.ReservedHandle, this);
							break;
						}
						num = 2;
						continue;
					case 2:
						goto IL_7B;
					}
					if (this.ᜌ != null)
					{
						break;
					}
					num = 0;
				}
				IL_7B:
				return this.ᜌ;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x000AA630 File Offset: 0x000A9630
		protected internal XlsVPageBreaksCollection VPageBreaks
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
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
							this.\u170D = new VPageBreaksCollection((spr\u2158)base.ReservedHandle, this);
							break;
						}
						num = 2;
						continue;
					case 2:
						goto IL_7B;
					}
					if (true)
					{
					}
					if (this.\u170D != null)
					{
						break;
					}
					num = 1;
				}
				IL_7B:
				return this.\u170D;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x000AA6C0 File Offset: 0x000A96C0
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x000AA704 File Offset: 0x000A9704
		public string PrintArea
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
				return this.ExtractPrintArea();
			}
			set
			{
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
							if (true)
							{
							}
							if (false)
							{
							}
							this.ParsePrintAreaExpression(value);
							break;
						}
						num = 0;
						continue;
					}
					if (!(value != this.ExtractPrintArea()))
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x000AA784 File Offset: 0x000A9784
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x000AA7C8 File Offset: 0x000A97C8
		public string PrintTitleColumns
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
				return this.ExtractPrintTitleRowColumn(false);
			}
			set
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
							break;
						default:
							if (false)
							{
							}
							this.ParsePrintTitleColumns(value);
							break;
						}
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						return;
					}
					if (!(value != this.ExtractPrintTitleRowColumn(false)))
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x000AA84C File Offset: 0x000A984C
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x000AA890 File Offset: 0x000A9890
		public string PrintTitleRows
		{
			get
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
				return this.ExtractPrintTitleRowColumn(true);
			}
			set
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
							break;
						default:
							if (false)
							{
							}
							this.ParsePrintTitleRows(value);
							break;
						}
						num = 2;
						continue;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (!(value != this.ExtractPrintTitleRowColumn(true)))
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x000AA914 File Offset: 0x000A9914
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x000AA95C File Offset: 0x000A995C
		public override bool IsFitToPage
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
				return this.ᜊ.ᜁ();
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜊ.ᜃ(value);
							base.SetChanged();
							break;
						}
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (this.ᜊ.ᜁ() == value)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x000AA9E8 File Offset: 0x000A99E8
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x000AAA30 File Offset: 0x000A9A30
		public bool IsSummaryRowBelow
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
				return this.ᜊ.ᜂ();
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
				this.ᜊ.ᜀ(value);
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000AAA78 File Offset: 0x000A9A78
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x000AAAC0 File Offset: 0x000A9AC0
		public bool IsSummaryColumnRight
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
				return this.ᜊ.ᜀ();
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
				this.ᜊ.ᜄ(value);
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x000AAB08 File Offset: 0x000A9B08
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x000AAB50 File Offset: 0x000A9B50
		public int DefaultRowHeight
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
				return (int)this.ᜉ.ᜁ();
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
				this.ᜉ.ᜁ((ushort)value);
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x000AAB98 File Offset: 0x000A9B98
		// (set) Token: 0x06001170 RID: 4464 RVA: 0x000AABE4 File Offset: 0x000A9BE4
		public bool DefaultRowHeightFlag
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
				return (this.ᜉ.ᜃ() & 1) == 1;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_35;
					case 1:
						goto IL_6E;
					case 2:
						if (!this.ᜋ.IsZeroHeight)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_A0;
							}
						}
						num = 1;
						continue;
					}
					if (value)
					{
						num = 0;
					}
					else
					{
						num = 2;
					}
				}
				IL_35:
				this.ᜉ.ᜀ(this.ᜉ.ᜃ() | 1);
				return;
				IL_6E:
				this.ᜉ.ᜀ(this.ᜉ.ᜃ() | 2);
				return;
				IL_A0:
				if (true)
				{
				}
				if (false)
				{
				}
				spr\u2076 spr_u = this.ᜉ;
				this.ᜉ.ᜃ();
				spr_u.ᜀ((ushort)0);
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000AACB8 File Offset: 0x000A9CB8
		public override int GetStoreSize(ExcelVersion version)
		{
			int num;
			for (;;)
			{
				this.FillGutsRecord();
				num = base.GetStoreSize(version) + 2 + 4 + 2 + 4 + 2 + 4 + this.ᜈ.GetStoreSize(version) + 4 + this.ᜉ.GetStoreSize(version) + 4 + this.ᜊ.GetStoreSize(version) + 4;
				int num2 = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						if (this.ᜌ != null)
						{
							num2 = 5;
							continue;
						}
						goto IL_D0;
					case 1:
						num += this.\u170D.GetStoreSize(version) + 4;
						num2 = 2;
						continue;
					case 2:
						goto IL_B2;
					case 3:
						goto IL_D0;
					case 4:
						if (this.\u170D != null)
						{
							num2 = 1;
							continue;
						}
						return num;
					case 5:
						num += this.ᜌ.GetStoreSize(version) + 4;
						num2 = 3;
						continue;
					}
					break;
					IL_D0:
					num2 = 4;
				}
			}
			IL_B2:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			return num;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x000AADD4 File Offset: 0x000A9DD4
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x000AAE18 File Offset: 0x000A9E18
		public string RelationId
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
				return this.ᜎ;
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
				this.ᜎ = value;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x000AAE5C File Offset: 0x000A9E5C
		internal XlsWorksheet Worksheet
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
				return this.ᜋ;
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000AAEA0 File Offset: 0x000A9EA0
		internal XlsPageSetup(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
			this.ᜁ();
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000AAEC8 File Offset: 0x000A9EC8
		internal XlsPageSetup(spr\u1DF5 A_0, object A_1, sprἛ A_2) : base(A_0, A_1)
		{
			this.ᜀ();
			this.ᜀ(A_2);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000AAEF4 File Offset: 0x000A9EF4
		internal XlsPageSetup(spr\u1DF5 A_0, object A_1, BiffRecordRaw[] A_2, int A_3) : base(A_0, A_1)
		{
			this.ᜀ();
			this.Parse(A_2, A_3);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x000AAF20 File Offset: 0x000A9F20
		internal XlsPageSetup(spr\u1DF5 A_0, object A_1, List<BiffRecordRaw> A_2, int A_3) : base(A_0, A_1)
		{
			this.ᜀ();
			this.Parse(A_2, A_3);
			this.ᜁ();
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x000AAF54 File Offset: 0x000A9F54
		protected override void FindParents()
		{
			int a_ = 10;
			base.FindParents();
			object obj = base.FindParent(typeof(XlsWorksheet));
			if (obj == null)
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
					if (false)
					{
					}
					break;
				}
				throw new ArgumentException(RecordTableEnumerator.b("ဿ⍁㙃⍅♇㹉汋⅍㉏㡑ㅓ㕕ⱗ穙㽛㽝๟ౡୣብ䡧ࡩ५乭ᙯᵱųᡵᱷ呹", a_));
			}
			this.ᜋ = (XlsWorksheet)obj;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000AAFD4 File Offset: 0x000A9FD4
		private void ᜁ()
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (this.ᜊ == null)
					{
						num = 9;
						continue;
					}
					return;
				case 2:
					goto IL_B0;
				case 3:
					goto IL_126;
				case 4:
					goto IL_D8;
				case 5:
					if (this.ᜉ == null)
					{
						num = 4;
						continue;
					}
					num = 11;
					continue;
				case 6:
					this.ᜈ = (spr\u1922)spr\u175E.ᜀ(TBIFFRecord.Guts);
					if (true)
					{
					}
					num = 2;
					continue;
				case 8:
					this.ᜋ.IsZeroHeight = true;
					num = 3;
					continue;
				case 9:
					this.ᜊ = (spr᧕)spr\u175E.ᜀ(TBIFFRecord.WSBool);
					num = 0;
					continue;
				case 10:
					goto IL_126;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D8;
					default:
						if (false)
						{
						}
						if (this.ᜉ.ᜃ() == 2)
						{
							num = 8;
							continue;
						}
						goto IL_126;
					}
					break;
				}
				if (this.ᜈ == null)
				{
					num = 6;
					continue;
				}
				IL_B0:
				num = 5;
				continue;
				IL_D8:
				this.ᜉ = (spr\u2076)spr\u175E.ᜀ(TBIFFRecord.DefaultRowHeight);
				num = 10;
				continue;
				IL_126:
				num = 1;
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000AB14C File Offset: 0x000AA14C
		internal override bool ParseRecord(BiffRecordRaw record)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 6;
				bool flag;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1A0;
					case 1:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.PrintHeaders:
						{
							spr\u240B spr_u240B = (spr\u240B)record;
							this.ᜅ = spr_u240B.ᜀ();
							num = 14;
							continue;
						}
						case TBIFFRecord.PrintGridlines:
						{
							spr\u2260 spr_u = (spr\u2260)record;
							this.ᜆ = spr_u.ᜁ();
							num = 10;
							continue;
						}
						default:
							num = 22;
							continue;
						}
						break;
					}
					case 2:
						goto IL_1C6;
					case 3:
					{
						TBIFFRecord typeCode;
						if (typeCode <= TBIFFRecord.PrintGridlines)
						{
							num = 15;
							continue;
						}
						num = 9;
						continue;
					}
					case 4:
						return flag;
					case 5:
						goto IL_232;
					case 7:
						goto IL_BB;
					case 8:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.VerticalPageBreaks:
						{
							spr\u2583 a_2 = (spr\u2583)record;
							this.VPageBreaks.ᜀ(a_2);
							num = 2;
							continue;
						}
						case TBIFFRecord.HorizontalPageBreaks:
						{
							spr\u2539 a_3 = (spr\u2539)record;
							this.HPageBreaks.ᜀ(a_3);
							num = 23;
							continue;
						}
						default:
							num = 13;
							continue;
						}
						break;
					}
					case 9:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.Guts:
							this.ᜈ = (spr\u1922)record;
							num = 24;
							continue;
						case TBIFFRecord.WSBool:
							this.ᜊ = (spr᧕)record;
							num = 16;
							continue;
						case TBIFFRecord.Gridset:
						{
							sprᴞ sprᴞ = (sprᴞ)record;
							this.ᜇ = sprᴞ.ᜀ();
							num = 0;
							continue;
						}
						default:
							num = 18;
							continue;
						}
						break;
					}
					case 10:
						return flag;
					case 11:
						if (!flag)
						{
							num = 21;
							continue;
						}
						return flag;
					case 12:
						goto IL_240;
					case 13:
						num = 1;
						continue;
					case 14:
						goto IL_FC;
					case 15:
						num = 8;
						continue;
					case 16:
						goto IL_223;
					case 17:
						goto IL_232;
					case 18:
						num = 20;
						continue;
					case 19:
						num = 5;
						continue;
					case 20:
					{
						TBIFFRecord typeCode;
						if (typeCode != TBIFFRecord.DefaultRowHeight)
						{
							num = 19;
							continue;
						}
						this.ᜉ = (spr\u2076)record;
						num = 4;
						continue;
					}
					case 21:
					{
						flag = true;
						TBIFFRecord typeCode = record.TypeCode;
						num = 3;
						continue;
					}
					case 22:
						if (true)
						{
						}
						num = 17;
						continue;
					case 23:
						goto IL_151;
					case 24:
						goto IL_D8;
					}
					if (record != null)
					{
						flag = base.ParseRecord(record);
						num = 11;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_278;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					IL_232:
					flag = false;
					num = 12;
				}
				IL_BB:
				goto IL_278;
				IL_D8:
				IL_FC:
				IL_151:
				IL_1A0:
				IL_1C6:
				IL_223:
				IL_240:
				return flag;
				IL_278:
				throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃╅❇㡉⡋", a_));
			}
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000AB490 File Offset: 0x000AA490
		internal void ᜀ(sprἛ A_0)
		{
			int a_ = 19;
			if (A_0 == null)
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
					if (false)
					{
					}
					break;
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000AB4F4 File Offset: 0x000AA4F4
		private void ᜀ(IList A_0, ref int A_1)
		{
			int a_ = 18;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9F;
				case 1:
					if (!(A_0[A_1] is sprᱬ))
					{
						num = 4;
						continue;
					}
					A_1++;
					num = 0;
					continue;
				case 2:
					goto IL_5D;
				case 3:
					if (A_1 > A_0.Count)
					{
						num = 8;
						continue;
					}
					goto IL_9F;
				case 4:
					return;
				case 5:
					if (A_1 >= 0)
					{
						num = 7;
						continue;
					}
					goto IL_102;
				case 6:
					if (true)
					{
					}
					break;
				case 7:
					num = 3;
					continue;
				case 8:
					goto IL_89;
				}
				goto IL_4F;
				IL_55:
				num = 2;
				continue;
				IL_4F:
				if (A_0 == null)
				{
					goto IL_55;
				}
				num = 5;
				continue;
				IL_9F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_55;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
			}
			IL_5D:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱇ⭉㡋⽍", a_));
			IL_89:
			IL_102:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㡇╉㽋", a_), RecordTableEnumerator.b("ṇ⭉⁋㭍㕏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥ѧཀྵὫᵭ偯ٱᱳ᝵ᙷ婹䱻幽ꚅ뚕ﶛ肟욡얣튥즧誩삫쮭\udeaf햱삳\udeb5", a_));
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000AB628 File Offset: 0x000AA628
		internal override void SerializeStartRecords(RecordArrayList records)
		{
			int a_ = 6;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_25A;
				case 1:
				{
					if (this.ᜊ == null)
					{
						num = 10;
						continue;
					}
					this.FillGutsRecord();
					spr\u240B spr_u240B = (spr\u240B)spr\u175E.ᜀ(TBIFFRecord.PrintHeaders);
					spr_u240B.ᜀ(this.ᜅ);
					records.ᜀ(spr_u240B);
					spr\u2260 spr_u = (spr\u2260)spr\u175E.ᜀ(TBIFFRecord.PrintGridlines);
					spr_u.ᜀ(this.ᜆ);
					records.ᜀ(spr_u);
					sprᴞ sprᴞ = (sprᴞ)spr\u175E.ᜀ(TBIFFRecord.Gridset);
					sprᴞ.ᜀ(this.ᜇ);
					records.ᜀ(sprᴞ);
					records.ᜀ(this.ᜈ);
					records.ᜀ(this.ᜉ);
					records.ᜀ(this.ᜊ);
					num = 2;
					continue;
				}
				case 2:
					if (this.ᜌ != null)
					{
						num = 8;
						continue;
					}
					goto IL_170;
				case 3:
					if (this.\u170D != null)
					{
						num = 13;
						continue;
					}
					return;
				case 4:
					if (this.ᜉ == null)
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				case 5:
					goto IL_1FB;
				case 6:
					goto IL_16B;
				case 7:
					goto IL_5F;
				case 8:
					this.ᜌ.ᜀ(records);
					num = 11;
					continue;
				case 10:
					goto IL_145;
				case 11:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_155;
					default:
						if (false)
						{
						}
						goto IL_170;
					}
					break;
				case 12:
					goto IL_155;
				case 13:
					this.\u170D.ᜀ(records);
					num = 5;
					continue;
				}
				if (records == null)
				{
					num = 7;
					continue;
				}
				num = 12;
				continue;
				IL_155:
				if (this.ᜈ == null)
				{
					num = 6;
					continue;
				}
				num = 4;
				continue;
				IL_170:
				num = 3;
			}
			IL_5F:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅㭇", a_));
			IL_145:
			throw new ArgumentNullException(RecordTableEnumerator.b("儻愽᜿ᅁك⥅❇♉", a_));
			IL_16B:
			throw new ArgumentNullException(RecordTableEnumerator.b("儻愽ܿ㝁ぃ㕅", a_));
			IL_1FB:
			return;
			IL_25A:
			throw new ArgumentNullException(RecordTableEnumerator.b("儻愽п❁≃ᑅ❇㵉ы⭍㥏㕑㱓≕", a_));
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000AB894 File Offset: 0x000AA894
		internal override int FillStreamStart(BinaryWriter writer, DataProvider provider, IEncryptor encryptor, int streamPosition)
		{
			int num;
			for (;;)
			{
				num = base.ᜀ(writer, provider, encryptor, TBIFFRecord.PrintHeaders, this.ᜅ, streamPosition);
				num += base.ᜀ(writer, provider, encryptor, TBIFFRecord.PrintGridlines, this.ᜆ, streamPosition + num);
				num += base.ᜀ(writer, provider, encryptor, TBIFFRecord.Gridset, this.ᜇ, streamPosition + num);
				num += this.ᜈ.FillStream(writer, provider, encryptor, streamPosition + num);
				num += this.ᜉ.FillStream(writer, provider, encryptor, streamPosition + num);
				num += this.ᜊ.FillStream(writer, provider, encryptor, streamPosition + num);
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (this.ᜌ != null)
						{
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						goto IL_F0;
					case 1:
						for (;;)
						{
							num += this.ᜌ.FillStream(writer, provider, encryptor, streamPosition + num);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_13B;
							}
						}
						IL_13B:
						if (false)
						{
						}
						num2 = 3;
						continue;
					case 2:
						if (this.\u170D != null)
						{
							num2 = 5;
							continue;
						}
						return num;
					case 3:
						goto IL_F0;
					case 4:
						return num;
					case 5:
						num += this.\u170D.FillStream(writer, provider, encryptor, streamPosition + num);
						num2 = 4;
						continue;
					}
					break;
					IL_F0:
					num2 = 2;
				}
			}
			return num;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x000AB9FC File Offset: 0x000AA9FC
		protected void FillGutsRecord()
		{
			switch (0)
			{
			default:
			{
				for (;;)
				{
					this.ᜈ.ᜂ(0);
					this.ᜈ.ᜁ(0);
					int firstRow = this.ᜋ.FirstRow;
					int num = 25;
					for (;;)
					{
						int num2;
						int num3;
						spr\u216E[] array;
						switch (num)
						{
						case 0:
							goto IL_289;
						case 1:
						{
							spr\u2502 spr_u;
							this.ᜈ.ᜁ(spr_u.ᜀ());
							num = 4;
							continue;
						}
						case 2:
							goto IL_205;
						case 3:
							goto IL_1A5;
						case 4:
							goto IL_355;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_31F;
							default:
								if (false)
								{
								}
								goto IL_C7;
							}
							break;
						case 6:
						{
							spr\u2502 spr_u;
							if (spr_u.ᜀ() > this.ᜈ.ᜃ())
							{
								num = 1;
								continue;
							}
							goto IL_355;
						}
						case 7:
							goto IL_303;
						case 8:
							if (this.ᜈ.ᜄ() != 0)
							{
								num = 12;
								continue;
							}
							this.ᜈ.ᜀ(0);
							num = 3;
							continue;
						case 9:
							goto IL_31F;
						case 10:
						{
							num2 = firstRow;
							int lastRow = this.ᜋ.LastRow;
							num = 11;
							continue;
						}
						case 11:
							goto IL_289;
						case 12:
						{
							spr\u1922 spr_u2 = this.ᜈ;
							spr_u2.ᜂ(spr_u2.ᜄ() + 1);
							this.ᜈ.ᜀ(this.ᜈ.ᜄ() * 14 - 1);
							num = 15;
							continue;
						}
						case 13:
						{
							spr\u2502 spr_u;
							if (spr_u != null)
							{
								num = 16;
								continue;
							}
							goto IL_355;
						}
						case 14:
							goto IL_1CD;
						case 15:
							goto IL_1A5;
						case 16:
							num = 6;
							continue;
						case 17:
							num = 8;
							continue;
						case 18:
						{
							int lastRow;
							if (num2 > lastRow)
							{
								num = 7;
								continue;
							}
							spr\u2502 spr_u3 = sprᜑ.ᜂ(this.ᜋ, num2);
							num = 23;
							continue;
						}
						case 19:
						{
							spr\u2502 spr_u3;
							this.ᜈ.ᜂ(spr_u3.ᜀ());
							num = 2;
							continue;
						}
						case 20:
						{
							if (num3 >= array.Length)
							{
								num = 17;
								continue;
							}
							spr\u2502 spr_u = array[num3];
							if (true)
							{
							}
							num = 13;
							continue;
						}
						case 21:
						{
							spr\u2502 spr_u3;
							if (spr_u3.ᜀ() > this.ᜈ.ᜄ())
							{
								num = 19;
								continue;
							}
							goto IL_205;
						}
						case 22:
							num = 21;
							continue;
						case 23:
						{
							spr\u2502 spr_u3;
							if (spr_u3 != null)
							{
								num = 22;
								continue;
							}
							goto IL_205;
						}
						case 24:
							if (this.ᜈ.ᜃ() != 0)
							{
								num = 14;
								continue;
							}
							goto IL_388;
						case 25:
							if (firstRow > 0)
							{
								num = 10;
								continue;
							}
							goto IL_303;
						}
						break;
						IL_C7:
						num = 20;
						continue;
						IL_31F:
						goto IL_C7;
						IL_1A5:
						num = 24;
						continue;
						IL_205:
						num2++;
						num = 0;
						continue;
						IL_289:
						num = 18;
						continue;
						IL_303:
						array = this.ᜋ.ColumnInformation;
						num3 = 0;
						num = 9;
						continue;
						IL_355:
						num3++;
						num = 5;
					}
				}
				IL_1CD:
				spr\u1922 spr_u4 = this.ᜈ;
				spr_u4.ᜁ(spr_u4.ᜃ() + 1);
				this.ᜈ.ᜃ(this.ᜈ.ᜃ() * 14 - 1);
				return;
				IL_388:
				this.ᜈ.ᜃ(0);
				return;
			}
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000ABDA0 File Offset: 0x000AADA0
		private void ᜀ()
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

		// Token: 0x06001182 RID: 4482 RVA: 0x000ABDDC File Offset: 0x000AADDC
		protected string ConvertTo3dRangeName(string value)
		{
			int a_ = 8;
			Match match;
			Match match2;
			for (;;)
			{
				match = FormulaUtil.CellRangeRegex.Match(value);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_76;
						}
						break;
					case 1:
						goto IL_DC;
					case 2:
						if (match.Success)
						{
							num = 1;
							continue;
						}
						match = FormulaUtil.FullColumnRangeRegex.Match(value);
						num = 6;
						continue;
					case 3:
						if (match2.Success)
						{
							num = 5;
							continue;
						}
						match = FormulaUtil.FullRowRangeRegex.Match(value);
						num = 2;
						continue;
					case 4:
						if (true)
						{
						}
						if (match.Success)
						{
							num = 0;
							continue;
						}
						match2 = FormulaUtil.CellRegex.Match(value);
						num = 3;
						continue;
					case 5:
						goto IL_112;
					case 6:
						if (match.Success)
						{
							num = 7;
							continue;
						}
						goto IL_1F6;
					case 7:
						goto IL_1B0;
					}
					break;
				}
			}
			IL_76:
			if (false)
			{
			}
			return RecordTableEnumerator.b("᤽", a_) + this.ᜋ.Name + RecordTableEnumerator.b("᤽愿", a_) + match.Result(RecordTableEnumerator.b("ᨽ㬿Ł⭃⩅㵇❉≋罍ⵏ癑⽓ѕ㝗ⵙ浛⍝婟䙡ὣ╥ݧ٩ᥫͭṯ䁱ॳ創ͷ⡹፻ॽ뉿ﾁ", a_));
			IL_DC:
			return RecordTableEnumerator.b("᤽", a_) + this.ᜋ.Name + RecordTableEnumerator.b("᤽愿", a_) + value;
			IL_112:
			return RecordTableEnumerator.b("᤽", a_) + this.ᜋ.Name + RecordTableEnumerator.b("᤽愿", a_) + match2.Result(RecordTableEnumerator.b("ᨽ㬿Ł⭃⩅㵇❉≋罍ⵏ癑⽓ѕ㝗ⵙ浛⍝", a_));
			IL_1B0:
			return RecordTableEnumerator.b("᤽", a_) + this.ᜋ.Name + RecordTableEnumerator.b("᤽愿", a_) + value;
			IL_1F6:
			return null;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000ABFE0 File Offset: 0x000AAFE0
		protected void ParsePrintAreaExpression(string value)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 4;
				Ptg[] array2;
				XlsName xlsName2;
				sprῚ sprῚ;
				for (;;)
				{
					bool r1C1ReferenceMode;
					spr\u1CD5 spr_u1CD;
					XlsName xlsName;
					ParseFormulaOptions parseFormulaOptions;
					int num2;
					int num3;
					Ptg[] array;
					Ptg ptg;
					int num4;
					ExcelVersion version;
					switch (num)
					{
					case 0:
						if (!r1C1ReferenceMode)
						{
							num = 13;
							continue;
						}
						num = 8;
						continue;
					case 1:
						if (spr_u1CD != null)
						{
							num = 9;
							continue;
						}
						goto IL_DB;
					case 2:
						xlsName = this.ᜋ.InnerNames.ᜁ(XlsPageSetup.ᜁ);
						goto IL_10C;
					case 3:
						if (this.ᜋ.Workbook.Version != ExcelVersion.Version97to2003)
						{
							num = 19;
							continue;
						}
						num = 10;
						continue;
					case 5:
						goto IL_35C;
					case 6:
						if (value.Length == 0)
						{
							num = 20;
							continue;
						}
						num = 3;
						continue;
					case 7:
						num = 6;
						continue;
					case 8:
						parseFormulaOptions = (ParseFormulaOptions.InName | ParseFormulaOptions.UseR1C1);
						goto IL_306;
					case 9:
						goto IL_22E;
					case 10:
						xlsName = this.ᜋ.InnerNames.ᜁ(XlsPageSetup.ᜀ);
						goto IL_10C;
					case 11:
						goto IL_37D;
					case 12:
						if (num2 >= num3)
						{
							num = 11;
							continue;
						}
						ptg = array[num2];
						spr_u1CD = (ptg as spr\u1CD5);
						if (true)
						{
						}
						num = 1;
						continue;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_22E;
						default:
							if (false)
							{
							}
							num = 15;
							continue;
						}
						break;
					case 14:
						goto IL_DB;
					case 15:
						parseFormulaOptions = ParseFormulaOptions.InName;
						goto IL_306;
					case 16:
						goto IL_35C;
					case 17:
						if (Array.IndexOf<FormulaToken>(XlsPageSetup.ᜄ, ptg.TokenCode) == -1)
						{
							num = 18;
							continue;
						}
						array2[num2] = ptg;
						num4 += ptg.GetSize(version);
						num2++;
						num = 5;
						continue;
					case 18:
						goto IL_107;
					case 19:
						num = 2;
						continue;
					case 20:
						goto IL_2E5;
					}
					if (value != null)
					{
						num = 7;
						continue;
					}
					goto IL_262;
					IL_DB:
					num = 17;
					continue;
					IL_10C:
					xlsName2 = xlsName;
					sprῚ = xlsName2.Record;
					num4 = 0;
					XlsWorkbook parentWorkbook = this.ᜋ.ParentWorkbook;
					FormulaUtil formulaUtil = parentWorkbook.FormulaUtil;
					r1C1ReferenceMode = parentWorkbook.CalculationOptions.R1C1ReferenceMode;
					int a_2 = parentWorkbook.AddSheetReference(this.ᜋ);
					Dictionary<Type, sprᨳ> dictionary = new Dictionary<Type, sprᨳ>();
					dictionary.Add(typeof(spr\u1BFD), new sprᨳ(1));
					dictionary.Add(typeof(sprᣋ), new sprᨳ(1));
					dictionary.Add(typeof(sprᲔ), new sprᨳ(1));
					dictionary.Add(typeof(sprᦊ), new sprᨳ(1));
					num = 0;
					continue;
					IL_22E:
					ptg = spr_u1CD.ᜀ(a_2);
					num = 14;
					continue;
					IL_306:
					ParseFormulaOptions a_3 = parseFormulaOptions;
					array = formulaUtil.ᜁ(value, this.ᜋ, dictionary, 0, null, a_3, 0, 0);
					num3 = array.Length;
					array2 = new Ptg[num3];
					version = this.ᜋ.ParentWorkbook.Version;
					num2 = 0;
					num = 16;
					continue;
					IL_35C:
					num = 12;
				}
				IL_107:
				throw new ArgumentException(RecordTableEnumerator.b("ᡇ㡉╋⁍⑏牑㕓⑕㵗㭙籛㙝şᅡ䑣ཥ٧ᱩ൫ɭ᥯ᙱ味ၵ᝷ࡹᅻώ", a_));
				IL_262:
				this.ᜋ.Names.Remove(XlsPageSetup.ᜀ);
				return;
				IL_2E5:
				goto IL_262;
				IL_37D:
				sprῚ.ᜀ(array2);
				((spr\u1D46)xlsName2).ᜀ();
				return;
			}
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000AC37C File Offset: 0x000AB37C
		protected void ParsePrintTitleColumns(string value)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 11;
				List<Ptg> list;
				sprῚ sprῚ;
				for (;;)
				{
					bool flag;
					spr\u1BFD spr_u1BFD;
					Ptg[] array;
					spr\u1BFD spr_u1BFD2;
					Ptg ptg2;
					XlsName xlsName;
					bool flag2;
					switch (num)
					{
					case 0:
						if (!flag)
						{
							num = 7;
							continue;
						}
						goto IL_45B;
					case 1:
						if (spr_u1BFD.ᜋ() == 0)
						{
							num = 18;
							continue;
						}
						goto IL_255;
					case 2:
					{
						spr_u1BFD = (array[0] as spr\u1BFD);
						IWorkbook workbook = this.ᜋ.Workbook;
						num = 1;
						continue;
					}
					case 3:
						goto IL_363;
					case 4:
						if (this.ᜋ.Workbook.Version != ExcelVersion.Version97to2003)
						{
							num = 34;
							continue;
						}
						num = 29;
						continue;
					case 5:
						goto IL_3C6;
					case 6:
						if (spr_u1BFD2 != null)
						{
							num = 33;
							continue;
						}
						goto IL_45B;
					case 7:
						list.Add(spr_u1BFD2);
						num = 31;
						continue;
					case 8:
					{
						int num2 = array.Length;
						num = 24;
						continue;
					}
					case 9:
					{
						IWorkbook workbook;
						if (spr_u1BFD.ᜉ() != workbook.MaxRowCount - 1)
						{
							num = 12;
							continue;
						}
						goto IL_2B6;
					}
					case 10:
						goto IL_1E1;
					case 12:
						goto IL_255;
					case 13:
					{
						int num2;
						if (num2 == 1)
						{
							num = 2;
							continue;
						}
						goto IL_2B6;
					}
					case 14:
						if (spr_u1BFD2 != null)
						{
							num = 27;
							continue;
						}
						goto IL_3E5;
					case 15:
						goto IL_2B6;
					case 16:
						goto IL_2B0;
					case 17:
						if (flag)
						{
							num = 32;
							continue;
						}
						goto IL_445;
					case 18:
						num = 9;
						continue;
					case 19:
						num = 26;
						continue;
					case 20:
					{
						XlsWorkbook parentWorkbook = this.ᜋ.ParentWorkbook;
						FormulaUtil formulaUtil = parentWorkbook.FormulaUtil;
						ExcelVersion version = parentWorkbook.Version;
						Ptg ptg = FormulaUtil.ᜀ(FormulaToken.tCellRangeList, formulaUtil.OperandsSeparator);
						int a_ = ptg2.GetSize(version) + spr_u1BFD2.GetSize(version) + ptg.GetSize(version);
						list.AddRange(new Ptg[]
						{
							new spr\u1DFC(a_),
							ptg2,
							spr_u1BFD2,
							ptg
						});
						num = 10;
						continue;
					}
					case 21:
						goto IL_2B6;
					case 22:
						xlsName = this.ᜋ.InnerNames.ᜁ(XlsPageSetup.ᜃ);
						goto IL_406;
					case 23:
					{
						value = this.ConvertTo3dRangeName(value);
						XlsWorkbook parentWorkbook2 = this.ᜋ.ParentWorkbook;
						Ptg[] array2 = parentWorkbook2.FormulaUtil.ᜃ(value);
						ptg2 = array2[0];
						ptg2.TokenCode = FormulaToken.tArea3d1;
						num = 3;
						continue;
					}
					case 24:
					{
						int num2;
						if (num2 == 4)
						{
							num = 25;
							continue;
						}
						num = 13;
						continue;
					}
					case 25:
						spr_u1BFD2 = (array[2] as spr\u1BFD);
						num = 15;
						continue;
					case 26:
						flag2 = (value.Length > 0);
						goto IL_47B;
					case 27:
						num = 35;
						continue;
					case 28:
						if (array != null)
						{
							num = 8;
							continue;
						}
						goto IL_2B6;
					case 29:
						xlsName = this.ᜋ.InnerNames.ᜁ(XlsPageSetup.ᜂ);
						goto IL_406;
					case 30:
						if (flag)
						{
							num = 23;
							continue;
						}
						goto IL_363;
					case 31:
						goto IL_3E0;
					case 32:
						list.Add(ptg2);
						num = 5;
						continue;
					case 33:
						num = 0;
						continue;
					case 34:
						num = 22;
						continue;
					case 35:
						if (!flag)
						{
							goto IL_3E5;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B0;
						default:
							if (false)
							{
							}
							num = 20;
							continue;
						}
						break;
					}
					if (value != null)
					{
						num = 19;
						continue;
					}
					num = 16;
					continue;
					IL_255:
					spr_u1BFD2 = spr_u1BFD;
					num = 21;
					continue;
					IL_2B6:
					ptg2 = null;
					num = 30;
					continue;
					IL_363:
					list = new List<Ptg>();
					num = 14;
					continue;
					IL_3E5:
					num = 6;
					continue;
					IL_406:
					XlsName xlsName2 = xlsName;
					sprῚ = xlsName2.Record;
					array = sprῚ.ᜈ();
					spr_u1BFD2 = null;
					num = 28;
					continue;
					IL_45B:
					num = 17;
					continue;
					IL_47B:
					flag = flag2;
					num = 4;
					continue;
					IL_2B0:
					flag2 = false;
					goto IL_47B;
				}
				IL_1E1:
				IL_3C6:
				IL_3E0:
				goto IL_4AE;
				IL_445:
				this.ᜋ.Names.Remove(XlsPageSetup.ᜂ);
				return;
				IL_4AE:
				sprῚ.ᜀ(list.ToArray());
				return;
			}
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x000AC844 File Offset: 0x000AB844
		protected void ParsePrintTitleRows(string value)
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				List<Ptg> list;
				sprῚ sprῚ;
				for (;;)
				{
					Ptg ptg;
					spr\u1BFD spr_u1BFD;
					bool flag;
					spr\u1BFD spr_u1BFD2;
					bool flag2;
					Ptg[] array;
					XlsName xlsName;
					switch (num)
					{
					case 0:
						goto IL_347;
					case 1:
						list.Add(ptg);
						num = 35;
						continue;
					case 2:
					{
						int num2;
						if (num2 == 4)
						{
							num = 22;
							continue;
						}
						num = 9;
						continue;
					}
					case 3:
					{
						IWorkbook workbook;
						if (spr_u1BFD.ᜉ() == workbook.MaxRowCount - 1)
						{
							num = 26;
							continue;
						}
						goto IL_29A;
					}
					case 4:
						flag = (value.Length > 0);
						goto IL_47E;
					case 5:
						list.Add(spr_u1BFD2);
						num = 23;
						continue;
					case 6:
						if (flag2)
						{
							num = 18;
							continue;
						}
						goto IL_3C9;
					case 8:
						num = 14;
						continue;
					case 9:
					{
						int num2;
						if (num2 == 1)
						{
							goto IL_104;
						}
						goto IL_29A;
					}
					case 10:
						if (this.ᜋ.Workbook.Version != ExcelVersion.Version97to2003)
						{
							num = 8;
							continue;
						}
						num = 15;
						continue;
					case 11:
						if (array != null)
						{
							num = 31;
							continue;
						}
						goto IL_29A;
					case 12:
					{
						value = this.ConvertTo3dRangeName(value);
						XlsWorkbook parentWorkbook = this.ᜋ.ParentWorkbook;
						Ptg[] array2 = parentWorkbook.FormulaUtil.ᜃ(value);
						ptg = array2[0];
						ptg.TokenCode = FormulaToken.tArea3d1;
						num = 0;
						continue;
					}
					case 13:
						if (true)
						{
						}
						num = 21;
						continue;
					case 14:
						xlsName = this.ᜋ.InnerNames.ᜁ(XlsPageSetup.ᜃ);
						goto IL_3ED;
					case 15:
						xlsName = this.ᜋ.InnerNames.ᜁ(XlsPageSetup.ᜂ);
						goto IL_3ED;
					case 16:
						goto IL_29A;
					case 17:
						num = 4;
						continue;
					case 18:
					{
						XlsWorkbook parentWorkbook2 = this.ᜋ.ParentWorkbook;
						FormulaUtil formulaUtil = parentWorkbook2.FormulaUtil;
						ExcelVersion version = parentWorkbook2.Version;
						Ptg ptg2 = FormulaUtil.ᜀ(FormulaToken.tCellRangeList, formulaUtil.OperandsSeparator);
						int a_ = spr_u1BFD2.GetSize(version) + ptg.GetSize(version) + ptg2.GetSize(version);
						list.AddRange(new Ptg[]
						{
							new spr\u1DFC(a_),
							spr_u1BFD2,
							ptg,
							ptg2
						});
						num = 25;
						continue;
					}
					case 19:
						if (spr_u1BFD2 != null)
						{
							num = 34;
							continue;
						}
						goto IL_3C9;
					case 20:
					{
						spr_u1BFD = (array[0] as spr\u1BFD);
						IWorkbook workbook = this.ᜋ.Workbook;
						num = 33;
						continue;
					}
					case 21:
						if (!flag2)
						{
							num = 5;
							continue;
						}
						goto IL_45E;
					case 22:
						spr_u1BFD2 = (array[1] as spr\u1BFD);
						num = 28;
						continue;
					case 23:
						goto IL_3C4;
					case 24:
						num = 3;
						continue;
					case 25:
						goto IL_1BD;
					case 26:
						goto IL_231;
					case 27:
						flag = false;
						goto IL_47E;
					case 28:
						goto IL_29A;
					case 29:
						if (flag2)
						{
							num = 1;
							continue;
						}
						goto IL_448;
					case 30:
						if (spr_u1BFD2 != null)
						{
							num = 13;
							continue;
						}
						goto IL_45E;
					case 31:
					{
						int num2 = array.Length;
						num = 2;
						continue;
					}
					case 32:
						if (flag2)
						{
							num = 12;
							continue;
						}
						goto IL_347;
					case 33:
						if (spr_u1BFD.ᜋ() != 0)
						{
							num = 24;
							continue;
						}
						goto IL_231;
					case 34:
						num = 6;
						continue;
					case 35:
						goto IL_3AA;
					}
					if (value != null)
					{
						num = 17;
						continue;
					}
					num = 27;
					continue;
					IL_104:
					num = 20;
					continue;
					IL_3ED:
					XlsName xlsName2 = xlsName;
					sprῚ = xlsName2.Record;
					array = sprῚ.ᜈ();
					spr_u1BFD2 = null;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_104;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					IL_231:
					spr_u1BFD2 = spr_u1BFD;
					num = 16;
					continue;
					IL_29A:
					ptg = null;
					num = 32;
					continue;
					IL_347:
					list = new List<Ptg>();
					num = 19;
					continue;
					IL_3C9:
					num = 30;
					continue;
					IL_45E:
					num = 29;
					continue;
					IL_47E:
					flag2 = flag;
					num = 10;
				}
				IL_1BD:
				IL_3AA:
				IL_3C4:
				goto IL_4B1;
				IL_448:
				this.ᜋ.Names.Remove(XlsPageSetup.ᜂ);
				return;
				IL_4B1:
				sprῚ.ᜀ(list.ToArray());
				return;
			}
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000ACD10 File Offset: 0x000ABD10
		protected string ExtractPrintArea()
		{
			XlsName xlsName2;
			for (;;)
			{
				INameRanges names = this.ᜋ.Names;
				int num = 4;
				for (;;)
				{
					XlsName xlsName;
					switch (num)
					{
					case 0:
						goto IL_BE;
					case 1:
						num = 5;
						continue;
					case 2:
						xlsName = (XlsName)names[XlsPageSetup.ᜀ];
						goto IL_88;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3E;
						default:
							if (false)
							{
							}
							if (xlsName2 != null)
							{
								num = 0;
								continue;
							}
							goto IL_E5;
						}
						break;
					case 4:
						goto IL_3E;
					case 5:
						xlsName = (XlsName)names[XlsPageSetup.ᜁ];
						goto IL_88;
					}
					break;
					IL_3E:
					if (this.ᜋ.Workbook.Version != ExcelVersion.Version97to2003)
					{
						num = 1;
						continue;
					}
					if (true)
					{
					}
					num = 2;
					continue;
					IL_88:
					xlsName2 = xlsName;
					num = 3;
				}
			}
			IL_BE:
			sprῚ sprῚ = xlsName2.Record;
			return this.ᜀ(sprῚ.ᜈ());
			IL_E5:
			return null;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000ACE04 File Offset: 0x000ABE04
		protected string ExtractPrintTitleRowColumn(bool bRowExtract)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				Ptg[] array;
				for (;;)
				{
					INameRanges names = this.ᜋ.Names;
					int num = 18;
					for (;;)
					{
						XlsName xlsName;
						XlsName xlsName2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_F8;
							default:
								if (false)
								{
								}
								num = 25;
								continue;
							}
							break;
						case 1:
							if (array.Length > 4)
							{
								num = 29;
								continue;
							}
							num = 12;
							continue;
						case 2:
							xlsName = (XlsName)names[XlsPageSetup.ᜂ];
							goto IL_2D3;
						case 3:
							goto IL_14B;
						case 4:
							if (xlsName2 != null)
							{
								num = 14;
								continue;
							}
							goto IL_3F1;
						case 5:
							if (true)
							{
							}
							num = 16;
							continue;
						case 6:
							num = 1;
							continue;
						case 7:
							num = 15;
							continue;
						case 8:
							if (array.Length > 0)
							{
								num = 6;
								continue;
							}
							goto IL_10E;
						case 9:
							num = 22;
							continue;
						case 10:
						{
							string result = this.ᜀ(array);
							spr\u1BFD spr_u1BFD = array[0] as spr\u1BFD;
							num = 24;
							continue;
						}
						case 11:
							num = 19;
							continue;
						case 12:
							if (array.Length == 4)
							{
								goto IL_F8;
							}
							num = 17;
							continue;
						case 13:
							if (array.Length == 1)
							{
								num = 10;
								continue;
							}
							goto IL_3F1;
						case 14:
						{
							sprῚ sprῚ = xlsName2.Record;
							array = sprῚ.ᜈ();
							num = 8;
							continue;
						}
						case 15:
						{
							spr\u1BFD spr_u1BFD;
							if (spr_u1BFD.ᜉ() == this.ᜋ.ParentWorkbook.MaxRowCount - 1)
							{
								num = 28;
								continue;
							}
							string result;
							return result;
						}
						case 16:
							if (bRowExtract)
							{
								num = 3;
								continue;
							}
							goto IL_1A1;
						case 17:
							if (array.Length == 3)
							{
								num = 21;
								continue;
							}
							num = 13;
							continue;
						case 18:
							if (this.ᜋ.Workbook.Version != ExcelVersion.Version97to2003)
							{
								num = 11;
								continue;
							}
							num = 2;
							continue;
						case 19:
							xlsName = (XlsName)names[XlsPageSetup.ᜃ];
							goto IL_2D3;
						case 20:
						{
							string result;
							return result;
						}
						case 21:
							num = 26;
							continue;
						case 22:
						{
							spr\u1BFD spr_u1BFD;
							if (spr_u1BFD.ᜉ() == this.ᜋ.ParentWorkbook.MaxRowCount - 1)
							{
								num = 20;
								continue;
							}
							goto IL_3A1;
						}
						case 23:
							goto IL_3EC;
						case 24:
							if (bRowExtract)
							{
								num = 0;
								continue;
							}
							num = 27;
							continue;
						case 25:
						{
							spr\u1BFD spr_u1BFD;
							if (spr_u1BFD.ᜋ() == 0)
							{
								num = 7;
								continue;
							}
							string result;
							return result;
						}
						case 26:
							if (bRowExtract)
							{
								num = 23;
								continue;
							}
							goto IL_150;
						case 27:
						{
							spr\u1BFD spr_u1BFD;
							if (spr_u1BFD.ᜋ() == 0)
							{
								num = 9;
								continue;
							}
							goto IL_3A1;
						}
						case 28:
							goto IL_350;
						case 29:
							goto IL_2A5;
						}
						break;
						IL_F8:
						num = 5;
						continue;
						IL_2D3:
						xlsName2 = xlsName;
						num = 4;
					}
				}
				IL_10E:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("氻䰽⤿ⱁぃቅⅇ㹉⁋⭍⍏", a_), RecordTableEnumerator.b("氻䰽⤿ⱁぃ᥅᱇⍉㡋≍㕏⅑瑓ᡕ㥗㝙㥛繝቟ݡݣ॥ᩧ๩䱫٭ᅯű味ŵ੷ᕹቻ᥽ꁿﺉ揄늑ﮓ뢗ﲙ춟힡좣장袧\udea9쎫얭햯\udcb1잳颵", a_));
				IL_14B:
				return this.ᜀ(new Ptg[]
				{
					array[2]
				});
				IL_150:
				return this.ᜀ(new Ptg[]
				{
					array[0]
				});
				IL_1A1:
				return this.ᜀ(new Ptg[]
				{
					array[1]
				});
				IL_2A5:
				goto IL_10E;
				IL_350:
				return null;
				IL_3A1:
				return null;
				IL_3EC:
				return this.ᜀ(new Ptg[]
				{
					array[1]
				});
				IL_3F1:
				return null;
			}
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000AD204 File Offset: 0x000AC204
		internal string ᜀ(Ptg[] A_0)
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
			XlsWorkbook parentWorkbook = this.ᜋ.ParentWorkbook;
			FormulaUtil formulaUtil = parentWorkbook.FormulaUtil;
			return formulaUtil.ᜀ(A_0, 0, 0, false, null, true);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000AD260 File Offset: 0x000AC260
		public XlsPageSetup Clone(object parent)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_126:
				if (true)
				{
				}
				num = 4;
				break;
			default:
				if (false)
				{
				}
				goto IL_3C;
			}
			XlsPageSetup xlsPageSetup;
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					goto IL_13B;
				case 1:
					goto IL_159;
				case 2:
					xlsPageSetup.ᜌ = (XlsHPageBreaksCollection)this.ᜌ.Clone(xlsPageSetup);
					num = 0;
					continue;
				case 3:
					if (this.ᜌ != null)
					{
						num = 2;
						continue;
					}
					goto IL_13B;
				case 4:
					goto IL_139;
				case 5:
					if (this.\u170D != null)
					{
						num = 1;
						continue;
					}
					return xlsPageSetup;
				}
				goto IL_3C;
				IL_13B:
				num = 5;
			}
			IL_139:
			return xlsPageSetup;
			IL_159:
			xlsPageSetup.\u170D = (XlsVPageBreaksCollection)this.\u170D.Clone(xlsPageSetup);
			goto IL_126;
			IL_3C:
			xlsPageSetup = (XlsPageSetup)base.MemberwiseClone();
			xlsPageSetup.SetParent(parent);
			xlsPageSetup.FindParents();
			this.ᜈ = (spr\u1922)spr\u1CD3.ᜀ(this.ᜈ);
			this.ᜉ = (spr\u2076)spr\u1CD3.ᜀ(this.ᜉ);
			this.ᜊ = (spr᧕)spr\u1CD3.ᜀ(this.ᜊ);
			this.ᜃ = (sprᾂ)spr\u1CD3.ᜀ(this.ᜃ);
			this.ᜄ = (spr\u1A56)spr\u1CD3.ᜀ(this.ᜄ);
			this.m_arrHeaders = spr\u1CD3.ᜀ(this.m_arrHeaders);
			this.m_arrFooters = spr\u1CD3.ᜀ(this.m_arrFooters);
			num = 3;
			goto IL_1E;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000AD3F0 File Offset: 0x000AC3F0
		// Note: this type is marked as 'beforefieldinit'.
		static XlsPageSetup()
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
			XlsPageSetup.ᜀ = sprῚ.ᜂ[6];
			XlsPageSetup.ᜁ = sprῚ.ᜂ[15];
			XlsPageSetup.ᜂ = sprῚ.ᜂ[7];
			XlsPageSetup.ᜃ = sprῚ.ᜂ[14];
			XlsPageSetup.ᜄ = new FormulaToken[]
			{
				FormulaToken.tRef3d1,
				FormulaToken.tRef3d2,
				FormulaToken.tRef3d3,
				FormulaToken.tArea3d1,
				FormulaToken.tArea3d2,
				FormulaToken.tArea3d3,
				FormulaToken.tCellRangeList
			};
		}

		// Token: 0x04000E19 RID: 3609
		internal new static readonly string ᜀ;

		// Token: 0x04000E1A RID: 3610
		internal new static readonly string ᜁ;

		// Token: 0x04000E1B RID: 3611
		private static readonly string ᜂ;

		// Token: 0x04000E1C RID: 3612
		private new static readonly string ᜃ;

		// Token: 0x04000E1D RID: 3613
		private new static readonly FormulaToken[] ᜄ;

		// Token: 0x04000E1E RID: 3614
		private ushort ᜅ;

		// Token: 0x04000E1F RID: 3615
		private new ushort ᜆ;

		// Token: 0x04000E20 RID: 3616
		private ushort ᜇ = 1;

		// Token: 0x04000E21 RID: 3617
		private spr\u1922 ᜈ;

		// Token: 0x04000E22 RID: 3618
		private spr\u2076 ᜉ;

		// Token: 0x04000E23 RID: 3619
		private spr᧕ ᜊ;

		// Token: 0x04000E24 RID: 3620
		private XlsWorksheet ᜋ;

		// Token: 0x04000E25 RID: 3621
		private XlsHPageBreaksCollection ᜌ;

		// Token: 0x04000E26 RID: 3622
		private int[] \u25D9\u0086ª\u0089;

		// Token: 0x04000E27 RID: 3623
		private XlsVPageBreaksCollection \u170D;

		// Token: 0x04000E28 RID: 3624
		private string ᜎ;
	}
}
