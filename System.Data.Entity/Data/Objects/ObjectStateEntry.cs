using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Diagnostics;

namespace System.Data.Objects
{
	// Token: 0x0200013A RID: 314
	public abstract class ObjectStateEntry : IEntityStateEntry, IEntityChangeTracker
	{
		// Token: 0x060016B8 RID: 5816 RVA: 0x0004C3CB File Offset: 0x0004A5CB
		internal ObjectStateEntry(ObjectStateManager cache, EntitySet entitySet, EntityState state)
		{
			this._cache = cache;
			this._entitySet = entitySet;
			this._state = state;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0004C3E8 File Offset: 0x0004A5E8
		public ObjectStateManager ObjectStateManager
		{
			get
			{
				this.ValidateState();
				return this._cache;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0004C3F6 File Offset: 0x0004A5F6
		public EntitySetBase EntitySet
		{
			get
			{
				this.ValidateState();
				return this._entitySet;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x0004C404 File Offset: 0x0004A604
		// (set) Token: 0x060016BC RID: 5820 RVA: 0x0004C40C File Offset: 0x0004A60C
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

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060016BD RID: 5821
		public abstract object Entity { get; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060016BE RID: 5822
		// (set) Token: 0x060016BF RID: 5823
		public abstract EntityKey EntityKey { get; internal set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060016C0 RID: 5824
		public abstract bool IsRelationship { get; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060016C1 RID: 5825
		internal abstract BitArray ModifiedProperties { get; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0004C415 File Offset: 0x0004A615
		BitArray IEntityStateEntry.ModifiedProperties
		{
			get
			{
				return this.ModifiedProperties;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060016C3 RID: 5827
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public abstract DbDataRecord OriginalValues { get; }

		// Token: 0x060016C4 RID: 5828
		public abstract OriginalValueRecord GetUpdatableOriginalValues();

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060016C5 RID: 5829
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public abstract CurrentValueRecord CurrentValues { get; }

		// Token: 0x060016C6 RID: 5830
		public abstract void AcceptChanges();

		// Token: 0x060016C7 RID: 5831
		public abstract void Delete();

		// Token: 0x060016C8 RID: 5832
		public abstract IEnumerable<string> GetModifiedProperties();

		// Token: 0x060016C9 RID: 5833
		public abstract void SetModified();

		// Token: 0x060016CA RID: 5834
		public abstract void SetModifiedProperty(string propertyName);

		// Token: 0x060016CB RID: 5835
		public abstract void RejectPropertyChanges(string propertyName);

		// Token: 0x060016CC RID: 5836
		public abstract bool IsPropertyChanged(string propertyName);

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060016CD RID: 5837
		public abstract RelationshipManager RelationshipManager { get; }

		// Token: 0x060016CE RID: 5838
		public abstract void ChangeState(EntityState state);

		// Token: 0x060016CF RID: 5839
		public abstract void ApplyCurrentValues(object currentEntity);

		// Token: 0x060016D0 RID: 5840
		public abstract void ApplyOriginalValues(object originalEntity);

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0004C41D File Offset: 0x0004A61D
		IEntityStateManager IEntityStateEntry.StateManager
		{
			get
			{
				return this.ObjectStateManager;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x0004C425 File Offset: 0x0004A625
		bool IEntityStateEntry.IsKeyEntry
		{
			get
			{
				return this.IsKeyEntry;
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0004C42D File Offset: 0x0004A62D
		void IEntityChangeTracker.EntityMemberChanging(string entityMemberName)
		{
			this.EntityMemberChanging(entityMemberName);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0004C436 File Offset: 0x0004A636
		void IEntityChangeTracker.EntityMemberChanged(string entityMemberName)
		{
			this.EntityMemberChanged(entityMemberName);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0004C43F File Offset: 0x0004A63F
		void IEntityChangeTracker.EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			this.EntityComplexMemberChanging(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0004C44A File Offset: 0x0004A64A
		void IEntityChangeTracker.EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			this.EntityComplexMemberChanged(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0004C455 File Offset: 0x0004A655
		EntityState IEntityChangeTracker.EntityState
		{
			get
			{
				return this.State;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060016D8 RID: 5848
		internal abstract bool IsKeyEntry { get; }

		// Token: 0x060016D9 RID: 5849
		internal abstract int GetFieldCount(StateManagerTypeMetadata metadata);

		// Token: 0x060016DA RID: 5850
		internal abstract Type GetFieldType(int ordinal, StateManagerTypeMetadata metadata);

		// Token: 0x060016DB RID: 5851
		internal abstract string GetCLayerName(int ordinal, StateManagerTypeMetadata metadata);

		// Token: 0x060016DC RID: 5852
		internal abstract int GetOrdinalforCLayerName(string name, StateManagerTypeMetadata metadata);

		// Token: 0x060016DD RID: 5853
		internal abstract void RevertDelete();

		// Token: 0x060016DE RID: 5854
		internal abstract void SetModifiedAll();

		// Token: 0x060016DF RID: 5855
		internal abstract void EntityMemberChanging(string entityMemberName);

		// Token: 0x060016E0 RID: 5856
		internal abstract void EntityMemberChanged(string entityMemberName);

		// Token: 0x060016E1 RID: 5857
		internal abstract void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x060016E2 RID: 5858
		internal abstract void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x060016E3 RID: 5859
		internal abstract DataRecordInfo GetDataRecordInfo(StateManagerTypeMetadata metadata, object userObject);

		// Token: 0x060016E4 RID: 5860 RVA: 0x0004C45D File Offset: 0x0004A65D
		internal virtual void Reset()
		{
			this._cache = null;
			this._entitySet = null;
			this._state = EntityState.Detached;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0004C474 File Offset: 0x0004A674
		internal void ValidateState()
		{
			if (this._state == EntityState.Detached)
			{
				throw EntityUtil.ObjectStateEntryinInvalidState();
			}
		}

		// Token: 0x04000A61 RID: 2657
		internal ObjectStateManager _cache;

		// Token: 0x04000A62 RID: 2658
		internal EntitySetBase _entitySet;

		// Token: 0x04000A63 RID: 2659
		internal EntityState _state;
	}
}
