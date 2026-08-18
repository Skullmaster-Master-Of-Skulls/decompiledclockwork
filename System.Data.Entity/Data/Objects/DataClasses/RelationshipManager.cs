using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Data.Objects.Internal;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000190 RID: 400
	[Serializable]
	public class RelationshipManager
	{
		// Token: 0x06001CB9 RID: 7353 RVA: 0x00002050 File Offset: 0x00000250
		private RelationshipManager()
		{
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x000620D1 File Offset: 0x000602D1
		internal IEnumerable<RelatedEnd> Relationships
		{
			get
			{
				this.EnsureRelationshipsInitialized();
				return this._relationships.ToArray();
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x000620E4 File Offset: 0x000602E4
		private void EnsureRelationshipsInitialized()
		{
			if (this._relationships == null)
			{
				this._relationships = new List<RelatedEnd>();
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x000620F9 File Offset: 0x000602F9
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x00062101 File Offset: 0x00060301
		internal bool NodeVisited
		{
			get
			{
				return this._nodeVisited;
			}
			set
			{
				this._nodeVisited = value;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x0006210A File Offset: 0x0006030A
		internal IEntityWrapper WrappedOwner
		{
			get
			{
				if (this._wrappedOwner == null)
				{
					this._wrappedOwner = EntityWrapperFactory.CreateNewWrapper(this._owner, null);
				}
				return this._wrappedOwner;
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0006212C File Offset: 0x0006032C
		public static RelationshipManager Create(IEntityWithRelationships owner)
		{
			EntityUtil.CheckArgumentNull<IEntityWithRelationships>(owner, "owner");
			return new RelationshipManager
			{
				_owner = owner
			};
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00062153 File Offset: 0x00060353
		internal static RelationshipManager Create()
		{
			return new RelationshipManager();
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0006215C File Offset: 0x0006035C
		internal void SetWrappedOwner(IEntityWrapper wrappedOwner, object expectedOwner)
		{
			this._wrappedOwner = wrappedOwner;
			if (this._owner != null && expectedOwner != this._owner)
			{
				throw EntityUtil.InvalidRelationshipManagerOwner();
			}
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this._relationships)
				{
					relatedEnd.SetWrappedOwner(wrappedOwner);
				}
			}
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000621D8 File Offset: 0x000603D8
		internal EntityCollection<TTargetEntity> GetRelatedCollection<TSourceEntity, TTargetEntity>(string relationshipName, string sourceRoleName, string targetRoleName, NavigationPropertyAccessor sourceAccessor, NavigationPropertyAccessor targetAccessor, RelationshipMultiplicity sourceRoleMultiplicity, RelatedEnd existingRelatedEnd) where TSourceEntity : class where TTargetEntity : class
		{
			RelatedEnd relatedEnd;
			this.TryGetCachedRelatedEnd(relationshipName, targetRoleName, out relatedEnd);
			if (existingRelatedEnd != null)
			{
				if (relatedEnd != null)
				{
					this._relationships.Remove(relatedEnd);
				}
				RelationshipNavigation navigation = new RelationshipNavigation(relationshipName, sourceRoleName, targetRoleName, sourceAccessor, targetAccessor);
				EntityCollection<TTargetEntity> entityCollection = this.CreateRelatedEnd<TSourceEntity, TTargetEntity>(navigation, sourceRoleMultiplicity, RelationshipMultiplicity.Many, existingRelatedEnd) as EntityCollection<TTargetEntity>;
				if (entityCollection != null)
				{
					bool flag = true;
					try
					{
						this.RemergeCollections<TTargetEntity>(relatedEnd as EntityCollection<TTargetEntity>, entityCollection);
						flag = false;
					}
					finally
					{
						if (flag && relatedEnd != null)
						{
							this._relationships.Remove(entityCollection);
							this._relationships.Add(relatedEnd);
						}
					}
				}
				return entityCollection;
			}
			if (relatedEnd != null)
			{
				EntityCollection<TTargetEntity> entityCollection = relatedEnd as EntityCollection<TTargetEntity>;
				return entityCollection;
			}
			RelationshipNavigation navigation2 = new RelationshipNavigation(relationshipName, sourceRoleName, targetRoleName, sourceAccessor, targetAccessor);
			return this.CreateRelatedEnd<TSourceEntity, TTargetEntity>(navigation2, sourceRoleMultiplicity, RelationshipMultiplicity.Many, existingRelatedEnd) as EntityCollection<TTargetEntity>;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00062298 File Offset: 0x00060498
		private void RemergeCollections<TTargetEntity>(EntityCollection<TTargetEntity> previousCollection, EntityCollection<TTargetEntity> collection) where TTargetEntity : class
		{
			int num = 0;
			List<IEntityWrapper> list = new List<IEntityWrapper>(collection.CountInternal);
			foreach (IEntityWrapper item in collection.GetWrappedEntities())
			{
				list.Add(item);
			}
			foreach (IEntityWrapper wrappedEntity in list)
			{
				bool flag = true;
				if (previousCollection != null && previousCollection.ContainsEntity(wrappedEntity))
				{
					num++;
					flag = false;
				}
				if (flag)
				{
					collection.Remove(wrappedEntity, false);
					collection.Add(wrappedEntity);
				}
			}
			if (previousCollection != null && num != previousCollection.CountInternal)
			{
				throw EntityUtil.CannotRemergeCollections();
			}
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0006236C File Offset: 0x0006056C
		internal EntityReference<TTargetEntity> GetRelatedReference<TSourceEntity, TTargetEntity>(string relationshipName, string sourceRoleName, string targetRoleName, NavigationPropertyAccessor sourceAccessor, NavigationPropertyAccessor targetAccessor, RelationshipMultiplicity sourceRoleMultiplicity, RelatedEnd existingRelatedEnd) where TSourceEntity : class where TTargetEntity : class
		{
			RelatedEnd relatedEnd;
			if (this.TryGetCachedRelatedEnd(relationshipName, targetRoleName, out relatedEnd))
			{
				return relatedEnd as EntityReference<TTargetEntity>;
			}
			RelationshipNavigation navigation = new RelationshipNavigation(relationshipName, sourceRoleName, targetRoleName, sourceAccessor, targetAccessor);
			return this.CreateRelatedEnd<TSourceEntity, TTargetEntity>(navigation, sourceRoleMultiplicity, RelationshipMultiplicity.One, existingRelatedEnd) as EntityReference<TTargetEntity>;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x000623AC File Offset: 0x000605AC
		internal RelatedEnd GetRelatedEnd(string navigationProperty, bool throwArgumentException = false)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			EntityType item = wrappedOwner.Context.MetadataWorkspace.GetItem<EntityType>(wrappedOwner.IdentityType.FullName, DataSpace.OSpace);
			EdmMember edmMember;
			if (!wrappedOwner.Context.Perspective.TryGetMember(item, navigationProperty, false, out edmMember) || !(edmMember is NavigationProperty))
			{
				string message = Strings.RelationshipManager_NavigationPropertyNotFound(navigationProperty);
				throw throwArgumentException ? new ArgumentException(message) : new InvalidOperationException(message);
			}
			NavigationProperty navigationProperty2 = (NavigationProperty)edmMember;
			return this.GetRelatedEndInternal(navigationProperty2.RelationshipType.FullName, navigationProperty2.ToEndMember.Name);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0006243B File Offset: 0x0006063B
		public IRelatedEnd GetRelatedEnd(string relationshipName, string targetRoleName)
		{
			return this.GetRelatedEndInternal(this.PrependNamespaceToRelationshipName(relationshipName), targetRoleName);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0006244C File Offset: 0x0006064C
		internal RelatedEnd GetRelatedEndInternal(string relationshipName, string targetRoleName)
		{
			EntityUtil.CheckArgumentNull<string>(relationshipName, "relationshipName");
			EntityUtil.CheckArgumentNull<string>(targetRoleName, "targetRoleName");
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context == null && wrappedOwner.RequiresRelationshipChangeTracking)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_CannotGetRelatEndForDetachedPocoEntity);
			}
			RelatedEnd relatedEnd = null;
			AssociationType relationship = null;
			if (!RelationshipManager.TryGetRelationshipType(wrappedOwner, wrappedOwner.IdentityType, relationshipName, out relationship))
			{
				if (this._relationships != null)
				{
					relatedEnd = (from RelatedEnd end in this._relationships
					where end.RelationshipName == relationshipName && end.TargetRoleName == targetRoleName
					select end).FirstOrDefault<RelatedEnd>();
				}
				if (relatedEnd == null && !EntityProxyFactory.TryGetAssociationTypeFromProxyInfo(wrappedOwner, relationshipName, targetRoleName, out relationship))
				{
					throw RelationshipManager.UnableToGetMetadata(this.WrappedOwner, relationshipName);
				}
			}
			if (relatedEnd == null)
			{
				relatedEnd = this.GetRelatedEndInternal(relationshipName, targetRoleName, null, relationship);
			}
			return relatedEnd;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00062539 File Offset: 0x00060739
		private RelatedEnd GetRelatedEndInternal(string relationshipName, string targetRoleName, RelatedEnd existingRelatedEnd, AssociationType relationship)
		{
			return this.GetRelatedEndInternal(relationshipName, targetRoleName, existingRelatedEnd, relationship, true);
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00062548 File Offset: 0x00060748
		private RelatedEnd GetRelatedEndInternal(string relationshipName, string targetRoleName, RelatedEnd existingRelatedEnd, AssociationType relationship, bool throwOnError)
		{
			RelatedEnd result = null;
			AssociationEndMember associationEndMember = relationship.AssociationEndMembers[1];
			AssociationEndMember associationEndMember2;
			if (associationEndMember.Identity != targetRoleName)
			{
				associationEndMember2 = associationEndMember;
				associationEndMember = relationship.AssociationEndMembers[0];
				if (associationEndMember.Identity != targetRoleName)
				{
					if (throwOnError)
					{
						throw EntityUtil.InvalidTargetRole(relationshipName, targetRoleName, "targetRoleName");
					}
					return result;
				}
			}
			else
			{
				associationEndMember2 = relationship.AssociationEndMembers[0];
			}
			EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(associationEndMember2);
			Type clrType = entityTypeForEnd.ClrType;
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (!clrType.IsAssignableFrom(wrappedOwner.IdentityType))
			{
				if (throwOnError)
				{
					throw EntityUtil.OwnerIsNotSourceType(wrappedOwner.IdentityType.FullName, clrType.FullName, associationEndMember2.Name, relationshipName);
				}
			}
			else if (this.VerifyRelationship(relationship, associationEndMember2.Name, throwOnError))
			{
				result = LightweightCodeGenerator.GetRelatedEnd(this, associationEndMember2, associationEndMember, existingRelatedEnd);
			}
			return result;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0006261C File Offset: 0x0006081C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InitializeRelatedReference<TTargetEntity>(string relationshipName, string targetRoleName, EntityReference<TTargetEntity> entityReference) where TTargetEntity : class
		{
			EntityUtil.CheckArgumentNull<string>(relationshipName, "relationshipName");
			EntityUtil.CheckArgumentNull<string>(targetRoleName, "targetRoleName");
			EntityUtil.CheckArgumentNull<EntityReference<TTargetEntity>>(entityReference, "entityReference");
			if (entityReference.WrappedOwner.Entity != null)
			{
				throw EntityUtil.ReferenceAlreadyInitialized();
			}
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context != null && wrappedOwner.MergeOption != MergeOption.NoTracking)
			{
				throw EntityUtil.RelationshipManagerAttached();
			}
			relationshipName = this.PrependNamespaceToRelationshipName(relationshipName);
			AssociationType relationshipType = this.GetRelationshipType(wrappedOwner.IdentityType, relationshipName);
			RelatedEnd relatedEnd;
			if (this.TryGetCachedRelatedEnd(relationshipName, targetRoleName, out relatedEnd))
			{
				if (!relatedEnd.IsEmpty())
				{
					entityReference.InitializeWithValue(relatedEnd);
				}
				this._relationships.Remove(relatedEnd);
			}
			if (!(this.GetRelatedEndInternal(relationshipName, targetRoleName, entityReference, relationshipType) is EntityReference<TTargetEntity>))
			{
				throw EntityUtil.ExpectedReferenceGotCollection(typeof(TTargetEntity).Name, targetRoleName, relationshipName);
			}
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x000626E8 File Offset: 0x000608E8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InitializeRelatedCollection<TTargetEntity>(string relationshipName, string targetRoleName, EntityCollection<TTargetEntity> entityCollection) where TTargetEntity : class
		{
			EntityUtil.CheckArgumentNull<string>(relationshipName, "relationshipName");
			EntityUtil.CheckArgumentNull<string>(targetRoleName, "targetRoleName");
			EntityUtil.CheckArgumentNull<EntityCollection<TTargetEntity>>(entityCollection, "entityCollection");
			if (entityCollection.WrappedOwner.Entity != null)
			{
				throw EntityUtil.CollectionAlreadyInitialized();
			}
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context != null && wrappedOwner.MergeOption != MergeOption.NoTracking)
			{
				throw EntityUtil.CollectionRelationshipManagerAttached();
			}
			relationshipName = this.PrependNamespaceToRelationshipName(relationshipName);
			AssociationType relationshipType = this.GetRelationshipType(wrappedOwner.IdentityType, relationshipName);
			if (!(this.GetRelatedEndInternal(relationshipName, targetRoleName, entityCollection, relationshipType) is EntityCollection<TTargetEntity>))
			{
				throw EntityUtil.ExpectedCollectionGotReference(typeof(TTargetEntity).Name, targetRoleName, relationshipName);
			}
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0006278C File Offset: 0x0006098C
		private string PrependNamespaceToRelationshipName(string relationshipName)
		{
			EntityUtil.CheckArgumentNull<string>(relationshipName, "relationshipName");
			if (!relationshipName.Contains('.'))
			{
				string fullName = this.WrappedOwner.IdentityType.FullName;
				ObjectItemCollection objectItemCollection = RelationshipManager.GetObjectItemCollection(this.WrappedOwner);
				EdmType edmType = null;
				if (objectItemCollection != null)
				{
					objectItemCollection.TryGetItem<EdmType>(fullName, out edmType);
				}
				else
				{
					Dictionary<string, EdmType> dictionary = ObjectItemCollection.LoadTypesExpensiveWay(this.WrappedOwner.IdentityType.Assembly);
					if (dictionary != null)
					{
						dictionary.TryGetValue(fullName, out edmType);
					}
				}
				ClrEntityType clrEntityType = edmType as ClrEntityType;
				if (clrEntityType != null)
				{
					string cspaceNamespaceName = clrEntityType.CSpaceNamespaceName;
					return cspaceNamespaceName + "." + relationshipName;
				}
			}
			return relationshipName;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00062824 File Offset: 0x00060A24
		private static ObjectItemCollection GetObjectItemCollection(IEntityWrapper wrappedOwner)
		{
			if (wrappedOwner.Context != null && wrappedOwner.Context.MetadataWorkspace != null)
			{
				return (ObjectItemCollection)wrappedOwner.Context.MetadataWorkspace.GetItemCollection(DataSpace.OSpace);
			}
			return null;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00062854 File Offset: 0x00060A54
		private bool TryGetOwnerEntityType(out EntityType entityType)
		{
			DefaultObjectMappingItemCollection defaultObjectMappingItemCollection;
			Map map;
			if (RelationshipManager.TryGetObjectMappingItemCollection(this.WrappedOwner, out defaultObjectMappingItemCollection) && defaultObjectMappingItemCollection.TryGetMap(this.WrappedOwner.IdentityType.FullName, DataSpace.OSpace, out map))
			{
				ObjectTypeMapping objectTypeMapping = (ObjectTypeMapping)map;
				if (Helper.IsEntityType(objectTypeMapping.EdmType))
				{
					entityType = (EntityType)objectTypeMapping.EdmType;
					return true;
				}
			}
			entityType = null;
			return false;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x000628B2 File Offset: 0x00060AB2
		private static bool TryGetObjectMappingItemCollection(IEntityWrapper wrappedOwner, out DefaultObjectMappingItemCollection collection)
		{
			if (wrappedOwner.Context != null && wrappedOwner.Context.MetadataWorkspace != null)
			{
				collection = (DefaultObjectMappingItemCollection)wrappedOwner.Context.MetadataWorkspace.GetItemCollection(DataSpace.OCSpace);
				return collection != null;
			}
			collection = null;
			return false;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000628EC File Offset: 0x00060AEC
		internal static bool TryGetRelationshipType(IEntityWrapper wrappedOwner, Type entityClrType, string relationshipName, out AssociationType associationType)
		{
			ObjectItemCollection objectItemCollection = RelationshipManager.GetObjectItemCollection(wrappedOwner);
			if (objectItemCollection != null)
			{
				associationType = objectItemCollection.GetRelationshipType(entityClrType, relationshipName);
			}
			else
			{
				associationType = ObjectItemCollection.GetRelationshipTypeExpensiveWay(entityClrType, relationshipName);
			}
			return associationType != null;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00062920 File Offset: 0x00060B20
		private AssociationType GetRelationshipType(Type entityClrType, string relationshipName)
		{
			AssociationType result = null;
			if (!RelationshipManager.TryGetRelationshipType(this.WrappedOwner, entityClrType, relationshipName, out result))
			{
				throw RelationshipManager.UnableToGetMetadata(this.WrappedOwner, relationshipName);
			}
			return result;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00062950 File Offset: 0x00060B50
		internal static Exception UnableToGetMetadata(IEntityWrapper wrappedOwner, string relationshipName)
		{
			ArgumentException ex = EntityUtil.UnableToFindRelationshipTypeInMetadata(relationshipName, "relationshipName");
			if (EntityProxyFactory.IsProxyType(wrappedOwner.Entity.GetType()))
			{
				return EntityUtil.ProxyMetadataIsUnavailable(wrappedOwner.IdentityType, ex);
			}
			return ex;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00062989 File Offset: 0x00060B89
		private IEnumerable<AssociationEndMember> GetAllTargetEnds(EntityType ownerEntityType, EntitySet ownerEntitySet)
		{
			foreach (AssociationSet assocSet in MetadataHelper.GetAssociationsForEntitySet(ownerEntitySet))
			{
				EntityType entityType = assocSet.ElementType.AssociationEndMembers[1].GetEntityType();
				if (entityType.IsAssignableFrom(ownerEntityType))
				{
					yield return assocSet.ElementType.AssociationEndMembers[0];
				}
				EntityType entityType2 = assocSet.ElementType.AssociationEndMembers[0].GetEntityType();
				if (entityType2.IsAssignableFrom(ownerEntityType))
				{
					yield return assocSet.ElementType.AssociationEndMembers[1];
				}
				assocSet = null;
			}
			List<AssociationSet>.Enumerator enumerator = default(List<AssociationSet>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000629A0 File Offset: 0x00060BA0
		private IEnumerable<AssociationEndMember> GetAllTargetEnds(Type entityClrType)
		{
			ObjectItemCollection objectItemCollection = RelationshipManager.GetObjectItemCollection(this.WrappedOwner);
			IEnumerable<AssociationType> enumerable;
			if (objectItemCollection != null)
			{
				enumerable = objectItemCollection.GetItems<AssociationType>();
			}
			else
			{
				enumerable = ObjectItemCollection.GetAllRelationshipTypesExpensiveWay(entityClrType.Assembly);
			}
			foreach (AssociationType association in enumerable)
			{
				RefType refType = association.AssociationEndMembers[0].TypeUsage.EdmType as RefType;
				if (refType != null && refType.ElementType.ClrType.IsAssignableFrom(entityClrType))
				{
					yield return association.AssociationEndMembers[1];
				}
				refType = (association.AssociationEndMembers[1].TypeUsage.EdmType as RefType);
				if (refType != null && refType.ElementType.ClrType.IsAssignableFrom(entityClrType))
				{
					yield return association.AssociationEndMembers[0];
				}
				association = null;
			}
			IEnumerator<AssociationType> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x000629B8 File Offset: 0x00060BB8
		private bool VerifyRelationship(AssociationType relationship, string sourceEndName, bool throwOnError)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context == null)
			{
				return true;
			}
			EntityKey entityKey = wrappedOwner.EntityKey;
			if (entityKey == null)
			{
				return true;
			}
			bool result = true;
			TypeUsage typeUsage;
			if (wrappedOwner.Context.Perspective.TryGetTypeByName(relationship.FullName, false, out typeUsage))
			{
				EntityContainer entityContainer = wrappedOwner.Context.MetadataWorkspace.GetEntityContainer(entityKey.EntityContainerName, DataSpace.CSpace);
				EntitySet entitySet;
				if (MetadataHelper.GetAssociationsForEntitySetAndAssociationType(entityContainer, entityKey.EntitySetName, (AssociationType)typeUsage.EdmType, sourceEndName, out entitySet) == null)
				{
					if (throwOnError)
					{
						throw EntityUtil.NoRelationshipSetMatched(relationship.FullName);
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x00062A50 File Offset: 0x00060C50
		public EntityCollection<TTargetEntity> GetRelatedCollection<TTargetEntity>(string relationshipName, string targetRoleName) where TTargetEntity : class
		{
			EntityCollection<TTargetEntity> entityCollection = this.GetRelatedEndInternal(this.PrependNamespaceToRelationshipName(relationshipName), targetRoleName) as EntityCollection<TTargetEntity>;
			if (entityCollection == null)
			{
				throw EntityUtil.ExpectedCollectionGotReference(typeof(TTargetEntity).Name, targetRoleName, relationshipName);
			}
			return entityCollection;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x00062A8C File Offset: 0x00060C8C
		public EntityReference<TTargetEntity> GetRelatedReference<TTargetEntity>(string relationshipName, string targetRoleName) where TTargetEntity : class
		{
			EntityReference<TTargetEntity> entityReference = this.GetRelatedEndInternal(this.PrependNamespaceToRelationshipName(relationshipName), targetRoleName) as EntityReference<TTargetEntity>;
			if (entityReference == null)
			{
				throw EntityUtil.ExpectedReferenceGotCollection(typeof(TTargetEntity).Name, targetRoleName, relationshipName);
			}
			return entityReference;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x00062AC8 File Offset: 0x00060CC8
		internal RelatedEnd GetRelatedEnd(RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
		{
			RelatedEnd result;
			if (this.TryGetCachedRelatedEnd(navigation.RelationshipName, navigation.To, out result))
			{
				return result;
			}
			result = relationshipFixer.CreateSourceEnd(navigation, this);
			return result;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00062AF8 File Offset: 0x00060CF8
		internal RelatedEnd CreateRelatedEnd<TSourceEntity, TTargetEntity>(RelationshipNavigation navigation, RelationshipMultiplicity sourceRoleMultiplicity, RelationshipMultiplicity targetRoleMultiplicity, RelatedEnd existingRelatedEnd) where TSourceEntity : class where TTargetEntity : class
		{
			IRelationshipFixer relationshipFixer = new RelationshipFixer<TSourceEntity, TTargetEntity>(sourceRoleMultiplicity, targetRoleMultiplicity);
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			RelatedEnd relatedEnd;
			if (targetRoleMultiplicity > RelationshipMultiplicity.One)
			{
				if (targetRoleMultiplicity != RelationshipMultiplicity.Many)
				{
					throw EntityUtil.InvalidEnumerationValue(typeof(RelationshipMultiplicity), (int)targetRoleMultiplicity);
				}
				if (existingRelatedEnd != null)
				{
					existingRelatedEnd.InitializeRelatedEnd(wrappedOwner, navigation, relationshipFixer);
					relatedEnd = existingRelatedEnd;
				}
				else
				{
					relatedEnd = new EntityCollection<TTargetEntity>(wrappedOwner, navigation, relationshipFixer);
				}
			}
			else if (existingRelatedEnd != null)
			{
				existingRelatedEnd.InitializeRelatedEnd(wrappedOwner, navigation, relationshipFixer);
				relatedEnd = existingRelatedEnd;
			}
			else
			{
				relatedEnd = new EntityReference<TTargetEntity>(wrappedOwner, navigation, relationshipFixer);
			}
			if (wrappedOwner.Context != null)
			{
				relatedEnd.AttachContext(wrappedOwner.Context, wrappedOwner.MergeOption);
			}
			this.EnsureRelationshipsInitialized();
			this._relationships.Add(relatedEnd);
			return relatedEnd;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00062B9A File Offset: 0x00060D9A
		public IEnumerable<IRelatedEnd> GetAllRelatedEnds()
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			EntityType ownerEntityType;
			if (wrappedOwner.Context != null && wrappedOwner.Context.MetadataWorkspace != null && this.TryGetOwnerEntityType(out ownerEntityType))
			{
				EntitySet entitySet = wrappedOwner.Context.GetEntitySet(wrappedOwner.EntityKey.EntitySetName, wrappedOwner.EntityKey.EntityContainerName);
				foreach (AssociationEndMember associationEndMember in this.GetAllTargetEnds(ownerEntityType, entitySet))
				{
					yield return this.GetRelatedEnd(associationEndMember.DeclaringType.FullName, associationEndMember.Name);
				}
				IEnumerator<AssociationEndMember> enumerator = null;
			}
			else if (wrappedOwner.Entity != null)
			{
				foreach (AssociationEndMember associationEndMember2 in this.GetAllTargetEnds(wrappedOwner.IdentityType))
				{
					yield return this.GetRelatedEnd(associationEndMember2.DeclaringType.FullName, associationEndMember2.Name);
				}
				IEnumerator<AssociationEndMember> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x00062BAC File Offset: 0x00060DAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[OnSerializing]
		public void OnSerializing(StreamingContext context)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (!(wrappedOwner.Entity is IEntityWithRelationships))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_CannotSerialize("RelationshipManager"));
			}
			if (wrappedOwner.Context != null && wrappedOwner.MergeOption != MergeOption.NoTracking)
			{
				foreach (IRelatedEnd relatedEnd in this.GetAllRelatedEnds())
				{
					RelatedEnd relatedEnd2 = (RelatedEnd)relatedEnd;
					EntityReference entityReference = relatedEnd2 as EntityReference;
					if (entityReference != null && entityReference.EntityKey != null)
					{
						entityReference.DetachedEntityKey = entityReference.EntityKey;
					}
				}
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00062C50 File Offset: 0x00060E50
		internal bool HasRelationships
		{
			get
			{
				return this._relationships != null;
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00062C5C File Offset: 0x00060E5C
		internal void AddRelatedEntitiesToObjectStateManager(bool doAttach)
		{
			if (this._relationships != null)
			{
				bool flag = true;
				try
				{
					foreach (RelatedEnd relatedEnd in this.Relationships)
					{
						relatedEnd.Include(false, doAttach);
					}
					flag = false;
				}
				finally
				{
					if (flag)
					{
						IEntityWrapper wrappedOwner = this.WrappedOwner;
						TransactionManager transactionManager = wrappedOwner.Context.ObjectStateManager.TransactionManager;
						wrappedOwner.Context.ObjectStateManager.DegradePromotedRelationships();
						this.NodeVisited = true;
						RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(wrappedOwner);
						EntityEntry entityEntry;
						if (transactionManager.IsAttachTracking && transactionManager.PromotedKeyEntries.TryGetValue(wrappedOwner.Entity, out entityEntry))
						{
							entityEntry.DegradeEntry();
						}
						else
						{
							RelatedEnd.RemoveEntityFromObjectStateManager(wrappedOwner);
						}
					}
				}
			}
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00062D30 File Offset: 0x00060F30
		internal static void RemoveRelatedEntitiesFromObjectStateManager(IEntityWrapper wrappedEntity)
		{
			foreach (RelatedEnd relatedEnd in wrappedEntity.RelationshipManager.Relationships)
			{
				if (relatedEnd.ObjectContext != null)
				{
					relatedEnd.Exclude();
					relatedEnd.DetachContext();
				}
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00062D90 File Offset: 0x00060F90
		internal void RemoveEntityFromRelationships()
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					relatedEnd.RemoveAll();
				}
			}
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00062DE4 File Offset: 0x00060FE4
		internal void NullAllFKsInDependentsForWhichThisIsThePrincipal()
		{
			if (this._relationships != null)
			{
				List<EntityReference> list = new List<EntityReference>();
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					if (relatedEnd.IsForeignKey)
					{
						foreach (IEntityWrapper wrappedEntity in relatedEnd.GetWrappedEntities())
						{
							RelatedEnd otherEndOfRelationship = relatedEnd.GetOtherEndOfRelationship(wrappedEntity);
							if (otherEndOfRelationship.IsDependentEndOfReferentialConstraint(false))
							{
								list.Add((EntityReference)otherEndOfRelationship);
							}
						}
					}
				}
				foreach (EntityReference entityReference in list)
				{
					entityReference.NullAllForeignKeys();
				}
			}
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00062EDC File Offset: 0x000610DC
		internal void DetachEntityFromRelationships(EntityState ownerEntityState)
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					relatedEnd.DetachAll(ownerEntityState);
				}
			}
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x00062F34 File Offset: 0x00061134
		internal void RemoveEntity(string toRole, string relationshipName, IEntityWrapper wrappedEntity)
		{
			RelatedEnd relatedEnd;
			if (this.TryGetCachedRelatedEnd(relationshipName, toRole, out relatedEnd))
			{
				relatedEnd.Remove(wrappedEntity, false);
			}
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00062F58 File Offset: 0x00061158
		internal void ClearRelatedEndWrappers()
		{
			if (this._relationships != null)
			{
				foreach (IRelatedEnd relatedEnd in this.Relationships)
				{
					((RelatedEnd)relatedEnd).ClearWrappedValues();
				}
			}
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00062FB4 File Offset: 0x000611B4
		internal void RetrieveReferentialConstraintProperties(out Dictionary<string, KeyValuePair<object, IntBox>> properties, HashSet<object> visited, bool includeOwnValues)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			properties = new Dictionary<string, KeyValuePair<object, IntBox>>();
			EntityKey entityKey = wrappedOwner.EntityKey;
			if (entityKey.IsTemporary)
			{
				List<string> list;
				bool flag;
				this.FindNamesOfReferentialConstraintProperties(out list, out flag, false);
				if (list != null)
				{
					if (this._relationships != null)
					{
						foreach (RelatedEnd relatedEnd in this._relationships)
						{
							relatedEnd.RetrieveReferentialConstraintProperties(properties, visited);
						}
					}
					if (!RelationshipManager.CheckIfAllPropertiesWereRetrieved(properties, list))
					{
						EntityEntry entityEntry = wrappedOwner.Context.ObjectStateManager.FindEntityEntry(entityKey);
						entityEntry.RetrieveReferentialConstraintPropertiesFromKeyEntries(properties);
						if (!RelationshipManager.CheckIfAllPropertiesWereRetrieved(properties, list))
						{
							throw EntityUtil.UnableToRetrieveReferentialConstraintProperties();
						}
					}
				}
			}
			if (!entityKey.IsTemporary || includeOwnValues)
			{
				EntityEntry entityEntry2 = wrappedOwner.Context.ObjectStateManager.FindEntityEntry(entityKey);
				entityEntry2.GetOtherKeyProperties(properties);
			}
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x000630A4 File Offset: 0x000612A4
		private static bool CheckIfAllPropertiesWereRetrieved(Dictionary<string, KeyValuePair<object, IntBox>> properties, List<string> propertiesToRetrieve)
		{
			bool flag = true;
			List<int> list = new List<int>();
			ICollection<KeyValuePair<object, IntBox>> values = properties.Values;
			foreach (KeyValuePair<object, IntBox> keyValuePair in values)
			{
				list.Add(keyValuePair.Value.Value);
			}
			foreach (string key in propertiesToRetrieve)
			{
				if (!properties.ContainsKey(key))
				{
					flag = false;
					break;
				}
				KeyValuePair<object, IntBox> keyValuePair2 = properties[key];
				keyValuePair2.Value.Value = keyValuePair2.Value.Value - 1;
				if (keyValuePair2.Value.Value < 0)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				foreach (KeyValuePair<object, IntBox> keyValuePair3 in values)
				{
					if (keyValuePair3.Value.Value != 0)
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				IEnumerator<int> enumerator4 = list.GetEnumerator();
				foreach (KeyValuePair<object, IntBox> keyValuePair4 in values)
				{
					enumerator4.MoveNext();
					keyValuePair4.Value.Value = enumerator4.Current;
				}
			}
			return flag;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00063234 File Offset: 0x00061434
		internal void CheckReferentialConstraintProperties(EntityEntry ownerEntry)
		{
			List<string> list;
			bool flag;
			this.FindNamesOfReferentialConstraintProperties(out list, out flag, false);
			if ((list != null || flag) && this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this._relationships)
				{
					if (!relatedEnd.CheckReferentialConstraintProperties(ownerEntry))
					{
						throw EntityUtil.InconsistentReferentialConstraintProperties();
					}
				}
			}
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x000632AC File Offset: 0x000614AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[OnDeserialized]
		public void OnDeserialized(StreamingContext context)
		{
			this._wrappedOwner = EntityWrapperFactory.WrapEntityUsingContext(this._owner, null);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x000632C0 File Offset: 0x000614C0
		private bool TryGetCachedRelatedEnd(string relationshipName, string targetRoleName, out RelatedEnd relatedEnd)
		{
			relatedEnd = null;
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd2 in this._relationships)
				{
					RelationshipNavigation relationshipNavigation = relatedEnd2.RelationshipNavigation;
					if (relationshipNavigation.RelationshipName == relationshipName && relationshipNavigation.To == targetRoleName)
					{
						relatedEnd = relatedEnd2;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x00063344 File Offset: 0x00061544
		internal bool FindNamesOfReferentialConstraintProperties(out List<string> propertiesToRetrieve, out bool propertiesToPropagateExist, bool skipFK)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			EntityKey entityKey = wrappedOwner.EntityKey;
			EntityUtil.CheckEntityKeyNull(entityKey);
			propertiesToRetrieve = null;
			propertiesToPropagateExist = false;
			EntityUtil.CheckContextNull(wrappedOwner.Context);
			EntitySet entitySet = entityKey.GetEntitySet(wrappedOwner.Context.MetadataWorkspace);
			List<AssociationSet> associationsForEntitySet = MetadataHelper.GetAssociationsForEntitySet(entitySet);
			bool result = false;
			foreach (AssociationSet associationSet in associationsForEntitySet)
			{
				if (skipFK && associationSet.ElementType.IsForeignKey)
				{
					result = true;
				}
				else
				{
					foreach (ReferentialConstraint referentialConstraint in associationSet.ElementType.ReferentialConstraints)
					{
						if (referentialConstraint.ToRole.TypeUsage.EdmType == entitySet.ElementType.GetReferenceType())
						{
							propertiesToRetrieve = (propertiesToRetrieve ?? new List<string>());
							foreach (EdmProperty edmProperty in referentialConstraint.ToProperties)
							{
								propertiesToRetrieve.Add(edmProperty.Name);
							}
						}
						if (referentialConstraint.FromRole.TypeUsage.EdmType == entitySet.ElementType.GetReferenceType())
						{
							propertiesToPropagateExist = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x000634D0 File Offset: 0x000616D0
		internal bool IsOwner(IEntityWrapper wrappedEntity)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			return wrappedEntity.Entity == wrappedOwner.Entity;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000634F4 File Offset: 0x000616F4
		internal void AttachContextToRelatedEnds(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					EdmType edmType;
					RelationshipSet relationshipSet;
					relatedEnd.FindRelationshipSet(context, entitySet, out edmType, out relationshipSet);
					if (relationshipSet != null || !relatedEnd.IsEmpty())
					{
						relatedEnd.AttachContext(context, entitySet, mergeOption);
					}
					else
					{
						this._relationships.Remove(relatedEnd);
					}
				}
			}
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x00063574 File Offset: 0x00061774
		internal void ResetContextOnRelatedEnds(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					relatedEnd.AttachContext(context, entitySet, mergeOption);
					foreach (IEntityWrapper entityWrapper in relatedEnd.GetWrappedEntities())
					{
						entityWrapper.ResetContext(context, relatedEnd.GetTargetEntitySetFromRelationshipSet(), mergeOption);
					}
				}
			}
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x00063610 File Offset: 0x00061810
		internal void DetachContextFromRelatedEnds()
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this._relationships)
				{
					relatedEnd.DetachContext();
				}
			}
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0006366C File Offset: 0x0006186C
		[Conditional("DEBUG")]
		internal void VerifyIsNotRelated()
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this._relationships)
				{
					relatedEnd.IsEmpty();
				}
			}
		}

		// Token: 0x04000BB5 RID: 2997
		private IEntityWithRelationships _owner;

		// Token: 0x04000BB6 RID: 2998
		private List<RelatedEnd> _relationships;

		// Token: 0x04000BB7 RID: 2999
		[NonSerialized]
		private bool _nodeVisited;

		// Token: 0x04000BB8 RID: 3000
		[NonSerialized]
		private IEntityWrapper _wrappedOwner;
	}
}
