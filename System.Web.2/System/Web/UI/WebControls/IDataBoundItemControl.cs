using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200043E RID: 1086
	public interface IDataBoundItemControl : IDataBoundControl
	{
		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x0600348F RID: 13455
		DataKey DataKey { get; }

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06003490 RID: 13456
		DataBoundControlMode Mode { get; }
	}
}
