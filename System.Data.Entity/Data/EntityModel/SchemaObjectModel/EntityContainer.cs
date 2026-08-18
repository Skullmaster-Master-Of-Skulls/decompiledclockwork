using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002E3 RID: 739
	[DebuggerDisplay("Name={Name}")]
	internal sealed class EntityContainer : SchemaType
	{
		// Token: 0x06002C5B RID: 11355 RVA: 0x000A8864 File Offset: 0x000A6A64
		public EntityContainer(Schema parentElement) : base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x000A8890 File Offset: 0x000A6A90
		private SchemaElementLookUpTable<SchemaElement> Members
		{
			get
			{
				if (this._members == null)
				{
					this._members = new SchemaElementLookUpTable<SchemaElement>();
				}
				return this._members;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x000A88AB File Offset: 0x000A6AAB
		public ISchemaElementLookUpTable<EntityContainerEntitySet> EntitySets
		{
			get
			{
				if (this._entitySets == null)
				{
					this._entitySets = new FilteredSchemaElementLookUpTable<EntityContainerEntitySet, SchemaElement>(this.Members);
				}
				return this._entitySets;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x000A88CC File Offset: 0x000A6ACC
		public ISchemaElementLookUpTable<EntityContainerRelationshipSet> RelationshipSets
		{
			get
			{
				if (this._relationshipSets == null)
				{
					this._relationshipSets = new FilteredSchemaElementLookUpTable<EntityContainerRelationshipSet, SchemaElement>(this.Members);
				}
				return this._relationshipSets;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000A88ED File Offset: 0x000A6AED
		public ISchemaElementLookUpTable<Function> FunctionImports
		{
			get
			{
				if (this._functionImports == null)
				{
					this._functionImports = new FilteredSchemaElementLookUpTable<Function, SchemaElement>(this.Members);
				}
				return this._functionImports;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000A890E File Offset: 0x000A6B0E
		public EntityContainer ExtendingEntityContainer
		{
			get
			{
				return this._entityContainerGettingExtended;
			}
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000A8916 File Offset: 0x000A6B16
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Extends"))
			{
				this.HandleExtendsAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000A893C File Offset: 0x000A6B3C
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "EntitySet"))
			{
				this.HandleEntitySetElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "AssociationSet"))
			{
				this.HandleAssociationSetElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "FunctionImport"))
			{
				this.HandleFunctionImport(reader);
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				if (base.CanHandleElement(reader, "ValueAnnotation"))
				{
					base.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					base.SkipElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000A89D8 File Offset: 0x000A6BD8
		private void HandleEntitySetElement(XmlReader reader)
		{
			EntityContainerEntitySet entityContainerEntitySet = new EntityContainerEntitySet(this);
			entityContainerEntitySet.Parse(reader);
			this.Members.Add(entityContainerEntitySet, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000A8A0C File Offset: 0x000A6C0C
		private void HandleAssociationSetElement(XmlReader reader)
		{
			EntityContainerAssociationSet entityContainerAssociationSet = new EntityContainerAssociationSet(this);
			entityContainerAssociationSet.Parse(reader);
			this.Members.Add(entityContainerAssociationSet, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000A8A40 File Offset: 0x000A6C40
		private void HandleFunctionImport(XmlReader reader)
		{
			FunctionImportElement functionImportElement = new FunctionImportElement(this);
			functionImportElement.Parse(reader);
			this.Members.Add(functionImportElement, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000A8A74 File Offset: 0x000A6C74
		private void HandleExtendsAttribute(XmlReader reader)
		{
			this._unresolvedExtendedEntityContainerName = base.HandleUndottedNameAttribute(reader, this._unresolvedExtendedEntityContainerName);
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000A8A8C File Offset: 0x000A6C8C
		internal override void ResolveTopLevelNames()
		{
			if (!this._isAlreadyResolved)
			{
				base.ResolveTopLevelNames();
				if (!string.IsNullOrEmpty(this._unresolvedExtendedEntityContainerName))
				{
					SchemaType schemaType;
					if (this._unresolvedExtendedEntityContainerName == this.Name)
					{
						base.AddError(ErrorCode.EntityContainerCannotExtendItself, EdmSchemaErrorSeverity.Error, Strings.EntityContainerCannotExtendItself(this.Name));
					}
					else if (!base.Schema.SchemaManager.TryResolveType(null, this._unresolvedExtendedEntityContainerName, out schemaType))
					{
						base.AddError(ErrorCode.InvalidEntityContainerNameInExtends, EdmSchemaErrorSeverity.Error, Strings.InvalidEntityContainerNameInExtends(this._unresolvedExtendedEntityContainerName));
					}
					else
					{
						this._entityContainerGettingExtended = (EntityContainer)schemaType;
						this._entityContainerGettingExtended.ResolveTopLevelNames();
					}
				}
				foreach (SchemaElement schemaElement in this.Members)
				{
					schemaElement.ResolveTopLevelNames();
				}
				this._isAlreadyResolved = true;
			}
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x000A8B74 File Offset: 0x000A6D74
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (SchemaElement schemaElement in this.Members)
			{
				schemaElement.ResolveSecondLevelNames();
			}
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000A8BC8 File Offset: 0x000A6DC8
		internal override void Validate()
		{
			if (!this._isAlreadyValidated)
			{
				base.Validate();
				if (this.ExtendingEntityContainer != null)
				{
					this.ExtendingEntityContainer.Validate();
					foreach (SchemaElement schemaElement in this.ExtendingEntityContainer.Members)
					{
						AddErrorKind error = this.Members.TryAdd(schemaElement.Clone(this));
						this.DuplicateOrEquivalentMemberNameWhileExtendingEntityContainer(schemaElement, error);
					}
				}
				HashSet<string> tableKeys = new HashSet<string>();
				foreach (SchemaElement schemaElement2 in this.Members)
				{
					EntityContainerEntitySet entityContainerEntitySet = schemaElement2 as EntityContainerEntitySet;
					if (entityContainerEntitySet != null && base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
					{
						this.CheckForDuplicateTableMapping(tableKeys, entityContainerEntitySet);
					}
					schemaElement2.Validate();
				}
				this.ValidateRelationshipSetHaveUniqueEnds();
				this.ValidateOnlyBaseEntitySetTypeDefinesConcurrency();
				this._isAlreadyValidated = true;
			}
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000A8CD4 File Offset: 0x000A6ED4
		internal EntityContainerEntitySet FindEntitySet(string name)
		{
			for (EntityContainer entityContainer = this; entityContainer != null; entityContainer = entityContainer.ExtendingEntityContainer)
			{
				foreach (EntityContainerEntitySet entityContainerEntitySet in entityContainer.EntitySets)
				{
					if (Utils.CompareNames(entityContainerEntitySet.Name, name) == 0)
					{
						return entityContainerEntitySet;
					}
				}
			}
			return null;
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000A8D40 File Offset: 0x000A6F40
		private void DuplicateOrEquivalentMemberNameWhileExtendingEntityContainer(SchemaElement schemaElement, AddErrorKind error)
		{
			if (error != AddErrorKind.Succeeded)
			{
				schemaElement.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.DuplicateMemberNameInExtendedEntityContainer(schemaElement.Name, this.ExtendingEntityContainer.Name, this.Name));
			}
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000A8D6C File Offset: 0x000A6F6C
		private void ValidateOnlyBaseEntitySetTypeDefinesConcurrency()
		{
			Dictionary<SchemaEntityType, EntityContainerEntitySet> dictionary = new Dictionary<SchemaEntityType, EntityContainerEntitySet>();
			foreach (SchemaElement schemaElement in this.Members)
			{
				EntityContainerEntitySet entityContainerEntitySet = schemaElement as EntityContainerEntitySet;
				if (entityContainerEntitySet != null && !dictionary.ContainsKey(entityContainerEntitySet.EntityType))
				{
					dictionary.Add(entityContainerEntitySet.EntityType, entityContainerEntitySet);
				}
			}
			foreach (SchemaType schemaType in base.Schema.SchemaTypes)
			{
				SchemaEntityType schemaEntityType = schemaType as SchemaEntityType;
				EntityContainerEntitySet entityContainerEntitySet2;
				if (schemaEntityType != null && EntityContainer.TypeIsSubTypeOf(schemaEntityType, dictionary, out entityContainerEntitySet2) && EntityContainer.TypeDefinesNewConcurrencyProperties(schemaEntityType))
				{
					base.AddError(ErrorCode.ConcurrencyRedefinedOnSubTypeOfEntitySetType, EdmSchemaErrorSeverity.Error, Strings.ConcurrencyRedefinedOnSubTypeOfEntitySetType(schemaEntityType.FQName, entityContainerEntitySet2.EntityType.FQName, entityContainerEntitySet2.FQName));
				}
			}
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000A8E6C File Offset: 0x000A706C
		private void ValidateRelationshipSetHaveUniqueEnds()
		{
			List<EntityContainerRelationshipSetEnd> list = new List<EntityContainerRelationshipSetEnd>();
			bool flag = true;
			foreach (EntityContainerRelationshipSet entityContainerRelationshipSet in this.RelationshipSets)
			{
				foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in entityContainerRelationshipSet.Ends)
				{
					flag = false;
					foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd2 in list)
					{
						if (EntityContainer.AreRelationshipEndsEqual(entityContainerRelationshipSetEnd2, entityContainerRelationshipSetEnd))
						{
							base.AddError(ErrorCode.SimilarRelationshipEnd, EdmSchemaErrorSeverity.Error, Strings.SimilarRelationshipEnd(entityContainerRelationshipSetEnd2.Name, entityContainerRelationshipSetEnd2.ParentElement.Name, entityContainerRelationshipSetEnd.ParentElement.Name, entityContainerRelationshipSetEnd2.EntitySet.Name, this.FQName));
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list.Add(entityContainerRelationshipSetEnd);
					}
				}
			}
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000A8F98 File Offset: 0x000A7198
		private static bool TypeIsSubTypeOf(SchemaEntityType itemType, Dictionary<SchemaEntityType, EntityContainerEntitySet> baseEntitySetTypes, out EntityContainerEntitySet set)
		{
			if (itemType.IsTypeHierarchyRoot)
			{
				set = null;
				return false;
			}
			for (SchemaEntityType schemaEntityType = itemType.BaseType as SchemaEntityType; schemaEntityType != null; schemaEntityType = (schemaEntityType.BaseType as SchemaEntityType))
			{
				if (baseEntitySetTypes.ContainsKey(schemaEntityType))
				{
					set = baseEntitySetTypes[schemaEntityType];
					return true;
				}
			}
			set = null;
			return false;
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000A8FE8 File Offset: 0x000A71E8
		private static bool TypeDefinesNewConcurrencyProperties(SchemaEntityType itemType)
		{
			foreach (StructuredProperty structuredProperty in itemType.Properties)
			{
				if (structuredProperty.Type is ScalarType && MetadataHelper.GetConcurrencyMode(structuredProperty.TypeUsage) != ConcurrencyMode.None)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000A9050 File Offset: 0x000A7250
		public override string FQName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x000A9050 File Offset: 0x000A7250
		public override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000A9058 File Offset: 0x000A7258
		private void CheckForDuplicateTableMapping(HashSet<string> tableKeys, EntityContainerEntitySet entitySet)
		{
			string text;
			if (string.IsNullOrEmpty(entitySet.DbSchema))
			{
				text = this.Name;
			}
			else
			{
				text = entitySet.DbSchema;
			}
			string text2;
			if (string.IsNullOrEmpty(entitySet.Table))
			{
				text2 = entitySet.Name;
			}
			else
			{
				text2 = entitySet.Table;
			}
			string item = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				text,
				text2
			});
			if (entitySet.DefiningQuery != null)
			{
				item = entitySet.Name;
			}
			bool flag = !tableKeys.Add(item);
			if (flag)
			{
				entitySet.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.DuplicateEntitySetTable(entitySet.Name, text, text2));
			}
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000A90F0 File Offset: 0x000A72F0
		private static bool AreRelationshipEndsEqual(EntityContainerRelationshipSetEnd left, EntityContainerRelationshipSetEnd right)
		{
			return left.EntitySet == right.EntitySet && left.ParentElement.Relationship == right.ParentElement.Relationship && left.Name == right.Name;
		}

		// Token: 0x04001302 RID: 4866
		private SchemaElementLookUpTable<SchemaElement> _members;

		// Token: 0x04001303 RID: 4867
		private ISchemaElementLookUpTable<EntityContainerEntitySet> _entitySets;

		// Token: 0x04001304 RID: 4868
		private ISchemaElementLookUpTable<EntityContainerRelationshipSet> _relationshipSets;

		// Token: 0x04001305 RID: 4869
		private ISchemaElementLookUpTable<Function> _functionImports;

		// Token: 0x04001306 RID: 4870
		private string _unresolvedExtendedEntityContainerName;

		// Token: 0x04001307 RID: 4871
		private EntityContainer _entityContainerGettingExtended;

		// Token: 0x04001308 RID: 4872
		private bool _isAlreadyValidated;

		// Token: 0x04001309 RID: 4873
		private bool _isAlreadyResolved;
	}
}
