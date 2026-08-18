using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using a;
using a.i;
using a.j;

namespace MailBee.Security
{
	// Token: 0x020000F9 RID: 249
	public class Certificate : IDisposable
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x00026483 File Offset: 0x00025483
		private Certificate()
		{
			if (Powerup.License == null)
			{
				Powerup.a(null);
			}
			if (!Powerup.License.d())
			{
				throw new MailBeeLicenseException(Powerup.License, typeof(Powerup));
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000264C0 File Offset: 0x000254C0
		public Certificate(X509Certificate2 x509Cert2) : this()
		{
			if (x509Cert2 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.a = x509Cert2;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000264DC File Offset: 0x000254DC
		[SecuritySafeCritical]
		internal Certificate(IntPtr A_0) : this()
		{
			try
			{
				this.a = new X509Certificate2(A_0);
			}
			catch (CryptographicException a_)
			{
				throw new MailBeeCertificateParsingException(a_);
			}
			this.c = this.a();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00026520 File Offset: 0x00025520
		internal Certificate(string A_0, CertFileType A_1) : this(A_0, A_1, null)
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002652B File Offset: 0x0002552B
		internal Certificate(byte[] A_0, CertFileType A_1) : this(A_0, A_1, null)
		{
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00026538 File Offset: 0x00025538
		private void b(byte[] A_0, CertFileType A_1, string A_2)
		{
			if (global::a.w.b(A_0, 0, A_0.Length, Encoding.ASCII.GetBytes("-----BEGIN CERTIFICATE-----")) == 0)
			{
				A_0 = global::a.w.b(A_0, 0, A_0.Length, Encoding.ASCII.GetBytes("-----BEGIN CERTIFICATE-----"), new byte[0]);
				A_0 = global::a.w.b(A_0, 0, A_0.Length, Encoding.ASCII.GetBytes("-----END CERTIFICATE-----"), new byte[0]);
				A_0 = global::a.i.h.b(A_0);
			}
			this.a(A_0, A_1, A_2);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000265B4 File Offset: 0x000255B4
		public Certificate(string filename, CertFileType fileType, string pfxFilePassword) : this()
		{
			if (filename == null || filename == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			byte[] a_ = global::a.ap.e(filename);
			this.b(a_, fileType, pfxFilePassword);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000265F0 File Offset: 0x000255F0
		public static Task<Certificate> LoadFromFileAsync(string filename, CertFileType fileType, string pfxFilePassword)
		{
			Certificate.c c;
			c.c = filename;
			c.d = fileType;
			c.e = pfxFilePassword;
			c.b = AsyncTaskMethodBuilder<Certificate>.Create();
			c.a = -1;
			AsyncTaskMethodBuilder<Certificate> asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<Certificate.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00026645 File Offset: 0x00025645
		public Certificate(byte[] bytes, CertFileType fileType, string pfxFilePassword) : this()
		{
			if (bytes == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.a(bytes, fileType, pfxFilePassword);
		}

		// Token: 0x17000298 RID: 664
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00026661 File Offset: 0x00025661
		internal bool NameMismatch
		{
			set
			{
				this.f = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0002666A File Offset: 0x0002566A
		internal IntPtr Handle
		{
			[SecuritySafeCritical]
			get
			{
				return this.a.Handle;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00026677 File Offset: 0x00025677
		public byte[] SerialNumber
		{
			get
			{
				return this.a.GetSerialNumber();
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00026684 File Offset: 0x00025684
		public string SerialNumberString
		{
			get
			{
				return this.a.GetSerialNumberString();
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00026691 File Offset: 0x00025691
		public Algorithm SignatureAlgorithm
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00026699 File Offset: 0x00025699
		public string KeyAlgorithmString
		{
			get
			{
				return this.a.GetKeyAlgorithm();
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000266A6 File Offset: 0x000256A6
		public string IssuerDetails
		{
			get
			{
				return this.a.Issuer;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x000266B4 File Offset: 0x000256B4
		public string IssuedTo
		{
			get
			{
				string[] array = this.a.Subject.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						'='
					});
					if (array2[0].Trim().ToUpper() == "CN" && array2.Length > 1)
					{
						return array2[1].Trim();
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0002672C File Offset: 0x0002572C
		public string IssuedBy
		{
			get
			{
				string[] array = this.a.Issuer.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						'='
					});
					if (array2[0].Trim().ToUpper() == "CN" && array2.Length > 1)
					{
						return array2[1].Trim();
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x000267A4 File Offset: 0x000257A4
		public string Name
		{
			get
			{
				return this.a.Subject;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x000267B1 File Offset: 0x000257B1
		public DateTime ValidFromDate
		{
			get
			{
				return this.a.NotBefore;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x000267BE File Offset: 0x000257BE
		public DateTime ValidToDate
		{
			get
			{
				return this.a.NotAfter;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x000267CC File Offset: 0x000257CC
		public string Subject
		{
			get
			{
				string[] array = this.a.SubjectName.Decode(X500DistinguishedNameFlags.None).Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IndexOf('=') != -1)
					{
						array[i] = array[i].Split(new char[]
						{
							'='
						})[1].Trim();
					}
				}
				return string.Join(", ", array);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00026840 File Offset: 0x00025840
		public string SubjectAlternativeName
		{
			get
			{
				if (this.a.Extensions != null)
				{
					foreach (X509Extension x509Extension in this.a.Extensions)
					{
						if (x509Extension.Oid != null && x509Extension.Oid.Value == "2.5.29.17")
						{
							return x509Extension.Format(true);
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000268A4 File Offset: 0x000258A4
		public byte[] PublicKey
		{
			get
			{
				return this.a.GetPublicKey();
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x000268B1 File Offset: 0x000258B1
		public string PublicKeyString
		{
			get
			{
				return this.a.GetPublicKeyString();
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000268BE File Offset: 0x000258BE
		public X509Certificate2 AsX509Certificate
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000268C6 File Offset: 0x000258C6
		public byte[] RawData
		{
			get
			{
				return this.a.GetRawCertData();
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000268D4 File Offset: 0x000258D4
		public string EmailAddress
		{
			get
			{
				string[] array = this.a.Subject.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						'='
					});
					if (array2[0].Trim().ToUpper() == "E" && array2.Length > 1)
					{
						return array2[1].Trim();
					}
				}
				if (this.SubjectAlternativeName != null)
				{
					array = this.SubjectAlternativeName.Split(new char[]
					{
						'\n'
					});
					for (int j = 0; j < array.Length; j++)
					{
						string[] array3 = array[j].Split(new char[]
						{
							'='
						});
						if (array3[0].Trim().ToUpper() == "RFC822 NAME" && array3.Length > 1)
						{
							return array3[1].Trim();
						}
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000269B9 File Offset: 0x000259B9
		public bool HasPrivateKey
		{
			get
			{
				return this.a.HasPrivateKey;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000269C6 File Offset: 0x000259C6
		public string Thumbprint
		{
			get
			{
				return this.a.Thumbprint;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x000269D3 File Offset: 0x000259D3
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x000269DB File Offset: 0x000259DB
		public bool ThrowExceptions
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x000269E4 File Offset: 0x000259E4
		public int LastResult
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000269EC File Offset: 0x000259EC
		public bool SaveToFile(string filename, CertFileType fileType, string pfxPassword)
		{
			if (filename == null || filename == string.Empty)
			{
				this.e = 22;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			this.e = 0;
			if (fileType == CertFileType.P7b)
			{
				return this.a(filename);
			}
			if (fileType != CertFileType.Pfx)
			{
				return this.b(filename);
			}
			return this.a(filename, pfxPassword);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00026A48 File Offset: 0x00025A48
		private Task<bool> d(string A_0)
		{
			Certificate.a a;
			a.c = this;
			a.d = A_0;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<Certificate.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00026A98 File Offset: 0x00025A98
		private Task<bool> c(string A_0)
		{
			Certificate.d d;
			d.c = this;
			d.d = A_0;
			d.b = AsyncTaskMethodBuilder<bool>.Create();
			d.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<Certificate.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00026AE8 File Offset: 0x00025AE8
		private Task<bool> b(string A_0, string A_1)
		{
			Certificate.b b;
			b.c = this;
			b.e = A_0;
			b.d = A_1;
			b.b = AsyncTaskMethodBuilder<bool>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<Certificate.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00026B40 File Offset: 0x00025B40
		public Task<bool> SaveToFileAsync(string filename, CertFileType fileType, string pfxPassword)
		{
			if (filename == null || filename == string.Empty)
			{
				this.e = 22;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			this.e = 0;
			if (fileType == CertFileType.P7b)
			{
				return this.c(filename);
			}
			if (fileType != CertFileType.Pfx)
			{
				return this.d(filename);
			}
			return this.b(filename, pfxPassword);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00026B9A File Offset: 0x00025B9A
		public CertificateValidationFlags Validate()
		{
			return this.Validate(null);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00026BA4 File Offset: 0x00025BA4
		[SecuritySafeCritical]
		public CertificateValidationFlags Validate(CertificateStore extraStore)
		{
			CertificateValidationFlags certificateValidationFlags = this.f ? CertificateValidationFlags.NameMismatch : CertificateValidationFlags.None;
			if (extraStore == null)
			{
				X509Chain x509Chain = new X509Chain();
				try
				{
					x509Chain.Build(this.AsX509Certificate);
				}
				catch (CryptographicException a_)
				{
					throw new MailBeeCertificateException(35, a_);
				}
				foreach (X509ChainStatus x509ChainStatus in x509Chain.ChainStatus)
				{
					if ((x509ChainStatus.Status & X509ChainStatusFlags.NotTimeValid) == X509ChainStatusFlags.NotTimeValid)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotTimeValid;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.NotTimeNested) == X509ChainStatusFlags.NotTimeNested)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotTimeNested;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.Revoked) == X509ChainStatusFlags.Revoked)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsRevoked;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.NotSignatureValid) == X509ChainStatusFlags.NotSignatureValid)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotSignatureValid;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.NotValidForUsage) == X509ChainStatusFlags.NotValidForUsage)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotValidForUsage;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.UntrustedRoot) == X509ChainStatusFlags.UntrustedRoot)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsUntrustedRoot;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.RevocationStatusUnknown) == X509ChainStatusFlags.RevocationStatusUnknown)
					{
						certificateValidationFlags |= CertificateValidationFlags.RevocationStatusUnknown;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.Cyclic) == X509ChainStatusFlags.Cyclic)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsCyclic;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.PartialChain)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsPartialChain;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.CtlNotTimeValid) == X509ChainStatusFlags.CtlNotTimeValid)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotTimeValidCtl;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.CtlNotSignatureValid) == X509ChainStatusFlags.CtlNotSignatureValid)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotSignatureValidCtl;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.CtlNotValidForUsage) == X509ChainStatusFlags.CtlNotValidForUsage)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotValidForUsageCtl;
					}
				}
				return certificateValidationFlags;
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			global::a.j.x x = default(global::a.j.x);
			global::a.j.au au = default(global::a.j.au);
			global::a.j.o o = default(global::a.j.o);
			uint a_2 = 0U;
			x.a = 0U;
			x.b = IntPtr.Zero;
			au.a = 0U;
			au.b = x;
			o.a = (uint)Marshal.SizeOf(o);
			o.b = au;
			try
			{
				intPtr = Marshal.AllocHGlobal((int)o.a);
				Marshal.StructureToPtr(o, intPtr, true);
				if (global::a.j.ab.c.CertGetCertificateChain(IntPtr.Zero, this.Handle, IntPtr.Zero, (extraStore == null) ? IntPtr.Zero : extraStore.Handle, intPtr, a_2, IntPtr.Zero, ref zero) == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					this.e = 1102;
					if (this.d)
					{
						throw new MailBeeCertificateWin32Exception(lastWin32Error);
					}
				}
				global::a.j.ah ah = (global::a.j.ah)Marshal.PtrToStructure(zero, typeof(global::a.j.ah));
				if ((ah.b.a & 1U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsNotTimeValid;
				}
				if ((ah.b.a & 2U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsNotTimeNested;
				}
				if ((ah.b.a & 4U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsRevoked;
				}
				if ((ah.b.a & 8U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsNotSignatureValid;
				}
				if ((ah.b.a & 16U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsNotValidForUsage;
				}
				if ((ah.b.a & 32U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsUntrustedRoot;
				}
				if ((ah.b.a & 64U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.RevocationStatusUnknown;
				}
				if ((ah.b.a & 128U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsCyclic;
				}
				if ((ah.b.a & 65536U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsPartialChain;
				}
				if ((ah.b.a & 131072U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsNotTimeValidCtl;
				}
				if ((ah.b.a & 262144U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsNotSignatureValidCtl;
				}
				if ((ah.b.a & 524288U) > 0U)
				{
					certificateValidationFlags |= CertificateValidationFlags.IsNotValidForUsageCtl;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (zero != IntPtr.Zero)
				{
					global::a.j.ab.c.CertFreeCertificateChain(zero);
				}
			}
			return certificateValidationFlags;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00026FB0 File Offset: 0x00025FB0
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00026FB8 File Offset: 0x00025FB8
		private void a(byte[] A_0, CertFileType A_1, string A_2)
		{
			this.a = null;
			this.b = null;
			switch (A_1)
			{
			case CertFileType.Cer:
				try
				{
					this.a = new X509Certificate2(A_0);
					goto IL_BF;
				}
				catch (CryptographicException a_)
				{
					throw new MailBeeCertificateParsingException(a_);
				}
				break;
			case CertFileType.P7b:
				break;
			case CertFileType.Pfx:
				try
				{
					this.a = new X509Certificate2(A_0, A_2);
				}
				catch (CryptographicException a_2)
				{
					try
					{
						this.a = new X509Certificate2(A_0, A_2, X509KeyStorageFlags.MachineKeySet);
					}
					catch (CryptographicException)
					{
						throw new MailBeeCertificateParsingException(a_2);
					}
				}
				goto IL_BF;
			default:
				goto IL_BF;
			}
			this.b = new SignedCms();
			try
			{
				this.b.Decode(A_0);
				if (this.b.Certificates.Count > 0)
				{
					this.a = this.b.Certificates[0];
				}
			}
			catch (CryptographicException a_3)
			{
				throw new MailBeeCertificateParsingException(a_3);
			}
			if (this.a == null)
			{
				throw new MailBeeCertificateParsingException();
			}
			IL_BF:
			this.c = this.a();
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000270C4 File Offset: 0x000260C4
		private bool b(string A_0)
		{
			byte[] a_ = this.a.Export(X509ContentType.Cert);
			try
			{
				global::a.ap.b(A_0, a_, null);
			}
			catch (MailBeeException ex)
			{
				this.e = ex.ErrorCode;
				if (this.d)
				{
					throw;
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00027118 File Offset: 0x00026118
		private bool a(string A_0)
		{
			byte[] a_;
			if (this.b != null)
			{
				a_ = this.b.Encode();
			}
			else
			{
				a_ = new X509Certificate2Collection
				{
					this.a
				}.Export(X509ContentType.Pkcs7);
			}
			try
			{
				global::a.ap.b(A_0, a_, null);
			}
			catch (MailBeeException ex)
			{
				this.e = ex.ErrorCode;
				if (this.d)
				{
					throw;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00027190 File Offset: 0x00026190
		private bool a(string A_0, string A_1)
		{
			byte[] a_ = this.a.Export(X509ContentType.Pfx, A_1);
			try
			{
				global::a.ap.b(A_0, a_, null);
			}
			catch (MailBeeException ex)
			{
				this.e = ex.ErrorCode;
				if (this.d)
				{
					throw;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000271E4 File Offset: 0x000261E4
		private Algorithm a()
		{
			uint a_ = Algorithm.a(this.a.SignatureAlgorithm.Value);
			return new Algorithm(a_, 0, Algorithm.c(a_), this.a.SignatureAlgorithm.FriendlyName, Algorithm.b(a_));
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002722C File Offset: 0x0002622C
		[SecuritySafeCritical]
		internal static bool a(Certificate A_0)
		{
			X509Chain x509Chain = new X509Chain();
			x509Chain.Build(A_0.AsX509Certificate);
			x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.Offline;
			foreach (X509ChainStatus x509ChainStatus in x509Chain.ChainStatus)
			{
				if (x509ChainStatus.Status == X509ChainStatusFlags.Revoked)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00027280 File Offset: 0x00026280
		[SecuritySafeCritical]
		internal static IntPtr a(IntPtr A_0)
		{
			return global::a.j.ab.c.CertDuplicateCertificateContext(A_0);
		}

		// Token: 0x040006AD RID: 1709
		private X509Certificate2 a;

		// Token: 0x040006AE RID: 1710
		private SignedCms b;

		// Token: 0x040006AF RID: 1711
		private Algorithm c;

		// Token: 0x040006B0 RID: 1712
		private bool d = true;

		// Token: 0x040006B1 RID: 1713
		private int e;

		// Token: 0x040006B2 RID: 1714
		private bool f;
	}
}
