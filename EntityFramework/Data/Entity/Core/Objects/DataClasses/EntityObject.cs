using System;
using System.ComponentModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000540 RID: 1344
	[DataContract(IsReference = true)]
	[Serializable]
	public abstract class EntityObject : StructuralObject, IEntityWithKey, IEntityWithChangeTracker, IEntityWithRelationships
	{
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000F41FB File Offset: 0x000F23FB
		// (set) Token: 0x060033C7 RID: 13255 RVA: 0x000F4216 File Offset: 0x000F2416
		private IEntityChangeTracker EntityChangeTracker
		{
			get
			{
				if (this._entityChangeTracker == null)
				{
					this._entityChangeTracker = EntityObject._detachedEntityChangeTracker;
				}
				return this._entityChangeTracker;
			}
			set
			{
				this._entityChangeTracker = value;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x000F421F File Offset: 0x000F241F
		[XmlIgnore]
		[Browsable(false)]
		public EntityState EntityState
		{
			get
			{
				return this.EntityChangeTracker.EntityState;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x000F422C File Offset: 0x000F242C
		// (set) Token: 0x060033CA RID: 13258 RVA: 0x000F4234 File Offset: 0x000F2434
		[DataMember]
		[Browsable(false)]
		public EntityKey EntityKey
		{
			get
			{
				return this._entityKey;
			}
			set
			{
				this.EntityChangeTracker.EntityMemberChanging("-EntityKey-");
				this._entityKey = value;
				this.EntityChangeTracker.EntityMemberChanged("-EntityKey-");
			}
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000F4260 File Offset: 0x000F2460
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IEntityWithChangeTracker.SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			if (changeTracker != null && this.EntityChangeTracker != EntityObject._detachedEntityChangeTracker && !object.ReferenceEquals(changeTracker, this.EntityChangeTracker))
			{
				EntityEntry entityEntry = this.EntityChangeTracker as EntityEntry;
				if (entityEntry == null || !entityEntry.ObjectStateManager.IsDisposed)
				{
					throw new InvalidOperationException(Strings.Entity_EntityCantHaveMultipleChangeTrackers);
				}
			}
			this.EntityChangeTracker = changeTracker;
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x000F42B9 File Offset: 0x000F24B9
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
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

		// Token: 0x060033CD RID: 13261 RVA: 0x000F42D5 File Offset: 0x000F24D5
		protected sealed override void ReportPropertyChanging(string property)
		{
			Check.NotEmpty(property, "property");
			base.ReportPropertyChanging(property);
			this.EntityChangeTracker.EntityMemberChanging(property);
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000F42F6 File Offset: 0x000F24F6
		protected sealed override void ReportPropertyChanged(string property)
		{
			Check.NotEmpty(property, "property");
			this.EntityChangeTracker.EntityMemberChanged(property);
			base.ReportPropertyChanged(property);
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x000F4317 File Offset: 0x000F2517
		internal sealed override bool IsChangeTracked
		{
			get
			{
				return this.EntityState != EntityState.Detached;
			}
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000F4325 File Offset: 0x000F2525
		internal sealed override void ReportComplexPropertyChanging(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			this.EntityChangeTracker.EntityComplexMemberChanging(entityMemberName, complexObject, complexMemberName);
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000F4335 File Offset: 0x000F2535
		internal sealed override void ReportComplexPropertyChanged(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			this.EntityChangeTracker.EntityComplexMemberChanged(entityMemberName, complexObject, complexMemberName);
		}

		// Token: 0x04001390 RID: 5008
		private RelationshipManager _relationships;

		// Token: 0x04001391 RID: 5009
		private EntityKey _entityKey;

		// Token: 0x04001392 RID: 5010
		[NonSerialized]
		private IEntityChangeTracker _entityChangeTracker = EntityObject._detachedEntityChangeTracker;

		// Token: 0x04001393 RID: 5011
		[NonSerialized]
		private static readonly EntityObject.DetachedEntityChangeTracker _detachedEntityChangeTracker = new EntityObject.DetachedEntityChangeTracker();

		// Token: 0x02000542 RID: 1346
		private class DetachedEntityChangeTracker : IEntityChangeTracker
		{
			// Token: 0x060033D9 RID: 13273 RVA: 0x000F4364 File Offset: 0x000F2564
			void IEntityChangeTracker.EntityMemberChanging(string entityMemberName)
			{
			}

			// Token: 0x060033DA RID: 13274 RVA: 0x000F4366 File Offset: 0x000F2566
			void IEntityChangeTracker.EntityMemberChanged(string entityMemberName)
			{
			}

			// Token: 0x060033DB RID: 13275 RVA: 0x000F4368 File Offset: 0x000F2568
			void IEntityChangeTracker.EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexMemberName)
			{
			}

			// Token: 0x060033DC RID: 13276 RVA: 0x000F436A File Offset: 0x000F256A
			void IEntityChangeTracker.EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexMemberName)
			{
			}

			// Token: 0x170007B0 RID: 1968
			// (get) Token: 0x060033DD RID: 13277 RVA: 0x000F436C File Offset: 0x000F256C
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
