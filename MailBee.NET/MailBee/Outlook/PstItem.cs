using System;
using System.Collections;
using System.IO;
using System.Text;
using a.b;
using MailBee.Mime;

namespace MailBee.Outlook
{
	// Token: 0x020005AC RID: 1452
	public class PstItem
	{
		// Token: 0x060030D9 RID: 12505 RVA: 0x000E4164 File Offset: 0x000E3164
		internal PstItem(ii A_0)
		{
			this.c = "X-Pst-";
			this.a = A_0;
			this.b["MessageClass"] = A_0.gr();
			this.b["DisplayName"] = A_0.kn();
			this.b["AddrType"] = A_0.ff();
			this.b["EmailAddress"] = A_0.fb();
			this.b["Comment"] = A_0.e8();
			this.b["CreationTime"] = A_0.ko();
			this.b["LastModificationTime"] = A_0.e9();
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x000E4238 File Offset: 0x000E3238
		public virtual PstItemType PstType
		{
			get
			{
				if (this.a is fo)
				{
					return PstItemType.Contact;
				}
				if (this.a is h5)
				{
					return PstItemType.Rss;
				}
				if (this.a is cv)
				{
					return PstItemType.Task;
				}
				if (this.a is by)
				{
					return PstItemType.Appointment;
				}
				if (this.a is fm)
				{
					return PstItemType.Activity;
				}
				if (this.a is el)
				{
					return PstItemType.DistList;
				}
				if (this.a is co)
				{
					return PstItemType.Message;
				}
				return PstItemType.Other;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060030DB RID: 12507 RVA: 0x000E42AF File Offset: 0x000E32AF
		public virtual Hashtable PstFields
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x000E42B8 File Offset: 0x000E32B8
		public string Keywords
		{
			get
			{
				int a_;
				if (this.a.u.h().TryGetValue("Keywords", out a_))
				{
					e2 e = this.a.x.b(a_);
					if (e != null)
					{
						string text = this.a(e);
						if (text != null)
						{
							return text;
						}
						return e.a();
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000E4314 File Offset: 0x000E3314
		private string a(e2 A_0)
		{
			bool flag = true;
			Encoding.Unicode.GetString(A_0.h, 0, A_0.h.Length);
			int num = p.f(A_0.h);
			int num2 = 4;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i <= num + 1; i++)
			{
				int num3;
				if (i <= num)
				{
					num3 = p.i(A_0.h, i * 4);
				}
				else
				{
					num3 = A_0.h.Length;
				}
				if (i > 1)
				{
					if (num3 > A_0.h.Length)
					{
						flag = false;
						break;
					}
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(";");
					}
					stringBuilder.Append(Encoding.Unicode.GetString(A_0.h, num2, num3 - num2));
				}
				num2 = num3;
			}
			if (flag)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000E43DC File Offset: 0x000E33DC
		public virtual MailMessage GetAsMailMessage()
		{
			MailMessage a_ = new MailMessage();
			return this.a(a_);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000E43F8 File Offset: 0x000E33F8
		internal MailMessage a(MailMessage A_0)
		{
			foreach (object obj in this.b.Keys)
			{
				string text = (string)obj;
				if (this.b[text] != null && this.b[text].ToString() != string.Empty)
				{
					A_0.Headers[this.c + text] = this.b[text].ToString().Replace("\r\n\r\n", "\r\n").Replace("\r\n\r\n", "\r\n");
				}
			}
			return A_0;
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060030E0 RID: 12512 RVA: 0x000E44C4 File Offset: 0x000E34C4
		public virtual int PstID
		{
			get
			{
				return this.a.fd().a;
			}
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000E44D8 File Offset: 0x000E34D8
		public static string MakeStringSafeForFileName(string str)
		{
			if (str == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			foreach (char c in Path.GetInvalidFileNameChars())
			{
				if (str.IndexOf(c) != -1)
				{
					str = str.Replace(c, '_');
				}
			}
			return str;
		}

		// Token: 0x04002036 RID: 8246
		internal ii a;

		// Token: 0x04002037 RID: 8247
		internal Hashtable b = new Hashtable();

		// Token: 0x04002038 RID: 8248
		internal string c;

		// Token: 0x04002039 RID: 8249
		private const string d = "Keywords";
	}
}
