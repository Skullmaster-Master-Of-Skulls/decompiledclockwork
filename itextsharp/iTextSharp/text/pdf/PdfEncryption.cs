using System;
using System.IO;
using System.Text;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.crypto;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000160 RID: 352
	public class PdfEncryption
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x00048A24 File Offset: 0x00047A24
		public PdfEncryption()
		{
			this.md5 = DigestUtilities.GetDigest("MD5");
			this.publicKeyHandler = new PdfPublicKeySecurityHandler();
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00048A84 File Offset: 0x00047A84
		public PdfEncryption(PdfEncryption enc) : this()
		{
			this.mkey = (byte[])enc.mkey.Clone();
			this.ownerKey = (byte[])enc.ownerKey.Clone();
			this.userKey = (byte[])enc.userKey.Clone();
			this.permissions = enc.permissions;
			if (enc.documentID != null)
			{
				this.documentID = (byte[])enc.documentID.Clone();
			}
			this.revision = enc.revision;
			this.keyLength = enc.keyLength;
			this.encryptMetadata = enc.encryptMetadata;
			this.embeddedFilesOnly = enc.embeddedFilesOnly;
			this.publicKeyHandler = enc.publicKeyHandler;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00048B40 File Offset: 0x00047B40
		public void SetCryptoMode(int mode, int kl)
		{
			this.cryptoMode = mode;
			this.encryptMetadata = ((mode & 8) == 0);
			this.embeddedFilesOnly = ((mode & 24) != 0);
			mode &= 7;
			switch (mode)
			{
			case 0:
				this.encryptMetadata = true;
				this.embeddedFilesOnly = false;
				this.keyLength = 40;
				this.revision = 2;
				return;
			case 1:
				this.embeddedFilesOnly = false;
				if (kl > 0)
				{
					this.keyLength = kl;
				}
				else
				{
					this.keyLength = 128;
				}
				this.revision = 3;
				return;
			case 2:
				this.keyLength = 128;
				this.revision = 4;
				return;
			default:
				throw new ArgumentException(MessageLocalization.GetComposedMessage("no.valid.encryption.mode"));
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00048BF2 File Offset: 0x00047BF2
		public int GetCryptoMode()
		{
			return this.cryptoMode;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00048BFA File Offset: 0x00047BFA
		public bool IsMetadataEncrypted()
		{
			return this.encryptMetadata;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00048C02 File Offset: 0x00047C02
		public bool IsEmbeddedFilesOnly()
		{
			return this.embeddedFilesOnly;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00048C0C File Offset: 0x00047C0C
		private byte[] PadPassword(byte[] userPassword)
		{
			byte[] array = new byte[32];
			if (userPassword == null)
			{
				Array.Copy(PdfEncryption.pad, 0, array, 0, 32);
			}
			else
			{
				Array.Copy(userPassword, 0, array, 0, Math.Min(userPassword.Length, 32));
				if (userPassword.Length < 32)
				{
					Array.Copy(PdfEncryption.pad, 0, array, userPassword.Length, 32 - userPassword.Length);
				}
			}
			return array;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00048C68 File Offset: 0x00047C68
		private byte[] ComputeOwnerKey(byte[] userPad, byte[] ownerPad)
		{
			byte[] array = new byte[32];
			byte[] array2 = PdfEncryption.DigestComputeHash("MD5", ownerPad);
			if (this.revision == 3 || this.revision == 4)
			{
				byte[] array3 = new byte[this.keyLength / 8];
				for (int i = 0; i < 50; i++)
				{
					Array.Copy(PdfEncryption.DigestComputeHash("MD5", array2, 0, array3.Length), 0, array2, 0, array3.Length);
				}
				Array.Copy(userPad, 0, array, 0, 32);
				for (int j = 0; j < 20; j++)
				{
					for (int k = 0; k < array3.Length; k++)
					{
						array3[k] = (byte)((int)array2[k] ^ j);
					}
					this.rc4.PrepareARCFOURKey(array3);
					this.rc4.EncryptARCFOUR(array);
				}
			}
			else
			{
				this.rc4.PrepareARCFOURKey(array2, 0, 5);
				this.rc4.EncryptARCFOUR(userPad, array);
			}
			return array;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00048D44 File Offset: 0x00047D44
		private void SetupGlobalEncryptionKey(byte[] documentID, byte[] userPad, byte[] ownerKey, int permissions)
		{
			this.documentID = documentID;
			this.ownerKey = ownerKey;
			this.permissions = permissions;
			this.mkey = new byte[this.keyLength / 8];
			this.md5.Reset();
			this.md5.BlockUpdate(userPad, 0, userPad.Length);
			this.md5.BlockUpdate(ownerKey, 0, ownerKey.Length);
			byte[] input = new byte[]
			{
				(byte)permissions,
				(byte)(permissions >> 8),
				(byte)(permissions >> 16),
				(byte)(permissions >> 24)
			};
			this.md5.BlockUpdate(input, 0, 4);
			if (documentID != null)
			{
				this.md5.BlockUpdate(documentID, 0, documentID.Length);
			}
			if (!this.encryptMetadata)
			{
				this.md5.BlockUpdate(PdfEncryption.metadataPad, 0, PdfEncryption.metadataPad.Length);
			}
			byte[] array = new byte[this.md5.GetDigestSize()];
			this.md5.DoFinal(array, 0);
			byte[] array2 = new byte[this.mkey.Length];
			Array.Copy(array, 0, array2, 0, this.mkey.Length);
			this.md5.Reset();
			if (this.revision == 3 || this.revision == 4)
			{
				for (int i = 0; i < 50; i++)
				{
					Array.Copy(PdfEncryption.DigestComputeHash("MD5", array2), 0, array2, 0, this.mkey.Length);
				}
			}
			Array.Copy(array2, 0, this.mkey, 0, this.mkey.Length);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00048EA8 File Offset: 0x00047EA8
		private void SetupUserKey()
		{
			if (this.revision == 3 || this.revision == 4)
			{
				this.md5.BlockUpdate(PdfEncryption.pad, 0, PdfEncryption.pad.Length);
				this.md5.BlockUpdate(this.documentID, 0, this.documentID.Length);
				byte[] array = new byte[this.md5.GetDigestSize()];
				this.md5.DoFinal(array, 0);
				this.md5.Reset();
				Array.Copy(array, 0, this.userKey, 0, 16);
				for (int i = 16; i < 32; i++)
				{
					this.userKey[i] = 0;
				}
				for (int j = 0; j < 20; j++)
				{
					for (int k = 0; k < this.mkey.Length; k++)
					{
						array[k] = (byte)((int)this.mkey[k] ^ j);
					}
					this.rc4.PrepareARCFOURKey(array, 0, this.mkey.Length);
					this.rc4.EncryptARCFOUR(this.userKey, 0, 16);
				}
				return;
			}
			this.rc4.PrepareARCFOURKey(this.mkey);
			this.rc4.EncryptARCFOUR(PdfEncryption.pad, this.userKey);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00048FCC File Offset: 0x00047FCC
		public void SetupAllKeys(byte[] userPassword, byte[] ownerPassword, int permissions)
		{
			if (ownerPassword == null || ownerPassword.Length == 0)
			{
				ownerPassword = PdfEncryption.DigestComputeHash("MD5", PdfEncryption.CreateDocumentId());
			}
			this.md5.Reset();
			permissions |= ((this.revision == 3 || this.revision == 4) ? -3904 : -64);
			permissions &= -4;
			byte[] userPad = this.PadPassword(userPassword);
			byte[] ownerPad = this.PadPassword(ownerPassword);
			this.ownerKey = this.ComputeOwnerKey(userPad, ownerPad);
			this.documentID = PdfEncryption.CreateDocumentId();
			this.SetupByUserPad(this.documentID, userPad, this.ownerKey, permissions);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00049060 File Offset: 0x00048060
		public static byte[] CreateDocumentId()
		{
			long num = DateTime.Now.Ticks + (long)Environment.TickCount;
			long totalMemory = GC.GetTotalMemory(false);
			object[] array = new object[5];
			array[0] = num;
			array[1] = "+";
			array[2] = totalMemory;
			array[3] = "+";
			object[] array2 = array;
			int num2 = 4;
			long num3 = PdfEncryption.seq;
			PdfEncryption.seq = num3 + 1L;
			array2[num2] = num3;
			string s = string.Concat(array);
			byte[] bytes = Encoding.ASCII.GetBytes(s);
			return PdfEncryption.DigestComputeHash("MD5", bytes);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000490EE File Offset: 0x000480EE
		public void SetupByUserPassword(byte[] documentID, byte[] userPassword, byte[] ownerKey, int permissions)
		{
			this.SetupByUserPad(documentID, this.PadPassword(userPassword), ownerKey, permissions);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00049101 File Offset: 0x00048101
		private void SetupByUserPad(byte[] documentID, byte[] userPad, byte[] ownerKey, int permissions)
		{
			this.SetupGlobalEncryptionKey(documentID, userPad, ownerKey, permissions);
			this.SetupUserKey();
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00049114 File Offset: 0x00048114
		public void SetupByOwnerPassword(byte[] documentID, byte[] ownerPassword, byte[] userKey, byte[] ownerKey, int permissions)
		{
			this.SetupByOwnerPad(documentID, this.PadPassword(ownerPassword), userKey, ownerKey, permissions);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0004912C File Offset: 0x0004812C
		private void SetupByOwnerPad(byte[] documentID, byte[] ownerPad, byte[] userKey, byte[] ownerKey, int permissions)
		{
			byte[] userPad = this.ComputeOwnerKey(ownerKey, ownerPad);
			this.SetupGlobalEncryptionKey(documentID, userPad, ownerKey, permissions);
			this.SetupUserKey();
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00049155 File Offset: 0x00048155
		public void SetupByEncryptionKey(byte[] key, int keylength)
		{
			this.mkey = new byte[keylength / 8];
			Array.Copy(key, 0, this.mkey, 0, this.mkey.Length);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0004917C File Offset: 0x0004817C
		public void SetHashKey(int number, int generation)
		{
			this.md5.Reset();
			this.extra[0] = (byte)number;
			this.extra[1] = (byte)(number >> 8);
			this.extra[2] = (byte)(number >> 16);
			this.extra[3] = (byte)generation;
			this.extra[4] = (byte)(generation >> 8);
			this.md5.BlockUpdate(this.mkey, 0, this.mkey.Length);
			this.md5.BlockUpdate(this.extra, 0, this.extra.Length);
			if (this.revision == 4)
			{
				this.md5.BlockUpdate(PdfEncryption.salt, 0, PdfEncryption.salt.Length);
			}
			this.key = new byte[this.md5.GetDigestSize()];
			this.md5.DoFinal(this.key, 0);
			this.md5.Reset();
			this.keySize = this.mkey.Length + 5;
			if (this.keySize > 16)
			{
				this.keySize = 16;
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00049278 File Offset: 0x00048278
		public static PdfObject CreateInfoId(byte[] id)
		{
			ByteBuffer byteBuffer = new ByteBuffer(90);
			byteBuffer.Append('[').Append('<');
			for (int i = 0; i < 16; i++)
			{
				byteBuffer.AppendHex(id[i]);
			}
			byteBuffer.Append('>').Append('<');
			id = PdfEncryption.CreateDocumentId();
			for (int j = 0; j < 16; j++)
			{
				byteBuffer.AppendHex(id[j]);
			}
			byteBuffer.Append('>').Append(']');
			return new PdfLiteral(byteBuffer.ToByteArray());
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00049300 File Offset: 0x00048300
		public PdfDictionary GetEncryptionDictionary()
		{
			PdfDictionary pdfDictionary = new PdfDictionary();
			if (this.publicKeyHandler.GetRecipientsSize() > 0)
			{
				pdfDictionary.Put(PdfName.FILTER, PdfName.PUBSEC);
				pdfDictionary.Put(PdfName.R, new PdfNumber(this.revision));
				PdfArray encodedRecipients = this.publicKeyHandler.GetEncodedRecipients();
				if (this.revision == 2)
				{
					pdfDictionary.Put(PdfName.V, new PdfNumber(1));
					pdfDictionary.Put(PdfName.SUBFILTER, PdfName.ADBE_PKCS7_S4);
					pdfDictionary.Put(PdfName.RECIPIENTS, encodedRecipients);
				}
				else if (this.revision == 3 && this.encryptMetadata)
				{
					pdfDictionary.Put(PdfName.V, new PdfNumber(2));
					pdfDictionary.Put(PdfName.LENGTH, new PdfNumber(128));
					pdfDictionary.Put(PdfName.SUBFILTER, PdfName.ADBE_PKCS7_S4);
					pdfDictionary.Put(PdfName.RECIPIENTS, encodedRecipients);
				}
				else
				{
					pdfDictionary.Put(PdfName.R, new PdfNumber(4));
					pdfDictionary.Put(PdfName.V, new PdfNumber(4));
					pdfDictionary.Put(PdfName.SUBFILTER, PdfName.ADBE_PKCS7_S5);
					PdfDictionary pdfDictionary2 = new PdfDictionary();
					pdfDictionary2.Put(PdfName.RECIPIENTS, encodedRecipients);
					if (!this.encryptMetadata)
					{
						pdfDictionary2.Put(PdfName.ENCRYPTMETADATA, PdfBoolean.PDFFALSE);
					}
					if (this.revision == 4)
					{
						pdfDictionary2.Put(PdfName.CFM, PdfName.AESV2);
					}
					else
					{
						pdfDictionary2.Put(PdfName.CFM, PdfName.V2);
					}
					PdfDictionary pdfDictionary3 = new PdfDictionary();
					pdfDictionary3.Put(PdfName.DEFAULTCRYPTFILTER, pdfDictionary2);
					pdfDictionary.Put(PdfName.CF, pdfDictionary3);
					if (this.embeddedFilesOnly)
					{
						pdfDictionary.Put(PdfName.EFF, PdfName.DEFAULTCRYPTFILTER);
						pdfDictionary.Put(PdfName.STRF, PdfName.IDENTITY);
						pdfDictionary.Put(PdfName.STMF, PdfName.IDENTITY);
					}
					else
					{
						pdfDictionary.Put(PdfName.STRF, PdfName.DEFAULTCRYPTFILTER);
						pdfDictionary.Put(PdfName.STMF, PdfName.DEFAULTCRYPTFILTER);
					}
				}
				IDigest digest = DigestUtilities.GetDigest("SHA1");
				byte[] seed = this.publicKeyHandler.GetSeed();
				digest.BlockUpdate(seed, 0, seed.Length);
				for (int i = 0; i < this.publicKeyHandler.GetRecipientsSize(); i++)
				{
					byte[] encodedRecipient = this.publicKeyHandler.GetEncodedRecipient(i);
					digest.BlockUpdate(encodedRecipient, 0, encodedRecipient.Length);
				}
				if (!this.encryptMetadata)
				{
					digest.BlockUpdate(PdfEncryption.metadataPad, 0, PdfEncryption.metadataPad.Length);
				}
				byte[] output = new byte[digest.GetDigestSize()];
				digest.DoFinal(output, 0);
				this.SetupByEncryptionKey(output, this.keyLength);
			}
			else
			{
				pdfDictionary.Put(PdfName.FILTER, PdfName.STANDARD);
				pdfDictionary.Put(PdfName.O, new PdfLiteral(PdfContentByte.EscapeString(this.ownerKey)));
				pdfDictionary.Put(PdfName.U, new PdfLiteral(PdfContentByte.EscapeString(this.userKey)));
				pdfDictionary.Put(PdfName.P, new PdfNumber(this.permissions));
				pdfDictionary.Put(PdfName.R, new PdfNumber(this.revision));
				if (this.revision == 2)
				{
					pdfDictionary.Put(PdfName.V, new PdfNumber(1));
				}
				else if (this.revision == 3 && this.encryptMetadata)
				{
					pdfDictionary.Put(PdfName.V, new PdfNumber(2));
					pdfDictionary.Put(PdfName.LENGTH, new PdfNumber(128));
				}
				else
				{
					if (!this.encryptMetadata)
					{
						pdfDictionary.Put(PdfName.ENCRYPTMETADATA, PdfBoolean.PDFFALSE);
					}
					pdfDictionary.Put(PdfName.R, new PdfNumber(4));
					pdfDictionary.Put(PdfName.V, new PdfNumber(4));
					pdfDictionary.Put(PdfName.LENGTH, new PdfNumber(128));
					PdfDictionary pdfDictionary4 = new PdfDictionary();
					pdfDictionary4.Put(PdfName.LENGTH, new PdfNumber(16));
					if (this.embeddedFilesOnly)
					{
						pdfDictionary4.Put(PdfName.AUTHEVENT, PdfName.EFOPEN);
						pdfDictionary.Put(PdfName.EFF, PdfName.STDCF);
						pdfDictionary.Put(PdfName.STRF, PdfName.IDENTITY);
						pdfDictionary.Put(PdfName.STMF, PdfName.IDENTITY);
					}
					else
					{
						pdfDictionary4.Put(PdfName.AUTHEVENT, PdfName.DOCOPEN);
						pdfDictionary.Put(PdfName.STRF, PdfName.STDCF);
						pdfDictionary.Put(PdfName.STMF, PdfName.STDCF);
					}
					if (this.revision == 4)
					{
						pdfDictionary4.Put(PdfName.CFM, PdfName.AESV2);
					}
					else
					{
						pdfDictionary4.Put(PdfName.CFM, PdfName.V2);
					}
					PdfDictionary pdfDictionary5 = new PdfDictionary();
					pdfDictionary5.Put(PdfName.STDCF, pdfDictionary4);
					pdfDictionary.Put(PdfName.CF, pdfDictionary5);
				}
			}
			return pdfDictionary;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00049795 File Offset: 0x00048795
		public PdfObject FileID
		{
			get
			{
				return PdfEncryption.CreateInfoId(this.documentID);
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000497A2 File Offset: 0x000487A2
		public OutputStreamEncryption GetEncryptionStream(Stream os)
		{
			return new OutputStreamEncryption(os, this.key, 0, this.keySize, this.revision);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000497BD File Offset: 0x000487BD
		public int CalculateStreamSize(int n)
		{
			if (this.revision == 4)
			{
				return (n & 2147483632) + 32;
			}
			return n;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000497D4 File Offset: 0x000487D4
		public byte[] EncryptByteArray(byte[] b)
		{
			MemoryStream memoryStream = new MemoryStream();
			OutputStreamEncryption encryptionStream = this.GetEncryptionStream(memoryStream);
			encryptionStream.Write(b, 0, b.Length);
			encryptionStream.Finish();
			return memoryStream.ToArray();
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00049806 File Offset: 0x00048806
		public StandardDecryption GetDecryptor()
		{
			return new StandardDecryption(this.key, 0, this.keySize, this.revision);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00049820 File Offset: 0x00048820
		public byte[] DecryptByteArray(byte[] b)
		{
			MemoryStream memoryStream = new MemoryStream();
			StandardDecryption decryptor = this.GetDecryptor();
			byte[] array = decryptor.Update(b, 0, b.Length);
			if (array != null)
			{
				memoryStream.Write(array, 0, array.Length);
			}
			array = decryptor.Finish();
			if (array != null)
			{
				memoryStream.Write(array, 0, array.Length);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0004986F File Offset: 0x0004886F
		public void AddRecipient(X509Certificate cert, int permission)
		{
			this.documentID = PdfEncryption.CreateDocumentId();
			this.publicKeyHandler.AddRecipient(new PdfPublicKeyRecipient(cert, permission));
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00049890 File Offset: 0x00048890
		public byte[] ComputeUserPassword(byte[] ownerPassword)
		{
			byte[] array = this.ComputeOwnerKey(this.ownerKey, this.PadPassword(ownerPassword));
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = true;
				for (int j = 0; j < array.Length - i; j++)
				{
					if (array[i + j] != PdfEncryption.pad[j])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					byte[] array2 = new byte[i];
					Array.Copy(array, 0, array2, 0, i);
					return array2;
				}
			}
			return array;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00049900 File Offset: 0x00048900
		public static byte[] DigestComputeHash(string algo, byte[] b, int offset, int len)
		{
			IDigest digest = DigestUtilities.GetDigest(algo);
			digest.BlockUpdate(b, offset, len);
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return array;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00049933 File Offset: 0x00048933
		public static byte[] DigestComputeHash(string algo, byte[] b)
		{
			return PdfEncryption.DigestComputeHash(algo, b, 0, b.Length);
		}

		// Token: 0x040009D9 RID: 2521
		public const int STANDARD_ENCRYPTION_40 = 2;

		// Token: 0x040009DA RID: 2522
		public const int STANDARD_ENCRYPTION_128 = 3;

		// Token: 0x040009DB RID: 2523
		public const int AES_128 = 4;

		// Token: 0x040009DC RID: 2524
		private static byte[] pad = new byte[]
		{
			40,
			191,
			78,
			94,
			78,
			117,
			138,
			65,
			100,
			0,
			78,
			86,
			byte.MaxValue,
			250,
			1,
			8,
			46,
			46,
			0,
			182,
			208,
			104,
			62,
			128,
			47,
			12,
			169,
			254,
			100,
			83,
			105,
			122
		};

		// Token: 0x040009DD RID: 2525
		private static readonly byte[] salt = new byte[]
		{
			115,
			65,
			108,
			84
		};

		// Token: 0x040009DE RID: 2526
		internal static readonly byte[] metadataPad = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x040009DF RID: 2527
		internal byte[] key;

		// Token: 0x040009E0 RID: 2528
		internal int keySize;

		// Token: 0x040009E1 RID: 2529
		internal byte[] mkey;

		// Token: 0x040009E2 RID: 2530
		internal byte[] extra = new byte[5];

		// Token: 0x040009E3 RID: 2531
		internal IDigest md5;

		// Token: 0x040009E4 RID: 2532
		internal byte[] ownerKey = new byte[32];

		// Token: 0x040009E5 RID: 2533
		internal byte[] userKey = new byte[32];

		// Token: 0x040009E6 RID: 2534
		protected PdfPublicKeySecurityHandler publicKeyHandler;

		// Token: 0x040009E7 RID: 2535
		internal int permissions;

		// Token: 0x040009E8 RID: 2536
		internal byte[] documentID;

		// Token: 0x040009E9 RID: 2537
		internal static long seq = DateTime.Now.Ticks + (long)Environment.TickCount;

		// Token: 0x040009EA RID: 2538
		private int revision;

		// Token: 0x040009EB RID: 2539
		private ARCFOUREncryption rc4 = new ARCFOUREncryption();

		// Token: 0x040009EC RID: 2540
		private int keyLength;

		// Token: 0x040009ED RID: 2541
		private bool encryptMetadata;

		// Token: 0x040009EE RID: 2542
		private bool embeddedFilesOnly;

		// Token: 0x040009EF RID: 2543
		private int cryptoMode;
	}
}
