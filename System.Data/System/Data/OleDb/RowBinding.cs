using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200025E RID: 606
	internal sealed class RowBinding : DbBuffer
	{
		// Token: 0x060020A5 RID: 8357 RVA: 0x00281878 File Offset: 0x00280C78
		internal static RowBinding CreateBuffer(int bindingCount, int databuffersize, bool needToReset)
		{
			int num = RowBinding.AlignDataSize(bindingCount * ODB.SizeOf_tagDBBINDING);
			int length = RowBinding.AlignDataSize(num + databuffersize) + 8;
			return new RowBinding(bindingCount, num, databuffersize, length, needToReset);
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x002818A8 File Offset: 0x00280CA8
		private RowBinding(int bindingCount, int headerLength, int dataLength, int length, bool needToReset) : base(length)
		{
			this._bindingCount = bindingCount;
			this._headerLength = headerLength;
			this._dataLength = dataLength;
			this._emptyStringOffset = length - 8;
			this._needToReset = needToReset;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x002818E8 File Offset: 0x00280CE8
		internal void StartDataBlock()
		{
			if (this._haveData)
			{
				this.ResetValues();
			}
			this._haveData = true;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x00281918 File Offset: 0x00280D18
		internal int BindingCount()
		{
			return this._bindingCount;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x00281938 File Offset: 0x00280D38
		internal IntPtr DangerousGetAccessorHandle()
		{
			return this._accessorHandle;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x00281958 File Offset: 0x00280D58
		internal IntPtr DangerousGetDataPtr()
		{
			return ADP.IntPtrOffset(base.DangerousGetHandle(), this._headerLength);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00281978 File Offset: 0x00280D78
		internal IntPtr DangerousGetDataPtr(int valueOffset)
		{
			return ADP.IntPtrOffset(base.DangerousGetHandle(), valueOffset);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00281998 File Offset: 0x00280D98
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

		// Token: 0x060020AD RID: 8365 RVA: 0x00281A38 File Offset: 0x00280E38
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

		// Token: 0x060020AE RID: 8366 RVA: 0x00281AF8 File Offset: 0x00280EF8
		internal static int AlignDataSize(int value)
		{
			return Math.Max(8, value + 7 & -8);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00281B18 File Offset: 0x00280F18
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

		// Token: 0x060020B0 RID: 8368 RVA: 0x00281B88 File Offset: 0x00280F88
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

		// Token: 0x060020B1 RID: 8369 RVA: 0x00281C28 File Offset: 0x00281028
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

		// Token: 0x060020B2 RID: 8370 RVA: 0x00281CD8 File Offset: 0x002810D8
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

		// Token: 0x060020B3 RID: 8371 RVA: 0x00281D88 File Offset: 0x00281188
		internal void CloseFromConnection()
		{
			this._iaccessor = null;
			this._accessorHandle = ODB.DB_INVALID_HACCESSOR;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x00281DA8 File Offset: 0x002811A8
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

		// Token: 0x060020B5 RID: 8373 RVA: 0x00281E08 File Offset: 0x00281208
		internal void ResetValues()
		{
			if (this._needToReset && this._haveData)
			{
				lock (this)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						base.DangerousAddRef(ref flag);
						this.ResetValues(base.DangerousGetHandle(), this._iaccessor);
					}
					finally
					{
						if (flag)
						{
							base.DangerousRelease();
						}
					}
					return;
				}
			}
			this._haveData = false;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00281EA8 File Offset: 0x002812A8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void ResetValues(IntPtr buffer, object iaccessor)
		{
			for (int i = 0; i < this._bindingCount; i++)
			{
				IntPtr ptr = ADP.IntPtrOffset(buffer, i * ODB.SizeOf_tagDBBINDING);
				int valueOffset = this._headerLength + Marshal.ReadIntPtr(ptr, ODB.OffsetOf_tagDBBINDING_obValue).ToInt32();
				short num = Marshal.ReadInt16(ptr, ODB.OffsetOf_tagDBBINDING_wType);
				short num2 = num;
				if (num2 <= 12)
				{
					if (num2 != 8)
					{
						if (num2 == 12)
						{
							RowBinding.FreeVariant(buffer, valueOffset);
						}
					}
					else
					{
						RowBinding.FreeBstr(buffer, valueOffset);
					}
				}
				else
				{
					switch (num2)
					{
					case 136:
						if (iaccessor != null)
						{
							RowBinding.FreeChapter(buffer, valueOffset, iaccessor);
						}
						break;
					case 137:
						break;
					case 138:
						RowBinding.FreePropVariant(buffer, valueOffset);
						break;
					default:
						switch (num2)
						{
						case 16512:
						case 16514:
							RowBinding.FreeCoTaskMem(buffer, valueOffset);
							break;
						}
						break;
					}
				}
			}
			this._haveData = false;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00281F88 File Offset: 0x00281388
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

		// Token: 0x060020B8 RID: 8376 RVA: 0x00281FE8 File Offset: 0x002813E8
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

		// Token: 0x060020B9 RID: 8377 RVA: 0x00282088 File Offset: 0x00281488
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

		// Token: 0x060020BA RID: 8378 RVA: 0x00282118 File Offset: 0x00281518
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

		// Token: 0x060020BB RID: 8379 RVA: 0x00282198 File Offset: 0x00281598
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

		// Token: 0x060020BC RID: 8380 RVA: 0x00282218 File Offset: 0x00281618
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

		// Token: 0x060020BD RID: 8381 RVA: 0x00282278 File Offset: 0x00281678
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

		// Token: 0x04001547 RID: 5447
		private readonly int _bindingCount;

		// Token: 0x04001548 RID: 5448
		private readonly int _headerLength;

		// Token: 0x04001549 RID: 5449
		private readonly int _dataLength;

		// Token: 0x0400154A RID: 5450
		private readonly int _emptyStringOffset;

		// Token: 0x0400154B RID: 5451
		private UnsafeNativeMethods.IAccessor _iaccessor;

		// Token: 0x0400154C RID: 5452
		private IntPtr _accessorHandle;

		// Token: 0x0400154D RID: 5453
		private readonly bool _needToReset;

		// Token: 0x0400154E RID: 5454
		private bool _haveData;
	}
}
