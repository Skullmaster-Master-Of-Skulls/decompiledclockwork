using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001D1 RID: 465
	public class ForeignKeyIndexConvention : IStoreModelConvention<AssociationType>, IConvention
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x00041520 File Offset: 0x0003F720
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			if (item.Constraint == null)
			{
				return;
			}
			IEnumerable<ConsolidatedIndex> source = ConsolidatedIndex.BuildIndexes(item.Name, from p in item.Constraint.ToProperties
			select Tuple.Create<string, EdmProperty>(p.Name, p));
			IEnumerable<string> dependentColumnNames = from p in item.Constraint.ToProperties
			select p.Name;
			if (!source.Any((ConsolidatedIndex c) => c.Columns.SequenceEqual(dependentColumnNames)))
			{
				string name = IndexOperation.BuildDefaultName(dependentColumnNames);
				int num = 0;
				foreach (EdmProperty edmProperty in item.Constraint.ToProperties)
				{
					IndexAnnotation indexAnnotation = new IndexAnnotation(new IndexAttribute(name, num++));
					object annotation = edmProperty.Annotations.GetAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index");
					if (annotation != null)
					{
						indexAnnotation = (IndexAnnotation)((IndexAnnotation)annotation).MergeWith(indexAnnotation);
					}
					edmProperty.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index", indexAnnotation);
				}
			}
		}
	}
}
