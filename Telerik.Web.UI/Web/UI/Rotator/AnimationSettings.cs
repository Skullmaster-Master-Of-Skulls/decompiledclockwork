using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Rotator
{
	// Token: 0x020019D8 RID: 6616
	public class AnimationSettings : ObjectWithState
	{
		// Token: 0x06010039 RID: 65593 RVA: 0x00397618 File Offset: 0x00395818
		public AnimationSettings(string keyPrefix, StateBag ownerViewState) : base(keyPrefix, ownerViewState)
		{
		}

		// Token: 0x17004D55 RID: 19797
		// (get) Token: 0x0601003A RID: 65594 RVA: 0x00397622 File Offset: 0x00395822
		// (set) Token: 0x0601003B RID: 65595 RVA: 0x0039764D File Offset: 0x0039584D
		internal bool isValueSet
		{
			get
			{
				return base.ViewState["isVallueSet"] != null && (bool)base.ViewState["isVallueSet"];
			}
			set
			{
				base.ViewState["isVallueSet"] = value;
			}
		}

		// Token: 0x17004D56 RID: 19798
		// (get) Token: 0x0601003C RID: 65596 RVA: 0x00397665 File Offset: 0x00395865
		// (set) Token: 0x0601003D RID: 65597 RVA: 0x00397686 File Offset: 0x00395886
		[NotifyParentProperty(true)]
		[DefaultValue(AnimationType.None)]
		public AnimationType Type
		{
			get
			{
				return (AnimationType)(base.ViewState["AnimationType"] ?? AnimationType.None);
			}
			set
			{
				base.ViewState["AnimationType"] = value;
				this.isValueSet = true;
			}
		}

		// Token: 0x17004D57 RID: 19799
		// (get) Token: 0x0601003E RID: 65598 RVA: 0x003976A5 File Offset: 0x003958A5
		// (set) Token: 0x0601003F RID: 65599 RVA: 0x003976CA File Offset: 0x003958CA
		[NotifyParentProperty(true)]
		[DefaultValue(500)]
		[Description("The animation duration in milliseconds")]
		public int Duration
		{
			get
			{
				return (int)(base.ViewState["Duration"] ?? 500);
			}
			set
			{
				base.ViewState["Duration"] = value;
				this.isValueSet = true;
			}
		}

		// Token: 0x06010040 RID: 65600 RVA: 0x003976EC File Offset: 0x003958EC
		internal void Describe(string propertyName, IScriptDescriptor descriptor)
		{
			if (this.isValueSet)
			{
				descriptor.AddProperty(propertyName, new Hashtable
				{
					{
						"type",
						this.Type
					},
					{
						"duration",
						this.Duration
					}
				});
			}
		}

		// Token: 0x0400487E RID: 18558
		private const AnimationType _defaultType = AnimationType.None;

		// Token: 0x0400487F RID: 18559
		private const int _defaultDuration = 500;
	}
}
