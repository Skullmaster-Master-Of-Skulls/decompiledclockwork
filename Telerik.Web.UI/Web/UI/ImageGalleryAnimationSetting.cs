using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000539 RID: 1337
	public class ImageGalleryAnimationSetting : StateManager
	{
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x0009AAB8 File Offset: 0x00098CB8
		// (set) Token: 0x06002F4B RID: 12107 RVA: 0x0009AAE1 File Offset: 0x00098CE1
		[Description("Gets or sets the type of animation that will be used for the current animation.")]
		[DefaultValue(ImageGalleryAnimationType.None)]
		[NotifyParentProperty(true)]
		public ImageGalleryAnimationType Type
		{
			get
			{
				object obj = base.ViewState["Type"];
				if (obj != null)
				{
					return (ImageGalleryAnimationType)obj;
				}
				return ImageGalleryAnimationType.None;
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x0009AAFC File Offset: 0x00098CFC
		// (set) Token: 0x06002F4D RID: 12109 RVA: 0x0009AB29 File Offset: 0x00098D29
		[Description("Gets or sets the time that the current animation will last. The property is in milliseconds.")]
		[DefaultValue(1200)]
		[NotifyParentProperty(true)]
		public int Speed
		{
			get
			{
				object obj = base.ViewState["Speed"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1200;
			}
			set
			{
				base.ViewState["Speed"] = value;
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06002F4E RID: 12110 RVA: 0x0009AB44 File Offset: 0x00098D44
		// (set) Token: 0x06002F4F RID: 12111 RVA: 0x0009AB6D File Offset: 0x00098D6D
		[NotifyParentProperty(true)]
		[Description("Gets or sets the easing that will be applied to the animation.")]
		[DefaultValue(ImageGalleryEasingType.Linear)]
		public ImageGalleryEasingType Easing
		{
			get
			{
				object obj = base.ViewState["Easing"];
				if (obj != null)
				{
					return (ImageGalleryEasingType)obj;
				}
				return ImageGalleryEasingType.Linear;
			}
			set
			{
				base.ViewState["Easing"] = value;
			}
		}
	}
}
