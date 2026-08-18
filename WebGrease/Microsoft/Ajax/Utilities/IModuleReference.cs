using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000014 RID: 20
	public interface IModuleReference
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000158 RID: 344
		string ModuleName { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000159 RID: 345
		// (set) Token: 0x0600015A RID: 346
		ModuleScope ReferencedModule { get; set; }
	}
}
