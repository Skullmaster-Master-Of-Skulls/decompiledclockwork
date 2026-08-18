using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200056F RID: 1391
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadLightBoxItem : StateManager, INamingContainer
	{
		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x000A3E40 File Offset: 0x000A2040
		internal bool HasTemplates
		{
			get
			{
				return this.ItemTemplate != null || this.DescriptionTemplate != null;
			}
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000A3E58 File Offset: 0x000A2058
		protected override void TrackViewState()
		{
			if (this.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000A3E70 File Offset: 0x000A2070
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int num = 0;
				base.LoadViewState(array[num++]);
			}
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000A3E98 File Offset: 0x000A2098
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x000A3ED0 File Offset: 0x000A20D0
		// (set) Token: 0x060031F8 RID: 12792 RVA: 0x000A3EFD File Offset: 0x000A20FD
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		public string ImageUrl
		{
			get
			{
				object obj = base.ViewState["ImageUrl"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000A3F10 File Offset: 0x000A2110
		// (set) Token: 0x060031FA RID: 12794 RVA: 0x000A3F3D File Offset: 0x000A213D
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		public string NavigateUrl
		{
			get
			{
				object obj = base.ViewState["NavigateUrl"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x000A3F50 File Offset: 0x000A2150
		// (set) Token: 0x060031FC RID: 12796 RVA: 0x000A3F7D File Offset: 0x000A217D
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string Title
		{
			get
			{
				object obj = base.ViewState["Title"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x000A3F90 File Offset: 0x000A2190
		// (set) Token: 0x060031FE RID: 12798 RVA: 0x000A3FBD File Offset: 0x000A21BD
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x000A3FD0 File Offset: 0x000A21D0
		// (set) Token: 0x06003200 RID: 12800 RVA: 0x000A3FFD File Offset: 0x000A21FD
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string TargetControlID
		{
			get
			{
				object obj = base.ViewState["TargetControlID"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["TargetControlID"] = value;
			}
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x000A4010 File Offset: 0x000A2210
		// (set) Token: 0x06003202 RID: 12802 RVA: 0x000A4048 File Offset: 0x000A2248
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool IsClientID
		{
			get
			{
				return base.ViewState["IsClientID"] != null && (bool)base.ViewState["IsClientID"];
			}
			set
			{
				base.ViewState["IsClientID"] = value;
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000A4060 File Offset: 0x000A2260
		// (set) Token: 0x06003204 RID: 12804 RVA: 0x000A408F File Offset: 0x000A228F
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		public Unit Width
		{
			get
			{
				if (base.ViewState["Width"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["Width"];
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000A40A7 File Offset: 0x000A22A7
		// (set) Token: 0x06003206 RID: 12806 RVA: 0x000A40D6 File Offset: 0x000A22D6
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		public Unit Height
		{
			get
			{
				if (base.ViewState["Height"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["Height"];
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000A40EE File Offset: 0x000A22EE
		// (set) Token: 0x06003208 RID: 12808 RVA: 0x000A40F6 File Offset: 0x000A22F6
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadLightBoxItem))]
		public ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
			}
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000A40FF File Offset: 0x000A22FF
		// (set) Token: 0x0600320A RID: 12810 RVA: 0x000A4107 File Offset: 0x000A2307
		[TemplateContainer(typeof(RadLightBoxItem))]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public ITemplate DescriptionTemplate
		{
			get
			{
				return this.descriptionTemplate;
			}
			set
			{
				this.descriptionTemplate = value;
			}
		}

		// Token: 0x04000DB9 RID: 3513
		private ITemplate itemTemplate;

		// Token: 0x04000DBA RID: 3514
		private ITemplate descriptionTemplate;
	}
}
