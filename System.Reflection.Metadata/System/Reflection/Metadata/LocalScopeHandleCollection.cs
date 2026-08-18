using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008F RID: 143
	public struct LocalScopeHandleCollection : IReadOnlyCollection<LocalScopeHandle>, IEnumerable<LocalScopeHandle>, IEnumerable
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x0000EF6D File Offset: 0x0000D16D
		internal LocalScopeHandleCollection(MetadataReader reader, int methodDefinitionRowId)
		{
			this._reader = reader;
			if (methodDefinitionRowId == 0)
			{
				this._firstRowId = 1;
				this._lastRowId = reader.LocalScopeTable.NumberOfRows;
				return;
			}
			reader.LocalScopeTable.GetLocalScopeRange(methodDefinitionRowId, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0000EFAA File Offset: 0x0000D1AA
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000EFBB File Offset: 0x0000D1BB
		public LocalScopeHandleCollection.Enumerator GetEnumerator()
		{
			return new LocalScopeHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		IEnumerator<LocalScopeHandle> IEnumerable<LocalScopeHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040003D8 RID: 984
		private readonly MetadataReader _reader;

		// Token: 0x040003D9 RID: 985
		private readonly int _firstRowId;

		// Token: 0x040003DA RID: 986
		private readonly int _lastRowId;

		// Token: 0x0200018B RID: 395
		public struct Enumerator : IEnumerator<LocalScopeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BF0 RID: 3056 RVA: 0x0002176F File Offset: 0x0001F96F
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00021788 File Offset: 0x0001F988
			public LocalScopeHandle Current
			{
				get
				{
					return LocalScopeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BF2 RID: 3058 RVA: 0x0002179E File Offset: 0x0001F99E
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

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x000217CA File Offset: 0x0001F9CA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BF4 RID: 3060 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BF5 RID: 3061 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A07 RID: 2567
			private readonly MetadataReader _reader;

			// Token: 0x04000A08 RID: 2568
			private readonly int _lastRowId;

			// Token: 0x04000A09 RID: 2569
			private int _currentRowId;

			// Token: 0x04000A0A RID: 2570
			private const int EnumEnded = 16777216;
		}

		// Token: 0x0200018C RID: 396
		public struct ChildrenEnumerator : IEnumerator<LocalScopeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BF6 RID: 3062 RVA: 0x000217D7 File Offset: 0x0001F9D7
			internal ChildrenEnumerator(MetadataReader reader, int parentRowId)
			{
				this._reader = reader;
				this._parentEndOffset = reader.LocalScopeTable.GetEndOffset(parentRowId);
				this._parentMethodRowId = reader.LocalScopeTable.GetMethod(parentRowId);
				this._currentRowId = 0;
				this._parentRowId = parentRowId;
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00021812 File Offset: 0x0001FA12
			public LocalScopeHandle Current
			{
				get
				{
					return LocalScopeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BF8 RID: 3064 RVA: 0x00021828 File Offset: 0x0001FA28
			public bool MoveNext()
			{
				int currentRowId = this._currentRowId;
				if (currentRowId == 16777216)
				{
					return false;
				}
				int num;
				int num2;
				if (currentRowId == 0)
				{
					num = -1;
					num2 = this._parentRowId + 1;
				}
				else
				{
					num = this._reader.LocalScopeTable.GetEndOffset(currentRowId);
					num2 = currentRowId + 1;
				}
				int numberOfRows = this._reader.LocalScopeTable.NumberOfRows;
				while (num2 <= numberOfRows && !(this._parentMethodRowId != this._reader.LocalScopeTable.GetMethod(num2)))
				{
					int endOffset = this._reader.LocalScopeTable.GetEndOffset(num2);
					if (endOffset > num)
					{
						if (endOffset > this._parentEndOffset)
						{
							this._currentRowId = 16777216;
							return false;
						}
						this._currentRowId = num2;
						return true;
					}
					else
					{
						num2++;
					}
				}
				this._currentRowId = 16777216;
				return false;
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x000218E9 File Offset: 0x0001FAE9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BFA RID: 3066 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BFB RID: 3067 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A0B RID: 2571
			private readonly MetadataReader _reader;

			// Token: 0x04000A0C RID: 2572
			private readonly int _parentEndOffset;

			// Token: 0x04000A0D RID: 2573
			private readonly int _parentRowId;

			// Token: 0x04000A0E RID: 2574
			private readonly MethodDefinitionHandle _parentMethodRowId;

			// Token: 0x04000A0F RID: 2575
			private int _currentRowId;

			// Token: 0x04000A10 RID: 2576
			private const int EnumEnded = 16777216;
		}
	}
}
