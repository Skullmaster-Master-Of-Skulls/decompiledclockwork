using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200038A RID: 906
	internal sealed class SchemaElementLookUpTableEnumerator<T, S> : IEnumerator<!0>, IDisposable, IEnumerator where T : S where S : SchemaElement
	{
		// Token: 0x060020CD RID: 8397 RVA: 0x0009A823 File Offset: 0x00098A23
		public SchemaElementLookUpTableEnumerator(Dictionary<string, S> data, List<string> keysInOrder)
		{
			this._data = data;
			this._enumerator = keysInOrder.GetEnumerator();
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0009A83E File Offset: 0x00098A3E
		public void Reset()
		{
			((IEnumerator)this._enumerator).Reset();
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x0009A850 File Offset: 0x00098A50
		public T Current
		{
			get
			{
				string key = this._enumerator.Current;
				return this._data[key] as T;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x0009A884 File Offset: 0x00098A84
		object IEnumerator.Current
		{
			get
			{
				string key = this._enumerator.Current;
				return this._data[key] as T;
			}
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0009A8BD File Offset: 0x00098ABD
		public bool MoveNext()
		{
			while (this._enumerator.MoveNext())
			{
				if (this.Current != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x0009A8DE File Offset: 0x00098ADE
		public void Dispose()
		{
		}

		// Token: 0x04000B9B RID: 2971
		private readonly Dictionary<string, S> _data;

		// Token: 0x04000B9C RID: 2972
		private List<string>.Enumerator _enumerator;
	}
}
