using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AA9 RID: 2729
	internal sealed class GridSet : BaseBiffRecord, IRecord
	{
		// Token: 0x060067F7 RID: 26615 RVA: 0x00185015 File Offset: 0x00183215
		public GridSet() : base(130)
		{
			base.Length = 2;
			this.fGridSet = 1;
		}

		// Token: 0x060067F8 RID: 26616 RVA: 0x00185030 File Offset: 0x00183230
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fGridSet);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x0018505C File Offset: 0x0018325C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[GRIDSET]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fGridSet={0};", this.fGridSet);
			stringBuilder.Append("[/GRIDSET]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001AFC RID: 6908
		private const ushort type = 130;

		// Token: 0x04001AFD RID: 6909
		private const ushort length = 2;

		// Token: 0x04001AFE RID: 6910
		private ushort fGridSet;
	}
}
