using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000321 RID: 801
	internal abstract class ModelFunctionTypeElement : FacetEnabledSchemaElement
	{
		// Token: 0x06002F4A RID: 12106 RVA: 0x000B2F43 File Offset: 0x000B1143
		internal ModelFunctionTypeElement(SchemaElement parentElement) : base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x06002F4B RID: 12107
		internal abstract void WriteIdentity(StringBuilder builder);

		// Token: 0x06002F4C RID: 12108
		internal abstract TypeUsage GetTypeUsage();

		// Token: 0x06002F4D RID: 12109
		internal abstract bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems);

		// Token: 0x0400145C RID: 5212
		protected TypeUsage _typeUsage;
	}
}
