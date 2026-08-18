using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000016 RID: 22
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x06000082 RID: 130 RVA: 0x001D6A78 File Offset: 0x001D5E78
		internal void AddHandler(EventHandler handler)
		{
			RuntimeHelpers.PrepareDelegate(handler);
			this.Push(handler);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x001D6EFC File Offset: 0x001D62FC
		private ModuleUninitializer()
		{
			EventHandler value = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += value;
			AppDomain.CurrentDomain.ProcessExit += value;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x001D6A94 File Offset: 0x001D5E94
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

		// Token: 0x04000078 RID: 120
		private static object @lock = new object();

		// Token: 0x04000079 RID: 121
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
