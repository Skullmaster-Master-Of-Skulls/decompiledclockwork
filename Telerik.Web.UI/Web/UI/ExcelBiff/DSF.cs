using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A83 RID: 2691
	internal sealed class DSF : BaseBiffRecord, IRecord
	{
		// Token: 0x06006771 RID: 26481 RVA: 0x00182BED File Offset: 0x00180DED
		public DSF() : base(353)
		{
			base.Length = 2;
			this.fDSF = 0;
		}

		// Token: 0x06006772 RID: 26482 RVA: 0x00182C08 File Offset: 0x00180E08
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fDSF);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006773 RID: 26483 RVA: 0x00182C34 File Offset: 0x00180E34
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DSF]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("dsf={0};", this.fDSF);
			stringBuilder.Append("[/DSF]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001A2F RID: 6703
		private const ushort type = 353;

		// Token: 0x04001A30 RID: 6704
		private const ushort length = 2;

		// Token: 0x04001A31 RID: 6705
		private ushort fDSF;
	}
}
