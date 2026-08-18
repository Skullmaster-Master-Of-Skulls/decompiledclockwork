using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x020002A8 RID: 680
	internal class LazyEnumerator<T> : IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x06001807 RID: 6151 RVA: 0x00079676 File Offset: 0x00077876
		public LazyEnumerator(Func<ObjectResult<T>> getObjectResult)
		{
			this._getObjectResult = getObjectResult;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00079688 File Offset: 0x00077888
		public T Current
		{
			get
			{
				if (this._objectResultEnumerator != null)
				{
					return this._objectResultEnumerator.Current;
				}
				return default(T);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x000796B2 File Offset: 0x000778B2
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x000796BF File Offset: 0x000778BF
		public void Dispose()
		{
			if (this._objectResultEnumerator != null)
			{
				this._objectResultEnumerator.Dispose();
			}
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x000796D4 File Offset: 0x000778D4
		public bool MoveNext()
		{
			if (this._objectResultEnumerator == null)
			{
				ObjectResult<T> objectResult = this._getObjectResult();
				try
				{
					this._objectResultEnumerator = objectResult.GetEnumerator();
				}
				catch
				{
					objectResult.Dispose();
					throw;
				}
			}
			return this._objectResultEnumerator.MoveNext();
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00079728 File Offset: 0x00077928
		public void Reset()
		{
			if (this._objectResultEnumerator != null)
			{
				this._objectResultEnumerator.Reset();
			}
		}

		// Token: 0x0400086A RID: 2154
		private readonly Func<ObjectResult<T>> _getObjectResult;

		// Token: 0x0400086B RID: 2155
		private IEnumerator<T> _objectResultEnumerator;
	}
}
