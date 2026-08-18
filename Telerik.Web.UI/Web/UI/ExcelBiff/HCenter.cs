using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AAB RID: 2731
	internal sealed class HCenter : BaseBiffRecord, IRecord
	{
		// Token: 0x060067FD RID: 26621 RVA: 0x001851FA File Offset: 0x001833FA
		public HCenter() : base(131)
		{
			base.Length = 2;
			this.fHCenter = 0;
		}

		// Token: 0x060067FE RID: 26622 RVA: 0x00185218 File Offset: 0x00183418
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fHCenter);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060067FF RID: 26623 RVA: 0x00185244 File Offset: 0x00183444
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[HCENTER]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fHCenter={0};", this.fHCenter);
			stringBuilder.Append("[/HCENTER]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B05 RID: 6917
		private const ushort type = 131;

		// Token: 0x04001B06 RID: 6918
		private const ushort length = 2;

		// Token: 0x04001B07 RID: 6919
		private ushort fHCenter;
	}
}
