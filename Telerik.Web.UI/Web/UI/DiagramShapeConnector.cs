using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000244 RID: 580
	public class DiagramShapeConnector : StateManager
	{
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00049653 File Offset: 0x00047853
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x00049673 File Offset: 0x00047873
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? "");
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00049686 File Offset: 0x00047886
		// (set) Token: 0x06001568 RID: 5480 RVA: 0x000496A6 File Offset: 0x000478A6
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return (string)(base.ViewState["Description"] ?? "");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x000496B9 File Offset: 0x000478B9
		// (set) Token: 0x0600156A RID: 5482 RVA: 0x000496D9 File Offset: 0x000478D9
		[DefaultValue("")]
		public string Position
		{
			get
			{
				return (string)(base.ViewState["Position"] ?? "");
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}
	}
}
