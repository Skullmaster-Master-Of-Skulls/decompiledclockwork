using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001837 RID: 6199
	public class RadSessionPageStateCompression : RadCompression
	{
		// Token: 0x0600F0E5 RID: 61669 RVA: 0x0036BD55 File Offset: 0x00369F55
		public override PageStatePersister GetStatePersister()
		{
			if (this.IsStateCompressionEnabled())
			{
				return new RadSessionPageStatePersister(base.Page);
			}
			return base.GetStatePersister();
		}
	}
}
