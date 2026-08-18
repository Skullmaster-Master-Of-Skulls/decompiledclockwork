using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A80 RID: 2688
	internal sealed class DefColWidth : BaseBiffRecord, IRecord
	{
		// Token: 0x06006769 RID: 26473 RVA: 0x001829F0 File Offset: 0x00180BF0
		public DefColWidth() : base(85)
		{
			base.Length = 2;
			this.cchdefColWidth = 8;
		}

		// Token: 0x0600676A RID: 26474 RVA: 0x00182A08 File Offset: 0x00180C08
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.cchdefColWidth);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x0600676B RID: 26475 RVA: 0x00182A34 File Offset: 0x00180C34
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DEFCOLWIDTH]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("cchdefColWidth={0};", this.cchdefColWidth);
			stringBuilder.Append("[/DEFCOLWIDTH]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001A22 RID: 6690
		private const ushort type = 85;

		// Token: 0x04001A23 RID: 6691
		private const ushort length = 2;

		// Token: 0x04001A24 RID: 6692
		private ushort cchdefColWidth;
	}
}
