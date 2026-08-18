using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000268 RID: 616
	internal class bs
	{
		// Token: 0x06001609 RID: 5641 RVA: 0x00062FE2 File Offset: 0x00061FE2
		public virtual int f()
		{
			return this.af;
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00062FEA File Offset: 0x00061FEA
		public virtual int a()
		{
			return this.y;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00062FF2 File Offset: 0x00061FF2
		public virtual Stream g()
		{
			return this.ae;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00062FFC File Offset: 0x00061FFC
		public virtual fk e()
		{
			dx a_ = this.f(33L);
			return new fk(this, a_);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x0006301C File Offset: 0x0006201C
		public virtual bj b()
		{
			dx a_ = this.f(290L);
			return new bj(this, a_);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00063040 File Offset: 0x00062040
		public bs(string A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			try
			{
				this.b(new FileStream(A_0, FileMode.Open, FileAccess.Read));
			}
			catch (ArgumentException a_)
			{
				throw new MailBeeIOException(20, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (IOException a_3)
			{
				throw new MailBeeIOException(30, a_3);
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000630E0 File Offset: 0x000620E0
		public bs(Stream A_0)
		{
			this.b(A_0);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00063108 File Offset: 0x00062108
		private void b(Stream A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!A_0.CanRead || !A_0.CanSeek)
			{
				throw new MailBeeStreamException(40);
			}
			this.ae = A_0;
			try
			{
				byte[] array = new byte[4];
				this.ae.Read(array, 0, array.Length);
				string @string = Encoding.UTF8.GetString(array, 0, array.Length);
				if (!@string.Equals("!BDN"))
				{
					throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstInvalidFileHeader0, @string), 1210);
				}
				byte[] array2 = new byte[2];
				this.ae.Seek(10L, SeekOrigin.Begin);
				this.ae.Read(array2, 0, array2.Length);
				if (array2[0] == 15)
				{
					array2[0] = 14;
				}
				if (array2[0] != 14 && array2[0] != 23)
				{
					throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstUnrecognisedPstFileVersion0, array2[0]), 1210);
				}
				this.af = (int)array2[0];
				if (this.f() == 14)
				{
					this.ae.Seek(461L, SeekOrigin.Begin);
				}
				else
				{
					this.ae.Seek(513L, SeekOrigin.Begin);
				}
				this.y = (int)((byte)this.ae.ReadByte());
				if (this.y == 2)
				{
					throw new MailBeePstParsingException(Resources.Instance.ErrorDesc_OutlookPstOnlyUnencryptedFilesSupported, 1210);
				}
				this.a(this.ae);
			}
			catch (IOException a_)
			{
				throw new MailBeePstParsingException(1210, a_);
			}
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00063298 File Offset: 0x00062298
		private void a(Stream A_0)
		{
			this.ag = new Dictionary<string, int>();
			for (int i = 0; i < bs.w.Length; i++)
			{
				a4 a_ = a4.a(bs.w[i]);
				this.x.a(a_, i);
			}
			dx dx = this.f(97L);
			fb a_2 = null;
			if (dx.c != 0L)
			{
				a_2 = this.d(dx.c);
			}
			hp a_3 = this.e(dx.b);
			di di = new di(this, a_3);
			byte[] a_4 = new byte[1024];
			di.b(a_4);
			gs gs = new c0(di).a();
			e2 a_5 = gs.b(2);
			this.ac = this.a(a_5, a_2);
			int num = this.ac.Length / 16;
			a4[] array = new a4[num];
			int[] array2 = new int[num];
			int num2 = 0;
			for (int j = 0; j < num; j++)
			{
				byte[] array3 = new byte[16];
				Array.Copy(this.ac, num2, array3, 0, 16);
				array[j] = new a4(array3);
				if (this.x.a(array[j]))
				{
					array2[j] = this.x.b(array[j]);
				}
				else
				{
					array2[j] = -1;
				}
				num2 += 16;
			}
			e2 a_6 = gs.b(3);
			byte[] array4 = this.a(a_6, a_2);
			e2 a_7 = gs.b(4);
			byte[] array5 = this.a(a_7, a_2);
			int num3 = 0;
			while (num3 + 8 < array4.Length)
			{
				int num4 = (int)ii.b(array4, num3, num3 + 4);
				int num5 = (int)ii.b(array4, num3 + 4, num3 + 6);
				int num6 = (int)ii.b(array4, num3 + 6, num3 + 8);
				if ((num5 & 1) == 0)
				{
					num6 += 32768;
					num5 >>= 1;
					int num7;
					if (num5 == 1)
					{
						num7 = 12;
					}
					else if (num5 == 2)
					{
						num7 = 0;
					}
					else
					{
						num7 = array2[num5 - 3];
					}
					this.aa.a((long)((ulong)num4 | (ulong)((ulong)((long)num7) << 32)), num6);
					bs.ab.a(num6, (long)num4);
				}
				else
				{
					int num8 = (int)ii.b(array5, num4, num4 + 4);
					new byte[num8];
					string @string = Encoding.GetEncoding("UTF-16LE").GetString(array5, num4 + 4, num8);
					this.ag.Add(@string, num6 + 32768);
				}
				num3 += 8;
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000634FC File Offset: 0x000624FC
		public Dictionary<string, int> h()
		{
			return this.ag;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00063504 File Offset: 0x00062504
		private byte[] a(hy A_0, fb A_1)
		{
			if (A_0.h.Length != 0)
			{
				return A_0.h;
			}
			if (A_1 == null)
			{
				throw new MailBeePstParsingException(Resources.Instance.ErrorDesc_OutlookPstExternalReferenceButNoLocalDescriptorItems, 1210);
			}
			if (A_0.f != 258)
			{
				throw new MailBeePstParsingException(Resources.Instance.ErrorDesc_OutlookPstAttemptingToGetNonBinaryData, 1210);
			}
			return A_1.b(A_0.g).b();
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00063570 File Offset: 0x00062570
		internal virtual int b(int A_0, int A_1)
		{
			long a_ = (long)A_1 << 32 | (long)((ulong)A_0);
			if (!this.aa.a(a_))
			{
				return -1;
			}
			return this.aa.b(a_);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000635A2 File Offset: 0x000625A2
		internal static long b(int A_0)
		{
			if (!bs.ab.a(A_0))
			{
				return -1L;
			}
			return bs.ab.b(A_0);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000635C0 File Offset: 0x000625C0
		internal static string a(int A_0)
		{
			if (A_0 == 20127)
			{
				return "Windows-1252";
			}
			if (A_0 == 50932)
			{
				return "_autodetect";
			}
			if (A_0 != 50949)
			{
				string result;
				try
				{
					result = Encoding.GetEncoding(A_0).WebName;
				}
				catch (NotSupportedException)
				{
					result = null;
				}
				return result;
			}
			return "_autodetect_kr";
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00063620 File Offset: 0x00062620
		internal static string a(int A_0, bool A_1)
		{
			if (bs.ai)
			{
				bs.ai = false;
				bs.ah = new dd();
			}
			if (bs.ah != null)
			{
				string a_ = string.Format(A_1 ? "{0:X8}" : "{0:X4}", A_0);
				return bs.ah.a(a_);
			}
			return null;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00063674 File Offset: 0x00062674
		internal static string a(int A_0, int A_1)
		{
			string result = string.Empty;
			if (A_0 < 32768)
			{
				string text = bs.a(A_0, false);
				if (text != null)
				{
					result = string.Format("{0}:{1:X4}: ", text, A_1);
				}
				else
				{
					result = string.Format("0x{0:X4}:{1:X4}: ", A_0, A_1);
				}
			}
			else
			{
				long num = bs.b(A_0);
				if (num == -1L)
				{
					result = string.Format("0xFFFF({0:X4}):{1:X4}: ", A_0, A_1);
				}
				else
				{
					string text2 = bs.a((int)num, true);
					if (text2 != null)
					{
						result = string.Format("{0}({1:X4}):{2:X4}: ", text2, A_0, A_1);
					}
					else
					{
						result = string.Format("0x{0:X4}({1:X4}):{2:X4}: ", num, A_0, A_1);
					}
				}
			}
			return result;
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00063733 File Offset: 0x00062733
		public void c()
		{
			this.ae.Close();
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00063740 File Offset: 0x00062740
		internal virtual di c(long A_0)
		{
			hp a_ = this.e(A_0);
			return new di(this, a_);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0006375C File Offset: 0x0006275C
		public virtual int b(long A_0)
		{
			hp hp = this.e(A_0);
			if ((hp.a & 2L) == 0L)
			{
				return hp.c;
			}
			byte[] array = new byte[8];
			this.ae.Seek(hp.b, SeekOrigin.Begin);
			this.ae.Read(array, 0, array.Length);
			return (int)ii.b(array, 4, 8);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000637B8 File Offset: 0x000627B8
		protected internal virtual long g(long A_0)
		{
			long num = 0L;
			if (this.f() == 14)
			{
				this.ae.Seek(A_0, SeekOrigin.Begin);
				byte[] array = new byte[4];
				this.ae.Read(array, 0, array.Length);
				num |= (long)((ulong)(array[3] & byte.MaxValue));
				num <<= 8;
				num |= (long)((ulong)(array[2] & byte.MaxValue));
				num <<= 8;
				num |= (long)((ulong)(array[1] & byte.MaxValue));
				num <<= 8;
				num |= (long)((ulong)(array[0] & byte.MaxValue));
			}
			else
			{
				this.ae.Seek(A_0, SeekOrigin.Begin);
				byte[] array2 = new byte[8];
				this.ae.Read(array2, 0, array2.Length);
				num = (long)(array2[7] & byte.MaxValue);
				for (int i = 6; i >= 0; i--)
				{
					num <<= 8;
					long num2 = (long)((ulong)array2[i] & 255UL);
					num |= num2;
				}
			}
			return num;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00063894 File Offset: 0x00062894
		private byte[] a(Stream A_0, long A_1, bool A_2)
		{
			long num;
			if (this.f() == 14)
			{
				num = this.g(196L);
				if (A_2)
				{
					num = this.g(188L);
				}
			}
			else
			{
				num = this.g(240L);
				if (A_2)
				{
					num = this.g(224L);
				}
			}
			byte[] array = new byte[2];
			if (this.f() == 14)
			{
				A_0.Seek(num + 500L, SeekOrigin.Begin);
			}
			else
			{
				A_0.Seek(num + 496L, SeekOrigin.Begin);
			}
			A_0.Read(array, 0, array.Length);
			while ((array[0] == 128 && array[1] == 128 && !A_2) || (array[0] == 129 && array[1] == 129 && A_2))
			{
				byte[] array2;
				if (this.f() == 14)
				{
					array2 = new byte[496];
				}
				else
				{
					array2 = new byte[488];
				}
				A_0.Seek(num, SeekOrigin.Begin);
				A_0.Read(array2, 0, array2.Length);
				int num2 = A_0.ReadByte();
				A_0.ReadByte();
				A_0.ReadByte();
				if (A_0.ReadByte() <= 0)
				{
					for (int i = 0; i < num2; i++)
					{
						if (this.f() == 14)
						{
							if (A_2)
							{
								A_0.Seek(num + (long)(i * 16), SeekOrigin.Begin);
								array = new byte[4];
								A_0.Read(array, 0, array.Length);
								if (ii.a(array) == A_1)
								{
									A_0.Seek(num + (long)(i * 16), SeekOrigin.Begin);
									array = new byte[16];
									A_0.Read(array, 0, array.Length);
									return array;
								}
							}
							else if (this.g(num + (long)(i * 12)) == A_1)
							{
								A_0.Seek(num + (long)(i * 12), SeekOrigin.Begin);
								array = new byte[12];
								A_0.Read(array, 0, array.Length);
								return array;
							}
						}
						else if (A_2)
						{
							A_0.Seek(num + (long)(i * 32), SeekOrigin.Begin);
							array = new byte[4];
							A_0.Read(array, 0, array.Length);
							if (ii.a(array) == A_1)
							{
								A_0.Seek(num + (long)(i * 32), SeekOrigin.Begin);
								array = new byte[32];
								A_0.Read(array, 0, array.Length);
								return array;
							}
						}
						else if (this.g(num + (long)(i * 24)) == A_1)
						{
							A_0.Seek(num + (long)(i * 24), SeekOrigin.Begin);
							array = new byte[24];
							A_0.Read(array, 0, array.Length);
							return array;
						}
					}
					throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstUnableToFind0, A_1), 1210);
				}
				bool flag = false;
				for (int j = 0; j < num2; j++)
				{
					if (this.f() == 14)
					{
						if (this.g(num + (long)(j * 12)) > A_1)
						{
							num = this.g(num + (long)((j - 1) * 12) + 8L);
							A_0.Seek(num + 500L, SeekOrigin.Begin);
							A_0.Read(array, 0, array.Length);
							flag = true;
							break;
						}
					}
					else if (this.g(num + (long)(j * 24)) > A_1)
					{
						num = this.g(num + (long)((j - 1) * 24) + 16L);
						A_0.Seek(num + 496L, SeekOrigin.Begin);
						A_0.Read(array, 0, array.Length);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (this.f() == 14)
					{
						num = this.g(num + (long)((num2 - 1) * 12) + 8L);
						A_0.Seek(num + 500L, SeekOrigin.Begin);
						A_0.Read(array, 0, array.Length);
					}
					else
					{
						num = this.g(num + (long)((num2 - 1) * 24) + 16L);
						A_0.Seek(num + 496L, SeekOrigin.Begin);
						A_0.Read(array, 0, array.Length);
					}
				}
			}
			throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstUnableToFindNode0, A_1), 1210);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00063C65 File Offset: 0x00062C65
		internal virtual dx f(long A_0)
		{
			return new dx(this.a(this.ae, A_0, true), this.f());
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00063C80 File Offset: 0x00062C80
		internal virtual hp e(long A_0)
		{
			return new hp(this.a(this.ae, A_0, false), this.f());
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00063C9B File Offset: 0x00062C9B
		internal virtual fb d(long A_0)
		{
			return this.a(this.c(A_0));
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00063CAC File Offset: 0x00062CAC
		internal virtual fb a(di A_0)
		{
			A_0.a(0L);
			int num = A_0.ReadByte();
			if (num != 2)
			{
				throw new MailBeePstParsingException("Unable to process descriptor node, bad signature: " + num, 1210);
			}
			fb fb = new fb();
			int num2 = (int)A_0.a(2L, 2);
			int num3;
			if (this.f() == 14)
			{
				num3 = 4;
			}
			else
			{
				num3 = 8;
			}
			byte[] a_ = new byte[(int)A_0.Length];
			A_0.a(0L);
			A_0.b(a_);
			for (int i = 0; i < num2; i++)
			{
				h1 h = new h1(a_, num3, this);
				fb.a(h.a, h);
				if (this.f() == 14)
				{
					num3 += 12;
				}
				else
				{
					num3 += 24;
				}
			}
			return fb;
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00063D68 File Offset: 0x00062D68
		internal dl d()
		{
			if (this.z == null)
			{
				long a_;
				if (this.f() == 14)
				{
					a_ = this.g(188L);
				}
				else
				{
					a_ = this.g(224L);
				}
				this.z = new dl();
				this.a(a_);
			}
			return this.z;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00063DC0 File Offset: 0x00062DC0
		private void a(long A_0)
		{
			byte[] array = new byte[2];
			if (this.f() == 14)
			{
				this.ae.Seek(A_0 + 500L, SeekOrigin.Begin);
			}
			else
			{
				this.ae.Seek(A_0 + 496L, SeekOrigin.Begin);
			}
			this.ae.Read(array, 0, array.Length);
			if (array[0] != 129 || array[1] != 129)
			{
				throw new MailBeePstParsingException(Resources.Instance.ErrorDesc_OutlookPstUnableToReadDescriptorNodeIsNotADescriptor, 1210);
			}
			if (this.f() == 14)
			{
				this.ae.Seek(A_0 + 496L, SeekOrigin.Begin);
			}
			else
			{
				this.ae.Seek(A_0 + 488L, SeekOrigin.Begin);
			}
			int num = this.ae.ReadByte();
			this.ae.ReadByte();
			this.ae.ReadByte();
			if (this.ae.ReadByte() > 0)
			{
				for (int i = 0; i < num; i++)
				{
					if (this.f() == 14)
					{
						long num2 = A_0 + (long)(12 * i);
						long a_ = this.g(num2 + 8L);
						this.a(a_);
					}
					else
					{
						long num3 = A_0 + (long)(24 * i);
						long a_2 = this.g(num3 + 16L);
						this.a(a_2);
					}
				}
				return;
			}
			for (int j = 0; j < num; j++)
			{
				if (this.f() == 14)
				{
					this.ae.Seek(A_0 + (long)(j * 16), SeekOrigin.Begin);
					array = new byte[16];
					this.ae.Read(array, 0, array.Length);
				}
				else
				{
					this.ae.Seek(A_0 + (long)(j * 32), SeekOrigin.Begin);
					array = new byte[32];
					this.ae.Read(array, 0, array.Length);
				}
				dx dx = new dx(array, this.f());
				if (dx.d != dx.a)
				{
					if (this.z.a(dx.d))
					{
						this.z.b(dx.d).a(dx);
					}
					else
					{
						h8 h = new h8();
						h.a(dx);
						this.z.a(dx.d, h);
					}
				}
				this.ad++;
			}
		}

		// Token: 0x04001080 RID: 4224
		public const int a = 0;

		// Token: 0x04001081 RID: 4225
		public const int b = 1;

		// Token: 0x04001082 RID: 4226
		private const int c = 33;

		// Token: 0x04001083 RID: 4227
		private const int d = 290;

		// Token: 0x04001084 RID: 4228
		public const int e = 14;

		// Token: 0x04001085 RID: 4229
		protected internal const int f = 15;

		// Token: 0x04001086 RID: 4230
		public const int g = 23;

		// Token: 0x04001087 RID: 4231
		public const int h = 0;

		// Token: 0x04001088 RID: 4232
		public const int i = 1;

		// Token: 0x04001089 RID: 4233
		public const int j = 2;

		// Token: 0x0400108A RID: 4234
		public const int k = 3;

		// Token: 0x0400108B RID: 4235
		public const int l = 4;

		// Token: 0x0400108C RID: 4236
		public const int m = 5;

		// Token: 0x0400108D RID: 4237
		public const int n = 6;

		// Token: 0x0400108E RID: 4238
		public const int o = 7;

		// Token: 0x0400108F RID: 4239
		public const int p = 8;

		// Token: 0x04001090 RID: 4240
		public const int q = 9;

		// Token: 0x04001091 RID: 4241
		public const int r = 10;

		// Token: 0x04001092 RID: 4242
		public const int s = 11;

		// Token: 0x04001093 RID: 4243
		public const int t = 12;

		// Token: 0x04001094 RID: 4244
		public const int u = 13;

		// Token: 0x04001095 RID: 4245
		public const int v = 14;

		// Token: 0x04001096 RID: 4246
		private static readonly string[] w = new string[]
		{
			"00020329-0000-0000-C000-000000000046",
			"00062008-0000-0000-C000-000000000046",
			"00062004-0000-0000-C000-000000000046",
			"00020386-0000-0000-C000-000000000046",
			"00062002-0000-0000-C000-000000000046",
			"6ED8DA90-450B-101B-98DA-00AA003F1305",
			"0006200A-0000-0000-C000-000000000046",
			"41F28F13-83F4-4114-A584-EEDB5A6B0BFF",
			"0006200E-0000-0000-C000-000000000046",
			"00062041-0000-0000-C000-000000000046",
			"00062003-0000-0000-C000-000000000046",
			"4442858E-A9E3-4E80-B900-317A210CC15B",
			"00020328-0000-0000-C000-000000000046",
			"71035549-0739-4DCB-9163-00F0580DBBDF",
			"00062040-0000-0000-C000-000000000046"
		};

		// Token: 0x04001097 RID: 4247
		private q x = new q();

		// Token: 0x04001098 RID: 4248
		private int y;

		// Token: 0x04001099 RID: 4249
		private dl z;

		// Token: 0x0400109A RID: 4250
		private dh aa = new dh();

		// Token: 0x0400109B RID: 4251
		private static ir ab = new ir();

		// Token: 0x0400109C RID: 4252
		private byte[] ac;

		// Token: 0x0400109D RID: 4253
		private int ad;

		// Token: 0x0400109E RID: 4254
		private Stream ae;

		// Token: 0x0400109F RID: 4255
		private int af;

		// Token: 0x040010A0 RID: 4256
		private Dictionary<string, int> ag;

		// Token: 0x040010A1 RID: 4257
		private static dd ah = null;

		// Token: 0x040010A2 RID: 4258
		private static bool ai = true;
	}
}
