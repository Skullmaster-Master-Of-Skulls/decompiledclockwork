using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AEB RID: 2795
	internal sealed class VCenter : BaseBiffRecord, IRecord
	{
		// Token: 0x060068F3 RID: 26867 RVA: 0x00189BBD File Offset: 0x00187DBD
		public VCenter() : base(132)
		{
			base.Length = 2;
			this.fVCenter = 0;
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x00189BD8 File Offset: 0x00187DD8
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fVCenter);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060068F5 RID: 26869 RVA: 0x00189C04 File Offset: 0x00187E04
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[VCENTER]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fVCenter={0};", this.fVCenter);
			stringBuilder.Append("[/VCENTER]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C23 RID: 7203
		private const ushort type = 132;

		// Token: 0x04001C24 RID: 7204
		private const ushort length = 2;

		// Token: 0x04001C25 RID: 7205
		private ushort fVCenter;
	}
}
