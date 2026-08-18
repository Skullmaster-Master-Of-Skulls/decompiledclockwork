using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002E2 RID: 738
	internal abstract class Shaper
	{
		// Token: 0x060019F7 RID: 6647 RVA: 0x00080E00 File Offset: 0x0007F000
		internal Shaper(DbDataReader reader, ObjectContext context, MetadataWorkspace workspace, MergeOption mergeOption, int stateCount, bool streaming)
		{
			this.Reader = reader;
			this.MergeOption = mergeOption;
			this.State = new object[stateCount];
			this.Context = context;
			this.Workspace = workspace;
			this._spatialReader = new Lazy<DbSpatialDataReader>(new Func<DbSpatialDataReader>(this.CreateSpatialDataReader));
			this.Streaming = streaming;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00080E60 File Offset: 0x0007F060
		public TElement Discriminate<TElement>(object[] discriminatorValues, Func<object[], EntityType> discriminate, KeyValuePair<EntityType, Func<Shaper, TElement>>[] elementDelegates)
		{
			EntityType entityType = discriminate(discriminatorValues);
			Func<Shaper, TElement> func = null;
			foreach (KeyValuePair<EntityType, Func<Shaper, TElement>> keyValuePair in elementDelegates)
			{
				if (keyValuePair.Key == entityType)
				{
					func = keyValuePair.Value;
				}
			}
			return func(this);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x00080EB2 File Offset: 0x0007F0B2
		public IEntityWrapper HandleEntityNoTracking<TEntity>(IEntityWrapper wrappedEntity)
		{
			this.RegisterMaterializedEntityForEvent(wrappedEntity);
			return wrappedEntity;
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00080EBC File Offset: 0x0007F0BC
		public IEntityWrapper HandleEntity<TEntity>(IEntityWrapper wrappedEntity, EntityKey entityKey, EntitySet entitySet)
		{
			IEntityWrapper entityWrapper = wrappedEntity;
			if (entityKey != null)
			{
				EntityEntry entityEntry = this.Context.ObjectStateManager.FindEntityEntry(entityKey);
				if (entityEntry != null && !entityEntry.IsKeyEntry)
				{
					this.UpdateEntry<TEntity>(wrappedEntity, entityEntry);
					entityWrapper = entityEntry.WrappedEntity;
				}
				else
				{
					this.RegisterMaterializedEntityForEvent(entityWrapper);
					if (entityEntry == null)
					{
						this.Context.ObjectStateManager.AddEntry(wrappedEntity, entityKey, entitySet, "HandleEntity", false);
					}
					else
					{
						this.Context.ObjectStateManager.PromoteKeyEntry(entityEntry, wrappedEntity, false, true, false);
					}
				}
			}
			return entityWrapper;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00080F38 File Offset: 0x0007F138
		public IEntityWrapper HandleEntityAppendOnly<TEntity>(Func<Shaper, IEntityWrapper> constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
		{
			IEntityWrapper entityWrapper;
			if (entityKey == null)
			{
				entityWrapper = constructEntityDelegate(this);
				this.RegisterMaterializedEntityForEvent(entityWrapper);
			}
			else
			{
				EntityEntry entityEntry = this.Context.ObjectStateManager.FindEntityEntry(entityKey);
				if (entityEntry != null && !entityEntry.IsKeyEntry)
				{
					if (typeof(TEntity) != entityEntry.WrappedEntity.IdentityType)
					{
						EntityKey entityKey2 = entityEntry.EntityKey;
						throw new NotSupportedException(Strings.Materializer_RecyclingEntity(TypeHelpers.GetFullName(entityKey2.EntityContainerName, entityKey2.EntitySetName), typeof(TEntity).FullName, entityEntry.WrappedEntity.IdentityType.FullName, entityKey2.ConcatKeyValue()));
					}
					if (EntityState.Added == entityEntry.State)
					{
						throw new InvalidOperationException(Strings.Materializer_AddedEntityAlreadyExists(entityEntry.EntityKey.ConcatKeyValue()));
					}
					entityWrapper = entityEntry.WrappedEntity;
				}
				else
				{
					entityWrapper = constructEntityDelegate(this);
					this.RegisterMaterializedEntityForEvent(entityWrapper);
					if (entityEntry == null)
					{
						this.Context.ObjectStateManager.AddEntry(entityWrapper, entityKey, entitySet, "HandleEntity", false);
					}
					else
					{
						this.Context.ObjectStateManager.PromoteKeyEntry(entityEntry, entityWrapper, false, true, false);
					}
				}
			}
			return entityWrapper;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00081074 File Offset: 0x0007F274
		public IEntityWrapper HandleFullSpanCollection<TTargetEntity>(IEntityWrapper wrappedEntity, Coordinator<TTargetEntity> coordinator, AssociationEndMember targetMember)
		{
			if (wrappedEntity.Entity != null)
			{
				coordinator.RegisterCloseHandler(delegate(Shaper state, List<IEntityWrapper> spannedEntities)
				{
					this.FullSpanAction<IEntityWrapper>(wrappedEntity, spannedEntities, targetMember);
				});
			}
			return wrappedEntity;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000810C8 File Offset: 0x0007F2C8
		public IEntityWrapper HandleFullSpanElement(IEntityWrapper wrappedSource, IEntityWrapper wrappedSpannedEntity, AssociationEndMember targetMember)
		{
			if (wrappedSource.Entity == null)
			{
				return wrappedSource;
			}
			List<IEntityWrapper> list = null;
			if (wrappedSpannedEntity.Entity != null)
			{
				list = new List<IEntityWrapper>(1);
				list.Add(wrappedSpannedEntity);
			}
			else
			{
				EntityKey entityKey = wrappedSource.EntityKey;
				this.CheckClearedEntryOnSpan(null, wrappedSource, entityKey, targetMember);
			}
			this.FullSpanAction<IEntityWrapper>(wrappedSource, list, targetMember);
			return wrappedSource;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00081114 File Offset: 0x0007F314
		public IEntityWrapper HandleRelationshipSpan(IEntityWrapper wrappedEntity, EntityKey targetKey, AssociationEndMember targetMember)
		{
			if (wrappedEntity.Entity == null)
			{
				return wrappedEntity;
			}
			EntityKey entityKey = wrappedEntity.EntityKey;
			AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(targetMember);
			this.CheckClearedEntryOnSpan(targetKey, wrappedEntity, entityKey, targetMember);
			RelatedEnd relatedEnd;
			if (targetKey != null)
			{
				EntitySet entitySet;
				AssociationSet associationSet = this.Context.MetadataWorkspace.MetadataOptimization.FindCSpaceAssociationSet((AssociationType)targetMember.DeclaringType, targetMember.Name, targetKey.EntitySetName, targetKey.EntityContainerName, out entitySet);
				ObjectStateManager objectStateManager = this.Context.ObjectStateManager;
				EntityState entityState;
				if (!ObjectStateManager.TryUpdateExistingRelationships(this.Context, this.MergeOption, associationSet, otherAssociationEnd, entityKey, wrappedEntity, targetMember, targetKey, true, out entityState))
				{
					EntityEntry entityEntry = objectStateManager.GetOrAddKeyEntry(targetKey, entitySet);
					bool flag = true;
					switch (otherAssociationEnd.RelationshipMultiplicity)
					{
					case RelationshipMultiplicity.ZeroOrOne:
					case RelationshipMultiplicity.One:
						flag = !ObjectStateManager.TryUpdateExistingRelationships(this.Context, this.MergeOption, associationSet, targetMember, targetKey, entityEntry.WrappedEntity, otherAssociationEnd, entityKey, true, out entityState);
						if (entityEntry.State == EntityState.Detached)
						{
							entityEntry = objectStateManager.AddKeyEntry(targetKey, entitySet);
						}
						break;
					}
					if (flag)
					{
						if (entityEntry.IsKeyEntry || entityState == EntityState.Deleted)
						{
							RelationshipWrapper wrapper = new RelationshipWrapper(associationSet, otherAssociationEnd.Name, entityKey, targetMember.Name, targetKey);
							objectStateManager.AddNewRelation(wrapper, entityState);
						}
						else if (entityEntry.State != EntityState.Deleted)
						{
							ObjectStateManager.AddEntityToCollectionOrReference(this.MergeOption, wrappedEntity, otherAssociationEnd, entityEntry.WrappedEntity, targetMember, true, false, false);
						}
						else
						{
							RelationshipWrapper wrapper2 = new RelationshipWrapper(associationSet, otherAssociationEnd.Name, entityKey, targetMember.Name, targetKey);
							objectStateManager.AddNewRelation(wrapper2, EntityState.Deleted);
						}
					}
				}
			}
			else if (this.TryGetRelatedEnd(wrappedEntity, (AssociationType)targetMember.DeclaringType, otherAssociationEnd.Name, targetMember.Name, out relatedEnd))
			{
				this.SetIsLoadedForSpan(relatedEnd, false);
			}
			return wrappedEntity;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x000812E8 File Offset: 0x0007F4E8
		private bool TryGetRelatedEnd(IEntityWrapper wrappedEntity, AssociationType associationType, string sourceEndName, string targetEndName, out RelatedEnd relatedEnd)
		{
			AssociationType ospaceAssociationType = this.Workspace.MetadataOptimization.GetOSpaceAssociationType(associationType, () => this.Workspace.GetItemCollection(DataSpace.OSpace).GetItem<AssociationType>(associationType.FullName));
			AssociationEndMember associationEndMember = null;
			AssociationEndMember associationEndMember2 = null;
			foreach (AssociationEndMember associationEndMember3 in ospaceAssociationType.AssociationEndMembers)
			{
				if (associationEndMember3.Name == sourceEndName)
				{
					associationEndMember = associationEndMember3;
				}
				else if (associationEndMember3.Name == targetEndName)
				{
					associationEndMember2 = associationEndMember3;
				}
			}
			if (associationEndMember != null && associationEndMember2 != null)
			{
				bool flag = false;
				if (wrappedEntity.EntityKey == null)
				{
					flag = true;
				}
				else
				{
					EntitySet entitySet;
					AssociationSet associationSet = this.Workspace.MetadataOptimization.FindCSpaceAssociationSet(associationType, sourceEndName, wrappedEntity.EntityKey.EntitySetName, wrappedEntity.EntityKey.EntityContainerName, out entitySet);
					if (associationSet != null)
					{
						flag = true;
					}
				}
				if (flag)
				{
					relatedEnd = DelegateFactory.GetRelatedEnd(wrappedEntity.RelationshipManager, associationEndMember, associationEndMember2, null);
					return true;
				}
			}
			relatedEnd = null;
			return false;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0008140C File Offset: 0x0007F60C
		private void SetIsLoadedForSpan(RelatedEnd relatedEnd, bool forceToTrue)
		{
			if (!forceToTrue)
			{
				forceToTrue = relatedEnd.IsEmpty();
				EntityReference entityReference = relatedEnd as EntityReference;
				if (entityReference != null)
				{
					forceToTrue &= (entityReference.EntityKey == null);
				}
			}
			if (forceToTrue || this.MergeOption == MergeOption.OverwriteChanges)
			{
				relatedEnd.IsLoaded = true;
			}
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00081451 File Offset: 0x0007F651
		public IEntityWrapper HandleIEntityWithKey<TEntity>(IEntityWrapper wrappedEntity, EntitySet entitySet)
		{
			return this.HandleEntity<TEntity>(wrappedEntity, wrappedEntity.EntityKey, entitySet);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00081464 File Offset: 0x0007F664
		public bool SetColumnValue(int recordStateSlotNumber, int ordinal, object value)
		{
			RecordState recordState = (RecordState)this.State[recordStateSlotNumber];
			recordState.SetColumnValue(ordinal, value);
			return true;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0008148C File Offset: 0x0007F68C
		public bool SetEntityRecordInfo(int recordStateSlotNumber, EntityKey entityKey, EntitySet entitySet)
		{
			RecordState recordState = (RecordState)this.State[recordStateSlotNumber];
			recordState.SetEntityRecordInfo(entityKey, entitySet);
			return true;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000814B1 File Offset: 0x0007F6B1
		public bool SetState<T>(int ordinal, T value)
		{
			this.State[ordinal] = value;
			return true;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000814C2 File Offset: 0x0007F6C2
		public T SetStatePassthrough<T>(int ordinal, T value)
		{
			this.State[ordinal] = value;
			return value;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x000814D4 File Offset: 0x0007F6D4
		public TProperty GetPropertyValueWithErrorHandling<TProperty>(int ordinal, string propertyName, string typeName)
		{
			return new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName).GetValue(this.Reader, ordinal);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000814F8 File Offset: 0x0007F6F8
		public TColumn GetColumnValueWithErrorHandling<TColumn>(int ordinal)
		{
			return new Shaper.ColumnErrorHandlingValueReader<TColumn>().GetValue(this.Reader, ordinal);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00081518 File Offset: 0x0007F718
		protected virtual DbSpatialDataReader CreateSpatialDataReader()
		{
			return SpatialHelpers.CreateSpatialDataReader(this.Workspace, this.Reader);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0008152B File Offset: 0x0007F72B
		public DbGeography GetGeographyColumnValue(int ordinal)
		{
			if (this.Streaming)
			{
				return this._spatialReader.Value.GetGeography(ordinal);
			}
			return (DbGeography)this.Reader.GetValue(ordinal);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00081558 File Offset: 0x0007F758
		public DbGeometry GetGeometryColumnValue(int ordinal)
		{
			if (this.Streaming)
			{
				return this._spatialReader.Value.GetGeometry(ordinal);
			}
			return (DbGeometry)this.Reader.GetValue(ordinal);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00081620 File Offset: 0x0007F820
		public TColumn GetSpatialColumnValueWithErrorHandling<TColumn>(int ordinal, PrimitiveTypeKind spatialTypeKind)
		{
			TColumn value;
			if (spatialTypeKind == PrimitiveTypeKind.Geography)
			{
				if (this.Streaming)
				{
					value = new Shaper.ColumnErrorHandlingValueReader<TColumn>((DbDataReader reader, int column) => (TColumn)((object)this._spatialReader.Value.GetGeography(column)), (DbDataReader reader, int column) => this._spatialReader.Value.GetGeography(column)).GetValue(this.Reader, ordinal);
				}
				else
				{
					value = new Shaper.ColumnErrorHandlingValueReader<TColumn>((DbDataReader reader, int column) => (TColumn)((object)this.Reader.GetValue(column)), (DbDataReader reader, int column) => this.Reader.GetValue(column)).GetValue(this.Reader, ordinal);
				}
			}
			else if (this.Streaming)
			{
				value = new Shaper.ColumnErrorHandlingValueReader<TColumn>((DbDataReader reader, int column) => (TColumn)((object)this._spatialReader.Value.GetGeometry(column)), (DbDataReader reader, int column) => this._spatialReader.Value.GetGeometry(column)).GetValue(this.Reader, ordinal);
			}
			else
			{
				value = new Shaper.ColumnErrorHandlingValueReader<TColumn>((DbDataReader reader, int column) => (TColumn)((object)this.Reader.GetValue(column)), (DbDataReader reader, int column) => this.Reader.GetValue(column)).GetValue(this.Reader, ordinal);
			}
			return value;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000817D8 File Offset: 0x0007F9D8
		public TProperty GetSpatialPropertyValueWithErrorHandling<TProperty>(int ordinal, string propertyName, string typeName, PrimitiveTypeKind spatialTypeKind)
		{
			TProperty value;
			if (Helper.IsGeographicTypeKind(spatialTypeKind))
			{
				if (this.Streaming)
				{
					value = new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName, (DbDataReader reader, int column) => (TProperty)((object)this._spatialReader.Value.GetGeography(column)), (DbDataReader reader, int column) => this._spatialReader.Value.GetGeography(column)).GetValue(this.Reader, ordinal);
				}
				else
				{
					value = new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName, (DbDataReader reader, int column) => (TProperty)((object)this.Reader.GetValue(column)), (DbDataReader reader, int column) => this.Reader.GetValue(column)).GetValue(this.Reader, ordinal);
				}
			}
			else if (this.Streaming)
			{
				value = new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName, (DbDataReader reader, int column) => (TProperty)((object)this._spatialReader.Value.GetGeometry(column)), (DbDataReader reader, int column) => this._spatialReader.Value.GetGeometry(column)).GetValue(this.Reader, ordinal);
			}
			else
			{
				value = new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName, (DbDataReader reader, int column) => (TProperty)((object)this.Reader.GetValue(column)), (DbDataReader reader, int column) => this.Reader.GetValue(column)).GetValue(this.Reader, ordinal);
			}
			return value;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0008190C File Offset: 0x0007FB0C
		private void CheckClearedEntryOnSpan(object targetValue, IEntityWrapper wrappedSource, EntityKey sourceKey, AssociationEndMember targetMember)
		{
			if (sourceKey != null && targetValue == null && (this.MergeOption == MergeOption.PreserveChanges || this.MergeOption == MergeOption.OverwriteChanges))
			{
				AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(targetMember);
				EdmType elementType = ((RefType)otherAssociationEnd.TypeUsage.EdmType).ElementType;
				TypeUsage typeUsage;
				if (!this.Context.Perspective.TryGetType(wrappedSource.IdentityType, out typeUsage) || typeUsage.EdmType.EdmEquals(elementType) || TypeSemantics.IsSubTypeOf(typeUsage.EdmType, elementType))
				{
					this.CheckClearedEntryOnSpan(sourceKey, targetMember);
				}
			}
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00081990 File Offset: 0x0007FB90
		private void CheckClearedEntryOnSpan(EntityKey sourceKey, AssociationEndMember targetMember)
		{
			AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(targetMember);
			EntitySet entitySet;
			AssociationSet associationSet = this.Context.MetadataWorkspace.MetadataOptimization.FindCSpaceAssociationSet((AssociationType)otherAssociationEnd.DeclaringType, otherAssociationEnd.Name, sourceKey.EntitySetName, sourceKey.EntityContainerName, out entitySet);
			if (associationSet != null)
			{
				this.Context.ObjectStateManager.RemoveRelationships(this.MergeOption, associationSet, sourceKey, otherAssociationEnd);
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000819F8 File Offset: 0x0007FBF8
		private void FullSpanAction<TTargetEntity>(IEntityWrapper wrappedSource, IList<TTargetEntity> spannedEntities, AssociationEndMember targetMember)
		{
			if (wrappedSource.Entity != null)
			{
				AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(targetMember);
				RelatedEnd relatedEnd;
				if (this.TryGetRelatedEnd(wrappedSource, (AssociationType)targetMember.DeclaringType, otherAssociationEnd.Name, targetMember.Name, out relatedEnd))
				{
					int num = this.Context.ObjectStateManager.UpdateRelationships(this.Context, this.MergeOption, (AssociationSet)relatedEnd.RelationshipSet, otherAssociationEnd, wrappedSource, targetMember, (List<TTargetEntity>)spannedEntities, true);
					this.SetIsLoadedForSpan(relatedEnd, num > 0);
				}
			}
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00081A74 File Offset: 0x0007FC74
		private void UpdateEntry<TEntity>(IEntityWrapper wrappedEntity, EntityEntry existingEntry)
		{
			Type typeFromHandle = typeof(TEntity);
			if (typeFromHandle != existingEntry.WrappedEntity.IdentityType)
			{
				EntityKey entityKey = existingEntry.EntityKey;
				throw new NotSupportedException(Strings.Materializer_RecyclingEntity(TypeHelpers.GetFullName(entityKey.EntityContainerName, entityKey.EntitySetName), typeFromHandle.FullName, existingEntry.WrappedEntity.IdentityType.FullName, entityKey.ConcatKeyValue()));
			}
			if (EntityState.Added == existingEntry.State)
			{
				throw new InvalidOperationException(Strings.Materializer_AddedEntityAlreadyExists(existingEntry.EntityKey.ConcatKeyValue()));
			}
			if (this.MergeOption != MergeOption.AppendOnly)
			{
				if (MergeOption.OverwriteChanges == this.MergeOption)
				{
					if (EntityState.Deleted == existingEntry.State)
					{
						existingEntry.RevertDelete();
					}
					existingEntry.UpdateCurrentValueRecord(wrappedEntity.Entity);
					this.Context.ObjectStateManager.ForgetEntryWithConceptualNull(existingEntry, true);
					existingEntry.AcceptChanges();
					this.Context.ObjectStateManager.FixupReferencesByForeignKeys(existingEntry, true);
					return;
				}
				if (EntityState.Unchanged == existingEntry.State)
				{
					existingEntry.UpdateCurrentValueRecord(wrappedEntity.Entity);
					this.Context.ObjectStateManager.ForgetEntryWithConceptualNull(existingEntry, true);
					existingEntry.AcceptChanges();
					this.Context.ObjectStateManager.FixupReferencesByForeignKeys(existingEntry, true);
					return;
				}
				if (this.Context.ContextOptions.UseLegacyPreserveChangesBehavior)
				{
					existingEntry.UpdateRecordWithoutSetModified(wrappedEntity.Entity, existingEntry.EditableOriginalValues);
					return;
				}
				existingEntry.UpdateRecordWithSetModified(wrappedEntity.Entity, existingEntry.EditableOriginalValues);
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00081BD0 File Offset: 0x0007FDD0
		public void RaiseMaterializedEvents()
		{
			if (this._materializedEntities != null)
			{
				foreach (IEntityWrapper entityWrapper in this._materializedEntities)
				{
					this.Context.OnObjectMaterialized(entityWrapper.Entity);
				}
				this._materializedEntities.Clear();
			}
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00081C3C File Offset: 0x0007FE3C
		public void InitializeForOnMaterialize()
		{
			if (this.Context.OnMaterializedHasHandlers)
			{
				if (this._materializedEntities == null)
				{
					this._materializedEntities = new List<IEntityWrapper>();
					return;
				}
			}
			else if (this._materializedEntities != null)
			{
				this._materializedEntities = null;
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00081C6E File Offset: 0x0007FE6E
		protected void RegisterMaterializedEntityForEvent(IEntityWrapper wrappedEntity)
		{
			if (this._materializedEntities != null)
			{
				this._materializedEntities.Add(wrappedEntity);
			}
		}

		// Token: 0x040008FD RID: 2301
		private IList<IEntityWrapper> _materializedEntities;

		// Token: 0x040008FE RID: 2302
		public readonly DbDataReader Reader;

		// Token: 0x040008FF RID: 2303
		public readonly object[] State;

		// Token: 0x04000900 RID: 2304
		public readonly ObjectContext Context;

		// Token: 0x04000901 RID: 2305
		public readonly MetadataWorkspace Workspace;

		// Token: 0x04000902 RID: 2306
		public readonly MergeOption MergeOption;

		// Token: 0x04000903 RID: 2307
		protected readonly bool Streaming;

		// Token: 0x04000904 RID: 2308
		private readonly Lazy<DbSpatialDataReader> _spatialReader;

		// Token: 0x020002E3 RID: 739
		internal abstract class ErrorHandlingValueReader<T>
		{
			// Token: 0x06001A24 RID: 6692 RVA: 0x00081C84 File Offset: 0x0007FE84
			protected ErrorHandlingValueReader(Func<DbDataReader, int, T> typedValueAccessor, Func<DbDataReader, int, object> untypedValueAccessor)
			{
				this.getTypedValue = typedValueAccessor;
				this.getUntypedValue = untypedValueAccessor;
			}

			// Token: 0x06001A25 RID: 6693 RVA: 0x00081C9A File Offset: 0x0007FE9A
			protected ErrorHandlingValueReader() : this(new Func<DbDataReader, int, T>(Shaper.ErrorHandlingValueReader<T>.GetTypedValueDefault), new Func<DbDataReader, int, object>(Shaper.ErrorHandlingValueReader<T>.GetUntypedValueDefault))
			{
			}

			// Token: 0x06001A26 RID: 6694 RVA: 0x00081CBC File Offset: 0x0007FEBC
			private static T GetTypedValueDefault(DbDataReader reader, int ordinal)
			{
				Type underlyingType = Nullable.GetUnderlyingType(typeof(T));
				if (underlyingType != null && underlyingType.IsEnum())
				{
					MethodInfo genericTypedValueDefaultMethod = Shaper.ErrorHandlingValueReader<T>.GetGenericTypedValueDefaultMethod(underlyingType);
					return (T)((object)genericTypedValueDefaultMethod.Invoke(null, new object[]
					{
						reader,
						ordinal
					}));
				}
				bool flag;
				MethodInfo readerMethod = CodeGenEmitter.GetReaderMethod(typeof(T), out flag);
				return (T)((object)readerMethod.Invoke(reader, new object[]
				{
					ordinal
				}));
			}

			// Token: 0x06001A27 RID: 6695 RVA: 0x00081D50 File Offset: 0x0007FF50
			public static MethodInfo GetGenericTypedValueDefaultMethod(Type underlyingType)
			{
				return typeof(Shaper.ErrorHandlingValueReader<>).MakeGenericType(new Type[]
				{
					underlyingType
				}).GetOnlyDeclaredMethod("GetTypedValueDefault");
			}

			// Token: 0x06001A28 RID: 6696 RVA: 0x00081D82 File Offset: 0x0007FF82
			private static object GetUntypedValueDefault(DbDataReader reader, int ordinal)
			{
				return reader.GetValue(ordinal);
			}

			// Token: 0x06001A29 RID: 6697 RVA: 0x00081D8C File Offset: 0x0007FF8C
			internal T GetValue(DbDataReader reader, int ordinal)
			{
				if (reader.IsDBNull(ordinal))
				{
					try
					{
						return (T)((object)null);
					}
					catch (NullReferenceException)
					{
						throw this.CreateNullValueException();
					}
				}
				T result;
				try
				{
					result = this.getTypedValue(reader, ordinal);
				}
				catch (Exception e)
				{
					if (e.IsCatchableExceptionType())
					{
						object obj = this.getUntypedValue(reader, ordinal);
						Type type = (obj == null) ? null : obj.GetType();
						if (!typeof(T).IsAssignableFrom(type))
						{
							throw this.CreateWrongTypeException(type);
						}
					}
					throw;
				}
				return result;
			}

			// Token: 0x06001A2A RID: 6698
			protected abstract Exception CreateNullValueException();

			// Token: 0x06001A2B RID: 6699
			protected abstract Exception CreateWrongTypeException(Type resultType);

			// Token: 0x04000905 RID: 2309
			private readonly Func<DbDataReader, int, T> getTypedValue;

			// Token: 0x04000906 RID: 2310
			private readonly Func<DbDataReader, int, object> getUntypedValue;
		}

		// Token: 0x020002E4 RID: 740
		private class ColumnErrorHandlingValueReader<TColumn> : Shaper.ErrorHandlingValueReader<TColumn>
		{
			// Token: 0x06001A2C RID: 6700 RVA: 0x00081E20 File Offset: 0x00080020
			internal ColumnErrorHandlingValueReader()
			{
			}

			// Token: 0x06001A2D RID: 6701 RVA: 0x00081E28 File Offset: 0x00080028
			internal ColumnErrorHandlingValueReader(Func<DbDataReader, int, TColumn> typedAccessor, Func<DbDataReader, int, object> untypedAccessor) : base(typedAccessor, untypedAccessor)
			{
			}

			// Token: 0x06001A2E RID: 6702 RVA: 0x00081E32 File Offset: 0x00080032
			protected override Exception CreateNullValueException()
			{
				return new InvalidOperationException(Strings.Materializer_NullReferenceCast(typeof(TColumn)));
			}

			// Token: 0x06001A2F RID: 6703 RVA: 0x00081E48 File Offset: 0x00080048
			protected override Exception CreateWrongTypeException(Type resultType)
			{
				return EntityUtil.ValueInvalidCast(resultType, typeof(TColumn));
			}
		}

		// Token: 0x020002E5 RID: 741
		private class PropertyErrorHandlingValueReader<TProperty> : Shaper.ErrorHandlingValueReader<TProperty>
		{
			// Token: 0x06001A30 RID: 6704 RVA: 0x00081E5A File Offset: 0x0008005A
			internal PropertyErrorHandlingValueReader(string propertyName, string typeName)
			{
				this._propertyName = propertyName;
				this._typeName = typeName;
			}

			// Token: 0x06001A31 RID: 6705 RVA: 0x00081E70 File Offset: 0x00080070
			internal PropertyErrorHandlingValueReader(string propertyName, string typeName, Func<DbDataReader, int, TProperty> typedAccessor, Func<DbDataReader, int, object> untypedAccessor) : base(typedAccessor, untypedAccessor)
			{
				this._propertyName = propertyName;
				this._typeName = typeName;
			}

			// Token: 0x06001A32 RID: 6706 RVA: 0x00081E89 File Offset: 0x00080089
			protected override Exception CreateNullValueException()
			{
				return new ConstraintException(Strings.Materializer_SetInvalidValue(Nullable.GetUnderlyingType(typeof(TProperty)) ?? typeof(TProperty), this._typeName, this._propertyName, "null"));
			}

			// Token: 0x06001A33 RID: 6707 RVA: 0x00081EC3 File Offset: 0x000800C3
			protected override Exception CreateWrongTypeException(Type resultType)
			{
				return new InvalidOperationException(Strings.Materializer_SetInvalidValue(Nullable.GetUnderlyingType(typeof(TProperty)) ?? typeof(TProperty), this._typeName, this._propertyName, resultType));
			}

			// Token: 0x04000907 RID: 2311
			private readonly string _propertyName;

			// Token: 0x04000908 RID: 2312
			private readonly string _typeName;
		}
	}
}
