using System;
using System.Globalization;
using System.IO;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200039D RID: 925
	internal class ex : at, IDisposable
	{
		// Token: 0x06002154 RID: 8532 RVA: 0x00089454 File Offset: 0x00088454
		public ex(string A_0) : this(A_0, new ez())
		{
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00089462 File Offset: 0x00088462
		public ex(string A_0, ez A_1)
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

		// Token: 0x06002156 RID: 8534 RVA: 0x00089494 File Offset: 0x00088494
		public string d()
		{
			return this.b;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0008949C File Offset: 0x0008849C
		public ez e()
		{
			return this.c;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000894A4 File Offset: 0x000884A4
		public virtual void Dispose()
		{
			this.a();
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000894AC File Offset: 0x000884AC
		protected override void ey()
		{
			this.c();
			this.b();
			if (this.c.l() && !string.IsNullOrEmpty(this.c.g()))
			{
				this.b(new string[]
				{
					this.c.g()
				});
			}
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000894FE File Offset: 0x000884FE
		protected override void ez()
		{
			if (this.c.l() && !string.IsNullOrEmpty(this.c.f()))
			{
				this.b(new string[]
				{
					this.c.f()
				});
			}
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0008953C File Offset: 0x0008853C
		protected override void e0(c9 A_0)
		{
			if (this.c.l() && !string.IsNullOrEmpty(this.c.a()))
			{
				this.b(new string[]
				{
					string.Format(CultureInfo.InvariantCulture, this.c.a(), new object[]
					{
						A_0
					})
				});
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00089598 File Offset: 0x00088598
		protected override void e1(bp A_0)
		{
			if (this.c.l() && !string.IsNullOrEmpty(this.c.e()))
			{
				string text = A_0.eu();
				if (text.Length > this.c.i() && !string.IsNullOrEmpty(this.c.c()))
				{
					text = text.Substring(0, text.Length - this.c.c().Length) + this.c.c();
				}
				this.b(new string[]
				{
					string.Format(CultureInfo.InvariantCulture, this.c.e(), new object[]
					{
						text
					})
				});
			}
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00089653 File Offset: 0x00088653
		protected override void e2()
		{
			if (this.c.l() && !string.IsNullOrEmpty(this.c.j()))
			{
				this.b(new string[]
				{
					this.c.j()
				});
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0008968E File Offset: 0x0008868E
		protected override void j7()
		{
			if (this.c.l() && !string.IsNullOrEmpty(this.c.k()))
			{
				this.b(new string[]
				{
					this.c.k()
				});
			}
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000896CC File Offset: 0x000886CC
		protected override void j8(RtfException A_0)
		{
			if (this.c.l())
			{
				if (A_0 != null)
				{
					if (!string.IsNullOrEmpty(this.c.d()))
					{
						this.b(new string[]
						{
							string.Format(CultureInfo.InvariantCulture, this.c.d(), new object[]
							{
								A_0.Message
							})
						});
						return;
					}
				}
				else if (!string.IsNullOrEmpty(this.c.h()))
				{
					this.b(new string[]
					{
						this.c.h()
					});
				}
			}
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0008975C File Offset: 0x0008875C
		protected override void e3()
		{
			if (this.c.l() && !string.IsNullOrEmpty(this.c.b()))
			{
				this.b(new string[]
				{
					this.c.b()
				});
			}
			this.a();
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000897A8 File Offset: 0x000887A8
		private void b(params string[] A_0)
		{
			if (this.d == null)
			{
				return;
			}
			string value = this.a(A_0);
			this.d.WriteLine(value);
			this.d.Flush();
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000897E0 File Offset: 0x000887E0
		private string a(params string[] A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (A_0 != null)
			{
				for (int i = 0; i < base.f(); i++)
				{
					stringBuilder.Append(" ");
				}
				foreach (string value in A_0)
				{
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00089838 File Offset: 0x00088838
		private void c()
		{
			FileInfo fileInfo = new FileInfo(this.b);
			if (!string.IsNullOrEmpty(fileInfo.DirectoryName) && !Directory.Exists(fileInfo.DirectoryName))
			{
				Directory.CreateDirectory(fileInfo.DirectoryName);
			}
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00089877 File Offset: 0x00088877
		private void b()
		{
			if (this.d != null)
			{
				return;
			}
			this.d = new StreamWriter(this.b);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00089893 File Offset: 0x00088893
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

		// Token: 0x040014DF RID: 5343
		public const string a = ".parser.log";

		// Token: 0x040014E0 RID: 5344
		private readonly string b;

		// Token: 0x040014E1 RID: 5345
		private readonly ez c;

		// Token: 0x040014E2 RID: 5346
		private StreamWriter d;
	}
}
