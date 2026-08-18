using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000622 RID: 1570
	public class NavigationNodeEventArguments : EventArgs
	{
		// Token: 0x06003911 RID: 14609 RVA: 0x000BBAD2 File Offset: 0x000B9CD2
		public NavigationNodeEventArguments(NavigationNode node)
		{
			this._node = node;
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06003912 RID: 14610 RVA: 0x000BBAE1 File Offset: 0x000B9CE1
		public NavigationNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x04000F3C RID: 3900
		private NavigationNode _node;
	}
}
