using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000558 RID: 1368
	internal class EndpointFilterProvider
	{
		// Token: 0x06003560 RID: 13664 RVA: 0x000CF8C5 File Offset: 0x000CDAC5
		public EndpointFilterProvider(params string[] initiatingActions)
		{
			this.mutex = new object();
			this.initiatingActions = new SynchronizedCollection<string>(this.mutex, initiatingActions);
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x000CF8EA File Offset: 0x000CDAEA
		public SynchronizedCollection<string> InitiatingActions
		{
			get
			{
				return this.initiatingActions;
			}
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000CF8F4 File Offset: 0x000CDAF4
		public MessageFilter CreateFilter(out int priority)
		{
			object obj = this.mutex;
			MessageFilter result;
			lock (obj)
			{
				priority = 1;
				if (this.initiatingActions.Count == 0)
				{
					result = new MatchNoneMessageFilter();
				}
				else
				{
					string[] array = new string[this.initiatingActions.Count];
					int num = 0;
					for (int i = 0; i < this.initiatingActions.Count; i++)
					{
						string text = this.initiatingActions[i];
						if (text == "*")
						{
							priority = 0;
							return new MatchAllMessageFilter();
						}
						array[num] = text;
						num++;
					}
					result = new ActionMessageFilter(array);
				}
			}
			return result;
		}

		// Token: 0x04002876 RID: 10358
		private SynchronizedCollection<string> initiatingActions;

		// Token: 0x04002877 RID: 10359
		private object mutex;
	}
}
