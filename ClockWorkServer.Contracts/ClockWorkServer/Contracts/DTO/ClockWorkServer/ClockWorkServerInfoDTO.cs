using System;
using System.Drawing;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServer
{
	// Token: 0x0200087F RID: 2175
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(Bitmap))]
	public class ClockWorkServerInfoDTO
	{
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x00014D70 File Offset: 0x00012F70
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x00014D88 File Offset: 0x00012F88
		[DataMember]
		public string Id
		{
			get
			{
				return this.DepartmentTitle;
			}
			set
			{
				this.DepartmentTitle = value;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x00014D93 File Offset: 0x00012F93
		// (set) Token: 0x06002C0A RID: 11274 RVA: 0x00014D9B File Offset: 0x00012F9B
		[DataMember]
		public Uri DiscoveryEnpointAddress { get; set; }

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x00014DA4 File Offset: 0x00012FA4
		// (set) Token: 0x06002C0C RID: 11276 RVA: 0x00014DAC File Offset: 0x00012FAC
		[DataMember]
		public string DepartmentTitle { get; set; }

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x00014DB5 File Offset: 0x00012FB5
		// (set) Token: 0x06002C0E RID: 11278 RVA: 0x00014DBD File Offset: 0x00012FBD
		[DataMember]
		public string DepartmentDescription { get; set; }

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x00014DC6 File Offset: 0x00012FC6
		// (set) Token: 0x06002C10 RID: 11280 RVA: 0x00014DCE File Offset: 0x00012FCE
		[DataMember]
		public string ServerVersion { get; set; }

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x00014DD7 File Offset: 0x00012FD7
		// (set) Token: 0x06002C12 RID: 11282 RVA: 0x00014DDF File Offset: 0x00012FDF
		[DataMember]
		public eBindingType PreferredBindingType { get; set; }

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x00014DE8 File Offset: 0x00012FE8
		// (set) Token: 0x06002C14 RID: 11284 RVA: 0x00014DF0 File Offset: 0x00012FF0
		[DataMember]
		public Image DepartmentLogoImage { get; set; }
	}
}
