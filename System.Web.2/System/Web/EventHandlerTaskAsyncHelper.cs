using System;

namespace System.Web
{
	// Token: 0x0200004D RID: 77
	public sealed class EventHandlerTaskAsyncHelper
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x00007878 File Offset: 0x00005A78
		public EventHandlerTaskAsyncHelper(TaskEventHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.BeginEventHandler = ((object sender, EventArgs e, AsyncCallback cb, object extraData) => TaskAsyncHelper.BeginTask(() => handler(sender, e), cb, extraData));
			this.EndEventHandler = new EndEventHandler(TaskAsyncHelper.EndTask);
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000078CF File Offset: 0x00005ACF
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x000078D7 File Offset: 0x00005AD7
		public BeginEventHandler BeginEventHandler { get; private set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000078E0 File Offset: 0x00005AE0
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x000078E8 File Offset: 0x00005AE8
		public EndEventHandler EndEventHandler { get; private set; }
	}
}
