using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001B1 RID: 433
	internal class X509CertificateChain
	{
		// Token: 0x06000E18 RID: 3608 RVA: 0x0003FFD4 File Offset: 0x0003E1D4
		public X509CertificateChain() : this(false)
		{
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003FFDD File Offset: 0x0003E1DD
		public X509CertificateChain(bool useMachineContext)
		{
			this.chainPolicyOID = 1U;
			base..ctor();
			this.useMachineContext = useMachineContext;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0003FFF3 File Offset: 0x0003E1F3
		public X509CertificateChain(bool useMachineContext, uint chainPolicyOID)
		{
			this.chainPolicyOID = 1U;
			base..ctor();
			this.useMachineContext = useMachineContext;
			this.chainPolicyOID = chainPolicyOID;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00040010 File Offset: 0x0003E210
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x0004002B File Offset: 0x0003E22B
		public X509ChainPolicy ChainPolicy
		{
			get
			{
				if (this.chainPolicy == null)
				{
					this.chainPolicy = new X509ChainPolicy();
				}
				return this.chainPolicy;
			}
			set
			{
				this.chainPolicy = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00002D0C File Offset: 0x00000F0C
		public X509ChainStatus[] ChainStatus
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00040034 File Offset: 0x0003E234
		[SecuritySafeCritical]
		[StorePermission(SecurityAction.Demand, CreateStore = true, OpenStore = true, EnumerateCertificates = true)]
		public bool Build(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			if (certificate.Handle == IntPtr.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("certificate", SR.GetString("ArgumentInvalidCertificate"));
			}
			SafeCertChainHandle invalidHandle = SafeCertChainHandle.InvalidHandle;
			X509ChainPolicy x509ChainPolicy = this.ChainPolicy;
			x509ChainPolicy.VerificationTime = DateTime.Now;
			if (x509ChainPolicy.RevocationMode == X509RevocationMode.Online && (certificate.Extensions["2.5.29.31"] != null || certificate.Extensions["1.3.6.1.5.5.7.1.1"] != null))
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new WebPermission(PermissionState.Unrestricted));
				permissionSet.AddPermission(new StorePermission(StorePermissionFlags.AddToStore));
				permissionSet.Demand();
			}
			X509CertificateChain.BuildChain(this.useMachineContext ? new IntPtr(1L) : new IntPtr(0L), certificate.Handle, x509ChainPolicy.ExtraStore, x509ChainPolicy.ApplicationPolicy, x509ChainPolicy.CertificatePolicy, x509ChainPolicy.RevocationMode, x509ChainPolicy.RevocationFlag, x509ChainPolicy.VerificationTime, x509ChainPolicy.UrlRetrievalTimeout, out invalidHandle);
			CAPI.CERT_CHAIN_POLICY_PARA cert_CHAIN_POLICY_PARA = new CAPI.CERT_CHAIN_POLICY_PARA(Marshal.SizeOf(typeof(CAPI.CERT_CHAIN_POLICY_PARA)));
			CAPI.CERT_CHAIN_POLICY_STATUS cert_CHAIN_POLICY_STATUS = new CAPI.CERT_CHAIN_POLICY_STATUS(Marshal.SizeOf(typeof(CAPI.CERT_CHAIN_POLICY_STATUS)));
			cert_CHAIN_POLICY_PARA.dwFlags = (uint)(x509ChainPolicy.VerificationFlags | (X509VerificationFlags)4096);
			if (!CAPI.CertVerifyCertificateChainPolicy(new IntPtr((long)((ulong)this.chainPolicyOID)), invalidHandle, ref cert_CHAIN_POLICY_PARA, ref cert_CHAIN_POLICY_STATUS))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(lastWin32Error));
			}
			if (cert_CHAIN_POLICY_STATUS.dwError != 0U)
			{
				int dwError = (int)cert_CHAIN_POLICY_STATUS.dwError;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("X509ChainBuildFail", new object[]
				{
					SecurityUtils.GetCertificateId(certificate),
					new CryptographicException(dwError).Message
				})));
			}
			return true;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x000401FC File Offset: 0x0003E3FC
		[SecurityCritical]
		private unsafe static void BuildChain(IntPtr hChainEngine, IntPtr pCertContext, X509Certificate2Collection extraStore, OidCollection applicationPolicy, OidCollection certificatePolicy, X509RevocationMode revocationMode, X509RevocationFlag revocationFlag, DateTime verificationTime, TimeSpan timeout, out SafeCertChainHandle ppChainContext)
		{
			SafeCertStoreHandle safeCertStoreHandle = X509CertificateChain.ExportToMemoryStore(extraStore, pCertContext);
			CAPI.CERT_CHAIN_PARA cert_CHAIN_PARA = default(CAPI.CERT_CHAIN_PARA);
			cert_CHAIN_PARA.cbSize = (uint)Marshal.SizeOf(typeof(CAPI.CERT_CHAIN_PARA));
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			SafeHGlobalHandle safeHGlobalHandle2 = SafeHGlobalHandle.InvalidHandle;
			try
			{
				if (applicationPolicy != null && applicationPolicy.Count > 0)
				{
					cert_CHAIN_PARA.RequestedUsage.dwType = 0U;
					cert_CHAIN_PARA.RequestedUsage.Usage.cUsageIdentifier = (uint)applicationPolicy.Count;
					safeHGlobalHandle = X509CertificateChain.CopyOidsToUnmanagedMemory(applicationPolicy);
					cert_CHAIN_PARA.RequestedUsage.Usage.rgpszUsageIdentifier = safeHGlobalHandle.DangerousGetHandle();
				}
				if (certificatePolicy != null && certificatePolicy.Count > 0)
				{
					cert_CHAIN_PARA.RequestedIssuancePolicy.dwType = 0U;
					cert_CHAIN_PARA.RequestedIssuancePolicy.Usage.cUsageIdentifier = (uint)certificatePolicy.Count;
					safeHGlobalHandle2 = X509CertificateChain.CopyOidsToUnmanagedMemory(certificatePolicy);
					cert_CHAIN_PARA.RequestedIssuancePolicy.Usage.rgpszUsageIdentifier = safeHGlobalHandle2.DangerousGetHandle();
				}
				cert_CHAIN_PARA.dwUrlRetrievalTimeout = (uint)timeout.Milliseconds;
				System.Runtime.InteropServices.ComTypes.FILETIME filetime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
				*(long*)(&filetime) = verificationTime.ToFileTime();
				uint dwFlags = X509CertificateChain.MapRevocationFlags(revocationMode, revocationFlag);
				if (!CAPI.CertGetCertificateChain(hChainEngine, pCertContext, ref filetime, safeCertStoreHandle, ref cert_CHAIN_PARA, dwFlags, IntPtr.Zero, out ppChainContext))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(lastWin32Error));
				}
			}
			finally
			{
				if (safeHGlobalHandle != null)
				{
					safeHGlobalHandle.Dispose();
				}
				if (safeHGlobalHandle2 != null)
				{
					safeHGlobalHandle2.Dispose();
				}
				safeCertStoreHandle.Close();
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00040364 File Offset: 0x0003E564
		[SecurityCritical]
		private static SafeCertStoreHandle ExportToMemoryStore(X509Certificate2Collection collection, IntPtr pCertContext)
		{
			CAPI.CERT_CONTEXT cert_CONTEXT = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(pCertContext, typeof(CAPI.CERT_CONTEXT));
			if ((collection == null || collection.Count <= 0) && cert_CONTEXT.hCertStore == IntPtr.Zero)
			{
				return SafeCertStoreHandle.InvalidHandle;
			}
			SafeCertStoreHandle safeCertStoreHandle = CAPI.CertOpenStore(new IntPtr(2L), 65537U, IntPtr.Zero, 8704U, null);
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(lastWin32Error));
			}
			if (collection != null && collection.Count > 0)
			{
				foreach (X509Certificate2 x509Certificate in collection)
				{
					if (!CAPI.CertAddCertificateLinkToStore(safeCertStoreHandle, x509Certificate.Handle, 4U, SafeCertContextHandle.InvalidHandle))
					{
						int lastWin32Error2 = Marshal.GetLastWin32Error();
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(lastWin32Error2));
					}
				}
			}
			using (SafeCertContextHandle safeCertContextHandle = CAPI.CertCreateCertificateContext(cert_CONTEXT.dwCertEncodingType, cert_CONTEXT.pbCertEncoded, cert_CONTEXT.cbCertEncoded))
			{
				X509Certificate2 x509Certificate2 = new X509Certificate2(safeCertContextHandle.DangerousGetHandle());
				CAPI.CERT_CONTEXT cert_CONTEXT2 = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(x509Certificate2.Handle, typeof(CAPI.CERT_CONTEXT));
				if (cert_CONTEXT2.hCertStore != IntPtr.Zero)
				{
					X509Certificate2Collection x509Certificate2Collection = null;
					X509Store x509Store = new X509Store(cert_CONTEXT2.hCertStore);
					try
					{
						x509Certificate2Collection = x509Store.Certificates;
						foreach (X509Certificate2 x509Certificate3 in x509Certificate2Collection)
						{
							if (!CAPI.CertAddCertificateLinkToStore(safeCertStoreHandle, x509Certificate3.Handle, 4U, SafeCertContextHandle.InvalidHandle))
							{
								int lastWin32Error3 = Marshal.GetLastWin32Error();
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(lastWin32Error3));
							}
						}
					}
					finally
					{
						SecurityUtils.ResetAllCertificates(x509Certificate2Collection);
						x509Store.Close();
					}
				}
			}
			return safeCertStoreHandle;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00040538 File Offset: 0x0003E738
		[SecurityCritical]
		private static SafeHGlobalHandle CopyOidsToUnmanagedMemory(OidCollection oids)
		{
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			if (oids == null || oids.Count == 0)
			{
				return safeHGlobalHandle;
			}
			List<string> list = new List<string>();
			foreach (Oid oid in oids)
			{
				list.Add(oid.Value);
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			checked
			{
				int num = list.Count * Marshal.SizeOf(typeof(IntPtr));
				int num2 = 0;
				foreach (string text in list)
				{
					num2 += text.Length + 1;
				}
				safeHGlobalHandle = SafeHGlobalHandle.AllocHGlobal(num + num2);
				zero = new IntPtr((long)safeHGlobalHandle.DangerousGetHandle() + unchecked((long)num));
			}
			for (int i = 0; i < list.Count; i++)
			{
				Marshal.WriteIntPtr(new IntPtr((long)safeHGlobalHandle.DangerousGetHandle() + (long)(i * Marshal.SizeOf(typeof(IntPtr)))), zero);
				byte[] bytes = Encoding.ASCII.GetBytes(list[i]);
				if (bytes.Length != list[i].Length)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CollectionWasModified")));
				}
				Marshal.Copy(bytes, 0, zero, bytes.Length);
				zero2 = new IntPtr((long)zero + (long)bytes.Length);
				Marshal.WriteByte(zero2, 0);
				zero = new IntPtr((long)zero + (long)list[i].Length + 1L);
			}
			return safeHGlobalHandle;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000406E8 File Offset: 0x0003E8E8
		private static uint MapRevocationFlags(X509RevocationMode revocationMode, X509RevocationFlag revocationFlag)
		{
			uint num = 0U;
			if (revocationMode == X509RevocationMode.NoCheck)
			{
				return num;
			}
			if (revocationMode == X509RevocationMode.Offline)
			{
				num |= 2147483648U;
			}
			if (revocationFlag == X509RevocationFlag.EndCertificateOnly)
			{
				num |= 268435456U;
			}
			else if (revocationFlag == X509RevocationFlag.EntireChain)
			{
				num |= 536870912U;
			}
			else
			{
				num |= 1073741824U;
			}
			return num;
		}

		// Token: 0x04000CEC RID: 3308
		public const uint DefaultChainPolicyOID = 1U;

		// Token: 0x04000CED RID: 3309
		private bool useMachineContext;

		// Token: 0x04000CEE RID: 3310
		private X509ChainPolicy chainPolicy;

		// Token: 0x04000CEF RID: 3311
		private uint chainPolicyOID;
	}
}
