using System;
using System.IO;
using System.Security.Cryptography;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002AB RID: 683
	internal class hh : bb
	{
		// Token: 0x060017D8 RID: 6104 RVA: 0x0006D0EE File Offset: 0x0006C0EE
		public hh()
		{
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0006D0FE File Offset: 0x0006C0FE
		public byte[] b()
		{
			return this.b;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0006D106 File Offset: 0x0006C106
		public new void a(byte[] A_0)
		{
			this.b = A_0;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0006D10F File Offset: 0x0006C10F
		public new c6 a()
		{
			return this.a;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0006D117 File Offset: 0x0006C117
		public new void a(c6 A_0)
		{
			this.a = A_0;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0006D120 File Offset: 0x0006C120
		public override bool bi(string A_0)
		{
			iq iq = this.a.e();
			int a_ = iq.d();
			int a_2 = iq.f();
			byte[] a_3 = base.a(this.a, A_0);
			byte[] a_4 = this.a(a_, iq.c(), null);
			byte[] a_5 = this.a(a_3, hh.d);
			SymmetricAlgorithm a_6 = this.a(a_, a_2, a_5, a_4);
			Array sourceArray = base.a(a_6, iq.e());
			HashAlgorithm hashAlgorithm = SHA1.Create();
			byte[] array = new byte[iq.c().Length];
			Array.Copy(sourceArray, 0, array, 0, array.Length);
			byte[] array2 = hashAlgorithm.ComputeHash(array);
			a_5 = this.a(a_3, hh.e);
			a_4 = this.a(a_, iq.c(), null);
			a_6 = this.a(a_, a_2, a_5, a_4);
			Array sourceArray2 = base.a(a_6, iq.g());
			array = new byte[array2.Length];
			Array.Copy(sourceArray2, 0, array, 0, array.Length);
			if (d4.a(array, array2))
			{
				a_5 = this.a(a_3, hh.f);
				a_4 = this.a(a_, iq.c(), null);
				a_6 = this.a(a_, a_2, a_5, a_4);
				Array sourceArray3 = base.a(a_6, iq.b());
				byte[] array3 = new byte[this.a.b().f() / 8];
				Array.Copy(sourceArray3, 0, array3, 0, array3.Length);
				this.b = array3;
				return true;
			}
			return false;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0006D285 File Offset: 0x0006C285
		public hh(c6 A_0)
		{
			this.a = A_0;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0006D29C File Offset: 0x0006C29C
		public override Stream bj(DirectoryNode A_0)
		{
			az az = A_0.a("EncryptedPackage");
			this.c = az.ax();
			return new cq(az, this.c, this);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0006D2CE File Offset: 0x0006C2CE
		public override long bk()
		{
			if (this.c == -1L)
			{
				throw new InvalidOperationException("EcmaDecryptor.getDataStream() was not called");
			}
			return this.c;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0006D2EC File Offset: 0x0006C2EC
		public new SymmetricAlgorithm a(int A_0, int A_1, byte[] A_2, byte[] A_3)
		{
			SymmetricAlgorithm result;
			try
			{
				string text = null;
				if (A_0 == 26126 || A_0 != 26127)
				{
				}
				if (A_1 == 2)
				{
					text = "CBC";
				}
				else if (A_1 == 3)
				{
					text = "CFB";
				}
				SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
				symmetricAlgorithm.Key = A_2;
				symmetricAlgorithm.IV = A_3;
				symmetricAlgorithm.Padding = PaddingMode.None;
				symmetricAlgorithm.Mode = ((text == "CBC") ? CipherMode.CBC : CipherMode.CFB);
				result = symmetricAlgorithm;
			}
			catch (CryptographicException ex)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0006D370 File Offset: 0x0006C370
		private new byte[] a(int A_0, byte[] A_1)
		{
			byte[] array = new byte[bb.a(A_0)];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 54;
			}
			Array.Copy(A_1, 0, array, 0, Math.Min(array.Length, A_1.Length));
			return array;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0006D3B4 File Offset: 0x0006C3B4
		private new byte[] a(byte[] A_0, byte[] A_1)
		{
			SHA1 sha = new SHA1CryptoServiceProvider();
			byte[] array = new byte[A_0.Length + A_1.Length];
			Array.Copy(A_0, array, A_0.Length);
			Array.Copy(A_1, 0, array, A_0.Length, A_1.Length);
			return this.a(this.a.e().d(), sha.ComputeHash(array));
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0006D40C File Offset: 0x0006C40C
		public new byte[] a(int A_0, byte[] A_1, byte[] A_2)
		{
			byte[] result;
			try
			{
				if (A_2 == null)
				{
					result = this.a(A_0, A_1);
				}
				else
				{
					HashAlgorithm hashAlgorithm = SHA1.Create();
					hashAlgorithm.ComputeHash(A_1);
					result = this.a(A_0, hashAlgorithm.ComputeHash(A_2));
				}
			}
			catch (CryptographicException ex)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x040011F0 RID: 4592
		private new c6 a;

		// Token: 0x040011F1 RID: 4593
		private byte[] b;

		// Token: 0x040011F2 RID: 4594
		private long c = -1L;

		// Token: 0x040011F3 RID: 4595
		private static byte[] d = new byte[]
		{
			254,
			167,
			210,
			118,
			59,
			75,
			158,
			121
		};

		// Token: 0x040011F4 RID: 4596
		private static byte[] e = new byte[]
		{
			215,
			170,
			15,
			109,
			48,
			97,
			52,
			78
		};

		// Token: 0x040011F5 RID: 4597
		private static byte[] f = new byte[]
		{
			20,
			110,
			11,
			231,
			171,
			172,
			208,
			214
		};
	}
}
