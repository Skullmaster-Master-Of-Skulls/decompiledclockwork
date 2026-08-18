using System;
using System.Runtime.ConstrainedExecution;

namespace System.Reflection.Internal
{
	// Token: 0x02000083 RID: 131
	internal abstract class CriticalDisposableObject : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x06000338 RID: 824
		protected abstract void Release();

		// Token: 0x06000339 RID: 825 RVA: 0x00008130 File Offset: 0x00006330
		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00008140 File Offset: 0x00006340
		~CriticalDisposableObject()
		{
			this.Release();
		}
	}
}
