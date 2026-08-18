using System;

namespace System.Data
{
	// Token: 0x020000BA RID: 186
	public interface IDataParameter
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000C60 RID: 3168
		// (set) Token: 0x06000C61 RID: 3169
		DbType DbType { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000C62 RID: 3170
		// (set) Token: 0x06000C63 RID: 3171
		ParameterDirection Direction { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000C64 RID: 3172
		bool IsNullable { get; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000C65 RID: 3173
		// (set) Token: 0x06000C66 RID: 3174
		string ParameterName { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000C67 RID: 3175
		// (set) Token: 0x06000C68 RID: 3176
		string SourceColumn { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000C69 RID: 3177
		// (set) Token: 0x06000C6A RID: 3178
		DataRowVersion SourceVersion { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000C6B RID: 3179
		// (set) Token: 0x06000C6C RID: 3180
		object Value { get; set; }
	}
}
