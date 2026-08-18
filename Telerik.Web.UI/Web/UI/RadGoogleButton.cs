using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000880 RID: 2176
	public class RadGoogleButton : RadSocialButtonBase
	{
		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x0600508E RID: 20622 RVA: 0x000FBA8B File Offset: 0x000F9C8B
		// (set) Token: 0x0600508F RID: 20623 RVA: 0x000FBA8E File Offset: 0x000F9C8E
		[Browsable(false)]
		[DefaultValue(SocialNetType.GooglePlusOne)]
		[Category("Behavior")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override SocialNetType SocialNetType
		{
			get
			{
				return SocialNetType.GooglePlusOne;
			}
			set
			{
				this.SocialNetType = SocialNetType.GooglePlusOne;
			}
		}

		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x06005090 RID: 20624 RVA: 0x000FBA97 File Offset: 0x000F9C97
		// (set) Token: 0x06005091 RID: 20625 RVA: 0x000FBAB8 File Offset: 0x000F9CB8
		[DefaultValue(GooglePlusOneSize.Standard)]
		[Description("Specifies the size of the button.")]
		[Category("Appearance")]
		public GooglePlusOneSize ButtonSize
		{
			get
			{
				return (GooglePlusOneSize)(base.ViewState["ButtonSize"] ?? GooglePlusOneSize.Standard);
			}
			set
			{
				base.ViewState["ButtonSize"] = value;
			}
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x06005092 RID: 20626 RVA: 0x000FBAD0 File Offset: 0x000F9CD0
		// (set) Token: 0x06005093 RID: 20627 RVA: 0x000FBAF1 File Offset: 0x000F9CF1
		[Description("Specifies the annotation type of the button.")]
		[DefaultValue(GooglePlusOneAnnotation.None)]
		[Category("Behavior")]
		public GooglePlusOneAnnotation AnnotationType
		{
			get
			{
				return (GooglePlusOneAnnotation)(base.ViewState["AnnotationType"] ?? GooglePlusOneAnnotation.None);
			}
			set
			{
				base.ViewState["AnnotationType"] = value;
			}
		}

		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x06005094 RID: 20628 RVA: 0x000FBB09 File Offset: 0x000F9D09
		// (set) Token: 0x06005095 RID: 20629 RVA: 0x000FBB25 File Offset: 0x000F9D25
		[DefaultValue(null)]
		[Description("Specifies the width of the button.")]
		[Category("Behavior")]
		public int? Width
		{
			get
			{
				return (int?)(base.ViewState["Width"] ?? null);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}
	}
}
