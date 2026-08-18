using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000141 RID: 321
	public class ModalPopupExtenderDesigner : ExtenderControlBaseDesigner<ModalPopupExtender>
	{
		// Token: 0x02000142 RID: 322
		// (Invoke) Token: 0x06000833 RID: 2099
		[PageMethodSignature("Dynamic Populate", "DynamicServicePath", "DynamicServiceMethod")]
		private delegate string GetDynamicContent(string contextKey);
	}
}
