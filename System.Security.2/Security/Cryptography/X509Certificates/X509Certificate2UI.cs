using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000068 RID: 104
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public static class X509Certificate2UI
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x00014039 File Offset: 0x00012239
		[SecuritySafeCritical]
		public static void DisplayCertificate(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			X509Certificate2UI.DisplayX509Certificate(X509Utils.GetCertContext(certificate), IntPtr.Zero);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00014059 File Offset: 0x00012259
		[SecurityCritical]
		public static void DisplayCertificate(X509Certificate2 certificate, IntPtr hwndParent)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			X509Certificate2UI.DisplayX509Certificate(X509Utils.GetCertContext(certificate), hwndParent);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00014075 File Offset: 0x00012275
		public static X509Certificate2Collection SelectFromCollection(X509Certificate2Collection certificates, string title, string message, X509SelectionFlag selectionFlag)
		{
			return X509Certificate2UI.SelectFromCollectionHelper(certificates, title, message, selectionFlag, IntPtr.Zero);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00014085 File Offset: 0x00012285
		[SecurityCritical]
		public static X509Certificate2Collection SelectFromCollection(X509Certificate2Collection certificates, string title, string message, X509SelectionFlag selectionFlag, IntPtr hwndParent)
		{
			return X509Certificate2UI.SelectFromCollectionHelper(certificates, title, message, selectionFlag, hwndParent);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00014094 File Offset: 0x00012294
		[SecurityCritical]
		private static void DisplayX509Certificate(SafeCertContextHandle safeCertContext, IntPtr hwndParent)
		{
			if (safeCertContext.IsInvalid)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "safeCertContext");
			}
			int num = 0;
			CAPI.CRYPTUI_VIEWCERTIFICATE_STRUCTW cryptui_VIEWCERTIFICATE_STRUCTW = new CAPI.CRYPTUI_VIEWCERTIFICATE_STRUCTW();
			cryptui_VIEWCERTIFICATE_STRUCTW.dwSize = (uint)Marshal.SizeOf(cryptui_VIEWCERTIFICATE_STRUCTW);
			cryptui_VIEWCERTIFICATE_STRUCTW.hwndParent = hwndParent;
			cryptui_VIEWCERTIFICATE_STRUCTW.dwFlags = 0U;
			cryptui_VIEWCERTIFICATE_STRUCTW.szTitle = null;
			cryptui_VIEWCERTIFICATE_STRUCTW.pCertContext = safeCertContext.DangerousGetHandle();
			cryptui_VIEWCERTIFICATE_STRUCTW.rgszPurposes = IntPtr.Zero;
			cryptui_VIEWCERTIFICATE_STRUCTW.cPurposes = 0U;
			cryptui_VIEWCERTIFICATE_STRUCTW.pCryptProviderData = IntPtr.Zero;
			cryptui_VIEWCERTIFICATE_STRUCTW.fpCryptProviderDataTrustedUsage = false;
			cryptui_VIEWCERTIFICATE_STRUCTW.idxSigner = 0U;
			cryptui_VIEWCERTIFICATE_STRUCTW.idxCert = 0U;
			cryptui_VIEWCERTIFICATE_STRUCTW.fCounterSigner = false;
			cryptui_VIEWCERTIFICATE_STRUCTW.idxCounterSigner = 0U;
			cryptui_VIEWCERTIFICATE_STRUCTW.cStores = 0U;
			cryptui_VIEWCERTIFICATE_STRUCTW.rghStores = IntPtr.Zero;
			cryptui_VIEWCERTIFICATE_STRUCTW.cPropSheetPages = 0U;
			cryptui_VIEWCERTIFICATE_STRUCTW.rgPropSheetPages = IntPtr.Zero;
			cryptui_VIEWCERTIFICATE_STRUCTW.nStartPage = 0U;
			if (!CAPI.CryptUIDlgViewCertificateW(cryptui_VIEWCERTIFICATE_STRUCTW, IntPtr.Zero))
			{
				num = Marshal.GetLastWin32Error();
			}
			if (num != 0 && num != 1223)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00014188 File Offset: 0x00012388
		[SecuritySafeCritical]
		private static X509Certificate2Collection SelectFromCollectionHelper(X509Certificate2Collection certificates, string title, string message, X509SelectionFlag selectionFlag, IntPtr hwndParent)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (selectionFlag < X509SelectionFlag.SingleSelection || selectionFlag > X509SelectionFlag.MultiSelection)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Arg_EnumIllegalVal"), new object[]
				{
					"selectionFlag"
				}));
			}
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AllFlags);
			storePermission.Assert();
			X509Certificate2Collection certificates2;
			using (SafeCertStoreHandle safeCertStoreHandle = X509Utils.ExportToMemoryStore(certificates, null))
			{
				using (SafeCertStoreHandle safeCertStoreHandle2 = X509Certificate2UI.SelectFromStore(safeCertStoreHandle, title, message, selectionFlag, hwndParent))
				{
					certificates2 = X509Utils.GetCertificates(safeCertStoreHandle2);
				}
			}
			return certificates2;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00014234 File Offset: 0x00012434
		[SecurityCritical]
		private unsafe static SafeCertStoreHandle SelectFromStore(SafeCertStoreHandle safeSourceStoreHandle, string title, string message, X509SelectionFlag selectionFlags, IntPtr hwndParent)
		{
			int num = 0;
			SafeCertStoreHandle safeCertStoreHandle = CAPI.CertOpenStore((IntPtr)((long)((ulong)2)), 65537U, IntPtr.Zero, 0U, null);
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			CAPI.CRYPTUI_SELECTCERTIFICATE_STRUCTW cryptui_SELECTCERTIFICATE_STRUCTW = new CAPI.CRYPTUI_SELECTCERTIFICATE_STRUCTW();
			cryptui_SELECTCERTIFICATE_STRUCTW.dwSize = (uint)((int)Marshal.OffsetOf(typeof(CAPI.CRYPTUI_SELECTCERTIFICATE_STRUCTW), "hSelectedCertStore"));
			cryptui_SELECTCERTIFICATE_STRUCTW.hwndParent = hwndParent;
			cryptui_SELECTCERTIFICATE_STRUCTW.dwFlags = (uint)selectionFlags;
			cryptui_SELECTCERTIFICATE_STRUCTW.szTitle = title;
			cryptui_SELECTCERTIFICATE_STRUCTW.dwDontUseColumn = 0U;
			cryptui_SELECTCERTIFICATE_STRUCTW.szDisplayString = message;
			cryptui_SELECTCERTIFICATE_STRUCTW.pFilterCallback = IntPtr.Zero;
			cryptui_SELECTCERTIFICATE_STRUCTW.pDisplayCallback = IntPtr.Zero;
			cryptui_SELECTCERTIFICATE_STRUCTW.pvCallbackData = IntPtr.Zero;
			cryptui_SELECTCERTIFICATE_STRUCTW.cDisplayStores = 1U;
			IntPtr intPtr = safeSourceStoreHandle.DangerousGetHandle();
			cryptui_SELECTCERTIFICATE_STRUCTW.rghDisplayStores = new IntPtr((void*)(&intPtr));
			cryptui_SELECTCERTIFICATE_STRUCTW.cStores = 0U;
			cryptui_SELECTCERTIFICATE_STRUCTW.rghStores = IntPtr.Zero;
			cryptui_SELECTCERTIFICATE_STRUCTW.cPropSheetPages = 0U;
			cryptui_SELECTCERTIFICATE_STRUCTW.rgPropSheetPages = IntPtr.Zero;
			cryptui_SELECTCERTIFICATE_STRUCTW.hSelectedCertStore = safeCertStoreHandle.DangerousGetHandle();
			SafeCertContextHandle safeCertContextHandle = CAPI.CryptUIDlgSelectCertificateW(cryptui_SELECTCERTIFICATE_STRUCTW);
			if (safeCertContextHandle != null && !safeCertContextHandle.IsInvalid)
			{
				SafeCertContextHandle invalidHandle = SafeCertContextHandle.InvalidHandle;
				if (!CAPI.CertAddCertificateLinkToStore(safeCertStoreHandle, safeCertContextHandle, 4U, invalidHandle))
				{
					num = Marshal.GetLastWin32Error();
				}
			}
			if (num != 0)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return safeCertStoreHandle;
		}
	}
}
