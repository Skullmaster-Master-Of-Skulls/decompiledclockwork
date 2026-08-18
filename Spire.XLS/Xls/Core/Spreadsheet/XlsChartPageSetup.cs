using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200019F RID: 415
	public class XlsChartPageSetup : XlsPageSetupBase, IChartPageSetup
	{
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x000C7350 File Offset: 0x000C6350
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x000C739C File Offset: 0x000C639C
		public new bool FitToPagesTall
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
				return this.ᜄ.\u1712() != 0;
			}
			set
			{
				int num = 6;
				for (;;)
				{
					ushort num2;
					ushort num3;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BB;
						default:
							if (false)
							{
							}
							this.ᜄ.ᜅ(num2);
							base.SetChanged();
							num = 3;
							continue;
						}
						break;
					case 1:
						if (true)
						{
						}
						num = 5;
						continue;
					case 2:
						num3 = 1;
						goto IL_94;
					case 3:
						return;
					case 4:
						if (this.ᜄ.\u1712() != num2)
						{
							num = 0;
							continue;
						}
						return;
					case 5:
						num3 = 0;
						goto IL_94;
					}
					if (!value)
					{
						num = 1;
						continue;
					}
					goto IL_BB;
					IL_94:
					num2 = num3;
					num = 4;
					continue;
					IL_BB:
					num = 2;
				}
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000C7474 File Offset: 0x000C6474
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x000C74C0 File Offset: 0x000C64C0
		public new bool FitToPagesWide
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
				return this.ᜄ.ᜌ() != 0;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					ushort num2;
					ushort num3;
					switch (num)
					{
					case 0:
						num2 = 1;
						goto IL_86;
					case 1:
						num2 = 0;
						goto IL_86;
					case 2:
						num = 1;
						continue;
					case 4:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B5;
						default:
							if (false)
							{
							}
							this.ᜄ.ᜄ(num3);
							base.SetChanged();
							num = 6;
							continue;
						}
						break;
					case 5:
						if (this.ᜄ.ᜌ() != num3)
						{
							num = 4;
							continue;
						}
						return;
					case 6:
						return;
					}
					if (!value)
					{
						num = 2;
						continue;
					}
					goto IL_B5;
					IL_86:
					num3 = num2;
					num = 5;
					continue;
					IL_B5:
					num = 0;
				}
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x000C7590 File Offset: 0x000C6590
		internal XlsChartPageSetup(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.FindParents();
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000C75C0 File Offset: 0x000C65C0
		internal XlsChartPageSetup(spr\u1DF5 A_0, object A_1, sprἛ A_2) : base(A_0, A_1)
		{
			this.FindParents();
			this.ᜀ(A_2);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000C75F4 File Offset: 0x000C65F4
		internal XlsChartPageSetup(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1)
		{
			this.FindParents();
			A_3 = this.Parse(A_2, A_3);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000C7630 File Offset: 0x000C6630
		internal XlsChartPageSetup(spr\u1DF5 A_0, object A_1, List<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1)
		{
			this.FindParents();
			A_3 = this.Parse(A_2, A_3);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000C766C File Offset: 0x000C666C
		internal override bool ParseRecord(BiffRecordRaw record)
		{
			bool flag;
			for (;;)
			{
				flag = base.ParseRecord(record);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						TBIFFRecord typeCode;
						if (typeCode != TBIFFRecord.PrintedChartSize)
						{
							flag = false;
							num = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_49;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					case 1:
						if (!flag)
						{
							num = 4;
							continue;
						}
						return flag;
					case 2:
						goto IL_49;
					case 3:
						return flag;
					case 4:
					{
						flag = true;
						TBIFFRecord typeCode = record.TypeCode;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					case 5:
						return flag;
					}
					break;
					IL_49:
					this.ᜀ = (spr\u2605)record;
					num = 3;
				}
			}
			return flag;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000C772C File Offset: 0x000C672C
		internal void ᜀ(sprἛ A_0)
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
			throw new NotImplementedException();
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x000C776C File Offset: 0x000C676C
		internal override void SerializeEndRecords(RecordArrayList records)
		{
			int a_ = 11;
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6E;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					if (this.ᜀ == null)
					{
						num = 3;
						continue;
					}
					goto IL_A6;
				case 2:
					goto IL_58;
				case 3:
					goto IL_86;
				}
				if (records == null)
				{
					num = 2;
					continue;
				}
				IL_6E:
				num = 1;
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂♄⡆㭈⽊㹌", a_));
			IL_86:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱀ᱂♄⽆⡈㥊㥌ᱎ㡐⥒ご", a_));
			IL_A6:
			base.SerializeEndRecords(records);
			records.ᜀ((BiffRecordRaw)this.ᜀ.Clone());
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000C7840 File Offset: 0x000C6840
		public XlsChartPageSetup Clone(object parent)
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
			XlsChartPageSetup xlsChartPageSetup = (XlsChartPageSetup)base.MemberwiseClone();
			xlsChartPageSetup.SetParent(parent);
			xlsChartPageSetup.FindParents();
			this.m_arrFooters = spr\u1CD3.ᜀ(this.m_arrFooters);
			this.m_arrHeaders = spr\u1CD3.ᜀ(this.m_arrHeaders);
			this.ᜀ = (spr\u2605)spr\u1CD3.ᜀ(this.ᜀ);
			this.ᜄ = (spr\u1A56)spr\u1CD3.ᜀ(this.ᜄ);
			this.ᜃ = (sprᾂ)spr\u1CD3.ᜀ(this.ᜃ);
			return xlsChartPageSetup;
		}

		// Token: 0x04000F19 RID: 3865
		private float \u2460\u0090\u00A6\u00A3;

		// Token: 0x04000F1A RID: 3866
		private new spr\u2605 ᜀ = (spr\u2605)spr\u175E.ᜀ(TBIFFRecord.PrintedChartSize);
	}
}
