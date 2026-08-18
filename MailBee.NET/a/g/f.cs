using System;
using System.Collections;
using System.Reflection;
using MailBee;
using MailBee.DnsMX;

namespace a.g
{
	// Token: 0x02000405 RID: 1029
	[DefaultMember("Item")]
	internal class f : SortableByPriorityCollection
	{
		// Token: 0x0600242F RID: 9263 RVA: 0x00099F33 File Offset: 0x00098F33
		public f() : this(false)
		{
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00099F3C File Offset: 0x00098F3C
		public f(bool A_0)
		{
			this.b = DateTime.Now;
			this.a = global::a.g.b.a;
			this.c = A_0;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x00099F5D File Offset: 0x00098F5D
		public static f a()
		{
			return new f
			{
				d = true
			};
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x00099F6B File Offset: 0x00098F6B
		public m b(int A_0)
		{
			return (m)base.List[A_0];
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00099F7E File Offset: 0x00098F7E
		public void a(int A_0, m A_1)
		{
			base.List[A_0] = A_1;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x00099F8D File Offset: 0x00098F8D
		public void a(m A_0)
		{
			base.List.Add(A_0);
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x00099F9C File Offset: 0x00098F9C
		public void a(int A_0)
		{
			base.List.RemoveAt(A_0);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00099FAA File Offset: 0x00098FAA
		internal b e()
		{
			return this.a;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00099FB2 File Offset: 0x00098FB2
		internal void a(b A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00099FBB File Offset: 0x00098FBB
		public bool f()
		{
			return this.b.AddMinutes((double)DnsCache.Timeout) < DateTime.Now;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00099FD8 File Offset: 0x00098FD8
		public bool d()
		{
			return this.c;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00099FE0 File Offset: 0x00098FE0
		public bool b()
		{
			return this.d;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00099FE8 File Offset: 0x00098FE8
		public bool c()
		{
			if (base.List.Count > 0)
			{
				using (IEnumerator enumerator = base.List.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((m)enumerator.Current).e())
						{
							return true;
						}
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x0009A058 File Offset: 0x00099058
		public void g()
		{
			foreach (object obj in base.List)
			{
				((m)obj).d();
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x0009A0B0 File Offset: 0x000990B0
		protected override void OnValidate(object value)
		{
		}

		// Token: 0x0400180F RID: 6159
		private b a;

		// Token: 0x04001810 RID: 6160
		private DateTime b;

		// Token: 0x04001811 RID: 6161
		private new bool c;

		// Token: 0x04001812 RID: 6162
		private bool d;
	}
}
