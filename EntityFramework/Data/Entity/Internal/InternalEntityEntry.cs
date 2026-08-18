using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Internal.Validation;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000778 RID: 1912
	internal class InternalEntityEntry
	{
		// Token: 0x06005694 RID: 22164 RVA: 0x00176962 File Offset: 0x00174B62
		public InternalEntityEntry(InternalContext internalContext, IEntityStateEntry stateEntry)
		{
			this._internalContext = internalContext;
			this._stateEntry = stateEntry;
			this._entity = stateEntry.Entity;
			this._entityType = ObjectContextTypeCache.GetObjectType(this._entity.GetType());
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x0017699C File Offset: 0x00174B9C
		public InternalEntityEntry(InternalContext internalContext, object entity)
		{
			this._internalContext = internalContext;
			this._entity = entity;
			this._entityType = ObjectContextTypeCache.GetObjectType(this._entity.GetType());
			this._stateEntry = this._internalContext.GetStateEntry(entity);
			if (this._stateEntry == null)
			{
				this._internalContext.Set(this._entityType).InternalSet.Initialize();
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06005696 RID: 22166 RVA: 0x00176A08 File Offset: 0x00174C08
		public virtual object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06005697 RID: 22167 RVA: 0x00176A10 File Offset: 0x00174C10
		// (set) Token: 0x06005698 RID: 22168 RVA: 0x00176A28 File Offset: 0x00174C28
		public virtual EntityState State
		{
			get
			{
				if (!this.IsDetached)
				{
					return this._stateEntry.State;
				}
				return EntityState.Detached;
			}
			set
			{
				if (!this.IsDetached)
				{
					if (this._stateEntry.State == EntityState.Modified && value == EntityState.Unchanged)
					{
						this.CurrentValues.SetValues(this.OriginalValues);
					}
					this._stateEntry.ChangeState(value);
					return;
				}
				switch (value)
				{
				case EntityState.Unchanged:
					this._internalContext.Set(this._entityType).InternalSet.Attach(this._entity);
					return;
				case EntityState.Detached | EntityState.Unchanged:
					break;
				case EntityState.Added:
					this._internalContext.Set(this._entityType).InternalSet.Add(this._entity);
					return;
				default:
					if (value != EntityState.Deleted && value != EntityState.Modified)
					{
						return;
					}
					this._internalContext.Set(this._entityType).InternalSet.Attach(this._entity);
					this._stateEntry = this._internalContext.GetStateEntry(this._entity);
					this._stateEntry.ChangeState(value);
					break;
				}
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06005699 RID: 22169 RVA: 0x00176B16 File Offset: 0x00174D16
		public virtual InternalPropertyValues CurrentValues
		{
			get
			{
				this.ValidateStateToGetValues("CurrentValues", EntityState.Deleted);
				return new DbDataRecordPropertyValues(this._internalContext, this._entityType, this._stateEntry.CurrentValues, true);
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x0600569A RID: 22170 RVA: 0x00176B41 File Offset: 0x00174D41
		public virtual InternalPropertyValues OriginalValues
		{
			get
			{
				this.ValidateStateToGetValues("OriginalValues", EntityState.Added);
				return new DbDataRecordPropertyValues(this._internalContext, this._entityType, this._stateEntry.GetUpdatableOriginalValues(), true);
			}
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x00176B6C File Offset: 0x00174D6C
		public virtual InternalPropertyValues GetDatabaseValues()
		{
			this.ValidateStateToGetValues("GetDatabaseValues", EntityState.Added);
			DbDataRecord dbDataRecord = this.GetDatabaseValuesQuery().SingleOrDefault<DbDataRecord>();
			if (dbDataRecord != null)
			{
				return new ClonedPropertyValues(this.OriginalValues, dbDataRecord);
			}
			return null;
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x00176CD4 File Offset: 0x00174ED4
		public virtual async Task<InternalPropertyValues> GetDatabaseValuesAsync(CancellationToken cancellationToken)
		{
			this.ValidateStateToGetValues("GetDatabaseValuesAsync", EntityState.Added);
			cancellationToken.ThrowIfCancellationRequested();
			DbDataRecord dataRecord = await this.GetDatabaseValuesQuery().SingleOrDefaultAsync(cancellationToken).WithCurrentCulture<DbDataRecord>();
			return (dataRecord == null) ? null : new ClonedPropertyValues(this.OriginalValues, dataRecord);
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x00176D24 File Offset: 0x00174F24
		private ObjectQuery<DbDataRecord> GetDatabaseValuesQuery()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ");
			this.AppendEntitySqlRow(stringBuilder, "X", this.OriginalValues);
			string text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				DbHelpers.QuoteIdentifier(this._stateEntry.EntitySet.EntityContainer.Name),
				DbHelpers.QuoteIdentifier(this._stateEntry.EntitySet.Name)
			});
			string text2 = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				DbHelpers.QuoteIdentifier(this.EntityType.NestingNamespace()),
				DbHelpers.QuoteIdentifier(this.EntityType.Name)
			});
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " FROM (SELECT VALUE TREAT (Y AS {0}) FROM {1} AS Y) AS X WHERE ", new object[]
			{
				text2,
				text
			});
			EntityKeyMember[] entityKeyValues = this._stateEntry.EntityKey.EntityKeyValues;
			ObjectParameter[] array = new ObjectParameter[entityKeyValues.Length];
			for (int i = 0; i < entityKeyValues.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" AND ");
				}
				string text3 = string.Format(CultureInfo.InvariantCulture, "p{0}", new object[]
				{
					i.ToString(CultureInfo.InvariantCulture)
				});
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "X.{0} = @{1}", new object[]
				{
					DbHelpers.QuoteIdentifier(entityKeyValues[i].Key),
					text3
				});
				array[i] = new ObjectParameter(text3, entityKeyValues[i].Value);
			}
			return this._internalContext.ObjectContext.CreateQuery<DbDataRecord>(stringBuilder.ToString(), array);
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x00176EDC File Offset: 0x001750DC
		private void AppendEntitySqlRow(StringBuilder queryBuilder, string prefix, InternalPropertyValues templateValues)
		{
			bool flag = false;
			foreach (string text in templateValues.PropertyNames)
			{
				if (flag)
				{
					queryBuilder.Append(", ");
				}
				else
				{
					flag = true;
				}
				string text2 = DbHelpers.QuoteIdentifier(text);
				IPropertyValuesItem item = templateValues.GetItem(text);
				if (item.IsComplex)
				{
					InternalPropertyValues internalPropertyValues = item.Value as InternalPropertyValues;
					if (internalPropertyValues == null)
					{
						throw Error.DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull(text, this.EntityType.Name);
					}
					queryBuilder.Append("ROW(");
					this.AppendEntitySqlRow(queryBuilder, string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
					{
						prefix,
						text2
					}), internalPropertyValues);
					queryBuilder.AppendFormat(CultureInfo.InvariantCulture, ") AS {0}", new object[]
					{
						text2
					});
				}
				else
				{
					queryBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}.{1} ", new object[]
					{
						prefix,
						text2
					});
				}
			}
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x00176FFC File Offset: 0x001751FC
		private void ValidateStateToGetValues(string method, EntityState invalidState)
		{
			this.ValidateNotDetachedAndInitializeRelatedEnd(method);
			if (this.State == invalidState)
			{
				throw Error.DbPropertyValues_CannotGetValuesForState(method, this.State);
			}
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x00177020 File Offset: 0x00175220
		public virtual void Reload()
		{
			this.ValidateStateToGetValues("Reload", EntityState.Added);
			this._internalContext.ObjectContext.Refresh(RefreshMode.StoreWins, this.Entity);
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x00177045 File Offset: 0x00175245
		public virtual Task ReloadAsync(CancellationToken cancellationToken)
		{
			this.ValidateStateToGetValues("ReloadAsync", EntityState.Added);
			return this._internalContext.ObjectContext.RefreshAsync(RefreshMode.StoreWins, this.Entity, cancellationToken);
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x0017706B File Offset: 0x0017526B
		public virtual InternalReferenceEntry Reference(string navigationProperty, Type requestedType = null)
		{
			return (InternalReferenceEntry)this.ValidateAndGetNavigationMetadata(navigationProperty, requestedType ?? typeof(object), false).CreateMemberEntry(this, null);
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x00177090 File Offset: 0x00175290
		public virtual InternalCollectionEntry Collection(string navigationProperty, Type requestedType = null)
		{
			return (InternalCollectionEntry)this.ValidateAndGetNavigationMetadata(navigationProperty, requestedType ?? typeof(object), true).CreateMemberEntry(this, null);
		}

		// Token: 0x060056A4 RID: 22180 RVA: 0x001770B8 File Offset: 0x001752B8
		public virtual InternalMemberEntry Member(string propertyName, Type requestedType = null)
		{
			requestedType = (requestedType ?? typeof(object));
			IList<string> list = InternalEntityEntry.SplitName(propertyName);
			if (list.Count > 1)
			{
				return this.Property(null, propertyName, list, requestedType, false);
			}
			MemberEntryMetadata memberEntryMetadata = this.GetNavigationMetadata(propertyName) ?? this.ValidateAndGetPropertyMetadata(propertyName, this.EntityType, requestedType);
			if (memberEntryMetadata == null)
			{
				throw Error.DbEntityEntry_NotAProperty(propertyName, this.EntityType.Name);
			}
			if (memberEntryMetadata.MemberEntryType != MemberEntryType.CollectionNavigationProperty && !requestedType.IsAssignableFrom(memberEntryMetadata.MemberType))
			{
				throw Error.DbEntityEntry_WrongGenericForNavProp(propertyName, this.EntityType.Name, requestedType.Name, memberEntryMetadata.MemberType.Name);
			}
			return memberEntryMetadata.CreateMemberEntry(this, null);
		}

		// Token: 0x060056A5 RID: 22181 RVA: 0x00177163 File Offset: 0x00175363
		public virtual InternalPropertyEntry Property(string property, Type requestedType = null, bool requireComplex = false)
		{
			return this.Property(null, property, requestedType ?? typeof(object), requireComplex);
		}

		// Token: 0x060056A6 RID: 22182 RVA: 0x0017717D File Offset: 0x0017537D
		public InternalPropertyEntry Property(InternalPropertyEntry parentProperty, string propertyName, Type requestedType, bool requireComplex)
		{
			return this.Property(parentProperty, propertyName, InternalEntityEntry.SplitName(propertyName), requestedType, requireComplex);
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x00177190 File Offset: 0x00175390
		private InternalPropertyEntry Property(InternalPropertyEntry parentProperty, string propertyName, IList<string> properties, Type requestedType, bool requireComplex)
		{
			bool flag = properties.Count > 1;
			Type requestedType2 = flag ? typeof(object) : requestedType;
			Type type = (parentProperty != null) ? parentProperty.EntryMetadata.ElementType : this.EntityType;
			PropertyEntryMetadata propertyEntryMetadata = this.ValidateAndGetPropertyMetadata(properties[0], type, requestedType2);
			if (propertyEntryMetadata == null || ((flag || requireComplex) && !propertyEntryMetadata.IsComplex))
			{
				if (flag)
				{
					throw Error.DbEntityEntry_DottedPartNotComplex(properties[0], propertyName, type.Name);
				}
				throw requireComplex ? Error.DbEntityEntry_NotAComplexProperty(properties[0], type.Name) : Error.DbEntityEntry_NotAScalarProperty(properties[0], type.Name);
			}
			else
			{
				InternalPropertyEntry internalPropertyEntry = (InternalPropertyEntry)propertyEntryMetadata.CreateMemberEntry(this, parentProperty);
				if (!flag)
				{
					return internalPropertyEntry;
				}
				return this.Property(internalPropertyEntry, propertyName, properties.Skip(1).ToList<string>(), requestedType, requireComplex);
			}
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x00177264 File Offset: 0x00175464
		private NavigationEntryMetadata ValidateAndGetNavigationMetadata(string navigationProperty, Type requestedType, bool requireCollection)
		{
			if (InternalEntityEntry.SplitName(navigationProperty).Count != 1)
			{
				throw Error.DbEntityEntry_DottedPathMustBeProperty(navigationProperty);
			}
			NavigationEntryMetadata navigationMetadata = this.GetNavigationMetadata(navigationProperty);
			if (navigationMetadata == null)
			{
				throw Error.DbEntityEntry_NotANavigationProperty(navigationProperty, this.EntityType.Name);
			}
			if (requireCollection)
			{
				if (navigationMetadata.MemberEntryType == MemberEntryType.ReferenceNavigationProperty)
				{
					throw Error.DbEntityEntry_UsedCollectionForReferenceProp(navigationProperty, this.EntityType.Name);
				}
			}
			else if (navigationMetadata.MemberEntryType == MemberEntryType.CollectionNavigationProperty)
			{
				throw Error.DbEntityEntry_UsedReferenceForCollectionProp(navigationProperty, this.EntityType.Name);
			}
			if (!requestedType.IsAssignableFrom(navigationMetadata.ElementType))
			{
				throw Error.DbEntityEntry_WrongGenericForNavProp(navigationProperty, this.EntityType.Name, requestedType.Name, navigationMetadata.ElementType.Name);
			}
			return navigationMetadata;
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x00177310 File Offset: 0x00175510
		public virtual NavigationEntryMetadata GetNavigationMetadata(string propertyName)
		{
			EdmMember edmMember;
			this.EdmEntityType.Members.TryGetValue(propertyName, false, out edmMember);
			NavigationProperty navigationProperty = edmMember as NavigationProperty;
			if (navigationProperty != null)
			{
				return new NavigationEntryMetadata(this.EntityType, this.GetNavigationTargetType(navigationProperty), propertyName, navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many);
			}
			return null;
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x00177384 File Offset: 0x00175584
		private Type GetNavigationTargetType(NavigationProperty navigationProperty)
		{
			MetadataWorkspace metadataWorkspace = this._internalContext.ObjectContext.MetadataWorkspace;
			EntityType entityType = navigationProperty.RelationshipType.RelationshipEndMembers.Single((RelationshipEndMember e) => navigationProperty.ToEndMember.Name == e.Name).GetEntityType();
			StructuralType objectSpaceType = metadataWorkspace.GetObjectSpaceType(entityType);
			ObjectItemCollection objectItemCollection = (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace);
			return objectItemCollection.GetClrType(objectSpaceType);
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x001773F8 File Offset: 0x001755F8
		public virtual IRelatedEnd GetRelatedEnd(string navigationProperty)
		{
			EdmMember edmMember;
			this.EdmEntityType.Members.TryGetValue(navigationProperty, false, out edmMember);
			NavigationProperty navigationProperty2 = (NavigationProperty)edmMember;
			RelationshipManager relationshipManager = this._internalContext.ObjectContext.ObjectStateManager.GetRelationshipManager(this.Entity);
			return relationshipManager.GetRelatedEnd(navigationProperty2.RelationshipType.FullName, navigationProperty2.ToEndMember.Name);
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x00177459 File Offset: 0x00175659
		public virtual PropertyEntryMetadata ValidateAndGetPropertyMetadata(string propertyName, Type declaringType, Type requestedType)
		{
			return PropertyEntryMetadata.ValidateNameAndGetMetadata(this._internalContext, declaringType, requestedType, propertyName);
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x0017746C File Offset: 0x0017566C
		private static IList<string> SplitName(string propertyName)
		{
			return propertyName.Split(new char[]
			{
				'.'
			});
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x0017748C File Offset: 0x0017568C
		private void ValidateNotDetachedAndInitializeRelatedEnd(string method)
		{
			if (this.IsDetached)
			{
				throw Error.DbEntityEntry_NotSupportedForDetached(method, this._entityType.Name);
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060056AF RID: 22191 RVA: 0x001774A8 File Offset: 0x001756A8
		public virtual bool IsDetached
		{
			get
			{
				if (this._stateEntry == null || this._stateEntry.State == EntityState.Detached)
				{
					this._stateEntry = this._internalContext.GetStateEntry(this._entity);
					if (this._stateEntry == null)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060056B0 RID: 22192 RVA: 0x001774E2 File Offset: 0x001756E2
		public virtual Type EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060056B1 RID: 22193 RVA: 0x001774EC File Offset: 0x001756EC
		public virtual EntityType EdmEntityType
		{
			get
			{
				if (this._edmEntityType == null)
				{
					MetadataWorkspace metadataWorkspace = this._internalContext.ObjectContext.MetadataWorkspace;
					EntityType item = metadataWorkspace.GetItem<EntityType>(this._entityType.FullNameWithNesting(), DataSpace.OSpace);
					this._edmEntityType = (EntityType)metadataWorkspace.GetEdmSpaceType(item);
				}
				return this._edmEntityType;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x060056B2 RID: 22194 RVA: 0x0017753D File Offset: 0x0017573D
		public IEntityStateEntry ObjectStateEntry
		{
			get
			{
				return this._stateEntry;
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060056B3 RID: 22195 RVA: 0x00177545 File Offset: 0x00175745
		public InternalContext InternalContext
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x00177550 File Offset: 0x00175750
		public virtual DbEntityValidationResult GetValidationResult(IDictionary<object, object> items)
		{
			EntityValidator entityValidator = this.InternalContext.ValidationProvider.GetEntityValidator(this);
			bool lazyLoadingEnabled = this.InternalContext.LazyLoadingEnabled;
			this.InternalContext.LazyLoadingEnabled = false;
			DbEntityValidationResult result;
			try
			{
				result = ((entityValidator != null) ? entityValidator.Validate(this.InternalContext.ValidationProvider.GetEntityValidationContext(this, items)) : new DbEntityValidationResult(this, Enumerable.Empty<DbValidationError>()));
			}
			finally
			{
				this.InternalContext.LazyLoadingEnabled = lazyLoadingEnabled;
			}
			return result;
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x001775D0 File Offset: 0x001757D0
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && !(obj.GetType() != typeof(InternalEntityEntry)) && this.Equals((InternalEntityEntry)obj);
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x00177600 File Offset: 0x00175800
		public bool Equals(InternalEntityEntry other)
		{
			return object.ReferenceEquals(this, other) || (!object.ReferenceEquals(null, other) && object.ReferenceEquals(this._entity, other._entity) && object.ReferenceEquals(this._internalContext, other._internalContext));
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x0017763C File Offset: 0x0017583C
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this._entity);
		}

		// Token: 0x04002308 RID: 8968
		private readonly Type _entityType;

		// Token: 0x04002309 RID: 8969
		private readonly InternalContext _internalContext;

		// Token: 0x0400230A RID: 8970
		private readonly object _entity;

		// Token: 0x0400230B RID: 8971
		private IEntityStateEntry _stateEntry;

		// Token: 0x0400230C RID: 8972
		private EntityType _edmEntityType;
	}
}
