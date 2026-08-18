using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004F RID: 79
	public class SqlDataRecord : IDataRecord
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0003CCC0 File Offset: 0x0003C0C0
		public virtual int FieldCount
		{
			get
			{
				this.EnsureSubclassOverride();
				return this._columnMetaData.Length;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0003CCDC File Offset: 0x0003C0DC
		public virtual string GetName(int ordinal)
		{
			this.EnsureSubclassOverride();
			return this.GetSqlMetaData(ordinal).Name;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0003CCFC File Offset: 0x0003C0FC
		public virtual string GetDataTypeName(int ordinal)
		{
			this.EnsureSubclassOverride();
			SqlMetaData sqlMetaData = this.GetSqlMetaData(ordinal);
			if (SqlDbType.Udt == sqlMetaData.SqlDbType)
			{
				return sqlMetaData.UdtTypeName;
			}
			return MetaType.GetMetaTypeFromSqlDbType(sqlMetaData.SqlDbType, false).TypeName;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0003CD3C File Offset: 0x0003C13C
		public virtual Type GetFieldType(int ordinal)
		{
			this.EnsureSubclassOverride();
			if (SqlDbType.Udt == this.GetSqlMetaData(ordinal).SqlDbType)
			{
				return this.GetSqlMetaData(ordinal).Type;
			}
			SqlMetaData sqlMetaData = this.GetSqlMetaData(ordinal);
			return MetaType.GetMetaTypeFromSqlDbType(sqlMetaData.SqlDbType, false).ClassType;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0003CD88 File Offset: 0x0003C188
		public virtual object GetValue(int ordinal)
		{
			this.EnsureSubclassOverride();
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			if (this.SmiVersion >= 210UL)
			{
				return ValueUtilsSmi.GetValue200(this._eventSink, this._recordBuffer, ordinal, smiMetaData, this._recordContext);
			}
			return ValueUtilsSmi.GetValue(this._eventSink, this._recordBuffer, ordinal, smiMetaData, this._recordContext);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0003CDE4 File Offset: 0x0003C1E4
		public virtual int GetValues(object[] values)
		{
			this.EnsureSubclassOverride();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = (values.Length < this.FieldCount) ? values.Length : this.FieldCount;
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0003CE34 File Offset: 0x0003C234
		public virtual int GetOrdinal(string name)
		{
			this.EnsureSubclassOverride();
			if (this._fieldNameLookup == null)
			{
				string[] array = new string[this.FieldCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetSqlMetaData(i).Name;
				}
				this._fieldNameLookup = new FieldNameLookup(array, -1);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x1700005B RID: 91
		public virtual object this[int ordinal]
		{
			get
			{
				this.EnsureSubclassOverride();
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x1700005C RID: 92
		public virtual object this[string name]
		{
			get
			{
				this.EnsureSubclassOverride();
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0003CED0 File Offset: 0x0003C2D0
		public virtual bool GetBoolean(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0003CEFC File Offset: 0x0003C2FC
		public virtual byte GetByte(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0003CF28 File Offset: 0x0003C328
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length, true);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0003CF5C File Offset: 0x0003C35C
		public virtual char GetChar(int ordinal)
		{
			this.EnsureSubclassOverride();
			throw ADP.NotSupported();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0003CF74 File Offset: 0x0003C374
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0003CFA8 File Offset: 0x0003C3A8
		public virtual Guid GetGuid(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0003CFD4 File Offset: 0x0003C3D4
		public virtual short GetInt16(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0003D000 File Offset: 0x0003C400
		public virtual int GetInt32(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0003D02C File Offset: 0x0003C42C
		public virtual long GetInt64(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0003D058 File Offset: 0x0003C458
		public virtual float GetFloat(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0003D084 File Offset: 0x0003C484
		public virtual double GetDouble(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0003D0B0 File Offset: 0x0003C4B0
		public virtual string GetString(int ordinal)
		{
			this.EnsureSubclassOverride();
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			if (this._usesStringStorageForXml && SqlDbType.Xml == smiMetaData.SqlDbType)
			{
				return ValueUtilsSmi.GetString(this._eventSink, this._recordBuffer, ordinal, SqlDataRecord.__maxNVarCharForXml);
			}
			return ValueUtilsSmi.GetString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0003D110 File Offset: 0x0003C510
		public virtual decimal GetDecimal(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0003D13C File Offset: 0x0003C53C
		public virtual DateTime GetDateTime(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0003D168 File Offset: 0x0003C568
		public virtual DateTimeOffset GetDateTimeOffset(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDateTimeOffset(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0003D194 File Offset: 0x0003C594
		public virtual TimeSpan GetTimeSpan(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetTimeSpan(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0003D1C0 File Offset: 0x0003C5C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		IDataReader IDataRecord.GetData(int ordinal)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0003D1D4 File Offset: 0x0003C5D4
		public virtual bool IsDBNull(int ordinal)
		{
			this.EnsureSubclassOverride();
			this.ThrowIfInvalidOrdinal(ordinal);
			return ValueUtilsSmi.IsDBNull(this._eventSink, this._recordBuffer, ordinal);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0003D200 File Offset: 0x0003C600
		public virtual SqlMetaData GetSqlMetaData(int ordinal)
		{
			this.EnsureSubclassOverride();
			return this._columnMetaData[ordinal];
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0003D21C File Offset: 0x0003C61C
		public virtual Type GetSqlFieldType(int ordinal)
		{
			this.EnsureSubclassOverride();
			SqlMetaData sqlMetaData = this.GetSqlMetaData(ordinal);
			return MetaType.GetMetaTypeFromSqlDbType(sqlMetaData.SqlDbType, false).SqlType;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0003D248 File Offset: 0x0003C648
		public virtual object GetSqlValue(int ordinal)
		{
			this.EnsureSubclassOverride();
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			if (this.SmiVersion >= 210UL)
			{
				return ValueUtilsSmi.GetSqlValue200(this._eventSink, this._recordBuffer, ordinal, smiMetaData, this._recordContext);
			}
			return ValueUtilsSmi.GetSqlValue(this._eventSink, this._recordBuffer, ordinal, smiMetaData, this._recordContext);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0003D2A4 File Offset: 0x0003C6A4
		public virtual int GetSqlValues(object[] values)
		{
			this.EnsureSubclassOverride();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = (values.Length < this.FieldCount) ? values.Length : this.FieldCount;
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetSqlValue(i);
			}
			return num;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0003D2F4 File Offset: 0x0003C6F4
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlBinary(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0003D320 File Offset: 0x0003C720
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), this._recordContext);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0003D354 File Offset: 0x0003C754
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlXml(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), this._recordContext);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0003D388 File Offset: 0x0003C788
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0003D3B4 File Offset: 0x0003C7B4
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0003D3E0 File Offset: 0x0003C7E0
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), this._recordContext);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0003D414 File Offset: 0x0003C814
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0003D440 File Offset: 0x0003C840
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0003D46C File Offset: 0x0003C86C
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0003D498 File Offset: 0x0003C898
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0003D4C4 File Offset: 0x0003C8C4
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0003D4F0 File Offset: 0x0003C8F0
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlMoney(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0003D51C File Offset: 0x0003C91C
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0003D548 File Offset: 0x0003C948
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0003D574 File Offset: 0x0003C974
		public virtual SqlString GetSqlString(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0003D5A0 File Offset: 0x0003C9A0
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0003D5CC File Offset: 0x0003C9CC
		public virtual int SetValues(params object[] values)
		{
			this.EnsureSubclassOverride();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = (values.Length > this.FieldCount) ? this.FieldCount : values.Length;
			ExtendedClrTypeCode[] array = new ExtendedClrTypeCode[num];
			for (int i = 0; i < num; i++)
			{
				SqlMetaData sqlMetaData = this.GetSqlMetaData(i);
				array[i] = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(sqlMetaData.SqlDbType, false, values[i], sqlMetaData.Type, this.SmiVersion);
				if (ExtendedClrTypeCode.Invalid == array[i])
				{
					throw ADP.InvalidCast();
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (this.SmiVersion >= 210UL)
				{
					ValueUtilsSmi.SetCompatibleValueV200(this._eventSink, this._recordBuffer, j, this.GetSmiMetaData(j), values[j], array[j], 0, 0, null);
				}
				else
				{
					ValueUtilsSmi.SetCompatibleValue(this._eventSink, this._recordBuffer, j, this.GetSmiMetaData(j), values[j], array[j], 0);
				}
			}
			return num;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0003D6AC File Offset: 0x0003CAAC
		public virtual void SetValue(int ordinal, object value)
		{
			this.EnsureSubclassOverride();
			SqlMetaData sqlMetaData = this.GetSqlMetaData(ordinal);
			ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(sqlMetaData.SqlDbType, false, value, sqlMetaData.Type, this.SmiVersion);
			if (ExtendedClrTypeCode.Invalid == extendedClrTypeCode)
			{
				throw ADP.InvalidCast();
			}
			if (this.SmiVersion >= 210UL)
			{
				ValueUtilsSmi.SetCompatibleValueV200(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value, extendedClrTypeCode, 0, 0, null);
				return;
			}
			ValueUtilsSmi.SetCompatibleValue(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value, extendedClrTypeCode, 0);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0003D734 File Offset: 0x0003CB34
		public virtual void SetBoolean(int ordinal, bool value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0003D764 File Offset: 0x0003CB64
		public virtual void SetByte(int ordinal, byte value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0003D794 File Offset: 0x0003CB94
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0003D7C8 File Offset: 0x0003CBC8
		public virtual void SetChar(int ordinal, char value)
		{
			this.EnsureSubclassOverride();
			throw ADP.NotSupported();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0003D7E0 File Offset: 0x0003CBE0
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0003D814 File Offset: 0x0003CC14
		public virtual void SetInt16(int ordinal, short value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0003D844 File Offset: 0x0003CC44
		public virtual void SetInt32(int ordinal, int value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0003D874 File Offset: 0x0003CC74
		public virtual void SetInt64(int ordinal, long value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0003D8A4 File Offset: 0x0003CCA4
		public virtual void SetFloat(int ordinal, float value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0003D8D4 File Offset: 0x0003CCD4
		public virtual void SetDouble(int ordinal, double value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0003D904 File Offset: 0x0003CD04
		public virtual void SetString(int ordinal, string value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0003D934 File Offset: 0x0003CD34
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0003D964 File Offset: 0x0003CD64
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0003D994 File Offset: 0x0003CD94
		public virtual void SetTimeSpan(int ordinal, TimeSpan value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetTimeSpan(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value, this.SmiVersion >= 210UL);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0003D9D4 File Offset: 0x0003CDD4
		public virtual void SetDateTimeOffset(int ordinal, DateTimeOffset value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDateTimeOffset(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value, this.SmiVersion >= 210UL);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0003DA14 File Offset: 0x0003CE14
		public virtual void SetDBNull(int ordinal)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDBNull(this._eventSink, this._recordBuffer, ordinal, true);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0003DA3C File Offset: 0x0003CE3C
		public virtual void SetGuid(int ordinal, Guid value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0003DA6C File Offset: 0x0003CE6C
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0003DA9C File Offset: 0x0003CE9C
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0003DACC File Offset: 0x0003CECC
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0003DAFC File Offset: 0x0003CEFC
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0003DB2C File Offset: 0x0003CF2C
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0003DB5C File Offset: 0x0003CF5C
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0003DB8C File Offset: 0x0003CF8C
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0003DBBC File Offset: 0x0003CFBC
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlMoney(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0003DBEC File Offset: 0x0003CFEC
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0003DC1C File Offset: 0x0003D01C
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlXml(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0003DC4C File Offset: 0x0003D04C
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0003DC7C File Offset: 0x0003D07C
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0003DCAC File Offset: 0x0003D0AC
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlBinary(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0003DCDC File Offset: 0x0003D0DC
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0003DD0C File Offset: 0x0003D10C
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0003DD3C File Offset: 0x0003D13C
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0003DD6C File Offset: 0x0003D16C
		public SqlDataRecord(params SqlMetaData[] metaData)
		{
			if (metaData == null)
			{
				throw ADP.ArgumentNull("metadata");
			}
			this._columnMetaData = new SqlMetaData[metaData.Length];
			this._columnSmiMetaData = new SmiExtendedMetaData[metaData.Length];
			ulong smiVersion = this.SmiVersion;
			for (int i = 0; i < this._columnSmiMetaData.Length; i++)
			{
				if (metaData[i] == null)
				{
					throw ADP.ArgumentNull("metadata[" + i.ToString() + "]");
				}
				this._columnMetaData[i] = metaData[i];
				this._columnSmiMetaData[i] = MetaDataUtilsSmi.SqlMetaDataToSmiExtendedMetaData(this._columnMetaData[i]);
				if (!MetaDataUtilsSmi.IsValidForSmiVersion(this._columnSmiMetaData[i], smiVersion))
				{
					throw ADP.VersionDoesNotSupportDataType(this._columnSmiMetaData[i].TypeName);
				}
			}
			this._eventSink = new SmiEventSink_Default();
			if (InOutOfProcHelper.InProc)
			{
				this._recordContext = SmiContextFactory.Instance.GetCurrentContext();
				this._recordBuffer = this._recordContext.CreateRecordBuffer(this._columnSmiMetaData, this._eventSink);
				this._usesStringStorageForXml = false;
			}
			else
			{
				this._recordContext = null;
				SmiMetaData[] columnSmiMetaData = this._columnSmiMetaData;
				this._recordBuffer = new MemoryRecordBuffer(columnSmiMetaData);
				this._usesStringStorageForXml = true;
			}
			this._eventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0003DE9C File Offset: 0x0003D29C
		internal SqlDataRecord(SmiRecordBuffer recordBuffer, params SmiExtendedMetaData[] metaData)
		{
			this._columnMetaData = new SqlMetaData[metaData.Length];
			this._columnSmiMetaData = new SmiExtendedMetaData[metaData.Length];
			for (int i = 0; i < this._columnSmiMetaData.Length; i++)
			{
				this._columnSmiMetaData[i] = metaData[i];
				this._columnMetaData[i] = MetaDataUtilsSmi.SmiExtendedMetaDataToSqlMetaData(this._columnSmiMetaData[i]);
			}
			this._eventSink = new SmiEventSink_Default();
			if (InOutOfProcHelper.InProc)
			{
				this._recordContext = SmiContextFactory.Instance.GetCurrentContext();
			}
			else
			{
				this._recordContext = null;
			}
			this._recordBuffer = recordBuffer;
			this._eventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0003DF3C File Offset: 0x0003D33C
		internal SmiRecordBuffer RecordBuffer
		{
			get
			{
				return this._recordBuffer;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0003DF50 File Offset: 0x0003D350
		internal SmiContext RecordContext
		{
			get
			{
				return this._recordContext;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0003DF64 File Offset: 0x0003D364
		private ulong SmiVersion
		{
			get
			{
				if (!InOutOfProcHelper.InProc)
				{
					return 210UL;
				}
				return SmiContextFactory.Instance.NegotiatedSmiVersion;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0003DF8C File Offset: 0x0003D38C
		internal SqlMetaData[] InternalGetMetaData()
		{
			return this._columnMetaData;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0003DFA0 File Offset: 0x0003D3A0
		internal SmiExtendedMetaData[] InternalGetSmiMetaData()
		{
			return this._columnSmiMetaData;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0003DFB4 File Offset: 0x0003D3B4
		internal SmiExtendedMetaData GetSmiMetaData(int ordinal)
		{
			return this._columnSmiMetaData[ordinal];
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0003DFCC File Offset: 0x0003D3CC
		internal void ThrowIfInvalidOrdinal(int ordinal)
		{
			if (0 > ordinal || this.FieldCount <= ordinal)
			{
				throw ADP.IndexOutOfRange(ordinal);
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0003DFF0 File Offset: 0x0003D3F0
		private void EnsureSubclassOverride()
		{
			if (this._recordBuffer == null)
			{
				throw SQL.SubclassMustOverride();
			}
		}

		// Token: 0x0400017F RID: 383
		private SmiRecordBuffer _recordBuffer;

		// Token: 0x04000180 RID: 384
		private SmiContext _recordContext;

		// Token: 0x04000181 RID: 385
		private SmiExtendedMetaData[] _columnSmiMetaData;

		// Token: 0x04000182 RID: 386
		private SmiEventSink_Default _eventSink;

		// Token: 0x04000183 RID: 387
		private SqlMetaData[] _columnMetaData;

		// Token: 0x04000184 RID: 388
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04000185 RID: 389
		private bool _usesStringStorageForXml;

		// Token: 0x04000186 RID: 390
		private static readonly SmiMetaData __maxNVarCharForXml = new SmiMetaData(SqlDbType.NVarChar, -1L, SmiMetaData.DefaultNVarChar_NoCollation.Precision, SmiMetaData.DefaultNVarChar_NoCollation.Scale, SmiMetaData.DefaultNVarChar.LocaleId, SmiMetaData.DefaultNVarChar.CompareOptions, null);
	}
}
