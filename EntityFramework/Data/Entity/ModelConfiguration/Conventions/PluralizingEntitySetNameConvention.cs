using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000800 RID: 2048
	public class PluralizingEntitySetNameConvention : IConceptualModelConvention<EntitySet>, IConvention
	{
		// Token: 0x06005C70 RID: 23664 RVA: 0x0018F270 File Offset: 0x0018D470
		public virtual void Apply(EntitySet item, DbModel model)
		{
			Check.NotNull<EntitySet>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.GetConfiguration() == null)
			{
				item.Name = model.ConceptualModel.GetEntitySets().Except(new EntitySet[]
				{
					item
				}).UniquifyName(PluralizingEntitySetNameConvention._pluralizationService.Pluralize(item.Name));
			}
		}

		// Token: 0x040024AC RID: 9388
		private static readonly IPluralizationService _pluralizationService = DbConfiguration.DependencyResolver.GetService<IPluralizationService>();
	}
}
