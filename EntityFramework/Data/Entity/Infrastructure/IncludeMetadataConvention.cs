using System;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200075A RID: 1882
	[Obsolete("The IncludeMetadataConvention is no longer used. EdmMetadata is not included in the model. <see cref=\"EdmModelDiffer\" /> is now used to detect changes in the model.")]
	public class IncludeMetadataConvention : Convention
	{
		// Token: 0x0600552C RID: 21804 RVA: 0x00172A83 File Offset: 0x00170C83
		internal virtual void Apply(ModelConfiguration modelConfiguration)
		{
			Check.NotNull<ModelConfiguration>(modelConfiguration, "modelConfiguration");
			EdmMetadataContext.ConfigureEdmMetadata(modelConfiguration);
		}
	}
}
