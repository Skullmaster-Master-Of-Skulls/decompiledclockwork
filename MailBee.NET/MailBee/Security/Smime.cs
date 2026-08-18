using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.Pkcs;
using a.d;
using a.i;
using a.j;
using MailBee.Mime;

namespace MailBee.Security
{
	// Token: 0x02000119 RID: 281
	public class Smime
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0002A198 File Offset: 0x00029198
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0002A1A0 File Offset: 0x000291A0
		public bool SetSignedCmsOnVerify
		{
			get
			{
				return this.f;
			}
			set
			{
				this.f = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0002A1A9 File Offset: 0x000291A9
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0002A1B1 File Offset: 0x000291B1
		public bool SetEnvelopedCmsOnDecrypt
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0002A1BA File Offset: 0x000291BA
		public Smime() : this(null)
		{
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0002A1C4 File Offset: 0x000291C4
		public Smime(string licenseKey)
		{
			if (Powerup.License == null)
			{
				Powerup.a(licenseKey);
			}
			if (!Powerup.License.d())
			{
				throw new MailBeeLicenseException(Powerup.License, typeof(Powerup));
			}
			this.ResetToDefaults();
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0002A213 File Offset: 0x00029213
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0002A21B File Offset: 0x0002921B
		public CryptoServiceProvider Provider
		{
			get
			{
				return this.a;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.a = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0002A22F File Offset: 0x0002922F
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x0002A237 File Offset: 0x00029237
		public Algorithm EncryptionAlgorithm
		{
			get
			{
				return this.b;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.b = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0002A24B File Offset: 0x0002924B
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0002A253 File Offset: 0x00029253
		public Algorithm HashAlgorithm
		{
			get
			{
				return this.c;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.c = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0002A267 File Offset: 0x00029267
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0002A26F File Offset: 0x0002926F
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

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0002A278 File Offset: 0x00029278
		public int LastResult
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0002A280 File Offset: 0x00029280
		public MailMessage Encrypt(MailMessage message, CertificateCollection encryptionCerts)
		{
			if (message == null)
			{
				this.e = 21;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			this.e = 0;
			if (message.IsEncrypted)
			{
				return message;
			}
			string text = null;
			EmailAddressCollection emailAddressCollection = global::a.d.a.a(message, ref text);
			byte[] messageRawData = message.GetMessageRawData();
			if (emailAddressCollection != null)
			{
				message.Bcc = emailAddressCollection;
			}
			byte[] array = this.a(messageRawData, encryptionCerts);
			if (array != null)
			{
				HeaderCollection headerCollection = new HeaderCollection();
				foreach (object obj in message.Headers)
				{
					Header header = (Header)obj;
					headerCollection.b(header.i());
				}
				return Smime.a(headerCollection, array);
			}
			return null;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0002A34C File Offset: 0x0002934C
		public MailMessage Sign(MailMessage message, Certificate signingCert)
		{
			if (signingCert == null || message == null)
			{
				this.e = 21;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			this.e = 0;
			if (message.IsSigned)
			{
				return message;
			}
			byte[] array = this.a(message.Clone());
			HeaderCollection headerCollection = new HeaderCollection();
			foreach (object obj in message.Headers)
			{
				Header header = (Header)obj;
				headerCollection.b(header.i());
			}
			byte[] a_ = this.a(array, signingCert, null, false);
			return this.a(headerCollection, array, a_);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0002A404 File Offset: 0x00029404
		public SmimeResult Decrypt(MailMessage message)
		{
			if (message == null)
			{
				this.e = 21;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			return this.Decrypt(message, null);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0002A428 File Offset: 0x00029428
		public SmimeResult Decrypt(MailMessage message, CertificateStore[] stores)
		{
			if (message == null)
			{
				this.e = 21;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			if (stores == null || stores.Length == 0)
			{
				stores = new CertificateStore[1];
				try
				{
					stores[0] = new CertificateStore("MY", CertStoreType.System, null);
				}
				catch (MailBeeException ex)
				{
					this.e = ex.ErrorCode;
					if (this.d)
					{
						throw;
					}
					return null;
				}
			}
			SmimeResult smimeResult = new SmimeResult();
			this.e = 0;
			if (message.IsEncrypted)
			{
				smimeResult = this.a(message.MimePart.PartValueAsBytes, stores);
				if (smimeResult != null && smimeResult.a != null && !smimeResult.a.Headers.Exists("MIME-Version"))
				{
					foreach (object obj in message.Headers)
					{
						Header header = (Header)obj;
						if (!smimeResult.a.Headers.Exists(header.Name) && header.Name.ToLower() != "content-disposition")
						{
							smimeResult.a.Headers.b(header);
						}
					}
					smimeResult.a.Builder.Apply();
				}
			}
			else
			{
				smimeResult.a = message;
			}
			return smimeResult;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002A598 File Offset: 0x00029598
		public SmimeResult Verify(MailMessage message, MessageVerificationFlags flags, CertificateStore extraStore)
		{
			return this.a(message, flags, extraStore, new SmimeResult());
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0002A5A8 File Offset: 0x000295A8
		internal SmimeResult a(MailMessage A_0, MessageVerificationFlags A_1, CertificateStore A_2, SmimeResult A_3)
		{
			if (A_0 == null)
			{
				this.e = 21;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			this.e = 0;
			A_3.d = MessageVerificationFlags.None;
			if (A_0.IsSigned)
			{
				byte[] a_ = null;
				byte[] a_2 = null;
				bool flag = false;
				if (A_0.AttachedSignatureVerified)
				{
					A_3.d = A_0.AttachedSignatureVerificationResult;
				}
				else
				{
					flag = Smime.c(A_0);
					Attachment attachment = flag ? A_0.Attachments["smime.p7s"] : A_0.Attachments["smime.p7m"];
					if (attachment == null)
					{
						A_3.d = MessageVerificationFlags.MessageTampered;
						return A_3;
					}
					a_ = attachment.GetData();
					a_2 = new byte[0];
					if (flag)
					{
						if (A_0.MimePart.SubParts == null || A_0.MimePart.SubParts.Count <= 0 || A_0.MimePart.SubParts[0].RawBody == null)
						{
							A_3.d = MessageVerificationFlags.MessageTampered;
							return A_3;
						}
						a_2 = A_0.MimePart.SubParts[0].RawBody;
					}
					else
					{
						if (A_0.MimePart.PartValueAsBytes == null)
						{
							A_3.d = MessageVerificationFlags.MessageTampered;
							return A_3;
						}
						a_2 = A_0.MimePart.PartValueAsBytes;
					}
				}
				byte[] array = null;
				this.a(a_2, a_, A_0.From.Email, A_1, A_2, A_3, flag, out array);
				if (array != null)
				{
					MailMessage mailMessage = new MailMessage(array);
					A_3.a = mailMessage;
					if (!mailMessage.Headers.Exists("MIME-Version"))
					{
						foreach (object obj in A_0.Headers)
						{
							Header header = (Header)obj;
							if (!mailMessage.Headers.Exists(header.Name) && header.Name.ToLower() != "content-disposition")
							{
								mailMessage.Headers.b(header);
							}
						}
						mailMessage.Builder.Apply();
					}
				}
			}
			return A_3;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002A7B8 File Offset: 0x000297B8
		public MailMessage SignAndEncrypt(MailMessage message, Certificate signingCert, CertificateCollection encryptionCerts)
		{
			MailMessage mailMessage = this.Sign(message, signingCert);
			if (mailMessage != null)
			{
				return this.Encrypt(mailMessage, encryptionCerts);
			}
			return null;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0002A7DB File Offset: 0x000297DB
		public SmimeResult DecryptAndVerify(MailMessage message, MessageVerificationFlags flags)
		{
			return this.DecryptAndVerify(message, flags, null, null);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0002A7E8 File Offset: 0x000297E8
		public SmimeResult DecryptAndVerify(MailMessage message, MessageVerificationFlags flags, CertificateStore[] storesForDecrypt, CertificateStore extraStoreForVerify)
		{
			SmimeResult smimeResult = this.Decrypt(message, storesForDecrypt);
			if (smimeResult != null && smimeResult.a != null)
			{
				return this.a(smimeResult.a, flags, extraStoreForVerify, smimeResult);
			}
			return smimeResult;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0002A81C File Offset: 0x0002981C
		public bool ResetToDefaults()
		{
			this.e = 0;
			this.b = Algorithm.CreateInstanceByOid("1.2.840.113549.3.7");
			this.c = Algorithm.CreateInstanceByOid("1.3.14.3.2.26");
			try
			{
				this.a = new CryptoServiceProvider();
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

		// Token: 0x0600093E RID: 2366 RVA: 0x0002A88C File Offset: 0x0002988C
		internal static bool c(MailMessage A_0)
		{
			return string.Compare(A_0.ContentType, "multipart/signed", true) == 0;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002A8A4 File Offset: 0x000298A4
		internal static bool b(MailMessage A_0)
		{
			bool result = false;
			A_0.Parser.ParseHeaderOnly = true;
			bool a_ = A_0.NeedToReparse;
			if (string.Compare(A_0.ContentType, "application/pkcs7-mime", true) == 0 || string.Compare(A_0.ContentType, "application/x-pkcs7-mime", true) == 0)
			{
				Header header = A_0.Headers.a("Content-Type");
				global::a.i.n n = (header.HeaderParameters == null) ? null : header.HeaderParameters.b("smime-type");
				if (n != null && string.Compare(n.c(), "signed-data", true) == 0)
				{
					result = true;
				}
			}
			A_0.Parser.ParseHeaderOnly = false;
			A_0.NeedToReparse = a_;
			return result;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0002A948 File Offset: 0x00029948
		internal static MailMessage a(HeaderCollection A_0, byte[] A_1)
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.Headers.Clear();
			mailMessage.Headers.a(A_0);
			mailMessage.MimePart.PartValueAsBytes = A_1;
			Header header = mailMessage.Headers.a("Content-Type");
			if (header != null)
			{
				header.Value = "application/pkcs7-mime";
				header.HeaderParameters = ((header.HeaderParameters != null) ? header.HeaderParameters : new global::a.i.j());
				header.HeaderParameters.b();
				header.HeaderParameters.c(new global::a.i.n("smime-type", "enveloped-data"));
				header.HeaderParameters.c(new global::a.i.n("name", "smime.p7m"));
			}
			else
			{
				header = new Header("Content-Type", "application/pkcs7-mime");
				header.HeaderParameters = ((header.HeaderParameters != null) ? header.HeaderParameters : new global::a.i.j());
				header.HeaderParameters.c(new global::a.i.n("smime-type", "enveloped-data"));
				header.HeaderParameters.c(new global::a.i.n("name", "smime.p7m"));
				mailMessage.Headers.b(header);
			}
			Header header2 = mailMessage.Headers.a("Content-Disposition");
			if (header2 != null)
			{
				header2.Value = "attachment";
				header2.HeaderParameters = ((header2.HeaderParameters != null) ? header2.HeaderParameters : new global::a.i.j());
				header2.HeaderParameters.b();
				header2.HeaderParameters.c(new global::a.i.n("name", "smime.p7m"));
			}
			else
			{
				header2 = new Header("Content-Disposition", "attachment");
				header2.HeaderParameters = ((header2.HeaderParameters != null) ? header2.HeaderParameters : new global::a.i.j());
				header2.HeaderParameters.c(new global::a.i.n("name", "smime.p7m"));
				mailMessage.Headers.b(header2);
			}
			mailMessage.MimePart.MimePartTransferEncoding = MailTransferEncoding.Base64;
			mailMessage.Builder.Apply();
			return new MailMessage(mailMessage.GetMessageRawData());
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0002AB40 File Offset: 0x00029B40
		private MailMessage a(HeaderCollection A_0, byte[] A_1, byte[] A_2)
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.Headers.Clear();
			mailMessage.Headers.a(A_0);
			mailMessage.MimePart.SubPartsInternal.b(MimePart.Parse(A_1));
			MimePart mimePart = new MimePart(mailMessage);
			Header header = mimePart.Headers.a("Content-Type");
			if (header != null)
			{
				header.Value = "application/pkcs7-signature";
				header.HeaderParameters = ((header.HeaderParameters != null) ? header.HeaderParameters : new global::a.i.j());
				header.HeaderParameters.b();
				header.HeaderParameters.c(new global::a.i.n("name", "smime.p7m"));
			}
			else
			{
				header = new Header("Content-Type", "application/pkcs7-signature");
				header.HeaderParameters = ((header.HeaderParameters != null) ? header.HeaderParameters : new global::a.i.j());
				header.HeaderParameters.c(new global::a.i.n("name", "smime.p7s"));
				mimePart.Headers.b(header);
			}
			Header header2 = mimePart.Headers.a("Content-Disposition");
			if (header2 != null)
			{
				header2.Value = "attachment";
				header2.HeaderParameters = ((header2.HeaderParameters != null) ? header2.HeaderParameters : new global::a.i.j());
				header2.HeaderParameters.b();
				header2.HeaderParameters.c(new global::a.i.n("filename", "smime.p7s"));
			}
			else
			{
				header2 = new Header("Content-Disposition", "attachment");
				header2.HeaderParameters = ((header2.HeaderParameters != null) ? header2.HeaderParameters : new global::a.i.j());
				header2.HeaderParameters.c(new global::a.i.n("filename", "smime.p7s"));
				mimePart.Headers.b(header2);
			}
			mimePart.MimePartTransferEncoding = MailTransferEncoding.Base64;
			mimePart.PartValueAsBytes = A_2;
			mailMessage.MimePart.SubPartsInternal.b(mimePart);
			header = mailMessage.Headers.a("Content-Type");
			if (header != null)
			{
				header.Value = "multipart/signed";
				header.HeaderParameters = ((header.HeaderParameters != null) ? header.HeaderParameters : new global::a.i.j());
				header.HeaderParameters.b();
				header.HeaderParameters.c(new global::a.i.n("protocol", "application/pkcs7-signature"));
				header.HeaderParameters.c(new global::a.i.n("micalg", (this.c.Name != null && this.c.Name.Length > 0) ? this.c.Name : "unknown"));
				header.HeaderParameters.c(new global::a.i.n("boundary", mailMessage.x()));
			}
			mailMessage.Builder.Apply();
			return new MailMessage(mailMessage.GetMessageRawData());
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002ADEF File Offset: 0x00029DEF
		private byte[] a(MailMessage A_0)
		{
			global::a.i.k.b(A_0.Headers);
			return A_0.GetMessageRawData();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002AE04 File Offset: 0x00029E04
		[SecuritySafeCritical]
		private SmimeResult a(byte[] A_0, CertificateStore[] A_1)
		{
			if (A_0 == null)
			{
				this.e = 21;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			if (A_1 == null || A_1.Length == 0)
			{
				A_1 = new CertificateStore[1];
				try
				{
					A_1[0] = new CertificateStore("MY", CertStoreType.System, null);
				}
				catch (MailBeeException ex)
				{
					this.e = ex.ErrorCode;
					if (this.d)
					{
						throw;
					}
					return null;
				}
			}
			SmimeResult smimeResult = new SmimeResult();
			this.e = 0;
			IntPtr[] array = new IntPtr[A_1.Length];
			for (int i = 0; i < A_1.Length; i++)
			{
				if (A_1[i] != null)
				{
					array[i] = A_1[i].Handle;
				}
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			uint num = (uint)A_0.Length;
			IntPtr intPtr3 = IntPtr.Zero;
			uint num2 = 0U;
			t t = default(t);
			t.a = (uint)Marshal.SizeOf(t.GetType());
			t.b = 65537U;
			t.c = (uint)array.Length;
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			t.d = gchandle.AddrOfPinnedObject();
			IntPtr zero = IntPtr.Zero;
			try
			{
				intPtr = Marshal.AllocHGlobal((int)t.a);
				Marshal.StructureToPtr(t, intPtr, true);
				intPtr2 = Marshal.AllocHGlobal((int)num);
				Marshal.Copy(A_0, 0, intPtr2, (int)num);
				if (ab.a.CryptDecryptMessage(intPtr, intPtr2, num, intPtr3, ref num2, ref zero) == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					this.e = 1103;
					if (this.d)
					{
						throw new MailBeeSmimeWin32Exception(lastWin32Error);
					}
					return null;
				}
				else
				{
					intPtr3 = Marshal.AllocHGlobal((int)num2);
					if (ab.a.CryptDecryptMessage(intPtr, intPtr2, num, intPtr3, ref num2, ref zero) == 0)
					{
						int lastWin32Error2 = Marshal.GetLastWin32Error();
						this.e = 1103;
						if (this.d)
						{
							throw new MailBeeSmimeWin32Exception(lastWin32Error2);
						}
						return null;
					}
					else
					{
						if (zero != IntPtr.Zero)
						{
							smimeResult.b = new Certificate(zero);
						}
						byte[] array2 = new byte[num2];
						Marshal.Copy(intPtr3, array2, 0, (int)num2);
						MailMessage mailMessage = new MailMessage(array2);
						if (Smime.b(mailMessage))
						{
							Attachment attachment = mailMessage.Attachments["smime.p7m"];
							if (attachment != null)
							{
								byte[] array3 = null;
								this.a(null, attachment.GetData(), null, MessageVerificationFlags.MessageTampered | MessageVerificationFlags.SignatureExpired | MessageVerificationFlags.CertificateRevoked, null, smimeResult, false, out array3);
								if (array3 != null)
								{
									mailMessage = new MailMessage(array3);
									mailMessage.z();
									mailMessage.AttachedSignatureVerified = true;
									mailMessage.AttachedSignatureVerificationResult = smimeResult.d;
								}
							}
						}
						smimeResult.a = mailMessage;
					}
				}
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
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
				gchandle.Free();
			}
			if (this.g)
			{
				smimeResult.f = new EnvelopedCms();
				smimeResult.f.Decode(A_0);
			}
			return smimeResult;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0002B108 File Offset: 0x0002A108
		[SecuritySafeCritical]
		private byte[] a(byte[] A_0, CertificateCollection A_1)
		{
			if (A_0 == null)
			{
				this.e = 21;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			if (A_1 == null || A_1.Count == 0)
			{
				this.e = 22;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			this.e = 0;
			IntPtr[] array = new IntPtr[A_1.Count];
			for (int i = 0; i < A_1.Count; i++)
			{
				array[i] = A_1[i].Handle;
			}
			am am = default(am);
			am.a = (uint)Marshal.SizeOf(am);
			am.b = 65537U;
			am.c = this.a.Handle;
			am.d.a = Marshal.StringToHGlobalAnsi(this.b.Oid);
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			uint num = (uint)A_0.Length;
			IntPtr intPtr3 = IntPtr.Zero;
			uint num2 = 0U;
			GCHandle gchandle = default(GCHandle);
			byte[] result;
			try
			{
				intPtr2 = Marshal.AllocHGlobal((int)num);
				Marshal.Copy(A_0, 0, intPtr2, (int)num);
				intPtr = Marshal.AllocHGlobal((int)am.a);
				Marshal.StructureToPtr(am, intPtr, true);
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				IntPtr a_ = gchandle.AddrOfPinnedObject();
				if (ab.a.CryptEncryptMessage(intPtr, (uint)array.Length, a_, intPtr2, num, intPtr3, ref num2) == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					this.e = 1103;
					if (this.d)
					{
						throw new MailBeeSmimeWin32Exception(lastWin32Error);
					}
					result = null;
				}
				else
				{
					intPtr3 = Marshal.AllocHGlobal((int)num2);
					if (ab.a.CryptEncryptMessage(intPtr, (uint)array.Length, a_, intPtr2, num, intPtr3, ref num2) == 0)
					{
						int lastWin32Error2 = Marshal.GetLastWin32Error();
						this.e = 1103;
						if (this.d)
						{
							throw new MailBeeSmimeWin32Exception(lastWin32Error2);
						}
						result = null;
					}
					else
					{
						byte[] array2 = new byte[num2];
						Marshal.Copy(intPtr3, array2, 0, array2.Length);
						result = array2;
					}
				}
			}
			finally
			{
				if (am.d.a != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(am.d.a);
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
				gchandle.Free();
			}
			return result;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0002B358 File Offset: 0x0002A358
		[SecuritySafeCritical]
		private byte[] a(byte[] A_0, Certificate A_1, Certificate[] A_2, bool A_3)
		{
			if (A_1 == null || A_0 == null)
			{
				this.e = 21;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			this.e = 0;
			byte[] array = null;
			uint num = (uint)A_0.Length;
			IntPtr intPtr = Marshal.AllocHGlobal((int)num);
			Marshal.Copy(A_0, 0, intPtr, (int)num);
			ArrayList arrayList = new ArrayList();
			arrayList.Add(A_1.Handle);
			if (A_2 != null)
			{
				foreach (Certificate certificate in A_2)
				{
					arrayList.Add(certificate.Handle);
				}
			}
			IntPtr[] array2 = (IntPtr[])arrayList.ToArray(typeof(IntPtr));
			global::a.j.i i2 = default(global::a.j.i);
			i2.a = (uint)Marshal.SizeOf(i2);
			i2.b = 65537U;
			i2.c = A_1.Handle;
			i2.d.a = Marshal.StringToHGlobalAnsi(this.c.Oid);
			i2.d.b.a = 0U;
			i2.f = (uint)array2.Length;
			GCHandle gchandle = GCHandle.Alloc(array2, GCHandleType.Pinned);
			i2.g = gchandle.AddrOfPinnedObject();
			i2.j = 0U;
			i2.o = 0U;
			i2.h = 0U;
			i2.l = 0U;
			i2.n = 0U;
			i2.e = IntPtr.Zero;
			i2.k = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			int a_ = (!A_3) ? 1 : 0;
			uint a_2 = 1U;
			IntPtr[] value = new IntPtr[]
			{
				intPtr
			};
			uint[] value2 = new uint[]
			{
				num
			};
			IntPtr intPtr3 = IntPtr.Zero;
			uint num2 = 0U;
			GCHandle gchandle2 = default(GCHandle);
			GCHandle gchandle3 = default(GCHandle);
			try
			{
				intPtr2 = Marshal.AllocHGlobal((int)i2.a);
				Marshal.StructureToPtr(i2, intPtr2, true);
				gchandle2 = GCHandle.Alloc(value, GCHandleType.Pinned);
				IntPtr a_3 = gchandle2.AddrOfPinnedObject();
				gchandle3 = GCHandle.Alloc(value2, GCHandleType.Pinned);
				IntPtr a_4 = gchandle3.AddrOfPinnedObject();
				if (ab.a.CryptSignMessage(intPtr2, a_, a_2, a_3, a_4, intPtr3, ref num2) == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					this.e = 1103;
					if (this.d)
					{
						throw new MailBeeSmimeWin32Exception(lastWin32Error);
					}
					return null;
				}
				else
				{
					intPtr3 = Marshal.AllocHGlobal((int)num2);
					if (ab.a.CryptSignMessage(intPtr2, a_, a_2, a_3, a_4, intPtr3, ref num2) == 0)
					{
						int lastWin32Error2 = Marshal.GetLastWin32Error();
						this.e = 1103;
						if (this.d)
						{
							throw new MailBeeSmimeWin32Exception(lastWin32Error2);
						}
						return null;
					}
					else
					{
						array = new byte[num2];
						Marshal.Copy(intPtr3, array, 0, (int)num2);
					}
				}
			}
			finally
			{
				if (i2.d.a != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(i2.d.a);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
				gchandle2.Free();
				gchandle3.Free();
			}
			gchandle.Free();
			return array;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0002B678 File Offset: 0x0002A678
		[SecuritySafeCritical]
		private void a(byte[] A_0, byte[] A_1, string A_2, MessageVerificationFlags A_3, CertificateStore A_4, SmimeResult A_5, bool A_6, out byte[] A_7)
		{
			A_7 = null;
			if (A_1 != null && (A_0 == null && A_6))
			{
				this.e = 21;
				throw new MailBeeInvalidArgumentException(this.e);
			}
			if (A_1 != null)
			{
				aw aw = default(aw);
				aw.a = (uint)Marshal.SizeOf(aw);
				aw.b = 65537U;
				aw.c = IntPtr.Zero;
				aw.d = IntPtr.Zero;
				aw.e = IntPtr.Zero;
				IntPtr intPtr = Marshal.AllocHGlobal((int)aw.a);
				Marshal.StructureToPtr(aw, intPtr, true);
				IntPtr zero = IntPtr.Zero;
				if (A_6)
				{
					IntPtr intPtr2 = IntPtr.Zero;
					uint num = (uint)A_1.Length;
					IntPtr intPtr3 = IntPtr.Zero;
					uint num2 = (uint)A_0.Length;
					intPtr2 = Marshal.AllocHGlobal((int)num);
					Marshal.Copy(A_1, 0, intPtr2, (int)num);
					intPtr3 = Marshal.AllocHGlobal((int)num2);
					Marshal.Copy(A_0, 0, intPtr3, (int)num2);
					IntPtr[] value = new IntPtr[]
					{
						intPtr3
					};
					uint[] array = new uint[]
					{
						num2
					};
					GCHandle gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
					IntPtr a_ = gchandle.AddrOfPinnedObject();
					if (ab.a.CryptVerifyDetachedMessageSignature(intPtr, 0U, intPtr2, num, 1U, a_, ref array[0], ref zero) == 0)
					{
						A_5.d |= MessageVerificationFlags.MessageTampered;
					}
					if (zero != IntPtr.Zero)
					{
						A_5.c = new Certificate(zero);
					}
					Marshal.FreeHGlobal(intPtr);
					if (intPtr2 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr2);
					}
					if (intPtr3 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr3);
					}
					gchandle.Free();
				}
				else
				{
					IntPtr intPtr4 = IntPtr.Zero;
					uint num3 = (uint)A_1.Length;
					IntPtr intPtr5 = IntPtr.Zero;
					uint num4 = 0U;
					intPtr4 = Marshal.AllocHGlobal((int)num3);
					Marshal.Copy(A_1, 0, intPtr4, (int)num3);
					if (ab.a.CryptVerifyMessageSignature(intPtr, 0U, intPtr4, num3, intPtr5, ref num4, ref zero) == 0)
					{
						A_5.d |= MessageVerificationFlags.MessageTampered;
					}
					intPtr5 = Marshal.AllocHGlobal((int)num4);
					if (ab.a.CryptVerifyMessageSignature(intPtr, 0U, intPtr4, num3, intPtr5, ref num4, ref zero) == 0)
					{
						A_5.d |= MessageVerificationFlags.MessageTampered;
					}
					A_7 = new byte[num4];
					if (intPtr5 != IntPtr.Zero)
					{
						Marshal.Copy(intPtr5, A_7, 0, (int)num4);
					}
					if (zero != IntPtr.Zero)
					{
						A_5.c = new Certificate(zero);
					}
					Marshal.FreeHGlobal(intPtr);
					if (intPtr4 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr4);
					}
					if (intPtr5 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr5);
					}
				}
			}
			if (A_5.c != null)
			{
				if ((A_3 & MessageVerificationFlags.CertificateRevoked) > MessageVerificationFlags.None && Certificate.a(A_5.c))
				{
					A_5.d |= MessageVerificationFlags.CertificateRevoked;
				}
				if ((A_3 & MessageVerificationFlags.SignatureExpired) > MessageVerificationFlags.None && A_5.c.ValidToDate < DateTime.Now)
				{
					A_5.d |= MessageVerificationFlags.SignatureExpired;
				}
				if ((A_3 & MessageVerificationFlags.SignerAndSenderDoNotMatch) > MessageVerificationFlags.None && A_2 != null && string.Compare(A_5.c.EmailAddress, A_2, true) != 0)
				{
					A_5.d |= MessageVerificationFlags.SignerAndSenderDoNotMatch;
				}
				if ((A_3 & MessageVerificationFlags.Untrusted) > MessageVerificationFlags.None && A_5.c.Validate(A_4) != CertificateValidationFlags.None)
				{
					A_5.d |= MessageVerificationFlags.Untrusted;
				}
				if (this.f)
				{
					if (A_6)
					{
						A_5.e = new SignedCms(new ContentInfo(A_0));
						A_5.e.Decode(A_1);
						return;
					}
					A_5.e = new SignedCms();
					A_5.e.Decode(A_1);
				}
			}
		}

		// Token: 0x0400070F RID: 1807
		private CryptoServiceProvider a;

		// Token: 0x04000710 RID: 1808
		private Algorithm b;

		// Token: 0x04000711 RID: 1809
		private Algorithm c;

		// Token: 0x04000712 RID: 1810
		private bool d = true;

		// Token: 0x04000713 RID: 1811
		private int e;

		// Token: 0x04000714 RID: 1812
		private bool f;

		// Token: 0x04000715 RID: 1813
		private bool g;
	}
}
