using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace a.b
{
	// Token: 0x020002DA RID: 730
	internal class eg : cr, af, gj
	{
		// Token: 0x060019CA RID: 6602 RVA: 0x00072518 File Offset: 0x00071518
		public eg(string A_0, i4[] A_1, int A_2)
		{
			this.d = A_2;
			if (A_1.Length == 0)
			{
				this.e = c5.b;
			}
			else
			{
				this.e = ((A_1[0].a() == 512) ? c5.b : c5.d);
			}
			this.g = new eg.b(this.e, eg.b(A_1));
			this.c = new gg(A_0, this.d);
			this.f = new eg.a(this.e, eg.b);
			this.c.a(this);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000725B0 File Offset: 0x000715B0
		private static ah[] b(bn[] A_0)
		{
			ah[] array = new ah[A_0.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ah((i4)A_0[i]);
			}
			return array;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000725E8 File Offset: 0x000715E8
		private static aw[] a(bn[] A_0)
		{
			if (A_0 is aw[])
			{
				return (aw[])A_0;
			}
			aw[] array = new aw[A_0.Length];
			Array.Copy(A_0, 0, array, 0, A_0.Length);
			return array;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0007261C File Offset: 0x0007161C
		public eg(string A_0, aw[] A_1, int A_2)
		{
			this.d = A_2;
			if (A_1.Length == 0)
			{
				this.e = c5.b;
			}
			else
			{
				this.e = A_1[0].a();
			}
			this.g = new eg.b(this.e, eg.a);
			this.c = new gg(A_0, this.d);
			this.f = new eg.a(this.e, A_1);
			this.c.a(this);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0007269C File Offset: 0x0007169C
		public eg(string A_0, y A_1, bn[] A_2, int A_3)
		{
			this.d = A_3;
			this.e = A_1;
			this.c = new gg(A_0, this.d);
			this.c.a(this);
			if (ed.b(this.d))
			{
				this.g = new eg.b(A_1, eg.a);
				this.f = new eg.a(A_1, eg.a(A_2));
				return;
			}
			this.g = new eg.b(A_1, eg.b(A_2));
			this.f = new eg.a(A_1, eg.b);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00072730 File Offset: 0x00071730
		public eg(string A_0, y A_1, Stream A_2)
		{
			List<ah> list = new List<ah>();
			this.d = 0;
			this.e = A_1;
			ah ah;
			do
			{
				ah = new ah(A_2, A_1);
				int num = ah.b();
				if (num > 0)
				{
					list.Add(ah);
					this.d += num;
				}
			}
			while (!ah.c());
			ah[] a_ = list.ToArray();
			this.g = new eg.b(A_1, a_);
			this.c = new gg(A_0, this.d);
			this.c.a(this);
			if (this.c.g())
			{
				this.f = new eg.a(A_1, aw.a(A_1, a_, this.d));
				this.g = new eg.b(A_1, new ah[0]);
				return;
			}
			this.f = new eg.a(A_1, eg.b);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00072803 File Offset: 0x00071803
		public eg(string A_0, Stream A_1) : this(A_0, c5.b, A_1)
		{
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00072814 File Offset: 0x00071814
		public eg(string A_0, int A_1, y A_2, db A_3, dn A_4)
		{
			this.d = A_1;
			this.e = A_2;
			this.c = new gg(A_0, this.d);
			this.c.a(this);
			if (this.c.g())
			{
				this.f = new eg.a(this.e, A_3, A_0, A_1, A_4);
				this.g = new eg.b(this.e, eg.a);
				return;
			}
			this.f = new eg.a(this.e, eg.b);
			this.g = new eg.b(this.e, A_3, A_0, A_1, A_4);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000728BB File Offset: 0x000718BB
		public eg(string A_0, int A_1, db A_2, dn A_3) : this(A_0, A_1, c5.b, A_2, A_3)
		{
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000728CD File Offset: 0x000718CD
		public eg(string A_0, bn[] A_1, int A_2) : this(A_0, c5.b, A_1, A_2)
		{
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000728DD File Offset: 0x000718DD
		public virtual void a(byte[] A_0, int A_1)
		{
			if (this.c.g())
			{
				aw.a(this.f.b(), A_0, A_1);
				return;
			}
			ah.a(this.g.b(), A_0, A_1);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00072911 File Offset: 0x00071911
		public virtual void a3(Stream A_0)
		{
			this.g.a(A_0);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00072920 File Offset: 0x00071920
		public fd a(int A_0)
		{
			if (A_0 >= this.d)
			{
				if (A_0 > this.d)
				{
					throw new Exception(string.Concat(new object[]
					{
						"Request for Offset ",
						A_0,
						" doc size is ",
						this.d
					}));
				}
				return null;
			}
			else
			{
				if (this.c.g())
				{
					return aw.a(this.f.b(), A_0);
				}
				return ah.a(this.g.b(), A_0);
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000729A9 File Offset: 0x000719A9
		public virtual int ap()
		{
			return this.g.a();
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000729B6 File Offset: 0x000719B6
		public virtual gg c()
		{
			return this.c;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000729BE File Offset: 0x000719BE
		public virtual bool jk()
		{
			return true;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x000729C4 File Offset: 0x000719C4
		public virtual string jl()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Document: \"").Append(this.c.f()).Append("\"");
			stringBuilder.Append(" size = ").Append(this.a());
			return stringBuilder.ToString();
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00072A18 File Offset: 0x00071A18
		public virtual int a()
		{
			return this.d;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00072A20 File Offset: 0x00071A20
		public virtual af[] b()
		{
			return this.f.b();
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00072A2D File Offset: 0x00071A2D
		public virtual int d()
		{
			return this.c.i();
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00072A3A File Offset: 0x00071A3A
		public virtual void jm(int A_0)
		{
			this.c.c(A_0);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00072A48 File Offset: 0x00071A48
		public Array ji()
		{
			object[] array = new object[1];
			string text;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					af[] array2 = null;
					if (this.g.c())
					{
						array2 = this.g.b();
					}
					else if (this.f.a())
					{
						array2 = this.f.b();
					}
					if (array2 != null)
					{
						for (int i = 0; i < array2.Length; i++)
						{
							array2[i].a3(memoryStream);
						}
						byte[] array3 = memoryStream.ToArray();
						if (array3.Length > this.c.h())
						{
							byte[] array4 = new byte[this.c.h()];
							Array.Copy(array3, 0, array4, 0, array4.Length);
							array3 = array4;
						}
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							f5.a(array3, 0L, memoryStream2, 0);
							Array buffer = memoryStream2.GetBuffer();
							char[] array5 = new char[(int)memoryStream2.Length];
							Array.Copy(buffer, 0, array5, 0, array5.Length);
							text = new string(array5);
							goto IL_F5;
						}
					}
					text = "<NO DATA>";
					IL_F5:;
				}
			}
			catch (IOException ex)
			{
				text = ex.Message;
			}
			array[0] = text;
			return array;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00072B90 File Offset: 0x00071B90
		public virtual IEnumerator jj()
		{
			return ArrayList.ReadOnly(new ArrayList()).GetEnumerator();
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00072BA4 File Offset: 0x00071BA4
		[CompilerGenerated]
		public void a(hg A_0)
		{
			hg hg = this.h;
			hg hg2;
			do
			{
				hg2 = hg;
				hg value = (hg)Delegate.Combine(hg2, A_0);
				hg = Interlocked.CompareExchange<hg>(ref this.h, value, hg2);
			}
			while (hg != hg2);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00072BDC File Offset: 0x00071BDC
		[CompilerGenerated]
		public void b(hg A_0)
		{
			hg hg = this.h;
			hg hg2;
			do
			{
				hg2 = hg;
				hg value = (hg)Delegate.Remove(hg2, A_0);
				hg = Interlocked.CompareExchange<hg>(ref this.h, value, hg2);
			}
			while (hg != hg2);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00072C11 File Offset: 0x00071C11
		protected virtual void a(av A_0)
		{
			if (this.h != null)
			{
				this.h(this, A_0);
			}
		}

		// Token: 0x0400128B RID: 4747
		private static ah[] a = new ah[0];

		// Token: 0x0400128C RID: 4748
		private static aw[] b = new aw[0];

		// Token: 0x0400128D RID: 4749
		private gg c;

		// Token: 0x0400128E RID: 4750
		private int d;

		// Token: 0x0400128F RID: 4751
		private y e;

		// Token: 0x04001290 RID: 4752
		private eg.a f;

		// Token: 0x04001291 RID: 4753
		private eg.b g;

		// Token: 0x04001292 RID: 4754
		[CompilerGenerated]
		private hg h;

		// Token: 0x020002DC RID: 732
		internal class a
		{
			// Token: 0x060019E6 RID: 6630 RVA: 0x00072C40 File Offset: 0x00071C40
			internal a(y A_0, aw[] A_1)
			{
				this.f = A_0;
				this.a = (aw[])A_1.Clone();
				this.b = null;
				this.c = null;
				this.d = -1;
				this.e = null;
			}

			// Token: 0x060019E7 RID: 6631 RVA: 0x00072C7C File Offset: 0x00071C7C
			internal a(y A_0, db A_1, string A_2, int A_3, dn A_4)
			{
				this.f = A_0;
				this.a = new aw[0];
				this.b = A_1;
				this.c = A_2;
				this.d = A_3;
				this.e = A_4;
			}

			// Token: 0x060019E8 RID: 6632 RVA: 0x00072CB8 File Offset: 0x00071CB8
			internal virtual aw[] b()
			{
				if (this.a() && this.e != null)
				{
					MemoryStream memoryStream = new MemoryStream(this.d);
					cm a_ = new cm(memoryStream, this.d);
					this.e.a(new fi(a_, this.b, this.c, this.d));
					this.a = aw.a(this.f, memoryStream.ToArray(), this.d);
				}
				return this.a;
			}

			// Token: 0x060019E9 RID: 6633 RVA: 0x00072D34 File Offset: 0x00071D34
			internal virtual bool a()
			{
				return this.a.Length != 0 || this.e != null;
			}

			// Token: 0x04001293 RID: 4755
			private aw[] a;

			// Token: 0x04001294 RID: 4756
			private db b;

			// Token: 0x04001295 RID: 4757
			private string c;

			// Token: 0x04001296 RID: 4758
			private int d;

			// Token: 0x04001297 RID: 4759
			private dn e;

			// Token: 0x04001298 RID: 4760
			private y f;
		}

		// Token: 0x020002DD RID: 733
		internal class b
		{
			// Token: 0x060019EA RID: 6634 RVA: 0x00072D4A File Offset: 0x00071D4A
			internal b(y A_0, ah[] A_1)
			{
				this.f = A_0;
				this.a = (ah[])A_1.Clone();
				this.b = null;
				this.c = null;
				this.d = -1;
				this.e = null;
			}

			// Token: 0x060019EB RID: 6635 RVA: 0x00072D86 File Offset: 0x00071D86
			internal b(y A_0, db A_1, string A_2, int A_3, dn A_4)
			{
				this.f = A_0;
				this.a = new ah[0];
				this.b = A_1;
				this.c = A_2;
				this.d = A_3;
				this.e = A_4;
			}

			// Token: 0x060019EC RID: 6636 RVA: 0x00072DBF File Offset: 0x00071DBF
			internal virtual bool c()
			{
				return this.a.Length != 0 || this.e != null;
			}

			// Token: 0x060019ED RID: 6637 RVA: 0x00072DD8 File Offset: 0x00071DD8
			internal virtual ah[] b()
			{
				if (this.c() && this.e != null)
				{
					MemoryStream memoryStream = new MemoryStream(this.d);
					cm a_ = new cm(memoryStream, this.d);
					this.e.a(new fi(a_, this.b, this.c, this.d));
					this.a = ah.a(this.f, memoryStream.ToArray(), this.d);
				}
				return this.a;
			}

			// Token: 0x060019EE RID: 6638 RVA: 0x00072E54 File Offset: 0x00071E54
			internal virtual void a(Stream A_0)
			{
				if (this.c())
				{
					if (this.e != null)
					{
						cm cm = new cm(A_0, this.d);
						this.e.a(new fi(cm, this.b, this.c, this.d));
						cm.a(this.a() * 512, ah.a());
						return;
					}
					for (int i = 0; i < this.a.Length; i++)
					{
						this.a[i].a3(A_0);
					}
				}
			}

			// Token: 0x060019EF RID: 6639 RVA: 0x00072EDC File Offset: 0x00071EDC
			internal virtual int a()
			{
				int result = 0;
				if (!this.c())
				{
					return result;
				}
				if (this.e != null)
				{
					return (this.d + 512 - 1) / 512;
				}
				return this.a.Length;
			}

			// Token: 0x04001299 RID: 4761
			private ah[] a;

			// Token: 0x0400129A RID: 4762
			private db b;

			// Token: 0x0400129B RID: 4763
			private string c;

			// Token: 0x0400129C RID: 4764
			private int d;

			// Token: 0x0400129D RID: 4765
			private dn e;

			// Token: 0x0400129E RID: 4766
			private y f;
		}
	}
}
