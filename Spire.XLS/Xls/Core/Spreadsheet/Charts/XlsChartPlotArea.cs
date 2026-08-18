using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x0200017D RID: 381
	public class XlsChartPlotArea : XlsChartFrameFormat, IChartFrameFormat
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x000B07E0 File Offset: 0x000AF7E0
		internal XlsChartPlotArea(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ = (spr\u17F1)spr\u175E.ᜀ(TBIFFRecord.ChartPlotArea);
			base.Border.Pattern = ChartLinePatternType.None;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x000B0818 File Offset: 0x000AF818
		internal XlsChartPlotArea(spr\u1DF5 A_0, object A_1, ExcelChartType A_2) : this(A_0, A_1)
		{
			bool flag = Array.IndexOf<ExcelChartType>(XlsChart.ᜨ, A_2) == -1;
			flag = (flag && Array.IndexOf<ExcelChartType>(XlsChart.ᜱ, A_2) == -1);
			if (flag && base.Workbook.Version == ExcelVersion.Version97to2003)
			{
				base.Interior.ForegroundKnownColor = ExcelColors.Gray25Percent;
				return;
			}
			base.Interior.ForegroundKnownColor = ExcelColors.WhiteCustom;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000B0884 File Offset: 0x000AF884
		internal XlsChartPlotArea(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1, false)
		{
			this.ᜀ(A_2, ref A_3);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000B08A4 File Offset: 0x000AF8A4
		internal new void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 5;
			int num = 1;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
					base.ᜀ(A_0, ref A_1);
					num = 4;
					continue;
				case 2:
					goto IL_4A;
				case 3:
					if (biffRecordRaw.TypeCode == TBIFFRecord.ChartFrame)
					{
						num = 0;
						continue;
					}
					goto IL_C6;
				case 4:
					goto IL_C6;
				}
				goto IL_2D;
				IL_30:
				if (true)
				{
				}
				num = 2;
				continue;
				IL_2D:
				if (A_0 == null)
				{
					goto IL_30;
				}
				biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartPlotArea);
				this.ᜀ = (spr\u17F1)biffRecordRaw;
				A_1++;
				biffRecordRaw = A_0[A_1];
				num = 3;
				continue;
				IL_C6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_30;
				default:
					goto IL_DC;
				}
			}
			IL_4A:
			throw new ArgumentNullException(RecordTableEnumerator.b("强尼䬾⁀", a_));
			IL_DC:
			if (false)
			{
			}
			A_1--;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000B099C File Offset: 0x000AF99C
		internal new void ᜀ(IList<IRecordStorage> A_0)
		{
			int a_ = 1;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.ᜀ != null)
					{
						num = 4;
						continue;
					}
					goto IL_9A;
				case 2:
					goto IL_9A;
				case 3:
					goto IL_38;
				case 4:
					A_0.Add((BiffRecordRaw)this.ᜀ.Clone());
					num = 2;
					continue;
				}
				IL_2D:
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
				IL_9A:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2D;
				default:
					goto IL_B0;
				}
			}
			IL_38:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀あ", a_));
			IL_B0:
			if (false)
			{
			}
			base.ᜀ(A_0);
		}

		// Token: 0x04000E47 RID: 3655
		private byte \u2609\u0080\u0089\u0089;

		// Token: 0x04000E48 RID: 3656
		private string[] \u25D8\u0081\u0089\u00B0;

		// Token: 0x04000E49 RID: 3657
		private new spr\u17F1 ᜀ;
	}
}
