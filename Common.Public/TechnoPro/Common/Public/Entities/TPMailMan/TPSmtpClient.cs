using System;

namespace TechnoPro.Common.Public.Entities.TPMailMan
{
	// Token: 0x02000169 RID: 361
	[Serializable]
	public class TPSmtpClient : BusinessBase<string>
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000120C6 File Offset: 0x000102C6
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x000120CE File Offset: 0x000102CE
		public string Server { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x000120D7 File Offset: 0x000102D7
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x000120DF File Offset: 0x000102DF
		public int Port { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x000120E8 File Offset: 0x000102E8
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x000120F0 File Offset: 0x000102F0
		public eSslProtocol SslProtocol { get; set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x000120F9 File Offset: 0x000102F9
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x00012101 File Offset: 0x00010301
		public string Username { get; set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001210A File Offset: 0x0001030A
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00012112 File Offset: 0x00010312
		public string Password { get; set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001211C File Offset: 0x0001031C
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x00012137 File Offset: 0x00010337
		[Obsolete]
		public bool UseSsl
		{
			get
			{
				return this.SslProtocol > eSslProtocol.None;
			}
			set
			{
				this.SslProtocol = (value ? eSslProtocol.Auto : eSslProtocol.None);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00012148 File Offset: 0x00010348
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00012150 File Offset: 0x00010350
		public string AuthenticationMethods { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00012159 File Offset: 0x00010359
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00012161 File Offset: 0x00010361
		public string AuthenticationOptions { get; set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0001216A File Offset: 0x0001036A
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00012172 File Offset: 0x00010372
		public string SslStartupMode { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0001217B File Offset: 0x0001037B
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00012183 File Offset: 0x00010383
		public int ServerTimeoutSeconds { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0001218C File Offset: 0x0001038C
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00012194 File Offset: 0x00010394
		public eExtendedSmtpOptions ExtendedSmtpOptions { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0001219D File Offset: 0x0001039D
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x000121A5 File Offset: 0x000103A5
		public bool EnableNonFipsAlgorithms { get; set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x000121AE File Offset: 0x000103AE
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x000121B6 File Offset: 0x000103B6
		public string HelloDomain { get; set; }

		// Token: 0x060008C2 RID: 2242 RVA: 0x000121C0 File Offset: 0x000103C0
		public override string ToString()
		{
			return string.Format("Server={0}; Port={1}; UseSsl[Obsolete]={2}; Username={3}; Password={4}; AuthMethods={5}; AuthOptions={6}; SslStartupMode={7}; ServerTimeoutSeconds={8}; ExtendedSmtpOptions={9},EnableNonFipsAlgorithms={10},HelloDomain:{11}", new object[]
			{
				this.Server ?? "NULL",
				this.Port.ToString(),
				this.UseSsl.ToString(),
				(this.Username == null) ? "NULL" : ("Length=" + this.Username.Length.ToString()),
				(this.Password == null) ? "NULL" : ("Length=" + this.Password.Length.ToString()),
				this.AuthenticationMethods ?? "NULL",
				this.AuthenticationOptions ?? "NULL",
				this.SslStartupMode ?? "NULL",
				this.ServerTimeoutSeconds.ToString(),
				this.ExtendedSmtpOptions.ToString(),
				this.EnableNonFipsAlgorithms.ToString(),
				this.HelloDomain ?? ""
			});
		}
	}
}
