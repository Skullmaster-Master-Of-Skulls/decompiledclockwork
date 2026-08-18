using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017D1 RID: 6097
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class StyleGridLineHidden : StyleGridLine
	{
		// Token: 0x170047D6 RID: 18390
		// (get) Token: 0x0600ED49 RID: 60745 RVA: 0x00361EFD File Offset: 0x003600FD
		// (set) Token: 0x0600ED4A RID: 60746 RVA: 0x00361F1E File Offset: 0x0036011E
		[DefaultValue(false)]
		public override bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? false);
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x0600ED4B RID: 60747 RVA: 0x00361F27 File Offset: 0x00360127
		internal override void Reset()
		{
			base.Reset();
			this.Visible = false;
		}
	}
}
