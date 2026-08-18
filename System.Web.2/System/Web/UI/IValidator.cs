using System;

namespace System.Web.UI
{
	// Token: 0x020002BE RID: 702
	public interface IValidator
	{
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001FDA RID: 8154
		// (set) Token: 0x06001FDB RID: 8155
		bool IsValid { get; set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001FDC RID: 8156
		// (set) Token: 0x06001FDD RID: 8157
		string ErrorMessage { get; set; }

		// Token: 0x06001FDE RID: 8158
		void Validate();
	}
}
