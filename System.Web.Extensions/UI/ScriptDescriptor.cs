using System;

namespace System.Web.UI
{
	// Token: 0x02000072 RID: 114
	public abstract class ScriptDescriptor
	{
		// Token: 0x06000409 RID: 1033
		protected internal abstract string GetScript();

		// Token: 0x0600040A RID: 1034 RVA: 0x000032F4 File Offset: 0x000014F4
		internal virtual void RegisterDisposeForDescriptor(ScriptManager scriptManager, Control owner)
		{
		}
	}
}
