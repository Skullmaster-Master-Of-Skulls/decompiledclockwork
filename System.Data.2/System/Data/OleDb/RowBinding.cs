using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000283 RID: 643
	internal sealed class RowBinding : DbBuffer
	{
		// Token: 0x060026E4 RID: 9956 RVA: 0x00107AC0 File Offset: 0x00106EC0
		internal static RowBinding CreateBuffer(int bindingCount, int databuffersize, bool needToReset)
		{
			int num = RowBinding.AlignDataSize(bindingCount * ODB.SizeOf_tagDBBINDING);
			int length = RowBinding.AlignDataSize(num + databuffersize) + 8;
			return new RowBinding(bindingCount, num, databuffersize, length, needToReset);
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x00107AF0 File Offset: 0x00106EF0
		private RowBinding(int bindingCount, int headerLength, int dataLength, int length, bool needToReset) : base(length)
		{
			this._bindingCount = bindingCount;
			this._headerLength = headerLength;
			this._dataLength = dataLength;
			this._emptyStringOffset = length - 8;
			this._needToReset = needToReset;
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x00107B2C File Offset: 0x00106F2C
		internal void StartDataBlock()
		{
			if (this._haveData)
			{
				this.ResetValues();
			}
			this._haveData = true;
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x00107B50 File Offset: 0x00106F50
		internal int BindingCount()
		{
			return this._bindingCount;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x00107B64 File Offset: 0x00106F64
		internal IntPtr DangerousGetAccessorHandle()
		{
			return this._accessorHandle;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x00107B78 File Offset: 0x00106F78
		internal IntPtr DangerousGetDataPtr()
		{
			return ADP.IntPtrOffset(base.DangerousGetHandle(), this._headerLength);
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x00107B98 File Offset: 0x00106F98
		internal IntPtr DangerousGetDataPtr(int valueOffset)
		{
			return ADP.IntPtrOffset(base.DangerousGetHandle(), valueOffset);
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x00107BB4 File Offset: 0x00106FB4
		internal OleDbHResult CreateAccessor(UnsafeNativeMethods.IAccessor iaccessor, int flags, ColumnBinding[] bindings)
		{
			int[] array = new int[this.BindingCount()];
			this._iaccessor = iaccessor;
			Bid.Trace("<oledb.IAccessor.CreateAccessor|API|OLEDB>\n");
			OleDbHResult oleDbHResult = iaccessor.CreateAccessor(flags, (IntPtr)array.Length, this, (IntPtr)this._dataLength, out this._accessorHandle, array);
			Bid.Trace("<oledb.IAccessor.CreateAccessor|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != 0)
				{
					if (4 == flags)
					{
						throw ODB.BadStatus_ParamAcc(bindings[i].ColumnBindingOrdinal, (DBBindStatus)array[i]);
					}
					if (2 == flags)
					{
						throw ODB.BadStatusRowAccessor(bindings[i].ColumnBindingOrdinal, (DBBindStatus)array[i]);
					}
				}
			}
			return oleDbHResult;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x00107C4C File Offset: 0x0010704C
		internal ColumnBinding[] SetBindings(OleDbDataReader dataReader, Bindings bindings, int indexStart, int indexForAccessor, OleDbParameter[] parameters, tagDBBINDING[] dbbindings, bool ifIRowsetElseIRow)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr pbase = base.DangerousGetHandle();
				for (int i = 0; i < dbbindings.Length; i++)
				{
					IntPtr ptr = ADP.IntPtrOffset(pbase, i * ODB.SizeOf_tagDBBINDING);
					Marshal.StructureToPtr(dbbindings[i], ptr, false);
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			ColumnBinding[] array = new ColumnBinding[dbbindings.Length];
			for (int j = 0; j < array.Length; j++)
			{
				int num = indexStart + j;
				OleDbParameter parameter = (parameters != null) ? parameters[num] : null;
				array[j] = new ColumnBinding(dataReader, num, indexForAccessor, j, parameter, this, bindings, dbbindings[j], this._headerLength, ifIRowsetElseIRow);
			}
			return array;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x00107D0C File Offset: 0x0010710C
		internal static int AlignDataSize(int value)
		{
			return Math.Max(8, value + 7 & -8);
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x00107D28 File Offset: 0x00107128
		internal object GetVariantValue(int offset)
		{
			object obj = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr pSrcNativeVariant = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				obj = Marshal.GetObjectForNativeVariant(pSrcNativeVariant);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			if (obj == null)
			{
				return DBNull.Value;
			}
			return obj;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x00107D8C File Offset: 0x0010718C
		internal void SetVariantValue(int offset, object value)
		{
			IntPtr intPtr = ADP.PtrZero;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Marshal.GetNativeVariantForObject(value, intPtr);
				}
				finally
				{
					NativeOledbWrapper.MemoryCopy(ADP.IntPtrOffset(intPtr, ODB.SizeOf_Variant), intPtr, ODB.SizeOf_Variant);
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x00107E20 File Offset: 0x00107220
		internal void SetBstrValue(int offset, string value)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IntPtr intPtr;
			try
			{
				base.DangerousAddRef(ref flag);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = SafeNativeMethods.SysAllocStringLen(value, value.Length);
					Marshal.WriteIntPtr(this.handle, offset, intPtr);
					Marshal.WriteIntPtr(this.handle, offset + ADP.PtrSize, intPtr);
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			if (IntPtr.Zero == intPtr)
			{
				throw new OutOfMemoryException();
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00107EC4 File Offset: 0x001072C4
		internal void SetByRefValue(int offset, IntPtr pinnedValue)
		{
			if (ADP.PtrZero == pinnedValue)
			{
				pinnedValue = ADP.IntPtrOffset(this.handle, this._emptyStringOffset);
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					Marshal.WriteIntPtr(this.handle, offset, pinnedValue);
					Marshal.WriteIntPtr(this.handle, offset + ADP.PtrSize, pinnedValue);
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x00107F68 File Offset: 0x00107368
		internal void CloseFromConnection()
		{
			this._iaccessor = null;
			this._accessorHandle = ODB.DB_INVALID_HACCESSOR;
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x00107F88 File Offset: 0x00107388
		internal new void Dispose()
		{
			this.ResetValues();
			UnsafeNativeMethods.IAccessor iaccessor = this._iaccessor;
			IntPtr accessorHandle = this._accessorHandle;
			this._iaccessor = null;
			this._accessorHandle = ODB.DB_INVALID_HACCESSOR;
			if (ODB.DB_INVALID_HACCESSOR != accessorHandle && iaccessor != null)
			{
				int num;
				OleDbHResult oleDbHResult = iaccessor.ReleaseAccessor(accessorHandle, out num);
				if (oleDbHResult < OleDbHResult.S_OK)
				{
					SafeNativeMethods.Wrapper.ClearErrorInfo();
				}
			}
			base.Dispose();
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x00107FE4 File Offset: 0x001073E4
		internal void ResetValues()
		{
			if (this._needToReset && this._haveData)
			{
				lock (this)
				{
					bool flag2 = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						base.DangerousAddRef(ref flag2);
						this.ResetValues(base.DangerousGetHandle(), this._iaccessor);
						return;
					}
					finally
					{
						if (flag2)
						{
							base.DangerousRelease();
						}
					}
				}
			}
			this._haveData = false;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x00108084 File Offset: 0x00107484
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void ResetValues(IntPtr buffer, object iaccessor)
		{
			for (int i = 0; i < this._bindingCount; i++)
			{
				IntPtr ptr = ADP.IntPtrOffset(buffer, i * ODB.SizeOf_tagDBBINDING);
				int valueOffset = this._headerLength + Marshal.ReadIntPtr(ptr, ODB.OffsetOf_tagDBBINDING_obValue).ToInt32();
				short num = Marshal.ReadInt16(ptr, ODB.OffsetOf_tagDBBINDING_wType);
				if (num <= 136)
				{
					if (num != 8)
					{
						if (num != 12)
						{
							if (num == 136)
							{
								if (iaccessor != null)
								{
									RowBinding.FreeChapter(buffer, valueOffset, iaccessor);
								}
							}
						}
						else
						{
							RowBinding.FreeVariant(buffer, valueOffset);
						}
					}
					else
					{
						RowBinding.FreeBstr(buffer, valueOffset);
					}
				}
				else if (num != 138)
				{
					if (num == 16512 || num == 16514)
					{
						RowBinding.FreeCoTaskMem(buffer, valueOffset);
					}
				}
				else
				{
					RowBinding.FreePropVariant(buffer, valueOffset);
				}
			}
			this._haveData = false;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x00108148 File Offset: 0x00107548
		private static void FreeChapter(IntPtr buffer, int valueOffset, object iaccessor)
		{
			UnsafeNativeMethods.IChapteredRowset chapteredRowset = iaccessor as UnsafeNativeMethods.IChapteredRowset;
			IntPtr intPtr = SafeNativeMethods.InterlockedExchangePointer(ADP.IntPtrOffset(buffer, valueOffset), ADP.PtrZero);
			if (ODB.DB_NULL_HCHAPTER != intPtr)
			{
				Bid.Trace("<oledb.IChapteredRowset.ReleaseChapter|API|OLEDB> Chapter=%Id\n", intPtr);
				int a2;
				OleDbHResult a = chapteredRowset.ReleaseChapter(intPtr, out a2);
				Bid.Trace("<oledb.IChapteredRowset.ReleaseChapter|API|OLEDB|RET> %08X{HRESULT}, RefCount=%d\n", a, a2);
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0010819C File Offset: 0x0010759C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void FreeBstr(IntPtr buffer, int valueOffset)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				IntPtr intPtr = Marshal.ReadIntPtr(buffer, valueOffset);
				IntPtr intPtr2 = Marshal.ReadIntPtr(buffer, valueOffset + ADP.PtrSize);
				if (ADP.PtrZero != intPtr && intPtr != intPtr2)
				{
					SafeNativeMethods.SysFreeString(intPtr);
				}
				if (ADP.PtrZero != intPtr2)
				{
					SafeNativeMethods.SysFreeString(intPtr2);
				}
				Marshal.WriteIntPtr(buffer, valueOffset, ADP.PtrZero);
				Marshal.WriteIntPtr(buffer, valueOffset + ADP.PtrSize, ADP.PtrZero);
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x00108230 File Offset: 0x00107630
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void FreeCoTaskMem(IntPtr buffer, int valueOffset)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				IntPtr intPtr = Marshal.ReadIntPtr(buffer, valueOffset);
				IntPtr value = Marshal.ReadIntPtr(buffer, valueOffset + ADP.PtrSize);
				if (ADP.PtrZero != intPtr && intPtr != value)
				{
					SafeNativeMethods.CoTaskMemFree(intPtr);
				}
				Marshal.WriteIntPtr(buffer, valueOffset, ADP.PtrZero);
				Marshal.WriteIntPtr(buffer, valueOffset + ADP.PtrSize, ADP.PtrZero);
			}
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x001082B4 File Offset: 0x001076B4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void FreeVariant(IntPtr buffer, int valueOffset)
		{
			IntPtr intPtr = ADP.IntPtrOffset(buffer, valueOffset);
			IntPtr intPtr2 = ADP.IntPtrOffset(buffer, valueOffset + ODB.SizeOf_Variant);
			bool flag = NativeOledbWrapper.MemoryCompare(intPtr, intPtr2, ODB.SizeOf_Variant);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				SafeNativeMethods.VariantClear(intPtr);
				if (flag)
				{
					SafeNativeMethods.VariantClear(intPtr2);
				}
				else
				{
					SafeNativeMethods.ZeroMemory(intPtr2, (IntPtr)ODB.SizeOf_Variant);
				}
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0010832C File Offset: 0x0010772C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void FreePropVariant(IntPtr buffer, int valueOffset)
		{
			IntPtr intPtr = ADP.IntPtrOffset(buffer, valueOffset);
			IntPtr intPtr2 = ADP.IntPtrOffset(buffer, valueOffset + NativeOledbWrapper.SizeOfPROPVARIANT);
			bool flag = NativeOledbWrapper.MemoryCompare(intPtr, intPtr2, NativeOledbWrapper.SizeOfPROPVARIANT);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				SafeNativeMethods.PropVariantClear(intPtr);
				if (flag)
				{
					SafeNativeMethods.PropVariantClear(intPtr2);
				}
				else
				{
					SafeNativeMethods.ZeroMemory(intPtr2, (IntPtr)NativeOledbWrapper.SizeOfPROPVARIANT);
				}
			}
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x001083A4 File Offset: 0x001077A4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal IntPtr InterlockedExchangePointer(int offset)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IntPtr result;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr lpAddress = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				result = SafeNativeMethods.InterlockedExchangePointer(lpAddress, IntPtr.Zero);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x00108404 File Offset: 0x00107804
		protected override bool ReleaseHandle()
		{
			this._iaccessor = null;
			if (this._needToReset && this._haveData)
			{
				IntPtr handle = this.handle;
				if (IntPtr.Zero != handle)
				{
					this.ResetValues(handle, null);
				}
			}
			return base.ReleaseHandle();
		}

		// Token: 0x040019E6 RID: 6630
		private readonly int _bindingCount;

		// Token: 0x040019E7 RID: 6631
		private readonly int _headerLength;

		// Token: 0x040019E8 RID: 6632
		private readonly int _dataLength;

		// Token: 0x040019E9 RID: 6633
		private readonly int _emptyStringOffset;

		// Token: 0x040019EA RID: 6634
		private UnsafeNativeMethods.IAccessor _iaccessor;

		// Token: 0x040019EB RID: 6635
		private IntPtr _accessorHandle;

		// Token: 0x040019EC RID: 6636
		private readonly bool _needToReset;

		// Token: 0x040019ED RID: 6637
		private bool _haveData;
	}
}
