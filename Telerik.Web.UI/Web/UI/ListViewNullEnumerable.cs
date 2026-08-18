using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020019A3 RID: 6563
	public class ListViewNullEnumerable : ListViewEnumerableBase
	{
		// Token: 0x0600FDDE RID: 64990 RVA: 0x0038FEE4 File Offset: 0x0038E0E4
		public override IEnumerable RawEnumerable()
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x0600FDDF RID: 64991 RVA: 0x0038FEF0 File Offset: 0x0038E0F0
		protected override void TransformEnumerable()
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x17004CAA RID: 19626
		// (get) Token: 0x0600FDE0 RID: 64992 RVA: 0x0038FEFC File Offset: 0x0038E0FC
		public override int DataSourceCount
		{
			get
			{
				throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
			}
		}
	}
}
