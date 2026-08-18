using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003D1 RID: 977
	internal sealed class Shaper<T> : Shaper
	{
		// Token: 0x060034C3 RID: 13507 RVA: 0x000CBBF0 File Offset: 0x000C9DF0
		internal Shaper(DbDataReader reader, ObjectContext context, MetadataWorkspace workspace, MergeOption mergeOption, int stateCount, CoordinatorFactory<T> rootCoordinatorFactory, Action checkPermissions, bool readerOwned) : base(reader, context, workspace, mergeOption, stateCount)
		{
			this.RootCoordinator = new Coordinator<T>(rootCoordinatorFactory, null, null);
			if (checkPermissions != null)
			{
				checkPermissions();
			}
			this.IsObjectQuery = !(typeof(T) == typeof(RecordState));
			this._isActive = true;
			this.RootCoordinator.Initialize(this);
			this._readerOwned = readerOwned;
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060034C4 RID: 13508 RVA: 0x000CBC64 File Offset: 0x000C9E64
		// (remove) Token: 0x060034C5 RID: 13509 RVA: 0x000CBC9C File Offset: 0x000C9E9C
		internal event EventHandler OnDone;

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x000CBCD1 File Offset: 0x000C9ED1
		// (set) Token: 0x060034C7 RID: 13511 RVA: 0x000CBCD9 File Offset: 0x000C9ED9
		internal bool DataWaiting
		{
			get
			{
				return this._dataWaiting;
			}
			set
			{
				this._dataWaiting = value;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x000CBCE2 File Offset: 0x000C9EE2
		internal IEnumerator<T> RootEnumerator
		{
			get
			{
				if (this._rootEnumerator == null)
				{
					this.InitializeRecordStates(this.RootCoordinator.CoordinatorFactory);
					this._rootEnumerator = this.GetEnumerator();
				}
				return this._rootEnumerator;
			}
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000CBD10 File Offset: 0x000C9F10
		private void InitializeRecordStates(CoordinatorFactory coordinatorFactory)
		{
			foreach (RecordStateFactory recordStateFactory in coordinatorFactory.RecordStateFactories)
			{
				this.State[recordStateFactory.StateSlotNumber] = recordStateFactory.Create(coordinatorFactory);
			}
			foreach (CoordinatorFactory coordinatorFactory2 in coordinatorFactory.NestedCoordinators)
			{
				this.InitializeRecordStates(coordinatorFactory2);
			}
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000CBDA8 File Offset: 0x000C9FA8
		public IEnumerator<T> GetEnumerator()
		{
			if (this.RootCoordinator.CoordinatorFactory.IsSimple)
			{
				return new Shaper<T>.SimpleEnumerator(this);
			}
			Shaper<T>.RowNestedResultEnumerator rowEnumerator = new Shaper<T>.RowNestedResultEnumerator(this);
			if (this.IsObjectQuery)
			{
				return new Shaper<T>.ObjectQueryNestedEnumerator(rowEnumerator);
			}
			return (IEnumerator<T>)new Shaper<T>.RecordStateEnumerator(rowEnumerator);
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000CBDF0 File Offset: 0x000C9FF0
		private void Finally()
		{
			if (this._isActive)
			{
				this._isActive = false;
				if (this._readerOwned)
				{
					if (this.IsObjectQuery)
					{
						this.Reader.Dispose();
					}
					if (this.Context != null)
					{
						this.Context.ReleaseConnection();
					}
				}
				if (this.OnDone != null)
				{
					this.OnDone(this, new EventArgs());
				}
			}
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000CBE54 File Offset: 0x000CA054
		private bool StoreRead()
		{
			bool result;
			try
			{
				result = this.Reader.Read();
			}
			catch (Exception ex)
			{
				if (this.Reader.IsClosed)
				{
					throw EntityUtil.DataReaderClosed("Read");
				}
				if (EntityUtil.IsCatchableEntityExceptionType(ex))
				{
					throw EntityUtil.CommandExecution(Strings.EntityClient_StoreReaderFailed, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000CBEB0 File Offset: 0x000CA0B0
		private void StartMaterializingElement()
		{
			if (this.Context != null)
			{
				this.Context.InMaterialization = true;
				base.InitializeForOnMaterialize();
			}
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000CBECC File Offset: 0x000CA0CC
		private void StopMaterializingElement()
		{
			if (this.Context != null)
			{
				this.Context.InMaterialization = false;
				base.RaiseMaterializedEvents();
			}
		}

		// Token: 0x04001719 RID: 5913
		internal readonly Coordinator<T> RootCoordinator;

		// Token: 0x0400171A RID: 5914
		private readonly bool IsObjectQuery;

		// Token: 0x0400171B RID: 5915
		private bool _isActive;

		// Token: 0x0400171C RID: 5916
		private IEnumerator<T> _rootEnumerator;

		// Token: 0x0400171D RID: 5917
		private bool _dataWaiting;

		// Token: 0x0400171E RID: 5918
		private bool _readerOwned;

		// Token: 0x0200069A RID: 1690
		private class SimpleEnumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06004559 RID: 17753 RVA: 0x000F9AE1 File Offset: 0x000F7CE1
			internal SimpleEnumerator(Shaper<T> shaper)
			{
				this._shaper = shaper;
			}

			// Token: 0x17000BC2 RID: 3010
			// (get) Token: 0x0600455A RID: 17754 RVA: 0x000F9AF0 File Offset: 0x000F7CF0
			public T Current
			{
				get
				{
					return this._shaper.RootCoordinator.Current;
				}
			}

			// Token: 0x17000BC3 RID: 3011
			// (get) Token: 0x0600455B RID: 17755 RVA: 0x000F9B02 File Offset: 0x000F7D02
			object IEnumerator.Current
			{
				get
				{
					return this._shaper.RootCoordinator.Current;
				}
			}

			// Token: 0x0600455C RID: 17756 RVA: 0x000F9B19 File Offset: 0x000F7D19
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._shaper.RootCoordinator.SetCurrentToDefault();
				this._shaper.Finally();
			}

			// Token: 0x0600455D RID: 17757 RVA: 0x000F9B3C File Offset: 0x000F7D3C
			public bool MoveNext()
			{
				if (!this._shaper._isActive)
				{
					return false;
				}
				if (this._shaper.StoreRead())
				{
					try
					{
						this._shaper.StartMaterializingElement();
						this._shaper.RootCoordinator.ReadNextElement(this._shaper);
					}
					finally
					{
						this._shaper.StopMaterializingElement();
					}
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600455E RID: 17758 RVA: 0x00013A81 File Offset: 0x00011C81
			public void Reset()
			{
				throw EntityUtil.NotSupported();
			}

			// Token: 0x04002005 RID: 8197
			private readonly Shaper<T> _shaper;
		}

		// Token: 0x0200069B RID: 1691
		private class RowNestedResultEnumerator : IEnumerator<Coordinator[]>, IDisposable, IEnumerator
		{
			// Token: 0x0600455F RID: 17759 RVA: 0x000F9BB0 File Offset: 0x000F7DB0
			internal RowNestedResultEnumerator(Shaper<T> shaper)
			{
				this._shaper = shaper;
				this._current = new Coordinator[this._shaper.RootCoordinator.MaxDistanceToLeaf() + 1];
			}

			// Token: 0x17000BC4 RID: 3012
			// (get) Token: 0x06004560 RID: 17760 RVA: 0x000F9BDC File Offset: 0x000F7DDC
			public Coordinator[] Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06004561 RID: 17761 RVA: 0x000F9BE4 File Offset: 0x000F7DE4
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._shaper.Finally();
			}

			// Token: 0x17000BC5 RID: 3013
			// (get) Token: 0x06004562 RID: 17762 RVA: 0x000F9BDC File Offset: 0x000F7DDC
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06004563 RID: 17763 RVA: 0x000F9BF8 File Offset: 0x000F7DF8
			public bool MoveNext()
			{
				Coordinator coordinator = this._shaper.RootCoordinator;
				try
				{
					this._shaper.StartMaterializingElement();
					if (!this._shaper.StoreRead())
					{
						this.RootCoordinator.ResetCollection(this._shaper);
						return false;
					}
					int i = 0;
					bool flag = false;
					while (i < this._current.Length)
					{
						while (coordinator != null && !coordinator.CoordinatorFactory.HasData(this._shaper))
						{
							coordinator = coordinator.Next;
						}
						if (coordinator == null)
						{
							IL_D8:
							while (i < this._current.Length)
							{
								this._current[i] = null;
								i++;
							}
							return true;
						}
						if (coordinator.HasNextElement(this._shaper))
						{
							if (!flag && coordinator.Child != null)
							{
								coordinator.Child.ResetCollection(this._shaper);
							}
							flag = true;
							coordinator.ReadNextElement(this._shaper);
							this._current[i] = coordinator;
						}
						else
						{
							this._current[i] = null;
						}
						coordinator = coordinator.Child;
						i++;
					}
					goto IL_D8;
				}
				finally
				{
					this._shaper.StopMaterializingElement();
				}
				return true;
			}

			// Token: 0x06004564 RID: 17764 RVA: 0x00013A81 File Offset: 0x00011C81
			public void Reset()
			{
				throw EntityUtil.NotSupported();
			}

			// Token: 0x17000BC6 RID: 3014
			// (get) Token: 0x06004565 RID: 17765 RVA: 0x000F9D0C File Offset: 0x000F7F0C
			internal Coordinator<T> RootCoordinator
			{
				get
				{
					return this._shaper.RootCoordinator;
				}
			}

			// Token: 0x04002006 RID: 8198
			private readonly Shaper<T> _shaper;

			// Token: 0x04002007 RID: 8199
			private readonly Coordinator[] _current;
		}

		// Token: 0x0200069C RID: 1692
		private class ObjectQueryNestedEnumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06004566 RID: 17766 RVA: 0x000F9D19 File Offset: 0x000F7F19
			internal ObjectQueryNestedEnumerator(Shaper<T>.RowNestedResultEnumerator rowEnumerator)
			{
				this._rowEnumerator = rowEnumerator;
				this._previousElement = default(T);
				this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.Start;
			}

			// Token: 0x17000BC7 RID: 3015
			// (get) Token: 0x06004567 RID: 17767 RVA: 0x000F9D3B File Offset: 0x000F7F3B
			public T Current
			{
				get
				{
					return this._previousElement;
				}
			}

			// Token: 0x06004568 RID: 17768 RVA: 0x000F9D43 File Offset: 0x000F7F43
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._rowEnumerator.Dispose();
			}

			// Token: 0x17000BC8 RID: 3016
			// (get) Token: 0x06004569 RID: 17769 RVA: 0x000F9D56 File Offset: 0x000F7F56
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600456A RID: 17770 RVA: 0x000F9D64 File Offset: 0x000F7F64
			public bool MoveNext()
			{
				switch (this._state)
				{
				case Shaper<T>.ObjectQueryNestedEnumerator.State.Start:
					if (this.TryReadToNextElement())
					{
						this.ReadElement();
					}
					else
					{
						this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRows;
					}
					break;
				case Shaper<T>.ObjectQueryNestedEnumerator.State.Reading:
					this.ReadElement();
					break;
				case Shaper<T>.ObjectQueryNestedEnumerator.State.NoRowsLastElementPending:
					this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRows;
					break;
				}
				bool result;
				if (this._state == Shaper<T>.ObjectQueryNestedEnumerator.State.NoRows)
				{
					this._previousElement = default(T);
					result = false;
				}
				else
				{
					result = true;
				}
				return result;
			}

			// Token: 0x0600456B RID: 17771 RVA: 0x000F9DD0 File Offset: 0x000F7FD0
			private void ReadElement()
			{
				this._previousElement = this._rowEnumerator.RootCoordinator.Current;
				if (this.TryReadToNextElement())
				{
					this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.Reading;
					return;
				}
				this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRowsLastElementPending;
			}

			// Token: 0x0600456C RID: 17772 RVA: 0x000F9DFF File Offset: 0x000F7FFF
			private bool TryReadToNextElement()
			{
				while (this._rowEnumerator.MoveNext())
				{
					if (this._rowEnumerator.Current[0] != null)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600456D RID: 17773 RVA: 0x000F9E22 File Offset: 0x000F8022
			public void Reset()
			{
				this._rowEnumerator.Reset();
			}

			// Token: 0x04002008 RID: 8200
			private readonly Shaper<T>.RowNestedResultEnumerator _rowEnumerator;

			// Token: 0x04002009 RID: 8201
			private T _previousElement;

			// Token: 0x0400200A RID: 8202
			private Shaper<T>.ObjectQueryNestedEnumerator.State _state;

			// Token: 0x02000781 RID: 1921
			private enum State
			{
				// Token: 0x040021B0 RID: 8624
				Start,
				// Token: 0x040021B1 RID: 8625
				Reading,
				// Token: 0x040021B2 RID: 8626
				NoRowsLastElementPending,
				// Token: 0x040021B3 RID: 8627
				NoRows
			}
		}

		// Token: 0x0200069D RID: 1693
		private class RecordStateEnumerator : IEnumerator<RecordState>, IDisposable, IEnumerator
		{
			// Token: 0x0600456E RID: 17774 RVA: 0x000F9E2F File Offset: 0x000F802F
			internal RecordStateEnumerator(Shaper<T>.RowNestedResultEnumerator rowEnumerator)
			{
				this._rowEnumerator = rowEnumerator;
				this._current = null;
				this._depth = -1;
				this._readerConsumed = false;
			}

			// Token: 0x17000BC9 RID: 3017
			// (get) Token: 0x0600456F RID: 17775 RVA: 0x000F9E53 File Offset: 0x000F8053
			public RecordState Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06004570 RID: 17776 RVA: 0x000F9E5B File Offset: 0x000F805B
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._rowEnumerator.Dispose();
			}

			// Token: 0x17000BCA RID: 3018
			// (get) Token: 0x06004571 RID: 17777 RVA: 0x000F9E53 File Offset: 0x000F8053
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06004572 RID: 17778 RVA: 0x000F9E70 File Offset: 0x000F8070
			public bool MoveNext()
			{
				if (!this._readerConsumed)
				{
					Coordinator coordinator;
					for (;;)
					{
						if (-1 == this._depth || this._rowEnumerator.Current.Length == this._depth)
						{
							if (!this._rowEnumerator.MoveNext())
							{
								break;
							}
							this._depth = 0;
						}
						coordinator = this._rowEnumerator.Current[this._depth];
						if (coordinator != null)
						{
							goto Block_3;
						}
						this._depth++;
					}
					this._current = null;
					this._readerConsumed = true;
					goto IL_97;
					Block_3:
					this._current = ((Coordinator<RecordState>)coordinator).Current;
					this._depth++;
				}
				IL_97:
				return !this._readerConsumed;
			}

			// Token: 0x06004573 RID: 17779 RVA: 0x000F9F1D File Offset: 0x000F811D
			public void Reset()
			{
				this._rowEnumerator.Reset();
			}

			// Token: 0x0400200B RID: 8203
			private readonly Shaper<T>.RowNestedResultEnumerator _rowEnumerator;

			// Token: 0x0400200C RID: 8204
			private RecordState _current;

			// Token: 0x0400200D RID: 8205
			private int _depth;

			// Token: 0x0400200E RID: 8206
			private bool _readerConsumed;
		}
	}
}
