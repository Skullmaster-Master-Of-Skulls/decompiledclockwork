using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace System.Web.Http.Description
{
	// Token: 0x020000BA RID: 186
	public class ApiDescription
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x0000CE84 File Offset: 0x0000B084
		public ApiDescription()
		{
			this.SupportedRequestBodyFormatters = new Collection<MediaTypeFormatter>();
			this.SupportedResponseFormatters = new Collection<MediaTypeFormatter>();
			this.ParameterDescriptions = new Collection<ApiParameterDescription>();
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000CEAD File Offset: 0x0000B0AD
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0000CEB5 File Offset: 0x0000B0B5
		public HttpMethod HttpMethod { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000CEBE File Offset: 0x0000B0BE
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0000CEC6 File Offset: 0x0000B0C6
		public string RelativePath { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000CECF File Offset: 0x0000B0CF
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0000CED7 File Offset: 0x0000B0D7
		public HttpActionDescriptor ActionDescriptor { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
		public IHttpRoute Route { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000CEF1 File Offset: 0x0000B0F1
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000CEF9 File Offset: 0x0000B0F9
		public string Documentation { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000CF02 File Offset: 0x0000B102
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000CF0A File Offset: 0x0000B10A
		public Collection<MediaTypeFormatter> SupportedResponseFormatters { get; internal set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000CF13 File Offset: 0x0000B113
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000CF1B File Offset: 0x0000B11B
		public Collection<MediaTypeFormatter> SupportedRequestBodyFormatters { get; internal set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000CF24 File Offset: 0x0000B124
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public Collection<ApiParameterDescription> ParameterDescriptions { get; internal set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000CF35 File Offset: 0x0000B135
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000CF3D File Offset: 0x0000B13D
		public ResponseDescription ResponseDescription { get; internal set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000CF46 File Offset: 0x0000B146
		public string ID
		{
			get
			{
				return ((this.HttpMethod != null) ? this.HttpMethod.Method : string.Empty) + (this.RelativePath ?? string.Empty);
			}
		}
	}
}
