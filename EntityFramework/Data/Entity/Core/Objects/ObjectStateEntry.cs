using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200056A RID: 1386
	public abstract class ObjectStateEntry : IEntityStateEntry, IEntityChangeTracker
	{
		// Token: 0x06003581 RID: 13697 RVA: 0x000FE2E4 File Offset: 0x000FC4E4
		internal ObjectStateEntry()
		{
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000FE2EC File Offset: 0x000FC4EC
		internal ObjectStateEntry(ObjectStateManager cache, EntitySet entitySet, EntityState state)
		{
			this._cache = cache;
			this._entitySet = entitySet;
			this._state = state;
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000FE309 File Offset: 0x000FC509
		public ObjectStateManager ObjectStateManager
		{
			get
			{
				this.ValidateState();
				return this._cache;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000FE317 File Offset: 0x000FC517
		public EntitySetBase EntitySet
		{
			get
			{
				this.ValidateState();
				return this._entitySet;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000FE325 File Offset: 0x000FC525
		// (set) Token: 0x06003586 RID: 13702 RVA: 0x000FE32D File Offset: 0x000FC52D
		public EntityState State
		{
			get
			{
				return this._state;
			}
			internal set
			{
				this._state = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06003587 RID: 13703
		public abstract object Entity { get; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06003588 RID: 13704
		// (set) Token: 0x06003589 RID: 13705
		public abstract EntityKey EntityKey { get; internal set; }

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600358A RID: 13706
		public abstract bool IsRelationship { get; }

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600358B RID: 13707
		internal abstract BitArray ModifiedProperties { get; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600358C RID: 13708 RVA: 0x000FE336 File Offset: 0x000FC536
		BitArray IEntityStateEntry.ModifiedProperties
		{
			get
			{
				return this.ModifiedProperties;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600358D RID: 13709
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public abstract DbDataRecord OriginalValues { get; }

		// Token: 0x0600358E RID: 13710
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public abstract OriginalValueRecord GetUpdatableOriginalValues();

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600358F RID: 13711
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public abstract CurrentValueRecord CurrentValues { get; }

		// Token: 0x06003590 RID: 13712
		public abstract void AcceptChanges();

		// Token: 0x06003591 RID: 13713
		public abstract void Delete();

		// Token: 0x06003592 RID: 13714
		public abstract IEnumerable<string> GetModifiedProperties();

		// Token: 0x06003593 RID: 13715
		public abstract void SetModified();

		// Token: 0x06003594 RID: 13716
		public abstract void SetModifiedProperty(string propertyName);

		// Token: 0x06003595 RID: 13717
		public abstract void RejectPropertyChanges(string propertyName);

		// Token: 0x06003596 RID: 13718
		public abstract bool IsPropertyChanged(string propertyName);

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06003597 RID: 13719
		public abstract RelationshipManager RelationshipManager { get; }

		// Token: 0x06003598 RID: 13720
		public abstract void ChangeState(EntityState state);

		// Token: 0x06003599 RID: 13721
		public abstract void ApplyCurrentValues(object currentEntity);

		// Token: 0x0600359A RID: 13722
		public abstract void ApplyOriginalValues(object originalEntity);

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000FE33E File Offset: 0x000FC53E
		IEntityStateManager IEntityStateEntry.StateManager
		{
			get
			{
				return this.ObjectStateManager;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x000FE346 File Offset: 0x000FC546
		bool IEntityStateEntry.IsKeyEntry
		{
			get
			{
				return this.IsKeyEntry;
			}
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000FE34E File Offset: 0x000FC54E
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IEntityChangeTracker.EntityMemberChanging(string entityMemberName)
		{
			this.EntityMemberChanging(entityMemberName);
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000FE357 File Offset: 0x000FC557
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IEntityChangeTracker.EntityMemberChanged(string entityMemberName)
		{
			this.EntityMemberChanged(entityMemberName);
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000FE360 File Offset: 0x000FC560
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IEntityChangeTracker.EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			this.EntityComplexMemberChanging(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000FE36B File Offset: 0x000FC56B
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IEntityChangeTracker.EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			this.EntityComplexMemberChanged(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000FE376 File Offset: 0x000FC576
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		EntityState IEntityChangeTracker.EntityState
		{
			get
			{
				return this.State;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060035A2 RID: 13730
		internal abstract bool IsKeyEntry { get; }

		// Token: 0x060035A3 RID: 13731
		internal abstract int GetFieldCount(StateManagerTypeMetadata metadata);

		// Token: 0x060035A4 RID: 13732
		internal abstract Type GetFieldType(int ordinal, StateManagerTypeMetadata metadata);

		// Token: 0x060035A5 RID: 13733
		internal abstract string GetCLayerName(int ordinal, StateManagerTypeMetadata metadata);

		// Token: 0x060035A6 RID: 13734
		internal abstract int GetOrdinalforCLayerName(string name, StateManagerTypeMetadata metadata);

		// Token: 0x060035A7 RID: 13735
		internal abstract void RevertDelete();

		// Token: 0x060035A8 RID: 13736
		internal abstract void SetModifiedAll();

		// Token: 0x060035A9 RID: 13737
		internal abstract void EntityMemberChanging(string entityMemberName);

		// Token: 0x060035AA RID: 13738
		internal abstract void EntityMemberChanged(string entityMemberName);

		// Token: 0x060035AB RID: 13739
		internal abstract void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x060035AC RID: 13740
		internal abstract void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x060035AD RID: 13741
		internal abstract DataRecordInfo GetDataRecordInfo(StateManagerTypeMetadata metadata, object userObject);

		// Token: 0x060035AE RID: 13742 RVA: 0x000FE37E File Offset: 0x000FC57E
		internal virtual void Reset()
		{
			this._cache = null;
			this._entitySet = null;
			this._state = EntityState.Detached;
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000FE395 File Offset: 0x000FC595
		internal void ValidateState()
		{
			if (this._state == EntityState.Detached)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_InvalidState);
			}
		}

		// Token: 0x040014B0 RID: 5296
		internal ObjectStateManager _cache;

		// Token: 0x040014B1 RID: 5297
		internal EntitySetBase _entitySet;

		// Token: 0x040014B2 RID: 5298
		internal EntityState _state;
	}
}
