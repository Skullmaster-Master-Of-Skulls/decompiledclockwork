using System;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200046D RID: 1133
	public class X509Chain : IDisposable
	{
		// Token: 0x06002A2C RID: 10796 RVA: 0x000C07A5 File Offset: 0x000BE9A5
		public static X509Chain Create()
		{
			return (X509Chain)CryptoConfig.CreateFromName("X509Chain");
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000C07B6 File Offset: 0x000BE9B6
		[SecurityCritical]
		public X509Chain() : this(false)
		{
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000C07C0 File Offset: 0x000BE9C0
		[SecurityCritical]
		public X509Chain(bool useMachineContext)
		{
			this.m_syncRoot = new object();
			base..ctor();
			this.m_status = 0U;
			this.m_chainPolicy = null;
			this.m_chainStatus = null;
			this.m_chainElementCollection = new X509ChainElementCollection();
			this.m_safeCertChainHandle = SafeX509ChainHandle.InvalidHandle;
			this.m_useMachineContext = useMachineContext;
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000C0810 File Offset: 0x000BEA10
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public X509Chain(IntPtr chainContext)
		{
			this.m_syncRoot = new object();
			base..ctor();
			if (chainContext == IntPtr.Zero)
			{
				throw new ArgumentNullException("chainContext");
			}
			this.m_safeCertChainHandle = CAPISafe.CertDuplicateCertificateChain(chainContext);
			if (this.m_safeCertChainHandle == null || this.m_safeCertChainHandle == SafeX509ChainHandle.InvalidHandle)
			{
				throw new CryptographicException(SR.GetString("Cryptography_InvalidContextHandle"), "chainContext");
			}
			this.Init();
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x000C0882 File Offset: 0x000BEA82
		public IntPtr ChainContext
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.m_safeCertChainHandle.DangerousGetHandle();
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x000C088F File Offset: 0x000BEA8F
		public SafeX509ChainHandle SafeHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.m_safeCertChainHandle;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x000C0897 File Offset: 0x000BEA97
		// (set) Token: 0x06002A33 RID: 10803 RVA: 0x000C08B2 File Offset: 0x000BEAB2
		public X509ChainPolicy ChainPolicy
		{
			get
			{
				if (this.m_chainPolicy == null)
				{
					this.m_chainPolicy = new X509ChainPolicy();
				}
				return this.m_chainPolicy;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_chainPolicy = value;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x000C08C9 File Offset: 0x000BEAC9
		public X509ChainStatus[] ChainStatus
		{
			get
			{
				if (this.m_chainStatus == null)
				{
					if (this.m_status == 0U)
					{
						this.m_chainStatus = new X509ChainStatus[0];
					}
					else
					{
						this.m_chainStatus = X509Chain.GetChainStatusInformation(this.m_status);
					}
				}
				return this.m_chainStatus;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x000C0900 File Offset: 0x000BEB00
		public X509ChainElementCollection ChainElements
		{
			get
			{
				return this.m_chainElementCollection;
			}
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000C0908 File Offset: 0x000BEB08
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public bool Build(X509Certificate2 certificate)
		{
			object syncRoot = this.m_syncRoot;
			bool result;
			lock (syncRoot)
			{
				if (certificate == null || certificate.CertContext.IsInvalid)
				{
					throw new ArgumentException(SR.GetString("Cryptography_InvalidContextHandle"), "certificate");
				}
				StorePermission storePermission = new StorePermission(StorePermissionFlags.OpenStore | StorePermissionFlags.EnumerateCertificates);
				storePermission.Demand();
				X509ChainPolicy chainPolicy = this.ChainPolicy;
				if (chainPolicy.RevocationMode == X509RevocationMode.Online && (certificate.Extensions["2.5.29.31"] != null || certificate.Extensions["1.3.6.1.5.5.7.1.1"] != null))
				{
					PermissionSet permissionSet = new PermissionSet(PermissionState.None);
					permissionSet.AddPermission(new WebPermission(PermissionState.Unrestricted));
					permissionSet.AddPermission(new StorePermission(StorePermissionFlags.AddToStore));
					permissionSet.Demand();
				}
				this.Reset();
				int num = X509Chain.BuildChain(this.m_useMachineContext ? new IntPtr(1L) : new IntPtr(0L), certificate.CertContext, chainPolicy.ExtraStore, chainPolicy.ApplicationPolicy, chainPolicy.CertificatePolicy, chainPolicy.RevocationMode, chainPolicy.RevocationFlag, chainPolicy.VerificationTime, chainPolicy.UrlRetrievalTimeout, ref this.m_safeCertChainHandle);
				if (num != 0)
				{
					if (X509Chain.CompatSwitches.ShouldThrowOnChainBuildingFailure)
					{
						throw new CryptographicException(num);
					}
					result = false;
				}
				else
				{
					this.Init();
					CAPIBase.CERT_CHAIN_POLICY_PARA cert_CHAIN_POLICY_PARA = new CAPIBase.CERT_CHAIN_POLICY_PARA(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_POLICY_PARA)));
					CAPIBase.CERT_CHAIN_POLICY_STATUS cert_CHAIN_POLICY_STATUS = new CAPIBase.CERT_CHAIN_POLICY_STATUS(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_POLICY_STATUS)));
					cert_CHAIN_POLICY_PARA.dwFlags = (uint)chainPolicy.VerificationFlags;
					if (!CAPISafe.CertVerifyCertificateChainPolicy(new IntPtr(1L), this.m_safeCertChainHandle, ref cert_CHAIN_POLICY_PARA, ref cert_CHAIN_POLICY_STATUS))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					CAPISafe.SetLastError(cert_CHAIN_POLICY_STATUS.dwError);
					result = (cert_CHAIN_POLICY_STATUS.dwError == 0U);
				}
			}
			return result;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000C0AD8 File Offset: 0x000BECD8
		[SecurityCritical]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public void Reset()
		{
			this.m_status = 0U;
			this.m_chainStatus = null;
			this.m_chainElementCollection = new X509ChainElementCollection();
			if (!this.m_safeCertChainHandle.IsInvalid)
			{
				this.m_safeCertChainHandle.Dispose();
				this.m_safeCertChainHandle = SafeX509ChainHandle.InvalidHandle;
			}
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000C0B16 File Offset: 0x000BED16
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000C0B25 File Offset: 0x000BED25
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Reset();
			}
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000C0B30 File Offset: 0x000BED30
		[SecurityCritical]
		private unsafe void Init()
		{
			using (SafeX509ChainHandle safeX509ChainHandle = CAPISafe.CertDuplicateCertificateChain(this.m_safeCertChainHandle))
			{
				CAPIBase.CERT_CHAIN_CONTEXT cert_CHAIN_CONTEXT = new CAPIBase.CERT_CHAIN_CONTEXT(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_CONTEXT)));
				uint num = (uint)Marshal.ReadInt32(safeX509ChainHandle.DangerousGetHandle());
				if ((ulong)num > (ulong)((long)Marshal.SizeOf(cert_CHAIN_CONTEXT)))
				{
					num = (uint)Marshal.SizeOf(cert_CHAIN_CONTEXT);
				}
				X509Utils.memcpy(this.m_safeCertChainHandle.DangerousGetHandle(), new IntPtr((void*)(&cert_CHAIN_CONTEXT)), num);
				this.m_status = cert_CHAIN_CONTEXT.dwErrorStatus;
				this.m_chainElementCollection = new X509ChainElementCollection(Marshal.ReadIntPtr(cert_CHAIN_CONTEXT.rgpChain));
			}
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000C0BE0 File Offset: 0x000BEDE0
		internal static X509ChainStatus[] GetChainStatusInformation(uint dwStatus)
		{
			if (dwStatus == 0U)
			{
				return new X509ChainStatus[0];
			}
			int num = 0;
			for (uint num2 = dwStatus; num2 != 0U; num2 >>= 1)
			{
				if ((num2 & 1U) != 0U)
				{
					num++;
				}
			}
			X509ChainStatus[] array = new X509ChainStatus[num];
			int num3 = 0;
			foreach (X509Chain.X509ChainErrorMapping x509ChainErrorMapping in X509Chain.s_x509ChainErrorMappings)
			{
				if ((dwStatus & x509ChainErrorMapping.Win32Flag) != 0U)
				{
					array[num3].StatusInformation = X509Utils.GetSystemErrorString(x509ChainErrorMapping.Win32ErrorCode);
					array[num3].Status = x509ChainErrorMapping.ChainStatusFlag;
					num3++;
					dwStatus &= ~x509ChainErrorMapping.Win32Flag;
				}
			}
			int num4 = 0;
			for (uint num5 = dwStatus; num5 != 0U; num5 >>= 1)
			{
				if ((num5 & 1U) != 0U)
				{
					array[num3].Status = (X509ChainStatusFlags)(1 << num4);
					array[num3].StatusInformation = SR.GetString("Unknown_Error");
					num3++;
				}
				num4++;
			}
			return array;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000C0CD0 File Offset: 0x000BEED0
		[SecurityCritical]
		internal unsafe static int BuildChain(IntPtr hChainEngine, SafeCertContextHandle pCertContext, X509Certificate2Collection extraStore, OidCollection applicationPolicy, OidCollection certificatePolicy, X509RevocationMode revocationMode, X509RevocationFlag revocationFlag, DateTime verificationTime, TimeSpan timeout, ref SafeX509ChainHandle ppChainContext)
		{
			if (pCertContext == null || pCertContext.IsInvalid)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidContextHandle"), "pCertContext");
			}
			SafeCertStoreHandle hAdditionalStore = SafeCertStoreHandle.InvalidHandle;
			if (extraStore != null && extraStore.Count > 0)
			{
				hAdditionalStore = X509Utils.ExportToMemoryStore(extraStore);
			}
			CAPIBase.CERT_CHAIN_PARA cert_CHAIN_PARA = default(CAPIBase.CERT_CHAIN_PARA);
			cert_CHAIN_PARA.cbSize = (uint)Marshal.SizeOf(cert_CHAIN_PARA);
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			SafeLocalAllocHandle safeLocalAllocHandle2 = SafeLocalAllocHandle.InvalidHandle;
			try
			{
				if (applicationPolicy != null && applicationPolicy.Count > 0)
				{
					cert_CHAIN_PARA.RequestedUsage.dwType = 0U;
					cert_CHAIN_PARA.RequestedUsage.Usage.cUsageIdentifier = (uint)applicationPolicy.Count;
					safeLocalAllocHandle = X509Utils.CopyOidsToUnmanagedMemory(applicationPolicy);
					cert_CHAIN_PARA.RequestedUsage.Usage.rgpszUsageIdentifier = safeLocalAllocHandle.DangerousGetHandle();
				}
				if (certificatePolicy != null && certificatePolicy.Count > 0)
				{
					cert_CHAIN_PARA.RequestedIssuancePolicy.dwType = 0U;
					cert_CHAIN_PARA.RequestedIssuancePolicy.Usage.cUsageIdentifier = (uint)certificatePolicy.Count;
					safeLocalAllocHandle2 = X509Utils.CopyOidsToUnmanagedMemory(certificatePolicy);
					cert_CHAIN_PARA.RequestedIssuancePolicy.Usage.rgpszUsageIdentifier = safeLocalAllocHandle2.DangerousGetHandle();
				}
				cert_CHAIN_PARA.dwUrlRetrievalTimeout = (uint)Math.Floor(timeout.TotalMilliseconds);
				System.Runtime.InteropServices.ComTypes.FILETIME filetime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
				*(long*)(&filetime) = verificationTime.ToFileTime();
				uint dwFlags = X509Utils.MapRevocationFlags(revocationMode, revocationFlag);
				if (!CAPISafe.CertGetCertificateChain(hChainEngine, pCertContext, ref filetime, hAdditionalStore, ref cert_CHAIN_PARA, dwFlags, IntPtr.Zero, ref ppChainContext))
				{
					return Marshal.GetHRForLastWin32Error();
				}
			}
			finally
			{
				safeLocalAllocHandle.Dispose();
				safeLocalAllocHandle2.Dispose();
			}
			return 0;
		}

		// Token: 0x04002600 RID: 9728
		private uint m_status;

		// Token: 0x04002601 RID: 9729
		private X509ChainPolicy m_chainPolicy;

		// Token: 0x04002602 RID: 9730
		private X509ChainStatus[] m_chainStatus;

		// Token: 0x04002603 RID: 9731
		private X509ChainElementCollection m_chainElementCollection;

		// Token: 0x04002604 RID: 9732
		[SecurityCritical]
		private SafeX509ChainHandle m_safeCertChainHandle;

		// Token: 0x04002605 RID: 9733
		private bool m_useMachineContext;

		// Token: 0x04002606 RID: 9734
		private readonly object m_syncRoot;

		// Token: 0x04002607 RID: 9735
		private static readonly X509Chain.X509ChainErrorMapping[] s_x509ChainErrorMappings = new X509Chain.X509ChainErrorMapping[]
		{
			new X509Chain.X509ChainErrorMapping(8U, -2146869244, X509ChainStatusFlags.NotSignatureValid),
			new X509Chain.X509ChainErrorMapping(262144U, -2146869244, X509ChainStatusFlags.CtlNotSignatureValid),
			new X509Chain.X509ChainErrorMapping(32U, -2146762487, X509ChainStatusFlags.UntrustedRoot),
			new X509Chain.X509ChainErrorMapping(65536U, -2146762486, X509ChainStatusFlags.PartialChain),
			new X509Chain.X509ChainErrorMapping(4U, -2146885616, X509ChainStatusFlags.Revoked),
			new X509Chain.X509ChainErrorMapping(16U, -2146762480, X509ChainStatusFlags.NotValidForUsage),
			new X509Chain.X509ChainErrorMapping(524288U, -2146762480, X509ChainStatusFlags.CtlNotValidForUsage),
			new X509Chain.X509ChainErrorMapping(1U, -2146762495, X509ChainStatusFlags.NotTimeValid),
			new X509Chain.X509ChainErrorMapping(131072U, -2146762495, X509ChainStatusFlags.CtlNotTimeValid),
			new X509Chain.X509ChainErrorMapping(2048U, -2146762476, X509ChainStatusFlags.InvalidNameConstraints),
			new X509Chain.X509ChainErrorMapping(4096U, -2146762476, X509ChainStatusFlags.HasNotSupportedNameConstraint),
			new X509Chain.X509ChainErrorMapping(8192U, -2146762476, X509ChainStatusFlags.HasNotDefinedNameConstraint),
			new X509Chain.X509ChainErrorMapping(16384U, -2146762476, X509ChainStatusFlags.HasNotPermittedNameConstraint),
			new X509Chain.X509ChainErrorMapping(32768U, -2146762476, X509ChainStatusFlags.HasExcludedNameConstraint),
			new X509Chain.X509ChainErrorMapping(512U, -2146762477, X509ChainStatusFlags.InvalidPolicyConstraints),
			new X509Chain.X509ChainErrorMapping(33554432U, -2146762477, X509ChainStatusFlags.NoIssuanceChainPolicy),
			new X509Chain.X509ChainErrorMapping(1024U, -2146869223, X509ChainStatusFlags.InvalidBasicConstraints),
			new X509Chain.X509ChainErrorMapping(2U, -2146762494, X509ChainStatusFlags.NotTimeNested),
			new X509Chain.X509ChainErrorMapping(64U, -2146885614, X509ChainStatusFlags.RevocationStatusUnknown),
			new X509Chain.X509ChainErrorMapping(16777216U, -2146885613, X509ChainStatusFlags.OfflineRevocation),
			new X509Chain.X509ChainErrorMapping(67108864U, -2146762479, X509ChainStatusFlags.ExplicitDistrust),
			new X509Chain.X509ChainErrorMapping(134217728U, -2146762491, X509ChainStatusFlags.HasNotSupportedCriticalExtension),
			new X509Chain.X509ChainErrorMapping(1048576U, -2146877418, X509ChainStatusFlags.HasWeakSignature)
		};

		// Token: 0x02000879 RID: 2169
		private struct X509ChainErrorMapping
		{
			// Token: 0x06004573 RID: 17779 RVA: 0x00121C90 File Offset: 0x0011FE90
			public X509ChainErrorMapping(uint win32Flag, int win32ErrorCode, X509ChainStatusFlags chainStatusFlag)
			{
				this.Win32Flag = win32Flag;
				this.Win32ErrorCode = win32ErrorCode;
				this.ChainStatusFlag = chainStatusFlag;
			}

			// Token: 0x0400372A RID: 14122
			public readonly uint Win32Flag;

			// Token: 0x0400372B RID: 14123
			public readonly int Win32ErrorCode;

			// Token: 0x0400372C RID: 14124
			public readonly X509ChainStatusFlags ChainStatusFlag;
		}

		// Token: 0x0200087A RID: 2170
		private static class CompatSwitches
		{
			// Token: 0x06004574 RID: 17780 RVA: 0x00121CA8 File Offset: 0x0011FEA8
			[SecuritySafeCritical]
			[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
			private static int ReadInt32CompatSwitch(string switchName, int defaultValue)
			{
				string environmentVariable = Environment.GetEnvironmentVariable("COMPlus_" + switchName);
				int result;
				if (environmentVariable != null && int.TryParse(environmentVariable, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
				{
					return result;
				}
				int? num = X509Chain.CompatSwitches.ReadInt32CompatSwitchFromRegistry(RegistryHive.CurrentUser, switchName);
				if (num != null)
				{
					return num.GetValueOrDefault();
				}
				int? num2 = X509Chain.CompatSwitches.ReadInt32CompatSwitchFromRegistry(RegistryHive.LocalMachine, switchName);
				if (num2 == null)
				{
					return defaultValue;
				}
				return num2.GetValueOrDefault();
			}

			// Token: 0x06004575 RID: 17781 RVA: 0x00121D18 File Offset: 0x0011FF18
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
			private static int? ReadInt32CompatSwitchFromRegistry(RegistryHive hive, string switchName)
			{
				try
				{
					using (RegistryKey registryKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64))
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework", false))
						{
							return ((registryKey2 != null) ? registryKey2.GetValue(switchName) : null) as int?;
						}
					}
				}
				catch
				{
				}
				return null;
			}

			// Token: 0x0400372D RID: 14125
			internal static readonly bool ShouldThrowOnChainBuildingFailure = X509Chain.CompatSwitches.ReadInt32CompatSwitch("X509Chain_ThrowOnBuildFailure", 1) != 0;
		}
	}
}
