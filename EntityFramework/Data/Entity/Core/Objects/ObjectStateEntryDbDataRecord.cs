using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005AE RID: 1454
	internal sealed class ObjectStateEntryDbDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x060039B0 RID: 14768 RVA: 0x00111904 File Offset: 0x0010FB04
		internal ObjectStateEntryDbDataRecord(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject)
		{
			EntityState state = cacheEntry.State;
			if (state != EntityState.Unchanged && state != EntityState.Deleted && state != EntityState.Modified)
			{
				return;
			}
			this._cacheEntry = cacheEntry;
			this._userObject = userObject;
			this._metadata = metadata;
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x00111944 File Offset: 0x0010FB44
		internal ObjectStateEntryDbDataRecord(RelationshipEntry cacheEntry)
		{
			EntityState state = cacheEntry.State;
			if (state != EntityState.Unchanged && state != EntityState.Deleted && state != EntityState.Modified)
			{
				return;
			}
			this._cacheEntry = cacheEntry;
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060039B2 RID: 14770 RVA: 0x00111973 File Offset: 0x0010FB73
		public override int FieldCount
		{
			get
			{
				return this._cacheEntry.GetFieldCount(this._metadata);
			}
		}

		// Token: 0x170008C1 RID: 2241
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x170008C2 RID: 2242
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x0011199E File Offset: 0x0010FB9E
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this.GetValue(ordinal);
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x001119AC File Offset: 0x0010FBAC
		public override byte GetByte(int ordinal)
		{
			return (byte)this.GetValue(ordinal);
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x001119BC File Offset: 0x0010FBBC
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
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

		// Token: 0x060039B8 RID: 14776 RVA: 0x00111A98 File Offset: 0x0010FC98
		public override char GetChar(int ordinal)
		{
			return (char)this.GetValue(ordinal);
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x00111AA8 File Offset: 0x0010FCA8
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
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
				throw new ArgumentOutOfRangeException("bufferIndex", Strings.ADP_InvalidSourceBufferIndex(buffer.Length.ToString(CultureInfo.InvariantCulture), ((long)bufferIndex).ToString(CultureInfo.InvariantCulture)));
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

		// Token: 0x060039BA RID: 14778 RVA: 0x00111B85 File Offset: 0x0010FD85
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x00111B8C File Offset: 0x0010FD8C
		public override string GetDataTypeName(int ordinal)
		{
			return this.GetFieldType(ordinal).Name;
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x00111B9A File Offset: 0x0010FD9A
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this.GetValue(ordinal);
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x00111BA8 File Offset: 0x0010FDA8
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this.GetValue(ordinal);
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x00111BB6 File Offset: 0x0010FDB6
		public override double GetDouble(int ordinal)
		{
			return (double)this.GetValue(ordinal);
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x00111BC4 File Offset: 0x0010FDC4
		public override Type GetFieldType(int ordinal)
		{
			return this._cacheEntry.GetFieldType(ordinal, this._metadata);
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x00111BD8 File Offset: 0x0010FDD8
		public override float GetFloat(int ordinal)
		{
			return (float)this.GetValue(ordinal);
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x00111BE6 File Offset: 0x0010FDE6
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this.GetValue(ordinal);
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x00111BF4 File Offset: 0x0010FDF4
		public override short GetInt16(int ordinal)
		{
			return (short)this.GetValue(ordinal);
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x00111C02 File Offset: 0x0010FE02
		public override int GetInt32(int ordinal)
		{
			return (int)this.GetValue(ordinal);
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x00111C10 File Offset: 0x0010FE10
		public override long GetInt64(int ordinal)
		{
			return (long)this.GetValue(ordinal);
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x00111C1E File Offset: 0x0010FE1E
		public override string GetName(int ordinal)
		{
			return this._cacheEntry.GetCLayerName(ordinal, this._metadata);
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x00111C34 File Offset: 0x0010FE34
		public override int GetOrdinal(string name)
		{
			int ordinalforCLayerName = this._cacheEntry.GetOrdinalforCLayerName(name, this._metadata);
			if (ordinalforCLayerName == -1)
			{
				throw new ArgumentOutOfRangeException("name");
			}
			return ordinalforCLayerName;
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x00111C64 File Offset: 0x0010FE64
		public override string GetString(int ordinal)
		{
			return (string)this.GetValue(ordinal);
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x00111C72 File Offset: 0x0010FE72
		public override object GetValue(int ordinal)
		{
			if (this._cacheEntry.IsRelationship)
			{
				return (this._cacheEntry as RelationshipEntry).GetOriginalRelationValue(ordinal);
			}
			return (this._cacheEntry as EntityEntry).GetOriginalEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.OriginalReadonly);
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x00111CB4 File Offset: 0x0010FEB4
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

		// Token: 0x060039CA RID: 14794 RVA: 0x00111CF3 File Offset: 0x0010FEF3
		public override bool IsDBNull(int ordinal)
		{
			return this.GetValue(ordinal) == DBNull.Value;
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060039CB RID: 14795 RVA: 0x00111D03 File Offset: 0x0010FF03
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

		// Token: 0x060039CC RID: 14796 RVA: 0x00111D30 File Offset: 0x0010FF30
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return (DbDataRecord)this.GetValue(ordinal);
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x00111D3E File Offset: 0x0010FF3E
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x040015F4 RID: 5620
		private readonly StateManagerTypeMetadata _metadata;

		// Token: 0x040015F5 RID: 5621
		private readonly ObjectStateEntry _cacheEntry;

		// Token: 0x040015F6 RID: 5622
		private readonly object _userObject;

		// Token: 0x040015F7 RID: 5623
		private DataRecordInfo _recordInfo;
	}
}
