using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000046 RID: 70
	public struct EventDefinitionHandleCollection : IReadOnlyCollection<EventDefinitionHandle>, IEnumerable<EventDefinitionHandle>, IEnumerable
	{
		// Token: 0x0600032B RID: 811 RVA: 0x00008A2D File Offset: 0x00006C2D
		internal EventDefinitionHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.EventTable.NumberOfRows;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00008A4E File Offset: 0x00006C4E
		internal EventDefinitionHandleCollection(MetadataReader reader, TypeDefinitionHandle containingType)
		{
			this._reader = reader;
			reader.GetEventRange(containingType, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00008A6A File Offset: 0x00006C6A
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00008A7B File Offset: 0x00006C7B
		public EventDefinitionHandleCollection.Enumerator GetEnumerator()
		{
			return new EventDefinitionHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008A94 File Offset: 0x00006C94
		IEnumerator<EventDefinitionHandle> IEnumerable<EventDefinitionHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008A94 File Offset: 0x00006C94
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002B0 RID: 688
		private readonly MetadataReader _reader;

		// Token: 0x040002B1 RID: 689
		private readonly int _firstRowId;

		// Token: 0x040002B2 RID: 690
		private readonly int _lastRowId;

		// Token: 0x02000179 RID: 377
		public struct Enumerator : IEnumerator<EventDefinitionHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000B9C RID: 2972 RVA: 0x00021131 File Offset: 0x0001F331
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0002114A File Offset: 0x0001F34A
			public EventDefinitionHandle Current
			{
				get
				{
					if (this._reader.UseEventPtrTable)
					{
						return this.GetCurrentEventIndirect();
					}
					return EventDefinitionHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000B9E RID: 2974 RVA: 0x00021174 File Offset: 0x0001F374
			private EventDefinitionHandle GetCurrentEventIndirect()
			{
				return this._reader.EventPtrTable.GetEventFor(this._currentRowId & 16777215);
			}

			// Token: 0x06000B9F RID: 2975 RVA: 0x00021192 File Offset: 0x0001F392
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

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x000211BE File Offset: 0x0001F3BE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BA1 RID: 2977 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BA2 RID: 2978 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400097A RID: 2426
			private readonly MetadataReader _reader;

			// Token: 0x0400097B RID: 2427
			private readonly int _lastRowId;

			// Token: 0x0400097C RID: 2428
			private int _currentRowId;

			// Token: 0x0400097D RID: 2429
			private const int EnumEnded = 16777216;
		}
	}
}
