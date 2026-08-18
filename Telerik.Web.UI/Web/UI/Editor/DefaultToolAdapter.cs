using System;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002DB RID: 731
	internal class DefaultToolAdapter : ToolbarAdapterBase
	{
		// Token: 0x06001968 RID: 6504 RVA: 0x000538E3 File Offset: 0x00051AE3
		public DefaultToolAdapter()
		{
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000538EB File Offset: 0x00051AEB
		public DefaultToolAdapter(RadEditor editor) : base(editor)
		{
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x000538F4 File Offset: 0x00051AF4
		public override string ClientType
		{
			get
			{
				return "Telerik.Web.UI.Editor.DefaultToolAdapter";
			}
		}
	}
}
