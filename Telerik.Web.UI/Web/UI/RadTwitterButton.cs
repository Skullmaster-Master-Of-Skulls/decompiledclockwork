using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000883 RID: 2179
	public class RadTwitterButton : RadSocialButtonBase
	{
		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x060050AB RID: 20651 RVA: 0x000FBCEE File Offset: 0x000F9EEE
		// (set) Token: 0x060050AC RID: 20652 RVA: 0x000FBCF1 File Offset: 0x000F9EF1
		[Category("Behavior")]
		[DefaultValue(SocialNetType.Twitter)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override SocialNetType SocialNetType
		{
			get
			{
				return SocialNetType.Twitter;
			}
			set
			{
				this.SocialNetType = SocialNetType.Twitter;
			}
		}

		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x060050AD RID: 20653 RVA: 0x000FBCFA File Offset: 0x000F9EFA
		// (set) Token: 0x060050AE RID: 20654 RVA: 0x000FBD1B File Offset: 0x000F9F1B
		[Description("Specifies the counter mode for the button.")]
		[DefaultValue(TwitterCounterMode.None)]
		[Category("Behavior")]
		public TwitterCounterMode CounterMode
		{
			get
			{
				return (TwitterCounterMode)(base.ViewState["CounterMode"] ?? TwitterCounterMode.None);
			}
			set
			{
				base.ViewState["CounterMode"] = value;
			}
		}
	}
}
