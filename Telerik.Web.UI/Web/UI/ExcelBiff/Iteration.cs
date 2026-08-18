using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AB5 RID: 2741
	internal sealed class Iteration : BaseBiffRecord, IRecord
	{
		// Token: 0x06006815 RID: 26645 RVA: 0x00185899 File Offset: 0x00183A99
		public Iteration() : base(17)
		{
			base.Length = 2;
			this.fIter = 0;
		}

		// Token: 0x06006816 RID: 26646 RVA: 0x001858B4 File Offset: 0x00183AB4
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fIter);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006817 RID: 26647 RVA: 0x001858E0 File Offset: 0x00183AE0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ITERATION]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fIter={0};", this.fIter);
			stringBuilder.Append("[/ITERATION]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B2D RID: 6957
		private const ushort type = 17;

		// Token: 0x04001B2E RID: 6958
		private const ushort length = 2;

		// Token: 0x04001B2F RID: 6959
		private ushort fIter;
	}
}
