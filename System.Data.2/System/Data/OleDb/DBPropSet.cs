using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000240 RID: 576
	internal sealed class DBPropSet : SafeHandle
	{
		// Token: 0x060023D0 RID: 9168 RVA: 0x000F6354 File Offset: 0x000F5754
		private DBPropSet() : base(IntPtr.Zero, true)
		{
			this.propertySetCount = 0;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000F6374 File Offset: 0x000F5774
		internal DBPropSet(int propertysetCount) : this()
		{
			this.propertySetCount = propertysetCount;
			IntPtr intPtr = (IntPtr)(propertysetCount * ODB.SizeOf_tagDBPROPSET);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.handle = SafeNativeMethods.CoTaskMemAlloc(intPtr);
				if (ADP.PtrZero != this.handle)
				{
					SafeNativeMethods.ZeroMemory(this.handle, intPtr);
				}
			}
			if (ADP.PtrZero == this.handle)
			{
				throw new OutOfMemoryException();
			}
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000F6404 File Offset: 0x000F5804
		internal DBPropSet(UnsafeNativeMethods.IDBProperties properties, PropertyIDSet propidset, out OleDbHResult hr) : this()
		{
			int cPropertyIDSets = 0;
			if (propidset != null)
			{
				cPropertyIDSets = propidset.Count;
			}
			Bid.Trace("<oledb.IDBProperties.GetProperties|API|OLEDB>\n");
			hr = properties.GetProperties(cPropertyIDSets, propidset, out this.propertySetCount, out this.handle);
			Bid.Trace("<oledb.IDBProperties.GetProperties|API|OLEDB|RET> %08X{HRESULT}\n", hr);
			if (hr < OleDbHResult.S_OK)
			{
				this.SetLastErrorInfo(hr);
			}
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000F645C File Offset: 0x000F585C
		internal DBPropSet(UnsafeNativeMethods.IRowsetInfo properties, PropertyIDSet propidset, out OleDbHResult hr) : this()
		{
			int cPropertyIDSets = 0;
			if (propidset != null)
			{
				cPropertyIDSets = propidset.Count;
			}
			Bid.Trace("<oledb.IRowsetInfo.GetProperties|API|OLEDB>\n");
			hr = properties.GetProperties(cPropertyIDSets, propidset, out this.propertySetCount, out this.handle);
			Bid.Trace("<oledb.IRowsetInfo.GetProperties|API|OLEDB|RET> %08X{HRESULT}\n", hr);
			if (hr < OleDbHResult.S_OK)
			{
				this.SetLastErrorInfo(hr);
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000F64B4 File Offset: 0x000F58B4
		internal DBPropSet(UnsafeNativeMethods.ICommandProperties properties, PropertyIDSet propidset, out OleDbHResult hr) : this()
		{
			int cPropertyIDSets = 0;
			if (propidset != null)
			{
				cPropertyIDSets = propidset.Count;
			}
			Bid.Trace("<oledb.ICommandProperties.GetProperties|API|OLEDB>\n");
			hr = properties.GetProperties(cPropertyIDSets, propidset, out this.propertySetCount, out this.handle);
			Bid.Trace("<oledb.ICommandProperties.GetProperties|API|OLEDB|RET> %08X{HRESULT}\n", hr);
			if (hr < OleDbHResult.S_OK)
			{
				this.SetLastErrorInfo(hr);
			}
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000F650C File Offset: 0x000F590C
		private void SetLastErrorInfo(OleDbHResult lastErrorHr)
		{
			UnsafeNativeMethods.IErrorInfo errorInfo = null;
			string empty = string.Empty;
			if (UnsafeNativeMethods.GetErrorInfo(0, out errorInfo) == OleDbHResult.S_OK && errorInfo != null)
			{
				ODB.GetErrorDescription(errorInfo, lastErrorHr, out empty);
			}
			this.lastErrorFromProvider = new COMException(empty, (int)lastErrorHr);
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x000F6548 File Offset: 0x000F5948
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000F6568 File Offset: 0x000F5968
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (ADP.PtrZero != handle)
			{
				int num = this.propertySetCount;
				int i = 0;
				int num2 = 0;
				while (i < num)
				{
					IntPtr intPtr = Marshal.ReadIntPtr(handle, num2);
					if (ADP.PtrZero != intPtr)
					{
						int num3 = Marshal.ReadInt32(handle, num2 + ADP.PtrSize);
						IntPtr intPtr2 = ADP.IntPtrOffset(intPtr, ODB.OffsetOf_tagDBPROP_Value);
						int j = 0;
						while (j < num3)
						{
							SafeNativeMethods.VariantClear(intPtr2);
							j++;
							intPtr2 = ADP.IntPtrOffset(intPtr2, ODB.SizeOf_tagDBPROP);
						}
						SafeNativeMethods.CoTaskMemFree(intPtr);
					}
					i++;
					num2 += ODB.SizeOf_tagDBPROPSET;
				}
				SafeNativeMethods.CoTaskMemFree(handle);
			}
			return true;
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x000F661C File Offset: 0x000F5A1C
		internal int PropertySetCount
		{
			get
			{
				return this.propertySetCount;
			}
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000F6630 File Offset: 0x000F5A30
		internal tagDBPROP[] GetPropertySet(int index, out Guid propertyset)
		{
			if (index >= 0 && this.PropertySetCount > index)
			{
				tagDBPROPSET tagDBPROPSET = new tagDBPROPSET();
				tagDBPROP[] array = null;
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.DangerousAddRef(ref flag);
					IntPtr ptr = ADP.IntPtrOffset(base.DangerousGetHandle(), index * ODB.SizeOf_tagDBPROPSET);
					Marshal.PtrToStructure(ptr, tagDBPROPSET);
					propertyset = tagDBPROPSET.guidPropertySet;
					array = new tagDBPROP[tagDBPROPSET.cProperties];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = new tagDBPROP();
						IntPtr ptr2 = ADP.IntPtrOffset(tagDBPROPSET.rgProperties, i * ODB.SizeOf_tagDBPROP);
						Marshal.PtrToStructure(ptr2, array[i]);
					}
				}
				finally
				{
					if (flag)
					{
						base.DangerousRelease();
					}
				}
				return array;
			}
			if (this.lastErrorFromProvider != null)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer, this.lastErrorFromProvider);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000F6710 File Offset: 0x000F5B10
		internal void SetPropertySet(int index, Guid propertySet, tagDBPROP[] properties)
		{
			if (index >= 0 && this.PropertySetCount > index)
			{
				IntPtr intPtr = (IntPtr)(properties.Length * ODB.SizeOf_tagDBPROP);
				tagDBPROPSET tagDBPROPSET = new tagDBPROPSET(properties.Length, propertySet);
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.DangerousAddRef(ref flag);
					IntPtr ptr = ADP.IntPtrOffset(base.DangerousGetHandle(), index * ODB.SizeOf_tagDBPROPSET);
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						tagDBPROPSET.rgProperties = SafeNativeMethods.CoTaskMemAlloc(intPtr);
						if (ADP.PtrZero != tagDBPROPSET.rgProperties)
						{
							SafeNativeMethods.ZeroMemory(tagDBPROPSET.rgProperties, intPtr);
							Marshal.StructureToPtr(tagDBPROPSET, ptr, false);
						}
					}
					if (ADP.PtrZero == tagDBPROPSET.rgProperties)
					{
						throw new OutOfMemoryException();
					}
					for (int i = 0; i < properties.Length; i++)
					{
						IntPtr ptr2 = ADP.IntPtrOffset(tagDBPROPSET.rgProperties, i * ODB.SizeOf_tagDBPROP);
						Marshal.StructureToPtr(properties[i], ptr2, false);
					}
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
			if (this.lastErrorFromProvider != null)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer, this.lastErrorFromProvider);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000F6844 File Offset: 0x000F5C44
		internal static DBPropSet CreateProperty(Guid propertySet, int propertyId, bool required, object value)
		{
			tagDBPROP tagDBPROP = new tagDBPROP(propertyId, required, value);
			DBPropSet dbpropSet = new DBPropSet(1);
			dbpropSet.SetPropertySet(0, propertySet, new tagDBPROP[]
			{
				tagDBPROP
			});
			return dbpropSet;
		}

		// Token: 0x04001583 RID: 5507
		private readonly int propertySetCount;

		// Token: 0x04001584 RID: 5508
		private Exception lastErrorFromProvider;
	}
}
