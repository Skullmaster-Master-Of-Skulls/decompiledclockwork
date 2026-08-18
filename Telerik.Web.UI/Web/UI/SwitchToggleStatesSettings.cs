using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000018 RID: 24
	[ToolboxItem(false)]
	public class SwitchToggleStatesSettings : StateManager
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004156 File Offset: 0x00002356
		[Description("Configure the ON/Checked toggle state settings.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		[Category("Behavior")]
		public virtual SwitchToggleState ToggleStateOn
		{
			get
			{
				if (this._toggleStateOn == null)
				{
					this._toggleStateOn = new SwitchToggleState();
				}
				return this._toggleStateOn;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004171 File Offset: 0x00002371
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		[Description("Configure the OFF/Unchecked toggle state settings.")]
		[Category("Behavior")]
		public virtual SwitchToggleState ToggleStateOff
		{
			get
			{
				if (this._toggleStateOff == null)
				{
					this._toggleStateOff = new SwitchToggleState();
				}
				return this._toggleStateOff;
			}
		}

		// Token: 0x04000015 RID: 21
		private SwitchToggleState _toggleStateOn;

		// Token: 0x04000016 RID: 22
		private SwitchToggleState _toggleStateOff;
	}
}
