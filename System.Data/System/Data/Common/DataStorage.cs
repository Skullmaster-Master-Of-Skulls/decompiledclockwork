using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000113 RID: 275
	internal abstract class DataStorage
	{
		// Token: 0x06001150 RID: 4432 RVA: 0x002330F8 File Offset: 0x002324F8
		protected DataStorage(DataColumn column, Type type, object defaultValue) : this(column, type, defaultValue, DBNull.Value, false)
		{
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00233118 File Offset: 0x00232518
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue) : this(column, type, defaultValue, nullValue, false)
		{
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00233138 File Offset: 0x00232538
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, bool isICloneable)
		{
			this.Column = column;
			this.Table = column.Table;
			this.DataType = type;
			this.StorageTypeCode = DataStorage.GetStorageType(type);
			this.DefaultValue = defaultValue;
			this.NullValue = nullValue;
			this.IsCloneable = isICloneable;
			this.IsCustomDefinedType = DataStorage.IsTypeCustomType(this.StorageTypeCode);
			this.IsStringType = (StorageType.String == this.StorageTypeCode || StorageType.SqlString == this.StorageTypeCode);
			this.IsValueType = DataStorage.DetermineIfValueType(this.StorageTypeCode, type);
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x002331C8 File Offset: 0x002325C8
		internal DataSetDateTime DateTimeMode
		{
			get
			{
				return this.Column.DateTimeMode;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x002331E8 File Offset: 0x002325E8
		internal IFormatProvider FormatProvider
		{
			get
			{
				return this.Table.FormatProvider;
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00233208 File Offset: 0x00232608
		public virtual object Aggregate(int[] recordNos, AggregateType kind)
		{
			if (AggregateType.Count == kind)
			{
				int num = 0;
				for (int i = 0; i < recordNos.Length; i++)
				{
					if (!this.dbNullBits.Get(recordNos[i]))
					{
						num++;
					}
				}
				return num;
			}
			return null;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00233248 File Offset: 0x00232648
		protected int CompareBits(int recordNo1, int recordNo2)
		{
			bool flag = this.dbNullBits.Get(recordNo1);
			bool flag2 = this.dbNullBits.Get(recordNo2);
			if (!(flag ^ flag2))
			{
				return 0;
			}
			if (flag)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00233288 File Offset: 0x00232688
		public virtual int Compare(int recordNo1, int recordNo2)
		{
			object obj = this.Get(recordNo1);
			if (obj is IComparable)
			{
				object obj2 = this.Get(recordNo2);
				if (obj2.GetType() == obj.GetType())
				{
					return ((IComparable)obj).CompareTo(obj2);
				}
				this.CompareBits(recordNo1, recordNo2);
			}
			return 0;
		}

		// Token: 0x06001158 RID: 4440
		public abstract int CompareValueTo(int recordNo1, object value);

		// Token: 0x06001159 RID: 4441 RVA: 0x002332D8 File Offset: 0x002326D8
		public virtual object ConvertValue(object value)
		{
			return value;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x002332E8 File Offset: 0x002326E8
		protected void CopyBits(int srcRecordNo, int dstRecordNo)
		{
			this.dbNullBits.Set(dstRecordNo, this.dbNullBits.Get(srcRecordNo));
		}

		// Token: 0x0600115B RID: 4443
		public abstract void Copy(int recordNo1, int recordNo2);

		// Token: 0x0600115C RID: 4444
		public abstract object Get(int recordNo);

		// Token: 0x0600115D RID: 4445 RVA: 0x00233318 File Offset: 0x00232718
		protected object GetBits(int recordNo)
		{
			if (this.dbNullBits.Get(recordNo))
			{
				return this.NullValue;
			}
			return this.DefaultValue;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00233348 File Offset: 0x00232748
		public virtual int GetStringLength(int record)
		{
			return int.MaxValue;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00233368 File Offset: 0x00232768
		protected bool HasValue(int recordNo)
		{
			return !this.dbNullBits.Get(recordNo);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00233388 File Offset: 0x00232788
		public virtual bool IsNull(int recordNo)
		{
			return this.dbNullBits.Get(recordNo);
		}

		// Token: 0x06001161 RID: 4449
		public abstract void Set(int recordNo, object value);

		// Token: 0x06001162 RID: 4450 RVA: 0x002333A8 File Offset: 0x002327A8
		protected void SetNullBit(int recordNo, bool flag)
		{
			this.dbNullBits.Set(recordNo, flag);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x002333C8 File Offset: 0x002327C8
		public virtual void SetCapacity(int capacity)
		{
			if (this.dbNullBits == null)
			{
				this.dbNullBits = new BitArray(capacity);
				return;
			}
			this.dbNullBits.Length = capacity;
		}

		// Token: 0x06001164 RID: 4452
		public abstract object ConvertXmlToObject(string s);

		// Token: 0x06001165 RID: 4453 RVA: 0x002333F8 File Offset: 0x002327F8
		public virtual object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			return this.ConvertXmlToObject(xmlReader.Value);
		}

		// Token: 0x06001166 RID: 4454
		public abstract string ConvertObjectToXml(object value);

		// Token: 0x06001167 RID: 4455 RVA: 0x00233418 File Offset: 0x00232818
		public virtual void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			xmlWriter.WriteString(this.ConvertObjectToXml(value));
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00233438 File Offset: 0x00232838
		public static DataStorage CreateStorage(DataColumn column, Type dataType)
		{
			StorageType storageType = DataStorage.GetStorageType(dataType);
			if (storageType != StorageType.Empty || dataType == null)
			{
				switch (storageType)
				{
				case StorageType.Empty:
					throw ExceptionBuilder.InvalidStorageType(TypeCode.Empty);
				case StorageType.DBNull:
					throw ExceptionBuilder.InvalidStorageType(TypeCode.DBNull);
				case StorageType.Boolean:
					return new BooleanStorage(column);
				case StorageType.Char:
					return new CharStorage(column);
				case StorageType.SByte:
					return new SByteStorage(column);
				case StorageType.Byte:
					return new ByteStorage(column);
				case StorageType.Int16:
					return new Int16Storage(column);
				case StorageType.UInt16:
					return new UInt16Storage(column);
				case StorageType.Int32:
					return new Int32Storage(column);
				case StorageType.UInt32:
					return new UInt32Storage(column);
				case StorageType.Int64:
					return new Int64Storage(column);
				case StorageType.UInt64:
					return new UInt64Storage(column);
				case StorageType.Single:
					return new SingleStorage(column);
				case StorageType.Double:
					return new DoubleStorage(column);
				case StorageType.Decimal:
					return new DecimalStorage(column);
				case StorageType.DateTime:
					return new DateTimeStorage(column);
				case StorageType.TimeSpan:
					return new TimeSpanStorage(column);
				case StorageType.String:
					return new StringStorage(column);
				case StorageType.Guid:
					return new ObjectStorage(column, dataType);
				case StorageType.ByteArray:
					return new ObjectStorage(column, dataType);
				case StorageType.CharArray:
					return new ObjectStorage(column, dataType);
				case StorageType.Type:
					return new ObjectStorage(column, dataType);
				case StorageType.DateTimeOffset:
					return new DateTimeOffsetStorage(column);
				case StorageType.Uri:
					return new ObjectStorage(column, dataType);
				case StorageType.SqlBinary:
					return new SqlBinaryStorage(column);
				case StorageType.SqlBoolean:
					return new SqlBooleanStorage(column);
				case StorageType.SqlByte:
					return new SqlByteStorage(column);
				case StorageType.SqlBytes:
					return new SqlBytesStorage(column);
				case StorageType.SqlChars:
					return new SqlCharsStorage(column);
				case StorageType.SqlDateTime:
					return new SqlDateTimeStorage(column);
				case StorageType.SqlDecimal:
					return new SqlDecimalStorage(column);
				case StorageType.SqlDouble:
					return new SqlDoubleStorage(column);
				case StorageType.SqlGuid:
					return new SqlGuidStorage(column);
				case StorageType.SqlInt16:
					return new SqlInt16Storage(column);
				case StorageType.SqlInt32:
					return new SqlInt32Storage(column);
				case StorageType.SqlInt64:
					return new SqlInt64Storage(column);
				case StorageType.SqlMoney:
					return new SqlMoneyStorage(column);
				case StorageType.SqlSingle:
					return new SqlSingleStorage(column);
				case StorageType.SqlString:
					return new SqlStringStorage(column);
				}
				return new ObjectStorage(column, dataType);
			}
			if (typeof(INullable).IsAssignableFrom(dataType))
			{
				return new SqlUdtStorage(column, dataType);
			}
			return new ObjectStorage(column, dataType);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00233648 File Offset: 0x00232A48
		internal static StorageType GetStorageType(Type dataType)
		{
			for (int i = 0; i < DataStorage.StorageClassType.Length; i++)
			{
				if (dataType == DataStorage.StorageClassType[i])
				{
					return (StorageType)i;
				}
			}
			TypeCode typeCode = Type.GetTypeCode(dataType);
			if (TypeCode.Object != typeCode)
			{
				return (StorageType)typeCode;
			}
			return StorageType.Empty;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00233688 File Offset: 0x00232A88
		internal static Type GetTypeStorage(StorageType storageType)
		{
			return DataStorage.StorageClassType[(int)storageType];
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x002336A8 File Offset: 0x00232AA8
		internal static bool IsTypeCustomType(Type type)
		{
			return DataStorage.IsTypeCustomType(DataStorage.GetStorageType(type));
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x002336C8 File Offset: 0x00232AC8
		internal static bool IsTypeCustomType(StorageType typeCode)
		{
			return StorageType.Object == typeCode || typeCode == StorageType.Empty || StorageType.CharArray == typeCode;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x002336E8 File Offset: 0x00232AE8
		internal static bool IsSqlType(StorageType storageType)
		{
			return StorageType.SqlBinary <= storageType;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00233708 File Offset: 0x00232B08
		public static bool IsSqlType(Type dataType)
		{
			for (int i = 25; i < DataStorage.StorageClassType.Length; i++)
			{
				if (dataType == DataStorage.StorageClassType[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00233738 File Offset: 0x00232B38
		private static bool DetermineIfValueType(StorageType typeCode, Type dataType)
		{
			bool result;
			switch (typeCode)
			{
			case StorageType.Boolean:
			case StorageType.Char:
			case StorageType.SByte:
			case StorageType.Byte:
			case StorageType.Int16:
			case StorageType.UInt16:
			case StorageType.Int32:
			case StorageType.UInt32:
			case StorageType.Int64:
			case StorageType.UInt64:
			case StorageType.Single:
			case StorageType.Double:
			case StorageType.Decimal:
			case StorageType.DateTime:
			case StorageType.TimeSpan:
			case StorageType.Guid:
			case StorageType.DateTimeOffset:
			case StorageType.SqlBinary:
			case StorageType.SqlBoolean:
			case StorageType.SqlByte:
			case StorageType.SqlDateTime:
			case StorageType.SqlDecimal:
			case StorageType.SqlDouble:
			case StorageType.SqlGuid:
			case StorageType.SqlInt16:
			case StorageType.SqlInt32:
			case StorageType.SqlInt64:
			case StorageType.SqlMoney:
			case StorageType.SqlSingle:
			case StorageType.SqlString:
				result = true;
				break;
			case StorageType.String:
			case StorageType.ByteArray:
			case StorageType.CharArray:
			case StorageType.Type:
			case StorageType.Uri:
			case StorageType.SqlBytes:
			case StorageType.SqlChars:
				result = false;
				break;
			default:
				result = dataType.IsValueType;
				break;
			}
			return result;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x002337F8 File Offset: 0x00232BF8
		internal static void ImplementsInterfaces(StorageType typeCode, Type dataType, out bool sqlType, out bool nullable, out bool xmlSerializable, out bool changeTracking, out bool revertibleChangeTracking)
		{
			if (DataStorage.IsSqlType(typeCode))
			{
				sqlType = true;
				nullable = true;
				changeTracking = false;
				revertibleChangeTracking = false;
				xmlSerializable = true;
				return;
			}
			if (typeCode != StorageType.Empty)
			{
				sqlType = false;
				nullable = false;
				changeTracking = false;
				revertibleChangeTracking = false;
				xmlSerializable = false;
				return;
			}
			sqlType = false;
			nullable = typeof(INullable).IsAssignableFrom(dataType);
			changeTracking = typeof(IChangeTracking).IsAssignableFrom(dataType);
			revertibleChangeTracking = typeof(IRevertibleChangeTracking).IsAssignableFrom(dataType);
			xmlSerializable = typeof(IXmlSerializable).IsAssignableFrom(dataType);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00233888 File Offset: 0x00232C88
		internal static bool ImplementsINullableValue(StorageType typeCode, Type dataType)
		{
			return typeCode == StorageType.Empty && dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x002338B8 File Offset: 0x00232CB8
		public static bool IsObjectNull(object value)
		{
			return value == null || DBNull.Value == value || DataStorage.IsObjectSqlNull(value);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x002338D8 File Offset: 0x00232CD8
		public static bool IsObjectSqlNull(object value)
		{
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x002338F8 File Offset: 0x00232CF8
		internal object GetEmptyStorageInternal(int recordCount)
		{
			return this.GetEmptyStorage(recordCount);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00233918 File Offset: 0x00232D18
		internal void CopyValueInternal(int record, object store, BitArray nullbits, int storeIndex)
		{
			this.CopyValue(record, store, nullbits, storeIndex);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00233938 File Offset: 0x00232D38
		internal void SetStorageInternal(object store, BitArray nullbits)
		{
			this.SetStorage(store, nullbits);
		}

		// Token: 0x06001177 RID: 4471
		protected abstract object GetEmptyStorage(int recordCount);

		// Token: 0x06001178 RID: 4472
		protected abstract void CopyValue(int record, object store, BitArray nullbits, int storeIndex);

		// Token: 0x06001179 RID: 4473
		protected abstract void SetStorage(object store, BitArray nullbits);

		// Token: 0x0600117A RID: 4474 RVA: 0x00233958 File Offset: 0x00232D58
		protected void SetNullStorage(BitArray nullbits)
		{
			this.dbNullBits = nullbits;
		}

		// Token: 0x04000B6A RID: 2922
		private static readonly Type[] StorageClassType = new Type[]
		{
			null,
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(string),
			typeof(Guid),
			typeof(byte[]),
			typeof(char[]),
			typeof(Type),
			typeof(DateTimeOffset),
			typeof(Uri),
			typeof(SqlBinary),
			typeof(SqlBoolean),
			typeof(SqlByte),
			typeof(SqlBytes),
			typeof(SqlChars),
			typeof(SqlDateTime),
			typeof(SqlDecimal),
			typeof(SqlDouble),
			typeof(SqlGuid),
			typeof(SqlInt16),
			typeof(SqlInt32),
			typeof(SqlInt64),
			typeof(SqlMoney),
			typeof(SqlSingle),
			typeof(SqlString)
		};

		// Token: 0x04000B6B RID: 2923
		internal readonly DataColumn Column;

		// Token: 0x04000B6C RID: 2924
		internal readonly DataTable Table;

		// Token: 0x04000B6D RID: 2925
		internal readonly Type DataType;

		// Token: 0x04000B6E RID: 2926
		internal readonly StorageType StorageTypeCode;

		// Token: 0x04000B6F RID: 2927
		private BitArray dbNullBits;

		// Token: 0x04000B70 RID: 2928
		private readonly object DefaultValue;

		// Token: 0x04000B71 RID: 2929
		internal readonly object NullValue;

		// Token: 0x04000B72 RID: 2930
		internal readonly bool IsCloneable;

		// Token: 0x04000B73 RID: 2931
		internal readonly bool IsCustomDefinedType;

		// Token: 0x04000B74 RID: 2932
		internal readonly bool IsStringType;

		// Token: 0x04000B75 RID: 2933
		internal readonly bool IsValueType;
	}
}
