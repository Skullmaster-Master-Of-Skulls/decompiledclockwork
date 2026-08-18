using System;
using \u0002;
using \u0005;

namespace OracleInternal.Secure.Network
{
	// Token: 0x0200034D RID: 845
	public abstract class DataIntegrityAlgorithm
	{
		// Token: 0x06001DD3 RID: 7635 RVA: 0x001242D8 File Offset: 0x001224D8
		public void init(byte[] k, byte[] iv, string a)
		{
			this.\u0001(k, iv, a);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x001242E4 File Offset: 0x001224E4
		private void \u0001(byte[] \u0002, byte[] \u0003, string \u0004)
		{
			this.\u0008.\u0001();
			this.\u0007 = \u0004;
			if (this.\u0007 == global::\u0005.\u0001.\u0001(553) || this.\u0007 == global::\u0005.\u0001.\u0001(562) || this.\u0007 == global::\u0005.\u0001.\u0001(571))
			{
				this.\u0004 = true;
			}
			if (this.\u0004)
			{
				this.\u0002 = new AES(1, 1, DataIntegrityAlgorithm.\u0001, \u0002, \u0003, true);
			}
			else
			{
				this.\u0003 = new RC4(DataIntegrityAlgorithm.\u0001 * 8, \u0002, \u0003, true);
			}
			this.\u0006 = new byte[this.size()];
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00124394 File Offset: 0x00122594
		public bool compare(byte[] b, int l, byte[] xs, int off)
		{
			return this.\u0001(b, l, xs, off);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x001243A4 File Offset: 0x001225A4
		private bool \u0001(byte[] \u0002, int \u0003, byte[] \u0004, int \u0005)
		{
			byte[] array = new byte[this.size()];
			if (this.\u0004)
			{
				if (this.\u0005 != null)
				{
					Buffer.BlockCopy(this.\u0005, 0, array, 0, this.\u0005.Length);
					this.\u0005 = null;
				}
				this.\u0005 = this.\u0002.\u0001(array, 1);
			}
			else
			{
				this.\u0005 = this.\u0003.decrypt(array);
			}
			if (this.\u0004)
			{
				this.\u0008.\u0001(\u0002, 0, \u0003, this.\u0005, this.\u0005.Length, ref array);
			}
			else
			{
				this.\u0008.\u0001();
				this.\u0008.\u0001(\u0002, 0, \u0003);
				this.\u0008.\u0001(this.\u0005, 0, this.\u0005.Length);
				this.\u0008.\u0001(array, 0);
			}
			bool result = false;
			for (int i = 0; i < this.size(); i++)
			{
				if (array[i] != \u0004[\u0005 + i])
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x001244A0 File Offset: 0x001226A0
		public byte[] compute(byte[] s, int off, int l)
		{
			return this.\u0001(s, off, l);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x001244AC File Offset: 0x001226AC
		private byte[] \u0001(byte[] \u0002, int \u0003, int \u0004)
		{
			if (\u0002.Length - \u0003 < \u0004)
			{
				return null;
			}
			byte[] result = new byte[this.size()];
			byte[] array;
			if (this.\u0004)
			{
				array = this.\u0002.\u0001(this.\u0006, 2);
				Buffer.BlockCopy(array, 0, this.\u0006, 0, array.Length);
				this.\u0008.\u0001(\u0002, \u0003, \u0004, array, array.Length, ref result);
				return result;
			}
			array = this.\u0003.encrypt(this.\u0006);
			this.\u0008.\u0001();
			this.\u0008.\u0001(\u0002, \u0003, \u0004);
			this.\u0008.\u0001(array, 0, array.Length);
			this.\u0008.\u0001(array, 0);
			return array;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00124560 File Offset: 0x00122760
		public int takeSessionKey(byte[] key, byte[] iv)
		{
			try
			{
				if (this.\u0004)
				{
					this.\u0002.setSessionKey(key, iv);
					this.\u0006 = new byte[this.size()];
					this.\u0005 = null;
				}
				else
				{
					this.\u0003.setSessionKey(key, iv);
				}
			}
			catch (Exception)
			{
			}
			return 0;
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x001245C0 File Offset: 0x001227C0
		public void renew()
		{
			if (this.\u0004)
			{
				this.\u0002.renewKey();
				return;
			}
			this.\u0003.\u0001();
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x001245E4 File Offset: 0x001227E4
		public int size()
		{
			return this.\u0008.\u0001();
		}

		// Token: 0x04002031 RID: 8241
		private static int \u0001 = 5;

		// Token: 0x04002032 RID: 8242
		private AES \u0002;

		// Token: 0x04002033 RID: 8243
		private RC4 \u0003;

		// Token: 0x04002034 RID: 8244
		private bool \u0004;

		// Token: 0x04002035 RID: 8245
		private byte[] \u0005;

		// Token: 0x04002036 RID: 8246
		private byte[] \u0006;

		// Token: 0x04002037 RID: 8247
		private string \u0007;

		// Token: 0x04002038 RID: 8248
		internal global::\u0002.\u0002 \u0008;
	}
}
