using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000398 RID: 920
	internal class TypeRefElement : ModelFunctionTypeElement
	{
		// Token: 0x06002132 RID: 8498 RVA: 0x0009BFFC File Offset: 0x0009A1FC
		internal TypeRefElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0009C005 File Offset: 0x0009A205
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

		// Token: 0x06002134 RID: 8500 RVA: 0x0009C02C File Offset: 0x0009A22C
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

		// Token: 0x06002135 RID: 8501 RVA: 0x0009C064 File Offset: 0x0009A264
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

		// Token: 0x06002136 RID: 8502 RVA: 0x0009C0F8 File Offset: 0x0009A2F8
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append(base.UnresolvedType);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0009C107 File Offset: 0x0009A307
		internal override TypeUsage GetTypeUsage()
		{
			return this._typeUsage;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0009C10F File Offset: 0x0009A30F
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
		}
	}
}
