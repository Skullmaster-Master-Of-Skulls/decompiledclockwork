using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200181B RID: 6171
	internal interface IScriptReferenceResolver
	{
		// Token: 0x0600F034 RID: 61492
		void ResolveScriptReference(ScriptReference script);

		// Token: 0x0600F035 RID: 61493
		Uri ResoveScriptUri(string resourceUri);
	}
}
