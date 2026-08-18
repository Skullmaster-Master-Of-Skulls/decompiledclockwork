using System;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Numerics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020002DA RID: 730
	internal abstract class DataStorage
	{
		// Token: 0x06002D5E RID: 11614 RVA: 0x00123C0C File Offset: 0x0012300C
		protected DataStorage(DataColumn column, Type type, object defaultValue, StorageType storageType) : this(column, type, defaultValue, DBNull.Value, false, storageType)
		{
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x00123C2C File Offset: 0x0012302C
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, StorageType storageType) : this(column, type, defaultValue, nullValue, false, storageType)
		{
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x00123C48 File Offset: 0x00123048
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, bool isICloneable, StorageType storageType)
		{
			this.Column = column;
			this.Table = column.Table;
			this.DataType = type;
			this.StorageTypeCode = storageType;
			this.DefaultValue = defaultValue;
			this.NullValue = nullValue;
			this.IsCloneable = isICloneable;
			this.IsCustomDefinedType = DataStorage.IsTypeCustomType(this.StorageTypeCode);
			this.IsStringType = (StorageType.String == this.StorageTypeCode || StorageType.SqlString == this.StorageTypeCode);
			this.IsValueType = DataStorage.DetermineIfValueType(this.StorageTypeCode, type);
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x00123CD4 File Offset: 0x001230D4
		internal DataSetDateTime DateTimeMode
		{
			get
			{
				return this.Column.DateTimeMode;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x00123CEC File Offset: 0x001230EC
		internal IFormatProvider FormatProvider
		{
			get
			{
				return this.Table.FormatProvider;
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x00123D04 File Offset: 0x00123104
		public virtual object Aggregate(int[] recordNos, AggregateType kind)
		{
			if (AggregateType.Count == kind)
			{
				return this.AggregateCount(recordNos);
			}
			return null;
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x00123D20 File Offset: 0x00123120
		public object AggregateCount(int[] recordNos)
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

		// Token: 0x06002D65 RID: 11621 RVA: 0x00123D58 File Offset: 0x00123158
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

		// Token: 0x06002D66 RID: 11622
		public abstract int Compare(int recordNo1, int recordNo2);

		// Token: 0x06002D67 RID: 11623
		public abstract int CompareValueTo(int recordNo1, object value);

		// Token: 0x06002D68 RID: 11624 RVA: 0x00123D8C File Offset: 0x0012318C
		public virtual object ConvertValue(object value)
		{
			return value;
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x00123D9C File Offset: 0x0012319C
		protected void CopyBits(int srcRecordNo, int dstRecordNo)
		{
			this.dbNullBits.Set(dstRecordNo, this.dbNullBits.Get(srcRecordNo));
		}

		// Token: 0x06002D6A RID: 11626
		public abstract void Copy(int recordNo1, int recordNo2);

		// Token: 0x06002D6B RID: 11627
		public abstract object Get(int recordNo);

		// Token: 0x06002D6C RID: 11628 RVA: 0x00123DC4 File Offset: 0x001231C4
		protected object GetBits(int recordNo)
		{
			if (this.dbNullBits.Get(recordNo))
			{
				return this.NullValue;
			}
			return this.DefaultValue;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x00123DEC File Offset: 0x001231EC
		public virtual int GetStringLength(int record)
		{
			return int.MaxValue;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x00123E00 File Offset: 0x00123200
		protected bool HasValue(int recordNo)
		{
			return !this.dbNullBits.Get(recordNo);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x00123E1C File Offset: 0x0012321C
		public virtual bool IsNull(int recordNo)
		{
			return this.dbNullBits.Get(recordNo);
		}

		// Token: 0x06002D70 RID: 11632
		public abstract void Set(int recordNo, object value);

		// Token: 0x06002D71 RID: 11633 RVA: 0x00123E38 File Offset: 0x00123238
		protected void SetNullBit(int recordNo, bool flag)
		{
			this.dbNullBits.Set(recordNo, flag);
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x00123E54 File Offset: 0x00123254
		public virtual void SetCapacity(int capacity)
		{
			if (this.dbNullBits == null)
			{
				this.dbNullBits = new BitArray(capacity);
				return;
			}
			this.dbNullBits.Length = capacity;
		}

		// Token: 0x06002D73 RID: 11635
		public abstract object ConvertXmlToObject(string s);

		// Token: 0x06002D74 RID: 11636 RVA: 0x00123E84 File Offset: 0x00123284
		public virtual object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			return this.ConvertXmlToObject(xmlReader.Value);
		}

		// Token: 0x06002D75 RID: 11637
		public abstract string ConvertObjectToXml(object value);

		// Token: 0x06002D76 RID: 11638 RVA: 0x00123EA0 File Offset: 0x001232A0
		public virtual void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			xmlWriter.WriteString(this.ConvertObjectToXml(value));
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x00123EBC File Offset: 0x001232BC
		public static DataStorage CreateStorage(DataColumn column, Type dataType, StorageType typeCode)
		{
			if (typeCode != StorageType.Empty || !(null != dataType))
			{
				switch (typeCode)
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
				case StorageType.BigInteger:
					return new BigIntegerStorage(column);
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

		// Token: 0x06002D78 RID: 11640 RVA: 0x001240C8 File Offset: 0x001234C8
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

		// Token: 0x06002D79 RID: 11641 RVA: 0x00124108 File Offset: 0x00123508
		internal static Type GetTypeStorage(StorageType storageType)
		{
			return DataStorage.StorageClassType[(int)storageType];
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x0012411C File Offset: 0x0012351C
		internal static bool IsTypeCustomType(Type type)
		{
			return DataStorage.IsTypeCustomType(DataStorage.GetStorageType(type));
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x00124134 File Offset: 0x00123534
		internal static bool IsTypeCustomType(StorageType typeCode)
		{
			return StorageType.Object == typeCode || typeCode == StorageType.Empty || StorageType.CharArray == typeCode;
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x00124150 File Offset: 0x00123550
		internal static bool IsSqlType(StorageType storageType)
		{
			return StorageType.SqlBinary <= storageType;
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x00124168 File Offset: 0x00123568
		public static bool IsSqlType(Type dataType)
		{
			for (int i = 26; i < DataStorage.StorageClassType.Length; i++)
			{
				if (dataType == DataStorage.StorageClassType[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x0012419C File Offset: 0x0012359C
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
			case StorageType.BigInteger:
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

		// Token: 0x06002D7F RID: 11647 RVA: 0x0012425C File Offset: 0x0012365C
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
			Tuple<bool, bool, bool, bool> orAdd = DataStorage._typeImplementsInterface.GetOrAdd(dataType, DataStorage._inspectTypeForInterfaces);
			sqlType = false;
			nullable = orAdd.Item1;
			changeTracking = orAdd.Item2;
			revertibleChangeTracking = orAdd.Item3;
			xmlSerializable = orAdd.Item4;
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x001242D4 File Offset: 0x001236D4
		private static Tuple<bool, bool, bool, bool> InspectTypeForInterfaces(Type dataType)
		{
			return new Tuple<bool, bool, bool, bool>(typeof(INullable).IsAssignableFrom(dataType), typeof(IChangeTracking).IsAssignableFrom(dataType), typeof(IRevertibleChangeTracking).IsAssignableFrom(dataType), typeof(IXmlSerializable).IsAssignableFrom(dataType));
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x00124328 File Offset: 0x00123728
		internal static bool ImplementsINullableValue(StorageType typeCode, Type dataType)
		{
			return typeCode == StorageType.Empty && dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x00124358 File Offset: 0x00123758
		public static bool IsObjectNull(object value)
		{
			return value == null || DBNull.Value == value || DataStorage.IsObjectSqlNull(value);
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x00124378 File Offset: 0x00123778
		public static bool IsObjectSqlNull(object value)
		{
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x00124398 File Offset: 0x00123798
		internal object GetEmptyStorageInternal(int recordCount)
		{
			return this.GetEmptyStorage(recordCount);
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x001243AC File Offset: 0x001237AC
		internal void CopyValueInternal(int record, object store, BitArray nullbits, int storeIndex)
		{
			this.CopyValue(record, store, nullbits, storeIndex);
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x001243C4 File Offset: 0x001237C4
		internal void SetStorageInternal(object store, BitArray nullbits)
		{
			this.SetStorage(store, nullbits);
		}

		// Token: 0x06002D87 RID: 11655
		protected abstract object GetEmptyStorage(int recordCount);

		// Token: 0x06002D88 RID: 11656
		protected abstract void CopyValue(int record, object store, BitArray nullbits, int storeIndex);

		// Token: 0x06002D89 RID: 11657
		protected abstract void SetStorage(object store, BitArray nullbits);

		// Token: 0x06002D8A RID: 11658 RVA: 0x001243DC File Offset: 0x001237DC
		protected void SetNullStorage(BitArray nullbits)
		{
			this.dbNullBits = nullbits;
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x001243F0 File Offset: 0x001237F0
		internal static Type GetType(string value)
		{
			Type type = Type.GetType(value);
			if (null == type && "System.Numerics.BigInteger" == value)
			{
				type = typeof(BigInteger);
			}
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return type;
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x0012442C File Offset: 0x0012382C
		internal static string GetQualifiedName(Type type)
		{
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return type.AssemblyQualifiedName;
		}

		// Token: 0x04001C6B RID: 7275
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
			typeof(BigInteger),
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

		// Token: 0x04001C6C RID: 7276
		internal readonly DataColumn Column;

		// Token: 0x04001C6D RID: 7277
		internal readonly DataTable Table;

		// Token: 0x04001C6E RID: 7278
		internal readonly Type DataType;

		// Token: 0x04001C6F RID: 7279
		internal readonly StorageType StorageTypeCode;

		// Token: 0x04001C70 RID: 7280
		private BitArray dbNullBits;

		// Token: 0x04001C71 RID: 7281
		private readonly object DefaultValue;

		// Token: 0x04001C72 RID: 7282
		internal readonly object NullValue;

		// Token: 0x04001C73 RID: 7283
		internal readonly bool IsCloneable;

		// Token: 0x04001C74 RID: 7284
		internal readonly bool IsCustomDefinedType;

		// Token: 0x04001C75 RID: 7285
		internal readonly bool IsStringType;

		// Token: 0x04001C76 RID: 7286
		internal readonly bool IsValueType;

		// Token: 0x04001C77 RID: 7287
		private static readonly Func<Type, Tuple<bool, bool, bool, bool>> _inspectTypeForInterfaces = new Func<Type, Tuple<bool, bool, bool, bool>>(DataStorage.InspectTypeForInterfaces);

		// Token: 0x04001C78 RID: 7288
		private static readonly ConcurrentDictionary<Type, Tuple<bool, bool, bool, bool>> _typeImplementsInterface = new ConcurrentDictionary<Type, Tuple<bool, bool, bool, bool>>();
	}
}
