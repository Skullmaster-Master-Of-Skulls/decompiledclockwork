using System;

namespace UnivOleDb
{
	// Token: 0x0200000D RID: 13
	public interface UnivCommand : IDisposable
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600008C RID: 140
		// (remove) Token: 0x0600008D RID: 141
		event DatabaseAccessStartedEnded databaseAccessStarted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600008E RID: 142
		// (remove) Token: 0x0600008F RID: 143
		event DatabaseAccessStartedEnded databaseAccessEnded;

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000090 RID: 144
		// (set) Token: 0x06000091 RID: 145
		string CommandText { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000092 RID: 146
		// (set) Token: 0x06000093 RID: 147
		UnivTransaction Transaction { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000094 RID: 148
		// (set) Token: 0x06000095 RID: 149
		int CommandTimeout { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000096 RID: 150
		UnivParameterCollection Parameters { get; }

		// Token: 0x06000097 RID: 151
		UnivDataReader ExecuteReader2();

		// Token: 0x06000098 RID: 152
		int ExecuteNonQuery();

		// Token: 0x06000099 RID: 153
		int ExecuteNonQuery(out string emsg);

		// Token: 0x0600009A RID: 154
		int ExecuteNonQuery2();

		// Token: 0x0600009B RID: 155
		int ExecuteNonQuery2(out string emsg);

		// Token: 0x0600009C RID: 156
		object ExecuteScalar();

		// Token: 0x0600009D RID: 157
		void OnDatabaseAccessStarted();

		// Token: 0x0600009E RID: 158
		void OnDatabaseAccessEnded();

		// Token: 0x0600009F RID: 159
		string ToStringParametersExpanded();
	}
}
