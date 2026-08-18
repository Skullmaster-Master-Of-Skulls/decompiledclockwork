using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200039A RID: 922
	internal sealed class ax : ck
	{
		// Token: 0x06002122 RID: 8482 RVA: 0x00088378 File Offset: 0x00087378
		public ax()
		{
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000883D0 File Offset: 0x000873D0
		public ax(params f6[] A_0) : base(A_0)
		{
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00088428 File Offset: 0x00087428
		protected override void bw(da A_0)
		{
			base.d();
			try
			{
				this.d(A_0.d8());
				base.e();
			}
			catch (RtfException a_)
			{
				base.a(a_);
				throw;
			}
			finally
			{
				base.c();
			}
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0008847C File Offset: 0x0008747C
		private void d(TextReader A_0)
		{
			this.a = new StringBuilder();
			this.b.Clear();
			this.d.Clear();
			this.c = 1;
			this.e = 0;
			this.f = 0;
			this.g = 0;
			this.h = -1;
			this.i = null;
			this.j = false;
			this.k.Clear();
			this.n.SetLength(0L);
			this.d(1252);
			int num = 0;
			int num2 = ax.a(A_0, false);
			bool flag = false;
			while (num2 != -1)
			{
				int num3 = 0;
				bool flag2 = false;
				if (num2 <= 92)
				{
					switch (num2)
					{
					case 9:
						A_0.Read();
						this.a(A_0, new er("tab"));
						break;
					case 10:
					case 13:
						A_0.Read();
						break;
					case 11:
					case 12:
						goto IL_39E;
					default:
					{
						if (num2 != 92)
						{
							goto IL_39E;
						}
						if (!flag)
						{
							A_0.Read();
						}
						int num4 = ax.a(A_0, true);
						if (num4 <= 42)
						{
							if (num4 <= 13)
							{
								if (num4 != 10 && num4 != 13)
								{
									goto IL_270;
								}
								A_0.Read();
								this.a(A_0, new er("par"));
								break;
							}
							else if (num4 != 39)
							{
								if (num4 != 42)
								{
									goto IL_270;
								}
							}
							else
							{
								A_0.Read();
								char a_ = (char)ax.b(A_0);
								char a_2 = (char)ax.b(A_0);
								if (!ax.b((int)a_))
								{
									throw new RtfHexEncodingException(bv.b(a_));
								}
								if (!ax.b((int)a_2))
								{
									throw new RtfHexEncodingException(bv.a(a_2));
								}
								int num5 = int.Parse(a_.ToString() + a_2.ToString(), NumberStyles.HexNumber);
								this.n.WriteByte((byte)num5);
								num3 = ax.a(A_0, false);
								flag2 = true;
								bool flag3 = true;
								if (num3 == 92)
								{
									A_0.Read();
									flag = true;
									if (ax.a(A_0, false) == 39)
									{
										flag3 = false;
									}
								}
								if (flag3)
								{
									this.b();
									break;
								}
								break;
							}
						}
						else
						{
							if (num4 > 58)
							{
								if (num4 != 92)
								{
									if (num4 == 95)
									{
										goto IL_245;
									}
									switch (num4)
									{
									case 123:
									case 125:
										break;
									case 124:
									case 126:
										goto IL_245;
									default:
										goto IL_270;
									}
								}
								this.a.Append(this.a(A_0));
								break;
							}
							if (num4 != 45 && num4 != 58)
							{
								goto IL_270;
							}
						}
						IL_245:
						this.a(A_0, new er(this.a(A_0).ToString() ?? ""));
						break;
						IL_270:
						this.c(A_0);
						break;
					}
					}
				}
				else if (num2 != 123)
				{
					if (num2 != 125)
					{
						goto IL_39E;
					}
					A_0.Read();
					this.a();
					if (this.e <= 0)
					{
						throw new RtfBraceNestingException(bv.s());
					}
					this.c = (int)this.b.Pop();
					if (this.h == this.e)
					{
						this.h = -1;
						this.i = null;
						this.j = false;
					}
					this.d((int)this.d.Pop());
					this.e--;
					base.g();
					num++;
				}
				else
				{
					A_0.Read();
					this.a();
					base.f();
					this.f = this.g;
					this.b.Push(this.c);
					this.d.Push((this.l == null) ? 0 : this.l.CodePage);
					this.e++;
				}
				IL_3B1:
				if (this.e == 0 && base.gm())
				{
					break;
				}
				if (flag2)
				{
					num2 = num3;
					continue;
				}
				num2 = ax.a(A_0, false);
				flag = false;
				continue;
				IL_39E:
				this.a.Append(this.a(A_0));
				goto IL_3B1;
			}
			this.a();
			A_0.Close();
			if (this.e > 0)
			{
				throw new RtfBraceNestingException(bv.r());
			}
			if (num == 0)
			{
				throw new RtfEmptyDocumentException(bv.q());
			}
			this.a = null;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00088898 File Offset: 0x00087898
		private void c(TextReader A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = null;
			bool flag = true;
			bool flag2 = false;
			int num = ax.a(A_0, true);
			while (!flag2)
			{
				if (flag && ax.c(num))
				{
					stringBuilder.Append(this.a(A_0));
				}
				else if (ax.a(num) || (num == 45 && stringBuilder2 == null))
				{
					flag = false;
					if (stringBuilder2 == null)
					{
						stringBuilder2 = new StringBuilder();
					}
					stringBuilder2.Append(this.a(A_0));
				}
				else
				{
					flag2 = true;
					c9 a_;
					if (stringBuilder2 != null && stringBuilder2.Length > 0)
					{
						a_ = new er(stringBuilder.ToString(), stringBuilder2.ToString());
					}
					else
					{
						a_ = new er(stringBuilder.ToString());
					}
					bool flag3 = this.a(A_0, a_);
					if (num == 32 && !flag3)
					{
						A_0.Read();
					}
				}
				if (!flag2)
				{
					num = ax.a(A_0, true);
				}
			}
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x00088968 File Offset: 0x00087968
		private bool a(TextReader A_0, c9 A_1)
		{
			if (this.e == 0)
			{
				throw new RtfStructureException(bv.c(A_1.ToString()));
			}
			if (this.g < 4)
			{
				this.a(A_1);
			}
			string text = A_1.jz();
			bool flag = this.j;
			if (this.f == this.g)
			{
				uint num = global::b.a(text);
				if (num <= 875660080U)
				{
					if (num <= 644779004U)
					{
						if (num != 596946891U)
						{
							if (num != 644779004U)
							{
								goto IL_13B;
							}
							if (!(text == "fdbmajor"))
							{
								goto IL_13B;
							}
						}
						else if (!(text == "fhimajor"))
						{
							goto IL_13B;
						}
					}
					else if (num != 747407905U)
					{
						if (num != 875660080U)
						{
							goto IL_13B;
						}
						if (!(text == "fdbminor"))
						{
							goto IL_13B;
						}
					}
					else if (!(text == "flominor"))
					{
						goto IL_13B;
					}
				}
				else if (num <= 2134103081U)
				{
					if (num != 1835979141U)
					{
						if (num != 2134103081U)
						{
							goto IL_13B;
						}
						if (!(text == "fbiminor"))
						{
							goto IL_13B;
						}
					}
					else if (!(text == "flomajor"))
					{
						goto IL_13B;
					}
				}
				else if (num != 2466964733U)
				{
					if (num != 3672565719U)
					{
						goto IL_13B;
					}
					if (!(text == "fhiminor"))
					{
						goto IL_13B;
					}
				}
				else if (!(text == "fbimajor"))
				{
					goto IL_13B;
				}
				this.j = true;
				IL_13B:
				flag = true;
			}
			if (flag)
			{
				if (!(text == "f"))
				{
					if (text == "fonttbl")
					{
						this.h = this.e;
					}
				}
				else if (this.h > 0)
				{
					this.i = A_1.jy();
					this.j = false;
				}
			}
			if (this.i != null && "fcharset".Equals(text))
			{
				int num2 = b3.a(A_1.j2());
				this.k[this.i] = num2;
				this.d(num2);
			}
			if (this.k.Count > 0 && "f".Equals(text))
			{
				int? num3 = (int?)this.k[A_1.jy()];
				if (num3 != null)
				{
					this.d(num3.Value);
				}
			}
			bool result = false;
			if (!(text == "u"))
			{
				if (!(text == "uc"))
				{
					this.a();
					base.b(A_1);
				}
				else
				{
					int num4 = A_1.j2();
					if (num4 < 0 || num4 > 10)
					{
						throw new RtfUnicodeEncodingException(bv.b(A_1.ToString()));
					}
					this.c = num4;
				}
			}
			else
			{
				char value = (char)A_1.j2();
				this.a.Append(value);
				int i = 0;
				while (i < this.c)
				{
					int num5 = ax.a(A_0, true);
					if (num5 <= 32)
					{
						if (num5 != 10 && num5 != 13 && num5 != 32)
						{
							goto IL_2D1;
						}
						A_0.Read();
						result = true;
						if (i == 0)
						{
							i--;
						}
					}
					else if (num5 != 92)
					{
						if (num5 != 123 && num5 != 125)
						{
							goto IL_2D1;
						}
						i = this.c;
					}
					else
					{
						A_0.Read();
						result = true;
						int num6 = ax.b(A_0);
						if (num6 == 39)
						{
							ax.b(A_0);
							ax.b(A_0);
						}
					}
					IL_2DA:
					i++;
					continue;
					IL_2D1:
					A_0.Read();
					result = true;
					goto IL_2DA;
				}
			}
			this.g++;
			return result;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00088CB0 File Offset: 0x00087CB0
		private void a(c9 A_0)
		{
			string text = A_0.jz();
			if (text == "ansi")
			{
				this.d(1252);
				return;
			}
			if (text == "mac")
			{
				this.d(10000);
				return;
			}
			if (text == "pc")
			{
				this.d(437);
				return;
			}
			if (text == "pca")
			{
				this.d(850);
				return;
			}
			if (!(text == "ansicpg"))
			{
				return;
			}
			this.d(A_0.j2());
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00088D44 File Offset: 0x00087D44
		private void d(int A_0)
		{
			if (this.l == null || A_0 != this.l.CodePage)
			{
				if (A_0 == 42 || A_0 == 1252)
				{
					this.l = b3.l;
				}
				else
				{
					this.l = Encoding.GetEncoding(A_0);
				}
				this.m = null;
			}
			if (this.m == null)
			{
				this.m = this.l.GetDecoder();
			}
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00088DAD File Offset: 0x00087DAD
		private static bool c(int A_0)
		{
			return (A_0 >= 97 && A_0 <= 122) || (A_0 >= 65 && A_0 <= 90);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00088DCA File Offset: 0x00087DCA
		private static bool b(int A_0)
		{
			return (A_0 >= 48 && A_0 <= 57) || (A_0 >= 97 && A_0 <= 102) || (A_0 >= 65 && A_0 <= 70);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00088DF1 File Offset: 0x00087DF1
		private static bool a(int A_0)
		{
			return A_0 >= 48 && A_0 <= 57;
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00088E02 File Offset: 0x00087E02
		private static int b(TextReader A_0)
		{
			int num = A_0.Read();
			if (num == -1)
			{
				throw new RtfUnicodeEncodingException(bv.p());
			}
			return num;
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x00088E1C File Offset: 0x00087E1C
		private char a(TextReader A_0)
		{
			bool flag = false;
			int num = 0;
			while (!flag)
			{
				int num2 = ax.b(A_0);
				byte[] bytes = BitConverter.GetBytes(num2);
				if (bytes[1] == 0 && bytes[2] == 0 && bytes[3] == 0)
				{
					this.o[num] = (byte)num2;
					num++;
					int num3;
					int num4;
					this.m.Convert(this.o, 0, num, this.p, 0, 1, true, out num3, out num4, out flag);
					if (flag && (num3 != num || num4 != 1))
					{
						throw new RtfMultiByteEncodingException(bv.a(this.o, num, this.l));
					}
				}
				else
				{
					this.p[0] = Convert.ToChar(num2);
					flag = true;
				}
			}
			return this.p[0];
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x00088EC4 File Offset: 0x00087EC4
		private void b()
		{
			long length = this.n.Length;
			if (length > 0L)
			{
				byte[] array = this.n.ToArray();
				char[] array2 = new char[length];
				int num = 0;
				bool flag = false;
				while (!flag && num < array.Length)
				{
					int num2;
					int num3;
					this.m.Convert(array, num, array.Length - num, array2, 0, array2.Length, true, out num2, out num3, out flag);
					this.a.Append(array2, 0, num3);
					num += num3;
				}
				this.n.SetLength(0L);
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x00088F48 File Offset: 0x00087F48
		private static int a(TextReader A_0, bool A_1)
		{
			int num = A_0.Peek();
			if (A_1 && num == -1)
			{
				throw new RtfMultiByteEncodingException(bv.o());
			}
			return num;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x00088F70 File Offset: 0x00087F70
		private void a()
		{
			if (this.a.Length > 0)
			{
				if (this.e == 0)
				{
					throw new RtfStructureException(bv.a(this.a.ToString()));
				}
				base.a(new b4(this.a.ToString()));
				this.a.Remove(0, this.a.Length);
			}
		}

		// Token: 0x040014CC RID: 5324
		private new StringBuilder a;

		// Token: 0x040014CD RID: 5325
		private new readonly Stack b = new Stack();

		// Token: 0x040014CE RID: 5326
		private new int c;

		// Token: 0x040014CF RID: 5327
		private new readonly Stack d = new Stack();

		// Token: 0x040014D0 RID: 5328
		private new int e;

		// Token: 0x040014D1 RID: 5329
		private new int f;

		// Token: 0x040014D2 RID: 5330
		private new int g;

		// Token: 0x040014D3 RID: 5331
		private int h;

		// Token: 0x040014D4 RID: 5332
		private string i;

		// Token: 0x040014D5 RID: 5333
		private bool j;

		// Token: 0x040014D6 RID: 5334
		private readonly Hashtable k = new Hashtable();

		// Token: 0x040014D7 RID: 5335
		private Encoding l;

		// Token: 0x040014D8 RID: 5336
		private Decoder m;

		// Token: 0x040014D9 RID: 5337
		private readonly MemoryStream n = new MemoryStream();

		// Token: 0x040014DA RID: 5338
		private readonly byte[] o = new byte[8];

		// Token: 0x040014DB RID: 5339
		private char[] p = new char[1];
	}
}
