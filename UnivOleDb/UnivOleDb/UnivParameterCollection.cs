using System;
using System.Data;

namespace UnivOleDb
{
	// Token: 0x02000014 RID: 20
	public interface UnivParameterCollection
	{
		// Token: 0x060000ED RID: 237
		void MakeAddParameterCommandTextChanges(string parameterName, object parameterValue);

		// Token: 0x060000EE RID: 238
		object AddNull(string parameterName);

		// Token: 0x060000EF RID: 239
		object Add(string parameterName, object parameterValue);

		// Token: 0x060000F0 RID: 240
		object Add(string parameterName, Type dbType, int size, object parameterValue);

		// Token: 0x060000F1 RID: 241
		object AddUsingSourceColumn(string parameterName, string sourceColumn);

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F2 RID: 242
		int Count { get; }

		// Token: 0x060000F3 RID: 243
		object Value(string parameterName);

		// Token: 0x1700002C RID: 44
		object this[string parameterName]
		{
			get;
		}

		// Token: 0x1700002D RID: 45
		object this[int index]
		{
			get;
		}

		// Token: 0x060000F6 RID: 246
		string ParameterName(int parameterIndex);

		// Token: 0x060000F7 RID: 247
		DbType ParameterDbType(int parameterIndex);

		// Token: 0x060000F8 RID: 248
		object Value(int parameterIndex);

		// Token: 0x060000F9 RID: 249
		object GetParameterNameValue(int parameterIndex, bool getName);

		// Token: 0x060000FA RID: 250
		bool Contains(string parameterName);

		// Token: 0x060000FB RID: 251
		void SetValue(string parameterName, object val);

		// Token: 0x060000FC RID: 252
		void Clear();

		// Token: 0x060000FD RID: 253
		void Clear(string parameterName);

		// Token: 0x060000FE RID: 254
		object AddNull(string parameterName, DbType dbType);

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FF RID: 255
		object ParameterCollection { get; }
	}
}
