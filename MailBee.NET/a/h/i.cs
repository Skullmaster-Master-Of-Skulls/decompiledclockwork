using System;
using System.Collections;
using System.Text;
using MailBee;
using MailBee.Mime;
using MailBee.Tnef;

namespace a.h
{
	// Token: 0x020001FF RID: 511
	internal class i
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x00045F88 File Offset: 0x00044F88
		public h b()
		{
			h result = null;
			m m = m.a(this.a, 36867);
			if (m != null)
			{
				try
				{
					result = (h)m.g();
				}
				catch (MailBeeTnefParsingException)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00045FD0 File Offset: 0x00044FD0
		public ArrayList d()
		{
			return this.a;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00045FD8 File Offset: 0x00044FD8
		public void b(ArrayList A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00045FE1 File Offset: 0x00044FE1
		public ArrayList j()
		{
			return this.b;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00045FE9 File Offset: 0x00044FE9
		public void a(ArrayList A_0)
		{
			this.b = A_0;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00045FF2 File Offset: 0x00044FF2
		public i()
		{
			this.a = new ArrayList();
			this.b = new ArrayList();
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00046010 File Offset: 0x00045010
		public i(k A_0) : this()
		{
			this.a(A_0);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00046020 File Offset: 0x00045020
		private void a(k A_0)
		{
			b b = null;
			m m;
			while ((m = A_0.a()) != null)
			{
				byte b2 = m.c();
				if (b2 != 1)
				{
					if (b2 != 2)
					{
						throw new MailBeeTnefParsingException(string.Format(Resources.Instance.ErrorDesc_TnefAttributeLevelInvalid0, m.c()), 1004);
					}
					int num = m.d();
					n n;
					if (num != 32783)
					{
						if (num != 32784)
						{
							switch (num)
							{
							case 36865:
								n = (n)m.g();
								try
								{
									b.b((string)m.g());
									continue;
								}
								finally
								{
									n.Close();
									m.e();
								}
								break;
							case 36866:
								if (b != null)
								{
									this.b.Add(b);
								}
								b = new b();
								b.a(m);
								continue;
							case 36869:
								n = (n)m.g();
								try
								{
									b.a(new h(n));
									continue;
								}
								finally
								{
									n.Close();
									m.e();
								}
								goto IL_AB;
							}
							b.a(m);
							continue;
						}
						continue;
					}
					IL_AB:
					n = (n)m.g();
					b.a(n);
					m.e();
				}
				else
				{
					this.a.Add(m);
				}
			}
			if (b != null)
			{
				this.b.Add(b);
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00046198 File Offset: 0x00045198
		public m b(int A_0)
		{
			return m.a(this.a, A_0);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000461A6 File Offset: 0x000451A6
		public void a(m A_0)
		{
			this.a.Add(A_0);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000461B5 File Offset: 0x000451B5
		public void a(b A_0)
		{
			this.b.Add(A_0);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x000461C4 File Offset: 0x000451C4
		public string f()
		{
			h h = this.b();
			if (h != null)
			{
				return (string)h.a(55);
			}
			return null;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x000461EC File Offset: 0x000451EC
		public EmailAddress h()
		{
			h h = this.b();
			if (h == null)
			{
				return null;
			}
			string text = (string)h.a(3103);
			if (text == null)
			{
				return null;
			}
			EmailAddress emailAddress = new EmailAddress(text);
			string text2 = (string)h.a(3098);
			if (text2 != null)
			{
				emailAddress.DisplayName = text2;
			}
			return emailAddress;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0004623E File Offset: 0x0004523E
		public EmailAddressCollection e()
		{
			return this.a(1);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00046247 File Offset: 0x00045247
		public EmailAddressCollection g()
		{
			return this.a(2);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00046250 File Offset: 0x00045250
		public EmailAddressCollection i()
		{
			return this.a(3);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0004625C File Offset: 0x0004525C
		private EmailAddressCollection a(int A_0)
		{
			EmailAddressCollection emailAddressCollection = new EmailAddressCollection();
			m m = m.a(this.a, 36868);
			if (m == null)
			{
				return null;
			}
			if (m.g() is IEnumerable)
			{
				foreach (object obj in ((IEnumerable)m.g()))
				{
					h h = (h)obj;
					if (A_0 == (int)h.a(3093))
					{
						string text = (string)h.a(12291);
						if (text != null)
						{
							EmailAddress emailAddress = new EmailAddress(text);
							string text2 = (string)h.a(12289);
							if (text2 != null)
							{
								emailAddress.DisplayName = text2;
							}
							emailAddressCollection.Add(emailAddress);
						}
					}
				}
			}
			return emailAddressCollection;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0004633C File Offset: 0x0004533C
		public DateTime c()
		{
			h h = this.b();
			if (h != null)
			{
				return (DateTime)h.a(12295);
			}
			return DateTime.MinValue;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0004636C File Offset: 0x0004536C
		public byte[] k()
		{
			h h = this.b();
			if (h != null)
			{
				n n = (n)h.a(4105);
				if (n != null)
				{
					return global::a.h.f.a(n.d());
				}
			}
			return null;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x000463A4 File Offset: 0x000453A4
		public void a()
		{
			for (int i = 0; i < this.a.Count; i++)
			{
				((m)this.a[i]).e();
			}
			for (int j = 0; j < this.b.Count; j++)
			{
				((b)this.b[j]).e();
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0004640C File Offset: 0x0004540C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Message:");
			stringBuilder.Append("\n  Attributes:");
			for (int i = 0; i < this.a.Count; i++)
			{
				stringBuilder.Append("\n    ").Append(this.a[i]);
			}
			stringBuilder.Append("\n  Attachments:");
			for (int j = 0; j < this.b.Count; j++)
			{
				stringBuilder.Append("\n    ").Append(this.b[j]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000E39 RID: 3641
		private ArrayList a;

		// Token: 0x04000E3A RID: 3642
		private ArrayList b;
	}
}
