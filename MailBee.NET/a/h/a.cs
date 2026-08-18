using System;
using System.IO;
using MailBee;
using MailBee.Tnef;

namespace a.h
{
	// Token: 0x020001FE RID: 510
	internal class a
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x00045C39 File Offset: 0x00044C39
		public int e()
		{
			return this.a;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00045C41 File Offset: 0x00044C41
		public int b()
		{
			return (int)this.b.Length;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00045C50 File Offset: 0x00044C50
		public byte[] d()
		{
			byte[] result = null;
			try
			{
				result = this.b.d();
			}
			catch (IOException)
			{
			}
			return result;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00045C84 File Offset: 0x00044C84
		public n a()
		{
			return new n(this.b);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00045C94 File Offset: 0x00044C94
		public object f()
		{
			object obj = null;
			n n = new n(this.b);
			try
			{
				int num = this.a;
				if (num <= 30)
				{
					switch (num)
					{
					case 0:
						goto IL_16D;
					case 1:
						goto IL_1C7;
					case 2:
						return (short)n.f();
					case 3:
						return (int)n.e();
					case 4:
					case 5:
					case 8:
					case 9:
					case 12:
						goto IL_1A2;
					case 6:
						break;
					case 7:
						goto IL_E4;
					case 10:
						return (int)n.e();
					case 11:
						return n.e() > 0U;
					case 13:
					{
						Guid guid = new Guid(n.b(16));
						obj = n;
						n = null;
						if (guid.Equals(g.z))
						{
							return new k((n)obj);
						}
						goto IL_1C7;
					}
					default:
						if (num != 20)
						{
							if (num != 30)
							{
								goto IL_1A2;
							}
							return n.a((int)n.Length);
						}
						break;
					}
					return new long[]
					{
						(long)((ulong)n.e()),
						(long)((ulong)n.e())
					};
				}
				if (num <= 64)
				{
					if (num == 31)
					{
						return n.c((int)n.Length);
					}
					if (num != 64)
					{
						goto IL_1A2;
					}
				}
				else
				{
					if (num == 72)
					{
						return n.d();
					}
					if (num != 258)
					{
						goto IL_1A2;
					}
					goto IL_16D;
				}
				IL_E4:
				if (n.Length == 8L)
				{
					return new DateTime((long)(n.c() + (ulong)new DateTime(1601, 1, 1, 0, 0, 0, 0).Ticks));
				}
				goto IL_1C7;
				IL_16D:
				obj = n;
				n = null;
				return obj;
				IL_1A2:
				throw new MailBeeTnefParsingException(string.Format(Resources.Instance.ErrorDesc_TnefMapiTypeUnknown0, this.a), 1005);
				IL_1C7:;
			}
			finally
			{
				if (n != null)
				{
					n.Close();
				}
			}
			return obj;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00045E94 File Offset: 0x00044E94
		public a(int A_0, n A_1, int A_2)
		{
			if ((A_0 & 4096) != 0)
			{
				throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefMapiMultivalueIsNotAllowedInSingleMapiValue, 1012);
			}
			this.a = A_0;
			this.b = new n(A_1, 0L, (long)A_2);
			A_1.a((long)A_2);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00045EE5 File Offset: 0x00044EE5
		public void c()
		{
			this.b.Close();
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00045EF4 File Offset: 0x00044EF4
		public override string ToString()
		{
			string result;
			try
			{
				object obj = this.f();
				if (obj is n)
				{
					n n = (n)obj;
					obj = n.ToString();
					n.Close();
				}
				else if (obj is byte[])
				{
					obj = global::a.h.f.a((byte[])obj, 512);
				}
				else if (obj is k)
				{
					k k = (k)obj;
					obj = k.ToString();
					k.d();
				}
				result = obj.ToString();
			}
			catch (IOException ex)
			{
				result = "An exception occurs: " + ex.ToString();
			}
			return result;
		}

		// Token: 0x04000E37 RID: 3639
		private int a;

		// Token: 0x04000E38 RID: 3640
		private n b;
	}
}
