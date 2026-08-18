using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000511 RID: 1297
	[ComVisible(true)]
	public interface ICustomMarshaler
	{
		// Token: 0x060031E2 RID: 12770
		object MarshalNativeToManaged(IntPtr pNativeData);

		// Token: 0x060031E3 RID: 12771
		IntPtr MarshalManagedToNative(object ManagedObj);

		// Token: 0x060031E4 RID: 12772
		void CleanUpNativeData(IntPtr pNativeData);

		// Token: 0x060031E5 RID: 12773
		void CleanUpManagedData(object ManagedObj);

		// Token: 0x060031E6 RID: 12774
		int GetNativeDataSize();
	}
}
