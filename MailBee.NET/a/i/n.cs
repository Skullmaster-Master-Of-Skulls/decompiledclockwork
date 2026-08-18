using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MailBee.Mime;

namespace a.i
{
	// Token: 0x020001E4 RID: 484
	internal class n
	{
		// Token: 0x06000F87 RID: 3975 RVA: 0x0003C14E File Offset: 0x0003B14E
		public bool e()
		{
			return this.a;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0003C156 File Offset: 0x0003B156
		public void a(bool A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0003C15F File Offset: 0x0003B15F
		public string a()
		{
			return this.b;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0003C167 File Offset: 0x0003B167
		public void b(string A_0)
		{
			this.b = A_0.Trim(k.b());
			this.a = true;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003C181 File Offset: 0x0003B181
		public string c()
		{
			return this.c;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003C18C File Offset: 0x0003B18C
		public void c(string A_0)
		{
			char[] trimChars = new char[]
			{
				' '
			};
			this.c = A_0.Trim(trimChars);
			this.a = true;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003C1B9 File Offset: 0x0003B1B9
		public int b()
		{
			return this.d;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003C1C1 File Offset: 0x0003B1C1
		public void a(int A_0)
		{
			this.d = A_0;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003C1CA File Offset: 0x0003B1CA
		public Encoding d()
		{
			return this.e;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003C1D2 File Offset: 0x0003B1D2
		public void a(Encoding A_0)
		{
			this.e = A_0;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0003C1DB File Offset: 0x0003B1DB
		public n()
		{
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003C200 File Offset: 0x0003B200
		public n(string A_0, string A_1, int A_2, Encoding A_3)
		{
			this.b = A_0;
			this.c = A_1;
			this.a = true;
			this.d = A_2;
			this.e = A_3;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003C254 File Offset: 0x0003B254
		public n(string A_0, string A_1) : this(A_0, A_1, -1, null)
		{
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003C260 File Offset: 0x0003B260
		public static n a(string A_0)
		{
			return n.a(A_0, '=');
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003C26A File Offset: 0x0003B26A
		public static n a(string A_0, char A_1)
		{
			n n = k.a(A_0, A_1);
			n.a = false;
			return n;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0003C27C File Offset: 0x0003B27C
		internal string a(bool A_0, string A_1)
		{
			if (this.b != null && this.b.Length != 0 && this.c != null && this.c.Length != 0)
			{
				string text = h.a(this.c, MailTransferEncoding.QuotedPrintable, A_1, HeaderEncodingOptions.None);
				if (!A_0)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[]
					{
						this.b,
						text
					});
				}
				return string.Format(CultureInfo.InvariantCulture, "{0}=\"{1}\"", new object[]
				{
					this.b,
					text
				});
			}
			else
			{
				if (this.b != null && this.b.Length == 0)
				{
					return this.c;
				}
				if (this.c != null && this.c.Length == 0)
				{
					return this.b;
				}
				return string.Empty;
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003C348 File Offset: 0x0003B348
		internal void a(XmlWriter A_0)
		{
			A_0.WriteStartElement("HeaderParameter");
			A_0.WriteElementString("Name", this.b);
			A_0.WriteElementString("Value", this.c);
			A_0.WriteEndElement();
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003C380 File Offset: 0x0003B380
		internal static n b(XmlReader A_0)
		{
			n n = new n();
			bool flag = true;
			A_0.Read();
			do
			{
				if (!A_0.IsEmptyElement)
				{
					string name = A_0.Name;
					if (!(name == "Name"))
					{
						if (!(name == "Value"))
						{
							flag = false;
						}
						else
						{
							n.c(A_0.ReadElementContentAsString());
						}
					}
					else
					{
						n.b(A_0.ReadElementContentAsString());
					}
				}
			}
			while (flag);
			A_0.Read();
			return n;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003C3F0 File Offset: 0x0003B3F0
		internal Task b(XmlWriter A_0)
		{
			n.b b;
			b.d = this;
			b.c = A_0;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<n.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003C440 File Offset: 0x0003B440
		internal static Task<n> a(XmlReader A_0)
		{
			n.a a;
			a.c = A_0;
			a.b = AsyncTaskMethodBuilder<n>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<n> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<n.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x04000B67 RID: 2919
		private bool a;

		// Token: 0x04000B68 RID: 2920
		private string b = string.Empty;

		// Token: 0x04000B69 RID: 2921
		private string c = string.Empty;

		// Token: 0x04000B6A RID: 2922
		private int d = -1;

		// Token: 0x04000B6B RID: 2923
		private Encoding e;
	}
}
