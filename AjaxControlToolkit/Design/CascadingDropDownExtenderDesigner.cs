using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000065 RID: 101
	public class CascadingDropDownExtenderDesigner : ExtenderControlBaseDesigner<CascadingDropDown>
	{
		// Token: 0x02000066 RID: 102
		// (Invoke) Token: 0x06000382 RID: 898
		[PageMethodSignature("CascadingDropDown", "ServicePath", "ServiceMethod", "UseContextKey")]
		private delegate CascadingDropDownNameValue[] GetDropDownContents(string knownCategoryValues, string category);
	}
}
