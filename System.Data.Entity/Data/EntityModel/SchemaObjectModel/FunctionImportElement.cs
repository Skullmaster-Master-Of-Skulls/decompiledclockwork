using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002EF RID: 751
	internal class FunctionImportElement : Function
	{
		// Token: 0x06002CD4 RID: 11476 RVA: 0x000AA363 File Offset: 0x000A8563
		internal FunctionImportElement(EntityContainer container) : base(container.Schema)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
			this._container = container;
			this._isComposable = false;
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x00017938 File Offset: 0x00015B38
		public override bool IsFunctionImport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000AA3A2 File Offset: 0x000A85A2
		public override string FQName
		{
			get
			{
				return this._container.Name + "." + this.Name;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x000AA3BF File Offset: 0x000A85BF
		public override string Identity
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x000AA3C7 File Offset: 0x000A85C7
		public EntityContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x000AA3CF File Offset: 0x000A85CF
		public EntityContainerEntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000AA3D8 File Offset: 0x000A85D8
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntitySet"))
			{
				string unresolvedEntitySet;
				if (Utils.GetString(base.Schema, reader, out unresolvedEntitySet))
				{
					this._unresolvedEntitySet = unresolvedEntitySet;
				}
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntitySetPath"))
			{
				string text;
				if (Utils.GetString(base.Schema, reader, out text))
				{
					this._entitySetPathDefined = true;
				}
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "IsBindable"))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "IsSideEffecting"))
			{
				bool value = true;
				if (base.HandleBoolAttribute(reader, ref value))
				{
					this._isSideEffecting = new bool?(value);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000AA474 File Offset: 0x000A8674
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			this.ResolveEntitySet(this, this._unresolvedEntitySet, ref this._entitySet);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000AA48F File Offset: 0x000A868F
		internal void ResolveEntitySet(SchemaElement owner, string unresolvedEntitySet, ref EntityContainerEntitySet entitySet)
		{
			if (entitySet == null && unresolvedEntitySet != null)
			{
				entitySet = this._container.FindEntitySet(unresolvedEntitySet);
				if (entitySet == null)
				{
					owner.AddError(ErrorCode.FunctionImportUnknownEntitySet, EdmSchemaErrorSeverity.Error, Strings.FunctionImportUnknownEntitySet(unresolvedEntitySet, this.FQName));
				}
			}
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000AA4C4 File Offset: 0x000A86C4
		internal override void Validate()
		{
			base.Validate();
			this.ValidateFunctionImportReturnType(this, this._type, base.CollectionKind, this._entitySet, this._entitySetPathDefined);
			if (this._returnTypeList != null)
			{
				foreach (ReturnType returnType in this._returnTypeList)
				{
					this.ValidateFunctionImportReturnType(returnType, returnType.Type, returnType.CollectionKind, returnType.EntitySet, returnType.EntitySetPathDefined);
				}
			}
			if (this._isComposable && this._isSideEffecting != null && this._isSideEffecting.Value)
			{
				base.AddError(ErrorCode.FunctionImportComposableAndSideEffectingNotAllowed, EdmSchemaErrorSeverity.Error, Strings.FunctionImportComposableAndSideEffectingNotAllowed(this.FQName));
			}
			if (this._parameters != null)
			{
				foreach (Parameter parameter in this._parameters)
				{
					if (parameter.IsRefType || parameter.CollectionKind != CollectionKind.None)
					{
						base.AddError(ErrorCode.FunctionImportCollectionAndRefParametersNotAllowed, EdmSchemaErrorSeverity.Error, Strings.FunctionImportCollectionAndRefParametersNotAllowed(this.FQName));
					}
					if (!parameter.TypeUsageBuilder.Nullable)
					{
						base.AddError(ErrorCode.FunctionImportNonNullableParametersNotAllowed, EdmSchemaErrorSeverity.Error, Strings.FunctionImportNonNullableParametersNotAllowed(this.FQName));
					}
				}
			}
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000AA620 File Offset: 0x000A8820
		private void ValidateFunctionImportReturnType(SchemaElement owner, SchemaType returnType, CollectionKind returnTypeCollectionKind, EntityContainerEntitySet entitySet, bool entitySetPathDefined)
		{
			if (returnType != null && !this.ReturnTypeMeetsFunctionImportBasicRequirements(returnType, returnTypeCollectionKind))
			{
				owner.AddError(ErrorCode.FunctionImportUnsupportedReturnType, EdmSchemaErrorSeverity.Error, owner, this.GetReturnTypeErrorMessage(base.Schema.SchemaVersion, this.Name));
			}
			this.ValidateFunctionImportReturnType(owner, returnType, entitySet, entitySetPathDefined);
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000AA660 File Offset: 0x000A8860
		private bool ReturnTypeMeetsFunctionImportBasicRequirements(SchemaType type, CollectionKind returnTypeCollectionKind)
		{
			if (type is ScalarType && returnTypeCollectionKind == CollectionKind.Bag)
			{
				return true;
			}
			if (type is SchemaEntityType && returnTypeCollectionKind == CollectionKind.Bag)
			{
				return true;
			}
			if (base.Schema.SchemaVersion == 1.1)
			{
				if (type is ScalarType && returnTypeCollectionKind == CollectionKind.None)
				{
					return true;
				}
				if (type is SchemaEntityType && returnTypeCollectionKind == CollectionKind.None)
				{
					return true;
				}
				if (type is SchemaComplexType && returnTypeCollectionKind == CollectionKind.None)
				{
					return true;
				}
				if (type is SchemaComplexType && returnTypeCollectionKind == CollectionKind.Bag)
				{
					return true;
				}
			}
			return (base.Schema.SchemaVersion >= 2.0 && type is SchemaComplexType && returnTypeCollectionKind == CollectionKind.Bag) || (base.Schema.SchemaVersion >= 3.0 && type is SchemaEnumType && returnTypeCollectionKind == CollectionKind.Bag);
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000AA720 File Offset: 0x000A8920
		private void ValidateFunctionImportReturnType(SchemaElement owner, SchemaType returnType, EntityContainerEntitySet entitySet, bool entitySetPathDefined)
		{
			SchemaEntityType schemaEntityType = returnType as SchemaEntityType;
			if (entitySet != null && entitySetPathDefined)
			{
				owner.AddError(ErrorCode.FunctionImportEntitySetAndEntitySetPathDeclared, EdmSchemaErrorSeverity.Error, Strings.FunctionImportEntitySetAndEntitySetPathDeclared(this.FQName));
			}
			if (schemaEntityType != null)
			{
				if (entitySet == null)
				{
					owner.AddError(ErrorCode.FunctionImportReturnsEntitiesButDoesNotSpecifyEntitySet, EdmSchemaErrorSeverity.Error, Strings.FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet(this.FQName));
					return;
				}
				if (entitySet.EntityType != null && !schemaEntityType.IsOfType(entitySet.EntityType))
				{
					owner.AddError(ErrorCode.FunctionImportEntityTypeDoesNotMatchEntitySet, EdmSchemaErrorSeverity.Error, Strings.FunctionImportEntityTypeDoesNotMatchEntitySet(this.FQName, entitySet.EntityType.FQName, entitySet.Name));
					return;
				}
			}
			else
			{
				SchemaComplexType schemaComplexType = returnType as SchemaComplexType;
				if (schemaComplexType != null)
				{
					if (entitySet != null || entitySetPathDefined)
					{
						owner.AddError(ErrorCode.ComplexTypeAsReturnTypeAndDefinedEntitySet, EdmSchemaErrorSeverity.Error, owner.LineNumber, owner.LinePosition, Strings.ComplexTypeAsReturnTypeAndDefinedEntitySet(this.FQName, schemaComplexType.Name));
						return;
					}
				}
				else if (entitySet != null || entitySetPathDefined)
				{
					owner.AddError(ErrorCode.FunctionImportSpecifiesEntitySetButDoesNotReturnEntityType, EdmSchemaErrorSeverity.Error, Strings.FunctionImportSpecifiesEntitySetButNotEntityType(this.FQName));
				}
			}
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000AA814 File Offset: 0x000A8A14
		private string GetReturnTypeErrorMessage(double schemaVersion, string functionName)
		{
			string result;
			if (base.Schema.SchemaVersion == 1.0)
			{
				result = Strings.FunctionImportWithUnsupportedReturnTypeV1(functionName);
			}
			else if (base.Schema.SchemaVersion == 1.1)
			{
				result = Strings.FunctionImportWithUnsupportedReturnTypeV1_1(functionName);
			}
			else
			{
				result = Strings.FunctionImportWithUnsupportedReturnTypeV2(functionName);
			}
			return result;
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000AA868 File Offset: 0x000A8A68
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			FunctionImportElement functionImportElement = new FunctionImportElement((EntityContainer)parentElement);
			base.CloneSetFunctionFields(functionImportElement);
			functionImportElement._container = this._container;
			functionImportElement._entitySet = this._entitySet;
			functionImportElement._unresolvedEntitySet = this._unresolvedEntitySet;
			functionImportElement._entitySetPathDefined = this._entitySetPathDefined;
			return functionImportElement;
		}

		// Token: 0x040013B7 RID: 5047
		private string _unresolvedEntitySet;

		// Token: 0x040013B8 RID: 5048
		private bool _entitySetPathDefined;

		// Token: 0x040013B9 RID: 5049
		private EntityContainer _container;

		// Token: 0x040013BA RID: 5050
		private EntityContainerEntitySet _entitySet;

		// Token: 0x040013BB RID: 5051
		private bool? _isSideEffecting;
	}
}
