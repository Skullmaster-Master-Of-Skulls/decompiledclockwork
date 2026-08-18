using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Common.Utils
{
	// Token: 0x0200039E RID: 926
	internal class DisposableCollectionWrapper<T> : IDisposable, IEnumerable<!0>, IEnumerable where T : IDisposable
	{
		// Token: 0x06003349 RID: 13129 RVA: 0x000C7AC3 File Offset: 0x000C5CC3
		internal DisposableCollectionWrapper(IEnumerable<T> enumerable)
		{
			this._enumerable = enumerable;
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000C7AD4 File Offset: 0x000C5CD4
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

		// Token: 0x0600334B RID: 13131 RVA: 0x000C7B40 File Offset: 0x000C5D40
		public IEnumerator<T> GetEnumerator()
		{
			return this._enumerable.GetEnumerator();
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000C7B4D File Offset: 0x000C5D4D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._enumerable.GetEnumerator();
		}

		// Token: 0x04001675 RID: 5749
		private IEnumerable<T> _enumerable;
	}
}
