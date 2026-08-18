using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200004F RID: 79
	public class AutoCompleteExtenderDesigner : ExtenderControlBaseDesigner<AutoCompleteExtender>
	{
		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x060002A7 RID: 679
		[PageMethodSignature("AutoComplete", "ServicePath", "ServiceMethod", "UseContextKey")]
		private delegate string[] GetCompletionList(string prefixText, int count, string contextKey);
	}
}
