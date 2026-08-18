using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;

namespace AjaxControlToolkit
{
	// Token: 0x02000160 RID: 352
	[RequiredScript(typeof(AnimationExtender))]
	[TargetControlType(typeof(HtmlControl))]
	[ClientScriptResource("Sys.Extended.UI.PopupBehavior", "Popup")]
	[TargetControlType(typeof(WebControl))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ToolboxItem(false)]
	[Designer(typeof(PopupExtenderDesigner))]
	public class PopupExtender : AnimationExtenderControlBase
	{
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0001828C File Offset: 0x0001648C
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x0001829E File Offset: 0x0001649E
		[RequiredProperty]
		[ExtenderControlProperty]
		[IDReferenceProperty]
		[ClientPropertyName("parentElement")]
		[ElementReference]
		public string ParentElementID
		{
			get
			{
				return base.GetPropertyValue<string>("ParentElementID", "");
			}
			set
			{
				base.SetPropertyValue<string>("ParentElementID", value);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x000182AC File Offset: 0x000164AC
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x000182BA File Offset: 0x000164BA
		[ExtenderControlProperty]
		[ClientPropertyName("x")]
		[DefaultValue(0)]
		public int X
		{
			get
			{
				return base.GetPropertyValue<int>("X", 0);
			}
			set
			{
				base.SetPropertyValue<int>("X", value);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x000182C8 File Offset: 0x000164C8
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x000182D6 File Offset: 0x000164D6
		[ExtenderControlProperty]
		[ClientPropertyName("y")]
		[DefaultValue(0)]
		public int Y
		{
			get
			{
				return base.GetPropertyValue<int>("Y", 0);
			}
			set
			{
				base.SetPropertyValue<int>("Y", value);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x000182E4 File Offset: 0x000164E4
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x000182F2 File Offset: 0x000164F2
		[ClientPropertyName("positioningMode")]
		[ExtenderControlProperty]
		[DefaultValue(PositioningMode.Absolute)]
		public PositioningMode PositioningMode
		{
			get
			{
				return base.GetPropertyValue<PositioningMode>("PositioningMode", PositioningMode.Absolute);
			}
			set
			{
				base.SetPropertyValue<PositioningMode>("PositioningMode", value);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00018300 File Offset: 0x00016500
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x0001830E File Offset: 0x0001650E
		[ExtenderControlProperty]
		[ClientPropertyName("reparent")]
		[DefaultValue(false)]
		public bool Reparent
		{
			get
			{
				return base.GetPropertyValue<bool>("Reparent", false);
			}
			set
			{
				base.SetPropertyValue<bool>("Reparent", value);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0001831C File Offset: 0x0001651C
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x0001832F File Offset: 0x0001652F
		[ExtenderControlProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientPropertyName("onShow")]
		[Browsable(false)]
		[DefaultValue(null)]
		public Animation OnShow
		{
			get
			{
				return base.GetAnimation(ref this._onShow, "OnShow");
			}
			set
			{
				base.SetAnimation(ref this._onShow, "OnShow", value);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00018343 File Offset: 0x00016543
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x00018356 File Offset: 0x00016556
		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("onHide")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Animation OnHide
		{
			get
			{
				return base.GetAnimation(ref this._onHide, "OnHide");
			}
			set
			{
				base.SetAnimation(ref this._onHide, "OnHide", value);
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001836A File Offset: 0x0001656A
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.ResolveControlIDs(this._onShow);
			base.ResolveControlIDs(this._onHide);
		}

		// Token: 0x040003AD RID: 941
		private Animation _onShow;

		// Token: 0x040003AE RID: 942
		private Animation _onHide;
	}
}
