using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200025B RID: 603
	internal sealed class PropertyIDSet : DbBuffer
	{
		// Token: 0x0600209B RID: 8347 RVA: 0x00281278 File Offset: 0x00280678
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

		// Token: 0x0600209C RID: 8348 RVA: 0x002812F8 File Offset: 0x002806F8
		internal PropertyIDSet(Guid[] propertySets) : base(PropertyIDSet.PropertyIDSetSize * propertySets.Length)
		{
			this._count = propertySets.Length;
			for (int i = 0; i < propertySets.Length; i++)
			{
				IntPtr ptr = ADP.IntPtrOffset(this.handle, i * PropertyIDSet.PropertyIDSetSize + ODB.OffsetOf_tagDBPROPIDSET_PropertySet);
				Marshal.StructureToPtr(propertySets[i], ptr, false);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x00281368 File Offset: 0x00280768
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x04001539 RID: 5433
		private static readonly int PropertyIDSetAndValueSize = ODB.SizeOf_tagDBPROPIDSET + ADP.PtrSize;

		// Token: 0x0400153A RID: 5434
		private static readonly int PropertyIDSetSize = ODB.SizeOf_tagDBPROPIDSET;

		// Token: 0x0400153B RID: 5435
		private int _count;
	}
}
