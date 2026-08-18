using System;
using System.IO;
using System.Security.Cryptography;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002AE RID: 686
	internal class an : bb
	{
		// Token: 0x06001803 RID: 6147 RVA: 0x0006D9D6 File Offset: 0x0006C9D6
		public an(c6 A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0006D9F0 File Offset: 0x0006C9F0
		private new byte[] a(int A_0)
		{
			byte[] result;
			try
			{
				HashAlgorithm hashAlgorithm = SHA1.Create();
				byte[] array = new byte[4];
				p.c(array, 0, A_0);
				byte[] array2 = new byte[array.Length + this.b.Length];
				Array.Copy(this.b, array2, this.b.Length);
				Array.Copy(array, 0, array2, this.b.Length, array.Length);
				byte[] array3 = hashAlgorithm.ComputeHash(array2);
				int a_ = this.a.b().f() / 8;
				byte[] array4 = new byte[64];
				for (int i = 0; i < array4.Length; i++)
				{
					array4[i] = 54;
				}
				for (int j = 0; j < array3.Length; j++)
				{
					array4[j] ^= array3[j];
				}
				byte[] array5 = hashAlgorithm.ComputeHash(array4);
				for (int k = 0; k < array4.Length; k++)
				{
					array4[k] = 92;
				}
				for (int l = 0; l < array3.Length; l++)
				{
					array4[l] ^= array3[l];
				}
				byte[] array6 = hashAlgorithm.ComputeHash(array4);
				byte[] array7 = new byte[array5.Length + array6.Length];
				Array.Copy(array5, 0, array7, 0, array5.Length);
				Array.Copy(array6, 0, array7, array5.Length, array6.Length);
				result = this.a(array7, a_);
			}
			catch (CryptographicException ex)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0006DB5C File Offset: 0x0006CB5C
		public override bool bi(string A_0)
		{
			this.b = base.a(this.a, A_0);
			SymmetricAlgorithm a_ = this.a();
			byte[] buffer = base.a(a_, this.a.e().e());
			byte[] array = SHA1.Create().ComputeHash(buffer);
			base.a(a_, this.a.e().g());
			byte[] a_2 = this.a(array, array.Length);
			return d4.a(array, a_2);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0006DBD4 File Offset: 0x0006CBD4
		private new byte[] a(byte[] A_0, int A_1)
		{
			byte[] array = new byte[A_1];
			Array.Copy(A_0, 0, array, 0, Math.Min(A_1, A_0.Length));
			if (A_1 > A_0.Length)
			{
				for (int i = A_0.Length; i < A_1; i++)
				{
					array[i] = 0;
				}
			}
			return array;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0006DC14 File Offset: 0x0006CC14
		private new SymmetricAlgorithm a()
		{
			byte[] key = this.a(0);
			SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
			symmetricAlgorithm.Mode = CipherMode.ECB;
			symmetricAlgorithm.Padding = PaddingMode.None;
			symmetricAlgorithm.Key = key;
			return symmetricAlgorithm;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0006DC44 File Offset: 0x0006CC44
		public override Stream bj(DirectoryNode A_0)
		{
			az az = A_0.a("EncryptedPackage");
			this.c = az.ax();
			SymmetricAlgorithm symmetricAlgorithm = this.a();
			return new CryptoStream(az, symmetricAlgorithm.CreateDecryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV), CryptoStreamMode.Read);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0006DC89 File Offset: 0x0006CC89
		public override long bk()
		{
			if (this.c == -1L)
			{
				throw new InvalidOperationException("EcmaDecryptor.getDataStream() was not called");
			}
			return this.c;
		}

		// Token: 0x040011FE RID: 4606
		private new c6 a;

		// Token: 0x040011FF RID: 4607
		private byte[] b;

		// Token: 0x04001200 RID: 4608
		private long c = -1L;
	}
}
