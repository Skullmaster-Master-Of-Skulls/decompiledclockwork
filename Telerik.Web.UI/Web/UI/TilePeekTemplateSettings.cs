using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000918 RID: 2328
	public class TilePeekTemplateSettings : StateManager
	{
		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x0600583C RID: 22588 RVA: 0x0010D9B4 File Offset: 0x0010BBB4
		// (set) Token: 0x0600583D RID: 22589 RVA: 0x0010D9D5 File Offset: 0x0010BBD5
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Gets a value indicating whether the peek template should be shown on mouse in.")]
		public virtual bool ShowPeekTemplateOnMouseOver
		{
			get
			{
				return (bool)(base.ViewState["ShowPeekTemplateOnMouseOver"] ?? false);
			}
			set
			{
				base.ViewState["ShowPeekTemplateOnMouseOver"] = value;
			}
		}

		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x0600583E RID: 22590 RVA: 0x0010D9ED File Offset: 0x0010BBED
		// (set) Token: 0x0600583F RID: 22591 RVA: 0x0010DA0E File Offset: 0x0010BC0E
		[Description("Gets a value indicating whether the peek template should be hidden on mouse out.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		public virtual bool HidePeekTemplateOnMouseOut
		{
			get
			{
				return (bool)(base.ViewState["HidePeekTemplateOnMouseOut"] ?? false);
			}
			set
			{
				base.ViewState["HidePeekTemplateOnMouseOut"] = value;
			}
		}

		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x06005840 RID: 22592 RVA: 0x0010DA26 File Offset: 0x0010BC26
		// (set) Token: 0x06005841 RID: 22593 RVA: 0x0010DA47 File Offset: 0x0010BC47
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(PeekTemplateAnimation.Fade)]
		[Description("Get/Set the animation effect of the PeekTemplate conent element.")]
		public PeekTemplateAnimation Animation
		{
			get
			{
				return (PeekTemplateAnimation)(base.ViewState["Animation"] ?? PeekTemplateAnimation.Fade);
			}
			set
			{
				base.ViewState["Animation"] = value;
			}
		}

		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x06005842 RID: 22594 RVA: 0x0010DA5F File Offset: 0x0010BC5F
		// (set) Token: 0x06005843 RID: 22595 RVA: 0x0010DA84 File Offset: 0x0010BC84
		[Description("Sets/gets the duration of the animation in milliseconds.")]
		[DefaultValue(500)]
		[Category("Behavior")]
		[ClientControlProperty]
		public int AnimationDuration
		{
			get
			{
				return (int)(base.ViewState["AnimationDuration"] ?? 500);
			}
			set
			{
				base.ViewState["AnimationDuration"] = value;
			}
		}

		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x06005844 RID: 22596 RVA: 0x0010DA9C File Offset: 0x0010BC9C
		// (set) Token: 0x06005845 RID: 22597 RVA: 0x0010DAC1 File Offset: 0x0010BCC1
		[Category("Behavior")]
		[DefaultValue(10000)]
		[Description("Gets or sets when the interval after which the peek template will automatically show.")]
		[ClientControlProperty]
		public int ShowInterval
		{
			get
			{
				return (int)(base.ViewState["ShowInterval"] ?? 10000);
			}
			set
			{
				base.ViewState["ShowInterval"] = value;
			}
		}

		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x06005846 RID: 22598 RVA: 0x0010DAD9 File Offset: 0x0010BCD9
		// (set) Token: 0x06005847 RID: 22599 RVA: 0x0010DAFE File Offset: 0x0010BCFE
		[DefaultValue(7000)]
		[Category("Behavior")]
		[Description("Specifies the interval after which the notification will automatically update the content.")]
		[ClientControlProperty]
		public int CloseDelay
		{
			get
			{
				return (int)(base.ViewState["CloseDelay"] ?? 7000);
			}
			set
			{
				base.ViewState["CloseDelay"] = value;
			}
		}

		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x06005848 RID: 22600 RVA: 0x0010DB16 File Offset: 0x0010BD16
		// (set) Token: 0x06005849 RID: 22601 RVA: 0x0010DB36 File Offset: 0x0010BD36
		[DefaultValue("")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Gets or sets easing that will be applied on the animation.")]
		public string Easing
		{
			get
			{
				return (string)(base.ViewState["Easing"] ?? "");
			}
			set
			{
				base.ViewState["Easing"] = value;
			}
		}
	}
}
