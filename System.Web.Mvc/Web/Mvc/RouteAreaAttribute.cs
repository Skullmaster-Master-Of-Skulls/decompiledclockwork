using System;

namespace System.Web.Mvc
{
	// Token: 0x02000099 RID: 153
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class RouteAreaAttribute : Attribute
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0000C4BB File Offset: 0x0000A6BB
		public RouteAreaAttribute()
		{
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000C4C3 File Offset: 0x0000A6C3
		public RouteAreaAttribute(string areaName)
		{
			this.AreaName = areaName;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000C4D2 File Offset: 0x0000A6D2
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000C4DA File Offset: 0x0000A6DA
		public string AreaName { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000C4E3 File Offset: 0x0000A6E3
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000C4EB File Offset: 0x0000A6EB
		public string AreaPrefix { get; set; }
	}
}
