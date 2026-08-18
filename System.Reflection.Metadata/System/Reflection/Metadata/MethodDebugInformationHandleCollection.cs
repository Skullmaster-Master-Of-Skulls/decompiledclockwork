using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008E RID: 142
	public struct MethodDebugInformationHandleCollection : IReadOnlyCollection<MethodDebugInformationHandle>, IEnumerable<MethodDebugInformationHandle>, IEnumerable
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x0000EF15 File Offset: 0x0000D115
		internal MethodDebugInformationHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.MethodDebugInformationTable.NumberOfRows;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0000EF36 File Offset: 0x0000D136
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0000EF47 File Offset: 0x0000D147
		public MethodDebugInformationHandleCollection.Enumerator GetEnumerator()
		{
			return new MethodDebugInformationHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000EF60 File Offset: 0x0000D160
		IEnumerator<MethodDebugInformationHandle> IEnumerable<MethodDebugInformationHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000EF60 File Offset: 0x0000D160
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040003D5 RID: 981
		private readonly MetadataReader _reader;

		// Token: 0x040003D6 RID: 982
		private readonly int _firstRowId;

		// Token: 0x040003D7 RID: 983
		private readonly int _lastRowId;

		// Token: 0x0200018A RID: 394
		public struct Enumerator : IEnumerator<MethodDebugInformationHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BEA RID: 3050 RVA: 0x00021707 File Offset: 0x0001F907
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00021720 File Offset: 0x0001F920
			public MethodDebugInformationHandle Current
			{
				get
				{
					return MethodDebugInformationHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BEC RID: 3052 RVA: 0x00021736 File Offset: 0x0001F936
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

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06000BED RID: 3053 RVA: 0x00021762 File Offset: 0x0001F962
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BEE RID: 3054 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BEF RID: 3055 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A03 RID: 2563
			private readonly MetadataReader _reader;

			// Token: 0x04000A04 RID: 2564
			private readonly int _lastRowId;

			// Token: 0x04000A05 RID: 2565
			private int _currentRowId;

			// Token: 0x04000A06 RID: 2566
			private const int EnumEnded = 16777216;
		}
	}
}
