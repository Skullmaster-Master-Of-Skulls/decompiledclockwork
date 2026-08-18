using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000369 RID: 873
	internal class FunctionImportElement : Function
	{
		// Token: 0x06001F53 RID: 8019 RVA: 0x00095413 File Offset: 0x00093613
		internal FunctionImportElement(EntityContainer container) : base(container.Schema)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
			this._container = container;
			this._isComposable = false;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x00095452 File Offset: 0x00093652
		public override bool IsFunctionImport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x00095455 File Offset: 0x00093655
		public override string FQName
		{
			get
			{
				return this._container.Name + "." + this.Name;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x00095472 File Offset: 0x00093672
		public override string Identity
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x0009547A File Offset: 0x0009367A
		public EntityContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x00095482 File Offset: 0x00093682
		public EntityContainerEntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0009548C File Offset: 0x0009368C
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

		// Token: 0x06001F5A RID: 8026 RVA: 0x00095528 File Offset: 0x00093728
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			this.ResolveEntitySet(this, this._unresolvedEntitySet, ref this._entitySet);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00095543 File Offset: 0x00093743
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

		// Token: 0x06001F5C RID: 8028 RVA: 0x00095578 File Offset: 0x00093778
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

		// Token: 0x06001F5D RID: 8029 RVA: 0x000956D4 File Offset: 0x000938D4
		private void ValidateFunctionImportReturnType(SchemaElement owner, SchemaType returnType, CollectionKind returnTypeCollectionKind, EntityContainerEntitySet entitySet, bool entitySetPathDefined)
		{
			if (returnType != null && !this.ReturnTypeMeetsFunctionImportBasicRequirements(returnType, returnTypeCollectionKind))
			{
				owner.AddError(ErrorCode.FunctionImportUnsupportedReturnType, EdmSchemaErrorSeverity.Error, owner, this.GetReturnTypeErrorMessage(this.Name));
			}
			this.ValidateFunctionImportReturnType(owner, returnType, entitySet, entitySetPathDefined);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00095708 File Offset: 0x00093908
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
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

		// Token: 0x06001F5F RID: 8031 RVA: 0x000957C8 File Offset: 0x000939C8
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

		// Token: 0x06001F60 RID: 8032 RVA: 0x000958B8 File Offset: 0x00093AB8
		private string GetReturnTypeErrorMessage(string functionName)
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

		// Token: 0x06001F61 RID: 8033 RVA: 0x0009590C File Offset: 0x00093B0C
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

		// Token: 0x04000B3B RID: 2875
		private string _unresolvedEntitySet;

		// Token: 0x04000B3C RID: 2876
		private bool _entitySetPathDefined;

		// Token: 0x04000B3D RID: 2877
		private EntityContainer _container;

		// Token: 0x04000B3E RID: 2878
		private EntityContainerEntitySet _entitySet;

		// Token: 0x04000B3F RID: 2879
		private bool? _isSideEffecting;
	}
}
