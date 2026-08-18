using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200020E RID: 526
	internal sealed class DBPropSet : SafeHandle
	{
		// Token: 0x06001D6F RID: 7535 RVA: 0x0026DE38 File Offset: 0x0026D238
		private DBPropSet() : base(IntPtr.Zero, true)
		{
			this.propertySetCount = 0;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0026DE58 File Offset: 0x0026D258
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

		// Token: 0x06001D71 RID: 7537 RVA: 0x0026DEE8 File Offset: 0x0026D2E8
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
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0026DF38 File Offset: 0x0026D338
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
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0026DF88 File Offset: 0x0026D388
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
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0026DFD8 File Offset: 0x0026D3D8
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0026DFF8 File Offset: 0x0026D3F8
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0026E0B8 File Offset: 0x0026D4B8
		internal int PropertySetCount
		{
			get
			{
				return this.propertySetCount;
			}
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0026E0D8 File Offset: 0x0026D4D8
		internal tagDBPROP[] GetPropertySet(int index, out Guid propertyset)
		{
			if (index < 0 || this.PropertySetCount <= index)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
			}
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

		// Token: 0x06001D78 RID: 7544 RVA: 0x0026E1A8 File Offset: 0x0026D5A8
		internal void SetPropertySet(int index, Guid propertySet, tagDBPROP[] properties)
		{
			if (index < 0 || this.PropertySetCount <= index)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
			}
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
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0026E2C8 File Offset: 0x0026D6C8
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

		// Token: 0x040010C2 RID: 4290
		private readonly int propertySetCount;
	}
}
