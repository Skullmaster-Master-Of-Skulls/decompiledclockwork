using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000119 RID: 281
	public class ClientDataSourceFilterEntryCollection : List<ClientDataSourceFilterEntry>, IStateManager
	{
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0002839D File Offset: 0x0002659D
		public bool IsTrackingViewState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000283A4 File Offset: 0x000265A4
		public void LoadViewState(object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x000283AB File Offset: 0x000265AB
		public object SaveViewState()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000283B2 File Offset: 0x000265B2
		public void TrackViewState()
		{
			throw new NotImplementedException();
		}
	}
}
