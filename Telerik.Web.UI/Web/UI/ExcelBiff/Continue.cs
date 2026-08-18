using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A7B RID: 2683
	internal sealed class Continue : BaseBiffRecord, IRecord
	{
		// Token: 0x06006757 RID: 26455 RVA: 0x0018253D File Offset: 0x0018073D
		public Continue() : base(60)
		{
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x00182547 File Offset: 0x00180747
		internal Continue(byte[] data) : base(60)
		{
			base.Length = (ushort)data.Length;
			this.dataContinue = data;
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x00182564 File Offset: 0x00180764
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			this.dataContinue.CopyTo(data, index);
			return data;
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x00182588 File Offset: 0x00180788
		public byte[] GetHeaderData(ushort length)
		{
			base.Length = length;
			return base.GetBaseData();
		}

		// Token: 0x04001A11 RID: 6673
		private const ushort type = 60;

		// Token: 0x04001A12 RID: 6674
		private byte[] dataContinue;
	}
}
