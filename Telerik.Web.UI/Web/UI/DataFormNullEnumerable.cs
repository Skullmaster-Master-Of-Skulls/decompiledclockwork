using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020001E8 RID: 488
	public class DataFormNullEnumerable : DataFormEnumerableBase
	{
		// Token: 0x06001146 RID: 4422 RVA: 0x0003EE70 File Offset: 0x0003D070
		public override IEnumerable RawEnumerable()
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003EE7C File Offset: 0x0003D07C
		protected override void TransformEnumerable()
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0003EE88 File Offset: 0x0003D088
		public override int DataSourceCount
		{
			get
			{
				throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
			}
		}
	}
}
