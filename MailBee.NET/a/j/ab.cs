using System;
using System.Runtime.InteropServices;

namespace a.j
{
	// Token: 0x020001BD RID: 445
	internal class ab
	{
		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003884A File Offset: 0x0003784A
		private ab()
		{
		}

		// Token: 0x020001BE RID: 446
		public class b
		{
			// Token: 0x06000EE5 RID: 3813 RVA: 0x00038852 File Offset: 0x00037852
			private b()
			{
			}

			// Token: 0x06000EE6 RID: 3814
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern int CryptAcquireContext(IntPtr A_0, IntPtr A_1, IntPtr A_2, uint A_3, uint A_4);

			// Token: 0x06000EE7 RID: 3815
			[DllImport("advapi32", SetLastError = true)]
			public static extern int CryptReleaseContext(IntPtr A_0, uint A_1);

			// Token: 0x06000EE8 RID: 3816
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern int CryptEnumProviders(uint A_0, IntPtr A_1, uint A_2, out uint A_3, IntPtr A_4, ref uint A_5);

			// Token: 0x06000EE9 RID: 3817
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern int CryptSetProvider(IntPtr A_0, uint A_1);

			// Token: 0x06000EEA RID: 3818
			[DllImport("advapi32", SetLastError = true)]
			public static extern int CryptGetProvParam(IntPtr A_0, uint A_1, IntPtr A_2, ref uint A_3, uint A_4);

			// Token: 0x06000EEB RID: 3819
			[DllImport("Ncrypt", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern int NCryptFreeObject(IntPtr A_0);

			// Token: 0x06000EEC RID: 3820
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern int CryptGetDefaultProvider(uint A_0, IntPtr A_1, uint A_2, IntPtr A_3, ref uint A_4);
		}

		// Token: 0x020001BF RID: 447
		public class d
		{
			// Token: 0x06000EED RID: 3821 RVA: 0x0003885A File Offset: 0x0003785A
			private d()
			{
			}

			// Token: 0x06000EEE RID: 3822
			[DllImport("crypt32", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern IntPtr CertOpenStore(IntPtr A_0, uint A_1, IntPtr A_2, uint A_3, IntPtr A_4);

			// Token: 0x06000EEF RID: 3823
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CertCloseStore(IntPtr A_0, uint A_1);

			// Token: 0x06000EF0 RID: 3824
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CertGetStoreProperty(IntPtr A_0, uint A_1, IntPtr A_2, out uint A_3);

			// Token: 0x06000EF1 RID: 3825
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CertSaveStore(IntPtr A_0, uint A_1, uint A_2, uint A_3, IntPtr A_4, uint A_5);

			// Token: 0x06000EF2 RID: 3826
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CertAddCertificateContextToStore(IntPtr A_0, IntPtr A_1, uint A_2, IntPtr A_3);

			// Token: 0x06000EF3 RID: 3827
			[DllImport("crypt32", SetLastError = true)]
			public static extern IntPtr CertEnumCertificatesInStore(IntPtr A_0, IntPtr A_1);

			// Token: 0x06000EF4 RID: 3828
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CertDeleteCertificateFromStore(IntPtr A_0);

			// Token: 0x06000EF5 RID: 3829
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CertRegisterSystemStore(IntPtr A_0, uint A_1, IntPtr A_2, IntPtr A_3);

			// Token: 0x06000EF6 RID: 3830
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CertUnregisterSystemStore(IntPtr A_0, uint A_1);
		}

		// Token: 0x020001C0 RID: 448
		public class c
		{
			// Token: 0x06000EF7 RID: 3831 RVA: 0x00038862 File Offset: 0x00037862
			private c()
			{
			}

			// Token: 0x06000EF8 RID: 3832
			[DllImport("crypt32", SetLastError = true)]
			public static extern IntPtr CertCreateCertificateContext(uint A_0, IntPtr A_1, uint A_2);

			// Token: 0x06000EF9 RID: 3833
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CertFreeCertificateContext(IntPtr A_0);

			// Token: 0x06000EFA RID: 3834
			[DllImport("crypt32", SetLastError = true)]
			public static extern IntPtr CertDuplicateCertificateContext(IntPtr A_0);

			// Token: 0x06000EFB RID: 3835
			[DllImport("crypt32", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern IntPtr PFXImportCertStore(IntPtr A_0, string A_1, uint A_2);

			// Token: 0x06000EFC RID: 3836
			[DllImport("crypt32", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern int PFXExportCertStoreEx(IntPtr A_0, IntPtr A_1, string A_2, IntPtr A_3, uint A_4);

			// Token: 0x06000EFD RID: 3837
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CertGetCertificateChain(IntPtr A_0, IntPtr A_1, IntPtr A_2, IntPtr A_3, IntPtr A_4, uint A_5, IntPtr A_6, ref IntPtr A_7);

			// Token: 0x06000EFE RID: 3838
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CertFreeCertificateChain(IntPtr A_0);
		}

		// Token: 0x020001C1 RID: 449
		public class e
		{
			// Token: 0x06000EFF RID: 3839
			[DllImport("crypt32", SetLastError = true)]
			public static extern uint CertOIDToAlgId(IntPtr A_0);

			// Token: 0x06000F00 RID: 3840
			[DllImport("crypt32", SetLastError = true)]
			public static extern IntPtr CertAlgIdToOID(uint A_0);
		}

		// Token: 0x020001C2 RID: 450
		public class a
		{
			// Token: 0x06000F02 RID: 3842
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CryptDecryptMessage(IntPtr A_0, IntPtr A_1, uint A_2, IntPtr A_3, ref uint A_4, ref IntPtr A_5);

			// Token: 0x06000F03 RID: 3843
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CryptEncryptMessage(IntPtr A_0, uint A_1, IntPtr A_2, IntPtr A_3, uint A_4, IntPtr A_5, ref uint A_6);

			// Token: 0x06000F04 RID: 3844
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CryptSignMessage(IntPtr A_0, int A_1, uint A_2, IntPtr A_3, IntPtr A_4, IntPtr A_5, ref uint A_6);

			// Token: 0x06000F05 RID: 3845
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CryptVerifyDetachedMessageSignature(IntPtr A_0, uint A_1, IntPtr A_2, uint A_3, uint A_4, IntPtr A_5, ref uint A_6, ref IntPtr A_7);

			// Token: 0x06000F06 RID: 3846
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CryptVerifyMessageSignature(IntPtr A_0, uint A_1, IntPtr A_2, uint A_3, IntPtr A_4, ref uint A_5, ref IntPtr A_6);

			// Token: 0x06000F07 RID: 3847
			[DllImport("Crypt32", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern IntPtr CryptGetMessageCertificates(uint A_0, IntPtr A_1, uint A_2, IntPtr A_3, uint A_4);

			// Token: 0x06000F08 RID: 3848
			[DllImport("crypt32", SetLastError = true)]
			public static extern IntPtr CryptMsgOpenToDecode(uint A_0, uint A_1, uint A_2, IntPtr A_3, IntPtr A_4, IntPtr A_5);

			// Token: 0x06000F09 RID: 3849
			[DllImport("crypt32", SetLastError = true)]
			public static extern IntPtr CryptMsgOpenToEncode(uint A_0, uint A_1, uint A_2, IntPtr A_3, IntPtr A_4, IntPtr A_5);

			// Token: 0x06000F0A RID: 3850
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CryptMsgUpdate(IntPtr A_0, IntPtr A_1, uint A_2, int A_3);

			// Token: 0x06000F0B RID: 3851
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CryptMsgGetParam(IntPtr A_0, uint A_1, uint A_2, IntPtr A_3, ref uint A_4);

			// Token: 0x06000F0C RID: 3852
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CryptMsgControl(IntPtr A_0, uint A_1, uint A_2, IntPtr A_3);

			// Token: 0x06000F0D RID: 3853
			[DllImport("crypt32", SetLastError = true)]
			public static extern int CryptMsgClose(IntPtr A_0);

			// Token: 0x06000F0E RID: 3854
			[DllImport("crypt32", SetLastError = true)]
			public static extern uint CryptMsgCalculateEncodedLength(uint A_0, uint A_1, uint A_2, IntPtr A_3, string A_4, uint A_5);
		}
	}
}
