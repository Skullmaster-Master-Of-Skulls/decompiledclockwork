using System;
using System.Text;
using a;
using a.e;

namespace MailBee.Proxy
{
	// Token: 0x02000507 RID: 1287
	public class ProxyServer
	{
		// Token: 0x06002ABA RID: 10938 RVA: 0x000CBA04 File Offset: 0x000CAA04
		internal ProxyServer()
		{
			this.a = ProxyProtocol.NoProxy;
			this.b = string.Empty;
			this.c = 1080;
			this.d = string.Empty;
			this.e = string.Empty;
			this.f = Global.DefaultEncoding;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000CBA58 File Offset: 0x000CAA58
		internal r a()
		{
			switch (this.a)
			{
			case ProxyProtocol.NoProxy:
				return null;
			case ProxyProtocol.Socks4:
				return new global::a.e.b(this.b, this.c, this.d, this.f);
			case ProxyProtocol.Socks5:
				return new global::a.e.a(this.b, this.c, this.d, this.e, this.f);
			default:
				return new global::a.e.c(this.b, this.c, this.d, this.e, this.f);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x000CBAE8 File Offset: 0x000CAAE8
		internal string ProtocolName
		{
			get
			{
				switch (this.a)
				{
				case ProxyProtocol.NoProxy:
					return null;
				case ProxyProtocol.Socks4:
					return "SOCKS4";
				case ProxyProtocol.Socks5:
					return "SOCKS5";
				default:
					return "HTTP";
				}
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x000CBB23 File Offset: 0x000CAB23
		// (set) Token: 0x06002ABE RID: 10942 RVA: 0x000CBB2B File Offset: 0x000CAB2B
		public ProxyProtocol Protocol
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000CBB34 File Offset: 0x000CAB34
		// (set) Token: 0x06002AC0 RID: 10944 RVA: 0x000CBB3C File Offset: 0x000CAB3C
		public string Name
		{
			get
			{
				return this.b;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.b = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x000CBB50 File Offset: 0x000CAB50
		// (set) Token: 0x06002AC2 RID: 10946 RVA: 0x000CBB58 File Offset: 0x000CAB58
		public int Port
		{
			get
			{
				return this.c;
			}
			set
			{
				if (value > 65535 || value < 0)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				this.c = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x000CBB75 File Offset: 0x000CAB75
		// (set) Token: 0x06002AC4 RID: 10948 RVA: 0x000CBB7D File Offset: 0x000CAB7D
		public string AccountName
		{
			get
			{
				return this.d;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.d = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x000CBB91 File Offset: 0x000CAB91
		// (set) Token: 0x06002AC6 RID: 10950 RVA: 0x000CBB99 File Offset: 0x000CAB99
		public string Password
		{
			get
			{
				return this.e;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.e = value;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x000CBBAD File Offset: 0x000CABAD
		// (set) Token: 0x06002AC8 RID: 10952 RVA: 0x000CBBB5 File Offset: 0x000CABB5
		public Encoding StringEncoding
		{
			get
			{
				return this.f;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.f = value;
			}
		}

		// Token: 0x04001D8A RID: 7562
		private ProxyProtocol a;

		// Token: 0x04001D8B RID: 7563
		private string b;

		// Token: 0x04001D8C RID: 7564
		private int c;

		// Token: 0x04001D8D RID: 7565
		private string d;

		// Token: 0x04001D8E RID: 7566
		private string e;

		// Token: 0x04001D8F RID: 7567
		private Encoding f;
	}
}
