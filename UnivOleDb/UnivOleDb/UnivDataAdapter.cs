using System;
using System.Collections;
using System.Data;

namespace UnivOleDb
{
	// Token: 0x0200000F RID: 15
	public interface UnivDataAdapter : IDisposable
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000AF RID: 175
		// (remove) Token: 0x060000B0 RID: 176
		event DatabaseAccessStartedEnded databaseAccessStarted;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060000B1 RID: 177
		// (remove) Token: 0x060000B2 RID: 178
		event DatabaseAccessStartedEnded databaseAccessEnded;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060000B3 RID: 179
		// (remove) Token: 0x060000B4 RID: 180
		event DatabaseErrorHandler databaseError;

		// Token: 0x060000B5 RID: 181
		bool DoesTableExist(string tableName);

		// Token: 0x060000B6 RID: 182
		bool DoesColumnExist(string tableName, string colName);

		// Token: 0x060000B7 RID: 183
		string GetSQLCommandParametersFilledIn(UnivDataAdapter da);

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B8 RID: 184
		// (set) Token: 0x060000B9 RID: 185
		ArrayList availableFeatures { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BA RID: 186
		// (set) Token: 0x060000BB RID: 187
		ArrayList unavailableFeatures { get; set; }

		// Token: 0x060000BC RID: 188
		UnivDataAdapter Clone();

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BD RID: 189
		UnivConnection Connection { get; }

		// Token: 0x060000BE RID: 190
		UnivCommand CreateCommand(string sql);

		// Token: 0x060000BF RID: 191
		int FillReturnIdentity(DataTable dataTable, string autoIncrementColName, string tableName);

		// Token: 0x060000C0 RID: 192
		int FillReturnIdentity(DataTable dataTable, string autoIncrementColName, string tableName, out string emsg);

		// Token: 0x060000C1 RID: 193
		int Fill(DataTable t, out string errorMessage);

		// Token: 0x060000C2 RID: 194
		int Fill(DataTable t);

		// Token: 0x060000C3 RID: 195
		int Fill(DataSet ds, string tableName);

		// Token: 0x060000C4 RID: 196
		int Fill(DataSet ds, string tableName, out string errorMessage);

		// Token: 0x060000C5 RID: 197
		int Fill(DataSet ds, string tableName, DataTable t, out string errorMessage);

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C6 RID: 198
		// (set) Token: 0x060000C7 RID: 199
		UnivCommand SelectCommand { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C8 RID: 200
		// (set) Token: 0x060000C9 RID: 201
		UnivCommand InsertCommand { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CA RID: 202
		// (set) Token: 0x060000CB RID: 203
		UnivCommand UpdateCommand { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CC RID: 204
		// (set) Token: 0x060000CD RID: 205
		UnivCommand DeleteCommand { get; set; }

		// Token: 0x060000CE RID: 206
		DataTable GetTableList(out string errmsg);
	}
}
