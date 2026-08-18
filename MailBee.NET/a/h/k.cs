using System;
using System.IO;
using System.Text;
using MailBee;
using MailBee.Tnef;

namespace a.h
{
	// Token: 0x02000203 RID: 515
	internal class k
	{
		// Token: 0x060010C3 RID: 4291 RVA: 0x00046BB0 File Offset: 0x00045BB0
		public static void a(bool A_0)
		{
			k.a = A_0;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00046BB8 File Offset: 0x00045BB8
		public int f()
		{
			return this.c;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00046BC0 File Offset: 0x00045BC0
		public k(Stream A_0)
		{
			this.b = A_0;
			long num = (long)((ulong)this.e());
			if (num != 574529400L)
			{
				throw new MailBeeTnefNotFoundException(string.Format(Resources.Instance.ErrorDesc_TnefSignature0Invalid, Convert.ToString(num, 16).ToUpper()), 1000);
			}
			this.c = (int)this.c();
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00046C1E File Offset: 0x00045C1E
		public k(FileInfo A_0) : this(new n(A_0))
		{
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00046C2C File Offset: 0x00045C2C
		public k(string A_0) : this(new FileInfo(A_0))
		{
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00046C3A File Offset: 0x00045C3A
		public virtual void d()
		{
			if (this.b != null)
			{
				this.b.Close();
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00046C4F File Offset: 0x00045C4F
		internal byte b()
		{
			int num = this.b.ReadByte();
			if (num == -1)
			{
				throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefUnexpectedEndOfStream, 1001);
			}
			return (byte)num;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00046C76 File Offset: 0x00045C76
		internal ushort c()
		{
			return global::a.h.f.a(this.b(), this.b());
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00046C89 File Offset: 0x00045C89
		internal uint e()
		{
			return global::a.h.f.a(this.b(), this.b(), this.b(), this.b());
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00046CA8 File Offset: 0x00045CA8
		internal m a()
		{
			int num = this.b.ReadByte();
			if (num == -1)
			{
				return null;
			}
			if (num != 1 && num != 2)
			{
				throw new MailBeeTnefParsingException(string.Format(Resources.Instance.ErrorDesc_TnefLevelTypeInvalid0, num), 1002);
			}
			int a_ = (int)this.e();
			int num2 = (int)this.e();
			n n2;
			if (this.b is n)
			{
				n n = (n)this.b;
				n2 = new n(n, 0L, (long)num2);
				if (n.a((long)num2) != (long)num2)
				{
					throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefUnexpectedEndOfStream, 1001);
				}
			}
			else
			{
				byte[] array = new byte[num2];
				int num3;
				for (int i = 0; i < num2; i += num3)
				{
					num3 = this.b.Read(array, i, num2 - i);
					if (num3 < 0)
					{
						throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefUnexpectedEndOfStream, 1001);
					}
				}
				n2 = new n(array, 0L, (long)num2);
			}
			int num4 = (int)this.c();
			if (!k.a && num4 != global::a.h.f.a(n2))
			{
				throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefAttributeChecksumInvalid, 1003);
			}
			return new m((byte)num, global::a.h.f.a(a_), global::a.h.f.b(a_), n2);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00046DD6 File Offset: 0x00045DD6
		public override string ToString()
		{
			return new StringBuilder().Append(base.GetType().Name).Append(" (key=").Append(this.f()).Append(")").ToString();
		}

		// Token: 0x04000E4E RID: 3662
		private static bool a;

		// Token: 0x04000E4F RID: 3663
		private Stream b;

		// Token: 0x04000E50 RID: 3664
		private int c;

		// Token: 0x04000E51 RID: 3665
		private const long d = 574529400L;
	}
}
