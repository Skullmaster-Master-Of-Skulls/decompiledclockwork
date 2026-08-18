using System;
using System.Security.Cryptography;
using \u0005;

namespace \u0003
{
	// Token: 0x02000342 RID: 834
	internal class \u0001 : IDisposable
	{
		// Token: 0x06001D46 RID: 7494 RVA: 0x0011F3D8 File Offset: 0x0011D5D8
		internal \u0001(HMAC algorithm, byte[] salt, int iterations)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException(global::\u0005.\u0001.\u0001(172), global::\u0005.\u0001.\u0001(185));
			}
			if (salt == null)
			{
				throw new ArgumentNullException(global::\u0005.\u0001.\u0001(222), global::\u0005.\u0001.\u0001(231));
			}
			try
			{
				this.\u0001 = algorithm;
				this.\u0002 = salt;
				this.\u0003 = iterations;
				this.\u0004 = this.\u0001.HashSize / 8;
				this.\u0006 = new byte[this.\u0004];
			}
			catch
			{
				if (this.\u0001 != null)
				{
					this.\u0001.Clear();
					this.\u0001.Dispose();
				}
				throw;
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0011F498 File Offset: 0x0011D698
		protected virtual void \u0001()
		{
			try
			{
				this.\u0002();
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0011F4C4 File Offset: 0x0011D6C4
		public void \u0002()
		{
			if (this.\u0001 != null)
			{
				this.\u0001.Clear();
				this.\u0001.Dispose();
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0011F4E4 File Offset: 0x0011D6E4
		internal byte[] \u0001(int \u0002)
		{
			byte[] array = new byte[\u0002];
			int i = 0;
			int num = this.\u0008 - this.\u0007;
			if (num > 0)
			{
				if (\u0002 < num)
				{
					Buffer.BlockCopy(this.\u0006, this.\u0007, array, 0, \u0002);
					this.\u0007 += \u0002;
					return array;
				}
				Buffer.BlockCopy(this.\u0006, this.\u0007, array, 0, num);
				this.\u0007 = (this.\u0008 = 0);
				i += num;
			}
			while (i < \u0002)
			{
				byte[] src = this.\u0001();
				int num2 = \u0002 - i;
				if (num2 <= this.\u0004)
				{
					Buffer.BlockCopy(src, 0, array, i, num2);
					i += num2;
					Buffer.BlockCopy(src, num2, this.\u0006, this.\u0007, this.\u0004 - num2);
					this.\u0008 += this.\u0004 - num2;
					return array;
				}
				Buffer.BlockCopy(src, 0, array, i, 20);
				i += this.\u0004;
			}
			return array;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0011F5DC File Offset: 0x0011D7DC
		private byte[] \u0001()
		{
			byte[] array = global::\u0003.\u0001.\u0001(this.\u0005);
			this.\u0001.TransformBlock(this.\u0002, 0, this.\u0002.Length, this.\u0002, 0);
			this.\u0001.TransformFinalBlock(array, 0, array.Length);
			byte[] array2 = this.\u0001.Hash;
			this.\u0001.Initialize();
			byte[] array3 = array2;
			for (int i = 2; i <= this.\u0003; i++)
			{
				array2 = this.\u0001.ComputeHash(array2);
				for (int j = 0; j < this.\u0004; j++)
				{
					array3[j] ^= array2[j];
				}
			}
			this.\u0005 += 1U;
			return array3;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0011F694 File Offset: 0x0011D894
		private static byte[] \u0001(uint \u0002)
		{
			byte[] bytes = BitConverter.GetBytes(\u0002);
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					bytes[3],
					bytes[2],
					bytes[1],
					bytes[0]
				};
			}
			return bytes;
		}

		// Token: 0x04001F8F RID: 8079
		private HMAC \u0001;

		// Token: 0x04001F90 RID: 8080
		private byte[] \u0002;

		// Token: 0x04001F91 RID: 8081
		private int \u0003;

		// Token: 0x04001F92 RID: 8082
		private readonly int \u0004;

		// Token: 0x04001F93 RID: 8083
		private uint \u0005 = 1U;

		// Token: 0x04001F94 RID: 8084
		private byte[] \u0006;

		// Token: 0x04001F95 RID: 8085
		private int \u0007;

		// Token: 0x04001F96 RID: 8086
		private int \u0008;
	}
}
