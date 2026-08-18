using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000322 RID: 802
	internal class ReferenceTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x06002F4E RID: 12110 RVA: 0x000B2BEA File Offset: 0x000B0DEA
		internal ReferenceTypeElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000B2F58 File Offset: 0x000B1158
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Type"))
			{
				this.HandleTypeElementAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000B2F7C File Offset: 0x000B117C
		protected void HandleTypeElementAttribute(XmlReader reader)
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

		// Token: 0x06002F51 RID: 12113 RVA: 0x000B2FB1 File Offset: 0x000B11B1
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Ref(" + base.UnresolvedType + ")");
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000B2FCF File Offset: 0x000B11CF
		internal override TypeUsage GetTypeUsage()
		{
			return this._typeUsage;
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000B2FD8 File Offset: 0x000B11D8
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (this._typeUsage == null)
			{
				EdmType edmType = (EdmType)Converter.LoadSchemaElement(this._type, this._type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
				EntityType entityType = edmType as EntityType;
				RefType refType = new RefType(entityType);
				refType.AddMetadataProperties(base.OtherContent);
				this._typeUsage = TypeUsage.Create(refType);
			}
			return true;
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000B3037 File Offset: 0x000B1237
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateRefType(this, this._type);
		}
	}
}
