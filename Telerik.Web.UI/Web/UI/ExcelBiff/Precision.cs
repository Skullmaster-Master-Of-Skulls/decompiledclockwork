using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ACD RID: 2765
	internal sealed class Precision : BaseBiffRecord, IRecord
	{
		// Token: 0x06006866 RID: 26726 RVA: 0x00187241 File Offset: 0x00185441
		public Precision() : base(14)
		{
			base.Length = 2;
			this.fFullPrecision = 1;
		}

		// Token: 0x06006867 RID: 26727 RVA: 0x0018725C File Offset: 0x0018545C
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fFullPrecision);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006868 RID: 26728 RVA: 0x00187288 File Offset: 0x00185488
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PRECISION]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fFullPrecision={0};", this.fFullPrecision);
			stringBuilder.Append("[/PRECISION]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BA2 RID: 7074
		private const ushort type = 14;

		// Token: 0x04001BA3 RID: 7075
		private const ushort length = 2;

		// Token: 0x04001BA4 RID: 7076
		private ushort fFullPrecision;
	}
}
