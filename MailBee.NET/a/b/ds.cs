using System;

namespace a.b
{
	// Token: 0x0200029D RID: 669
	internal class ds
	{
		// Token: 0x06001786 RID: 6022 RVA: 0x0006B2DE File Offset: 0x0006A2DE
		public ds()
		{
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0006B2E6 File Offset: 0x0006A2E6
		public ds(byte[] A_0, int A_1)
		{
			this.c(A_0, A_1);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0006B2F7 File Offset: 0x0006A2F7
		public ds(int A_0, object A_1)
		{
			this.b = A_0;
			this.c = A_1;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0006B30D File Offset: 0x0006A30D
		public object a()
		{
			return this.c;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0006B318 File Offset: 0x0006A318
		public int c(byte[] A_0, int A_1)
		{
			this.b = (int)p.k(A_0, A_1);
			int num = A_1 + 2;
			short num2 = p.k(A_0, num);
			num += 2;
			if (num2 != 0)
			{
				ds.a.iv(5, string.Concat(new object[]
				{
					"TypedPropertyValue padding at offset ",
					num,
					" MUST be 0, but it's value is ",
					num2
				}));
			}
			num += this.b(A_0, num);
			return num - A_1;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0006B390 File Offset: 0x0006A390
		public int b(byte[] A_0, int A_1)
		{
			int num = this.b;
			if (num <= 4127)
			{
				switch (num)
				{
				case 0:
				case 1:
					this.c = null;
					return 0;
				case 2:
					this.c = p.k(A_0, A_1);
					return 4;
				case 3:
					this.c = p.i(A_0, A_1);
					return 4;
				case 4:
					this.c = p.k(A_0, A_1);
					return 4;
				case 5:
					this.c = p.f(A_0, A_1);
					return 8;
				case 6:
					this.c = new h(A_0, A_1);
					return 8;
				case 7:
					this.c = new hk(A_0, A_1);
					return 8;
				case 8:
					this.c = new iz(A_0, A_1);
					return ((iz)this.c).a();
				case 9:
				case 12:
				case 13:
				case 15:
				case 24:
				case 25:
				case 26:
				case 27:
				case 28:
				case 29:
					goto IL_483;
				case 10:
					this.c = p.h(A_0, A_1);
					return 4;
				case 11:
					this.c = new a2(A_0, A_1);
					return 2;
				case 14:
					this.c = new et(A_0, A_1);
					return 16;
				case 16:
					this.c = A_0[A_1];
					return 1;
				case 17:
					this.c = p.b(A_0, A_1);
					return 2;
				case 18:
					this.c = p.j(A_0, A_1);
					return 4;
				case 19:
					this.c = p.h(A_0, A_1);
					return 4;
				case 20:
					this.c = p.g(A_0, A_1);
					return 8;
				case 21:
					this.c = p.b(A_0, A_1, 8);
					return 8;
				case 22:
					this.c = p.i(A_0, A_1);
					return 4;
				case 23:
					this.c = p.h(A_0, A_1);
					return 4;
				case 30:
					this.c = new iz(A_0, A_1);
					return ((iz)this.c).a();
				case 31:
					this.c = new ft(A_0, A_1);
					return ((ft)this.c).a();
				default:
					switch (num)
					{
					case 64:
						this.c = new gv(A_0, A_1);
						return 8;
					case 65:
						this.c = new t(A_0, A_1);
						return ((t)this.c).a();
					case 66:
					case 67:
					case 68:
					case 69:
						this.c = new @if(A_0, A_1);
						return ((@if)this.c).a();
					case 70:
						this.c = new t(A_0, A_1);
						return ((t)this.c).a();
					case 71:
						this.c = new c(A_0, A_1);
						return ((c)this.c).a();
					case 72:
						this.c = new ai(A_0, A_1);
						return 16;
					case 73:
						this.c = new fq(A_0, A_1);
						return ((fq)this.c).a();
					default:
						switch (num)
						{
						case 4098:
						case 4099:
						case 4100:
						case 4101:
						case 4102:
						case 4103:
						case 4104:
						case 4106:
						case 4107:
						case 4108:
						case 4112:
						case 4113:
						case 4114:
						case 4115:
						case 4116:
						case 4117:
						case 4126:
						case 4127:
							break;
						case 4105:
						case 4109:
						case 4110:
						case 4111:
						case 4118:
						case 4119:
						case 4120:
						case 4121:
						case 4122:
						case 4123:
						case 4124:
						case 4125:
							goto IL_483;
						default:
							goto IL_483;
						}
						break;
					}
					break;
				}
			}
			else if (num <= 4167)
			{
				if (num != 4160 && num != 4167)
				{
					goto IL_483;
				}
			}
			else if (num != 4168)
			{
				switch (num)
				{
				case 8194:
				case 8195:
				case 8196:
				case 8197:
				case 8198:
				case 8199:
				case 8200:
				case 8202:
				case 8203:
				case 8204:
				case 8206:
				case 8208:
				case 8209:
				case 8210:
				case 8211:
				case 8214:
				case 8215:
					this.c = new bt();
					return ((bt)this.c).a(A_0, A_1);
				case 8201:
				case 8205:
				case 8207:
				case 8212:
				case 8213:
					goto IL_483;
				default:
					goto IL_483;
				}
			}
			this.c = new ev((short)(this.b & 4095));
			return ((ev)this.c).a(A_0, A_1);
			IL_483:
			throw new InvalidOperationException("Unknown (possibly, incorrect) TypedPropertyValue type: " + this.b);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0006B83C File Offset: 0x0006A83C
		internal int a(byte[] A_0, int A_1)
		{
			int num = this.b(A_0, A_1);
			if ((num & 3) != 0)
			{
				return num + (4 - (num & 3));
			}
			return num;
		}

		// Token: 0x0400116E RID: 4462
		private static dm a = gn.a(typeof(ds));

		// Token: 0x0400116F RID: 4463
		private int b;

		// Token: 0x04001170 RID: 4464
		private object c;
	}
}
