using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A84 RID: 2692
	internal sealed class EOF : BaseBiffRecord, IRecord
	{
		// Token: 0x06006774 RID: 26484 RVA: 0x00182C89 File Offset: 0x00180E89
		public EOF() : base(10)
		{
			base.Length = 0;
		}

		// Token: 0x06006775 RID: 26485 RVA: 0x00182C9C File Offset: 0x00180E9C
		public byte[] GetData()
		{
			int num;
			return base.GetData(out num);
		}

		// Token: 0x04001A32 RID: 6706
		private const ushort type = 10;

		// Token: 0x04001A33 RID: 6707
		private const ushort length = 0;
	}
}
