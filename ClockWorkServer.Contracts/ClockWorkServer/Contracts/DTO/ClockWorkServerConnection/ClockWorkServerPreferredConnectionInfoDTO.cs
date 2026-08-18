using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection
{
	// Token: 0x02000885 RID: 2181
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkServerPreferredConnectionInfoDTO
	{
		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x00014E5F File Offset: 0x0001305F
		// (set) Token: 0x06002C28 RID: 11304 RVA: 0x00014E67 File Offset: 0x00013067
		[DataMember]
		public string Hostname { get; set; }

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x00014E70 File Offset: 0x00013070
		// (set) Token: 0x06002C2A RID: 11306 RVA: 0x00014E78 File Offset: 0x00013078
		[DataMember]
		public int Port { get; set; }

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x00014E81 File Offset: 0x00013081
		// (set) Token: 0x06002C2C RID: 11308 RVA: 0x00014E89 File Offset: 0x00013089
		[DataMember]
		public string ExternalHostname { get; set; }

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x00014E92 File Offset: 0x00013092
		// (set) Token: 0x06002C2E RID: 11310 RVA: 0x00014E9A File Offset: 0x0001309A
		[DataMember]
		public int ExternalPort { get; set; }

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x00014EA3 File Offset: 0x000130A3
		// (set) Token: 0x06002C30 RID: 11312 RVA: 0x00014EAB File Offset: 0x000130AB
		[DataMember]
		public string VirtualDirectory { get; set; }

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x00014EB4 File Offset: 0x000130B4
		// (set) Token: 0x06002C32 RID: 11314 RVA: 0x00014EBC File Offset: 0x000130BC
		[DataMember]
		public string IdentityDNS { get; set; }

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x00014EC5 File Offset: 0x000130C5
		// (set) Token: 0x06002C34 RID: 11316 RVA: 0x00014ECD File Offset: 0x000130CD
		[DataMember]
		public InternetInformationServicesVersionDTO IISVersion { get; set; }

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06002C35 RID: 11317 RVA: 0x00014ED6 File Offset: 0x000130D6
		// (set) Token: 0x06002C36 RID: 11318 RVA: 0x00014EDE File Offset: 0x000130DE
		[DataMember]
		public eBindingType BindingType { get; set; }

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x00014EE7 File Offset: 0x000130E7
		// (set) Token: 0x06002C38 RID: 11320 RVA: 0x00014EEF File Offset: 0x000130EF
		[DataMember]
		public CertificateInfoDTO Certificate { get; set; }
	}
}
