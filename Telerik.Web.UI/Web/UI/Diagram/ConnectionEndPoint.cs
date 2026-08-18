using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000219 RID: 537
	public class ConnectionEndPoint : StateManager, IDefaultCheck
	{
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x000456FA File Offset: 0x000438FA
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x0004571A File Offset: 0x0004391A
		[DefaultValue("")]
		public string ShapeId
		{
			get
			{
				return (string)(base.ViewState["ShapeId"] ?? "");
			}
			set
			{
				base.ViewState["ShapeId"] = value;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0004572D File Offset: 0x0004392D
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x0004574D File Offset: 0x0004394D
		[DefaultValue("")]
		public string Connector
		{
			get
			{
				return (string)(base.ViewState["Connector"] ?? "");
			}
			set
			{
				base.ViewState["Connector"] = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00045760 File Offset: 0x00043960
		public bool IsDefault
		{
			get
			{
				return this.ShapeId == "" && this.Connector == "";
			}
		}
	}
}
