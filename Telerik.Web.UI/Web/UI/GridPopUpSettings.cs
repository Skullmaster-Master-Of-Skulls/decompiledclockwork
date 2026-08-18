using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010CE RID: 4302
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridPopUpSettings : ObjectWithState
	{
		// Token: 0x0600AFA1 RID: 44961 RVA: 0x002610C4 File Offset: 0x0025F2C4
		public GridPopUpSettings(StateBag OwnerStateBag) : base("cs_popups_", OwnerStateBag)
		{
		}

		// Token: 0x170038BD RID: 14525
		// (get) Token: 0x0600AFA2 RID: 44962 RVA: 0x002610D4 File Offset: 0x0025F2D4
		// (set) Token: 0x0600AFA3 RID: 44963 RVA: 0x002610FD File Offset: 0x0025F2FD
		[DefaultValue(typeof(ScrollBars), "None")]
		[NotifyParentProperty(true)]
		public virtual ScrollBars ScrollBars
		{
			get
			{
				object obj = base.ViewState["ScrollBars"];
				if (obj != null)
				{
					return (ScrollBars)obj;
				}
				return ScrollBars.None;
			}
			set
			{
				base.ViewState["ScrollBars"] = value;
			}
		}

		// Token: 0x170038BE RID: 14526
		// (get) Token: 0x0600AFA4 RID: 44964 RVA: 0x00261118 File Offset: 0x0025F318
		// (set) Token: 0x0600AFA5 RID: 44965 RVA: 0x00261141 File Offset: 0x0025F341
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool Modal
		{
			get
			{
				object obj = base.ViewState["Modal"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["Modal"] = value;
			}
		}

		// Token: 0x170038BF RID: 14527
		// (get) Token: 0x0600AFA6 RID: 44966 RVA: 0x0026115C File Offset: 0x0025F35C
		// (set) Token: 0x0600AFA7 RID: 44967 RVA: 0x00261189 File Offset: 0x0025F389
		[NotifyParentProperty(true)]
		[DefaultValue(2500)]
		public virtual int ZIndex
		{
			get
			{
				object obj = base.ViewState["ZIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 2500;
			}
			set
			{
				base.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x170038C0 RID: 14528
		// (get) Token: 0x0600AFA8 RID: 44968 RVA: 0x002611A4 File Offset: 0x0025F3A4
		// (set) Token: 0x0600AFA9 RID: 44969 RVA: 0x002611D1 File Offset: 0x0025F3D1
		[Description("")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Category("Client")]
		public virtual Unit Height
		{
			get
			{
				object obj = base.ViewState["Height"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x170038C1 RID: 14529
		// (get) Token: 0x0600AFAA RID: 44970 RVA: 0x002611EC File Offset: 0x0025F3EC
		// (set) Token: 0x0600AFAB RID: 44971 RVA: 0x0026121E File Offset: 0x0025F41E
		[Description("")]
		[DefaultValue(typeof(Unit), "400px")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual Unit Width
		{
			get
			{
				object obj = base.ViewState["Width"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Pixel(400);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x170038C2 RID: 14530
		// (get) Token: 0x0600AFAC RID: 44972 RVA: 0x00261236 File Offset: 0x0025F436
		// (set) Token: 0x0600AFAD RID: 44973 RVA: 0x00261256 File Offset: 0x0025F456
		[NotifyParentProperty(true)]
		[Description("Gets or sets the tooltip that will be displayed when you hover the close button of the popup edit form.")]
		[DefaultValue("Close")]
		[Localizable(true)]
		public string CloseButtonToolTip
		{
			get
			{
				return (string)(base.ViewState["CloseText"] ?? "Close");
			}
			set
			{
				base.ViewState["CloseText"] = value;
			}
		}

		// Token: 0x170038C3 RID: 14531
		// (get) Token: 0x0600AFAE RID: 44974 RVA: 0x0026126C File Offset: 0x0025F46C
		// (set) Token: 0x0600AFAF RID: 44975 RVA: 0x00261295 File Offset: 0x0025F495
		[Description("Gets or sets a value indicating whether the caption text is shown in the edit form.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool ShowCaptionInEditForm
		{
			get
			{
				object obj = base.ViewState["ShowCaptionInEditForm"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowCaptionInEditForm"] = value;
			}
		}

		// Token: 0x170038C4 RID: 14532
		// (get) Token: 0x0600AFB0 RID: 44976 RVA: 0x002612B0 File Offset: 0x0025F4B0
		// (set) Token: 0x0600AFB1 RID: 44977 RVA: 0x002612D9 File Offset: 0x0025F4D9
		[DefaultValue(GridPopupPostion.Center)]
		[Description("Gets or sets a value determining the way the popup will be displayed if it can not be accommodated inside the visible viewport.")]
		public GridPopupPostion OverflowPosition
		{
			get
			{
				object obj = base.ViewState["OverflowPosition"];
				if (obj != null)
				{
					return (GridPopupPostion)obj;
				}
				return GridPopupPostion.Center;
			}
			set
			{
				base.ViewState["OverflowPosition"] = value;
			}
		}

		// Token: 0x170038C5 RID: 14533
		// (get) Token: 0x0600AFB2 RID: 44978 RVA: 0x002612F4 File Offset: 0x0025F4F4
		// (set) Token: 0x0600AFB3 RID: 44979 RVA: 0x0026131D File Offset: 0x0025F51D
		[Description("Gets or sets a value indicating whether the popup editor will be displayed in the visible viewport of the browser window.")]
		[DefaultValue(false)]
		public bool KeepInScreenBounds
		{
			get
			{
				object obj = base.ViewState["KeepInScreenBounds"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["KeepInScreenBounds"] = value;
			}
		}

		// Token: 0x0600AFB4 RID: 44980 RVA: 0x00261335 File Offset: 0x0025F535
		internal void SetLocalizedStrings(GridStrings localizedStrings)
		{
			this.CloseButtonToolTip = localizedStrings.CloseText;
		}
	}
}
