using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ACE RID: 2766
	internal sealed class PrintGridLines : BaseBiffRecord, IRecord
	{
		// Token: 0x06006869 RID: 26729 RVA: 0x001872DD File Offset: 0x001854DD
		public PrintGridLines() : base(43)
		{
			base.Length = 2;
			this.fPrintGrid = 0;
		}

		// Token: 0x0600686A RID: 26730 RVA: 0x001872F8 File Offset: 0x001854F8
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fPrintGrid);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x0600686B RID: 26731 RVA: 0x00187324 File Offset: 0x00185524
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PRINTGRIDLINES]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fPrintGrid={0};", this.fPrintGrid);
			stringBuilder.Append("[/PRINTGRIDLINES]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BA5 RID: 7077
		private const ushort type = 43;

		// Token: 0x04001BA6 RID: 7078
		private const ushort length = 2;

		// Token: 0x04001BA7 RID: 7079
		private ushort fPrintGrid;
	}
}
