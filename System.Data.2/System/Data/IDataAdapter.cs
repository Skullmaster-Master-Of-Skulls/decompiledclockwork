using System;

namespace System.Data
{
	// Token: 0x02000100 RID: 256
	public interface IDataAdapter
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001062 RID: 4194
		// (set) Token: 0x06001063 RID: 4195
		MissingMappingAction MissingMappingAction { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001064 RID: 4196
		// (set) Token: 0x06001065 RID: 4197
		MissingSchemaAction MissingSchemaAction { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06001066 RID: 4198
		ITableMappingCollection TableMappings { get; }

		// Token: 0x06001067 RID: 4199
		DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType);

		// Token: 0x06001068 RID: 4200
		int Fill(DataSet dataSet);

		// Token: 0x06001069 RID: 4201
		IDataParameter[] GetFillParameters();

		// Token: 0x0600106A RID: 4202
		int Update(DataSet dataSet);
	}
}
