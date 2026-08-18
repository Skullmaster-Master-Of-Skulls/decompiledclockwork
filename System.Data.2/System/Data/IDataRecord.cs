using System;

namespace System.Data
{
	// Token: 0x02000104 RID: 260
	public interface IDataRecord
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001084 RID: 4228
		int FieldCount { get; }

		// Token: 0x17000272 RID: 626
		object this[int i]
		{
			get;
		}

		// Token: 0x17000273 RID: 627
		object this[string name]
		{
			get;
		}

		// Token: 0x06001087 RID: 4231
		string GetName(int i);

		// Token: 0x06001088 RID: 4232
		string GetDataTypeName(int i);

		// Token: 0x06001089 RID: 4233
		Type GetFieldType(int i);

		// Token: 0x0600108A RID: 4234
		object GetValue(int i);

		// Token: 0x0600108B RID: 4235
		int GetValues(object[] values);

		// Token: 0x0600108C RID: 4236
		int GetOrdinal(string name);

		// Token: 0x0600108D RID: 4237
		bool GetBoolean(int i);

		// Token: 0x0600108E RID: 4238
		byte GetByte(int i);

		// Token: 0x0600108F RID: 4239
		long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length);

		// Token: 0x06001090 RID: 4240
		char GetChar(int i);

		// Token: 0x06001091 RID: 4241
		long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length);

		// Token: 0x06001092 RID: 4242
		Guid GetGuid(int i);

		// Token: 0x06001093 RID: 4243
		short GetInt16(int i);

		// Token: 0x06001094 RID: 4244
		int GetInt32(int i);

		// Token: 0x06001095 RID: 4245
		long GetInt64(int i);

		// Token: 0x06001096 RID: 4246
		float GetFloat(int i);

		// Token: 0x06001097 RID: 4247
		double GetDouble(int i);

		// Token: 0x06001098 RID: 4248
		string GetString(int i);

		// Token: 0x06001099 RID: 4249
		decimal GetDecimal(int i);

		// Token: 0x0600109A RID: 4250
		DateTime GetDateTime(int i);

		// Token: 0x0600109B RID: 4251
		IDataReader GetData(int i);

		// Token: 0x0600109C RID: 4252
		bool IsDBNull(int i);
	}
}
