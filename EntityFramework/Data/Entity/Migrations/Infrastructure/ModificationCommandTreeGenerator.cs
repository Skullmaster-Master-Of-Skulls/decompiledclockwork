using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x02000280 RID: 640
	internal class ModificationCommandTreeGenerator
	{
		// Token: 0x06001692 RID: 5778 RVA: 0x0006DDF8 File Offset: 0x0006BFF8
		public ModificationCommandTreeGenerator(DbModel model, DbConnection connection = null)
		{
			this._compiledModel = new DbCompiledModel(model);
			this._connection = connection;
			using (DbContext dbContext = this.CreateContext())
			{
				this._metadataWorkspace = ((IObjectContextAdapter)dbContext).ObjectContext.MetadataWorkspace;
			}
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0006DE54 File Offset: 0x0006C054
		private DbContext CreateContext()
		{
			if (this._connection != null)
			{
				return new ModificationCommandTreeGenerator.TempDbContext(this._connection, this._compiledModel);
			}
			return new ModificationCommandTreeGenerator.TempDbContext(this._compiledModel);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0006DE7B File Offset: 0x0006C07B
		public IEnumerable<DbInsertCommandTree> GenerateAssociationInsert(string associationIdentity)
		{
			return this.GenerateAssociation<DbInsertCommandTree>(associationIdentity, EntityState.Added);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0006DE85 File Offset: 0x0006C085
		public IEnumerable<DbDeleteCommandTree> GenerateAssociationDelete(string associationIdentity)
		{
			return this.GenerateAssociation<DbDeleteCommandTree>(associationIdentity, EntityState.Deleted);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0006E228 File Offset: 0x0006C428
		private IEnumerable<TCommandTree> GenerateAssociation<TCommandTree>(string associationIdentity, EntityState state) where TCommandTree : DbCommandTree
		{
			AssociationType associationType = this._metadataWorkspace.GetItem<AssociationType>(associationIdentity, DataSpace.CSpace);
			using (DbContext context = this.CreateContext())
			{
				EntityType sourceEntityType = associationType.SourceEnd.GetEntityType();
				object sourceEntity = this.InstantiateAndAttachEntity(sourceEntityType, context);
				EntityType targetEntityType = associationType.TargetEnd.GetEntityType();
				object targetEntity = (sourceEntityType.GetRootType() == targetEntityType.GetRootType()) ? sourceEntity : this.InstantiateAndAttachEntity(targetEntityType, context);
				ObjectStateManager objectStateManager = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager;
				objectStateManager.ChangeRelationshipState(sourceEntity, targetEntity, associationType.FullName, associationType.TargetEnd.Name, (state == EntityState.Deleted) ? state : EntityState.Added);
				using (CommandTracer commandTracer = new CommandTracer(context))
				{
					context.SaveChanges();
					foreach (DbCommandTree commandTree in commandTracer.CommandTrees)
					{
						yield return (TCommandTree)((object)commandTree);
					}
				}
			}
			yield break;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0006E254 File Offset: 0x0006C454
		private object InstantiateAndAttachEntity(EntityType entityType, DbContext context)
		{
			Type clrType = entityType.GetClrType();
			DbSet dbSet = context.Set(clrType);
			object obj = this.InstantiateEntity(entityType, context, clrType, dbSet);
			ModificationCommandTreeGenerator.SetFakeReferenceKeyValues(obj, entityType);
			ModificationCommandTreeGenerator.SetFakeKeyValues(obj, entityType);
			dbSet.Attach(obj);
			return obj;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0006E2B8 File Offset: 0x0006C4B8
		private object InstantiateEntity(EntityType entityType, DbContext context, Type clrType, DbSet set)
		{
			object obj;
			if (!clrType.IsAbstract())
			{
				obj = set.Create();
			}
			else
			{
				EntityType entityType2 = this._metadataWorkspace.GetItems<EntityType>(DataSpace.CSpace).First((EntityType et) => entityType.IsAncestorOf(et) && !et.Abstract);
				obj = context.Set(entityType2.GetClrType()).Create();
			}
			ModificationCommandTreeGenerator.InstantiateComplexProperties(obj, entityType.Properties);
			return obj;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0006E32D File Offset: 0x0006C52D
		public IEnumerable<DbModificationCommandTree> GenerateInsert(string entityIdentity)
		{
			return this.Generate(entityIdentity, EntityState.Added);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0006E337 File Offset: 0x0006C537
		public IEnumerable<DbModificationCommandTree> GenerateUpdate(string entityIdentity)
		{
			return this.Generate(entityIdentity, EntityState.Modified);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0006E342 File Offset: 0x0006C542
		public IEnumerable<DbModificationCommandTree> GenerateDelete(string entityIdentity)
		{
			return this.Generate(entityIdentity, EntityState.Deleted);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0006E6B4 File Offset: 0x0006C8B4
		private IEnumerable<DbModificationCommandTree> Generate(string entityIdentity, EntityState state)
		{
			EntityType entityType = this._metadataWorkspace.GetItem<EntityType>(entityIdentity, DataSpace.CSpace);
			using (DbContext context = this.CreateContext())
			{
				object entity = this.InstantiateAndAttachEntity(entityType, context);
				if (state != EntityState.Deleted)
				{
					context.Entry(entity).State = state;
				}
				this.ChangeRelationshipStates(context, entityType, entity, state);
				if (state == EntityState.Deleted)
				{
					context.Entry(entity).State = state;
				}
				this.HandleTableSplitting(context, entityType, entity, state);
				using (CommandTracer commandTracer = new CommandTracer(context))
				{
					((IObjectContextAdapter)context).ObjectContext.SaveChanges(SaveOptions.None);
					foreach (DbCommandTree commandTree in commandTracer.CommandTrees)
					{
						yield return (DbModificationCommandTree)commandTree;
					}
				}
			}
			yield break;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0006E738 File Offset: 0x0006C938
		private void ChangeRelationshipStates(DbContext context, EntityType entityType, object entity, EntityState state)
		{
			ObjectStateManager objectStateManager = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager;
			IEnumerable<AssociationType> enumerable = from at in this._metadataWorkspace.GetItems<AssociationType>(DataSpace.CSpace)
			where !at.IsForeignKey && !at.IsManyToMany() && (at.SourceEnd.GetEntityType().IsAssignableFrom(entityType) || at.TargetEnd.GetEntityType().IsAssignableFrom(entityType))
			select at;
			foreach (AssociationType associationType in enumerable)
			{
				AssociationEndMember sourceEnd;
				AssociationEndMember targetEnd;
				if (!associationType.TryGuessPrincipalAndDependentEnds(out sourceEnd, out targetEnd))
				{
					sourceEnd = associationType.SourceEnd;
					targetEnd = associationType.TargetEnd;
				}
				if (targetEnd.GetEntityType().IsAssignableFrom(entityType))
				{
					EntityType entityType2 = sourceEnd.GetEntityType();
					Type clrType = entityType2.GetClrType();
					DbSet dbSet = context.Set(clrType);
					object obj = dbSet.Local.Cast<object>().SingleOrDefault<object>();
					if (obj == null || (object.ReferenceEquals(entity, obj) && state == EntityState.Added))
					{
						obj = this.InstantiateEntity(entityType2, context, clrType, dbSet);
						ModificationCommandTreeGenerator.SetFakeReferenceKeyValues(obj, entityType2);
						dbSet.Attach(obj);
					}
					if (sourceEnd.IsRequired() && state == EntityState.Modified)
					{
						object obj2 = this.InstantiateEntity(entityType2, context, clrType, dbSet);
						ModificationCommandTreeGenerator.SetFakeKeyValues(obj2, entityType2);
						dbSet.Attach(obj2);
						objectStateManager.ChangeRelationshipState(entity, obj2, associationType.FullName, sourceEnd.Name, EntityState.Deleted);
					}
					objectStateManager.ChangeRelationshipState(entity, obj, associationType.FullName, sourceEnd.Name, (state == EntityState.Deleted) ? state : EntityState.Added);
				}
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0006E998 File Offset: 0x0006CB98
		private void HandleTableSplitting(DbContext context, EntityType entityType, object entity, EntityState state)
		{
			IEnumerable<AssociationType> enumerable = from at in this._metadataWorkspace.GetItems<AssociationType>(DataSpace.CSpace)
			where at.IsForeignKey && at.IsRequiredToRequired() && !at.IsSelfReferencing() && (at.SourceEnd.GetEntityType().IsAssignableFrom(entityType) || at.TargetEnd.GetEntityType().IsAssignableFrom(entityType)) && this._metadataWorkspace.GetItems<AssociationType>(DataSpace.SSpace).All((AssociationType fk) => fk.Name != at.Name)
			select at;
			foreach (AssociationType associationType in enumerable)
			{
				AssociationEndMember sourceEnd;
				AssociationEndMember targetEnd;
				if (!associationType.TryGuessPrincipalAndDependentEnds(out sourceEnd, out targetEnd))
				{
					sourceEnd = associationType.SourceEnd;
					targetEnd = associationType.TargetEnd;
				}
				bool flag = false;
				EntityType entityType2;
				if (sourceEnd.GetEntityType().GetRootType() == entityType.GetRootType())
				{
					flag = true;
					entityType2 = targetEnd.GetEntityType();
				}
				else
				{
					entityType2 = sourceEnd.GetEntityType();
				}
				object entity2 = this.InstantiateAndAttachEntity(entityType2, context);
				if (!flag)
				{
					if (state == EntityState.Added)
					{
						context.Entry(entity).State = EntityState.Modified;
					}
					else if (state == EntityState.Deleted)
					{
						context.Entry(entity).State = EntityState.Unchanged;
					}
				}
				else if (state != EntityState.Modified)
				{
					context.Entry(entity2).State = state;
				}
			}
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0006EAB4 File Offset: 0x0006CCB4
		private static void SetFakeReferenceKeyValues(object entity, EntityType entityType)
		{
			foreach (EdmProperty edmProperty in entityType.KeyProperties)
			{
				PropertyInfo clrPropertyInfo = edmProperty.GetClrPropertyInfo();
				object fakeReferenceKeyValue = ModificationCommandTreeGenerator.GetFakeReferenceKeyValue(edmProperty.UnderlyingPrimitiveType.PrimitiveTypeKind);
				if (fakeReferenceKeyValue != null)
				{
					clrPropertyInfo.GetPropertyInfoForSet().SetValue(entity, fakeReferenceKeyValue, null);
				}
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0006EB2C File Offset: 0x0006CD2C
		private static object GetFakeReferenceKeyValue(PrimitiveTypeKind primitiveTypeKind)
		{
			if (primitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				switch (primitiveTypeKind)
				{
				case PrimitiveTypeKind.String:
					return "42";
				case PrimitiveTypeKind.Geometry:
					return DefaultSpatialServices.Instance.GeometryFromText("POINT (4 2)");
				case PrimitiveTypeKind.Geography:
					return DefaultSpatialServices.Instance.GeographyFromText("POINT (4 2)");
				}
				return null;
			}
			return new byte[0];
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0006EB8C File Offset: 0x0006CD8C
		private static void SetFakeKeyValues(object entity, EntityType entityType)
		{
			foreach (EdmProperty edmProperty in entityType.KeyProperties)
			{
				PropertyInfo clrPropertyInfo = edmProperty.GetClrPropertyInfo();
				object fakeKeyValue = ModificationCommandTreeGenerator.GetFakeKeyValue(edmProperty.UnderlyingPrimitiveType.PrimitiveTypeKind);
				clrPropertyInfo.GetPropertyInfoForSet().SetValue(entity, fakeKeyValue, null);
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0006EC00 File Offset: 0x0006CE00
		private static object GetFakeKeyValue(PrimitiveTypeKind primitiveTypeKind)
		{
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				return new byte[]
				{
					66
				};
			case PrimitiveTypeKind.Boolean:
				return true;
			case PrimitiveTypeKind.Byte:
				return 66;
			case PrimitiveTypeKind.DateTime:
				return DateTime.Now;
			case PrimitiveTypeKind.Decimal:
				return 42m;
			case PrimitiveTypeKind.Double:
				return 42.0;
			case PrimitiveTypeKind.Guid:
				return Guid.NewGuid();
			case PrimitiveTypeKind.Single:
				return 42f;
			case PrimitiveTypeKind.SByte:
				return 42;
			case PrimitiveTypeKind.Int16:
				return 42;
			case PrimitiveTypeKind.Int32:
				return 42;
			case PrimitiveTypeKind.Int64:
				return 42L;
			case PrimitiveTypeKind.String:
				return "42'";
			case PrimitiveTypeKind.Time:
				return TimeSpan.FromMilliseconds(42.0);
			case PrimitiveTypeKind.DateTimeOffset:
				return DateTimeOffset.Now;
			case PrimitiveTypeKind.Geometry:
				return DefaultSpatialServices.Instance.GeometryFromText("POINT (4 3)");
			case PrimitiveTypeKind.Geography:
				return DefaultSpatialServices.Instance.GeographyFromText("POINT (4 3)");
			default:
				return null;
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0006ED20 File Offset: 0x0006CF20
		private static void InstantiateComplexProperties(object structuralObject, IEnumerable<EdmProperty> properties)
		{
			foreach (EdmProperty edmProperty in properties)
			{
				if (edmProperty.IsComplexType)
				{
					PropertyInfo clrPropertyInfo = edmProperty.GetClrPropertyInfo();
					object obj = Activator.CreateInstance(clrPropertyInfo.PropertyType);
					ModificationCommandTreeGenerator.InstantiateComplexProperties(obj, edmProperty.ComplexType.Properties);
					clrPropertyInfo.GetPropertyInfoForSet().SetValue(structuralObject, obj, null);
				}
			}
		}

		// Token: 0x0400080C RID: 2060
		private readonly DbCompiledModel _compiledModel;

		// Token: 0x0400080D RID: 2061
		private readonly DbConnection _connection;

		// Token: 0x0400080E RID: 2062
		private readonly MetadataWorkspace _metadataWorkspace;

		// Token: 0x02000281 RID: 641
		private class TempDbContext : DbContext
		{
			// Token: 0x060016A4 RID: 5796 RVA: 0x0006ED9C File Offset: 0x0006CF9C
			[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
			public TempDbContext(DbCompiledModel model) : base(model)
			{
				this.InternalContext.InitializerDisabled = true;
			}

			// Token: 0x060016A5 RID: 5797 RVA: 0x0006EDB1 File Offset: 0x0006CFB1
			[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
			public TempDbContext(DbConnection connection, DbCompiledModel model) : base(connection, model, false)
			{
				this.InternalContext.InitializerDisabled = true;
			}
		}
	}
}
