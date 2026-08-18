using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006A6 RID: 1702
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportFormFromXmlReq : BaseMessageReq
	{
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0000FC60 File Offset: 0x0000DE60
		// (set) Token: 0x06002288 RID: 8840 RVA: 0x0000FC68 File Offset: 0x0000DE68
		[DataMember]
		public string Xml { get; set; }

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x0000FC71 File Offset: 0x0000DE71
		// (set) Token: 0x0600228A RID: 8842 RVA: 0x0000FC79 File Offset: 0x0000DE79
		[DataMember]
		public int ScreenNumToImportControlsInto { get; set; }
	}
}
