using System;

namespace EmailClassLibrary
{
	// Token: 0x0200000D RID: 13
	public class SmtpSettings
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003AA6 File Offset: 0x00002AA6
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003AAE File Offset: 0x00002AAE
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003AB7 File Offset: 0x00002AB7
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003ABF File Offset: 0x00002ABF
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003AC7 File Offset: 0x00002AC7
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003ACF File Offset: 0x00002ACF
		public string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003AD7 File Offset: 0x00002AD7
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003ADF File Offset: 0x00002ADF
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003AE8 File Offset: 0x00002AE8
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003AF0 File Offset: 0x00002AF0
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003AF9 File Offset: 0x00002AF9
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003B01 File Offset: 0x00002B01
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

		// Token: 0x0600005B RID: 91 RVA: 0x00003B0A File Offset: 0x00002B0A
		public SmtpSettings(int port, string server)
		{
			this.port = port;
			this.server = server;
			this.username = null;
			this.password = null;
			this.useSsl = false;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003B35 File Offset: 0x00002B35
		public bool RequiresCredentials()
		{
			return this.Username != null && this.Username.Trim().Length > 0;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003B54 File Offset: 0x00002B54
		public SmtpSettings(int port, string server, string username, string password)
		{
			this.port = port;
			this.server = server;
			this.username = ((username == null || username.Trim().Length < 1) ? null : username);
			this.password = ((password == null || password.Trim().Length < 1) ? null : password);
			this.useSsl = false;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003BB5 File Offset: 0x00002BB5
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

		// Token: 0x04000037 RID: 55
		private bool useSsl;

		// Token: 0x04000038 RID: 56
		private int port;

		// Token: 0x04000039 RID: 57
		private string server;

		// Token: 0x0400003A RID: 58
		private string username;

		// Token: 0x0400003B RID: 59
		private string password;

		// Token: 0x0400003C RID: 60
		private string defaultFrom;

		// Token: 0x0400003D RID: 61
		private bool bodyHtml;

		// Token: 0x0400003E RID: 62
		private bool useDefaultEmailSoftware;
	}
}
