using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace a.k
{
	// Token: 0x02000133 RID: 307
	internal class a : XmlResolver
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x0002D450 File Offset: 0x0002C450
		public a(byte[] A_0)
		{
			this.f = new Hashtable();
			this.g = A_0;
			int i = 0;
			int num = A_0.Length;
			while (i < A_0.Length)
			{
				string text = null;
				int num2 = w.b(A_0, i, num - i, global::a.k.a.a);
				if (num2 > -1)
				{
					i = num2 + global::a.k.a.a.Length;
					if (i < num)
					{
						num2 = w.b(A_0, i, num - i, global::a.k.a.b);
						if (num2 > -1)
						{
							text = Encoding.UTF8.GetString(A_0, i, num2 - i);
							i = num2 + global::a.k.a.b.Length;
						}
					}
				}
				if (text == null || text == string.Empty)
				{
					break;
				}
				i = w.b(A_0, i, num - i, global::a.k.a.c);
				if (i > -1)
				{
					i += global::a.k.a.c.Length;
					if (i < num)
					{
						num2 = w.b(A_0, i, num - i, global::a.k.a.d);
						if (num2 <= i)
						{
							break;
						}
						this.f.Add(text, new global::a.k.a.a(i, num2 - i));
						i = num2 + global::a.k.a.d.Length;
					}
				}
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002D544 File Offset: 0x0002C544
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			string text = absoluteUri.AbsoluteUri;
			int num = text.IndexOf("BounceDatabase");
			if (num > -1)
			{
				num += "BounceDatabase".Length;
				while (num < text.Length && (text[num] == '\\' || text[num] == '/'))
				{
					num++;
				}
				if (num < text.Length)
				{
					text = text.Substring(num);
					global::a.k.a.a a = (global::a.k.a.a)this.f[text];
					if (a != null)
					{
						return new MemoryStream(this.g, a.a, a.b);
					}
				}
			}
			return null;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002D5D9 File Offset: 0x0002C5D9
		public override void set_Credentials(ICredentials value)
		{
		}

		// Token: 0x040007AF RID: 1967
		private static readonly byte[] a = Encoding.ASCII.GetBytes("~*{");

		// Token: 0x040007B0 RID: 1968
		private static readonly byte[] b = Encoding.ASCII.GetBytes("}*~");

		// Token: 0x040007B1 RID: 1969
		private static readonly byte[] c = Encoding.ASCII.GetBytes("~*{BEGIN}*~\r\n");

		// Token: 0x040007B2 RID: 1970
		private static readonly byte[] d = Encoding.ASCII.GetBytes("~*{END}*~");

		// Token: 0x040007B3 RID: 1971
		public const string e = "BounceDatabase";

		// Token: 0x040007B4 RID: 1972
		private Hashtable f;

		// Token: 0x040007B5 RID: 1973
		private byte[] g;

		// Token: 0x02000134 RID: 308
		private class a
		{
			// Token: 0x060009C3 RID: 2499 RVA: 0x0002D639 File Offset: 0x0002C639
			public a(int A_0, int A_1)
			{
				this.a = A_0;
				this.b = A_1;
			}

			// Token: 0x040007B6 RID: 1974
			public int a;

			// Token: 0x040007B7 RID: 1975
			public int b;
		}
	}
}
