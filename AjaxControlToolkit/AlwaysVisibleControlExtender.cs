using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200002D RID: 45
	[RequiredScript(typeof(AnimationScripts))]
	[TargetControlType(typeof(UpdateProgress))]
	[ToolboxBitmap(typeof(Accessor), "AlwaysVisibleControl.bmp")]
	[Designer(typeof(AlwaysVisibleControlExtenderDesigner))]
	[ClientScriptResource("Sys.Extended.UI.AlwaysVisibleControlBehavior", "AlwaysVisibleControl")]
	[DefaultProperty("VerticalOffset")]
	[TargetControlType(typeof(WebControl))]
	[TargetControlType(typeof(HtmlControl))]
	public class AlwaysVisibleControlExtender : ExtenderControlBase
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006387 File Offset: 0x00004587
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00006395 File Offset: 0x00004595
		[DefaultValue(0)]
		[ClientPropertyName("horizontalOffset")]
		[ExtenderControlProperty]
		public int HorizontalOffset
		{
			get
			{
				return base.GetPropertyValue<int>("HorizontalOffset", 0);
			}
			set
			{
				base.SetPropertyValue<int>("HorizontalOffset", value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000063A3 File Offset: 0x000045A3
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000063B1 File Offset: 0x000045B1
		[ClientPropertyName("horizontalSide")]
		[ExtenderControlProperty]
		[DefaultValue(HorizontalSide.Left)]
		public HorizontalSide HorizontalSide
		{
			get
			{
				return base.GetPropertyValue<HorizontalSide>("HorizontalSide", HorizontalSide.Left);
			}
			set
			{
				base.SetPropertyValue<HorizontalSide>("HorizontalSide", value);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000063BF File Offset: 0x000045BF
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000063CD File Offset: 0x000045CD
		[DefaultValue(0)]
		[ClientPropertyName("verticalOffset")]
		[ExtenderControlProperty]
		public int VerticalOffset
		{
			get
			{
				return base.GetPropertyValue<int>("VerticalOffset", 0);
			}
			set
			{
				base.SetPropertyValue<int>("VerticalOffset", value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000063DB File Offset: 0x000045DB
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000063E9 File Offset: 0x000045E9
		[DefaultValue(VerticalSide.Top)]
		[ClientPropertyName("verticalSide")]
		[ExtenderControlProperty]
		public VerticalSide VerticalSide
		{
			get
			{
				return base.GetPropertyValue<VerticalSide>("VerticalSide", VerticalSide.Top);
			}
			set
			{
				base.SetPropertyValue<VerticalSide>("VerticalSide", value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000063F7 File Offset: 0x000045F7
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000640A File Offset: 0x0000460A
		[ExtenderControlProperty]
		[DefaultValue(0.1f)]
		[ClientPropertyName("scrollEffectDuration")]
		public float ScrollEffectDuration
		{
			get
			{
				return base.GetPropertyValue<float>("ScrollEffectDuration", 0.1f);
			}
			set
			{
				base.SetPropertyValue<float>("ScrollEffectDuration", value);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006418 File Offset: 0x00004618
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00006426 File Offset: 0x00004626
		[ClientPropertyName("useAnimation")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool UseAnimation
		{
			get
			{
				return base.GetPropertyValue<bool>("UseAnimation", false);
			}
			set
			{
				base.SetPropertyValue<bool>("UseAnimation", value);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006434 File Offset: 0x00004634
		public override void EnsureValid()
		{
			base.EnsureValid();
			if (this.VerticalOffset < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, "AlwaysVisibleControlExtender on '{0}' cannot have a negative VerticalOffset value", new object[]
				{
					base.TargetControlID
				}));
			}
			if (this.HorizontalOffset < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, "AlwaysVisibleControlExtender on '{0}' cannot have a negative HorizontalOffset value", new object[]
				{
					base.TargetControlID
				}));
			}
			if (this.ScrollEffectDuration <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, "AlwaysVisibleControlExtender on '{0}' must have a positive ScrollEffectDuration", new object[]
				{
					base.TargetControlID
				}));
			}
		}
	}
}
