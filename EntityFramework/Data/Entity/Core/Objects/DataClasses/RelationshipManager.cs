using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000548 RID: 1352
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	[Serializable]
	public class RelationshipManager
	{
		// Token: 0x0600342C RID: 13356 RVA: 0x000F63E9 File Offset: 0x000F45E9
		private RelationshipManager()
		{
			this._entityWrapperFactory = new EntityWrapperFactory();
			this._expensiveLoader = new ExpensiveOSpaceLoader();
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000F6407 File Offset: 0x000F4607
		internal RelationshipManager(ExpensiveOSpaceLoader expensiveLoader)
		{
			this._entityWrapperFactory = new EntityWrapperFactory();
			this._expensiveLoader = (expensiveLoader ?? new ExpensiveOSpaceLoader());
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000F642A File Offset: 0x000F462A
		internal void SetExpensiveLoader(ExpensiveOSpaceLoader loader)
		{
			this._expensiveLoader = loader;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000F6433 File Offset: 0x000F4633
		internal IEnumerable<RelatedEnd> Relationships
		{
			get
			{
				this.EnsureRelationshipsInitialized();
				return this._relationships.ToArray();
			}
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000F6446 File Offset: 0x000F4646
		private void EnsureRelationshipsInitialized()
		{
			if (this._relationships == null)
			{
				this._relationships = new List<RelatedEnd>();
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x000F645B File Offset: 0x000F465B
		// (set) Token: 0x06003432 RID: 13362 RVA: 0x000F6463 File Offset: 0x000F4663
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

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000F646C File Offset: 0x000F466C
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

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000F648E File Offset: 0x000F468E
		internal virtual EntityWrapperFactory EntityWrapperFactory
		{
			get
			{
				return this._entityWrapperFactory;
			}
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000F6498 File Offset: 0x000F4698
		public static RelationshipManager Create(IEntityWithRelationships owner)
		{
			Check.NotNull<IEntityWithRelationships>(owner, "owner");
			return new RelationshipManager
			{
				_owner = owner
			};
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000F64BF File Offset: 0x000F46BF
		internal static RelationshipManager Create()
		{
			return new RelationshipManager();
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000F64C8 File Offset: 0x000F46C8
		internal void SetWrappedOwner(IEntityWrapper wrappedOwner, object expectedOwner)
		{
			this._wrappedOwner = wrappedOwner;
			if (this._owner != null && !object.ReferenceEquals(expectedOwner, this._owner))
			{
				throw new InvalidOperationException(Strings.RelationshipManager_InvalidRelationshipManagerOwner);
			}
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this._relationships)
				{
					relatedEnd.SetWrappedOwner(wrappedOwner);
				}
			}
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000F654C File Offset: 0x000F474C
		internal EntityCollection<TTargetEntity> GetRelatedCollection<TSourceEntity, TTargetEntity>(AssociationEndMember sourceMember, AssociationEndMember targetMember, NavigationPropertyAccessor sourceAccessor, NavigationPropertyAccessor targetAccessor, RelatedEnd existingRelatedEnd) where TSourceEntity : class where TTargetEntity : class
		{
			string fullName = sourceMember.DeclaringType.FullName;
			string name = targetMember.Name;
			RelationshipMultiplicity relationshipMultiplicity = sourceMember.RelationshipMultiplicity;
			RelatedEnd relatedEnd;
			this.TryGetCachedRelatedEnd(fullName, name, out relatedEnd);
			EntityCollection<TTargetEntity> entityCollection = relatedEnd as EntityCollection<TTargetEntity>;
			if (existingRelatedEnd != null)
			{
				if (relatedEnd != null)
				{
					this._relationships.Remove(relatedEnd);
				}
				RelationshipNavigation navigation = new RelationshipNavigation((AssociationType)sourceMember.DeclaringType, sourceMember.Name, targetMember.Name, sourceAccessor, targetAccessor);
				EntityCollection<TTargetEntity> entityCollection2 = this.CreateRelatedEnd<TSourceEntity, TTargetEntity>(navigation, relationshipMultiplicity, RelationshipMultiplicity.Many, existingRelatedEnd) as EntityCollection<TTargetEntity>;
				if (entityCollection2 != null)
				{
					bool flag = true;
					try
					{
						RelationshipManager.RemergeCollections<TTargetEntity>(entityCollection, entityCollection2);
						flag = false;
					}
					finally
					{
						if (flag && relatedEnd != null)
						{
							this._relationships.Remove(entityCollection2);
							this._relationships.Add(relatedEnd);
						}
					}
				}
				return entityCollection2;
			}
			if (relatedEnd != null)
			{
				return entityCollection;
			}
			RelationshipNavigation navigation2 = new RelationshipNavigation((AssociationType)sourceMember.DeclaringType, sourceMember.Name, targetMember.Name, sourceAccessor, targetAccessor);
			return this.CreateRelatedEnd<TSourceEntity, TTargetEntity>(navigation2, relationshipMultiplicity, RelationshipMultiplicity.Many, existingRelatedEnd) as EntityCollection<TTargetEntity>;
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000F6650 File Offset: 0x000F4850
		private static void RemergeCollections<TTargetEntity>(EntityCollection<TTargetEntity> previousCollection, EntityCollection<TTargetEntity> collection) where TTargetEntity : class
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
				throw new InvalidOperationException(Strings.Collections_UnableToMergeCollections);
			}
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000F6728 File Offset: 0x000F4928
		internal EntityReference<TTargetEntity> GetRelatedReference<TSourceEntity, TTargetEntity>(AssociationEndMember sourceMember, AssociationEndMember targetMember, NavigationPropertyAccessor sourceAccessor, NavigationPropertyAccessor targetAccessor, RelatedEnd existingRelatedEnd) where TSourceEntity : class where TTargetEntity : class
		{
			string fullName = sourceMember.DeclaringType.FullName;
			string name = targetMember.Name;
			RelationshipMultiplicity relationshipMultiplicity = sourceMember.RelationshipMultiplicity;
			RelatedEnd relatedEnd;
			if (this.TryGetCachedRelatedEnd(fullName, name, out relatedEnd))
			{
				return relatedEnd as EntityReference<TTargetEntity>;
			}
			RelationshipNavigation navigation = new RelationshipNavigation((AssociationType)sourceMember.DeclaringType, sourceMember.Name, targetMember.Name, sourceAccessor, targetAccessor);
			return this.CreateRelatedEnd<TSourceEntity, TTargetEntity>(navigation, relationshipMultiplicity, RelationshipMultiplicity.One, existingRelatedEnd) as EntityReference<TTargetEntity>;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000F6798 File Offset: 0x000F4998
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal RelatedEnd GetRelatedEnd(string navigationProperty, bool throwArgumentException = false)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			EntityType item = wrappedOwner.Context.MetadataWorkspace.GetItem<EntityType>(wrappedOwner.IdentityType.FullNameWithNesting(), DataSpace.OSpace);
			EdmMember edmMember;
			if (!wrappedOwner.Context.Perspective.TryGetMember(item, navigationProperty, false, out edmMember) || !(edmMember is NavigationProperty))
			{
				string message = Strings.RelationshipManager_NavigationPropertyNotFound(navigationProperty);
				throw throwArgumentException ? new ArgumentException(message) : new InvalidOperationException(message);
			}
			NavigationProperty navigationProperty2 = (NavigationProperty)edmMember;
			return this.GetRelatedEndInternal(navigationProperty2.RelationshipType.FullName, navigationProperty2.ToEndMember.Name);
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000F6827 File Offset: 0x000F4A27
		public IRelatedEnd GetRelatedEnd(string relationshipName, string targetRoleName)
		{
			return this.GetRelatedEndInternal(this.PrependNamespaceToRelationshipName(relationshipName), targetRoleName);
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000F6838 File Offset: 0x000F4A38
		internal RelatedEnd GetRelatedEndInternal(string relationshipName, string targetRoleName)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context == null && wrappedOwner.RequiresRelationshipChangeTracking)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_CannotGetRelatEndForDetachedPocoEntity);
			}
			AssociationType relationshipType = this.GetRelationshipType(relationshipName);
			return this.GetRelatedEndInternal(relationshipName, targetRoleName, null, relationshipType);
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000F687C File Offset: 0x000F4A7C
		private RelatedEnd GetRelatedEndInternal(string relationshipName, string targetRoleName, RelatedEnd existingRelatedEnd, AssociationType relationship)
		{
			AssociationEndMember associationEndMember;
			AssociationEndMember targetMember;
			RelationshipManager.GetAssociationEnds(relationship, targetRoleName, out associationEndMember, out targetMember);
			EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(associationEndMember);
			Type clrType = entityTypeForEnd.ClrType;
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (!clrType.IsAssignableFrom(wrappedOwner.IdentityType))
			{
				throw new InvalidOperationException(Strings.RelationshipManager_OwnerIsNotSourceType(wrappedOwner.IdentityType.FullName, clrType.FullName, associationEndMember.Name, relationshipName));
			}
			if (!this.VerifyRelationship(relationship, associationEndMember.Name))
			{
				return null;
			}
			return DelegateFactory.GetRelatedEnd(this, associationEndMember, targetMember, existingRelatedEnd);
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000F68FC File Offset: 0x000F4AFC
		internal RelatedEnd GetRelatedEndInternal(AssociationType csAssociationType, AssociationEndMember csTargetEnd)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context == null && wrappedOwner.RequiresRelationshipChangeTracking)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_CannotGetRelatEndForDetachedPocoEntity);
			}
			AssociationType relationshipType = this.GetRelationshipType(csAssociationType);
			AssociationEndMember associationEndMember;
			AssociationEndMember targetMember;
			RelationshipManager.GetAssociationEnds(relationshipType, csTargetEnd.Name, out associationEndMember, out targetMember);
			EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(associationEndMember);
			Type clrType = entityTypeForEnd.ClrType;
			if (!clrType.IsAssignableFrom(wrappedOwner.IdentityType))
			{
				throw new InvalidOperationException(Strings.RelationshipManager_OwnerIsNotSourceType(wrappedOwner.IdentityType.FullName, clrType.FullName, associationEndMember.Name, csAssociationType.FullName));
			}
			if (!this.VerifyRelationship(relationshipType, csAssociationType, associationEndMember.Name))
			{
				return null;
			}
			return DelegateFactory.GetRelatedEnd(this, associationEndMember, targetMember, null);
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000F69A8 File Offset: 0x000F4BA8
		private static void GetAssociationEnds(AssociationType associationType, string targetRoleName, out AssociationEndMember sourceEnd, out AssociationEndMember targetEnd)
		{
			targetEnd = associationType.TargetEnd;
			if (targetEnd.Identity != targetRoleName)
			{
				sourceEnd = targetEnd;
				targetEnd = associationType.SourceEnd;
				if (targetEnd.Identity != targetRoleName)
				{
					throw new InvalidOperationException(Strings.RelationshipManager_InvalidTargetRole(associationType.FullName, targetRoleName));
				}
			}
			else
			{
				sourceEnd = associationType.SourceEnd;
			}
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000F6A04 File Offset: 0x000F4C04
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public void InitializeRelatedReference<TTargetEntity>(string relationshipName, string targetRoleName, EntityReference<TTargetEntity> entityReference) where TTargetEntity : class
		{
			Check.NotNull<string>(relationshipName, "relationshipName");
			Check.NotNull<string>(targetRoleName, "targetRoleName");
			Check.NotNull<EntityReference<TTargetEntity>>(entityReference, "entityReference");
			if (entityReference.WrappedOwner.Entity != null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_ReferenceAlreadyInitialized(Strings.RelationshipManager_InitializeIsForDeserialization));
			}
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context != null && wrappedOwner.MergeOption != MergeOption.NoTracking)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_RelationshipManagerAttached(Strings.RelationshipManager_InitializeIsForDeserialization));
			}
			relationshipName = this.PrependNamespaceToRelationshipName(relationshipName);
			AssociationType relationshipType = this.GetRelationshipType(relationshipName);
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
				throw new InvalidOperationException(Strings.EntityReference_ExpectedReferenceGotCollection(typeof(TTargetEntity).Name, targetRoleName, relationshipName));
			}
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000F6AE4 File Offset: 0x000F4CE4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InitializeRelatedCollection<TTargetEntity>(string relationshipName, string targetRoleName, EntityCollection<TTargetEntity> entityCollection) where TTargetEntity : class
		{
			Check.NotNull<string>(relationshipName, "relationshipName");
			Check.NotNull<string>(targetRoleName, "targetRoleName");
			Check.NotNull<EntityCollection<TTargetEntity>>(entityCollection, "entityCollection");
			if (entityCollection.WrappedOwner.Entity != null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_CollectionAlreadyInitialized(Strings.RelationshipManager_CollectionInitializeIsForDeserialization));
			}
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context != null && wrappedOwner.MergeOption != MergeOption.NoTracking)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_CollectionRelationshipManagerAttached(Strings.RelationshipManager_CollectionInitializeIsForDeserialization));
			}
			relationshipName = this.PrependNamespaceToRelationshipName(relationshipName);
			AssociationType relationshipType = this.GetRelationshipType(relationshipName);
			if (!(this.GetRelatedEndInternal(relationshipName, targetRoleName, entityCollection, relationshipType) is EntityCollection<TTargetEntity>))
			{
				throw new InvalidOperationException(Strings.Collections_ExpectedCollectionGotReference(typeof(TTargetEntity).Name, targetRoleName, relationshipName));
			}
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000F6BC8 File Offset: 0x000F4DC8
		internal string PrependNamespaceToRelationshipName(string relationshipName)
		{
			if (!relationshipName.Contains("."))
			{
				AssociationType associationType;
				if (EntityProxyFactory.TryGetAssociationTypeFromProxyInfo(this.WrappedOwner, relationshipName, out associationType))
				{
					return associationType.FullName;
				}
				if (this._relationships != null)
				{
					string text = (from r in this._relationships
					select r.RelationshipName).FirstOrDefault((string n) => n.Substring(n.LastIndexOf('.') + 1) == relationshipName);
					if (text != null)
					{
						return text;
					}
				}
				string text2 = this.WrappedOwner.IdentityType.FullNameWithNesting();
				ObjectItemCollection objectItemCollection = RelationshipManager.GetObjectItemCollection(this.WrappedOwner);
				EdmType edmType = null;
				if (objectItemCollection != null)
				{
					objectItemCollection.TryGetItem<EdmType>(text2, out edmType);
				}
				else
				{
					Dictionary<string, EdmType> dictionary = this._expensiveLoader.LoadTypesExpensiveWay(this.WrappedOwner.IdentityType.Assembly());
					if (dictionary != null)
					{
						dictionary.TryGetValue(text2, out edmType);
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

		// Token: 0x06003444 RID: 13380 RVA: 0x000F6CF6 File Offset: 0x000F4EF6
		private static ObjectItemCollection GetObjectItemCollection(IEntityWrapper wrappedOwner)
		{
			if (wrappedOwner.Context != null)
			{
				return (ObjectItemCollection)wrappedOwner.Context.MetadataWorkspace.GetItemCollection(DataSpace.OSpace);
			}
			return null;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000F6D18 File Offset: 0x000F4F18
		private bool TryGetOwnerEntityType(out EntityType entityType)
		{
			DefaultObjectMappingItemCollection defaultObjectMappingItemCollection;
			MappingBase mappingBase;
			if (RelationshipManager.TryGetObjectMappingItemCollection(this.WrappedOwner, out defaultObjectMappingItemCollection) && defaultObjectMappingItemCollection.TryGetMap(this.WrappedOwner.IdentityType.FullNameWithNesting(), DataSpace.OSpace, out mappingBase))
			{
				ObjectTypeMapping objectTypeMapping = (ObjectTypeMapping)mappingBase;
				if (Helper.IsEntityType(objectTypeMapping.EdmType))
				{
					entityType = (EntityType)objectTypeMapping.EdmType;
					return true;
				}
			}
			entityType = null;
			return false;
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000F6D76 File Offset: 0x000F4F76
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

		// Token: 0x06003447 RID: 13383 RVA: 0x000F6DD4 File Offset: 0x000F4FD4
		internal AssociationType GetRelationshipType(AssociationType csAssociationType)
		{
			MetadataWorkspace metadataWorkspace = this.WrappedOwner.Context.MetadataWorkspace;
			if (metadataWorkspace != null)
			{
				return metadataWorkspace.MetadataOptimization.GetOSpaceAssociationType(csAssociationType, () => this.GetRelationshipType(csAssociationType.FullName));
			}
			return this.GetRelationshipType(csAssociationType.FullName);
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000F6E64 File Offset: 0x000F5064
		internal AssociationType GetRelationshipType(string relationshipName)
		{
			AssociationType associationType = null;
			ObjectItemCollection objectItemCollection = RelationshipManager.GetObjectItemCollection(this.WrappedOwner);
			if (objectItemCollection != null)
			{
				associationType = objectItemCollection.GetRelationshipType(relationshipName);
			}
			if (associationType == null)
			{
				EntityProxyFactory.TryGetAssociationTypeFromProxyInfo(this.WrappedOwner, relationshipName, out associationType);
			}
			if (associationType == null && this._relationships != null)
			{
				associationType = (from e in this._relationships
				where e.RelationshipName == relationshipName
				select e.RelationMetadata).OfType<AssociationType>().FirstOrDefault<AssociationType>();
			}
			if (associationType == null)
			{
				associationType = this._expensiveLoader.GetRelationshipTypeExpensiveWay(this.WrappedOwner.IdentityType, relationshipName);
			}
			if (associationType == null)
			{
				throw RelationshipManager.UnableToGetMetadata(this.WrappedOwner, relationshipName);
			}
			return associationType;
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x000F6F40 File Offset: 0x000F5140
		internal static Exception UnableToGetMetadata(IEntityWrapper wrappedOwner, string relationshipName)
		{
			ArgumentException ex = new ArgumentException(Strings.RelationshipManager_UnableToFindRelationshipTypeInMetadata(relationshipName), "relationshipName");
			if (EntityProxyFactory.IsProxyType(wrappedOwner.Entity.GetType()))
			{
				return new InvalidOperationException(Strings.EntityProxyTypeInfo_ProxyMetadataIsUnavailable(wrappedOwner.IdentityType.FullName), ex);
			}
			return ex;
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000F71E0 File Offset: 0x000F53E0
		private static IEnumerable<AssociationEndMember> GetAllTargetEnds(EntityType ownerEntityType, EntitySet ownerEntitySet)
		{
			foreach (AssociationSet assocSet in MetadataHelper.GetAssociationsForEntitySet(ownerEntitySet))
			{
				EntityType end2EntityType = assocSet.ElementType.AssociationEndMembers[1].GetEntityType();
				if (end2EntityType.IsAssignableFrom(ownerEntityType))
				{
					yield return assocSet.ElementType.AssociationEndMembers[0];
				}
				EntityType end1EntityType = assocSet.ElementType.AssociationEndMembers[0].GetEntityType();
				if (end1EntityType.IsAssignableFrom(ownerEntityType))
				{
					yield return assocSet.ElementType.AssociationEndMembers[1];
				}
			}
			yield break;
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000F74F4 File Offset: 0x000F56F4
		private IEnumerable<AssociationEndMember> GetAllTargetEnds(Type entityClrType)
		{
			ObjectItemCollection objectItemCollection = RelationshipManager.GetObjectItemCollection(this.WrappedOwner);
			IEnumerable<AssociationType> associations = null;
			if (objectItemCollection != null)
			{
				associations = objectItemCollection.GetItems<AssociationType>();
			}
			else
			{
				associations = EntityProxyFactory.TryGetAllAssociationTypesFromProxyInfo(this.WrappedOwner);
				if (associations == null)
				{
					associations = this._expensiveLoader.GetAllRelationshipTypesExpensiveWay(entityClrType.Assembly());
				}
			}
			foreach (AssociationType association in associations)
			{
				RefType referenceType = association.AssociationEndMembers[0].TypeUsage.EdmType as RefType;
				if (referenceType != null && referenceType.ElementType.ClrType.IsAssignableFrom(entityClrType))
				{
					yield return association.AssociationEndMembers[1];
				}
				referenceType = (association.AssociationEndMembers[1].TypeUsage.EdmType as RefType);
				if (referenceType != null && referenceType.ElementType.ClrType.IsAssignableFrom(entityClrType))
				{
					yield return association.AssociationEndMembers[0];
				}
			}
			yield break;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000F7518 File Offset: 0x000F5718
		private bool VerifyRelationship(AssociationType relationship, string sourceEndName)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context == null)
			{
				return true;
			}
			EntityKey entityKey = wrappedOwner.EntityKey;
			return entityKey == null || RelationshipManager.VerifyRelationship(wrappedOwner, entityKey, relationship, sourceEndName);
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000F7554 File Offset: 0x000F5754
		private bool VerifyRelationship(AssociationType osAssociationType, AssociationType csAssociationType, string sourceEndName)
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
			if (osAssociationType.Index < 0)
			{
				return RelationshipManager.VerifyRelationship(wrappedOwner, entityKey, osAssociationType, sourceEndName);
			}
			MetadataWorkspace metadataWorkspace = wrappedOwner.Context.MetadataWorkspace;
			EntitySet entitySet;
			if (metadataWorkspace.MetadataOptimization.FindCSpaceAssociationSet(csAssociationType, sourceEndName, entityKey.EntitySetName, entityKey.EntityContainerName, out entitySet) == null)
			{
				throw Error.Collections_NoRelationshipSetMatched(osAssociationType.FullName);
			}
			return true;
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000F75D4 File Offset: 0x000F57D4
		private static bool VerifyRelationship(IEntityWrapper wrappedOwner, EntityKey ownerKey, AssociationType relationship, string sourceEndName)
		{
			TypeUsage typeUsage;
			EntitySet entitySet;
			if (wrappedOwner.Context.Perspective.TryGetTypeByName(relationship.FullName, false, out typeUsage) && wrappedOwner.Context.MetadataWorkspace.MetadataOptimization.FindCSpaceAssociationSet((AssociationType)typeUsage.EdmType, sourceEndName, ownerKey.EntitySetName, ownerKey.EntityContainerName, out entitySet) == null)
			{
				string fullName = relationship.FullName;
				throw Error.Collections_NoRelationshipSetMatched(fullName);
			}
			return true;
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000F7640 File Offset: 0x000F5840
		public EntityCollection<TTargetEntity> GetRelatedCollection<TTargetEntity>(string relationshipName, string targetRoleName) where TTargetEntity : class
		{
			EntityCollection<TTargetEntity> entityCollection = this.GetRelatedEndInternal(this.PrependNamespaceToRelationshipName(relationshipName), targetRoleName) as EntityCollection<TTargetEntity>;
			if (entityCollection == null)
			{
				throw new InvalidOperationException(Strings.Collections_ExpectedCollectionGotReference(typeof(TTargetEntity).Name, targetRoleName, relationshipName));
			}
			return entityCollection;
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000F7684 File Offset: 0x000F5884
		public EntityReference<TTargetEntity> GetRelatedReference<TTargetEntity>(string relationshipName, string targetRoleName) where TTargetEntity : class
		{
			EntityReference<TTargetEntity> entityReference = this.GetRelatedEndInternal(this.PrependNamespaceToRelationshipName(relationshipName), targetRoleName) as EntityReference<TTargetEntity>;
			if (entityReference == null)
			{
				throw new InvalidOperationException(Strings.EntityReference_ExpectedReferenceGotCollection(typeof(TTargetEntity).Name, targetRoleName, relationshipName));
			}
			return entityReference;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000F76C8 File Offset: 0x000F58C8
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

		// Token: 0x06003452 RID: 13394 RVA: 0x000F76F8 File Offset: 0x000F58F8
		internal RelatedEnd CreateRelatedEnd<TSourceEntity, TTargetEntity>(RelationshipNavigation navigation, RelationshipMultiplicity sourceRoleMultiplicity, RelationshipMultiplicity targetRoleMultiplicity, RelatedEnd existingRelatedEnd) where TSourceEntity : class where TTargetEntity : class
		{
			IRelationshipFixer relationshipFixer = new RelationshipFixer<TSourceEntity, TTargetEntity>(sourceRoleMultiplicity, targetRoleMultiplicity);
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			RelatedEnd relatedEnd;
			switch (targetRoleMultiplicity)
			{
			case RelationshipMultiplicity.ZeroOrOne:
			case RelationshipMultiplicity.One:
				if (existingRelatedEnd != null)
				{
					existingRelatedEnd.InitializeRelatedEnd(wrappedOwner, navigation, relationshipFixer);
					relatedEnd = existingRelatedEnd;
				}
				else
				{
					relatedEnd = new EntityReference<TTargetEntity>(wrappedOwner, navigation, relationshipFixer);
				}
				break;
			case RelationshipMultiplicity.Many:
				if (existingRelatedEnd != null)
				{
					existingRelatedEnd.InitializeRelatedEnd(wrappedOwner, navigation, relationshipFixer);
					relatedEnd = existingRelatedEnd;
				}
				else
				{
					relatedEnd = new EntityCollection<TTargetEntity>(wrappedOwner, navigation, relationshipFixer);
				}
				break;
			default:
			{
				Type typeFromHandle = typeof(RelationshipMultiplicity);
				string name = typeFromHandle.Name;
				object name2 = typeFromHandle.Name;
				int num = (int)targetRoleMultiplicity;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name2, num.ToString(CultureInfo.InvariantCulture)));
			}
			}
			if (wrappedOwner.Context != null)
			{
				relatedEnd.AttachContext(wrappedOwner.Context, wrappedOwner.MergeOption);
			}
			this.EnsureRelationshipsInitialized();
			this._relationships.Add(relatedEnd);
			return relatedEnd;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000F7AFC File Offset: 0x000F5CFC
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public IEnumerable<IRelatedEnd> GetAllRelatedEnds()
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			EntityType entityType;
			if (wrappedOwner.Context != null && wrappedOwner.Context.MetadataWorkspace != null && this.TryGetOwnerEntityType(out entityType))
			{
				EntitySet entitySet = wrappedOwner.Context.GetEntitySet(wrappedOwner.EntityKey.EntitySetName, wrappedOwner.EntityKey.EntityContainerName);
				foreach (AssociationEndMember endMember in RelationshipManager.GetAllTargetEnds(entityType, entitySet))
				{
					yield return this.GetRelatedEnd(endMember.DeclaringType.FullName, endMember.Name);
				}
			}
			else if (wrappedOwner.Entity != null)
			{
				foreach (AssociationEndMember endMember2 in this.GetAllTargetEnds(wrappedOwner.IdentityType))
				{
					yield return this.GetRelatedEnd(endMember2.DeclaringType.FullName, endMember2.Name);
				}
			}
			yield break;
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000F7B1C File Offset: 0x000F5D1C
		[OnSerializing]
		[SuppressMessage("Microsoft.Usage", "CA2238:ImplementSerializationMethodsCorrectly")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06003455 RID: 13397 RVA: 0x000F7BC0 File Offset: 0x000F5DC0
		internal bool HasRelationships
		{
			get
			{
				return this._relationships != null;
			}
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000F7BD0 File Offset: 0x000F5DD0
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

		// Token: 0x06003457 RID: 13399 RVA: 0x000F7CA8 File Offset: 0x000F5EA8
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

		// Token: 0x06003458 RID: 13400 RVA: 0x000F7D08 File Offset: 0x000F5F08
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

		// Token: 0x06003459 RID: 13401 RVA: 0x000F7D5C File Offset: 0x000F5F5C
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

		// Token: 0x0600345A RID: 13402 RVA: 0x000F7E58 File Offset: 0x000F6058
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

		// Token: 0x0600345B RID: 13403 RVA: 0x000F7EB0 File Offset: 0x000F60B0
		internal void RemoveEntity(string toRole, string relationshipName, IEntityWrapper wrappedEntity)
		{
			RelatedEnd relatedEnd;
			if (this.TryGetCachedRelatedEnd(relationshipName, toRole, out relatedEnd))
			{
				relatedEnd.Remove(wrappedEntity, false);
			}
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000F7ED4 File Offset: 0x000F60D4
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

		// Token: 0x0600345D RID: 13405 RVA: 0x000F7F30 File Offset: 0x000F6130
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
							throw new InvalidOperationException(Strings.RelationshipManager_UnableToRetrieveReferentialConstraintProperties);
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

		// Token: 0x0600345E RID: 13406 RVA: 0x000F8024 File Offset: 0x000F6224
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

		// Token: 0x0600345F RID: 13407 RVA: 0x000F81B8 File Offset: 0x000F63B8
		internal void CheckReferentialConstraintProperties(EntityEntry ownerEntry)
		{
			List<string> list;
			bool flag;
			this.FindNamesOfReferentialConstraintProperties(out list, out flag, false);
			if ((list != null || flag) && this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this._relationships)
				{
					relatedEnd.CheckReferentialConstraintProperties(ownerEntry);
				}
			}
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000F8228 File Offset: 0x000F6428
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[OnDeserialized]
		[SuppressMessage("Microsoft.Usage", "CA2238:ImplementSerializationMethodsCorrectly")]
		public void OnDeserialized(StreamingContext context)
		{
			this._entityWrapperFactory = new EntityWrapperFactory();
			this._expensiveLoader = new ExpensiveOSpaceLoader();
			this._wrappedOwner = this.EntityWrapperFactory.WrapEntityUsingContext(this._owner, null);
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x000F8258 File Offset: 0x000F6458
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

		// Token: 0x06003462 RID: 13410 RVA: 0x000F82DC File Offset: 0x000F64DC
		internal bool FindNamesOfReferentialConstraintProperties(out List<string> propertiesToRetrieve, out bool propertiesToPropagateExist, bool skipFK)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			EntityKey entityKey = wrappedOwner.EntityKey;
			if (entityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			propertiesToRetrieve = null;
			propertiesToPropagateExist = false;
			if (wrappedOwner.Context == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNullContext);
			}
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

		// Token: 0x06003463 RID: 13411 RVA: 0x000F8474 File Offset: 0x000F6674
		internal bool IsOwner(IEntityWrapper wrappedEntity)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			return object.ReferenceEquals(wrappedEntity.Entity, wrappedOwner.Entity);
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000F849C File Offset: 0x000F669C
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

		// Token: 0x06003465 RID: 13413 RVA: 0x000F851C File Offset: 0x000F671C
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

		// Token: 0x06003466 RID: 13414 RVA: 0x000F85B8 File Offset: 0x000F67B8
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

		// Token: 0x06003467 RID: 13415 RVA: 0x000F8614 File Offset: 0x000F6814
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

		// Token: 0x0400139D RID: 5021
		private IEntityWithRelationships _owner;

		// Token: 0x0400139E RID: 5022
		private List<RelatedEnd> _relationships;

		// Token: 0x0400139F RID: 5023
		[NonSerialized]
		private bool _nodeVisited;

		// Token: 0x040013A0 RID: 5024
		[NonSerialized]
		private IEntityWrapper _wrappedOwner;

		// Token: 0x040013A1 RID: 5025
		[NonSerialized]
		private EntityWrapperFactory _entityWrapperFactory;

		// Token: 0x040013A2 RID: 5026
		[NonSerialized]
		private ExpensiveOSpaceLoader _expensiveLoader;
	}
}
