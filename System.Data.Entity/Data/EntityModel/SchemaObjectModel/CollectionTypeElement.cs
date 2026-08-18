using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000320 RID: 800
	internal class CollectionTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x06002F3C RID: 12092 RVA: 0x000B2BEA File Offset: 0x000B0DEA
		internal CollectionTypeElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x000B2BF3 File Offset: 0x000B0DF3
		internal ModelFunctionTypeElement SubElement
		{
			get
			{
				return this._typeSubElement;
			}
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000B2BFB File Offset: 0x000B0DFB
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ElementType"))
			{
				this.HandleElementTypeAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000B2C20 File Offset: 0x000B0E20
		protected void HandleElementTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			this._unresolvedType = text;
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000B2C58 File Offset: 0x000B0E58
		protected override bool HandleElement(XmlReader reader)
		{
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

		// Token: 0x06002F41 RID: 12097 RVA: 0x000B2CC4 File Offset: 0x000B0EC4
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000B2CE8 File Offset: 0x000B0EE8
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000B2D0C File Offset: 0x000B0F0C
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000B2D30 File Offset: 0x000B0F30
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000B2D52 File Offset: 0x000B0F52
		internal override void ResolveTopLevelNames()
		{
			if (this._typeSubElement != null)
			{
				this._typeSubElement.ResolveTopLevelNames();
			}
			if (this._unresolvedType != null)
			{
				base.ResolveTopLevelNames();
			}
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000B2D78 File Offset: 0x000B0F78
		internal override void WriteIdentity(StringBuilder builder)
		{
			if (base.UnresolvedType != null && !base.UnresolvedType.Trim().Equals(string.Empty))
			{
				builder.Append("Collection(" + base.UnresolvedType + ")");
				return;
			}
			builder.Append("Collection(");
			this._typeSubElement.WriteIdentity(builder);
			builder.Append(")");
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000B2DE8 File Offset: 0x000B0FE8
		internal override TypeUsage GetTypeUsage()
		{
			if (this._typeUsage != null)
			{
				return this._typeUsage;
			}
			if (this._typeSubElement != null)
			{
				CollectionType collectionType = new CollectionType(this._typeSubElement.GetTypeUsage());
				collectionType.AddMetadataProperties(base.OtherContent);
				this._typeUsage = TypeUsage.Create(collectionType);
			}
			return this._typeUsage;
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000B2E3C File Offset: 0x000B103C
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (this._typeUsage != null)
			{
				return true;
			}
			if (this._typeSubElement != null)
			{
				return this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
			}
			if (this._type is ScalarType)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(this._type as ScalarType, false);
				this._typeUsage = TypeUsage.Create(new CollectionType(this._typeUsageBuilder.TypeUsage));
				return true;
			}
			EdmType edmType = (EdmType)Converter.LoadSchemaElement(this._type, this._type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
			if (edmType != null)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
				this._typeUsage = TypeUsage.Create(new CollectionType(this._typeUsageBuilder.TypeUsage));
			}
			return this._typeUsage != null;
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000B2F04 File Offset: 0x000B1104
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			ValidationHelper.ValidateTypeDeclaration(this, this._type, this._typeSubElement);
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x0400145B RID: 5211
		private ModelFunctionTypeElement _typeSubElement;
	}
}
