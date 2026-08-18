using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MailBee;

namespace a.e
{
	// Token: 0x0200040C RID: 1036
	internal class c : r
	{
		// Token: 0x06002455 RID: 9301 RVA: 0x0009A20A File Offset: 0x0009920A
		public c(string A_0, int A_1, string A_2, string A_3, Encoding A_4) : base(A_0, A_1, A_4)
		{
			this.g = A_2;
			this.h = A_3;
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0009A228 File Offset: 0x00099228
		private new byte[] g(string A_0, int A_1)
		{
			string str = string.Format("CONNECT {0}:{1} HTTP/1.0\r\n", A_0, A_1);
			string arg = Convert.ToBase64String(this.i.GetBytes(string.Format("{0}:{1}", this.g, this.h)));
			string str2 = string.Format("Proxy-authorization: Basic {0}\r\n", arg);
			string s = str + str2 + "\r\n";
			return this.i.GetBytes(s);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0009A291 File Offset: 0x00099291
		protected override void ab(string A_0, int A_1)
		{
			this.e.i(this.g(A_0, A_1));
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x0009A2A8 File Offset: 0x000992A8
		private new byte[] g(byte[] A_0, int A_1)
		{
			string @string = this.i.GetString(A_0, 0, A_1);
			byte[] bytes = Encoding.ASCII.GetBytes("\r\n\r\n");
			int num = w.b(A_0, 0, A_1, bytes);
			if (num > 0)
			{
				byte[] array = new byte[num];
				Buffer.BlockCopy(A_0, 0, array, 0, num);
				if (num + bytes.Length < A_1)
				{
					string s = @string.Substring(num + bytes.Length);
					this.j = this.i.GetBytes(s);
					this.j = new byte[A_1 - (num + bytes.Length)];
					Buffer.BlockCopy(A_0, num + bytes.Length, this.j, 0, A_1 - (num + bytes.Length));
				}
				else
				{
					this.j = new byte[0];
				}
				return array;
			}
			return null;
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0009A358 File Offset: 0x00099358
		protected override byte[] ac()
		{
			byte[] array = new byte[Global.TcpBufSize];
			int num = 0;
			byte[] array3;
			do
			{
				if (num == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, 0, array2, 0, array.Length);
					array = array2;
				}
				int num2 = this.e.d3(array, num);
				if (num2 <= 0)
				{
					goto IL_53;
				}
				num += num2;
				array3 = this.g(array, num);
			}
			while (array3 == null);
			return array3;
			IL_53:
			return new byte[0];
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x0009A3C0 File Offset: 0x000993C0
		protected override bool ad(byte[] A_0)
		{
			if (A_0 != null)
			{
				string[] array = this.i.GetString(A_0, 0, A_0.Length).Split(new char[]
				{
					'\n'
				});
				if (array != null && array.Length != 0)
				{
					string[] array2 = array[0].Split(new char[]
					{
						' '
					});
					if (array2 != null && array2.Length > 1 && array2[1] == "200")
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x0009A427 File Offset: 0x00099427
		protected override Task ae(string A_0, int A_1)
		{
			return this.e.k(this.g(A_0, A_1));
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x0009A43C File Offset: 0x0009943C
		protected override Task<byte[]> af()
		{
			c.a a;
			a.d = this;
			a.b = AsyncTaskMethodBuilder<byte[]>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<byte[]> b = a.b;
			b.Start<c.a>(ref a);
			return a.b.Task;
		}
	}
}
