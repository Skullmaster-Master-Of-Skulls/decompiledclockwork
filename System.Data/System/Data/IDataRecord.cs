using System;

namespace System.Data
{
	// Token: 0x0200004F RID: 79
	public interface IDataRecord
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002EC RID: 748
		int FieldCount { get; }

		// Token: 0x1700005B RID: 91
		object this[int i]
		{
			get;
		}

		// Token: 0x1700005C RID: 92
		object this[string name]
		{
			get;
		}

		// Token: 0x060002EF RID: 751
		string GetName(int i);

		// Token: 0x060002F0 RID: 752
		string GetDataTypeName(int i);

		// Token: 0x060002F1 RID: 753
		Type GetFieldType(int i);

		// Token: 0x060002F2 RID: 754
		object GetValue(int i);

		// Token: 0x060002F3 RID: 755
		int GetValues(object[] values);

		// Token: 0x060002F4 RID: 756
		int GetOrdinal(string name);

		// Token: 0x060002F5 RID: 757
		bool GetBoolean(int i);

		// Token: 0x060002F6 RID: 758
		byte GetByte(int i);

		// Token: 0x060002F7 RID: 759
		long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length);

		// Token: 0x060002F8 RID: 760
		char GetChar(int i);

		// Token: 0x060002F9 RID: 761
		long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length);

		// Token: 0x060002FA RID: 762
		Guid GetGuid(int i);

		// Token: 0x060002FB RID: 763
		short GetInt16(int i);

		// Token: 0x060002FC RID: 764
		int GetInt32(int i);

		// Token: 0x060002FD RID: 765
		long GetInt64(int i);

		// Token: 0x060002FE RID: 766
		float GetFloat(int i);

		// Token: 0x060002FF RID: 767
		double GetDouble(int i);

		// Token: 0x06000300 RID: 768
		string GetString(int i);

		// Token: 0x06000301 RID: 769
		decimal GetDecimal(int i);

		// Token: 0x06000302 RID: 770
		DateTime GetDateTime(int i);

		// Token: 0x06000303 RID: 771
		IDataReader GetData(int i);

		// Token: 0x06000304 RID: 772
		bool IsDBNull(int i);
	}
}
