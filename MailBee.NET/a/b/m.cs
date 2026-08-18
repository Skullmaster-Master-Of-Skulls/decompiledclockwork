using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace a.b
{
	// Token: 0x0200028A RID: 650
	[DefaultMember("Item")]
	internal class m : Hashtable
	{
		// Token: 0x060016EC RID: 5868 RVA: 0x00068E4C File Offset: 0x00067E4C
		public cz a(string A_0, cz A_1)
		{
			if (string.IsNullOrEmpty(A_0))
			{
				this.c = false;
				return null;
			}
			if (A_0 == null)
			{
				throw new ArgumentException("The name of a custom property must be a String, but it is a " + A_0.GetType().Name);
			}
			if (!A_0.Equals(A_1.a()))
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Parameter \"name\" (",
					A_0,
					") and custom property's name (",
					A_1.a(),
					") do not match."
				}));
			}
			long num = A_1.e();
			object obj = this.b[A_0];
			if (obj != null)
			{
				this.a.Remove(obj);
			}
			this.b[A_0] = num;
			this.a[num] = A_0;
			if (obj != null)
			{
				base.Remove(obj);
			}
			base[num] = A_1;
			return A_1;
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00068F2B File Offset: 0x00067F2B
		public ICollection d()
		{
			return this.b.Keys;
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00068F38 File Offset: 0x00067F38
		public ICollection c()
		{
			return this.b.Keys;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00068F45 File Offset: 0x00067F45
		public ICollection e()
		{
			return this.b.Keys;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00068F54 File Offset: 0x00067F54
		private object a(cz A_0)
		{
			string text = A_0.a();
			object obj = this.b[text];
			if (obj != null)
			{
				A_0.a((long)obj);
			}
			else
			{
				long num = 1L;
				foreach (object obj2 in this.a.Keys)
				{
					long num2 = (long)obj2;
					if (num2 > num)
					{
						num = num2;
					}
				}
				A_0.a(num + 1L);
			}
			return this.a(text, A_0);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00068FCC File Offset: 0x00067FCC
		public object b(string A_0)
		{
			if (this.b[A_0] == null)
			{
				return null;
			}
			long num = (long)this.b[A_0];
			this.a.Remove(num);
			this.b.Remove(A_0);
			object result = (cz)this[num];
			this.Remove(num);
			return result;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00069038 File Offset: 0x00068038
		public object a(string A_0, string A_1)
		{
			fu fu = new fu();
			fu.a(-1L);
			fu.b(31L);
			fu.b(A_1);
			cz a_ = new cz(fu, A_0);
			return this.a(a_);
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00069070 File Offset: 0x00068070
		public object a(string A_0, long A_1)
		{
			fu fu = new fu();
			fu.a(-1L);
			fu.b(20L);
			fu.b(A_1);
			cz a_ = new cz(fu, A_0);
			return this.a(a_);
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000690B0 File Offset: 0x000680B0
		public object a(string A_0, double A_1)
		{
			fu fu = new fu();
			fu.a(-1L);
			fu.b(5L);
			fu.b(A_1);
			cz a_ = new cz(fu, A_0);
			return this.a(a_);
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x000690EC File Offset: 0x000680EC
		public object a(string A_0, int A_1)
		{
			fu fu = new fu();
			fu.a(-1L);
			fu.b(3L);
			fu.b(A_1);
			cz a_ = new cz(fu, A_0);
			return this.a(a_);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00069128 File Offset: 0x00068128
		public object a(string A_0, bool A_1)
		{
			fu fu = new fu();
			fu.a(-1L);
			fu.b(11L);
			fu.b(A_1);
			cz a_ = new cz(fu, A_0);
			return this.a(a_);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00069168 File Offset: 0x00068168
		public object a(string A_0, DateTime A_1)
		{
			fu fu = new fu();
			fu.a(-1L);
			fu.b(64L);
			fu.b(A_1);
			cz a_ = new cz(fu, A_0);
			return this.a(a_);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000691A8 File Offset: 0x000681A8
		public object a(string A_0)
		{
			object obj = this.b[A_0];
			if (obj == null)
			{
				foreach (object obj2 in this.b)
				{
					string s = ((DictionaryEntry)obj2).Key as string;
					int num = this.a();
					if (num < 0)
					{
						num = 1200;
					}
					object bytes = Encoding.GetEncoding(num).GetBytes(s);
					byte[] bytes2 = Encoding.UTF8.GetBytes(A_0);
					if (d4.a(bytes, bytes2))
					{
						IEnumerator enumerator;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
						obj = dictionaryEntry.Value;
					}
				}
				if (obj == null)
				{
					return null;
				}
			}
			long num2 = (long)obj;
			cz cz = (cz)base[num2];
			if (cz == null)
			{
				return null;
			}
			return cz.c();
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00069270 File Offset: 0x00068270
		public override bool ContainsKey(object key)
		{
			if (key is long)
			{
				return base.ContainsKey((long)key);
			}
			return key is string && base.ContainsKey((long)this.b[key]);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000692C0 File Offset: 0x000682C0
		public override bool ContainsValue(object value)
		{
			if (value is cz)
			{
				return base.ContainsValue(value);
			}
			using (IEnumerator enumerator = base.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((enumerator.Current as cz).c() == value)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00069330 File Offset: 0x00068330
		public IDictionary f()
		{
			return this.a;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00069338 File Offset: 0x00068338
		public int a()
		{
			int num = -1;
			IEnumerator enumerator = this.Values.GetEnumerator();
			while (num == -1 && enumerator.MoveNext())
			{
				cz cz = (cz)enumerator.Current;
				if (cz.e() == 1L)
				{
					num = (int)cz.c();
				}
			}
			return num;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00069384 File Offset: 0x00068384
		public void a(int A_0)
		{
			fu fu = new fu();
			fu.a(1L);
			fu.b(2L);
			fu.b(A_0);
			this.a(new cz(fu));
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000693C0 File Offset: 0x000683C0
		public bool b()
		{
			return this.c;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000693C8 File Offset: 0x000683C8
		public void a(bool A_0)
		{
			this.c = A_0;
		}

		// Token: 0x04001139 RID: 4409
		private Hashtable a = new Hashtable();

		// Token: 0x0400113A RID: 4410
		private Hashtable b = new Hashtable();

		// Token: 0x0400113B RID: 4411
		private bool c = true;
	}
}
