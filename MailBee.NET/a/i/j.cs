using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using MailBee;

namespace a.i
{
	// Token: 0x020001E7 RID: 487
	[DefaultMember("Item")]
	internal class j : CollectionBase
	{
		// Token: 0x06000F9F RID: 3999 RVA: 0x0003C9B0 File Offset: 0x0003B9B0
		public bool a()
		{
			if (!this.a)
			{
				using (IEnumerator enumerator = base.List.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((n)enumerator.Current).e())
						{
							this.a = true;
							break;
						}
					}
				}
			}
			return this.a;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0003CA20 File Offset: 0x0003BA20
		public void a(bool A_0)
		{
			foreach (object obj in base.List)
			{
				((n)obj).a(A_0);
			}
			this.a = A_0;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0003CA88 File Offset: 0x0003BA88
		public n a(int A_0)
		{
			return (n)base.List[A_0];
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003CA9B File Offset: 0x0003BA9B
		public void b(int A_0, n A_1)
		{
			base.List[A_0] = A_1;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0003CAAC File Offset: 0x0003BAAC
		public n b(string A_0)
		{
			foreach (object obj in base.List)
			{
				n n = (n)obj;
				if (string.Compare(n.a(), A_0, true) == 0)
				{
					return n;
				}
			}
			return null;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0003CB14 File Offset: 0x0003BB14
		public int c(n A_0)
		{
			this.a = true;
			return base.List.Add(A_0);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003CB29 File Offset: 0x0003BB29
		public int e(n A_0)
		{
			return base.List.IndexOf(A_0);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003CB37 File Offset: 0x0003BB37
		public void a(int A_0, n A_1)
		{
			base.List.Insert(A_0, A_1);
			this.a = true;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003CB4D File Offset: 0x0003BB4D
		public void b(n A_0)
		{
			base.List.Remove(A_0);
			this.a = true;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003CB62 File Offset: 0x0003BB62
		public void b()
		{
			base.List.Clear();
			this.a = true;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0003CB76 File Offset: 0x0003BB76
		public bool d(n A_0)
		{
			return base.List.Contains(A_0);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003CB84 File Offset: 0x0003BB84
		public static j a(string A_0)
		{
			j j = new j();
			Match match = m.c.Match(A_0);
			while (match.Success)
			{
				j.c(n.a(match.Value));
				match = match.NextMatch();
			}
			j.a(false);
			return j;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003CBD0 File Offset: 0x0003BBD0
		private static void a(n A_0)
		{
			if (A_0.b() > -1)
			{
				string a_ = A_0.c().Substring(A_0.b());
				a_ = au.d(a_, A_0.d());
				A_0.a(-1);
				A_0.c(a_);
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003CC14 File Offset: 0x0003BC14
		public static j a(string A_0, Encoding A_1)
		{
			j j = new j();
			Match match = m.d.Match(A_0);
			Encoding encoding = Global.DefaultEncoding;
			while (match.Success)
			{
				bool flag = false;
				if (string.Compare(match.Groups["decode"].Value, "*", true) == 0)
				{
					flag = true;
				}
				string value = match.Groups["value"].Value;
				if (flag)
				{
					Match match2 = m.e.Match(value);
					if (match2.Success)
					{
						string text = match2.Groups["encoding"].Value;
						if (text != null && text.Length > 1)
						{
							text = text.Substring(0, text.Length - 1);
							encoding = bb.a(text);
						}
						value = match2.Groups["value"].Value;
						flag = true;
					}
				}
				n n = new n(match.Groups["name"].Value, value, flag ? 0 : -1, encoding);
				n n2 = j.b(n.a());
				if (n2 != null)
				{
					if (n2.b() > -1 && (!flag || encoding != n2.d()))
					{
						j.a(n2);
					}
					if (flag && n2.b() < 0)
					{
						n2.a(n2.c().Length);
					}
					n n3 = n2;
					n3.c(n3.c() + n.c());
				}
				else
				{
					j.c(n);
				}
				match = match.NextMatch();
			}
			foreach (object obj in j)
			{
				n n4 = (n)obj;
				j.a(n4);
				n4.c(h.a(n4.c(), A_1));
			}
			j.a(false);
			return j;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003CE04 File Offset: 0x0003BE04
		public void a(XmlWriter A_0)
		{
			A_0.WriteStartElement("HeaderParameters");
			foreach (object obj in base.List)
			{
				((n)obj).a(A_0);
			}
			A_0.WriteEndElement();
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0003CE6C File Offset: 0x0003BE6C
		public static j b(XmlReader A_0)
		{
			j j = new j();
			A_0.Read();
			while (A_0.Name == "HeaderParameter")
			{
				j.c(n.b(A_0));
			}
			A_0.Read();
			return j;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003CEB0 File Offset: 0x0003BEB0
		public Task b(XmlWriter A_0)
		{
			j.b b;
			b.d = this;
			b.c = A_0;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder b2 = b.b;
			b2.Start<j.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0003CF00 File Offset: 0x0003BF00
		public static Task<j> a(XmlReader A_0)
		{
			j.a a;
			a.c = A_0;
			a.b = AsyncTaskMethodBuilder<j>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<j> b = a.b;
			b.Start<j.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x04000B79 RID: 2937
		private bool a;
	}
}
