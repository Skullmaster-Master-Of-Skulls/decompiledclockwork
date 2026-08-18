using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ACF RID: 2767
	internal sealed class PrintHeaders : BaseBiffRecord, IRecord
	{
		// Token: 0x0600686C RID: 26732 RVA: 0x00187379 File Offset: 0x00185579
		public PrintHeaders() : base(42)
		{
			base.Length = 2;
			this.fPrintRwCol = 0;
		}

		// Token: 0x0600686D RID: 26733 RVA: 0x00187394 File Offset: 0x00185594
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fPrintRwCol);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x0600686E RID: 26734 RVA: 0x001873C0 File Offset: 0x001855C0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PRINTHEADERS]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fPrintRwCol={0};", this.fPrintRwCol);
			stringBuilder.Append("[/PRINTHEADERS]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BA8 RID: 7080
		private const ushort type = 42;

		// Token: 0x04001BA9 RID: 7081
		private const ushort length = 2;

		// Token: 0x04001BAA RID: 7082
		private ushort fPrintRwCol;
	}
}
