using System;

namespace UnivOleDb
{
	// Token: 0x0200000E RID: 14
	public interface UnivConnection : IDisposable
	{
		// Token: 0x060000A0 RID: 160
		UnivDataAdapter CreateDataAdapter();

		// Token: 0x060000A1 RID: 161
		string GetDatabaseName();

		// Token: 0x060000A2 RID: 162
		string GetDatabaseDescription();

		// Token: 0x060000A3 RID: 163
		string GetConcatString(string[] strings);

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A4 RID: 164
		string OriginalConnectionString { get; }

		// Token: 0x060000A5 RID: 165
		string GetTempTablePrefix();

		// Token: 0x060000A6 RID: 166
		void Open();

		// Token: 0x060000A7 RID: 167
		void Close();

		// Token: 0x060000A8 RID: 168
		UnivTransaction BeginTransaction();

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A9 RID: 169
		string ConnectionString { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AA RID: 170
		bool IsOpen { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AB RID: 171
		UnivTransaction Transaction { get; }

		// Token: 0x060000AC RID: 172
		object GetConnection();

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AD RID: 173
		// (set) Token: 0x060000AE RID: 174
		bool RunThroughClockWorkServer { get; set; }
	}
}
