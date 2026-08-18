using System;

namespace System.Data
{
	// Token: 0x020000B9 RID: 185
	public interface IDataAdapter
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000C57 RID: 3159
		// (set) Token: 0x06000C58 RID: 3160
		MissingMappingAction MissingMappingAction { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000C59 RID: 3161
		// (set) Token: 0x06000C5A RID: 3162
		MissingSchemaAction MissingSchemaAction { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000C5B RID: 3163
		ITableMappingCollection TableMappings { get; }

		// Token: 0x06000C5C RID: 3164
		DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType);

		// Token: 0x06000C5D RID: 3165
		int Fill(DataSet dataSet);

		// Token: 0x06000C5E RID: 3166
		IDataParameter[] GetFillParameters();

		// Token: 0x06000C5F RID: 3167
		int Update(DataSet dataSet);
	}
}
