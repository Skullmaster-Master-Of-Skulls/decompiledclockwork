using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000357 RID: 855
	internal abstract class ModelFunctionTypeElement : FacetEnabledSchemaElement
	{
		// Token: 0x06001E98 RID: 7832 RVA: 0x00092849 File Offset: 0x00090A49
		internal ModelFunctionTypeElement(SchemaElement parentElement) : base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x06001E99 RID: 7833
		internal abstract void WriteIdentity(StringBuilder builder);

		// Token: 0x06001E9A RID: 7834
		internal abstract TypeUsage GetTypeUsage();

		// Token: 0x06001E9B RID: 7835
		internal abstract bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems);

		// Token: 0x04000A73 RID: 2675
		protected TypeUsage _typeUsage;
	}
}
