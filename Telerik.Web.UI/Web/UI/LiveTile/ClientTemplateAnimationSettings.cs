using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.LiveTile
{
	// Token: 0x02000905 RID: 2309
	public class ClientTemplateAnimationSettings : ObjectWithState
	{
		// Token: 0x06005761 RID: 22369 RVA: 0x0010B2EB File Offset: 0x001094EB
		public ClientTemplateAnimationSettings(StateBag OwnerStateBag) : base("liveTile", OwnerStateBag)
		{
		}

		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x06005762 RID: 22370 RVA: 0x0010B2F9 File Offset: 0x001094F9
		// (set) Token: 0x06005763 RID: 22371 RVA: 0x0010B31A File Offset: 0x0010951A
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Get/Set the animation effect of the PeekTemplate conent element.")]
		[DefaultValue(ClientTemplateAnimation.Fade)]
		public ClientTemplateAnimation Animation
		{
			get
			{
				return (ClientTemplateAnimation)(base.ViewState["Animation"] ?? ClientTemplateAnimation.Fade);
			}
			set
			{
				base.ViewState["Animation"] = value;
			}
		}

		// Token: 0x17001CE6 RID: 7398
		// (get) Token: 0x06005764 RID: 22372 RVA: 0x0010B332 File Offset: 0x00109532
		// (set) Token: 0x06005765 RID: 22373 RVA: 0x0010B357 File Offset: 0x00109557
		[ClientControlProperty]
		[Description("Sets/gets the duration of the animation in milliseconds.")]
		[DefaultValue(500)]
		[Category("Behavior")]
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

		// Token: 0x17001CE7 RID: 7399
		// (get) Token: 0x06005766 RID: 22374 RVA: 0x0010B36F File Offset: 0x0010956F
		// (set) Token: 0x06005767 RID: 22375 RVA: 0x0010B38F File Offset: 0x0010958F
		[DefaultValue("")]
		[Description("Gets or sets easing that will be applied on the animation.")]
		[ClientControlProperty]
		[Category("Behavior")]
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
