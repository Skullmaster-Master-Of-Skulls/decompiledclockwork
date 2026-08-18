using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailBee;
using MailBee.Proxy;

namespace a.e
{
	// Token: 0x02000411 RID: 1041
	internal class a : r
	{
		// Token: 0x0600247C RID: 9340 RVA: 0x0009AD8A File Offset: 0x00099D8A
		public a(string A_0, int A_1, string A_2, string A_3, Encoding A_4) : base(A_0, A_1, A_4)
		{
			this.g = A_2;
			this.h = A_3;
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x0009ADA8 File Offset: 0x00099DA8
		private new byte[] g(string A_0, int A_1)
		{
			byte[] array = new byte[4];
			array[0] = 5;
			array[1] = 1;
			array[2] = 0;
			Match match = new Regex("(?<One>\\d{1,3}).(?<Two>\\d{1,3}).(?<Three>\\d{1,3}).(?<Four>\\d{1,3})", RegexOptions.None).Match(A_0);
			if (match.Success)
			{
				array[3] = 1;
				array = w.b(array, new byte[]
				{
					byte.Parse(match.Groups["One"].Value),
					byte.Parse(match.Groups["Two"].Value),
					byte.Parse(match.Groups["Three"].Value),
					byte.Parse(match.Groups["Four"].Value)
				});
			}
			else
			{
				array[3] = 3;
				byte[] bytes = this.i.GetBytes(A_0);
				array = w.b(array, new byte[]
				{
					(byte)bytes.Length
				});
				array = w.b(array, bytes);
			}
			byte[] bytes2 = BitConverter.GetBytes((short)A_1);
			Array.Reverse(bytes2);
			return w.b(array, bytes2);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0009AEB5 File Offset: 0x00099EB5
		protected override void ab(string A_0, int A_1)
		{
			this.l();
			this.e.i(this.g(A_0, A_1));
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x0009AED4 File Offset: 0x00099ED4
		protected override byte[] ac()
		{
			byte[] array = new byte[Global.TcpBufSize];
			int i = 0;
			int num = 4;
			while (i < num)
			{
				if (i == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, 0, array2, 0, array.Length);
					array = array2;
				}
				int num2 = this.e.d3(array, i);
				if (num2 <= 0)
				{
					break;
				}
				i += num2;
				if (i > num)
				{
					int num3 = 0;
					switch (array[3])
					{
					case 1:
						num3 = 4;
						break;
					case 3:
						num3 = (int)array[4];
						break;
					case 4:
						num3 = 16;
						break;
					}
					num = 6 + num3;
				}
			}
			if (i > num)
			{
				byte[] array3 = new byte[num];
				this.j = new byte[i - num];
				Array.Copy(array, num, this.j, 0, this.j.Length);
				Array.Copy(array, 0, array3, 0, array3.Length);
				return array3;
			}
			this.j = new byte[0];
			return array;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x0009AFB8 File Offset: 0x00099FB8
		protected override bool ad(byte[] A_0)
		{
			return A_0 != null && A_0.Length > 1 && A_0[1] == 0;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0009AFCC File Offset: 0x00099FCC
		private void l()
		{
			this.e.i(this.k());
			byte[] array = new byte[2];
			int i = 0;
			while (i < array.Length)
			{
				i += this.e.d3(array, i);
				if (i == 0)
				{
					throw new MailBeeAbortedByRemoteHostException(55, this.hs());
				}
			}
			switch (array[1])
			{
			case 0:
				return;
			case 1:
			case 2:
				this.i();
				return;
			default:
				throw new MailBeeProxyAuthenticationException(71, this.hs());
			}
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x0009B04A File Offset: 0x0009A04A
		private byte[] k()
		{
			return new byte[]
			{
				5,
				3,
				0,
				1,
				2
			};
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0009B068 File Offset: 0x0009A068
		private new byte[] j()
		{
			byte[] bytes = this.i.GetBytes(this.g);
			byte[] bytes2 = this.i.GetBytes(this.h);
			byte[] array = new byte[3 + bytes.Length + bytes2.Length];
			array[0] = 1;
			array[1] = (byte)bytes.Length;
			Array.Copy(bytes, 0, array, 2, bytes.Length);
			array[bytes.Length + 2] = (byte)bytes2.Length;
			Array.Copy(bytes2, 0, array, array.Length - bytes2.Length, bytes2.Length);
			return array;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x0009B0E0 File Offset: 0x0009A0E0
		private new void i()
		{
			this.e.i(this.j());
			byte[] array = new byte[2];
			int i = 0;
			while (i < array.Length)
			{
				i += this.e.d3(array, i);
				if (i == 0)
				{
					throw new MailBeeAbortedByRemoteHostException(55, this.hs());
				}
			}
			if (array[1] != 0)
			{
				throw new MailBeeProxyAuthenticationException(72, this.hs());
			}
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0009B144 File Offset: 0x0009A144
		protected override Task ae(string A_0, int A_1)
		{
			global::a.e.a.a a;
			a.c = this;
			a.d = A_0;
			a.e = A_1;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder b = a.b;
			b.Start<global::a.e.a.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0009B19C File Offset: 0x0009A19C
		protected override Task<byte[]> af()
		{
			global::a.e.a.c c;
			c.d = this;
			c.b = AsyncTaskMethodBuilder<byte[]>.Create();
			c.a = -1;
			AsyncTaskMethodBuilder<byte[]> b = c.b;
			b.Start<global::a.e.a.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0009B1E4 File Offset: 0x0009A1E4
		private new Task h()
		{
			global::a.e.a.b b;
			b.c = this;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder b2 = b.b;
			b2.Start<global::a.e.a.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0009B22C File Offset: 0x0009A22C
		private new Task g()
		{
			global::a.e.a.d d;
			d.c = this;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder b = d.b;
			b.Start<global::a.e.a.d>(ref d);
			return d.b.Task;
		}
	}
}
