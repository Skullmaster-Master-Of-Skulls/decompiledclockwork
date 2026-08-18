using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000021 RID: 33
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00005780 File Offset: 0x00004B80
		[SecuritySafeCritical]
		internal void AddHandler(EventHandler handler)
		{
			RuntimeHelpers.PrepareDelegate(handler);
			this.Push(handler);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005CBC File Offset: 0x000050BC
		[SecuritySafeCritical]
		private ModuleUninitializer()
		{
			EventHandler value = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += value;
			AppDomain.CurrentDomain.ProcessExit += value;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000579C File Offset: 0x00004B9C
		[PrePrepareMethod]
		[SecurityCritical]
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

		// Token: 0x040000A4 RID: 164
		private static object @lock = new object();

		// Token: 0x040000A5 RID: 165
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
