using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000044 RID: 68
	public struct FieldDefinitionHandleCollection : IReadOnlyCollection<FieldDefinitionHandle>, IEnumerable<FieldDefinitionHandle>, IEnumerable
	{
		// Token: 0x0600031F RID: 799 RVA: 0x00008945 File Offset: 0x00006B45
		internal FieldDefinitionHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.FieldTable.NumberOfRows;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00008966 File Offset: 0x00006B66
		internal FieldDefinitionHandleCollection(MetadataReader reader, TypeDefinitionHandle containingType)
		{
			this._reader = reader;
			reader.GetFieldRange(containingType, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00008982 File Offset: 0x00006B82
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00008993 File Offset: 0x00006B93
		public FieldDefinitionHandleCollection.Enumerator GetEnumerator()
		{
			return new FieldDefinitionHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000089AC File Offset: 0x00006BAC
		IEnumerator<FieldDefinitionHandle> IEnumerable<FieldDefinitionHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000089AC File Offset: 0x00006BAC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002AA RID: 682
		private readonly MetadataReader _reader;

		// Token: 0x040002AB RID: 683
		private readonly int _firstRowId;

		// Token: 0x040002AC RID: 684
		private readonly int _lastRowId;

		// Token: 0x02000177 RID: 375
		public struct Enumerator : IEnumerator<FieldDefinitionHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000B8E RID: 2958 RVA: 0x00020FFD File Offset: 0x0001F1FD
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00021016 File Offset: 0x0001F216
			public FieldDefinitionHandle Current
			{
				get
				{
					if (this._reader.UseFieldPtrTable)
					{
						return this.GetCurrentFieldIndirect();
					}
					return FieldDefinitionHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000B90 RID: 2960 RVA: 0x00021040 File Offset: 0x0001F240
			private FieldDefinitionHandle GetCurrentFieldIndirect()
			{
				return this._reader.FieldPtrTable.GetFieldFor(this._currentRowId & 16777215);
			}

			// Token: 0x06000B91 RID: 2961 RVA: 0x0002105E File Offset: 0x0001F25E
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

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0002108A File Offset: 0x0001F28A
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000B93 RID: 2963 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000B94 RID: 2964 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000972 RID: 2418
			private readonly MetadataReader _reader;

			// Token: 0x04000973 RID: 2419
			private readonly int _lastRowId;

			// Token: 0x04000974 RID: 2420
			private int _currentRowId;

			// Token: 0x04000975 RID: 2421
			private const int EnumEnded = 16777216;
		}
	}
}
