using System;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace Telerik.Web
{
	// Token: 0x020001CA RID: 458
	internal interface IAssemblyWhiteListLoader
	{
		// Token: 0x060010AA RID: 4266
		ICollection<AssemblyReference> LoadWhiteList();

		// Token: 0x060010AB RID: 4267
		void VerifyEntry(ScriptEntry entry);

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060010AC RID: 4268
		bool WhiteListEnabled { get; }
	}
}
