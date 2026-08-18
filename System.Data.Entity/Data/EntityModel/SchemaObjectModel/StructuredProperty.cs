using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000316 RID: 790
	internal class StructuredProperty : Property
	{
		// Token: 0x06002EB1 RID: 11953 RVA: 0x000B07CF File Offset: 0x000AE9CF
		internal StructuredProperty(StructuredType parentElement) : base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x000B07E4 File Offset: 0x000AE9E4
		public override SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x000B07EC File Offset: 0x000AE9EC
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsageBuilder.TypeUsage;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x000B07F9 File Offset: 0x000AE9F9
		public bool Nullable
		{
			get
			{
				return this._typeUsageBuilder.Nullable;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x000B0806 File Offset: 0x000AEA06
		public string Default
		{
			get
			{
				return this._typeUsageBuilder.Default;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000B0813 File Offset: 0x000AEA13
		public object DefaultAsObject
		{
			get
			{
				return this._typeUsageBuilder.DefaultAsObject;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x000B0820 File Offset: 0x000AEA20
		public CollectionKind CollectionKind
		{
			get
			{
				return this._collectionKind;
			}
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000B0828 File Offset: 0x000AEA28
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this._type != null)
			{
				return;
			}
			this._type = this.ResolveType(this.UnresolvedType);
			this._typeUsageBuilder.ValidateDefaultValue(this._type);
			ScalarType scalarType = this._type as ScalarType;
			if (scalarType != null)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(scalarType, true);
			}
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000B0884 File Offset: 0x000AEA84
		internal void EnsureEnumTypeFacets(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EdmType edmType = (EdmType)Converter.LoadSchemaElement(this.Type, this.Type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
			this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000B08C4 File Offset: 0x000AEAC4
		protected virtual SchemaType ResolveType(string typeName)
		{
			SchemaType schemaType;
			if (!base.Schema.ResolveTypeName(this, typeName, out schemaType))
			{
				return null;
			}
			if (!(schemaType is SchemaComplexType) && !(schemaType is ScalarType) && !(schemaType is SchemaEnumType))
			{
				base.AddError(ErrorCode.InvalidPropertyType, EdmSchemaErrorSeverity.Error, Strings.InvalidPropertyType(this.UnresolvedType));
				return null;
			}
			return schemaType;
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x000B0915 File Offset: 0x000AEB15
		// (set) Token: 0x06002EBC RID: 11964 RVA: 0x000B091D File Offset: 0x000AEB1D
		internal string UnresolvedType
		{
			get
			{
				return this._unresolvedType;
			}
			set
			{
				this._unresolvedType = value;
			}
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000B0928 File Offset: 0x000AEB28
		internal override void Validate()
		{
			base.Validate();
			if (this._collectionKind != CollectionKind.Bag)
			{
				CollectionKind collectionKind = this._collectionKind;
			}
			SchemaEnumType schemaEnumType = this._type as SchemaEnumType;
			if (schemaEnumType != null)
			{
				this._typeUsageBuilder.ValidateEnumFacets(schemaEnumType);
				return;
			}
			if (this.Nullable && base.Schema.SchemaVersion != 1.1 && this._type is SchemaComplexType)
			{
				base.AddError(ErrorCode.NullableComplexType, EdmSchemaErrorSeverity.Error, Strings.ComplexObject_NullableComplexTypesNotSupported(this.FQName));
			}
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000B09AC File Offset: 0x000AEBAC
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
			if (SchemaElement.CanHandleAttribute(reader, "CollectionKind"))
			{
				this.HandleCollectionKindAttribute(reader);
				return true;
			}
			return this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000B0A04 File Offset: 0x000AEC04
		private void HandleTypeAttribute(XmlReader reader)
		{
			if (this.UnresolvedType != null)
			{
				base.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, reader, Strings.PropertyTypeAlreadyDefined(reader.Name));
				return;
			}
			string unresolvedType;
			if (!Utils.GetDottedName(base.Schema, reader, out unresolvedType))
			{
				return;
			}
			this.UnresolvedType = unresolvedType;
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000B0A48 File Offset: 0x000AEC48
		private void HandleCollectionKindAttribute(XmlReader reader)
		{
			string value = reader.Value;
			if (value == "None")
			{
				this._collectionKind = CollectionKind.None;
				return;
			}
			if (value == "List")
			{
				this._collectionKind = CollectionKind.List;
				return;
			}
			if (value == "Bag")
			{
				this._collectionKind = CollectionKind.Bag;
				return;
			}
		}

		// Token: 0x04001438 RID: 5176
		private SchemaType _type;

		// Token: 0x04001439 RID: 5177
		private string _unresolvedType;

		// Token: 0x0400143A RID: 5178
		private TypeUsageBuilder _typeUsageBuilder;

		// Token: 0x0400143B RID: 5179
		private CollectionKind _collectionKind;
	}
}
