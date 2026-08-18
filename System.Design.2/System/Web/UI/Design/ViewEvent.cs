using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000081 RID: 129
	public sealed class ViewEvent
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0000362F File Offset: 0x0000182F
		private ViewEvent()
		{
		}

		// Token: 0x040001A4 RID: 420
		public static readonly ViewEvent Click = new ViewEvent();

		// Token: 0x040001A5 RID: 421
		public static readonly ViewEvent Paint = new ViewEvent();

		// Token: 0x040001A6 RID: 422
		public static readonly ViewEvent TemplateModeChanged = new ViewEvent();
	}
}
