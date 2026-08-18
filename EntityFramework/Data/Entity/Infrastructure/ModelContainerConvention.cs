using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200075B RID: 1883
	public class ModelContainerConvention : IConceptualModelConvention<EntityContainer>, IConvention
	{
		// Token: 0x0600552E RID: 21806 RVA: 0x00172A9F File Offset: 0x00170C9F
		internal ModelContainerConvention(string containerName)
		{
			this._containerName = containerName;
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x00172AAE File Offset: 0x00170CAE
		public virtual void Apply(EntityContainer item, DbModel model)
		{
			Check.NotNull<DbModel>(model, "model");
			item.Name = this._containerName;
		}

		// Token: 0x040022A3 RID: 8867
		private readonly string _containerName;
	}
}
