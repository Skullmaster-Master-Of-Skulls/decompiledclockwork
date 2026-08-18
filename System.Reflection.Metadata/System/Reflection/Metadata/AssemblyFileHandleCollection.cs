using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000052 RID: 82
	public struct AssemblyFileHandleCollection : IReadOnlyCollection<AssemblyFileHandle>, IEnumerable<AssemblyFileHandle>, IEnumerable
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00008D3E File Offset: 0x00006F3E
		internal AssemblyFileHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00008D47 File Offset: 0x00006F47
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00008D4F File Offset: 0x00006F4F
		public AssemblyFileHandleCollection.Enumerator GetEnumerator()
		{
			return new AssemblyFileHandleCollection.Enumerator(this._lastRowId);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00008D5C File Offset: 0x00006F5C
		IEnumerator<AssemblyFileHandle> IEnumerable<AssemblyFileHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00008D5C File Offset: 0x00006F5C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002C6 RID: 710
		private readonly int _lastRowId;

		// Token: 0x02000183 RID: 387
		public struct Enumerator : IEnumerator<AssemblyFileHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BDA RID: 3034 RVA: 0x000215F6 File Offset: 0x0001F7F6
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00021606 File Offset: 0x0001F806
			public AssemblyFileHandle Current
			{
				get
				{
					return AssemblyFileHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BDC RID: 3036 RVA: 0x0002161C File Offset: 0x0001F81C
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

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00021648 File Offset: 0x0001F848
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BDE RID: 3038 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BDF RID: 3039 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400099C RID: 2460
			private readonly int _lastRowId;

			// Token: 0x0400099D RID: 2461
			private int _currentRowId;

			// Token: 0x0400099E RID: 2462
			private const int EnumEnded = 16777216;
		}
	}
}
