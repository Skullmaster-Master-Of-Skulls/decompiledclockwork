using System;
using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.Util
{
	// Token: 0x0200000C RID: 12
	public sealed class StandardResponse<InnerType>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000022DC File Offset: 0x000004DC
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000022E4 File Offset: 0x000004E4
		[JsonProperty("data")]
		public InnerType Data { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000022ED File Offset: 0x000004ED
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000022F5 File Offset: 0x000004F5
		[JsonProperty("error")]
		public RequestError Error { get; set; }
	}
}
