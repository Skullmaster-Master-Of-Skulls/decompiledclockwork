using System;
using System.Data.Common;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Data.OleDb
{
	// Token: 0x0200023E RID: 574
	internal sealed class ColumnBinding
	{
		// Token: 0x06002344 RID: 9028 RVA: 0x000F3B74 File Offset: 0x000F2F74
		internal ColumnBinding(OleDbDataReader dataReader, int index, int indexForAccessor, int indexWithinAccessor, OleDbParameter parameter, RowBinding rowbinding, Bindings bindings, tagDBBINDING binding, int offset, bool ifIRowsetElseIRow)
		{
			this._dataReader = dataReader;
			this._rowbinding = rowbinding;
			this._bindings = bindings;
			this._index = index;
			this._indexForAccessor = indexForAccessor;
			this._indexWithinAccessor = indexWithinAccessor;
			if (parameter != null)
			{
				this._parameter = parameter;
				this._parameterChangeID = parameter.ChangeID;
			}
			this._offsetStatus = binding.obStatus.ToInt32() + offset;
			this._offsetLength = binding.obLength.ToInt32() + offset;
			this._offsetValue = binding.obValue.ToInt32() + offset;
			this._ordinal = binding.iOrdinal.ToInt32();
			this._maxLen = binding.cbMaxLen.ToInt32();
			this._wType = binding.wType;
			this._precision = binding.bPrecision;
			this._ifIRowsetElseIRow = ifIRowsetElseIRow;
			this.SetSize(this.Bindings.ParamSize.ToInt32());
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x000F3C6C File Offset: 0x000F306C
		internal Bindings Bindings
		{
			get
			{
				this._bindings.CurrentIndex = this.IndexWithinAccessor;
				return this._bindings;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x000F3C90 File Offset: 0x000F3090
		internal RowBinding RowBinding
		{
			get
			{
				return this._rowbinding;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000F3CA4 File Offset: 0x000F30A4
		internal int ColumnBindingOrdinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x000F3CB8 File Offset: 0x000F30B8
		private int ColumnBindingMaxLen
		{
			get
			{
				return this._maxLen;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x000F3CCC File Offset: 0x000F30CC
		private byte ColumnBindingPrecision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x000F3CE0 File Offset: 0x000F30E0
		private short DbType
		{
			get
			{
				return this._wType;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x000F3CF4 File Offset: 0x000F30F4
		private Type ExpectedType
		{
			get
			{
				return NativeDBType.FromDBType(this.DbType, false, false).dataType;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x000F3D14 File Offset: 0x000F3114
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x000F3D28 File Offset: 0x000F3128
		internal int IndexForAccessor
		{
			get
			{
				return this._indexForAccessor;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x000F3D3C File Offset: 0x000F313C
		internal int IndexWithinAccessor
		{
			get
			{
				return this._indexWithinAccessor;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x000F3D50 File Offset: 0x000F3150
		private int ValueBindingOffset
		{
			get
			{
				return this._valueBindingOffset;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x000F3D64 File Offset: 0x000F3164
		private int ValueBindingSize
		{
			get
			{
				return this._valueBindingSize;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000F3D78 File Offset: 0x000F3178
		internal int ValueOffset
		{
			get
			{
				return this._offsetValue;
			}
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000F3D8C File Offset: 0x000F318C
		private OleDbDataReader DataReader()
		{
			return this._dataReader;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000F3DA0 File Offset: 0x000F31A0
		internal bool IsParameterBindingInvalid(OleDbParameter parameter)
		{
			return this._parameter.ChangeID != this._parameterChangeID || this._parameter != parameter;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000F3DD0 File Offset: 0x000F31D0
		internal bool IsValueNull()
		{
			return DBStatus.S_ISNULL == this.StatusValue() || ((12 == this.DbType || 138 == this.DbType) && Convert.IsDBNull(this.ValueVariant()));
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000F3E0C File Offset: 0x000F320C
		private int LengthValue()
		{
			int val;
			if (this._ifIRowsetElseIRow)
			{
				val = this.RowBinding.ReadIntPtr(this._offsetLength).ToInt32();
			}
			else
			{
				val = this.Bindings.DBColumnAccess[this.IndexWithinAccessor].cbDataLen.ToInt32();
			}
			return Math.Max(val, 0);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000F3E68 File Offset: 0x000F3268
		private void LengthValue(int value)
		{
			this.RowBinding.WriteIntPtr(this._offsetLength, (IntPtr)value);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000F3E8C File Offset: 0x000F328C
		internal OleDbParameter Parameter()
		{
			return this._parameter;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000F3EA0 File Offset: 0x000F32A0
		internal void ResetValue()
		{
			this._value = null;
			StringMemHandle sptr = this._sptr;
			this._sptr = null;
			if (sptr != null)
			{
				sptr.Dispose();
			}
			if (this._pinnedBuffer.IsAllocated)
			{
				this._pinnedBuffer.Free();
			}
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000F3EE4 File Offset: 0x000F32E4
		internal DBStatus StatusValue()
		{
			if (this._ifIRowsetElseIRow)
			{
				return (DBStatus)this.RowBinding.ReadInt32(this._offsetStatus);
			}
			return (DBStatus)this.Bindings.DBColumnAccess[this.IndexWithinAccessor].dwStatus;
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000F3F28 File Offset: 0x000F3328
		internal void StatusValue(DBStatus value)
		{
			this.RowBinding.WriteInt32(this._offsetStatus, (int)value);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000F3F48 File Offset: 0x000F3348
		internal void SetOffset(int offset)
		{
			if (0 > offset)
			{
				throw ADP.InvalidOffsetValue(offset);
			}
			this._valueBindingOffset = Math.Max(offset, 0);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000F3F70 File Offset: 0x000F3370
		internal void SetSize(int size)
		{
			this._valueBindingSize = Math.Max(size, 0);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000F3F8C File Offset: 0x000F338C
		private void SetValueDBNull()
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_ISNULL);
			this.RowBinding.WriteInt64(this.ValueOffset, 0L);
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000F3FBC File Offset: 0x000F33BC
		private void SetValueEmpty()
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_DEFAULT);
			this.RowBinding.WriteInt64(this.ValueOffset, 0L);
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000F3FEC File Offset: 0x000F33EC
		internal object Value()
		{
			object obj = this._value;
			if (obj == null)
			{
				DBStatus dbstatus = this.StatusValue();
				switch (dbstatus)
				{
				case DBStatus.S_OK:
				{
					short dbType = this.DbType;
					if (dbType <= 72)
					{
						switch (dbType)
						{
						case 0:
						case 1:
							obj = DBNull.Value;
							goto IL_379;
						case 2:
							obj = this.Value_I2();
							goto IL_379;
						case 3:
							obj = this.Value_I4();
							goto IL_379;
						case 4:
							obj = this.Value_R4();
							goto IL_379;
						case 5:
							obj = this.Value_R8();
							goto IL_379;
						case 6:
							obj = this.Value_CY();
							goto IL_379;
						case 7:
							obj = this.Value_DATE();
							goto IL_379;
						case 8:
							obj = this.Value_BSTR();
							goto IL_379;
						case 9:
							obj = this.Value_IDISPATCH();
							goto IL_379;
						case 10:
							obj = this.Value_ERROR();
							goto IL_379;
						case 11:
							obj = this.Value_BOOL();
							goto IL_379;
						case 12:
							obj = this.Value_VARIANT();
							goto IL_379;
						case 13:
							obj = this.Value_IUNKNOWN();
							goto IL_379;
						case 14:
							obj = this.Value_DECIMAL();
							goto IL_379;
						case 15:
							break;
						case 16:
							obj = (short)this.Value_I1();
							goto IL_379;
						case 17:
							obj = this.Value_UI1();
							goto IL_379;
						case 18:
							obj = (int)this.Value_UI2();
							goto IL_379;
						case 19:
							obj = (long)((ulong)this.Value_UI4());
							goto IL_379;
						case 20:
							obj = this.Value_I8();
							goto IL_379;
						case 21:
							obj = this.Value_UI8();
							goto IL_379;
						default:
							if (dbType == 64)
							{
								obj = this.Value_FILETIME();
								goto IL_379;
							}
							if (dbType == 72)
							{
								obj = this.Value_GUID();
								goto IL_379;
							}
							break;
						}
					}
					else
					{
						switch (dbType)
						{
						case 128:
							obj = this.Value_BYTES();
							goto IL_379;
						case 129:
						case 132:
						case 137:
							break;
						case 130:
							obj = this.Value_WSTR();
							goto IL_379;
						case 131:
							obj = this.Value_NUMERIC();
							goto IL_379;
						case 133:
							obj = this.Value_DBDATE();
							goto IL_379;
						case 134:
							obj = this.Value_DBTIME();
							goto IL_379;
						case 135:
							obj = this.Value_DBTIMESTAMP();
							goto IL_379;
						case 136:
							obj = this.Value_HCHAPTER();
							goto IL_379;
						case 138:
							obj = this.Value_VARIANT();
							goto IL_379;
						default:
							if (dbType == 16512)
							{
								obj = this.Value_ByRefBYTES();
								goto IL_379;
							}
							if (dbType == 16514)
							{
								obj = this.Value_ByRefWSTR();
								goto IL_379;
							}
							break;
						}
					}
					throw ODB.GVtUnknown((int)this.DbType);
				}
				case DBStatus.E_BADACCESSOR:
				case DBStatus.E_CANTCONVERTVALUE:
					goto IL_372;
				case DBStatus.S_ISNULL:
					break;
				case DBStatus.S_TRUNCATED:
				{
					short dbType2 = this.DbType;
					if (dbType2 <= 130)
					{
						if (dbType2 == 128)
						{
							obj = this.Value_BYTES();
							goto IL_379;
						}
						if (dbType2 == 130)
						{
							obj = this.Value_WSTR();
							goto IL_379;
						}
					}
					else
					{
						if (dbType2 == 16512)
						{
							obj = this.Value_ByRefBYTES();
							goto IL_379;
						}
						if (dbType2 == 16514)
						{
							obj = this.Value_ByRefWSTR();
							goto IL_379;
						}
					}
					throw ODB.GVtUnknown((int)this.DbType);
				}
				default:
					if (dbstatus != DBStatus.S_DEFAULT)
					{
						goto IL_372;
					}
					break;
				}
				obj = DBNull.Value;
				goto IL_379;
				IL_372:
				throw this.CheckTypeValueStatusValue();
				IL_379:
				this._value = obj;
			}
			return obj;
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000F437C File Offset: 0x000F377C
		internal void Value(object value)
		{
			if (value == null)
			{
				this.SetValueEmpty();
				return;
			}
			if (Convert.IsDBNull(value))
			{
				this.SetValueDBNull();
				return;
			}
			short dbType = this.DbType;
			if (dbType <= 72)
			{
				switch (dbType)
				{
				case 0:
					this.SetValueEmpty();
					return;
				case 1:
					this.SetValueDBNull();
					return;
				case 2:
					this.Value_I2((short)value);
					return;
				case 3:
					this.Value_I4((int)value);
					return;
				case 4:
					this.Value_R4((float)value);
					return;
				case 5:
					this.Value_R8((double)value);
					return;
				case 6:
					this.Value_CY((decimal)value);
					return;
				case 7:
					this.Value_DATE((DateTime)value);
					return;
				case 8:
					this.Value_BSTR((string)value);
					return;
				case 9:
					this.Value_IDISPATCH(value);
					return;
				case 10:
					this.Value_ERROR((int)value);
					return;
				case 11:
					this.Value_BOOL((bool)value);
					return;
				case 12:
					this.Value_VARIANT(value);
					return;
				case 13:
					this.Value_IUNKNOWN(value);
					return;
				case 14:
					this.Value_DECIMAL((decimal)value);
					return;
				case 15:
					break;
				case 16:
					if (value is short)
					{
						this.Value_I1(Convert.ToSByte((short)value, CultureInfo.InvariantCulture));
						return;
					}
					this.Value_I1((sbyte)value);
					return;
				case 17:
					this.Value_UI1((byte)value);
					return;
				case 18:
					if (value is int)
					{
						this.Value_UI2(Convert.ToUInt16((int)value, CultureInfo.InvariantCulture));
						return;
					}
					this.Value_UI2((ushort)value);
					return;
				case 19:
					if (value is long)
					{
						this.Value_UI4(Convert.ToUInt32((long)value, CultureInfo.InvariantCulture));
						return;
					}
					this.Value_UI4((uint)value);
					return;
				case 20:
					this.Value_I8((long)value);
					return;
				case 21:
					if (value is decimal)
					{
						this.Value_UI8(Convert.ToUInt64((decimal)value, CultureInfo.InvariantCulture));
						return;
					}
					this.Value_UI8((ulong)value);
					return;
				default:
					if (dbType == 64)
					{
						this.Value_FILETIME((DateTime)value);
						return;
					}
					if (dbType == 72)
					{
						this.Value_GUID((Guid)value);
						return;
					}
					break;
				}
			}
			else
			{
				switch (dbType)
				{
				case 128:
					this.Value_BYTES((byte[])value);
					return;
				case 129:
				case 132:
				case 136:
				case 137:
					break;
				case 130:
					if (value is string)
					{
						this.Value_WSTR((string)value);
						return;
					}
					this.Value_WSTR((char[])value);
					return;
				case 131:
					this.Value_NUMERIC((decimal)value);
					return;
				case 133:
					this.Value_DBDATE((DateTime)value);
					return;
				case 134:
					this.Value_DBTIME((TimeSpan)value);
					return;
				case 135:
					this.Value_DBTIMESTAMP((DateTime)value);
					return;
				case 138:
					this.Value_VARIANT(value);
					return;
				default:
					if (dbType == 16512)
					{
						this.Value_ByRefBYTES((byte[])value);
						return;
					}
					if (dbType == 16514)
					{
						if (value is string)
						{
							this.Value_ByRefWSTR((string)value);
							return;
						}
						this.Value_ByRefWSTR((char[])value);
						return;
					}
					break;
				}
			}
			throw ODB.SVtUnknown((int)this.DbType);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000F46BC File Offset: 0x000F3ABC
		internal bool Value_BOOL()
		{
			short num = this.RowBinding.ReadInt16(this.ValueOffset);
			return num != 0;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000F46E0 File Offset: 0x000F3AE0
		private void Value_BOOL(bool value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteInt16(this.ValueOffset, value ? -1 : 0);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000F4714 File Offset: 0x000F3B14
		private string Value_BSTR()
		{
			string result = "";
			RowBinding rowBinding = this.RowBinding;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				rowBinding.DangerousAddRef(ref flag);
				IntPtr intPtr = rowBinding.ReadIntPtr(this.ValueOffset);
				if (ADP.PtrZero != intPtr)
				{
					result = Marshal.PtrToStringBSTR(intPtr);
				}
			}
			finally
			{
				if (flag)
				{
					rowBinding.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000F4788 File Offset: 0x000F3B88
		private void Value_BSTR(string value)
		{
			this.LengthValue(value.Length * 2);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.SetBstrValue(this.ValueOffset, value);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000F47BC File Offset: 0x000F3BBC
		private byte[] Value_ByRefBYTES()
		{
			byte[] array = null;
			RowBinding rowBinding = this.RowBinding;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				rowBinding.DangerousAddRef(ref flag);
				IntPtr intPtr = rowBinding.ReadIntPtr(this.ValueOffset);
				if (ADP.PtrZero != intPtr)
				{
					array = new byte[this.LengthValue()];
					Marshal.Copy(intPtr, array, 0, array.Length);
				}
			}
			finally
			{
				if (flag)
				{
					rowBinding.DangerousRelease();
				}
			}
			if (array == null)
			{
				return new byte[0];
			}
			return array;
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000F4848 File Offset: 0x000F3C48
		private void Value_ByRefBYTES(byte[] value)
		{
			int num = (this.ValueBindingOffset < value.Length) ? (value.Length - this.ValueBindingOffset) : 0;
			this.LengthValue((0 < this.ValueBindingSize) ? Math.Min(this.ValueBindingSize, num) : num);
			this.StatusValue(DBStatus.S_OK);
			IntPtr intPtr = ADP.PtrZero;
			if (0 < num)
			{
				this._pinnedBuffer = GCHandle.Alloc(value, GCHandleType.Pinned);
				intPtr = this._pinnedBuffer.AddrOfPinnedObject();
				intPtr = ADP.IntPtrOffset(intPtr, this.ValueBindingOffset);
			}
			this.RowBinding.SetByRefValue(this.ValueOffset, intPtr);
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000F48D8 File Offset: 0x000F3CD8
		private string Value_ByRefWSTR()
		{
			string result = "";
			RowBinding rowBinding = this.RowBinding;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				rowBinding.DangerousAddRef(ref flag);
				IntPtr intPtr = rowBinding.ReadIntPtr(this.ValueOffset);
				if (ADP.PtrZero != intPtr)
				{
					int len = this.LengthValue() / 2;
					result = Marshal.PtrToStringUni(intPtr, len);
				}
			}
			finally
			{
				if (flag)
				{
					rowBinding.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000F4958 File Offset: 0x000F3D58
		private void Value_ByRefWSTR(string value)
		{
			int num = (this.ValueBindingOffset < value.Length) ? (value.Length - this.ValueBindingOffset) : 0;
			this.LengthValue(((0 < this.ValueBindingSize) ? Math.Min(this.ValueBindingSize, num) : num) * 2);
			this.StatusValue(DBStatus.S_OK);
			IntPtr intPtr = ADP.PtrZero;
			if (0 < num)
			{
				this._pinnedBuffer = GCHandle.Alloc(value, GCHandleType.Pinned);
				intPtr = this._pinnedBuffer.AddrOfPinnedObject();
				intPtr = ADP.IntPtrOffset(intPtr, this.ValueBindingOffset);
			}
			this.RowBinding.SetByRefValue(this.ValueOffset, intPtr);
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000F49F0 File Offset: 0x000F3DF0
		private void Value_ByRefWSTR(char[] value)
		{
			int num = (this.ValueBindingOffset < value.Length) ? (value.Length - this.ValueBindingOffset) : 0;
			this.LengthValue(((0 < this.ValueBindingSize) ? Math.Min(this.ValueBindingSize, num) : num) * 2);
			this.StatusValue(DBStatus.S_OK);
			IntPtr intPtr = ADP.PtrZero;
			if (0 < num)
			{
				this._pinnedBuffer = GCHandle.Alloc(value, GCHandleType.Pinned);
				intPtr = this._pinnedBuffer.AddrOfPinnedObject();
				intPtr = ADP.IntPtrOffset(intPtr, this.ValueBindingOffset);
			}
			this.RowBinding.SetByRefValue(this.ValueOffset, intPtr);
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000F4A80 File Offset: 0x000F3E80
		private byte[] Value_BYTES()
		{
			int num = Math.Min(this.LengthValue(), this.ColumnBindingMaxLen);
			byte[] array = new byte[num];
			this.RowBinding.ReadBytes(this.ValueOffset, array, 0, num);
			return array;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000F4ABC File Offset: 0x000F3EBC
		private void Value_BYTES(byte[] value)
		{
			int num = (this.ValueBindingOffset < value.Length) ? Math.Min(value.Length - this.ValueBindingOffset, this.ColumnBindingMaxLen) : 0;
			this.LengthValue(num);
			this.StatusValue(DBStatus.S_OK);
			if (0 < num)
			{
				this.RowBinding.WriteBytes(this.ValueOffset, value, this.ValueBindingOffset, num);
			}
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000F4B18 File Offset: 0x000F3F18
		private decimal Value_CY()
		{
			return decimal.FromOACurrency(this.RowBinding.ReadInt64(this.ValueOffset));
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000F4B3C File Offset: 0x000F3F3C
		private void Value_CY(decimal value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteInt64(this.ValueOffset, decimal.ToOACurrency(value));
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000F4B70 File Offset: 0x000F3F70
		private DateTime Value_DATE()
		{
			return DateTime.FromOADate(this.RowBinding.ReadDouble(this.ValueOffset));
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000F4B94 File Offset: 0x000F3F94
		private void Value_DATE(DateTime value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteDouble(this.ValueOffset, value.ToOADate());
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000F4BC8 File Offset: 0x000F3FC8
		private DateTime Value_DBDATE()
		{
			return this.RowBinding.ReadDate(this.ValueOffset);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000F4BE8 File Offset: 0x000F3FE8
		private void Value_DBDATE(DateTime value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteDate(this.ValueOffset, value);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000F4C18 File Offset: 0x000F4018
		private TimeSpan Value_DBTIME()
		{
			return this.RowBinding.ReadTime(this.ValueOffset);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000F4C38 File Offset: 0x000F4038
		private void Value_DBTIME(TimeSpan value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteTime(this.ValueOffset, value);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000F4C68 File Offset: 0x000F4068
		private DateTime Value_DBTIMESTAMP()
		{
			return this.RowBinding.ReadDateTime(this.ValueOffset);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000F4C88 File Offset: 0x000F4088
		private void Value_DBTIMESTAMP(DateTime value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteDateTime(this.ValueOffset, value);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000F4CB8 File Offset: 0x000F40B8
		private decimal Value_DECIMAL()
		{
			int[] array = new int[4];
			this.RowBinding.ReadInt32Array(this.ValueOffset, array, 0, 4);
			return new decimal(array[2], array[3], array[1], (array[0] & int.MinValue) != 0, (byte)((array[0] & 16711680) >> 16));
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000F4D08 File Offset: 0x000F4108
		private void Value_DECIMAL(decimal value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			int[] bits = decimal.GetBits(value);
			int[] source = new int[]
			{
				bits[3],
				bits[2],
				bits[0],
				bits[1]
			};
			this.RowBinding.WriteInt32Array(this.ValueOffset, source, 0, 4);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000F4D60 File Offset: 0x000F4160
		private int Value_ERROR()
		{
			return this.RowBinding.ReadInt32(this.ValueOffset);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000F4D80 File Offset: 0x000F4180
		private void Value_ERROR(int value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteInt32(this.ValueOffset, value);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000F4DB0 File Offset: 0x000F41B0
		private DateTime Value_FILETIME()
		{
			long fileTime = this.RowBinding.ReadInt64(this.ValueOffset);
			return DateTime.FromFileTime(fileTime);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000F4DD8 File Offset: 0x000F41D8
		private void Value_FILETIME(DateTime value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			long value2 = value.ToFileTime();
			this.RowBinding.WriteInt64(this.ValueOffset, value2);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000F4E10 File Offset: 0x000F4210
		internal Guid Value_GUID()
		{
			return this.RowBinding.ReadGuid(this.ValueOffset);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000F4E30 File Offset: 0x000F4230
		private void Value_GUID(Guid value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteGuid(this.ValueOffset, value);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000F4E60 File Offset: 0x000F4260
		internal OleDbDataReader Value_HCHAPTER()
		{
			return this.DataReader().ResetChapter(this.IndexForAccessor, this.IndexWithinAccessor, this.RowBinding, this.ValueOffset);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000F4E90 File Offset: 0x000F4290
		private sbyte Value_I1()
		{
			byte b = this.RowBinding.ReadByte(this.ValueOffset);
			return (sbyte)b;
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000F4EB4 File Offset: 0x000F42B4
		private void Value_I1(sbyte value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteByte(this.ValueOffset, (byte)value);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000F4EE4 File Offset: 0x000F42E4
		internal short Value_I2()
		{
			return this.RowBinding.ReadInt16(this.ValueOffset);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000F4F04 File Offset: 0x000F4304
		private void Value_I2(short value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteInt16(this.ValueOffset, value);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000F4F34 File Offset: 0x000F4334
		private int Value_I4()
		{
			return this.RowBinding.ReadInt32(this.ValueOffset);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000F4F54 File Offset: 0x000F4354
		private void Value_I4(int value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteInt32(this.ValueOffset, value);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000F4F84 File Offset: 0x000F4384
		private long Value_I8()
		{
			return this.RowBinding.ReadInt64(this.ValueOffset);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000F4FA4 File Offset: 0x000F43A4
		private void Value_I8(long value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteInt64(this.ValueOffset, value);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000F4FD4 File Offset: 0x000F43D4
		private object Value_IDISPATCH()
		{
			RowBinding rowBinding = this.RowBinding;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			object objectForIUnknown;
			try
			{
				rowBinding.DangerousAddRef(ref flag);
				IntPtr pUnk = rowBinding.ReadIntPtr(this.ValueOffset);
				objectForIUnknown = Marshal.GetObjectForIUnknown(pUnk);
			}
			finally
			{
				if (flag)
				{
					rowBinding.DangerousRelease();
				}
			}
			return objectForIUnknown;
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000F5034 File Offset: 0x000F4434
		private void Value_IDISPATCH(object value)
		{
			new NamedPermissionSet("FullTrust").Demand();
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			IntPtr idispatchForObject = Marshal.GetIDispatchForObject(value);
			this.RowBinding.WriteIntPtr(this.ValueOffset, idispatchForObject);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000F5078 File Offset: 0x000F4478
		private object Value_IUNKNOWN()
		{
			RowBinding rowBinding = this.RowBinding;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			object objectForIUnknown;
			try
			{
				rowBinding.DangerousAddRef(ref flag);
				IntPtr pUnk = rowBinding.ReadIntPtr(this.ValueOffset);
				objectForIUnknown = Marshal.GetObjectForIUnknown(pUnk);
			}
			finally
			{
				if (flag)
				{
					rowBinding.DangerousRelease();
				}
			}
			return objectForIUnknown;
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000F50D8 File Offset: 0x000F44D8
		private void Value_IUNKNOWN(object value)
		{
			new NamedPermissionSet("FullTrust").Demand();
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(value);
			this.RowBinding.WriteIntPtr(this.ValueOffset, iunknownForObject);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000F511C File Offset: 0x000F451C
		private decimal Value_NUMERIC()
		{
			return this.RowBinding.ReadNumeric(this.ValueOffset);
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000F513C File Offset: 0x000F453C
		private void Value_NUMERIC(decimal value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteNumeric(this.ValueOffset, value, this.ColumnBindingPrecision);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000F5170 File Offset: 0x000F4570
		private float Value_R4()
		{
			return this.RowBinding.ReadSingle(this.ValueOffset);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000F5190 File Offset: 0x000F4590
		private void Value_R4(float value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteSingle(this.ValueOffset, value);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000F51C0 File Offset: 0x000F45C0
		private double Value_R8()
		{
			return this.RowBinding.ReadDouble(this.ValueOffset);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000F51E0 File Offset: 0x000F45E0
		private void Value_R8(double value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteDouble(this.ValueOffset, value);
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000F5210 File Offset: 0x000F4610
		private byte Value_UI1()
		{
			return this.RowBinding.ReadByte(this.ValueOffset);
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000F5230 File Offset: 0x000F4630
		private void Value_UI1(byte value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteByte(this.ValueOffset, value);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000F5260 File Offset: 0x000F4660
		internal ushort Value_UI2()
		{
			return (ushort)this.RowBinding.ReadInt16(this.ValueOffset);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000F5280 File Offset: 0x000F4680
		private void Value_UI2(ushort value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteInt16(this.ValueOffset, (short)value);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000F52B0 File Offset: 0x000F46B0
		internal uint Value_UI4()
		{
			return (uint)this.RowBinding.ReadInt32(this.ValueOffset);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000F52D0 File Offset: 0x000F46D0
		private void Value_UI4(uint value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteInt32(this.ValueOffset, (int)value);
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000F5300 File Offset: 0x000F4700
		internal ulong Value_UI8()
		{
			return (ulong)this.RowBinding.ReadInt64(this.ValueOffset);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000F5320 File Offset: 0x000F4720
		private void Value_UI8(ulong value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.WriteInt64(this.ValueOffset, (long)value);
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000F5350 File Offset: 0x000F4750
		private string Value_WSTR()
		{
			int num = Math.Min(this.LengthValue(), this.ColumnBindingMaxLen - 2);
			return this.RowBinding.PtrToStringUni(this.ValueOffset, num / 2);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000F5388 File Offset: 0x000F4788
		private void Value_WSTR(string value)
		{
			int num = (this.ValueBindingOffset < value.Length) ? Math.Min(value.Length - this.ValueBindingOffset, (this.ColumnBindingMaxLen - 2) / 2) : 0;
			this.LengthValue(num * 2);
			this.StatusValue(DBStatus.S_OK);
			if (0 < num)
			{
				char[] source = value.ToCharArray(this.ValueBindingOffset, num);
				this.RowBinding.WriteCharArray(this.ValueOffset, source, this.ValueBindingOffset, num);
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000F5400 File Offset: 0x000F4800
		private void Value_WSTR(char[] value)
		{
			int num = (this.ValueBindingOffset < value.Length) ? Math.Min(value.Length - this.ValueBindingOffset, (this.ColumnBindingMaxLen - 2) / 2) : 0;
			this.LengthValue(num * 2);
			this.StatusValue(DBStatus.S_OK);
			if (0 < num)
			{
				this.RowBinding.WriteCharArray(this.ValueOffset, value, this.ValueBindingOffset, num);
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000F5464 File Offset: 0x000F4864
		private object Value_VARIANT()
		{
			return this.RowBinding.GetVariantValue(this.ValueOffset);
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000F5484 File Offset: 0x000F4884
		private void Value_VARIANT(object value)
		{
			this.LengthValue(0);
			this.StatusValue(DBStatus.S_OK);
			this.RowBinding.SetVariantValue(this.ValueOffset, value);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000F54B4 File Offset: 0x000F48B4
		internal bool ValueBoolean()
		{
			if (this.StatusValue() == DBStatus.S_OK)
			{
				short dbType = this.DbType;
				bool result;
				if (dbType != 11)
				{
					if (dbType != 12)
					{
						throw ODB.ConversionRequired();
					}
					result = (bool)this.ValueVariant();
				}
				else
				{
					result = this.Value_BOOL();
				}
				return result;
			}
			throw this.CheckTypeValueStatusValue(typeof(bool));
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000F5510 File Offset: 0x000F4910
		internal byte[] ValueByteArray()
		{
			byte[] array = (byte[])this._value;
			if (array == null)
			{
				DBStatus dbstatus = this.StatusValue();
				if (dbstatus != DBStatus.S_OK)
				{
					if (dbstatus != DBStatus.S_TRUNCATED)
					{
						throw this.CheckTypeValueStatusValue(typeof(byte[]));
					}
					short dbType = this.DbType;
					if (dbType != 128)
					{
						if (dbType != 16512)
						{
							throw ODB.ConversionRequired();
						}
						array = this.Value_ByRefBYTES();
					}
					else
					{
						array = this.Value_BYTES();
					}
				}
				else
				{
					short dbType2 = this.DbType;
					if (dbType2 != 12)
					{
						if (dbType2 != 128)
						{
							if (dbType2 != 16512)
							{
								throw ODB.ConversionRequired();
							}
							array = this.Value_ByRefBYTES();
						}
						else
						{
							array = this.Value_BYTES();
						}
					}
					else
					{
						array = (byte[])this.ValueVariant();
					}
				}
				this._value = array;
			}
			return array;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000F55D0 File Offset: 0x000F49D0
		internal byte ValueByte()
		{
			if (this.StatusValue() == DBStatus.S_OK)
			{
				short dbType = this.DbType;
				byte result;
				if (dbType != 12)
				{
					if (dbType != 17)
					{
						throw ODB.ConversionRequired();
					}
					result = this.Value_UI1();
				}
				else
				{
					result = (byte)this.ValueVariant();
				}
				return result;
			}
			throw this.CheckTypeValueStatusValue(typeof(byte));
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000F5628 File Offset: 0x000F4A28
		internal OleDbDataReader ValueChapter()
		{
			OleDbDataReader oleDbDataReader = (OleDbDataReader)this._value;
			if (oleDbDataReader == null)
			{
				if (this.StatusValue() != DBStatus.S_OK)
				{
					throw this.CheckTypeValueStatusValue(typeof(string));
				}
				short dbType = this.DbType;
				if (dbType != 136)
				{
					throw ODB.ConversionRequired();
				}
				oleDbDataReader = this.Value_HCHAPTER();
				this._value = oleDbDataReader;
			}
			return oleDbDataReader;
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000F5688 File Offset: 0x000F4A88
		internal DateTime ValueDateTime()
		{
			if (this.StatusValue() == DBStatus.S_OK)
			{
				short dbType = this.DbType;
				if (dbType <= 12)
				{
					if (dbType == 7)
					{
						return this.Value_DATE();
					}
					if (dbType == 12)
					{
						return (DateTime)this.ValueVariant();
					}
				}
				else
				{
					if (dbType == 64)
					{
						return this.Value_FILETIME();
					}
					if (dbType == 133)
					{
						return this.Value_DBDATE();
					}
					if (dbType == 135)
					{
						return this.Value_DBTIMESTAMP();
					}
				}
				throw ODB.ConversionRequired();
			}
			throw this.CheckTypeValueStatusValue(typeof(short));
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000F5718 File Offset: 0x000F4B18
		internal decimal ValueDecimal()
		{
			if (this.StatusValue() == DBStatus.S_OK)
			{
				short dbType = this.DbType;
				if (dbType <= 12)
				{
					if (dbType == 6)
					{
						return this.Value_CY();
					}
					if (dbType == 12)
					{
						return (decimal)this.ValueVariant();
					}
				}
				else
				{
					if (dbType == 14)
					{
						return this.Value_DECIMAL();
					}
					if (dbType == 21)
					{
						return this.Value_UI8();
					}
					if (dbType == 131)
					{
						return this.Value_NUMERIC();
					}
				}
				throw ODB.ConversionRequired();
			}
			throw this.CheckTypeValueStatusValue(typeof(short));
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000F57AC File Offset: 0x000F4BAC
		internal Guid ValueGuid()
		{
			if (this.StatusValue() != DBStatus.S_OK)
			{
				throw this.CheckTypeValueStatusValue(typeof(short));
			}
			short dbType = this.DbType;
			if (dbType == 72)
			{
				return this.Value_GUID();
			}
			throw ODB.ConversionRequired();
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000F57F0 File Offset: 0x000F4BF0
		internal short ValueInt16()
		{
			if (this.StatusValue() == DBStatus.S_OK)
			{
				short dbType = this.DbType;
				short result;
				if (dbType != 2)
				{
					if (dbType != 12)
					{
						if (dbType != 16)
						{
							throw ODB.ConversionRequired();
						}
						result = (short)this.Value_I1();
					}
					else
					{
						object obj = this.ValueVariant();
						if (obj is sbyte)
						{
							result = (short)((sbyte)obj);
						}
						else
						{
							result = (short)obj;
						}
					}
				}
				else
				{
					result = this.Value_I2();
				}
				return result;
			}
			throw this.CheckTypeValueStatusValue(typeof(short));
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000F586C File Offset: 0x000F4C6C
		internal int ValueInt32()
		{
			if (this.StatusValue() == DBStatus.S_OK)
			{
				short dbType = this.DbType;
				int result;
				if (dbType != 3)
				{
					if (dbType != 12)
					{
						if (dbType != 18)
						{
							throw ODB.ConversionRequired();
						}
						result = (int)this.Value_UI2();
					}
					else
					{
						object obj = this.ValueVariant();
						if (obj is ushort)
						{
							result = (int)((ushort)obj);
						}
						else
						{
							result = (int)obj;
						}
					}
				}
				else
				{
					result = this.Value_I4();
				}
				return result;
			}
			throw this.CheckTypeValueStatusValue(typeof(int));
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000F58E8 File Offset: 0x000F4CE8
		internal long ValueInt64()
		{
			if (this.StatusValue() == DBStatus.S_OK)
			{
				short dbType = this.DbType;
				long result;
				if (dbType != 12)
				{
					if (dbType != 19)
					{
						if (dbType != 20)
						{
							throw ODB.ConversionRequired();
						}
						result = this.Value_I8();
					}
					else
					{
						result = (long)((ulong)this.Value_UI4());
					}
				}
				else
				{
					object obj = this.ValueVariant();
					if (obj is uint)
					{
						result = (long)((ulong)((uint)obj));
					}
					else
					{
						result = (long)obj;
					}
				}
				return result;
			}
			throw this.CheckTypeValueStatusValue(typeof(long));
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000F5964 File Offset: 0x000F4D64
		internal float ValueSingle()
		{
			if (this.StatusValue() == DBStatus.S_OK)
			{
				short dbType = this.DbType;
				float result;
				if (dbType != 4)
				{
					if (dbType != 12)
					{
						throw ODB.ConversionRequired();
					}
					result = (float)this.ValueVariant();
				}
				else
				{
					result = this.Value_R4();
				}
				return result;
			}
			throw this.CheckTypeValueStatusValue(typeof(float));
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000F59BC File Offset: 0x000F4DBC
		internal double ValueDouble()
		{
			if (this.StatusValue() == DBStatus.S_OK)
			{
				short dbType = this.DbType;
				double result;
				if (dbType != 5)
				{
					if (dbType != 12)
					{
						throw ODB.ConversionRequired();
					}
					result = (double)this.ValueVariant();
				}
				else
				{
					result = this.Value_R8();
				}
				return result;
			}
			throw this.CheckTypeValueStatusValue(typeof(double));
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000F5A14 File Offset: 0x000F4E14
		internal string ValueString()
		{
			string text = (string)this._value;
			if (text == null)
			{
				DBStatus dbstatus = this.StatusValue();
				if (dbstatus == DBStatus.S_OK)
				{
					short dbType = this.DbType;
					if (dbType <= 12)
					{
						if (dbType == 8)
						{
							text = this.Value_BSTR();
							goto IL_BF;
						}
						if (dbType == 12)
						{
							text = (string)this.ValueVariant();
							goto IL_BF;
						}
					}
					else
					{
						if (dbType == 130)
						{
							text = this.Value_WSTR();
							goto IL_BF;
						}
						if (dbType == 16514)
						{
							text = this.Value_ByRefWSTR();
							goto IL_BF;
						}
					}
					throw ODB.ConversionRequired();
				}
				if (dbstatus != DBStatus.S_TRUNCATED)
				{
					throw this.CheckTypeValueStatusValue(typeof(string));
				}
				short dbType2 = this.DbType;
				if (dbType2 != 130)
				{
					if (dbType2 != 16514)
					{
						throw ODB.ConversionRequired();
					}
					text = this.Value_ByRefWSTR();
				}
				else
				{
					text = this.Value_WSTR();
				}
				IL_BF:
				this._value = text;
			}
			return text;
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000F5AE8 File Offset: 0x000F4EE8
		private object ValueVariant()
		{
			object obj = this._value;
			if (obj == null)
			{
				obj = this.Value_VARIANT();
				this._value = obj;
			}
			return obj;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000F5B10 File Offset: 0x000F4F10
		private Exception CheckTypeValueStatusValue()
		{
			return this.CheckTypeValueStatusValue(this.ExpectedType);
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000F5B2C File Offset: 0x000F4F2C
		private Exception CheckTypeValueStatusValue(Type expectedType)
		{
			switch (this.StatusValue())
			{
			case DBStatus.S_OK:
			case DBStatus.E_CANTCONVERTVALUE:
			case DBStatus.S_TRUNCATED:
				return ODB.CantConvertValue();
			case DBStatus.E_BADACCESSOR:
				return ODB.BadAccessor();
			case DBStatus.S_ISNULL:
				return ADP.InvalidCast();
			case DBStatus.E_SIGNMISMATCH:
				return ODB.SignMismatch(expectedType);
			case DBStatus.E_DATAOVERFLOW:
				return ODB.DataOverflow(expectedType);
			case DBStatus.E_CANTCREATE:
				return ODB.CantCreate(expectedType);
			case DBStatus.E_UNAVAILABLE:
				return ODB.Unavailable(expectedType);
			default:
				return ODB.UnexpectedStatusValue(this.StatusValue());
			}
		}

		// Token: 0x04001560 RID: 5472
		private readonly OleDbDataReader _dataReader;

		// Token: 0x04001561 RID: 5473
		private readonly RowBinding _rowbinding;

		// Token: 0x04001562 RID: 5474
		private readonly Bindings _bindings;

		// Token: 0x04001563 RID: 5475
		private readonly OleDbParameter _parameter;

		// Token: 0x04001564 RID: 5476
		private readonly int _parameterChangeID;

		// Token: 0x04001565 RID: 5477
		private readonly int _offsetStatus;

		// Token: 0x04001566 RID: 5478
		private readonly int _offsetLength;

		// Token: 0x04001567 RID: 5479
		private readonly int _offsetValue;

		// Token: 0x04001568 RID: 5480
		private readonly int _ordinal;

		// Token: 0x04001569 RID: 5481
		private readonly int _maxLen;

		// Token: 0x0400156A RID: 5482
		private readonly short _wType;

		// Token: 0x0400156B RID: 5483
		private readonly byte _precision;

		// Token: 0x0400156C RID: 5484
		private readonly int _index;

		// Token: 0x0400156D RID: 5485
		private readonly int _indexForAccessor;

		// Token: 0x0400156E RID: 5486
		private readonly int _indexWithinAccessor;

		// Token: 0x0400156F RID: 5487
		private readonly bool _ifIRowsetElseIRow;

		// Token: 0x04001570 RID: 5488
		private int _valueBindingOffset;

		// Token: 0x04001571 RID: 5489
		private int _valueBindingSize;

		// Token: 0x04001572 RID: 5490
		internal StringMemHandle _sptr;

		// Token: 0x04001573 RID: 5491
		private GCHandle _pinnedBuffer;

		// Token: 0x04001574 RID: 5492
		private object _value;
	}
}
