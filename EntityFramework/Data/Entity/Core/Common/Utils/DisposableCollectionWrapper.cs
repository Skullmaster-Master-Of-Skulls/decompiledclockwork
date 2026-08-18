using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000326 RID: 806
	internal class DisposableCollectionWrapper<T> : IDisposable, IEnumerable<!0>, IEnumerable where T : IDisposable
	{
		// Token: 0x06001BC8 RID: 7112 RVA: 0x0008899C File Offset: 0x00086B9C
		internal DisposableCollectionWrapper(IEnumerable<T> enumerable)
		{
			this._enumerable = enumerable;
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x000889AC File Offset: 0x00086BAC
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			if (this._enumerable != null)
			{
				foreach (T t in this._enumerable)
				{
					if (t != null)
					{
						t.Dispose();
					}
				}
			}
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00088A18 File Offset: 0x00086C18
		public IEnumerator<T> GetEnumerator()
		{
			return this._enumerable.GetEnumerator();
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00088A25 File Offset: 0x00086C25
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._enumerable.GetEnumerator();
		}

		// Token: 0x040009B9 RID: 2489
		private readonly IEnumerable<T> _enumerable;
	}
}
