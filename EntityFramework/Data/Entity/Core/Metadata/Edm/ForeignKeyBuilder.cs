using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020001F7 RID: 503
	internal class ForeignKeyBuilder : MetadataItem, INamedDataModelItem
	{
		// Token: 0x06001194 RID: 4500 RVA: 0x0004AE62 File Offset: 0x00049062
		internal ForeignKeyBuilder()
		{
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0004AE6C File Offset: 0x0004906C
		public ForeignKeyBuilder(EdmModel database, string name)
		{
			Check.NotNull<EdmModel>(database, "database");
			this._database = database;
			this._associationType = new AssociationType(name, "CodeFirstDatabaseSchema", true, DataSpace.SSpace);
			this._associationSet = new AssociationSet(this._associationType.Name, this._associationType);
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0004AEC1 File Offset: 0x000490C1
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x0004AECE File Offset: 0x000490CE
		public string Name
		{
			get
			{
				return this._associationType.Name;
			}
			set
			{
				this._associationType.Name = value;
				this._associationSet.Name = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0004AEE8 File Offset: 0x000490E8
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x0004AEFC File Offset: 0x000490FC
		public virtual EntityType PrincipalTable
		{
			get
			{
				return this._associationType.SourceEnd.GetEntityType();
			}
			set
			{
				Check.NotNull<EntityType>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._associationType.SourceEnd = new AssociationEndMember(value.Name, value);
				this._associationSet.SourceSet = this._database.GetEntitySet(value);
				if (this._associationType.TargetEnd != null && value.Name == this._associationType.TargetEnd.Name)
				{
					this._associationType.TargetEnd.Name = value.Name + "Self";
				}
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0004AF94 File Offset: 0x00049194
		public virtual void SetOwner(EntityType owner)
		{
			Util.ThrowIfReadOnly(this);
			if (owner == null)
			{
				this._database.RemoveAssociationType(this._associationType);
				return;
			}
			this._associationType.TargetEnd = new AssociationEndMember((owner != this.PrincipalTable) ? owner.Name : (owner.Name + "Self"), owner);
			this._associationSet.TargetSet = this._database.GetEntitySet(owner);
			if (!this._database.AssociationTypes.Contains(this._associationType))
			{
				this._database.AddAssociationType(this._associationType);
				this._database.AddAssociationSet(this._associationSet);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0004B03F File Offset: 0x0004923F
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x0004B064 File Offset: 0x00049264
		public virtual IEnumerable<EdmProperty> DependentColumns
		{
			get
			{
				if (this._associationType.Constraint == null)
				{
					return Enumerable.Empty<EdmProperty>();
				}
				return this._associationType.Constraint.ToProperties;
			}
			set
			{
				Check.NotNull<IEnumerable<EdmProperty>>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._associationType.Constraint = new ReferentialConstraint(this._associationType.SourceEnd, this._associationType.TargetEnd, this.PrincipalTable.KeyProperties, value);
				this.SetMultiplicities();
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0004B0BB File Offset: 0x000492BB
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x0004B0DC File Offset: 0x000492DC
		public OperationAction DeleteAction
		{
			get
			{
				if (this._associationType.SourceEnd == null)
				{
					return OperationAction.None;
				}
				return this._associationType.SourceEnd.DeleteBehavior;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._associationType.SourceEnd.DeleteBehavior = value;
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0004B118 File Offset: 0x00049318
		private void SetMultiplicities()
		{
			this._associationType.SourceEnd.RelationshipMultiplicity = RelationshipMultiplicity.ZeroOrOne;
			this._associationType.TargetEnd.RelationshipMultiplicity = RelationshipMultiplicity.Many;
			EntityType dependentTable = this._associationType.TargetEnd.GetEntityType();
			List<EdmProperty> list = (from key in dependentTable.KeyProperties
			where dependentTable.DeclaredMembers.Contains(key)
			select key).ToList<EdmProperty>();
			if (list.Count == this.DependentColumns.Count<EdmProperty>() && list.All(new Func<EdmProperty, bool>(this.DependentColumns.Contains<EdmProperty>)))
			{
				this._associationType.SourceEnd.RelationshipMultiplicity = RelationshipMultiplicity.One;
				this._associationType.TargetEnd.RelationshipMultiplicity = RelationshipMultiplicity.ZeroOrOne;
				return;
			}
			if (!this.DependentColumns.Any((EdmProperty p) => p.Nullable))
			{
				this._associationType.SourceEnd.RelationshipMultiplicity = RelationshipMultiplicity.One;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0004B20F File Offset: 0x0004940F
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0004B216 File Offset: 0x00049416
		string INamedDataModelItem.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0004B21E File Offset: 0x0004941E
		internal override string Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000546 RID: 1350
		private const string SelfRefSuffix = "Self";

		// Token: 0x04000547 RID: 1351
		private readonly EdmModel _database;

		// Token: 0x04000548 RID: 1352
		private readonly AssociationType _associationType;

		// Token: 0x04000549 RID: 1353
		private readonly AssociationSet _associationSet;
	}
}
