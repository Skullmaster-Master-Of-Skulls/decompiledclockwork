using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001BE RID: 446
	[DataContract(Namespace = "http://tpro.ca")]
	[Serializable]
	public class TPSmtpClientDTO
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x000049DE File Offset: 0x00002BDE
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x000049E6 File Offset: 0x00002BE6
		[DataMember]
		public string Server { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x000049EF File Offset: 0x00002BEF
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x000049F7 File Offset: 0x00002BF7
		[DataMember]
		public int Port { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00004A00 File Offset: 0x00002C00
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x00004A1B File Offset: 0x00002C1B
		[DataMember]
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

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00004A2C File Offset: 0x00002C2C
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x00004A34 File Offset: 0x00002C34
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00004A3D File Offset: 0x00002C3D
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x00004A45 File Offset: 0x00002C45
		[DataMember]
		public string Password { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00004A4E File Offset: 0x00002C4E
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00004A56 File Offset: 0x00002C56
		[DataMember]
		public eSslProtocol SslProtocol { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00004A5F File Offset: 0x00002C5F
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x00004A67 File Offset: 0x00002C67
		[DataMember]
		public string AuthenticationMethods { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00004A70 File Offset: 0x00002C70
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x00004A78 File Offset: 0x00002C78
		[DataMember]
		public string AuthenticationOptions { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00004A81 File Offset: 0x00002C81
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x00004A89 File Offset: 0x00002C89
		[DataMember]
		public string SslStartupMode { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00004A92 File Offset: 0x00002C92
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x00004A9A File Offset: 0x00002C9A
		[DataMember]
		public int ServerTimeoutSeconds { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00004AA3 File Offset: 0x00002CA3
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x00004AAB File Offset: 0x00002CAB
		[DataMember]
		public eExtendedSmtpOptions ExtendedSmtpOptions { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00004AB4 File Offset: 0x00002CB4
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x00004ABC File Offset: 0x00002CBC
		[DataMember]
		public string HelloDomain { get; set; }
	}
}
