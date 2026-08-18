using System;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200068A RID: 1674
	internal sealed class SharedReference
	{
		// Token: 0x06003DEB RID: 15851 RVA: 0x000FDA24 File Offset: 0x000FBC24
		internal object Get()
		{
			if (Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				object target = this._ref.Target;
				this._locked = 0;
				return target;
			}
			return null;
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x000FDA55 File Offset: 0x000FBC55
		internal void Cache(object obj)
		{
			if (Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				this._ref.Target = obj;
				this._locked = 0;
			}
		}

		// Token: 0x04002CF6 RID: 11510
		private WeakReference _ref = new WeakReference(null);

		// Token: 0x04002CF7 RID: 11511
		private int _locked;
	}
}
