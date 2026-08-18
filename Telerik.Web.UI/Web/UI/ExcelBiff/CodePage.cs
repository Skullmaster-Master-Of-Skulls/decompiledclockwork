using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A78 RID: 2680
	internal sealed class CodePage : BaseBiffRecord, IRecord
	{
		// Token: 0x0600674D RID: 26445 RVA: 0x001822E9 File Offset: 0x001804E9
		public CodePage() : base(66)
		{
			base.Length = 2;
			this.cv = 1200;
		}

		// Token: 0x0600674E RID: 26446 RVA: 0x00182308 File Offset: 0x00180508
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.cv);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x0600674F RID: 26447 RVA: 0x00182334 File Offset: 0x00180534
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CODEPAGE]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("cv={0};", this.cv);
			stringBuilder.Append("[/CODEPAGE]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001A02 RID: 6658
		private const ushort type = 66;

		// Token: 0x04001A03 RID: 6659
		private const ushort length = 2;

		// Token: 0x04001A04 RID: 6660
		private ushort cv;
	}
}
