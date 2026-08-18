using System;
using System.Globalization;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200035B RID: 859
	internal class fr : en, IDisposable
	{
		// Token: 0x06001F42 RID: 8002 RVA: 0x00085629 File Offset: 0x00084629
		public fr(string A_0) : this(A_0, new cw())
		{
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00085637 File Offset: 0x00084637
		public fr(string A_0, cw A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.b = A_0;
			this.c = A_1;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00085669 File Offset: 0x00084669
		public string e()
		{
			return this.b;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00085671 File Offset: 0x00084671
		public cw d()
		{
			return this.c;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00085679 File Offset: 0x00084679
		public virtual void Dispose()
		{
			this.a();
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00085681 File Offset: 0x00084681
		protected override void db(eq A_0)
		{
			this.c();
			this.b();
			if (this.c.i() && !string.IsNullOrEmpty(this.c.h()))
			{
				this.a(this.c.h());
			}
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x000856C0 File Offset: 0x000846C0
		protected override void jt(eq A_0, string A_1)
		{
			if (this.c.i() && !string.IsNullOrEmpty(this.c.d()))
			{
				string text = A_1;
				if (text.Length > this.c.c() && !string.IsNullOrEmpty(this.c.b()))
				{
					text = text.Substring(0, text.Length - this.c.b().Length) + this.c.b();
				}
				this.a(string.Format(CultureInfo.InvariantCulture, this.c.d(), new object[]
				{
					text,
					A_0.kz()
				}));
			}
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00085778 File Offset: 0x00084778
		protected override void ju(eq A_0, RtfVisualSpecialCharKind A_1)
		{
			if (this.c.i() && !string.IsNullOrEmpty(this.c.e()))
			{
				this.a(string.Format(CultureInfo.InvariantCulture, this.c.e(), new object[]
				{
					A_1
				}));
			}
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000857D0 File Offset: 0x000847D0
		protected override void jv(eq A_0, RtfVisualBreakKind A_1)
		{
			if (this.c.i() && !string.IsNullOrEmpty(this.c.g()))
			{
				this.a(string.Format(CultureInfo.InvariantCulture, this.c.g(), new object[]
				{
					A_1
				}));
			}
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00085828 File Offset: 0x00084828
		protected override void dc(eq A_0, de A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7, string A_8)
		{
			if (this.c.i() && !string.IsNullOrEmpty(this.c.a()))
			{
				this.a(string.Format(CultureInfo.InvariantCulture, this.c.a(), new object[]
				{
					A_1,
					A_2,
					A_3,
					A_4,
					A_5,
					A_6,
					A_7,
					A_8,
					A_8.Length / 2
				}));
			}
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x000858D3 File Offset: 0x000848D3
		protected override void kq(eq A_0)
		{
			if (this.c.i() && !string.IsNullOrEmpty(this.c.f()))
			{
				this.a(this.c.f());
			}
			this.a();
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0008590B File Offset: 0x0008490B
		private void a(string A_0)
		{
			if (this.d == null)
			{
				return;
			}
			this.d.WriteLine(A_0);
			this.d.Flush();
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00085930 File Offset: 0x00084930
		private void c()
		{
			FileInfo fileInfo = new FileInfo(this.b);
			if (!string.IsNullOrEmpty(fileInfo.DirectoryName) && !Directory.Exists(fileInfo.DirectoryName))
			{
				Directory.CreateDirectory(fileInfo.DirectoryName);
			}
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0008596F File Offset: 0x0008496F
		private void b()
		{
			if (this.d != null)
			{
				return;
			}
			this.d = new StreamWriter(this.b);
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0008598B File Offset: 0x0008498B
		private void a()
		{
			if (this.d == null)
			{
				return;
			}
			this.d.Close();
			this.d.Dispose();
			this.d = null;
		}

		// Token: 0x04001431 RID: 5169
		public const string a = ".interpreter.log";

		// Token: 0x04001432 RID: 5170
		private readonly string b;

		// Token: 0x04001433 RID: 5171
		private readonly cw c;

		// Token: 0x04001434 RID: 5172
		private StreamWriter d;
	}
}
