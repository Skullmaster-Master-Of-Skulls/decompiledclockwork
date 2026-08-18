using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Security
{
	// Token: 0x0200031F RID: 799
	internal class SessionActionFilter : HeaderFilter
	{
		// Token: 0x06001BE2 RID: 7138 RVA: 0x000690A9 File Offset: 0x000672A9
		public SessionActionFilter(SecurityStandardsManager standardsManager, params string[] actions)
		{
			this.actions = actions;
			this.standardsManager = standardsManager;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000690C0 File Offset: 0x000672C0
		public override bool Match(Message message)
		{
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (message.Headers.Action == this.actions[i])
				{
					return this.standardsManager.DoesMessageContainSecurityHeader(message);
				}
			}
			return false;
		}

		// Token: 0x04001DAE RID: 7598
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001DAF RID: 7599
		private string[] actions;
	}
}
