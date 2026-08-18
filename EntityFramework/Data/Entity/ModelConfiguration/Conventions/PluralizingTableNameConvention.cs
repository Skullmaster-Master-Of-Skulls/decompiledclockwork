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
	// Token: 0x020007F5 RID: 2037
	public class PluralizingTableNameConvention : IStoreModelConvention<EntityType>, IConvention
	{
		// Token: 0x06005C49 RID: 23625 RVA: 0x0018E0FC File Offset: 0x0018C2FC
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			this._pluralizationService = DbConfiguration.DependencyResolver.GetService<IPluralizationService>();
			if (item.GetTableName() == null)
			{
				EntitySet entitySet = model.StoreModel.GetEntitySet(item);
				entitySet.Table = (from n in (from es in model.StoreModel.GetEntitySets()
				where es.Schema == entitySet.Schema
				select es).Except(new EntitySet[]
				{
					entitySet
				})
				select n.Table).Uniquify(this._pluralizationService.Pluralize(entitySet.Table));
			}
		}

		// Token: 0x0400249D RID: 9373
		private IPluralizationService _pluralizationService = DbConfiguration.DependencyResolver.GetService<IPluralizationService>();
	}
}
