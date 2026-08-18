using System;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerConnection
{
	// Token: 0x02000452 RID: 1106
	public class ClockWorkServerPreferredConnectionInfo
	{
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x0002571D File Offset: 0x0002391D
		// (set) Token: 0x0600217F RID: 8575 RVA: 0x00025725 File Offset: 0x00023925
		public string Hostname { get; set; }

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0002572E File Offset: 0x0002392E
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x00025736 File Offset: 0x00023936
		public int Port { get; set; }

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0002573F File Offset: 0x0002393F
		// (set) Token: 0x06002183 RID: 8579 RVA: 0x00025747 File Offset: 0x00023947
		public string ExternalHostname { get; set; }

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x00025750 File Offset: 0x00023950
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x00025758 File Offset: 0x00023958
		public int ExternalPort { get; set; }

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x00025761 File Offset: 0x00023961
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x00025769 File Offset: 0x00023969
		public string VirtualDirectory { get; set; }

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x00025772 File Offset: 0x00023972
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x0002577A File Offset: 0x0002397A
		public string IdentityDNS { get; set; }

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x00025783 File Offset: 0x00023983
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x0002578B File Offset: 0x0002398B
		public InternetInformationServicesVersion IISVersion { get; set; }

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x00025794 File Offset: 0x00023994
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x0002579C File Offset: 0x0002399C
		public eBindingType BindingType { get; set; }

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x000257A5 File Offset: 0x000239A5
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x000257AD File Offset: 0x000239AD
		public CertificateInfo Certificate { get; set; }
	}
}
