using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000052 RID: 82
	internal sealed class SqlRecordBuffer
	{
		// Token: 0x06000370 RID: 880 RVA: 0x001E1DD8 File Offset: 0x001E11D8
		internal SqlRecordBuffer(SmiMetaData metaData)
		{
			this._isNull = true;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000371 RID: 881 RVA: 0x001E1DF8 File Offset: 0x001E11F8
		internal bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000372 RID: 882 RVA: 0x001E1E18 File Offset: 0x001E1218
		// (set) Token: 0x06000373 RID: 883 RVA: 0x001E1E38 File Offset: 0x001E1238
		internal bool Boolean
		{
			get
			{
				return this._value._boolean;
			}
			set
			{
				this._value._boolean = value;
				this._type = SqlRecordBuffer.StorageType.Boolean;
				this._isNull = false;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000374 RID: 884 RVA: 0x001E1E68 File Offset: 0x001E1268
		// (set) Token: 0x06000375 RID: 885 RVA: 0x001E1E88 File Offset: 0x001E1288
		internal byte Byte
		{
			get
			{
				return this._value._byte;
			}
			set
			{
				this._value._byte = value;
				this._type = SqlRecordBuffer.StorageType.Byte;
				this._isNull = false;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000376 RID: 886 RVA: 0x001E1EB8 File Offset: 0x001E12B8
		// (set) Token: 0x06000377 RID: 887 RVA: 0x001E1ED8 File Offset: 0x001E12D8
		internal DateTime DateTime
		{
			get
			{
				return this._value._dateTime;
			}
			set
			{
				this._value._dateTime = value;
				this._type = SqlRecordBuffer.StorageType.DateTime;
				this._isNull = false;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000378 RID: 888 RVA: 0x001E1F08 File Offset: 0x001E1308
		// (set) Token: 0x06000379 RID: 889 RVA: 0x001E1F28 File Offset: 0x001E1328
		internal DateTimeOffset DateTimeOffset
		{
			get
			{
				return this._value._dateTimeOffset;
			}
			set
			{
				this._value._dateTimeOffset = value;
				this._type = SqlRecordBuffer.StorageType.DateTimeOffset;
				this._isNull = false;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600037A RID: 890 RVA: 0x001E1F58 File Offset: 0x001E1358
		// (set) Token: 0x0600037B RID: 891 RVA: 0x001E1F78 File Offset: 0x001E1378
		internal double Double
		{
			get
			{
				return this._value._double;
			}
			set
			{
				this._value._double = value;
				this._type = SqlRecordBuffer.StorageType.Double;
				this._isNull = false;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600037C RID: 892 RVA: 0x001E1FA8 File Offset: 0x001E13A8
		// (set) Token: 0x0600037D RID: 893 RVA: 0x001E1FC8 File Offset: 0x001E13C8
		internal Guid Guid
		{
			get
			{
				return this._value._guid;
			}
			set
			{
				this._value._guid = value;
				this._type = SqlRecordBuffer.StorageType.Guid;
				this._isNull = false;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600037E RID: 894 RVA: 0x001E1FF8 File Offset: 0x001E13F8
		// (set) Token: 0x0600037F RID: 895 RVA: 0x001E2018 File Offset: 0x001E1418
		internal short Int16
		{
			get
			{
				return this._value._int16;
			}
			set
			{
				this._value._int16 = value;
				this._type = SqlRecordBuffer.StorageType.Int16;
				this._isNull = false;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000380 RID: 896 RVA: 0x001E2048 File Offset: 0x001E1448
		// (set) Token: 0x06000381 RID: 897 RVA: 0x001E2068 File Offset: 0x001E1468
		internal int Int32
		{
			get
			{
				return this._value._int32;
			}
			set
			{
				this._value._int32 = value;
				this._type = SqlRecordBuffer.StorageType.Int32;
				this._isNull = false;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000382 RID: 898 RVA: 0x001E2098 File Offset: 0x001E1498
		// (set) Token: 0x06000383 RID: 899 RVA: 0x001E20B8 File Offset: 0x001E14B8
		internal long Int64
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				this._value._int64 = value;
				this._type = SqlRecordBuffer.StorageType.Int64;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000384 RID: 900 RVA: 0x001E20F8 File Offset: 0x001E14F8
		// (set) Token: 0x06000385 RID: 901 RVA: 0x001E2118 File Offset: 0x001E1518
		internal float Single
		{
			get
			{
				return this._value._single;
			}
			set
			{
				this._value._single = value;
				this._type = SqlRecordBuffer.StorageType.Single;
				this._isNull = false;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000386 RID: 902 RVA: 0x001E2148 File Offset: 0x001E1548
		// (set) Token: 0x06000387 RID: 903 RVA: 0x001E21B8 File Offset: 0x001E15B8
		internal string String
		{
			get
			{
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					return (string)this._object;
				}
				if (SqlRecordBuffer.StorageType.CharArray == this._type)
				{
					return new string((char[])this._object, 0, (int)this.CharsLength);
				}
				Stream value = new MemoryStream((byte[])this._object, false);
				return new SqlXml(value).Value;
			}
			set
			{
				this._object = value;
				this._value._int64 = (long)value.Length;
				this._type = SqlRecordBuffer.StorageType.String;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000388 RID: 904 RVA: 0x001E2208 File Offset: 0x001E1608
		// (set) Token: 0x06000389 RID: 905 RVA: 0x001E2228 File Offset: 0x001E1628
		internal SqlDecimal SqlDecimal
		{
			get
			{
				return (SqlDecimal)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlRecordBuffer.StorageType.SqlDecimal;
				this._isNull = false;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600038A RID: 906 RVA: 0x001E2258 File Offset: 0x001E1658
		// (set) Token: 0x0600038B RID: 907 RVA: 0x001E2278 File Offset: 0x001E1678
		internal TimeSpan TimeSpan
		{
			get
			{
				return this._value._timeSpan;
			}
			set
			{
				this._value._timeSpan = value;
				this._type = SqlRecordBuffer.StorageType.TimeSpan;
				this._isNull = false;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600038C RID: 908 RVA: 0x001E22A8 File Offset: 0x001E16A8
		// (set) Token: 0x0600038D RID: 909 RVA: 0x001E22D8 File Offset: 0x001E16D8
		internal long BytesLength
		{
			get
			{
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					this.ConvertXmlStringToByteArray();
				}
				return this._value._int64;
			}
			set
			{
				if (0L == value)
				{
					this._value._int64 = value;
					this._object = new byte[0];
					this._type = SqlRecordBuffer.StorageType.ByteArray;
					this._isNull = false;
					return;
				}
				this._value._int64 = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600038E RID: 910 RVA: 0x001E2328 File Offset: 0x001E1728
		// (set) Token: 0x0600038F RID: 911 RVA: 0x001E2348 File Offset: 0x001E1748
		internal long CharsLength
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				if (0L == value)
				{
					this._value._int64 = value;
					this._object = new char[0];
					this._type = SqlRecordBuffer.StorageType.CharArray;
					this._isNull = false;
					return;
				}
				this._value._int64 = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000390 RID: 912 RVA: 0x001E2398 File Offset: 0x001E1798
		// (set) Token: 0x06000391 RID: 913 RVA: 0x001E2498 File Offset: 0x001E1898
		internal SmiMetaData VariantType
		{
			get
			{
				switch (this._type)
				{
				case SqlRecordBuffer.StorageType.Boolean:
					return SmiMetaData.DefaultBit;
				case SqlRecordBuffer.StorageType.Byte:
					return SmiMetaData.DefaultTinyInt;
				case SqlRecordBuffer.StorageType.ByteArray:
					return SmiMetaData.DefaultVarBinary;
				case SqlRecordBuffer.StorageType.CharArray:
					return SmiMetaData.DefaultNVarChar;
				case SqlRecordBuffer.StorageType.DateTime:
					return SmiMetaData.DefaultDateTime;
				case SqlRecordBuffer.StorageType.DateTimeOffset:
					return SmiMetaData.DefaultDateTimeOffset;
				case SqlRecordBuffer.StorageType.Double:
					return SmiMetaData.DefaultFloat;
				case SqlRecordBuffer.StorageType.Guid:
					return SmiMetaData.DefaultUniqueIdentifier;
				case SqlRecordBuffer.StorageType.Int16:
					return SmiMetaData.DefaultSmallInt;
				case SqlRecordBuffer.StorageType.Int32:
					return SmiMetaData.DefaultInt;
				case SqlRecordBuffer.StorageType.Int64:
					return this._metadata ?? SmiMetaData.DefaultBigInt;
				case SqlRecordBuffer.StorageType.Single:
					return SmiMetaData.DefaultReal;
				case SqlRecordBuffer.StorageType.String:
					return this._metadata ?? SmiMetaData.DefaultNVarChar;
				case SqlRecordBuffer.StorageType.SqlDecimal:
					return new SmiMetaData(SqlDbType.Decimal, 17L, ((SqlDecimal)this._object).Precision, ((SqlDecimal)this._object).Scale, 0L, SqlCompareOptions.None, null);
				case SqlRecordBuffer.StorageType.TimeSpan:
					return SmiMetaData.DefaultTime;
				default:
					return null;
				}
			}
			set
			{
				this._metadata = value;
				this._isMetaSet = true;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x001E24B8 File Offset: 0x001E18B8
		internal int GetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int srcOffset = (int)fieldOffset;
			if (SqlRecordBuffer.StorageType.String == this._type)
			{
				this.ConvertXmlStringToByteArray();
			}
			Buffer.BlockCopy((byte[])this._object, srcOffset, buffer, bufferOffset, length);
			return length;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x001E24F8 File Offset: 0x001E18F8
		internal int GetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int sourceIndex = (int)fieldOffset;
			if (SqlRecordBuffer.StorageType.CharArray == this._type)
			{
				Array.Copy((char[])this._object, sourceIndex, buffer, bufferOffset, length);
			}
			else
			{
				((string)this._object).CopyTo(sourceIndex, buffer, bufferOffset, length);
			}
			return length;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x001E2548 File Offset: 0x001E1948
		internal int SetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (this.IsNull || SqlRecordBuffer.StorageType.ByteArray != this._type)
			{
				if (num != 0)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				this._object = new byte[length];
				this._type = SqlRecordBuffer.StorageType.ByteArray;
				this._isNull = false;
				this.BytesLength = (long)length;
			}
			else
			{
				if ((long)num > this.BytesLength)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				if ((long)(num + length) > this.BytesLength)
				{
					int num2 = ((byte[])this._object).Length;
					if (num + length > num2)
					{
						byte[] array = new byte[Math.Max(num + length, 2 * num2)];
						Buffer.BlockCopy((byte[])this._object, 0, array, 0, (int)this.BytesLength);
						this._object = array;
					}
					this.BytesLength = (long)(num + length);
				}
			}
			Buffer.BlockCopy(buffer, bufferOffset, (byte[])this._object, num, length);
			return length;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x001E2638 File Offset: 0x001E1A38
		internal int SetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (this.IsNull || (SqlRecordBuffer.StorageType.CharArray != this._type && SqlRecordBuffer.StorageType.String != this._type))
			{
				if (num != 0)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				this._object = new char[length];
				this._type = SqlRecordBuffer.StorageType.CharArray;
				this._isNull = false;
				this.CharsLength = (long)length;
			}
			else
			{
				if ((long)num > this.CharsLength)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					this._object = ((string)this._object).ToCharArray();
					this._type = SqlRecordBuffer.StorageType.CharArray;
				}
				if ((long)(num + length) > this.CharsLength)
				{
					int num2 = ((char[])this._object).Length;
					if (num + length > num2)
					{
						char[] array = new char[Math.Max(num + length, 2 * num2)];
						Array.Copy((char[])this._object, 0L, array, 0L, this.CharsLength);
						this._object = array;
					}
					this.CharsLength = (long)(num + length);
				}
			}
			Array.Copy(buffer, bufferOffset, (char[])this._object, num, length);
			return length;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x001E2758 File Offset: 0x001E1B58
		internal void SetNull()
		{
			this._isNull = true;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x001E2778 File Offset: 0x001E1B78
		private void ConvertXmlStringToByteArray()
		{
			string text = (string)this._object;
			byte[] array = new byte[2 + Encoding.Unicode.GetByteCount(text)];
			array[0] = byte.MaxValue;
			array[1] = 254;
			Encoding.Unicode.GetBytes(text, 0, text.Length, array, 2);
			this._object = array;
			this._value._int64 = (long)array.Length;
			this._type = SqlRecordBuffer.StorageType.ByteArray;
		}

		// Token: 0x0400062B RID: 1579
		private bool _isNull;

		// Token: 0x0400062C RID: 1580
		private SqlRecordBuffer.StorageType _type;

		// Token: 0x0400062D RID: 1581
		private SqlRecordBuffer.Storage _value;

		// Token: 0x0400062E RID: 1582
		private object _object;

		// Token: 0x0400062F RID: 1583
		private SmiMetaData _metadata;

		// Token: 0x04000630 RID: 1584
		private bool _isMetaSet;

		// Token: 0x02000053 RID: 83
		internal enum StorageType
		{
			// Token: 0x04000632 RID: 1586
			Boolean,
			// Token: 0x04000633 RID: 1587
			Byte,
			// Token: 0x04000634 RID: 1588
			ByteArray,
			// Token: 0x04000635 RID: 1589
			CharArray,
			// Token: 0x04000636 RID: 1590
			DateTime,
			// Token: 0x04000637 RID: 1591
			DateTimeOffset,
			// Token: 0x04000638 RID: 1592
			Double,
			// Token: 0x04000639 RID: 1593
			Guid,
			// Token: 0x0400063A RID: 1594
			Int16,
			// Token: 0x0400063B RID: 1595
			Int32,
			// Token: 0x0400063C RID: 1596
			Int64,
			// Token: 0x0400063D RID: 1597
			Single,
			// Token: 0x0400063E RID: 1598
			String,
			// Token: 0x0400063F RID: 1599
			SqlDecimal,
			// Token: 0x04000640 RID: 1600
			TimeSpan
		}

		// Token: 0x02000054 RID: 84
		[StructLayout(LayoutKind.Explicit)]
		internal struct Storage
		{
			// Token: 0x04000641 RID: 1601
			[FieldOffset(0)]
			internal bool _boolean;

			// Token: 0x04000642 RID: 1602
			[FieldOffset(0)]
			internal byte _byte;

			// Token: 0x04000643 RID: 1603
			[FieldOffset(0)]
			internal DateTime _dateTime;

			// Token: 0x04000644 RID: 1604
			[FieldOffset(0)]
			internal DateTimeOffset _dateTimeOffset;

			// Token: 0x04000645 RID: 1605
			[FieldOffset(0)]
			internal double _double;

			// Token: 0x04000646 RID: 1606
			[FieldOffset(0)]
			internal Guid _guid;

			// Token: 0x04000647 RID: 1607
			[FieldOffset(0)]
			internal short _int16;

			// Token: 0x04000648 RID: 1608
			[FieldOffset(0)]
			internal int _int32;

			// Token: 0x04000649 RID: 1609
			[FieldOffset(0)]
			internal long _int64;

			// Token: 0x0400064A RID: 1610
			[FieldOffset(0)]
			internal float _single;

			// Token: 0x0400064B RID: 1611
			[FieldOffset(0)]
			internal TimeSpan _timeSpan;
		}
	}
}
