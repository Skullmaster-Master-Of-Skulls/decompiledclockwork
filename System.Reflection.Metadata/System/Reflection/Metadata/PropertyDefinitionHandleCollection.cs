using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000045 RID: 69
	public struct PropertyDefinitionHandleCollection : IReadOnlyCollection<PropertyDefinitionHandle>, IEnumerable<PropertyDefinitionHandle>, IEnumerable
	{
		// Token: 0x06000325 RID: 805 RVA: 0x000089B9 File Offset: 0x00006BB9
		internal PropertyDefinitionHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.PropertyTable.NumberOfRows;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000089DA File Offset: 0x00006BDA
		internal PropertyDefinitionHandleCollection(MetadataReader reader, TypeDefinitionHandle containingType)
		{
			this._reader = reader;
			reader.GetPropertyRange(containingType, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000089F6 File Offset: 0x00006BF6
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00008A07 File Offset: 0x00006C07
		public PropertyDefinitionHandleCollection.Enumerator GetEnumerator()
		{
			return new PropertyDefinitionHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008A20 File Offset: 0x00006C20
		IEnumerator<PropertyDefinitionHandle> IEnumerable<PropertyDefinitionHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00008A20 File Offset: 0x00006C20
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002AD RID: 685
		private readonly MetadataReader _reader;

		// Token: 0x040002AE RID: 686
		private readonly int _firstRowId;

		// Token: 0x040002AF RID: 687
		private readonly int _lastRowId;

		// Token: 0x02000178 RID: 376
		public struct Enumerator : IEnumerator<PropertyDefinitionHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000B95 RID: 2965 RVA: 0x00021097 File Offset: 0x0001F297
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06000B96 RID: 2966 RVA: 0x000210B0 File Offset: 0x0001F2B0
			public PropertyDefinitionHandle Current
			{
				get
				{
					if (this._reader.UsePropertyPtrTable)
					{
						return this.GetCurrentPropertyIndirect();
					}
					return PropertyDefinitionHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x000210DA File Offset: 0x0001F2DA
			private PropertyDefinitionHandle GetCurrentPropertyIndirect()
			{
				return this._reader.PropertyPtrTable.GetPropertyFor(this._currentRowId & 16777215);
			}

			// Token: 0x06000B98 RID: 2968 RVA: 0x000210F8 File Offset: 0x0001F2F8
			public bool MoveNext()
			{
				if (this._currentRowId >= this._lastRowId)
				{
					this._currentRowId = 16777216;
					return false;
				}
				this._currentRowId++;
				return true;
			}

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00021124 File Offset: 0x0001F324
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000B9A RID: 2970 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000B9B RID: 2971 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000976 RID: 2422
			private readonly MetadataReader _reader;

			// Token: 0x04000977 RID: 2423
			private readonly int _lastRowId;

			// Token: 0x04000978 RID: 2424
			private int _currentRowId;

			// Token: 0x04000979 RID: 2425
			private const int EnumEnded = 16777216;
		}
	}
}
