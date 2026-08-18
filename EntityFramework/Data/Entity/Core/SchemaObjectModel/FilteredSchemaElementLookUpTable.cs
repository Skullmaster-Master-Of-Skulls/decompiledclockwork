using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000366 RID: 870
	internal sealed class FilteredSchemaElementLookUpTable<T, S> : IEnumerable<!0>, IEnumerable, ISchemaElementLookUpTable<T> where T : S where S : SchemaElement
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x000947E4 File Offset: 0x000929E4
		public FilteredSchemaElementLookUpTable(SchemaElementLookUpTable<S> lookUpTable)
		{
			this._lookUpTable = lookUpTable;
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000947F3 File Offset: 0x000929F3
		public IEnumerator<T> GetEnumerator()
		{
			return this._lookUpTable.GetFilteredEnumerator<T>();
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00094800 File Offset: 0x00092A00
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._lookUpTable.GetFilteredEnumerator<T>();
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x00094810 File Offset: 0x00092A10
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

		// Token: 0x06001F1E RID: 7966 RVA: 0x0009486C File Offset: 0x00092A6C
		public bool ContainsKey(string key)
		{
			return this._lookUpTable.ContainsKey(key) && this._lookUpTable[key] is T;
		}

		// Token: 0x170003B8 RID: 952
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
				throw new InvalidOperationException(Strings.UnexpectedTypeInCollection(s.GetType(), key));
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x00094903 File Offset: 0x00092B03
		public T LookUpEquivalentKey(string key)
		{
			return this._lookUpTable.LookUpEquivalentKey(key) as T;
		}

		// Token: 0x04000B29 RID: 2857
		private readonly SchemaElementLookUpTable<S> _lookUpTable;
	}
}
