using System;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000183 RID: 387
	public class ChartDataTableXls : XlsObject, IChartDataTable
	{
		// Token: 0x06001273 RID: 4723 RVA: 0x000B4400 File Offset: 0x000B3400
		internal ChartDataTableXls(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ = (spr\u1C8F)spr\u175E.ᜀ(TBIFFRecord.ChartDat);
			this.ᜀ.ᜁ(true);
			this.ᜀ.ᜂ(true);
			this.ᜀ.ᜀ(true);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000B445C File Offset: 0x000B345C
		internal ChartDataTableXls(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1)
		{
			this.ᜀ(A_2, ref A_3);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000B4488 File Offset: 0x000B3488
		private void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 10;
			int num = 2;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_EF;
				case 1:
					goto IL_EF;
				case 3:
					if (biffRecordRaw.TypeCode == TBIFFRecord.End)
					{
						num = 9;
						continue;
					}
					if (true)
					{
					}
					num = 4;
					continue;
				case 4:
					if (biffRecordRaw.TypeCode == TBIFFRecord.Begin)
					{
						num = 8;
						continue;
					}
					goto IL_121;
				case 5:
					if (num2 == 0)
					{
						num = 7;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					num = 3;
					continue;
				case 6:
					goto IL_54;
				case 7:
					return;
				case 8:
					num2++;
					num = 10;
					continue;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EF;
					default:
						if (false)
						{
						}
						num2--;
						num = 11;
						continue;
					}
					break;
				case 10:
					goto IL_121;
				case 11:
					goto IL_121;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartDat);
				this.ᜀ = (spr\u1C8F)biffRecordRaw;
				A_1++;
				biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.Begin);
				this.ᜁ.Add(biffRecordRaw);
				A_1++;
				num2 = 1;
				num = 0;
				continue;
				IL_EF:
				num = 5;
				continue;
				IL_121:
				this.ᜁ.Add(biffRecordRaw);
				A_1++;
				num = 1;
			}
			IL_54:
			throw new ArgumentNullException(RecordTableEnumerator.b("␿⍁ぃ❅", a_));
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x000B4640 File Offset: 0x000B3640
		[CLSCompliant(false)]
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 8;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1CD;
				case 1:
				{
					this.ᜁ.Add(spr\u175E.ᜀ(TBIFFRecord.Begin));
					spr\u1A8D spr_u1A8D = (spr\u1A8D)spr\u175E.ᜀ(TBIFFRecord.ChartLegend);
					spr_u1A8D.ᜂ(true);
					spr_u1A8D.ᜅ(true);
					spr_u1A8D.ᜀ(LegendPositionType.NotDocked);
					spr_u1A8D.ᜀ(LegendSpacingType.Medium);
					this.ᜁ.Add(spr_u1A8D);
					this.ᜁ.Add(spr\u175E.ᜀ(TBIFFRecord.Begin));
					spr\u23BE spr_u23BE = (spr\u23BE)spr\u175E.ᜀ(TBIFFRecord.ChartPos);
					spr_u23BE.ᜁ(3);
					this.ᜁ.Add(spr_u23BE);
					spr\u20B6 spr_u20B = (spr\u20B6)spr\u175E.ᜀ(TBIFFRecord.ChartText);
					spr_u20B.ᜀ(10816);
					this.ᜁ.Add(spr_u20B);
					this.ᜁ.Add(spr\u175E.ᜀ(TBIFFRecord.Begin));
					spr_u23BE = (spr\u23BE)spr\u175E.ᜀ(TBIFFRecord.ChartPos);
					spr_u23BE.ᜁ(2);
					spr_u23BE.ᜀ(2);
					this.ᜁ.Add(spr_u23BE);
					this.ᜁ.Add(spr\u175E.ᜀ(TBIFFRecord.ChartFontx));
					this.ᜁ.Add(spr\u175E.ᜀ(TBIFFRecord.ChartAI));
					this.ᜁ.Add(spr\u175E.ᜀ(TBIFFRecord.End));
					this.ᜁ.Add(spr\u175E.ᜀ(TBIFFRecord.End));
					this.ᜁ.Add(spr\u175E.ᜀ(TBIFFRecord.End));
					num = 0;
					continue;
				}
				case 2:
					goto IL_5F;
				case 4:
					if (this.ᜁ.Count == 0)
					{
						num = 1;
						continue;
					}
					goto IL_21B;
				}
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
					if (records == null)
					{
						num = 2;
					}
					else
					{
						records.ᜀ(this.ᜀ);
						num = 4;
					}
					break;
				}
			}
			IL_5F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⅁⭃㑅ⱇ㥉", a_));
			IL_1CD:
			IL_21B:
			records.AddList(this.ᜁ);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000B4874 File Offset: 0x000B3874
		public ChartDataTableXls Clone(object parent)
		{
			switch (0)
			{
			default:
			{
				ChartDataTableXls chartDataTableXls;
				for (;;)
				{
					chartDataTableXls = new ChartDataTableXls(base.ReservedHandle, parent);
					chartDataTableXls.m_bIsDisposed = this.m_bIsDisposed;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 4;
								continue;
							}
							BiffRecordRaw item = (BiffRecordRaw)this.ᜁ[num2].Clone();
							List<BiffRecordRaw> list;
							list.Add(item);
							num2++;
							num = 8;
							continue;
						}
						case 1:
							goto IL_94;
						case 2:
							return chartDataTableXls;
						case 3:
							goto IL_96;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_94;
							default:
							{
								if (true)
								{
								}
								if (false)
								{
								}
								List<BiffRecordRaw> list;
								chartDataTableXls.ᜁ = list;
								num = 7;
								continue;
							}
							}
							break;
						case 5:
							if (this.ᜁ != null)
							{
								num = 9;
								continue;
							}
							goto IL_74;
						case 6:
							if (this.ᜀ != null)
							{
								num = 1;
								continue;
							}
							return chartDataTableXls;
						case 7:
							goto IL_74;
						case 8:
							goto IL_96;
						case 9:
						{
							List<BiffRecordRaw> list = new List<BiffRecordRaw>();
							int num2 = 0;
							int count = this.ᜁ.Count;
							num = 3;
							continue;
						}
						}
						break;
						IL_74:
						num = 6;
						continue;
						IL_94:
						chartDataTableXls.ᜀ = (spr\u1C8F)this.ᜀ.Clone();
						num = 2;
						continue;
						IL_96:
						num = 0;
					}
				}
				return chartDataTableXls;
			}
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x000B49F8 File Offset: 0x000B39F8
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x000B4A40 File Offset: 0x000B3A40
		public bool HasHorzBorder
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
				return this.ᜀ.ᜁ();
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
				this.ᜀ.ᜂ(value);
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x000B4A88 File Offset: 0x000B3A88
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x000B4AD0 File Offset: 0x000B3AD0
		public bool HasVertBorder
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
				return this.ᜀ.ᜂ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x000B4B18 File Offset: 0x000B3B18
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x000B4B60 File Offset: 0x000B3B60
		public bool HasBorders
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
				return this.ᜀ.ᜀ();
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
				this.ᜀ.ᜁ(value);
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x000B4BA8 File Offset: 0x000B3BA8
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x000B4BF0 File Offset: 0x000B3BF0
		public bool ShowSeriesKeys
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
				return this.ᜀ.ᜄ();
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
				this.ᜀ.ᜃ(value);
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x000B4C38 File Offset: 0x000B3C38
		public ChartTextArea TextArea
		{
			get
			{
				int a_ = 18;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_72;
					case 2:
						this.ᜂ = new ChartTextArea((spr\u2158)base.AppImplementation, this);
						this.ᜂ.FontName = RecordTableEnumerator.b("େ⭉⁋❍㉏⁑㵓", a_);
						num = 3;
						continue;
					case 3:
						goto IL_B9;
					}
					if (this.ᜂ != null)
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
							spr\u1AA0.ᜀ(this.ᜂ);
							num = 0;
							continue;
						}
					}
					if (true)
					{
					}
					num = 2;
				}
				IL_72:
				IL_B9:
				return this.ᜂ as ChartTextArea;
			}
		}

		// Token: 0x04000E61 RID: 3681
		private long \u2460\u00A6\u00AC\u00A4;

		// Token: 0x04000E62 RID: 3682
		private long[] \u2460\u0089\u008F\u0089;

		// Token: 0x04000E63 RID: 3683
		private byte[] \u25D8\u008D\u009D\u008E;

		// Token: 0x04000E64 RID: 3684
		private byte \u2460\u0092\u0089\u00A4;

		// Token: 0x04000E65 RID: 3685
		private spr\u1C8F ᜀ;

		// Token: 0x04000E66 RID: 3686
		private List<BiffRecordRaw> ᜁ = new List<BiffRecordRaw>();

		// Token: 0x04000E67 RID: 3687
		private float[] \u2460\u00A4\u0099\u0086;

		// Token: 0x04000E68 RID: 3688
		private XlsChartTextArea ᜂ;
	}
}
