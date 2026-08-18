using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000394 RID: 916
	internal class StructuredProperty : Property
	{
		// Token: 0x0600210A RID: 8458 RVA: 0x0009B643 File Offset: 0x00099843
		internal StructuredProperty(StructuredType parentElement) : base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x0009B658 File Offset: 0x00099858
		public override SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x0009B660 File Offset: 0x00099860
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsageBuilder.TypeUsage;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x0009B66D File Offset: 0x0009986D
		public bool Nullable
		{
			get
			{
				return this._typeUsageBuilder.Nullable;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x0009B67A File Offset: 0x0009987A
		public string Default
		{
			get
			{
				return this._typeUsageBuilder.Default;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x0009B687 File Offset: 0x00099887
		public object DefaultAsObject
		{
			get
			{
				return this._typeUsageBuilder.DefaultAsObject;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x0009B694 File Offset: 0x00099894
		public CollectionKind CollectionKind
		{
			get
			{
				return this._collectionKind;
			}
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0009B69C File Offset: 0x0009989C
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

		// Token: 0x06002112 RID: 8466 RVA: 0x0009B6F8 File Offset: 0x000998F8
		internal void EnsureEnumTypeFacets(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EdmType edmType = (EdmType)Converter.LoadSchemaElement(this.Type, this.Type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
			this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0009B738 File Offset: 0x00099938
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

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x0009B787 File Offset: 0x00099987
		// (set) Token: 0x06002115 RID: 8469 RVA: 0x0009B78F File Offset: 0x0009998F
		internal string UnresolvedType { get; set; }

		// Token: 0x06002116 RID: 8470 RVA: 0x0009B798 File Offset: 0x00099998
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

		// Token: 0x06002117 RID: 8471 RVA: 0x0009B81C File Offset: 0x00099A1C
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

		// Token: 0x06002118 RID: 8472 RVA: 0x0009B874 File Offset: 0x00099A74
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

		// Token: 0x06002119 RID: 8473 RVA: 0x0009B8B8 File Offset: 0x00099AB8
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
			}
		}

		// Token: 0x04000BBA RID: 3002
		private SchemaType _type;

		// Token: 0x04000BBB RID: 3003
		private readonly TypeUsageBuilder _typeUsageBuilder;

		// Token: 0x04000BBC RID: 3004
		private CollectionKind _collectionKind;
	}
}
