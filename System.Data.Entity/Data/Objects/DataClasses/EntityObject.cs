using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x0200018C RID: 396
	[DataContract(IsReference = true)]
	[Serializable]
	public abstract class EntityObject : StructuralObject, IEntityWithKey, IEntityWithChangeTracker, IEntityWithRelationships
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0005FCF6 File Offset: 0x0005DEF6
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x0005FD11 File Offset: 0x0005DF11
		private IEntityChangeTracker EntityChangeTracker
		{
			get
			{
				if (this._entityChangeTracker == null)
				{
					this._entityChangeTracker = EntityObject.s_detachedEntityChangeTracker;
				}
				return this._entityChangeTracker;
			}
			set
			{
				this._entityChangeTracker = value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0005FD1A File Offset: 0x0005DF1A
		[Browsable(false)]
		[XmlIgnore]
		public EntityState EntityState
		{
			get
			{
				return this.EntityChangeTracker.EntityState;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0005FD27 File Offset: 0x0005DF27
		// (set) Token: 0x06001C40 RID: 7232 RVA: 0x0005FD2F File Offset: 0x0005DF2F
		[Browsable(false)]
		[DataMember]
		public EntityKey EntityKey
		{
			get
			{
				return this._entityKey;
			}
			set
			{
				this.EntityChangeTracker.EntityMemberChanging(StructuralObject.EntityKeyPropertyName);
				this._entityKey = value;
				this.EntityChangeTracker.EntityMemberChanged(StructuralObject.EntityKeyPropertyName);
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0005FD58 File Offset: 0x0005DF58
		void IEntityWithChangeTracker.SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			if (changeTracker != null && this.EntityChangeTracker != EntityObject.s_detachedEntityChangeTracker && changeTracker != this.EntityChangeTracker)
			{
				EntityEntry entityEntry = this.EntityChangeTracker as EntityEntry;
				if (entityEntry == null || !entityEntry.ObjectStateManager.IsDisposed)
				{
					throw EntityUtil.EntityCantHaveMultipleChangeTrackers();
				}
			}
			this.EntityChangeTracker = changeTracker;
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0005FDA7 File Offset: 0x0005DFA7
		RelationshipManager IEntityWithRelationships.RelationshipManager
		{
			get
			{
				if (this._relationships == null)
				{
					this._relationships = RelationshipManager.Create(this);
				}
				return this._relationships;
			}
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0005FDC3 File Offset: 0x0005DFC3
		protected sealed override void ReportPropertyChanging(string property)
		{
			EntityUtil.CheckStringArgument(property, "property");
			base.ReportPropertyChanging(property);
			this.EntityChangeTracker.EntityMemberChanging(property);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0005FDE3 File Offset: 0x0005DFE3
		protected sealed override void ReportPropertyChanged(string property)
		{
			EntityUtil.CheckStringArgument(property, "property");
			this.EntityChangeTracker.EntityMemberChanged(property);
			base.ReportPropertyChanged(property);
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x0005FE03 File Offset: 0x0005E003
		internal sealed override bool IsChangeTracked
		{
			get
			{
				return this.EntityState != EntityState.Detached;
			}
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0005FE11 File Offset: 0x0005E011
		internal sealed override void ReportComplexPropertyChanging(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			this.EntityChangeTracker.EntityComplexMemberChanging(entityMemberName, complexObject, complexMemberName);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0005FE21 File Offset: 0x0005E021
		internal sealed override void ReportComplexPropertyChanged(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			this.EntityChangeTracker.EntityComplexMemberChanged(entityMemberName, complexObject, complexMemberName);
		}

		// Token: 0x04000BAA RID: 2986
		private RelationshipManager _relationships;

		// Token: 0x04000BAB RID: 2987
		private EntityKey _entityKey;

		// Token: 0x04000BAC RID: 2988
		[NonSerialized]
		private IEntityChangeTracker _entityChangeTracker = EntityObject.s_detachedEntityChangeTracker;

		// Token: 0x04000BAD RID: 2989
		[NonSerialized]
		private static readonly EntityObject.DetachedEntityChangeTracker s_detachedEntityChangeTracker = new EntityObject.DetachedEntityChangeTracker();

		// Token: 0x020004C9 RID: 1225
		private class DetachedEntityChangeTracker : IEntityChangeTracker
		{
			// Token: 0x06003CC7 RID: 15559 RVA: 0x000089D0 File Offset: 0x00006BD0
			void IEntityChangeTracker.EntityMemberChanging(string entityMemberName)
			{
			}

			// Token: 0x06003CC8 RID: 15560 RVA: 0x000089D0 File Offset: 0x00006BD0
			void IEntityChangeTracker.EntityMemberChanged(string entityMemberName)
			{
			}

			// Token: 0x06003CC9 RID: 15561 RVA: 0x000089D0 File Offset: 0x00006BD0
			void IEntityChangeTracker.EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexMemberName)
			{
			}

			// Token: 0x06003CCA RID: 15562 RVA: 0x000089D0 File Offset: 0x00006BD0
			void IEntityChangeTracker.EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexMemberName)
			{
			}

			// Token: 0x17000AFA RID: 2810
			// (get) Token: 0x06003CCB RID: 15563 RVA: 0x00017938 File Offset: 0x00015B38
			EntityState IEntityChangeTracker.EntityState
			{
				get
				{
					return EntityState.Detached;
				}
			}
		}
	}
}
