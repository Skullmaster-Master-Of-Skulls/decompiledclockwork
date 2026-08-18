using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Objects.Internal;
using System.Data.Spatial;
using System.Reflection;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003D0 RID: 976
	internal abstract class Shaper
	{
		// Token: 0x0600349E RID: 13470 RVA: 0x000CAFD4 File Offset: 0x000C91D4
		internal Shaper(DbDataReader reader, ObjectContext context, MetadataWorkspace workspace, MergeOption mergeOption, int stateCount)
		{
			this.Reader = reader;
			this.MergeOption = mergeOption;
			this.State = new object[stateCount];
			this.Context = context;
			this.Workspace = workspace;
			this.AssociationSpaceMap = new Dictionary<AssociationType, AssociationType>();
			this.spatialReader = new Singleton<DbSpatialDataReader>(new Func<DbSpatialDataReader>(this.CreateSpatialDataReader));
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000CB034 File Offset: 0x000C9234
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

		// Token: 0x060034A0 RID: 13472 RVA: 0x000CB07D File Offset: 0x000C927D
		public IEntityWrapper HandleEntityNoTracking<TEntity>(IEntityWrapper wrappedEntity)
		{
			this.RegisterMaterializedEntityForEvent(wrappedEntity);
			return wrappedEntity;
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000CB088 File Offset: 0x000C9288
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
						this.Context.ObjectStateManager.PromoteKeyEntry(entityEntry, wrappedEntity, null, false, true, false, "HandleEntity");
					}
				}
			}
			return entityWrapper;
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000CB10C File Offset: 0x000C930C
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
						throw EntityUtil.RecyclingEntity(entityEntry.EntityKey, typeof(TEntity), entityEntry.WrappedEntity.IdentityType);
					}
					if (EntityState.Added == entityEntry.State)
					{
						throw EntityUtil.AddedEntityAlreadyExists(entityEntry.EntityKey);
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
						this.Context.ObjectStateManager.PromoteKeyEntry(entityEntry, entityWrapper, null, false, true, false, "HandleEntity");
					}
				}
			}
			return entityWrapper;
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000CB1F4 File Offset: 0x000C93F4
		public IEntityWrapper HandleFullSpanCollection<T_SourceEntity, T_TargetEntity>(IEntityWrapper wrappedEntity, Coordinator<T_TargetEntity> coordinator, AssociationEndMember targetMember)
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

		// Token: 0x060034A4 RID: 13476 RVA: 0x000CB244 File Offset: 0x000C9444
		public IEntityWrapper HandleFullSpanElement<T_SourceEntity, T_TargetEntity>(IEntityWrapper wrappedSource, IEntityWrapper wrappedSpannedEntity, AssociationEndMember targetMember)
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

		// Token: 0x060034A5 RID: 13477 RVA: 0x000CB290 File Offset: 0x000C9490
		public IEntityWrapper HandleRelationshipSpan<T_SourceEntity>(IEntityWrapper wrappedEntity, EntityKey targetKey, AssociationEndMember targetMember)
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
				EntityContainer entityContainer = this.Context.MetadataWorkspace.GetEntityContainer(targetKey.EntityContainerName, DataSpace.CSpace);
				EntitySet entitySet;
				AssociationSet associationsForEntitySetAndAssociationType = MetadataHelper.GetAssociationsForEntitySetAndAssociationType(entityContainer, targetKey.EntitySetName, (AssociationType)targetMember.DeclaringType, targetMember.Name, out entitySet);
				ObjectStateManager objectStateManager = this.Context.ObjectStateManager;
				EntityState entityState;
				if (!ObjectStateManager.TryUpdateExistingRelationships(this.Context, this.MergeOption, associationsForEntitySetAndAssociationType, otherAssociationEnd, entityKey, wrappedEntity, targetMember, targetKey, true, out entityState))
				{
					EntityEntry entityEntry = null;
					if (!objectStateManager.TryGetEntityEntry(targetKey, out entityEntry))
					{
						entityEntry = objectStateManager.AddKeyEntry(targetKey, entitySet);
					}
					bool flag = true;
					RelationshipMultiplicity relationshipMultiplicity = otherAssociationEnd.RelationshipMultiplicity;
					if (relationshipMultiplicity > RelationshipMultiplicity.One)
					{
						if (relationshipMultiplicity != RelationshipMultiplicity.Many)
						{
						}
					}
					else
					{
						flag = !ObjectStateManager.TryUpdateExistingRelationships(this.Context, this.MergeOption, associationsForEntitySetAndAssociationType, targetMember, targetKey, entityEntry.WrappedEntity, otherAssociationEnd, entityKey, true, out entityState);
						if (entityEntry.State == EntityState.Detached)
						{
							entityEntry = objectStateManager.AddKeyEntry(targetKey, entitySet);
						}
					}
					if (flag)
					{
						if (entityEntry.IsKeyEntry || entityState == EntityState.Deleted)
						{
							RelationshipWrapper wrapper = new RelationshipWrapper(associationsForEntitySetAndAssociationType, otherAssociationEnd.Name, entityKey, targetMember.Name, targetKey);
							objectStateManager.AddNewRelation(wrapper, entityState);
						}
						else if (entityEntry.State != EntityState.Deleted)
						{
							ObjectStateManager.AddEntityToCollectionOrReference(this.MergeOption, wrappedEntity, otherAssociationEnd, entityEntry.WrappedEntity, targetMember, true, false, false);
						}
						else
						{
							RelationshipWrapper wrapper2 = new RelationshipWrapper(associationsForEntitySetAndAssociationType, otherAssociationEnd.Name, entityKey, targetMember.Name, targetKey);
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

		// Token: 0x060034A6 RID: 13478 RVA: 0x000CB448 File Offset: 0x000C9648
		private bool TryGetRelatedEnd(IEntityWrapper wrappedEntity, AssociationType associationType, string sourceEndName, string targetEndName, out RelatedEnd relatedEnd)
		{
			AssociationType item;
			if (!this.AssociationSpaceMap.TryGetValue(associationType, out item))
			{
				item = this.Workspace.GetItemCollection(DataSpace.OSpace).GetItem<AssociationType>(associationType.FullName);
				this.AssociationSpaceMap[associationType] = item;
			}
			AssociationEndMember associationEndMember = null;
			AssociationEndMember associationEndMember2 = null;
			foreach (AssociationEndMember associationEndMember3 in item.AssociationEndMembers)
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
					EntitySet entitySet = wrappedEntity.EntityKey.GetEntitySet(this.Workspace);
					Tuple<string, string, string> item2 = Tuple.Create<string, string, string>(entitySet.Identity, associationType.Identity, sourceEndName);
					if (this._relatedEndCache == null)
					{
						this._relatedEndCache = new HashSet<Tuple<string, string, string>>();
					}
					if (this._relatedEndCache.Contains(item2))
					{
						flag = true;
					}
					else
					{
						foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
						{
							if (entitySetBase.ElementType == associationType && ((AssociationSet)entitySetBase).AssociationSetEnds[sourceEndName].EntitySet == entitySet)
							{
								flag = true;
								this._relatedEndCache.Add(item2);
								break;
							}
						}
					}
				}
				if (flag)
				{
					relatedEnd = LightweightCodeGenerator.GetRelatedEnd(wrappedEntity.RelationshipManager, associationEndMember, associationEndMember2, null);
					return true;
				}
			}
			relatedEnd = null;
			return false;
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000CB600 File Offset: 0x000C9800
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
				relatedEnd.SetIsLoaded(true);
			}
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000CB645 File Offset: 0x000C9845
		public IEntityWrapper HandleIEntityWithKey<TEntity>(IEntityWrapper wrappedEntity, EntitySet entitySet)
		{
			return this.HandleEntity<TEntity>(wrappedEntity, wrappedEntity.EntityKey, entitySet);
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000CB658 File Offset: 0x000C9858
		public bool SetColumnValue(int recordStateSlotNumber, int ordinal, object value)
		{
			RecordState recordState = (RecordState)this.State[recordStateSlotNumber];
			recordState.SetColumnValue(ordinal, value);
			return true;
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000CB680 File Offset: 0x000C9880
		public bool SetEntityRecordInfo(int recordStateSlotNumber, EntityKey entityKey, EntitySet entitySet)
		{
			RecordState recordState = (RecordState)this.State[recordStateSlotNumber];
			recordState.SetEntityRecordInfo(entityKey, entitySet);
			return true;
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000CB6A5 File Offset: 0x000C98A5
		public bool SetState<T>(int ordinal, T value)
		{
			this.State[ordinal] = value;
			return true;
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000CB6B6 File Offset: 0x000C98B6
		public T SetStatePassthrough<T>(int ordinal, T value)
		{
			this.State[ordinal] = value;
			return value;
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000CB6C8 File Offset: 0x000C98C8
		public TProperty GetPropertyValueWithErrorHandling<TProperty>(int ordinal, string propertyName, string typeName)
		{
			return new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName).GetValue(this.Reader, ordinal);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000CB6EC File Offset: 0x000C98EC
		public TColumn GetColumnValueWithErrorHandling<TColumn>(int ordinal)
		{
			return new Shaper.ColumnErrorHandlingValueReader<TColumn>().GetValue(this.Reader, ordinal);
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000CB70C File Offset: 0x000C990C
		private DbSpatialDataReader CreateSpatialDataReader()
		{
			return SpatialHelpers.CreateSpatialDataReader(this.Workspace, this.Reader);
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000CB71F File Offset: 0x000C991F
		public DbGeography GetGeographyColumnValue(int ordinal)
		{
			return this.spatialReader.Value.GetGeography(ordinal);
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000CB732 File Offset: 0x000C9932
		public DbGeometry GetGeometryColumnValue(int ordinal)
		{
			return this.spatialReader.Value.GetGeometry(ordinal);
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000CB748 File Offset: 0x000C9948
		public TColumn GetSpatialColumnValueWithErrorHandling<TColumn>(int ordinal, PrimitiveTypeKind spatialTypeKind)
		{
			TColumn value;
			if (spatialTypeKind == PrimitiveTypeKind.Geography)
			{
				value = new Shaper.ColumnErrorHandlingValueReader<TColumn>((DbDataReader reader, int column) => (TColumn)((object)this.spatialReader.Value.GetGeography(column)), (DbDataReader reader, int column) => this.spatialReader.Value.GetGeography(column)).GetValue(this.Reader, ordinal);
			}
			else
			{
				value = new Shaper.ColumnErrorHandlingValueReader<TColumn>((DbDataReader reader, int column) => (TColumn)((object)this.spatialReader.Value.GetGeometry(column)), (DbDataReader reader, int column) => this.spatialReader.Value.GetGeometry(column)).GetValue(this.Reader, ordinal);
			}
			return value;
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000CB7B4 File Offset: 0x000C99B4
		public TProperty GetSpatialPropertyValueWithErrorHandling<TProperty>(int ordinal, string propertyName, string typeName, PrimitiveTypeKind spatialTypeKind)
		{
			TProperty value;
			if (Helper.IsGeographicTypeKind(spatialTypeKind))
			{
				value = new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName, (DbDataReader reader, int column) => (TProperty)((object)this.spatialReader.Value.GetGeography(column)), (DbDataReader reader, int column) => this.spatialReader.Value.GetGeography(column)).GetValue(this.Reader, ordinal);
			}
			else
			{
				value = new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName, (DbDataReader reader, int column) => (TProperty)((object)this.spatialReader.Value.GetGeometry(column)), (DbDataReader reader, int column) => this.spatialReader.Value.GetGeometry(column)).GetValue(this.Reader, ordinal);
			}
			return value;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000CB828 File Offset: 0x000C9A28
		private void CheckClearedEntryOnSpan(object targetValue, IEntityWrapper wrappedSource, EntityKey sourceKey, AssociationEndMember targetMember)
		{
			if (sourceKey != null && targetValue == null && (this.MergeOption == MergeOption.PreserveChanges || this.MergeOption == MergeOption.OverwriteChanges))
			{
				AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(targetMember);
				EdmType elementType = ((RefType)otherAssociationEnd.TypeUsage.EdmType).ElementType;
				TypeUsage typeUsage;
				if (!this.Context.Perspective.TryGetType(wrappedSource.IdentityType, out typeUsage) || typeUsage.EdmType.EdmEquals(elementType) || TypeSemantics.IsSubTypeOf(typeUsage.EdmType, elementType))
				{
					this.CheckClearedEntryOnSpan(sourceKey, wrappedSource, targetMember);
				}
			}
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000CB8AC File Offset: 0x000C9AAC
		private void CheckClearedEntryOnSpan(EntityKey sourceKey, IEntityWrapper wrappedSource, AssociationEndMember targetMember)
		{
			AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(targetMember);
			EntityContainer entityContainer = this.Context.MetadataWorkspace.GetEntityContainer(sourceKey.EntityContainerName, DataSpace.CSpace);
			EntitySet entitySet;
			AssociationSet associationsForEntitySetAndAssociationType = MetadataHelper.GetAssociationsForEntitySetAndAssociationType(entityContainer, sourceKey.EntitySetName, (AssociationType)otherAssociationEnd.DeclaringType, otherAssociationEnd.Name, out entitySet);
			if (associationsForEntitySetAndAssociationType != null)
			{
				ObjectStateManager.RemoveRelationships(this.Context, this.MergeOption, associationsForEntitySetAndAssociationType, sourceKey, otherAssociationEnd);
			}
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000CB910 File Offset: 0x000C9B10
		private void FullSpanAction<T_TargetEntity>(IEntityWrapper wrappedSource, IList<T_TargetEntity> spannedEntities, AssociationEndMember targetMember)
		{
			if (wrappedSource.Entity != null)
			{
				EntityKey entityKey = wrappedSource.EntityKey;
				AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(targetMember);
				RelatedEnd relatedEnd;
				if (this.TryGetRelatedEnd(wrappedSource, (AssociationType)targetMember.DeclaringType, otherAssociationEnd.Name, targetMember.Name, out relatedEnd))
				{
					int num = ObjectStateManager.UpdateRelationships(this.Context, this.MergeOption, (AssociationSet)relatedEnd.RelationshipSet, otherAssociationEnd, entityKey, wrappedSource, targetMember, (List<T_TargetEntity>)spannedEntities, true);
					this.SetIsLoadedForSpan(relatedEnd, num > 0);
				}
			}
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000CB988 File Offset: 0x000C9B88
		private void UpdateEntry<TEntity>(IEntityWrapper wrappedEntity, EntityEntry existingEntry)
		{
			Type typeFromHandle = typeof(TEntity);
			if (typeFromHandle != existingEntry.WrappedEntity.IdentityType)
			{
				throw EntityUtil.RecyclingEntity(existingEntry.EntityKey, typeFromHandle, existingEntry.WrappedEntity.IdentityType);
			}
			if (EntityState.Added == existingEntry.State)
			{
				throw EntityUtil.AddedEntityAlreadyExists(existingEntry.EntityKey);
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

		// Token: 0x060034B8 RID: 13496 RVA: 0x000CBAB4 File Offset: 0x000C9CB4
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

		// Token: 0x060034B9 RID: 13497 RVA: 0x000CBB20 File Offset: 0x000C9D20
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

		// Token: 0x060034BA RID: 13498 RVA: 0x000CBB52 File Offset: 0x000C9D52
		protected void RegisterMaterializedEntityForEvent(IEntityWrapper wrappedEntity)
		{
			if (this._materializedEntities != null)
			{
				this._materializedEntities.Add(wrappedEntity);
			}
		}

		// Token: 0x04001710 RID: 5904
		private IList<IEntityWrapper> _materializedEntities;

		// Token: 0x04001711 RID: 5905
		public readonly DbDataReader Reader;

		// Token: 0x04001712 RID: 5906
		public readonly object[] State;

		// Token: 0x04001713 RID: 5907
		public readonly ObjectContext Context;

		// Token: 0x04001714 RID: 5908
		public readonly MetadataWorkspace Workspace;

		// Token: 0x04001715 RID: 5909
		public readonly MergeOption MergeOption;

		// Token: 0x04001716 RID: 5910
		private readonly Dictionary<AssociationType, AssociationType> AssociationSpaceMap;

		// Token: 0x04001717 RID: 5911
		private HashSet<Tuple<string, string, string>> _relatedEndCache;

		// Token: 0x04001718 RID: 5912
		private readonly Singleton<DbSpatialDataReader> spatialReader;

		// Token: 0x02000696 RID: 1686
		private abstract class ErrorHandlingValueReader<T>
		{
			// Token: 0x06004548 RID: 17736 RVA: 0x000F9860 File Offset: 0x000F7A60
			protected ErrorHandlingValueReader(Func<DbDataReader, int, T> typedValueAccessor, Func<DbDataReader, int, object> untypedValueAccessor)
			{
				this.getTypedValue = typedValueAccessor;
				this.getUntypedValue = untypedValueAccessor;
			}

			// Token: 0x06004549 RID: 17737 RVA: 0x000F9876 File Offset: 0x000F7A76
			protected ErrorHandlingValueReader() : this(new Func<DbDataReader, int, T>(Shaper.ErrorHandlingValueReader<T>.GetTypedValueDefault), new Func<DbDataReader, int, object>(Shaper.ErrorHandlingValueReader<T>.GetUntypedValueDefault))
			{
			}

			// Token: 0x0600454A RID: 17738 RVA: 0x000F9898 File Offset: 0x000F7A98
			private static T GetTypedValueDefault(DbDataReader reader, int ordinal)
			{
				Type underlyingType = Nullable.GetUnderlyingType(typeof(T));
				if (underlyingType != null && underlyingType.IsEnum)
				{
					Type type = typeof(Shaper.ErrorHandlingValueReader<>).MakeGenericType(new Type[]
					{
						underlyingType
					});
					return (T)((object)type.GetMethod(MethodBase.GetCurrentMethod().Name, BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[]
					{
						reader,
						ordinal
					}));
				}
				bool flag;
				MethodInfo readerMethod = Translator.GetReaderMethod(typeof(T), out flag);
				return (T)((object)readerMethod.Invoke(reader, new object[]
				{
					ordinal
				}));
			}

			// Token: 0x0600454B RID: 17739 RVA: 0x000F9942 File Offset: 0x000F7B42
			private static object GetUntypedValueDefault(DbDataReader reader, int ordinal)
			{
				return reader.GetValue(ordinal);
			}

			// Token: 0x0600454C RID: 17740 RVA: 0x000F994C File Offset: 0x000F7B4C
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
					if (EntityUtil.IsCatchableExceptionType(e))
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

			// Token: 0x0600454D RID: 17741
			protected abstract Exception CreateNullValueException();

			// Token: 0x0600454E RID: 17742
			protected abstract Exception CreateWrongTypeException(Type resultType);

			// Token: 0x04001FFE RID: 8190
			private readonly Func<DbDataReader, int, T> getTypedValue;

			// Token: 0x04001FFF RID: 8191
			private readonly Func<DbDataReader, int, object> getUntypedValue;
		}

		// Token: 0x02000697 RID: 1687
		private class ColumnErrorHandlingValueReader<TColumn> : Shaper.ErrorHandlingValueReader<TColumn>
		{
			// Token: 0x0600454F RID: 17743 RVA: 0x000F99E4 File Offset: 0x000F7BE4
			internal ColumnErrorHandlingValueReader()
			{
			}

			// Token: 0x06004550 RID: 17744 RVA: 0x000F99EC File Offset: 0x000F7BEC
			internal ColumnErrorHandlingValueReader(Func<DbDataReader, int, TColumn> typedAccessor, Func<DbDataReader, int, object> untypedAccessor) : base(typedAccessor, untypedAccessor)
			{
			}

			// Token: 0x06004551 RID: 17745 RVA: 0x000F99F6 File Offset: 0x000F7BF6
			protected override Exception CreateNullValueException()
			{
				return EntityUtil.ValueNullReferenceCast(typeof(TColumn));
			}

			// Token: 0x06004552 RID: 17746 RVA: 0x000F9A07 File Offset: 0x000F7C07
			protected override Exception CreateWrongTypeException(Type resultType)
			{
				return EntityUtil.ValueInvalidCast(resultType, typeof(TColumn));
			}
		}

		// Token: 0x02000698 RID: 1688
		private class PropertyErrorHandlingValueReader<TProperty> : Shaper.ErrorHandlingValueReader<TProperty>
		{
			// Token: 0x06004553 RID: 17747 RVA: 0x000F9A19 File Offset: 0x000F7C19
			internal PropertyErrorHandlingValueReader(string propertyName, string typeName)
			{
				this._propertyName = propertyName;
				this._typeName = typeName;
			}

			// Token: 0x06004554 RID: 17748 RVA: 0x000F9A2F File Offset: 0x000F7C2F
			internal PropertyErrorHandlingValueReader(string propertyName, string typeName, Func<DbDataReader, int, TProperty> typedAccessor, Func<DbDataReader, int, object> untypedAccessor) : base(typedAccessor, untypedAccessor)
			{
				this._propertyName = propertyName;
				this._typeName = typeName;
			}

			// Token: 0x06004555 RID: 17749 RVA: 0x000F9A48 File Offset: 0x000F7C48
			protected override Exception CreateNullValueException()
			{
				return EntityUtil.Constraint(Strings.Materializer_SetInvalidValue((Nullable.GetUnderlyingType(typeof(TProperty)) ?? typeof(TProperty)).Name, this._typeName, this._propertyName, "null"));
			}

			// Token: 0x06004556 RID: 17750 RVA: 0x000F9A87 File Offset: 0x000F7C87
			protected override Exception CreateWrongTypeException(Type resultType)
			{
				return EntityUtil.InvalidOperation(Strings.Materializer_SetInvalidValue((Nullable.GetUnderlyingType(typeof(TProperty)) ?? typeof(TProperty)).Name, this._typeName, this._propertyName, resultType.Name));
			}

			// Token: 0x04002000 RID: 8192
			private readonly string _propertyName;

			// Token: 0x04002001 RID: 8193
			private readonly string _typeName;
		}
	}
}
