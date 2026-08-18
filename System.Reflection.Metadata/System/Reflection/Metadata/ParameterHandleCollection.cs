using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000048 RID: 72
	public struct ParameterHandleCollection : IReadOnlyCollection<ParameterHandle>, IEnumerable<ParameterHandle>, IEnumerable
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00008B0E File Offset: 0x00006D0E
		internal ParameterHandleCollection(MetadataReader reader, MethodDefinitionHandle containingMethod)
		{
			this._reader = reader;
			reader.GetParameterRange(containingMethod, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00008B2A File Offset: 0x00006D2A
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00008B3B File Offset: 0x00006D3B
		public ParameterHandleCollection.Enumerator GetEnumerator()
		{
			return new ParameterHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00008B54 File Offset: 0x00006D54
		IEnumerator<ParameterHandle> IEnumerable<ParameterHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00008B54 File Offset: 0x00006D54
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002B5 RID: 693
		private readonly MetadataReader _reader;

		// Token: 0x040002B6 RID: 694
		private readonly int _firstRowId;

		// Token: 0x040002B7 RID: 695
		private readonly int _lastRowId;

		// Token: 0x0200017B RID: 379
		public struct Enumerator : IEnumerator<ParameterHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BA9 RID: 2985 RVA: 0x0002122C File Offset: 0x0001F42C
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			// Token: 0x170002D7 RID: 727
			// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00021245 File Offset: 0x0001F445
			public ParameterHandle Current
			{
				get
				{
					if (this._reader.UseParamPtrTable)
					{
						return this.GetCurrentParameterIndirect();
					}
					return ParameterHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BAB RID: 2987 RVA: 0x0002126F File Offset: 0x0001F46F
			private ParameterHandle GetCurrentParameterIndirect()
			{
				return this._reader.ParamPtrTable.GetParamFor(this._currentRowId & 16777215);
			}

			// Token: 0x06000BAC RID: 2988 RVA: 0x0002128D File Offset: 0x0001F48D
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

			// Token: 0x170002D8 RID: 728
			// (get) Token: 0x06000BAD RID: 2989 RVA: 0x000212B9 File Offset: 0x0001F4B9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BAE RID: 2990 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BAF RID: 2991 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000981 RID: 2433
			private readonly MetadataReader _reader;

			// Token: 0x04000982 RID: 2434
			private readonly int _lastRowId;

			// Token: 0x04000983 RID: 2435
			private int _currentRowId;

			// Token: 0x04000984 RID: 2436
			private const int EnumEnded = 16777216;
		}
	}
}
