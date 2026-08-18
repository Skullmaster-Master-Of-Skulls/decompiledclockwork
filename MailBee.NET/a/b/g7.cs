using System;
using System.Collections;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000298 RID: 664
	internal class g7
	{
		// Token: 0x06001763 RID: 5987 RVA: 0x0006A99B File Offset: 0x0006999B
		public ar e()
		{
			return this.b;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0006A9A3 File Offset: 0x000699A3
		public long g()
		{
			return this.c;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0006A9AB File Offset: 0x000699AB
		public virtual int ah()
		{
			return this.d;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0006A9B3 File Offset: 0x000699B3
		public virtual int ai()
		{
			return this.e.Length;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0006A9BD File Offset: 0x000699BD
		public virtual em[] aj()
		{
			return this.e;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0006A9C5 File Offset: 0x000699C5
		protected g7()
		{
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0006A9D0 File Offset: 0x000699D0
		public g7(byte[] A_0, int A_1)
		{
			this.b = new ar(A_0, A_1);
			int num = A_1 + 16;
			this.c = p.h(A_0, num);
			num = (int)this.c;
			this.d = (int)p.h(A_0, num);
			num += 4;
			int num2 = (int)p.h(A_0, num);
			num += 4;
			this.e = new em[num2];
			int num3 = num;
			ArrayList arrayList = new ArrayList(num2);
			for (int i = 0; i < this.e.Length; i++)
			{
				g7.a a = new g7.a();
				a.a = (int)p.h(A_0, num3);
				num3 += 4;
				a.b = (int)p.h(A_0, num3);
				num3 += 4;
				arrayList.Add(a);
			}
			arrayList.Sort();
			for (int j = 0; j < num2 - 1; j++)
			{
				g7.a a2 = (g7.a)arrayList[j];
				g7.a a3 = (g7.a)arrayList[j + 1];
				a2.c = a3.b - a2.b;
			}
			if (num2 > 0)
			{
				g7.a a = (g7.a)arrayList[num2 - 1];
				a.c = this.d - a.b;
			}
			int num4 = -1;
			IEnumerator enumerator = arrayList.GetEnumerator();
			while (num4 == -1 && enumerator.MoveNext())
			{
				g7.a a = (g7.a)enumerator.Current;
				if (a.a == 1)
				{
					int num5 = (int)(this.c + (long)a.b);
					long num6 = p.h(A_0, num5);
					num5 += 4;
					if (num6 != 2L)
					{
						throw new HPSFRuntimeException("Value type of property ID 1 is not VT_I2 but " + num6 + ".");
					}
					num4 = p.j(A_0, num5);
				}
			}
			int num7 = 0;
			foreach (object obj in arrayList)
			{
				g7.a a = (g7.a)obj;
				em em = new em((long)a.a, A_0, this.c + (long)a.b, a.c, num4);
				if (em.e() == 1L)
				{
					em = new em(em.e(), em.d(), num4);
				}
				this.e[num7++] = em;
			}
			this.a = (IDictionary)this.ak(0L);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0006AC1C File Offset: 0x00069C1C
		public virtual object ak(long A_0)
		{
			this.f = false;
			for (int i = 0; i < this.e.Length; i++)
			{
				if (A_0 == this.e[i].e())
				{
					return this.e[i].c();
				}
			}
			this.f = true;
			return null;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0006AC6C File Offset: 0x00069C6C
		public virtual int c(long A_0)
		{
			object obj = this.ak(A_0);
			if (obj == null)
			{
				return 0;
			}
			if (!(obj is long) && !(obj is int))
			{
				throw new HPSFRuntimeException("This property is not an integer type, but " + obj.GetType().Name + ".");
			}
			return (int)obj;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0006ACBC File Offset: 0x00069CBC
		public virtual bool b(int A_0)
		{
			return this.ak((long)A_0) != null && (bool)this.ak((long)A_0);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0006ACD7 File Offset: 0x00069CD7
		public virtual bool f()
		{
			return this.f;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0006ACE0 File Offset: 0x00069CE0
		public string b(long A_0)
		{
			string text = null;
			if (this.a != null)
			{
				text = (string)this.a[A_0];
			}
			if (text == null)
			{
				text = @as.a(this.e().a(), A_0);
			}
			if (text == null)
			{
				text = "[undefined]";
			}
			return text;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0006AD30 File Offset: 0x00069D30
		public override bool Equals(object o)
		{
			if (o == null || !(o is g7))
			{
				return false;
			}
			g7 g = (g7)o;
			if (!g.e().Equals(this.e()))
			{
				return false;
			}
			em[] array = new em[this.aj().Length];
			em[] array2 = new em[g.aj().Length];
			Array.Copy(this.aj(), 0, array, 0, array.Length);
			Array.Copy(g.aj(), 0, array2, 0, array2.Length);
			em em = null;
			em em2 = null;
			for (int i = 0; i < array.Length; i++)
			{
				long num = array[i].e();
				if (num == 0L)
				{
					em = array[i];
					array = this.a(array, i);
					i--;
				}
				if (num == 1L)
				{
					array = this.a(array, i);
					i--;
				}
			}
			for (int j = 0; j < array2.Length; j++)
			{
				long num2 = array2[j].e();
				if (num2 == 0L)
				{
					em2 = array2[j];
					array2 = this.a(array2, j);
					j--;
				}
				if (num2 == 1L)
				{
					array2 = this.a(array2, j);
					j--;
				}
			}
			if (array.Length != array2.Length)
			{
				return false;
			}
			bool flag = true;
			if (em != null && em2 != null)
			{
				Hashtable hashtable = (Hashtable)em.c();
				Hashtable hashtable2 = (Hashtable)em2.c();
				flag = (hashtable.Count == hashtable2.Count);
			}
			else if (em != null || em2 != null)
			{
				flag = false;
			}
			return flag && a8.b(array, array2);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0006AE90 File Offset: 0x00069E90
		private em[] a(em[] A_0, int A_1)
		{
			em[] array = new em[A_0.Length - 1];
			if (A_1 > 0)
			{
				Array.Copy(A_0, 0, array, 0, A_1);
			}
			Array.Copy(A_0, A_1 + 1, array, A_1, array.Length - A_1);
			return array;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0006AEC8 File Offset: 0x00069EC8
		public override int GetHashCode()
		{
			long num = 0L;
			num += (long)this.e().GetHashCode();
			em[] array = this.aj();
			for (int i = 0; i < array.Length; i++)
			{
				num += (long)array[i].GetHashCode();
			}
			return (int)(num & (long)((ulong)-1));
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0006AF10 File Offset: 0x00069F10
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			em[] array = this.aj();
			stringBuilder.Append(base.GetType().Name);
			stringBuilder.Append('[');
			stringBuilder.Append("formatID: ");
			stringBuilder.Append(this.e());
			stringBuilder.Append(", offset: ");
			stringBuilder.Append(this.g());
			stringBuilder.Append(", propertyCount: ");
			stringBuilder.Append(this.ai());
			stringBuilder.Append(", size: ");
			stringBuilder.Append(this.ah());
			stringBuilder.Append(", properties: [\n");
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString());
				stringBuilder.Append(",\n");
			}
			stringBuilder.Append(']');
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0006AFF6 File Offset: 0x00069FF6
		public virtual IDictionary al()
		{
			if (this.a == null)
			{
				this.a = new Hashtable();
			}
			return this.a;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0006B011 File Offset: 0x0006A011
		public virtual void am(IDictionary A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0006B01A File Offset: 0x0006A01A
		public int a()
		{
			if (this.ak(1L) == null)
			{
				return -1;
			}
			return (int)this.ak(1L);
		}

		// Token: 0x04001159 RID: 4441
		protected IDictionary a;

		// Token: 0x0400115A RID: 4442
		protected ar b;

		// Token: 0x0400115B RID: 4443
		protected long c;

		// Token: 0x0400115C RID: 4444
		protected int d;

		// Token: 0x0400115D RID: 4445
		protected em[] e;

		// Token: 0x0400115E RID: 4446
		private bool f;

		// Token: 0x0200029B RID: 667
		private class a : IComparable
		{
			// Token: 0x0600177C RID: 6012 RVA: 0x0006B164 File Offset: 0x0006A164
			public int CompareTo(object o)
			{
				if (!(o is g7.a))
				{
					throw new InvalidCastException(o.ToString());
				}
				int num = ((g7.a)o).b;
				if (this.b < num)
				{
					return -1;
				}
				if (this.b == num)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x0600177D RID: 6013 RVA: 0x0006B1A8 File Offset: 0x0006A1A8
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.GetType().Name);
				stringBuilder.Append("[id=");
				stringBuilder.Append(this.a);
				stringBuilder.Append(", offset=");
				stringBuilder.Append(this.b);
				stringBuilder.Append(", Length=");
				stringBuilder.Append(this.c);
				stringBuilder.Append(']');
				return stringBuilder.ToString();
			}

			// Token: 0x0400115F RID: 4447
			public int a;

			// Token: 0x04001160 RID: 4448
			public int b;

			// Token: 0x04001161 RID: 4449
			public int c;
		}
	}
}
