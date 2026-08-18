using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AD8 RID: 2776
	internal sealed class RefMode : BaseBiffRecord, IRecord
	{
		// Token: 0x060068A7 RID: 26791 RVA: 0x00188365 File Offset: 0x00186565
		public RefMode() : base(15)
		{
			base.Length = 2;
			this.fRefA1 = 1;
		}

		// Token: 0x060068A8 RID: 26792 RVA: 0x00188380 File Offset: 0x00186580
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fRefA1);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060068A9 RID: 26793 RVA: 0x001883AC File Offset: 0x001865AC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[REFMODE]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fRefA1={0};", this.fRefA1);
			stringBuilder.Append("[/REFMODE]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BC9 RID: 7113
		private const ushort type = 15;

		// Token: 0x04001BCA RID: 7114
		private const ushort length = 2;

		// Token: 0x04001BCB RID: 7115
		private ushort fRefA1;
	}
}
