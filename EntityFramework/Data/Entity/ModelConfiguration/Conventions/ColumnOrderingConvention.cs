using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000727 RID: 1831
	public class ColumnOrderingConvention : IStoreModelConvention<EntityType>, IConvention
	{
		// Token: 0x06004B55 RID: 19285 RVA: 0x001616B0 File Offset: 0x0015F8B0
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			this.ValidateColumns(item, model.StoreModel.GetEntitySet(item).Table);
			ColumnOrderingConvention.OrderColumns(item.Properties).Each(delegate(EdmProperty c)
			{
				bool isPrimaryKeyColumn = c.IsPrimaryKeyColumn;
				item.RemoveMember(c);
				item.AddMember(c);
				if (isPrimaryKeyColumn)
				{
					item.AddKeyMember(c);
				}
			});
			item.ForeignKeyBuilders.Each((ForeignKeyBuilder fk) => fk.DependentColumns = ColumnOrderingConvention.OrderColumns(fk.DependentColumns));
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x00161757 File Offset: 0x0015F957
		protected virtual void ValidateColumns(EntityType table, string tableName)
		{
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x001618B8 File Offset: 0x0015FAB8
		private static IEnumerable<EdmProperty> OrderColumns(IEnumerable<EdmProperty> columns)
		{
			var source = from c in columns
			select new
			{
				Column = c,
				Order = (c.GetOrder() ?? int.MaxValue)
			};
			return (from c in source
			orderby c.Order
			select c.Column).ToList<EdmProperty>();
		}
	}
}
