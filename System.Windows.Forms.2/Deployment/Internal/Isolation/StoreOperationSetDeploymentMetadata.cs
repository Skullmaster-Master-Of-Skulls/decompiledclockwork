using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000059 RID: 89
	internal struct StoreOperationSetDeploymentMetadata
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000754F File Offset: 0x0000574F
		public StoreOperationSetDeploymentMetadata(IDefinitionAppId Deployment, StoreApplicationReference Reference, StoreOperationMetadataProperty[] SetProperties)
		{
			this = new StoreOperationSetDeploymentMetadata(Deployment, Reference, SetProperties, null);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000755C File Offset: 0x0000575C
		[SecuritySafeCritical]
		public StoreOperationSetDeploymentMetadata(IDefinitionAppId Deployment, StoreApplicationReference Reference, StoreOperationMetadataProperty[] SetProperties, StoreOperationMetadataProperty[] TestProperties)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationSetDeploymentMetadata));
			this.Flags = StoreOperationSetDeploymentMetadata.OpFlags.Nothing;
			this.Deployment = Deployment;
			if (SetProperties != null)
			{
				this.PropertiesToSet = StoreOperationSetDeploymentMetadata.MarshalProperties(SetProperties);
				this.cPropertiesToSet = new IntPtr(SetProperties.Length);
			}
			else
			{
				this.PropertiesToSet = IntPtr.Zero;
				this.cPropertiesToSet = IntPtr.Zero;
			}
			if (TestProperties != null)
			{
				this.PropertiesToTest = StoreOperationSetDeploymentMetadata.MarshalProperties(TestProperties);
				this.cPropertiesToTest = new IntPtr(TestProperties.Length);
			}
			else
			{
				this.PropertiesToTest = IntPtr.Zero;
				this.cPropertiesToTest = IntPtr.Zero;
			}
			this.InstallerReference = Reference.ToIntPtr();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007608 File Offset: 0x00005808
		[SecurityCritical]
		public void Destroy()
		{
			if (this.PropertiesToSet != IntPtr.Zero)
			{
				StoreOperationSetDeploymentMetadata.DestroyProperties(this.PropertiesToSet, (ulong)this.cPropertiesToSet.ToInt64());
				this.PropertiesToSet = IntPtr.Zero;
				this.cPropertiesToSet = IntPtr.Zero;
			}
			if (this.PropertiesToTest != IntPtr.Zero)
			{
				StoreOperationSetDeploymentMetadata.DestroyProperties(this.PropertiesToTest, (ulong)this.cPropertiesToTest.ToInt64());
				this.PropertiesToTest = IntPtr.Zero;
				this.cPropertiesToTest = IntPtr.Zero;
			}
			if (this.InstallerReference != IntPtr.Zero)
			{
				StoreApplicationReference.Destroy(this.InstallerReference);
				this.InstallerReference = IntPtr.Zero;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000076BC File Offset: 0x000058BC
		[SecurityCritical]
		private static void DestroyProperties(IntPtr rgItems, ulong iItems)
		{
			if (rgItems != IntPtr.Zero)
			{
				ulong num = (ulong)((long)Marshal.SizeOf(typeof(StoreOperationMetadataProperty)));
				for (ulong num2 = 0UL; num2 < iItems; num2 += 1UL)
				{
					Marshal.DestroyStructure(new IntPtr((long)(num2 * num + (ulong)rgItems.ToInt64())), typeof(StoreOperationMetadataProperty));
				}
				Marshal.FreeCoTaskMem(rgItems);
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000771C File Offset: 0x0000591C
		[SecurityCritical]
		private static IntPtr MarshalProperties(StoreOperationMetadataProperty[] Props)
		{
			if (Props == null || Props.Length == 0)
			{
				return IntPtr.Zero;
			}
			int num = Marshal.SizeOf(typeof(StoreOperationMetadataProperty));
			IntPtr result = Marshal.AllocCoTaskMem(num * Props.Length);
			for (int num2 = 0; num2 != Props.Length; num2++)
			{
				Marshal.StructureToPtr(Props[num2], new IntPtr((long)(num2 * num) + result.ToInt64()), false);
			}
			return result;
		}

		// Token: 0x0400017E RID: 382
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400017F RID: 383
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationSetDeploymentMetadata.OpFlags Flags;

		// Token: 0x04000180 RID: 384
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Deployment;

		// Token: 0x04000181 RID: 385
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr InstallerReference;

		// Token: 0x04000182 RID: 386
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr cPropertiesToTest;

		// Token: 0x04000183 RID: 387
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr PropertiesToTest;

		// Token: 0x04000184 RID: 388
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr cPropertiesToSet;

		// Token: 0x04000185 RID: 389
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr PropertiesToSet;

		// Token: 0x0200052E RID: 1326
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037C8 RID: 14280
			Nothing = 0
		}

		// Token: 0x0200052F RID: 1327
		public enum Disposition
		{
			// Token: 0x040037CA RID: 14282
			Failed,
			// Token: 0x040037CB RID: 14283
			Set = 2
		}
	}
}
