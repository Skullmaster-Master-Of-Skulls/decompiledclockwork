using System;

namespace AjaxControlToolkit
{
	// Token: 0x0200001A RID: 26
	public interface IClientStateManager
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000100 RID: 256
		bool SupportsClientState { get; }

		// Token: 0x06000101 RID: 257
		void LoadClientState(string clientState);

		// Token: 0x06000102 RID: 258
		string SaveClientState();
	}
}
