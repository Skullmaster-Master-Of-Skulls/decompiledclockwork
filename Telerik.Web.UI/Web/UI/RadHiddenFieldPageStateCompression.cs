using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001835 RID: 6197
	public class RadHiddenFieldPageStateCompression : RadCompression
	{
		// Token: 0x0600F0DE RID: 61662 RVA: 0x0036BBCD File Offset: 0x00369DCD
		public override PageStatePersister GetStatePersister()
		{
			if (this.IsStateCompressionEnabled())
			{
				return new RadHiddenFieldPageStatePersister(base.Page);
			}
			return base.GetStatePersister();
		}
	}
}
