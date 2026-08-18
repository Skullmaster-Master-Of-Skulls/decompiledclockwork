using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls
{
	// Token: 0x02000044 RID: 68
	public class SparklineGroupCollection : List<ISparklineGroup>, spr\u2342
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00029308 File Offset: 0x00028308
		internal SparklineGroupCollection(XlsWorkbook A_0)
		{
			int a_ = 7;
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("弼倾⹀⡂", a_));
			}
			this.ᜀ = A_0;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00029344 File Offset: 0x00028344
		public void Clear(SparklineGroup sparklineGroup)
		{
			int a_ = 18;
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
				if (sparklineGroup == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("ᭇ㩉ⵋ㱍㭏㹑㵓㡕㵗ᵙ⹛ㅝᕟቡ", a_));
				}
				break;
			}
			base.Remove(sparklineGroup);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000293AC File Offset: 0x000283AC
		public SparklineGroup AddGroup(SparklineType sparklineType)
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
			SparklineGroup sparklineGroup = new SparklineGroup(this.ᜀ);
			sparklineGroup.SparklineType = sparklineType;
			base.Add(sparklineGroup);
			return sparklineGroup;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00029404 File Offset: 0x00028404
		public SparklineGroup AddGroup()
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
			SparklineGroup sparklineGroup = new SparklineGroup(this.ᜀ);
			base.Add(sparklineGroup);
			return sparklineGroup;
		}

		// Token: 0x040000D9 RID: 217
		private float \u2593\u007F\u00B0\u0080;

		// Token: 0x040000DA RID: 218
		private bool \u25D9\u009A\u0094\u009A;

		// Token: 0x040000DB RID: 219
		private long \u25D8\u008E\u0085\u00AB;

		// Token: 0x040000DC RID: 220
		private int[] \u25D8\u008E\u008F\u00A4;

		// Token: 0x040000DD RID: 221
		private long \u25D9\u009C\u0083\u0089;

		// Token: 0x040000DE RID: 222
		private XlsWorkbook ᜀ;
	}
}
