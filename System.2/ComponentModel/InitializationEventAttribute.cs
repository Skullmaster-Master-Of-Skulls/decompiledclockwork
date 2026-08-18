using System;

namespace System.ComponentModel
{
	// Token: 0x02000568 RID: 1384
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class InitializationEventAttribute : Attribute
	{
		// Token: 0x060033B5 RID: 13237 RVA: 0x000E4150 File Offset: 0x000E2350
		public InitializationEventAttribute(string eventName)
		{
			this.eventName = eventName;
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060033B6 RID: 13238 RVA: 0x000E415F File Offset: 0x000E235F
		public string EventName
		{
			get
			{
				return this.eventName;
			}
		}

		// Token: 0x040029C5 RID: 10693
		private string eventName;
	}
}
