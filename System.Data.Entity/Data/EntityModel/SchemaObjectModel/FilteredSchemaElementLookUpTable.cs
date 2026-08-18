using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002ED RID: 749
	internal sealed class FilteredSchemaElementLookUpTable<T, S> : IEnumerable<!0>, IEnumerable, ISchemaElementLookUpTable<T> where T : S where S : SchemaElement
	{
		// Token: 0x06002CC9 RID: 11465 RVA: 0x000AA1F9 File Offset: 0x000A83F9
		public FilteredSchemaElementLookUpTable(SchemaElementLookUpTable<S> lookUpTable)
		{
			this._lookUpTable = lookUpTable;
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000AA208 File Offset: 0x000A8408
		public IEnumerator<T> GetEnumerator()
		{
			return this._lookUpTable.GetFilteredEnumerator<T>();
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000AA208 File Offset: 0x000A8408
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._lookUpTable.GetFilteredEnumerator<T>();
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x000AA218 File Offset: 0x000A8418
		public int Count
		{
			get
			{
				int num = 0;
				foreach (S s in this._lookUpTable)
				{
					SchemaElement schemaElement = s;
					if (schemaElement is T)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000AA274 File Offset: 0x000A8474
		public bool ContainsKey(string key)
		{
			return this._lookUpTable.ContainsKey(key) && this._lookUpTable[key] is T;
		}

		// Token: 0x170008A4 RID: 2212
		public T this[string key]
		{
			get
			{
				S s = this._lookUpTable[key];
				if (s == null)
				{
					return default(T);
				}
				T t = s as T;
				if (t != null)
				{
					return t;
				}
				throw EntityUtil.InvalidOperation(Strings.UnexpectedTypeInCollection(s.GetType(), key));
			}
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000AA309 File Offset: 0x000A8509
		public T LookUpEquivalentKey(string key)
		{
			return this._lookUpTable.LookUpEquivalentKey(key) as T;
		}

		// Token: 0x040013B5 RID: 5045
		private SchemaElementLookUpTable<S> _lookUpTable;
	}
}
