using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007B3 RID: 1971
	internal static class TableOperations
	{
		// Token: 0x06005947 RID: 22855 RVA: 0x00180644 File Offset: 0x0017E844
		public static EdmProperty CopyColumnAndAnyConstraints(EdmModel database, EntityType fromTable, EntityType toTable, EdmProperty column, Func<EdmProperty, bool> isCompatible, bool useExisting)
		{
			EdmProperty edmProperty = column;
			if (fromTable != toTable)
			{
				edmProperty = TablePrimitiveOperations.IncludeColumn(toTable, column, isCompatible, useExisting);
				if (!edmProperty.IsPrimaryKeyColumn)
				{
					ForeignKeyPrimitiveOperations.CopyAllForeignKeyConstraintsForColumn(database, fromTable, toTable, column, edmProperty);
				}
			}
			return edmProperty;
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x00180678 File Offset: 0x0017E878
		public static EdmProperty MoveColumnAndAnyConstraints(EntityType fromTable, EntityType toTable, EdmProperty column, bool useExisting)
		{
			EdmProperty result = column;
			if (fromTable != toTable)
			{
				result = TablePrimitiveOperations.IncludeColumn(toTable, column, TablePrimitiveOperations.GetNameMatcher(column.Name), useExisting);
				TablePrimitiveOperations.RemoveColumn(fromTable, column);
				ForeignKeyPrimitiveOperations.MoveAllForeignKeyConstraintsForColumn(fromTable, toTable, column);
			}
			return result;
		}
	}
}
