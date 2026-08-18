using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A21 RID: 6689
	public class ResourceHeaderCreatedEventArgs : EventArgs
	{
		// Token: 0x17004EA1 RID: 20129
		// (get) Token: 0x060103C2 RID: 66498 RVA: 0x003A0ED8 File Offset: 0x0039F0D8
		public SchedulerResourceContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x060103C3 RID: 66499 RVA: 0x003A0EE0 File Offset: 0x0039F0E0
		public ResourceHeaderCreatedEventArgs(SchedulerResourceContainer container)
		{
			this._container = container;
		}

		// Token: 0x0400492C RID: 18732
		private readonly SchedulerResourceContainer _container;
	}
}
