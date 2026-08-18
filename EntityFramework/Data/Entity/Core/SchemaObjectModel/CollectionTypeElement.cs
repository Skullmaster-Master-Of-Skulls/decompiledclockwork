using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000358 RID: 856
	internal class CollectionTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x06001E9C RID: 7836 RVA: 0x0009285E File Offset: 0x00090A5E
		internal CollectionTypeElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00092867 File Offset: 0x00090A67
		internal ModelFunctionTypeElement SubElement
		{
			get
			{
				return this._typeSubElement;
			}
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0009286F File Offset: 0x00090A6F
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

		// Token: 0x06001E9F RID: 7839 RVA: 0x00092894 File Offset: 0x00090A94
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

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000928CC File Offset: 0x00090ACC
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

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00092938 File Offset: 0x00090B38
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0009295C File Offset: 0x00090B5C
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00092980 File Offset: 0x00090B80
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x000929A4 File Offset: 0x00090BA4
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x000929C6 File Offset: 0x00090BC6
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

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000929EC File Offset: 0x00090BEC
		internal override void WriteIdentity(StringBuilder builder)
		{
			if (!string.IsNullOrWhiteSpace(base.UnresolvedType))
			{
				builder.Append("Collection(" + base.UnresolvedType + ")");
				return;
			}
			builder.Append("Collection(");
			this._typeSubElement.WriteIdentity(builder);
			builder.Append(")");
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00092A48 File Offset: 0x00090C48
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

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00092A9C File Offset: 0x00090C9C
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

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00092B67 File Offset: 0x00090D67
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

		// Token: 0x04000A74 RID: 2676
		private ModelFunctionTypeElement _typeSubElement;
	}
}
