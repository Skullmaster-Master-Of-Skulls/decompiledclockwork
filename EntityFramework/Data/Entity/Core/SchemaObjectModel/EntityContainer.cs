using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200035C RID: 860
	[DebuggerDisplay("Name={Name}")]
	internal sealed class EntityContainer : SchemaType
	{
		// Token: 0x06001EB4 RID: 7860 RVA: 0x00092CD7 File Offset: 0x00090ED7
		public EntityContainer(Schema parentElement) : base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x00092D03 File Offset: 0x00090F03
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

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x00092D1E File Offset: 0x00090F1E
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

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x00092D3F File Offset: 0x00090F3F
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

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x00092D60 File Offset: 0x00090F60
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

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x00092D81 File Offset: 0x00090F81
		public EntityContainer ExtendingEntityContainer
		{
			get
			{
				return this._entityContainerGettingExtended;
			}
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00092D89 File Offset: 0x00090F89
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

		// Token: 0x06001EBB RID: 7867 RVA: 0x00092DB0 File Offset: 0x00090FB0
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
					this.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00092E4C File Offset: 0x0009104C
		private void HandleEntitySetElement(XmlReader reader)
		{
			EntityContainerEntitySet entityContainerEntitySet = new EntityContainerEntitySet(this);
			entityContainerEntitySet.Parse(reader);
			this.Members.Add(entityContainerEntitySet, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00092E80 File Offset: 0x00091080
		private void HandleAssociationSetElement(XmlReader reader)
		{
			EntityContainerAssociationSet entityContainerAssociationSet = new EntityContainerAssociationSet(this);
			entityContainerAssociationSet.Parse(reader);
			this.Members.Add(entityContainerAssociationSet, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00092EB4 File Offset: 0x000910B4
		private void HandleFunctionImport(XmlReader reader)
		{
			FunctionImportElement functionImportElement = new FunctionImportElement(this);
			functionImportElement.Parse(reader);
			this.Members.Add(functionImportElement, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00092EE8 File Offset: 0x000910E8
		private void HandleExtendsAttribute(XmlReader reader)
		{
			this._unresolvedExtendedEntityContainerName = base.HandleUndottedNameAttribute(reader, this._unresolvedExtendedEntityContainerName);
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00092F00 File Offset: 0x00091100
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

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00092FE8 File Offset: 0x000911E8
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (SchemaElement schemaElement in this.Members)
			{
				schemaElement.ResolveSecondLevelNames();
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0009303C File Offset: 0x0009123C
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

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00093148 File Offset: 0x00091348
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

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000931B4 File Offset: 0x000913B4
		private void DuplicateOrEquivalentMemberNameWhileExtendingEntityContainer(SchemaElement schemaElement, AddErrorKind error)
		{
			if (error != AddErrorKind.Succeeded)
			{
				schemaElement.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.DuplicateMemberNameInExtendedEntityContainer(schemaElement.Name, this.ExtendingEntityContainer.Name, this.Name));
			}
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000931E0 File Offset: 0x000913E0
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

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000932E4 File Offset: 0x000914E4
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

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00093410 File Offset: 0x00091610
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

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00093460 File Offset: 0x00091660
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

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000934C8 File Offset: 0x000916C8
		public override string FQName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x000934D0 File Offset: 0x000916D0
		public override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000934D8 File Offset: 0x000916D8
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

		// Token: 0x06001ECC RID: 7884 RVA: 0x00093578 File Offset: 0x00091778
		private static bool AreRelationshipEndsEqual(EntityContainerRelationshipSetEnd left, EntityContainerRelationshipSetEnd right)
		{
			return object.ReferenceEquals(left.EntitySet, right.EntitySet) && object.ReferenceEquals(left.ParentElement.Relationship, right.ParentElement.Relationship) && left.Name == right.Name;
		}

		// Token: 0x04000A79 RID: 2681
		private SchemaElementLookUpTable<SchemaElement> _members;

		// Token: 0x04000A7A RID: 2682
		private ISchemaElementLookUpTable<EntityContainerEntitySet> _entitySets;

		// Token: 0x04000A7B RID: 2683
		private ISchemaElementLookUpTable<EntityContainerRelationshipSet> _relationshipSets;

		// Token: 0x04000A7C RID: 2684
		private ISchemaElementLookUpTable<Function> _functionImports;

		// Token: 0x04000A7D RID: 2685
		private string _unresolvedExtendedEntityContainerName;

		// Token: 0x04000A7E RID: 2686
		private EntityContainer _entityContainerGettingExtended;

		// Token: 0x04000A7F RID: 2687
		private bool _isAlreadyValidated;

		// Token: 0x04000A80 RID: 2688
		private bool _isAlreadyResolved;
	}
}
