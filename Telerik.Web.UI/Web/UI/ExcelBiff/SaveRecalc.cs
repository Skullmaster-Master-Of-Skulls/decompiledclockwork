using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ADD RID: 2781
	internal sealed class SaveRecalc : BaseBiffRecord, IRecord
	{
		// Token: 0x060068BB RID: 26811 RVA: 0x001887FE File Offset: 0x001869FE
		public SaveRecalc() : base(95)
		{
			base.Length = 2;
			this.fSaveRecalc = 1;
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x00188818 File Offset: 0x00186A18
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fSaveRecalc);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x00188844 File Offset: 0x00186A44
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SAVERECALC]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fSaveRecalc={0};", this.fSaveRecalc);
			stringBuilder.Append("[/SAVERECALC]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BE1 RID: 7137
		private const ushort type = 95;

		// Token: 0x04001BE2 RID: 7138
		private const ushort length = 2;

		// Token: 0x04001BE3 RID: 7139
		private ushort fSaveRecalc;
	}
}
