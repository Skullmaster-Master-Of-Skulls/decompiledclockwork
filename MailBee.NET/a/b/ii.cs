using System;
using System.Collections;
using System.Text;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000262 RID: 610
	internal class ii
	{
		// Token: 0x06001556 RID: 5462 RVA: 0x00061483 File Offset: 0x00060483
		public virtual string fc()
		{
			return this.x.ToString();
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00061490 File Offset: 0x00060490
		public virtual dx fd()
		{
			return this.w;
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00061498 File Offset: 0x00060498
		public virtual long fa()
		{
			return (long)this.w.a;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000614A8 File Offset: 0x000604A8
		private string a()
		{
			e2 e = this.x.b(16381);
			if (e == null)
			{
				e = this.x.b(16350);
			}
			if (e != null)
			{
				return bs.a(e.g);
			}
			return null;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x000614EA File Offset: 0x000604EA
		public virtual string gr()
		{
			return this.d(26);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x000614F4 File Offset: 0x000604F4
		public virtual string kn()
		{
			return this.a(12289, 0, "UTF-7");
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00061507 File Offset: 0x00060507
		public virtual string ff()
		{
			return this.d(12290);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00061514 File Offset: 0x00060514
		public virtual string fb()
		{
			return this.d(12291);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00061521 File Offset: 0x00060521
		public virtual string e8()
		{
			return this.d(12292);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0006152E File Offset: 0x0006052E
		public virtual DateTime ko()
		{
			return this.f(12295);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0006153B File Offset: 0x0006053B
		public virtual DateTime e9()
		{
			return this.f(12296);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00061548 File Offset: 0x00060548
		protected internal ii(bs A_0, dx A_1)
		{
			this.u = A_0;
			this.w = A_1;
			c0 c = new c0(new di(this.u, this.u.e(A_1.b)));
			this.x = c.a();
			if (A_1.c != 0L)
			{
				this.y = A_0.d(A_1.c);
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x000615BD File Offset: 0x000605BD
		internal ii(bs A_0, dx A_1, c0 A_2, fb A_3)
		{
			this.u = A_0;
			this.w = A_1;
			this.x = A_2.a();
			this.z = A_2;
			this.y = A_3;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x000615FA File Offset: 0x000605FA
		protected internal virtual int fe()
		{
			return ii.a(this.w.a);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0006160C File Offset: 0x0006060C
		protected internal static int a(int A_0)
		{
			return A_0 & 31;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00061612 File Offset: 0x00060612
		protected internal virtual int h(int A_0)
		{
			return this.b(A_0, 0);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0006161C File Offset: 0x0006061C
		protected internal virtual int b(int A_0, int A_1)
		{
			if (this.x.a(A_0))
			{
				return this.x.b(A_0).g;
			}
			return A_1;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0006163F File Offset: 0x0006063F
		protected internal virtual bool e(int A_0)
		{
			return this.a(A_0, false);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00061649 File Offset: 0x00060649
		protected internal virtual bool a(int A_0, bool A_1)
		{
			if (this.x.a(A_0))
			{
				return this.x.b(A_0).g != 0;
			}
			return A_1;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0006166F File Offset: 0x0006066F
		protected internal virtual double k(int A_0)
		{
			return this.a(A_0, 0.0);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00061681 File Offset: 0x00060681
		protected internal virtual double a(int A_0, double A_1)
		{
			if (this.x.a(A_0))
			{
				return BitConverter.Int64BitsToDouble(ii.a(this.x.b(A_0).h));
			}
			return A_1;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x000616AE File Offset: 0x000606AE
		protected internal virtual long i(int A_0)
		{
			return this.a(A_0, 0L);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x000616BC File Offset: 0x000606BC
		protected internal virtual long a(int A_0, long A_1)
		{
			if (this.x.a(A_0))
			{
				e2 e = this.x.b(A_0);
				if (e.f == 3)
				{
					return (long)e.g;
				}
				if (e.f == 20 && e.h != null && e.h.Length == 8)
				{
					return ii.b(e.h, 0, 8);
				}
			}
			return A_1;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00061721 File Offset: 0x00060721
		protected internal virtual string d(int A_0)
		{
			return this.a(A_0, 0);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0006172B File Offset: 0x0006072B
		protected internal virtual string a(int A_0, int A_1)
		{
			return this.a(A_0, A_1, null);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00061738 File Offset: 0x00060738
		protected internal virtual string a(int A_0, int A_1, string A_2)
		{
			e2 e = this.x.b(A_0);
			if (e == null)
			{
				return string.Empty;
			}
			if (A_2 == null)
			{
				A_2 = this.a();
			}
			if (A_1 == 0)
			{
				A_1 = e.f;
			}
			if (!e.i)
			{
				return ii.a(e.h, A_1, A_2);
			}
			if (this.y != null && this.y.a(e.g))
			{
				h1 h = this.y.b(e.g);
				try
				{
					byte[] array = h.b();
					if (array == null)
					{
						return string.Empty;
					}
					return ii.a(array, A_1, A_2);
				}
				catch (Exception)
				{
					return "";
				}
			}
			return ii.a(this.v, A_1, A_2);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x000617FC File Offset: 0x000607FC
		internal static string a(byte[] A_0, int A_1, string A_2)
		{
			string result;
			try
			{
				if (A_1 == 31)
				{
					result = Encoding.GetEncoding("UTF-16LE").GetString(A_0, 0, A_0.Length);
				}
				else if (A_1 == 30)
				{
					result = Encoding.GetEncoding(1252).GetString(A_0, 0, A_0.Length);
				}
				else if (A_2 == null)
				{
					result = Encoding.UTF8.GetString(A_0, 0, A_0.Length);
				}
				else
				{
					result = Encoding.GetEncoding(A_2.ToUpper()).GetString(A_0, 0, A_0.Length);
				}
			}
			catch (Exception)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00061888 File Offset: 0x00060888
		public virtual DateTime f(int A_0)
		{
			if (!this.x.a(A_0))
			{
				return DateTime.MinValue;
			}
			e2 e = this.x.b(A_0);
			if (e.h.Length == 0)
			{
				return new DateTime(0L);
			}
			long num = ii.b(e.h, 4, 8);
			long num2 = ii.b(e.h, 0, 4);
			return DateTime.FromFileTime(num << 32 | num2);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000618F0 File Offset: 0x000608F0
		protected internal virtual byte[] g(int A_0)
		{
			if (this.x.a(A_0))
			{
				e2 e = this.x.b(A_0);
				if (e.f == 258)
				{
					if (!e.i)
					{
						return e.h;
					}
					if (this.y != null && this.y.a(e.g))
					{
						h1 h = this.y.b(e.g);
						try
						{
							return h.b();
						}
						catch (Exception)
						{
							return null;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00061984 File Offset: 0x00060984
		protected internal virtual dk j(int A_0)
		{
			byte[] array = this.g(A_0);
			if (array != null && array.Length != 0)
			{
				return new dk(array);
			}
			return null;
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x000619A8 File Offset: 0x000609A8
		public override string ToString()
		{
			return this.y + "\n" + this.x;
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000619C0 File Offset: 0x000609C0
		public static void a(byte[] A_0, bool A_1)
		{
			ii.a(A_0, A_1, new int[0]);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000619D0 File Offset: 0x000609D0
		protected internal static void a(byte[] A_0, bool A_1, int[] A_2)
		{
			string str = "";
			int num = 0;
			int num2 = 0;
			if (A_2.Length != 0)
			{
				num = A_2[0];
				num2++;
			}
			for (int i = 0; i < A_0.Length; i++)
			{
				long num3 = (long)((ulong)A_0[i] & 255UL);
				if (A_2.Length != 0 && i == num && num < A_0.Length)
				{
					str += "+";
					while (num2 < A_2.Length - 1 && A_2[num2] <= num)
					{
						num2++;
					}
					num = A_2[num2];
				}
				if (char.IsLetterOrDigit((char)num3))
				{
					str += ((char)num3).ToString();
				}
				else
				{
					str += ".";
				}
				int length = Convert.ToString(num3, 16).Length;
				bool flag = i % 2 == 1 && A_1;
				if (i % 16 == 15 && A_1)
				{
					str = "";
				}
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00061AA8 File Offset: 0x00060AA8
		protected internal static byte[] c(byte[] A_0)
		{
			for (int i = 0; i < A_0.Length; i++)
			{
				int num = (int)(A_0[i] & byte.MaxValue);
				A_0[i] = (byte)ii.aa[num];
			}
			return A_0;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00061ADC File Offset: 0x00060ADC
		protected internal static byte[] b(byte[] A_0)
		{
			int[] array = new int[ii.aa.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[ii.aa[i]] = i;
			}
			for (int j = 0; j < A_0.Length; j++)
			{
				int num = (int)(A_0[j] & byte.MaxValue);
				A_0[j] = (byte)array[num];
			}
			return A_0;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00061B2E File Offset: 0x00060B2E
		public static long a(byte[] A_0)
		{
			return ii.b(A_0, 0, A_0.Length);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00061B3C File Offset: 0x00060B3C
		public static long b(byte[] A_0, int A_1, int A_2)
		{
			long num = (long)(A_0[A_2 - 1] & byte.MaxValue);
			for (int i = A_2 - 2; i >= A_1; i--)
			{
				num <<= 8;
				long num2 = (long)((ulong)A_0[i] & 255UL);
				num |= num2;
			}
			return num;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00061B7C File Offset: 0x00060B7C
		public static long a(byte[] A_0, int A_1, int A_2)
		{
			long num = 0L;
			for (int i = A_1; i < A_2; i++)
			{
				num <<= 8;
				num |= (long)((ulong)A_0[i] & 255UL);
			}
			return num;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00061BAB File Offset: 0x00060BAB
		public static ii a(bs A_0, long A_1)
		{
			return ii.a(A_0, A_0.f(A_1));
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00061BBC File Offset: 0x00060BBC
		internal static ii a(bs A_0, dx A_1)
		{
			c0 c = new c0(new di(A_0, A_0.e(A_1.b)));
			IDictionaryEnumerator enumerator = c.a().GetEnumerator();
			string arg = string.Empty;
			int num = A_1.a & 31;
			while (enumerator.MoveNext())
			{
				int num2 = (int)enumerator.Key;
				if (num2 >= 1 && num2 <= 3071)
				{
					arg = "Message envelope";
					if (num != 4)
					{
						break;
					}
					break;
				}
				else
				{
					if (num2 >= 13312 && num2 <= 13823)
					{
						arg = "Message store";
						break;
					}
					if (num2 >= 13824 && num2 <= 14079)
					{
						arg = "Folder and address book";
						if (num != 2 && num != 3)
						{
							break;
						}
						break;
					}
					else if (num2 >= 15360 && num2 <= 15615)
					{
						arg = "Distribution list";
						break;
					}
				}
			}
			fb a_ = null;
			if (A_1.c != 0L)
			{
				a_ = A_0.d(A_1.c);
			}
			if (num == 2 || num == 3)
			{
				return new bj(A_0, A_1, c, a_);
			}
			if (num == 4)
			{
				return ii.a(A_0, A_1, c, a_);
			}
			throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstUnknownChildType01, arg, A_1.c), 1210);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00061CE8 File Offset: 0x00060CE8
		internal static co a(bs A_0, dx A_1, c0 A_2, fb A_3)
		{
			e2 e = A_2.a().b(26);
			string text = "";
			if (e != null)
			{
				text = e.c();
			}
			if (text.Equals("IPM.Note"))
			{
				return new co(A_0, A_1, A_2, A_3);
			}
			if (text.Equals("IPM.Appointment") || text.Equals("IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}") || text.StartsWith("IPM.Schedule.Meeting"))
			{
				return new by(A_0, A_1, A_2, A_3);
			}
			if (text.Equals("IPM.Contact"))
			{
				return new fo(A_0, A_1, A_2, A_3);
			}
			if (text.Equals("IPM.Task"))
			{
				return new cv(A_0, A_1, A_2, A_3);
			}
			if (text.Equals("IPM.Activity"))
			{
				return new fm(A_0, A_1, A_2, A_3);
			}
			if (text.Equals("IPM.Post.Rss"))
			{
				return new h5(A_0, A_1, A_2, A_3);
			}
			if (text.Equals("IPM.DistList"))
			{
				return new el(A_0, A_1, A_2, A_3);
			}
			return new co(A_0, A_1, A_2, A_3);
		}

		// Token: 0x04001058 RID: 4184
		public const int a = 0;

		// Token: 0x04001059 RID: 4185
		public const int b = 1;

		// Token: 0x0400105A RID: 4186
		public const int c = 2;

		// Token: 0x0400105B RID: 4187
		public const int d = 3;

		// Token: 0x0400105C RID: 4188
		public const int e = 4;

		// Token: 0x0400105D RID: 4189
		public const int f = 5;

		// Token: 0x0400105E RID: 4190
		public const int g = 6;

		// Token: 0x0400105F RID: 4191
		public const int h = 7;

		// Token: 0x04001060 RID: 4192
		public const int i = 8;

		// Token: 0x04001061 RID: 4193
		public const int j = 10;

		// Token: 0x04001062 RID: 4194
		public const int k = 11;

		// Token: 0x04001063 RID: 4195
		public const int l = 12;

		// Token: 0x04001064 RID: 4196
		public const int m = 13;

		// Token: 0x04001065 RID: 4197
		public const int n = 14;

		// Token: 0x04001066 RID: 4198
		public const int o = 15;

		// Token: 0x04001067 RID: 4199
		public const int p = 16;

		// Token: 0x04001068 RID: 4200
		public const int q = 17;

		// Token: 0x04001069 RID: 4201
		public const int r = 18;

		// Token: 0x0400106A RID: 4202
		public const int s = 19;

		// Token: 0x0400106B RID: 4203
		public const int t = 31;

		// Token: 0x0400106C RID: 4204
		protected internal bs u;

		// Token: 0x0400106D RID: 4205
		protected internal byte[] v = new byte[1];

		// Token: 0x0400106E RID: 4206
		protected internal dx w;

		// Token: 0x0400106F RID: 4207
		internal gs x;

		// Token: 0x04001070 RID: 4208
		internal fb y;

		// Token: 0x04001071 RID: 4209
		protected internal c0 z;

		// Token: 0x04001072 RID: 4210
		internal static int[] aa = new int[]
		{
			71,
			241,
			180,
			230,
			11,
			106,
			114,
			72,
			133,
			78,
			158,
			235,
			226,
			248,
			148,
			83,
			224,
			187,
			160,
			2,
			232,
			90,
			9,
			171,
			219,
			227,
			186,
			198,
			124,
			195,
			16,
			221,
			57,
			5,
			150,
			48,
			245,
			55,
			96,
			130,
			140,
			201,
			19,
			74,
			107,
			29,
			243,
			251,
			143,
			38,
			151,
			202,
			145,
			23,
			1,
			196,
			50,
			45,
			110,
			49,
			149,
			255,
			217,
			35,
			209,
			0,
			94,
			121,
			220,
			68,
			59,
			26,
			40,
			197,
			97,
			87,
			32,
			144,
			61,
			131,
			185,
			67,
			190,
			103,
			210,
			70,
			66,
			118,
			192,
			109,
			91,
			126,
			178,
			15,
			22,
			41,
			60,
			169,
			3,
			84,
			13,
			218,
			93,
			223,
			246,
			183,
			199,
			98,
			205,
			141,
			6,
			211,
			105,
			92,
			134,
			214,
			20,
			247,
			165,
			102,
			117,
			172,
			177,
			233,
			69,
			33,
			112,
			12,
			135,
			159,
			116,
			164,
			34,
			76,
			111,
			191,
			31,
			86,
			170,
			46,
			179,
			120,
			51,
			80,
			176,
			163,
			146,
			188,
			207,
			25,
			28,
			167,
			99,
			203,
			30,
			77,
			62,
			75,
			27,
			155,
			79,
			231,
			240,
			238,
			173,
			58,
			181,
			89,
			4,
			234,
			64,
			85,
			37,
			81,
			229,
			122,
			137,
			56,
			104,
			82,
			123,
			252,
			39,
			174,
			215,
			189,
			250,
			7,
			244,
			204,
			142,
			95,
			239,
			53,
			156,
			132,
			43,
			21,
			213,
			119,
			52,
			73,
			182,
			18,
			10,
			127,
			113,
			136,
			253,
			157,
			24,
			65,
			125,
			147,
			216,
			88,
			44,
			206,
			254,
			36,
			175,
			222,
			184,
			54,
			200,
			161,
			128,
			166,
			153,
			152,
			168,
			47,
			14,
			129,
			101,
			115,
			228,
			194,
			162,
			138,
			212,
			225,
			17,
			208,
			8,
			139,
			42,
			242,
			237,
			154,
			100,
			63,
			193,
			108,
			249,
			236
		};

		// Token: 0x04001073 RID: 4211
		private const long ab = 11644473600000L;
	}
}
