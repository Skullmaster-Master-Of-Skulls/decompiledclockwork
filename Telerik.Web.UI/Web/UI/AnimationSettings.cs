using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200180D RID: 6157
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class AnimationSettings : ObjectWithState
	{
		// Token: 0x17004896 RID: 18582
		// (get) Token: 0x0600EFF6 RID: 61430 RVA: 0x0036A093 File Offset: 0x00368293
		// (set) Token: 0x0600EFF7 RID: 61431 RVA: 0x0036A0B5 File Offset: 0x003682B5
		[DefaultValue(AnimationType.OutQuart)]
		[NotifyParentProperty(true)]
		public virtual AnimationType Type
		{
			get
			{
				return (AnimationType)(base.ViewState["EasingType"] ?? AnimationType.OutQuart);
			}
			set
			{
				base.ViewState["EasingType"] = value;
			}
		}

		// Token: 0x17004897 RID: 18583
		// (get) Token: 0x0600EFF8 RID: 61432 RVA: 0x0036A0CD File Offset: 0x003682CD
		// (set) Token: 0x0600EFF9 RID: 61433 RVA: 0x0036A0F2 File Offset: 0x003682F2
		[NotifyParentProperty(true)]
		[DefaultValue(300)]
		[Description("The duration of the animation in milliseconds")]
		public virtual int Duration
		{
			get
			{
				return (int)(base.ViewState["Duration"] ?? 300);
			}
			set
			{
				base.ViewState["Duration"] = value;
			}
		}

		// Token: 0x0600EFFA RID: 61434 RVA: 0x0036A10A File Offset: 0x0036830A
		public AnimationSettings(string prefix, StateBag viewState) : base(prefix, viewState)
		{
		}

		// Token: 0x0600EFFB RID: 61435 RVA: 0x0036A114 File Offset: 0x00368314
		internal void Describe(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new AnimationSettingsConverter()
			});
			AnimationSettingsConverter animationSettingsConverter = new AnimationSettingsConverter();
			IDictionary<string, object> dictionary = animationSettingsConverter.Serialize(this, serializer);
			if (dictionary.Count > 0)
			{
				descriptor.AddProperty(propertyName, serializer.Serialize(this));
			}
		}
	}
}
