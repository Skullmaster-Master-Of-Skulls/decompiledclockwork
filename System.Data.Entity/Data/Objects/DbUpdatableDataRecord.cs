using System;
using System.Data.Common;

namespace System.Data.Objects
{
	// Token: 0x02000131 RID: 305
	public abstract class DbUpdatableDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x06001625 RID: 5669 RVA: 0x0004ABF9 File Offset: 0x00048DF9
		internal DbUpdatableDataRecord(ObjectStateEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject)
		{
			this._cacheEntry = cacheEntry;
			this._userObject = userObject;
			this._metadata = metadata;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0004AC16 File Offset: 0x00048E16
		internal DbUpdatableDataRecord(ObjectStateEntry cacheEntry) : this(cacheEntry, null, null)
		{
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0004AC21 File Offset: 0x00048E21
		public override int FieldCount
		{
			get
			{
				return this._cacheEntry.GetFieldCount(this._metadata);
			}
		}

		// Token: 0x1700048C RID: 1164
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x1700048D RID: 1165
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000187FB File Offset: 0x000169FB
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this.GetValue(ordinal);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00018809 File Offset: 0x00016A09
		public override byte GetByte(int ordinal)
		{
			return (byte)this.GetValue(ordinal);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0004AC34 File Offset: 0x00048E34
		public override long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			byte[] array = (byte[])this.GetValue(ordinal);
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw EntityUtil.InvalidSourceBufferIndex(array.Length, (long)num, "dataIndex");
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw EntityUtil.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw EntityUtil.InvalidDataLength((long)length);
				}
				num2 = 0;
			}
			return (long)num2;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00018817 File Offset: 0x00016A17
		public override char GetChar(int ordinal)
		{
			return (char)this.GetValue(ordinal);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0004ACC0 File Offset: 0x00048EC0
		public override long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			char[] array = (char[])this.GetValue(ordinal);
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw EntityUtil.InvalidSourceBufferIndex(array.Length, (long)num, "dataIndex");
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw EntityUtil.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw EntityUtil.InvalidDataLength((long)length);
				}
				num2 = 0;
			}
			return (long)num2;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0001898B File Offset: 0x00016B8B
		IDataReader IDataRecord.GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00013A81 File Offset: 0x00011C81
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0004AD4C File Offset: 0x00048F4C
		public override string GetDataTypeName(int ordinal)
		{
			return this.GetFieldType(ordinal).Name;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00018825 File Offset: 0x00016A25
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this.GetValue(ordinal);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00018833 File Offset: 0x00016A33
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this.GetValue(ordinal);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00018841 File Offset: 0x00016A41
		public override double GetDouble(int ordinal)
		{
			return (double)this.GetValue(ordinal);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0004AD5A File Offset: 0x00048F5A
		public override Type GetFieldType(int ordinal)
		{
			return this._cacheEntry.GetFieldType(ordinal, this._metadata);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0001884F File Offset: 0x00016A4F
		public override float GetFloat(int ordinal)
		{
			return (float)this.GetValue(ordinal);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0001885D File Offset: 0x00016A5D
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this.GetValue(ordinal);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0001886B File Offset: 0x00016A6B
		public override short GetInt16(int ordinal)
		{
			return (short)this.GetValue(ordinal);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00018879 File Offset: 0x00016A79
		public override int GetInt32(int ordinal)
		{
			return (int)this.GetValue(ordinal);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00018887 File Offset: 0x00016A87
		public override long GetInt64(int ordinal)
		{
			return (long)this.GetValue(ordinal);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0004AD6E File Offset: 0x00048F6E
		public override string GetName(int ordinal)
		{
			return this._cacheEntry.GetCLayerName(ordinal, this._metadata);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0004AD84 File Offset: 0x00048F84
		public override int GetOrdinal(string name)
		{
			int ordinalforCLayerName = this._cacheEntry.GetOrdinalforCLayerName(name, this._metadata);
			if (ordinalforCLayerName == -1)
			{
				throw EntityUtil.ArgumentOutOfRange("name");
			}
			return ordinalforCLayerName;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00018895 File Offset: 0x00016A95
		public override string GetString(int ordinal)
		{
			return (string)this.GetValue(ordinal);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0004ADB4 File Offset: 0x00048FB4
		public override object GetValue(int ordinal)
		{
			return this.GetRecordValue(ordinal);
		}

		// Token: 0x0600163F RID: 5695
		protected abstract object GetRecordValue(int ordinal);

		// Token: 0x06001640 RID: 5696 RVA: 0x0004ADC0 File Offset: 0x00048FC0
		public override int GetValues(object[] values)
		{
			if (values == null)
			{
				throw EntityUtil.ArgumentNull("values");
			}
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0004AE01 File Offset: 0x00049001
		public override bool IsDBNull(int ordinal)
		{
			return this.GetValue(ordinal) == DBNull.Value;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0004AE11 File Offset: 0x00049011
		public void SetBoolean(int ordinal, bool value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0004AE20 File Offset: 0x00049020
		public void SetByte(int ordinal, byte value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0004AE2F File Offset: 0x0004902F
		public void SetChar(int ordinal, char value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0004AE3E File Offset: 0x0004903E
		public void SetDataRecord(int ordinal, IDataRecord value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0004AE48 File Offset: 0x00049048
		public void SetDateTime(int ordinal, DateTime value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0004AE57 File Offset: 0x00049057
		public void SetDecimal(int ordinal, decimal value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0004AE66 File Offset: 0x00049066
		public void SetDouble(int ordinal, double value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0004AE75 File Offset: 0x00049075
		public void SetFloat(int ordinal, float value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0004AE84 File Offset: 0x00049084
		public void SetGuid(int ordinal, Guid value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0004AE93 File Offset: 0x00049093
		public void SetInt16(int ordinal, short value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0004AEA2 File Offset: 0x000490A2
		public void SetInt32(int ordinal, int value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0004AEB1 File Offset: 0x000490B1
		public void SetInt64(int ordinal, long value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0004AE3E File Offset: 0x0004903E
		public void SetString(int ordinal, string value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0004AEC0 File Offset: 0x000490C0
		public void SetValue(int ordinal, object value)
		{
			this.SetRecordValue(ordinal, value);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0004AECC File Offset: 0x000490CC
		public int SetValues(params object[] values)
		{
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				this.SetRecordValue(i, values[i]);
			}
			return num;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0004AEFF File Offset: 0x000490FF
		public void SetDBNull(int ordinal)
		{
			this.SetRecordValue(ordinal, DBNull.Value);
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0004AF0D File Offset: 0x0004910D
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

		// Token: 0x06001653 RID: 5715 RVA: 0x0001897D File Offset: 0x00016B7D
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return (DbDataRecord)this.GetValue(ordinal);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0001898B File Offset: 0x00016B8B
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x06001655 RID: 5717
		protected abstract void SetRecordValue(int ordinal, object value);

		// Token: 0x04000A4F RID: 2639
		internal readonly StateManagerTypeMetadata _metadata;

		// Token: 0x04000A50 RID: 2640
		internal readonly ObjectStateEntry _cacheEntry;

		// Token: 0x04000A51 RID: 2641
		internal readonly object _userObject;

		// Token: 0x04000A52 RID: 2642
		internal DataRecordInfo _recordInfo;
	}
}
