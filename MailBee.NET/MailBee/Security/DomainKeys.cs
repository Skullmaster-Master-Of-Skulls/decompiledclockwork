using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using a;
using a.i;
using a.j;
using MailBee.Mime;
using MailBee.SmtpMail;

namespace MailBee.Security
{
	// Token: 0x02000104 RID: 260
	public class DomainKeys
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00028B20 File Offset: 0x00027B20
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00028B28 File Offset: 0x00027B28
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

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00028B31 File Offset: 0x00027B31
		public int LastResult
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00028B39 File Offset: 0x00027B39
		public DomainKeys()
		{
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00028B48 File Offset: 0x00027B48
		public DomainKeys(bool isWebApp)
		{
			if (isWebApp)
			{
				this.e = new CspParameters();
				this.e.Flags = CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00028B71 File Offset: 0x00027B71
		public MailMessage Sign(MailMessage msg, string[] headersToSign, string privateKeyStr, bool isFilename, string selector)
		{
			return this.Sign(msg, headersToSign, privateKeyStr, isFilename, selector, DomainKeysTypes.Both);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00028B81 File Offset: 0x00027B81
		public MailMessage Sign(MailMessage msg, string[] headersToSign, string privateKeyStr, bool isFilename, string selector, DomainKeysTypes dkTypes)
		{
			return this.a(msg, true, headersToSign, privateKeyStr, isFilename, selector, dkTypes);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00028B93 File Offset: 0x00027B93
		public MailMessage Sign(MailMessage msg, string[] headersToSign, byte[] privateKey, string selector)
		{
			return this.Sign(msg, headersToSign, privateKey, selector, DomainKeysTypes.Both);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00028BA1 File Offset: 0x00027BA1
		public MailMessage Sign(MailMessage msg, string[] headersToSign, byte[] privateKey, string selector, DomainKeysTypes dkTypes)
		{
			return this.a(msg, true, headersToSign, privateKey, selector, dkTypes);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00028BB4 File Offset: 0x00027BB4
		internal MailMessage a(MailMessage A_0, bool A_1, string[] A_2, string A_3, bool A_4, string A_5, DomainKeysTypes A_6)
		{
			byte[] array = this.a(A_3, A_4);
			if (array == null)
			{
				return null;
			}
			return this.a(A_0, A_1, A_2, array, A_5, A_6);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00028BE0 File Offset: 0x00027BE0
		internal MailMessage a(MailMessage A_0, bool A_1, string[] A_2, byte[] A_3, string A_4, DomainKeysTypes A_5)
		{
			global::a.j.ae ae = ((A_5 & DomainKeysTypes.DK) > (DomainKeysTypes)0) ? new global::a.j.ae(this.d, this.c) : null;
			global::a.j.al al = ((A_5 & DomainKeysTypes.DKIM) > (DomainKeysTypes)0) ? new global::a.j.al(this.d, this.c) : null;
			RSACryptoServiceProvider rsacryptoServiceProvider = null;
			if (ae != null)
			{
				ae.e = this.e;
				A_0 = ae.a(A_0, A_1, A_2, A_3, A_4, ref rsacryptoServiceProvider);
				if (A_0 == null)
				{
					this.d = ae.d;
					return null;
				}
			}
			if (al != null)
			{
				al.e = this.e;
				A_0 = al.a(A_0, A_1, A_2, A_3, A_4, ref rsacryptoServiceProvider);
				if (A_0 == null)
				{
					this.d = al.d;
					return null;
				}
			}
			return A_0;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00028C8A File Offset: 0x00027C8A
		public DomainKeysVerifyResult Verify(MailMessage msg, Smtp dnsRequestor)
		{
			return this.Verify(msg, dnsRequestor, DomainKeysTypes.Both);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00028C95 File Offset: 0x00027C95
		public DomainKeysVerifyResult Verify(MailMessage msg, Smtp dnsRequestor, DomainKeysTypes dkTypes)
		{
			return this.b(msg, dnsRequestor, dkTypes, null);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00028CA4 File Offset: 0x00027CA4
		private DomainKeysVerifyResult b(MailMessage A_0, Smtp A_1, DomainKeysTypes A_2, Header A_3)
		{
			global::a.j.ae ae = ((A_2 & DomainKeysTypes.DK) > (DomainKeysTypes)0) ? new global::a.j.ae(this.d, this.c) : null;
			global::a.j.al al = ((A_2 & DomainKeysTypes.DKIM) > (DomainKeysTypes)0) ? new global::a.j.al(this.d, this.c) : null;
			DomainKeysVerifyResult domainKeysVerifyResult = DomainKeysVerifyResult.MessageNotSigned;
			DomainKeysVerifyResult a_ = DomainKeysVerifyResult.MessageNotSigned;
			if (ae != null)
			{
				domainKeysVerifyResult = ae.b(A_0, A_1, A_3);
			}
			if (al != null)
			{
				try
				{
					a_ = al.b(A_0, A_1, A_3);
				}
				catch (MailBeeDomainKeysException ex)
				{
					if (ex.InnerException is InvalidOperationException && domainKeysVerifyResult != DomainKeysVerifyResult.MessageNotSigned)
					{
						return domainKeysVerifyResult;
					}
					throw;
				}
			}
			return this.a(domainKeysVerifyResult, a_, ae, al);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00028D40 File Offset: 0x00027D40
		public DomainKeysVerifyResult Verify(MailMessage msg, Smtp dnsRequestor, Header dkHeader)
		{
			if (dkHeader == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			string text = dkHeader.Name.ToLower();
			if (text == "dkim-signature")
			{
				return this.b(msg, dnsRequestor, DomainKeysTypes.DKIM, dkHeader);
			}
			if (text == "domainkey-signature")
			{
				return this.b(msg, dnsRequestor, DomainKeysTypes.DK, dkHeader);
			}
			throw new MailBeeInvalidArgumentException(20);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00028D9C File Offset: 0x00027D9C
		private DomainKeysVerifyResult a(DomainKeysVerifyResult A_0, DomainKeysVerifyResult A_1, global::a.j.ae A_2, global::a.j.al A_3)
		{
			if (A_0 == DomainKeysVerifyResult.MessageNotSigned && A_1 == DomainKeysVerifyResult.MessageNotSigned)
			{
				return DomainKeysVerifyResult.MessageNotSigned;
			}
			if (A_0 == DomainKeysVerifyResult.OK && A_1 == DomainKeysVerifyResult.OK)
			{
				return DomainKeysVerifyResult.OK;
			}
			if (A_2 != null && A_0 == DomainKeysVerifyResult.OK)
			{
				if (A_3 != null && A_1 != DomainKeysVerifyResult.MessageNotSigned && A_1 != DomainKeysVerifyResult.Sha256NotSupported)
				{
					return A_1;
				}
				return A_0;
			}
			else if (A_3 != null && A_1 == DomainKeysVerifyResult.OK)
			{
				if (A_2 != null && A_0 != DomainKeysVerifyResult.MessageNotSigned)
				{
					return A_0;
				}
				return A_1;
			}
			else
			{
				if (A_0 <= A_1)
				{
					return A_1;
				}
				return A_0;
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00028DEC File Offset: 0x00027DEC
		internal byte[] a(string A_0, bool A_1)
		{
			byte[] array = null;
			if (A_1)
			{
				try
				{
					return this.f(A_0);
				}
				catch (MailBeeIOException)
				{
					this.d = 30;
					if (this.c)
					{
						throw;
					}
					return null;
				}
			}
			array = this.d(A_0);
			if (array == null)
			{
				this.d = 1130;
				if (this.c)
				{
					throw new MailBeeDomainKeysException(this.d);
				}
				return null;
			}
			return array;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00028E60 File Offset: 0x00027E60
		internal global::a.i.j a(Smtp A_0, string A_1, string A_2)
		{
			string[] txtData = A_0.GetTxtData(string.Format("{0}._domainkey.{1}", A_1, A_2));
			for (int i = 0; i < txtData.Length; i++)
			{
				if (txtData[i].IndexOf("\n") > -1)
				{
					txtData[i] = txtData[i].Replace("\n", " ");
				}
			}
			if (txtData != null && txtData.Length != 0)
			{
				return global::a.i.j.a(string.Join(string.Empty, txtData));
			}
			return null;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00028ED0 File Offset: 0x00027ED0
		internal HeaderCollection a(HeaderCollection A_0, string A_1)
		{
			HeaderCollection headerCollection = new HeaderCollection();
			int num = A_0.Count - 1;
			while (num >= 0 && string.Compare(A_0[num].Name, A_1, true) != 0)
			{
				headerCollection.a(0, A_0[num]);
				num--;
			}
			return headerCollection;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00028F1C File Offset: 0x00027F1C
		internal string c(string A_0)
		{
			if (A_0.StartsWith("rsa-"))
			{
				string result = A_0.Substring(4).ToLower();
				if (result == "sha1" || result == "sha256")
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00028F60 File Offset: 0x00027F60
		internal HashAlgorithm e(string A_0)
		{
			HashAlgorithm result = null;
			if (!(A_0 == "sha1"))
			{
				if (!(A_0 == "sha256"))
				{
					return result;
				}
			}
			else
			{
				if (Global.FipsMode)
				{
					return new SHA1CryptoServiceProvider();
				}
				try
				{
					return new SHA1Managed();
				}
				catch (InvalidOperationException)
				{
					return new SHA1CryptoServiceProvider();
				}
			}
			if (Global.FipsMode)
			{
				result = new SHA256CryptoServiceProvider();
			}
			else
			{
				try
				{
					result = new SHA256Managed();
				}
				catch (InvalidOperationException)
				{
					result = new SHA256CryptoServiceProvider();
				}
			}
			return result;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00028FEC File Offset: 0x00027FEC
		internal byte[] a(HashAlgorithm A_0, string A_1)
		{
			return this.a(A_0, A_1, Encoding.GetEncoding(1252));
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00029000 File Offset: 0x00028000
		internal byte[] a(HashAlgorithm A_0, string A_1, Encoding A_2)
		{
			byte[] bytes = A_2.GetBytes(A_1);
			return A_0.ComputeHash(bytes);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002901C File Offset: 0x0002801C
		internal bool a(RSACryptoServiceProvider A_0, byte[] A_1, byte[] A_2, string A_3, byte[] A_4)
		{
			bool result = false;
			if (A_3.StartsWith("rsa-") && A_3.Length > 4)
			{
				string text = CryptoConfig.MapNameToOID(A_3.Substring(4).ToUpper());
				if (text == null)
				{
					return false;
				}
				result = A_0.VerifyHash(A_2, text, A_1);
			}
			return result;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00029068 File Offset: 0x00028068
		internal static RSACryptoServiceProvider b(byte[] A_0)
		{
			byte[] a_ = new byte[]
			{
				48,
				13,
				6,
				9,
				42,
				134,
				72,
				134,
				247,
				13,
				1,
				1,
				1,
				5,
				0
			};
			new byte[15];
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(A_0));
			RSACryptoServiceProvider result;
			try
			{
				ushort num = binaryReader.ReadUInt16();
				if (num == 33072)
				{
					binaryReader.ReadByte();
				}
				else if (num == 33328)
				{
					binaryReader.ReadInt16();
				}
				else if (num != 31792 && num != 23600 && num != 19504)
				{
					return null;
				}
				if (!DomainKeys.a(binaryReader.ReadBytes(15), a_))
				{
					result = null;
				}
				else
				{
					num = binaryReader.ReadUInt16();
					if (num == 33027)
					{
						binaryReader.ReadByte();
					}
					else if (num == 33283)
					{
						binaryReader.ReadInt16();
					}
					else if (num != 27395 && num != 19203 && num != 15107)
					{
						return null;
					}
					if (binaryReader.ReadByte() != 0)
					{
						result = null;
					}
					else
					{
						num = binaryReader.ReadUInt16();
						if (num == 33072)
						{
							binaryReader.ReadByte();
						}
						else if (num == 33328)
						{
							binaryReader.ReadInt16();
						}
						else if (num != 26672 && num != 18480 && num != 14384)
						{
							return null;
						}
						num = binaryReader.ReadUInt16();
						byte b = 0;
						byte b2;
						if (num == 33026)
						{
							b2 = binaryReader.ReadByte();
						}
						else if (num == 33282)
						{
							b = binaryReader.ReadByte();
							b2 = binaryReader.ReadByte();
						}
						else if (num == 24834)
						{
							b2 = 96;
						}
						else if (num == 16642)
						{
							b2 = 64;
						}
						else
						{
							if (num != 12546)
							{
								return null;
							}
							b2 = 48;
						}
						byte[] array = new byte[4];
						array[0] = b2;
						array[1] = b;
						int num2 = BitConverter.ToInt32(array, 0);
						if (binaryReader.PeekChar() == 0)
						{
							binaryReader.ReadByte();
							if (num != 24834 && num != 16642 && num != 12546)
							{
								num2--;
							}
						}
						byte[] modulus = binaryReader.ReadBytes(num2);
						if (binaryReader.ReadByte() != 2)
						{
							result = null;
						}
						else
						{
							int count = (int)binaryReader.ReadByte();
							byte[] exponent = binaryReader.ReadBytes(count);
							RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
							rsacryptoServiceProvider.ImportParameters(new RSAParameters
							{
								Modulus = modulus,
								Exponent = exponent
							});
							result = rsacryptoServiceProvider;
						}
					}
				}
			}
			catch (Exception)
			{
				result = null;
			}
			finally
			{
				binaryReader.Close();
			}
			return result;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x000292E4 File Offset: 0x000282E4
		internal RSACryptoServiceProvider c(byte[] A_0)
		{
			RSACryptoServiceProvider result;
			using (MemoryStream memoryStream = new MemoryStream(A_0))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					try
					{
						ushort num = binaryReader.ReadUInt16();
						if (num == 33072)
						{
							binaryReader.ReadByte();
						}
						else
						{
							if (num != 33328)
							{
								return null;
							}
							binaryReader.ReadInt16();
						}
						num = binaryReader.ReadUInt16();
						if (num != 258)
						{
							result = null;
						}
						else if (binaryReader.ReadByte() != 0)
						{
							result = null;
						}
						else
						{
							int count = DomainKeys.a(binaryReader);
							byte[] a_ = binaryReader.ReadBytes(count);
							count = DomainKeys.a(binaryReader);
							byte[] exponent = binaryReader.ReadBytes(count);
							count = DomainKeys.a(binaryReader);
							byte[] a_2 = binaryReader.ReadBytes(count);
							count = DomainKeys.a(binaryReader);
							byte[] a_3 = binaryReader.ReadBytes(count);
							count = DomainKeys.a(binaryReader);
							byte[] a_4 = binaryReader.ReadBytes(count);
							count = DomainKeys.a(binaryReader);
							byte[] a_5 = binaryReader.ReadBytes(count);
							count = DomainKeys.a(binaryReader);
							byte[] a_6 = binaryReader.ReadBytes(count);
							count = DomainKeys.a(binaryReader);
							byte[] a_7 = binaryReader.ReadBytes(count);
							RSACryptoServiceProvider rsacryptoServiceProvider;
							if (this.e != null)
							{
								rsacryptoServiceProvider = new RSACryptoServiceProvider(this.e);
							}
							else
							{
								rsacryptoServiceProvider = new RSACryptoServiceProvider();
							}
							rsacryptoServiceProvider.ImportParameters(new RSAParameters
							{
								Modulus = this.a(a_),
								Exponent = exponent,
								D = this.a(a_2),
								P = this.a(a_3),
								Q = this.a(a_4),
								DP = this.a(a_5),
								DQ = this.a(a_6),
								InverseQ = this.a(a_7)
							});
							result = rsacryptoServiceProvider;
						}
					}
					catch (IOException a_8)
					{
						throw new MailBeeDomainKeysException(35, a_8);
					}
				}
			}
			return result;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00029520 File Offset: 0x00028520
		private byte[] a(byte[] A_0)
		{
			if (A_0.Length % 4 > 0)
			{
				int num = 4 - A_0.Length % 4;
				byte[] array = new byte[A_0.Length + num];
				for (int i = 0; i < num; i++)
				{
					array[i] = 0;
				}
				Buffer.BlockCopy(A_0, 0, array, num, A_0.Length);
				return array;
			}
			return A_0;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00029568 File Offset: 0x00028568
		private static int a(BinaryReader A_0)
		{
			byte b = A_0.ReadByte();
			if (b != 2)
			{
				return 0;
			}
			b = A_0.ReadByte();
			int num;
			if (b == 129)
			{
				num = (int)A_0.ReadByte();
			}
			else if (b == 130)
			{
				byte b2 = A_0.ReadByte();
				byte b3 = A_0.ReadByte();
				byte[] array = new byte[4];
				array[0] = b3;
				array[1] = b2;
				num = BitConverter.ToInt32(array, 0);
			}
			else
			{
				num = (int)b;
			}
			long position = A_0.BaseStream.Position;
			byte b4 = A_0.ReadByte();
			A_0.BaseStream.Position = position;
			while (b4 == 0)
			{
				A_0.ReadByte();
				num--;
				position = A_0.BaseStream.Position;
				b4 = A_0.ReadByte();
				A_0.BaseStream.Position = position;
			}
			return num;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00029628 File Offset: 0x00028628
		private static bool a(byte[] A_0, byte[] A_1)
		{
			if (A_0.Length != A_1.Length)
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < A_0.Length; i++)
			{
				if (A_0[i] != A_1[num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00029660 File Offset: 0x00028660
		internal byte[] f(string A_0)
		{
			byte[] array = global::a.ap.e(A_0);
			return this.d(Encoding.ASCII.GetString(array, 0, array.Length));
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002968C File Offset: 0x0002868C
		internal byte[] d(string A_0)
		{
			A_0 = A_0.Trim();
			if (A_0.StartsWith("-----BEGIN RSA PRIVATE KEY-----") && A_0.EndsWith("-----END RSA PRIVATE KEY-----"))
			{
				string text = A_0.Substring("-----BEGIN RSA PRIVATE KEY-----".Length, A_0.Length - ("-----END RSA PRIVATE KEY-----".Length + "-----BEGIN RSA PRIVATE KEY-----".Length));
				text = text.Trim();
				try
				{
					return global::a.i.h.b(Encoding.ASCII.GetBytes(text));
				}
				catch (FormatException)
				{
				}
			}
			return null;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00029718 File Offset: 0x00028718
		public Task<DomainKeysVerifyResult> VerifyAsync(MailMessage msg, Smtp dnsRequestor)
		{
			return this.VerifyAsync(msg, dnsRequestor, DomainKeysTypes.Both);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00029723 File Offset: 0x00028723
		public Task<DomainKeysVerifyResult> VerifyAsync(MailMessage msg, Smtp dnsRequestor, DomainKeysTypes dkTypes)
		{
			return this.a(msg, dnsRequestor, dkTypes, null);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00029730 File Offset: 0x00028730
		private Task<DomainKeysVerifyResult> a(MailMessage A_0, Smtp A_1, DomainKeysTypes A_2, Header A_3)
		{
			DomainKeys.b b;
			b.d = this;
			b.e = A_0;
			b.f = A_1;
			b.c = A_2;
			b.g = A_3;
			b.b = AsyncTaskMethodBuilder<DomainKeysVerifyResult>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<DomainKeysVerifyResult> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<DomainKeys.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00029798 File Offset: 0x00028798
		public Task<DomainKeysVerifyResult> VerifyAsync(MailMessage msg, Smtp dnsRequestor, Header dkHeader)
		{
			if (dkHeader == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			string text = dkHeader.Name.ToLower();
			if (text == "dkim-signature")
			{
				return this.a(msg, dnsRequestor, DomainKeysTypes.DKIM, dkHeader);
			}
			if (text == "domainkey-signature")
			{
				return this.a(msg, dnsRequestor, DomainKeysTypes.DK, dkHeader);
			}
			throw new MailBeeInvalidArgumentException(20);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000297F4 File Offset: 0x000287F4
		internal Task<global::a.i.j> b(Smtp A_0, string A_1, string A_2)
		{
			DomainKeys.a a;
			a.c = A_0;
			a.d = A_1;
			a.e = A_2;
			a.b = AsyncTaskMethodBuilder<global::a.i.j>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<global::a.i.j> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<DomainKeys.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x040006ED RID: 1773
		private const string a = "-----BEGIN RSA PRIVATE KEY-----";

		// Token: 0x040006EE RID: 1774
		private const string b = "-----END RSA PRIVATE KEY-----";

		// Token: 0x040006EF RID: 1775
		internal bool c = true;

		// Token: 0x040006F0 RID: 1776
		internal int d;

		// Token: 0x040006F1 RID: 1777
		internal CspParameters e;
	}
}
