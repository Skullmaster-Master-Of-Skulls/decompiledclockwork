using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000325 RID: 805
	internal class TypeRefElement : ModelFunctionTypeElement
	{
		// Token: 0x06002F6C RID: 12140 RVA: 0x000B2BEA File Offset: 0x000B0DEA
		internal TypeRefElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x000B372A File Offset: 0x000B192A
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
			return false;
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x000B3750 File Offset: 0x000B1950
		protected void HandleTypeAttribute(XmlReader reader)
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

		// Token: 0x06002F6F RID: 12143 RVA: 0x000B3788 File Offset: 0x000B1988
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (this._type is ScalarType)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(this._type as ScalarType, false);
				this._typeUsage = this._typeUsageBuilder.TypeUsage;
				return true;
			}
			EdmType edmType = (EdmType)Converter.LoadSchemaElement(this._type, this._type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
			if (edmType != null)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
				this._typeUsage = this._typeUsageBuilder.TypeUsage;
			}
			return this._typeUsage != null;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000B3819 File Offset: 0x000B1A19
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append(base.UnresolvedType);
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000B2FCF File Offset: 0x000B11CF
		internal override TypeUsage GetTypeUsage()
		{
			return this._typeUsage;
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000B3828 File Offset: 0x000B1A28
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
		}
	}
}
