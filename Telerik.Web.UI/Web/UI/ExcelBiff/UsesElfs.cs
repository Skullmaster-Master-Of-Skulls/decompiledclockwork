using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AEA RID: 2794
	internal sealed class UsesElfs : BaseBiffRecord, IRecord
	{
		// Token: 0x060068F0 RID: 26864 RVA: 0x00189B20 File Offset: 0x00187D20
		public UsesElfs() : base(352)
		{
			base.Length = 2;
			this.fUsesElfs = 0;
		}

		// Token: 0x060068F1 RID: 26865 RVA: 0x00189B3C File Offset: 0x00187D3C
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fUsesElfs);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060068F2 RID: 26866 RVA: 0x00189B68 File Offset: 0x00187D68
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[USESELFS]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fUsesElfs={0};", this.fUsesElfs);
			stringBuilder.Append("[/USESELFS]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C20 RID: 7200
		private const ushort type = 352;

		// Token: 0x04001C21 RID: 7201
		private const ushort length = 2;

		// Token: 0x04001C22 RID: 7202
		private ushort fUsesElfs;
	}
}
