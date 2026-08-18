using System;
using System.Collections.Generic;
using System.Configuration.Provider;

namespace Telerik.Web.UI
{
	// Token: 0x02000E6F RID: 3695
	public abstract class TimeZoneProviderBase : ProviderBase
	{
		// Token: 0x17002C52 RID: 11346
		// (get) Token: 0x06008C2B RID: 35883 RVA: 0x001FD148 File Offset: 0x001FB348
		// (set) Token: 0x06008C2C RID: 35884 RVA: 0x001FD150 File Offset: 0x001FB350
		public ITimeZoneModel OperationTimeZone { get; set; }

		// Token: 0x06008C2D RID: 35885
		public abstract DateTime LocalToUtc(DateTime local);

		// Token: 0x06008C2E RID: 35886
		public abstract DateTime UtcToLocal(DateTime utc);

		// Token: 0x06008C2F RID: 35887
		public abstract List<TimeZoneNamePair> GetAllTimeZones();

		// Token: 0x04002762 RID: 10082
		public const string TimeZoneIdKey = "timeZoneId";
	}
}
