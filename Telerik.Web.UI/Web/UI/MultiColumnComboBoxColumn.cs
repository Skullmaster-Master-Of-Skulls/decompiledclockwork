using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020005ED RID: 1517
	public class MultiColumnComboBoxColumn : StateManager
	{
		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000B6107 File Offset: 0x000B4307
		// (set) Token: 0x060036FD RID: 14077 RVA: 0x000B6127 File Offset: 0x000B4327
		[DefaultValue("")]
		public string Field
		{
			get
			{
				return (string)(base.ViewState["Field"] ?? "");
			}
			set
			{
				base.ViewState["Field"] = value;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000B613A File Offset: 0x000B433A
		// (set) Token: 0x060036FF RID: 14079 RVA: 0x000B615A File Offset: 0x000B435A
		[DefaultValue("")]
		public string Title
		{
			get
			{
				return (string)(base.ViewState["Title"] ?? "");
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000B616D File Offset: 0x000B436D
		// (set) Token: 0x06003701 RID: 14081 RVA: 0x000B618D File Offset: 0x000B438D
		[Bindable(true)]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		public string Template
		{
			get
			{
				return (string)(base.ViewState["Template"] ?? "");
			}
			set
			{
				base.ViewState["Template"] = value;
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x000B61A0 File Offset: 0x000B43A0
		// (set) Token: 0x06003703 RID: 14083 RVA: 0x000B61C0 File Offset: 0x000B43C0
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		[Bindable(true)]
		[Browsable(true)]
		public string HeaderTemplate
		{
			get
			{
				return (string)(base.ViewState["HeaderTemplate"] ?? "");
			}
			set
			{
				base.ViewState["HeaderTemplate"] = value;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000B61D3 File Offset: 0x000B43D3
		// (set) Token: 0x06003705 RID: 14085 RVA: 0x000B61F3 File Offset: 0x000B43F3
		[DefaultValue("")]
		public string Width
		{
			get
			{
				return (string)(base.ViewState["Width"] ?? "");
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}
	}
}
