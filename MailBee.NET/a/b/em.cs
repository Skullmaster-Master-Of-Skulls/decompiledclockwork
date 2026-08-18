using System;
using System.Collections;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000296 RID: 662
	internal class em
	{
		// Token: 0x06001739 RID: 5945 RVA: 0x00069B97 File Offset: 0x00068B97
		public virtual long e()
		{
			return this.a;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00069B9F File Offset: 0x00068B9F
		public virtual void a(long A_0)
		{
			this.a = A_0;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00069BA8 File Offset: 0x00068BA8
		public virtual long d()
		{
			return this.b;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00069BB0 File Offset: 0x00068BB0
		public virtual void b(long A_0)
		{
			this.b = A_0;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00069BB9 File Offset: 0x00068BB9
		public virtual object c()
		{
			return this.c;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00069BC1 File Offset: 0x00068BC1
		public virtual void b(object A_0)
		{
			this.c = A_0;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00069BCA File Offset: 0x00068BCA
		public em(long A_0, long A_1, object A_2)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00069BE8 File Offset: 0x00068BE8
		public em(long A_0, byte[] A_1, long A_2, int A_3, int A_4)
		{
			this.a = A_0;
			if (A_0 == 0L)
			{
				this.c = this.a(A_1, A_2, A_3, A_4);
				return;
			}
			int num = (int)A_2;
			this.b = p.h(A_1, num);
			num += 4;
			try
			{
				this.c = e3.a(A_1, num, A_3, (long)((int)this.b), A_4);
			}
			catch (UnsupportedVariantTypeException ex)
			{
				e3.a(ex);
				this.c = ex.Value;
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00069C6C File Offset: 0x00068C6C
		protected em()
		{
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00069C74 File Offset: 0x00068C74
		protected IDictionary a(byte[] A_0, long A_1, int A_2, int A_3)
		{
			if (A_1 < 0L || A_1 > (long)A_0.Length)
			{
				throw new HPSFRuntimeException(string.Concat(new object[]
				{
					"Illegal offset ",
					A_1,
					" while HPSF stream Contains ",
					A_2,
					" bytes."
				}));
			}
			int num = (int)A_1;
			long num2 = p.h(A_0, num);
			num += 4;
			Hashtable hashtable = new Hashtable((int)num2, 1f);
			try
			{
				int num3 = 0;
				while ((long)num3 < num2)
				{
					long num4 = p.h(A_0, num);
					num += 4;
					long num5 = p.h(A_0, num);
					num += 4;
					StringBuilder stringBuilder = new StringBuilder();
					if (A_3 != -1)
					{
						if (A_3 != 1200)
						{
							stringBuilder.Append(Encoding.GetEncoding(A_3).GetString(A_0, num, (int)num5));
						}
						else
						{
							int num6 = (int)(num5 * 2L);
							byte[] array = new byte[num6];
							for (int i = 0; i < num6; i++)
							{
								array[i] = A_0[num + i];
							}
							stringBuilder.Append(Encoding.GetEncoding(A_3).GetString(array, 0, num6 - 2));
						}
					}
					else
					{
						stringBuilder.Append(Encoding.UTF8.GetString(A_0, num, (int)num5));
					}
					while (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\0')
					{
						stringBuilder.Length--;
					}
					if (A_3 == 1200)
					{
						if (num5 % 2L == 1L)
						{
							num5 += 1L;
						}
						num += (int)(num5 + num5);
					}
					else
					{
						num += (int)num5;
					}
					hashtable[num4] = stringBuilder.ToString();
					num3++;
				}
			}
			catch (Exception a_)
			{
				gn.a(typeof(em)).i4(5, "The property Set's dictionary Contains bogus data. All dictionary entries starting with the one with ID " + this.a + " will be ignored.", a_);
			}
			return hashtable;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00069E64 File Offset: 0x00068E64
		public int b()
		{
			int num = iu.a(this.b);
			if (num >= 0)
			{
				return num;
			}
			if (num == -2)
			{
				throw new WritingNotSupportedException(this.b, null);
			}
			int num2 = 4;
			int num3 = (int)this.b;
			if (num3 != 0)
			{
				if (num3 != 30)
				{
					throw new WritingNotSupportedException(this.b, this.c);
				}
				int num4 = ((string)this.c).Length + 1;
				int num5 = num4 % num2;
				if (num5 > 0)
				{
					num4 += num2 - num5;
				}
				num += num4;
			}
			return num;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00069EE4 File Offset: 0x00068EE4
		public override bool Equals(object o)
		{
			if (!(o is em))
			{
				return false;
			}
			em em = (em)o;
			object obj = em.c();
			long num = em.e();
			if (this.a != num || (this.a != 0L && !this.a(this.b, em.d())))
			{
				return false;
			}
			if (this.c == null && obj == null)
			{
				return true;
			}
			if (this.c == null || obj == null)
			{
				return false;
			}
			Type type = this.c.GetType();
			Type type2 = obj.GetType();
			if (!type.IsAssignableFrom(type2) && !type2.IsAssignableFrom(type))
			{
				return false;
			}
			if (this.c is byte[])
			{
				return d4.a((byte[])this.c, (byte[])obj);
			}
			return this.c.Equals(obj);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00069FAB File Offset: 0x00068FAB
		private bool a(long A_0, long A_1)
		{
			return A_0 == A_1 || (A_0 == 30L && A_1 == 31L) || (A_1 == 30L && A_0 == 31L);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00069FCC File Offset: 0x00068FCC
		public override int GetHashCode()
		{
			long num = 0L;
			num += this.a;
			num += this.b;
			if (this.c != null)
			{
				num += (long)this.c.GetHashCode();
			}
			return (int)(num & (long)((ulong)-1));
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0006A00C File Offset: 0x0006900C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.GetType().Name);
			stringBuilder.Append('[');
			stringBuilder.Append("id: ");
			stringBuilder.Append(this.e());
			stringBuilder.Append(", type: ");
			stringBuilder.Append(base.GetType());
			object obj = this.c();
			stringBuilder.Append(", value: ");
			if (obj is string)
			{
				stringBuilder.Append(obj.ToString());
				string text = obj.ToString();
				int length = text.Length;
				byte[] array = new byte[length * 2];
				for (int i = 0; i < length; i++)
				{
					char c = text[i];
					byte b = (byte)((c & '＀') >> 8);
					byte b2 = (byte)(c & 'ÿ');
					array[i * 2] = b;
					array[i * 2 + 1] = b2;
				}
				stringBuilder.Append(" [");
				if (array.Length != 0)
				{
					string value = f5.a(array, 0L, 0);
					stringBuilder.Append(value);
				}
				stringBuilder.Append("]");
			}
			else if (obj is byte[])
			{
				byte[] array2 = (byte[])obj;
				if (array2.Length != 0)
				{
					string value2 = f5.a(array2, 0L, 0);
					stringBuilder.Append(value2);
				}
			}
			else
			{
				stringBuilder.Append(obj.ToString());
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x04001153 RID: 4435
		protected long a;

		// Token: 0x04001154 RID: 4436
		protected long b;

		// Token: 0x04001155 RID: 4437
		protected object c;
	}
}
