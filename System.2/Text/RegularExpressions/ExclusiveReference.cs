using System;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000689 RID: 1673
	internal sealed class ExclusiveReference
	{
		// Token: 0x06003DE8 RID: 15848 RVA: 0x000FD97C File Offset: 0x000FBB7C
		internal object Get()
		{
			if (Interlocked.Exchange(ref this._locked, 1) != 0)
			{
				return null;
			}
			object @ref = this._ref;
			if (@ref == null)
			{
				this._locked = 0;
				return null;
			}
			this._obj = @ref;
			return @ref;
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x000FD9B4 File Offset: 0x000FBBB4
		internal void Release(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._obj == obj)
			{
				this._obj = null;
				this._locked = 0;
				return;
			}
			if (this._obj == null && Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				if (this._ref == null)
				{
					this._ref = (RegexRunner)obj;
				}
				this._locked = 0;
				return;
			}
		}

		// Token: 0x04002CF3 RID: 11507
		private RegexRunner _ref;

		// Token: 0x04002CF4 RID: 11508
		private object _obj;

		// Token: 0x04002CF5 RID: 11509
		private int _locked;
	}
}
