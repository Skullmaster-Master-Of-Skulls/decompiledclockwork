using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts;

namespace TechnoPro.ClockWorkServer.Client.Messaging.Core
{
	// Token: 0x02000005 RID: 5
	public abstract class MessageFormatter
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000267C File Offset: 0x0000087C
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002684 File Offset: 0x00000884
		public IList<Action<InstantMessage>> FormatterBuilders { get; private set; }

		// Token: 0x06000026 RID: 38 RVA: 0x0000268D File Offset: 0x0000088D
		protected MessageFormatter()
		{
			this.FormatterBuilders = new List<Action<InstantMessage>>();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026A0 File Offset: 0x000008A0
		public virtual string ApplyFormat(InstantMessage im)
		{
			foreach (Action<InstantMessage> action in this.FormatterBuilders)
			{
				action(im);
			}
			return im.Message;
		}
	}
}
