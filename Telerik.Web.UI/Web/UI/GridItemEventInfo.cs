using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001128 RID: 4392
	public abstract class GridItemEventInfo
	{
		// Token: 0x170039F5 RID: 14837
		// (get) Token: 0x0600B361 RID: 45921 RVA: 0x0027165B File Offset: 0x0026F85B
		public virtual string EventName
		{
			get
			{
				return base.GetType().Name;
			}
		}
	}
}
