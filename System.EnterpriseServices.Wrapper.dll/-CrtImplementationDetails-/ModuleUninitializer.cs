using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace <CrtImplementationDetails>
{
	// Token: 0x020000AE RID: 174
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00006DA8 File Offset: 0x000061A8
		internal void AddHandler(EventHandler handler)
		{
			RuntimeHelpers.PrepareDelegate(handler);
			this.Push(handler);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007324 File Offset: 0x00006724
		private ModuleUninitializer()
		{
			EventHandler value = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += value;
			AppDomain.CurrentDomain.ProcessExit += value;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006DC4 File Offset: 0x000061C4
		[PrePrepareMethod]
		private void SingletonDomainUnload(object source, EventArgs arguments)
		{
			using (IEnumerator enumerator = this.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					((EventHandler)enumerator.Current)(source, arguments);
				}
			}
		}

		// Token: 0x0400016F RID: 367
		private static object @lock = new object();

		// Token: 0x04000170 RID: 368
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
