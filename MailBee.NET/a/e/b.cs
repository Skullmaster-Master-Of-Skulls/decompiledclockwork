using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailBee;

namespace a.e
{
	// Token: 0x0200040F RID: 1039
	internal class b : r
	{
		// Token: 0x06002472 RID: 9330 RVA: 0x0009A972 File Offset: 0x00099972
		public b(string A_0, int A_1, string A_2, Encoding A_3) : base(A_0, A_1, A_3)
		{
			this.g = A_2;
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x0009A988 File Offset: 0x00099988
		private new byte[] g(string A_0, int A_1)
		{
			byte[] array = new byte[8];
			array[0] = 4;
			array[1] = 1;
			byte[] bytes = BitConverter.GetBytes(A_1);
			array[2] = bytes[1];
			array[3] = bytes[0];
			byte[] bytes2 = this.i.GetBytes(this.g);
			byte[] array2 = new byte[bytes2.Length + 1];
			bytes2.CopyTo(array2, 0);
			array = w.b(array, array2);
			Match match = new Regex("(?<One>\\d{1,3}).(?<Two>\\d{1,3}).(?<Three>\\d{1,3}).(?<Four>\\d{1,3})", RegexOptions.None).Match(A_0);
			if (match.Success)
			{
				array[4] = byte.Parse(match.Groups["One"].Value);
				array[5] = byte.Parse(match.Groups["Two"].Value);
				array[6] = byte.Parse(match.Groups["Three"].Value);
				array[7] = byte.Parse(match.Groups["Four"].Value);
			}
			else
			{
				array[4] = 0;
				array[5] = 0;
				array[6] = 0;
				array[7] = 1;
				byte[] bytes3 = this.i.GetBytes(A_0);
				byte[] array3 = new byte[bytes3.Length + 1];
				bytes3.CopyTo(array3, 0);
				array = w.b(array, array3);
			}
			return array;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x0009AAAC File Offset: 0x00099AAC
		protected override void ab(string A_0, int A_1)
		{
			this.e.i(this.g(A_0, A_1));
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x0009AAC4 File Offset: 0x00099AC4
		private new byte[] g(byte[] A_0, int A_1)
		{
			if (A_1 > 8)
			{
				byte[] array = new byte[8];
				this.j = new byte[A_1 - 8];
				Array.Copy(A_0, 8, this.j, 0, this.j.Length);
				Array.Copy(A_0, 0, array, 0, array.Length);
				return array;
			}
			this.j = new byte[0];
			return null;
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x0009AB1C File Offset: 0x00099B1C
		protected override byte[] ac()
		{
			byte[] array = new byte[Global.TcpBufSize];
			int i;
			int num;
			for (i = 0; i < 8; i += num)
			{
				if (i == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, 0, array2, 0, array.Length);
					array = array2;
				}
				num = this.e.d3(array, i);
				if (num <= 0)
				{
					break;
				}
			}
			byte[] array3 = this.g(array, i);
			if (array3 != null)
			{
				return array3;
			}
			return array;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x0009AB83 File Offset: 0x00099B83
		protected override bool ad(byte[] A_0)
		{
			return A_0 != null && A_0.Length > 1 && A_0[1] == 90;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x0009AB98 File Offset: 0x00099B98
		protected override Task ae(string A_0, int A_1)
		{
			return this.e.k(this.g(A_0, A_1));
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x0009ABB0 File Offset: 0x00099BB0
		protected override Task<byte[]> af()
		{
			b.a a;
			a.d = this;
			a.b = AsyncTaskMethodBuilder<byte[]>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<byte[]> b = a.b;
			b.Start<b.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x04001828 RID: 6184
		public new const int e = 8;
	}
}
