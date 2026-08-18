using System;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002B0 RID: 688
	internal class c6
	{
		// Token: 0x06001815 RID: 6165 RVA: 0x0006DFB8 File Offset: 0x0006CFB8
		public c6(POIFSFileSystem A_0) : this(A_0.Root)
		{
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0006DFC6 File Offset: 0x0006CFC6
		public c6(h0 A_0) : this(A_0.m())
		{
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0006DFD4 File Offset: 0x0006CFD4
		public c6(DirectoryNode A_0)
		{
			az az = A_0.a("EncryptionInfo");
			this.a = (int)az.az();
			this.b = (int)az.az();
			this.c = az.a0();
			if (this.a == 4 && this.b == 4 && this.c == 64)
			{
				StringBuilder stringBuilder = new StringBuilder();
				byte[] array = new byte[az.aq()];
				az.a(array);
				foreach (byte value in array)
				{
					stringBuilder.Append((char)value);
				}
				string a_ = stringBuilder.ToString();
				this.d = new hr(a_);
				this.e = new iq(a_);
				return;
			}
			az.a0();
			this.d = new hr(az);
			if (this.d.e() == 26625)
			{
				this.e = new iq(az, 20);
				return;
			}
			this.e = new iq(az, 32);
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0006E0D5 File Offset: 0x0006D0D5
		public int c()
		{
			return this.a;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0006E0DD File Offset: 0x0006D0DD
		public int a()
		{
			return this.b;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0006E0E5 File Offset: 0x0006D0E5
		public int d()
		{
			return this.c;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0006E0ED File Offset: 0x0006D0ED
		public hr b()
		{
			return this.d;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0006E0F5 File Offset: 0x0006D0F5
		public iq e()
		{
			return this.e;
		}

		// Token: 0x04001214 RID: 4628
		private int a;

		// Token: 0x04001215 RID: 4629
		private int b;

		// Token: 0x04001216 RID: 4630
		private int c;

		// Token: 0x04001217 RID: 4631
		private hr d;

		// Token: 0x04001218 RID: 4632
		private iq e;
	}
}
