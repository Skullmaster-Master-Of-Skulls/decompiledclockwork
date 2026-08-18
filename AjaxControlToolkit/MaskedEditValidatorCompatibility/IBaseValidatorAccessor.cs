using System;

namespace AjaxControlToolkit.MaskedEditValidatorCompatibility
{
	// Token: 0x02000133 RID: 307
	internal interface IBaseValidatorAccessor : IWebControlAccessor
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060007A0 RID: 1952
		bool RenderUpLevel { get; }

		// Token: 0x060007A1 RID: 1953
		void EnsureID();

		// Token: 0x060007A2 RID: 1954
		string GetControlRenderID(string name);
	}
}
