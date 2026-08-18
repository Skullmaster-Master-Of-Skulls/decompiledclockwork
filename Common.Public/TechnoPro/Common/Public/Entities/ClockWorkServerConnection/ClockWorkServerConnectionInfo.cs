using System;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerConnection
{
	// Token: 0x0200044F RID: 1103
	public class ClockWorkServerConnectionInfo
	{
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x0002562E File Offset: 0x0002382E
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x00025636 File Offset: 0x00023836
		public eClockWorkServerInstanceName ClockWorkServerInstanceName { get; set; }

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x0002563F File Offset: 0x0002383F
		// (set) Token: 0x06002165 RID: 8549 RVA: 0x00025647 File Offset: 0x00023847
		public string TcpHostname { get; set; }

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x00025650 File Offset: 0x00023850
		// (set) Token: 0x06002167 RID: 8551 RVA: 0x00025658 File Offset: 0x00023858
		public int TcpPort { get; set; }

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x00025661 File Offset: 0x00023861
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x00025669 File Offset: 0x00023869
		public string HttpHostname { get; set; }

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x00025672 File Offset: 0x00023872
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x0002567A File Offset: 0x0002387A
		public int HttpPort { get; set; }

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x00025683 File Offset: 0x00023883
		// (set) Token: 0x0600216D RID: 8557 RVA: 0x0002568B File Offset: 0x0002388B
		public string VirtualDirectory { get; set; }

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x00025694 File Offset: 0x00023894
		// (set) Token: 0x0600216F RID: 8559 RVA: 0x0002569C File Offset: 0x0002389C
		public string IdentityDNS { get; set; }

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x000256A5 File Offset: 0x000238A5
		// (set) Token: 0x06002171 RID: 8561 RVA: 0x000256AD File Offset: 0x000238AD
		public InternetInformationServicesVersion IISVersion { get; set; }

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x000256B6 File Offset: 0x000238B6
		// (set) Token: 0x06002173 RID: 8563 RVA: 0x000256BE File Offset: 0x000238BE
		public CertificateInfo Certificate { get; set; }

		// Token: 0x06002174 RID: 8564 RVA: 0x000256C7 File Offset: 0x000238C7
		public ClockWorkServerConnectionInfo()
		{
			this.ClockWorkServerInstanceName = eClockWorkServerInstanceName.ClockWorkServer;
		}
	}
}
