using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A74 RID: 2676
	internal sealed class CalcCount : BaseBiffRecord, IRecord
	{
		// Token: 0x0600670C RID: 26380 RVA: 0x00181CDF File Offset: 0x0017FEDF
		public CalcCount() : base(12)
		{
			base.Length = 2;
			this.cIter = 100;
		}

		// Token: 0x0600670D RID: 26381 RVA: 0x00181CF8 File Offset: 0x0017FEF8
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.cIter);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x00181D24 File Offset: 0x0017FF24
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CALCCOUNT]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("cIter={0};", this.cIter);
			stringBuilder.Append("[/CALCCOUNT]");
			return stringBuilder.ToString();
		}

		// Token: 0x040019E1 RID: 6625
		private const ushort type = 12;

		// Token: 0x040019E2 RID: 6626
		private const ushort length = 2;

		// Token: 0x040019E3 RID: 6627
		private ushort cIter;
	}
}
