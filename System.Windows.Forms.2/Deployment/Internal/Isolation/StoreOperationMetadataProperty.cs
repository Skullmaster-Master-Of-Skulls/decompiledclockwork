using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000058 RID: 88
	internal struct StoreOperationMetadataProperty
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000750E File Offset: 0x0000570E
		public StoreOperationMetadataProperty(Guid PropertySet, string Name)
		{
			this = new StoreOperationMetadataProperty(PropertySet, Name, null);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007519 File Offset: 0x00005719
		public StoreOperationMetadataProperty(Guid PropertySet, string Name, string Value)
		{
			this.GuidPropertySet = PropertySet;
			this.Name = Name;
			this.Value = Value;
			this.ValueSize = ((Value != null) ? new IntPtr((Value.Length + 1) * 2) : IntPtr.Zero);
		}

		// Token: 0x0400017A RID: 378
		public Guid GuidPropertySet;

		// Token: 0x0400017B RID: 379
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x0400017C RID: 380
		[MarshalAs(UnmanagedType.SysUInt)]
		public IntPtr ValueSize;

		// Token: 0x0400017D RID: 381
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;
	}
}
