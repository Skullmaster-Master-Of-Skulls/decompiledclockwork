using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000B7 RID: 183
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(int[]))]
	[KnownType(typeof(bool))]
	[KnownType(typeof(string))]
	[KnownType(typeof(TimeSpan))]
	[KnownType(typeof(Color))]
	[KnownType(typeof(DateTime))]
	[KnownType(typeof(int))]
	[KnownType(typeof(Image))]
	[KnownType(typeof(Bitmap))]
	public class AppSettingDTO
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x000024C3 File Offset: 0x000006C3
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x000024CB File Offset: 0x000006CB
		[DataMember]
		public LookupSettingDTO LookupSetting { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000024D4 File Offset: 0x000006D4
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x000024DC File Offset: 0x000006DC
		[DataMember]
		public object Value { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000024E5 File Offset: 0x000006E5
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x000024ED File Offset: 0x000006ED
		[DataMember]
		public string UserComment { get; set; }
	}
}
