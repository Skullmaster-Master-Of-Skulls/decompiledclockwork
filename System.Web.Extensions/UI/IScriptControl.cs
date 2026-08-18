using System;
using System.Collections.Generic;

namespace System.Web.UI
{
	// Token: 0x0200005B RID: 91
	public interface IScriptControl
	{
		// Token: 0x06000335 RID: 821
		IEnumerable<ScriptDescriptor> GetScriptDescriptors();

		// Token: 0x06000336 RID: 822
		IEnumerable<ScriptReference> GetScriptReferences();
	}
}
