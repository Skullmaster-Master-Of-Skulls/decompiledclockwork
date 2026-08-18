using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000040 RID: 64
	public struct GenericParameterConstraintHandleCollection : IReadOnlyList<GenericParameterConstraintHandle>, IReadOnlyCollection<GenericParameterConstraintHandle>, IEnumerable<GenericParameterConstraintHandle>, IEnumerable
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000877D File Offset: 0x0000697D
		internal GenericParameterConstraintHandleCollection(int firstRowId, ushort count)
		{
			this._firstRowId = firstRowId;
			this._count = count;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000878D File Offset: 0x0000698D
		public int Count
		{
			get
			{
				return (int)this._count;
			}
		}

		// Token: 0x1700014A RID: 330
		public GenericParameterConstraintHandle this[int index]
		{
			get
			{
				if (index < 0 || index >= (int)this._count)
				{
					Throw.IndexOutOfRange();
				}
				return GenericParameterConstraintHandle.FromRowId(this._firstRowId + index);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000087B6 File Offset: 0x000069B6
		public GenericParameterConstraintHandleCollection.Enumerator GetEnumerator()
		{
			return new GenericParameterConstraintHandleCollection.Enumerator(this._firstRowId, this._firstRowId + (int)this._count - 1);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000087D2 File Offset: 0x000069D2
		IEnumerator<GenericParameterConstraintHandle> IEnumerable<GenericParameterConstraintHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000087D2 File Offset: 0x000069D2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400029F RID: 671
		private readonly int _firstRowId;

		// Token: 0x040002A0 RID: 672
		private readonly ushort _count;

		// Token: 0x02000173 RID: 371
		public struct Enumerator : IEnumerator<GenericParameterConstraintHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000B74 RID: 2932 RVA: 0x00020DF3 File Offset: 0x0001EFF3
			internal Enumerator(int firstRowId, int lastRowId)
			{
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			// Token: 0x170002C7 RID: 711
			// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00020E05 File Offset: 0x0001F005
			public GenericParameterConstraintHandle Current
			{
				get
				{
					return GenericParameterConstraintHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x00020E1B File Offset: 0x0001F01B
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

			// Token: 0x170002C8 RID: 712
			// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00020E47 File Offset: 0x0001F047
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000B78 RID: 2936 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000B79 RID: 2937 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000963 RID: 2403
			private readonly int _lastRowId;

			// Token: 0x04000964 RID: 2404
			private int _currentRowId;

			// Token: 0x04000965 RID: 2405
			private const int EnumEnded = 16777216;
		}
	}
}
