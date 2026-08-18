using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200126D RID: 4717
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListPopUpSettings : StateManager
	{
		// Token: 0x0600C447 RID: 50247 RVA: 0x002BF03E File Offset: 0x002BD23E
		public TreeListPopUpSettings(RadTreeList owner)
		{
			this._owner = owner;
		}

		// Token: 0x17003F3F RID: 16191
		// (get) Token: 0x0600C448 RID: 50248 RVA: 0x002BF050 File Offset: 0x002BD250
		// (set) Token: 0x0600C449 RID: 50249 RVA: 0x002BF079 File Offset: 0x002BD279
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

		// Token: 0x17003F40 RID: 16192
		// (get) Token: 0x0600C44A RID: 50250 RVA: 0x002BF094 File Offset: 0x002BD294
		// (set) Token: 0x0600C44B RID: 50251 RVA: 0x002BF0BD File Offset: 0x002BD2BD
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

		// Token: 0x17003F41 RID: 16193
		// (get) Token: 0x0600C44C RID: 50252 RVA: 0x002BF0D8 File Offset: 0x002BD2D8
		// (set) Token: 0x0600C44D RID: 50253 RVA: 0x002BF105 File Offset: 0x002BD305
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

		// Token: 0x17003F42 RID: 16194
		// (get) Token: 0x0600C44E RID: 50254 RVA: 0x002BF120 File Offset: 0x002BD320
		// (set) Token: 0x0600C44F RID: 50255 RVA: 0x002BF14D File Offset: 0x002BD34D
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue(typeof(Unit), "")]
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

		// Token: 0x17003F43 RID: 16195
		// (get) Token: 0x0600C450 RID: 50256 RVA: 0x002BF168 File Offset: 0x002BD368
		// (set) Token: 0x0600C451 RID: 50257 RVA: 0x002BF19A File Offset: 0x002BD39A
		[Description("")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "400px")]
		[Category("Client")]
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

		// Token: 0x17003F44 RID: 16196
		// (get) Token: 0x0600C452 RID: 50258 RVA: 0x002BF1B2 File Offset: 0x002BD3B2
		// (set) Token: 0x0600C453 RID: 50259 RVA: 0x002BF1DD File Offset: 0x002BD3DD
		[Description("Gets or sets the tooltip that will be displayed when you hover the close button of the popup edit form.")]
		[DefaultValue("Close")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string CloseButtonToolTip
		{
			get
			{
				return (string)(base.ViewState["CloseText"] ?? this._owner.Localization.CloseText);
			}
			set
			{
				base.ViewState["CloseText"] = value;
			}
		}

		// Token: 0x17003F45 RID: 16197
		// (get) Token: 0x0600C454 RID: 50260 RVA: 0x002BF1F0 File Offset: 0x002BD3F0
		// (set) Token: 0x0600C455 RID: 50261 RVA: 0x002BF219 File Offset: 0x002BD419
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

		// Token: 0x04003407 RID: 13319
		private RadTreeList _owner;
	}
}
