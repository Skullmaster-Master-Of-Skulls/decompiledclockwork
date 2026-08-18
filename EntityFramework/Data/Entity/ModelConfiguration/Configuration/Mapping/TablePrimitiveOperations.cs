using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007B1 RID: 1969
	internal static class TablePrimitiveOperations
	{
		// Token: 0x06005931 RID: 22833 RVA: 0x0017F804 File Offset: 0x0017DA04
		public static void AddColumn(EntityType table, EdmProperty column)
		{
			if (!table.Properties.Contains(column))
			{
				PrimitivePropertyConfiguration primitivePropertyConfiguration = column.GetConfiguration() as PrimitivePropertyConfiguration;
				if (primitivePropertyConfiguration == null || string.IsNullOrWhiteSpace(primitivePropertyConfiguration.ColumnName))
				{
					string name = column.GetPreferredName() ?? column.Name;
					column.SetUnpreferredUniqueName(column.Name);
					column.Name = table.Properties.UniquifyName(name);
				}
				table.AddMember(column);
			}
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x0017F871 File Offset: 0x0017DA71
		public static EdmProperty RemoveColumn(EntityType table, EdmProperty column)
		{
			if (!column.IsPrimaryKeyColumn)
			{
				table.RemoveMember(column);
			}
			return column;
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x0017F884 File Offset: 0x0017DA84
		public static EdmProperty IncludeColumn(EntityType table, EdmProperty templateColumn, Func<EdmProperty, bool> isCompatible, bool useExisting)
		{
			EdmProperty edmProperty = table.Properties.FirstOrDefault(isCompatible);
			if (edmProperty == null)
			{
				templateColumn = templateColumn.Clone();
			}
			else if (!useExisting && !edmProperty.IsPrimaryKeyColumn)
			{
				templateColumn = templateColumn.Clone();
			}
			else
			{
				templateColumn = edmProperty;
			}
			TablePrimitiveOperations.AddColumn(table, templateColumn);
			return templateColumn;
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x0017F8E8 File Offset: 0x0017DAE8
		public static Func<EdmProperty, bool> GetNameMatcher(string name)
		{
			return (EdmProperty c) => string.Equals(c.Name, name, StringComparison.Ordinal);
		}
	}
}
