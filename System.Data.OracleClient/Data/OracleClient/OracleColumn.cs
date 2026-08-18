using System;
using System.Data.Common;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Data.OracleClient
{
	// Token: 0x0200004D RID: 77
	internal sealed class OracleColumn
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0005E104 File Offset: 0x0005D504
		internal OracleColumn(OciStatementHandle statementHandle, int ordinal, OciErrorHandle errorHandle, OracleConnection connection)
		{
			this._ordinal = ordinal;
			this._describeHandle = statementHandle.GetDescriptor(this._ordinal, errorHandle);
			this._connection = connection;
			this._connectionCloseCount = connection.CloseCount;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0005E154 File Offset: 0x0005D554
		internal string ColumnName
		{
			get
			{
				return this._columnName;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0005E174 File Offset: 0x0005D574
		internal bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0005E194 File Offset: 0x0005D594
		internal bool IsLob
		{
			get
			{
				return this._metaType.IsLob;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0005E1B4 File Offset: 0x0005D5B4
		internal bool IsLong
		{
			get
			{
				return this._metaType.IsLong;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0005E1D4 File Offset: 0x0005D5D4
		internal OracleType OracleType
		{
			get
			{
				return this._metaType.OracleType;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0005E1F4 File Offset: 0x0005D5F4
		internal int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0005E214 File Offset: 0x0005D614
		internal byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0005E234 File Offset: 0x0005D634
		internal byte Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0005E254 File Offset: 0x0005D654
		internal int SchemaTableSize
		{
			get
			{
				if (!this._bindAsUTF16 || this._metaType.IsLong)
				{
					return this._byteSize;
				}
				return this._byteSize / 2;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0005E294 File Offset: 0x0005D694
		private int _callback_GetColumnPiecewise(IntPtr octxp, IntPtr defnp, uint iter, IntPtr bufpp, IntPtr alenp, IntPtr piecep, IntPtr indpp, IntPtr rcodep)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<oc._callback_GetColumnPiecewise|ADV|OCI> octxp=0x%-07Ix defnp=0x%-07Ix iter=%-2d bufpp=0x%-07Ix alenp=0x%-07Ix piecep=0x%-07Ix indpp=0x%-07Ix rcodep=0x%-07Ix\n", octxp, defnp, (int)iter, bufpp, alenp, piecep, indpp, rcodep);
			}
			IntPtr val = (-1 != this._indicatorOffset) ? this._rowBuffer.DangerousGetDataPtr(this._indicatorOffset) : IntPtr.Zero;
			IntPtr intPtr;
			IntPtr chunk = this._longBuffer.GetChunk(out intPtr);
			Marshal.WriteIntPtr(bufpp, chunk);
			Marshal.WriteIntPtr(indpp, val);
			Marshal.WriteIntPtr(alenp, intPtr);
			Marshal.WriteInt32(intPtr, NativeBuffer_LongColumnData.MaxChunkSize);
			GC.KeepAlive(this);
			return -24200;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0005E324 File Offset: 0x0005D724
		internal void Bind(OciStatementHandle statementHandle, NativeBuffer_RowBuffer buffer, OciErrorHandle errorHandle, int rowBufferLength)
		{
			OciDefineHandle ociDefineHandle = null;
			OCI.MODE mode = OCI.MODE.OCI_DEFAULT;
			OCI.DATATYPE ociType = this._metaType.OciType;
			this._rowBuffer = buffer;
			int value_sz;
			if (this._metaType.IsLong)
			{
				mode = OCI.MODE.OCI_OBJECT;
				value_sz = int.MaxValue;
			}
			else
			{
				value_sz = this._byteSize;
			}
			IntPtr indp = IntPtr.Zero;
			IntPtr rlenp = IntPtr.Zero;
			IntPtr valuep = this._rowBuffer.DangerousGetDataPtr(this._valueOffset);
			if (-1 != this._indicatorOffset)
			{
				indp = this._rowBuffer.DangerousGetDataPtr(this._indicatorOffset);
			}
			if (-1 != this._lengthOffset && !this._metaType.IsLong)
			{
				rlenp = this._rowBuffer.DangerousGetDataPtr(this._lengthOffset);
			}
			checked
			{
				try
				{
					IntPtr value;
					int num = TracedNativeMethods.OCIDefineByPos(statementHandle, out value, errorHandle, (uint)this._ordinal + 1U, valuep, value_sz, ociType, indp, rlenp, IntPtr.Zero, mode);
					if (num != 0)
					{
						this._connection.CheckError(errorHandle, num);
					}
					ociDefineHandle = new OciDefineHandle(statementHandle, value);
					if (rowBufferLength != 0)
					{
						uint num2 = (uint)rowBufferLength;
						uint indskip = (-1 != this._indicatorOffset) ? num2 : 0U;
						uint rlskip = (-1 != this._lengthOffset && !this._metaType.IsLong) ? num2 : 0U;
						num = TracedNativeMethods.OCIDefineArrayOfStruct(ociDefineHandle, errorHandle, num2, indskip, rlskip, 0U);
						if (num != 0)
						{
							this._connection.CheckError(errorHandle, num);
						}
					}
					if (this._metaType.UsesNationalCharacterSet)
					{
						ociDefineHandle.SetAttribute(OCI.ATTR.OCI_ATTR_CHARSET_FORM, 2, errorHandle);
					}
					if (!this._connection.UnicodeEnabled && this._bindAsUTF16)
					{
						ociDefineHandle.SetAttribute(OCI.ATTR.OCI_ATTR_CHARSET_ID, 1000, errorHandle);
					}
					if (this._metaType.IsLong)
					{
						this._rowBuffer.WriteIntPtr(this._valueOffset, IntPtr.Zero);
						this._callback = new OCI.Callback.OCICallbackDefine(this._callback_GetColumnPiecewise);
						num = TracedNativeMethods.OCIDefineDynamic(ociDefineHandle, errorHandle, IntPtr.Zero, this._callback);
						if (num != 0)
						{
							this._connection.CheckError(errorHandle, num);
						}
					}
				}
				finally
				{
					NativeBuffer.SafeDispose(ref this._longBuffer);
					OciHandle.SafeDispose(ref ociDefineHandle);
				}
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0005E524 File Offset: 0x0005D924
		internal bool Describe(ref int offset, OracleConnection connection, OciErrorHandle errorHandle)
		{
			bool flag = false;
			bool result = false;
			this._describeHandle.GetAttribute(OCI.ATTR.OCI_ATTR_SQLCODE, out this._columnName, errorHandle, this._connection);
			short num;
			this._describeHandle.GetAttribute(OCI.ATTR.OCI_ATTR_OBJECT, out num, errorHandle);
			byte b;
			this._describeHandle.GetAttribute(OCI.ATTR.OCI_ATTR_SESSION, out b, errorHandle);
			this._isNullable = (0 != b);
			OCI.DATATYPE datatype = (OCI.DATATYPE)num;
			OCI.DATATYPE datatype2 = datatype;
			if (datatype2 <= OCI.DATATYPE.CHAR)
			{
				if (datatype2 <= OCI.DATATYPE.DATE)
				{
					switch (datatype2)
					{
					case OCI.DATATYPE.VARCHAR2:
						break;
					case OCI.DATATYPE.NUMBER:
						this._metaType = MetaType.GetMetaTypeForType(OracleType.Number);
						this._byteSize = this._metaType.BindSize;
						this._describeHandle.GetAttribute(OCI.ATTR.OCI_ATTR_ENV, out this._precision, errorHandle);
						this._describeHandle.GetAttribute(OCI.ATTR.OCI_ATTR_SERVER, out this._scale, errorHandle);
						goto IL_43B;
					default:
						switch (datatype2)
						{
						case OCI.DATATYPE.LONG:
							this._metaType = MetaType.GetMetaTypeForType(OracleType.LongVarChar);
							this._byteSize = this._metaType.BindSize;
							flag = true;
							result = true;
							this._bindAsUTF16 = connection.ServerVersionAtLeastOracle8;
							goto IL_43B;
						case (OCI.DATATYPE)9:
						case (OCI.DATATYPE)10:
							goto IL_434;
						case OCI.DATATYPE.ROWID:
							goto IL_318;
						case OCI.DATATYPE.DATE:
							this._metaType = MetaType.GetMetaTypeForType(OracleType.DateTime);
							this._byteSize = this._metaType.BindSize;
							flag = true;
							goto IL_43B;
						default:
							goto IL_434;
						}
						break;
					}
				}
				else
				{
					switch (datatype2)
					{
					case OCI.DATATYPE.RAW:
						this._metaType = MetaType.GetMetaTypeForType(OracleType.Raw);
						this._describeHandle.GetAttribute(OCI.ATTR.OCI_ATTR_FNCODE, out this._byteSize, errorHandle);
						flag = true;
						goto IL_43B;
					case OCI.DATATYPE.LONGRAW:
						this._metaType = MetaType.GetMetaTypeForType(OracleType.LongRaw);
						this._byteSize = this._metaType.BindSize;
						flag = true;
						result = true;
						goto IL_43B;
					default:
						if (datatype2 != OCI.DATATYPE.CHAR)
						{
							goto IL_434;
						}
						break;
					}
				}
				this._describeHandle.GetAttribute(OCI.ATTR.OCI_ATTR_FNCODE, out this._byteSize, errorHandle);
				this._describeHandle.GetAttribute(OCI.ATTR.OCI_ATTR_CHARSET_FORM, out b, errorHandle);
				OCI.CHARSETFORM charsetform = (OCI.CHARSETFORM)b;
				this._bindAsUTF16 = connection.ServerVersionAtLeastOracle8;
				int num2;
				if (connection.ServerVersionAtLeastOracle9i && OCI.ClientVersionAtLeastOracle9i)
				{
					this._describeHandle.GetAttribute(OCI.ATTR.OCI_ATTR_CHAR_SIZE, out num, errorHandle);
					num2 = (int)num;
				}
				else
				{
					num2 = this._byteSize;
				}
				if (charsetform == OCI.CHARSETFORM.SQLCS_NCHAR)
				{
					this._metaType = MetaType.GetMetaTypeForType((OCI.DATATYPE.CHAR == datatype) ? OracleType.NChar : OracleType.NVarChar);
				}
				else
				{
					this._metaType = MetaType.GetMetaTypeForType((OCI.DATATYPE.CHAR == datatype) ? OracleType.Char : OracleType.VarChar);
					if (this._bindAsUTF16)
					{
						this._byteSize *= ADP.CharSize;
					}
				}
				this._byteSize = Math.Max(this._byteSize, num2 * ADP.CharSize);
				flag = true;
				goto IL_43B;
			}
			if (datatype2 <= OCI.DATATYPE.BFILE)
			{
				if (datatype2 != OCI.DATATYPE.ROWID_DESC)
				{
					switch (datatype2)
					{
					case OCI.DATATYPE.CLOB:
						this._describeHandle.GetAttribute(OCI.ATTR.OCI_ATTR_CHARSET_FORM, out b, errorHandle);
						this._metaType = MetaType.GetMetaTypeForType((2 == b) ? OracleType.NClob : OracleType.Clob);
						this._byteSize = this._metaType.BindSize;
						result = true;
						goto IL_43B;
					case OCI.DATATYPE.BLOB:
						this._metaType = MetaType.GetMetaTypeForType(OracleType.Blob);
						this._byteSize = this._metaType.BindSize;
						result = true;
						goto IL_43B;
					case OCI.DATATYPE.BFILE:
						this._metaType = MetaType.GetMetaTypeForType(OracleType.BFile);
						this._byteSize = this._metaType.BindSize;
						result = true;
						goto IL_43B;
					default:
						goto IL_434;
					}
				}
			}
			else
			{
				switch (datatype2)
				{
				case OCI.DATATYPE.TIMESTAMP:
					this._metaType = MetaType.GetMetaTypeForType(OracleType.Timestamp);
					this._byteSize = this._metaType.BindSize;
					flag = true;
					goto IL_43B;
				case OCI.DATATYPE.TIMESTAMP_TZ:
					this._metaType = MetaType.GetMetaTypeForType(OracleType.TimestampWithTZ);
					this._byteSize = this._metaType.BindSize;
					flag = true;
					goto IL_43B;
				case OCI.DATATYPE.INTERVAL_YM:
					this._metaType = MetaType.GetMetaTypeForType(OracleType.IntervalYearToMonth);
					this._byteSize = this._metaType.BindSize;
					goto IL_43B;
				case OCI.DATATYPE.INTERVAL_DS:
					this._metaType = MetaType.GetMetaTypeForType(OracleType.IntervalDayToSecond);
					this._byteSize = this._metaType.BindSize;
					goto IL_43B;
				default:
					if (datatype2 != OCI.DATATYPE.UROWID)
					{
						if (datatype2 != OCI.DATATYPE.TIMESTAMP_LTZ)
						{
							goto IL_434;
						}
						this._metaType = MetaType.GetMetaTypeForType(OracleType.TimestampLocal);
						this._byteSize = this._metaType.BindSize;
						flag = true;
						goto IL_43B;
					}
					break;
				}
			}
			IL_318:
			this._metaType = MetaType.GetMetaTypeForType(OracleType.RowId);
			this._byteSize = this._metaType.BindSize;
			if (connection.UnicodeEnabled)
			{
				this._bindAsUTF16 = true;
				this._byteSize *= ADP.CharSize;
			}
			flag = true;
			goto IL_43B;
			IL_434:
			throw ADP.TypeNotSupported(datatype);
			IL_43B:
			if (this._isNullable)
			{
				this._indicatorOffset = offset;
				offset += IntPtr.Size;
			}
			else
			{
				this._indicatorOffset = -1;
			}
			if (flag)
			{
				this._lengthOffset = offset;
				offset += IntPtr.Size;
			}
			else
			{
				this._lengthOffset = -1;
			}
			this._valueOffset = offset;
			if (OCI.DATATYPE.LONG == datatype || OCI.DATATYPE.LONGRAW == datatype)
			{
				offset += IntPtr.Size;
			}
			else
			{
				offset += this._byteSize;
			}
			offset = (offset + (IntPtr.Size - 1) & ~(IntPtr.Size - 1));
			OciHandle.SafeDispose(ref this._describeHandle);
			return result;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0005EA04 File Offset: 0x0005DE04
		internal void Dispose()
		{
			NativeBuffer.SafeDispose(ref this._longBuffer);
			OciLobLocator.SafeDispose(ref this._lobLocator);
			OciHandle.SafeDispose(ref this._describeHandle);
			this._columnName = null;
			this._metaType = null;
			this._callback = null;
			this._connection = null;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0005EA54 File Offset: 0x0005DE54
		internal void FixupLongValueLength(NativeBuffer buffer)
		{
			if (this._longBuffer != null && -1 == this._longLength)
			{
				this._longLength = this._longBuffer.TotalLengthInBytes;
				if (this._bindAsUTF16)
				{
					this._longLength /= 2;
				}
				buffer.WriteInt32(this._lengthOffset, this._longLength);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0005EAB4 File Offset: 0x0005DEB4
		internal string GetDataTypeName()
		{
			return this._metaType.DataTypeName;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0005EAD4 File Offset: 0x0005DED4
		internal Type GetFieldType()
		{
			return this._metaType.BaseType;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0005EAF4 File Offset: 0x0005DEF4
		internal Type GetFieldOracleType()
		{
			return this._metaType.NoConvertType;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0005EB14 File Offset: 0x0005DF14
		internal object GetValue(NativeBuffer_RowBuffer buffer)
		{
			if (this.IsDBNull(buffer))
			{
				return DBNull.Value;
			}
			OCI.DATATYPE ociType = this._metaType.OciType;
			if (ociType <= OCI.DATATYPE.LONGRAW)
			{
				if (ociType <= OCI.DATATYPE.LONG)
				{
					if (ociType == OCI.DATATYPE.VARCHAR2)
					{
						goto IL_13F;
					}
					switch (ociType)
					{
					case OCI.DATATYPE.VARNUM:
						return this.GetDecimal(buffer);
					case (OCI.DATATYPE)7:
						goto IL_154;
					case OCI.DATATYPE.LONG:
						goto IL_13F;
					default:
						goto IL_154;
					}
				}
				else if (ociType != OCI.DATATYPE.DATE)
				{
					switch (ociType)
					{
					case OCI.DATATYPE.RAW:
					case OCI.DATATYPE.LONGRAW:
					{
						long bytes = this.GetBytes(buffer, 0L, null, 0, 0);
						byte[] array = new byte[bytes];
						this.GetBytes(buffer, 0L, array, 0, (int)bytes);
						return array;
					}
					default:
						goto IL_154;
					}
				}
			}
			else if (ociType <= OCI.DATATYPE.BFILE)
			{
				if (ociType == OCI.DATATYPE.CHAR)
				{
					goto IL_13F;
				}
				switch (ociType)
				{
				case OCI.DATATYPE.CLOB:
				case OCI.DATATYPE.BLOB:
				{
					object value;
					using (OracleLob oracleLob = this.GetOracleLob(buffer))
					{
						value = oracleLob.Value;
					}
					return value;
				}
				case OCI.DATATYPE.BFILE:
				{
					object value2;
					using (OracleBFile oracleBFile = this.GetOracleBFile(buffer))
					{
						value2 = oracleBFile.Value;
					}
					return value2;
				}
				default:
					goto IL_154;
				}
			}
			else
			{
				switch (ociType)
				{
				case OCI.DATATYPE.INT_TIMESTAMP:
				case OCI.DATATYPE.INT_TIMESTAMP_TZ:
					break;
				case OCI.DATATYPE.INT_INTERVAL_YM:
					return this.GetInt32(buffer);
				case OCI.DATATYPE.INT_INTERVAL_DS:
					return this.GetTimeSpan(buffer);
				default:
					if (ociType != OCI.DATATYPE.INT_TIMESTAMP_LTZ)
					{
						goto IL_154;
					}
					break;
				}
			}
			return this.GetDateTime(buffer);
			IL_13F:
			return this.GetString(buffer);
			IL_154:
			throw ADP.TypeNotSupported(this._metaType.OciType);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0005ECC4 File Offset: 0x0005E0C4
		internal object GetOracleValue(NativeBuffer_RowBuffer buffer)
		{
			OCI.DATATYPE ociType = this._metaType.OciType;
			if (ociType <= OCI.DATATYPE.LONGRAW)
			{
				if (ociType <= OCI.DATATYPE.LONG)
				{
					if (ociType == OCI.DATATYPE.VARCHAR2)
					{
						goto IL_E1;
					}
					switch (ociType)
					{
					case OCI.DATATYPE.VARNUM:
						return this.GetOracleNumber(buffer);
					case (OCI.DATATYPE)7:
						goto IL_FB;
					case OCI.DATATYPE.LONG:
						goto IL_E1;
					default:
						goto IL_FB;
					}
				}
				else if (ociType != OCI.DATATYPE.DATE)
				{
					switch (ociType)
					{
					case OCI.DATATYPE.RAW:
					case OCI.DATATYPE.LONGRAW:
						return this.GetOracleBinary(buffer);
					default:
						goto IL_FB;
					}
				}
			}
			else if (ociType <= OCI.DATATYPE.BFILE)
			{
				if (ociType == OCI.DATATYPE.CHAR)
				{
					goto IL_E1;
				}
				switch (ociType)
				{
				case OCI.DATATYPE.CLOB:
				case OCI.DATATYPE.BLOB:
					return this.GetOracleLob(buffer);
				case OCI.DATATYPE.BFILE:
					return this.GetOracleBFile(buffer);
				default:
					goto IL_FB;
				}
			}
			else
			{
				switch (ociType)
				{
				case OCI.DATATYPE.INT_TIMESTAMP:
				case OCI.DATATYPE.INT_TIMESTAMP_TZ:
					break;
				case OCI.DATATYPE.INT_INTERVAL_YM:
					return this.GetOracleMonthSpan(buffer);
				case OCI.DATATYPE.INT_INTERVAL_DS:
					return this.GetOracleTimeSpan(buffer);
				default:
					if (ociType != OCI.DATATYPE.INT_TIMESTAMP_LTZ)
					{
						goto IL_FB;
					}
					break;
				}
			}
			return this.GetOracleDateTime(buffer);
			IL_E1:
			return this.GetOracleString(buffer);
			IL_FB:
			throw ADP.TypeNotSupported(this._metaType.OciType);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0005EDE4 File Offset: 0x0005E1E4
		internal long GetBytes(NativeBuffer_RowBuffer buffer, long fieldOffset, byte[] destinationBuffer, int destinationOffset, int length)
		{
			if (length < 0)
			{
				throw ADP.InvalidDataLength((long)length);
			}
			if (destinationOffset < 0 || (destinationBuffer != null && destinationOffset >= destinationBuffer.Length))
			{
				throw ADP.InvalidDestinationBufferIndex(destinationBuffer.Length, destinationOffset, "bufferoffset");
			}
			if (0L > fieldOffset || (ulong)-1 < (ulong)fieldOffset)
			{
				throw ADP.InvalidSourceOffset("fieldOffset", 0L, (long)((ulong)-1));
			}
			int num3;
			if (this.IsLob)
			{
				OracleType oracleType = this._metaType.OracleType;
				if (OracleType.Blob != oracleType && OracleType.BFile != oracleType)
				{
					throw ADP.InvalidCast();
				}
				if (this.IsDBNull(buffer))
				{
					throw ADP.DataReaderNoData();
				}
				using (OracleLob oracleLob = new OracleLob(this._lobLocator))
				{
					uint num = (uint)oracleLob.Length;
					uint num2 = (uint)fieldOffset;
					if (num2 > num)
					{
						throw ADP.InvalidSourceBufferIndex((int)num, (long)num2, "fieldOffset");
					}
					num3 = (int)(num - num2);
					if (destinationBuffer != null)
					{
						num3 = Math.Min(num3, length);
						if (0 < num3)
						{
							oracleLob.Seek((long)((ulong)num2), SeekOrigin.Begin);
							oracleLob.Read(destinationBuffer, destinationOffset, num3);
						}
					}
					goto IL_155;
				}
			}
			if (OracleType.Raw != this.OracleType && OracleType.LongRaw != this.OracleType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				throw ADP.DataReaderNoData();
			}
			this.FixupLongValueLength(buffer);
			int length2 = OracleBinary.GetLength(buffer, this._lengthOffset, this._metaType);
			int num4 = (int)fieldOffset;
			num3 = length2 - num4;
			if (destinationBuffer != null)
			{
				num3 = Math.Min(num3, length);
				if (0 < num3)
				{
					OracleBinary.GetBytes(buffer, this._valueOffset, this._metaType, num4, destinationBuffer, destinationOffset, num3);
				}
			}
			IL_155:
			return (long)Math.Max(0, num3);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0005EF74 File Offset: 0x0005E374
		internal long GetChars(NativeBuffer_RowBuffer buffer, long fieldOffset, char[] destinationBuffer, int destinationOffset, int length)
		{
			if (length < 0)
			{
				throw ADP.InvalidDataLength((long)length);
			}
			if (destinationOffset < 0 || (destinationBuffer != null && destinationOffset >= destinationBuffer.Length))
			{
				throw ADP.InvalidDestinationBufferIndex(destinationBuffer.Length, destinationOffset, "bufferoffset");
			}
			if (0L > fieldOffset || (ulong)-1 < (ulong)fieldOffset)
			{
				throw ADP.InvalidSourceOffset("fieldOffset", 0L, (long)((ulong)-1));
			}
			int num2;
			if (this.IsLob)
			{
				OracleType oracleType = this._metaType.OracleType;
				if (OracleType.Clob != oracleType && OracleType.NClob != oracleType && OracleType.BFile != oracleType)
				{
					throw ADP.InvalidCast();
				}
				if (this.IsDBNull(buffer))
				{
					throw ADP.DataReaderNoData();
				}
				using (OracleLob oracleLob = new OracleLob(this._lobLocator))
				{
					string text = (string)oracleLob.Value;
					int length2 = text.Length;
					int num = (int)fieldOffset;
					if (num < 0)
					{
						throw ADP.InvalidSourceBufferIndex(length2, (long)num, "fieldOffset");
					}
					num2 = length2 - num;
					if (destinationBuffer != null)
					{
						num2 = Math.Min(num2, length);
						if (0 < num2)
						{
							char[] src = text.ToCharArray(num, num2);
							Buffer.BlockCopy(src, 0, destinationBuffer, destinationOffset, num2);
						}
					}
					goto IL_198;
				}
			}
			if (OracleType.Char != this.OracleType && OracleType.VarChar != this.OracleType && OracleType.LongVarChar != this.OracleType && OracleType.NChar != this.OracleType && OracleType.NVarChar != this.OracleType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				throw ADP.DataReaderNoData();
			}
			this.FixupLongValueLength(buffer);
			int length3 = OracleString.GetLength(buffer, this._lengthOffset, this._metaType);
			int num3 = (int)fieldOffset;
			num2 = length3 - num3;
			if (destinationBuffer != null)
			{
				num2 = Math.Min(num2, length);
				if (0 < num2)
				{
					OracleString.GetChars(buffer, this._valueOffset, this._lengthOffset, this._metaType, this._connection, this._bindAsUTF16, num3, destinationBuffer, destinationOffset, num2);
				}
			}
			IL_198:
			return (long)Math.Max(0, num2);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0005F144 File Offset: 0x0005E544
		internal DateTime GetDateTime(NativeBuffer_RowBuffer buffer)
		{
			if (this.IsDBNull(buffer))
			{
				throw ADP.DataReaderNoData();
			}
			if (typeof(DateTime) != this._metaType.BaseType)
			{
				throw ADP.InvalidCast();
			}
			return OracleDateTime.MarshalToDateTime(buffer, this._valueOffset, this._lengthOffset, this._metaType, this._connection);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0005F1A4 File Offset: 0x0005E5A4
		internal decimal GetDecimal(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(decimal) != this._metaType.BaseType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				throw ADP.DataReaderNoData();
			}
			return OracleNumber.MarshalToDecimal(buffer, this._valueOffset, this._connection);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0005F1F4 File Offset: 0x0005E5F4
		internal double GetDouble(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(decimal) != this._metaType.BaseType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				throw ADP.DataReaderNoData();
			}
			decimal value = OracleNumber.MarshalToDecimal(buffer, this._valueOffset, this._connection);
			return (double)value;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0005F254 File Offset: 0x0005E654
		internal float GetFloat(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(decimal) != this._metaType.BaseType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				throw ADP.DataReaderNoData();
			}
			decimal value = OracleNumber.MarshalToDecimal(buffer, this._valueOffset, this._connection);
			return (float)value;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0005F2B4 File Offset: 0x0005E6B4
		internal int GetInt32(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(int) != this._metaType.BaseType && typeof(decimal) != this._metaType.BaseType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				throw ADP.DataReaderNoData();
			}
			int result;
			if (typeof(int) == this._metaType.BaseType)
			{
				result = OracleMonthSpan.MarshalToInt32(buffer, this._valueOffset);
			}
			else
			{
				result = OracleNumber.MarshalToInt32(buffer, this._valueOffset, this._connection);
			}
			return result;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0005F344 File Offset: 0x0005E744
		internal long GetInt64(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(decimal) != this._metaType.BaseType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				throw ADP.DataReaderNoData();
			}
			return OracleNumber.MarshalToInt64(buffer, this._valueOffset, this._connection);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0005F394 File Offset: 0x0005E794
		internal string GetString(NativeBuffer_RowBuffer buffer)
		{
			if (this.IsLob)
			{
				OracleType oracleType = this._metaType.OracleType;
				if (OracleType.Clob != oracleType && OracleType.NClob != oracleType && OracleType.BFile != oracleType)
				{
					throw ADP.InvalidCast();
				}
				if (this.IsDBNull(buffer))
				{
					throw ADP.DataReaderNoData();
				}
				string result;
				using (OracleLob oracleLob = new OracleLob(this._lobLocator))
				{
					result = (string)oracleLob.Value;
				}
				return result;
			}
			else
			{
				if (typeof(string) != this._metaType.BaseType)
				{
					throw ADP.InvalidCast();
				}
				if (this.IsDBNull(buffer))
				{
					throw ADP.DataReaderNoData();
				}
				this.FixupLongValueLength(buffer);
				return OracleString.MarshalToString(buffer, this._valueOffset, this._lengthOffset, this._metaType, this._connection, this._bindAsUTF16, false);
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0005F474 File Offset: 0x0005E874
		internal TimeSpan GetTimeSpan(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(TimeSpan) != this._metaType.BaseType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				throw ADP.DataReaderNoData();
			}
			return OracleTimeSpan.MarshalToTimeSpan(buffer, this._valueOffset);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0005F4C4 File Offset: 0x0005E8C4
		internal OracleBFile GetOracleBFile(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(OracleBFile) != this._metaType.NoConvertType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				return OracleBFile.Null;
			}
			return new OracleBFile(this._lobLocator);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0005F514 File Offset: 0x0005E914
		internal OracleBinary GetOracleBinary(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(OracleBinary) != this._metaType.NoConvertType)
			{
				throw ADP.InvalidCast();
			}
			this.FixupLongValueLength(buffer);
			if (this.IsDBNull(buffer))
			{
				return OracleBinary.Null;
			}
			OracleBinary result = new OracleBinary(buffer, this._valueOffset, this._lengthOffset, this._metaType);
			return result;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0005F574 File Offset: 0x0005E974
		internal OracleDateTime GetOracleDateTime(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(OracleDateTime) != this._metaType.NoConvertType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				return OracleDateTime.Null;
			}
			OracleDateTime result = new OracleDateTime(buffer, this._valueOffset, this._lengthOffset, this._metaType, this._connection);
			return result;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0005F5D4 File Offset: 0x0005E9D4
		internal OracleLob GetOracleLob(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(OracleLob) != this._metaType.NoConvertType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				return OracleLob.Null;
			}
			return new OracleLob(this._lobLocator);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0005F624 File Offset: 0x0005EA24
		internal OracleMonthSpan GetOracleMonthSpan(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(OracleMonthSpan) != this._metaType.NoConvertType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				return OracleMonthSpan.Null;
			}
			OracleMonthSpan result = new OracleMonthSpan(buffer, this._valueOffset);
			return result;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0005F674 File Offset: 0x0005EA74
		internal OracleNumber GetOracleNumber(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(OracleNumber) != this._metaType.NoConvertType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				return OracleNumber.Null;
			}
			OracleNumber result = new OracleNumber(buffer, this._valueOffset);
			return result;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0005F6C4 File Offset: 0x0005EAC4
		internal OracleString GetOracleString(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(OracleString) != this._metaType.NoConvertType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				return OracleString.Null;
			}
			this.FixupLongValueLength(buffer);
			OracleString result = new OracleString(buffer, this._valueOffset, this._lengthOffset, this._metaType, this._connection, this._bindAsUTF16, false);
			return result;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0005F734 File Offset: 0x0005EB34
		internal OracleTimeSpan GetOracleTimeSpan(NativeBuffer_RowBuffer buffer)
		{
			if (typeof(OracleTimeSpan) != this._metaType.NoConvertType)
			{
				throw ADP.InvalidCast();
			}
			if (this.IsDBNull(buffer))
			{
				return OracleTimeSpan.Null;
			}
			OracleTimeSpan result = new OracleTimeSpan(buffer, this._valueOffset);
			return result;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0005F784 File Offset: 0x0005EB84
		internal bool IsDBNull(NativeBuffer_RowBuffer buffer)
		{
			return this._isNullable && buffer.ReadInt16(this._indicatorOffset) == -1;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0005F7B4 File Offset: 0x0005EBB4
		internal void Rebind(OracleConnection connection, ref bool mustRelease, ref SafeHandle handleToBind)
		{
			handleToBind = null;
			OCI.DATATYPE ociType = this._metaType.OciType;
			if (ociType != OCI.DATATYPE.LONG && ociType != OCI.DATATYPE.LONGRAW)
			{
				switch (ociType)
				{
				case OCI.DATATYPE.CLOB:
				case OCI.DATATYPE.BLOB:
				case OCI.DATATYPE.BFILE:
					OciLobLocator.SafeDispose(ref this._lobLocator);
					this._lobLocator = new OciLobLocator(connection, this._metaType.OracleType);
					handleToBind = this._lobLocator.Descriptor;
					break;
				}
			}
			else
			{
				this._rowBuffer.WriteInt32(this._lengthOffset, 0);
				this._longLength = -1;
				if (this._longBuffer != null)
				{
					this._longBuffer.Reset();
				}
				else
				{
					this._longBuffer = new NativeBuffer_LongColumnData();
				}
				handleToBind = this._longBuffer;
			}
			if (handleToBind != null)
			{
				handleToBind.DangerousAddRef(ref mustRelease);
				this._rowBuffer.WriteIntPtr(this._valueOffset, handleToBind.DangerousGetHandle());
			}
		}

		// Token: 0x0400033C RID: 828
		private OciParameterDescriptor _describeHandle;

		// Token: 0x0400033D RID: 829
		private int _ordinal;

		// Token: 0x0400033E RID: 830
		private string _columnName;

		// Token: 0x0400033F RID: 831
		private MetaType _metaType;

		// Token: 0x04000340 RID: 832
		private byte _precision;

		// Token: 0x04000341 RID: 833
		private byte _scale;

		// Token: 0x04000342 RID: 834
		private int _byteSize;

		// Token: 0x04000343 RID: 835
		private bool _isNullable;

		// Token: 0x04000344 RID: 836
		private int _indicatorOffset;

		// Token: 0x04000345 RID: 837
		private int _lengthOffset;

		// Token: 0x04000346 RID: 838
		private int _valueOffset;

		// Token: 0x04000347 RID: 839
		private NativeBuffer_RowBuffer _rowBuffer;

		// Token: 0x04000348 RID: 840
		private NativeBuffer_LongColumnData _longBuffer;

		// Token: 0x04000349 RID: 841
		private int _longLength;

		// Token: 0x0400034A RID: 842
		private OCI.Callback.OCICallbackDefine _callback;

		// Token: 0x0400034B RID: 843
		private OciLobLocator _lobLocator;

		// Token: 0x0400034C RID: 844
		private OracleConnection _connection;

		// Token: 0x0400034D RID: 845
		private int _connectionCloseCount;

		// Token: 0x0400034E RID: 846
		private bool _bindAsUTF16;
	}
}
