using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02001091 RID: 4241
	[ClientScriptResource("Telerik.Web.UI.EditorSpinBox", "Telerik.Web.UI.Common.Core.js")]
	public class EditorSpinBox : EditorToolsBase
	{
		// Token: 0x170037C5 RID: 14277
		// (get) Token: 0x0600AC7F RID: 44159 RVA: 0x002507F8 File Offset: 0x0024E9F8
		public override string Name
		{
			get
			{
				return "SpinBox";
			}
		}

		// Token: 0x170037C6 RID: 14278
		// (get) Token: 0x0600AC80 RID: 44160 RVA: 0x00250800 File Offset: 0x0024EA00
		// (set) Token: 0x0600AC81 RID: 44161 RVA: 0x00250829 File Offset: 0x0024EA29
		[ClientControlProperty]
		[DefaultValue(true)]
		public bool VisibleInput
		{
			get
			{
				object obj = this.ViewState["VisibleInput"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["VisibleInput"] = value;
			}
		}

		// Token: 0x0600AC82 RID: 44162 RVA: 0x00250841 File Offset: 0x0024EA41
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "visibleInput", this.VisibleInput, true);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600AC83 RID: 44163 RVA: 0x0025085D File Offset: 0x0024EA5D
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}
	}
}
