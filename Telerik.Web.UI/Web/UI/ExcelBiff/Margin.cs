using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AB8 RID: 2744
	internal sealed class Margin : BaseBiffRecord, IRecord
	{
		// Token: 0x0600681D RID: 26653 RVA: 0x00185B45 File Offset: 0x00183D45
		public Margin(double margin, ushort type) : base(type)
		{
			base.Length = 8;
			this.marginSize = margin;
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x00185B5C File Offset: 0x00183D5C
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.marginSize);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x00185B88 File Offset: 0x00183D88
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[MARGIN]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("RecordType=0x{0:x4};", base.RecordType);
			stringBuilder.AppendFormat("marginSize={0};", this.marginSize);
			stringBuilder.Append("[/MARGIN]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B3E RID: 6974
		public const ushort LeftMarginType = 38;

		// Token: 0x04001B3F RID: 6975
		public const ushort RightMarginType = 39;

		// Token: 0x04001B40 RID: 6976
		public const ushort TopMarginType = 40;

		// Token: 0x04001B41 RID: 6977
		public const ushort BottomMarginType = 41;

		// Token: 0x04001B42 RID: 6978
		private const ushort length = 8;

		// Token: 0x04001B43 RID: 6979
		private double marginSize;
	}
}
