using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200007E RID: 126
	internal sealed class SqlRecordBuffer
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x00048988 File Offset: 0x00047D88
		internal SqlRecordBuffer(SmiMetaData metaData)
		{
			this._isNull = true;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000489A4 File Offset: 0x00047DA4
		internal bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000489B8 File Offset: 0x00047DB8
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x000489D0 File Offset: 0x00047DD0
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000489F8 File Offset: 0x00047DF8
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x00048A10 File Offset: 0x00047E10
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00048A38 File Offset: 0x00047E38
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x00048A50 File Offset: 0x00047E50
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
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00048A90 File Offset: 0x00047E90
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x00048AA8 File Offset: 0x00047EA8
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00048AD0 File Offset: 0x00047ED0
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00048AE8 File Offset: 0x00047EE8
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00048B10 File Offset: 0x00047F10
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x00048B28 File Offset: 0x00047F28
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00048B50 File Offset: 0x00047F50
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x00048B68 File Offset: 0x00047F68
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

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00048B90 File Offset: 0x00047F90
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x00048BA8 File Offset: 0x00047FA8
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00048BD0 File Offset: 0x00047FD0
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x00048BE8 File Offset: 0x00047FE8
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00048C28 File Offset: 0x00048028
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x00048C40 File Offset: 0x00048040
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00048C68 File Offset: 0x00048068
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x00048CCC File Offset: 0x000480CC
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00048D18 File Offset: 0x00048118
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x00048D30 File Offset: 0x00048130
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00048D58 File Offset: 0x00048158
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x00048D70 File Offset: 0x00048170
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00048D98 File Offset: 0x00048198
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x00048DC0 File Offset: 0x000481C0
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
				if (value == 0L)
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00048E04 File Offset: 0x00048204
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00048E1C File Offset: 0x0004821C
		internal long CharsLength
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				if (value == 0L)
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00048E60 File Offset: 0x00048260
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00048F64 File Offset: 0x00048364
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
					return this._metadata ?? SmiMetaData.DefaultDateTime;
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

		// Token: 0x060005F9 RID: 1529 RVA: 0x00048F80 File Offset: 0x00048380
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

		// Token: 0x060005FA RID: 1530 RVA: 0x00048FB8 File Offset: 0x000483B8
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

		// Token: 0x060005FB RID: 1531 RVA: 0x00049000 File Offset: 0x00048400
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

		// Token: 0x060005FC RID: 1532 RVA: 0x000490E4 File Offset: 0x000484E4
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

		// Token: 0x060005FD RID: 1533 RVA: 0x000491FC File Offset: 0x000485FC
		internal void SetNull()
		{
			this._isNull = true;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00049210 File Offset: 0x00048610
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

		// Token: 0x04000260 RID: 608
		private bool _isNull;

		// Token: 0x04000261 RID: 609
		private SqlRecordBuffer.StorageType _type;

		// Token: 0x04000262 RID: 610
		private SqlRecordBuffer.Storage _value;

		// Token: 0x04000263 RID: 611
		private object _object;

		// Token: 0x04000264 RID: 612
		private SmiMetaData _metadata;

		// Token: 0x04000265 RID: 613
		private bool _isMetaSet;

		// Token: 0x02000342 RID: 834
		internal enum StorageType
		{
			// Token: 0x04001E92 RID: 7826
			Boolean,
			// Token: 0x04001E93 RID: 7827
			Byte,
			// Token: 0x04001E94 RID: 7828
			ByteArray,
			// Token: 0x04001E95 RID: 7829
			CharArray,
			// Token: 0x04001E96 RID: 7830
			DateTime,
			// Token: 0x04001E97 RID: 7831
			DateTimeOffset,
			// Token: 0x04001E98 RID: 7832
			Double,
			// Token: 0x04001E99 RID: 7833
			Guid,
			// Token: 0x04001E9A RID: 7834
			Int16,
			// Token: 0x04001E9B RID: 7835
			Int32,
			// Token: 0x04001E9C RID: 7836
			Int64,
			// Token: 0x04001E9D RID: 7837
			Single,
			// Token: 0x04001E9E RID: 7838
			String,
			// Token: 0x04001E9F RID: 7839
			SqlDecimal,
			// Token: 0x04001EA0 RID: 7840
			TimeSpan
		}

		// Token: 0x02000343 RID: 835
		[StructLayout(LayoutKind.Explicit)]
		internal struct Storage
		{
			// Token: 0x04001EA1 RID: 7841
			[FieldOffset(0)]
			internal bool _boolean;

			// Token: 0x04001EA2 RID: 7842
			[FieldOffset(0)]
			internal byte _byte;

			// Token: 0x04001EA3 RID: 7843
			[FieldOffset(0)]
			internal DateTime _dateTime;

			// Token: 0x04001EA4 RID: 7844
			[FieldOffset(0)]
			internal DateTimeOffset _dateTimeOffset;

			// Token: 0x04001EA5 RID: 7845
			[FieldOffset(0)]
			internal double _double;

			// Token: 0x04001EA6 RID: 7846
			[FieldOffset(0)]
			internal Guid _guid;

			// Token: 0x04001EA7 RID: 7847
			[FieldOffset(0)]
			internal short _int16;

			// Token: 0x04001EA8 RID: 7848
			[FieldOffset(0)]
			internal int _int32;

			// Token: 0x04001EA9 RID: 7849
			[FieldOffset(0)]
			internal long _int64;

			// Token: 0x04001EAA RID: 7850
			[FieldOffset(0)]
			internal float _single;

			// Token: 0x04001EAB RID: 7851
			[FieldOffset(0)]
			internal TimeSpan _timeSpan;
		}
	}
}
