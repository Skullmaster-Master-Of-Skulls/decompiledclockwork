using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200052C RID: 1324
	public abstract class DbUpdatableDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x06003241 RID: 12865 RVA: 0x000EFDA1 File Offset: 0x000EDFA1
		internal DbUpdatableDataRecord(ObjectStateEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject)
		{
			this._cacheEntry = cacheEntry;
			this._userObject = userObject;
			this._metadata = metadata;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000EFDBE File Offset: 0x000EDFBE
		internal DbUpdatableDataRecord(ObjectStateEntry cacheEntry) : this(cacheEntry, null, null)
		{
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000EFDC9 File Offset: 0x000EDFC9
		public override int FieldCount
		{
			get
			{
				return this._cacheEntry.GetFieldCount(this._metadata);
			}
		}

		// Token: 0x17000777 RID: 1911
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000778 RID: 1912
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000EFDF4 File Offset: 0x000EDFF4
		public override bool GetBoolean(int i)
		{
			return (bool)this.GetValue(i);
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000EFE02 File Offset: 0x000EE002
		public override byte GetByte(int i)
		{
			return (byte)this.GetValue(i);
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000EFE10 File Offset: 0x000EE010
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			byte[] array = (byte[])this.GetValue(i);
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("dataIndex", Strings.ADP_InvalidSourceBufferIndex(array.Length.ToString(CultureInfo.InvariantCulture), ((long)num).ToString(CultureInfo.InvariantCulture)));
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw new ArgumentOutOfRangeException("bufferIndex", Strings.ADP_InvalidDestinationBufferIndex(buffer.Length.ToString(CultureInfo.InvariantCulture), bufferIndex.ToString(CultureInfo.InvariantCulture)));
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw new IndexOutOfRangeException(Strings.ADP_InvalidDataLength(((long)length).ToString(CultureInfo.InvariantCulture)));
				}
				num2 = 0;
			}
			return (long)num2;
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000EFEEC File Offset: 0x000EE0EC
		public override char GetChar(int i)
		{
			return (char)this.GetValue(i);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000EFEFC File Offset: 0x000EE0FC
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			char[] array = (char[])this.GetValue(i);
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("dataIndex", Strings.ADP_InvalidSourceBufferIndex(array.Length.ToString(CultureInfo.InvariantCulture), ((long)num).ToString(CultureInfo.InvariantCulture)));
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw new ArgumentOutOfRangeException("bufferIndex", Strings.ADP_InvalidDestinationBufferIndex(buffer.Length.ToString(CultureInfo.InvariantCulture), bufferIndex.ToString(CultureInfo.InvariantCulture)));
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw new IndexOutOfRangeException(Strings.ADP_InvalidDataLength(((long)length).ToString(CultureInfo.InvariantCulture)));
				}
				num2 = 0;
			}
			return (long)num2;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000EFFD8 File Offset: 0x000EE1D8
		IDataReader IDataRecord.GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000EFFE1 File Offset: 0x000EE1E1
		protected override DbDataReader GetDbDataReader(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000EFFE8 File Offset: 0x000EE1E8
		public override string GetDataTypeName(int i)
		{
			return this.GetFieldType(i).Name;
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000EFFF6 File Offset: 0x000EE1F6
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this.GetValue(i);
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000F0004 File Offset: 0x000EE204
		public override decimal GetDecimal(int i)
		{
			return (decimal)this.GetValue(i);
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000F0012 File Offset: 0x000EE212
		public override double GetDouble(int i)
		{
			return (double)this.GetValue(i);
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000F0020 File Offset: 0x000EE220
		public override Type GetFieldType(int i)
		{
			return this._cacheEntry.GetFieldType(i, this._metadata);
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000F0034 File Offset: 0x000EE234
		public override float GetFloat(int i)
		{
			return (float)this.GetValue(i);
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000F0042 File Offset: 0x000EE242
		public override Guid GetGuid(int i)
		{
			return (Guid)this.GetValue(i);
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000F0050 File Offset: 0x000EE250
		public override short GetInt16(int i)
		{
			return (short)this.GetValue(i);
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000F005E File Offset: 0x000EE25E
		public override int GetInt32(int i)
		{
			return (int)this.GetValue(i);
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000F006C File Offset: 0x000EE26C
		public override long GetInt64(int i)
		{
			return (long)this.GetValue(i);
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000F007A File Offset: 0x000EE27A
		public override string GetName(int i)
		{
			return this._cacheEntry.GetCLayerName(i, this._metadata);
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000F0090 File Offset: 0x000EE290
		public override int GetOrdinal(string name)
		{
			int ordinalforCLayerName = this._cacheEntry.GetOrdinalforCLayerName(name, this._metadata);
			if (ordinalforCLayerName == -1)
			{
				throw new ArgumentOutOfRangeException("name");
			}
			return ordinalforCLayerName;
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000F00C0 File Offset: 0x000EE2C0
		public override string GetString(int i)
		{
			return (string)this.GetValue(i);
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000F00CE File Offset: 0x000EE2CE
		public override object GetValue(int i)
		{
			return this.GetRecordValue(i);
		}

		// Token: 0x0600325B RID: 12891
		protected abstract object GetRecordValue(int ordinal);

		// Token: 0x0600325C RID: 12892 RVA: 0x000F00D8 File Offset: 0x000EE2D8
		public override int GetValues(object[] values)
		{
			Check.NotNull<object[]>(values, "values");
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x000F0117 File Offset: 0x000EE317
		public override bool IsDBNull(int i)
		{
			return this.GetValue(i) == DBNull.Value;
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x000F0127 File Offset: 0x000EE327
		public void SetBoolean(int ordinal, bool value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x000F0136 File Offset: 0x000EE336
		public void SetByte(int ordinal, byte value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x000F0145 File Offset: 0x000EE345
		public void SetChar(int ordinal, char value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000F0154 File Offset: 0x000EE354
		public void SetDataRecord(int ordinal, IDataRecord value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000F015E File Offset: 0x000EE35E
		public void SetDateTime(int ordinal, DateTime value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000F016D File Offset: 0x000EE36D
		public void SetDecimal(int ordinal, decimal value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000F017C File Offset: 0x000EE37C
		public void SetDouble(int ordinal, double value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000F018B File Offset: 0x000EE38B
		public void SetFloat(int ordinal, float value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000F019A File Offset: 0x000EE39A
		public void SetGuid(int ordinal, Guid value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000F01A9 File Offset: 0x000EE3A9
		public void SetInt16(int ordinal, short value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000F01B8 File Offset: 0x000EE3B8
		public void SetInt32(int ordinal, int value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000F01C7 File Offset: 0x000EE3C7
		public void SetInt64(int ordinal, long value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000F01D6 File Offset: 0x000EE3D6
		public void SetString(int ordinal, string value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000F01E0 File Offset: 0x000EE3E0
		public void SetValue(int ordinal, object value)
		{
			this.SetRecordValue(ordinal, value);
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000F01EC File Offset: 0x000EE3EC
		public int SetValues(params object[] values)
		{
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				this.SetRecordValue(i, values[i]);
			}
			return num;
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000F021F File Offset: 0x000EE41F
		public void SetDBNull(int ordinal)
		{
			this.SetRecordValue(ordinal, DBNull.Value);
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x000F022D File Offset: 0x000EE42D
		public virtual DataRecordInfo DataRecordInfo
		{
			get
			{
				if (this._recordInfo == null)
				{
					this._recordInfo = this._cacheEntry.GetDataRecordInfo(this._metadata, this._userObject);
				}
				return this._recordInfo;
			}
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000F025A File Offset: 0x000EE45A
		public DbDataRecord GetDataRecord(int i)
		{
			return (DbDataRecord)this.GetValue(i);
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000F0268 File Offset: 0x000EE468
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x06003271 RID: 12913
		protected abstract void SetRecordValue(int ordinal, object value);

		// Token: 0x04001362 RID: 4962
		internal readonly StateManagerTypeMetadata _metadata;

		// Token: 0x04001363 RID: 4963
		internal readonly ObjectStateEntry _cacheEntry;

		// Token: 0x04001364 RID: 4964
		internal readonly object _userObject;

		// Token: 0x04001365 RID: 4965
		internal DataRecordInfo _recordInfo;
	}
}
