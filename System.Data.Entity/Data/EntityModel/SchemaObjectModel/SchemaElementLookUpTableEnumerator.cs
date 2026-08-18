using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200030D RID: 781
	internal sealed class SchemaElementLookUpTableEnumerator<T, S> : IEnumerator<!0>, IDisposable, IEnumerator where T : S where S : SchemaElement
	{
		// Token: 0x06002E74 RID: 11892 RVA: 0x000AF9ED File Offset: 0x000ADBED
		public SchemaElementLookUpTableEnumerator(Dictionary<string, S> data, List<string> keysInOrder)
		{
			this._data = data;
			this._enumerator = keysInOrder.GetEnumerator();
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000AFA08 File Offset: 0x000ADC08
		public void Reset()
		{
			((IEnumerator)this._enumerator).Reset();
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000AFA1C File Offset: 0x000ADC1C
		public T Current
		{
			get
			{
				string key = this._enumerator.Current;
				return this._data[key] as T;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x000AFA50 File Offset: 0x000ADC50
		object IEnumerator.Current
		{
			get
			{
				string key = this._enumerator.Current;
				return this._data[key] as T;
			}
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000AFA89 File Offset: 0x000ADC89
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

		// Token: 0x06002E79 RID: 11897 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void Dispose()
		{
		}

		// Token: 0x04001424 RID: 5156
		private Dictionary<string, S> _data;

		// Token: 0x04001425 RID: 5157
		private List<string>.Enumerator _enumerator;
	}
}
