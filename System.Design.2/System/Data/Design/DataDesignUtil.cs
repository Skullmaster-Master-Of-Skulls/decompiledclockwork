using System;
using System.Collections;
using System.Data.Common;

namespace System.Data.Design
{
	// Token: 0x0200021F RID: 543
	internal sealed class DataDesignUtil
	{
		// Token: 0x0600142A RID: 5162 RVA: 0x0000362F File Offset: 0x0000182F
		private DataDesignUtil()
		{
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0007267C File Offset: 0x0007087C
		internal static string[] MapColumnNames(DataColumnMappingCollection mappingCollection, string[] names, DataDesignUtil.MappingDirection direction)
		{
			if (mappingCollection == null || names == null)
			{
				return new string[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (string text in names)
			{
				string value;
				try
				{
					if (direction == DataDesignUtil.MappingDirection.DataSetToSource)
					{
						DataColumnMapping dataColumnMapping = mappingCollection.GetByDataSetColumn(text);
						value = dataColumnMapping.SourceColumn;
					}
					else
					{
						DataColumnMapping dataColumnMapping = mappingCollection[text];
						value = dataColumnMapping.DataSetColumn;
					}
				}
				catch (IndexOutOfRangeException)
				{
					value = text;
				}
				arrayList.Add(value);
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00072714 File Offset: 0x00070914
		public static void CopyColumn(DataColumn srcColumn, DataColumn destColumn)
		{
			destColumn.AllowDBNull = srcColumn.AllowDBNull;
			destColumn.AutoIncrement = srcColumn.AutoIncrement;
			destColumn.AutoIncrementSeed = srcColumn.AutoIncrementSeed;
			destColumn.AutoIncrementStep = srcColumn.AutoIncrementStep;
			destColumn.Caption = srcColumn.Caption;
			destColumn.ColumnMapping = srcColumn.ColumnMapping;
			destColumn.ColumnName = srcColumn.ColumnName;
			destColumn.DataType = srcColumn.DataType;
			destColumn.DefaultValue = srcColumn.DefaultValue;
			destColumn.Expression = srcColumn.Expression;
			destColumn.MaxLength = srcColumn.MaxLength;
			destColumn.Prefix = srcColumn.Prefix;
			destColumn.ReadOnly = srcColumn.ReadOnly;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x000727C0 File Offset: 0x000709C0
		public static DataColumn CloneColumn(DataColumn column)
		{
			DataColumn dataColumn = new DataColumn();
			DataDesignUtil.CopyColumn(column, dataColumn);
			return dataColumn;
		}

		// Token: 0x04000ACD RID: 2765
		internal static string DataSetClassName = typeof(DataSet).ToString();

		// Token: 0x020004BA RID: 1210
		internal enum MappingDirection
		{
			// Token: 0x04001E95 RID: 7829
			SourceToDataSet,
			// Token: 0x04001E96 RID: 7830
			DataSetToSource
		}
	}
}
