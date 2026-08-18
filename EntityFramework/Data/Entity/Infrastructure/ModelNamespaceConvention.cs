using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200075C RID: 1884
	public class ModelNamespaceConvention : Convention
	{
		// Token: 0x06005530 RID: 21808 RVA: 0x00172AC8 File Offset: 0x00170CC8
		internal ModelNamespaceConvention(string modelNamespace)
		{
			this._modelNamespace = modelNamespace;
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x00172AD7 File Offset: 0x00170CD7
		internal override void ApplyModelConfiguration(ModelConfiguration modelConfiguration)
		{
			base.ApplyModelConfiguration(modelConfiguration);
			modelConfiguration.ModelNamespace = this._modelNamespace;
		}

		// Token: 0x040022A4 RID: 8868
		private readonly string _modelNamespace;
	}
}
