using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020000C6 RID: 198
	public class ProxyBoundControlNullEnumerable : ProxyBoundControlEnumerableBase
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x0001CACC File Offset: 0x0001ACCC
		public override IEnumerable RawEnumerable()
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001CAD8 File Offset: 0x0001ACD8
		protected override void TransformEnumerable()
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		public override int DataSourceCount
		{
			get
			{
				throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
			}
		}
	}
}
