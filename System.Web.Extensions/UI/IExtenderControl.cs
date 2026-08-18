using System;
using System.Collections.Generic;

namespace System.Web.UI
{
	// Token: 0x02000058 RID: 88
	public interface IExtenderControl
	{
		// Token: 0x06000310 RID: 784
		IEnumerable<ScriptDescriptor> GetScriptDescriptors(Control targetControl);

		// Token: 0x06000311 RID: 785
		IEnumerable<ScriptReference> GetScriptReferences();
	}
}
