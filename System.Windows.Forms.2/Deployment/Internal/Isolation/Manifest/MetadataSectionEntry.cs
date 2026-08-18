using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000C9 RID: 201
	[StructLayout(LayoutKind.Sequential)]
	internal class MetadataSectionEntry : IDisposable
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00008C80 File Offset: 0x00006E80
		~MetadataSectionEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00008CB0 File Offset: 0x00006EB0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008CBC File Offset: 0x00006EBC
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.ManifestHash != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ManifestHash);
				this.ManifestHash = IntPtr.Zero;
			}
			if (this.MvidValue != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.MvidValue);
				this.MvidValue = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000320 RID: 800
		public uint SchemaVersion;

		// Token: 0x04000321 RID: 801
		public uint ManifestFlags;

		// Token: 0x04000322 RID: 802
		public uint UsagePatterns;

		// Token: 0x04000323 RID: 803
		public IDefinitionIdentity CdfIdentity;

		// Token: 0x04000324 RID: 804
		[MarshalAs(UnmanagedType.LPWStr)]
		public string LocalPath;

		// Token: 0x04000325 RID: 805
		public uint HashAlgorithm;

		// Token: 0x04000326 RID: 806
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ManifestHash;

		// Token: 0x04000327 RID: 807
		public uint ManifestHashSize;

		// Token: 0x04000328 RID: 808
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ContentType;

		// Token: 0x04000329 RID: 809
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeImageVersion;

		// Token: 0x0400032A RID: 810
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr MvidValue;

		// Token: 0x0400032B RID: 811
		public uint MvidValueSize;

		// Token: 0x0400032C RID: 812
		public DescriptionMetadataEntry DescriptionData;

		// Token: 0x0400032D RID: 813
		public DeploymentMetadataEntry DeploymentData;

		// Token: 0x0400032E RID: 814
		public DependentOSMetadataEntry DependentOSData;

		// Token: 0x0400032F RID: 815
		[MarshalAs(UnmanagedType.LPWStr)]
		public string defaultPermissionSetID;

		// Token: 0x04000330 RID: 816
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RequestedExecutionLevel;

		// Token: 0x04000331 RID: 817
		public bool RequestedExecutionLevelUIAccess;

		// Token: 0x04000332 RID: 818
		public IReferenceIdentity ResourceTypeResourcesDependency;

		// Token: 0x04000333 RID: 819
		public IReferenceIdentity ResourceTypeManifestResourcesDependency;

		// Token: 0x04000334 RID: 820
		[MarshalAs(UnmanagedType.LPWStr)]
		public string KeyInfoElement;

		// Token: 0x04000335 RID: 821
		public CompatibleFrameworksMetadataEntry CompatibleFrameworksData;
	}
}
