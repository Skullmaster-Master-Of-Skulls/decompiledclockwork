using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000280 RID: 640
	internal sealed class PropertyIDSet : DbBuffer
	{
		// Token: 0x060026DA RID: 9946 RVA: 0x00107504 File Offset: 0x00106904
		internal PropertyIDSet(Guid propertySet, int propertyID) : base(PropertyIDSet.PropertyIDSetAndValueSize)
		{
			this._count = 1;
			IntPtr intPtr = ADP.IntPtrOffset(this.handle, PropertyIDSet.PropertyIDSetSize);
			Marshal.WriteIntPtr(this.handle, 0, intPtr);
			Marshal.WriteInt32(this.handle, ADP.PtrSize, 1);
			intPtr = ADP.IntPtrOffset(this.handle, ODB.OffsetOf_tagDBPROPIDSET_PropertySet);
			Marshal.StructureToPtr(propertySet, intPtr, false);
			Marshal.WriteInt32(this.handle, PropertyIDSet.PropertyIDSetSize, propertyID);
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x00107584 File Offset: 0x00106984
		internal PropertyIDSet(Guid[] propertySets) : base(PropertyIDSet.PropertyIDSetSize * propertySets.Length)
		{
			this._count = propertySets.Length;
			for (int i = 0; i < propertySets.Length; i++)
			{
				IntPtr ptr = ADP.IntPtrOffset(this.handle, i * PropertyIDSet.PropertyIDSetSize + ODB.OffsetOf_tagDBPROPIDSET_PropertySet);
				Marshal.StructureToPtr(propertySets[i], ptr, false);
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x001075E4 File Offset: 0x001069E4
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x040019D8 RID: 6616
		private static readonly int PropertyIDSetAndValueSize = ODB.SizeOf_tagDBPROPIDSET + ADP.PtrSize;

		// Token: 0x040019D9 RID: 6617
		private static readonly int PropertyIDSetSize = ODB.SizeOf_tagDBPROPIDSET;

		// Token: 0x040019DA RID: 6618
		private int _count;
	}
}
