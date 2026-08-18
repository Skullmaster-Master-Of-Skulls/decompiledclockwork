using System;

namespace Telerik.Web
{
	// Token: 0x02000F62 RID: 3938
	public interface IClientStateManager
	{
		// Token: 0x17002F68 RID: 12136
		// (get) Token: 0x06009617 RID: 38423
		bool SupportsClientState { get; }

		// Token: 0x06009618 RID: 38424
		void LoadClientState(string clientState);

		// Token: 0x06009619 RID: 38425
		string SaveClientState();
	}
}
