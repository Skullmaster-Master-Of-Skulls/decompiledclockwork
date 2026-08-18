using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A75 RID: 2677
	internal sealed class CalcMode : BaseBiffRecord, IRecord
	{
		// Token: 0x0600670F RID: 26383 RVA: 0x00181D79 File Offset: 0x0017FF79
		public CalcMode() : base(13)
		{
			base.Length = 2;
			this.fAutoRecalc = 1;
		}

		// Token: 0x06006710 RID: 26384 RVA: 0x00181D94 File Offset: 0x0017FF94
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fAutoRecalc);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006711 RID: 26385 RVA: 0x00181DC0 File Offset: 0x0017FFC0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CALCMODE]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fAutoRecalc={0};", this.fAutoRecalc);
			stringBuilder.Append("[/CALCMODE]");
			return stringBuilder.ToString();
		}

		// Token: 0x040019E4 RID: 6628
		private const ushort type = 13;

		// Token: 0x040019E5 RID: 6629
		private const ushort length = 2;

		// Token: 0x040019E6 RID: 6630
		private ushort fAutoRecalc;
	}
}
