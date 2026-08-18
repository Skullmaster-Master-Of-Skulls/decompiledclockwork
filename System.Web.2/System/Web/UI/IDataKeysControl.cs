using System;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	// Token: 0x020002A0 RID: 672
	public interface IDataKeysControl
	{
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001F87 RID: 8071
		string[] ClientIDRowSuffix { get; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001F88 RID: 8072
		DataKeyArray ClientIDRowSuffixDataKeys { get; }
	}
}
