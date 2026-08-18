using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AB4 RID: 2740
	internal sealed class InterfaceHdr : BaseBiffRecord, IRecord
	{
		// Token: 0x06006812 RID: 26642 RVA: 0x001857F6 File Offset: 0x001839F6
		public InterfaceHdr() : base(225)
		{
			base.Length = 2;
			this.cv = 1200;
		}

		// Token: 0x06006813 RID: 26643 RVA: 0x00185818 File Offset: 0x00183A18
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.cv);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006814 RID: 26644 RVA: 0x00185844 File Offset: 0x00183A44
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[INTERFACEHDR]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("cv={0};", this.cv);
			stringBuilder.Append("[/INTERFACEHDR]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B2A RID: 6954
		private const ushort type = 225;

		// Token: 0x04001B2B RID: 6955
		private const ushort length = 2;

		// Token: 0x04001B2C RID: 6956
		private ushort cv;
	}
}
