using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200190C RID: 6412
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TargetInput
	{
		// Token: 0x0600F8D2 RID: 63698 RVA: 0x0038311A File Offset: 0x0038131A
		public TargetInput() : this(string.Empty, true)
		{
		}

		// Token: 0x0600F8D3 RID: 63699 RVA: 0x00383128 File Offset: 0x00381328
		public TargetInput(string controlID, bool enabled)
		{
			this.controlID = controlID;
			this.enabled = enabled;
		}

		// Token: 0x17004B34 RID: 19252
		// (get) Token: 0x0600F8D4 RID: 63700 RVA: 0x0038313E File Offset: 0x0038133E
		// (set) Token: 0x0600F8D5 RID: 63701 RVA: 0x00383146 File Offset: 0x00381346
		[Description("Gets or sets the TextBox ID")]
		[DefaultValue("ControlID")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public string ControlID
		{
			get
			{
				return this.controlID;
			}
			set
			{
				this.controlID = value;
			}
		}

		// Token: 0x17004B35 RID: 19253
		// (get) Token: 0x0600F8D6 RID: 63702 RVA: 0x0038314F File Offset: 0x0038134F
		// (set) Token: 0x0600F8D7 RID: 63703 RVA: 0x00383157 File Offset: 0x00381357
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating the control should be enabled or not.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x040046D0 RID: 18128
		private string controlID;

		// Token: 0x040046D1 RID: 18129
		private bool enabled;
	}
}
