using System;

namespace System.Data
{
	// Token: 0x02000101 RID: 257
	public interface IDataParameter
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600106B RID: 4203
		// (set) Token: 0x0600106C RID: 4204
		DbType DbType { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600106D RID: 4205
		// (set) Token: 0x0600106E RID: 4206
		ParameterDirection Direction { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600106F RID: 4207
		bool IsNullable { get; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001070 RID: 4208
		// (set) Token: 0x06001071 RID: 4209
		string ParameterName { get; set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001072 RID: 4210
		// (set) Token: 0x06001073 RID: 4211
		string SourceColumn { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001074 RID: 4212
		// (set) Token: 0x06001075 RID: 4213
		DataRowVersion SourceVersion { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001076 RID: 4214
		// (set) Token: 0x06001077 RID: 4215
		object Value { get; set; }
	}
}
