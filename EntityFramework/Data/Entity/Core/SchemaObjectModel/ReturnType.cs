using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000382 RID: 898
	internal class ReturnType : ModelFunctionTypeElement
	{
		// Token: 0x06002074 RID: 8308 RVA: 0x000994C4 File Offset: 0x000976C4
		internal ReturnType(Function parentElement) : base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x000994D9 File Offset: 0x000976D9
		internal bool IsRefType
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x000994E1 File Offset: 0x000976E1
		internal CollectionKind CollectionKind
		{
			get
			{
				return this._collectionKind;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x000994E9 File Offset: 0x000976E9
		internal EntityContainerEntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x000994F1 File Offset: 0x000976F1
		internal bool EntitySetPathDefined
		{
			get
			{
				return this._entitySetPathDefined;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x000994F9 File Offset: 0x000976F9
		internal ModelFunctionTypeElement SubElement
		{
			get
			{
				return this._typeSubElement;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x00099504 File Offset: 0x00097704
		internal override TypeUsage TypeUsage
		{
			get
			{
				if (this._typeSubElement != null)
				{
					return this._typeSubElement.GetTypeUsage();
				}
				if (this._typeUsage != null)
				{
					return this._typeUsage;
				}
				if (base.TypeUsage == null)
				{
					return null;
				}
				if (this._collectionKind != CollectionKind.None)
				{
					return TypeUsage.Create(new CollectionType(base.TypeUsage));
				}
				return base.TypeUsage;
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x00099560 File Offset: 0x00097760
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new ReturnType((Function)parentElement)
			{
				_type = this._type,
				Name = this.Name,
				_typeUsageBuilder = this._typeUsageBuilder,
				_unresolvedType = this._unresolvedType,
				_unresolvedEntitySet = this._unresolvedEntitySet,
				_entitySetPathDefined = this._entitySetPathDefined,
				_entitySet = this._entitySet
			};
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000995D0 File Offset: 0x000977D0
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Type"))
			{
				this.HandleTypeAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntitySet"))
			{
				this.HandleEntitySetAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntitySetPath"))
			{
				this.HandleEntitySetPathAttribute(reader);
				return true;
			}
			return this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0009963B File Offset: 0x0009783B
		internal bool ResolveNestedTypeNames(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0009964C File Offset: 0x0009784C
		private void HandleTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			TypeModifier typeModifier;
			Function.RemoveTypeModifier(ref text, out typeModifier, out this._isRefType);
			TypeModifier typeModifier2 = typeModifier;
			if (typeModifier2 == TypeModifier.Array)
			{
				this._collectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			base.UnresolvedType = text;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000996A0 File Offset: 0x000978A0
		private void HandleEntitySetAttribute(XmlReader reader)
		{
			string unresolvedEntitySet;
			if (Utils.GetString(base.Schema, reader, out unresolvedEntitySet))
			{
				this._unresolvedEntitySet = unresolvedEntitySet;
			}
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000996C4 File Offset: 0x000978C4
		private void HandleEntitySetPathAttribute(XmlReader reader)
		{
			string text;
			if (Utils.GetString(base.Schema, reader, out text))
			{
				this._entitySetPathDefined = true;
			}
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000996E8 File Offset: 0x000978E8
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "CollectionType"))
			{
				this.HandleCollectionTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ReferenceType"))
			{
				this.HandleReferenceTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "TypeRef"))
			{
				this.HandleTypeRefElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "RowType"))
			{
				this.HandleRowTypeElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00099760 File Offset: 0x00097960
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x00099784 File Offset: 0x00097984
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000997A8 File Offset: 0x000979A8
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000997CC File Offset: 0x000979CC
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000997F0 File Offset: 0x000979F0
		internal override void ResolveTopLevelNames()
		{
			if (this._unresolvedType != null)
			{
				base.ResolveTopLevelNames();
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.ResolveTopLevelNames();
			}
			if (base.ParentElement.IsFunctionImport && this._unresolvedEntitySet != null)
			{
				((FunctionImportElement)base.ParentElement).ResolveEntitySet(this, this._unresolvedEntitySet, ref this._entitySet);
			}
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x0009985C File Offset: 0x00097A5C
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateTypeDeclaration(this, this._type, this._typeSubElement);
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			if (this._isRefType)
			{
				ValidationHelper.ValidateRefType(this, this._type);
			}
			if (base.Schema.DataModel != SchemaDataModelOption.EntityDataModel)
			{
				if (base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel)
				{
					if ((this._type != null && (!(this._type is ScalarType) || this._collectionKind != CollectionKind.None)) || (this._typeSubElement != null && !(this._typeSubElement.Type is ScalarType)))
					{
						string p2 = "";
						if (this._type != null)
						{
							p2 = Function.GetTypeNameForErrorMessage(this._type, this._collectionKind, this._isRefType);
						}
						else if (this._typeSubElement != null)
						{
							p2 = this._typeSubElement.FQName;
						}
						base.AddError(ErrorCode.FunctionWithNonEdmTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonEdmPrimitiveTypeNotSupported(p2, base.ParentElement.FQName));
					}
				}
				else if (this._type != null)
				{
					if (!(this._type is ScalarType) || this._collectionKind != CollectionKind.None)
					{
						base.AddError(ErrorCode.FunctionWithNonPrimitiveTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonPrimitiveTypeNotSupported(this._isRefType ? this._unresolvedType : this._type.FQName, base.ParentElement.FQName));
					}
				}
				else if (this._typeSubElement != null && !(this._typeSubElement.Type is ScalarType))
				{
					if (base.Schema.SchemaVersion < 3.0)
					{
						base.AddError(ErrorCode.FunctionWithNonPrimitiveTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonPrimitiveTypeNotSupported(this._typeSubElement.FQName, base.ParentElement.FQName));
					}
					else
					{
						CollectionTypeElement collectionTypeElement = this._typeSubElement as CollectionTypeElement;
						if (collectionTypeElement != null)
						{
							RowTypeElement rowTypeElement = collectionTypeElement.SubElement as RowTypeElement;
							if (rowTypeElement != null)
							{
								if (rowTypeElement.Properties.Any((RowTypePropertyElement p) => !p.ValidateIsScalar()))
								{
									base.AddError(ErrorCode.TVFReturnTypeRowHasNonScalarProperty, EdmSchemaErrorSeverity.Error, this, Strings.TVFReturnTypeRowHasNonScalarProperty);
								}
							}
						}
					}
				}
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00099A88 File Offset: 0x00097C88
		internal override void WriteIdentity(StringBuilder builder)
		{
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00099A8A File Offset: 0x00097C8A
		internal override TypeUsage GetTypeUsage()
		{
			return this.TypeUsage;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00099A92 File Offset: 0x00097C92
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return false;
		}

		// Token: 0x04000B82 RID: 2946
		private CollectionKind _collectionKind;

		// Token: 0x04000B83 RID: 2947
		private bool _isRefType;

		// Token: 0x04000B84 RID: 2948
		private string _unresolvedEntitySet;

		// Token: 0x04000B85 RID: 2949
		private bool _entitySetPathDefined;

		// Token: 0x04000B86 RID: 2950
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x04000B87 RID: 2951
		private EntityContainerEntitySet _entitySet;
	}
}
