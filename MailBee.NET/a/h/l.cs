using System;
using System.Text;
using MailBee;
using MailBee.Tnef;

namespace a.h
{
	// Token: 0x020001FC RID: 508
	internal class l
	{
		// Token: 0x06001061 RID: 4193 RVA: 0x0004557B File Offset: 0x0004457B
		public Guid b()
		{
			return this.c;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00045583 File Offset: 0x00044583
		public int d()
		{
			return this.d;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004558B File Offset: 0x0004458B
		public string c()
		{
			return this.f;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00045593 File Offset: 0x00044593
		private int a()
		{
			return this.g;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0004559C File Offset: 0x0004459C
		public l(n A_0)
		{
			long position = A_0.Position;
			byte[] array = new byte[]
			{
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte(),
				(byte)A_0.ReadByte()
			};
			this.c = new Guid(BitConverter.ToInt32(new byte[]
			{
				array[3],
				array[2],
				array[1],
				array[0]
			}, 0), BitConverter.ToInt16(new byte[]
			{
				array[5],
				array[4]
			}, 0), BitConverter.ToInt16(new byte[]
			{
				array[7],
				array[6]
			}, 0), array[8], array[9], array[10], array[11], array[12], array[13], array[14], array[15]);
			this.d = (int)A_0.e();
			if (this.d == 1)
			{
				int num = (int)A_0.e();
				this.f = A_0.c(num);
				if (num % 4 != 0)
				{
					A_0.a((long)(4 - num % 4));
				}
				this.g += num;
			}
			else
			{
				if (this.d != 0)
				{
					throw new MailBeeTnefParsingException(string.Format(Resources.Instance.ErrorDesc_TnefMapiPropTypeInvalid0, this.d), 1011);
				}
				this.e = (long)((ulong)A_0.e());
			}
			this.g = (int)(A_0.Position - position);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00045779 File Offset: 0x00044779
		public l(Guid A_0, long A_1)
		{
			this.c = A_0;
			this.d = 0;
			this.e = A_1;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00045796 File Offset: 0x00044796
		public l(Guid A_0, string A_1)
		{
			this.c = A_0;
			this.d = 1;
			this.f = A_1;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000457B3 File Offset: 0x000447B3
		public long e()
		{
			return this.e;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000457BC File Offset: 0x000447BC
		public override string ToString()
		{
			return new StringBuilder().Append("Guid=").Append(this.c).Append(" Name=").Append((this.d == 1) ? this.f : ("0x" + Convert.ToString(this.e, 16))).ToString();
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00045824 File Offset: 0x00044824
		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (!(o is l))
			{
				return false;
			}
			l l = (l)o;
			return this.d == l.d && ((this.d == 0) ? (this.e == l.e) : this.f.Equals(l.f)) && this.c.Equals(l.c);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00045894 File Offset: 0x00044894
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 37 + this.d;
			num = num * 37 + this.c.GetHashCode();
			if (this.d == 0)
			{
				num = num * 37 + (int)(this.e ^ global::a.h.f.a(this.e, 32));
			}
			if (this.d == 1 && this.f != null)
			{
				num = num * 37 + this.f.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000E2E RID: 3630
		public const int a = 0;

		// Token: 0x04000E2F RID: 3631
		public const int b = 1;

		// Token: 0x04000E30 RID: 3632
		private Guid c;

		// Token: 0x04000E31 RID: 3633
		private int d;

		// Token: 0x04000E32 RID: 3634
		private long e;

		// Token: 0x04000E33 RID: 3635
		private string f;

		// Token: 0x04000E34 RID: 3636
		private int g;
	}
}
