using System;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000027 RID: 39
	public class SmtpSettings
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00029F74 File Offset: 0x00028174
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00029F8C File Offset: 0x0002818C
		public bool UseSsl
		{
			get
			{
				return this.useSsl;
			}
			set
			{
				this.useSsl = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00029F98 File Offset: 0x00028198
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00029FB0 File Offset: 0x000281B0
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00029FC8 File Offset: 0x000281C8
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00029FE0 File Offset: 0x000281E0
		public string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00029FF8 File Offset: 0x000281F8
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0002A010 File Offset: 0x00028210
		public string DefaultFrom
		{
			get
			{
				return this.defaultFrom;
			}
			set
			{
				this.defaultFrom = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0002A01C File Offset: 0x0002821C
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0002A034 File Offset: 0x00028234
		public bool BodyHtml
		{
			get
			{
				return this.bodyHtml;
			}
			set
			{
				this.bodyHtml = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0002A040 File Offset: 0x00028240
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0002A058 File Offset: 0x00028258
		public bool UseDefaultEmailSoftware
		{
			get
			{
				return this.useDefaultEmailSoftware;
			}
			set
			{
				this.useDefaultEmailSoftware = value;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0002A062 File Offset: 0x00028262
		public SmtpSettings(int port, string server)
		{
			this.port = port;
			this.server = server;
			this.username = null;
			this.password = null;
			this.useSsl = false;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0002A090 File Offset: 0x00028290
		public bool RequiresCredentials()
		{
			return this.Username != null && this.Username.Trim().Length > 0;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0002A0C0 File Offset: 0x000282C0
		public SmtpSettings(int port, string server, string username, string password)
		{
			this.port = port;
			this.server = server;
			this.username = ((username == null || username.Trim().Length < 1) ? null : username);
			this.password = ((password == null || password.Trim().Length < 1) ? null : password);
			this.useSsl = false;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0002A124 File Offset: 0x00028324
		public override string ToString()
		{
			return string.Format("Server: {0}\nPort: {1}\nUse Ssl: {2}\nUsername: {3}", new object[]
			{
				this.server,
				this.port.ToString(),
				this.useSsl.ToString(),
				this.username
			});
		}

		// Token: 0x040000FB RID: 251
		private bool useSsl;

		// Token: 0x040000FC RID: 252
		private int port;

		// Token: 0x040000FD RID: 253
		private string server;

		// Token: 0x040000FE RID: 254
		private string username;

		// Token: 0x040000FF RID: 255
		private string password;

		// Token: 0x04000100 RID: 256
		private string defaultFrom;

		// Token: 0x04000101 RID: 257
		private bool bodyHtml;

		// Token: 0x04000102 RID: 258
		private bool useDefaultEmailSoftware;
	}
}
