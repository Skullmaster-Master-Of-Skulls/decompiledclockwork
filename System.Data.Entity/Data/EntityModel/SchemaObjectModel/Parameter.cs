using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002FC RID: 764
	internal class Parameter : FacetEnabledSchemaElement
	{
		// Token: 0x06002D58 RID: 11608 RVA: 0x000ABBDF File Offset: 0x000A9DDF
		internal Parameter(Function parentElement) : base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x000ABBFB File Offset: 0x000A9DFB
		internal ParameterDirection ParameterDirection
		{
			get
			{
				return this._parameterDirection;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000ABC03 File Offset: 0x000A9E03
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x000ABC0B File Offset: 0x000A9E0B
		internal CollectionKind CollectionKind
		{
			get
			{
				return this._collectionKind;
			}
			set
			{
				this._collectionKind = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000ABC14 File Offset: 0x000A9E14
		internal bool IsRefType
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000ABC1C File Offset: 0x000A9E1C
		internal override TypeUsage TypeUsage
		{
			get
			{
				if (this._typeSubElement != null)
				{
					return this._typeSubElement.GetTypeUsage();
				}
				if (base.TypeUsage == null)
				{
					return null;
				}
				if (this.CollectionKind != CollectionKind.None)
				{
					return TypeUsage.Create(new CollectionType(base.TypeUsage));
				}
				return base.TypeUsage;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x000AA111 File Offset: 0x000A8311
		internal new SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000ABC5C File Offset: 0x000A9E5C
		internal void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Parameter(");
			if (base.UnresolvedType != null && !base.UnresolvedType.Trim().Equals(string.Empty))
			{
				if (this._collectionKind != CollectionKind.None)
				{
					builder.Append("Collection(" + base.UnresolvedType + ")");
				}
				else if (this._isRefType)
				{
					builder.Append("Ref(" + base.UnresolvedType + ")");
				}
				else
				{
					builder.Append(base.UnresolvedType);
				}
			}
			else if (this._typeSubElement != null)
			{
				this._typeSubElement.WriteIdentity(builder);
			}
			builder.Append(")");
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000ABD10 File Offset: 0x000A9F10
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new Parameter((Function)parentElement)
			{
				_collectionKind = this._collectionKind,
				_parameterDirection = this._parameterDirection,
				_type = this._type,
				Name = this.Name,
				_typeUsageBuilder = this._typeUsageBuilder
			};
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000ABD66 File Offset: 0x000A9F66
		internal bool ResolveNestedTypeNames(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return this._typeSubElement != null && this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000ABD80 File Offset: 0x000A9F80
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
			if (SchemaElement.CanHandleAttribute(reader, "Mode"))
			{
				this.HandleModeAttribute(reader);
				return true;
			}
			return this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000ABDD8 File Offset: 0x000A9FD8
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
				this.CollectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			base.UnresolvedType = text;
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000ABE28 File Offset: 0x000AA028
		private void HandleModeAttribute(XmlReader reader)
		{
			string text = reader.Value;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			text = text.Trim();
			if (!string.IsNullOrEmpty(text))
			{
				if (text == "In")
				{
					this._parameterDirection = ParameterDirection.Input;
					return;
				}
				if (!(text == "Out"))
				{
					if (!(text == "InOut"))
					{
						this.AddErrorBadParameterDirection(text, reader, new Func<object, object, object, object, string>(Strings.BadParameterDirection));
					}
					else
					{
						this._parameterDirection = ParameterDirection.InputOutput;
						if (base.ParentElement.IsComposable && base.ParentElement.IsFunctionImport)
						{
							this.AddErrorBadParameterDirection(text, reader, new Func<object, object, object, object, string>(Strings.BadParameterDirectionForComposableFunctions));
							return;
						}
					}
				}
				else
				{
					this._parameterDirection = ParameterDirection.Output;
					if (base.ParentElement.IsComposable && base.ParentElement.IsFunctionImport)
					{
						this.AddErrorBadParameterDirection(text, reader, new Func<object, object, object, object, string>(Strings.BadParameterDirectionForComposableFunctions));
						return;
					}
				}
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000ABF08 File Offset: 0x000AA108
		private void AddErrorBadParameterDirection(string value, XmlReader reader, Func<object, object, object, object, string> errorFunc)
		{
			base.AddError(ErrorCode.BadParameterDirection, EdmSchemaErrorSeverity.Error, reader, errorFunc(base.ParentElement.Parameters.Count, base.ParentElement.Name, base.ParentElement.ParentElement.FQName, value));
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000ABF58 File Offset: 0x000AA158
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

		// Token: 0x06002D67 RID: 11623 RVA: 0x000AC008 File Offset: 0x000AA208
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000AC02C File Offset: 0x000AA22C
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000AC050 File Offset: 0x000AA250
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000AC074 File Offset: 0x000AA274
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000AC096 File Offset: 0x000AA296
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
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000AC0BC File Offset: 0x000AA2BC
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateTypeDeclaration(this, this._type, this._typeSubElement);
			if (base.Schema.DataModel != SchemaDataModelOption.EntityDataModel)
			{
				bool isAggregate = base.ParentElement.IsAggregate;
				if (this._type != null && (!(this._type is ScalarType) || (!isAggregate && this._collectionKind != CollectionKind.None)))
				{
					string p = "";
					if (this._type != null)
					{
						p = Function.GetTypeNameForErrorMessage(this._type, this._collectionKind, this._isRefType);
					}
					else if (this._typeSubElement != null)
					{
						p = this._typeSubElement.FQName;
					}
					if (base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel)
					{
						base.AddError(ErrorCode.FunctionWithNonEdmTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonEdmPrimitiveTypeNotSupported(p, base.ParentElement.FQName));
						return;
					}
					base.AddError(ErrorCode.FunctionWithNonPrimitiveTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonPrimitiveTypeNotSupported(p, base.ParentElement.FQName));
					return;
				}
			}
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			if (this._isRefType)
			{
				ValidationHelper.ValidateRefType(this, this._type);
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x040013DC RID: 5084
		private ParameterDirection _parameterDirection = ParameterDirection.Input;

		// Token: 0x040013DD RID: 5085
		private CollectionKind _collectionKind;

		// Token: 0x040013DE RID: 5086
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x040013DF RID: 5087
		private bool _isRefType;
	}
}
