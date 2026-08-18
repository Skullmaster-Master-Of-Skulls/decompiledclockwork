using System;

namespace System.Web.Security
{
	// Token: 0x020005CF RID: 1487
	[Serializable]
	internal class AnonymousIdData
	{
		// Token: 0x06004B5C RID: 19292 RVA: 0x000FFB95 File Offset: 0x000FDD95
		internal AnonymousIdData(string id, DateTime dt)
		{
			this.ExpireDate = dt;
			this.AnonymousId = ((dt > DateTime.UtcNow) ? id : null);
		}

		// Token: 0x040028A4 RID: 10404
		internal string AnonymousId;

		// Token: 0x040028A5 RID: 10405
		internal DateTime ExpireDate;
	}
}
