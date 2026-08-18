using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017DC RID: 6108
	[ParseChildren(true)]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleYAxisLabel : StyleAxisLabel
	{
		// Token: 0x170047EF RID: 18415
		// (get) Token: 0x0600EDA9 RID: 60841 RVA: 0x00362D1E File Offset: 0x00360F1E
		// (set) Token: 0x0600EDAA RID: 60842 RVA: 0x00362D44 File Offset: 0x00360F44
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(float), "270")]
		[SkinnableProperty]
		public override float RotationAngle
		{
			get
			{
				return (float)(base.ViewState["RotationAngle"] ?? 270f);
			}
			set
			{
				if (base.ViewState["RotationAngle"] != null && value == 270f)
				{
					base.ViewState.Remove("RotationAngle");
					return;
				}
				if (value != 270f)
				{
					base.ViewState["RotationAngle"] = value;
				}
			}
		}

		// Token: 0x0600EDAB RID: 60843 RVA: 0x00362D9A File Offset: 0x00360F9A
		internal override void Reset()
		{
			base.Reset();
			this.RotationAngle = 270f;
		}
	}
}
