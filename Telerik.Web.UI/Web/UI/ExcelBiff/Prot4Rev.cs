using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AD0 RID: 2768
	internal sealed class Prot4Rev : BaseBiffRecord, IRecord
	{
		// Token: 0x0600686F RID: 26735 RVA: 0x00187415 File Offset: 0x00185615
		public Prot4Rev() : base(431)
		{
			base.Length = 2;
			this.fRevLock = 0;
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x00187430 File Offset: 0x00185630
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fRevLock);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006871 RID: 26737 RVA: 0x0018745C File Offset: 0x0018565C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PROT4REV]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fRevLock={0};", this.fRevLock);
			stringBuilder.Append("[/PROT4REV]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BAB RID: 7083
		private const ushort type = 431;

		// Token: 0x04001BAC RID: 7084
		private const ushort length = 2;

		// Token: 0x04001BAD RID: 7085
		private ushort fRevLock;
	}
}
