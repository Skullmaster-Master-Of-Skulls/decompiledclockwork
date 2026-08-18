using System;
using System.Data.Common;

namespace System.Data.Objects
{
	// Token: 0x0200013E RID: 318
	internal sealed class ObjectStateEntryDbDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x060016EA RID: 5866 RVA: 0x0004C4BC File Offset: 0x0004A6BC
		internal ObjectStateEntryDbDataRecord(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject)
		{
			EntityUtil.CheckArgumentNull<EntityEntry>(cacheEntry, "cacheEntry");
			EntityUtil.CheckArgumentNull<object>(userObject, "userObject");
			EntityUtil.CheckArgumentNull<StateManagerTypeMetadata>(metadata, "metadata");
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state == EntityState.Deleted || state == EntityState.Modified)
			{
				this._cacheEntry = cacheEntry;
				this._userObject = userObject;
				this._metadata = metadata;
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0004C51C File Offset: 0x0004A71C
		internal ObjectStateEntryDbDataRecord(RelationshipEntry cacheEntry)
		{
			EntityUtil.CheckArgumentNull<RelationshipEntry>(cacheEntry, "cacheEntry");
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state == EntityState.Deleted || state == EntityState.Modified)
			{
				this._cacheEntry = cacheEntry;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0004C556 File Offset: 0x0004A756
		public override int FieldCount
		{
			get
			{
				return this._cacheEntry.GetFieldCount(this._metadata);
			}
		}

		// Token: 0x170004AB RID: 1195
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x170004AC RID: 1196
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000187FB File Offset: 0x000169FB
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this.GetValue(ordinal);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00018809 File Offset: 0x00016A09
		public override byte GetByte(int ordinal)
		{
			return (byte)this.GetValue(ordinal);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0004C56C File Offset: 0x0004A76C
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

		// Token: 0x060016F2 RID: 5874 RVA: 0x00018817 File Offset: 0x00016A17
		public override char GetChar(int ordinal)
		{
			return (char)this.GetValue(ordinal);
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0004C5F8 File Offset: 0x0004A7F8
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
				throw EntityUtil.InvalidSourceBufferIndex(buffer.Length, (long)bufferIndex, "bufferIndex");
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

		// Token: 0x060016F4 RID: 5876 RVA: 0x00013A81 File Offset: 0x00011C81
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0004AD4C File Offset: 0x00048F4C
		public override string GetDataTypeName(int ordinal)
		{
			return this.GetFieldType(ordinal).Name;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00018825 File Offset: 0x00016A25
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this.GetValue(ordinal);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00018833 File Offset: 0x00016A33
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this.GetValue(ordinal);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00018841 File Offset: 0x00016A41
		public override double GetDouble(int ordinal)
		{
			return (double)this.GetValue(ordinal);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0004C685 File Offset: 0x0004A885
		public override Type GetFieldType(int ordinal)
		{
			return this._cacheEntry.GetFieldType(ordinal, this._metadata);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0001884F File Offset: 0x00016A4F
		public override float GetFloat(int ordinal)
		{
			return (float)this.GetValue(ordinal);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0001885D File Offset: 0x00016A5D
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this.GetValue(ordinal);
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0001886B File Offset: 0x00016A6B
		public override short GetInt16(int ordinal)
		{
			return (short)this.GetValue(ordinal);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00018879 File Offset: 0x00016A79
		public override int GetInt32(int ordinal)
		{
			return (int)this.GetValue(ordinal);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00018887 File Offset: 0x00016A87
		public override long GetInt64(int ordinal)
		{
			return (long)this.GetValue(ordinal);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0004C699 File Offset: 0x0004A899
		public override string GetName(int ordinal)
		{
			return this._cacheEntry.GetCLayerName(ordinal, this._metadata);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0004C6B0 File Offset: 0x0004A8B0
		public override int GetOrdinal(string name)
		{
			int ordinalforCLayerName = this._cacheEntry.GetOrdinalforCLayerName(name, this._metadata);
			if (ordinalforCLayerName == -1)
			{
				throw EntityUtil.ArgumentOutOfRange("name");
			}
			return ordinalforCLayerName;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00018895 File Offset: 0x00016A95
		public override string GetString(int ordinal)
		{
			return (string)this.GetValue(ordinal);
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0004C6E0 File Offset: 0x0004A8E0
		public override object GetValue(int ordinal)
		{
			if (this._cacheEntry.IsRelationship)
			{
				return (this._cacheEntry as RelationshipEntry).GetOriginalRelationValue(ordinal);
			}
			return (this._cacheEntry as EntityEntry).GetOriginalEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.OriginalReadonly);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0004C720 File Offset: 0x0004A920
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

		// Token: 0x06001704 RID: 5892 RVA: 0x0004AE01 File Offset: 0x00049001
		public override bool IsDBNull(int ordinal)
		{
			return this.GetValue(ordinal) == DBNull.Value;
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0004C761 File Offset: 0x0004A961
		public DataRecordInfo DataRecordInfo
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

		// Token: 0x06001706 RID: 5894 RVA: 0x0001897D File Offset: 0x00016B7D
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return (DbDataRecord)this.GetValue(ordinal);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0001898B File Offset: 0x00016B8B
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x04000A6D RID: 2669
		private readonly StateManagerTypeMetadata _metadata;

		// Token: 0x04000A6E RID: 2670
		private readonly ObjectStateEntry _cacheEntry;

		// Token: 0x04000A6F RID: 2671
		private readonly object _userObject;

		// Token: 0x04000A70 RID: 2672
		private DataRecordInfo _recordInfo;
	}
}
