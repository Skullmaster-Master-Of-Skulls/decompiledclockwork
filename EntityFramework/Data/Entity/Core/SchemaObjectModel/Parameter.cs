using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000376 RID: 886
	internal class Parameter : FacetEnabledSchemaElement
	{
		// Token: 0x06001FBA RID: 8122 RVA: 0x000966D6 File Offset: 0x000948D6
		internal Parameter(Function parentElement) : base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x000966F2 File Offset: 0x000948F2
		internal ParameterDirection ParameterDirection
		{
			get
			{
				return this._parameterDirection;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x000966FA File Offset: 0x000948FA
		// (set) Token: 0x06001FBD RID: 8125 RVA: 0x00096702 File Offset: 0x00094902
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x0009670B File Offset: 0x0009490B
		internal bool IsRefType
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x00096713 File Offset: 0x00094913
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

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x00096752 File Offset: 0x00094952
		internal new SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0009675C File Offset: 0x0009495C
		internal void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Parameter(");
			if (!string.IsNullOrWhiteSpace(base.UnresolvedType))
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

		// Token: 0x06001FC2 RID: 8130 RVA: 0x00096800 File Offset: 0x00094A00
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

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00096856 File Offset: 0x00094A56
		internal bool ResolveNestedTypeNames(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return this._typeSubElement != null && this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00096870 File Offset: 0x00094A70
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

		// Token: 0x06001FC5 RID: 8133 RVA: 0x000968C8 File Offset: 0x00094AC8
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
				this.CollectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			base.UnresolvedType = text;
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0009691C File Offset: 0x00094B1C
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
				string a;
				if ((a = text) != null)
				{
					if (a == "In")
					{
						this._parameterDirection = ParameterDirection.Input;
						return;
					}
					if (!(a == "Out"))
					{
						if (a == "InOut")
						{
							this._parameterDirection = ParameterDirection.InputOutput;
							if (base.ParentElement.IsComposable && base.ParentElement.IsFunctionImport)
							{
								this.AddErrorBadParameterDirection(text, reader, new Func<object, object, object, object, string>(Strings.BadParameterDirectionForComposableFunctions));
								return;
							}
							return;
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
						return;
					}
				}
				this.AddErrorBadParameterDirection(text, reader, new Func<object, object, object, object, string>(Strings.BadParameterDirection));
			}
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x00096A04 File Offset: 0x00094C04
		private void AddErrorBadParameterDirection(string value, XmlReader reader, Func<object, object, object, object, string> errorFunc)
		{
			base.AddError(ErrorCode.BadParameterDirection, EdmSchemaErrorSeverity.Error, reader, errorFunc(base.ParentElement.Parameters.Count, base.ParentElement.Name, base.ParentElement.ParentElement.FQName, value));
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x00096A54 File Offset: 0x00094C54
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

		// Token: 0x06001FC9 RID: 8137 RVA: 0x00096B04 File Offset: 0x00094D04
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00096B28 File Offset: 0x00094D28
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x00096B4C File Offset: 0x00094D4C
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x00096B70 File Offset: 0x00094D70
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x00096B92 File Offset: 0x00094D92
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

		// Token: 0x06001FCE RID: 8142 RVA: 0x00096BB8 File Offset: 0x00094DB8
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

		// Token: 0x04000B59 RID: 2905
		private ParameterDirection _parameterDirection = ParameterDirection.Input;

		// Token: 0x04000B5A RID: 2906
		private CollectionKind _collectionKind;

		// Token: 0x04000B5B RID: 2907
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x04000B5C RID: 2908
		private bool _isRefType;
	}
}
