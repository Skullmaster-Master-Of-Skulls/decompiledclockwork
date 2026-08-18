using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using a;
using a.j;

namespace MailBee.Security
{
	// Token: 0x020000FF RID: 255
	public class CertificateStore : IDisposable
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x000278A0 File Offset: 0x000268A0
		public CertificateStore()
		{
			this.a(null, CertStoreType.Memory, null, null, null, RegistryStoreLocation.CurrentUser);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000278CB File Offset: 0x000268CB
		internal CertificateStore(IntPtr A_0)
		{
			this.b = A_0;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000278EC File Offset: 0x000268EC
		public CertificateStore(string storeName, CertStoreType storeType, string pfxPassword)
		{
			this.a(storeName, storeType, pfxPassword, null, null, RegistryStoreLocation.CurrentUser);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00027917 File Offset: 0x00026917
		public CertificateStore(string storeName, CertStoreType storeType, string pfxPassword, CryptoServiceProvider csp, RegistryStoreLocation registryLocation)
		{
			this.a(storeName, storeType, pfxPassword, null, csp, registryLocation);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00027940 File Offset: 0x00026940
		public CertificateStore(byte[] storeData, CertStoreType storeType, CryptoServiceProvider csp)
		{
			this.a(null, storeType, null, storeData, csp, RegistryStoreLocation.CurrentUser);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002796B File Offset: 0x0002696B
		public CertificateStore(byte[] pfxData, string pfxPassword)
		{
			this.a(null, CertStoreType.PfxBytes, pfxPassword, pfxData, null, RegistryStoreLocation.CurrentUser);
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00027997 File Offset: 0x00026997
		internal IntPtr Handle
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0002799F File Offset: 0x0002699F
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x000279A7 File Offset: 0x000269A7
		public bool ThrowExceptions
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x000279B0 File Offset: 0x000269B0
		public int LastResult
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000279B8 File Offset: 0x000269B8
		[SecuritySafeCritical]
		public CertificateCollection GetAllCertificates()
		{
			CertificateCollection certificateCollection = new CertificateCollection();
			IntPtr intPtr = IntPtr.Zero;
			this.d = 0;
			while (IntPtr.Zero != (intPtr = global::a.j.ab.d.CertEnumCertificatesInStore(this.b, intPtr)))
			{
				Certificate cert = new Certificate(intPtr);
				certificateCollection.Add(cert);
			}
			return certificateCollection;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00027A08 File Offset: 0x00026A08
		[SecuritySafeCritical]
		public bool AddCertificate(Certificate cert, bool overwrite)
		{
			if (cert == null)
			{
				this.d = 21;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			this.d = 0;
			uint a_ = overwrite ? 3U : 2U;
			if (global::a.j.ab.d.CertAddCertificateContextToStore(this.b, cert.Handle, a_, IntPtr.Zero) != 0)
			{
				return true;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			this.d = 1101;
			if (this.c)
			{
				throw new MailBeeCertificateStoreWin32Exception(lastWin32Error);
			}
			return false;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00027A78 File Offset: 0x00026A78
		public bool AddCertificates(CertificateCollection certs, bool overwrite)
		{
			if (certs == null)
			{
				this.d = 21;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			this.d = 0;
			foreach (object obj in certs)
			{
				Certificate cert = (Certificate)obj;
				if (!this.AddCertificate(cert, overwrite))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00027AF4 File Offset: 0x00026AF4
		[SecuritySafeCritical]
		public bool DeleteCertificate(Certificate cert)
		{
			if (cert == null)
			{
				this.d = 21;
				throw new MailBeeInvalidArgumentException(21);
			}
			this.d = 0;
			if (global::a.j.ab.d.CertDeleteCertificateFromStore(cert.Handle) != 0)
			{
				return true;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			this.d = 1101;
			if (this.c)
			{
				throw new MailBeeCertificateStoreWin32Exception(lastWin32Error);
			}
			return false;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00027B4C File Offset: 0x00026B4C
		public CertificateCollection FindCertificates(string substring, CertificateFields fields)
		{
			if (substring == null)
			{
				this.d = 21;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			this.d = 0;
			CertificateCollection certificateCollection = new CertificateCollection();
			CollectionBase allCertificates = this.GetAllCertificates();
			substring = substring.ToLower();
			foreach (object obj in allCertificates)
			{
				Certificate certificate = (Certificate)obj;
				bool flag = false;
				if ((fields & CertificateFields.EmailAddress) > (CertificateFields)0)
				{
					if (certificate.EmailAddress != null && certificate.EmailAddress.ToLower().IndexOf(substring) != -1)
					{
						flag = true;
					}
					if (certificate.SubjectAlternativeName != null && certificate.SubjectAlternativeName.ToLower().IndexOf(substring) != -1)
					{
						flag = true;
					}
				}
				if ((fields & CertificateFields.Issuer) > (CertificateFields)0 && certificate.IssuerDetails != null && certificate.IssuerDetails.ToLower().IndexOf(substring) != -1)
				{
					flag = true;
				}
				if ((fields & CertificateFields.Name) > (CertificateFields)0 && certificate.Name != null && certificate.Name.ToLower().IndexOf(substring) != -1)
				{
					flag = true;
				}
				if ((fields & CertificateFields.PublicKey) > (CertificateFields)0 && certificate.PublicKeyString != null && certificate.PublicKeyString.ToLower().IndexOf(substring) != -1)
				{
					flag = true;
				}
				if ((fields & CertificateFields.SerialNumber) > (CertificateFields)0 && certificate.SerialNumberString != null && certificate.SerialNumberString.ToLower().IndexOf(substring) != -1)
				{
					flag = true;
				}
				if ((fields & CertificateFields.Subject) > (CertificateFields)0 && certificate.Subject != null && certificate.Subject.ToLower().IndexOf(substring) != -1)
				{
					flag = true;
				}
				if ((fields & CertificateFields.Thumbprint) > (CertificateFields)0 && certificate.Thumbprint != null && certificate.Thumbprint.ToLower().IndexOf(substring) != -1)
				{
					flag = true;
				}
				if (flag)
				{
					certificateCollection.Add(certificate);
				}
			}
			return certificateCollection;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00027D0C File Offset: 0x00026D0C
		[SecuritySafeCritical]
		public bool SaveToFile(string filename, CertStoreFileType fileType, string pfxPassword)
		{
			if (filename == null)
			{
				this.d = 21;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			this.d = 0;
			IntPtr intPtr = IntPtr.Zero;
			if (fileType == CertStoreFileType.Pfx)
			{
				IntPtr intPtr2 = IntPtr.Zero;
				global::a.j.g g = default(global::a.j.g);
				g.b = IntPtr.Zero;
				g.a = 0U;
				try
				{
					intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(g));
					Marshal.StructureToPtr(g, intPtr2, true);
					if (global::a.j.ab.c.PFXExportCertStoreEx(this.b, intPtr2, pfxPassword, IntPtr.Zero, 0U) == 0)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						this.d = 1101;
						if (this.c)
						{
							throw new MailBeeCertificateStoreWin32Exception(lastWin32Error);
						}
						return false;
					}
					else
					{
						g = global::a.j.g.c(intPtr2);
						g.b = Marshal.AllocHGlobal((int)g.a);
						Marshal.StructureToPtr(g, intPtr2, true);
						if (global::a.j.ab.c.PFXExportCertStoreEx(this.b, intPtr2, pfxPassword, IntPtr.Zero, 0U) != 0)
						{
							g = global::a.j.g.c(intPtr2);
							byte[] array = new byte[g.a];
							Marshal.Copy(g.b, array, 0, (int)g.a);
							return global::a.ap.b(filename, array, null);
						}
						int lastWin32Error2 = Marshal.GetLastWin32Error();
						this.d = 1101;
						if (this.c)
						{
							throw new MailBeeCertificateStoreWin32Exception(lastWin32Error2);
						}
						return false;
					}
				}
				finally
				{
					if (intPtr2 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr2);
					}
					if (g.b != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(g.b);
					}
				}
			}
			bool result;
			try
			{
				intPtr = Marshal.StringToHGlobalUni(filename);
				if (global::a.j.ab.d.CertSaveStore(this.b, 65537U, (uint)fileType, 4U, intPtr, 0U) == 0)
				{
					int lastWin32Error3 = Marshal.GetLastWin32Error();
					this.d = 1101;
					if (this.c)
					{
						throw new MailBeeCertificateStoreWin32Exception(lastWin32Error3);
					}
					result = false;
				}
				else
				{
					result = true;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00027F0C File Offset: 0x00026F0C
		[SecuritySafeCritical]
		public static void RegisterSystemStore(string name, RegistryStoreLocation registryLocation)
		{
			if (name == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalUni(name);
				if (global::a.j.ab.d.CertRegisterSystemStore(intPtr, (uint)registryLocation, IntPtr.Zero, IntPtr.Zero) == 0)
				{
					throw new MailBeeCertificateStoreWin32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00027F78 File Offset: 0x00026F78
		[SecuritySafeCritical]
		public static void UnregisterSystemStore(string name, RegistryStoreLocation registryLocation)
		{
			if (name == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalUni(name);
				if (global::a.j.ab.d.CertUnregisterSystemStore(intPtr, (uint)(registryLocation | (RegistryStoreLocation)16)) == 0)
				{
					throw new MailBeeCertificateStoreWin32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00027FDC File Offset: 0x00026FDC
		public void Dispose()
		{
			this.a(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00027FEC File Offset: 0x00026FEC
		[SecuritySafeCritical]
		private void a(bool A_0)
		{
			this.d = 0;
			if (!this.a && this.b != IntPtr.Zero && global::a.j.ab.d.CertCloseStore(this.b, 0U) == 0)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				this.d = 1102;
				if (this.c)
				{
					throw new MailBeeCertificateStoreWin32Exception(lastWin32Error);
				}
			}
			this.a = true;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00028054 File Offset: 0x00027054
		[SecuritySafeCritical]
		private bool a(string A_0, CertStoreType A_1, string A_2, byte[] A_3, CryptoServiceProvider A_4, RegistryStoreLocation A_5)
		{
			if ((A_0 == null || A_0 == string.Empty) && A_1 != CertStoreType.Memory && A_1 != CertStoreType.Pkcs7Bytes && A_1 != CertStoreType.SerializedBytes && A_1 != CertStoreType.PfxBytes)
			{
				this.d = 22;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			if (A_3 == null && (A_1 == CertStoreType.Pkcs7Bytes || A_1 == CertStoreType.SerializedBytes || A_1 == CertStoreType.PfxBytes))
			{
				this.d = 21;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			this.d = 0;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			global::a.j.g g = default(global::a.j.g);
			if (A_1 != CertStoreType.Pkcs7Bytes && A_1 != CertStoreType.SerializedBytes)
			{
				intPtr = Marshal.StringToHGlobalUni(A_0);
			}
			else
			{
				intPtr2 = Marshal.AllocHGlobal(A_3.Length);
				Marshal.Copy(A_3, 0, intPtr2, A_3.Length);
				g.b = intPtr2;
				g.a = (uint)A_3.Length;
				intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(g));
				Marshal.StructureToPtr(g, intPtr, false);
			}
			bool result;
			try
			{
				uint num = 0U;
				uint a_ = 0U;
				if (A_1 <= CertStoreType.PublicFile)
				{
					if (A_1 != CertStoreType.Pkcs7Bytes)
					{
						if (A_1 == CertStoreType.PublicFile)
						{
							a_ = 65537U;
						}
					}
					else
					{
						a_ = 65537U;
					}
				}
				else if (A_1 != CertStoreType.System)
				{
					if (A_1 == CertStoreType.PfxFile || A_1 == CertStoreType.PfxBytes)
					{
						try
						{
							if (A_1 == CertStoreType.PfxFile)
							{
								A_3 = global::a.ap.e(A_0);
							}
							this.b = CertificateStore.a(A_3, A_2);
						}
						catch (MailBeeException ex)
						{
							this.d = ex.ErrorCode;
							if (this.c)
							{
								throw;
							}
							return false;
						}
						return true;
					}
				}
				else
				{
					num |= (uint)A_5;
				}
				this.b = global::a.j.ab.d.CertOpenStore((IntPtr)((int)A_1), a_, (A_4 != null) ? A_4.Handle : IntPtr.Zero, num, intPtr);
				if (this.b == IntPtr.Zero)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					num |= 131072U;
					this.b = global::a.j.ab.d.CertOpenStore((IntPtr)((int)A_1), a_, (A_4 != null) ? A_4.Handle : IntPtr.Zero, num, intPtr);
					if (this.b == IntPtr.Zero)
					{
						this.d = 1101;
						if (this.c)
						{
							throw new MailBeeCertificateStoreWin32Exception(lastWin32Error);
						}
						return false;
					}
				}
				result = true;
			}
			finally
			{
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000282B8 File Offset: 0x000272B8
		[SecuritySafeCritical]
		internal static IntPtr a(byte[] A_0, string A_1)
		{
			IntPtr intPtr = IntPtr.Zero;
			uint num = (uint)A_0.Length;
			global::a.j.g g = default(global::a.j.g);
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			IntPtr result;
			try
			{
				intPtr = Marshal.AllocHGlobal((int)num);
				Marshal.Copy(A_0, 0, intPtr, (int)num);
				g.b = intPtr;
				g.a = num;
				intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(g));
				Marshal.StructureToPtr(g, intPtr2, false);
				intPtr3 = global::a.j.ab.c.PFXImportCertStore(intPtr2, (A_1 != null && A_1.Length > 0) ? A_1 : null, 0U);
				if (intPtr3 == IntPtr.Zero)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					intPtr3 = global::a.j.ab.c.PFXImportCertStore(intPtr2, (A_1 != null && A_1.Length > 0) ? A_1 : null, 32U);
					if (intPtr3 == IntPtr.Zero)
					{
						throw new MailBeeCertificateStoreWin32Exception(lastWin32Error);
					}
				}
				result = intPtr3;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
			return result;
		}

		// Token: 0x040006C9 RID: 1737
		public const string Personal = "MY";

		// Token: 0x040006CA RID: 1738
		public const string OtherPeople = "AddressBook";

		// Token: 0x040006CB RID: 1739
		public const string IntermediateCA = "CA";

		// Token: 0x040006CC RID: 1740
		public const string RootCA = "Root";

		// Token: 0x040006CD RID: 1741
		private bool a;

		// Token: 0x040006CE RID: 1742
		private IntPtr b = IntPtr.Zero;

		// Token: 0x040006CF RID: 1743
		private bool c = true;

		// Token: 0x040006D0 RID: 1744
		private int d;
	}
}
