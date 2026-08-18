using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020005EB RID: 1515
	public interface IDesignerOptionService
	{
		// Token: 0x06003825 RID: 14373
		object GetOptionValue(string pageName, string valueName);

		// Token: 0x06003826 RID: 14374
		void SetOptionValue(string pageName, string valueName, object value);
	}
}
