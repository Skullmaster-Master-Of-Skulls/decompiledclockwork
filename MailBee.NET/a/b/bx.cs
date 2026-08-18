using System;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x02000313 RID: 787
	internal class bx
	{
		// Token: 0x06001C15 RID: 7189 RVA: 0x0007B590 File Offset: 0x0007A590
		public bx()
		{
			this.a = new ulong[256];
			for (int i = 0; i < 256; i++)
			{
				ulong num = (ulong)((long)i);
				for (int j = 8; j > 0; j--)
				{
					if ((num & 1UL) == 1UL)
					{
						num = (num >> 1 ^ (ulong)-306674912);
					}
					else
					{
						num >>= 1;
					}
				}
				this.a[i] = num;
			}
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0007B5F4 File Offset: 0x0007A5F4
		public ulong a(ref byte[] A_0)
		{
			ulong num = (ulong)-1;
			ulong num2 = (ulong)((long)A_0.Length);
			for (ulong num3 = 0UL; num3 < num2; num3 += 1UL)
			{
				ulong num4 = num & 255UL;
				num4 ^= (ulong)A_0[(int)(checked((IntPtr)num3))];
				num >>= 8;
				num ^= this.a[(int)(checked((IntPtr)num4))];
			}
			return num ^ (ulong)-1;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x0007B640 File Offset: 0x0007A640
		public ulong b(string A_0)
		{
			byte[] bytes = Encoding.Default.GetBytes(A_0);
			return this.a(ref bytes);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x0007B664 File Offset: 0x0007A664
		public long a(string A_0)
		{
			long result;
			using (FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read))
			{
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				result = (long)this.a(ref array);
			}
			return result;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0007B6BC File Offset: 0x0007A6BC
		public long a(Stream A_0)
		{
			long result;
			try
			{
				byte[] array = new byte[A_0.Length];
				A_0.Read(array, 0, array.Length);
				A_0.Close();
				result = (long)this.a(ref array);
			}
			catch (IOException)
			{
				throw;
			}
			return result;
		}

		// Token: 0x04001351 RID: 4945
		protected ulong[] a;
	}
}
