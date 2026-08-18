using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000305 RID: 773
	internal class ReturnType : ModelFunctionTypeElement
	{
		// Token: 0x06002DC6 RID: 11718 RVA: 0x000AD434 File Offset: 0x000AB634
		internal ReturnType(Function parentElement) : base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x000AD449 File Offset: 0x000AB649
		internal bool IsRefType
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x000AD451 File Offset: 0x000AB651
		internal CollectionKind CollectionKind
		{
			get
			{
				return this._collectionKind;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x000AD459 File Offset: 0x000AB659
		internal EntityContainerEntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000AD461 File Offset: 0x000AB661
		internal bool EntitySetPathDefined
		{
			get
			{
				return this._entitySetPathDefined;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002DCB RID: 11723 RVA: 0x000AD469 File Offset: 0x000AB669
		internal ModelFunctionTypeElement SubElement
		{
			get
			{
				return this._typeSubElement;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x000AD474 File Offset: 0x000AB674
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

		// Token: 0x06002DCD RID: 11725 RVA: 0x000AD4D0 File Offset: 0x000AB6D0
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

		// Token: 0x06002DCE RID: 11726 RVA: 0x000AD540 File Offset: 0x000AB740
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

		// Token: 0x06002DCF RID: 11727 RVA: 0x000AD5AB File Offset: 0x000AB7AB
		internal bool ResolveNestedTypeNames(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000AD5BC File Offset: 0x000AB7BC
		private void HandleTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			TypeModifier typeModifier;
			Function.RemoveTypeModifier(ref text, out typeModifier, out this._isRefType);
			if (typeModifier == TypeModifier.Array)
			{
				this._collectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			base.UnresolvedType = text;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000AD60C File Offset: 0x000AB80C
		private void HandleEntitySetAttribute(XmlReader reader)
		{
			string unresolvedEntitySet;
			if (Utils.GetString(base.Schema, reader, out unresolvedEntitySet))
			{
				this._unresolvedEntitySet = unresolvedEntitySet;
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000AD630 File Offset: 0x000AB830
		private void HandleEntitySetPathAttribute(XmlReader reader)
		{
			string text;
			if (Utils.GetString(base.Schema, reader, out text))
			{
				this._entitySetPathDefined = true;
			}
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000AD654 File Offset: 0x000AB854
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

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000AD6CC File Offset: 0x000AB8CC
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000AD6F0 File Offset: 0x000AB8F0
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000AD714 File Offset: 0x000AB914
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000AD738 File Offset: 0x000AB938
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000AD75C File Offset: 0x000AB95C
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

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000AD7BC File Offset: 0x000AB9BC
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

		// Token: 0x06002DDA RID: 11738 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void WriteIdentity(StringBuilder builder)
		{
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000AD9EA File Offset: 0x000ABBEA
		internal override TypeUsage GetTypeUsage()
		{
			return this.TypeUsage;
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return false;
		}

		// Token: 0x040013F1 RID: 5105
		private CollectionKind _collectionKind;

		// Token: 0x040013F2 RID: 5106
		private bool _isRefType;

		// Token: 0x040013F3 RID: 5107
		private string _unresolvedEntitySet;

		// Token: 0x040013F4 RID: 5108
		private bool _entitySetPathDefined;

		// Token: 0x040013F5 RID: 5109
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x040013F6 RID: 5110
		private EntityContainerEntitySet _entitySet;
	}
}
