using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000561 RID: 1377
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class LightBoxAnimationSetting : StateManager
	{
		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x000A2FA0 File Offset: 0x000A11A0
		// (set) Token: 0x060031A1 RID: 12705 RVA: 0x000A2FCD File Offset: 0x000A11CD
		[Description("Determines the animation speed for this animation type")]
		[DefaultValue(400)]
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
				return 400;
			}
			set
			{
				base.ViewState["Speed"] = value;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x000A2FE8 File Offset: 0x000A11E8
		// (set) Token: 0x060031A3 RID: 12707 RVA: 0x000A3011 File Offset: 0x000A1211
		[Description("Determines the easing type for this animation")]
		[DefaultValue(LightBoxEasingType.Linear)]
		[NotifyParentProperty(true)]
		public LightBoxEasingType Easing
		{
			get
			{
				object obj = base.ViewState["Easing"];
				if (obj != null)
				{
					return (LightBoxEasingType)obj;
				}
				return LightBoxEasingType.Linear;
			}
			set
			{
				base.ViewState["Easing"] = value;
			}
		}
	}
}
