using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A81 RID: 2689
	internal sealed class Delta : BaseBiffRecord, IRecord
	{
		// Token: 0x0600676C RID: 26476 RVA: 0x00182A89 File Offset: 0x00180C89
		public Delta() : base(16)
		{
			base.Length = 8;
			this.numDelta = new byte[0];
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x00182AA8 File Offset: 0x00180CA8
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			this.numDelta.CopyTo(data, index);
			return data;
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x00182ACC File Offset: 0x00180CCC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DELTA]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("numDelta={0};", this.numDelta);
			stringBuilder.Append("[/DELTA]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001A25 RID: 6693
		private const ushort type = 16;

		// Token: 0x04001A26 RID: 6694
		private const ushort length = 8;

		// Token: 0x04001A27 RID: 6695
		private byte[] numDelta;
	}
}
