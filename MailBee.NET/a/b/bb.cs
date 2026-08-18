using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002AC RID: 684
	internal abstract class bb
	{
		// Token: 0x060017E6 RID: 6118
		public abstract Stream bj(DirectoryNode A_0);

		// Token: 0x060017E7 RID: 6119
		public abstract bool bi(string A_0);

		// Token: 0x060017E8 RID: 6120
		public abstract long bk();

		// Token: 0x060017E9 RID: 6121 RVA: 0x0006D4A8 File Offset: 0x0006C4A8
		public static bb a(c6 A_0)
		{
			int num = A_0.c();
			int num2 = A_0.a();
			if (num == 4 && num2 == 4)
			{
				return new hh(A_0);
			}
			if (num2 == 2 && (num == 3 || num == 4))
			{
				return new an(A_0);
			}
			throw new EncryptedDocumentException("Unsupported version");
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0006D4EF File Offset: 0x0006C4EF
		public Stream a(h0 A_0)
		{
			return this.bj(A_0.m());
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0006D4FD File Offset: 0x0006C4FD
		public Stream a(POIFSFileSystem A_0)
		{
			return this.bj(A_0.Root);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0006D50B File Offset: 0x0006C50B
		protected static int a(int A_0)
		{
			switch (A_0)
			{
			case 26126:
				return 16;
			case 26127:
				return 24;
			case 26128:
				return 32;
			default:
				throw new EncryptedDocumentException("Unknown block size");
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0006D53C File Offset: 0x0006C53C
		protected byte[] a(c6 A_0, string A_1)
		{
			HashAlgorithm hashAlgorithm = SHA1.Create();
			byte[] bytes = Encoding.Unicode.GetBytes(A_1);
			byte[] array = A_0.e().c();
			byte[] array2 = new byte[array.Length + bytes.Length];
			Array.Copy(array, array2, array.Length);
			Array.Copy(bytes, 0, array2, array.Length, bytes.Length);
			byte[] array3 = hashAlgorithm.ComputeHash(array2);
			byte[] array4 = new byte[4];
			array2 = new byte[24];
			for (int i = 0; i < A_0.e().a(); i++)
			{
				p.c(array4, 0, i);
				Array.Copy(array4, array2, array4.Length);
				Array.Copy(array3, 0, array2, array4.Length, array3.Length);
				array3 = hashAlgorithm.ComputeHash(array2);
			}
			return array3;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0006D5F4 File Offset: 0x0006C5F4
		protected byte[] a(SymmetricAlgorithm A_0, byte[] A_1)
		{
			byte[] result = new byte[0];
			using (MemoryStream memoryStream = new MemoryStream(A_1))
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, A_0.CreateDecryptor(A_0.Key, A_0.IV), CryptoStreamMode.Read))
				{
					using (MemoryStream memoryStream2 = new MemoryStream())
					{
						byte[] buffer = new byte[100];
						int count;
						while ((count = cryptoStream.Read(buffer, 0, 100)) > 0)
						{
							memoryStream2.Write(buffer, 0, count);
						}
						result = memoryStream2.ToArray();
					}
				}
			}
			return result;
		}

		// Token: 0x040011F6 RID: 4598
		public const string a = "VelvetSweatshop";
	}
}
