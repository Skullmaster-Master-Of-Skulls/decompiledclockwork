using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200037B RID: 891
	internal class ReferenceTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x06002020 RID: 8224 RVA: 0x000982BC File Offset: 0x000964BC
		internal ReferenceTypeElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000982C5 File Offset: 0x000964C5
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

		// Token: 0x06002022 RID: 8226 RVA: 0x000982EC File Offset: 0x000964EC
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

		// Token: 0x06002023 RID: 8227 RVA: 0x00098321 File Offset: 0x00096521
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Ref(" + base.UnresolvedType + ")");
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x0009833F File Offset: 0x0009653F
		internal override TypeUsage GetTypeUsage()
		{
			return this._typeUsage;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x00098348 File Offset: 0x00096548
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

		// Token: 0x06002026 RID: 8230 RVA: 0x000983A7 File Offset: 0x000965A7
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateRefType(this, this._type);
		}
	}
}
