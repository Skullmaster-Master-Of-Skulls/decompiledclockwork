using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FA4 RID: 4004
	[ToolboxItem(false)]
	public class RadTagCloudItem : StateManager
	{
		// Token: 0x170030A4 RID: 12452
		// (get) Token: 0x060099B3 RID: 39347 RVA: 0x00224DB3 File Offset: 0x00222FB3
		// (set) Token: 0x060099B4 RID: 39348 RVA: 0x00224DD3 File Offset: 0x00222FD3
		[Description("Gets or sets the access key of the TagCloud item.")]
		[DefaultValue("")]
		[Category("Accessibility")]
		public string AccessKey
		{
			get
			{
				return (base.ViewState["AccessKey"] as string) ?? "";
			}
			set
			{
				base.ViewState["AccessKey"] = value;
			}
		}

		// Token: 0x170030A5 RID: 12453
		// (get) Token: 0x060099B5 RID: 39349 RVA: 0x00224DE6 File Offset: 0x00222FE6
		// (set) Token: 0x060099B6 RID: 39350 RVA: 0x00224DEE File Offset: 0x00222FEE
		[Browsable(false)]
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		// Token: 0x170030A6 RID: 12454
		// (get) Token: 0x060099B7 RID: 39351 RVA: 0x00224DF7 File Offset: 0x00222FF7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal int Index
		{
			get
			{
				return this.Container.Items.IndexOf(this);
			}
		}

		// Token: 0x170030A7 RID: 12455
		// (get) Token: 0x060099B8 RID: 39352 RVA: 0x00224E0A File Offset: 0x0022300A
		// (set) Token: 0x060099B9 RID: 39353 RVA: 0x00224E2A File Offset: 0x0022302A
		[UrlProperty]
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("The URL of the TagCloud item.")]
		public string NavigateUrl
		{
			get
			{
				return (base.ViewState["NavigateUrl"] as string) ?? "";
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x170030A8 RID: 12456
		// (get) Token: 0x060099BA RID: 39354 RVA: 0x00224E3D File Offset: 0x0022303D
		// (set) Token: 0x060099BB RID: 39355 RVA: 0x00224E5E File Offset: 0x0022305E
		[Category("Accessibility")]
		[DefaultValue(typeof(short), "0")]
		[Description("Gets or sets the TabIndex of the tagCloud item.")]
		public short TabIndex
		{
			get
			{
				return (short)(base.ViewState["TabIndex"] ?? 0);
			}
			set
			{
				base.ViewState["TabIndex"] = value;
			}
		}

		// Token: 0x170030A9 RID: 12457
		// (get) Token: 0x060099BC RID: 39356 RVA: 0x00224E76 File Offset: 0x00223076
		// (set) Token: 0x060099BD RID: 39357 RVA: 0x00224E96 File Offset: 0x00223096
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("The text of the TagCloud item")]
		public string Text
		{
			get
			{
				return (base.ViewState["Text"] as string) ?? "";
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x170030AA RID: 12458
		// (get) Token: 0x060099BE RID: 39358 RVA: 0x00224EA9 File Offset: 0x002230A9
		// (set) Token: 0x060099BF RID: 39359 RVA: 0x00224EC9 File Offset: 0x002230C9
		[Description("The value of the TagCloud item")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string Value
		{
			get
			{
				return (base.ViewState["Value"] as string) ?? "";
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x170030AB RID: 12459
		// (get) Token: 0x060099C0 RID: 39360 RVA: 0x00224EDC File Offset: 0x002230DC
		// (set) Token: 0x060099C1 RID: 39361 RVA: 0x00224EFC File Offset: 0x002230FC
		[Description("The ToolTip of the TagCloud item.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				return (base.ViewState["ToolTip"] as string) ?? "";
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x170030AC RID: 12460
		// (get) Token: 0x060099C2 RID: 39362 RVA: 0x00224F0F File Offset: 0x0022310F
		// (set) Token: 0x060099C3 RID: 39363 RVA: 0x00224F38 File Offset: 0x00223138
		[Category("Behavior")]
		[DefaultValue(0.0)]
		[Description("The weight of the TagCloud item")]
		public double Weight
		{
			get
			{
				return (double)(base.ViewState["Weight"] ?? 0.0);
			}
			set
			{
				base.ViewState["Weight"] = value;
			}
		}

		// Token: 0x060099C4 RID: 39364 RVA: 0x00224F50 File Offset: 0x00223150
		public RadTagCloudItem()
		{
		}

		// Token: 0x060099C5 RID: 39365 RVA: 0x00224F58 File Offset: 0x00223158
		public RadTagCloudItem(object dataItem) : this()
		{
			this.DataItem = dataItem;
		}

		// Token: 0x060099C6 RID: 39366 RVA: 0x00224F67 File Offset: 0x00223167
		public RadTagCloudItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x060099C7 RID: 39367 RVA: 0x00224F76 File Offset: 0x00223176
		public RadTagCloudItem(string text, double weight) : this()
		{
			this.Text = text;
			this.Weight = weight;
		}

		// Token: 0x060099C8 RID: 39368 RVA: 0x00224F8C File Offset: 0x0022318C
		public RadTagCloudItem(string text, double weight, string navigateUrl) : this()
		{
			this.Text = text;
			this.Weight = weight;
			this.NavigateUrl = navigateUrl;
		}

		// Token: 0x060099C9 RID: 39369 RVA: 0x00224FA9 File Offset: 0x002231A9
		public RadTagCloudItem(string text, double weight, string navigateUrl, string toolTip) : this()
		{
			this.Text = text;
			this.Weight = weight;
			this.NavigateUrl = navigateUrl;
			this.ToolTip = toolTip;
		}

		// Token: 0x170030AD RID: 12461
		// (get) Token: 0x060099CA RID: 39370 RVA: 0x00224FCE File Offset: 0x002231CE
		// (set) Token: 0x060099CB RID: 39371 RVA: 0x00224FD6 File Offset: 0x002231D6
		internal RadTagCloud Container
		{
			get
			{
				return this._container;
			}
			set
			{
				this._container = value;
			}
		}

		// Token: 0x060099CC RID: 39372 RVA: 0x00224FE0 File Offset: 0x002231E0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x060099CD RID: 39373 RVA: 0x00225000 File Offset: 0x00223200
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
		}

		// Token: 0x04002BA9 RID: 11177
		private object _dataItem;

		// Token: 0x04002BAA RID: 11178
		private RadTagCloud _container;
	}
}
