using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002E8 RID: 744
	internal class Shaper<T> : Shaper
	{
		// Token: 0x06001A3B RID: 6715 RVA: 0x00081F6C File Offset: 0x0008016C
		internal Shaper(DbDataReader reader, ObjectContext context, MetadataWorkspace workspace, MergeOption mergeOption, int stateCount, CoordinatorFactory<T> rootCoordinatorFactory, bool readerOwned, bool streaming) : base(reader, context, workspace, mergeOption, stateCount, streaming)
		{
			this.RootCoordinator = (Coordinator<T>)rootCoordinatorFactory.CreateCoordinator(null, null);
			this._isObjectQuery = !(typeof(T) == typeof(RecordState));
			this._isActive = true;
			this.RootCoordinator.Initialize(this);
			this._readerOwned = readerOwned;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06001A3C RID: 6716 RVA: 0x00081FDC File Offset: 0x000801DC
		// (remove) Token: 0x06001A3D RID: 6717 RVA: 0x00082014 File Offset: 0x00080214
		internal event EventHandler OnDone;

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00082049 File Offset: 0x00080249
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x00082051 File Offset: 0x00080251
		internal bool DataWaiting { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0008205A File Offset: 0x0008025A
		internal IDbEnumerator<T> RootEnumerator
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

		// Token: 0x06001A41 RID: 6721 RVA: 0x00082088 File Offset: 0x00080288
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

		// Token: 0x06001A42 RID: 6722 RVA: 0x00082120 File Offset: 0x00080320
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public virtual IDbEnumerator<T> GetEnumerator()
		{
			if (this.RootCoordinator.CoordinatorFactory.IsSimple)
			{
				return new Shaper<T>.SimpleEnumerator(this);
			}
			Shaper<T>.RowNestedResultEnumerator rowEnumerator = new Shaper<T>.RowNestedResultEnumerator(this);
			if (this._isObjectQuery)
			{
				return new Shaper<T>.ObjectQueryNestedEnumerator(rowEnumerator);
			}
			return (IDbEnumerator<T>)new Shaper<T>.RecordStateEnumerator(rowEnumerator);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00082168 File Offset: 0x00080368
		private void Finally()
		{
			if (this._isActive)
			{
				this._isActive = false;
				if (this._readerOwned)
				{
					if (this._isObjectQuery)
					{
						this.Reader.Dispose();
					}
					if (this.Context != null && this.Streaming)
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

		// Token: 0x06001A44 RID: 6724 RVA: 0x000821D4 File Offset: 0x000803D4
		private bool StoreRead()
		{
			bool result;
			try
			{
				result = this.Reader.Read();
			}
			catch (Exception e)
			{
				this.HandleReaderException(e);
				throw;
			}
			return result;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00082330 File Offset: 0x00080530
		private async Task<bool> StoreReadAsync(CancellationToken cancellationToken)
		{
			bool readSucceeded;
			try
			{
				readSucceeded = await this.Reader.ReadAsync(cancellationToken).WithCurrentCulture<bool>();
			}
			catch (Exception e)
			{
				this.HandleReaderException(e);
				throw;
			}
			return readSucceeded;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0008237E File Offset: 0x0008057E
		private void HandleReaderException(Exception e)
		{
			if (!e.IsCatchableEntityExceptionType())
			{
				return;
			}
			if (this.Reader.IsClosed)
			{
				throw new EntityCommandExecutionException(Strings.ADP_DataReaderClosed("Read"), e);
			}
			throw new EntityCommandExecutionException(Strings.EntityClient_StoreReaderFailed, e);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000823B2 File Offset: 0x000805B2
		private void StartMaterializingElement()
		{
			if (this.Context != null)
			{
				this.Context.InMaterialization = true;
				base.InitializeForOnMaterialize();
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000823CE File Offset: 0x000805CE
		private void StopMaterializingElement()
		{
			if (this.Context != null)
			{
				this.Context.InMaterialization = false;
				base.RaiseMaterializedEvents();
			}
		}

		// Token: 0x0400090E RID: 2318
		private readonly bool _isObjectQuery;

		// Token: 0x0400090F RID: 2319
		private bool _isActive;

		// Token: 0x04000910 RID: 2320
		private IDbEnumerator<T> _rootEnumerator;

		// Token: 0x04000911 RID: 2321
		private readonly bool _readerOwned;

		// Token: 0x04000913 RID: 2323
		internal readonly Coordinator<T> RootCoordinator;

		// Token: 0x020002E9 RID: 745
		private class SimpleEnumerator : IDbEnumerator<T>, IEnumerator<!0>, IEnumerator, IDbAsyncEnumerator<T>, IDbAsyncEnumerator, IDisposable
		{
			// Token: 0x06001A49 RID: 6729 RVA: 0x000823EA File Offset: 0x000805EA
			internal SimpleEnumerator(Shaper<T> shaper)
			{
				this._shaper = shaper;
			}

			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06001A4A RID: 6730 RVA: 0x000823F9 File Offset: 0x000805F9
			public T Current
			{
				get
				{
					return this._shaper.RootCoordinator.Current;
				}
			}

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06001A4B RID: 6731 RVA: 0x0008240B File Offset: 0x0008060B
			object IEnumerator.Current
			{
				get
				{
					return this._shaper.RootCoordinator.Current;
				}
			}

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00082422 File Offset: 0x00080622
			object IDbAsyncEnumerator.Current
			{
				get
				{
					return this._shaper.RootCoordinator.Current;
				}
			}

			// Token: 0x06001A4D RID: 6733 RVA: 0x00082439 File Offset: 0x00080639
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._shaper.RootCoordinator.SetCurrentToDefault();
				this._shaper.Finally();
			}

			// Token: 0x06001A4E RID: 6734 RVA: 0x0008245C File Offset: 0x0008065C
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

			// Token: 0x06001A4F RID: 6735 RVA: 0x0008265C File Offset: 0x0008085C
			public async Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				bool result;
				if (!this._shaper._isActive)
				{
					result = false;
				}
				else
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (await this._shaper.StoreReadAsync(cancellationToken).WithCurrentCulture<bool>())
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
						result = true;
					}
					else
					{
						this.Dispose();
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06001A50 RID: 6736 RVA: 0x000826AA File Offset: 0x000808AA
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x04000915 RID: 2325
			private readonly Shaper<T> _shaper;
		}

		// Token: 0x020002EA RID: 746
		private class RowNestedResultEnumerator : IDbEnumerator<Coordinator[]>, IEnumerator<Coordinator[]>, IEnumerator, IDbAsyncEnumerator<Coordinator[]>, IDbAsyncEnumerator, IDisposable
		{
			// Token: 0x06001A51 RID: 6737 RVA: 0x000826B1 File Offset: 0x000808B1
			internal RowNestedResultEnumerator(Shaper<T> shaper)
			{
				this._shaper = shaper;
				this._current = new Coordinator[this._shaper.RootCoordinator.MaxDistanceToLeaf() + 1];
			}

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06001A52 RID: 6738 RVA: 0x000826DD File Offset: 0x000808DD
			public Coordinator[] Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06001A53 RID: 6739 RVA: 0x000826E5 File Offset: 0x000808E5
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06001A54 RID: 6740 RVA: 0x000826ED File Offset: 0x000808ED
			object IDbAsyncEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06001A55 RID: 6741 RVA: 0x000826F5 File Offset: 0x000808F5
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._shaper.Finally();
			}

			// Token: 0x06001A56 RID: 6742 RVA: 0x00082708 File Offset: 0x00080908
			public bool MoveNext()
			{
				try
				{
					this._shaper.StartMaterializingElement();
					if (!this._shaper.StoreRead())
					{
						this.RootCoordinator.ResetCollection(this._shaper);
						return false;
					}
					this.MaterializeRow();
				}
				finally
				{
					this._shaper.StopMaterializingElement();
				}
				return true;
			}

			// Token: 0x06001A57 RID: 6743 RVA: 0x000828C4 File Offset: 0x00080AC4
			public async Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				try
				{
					this._shaper.StartMaterializingElement();
					if (!(await this._shaper.StoreReadAsync(cancellationToken).WithCurrentCulture<bool>()))
					{
						this.RootCoordinator.ResetCollection(this._shaper);
						return false;
					}
					this.MaterializeRow();
				}
				finally
				{
					this._shaper.StopMaterializingElement();
				}
				return true;
			}

			// Token: 0x06001A58 RID: 6744 RVA: 0x00082914 File Offset: 0x00080B14
			private void MaterializeRow()
			{
				Coordinator coordinator = this._shaper.RootCoordinator;
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
						IL_A8:
						while (i < this._current.Length)
						{
							this._current[i] = null;
							i++;
						}
						return;
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
				goto IL_A8;
			}

			// Token: 0x06001A59 RID: 6745 RVA: 0x000829D4 File Offset: 0x00080BD4
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06001A5A RID: 6746 RVA: 0x000829DB File Offset: 0x00080BDB
			internal Coordinator<T> RootCoordinator
			{
				get
				{
					return this._shaper.RootCoordinator;
				}
			}

			// Token: 0x04000916 RID: 2326
			private readonly Shaper<T> _shaper;

			// Token: 0x04000917 RID: 2327
			private readonly Coordinator[] _current;
		}

		// Token: 0x020002EB RID: 747
		private class ObjectQueryNestedEnumerator : IDbEnumerator<T>, IEnumerator<!0>, IEnumerator, IDbAsyncEnumerator<T>, IDbAsyncEnumerator, IDisposable
		{
			// Token: 0x06001A5B RID: 6747 RVA: 0x000829E8 File Offset: 0x00080BE8
			internal ObjectQueryNestedEnumerator(Shaper<T>.RowNestedResultEnumerator rowEnumerator)
			{
				this._rowEnumerator = rowEnumerator;
				this._previousElement = default(T);
				this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.Start;
			}

			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06001A5C RID: 6748 RVA: 0x00082A0A File Offset: 0x00080C0A
			public T Current
			{
				get
				{
					return this._previousElement;
				}
			}

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06001A5D RID: 6749 RVA: 0x00082A12 File Offset: 0x00080C12
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00082A1F File Offset: 0x00080C1F
			object IDbAsyncEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001A5F RID: 6751 RVA: 0x00082A2C File Offset: 0x00080C2C
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._rowEnumerator.Dispose();
			}

			// Token: 0x06001A60 RID: 6752 RVA: 0x00082A40 File Offset: 0x00080C40
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

			// Token: 0x06001A61 RID: 6753 RVA: 0x00082D30 File Offset: 0x00080F30
			public async Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				cancellationToken.ThrowIfCancellationRequested();
				switch (this._state)
				{
				case Shaper<T>.ObjectQueryNestedEnumerator.State.Start:
					if (await this.TryReadToNextElementAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						await this.ReadElementAsync(cancellationToken).WithCurrentCulture();
					}
					else
					{
						this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRows;
					}
					break;
				case Shaper<T>.ObjectQueryNestedEnumerator.State.Reading:
					await this.ReadElementAsync(cancellationToken).WithCurrentCulture();
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

			// Token: 0x06001A62 RID: 6754 RVA: 0x00082D7E File Offset: 0x00080F7E
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

			// Token: 0x06001A63 RID: 6755 RVA: 0x00082ECC File Offset: 0x000810CC
			private async Task ReadElementAsync(CancellationToken cancellationToken)
			{
				this._previousElement = this._rowEnumerator.RootCoordinator.Current;
				if (await this.TryReadToNextElementAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.Reading;
				}
				else
				{
					this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRowsLastElementPending;
				}
			}

			// Token: 0x06001A64 RID: 6756 RVA: 0x00082F1A File Offset: 0x0008111A
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

			// Token: 0x06001A65 RID: 6757 RVA: 0x0008304C File Offset: 0x0008124C
			private async Task<bool> TryReadToNextElementAsync(CancellationToken cancellationToken)
			{
				while (await this._rowEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					if (this._rowEnumerator.Current[0] != null)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001A66 RID: 6758 RVA: 0x0008309A File Offset: 0x0008129A
			public void Reset()
			{
				this._rowEnumerator.Reset();
			}

			// Token: 0x04000918 RID: 2328
			private readonly Shaper<T>.RowNestedResultEnumerator _rowEnumerator;

			// Token: 0x04000919 RID: 2329
			private T _previousElement;

			// Token: 0x0400091A RID: 2330
			private Shaper<T>.ObjectQueryNestedEnumerator.State _state;

			// Token: 0x020002EC RID: 748
			private enum State
			{
				// Token: 0x0400091C RID: 2332
				Start,
				// Token: 0x0400091D RID: 2333
				Reading,
				// Token: 0x0400091E RID: 2334
				NoRowsLastElementPending,
				// Token: 0x0400091F RID: 2335
				NoRows
			}
		}

		// Token: 0x020002ED RID: 749
		private class RecordStateEnumerator : IDbEnumerator<RecordState>, IEnumerator<RecordState>, IEnumerator, IDbAsyncEnumerator<RecordState>, IDbAsyncEnumerator, IDisposable
		{
			// Token: 0x06001A67 RID: 6759 RVA: 0x000830A7 File Offset: 0x000812A7
			internal RecordStateEnumerator(Shaper<T>.RowNestedResultEnumerator rowEnumerator)
			{
				this._rowEnumerator = rowEnumerator;
				this._current = null;
				this._depth = -1;
				this._readerConsumed = false;
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06001A68 RID: 6760 RVA: 0x000830CB File Offset: 0x000812CB
			public RecordState Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06001A69 RID: 6761 RVA: 0x000830D3 File Offset: 0x000812D3
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06001A6A RID: 6762 RVA: 0x000830DB File Offset: 0x000812DB
			object IDbAsyncEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06001A6B RID: 6763 RVA: 0x000830E3 File Offset: 0x000812E3
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._rowEnumerator.Dispose();
			}

			// Token: 0x06001A6C RID: 6764 RVA: 0x000830F8 File Offset: 0x000812F8
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

			// Token: 0x06001A6D RID: 6765 RVA: 0x00083394 File Offset: 0x00081594
			public async Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				if (!this._readerConsumed)
				{
					cancellationToken.ThrowIfCancellationRequested();
					Coordinator currentCoordinator;
					for (;;)
					{
						if (-1 == this._depth || this._rowEnumerator.Current.Length == this._depth)
						{
							if (!(await this._rowEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>()))
							{
								break;
							}
							this._depth = 0;
						}
						currentCoordinator = this._rowEnumerator.Current[this._depth];
						if (currentCoordinator != null)
						{
							goto Block_4;
						}
						this._depth++;
					}
					this._current = null;
					this._readerConsumed = true;
					goto IL_176;
					Block_4:
					this._current = ((Coordinator<RecordState>)currentCoordinator).Current;
					this._depth++;
				}
				IL_176:
				return !this._readerConsumed;
			}

			// Token: 0x06001A6E RID: 6766 RVA: 0x000833E2 File Offset: 0x000815E2
			public void Reset()
			{
				this._rowEnumerator.Reset();
			}

			// Token: 0x04000920 RID: 2336
			private readonly Shaper<T>.RowNestedResultEnumerator _rowEnumerator;

			// Token: 0x04000921 RID: 2337
			private RecordState _current;

			// Token: 0x04000922 RID: 2338
			private int _depth;

			// Token: 0x04000923 RID: 2339
			private bool _readerConsumed;
		}
	}
}
