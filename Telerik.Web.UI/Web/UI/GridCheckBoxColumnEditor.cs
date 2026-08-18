using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x0200109B RID: 4251
	[TelerikToolboxCategory("Data")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Description("Telerik RadGrid")]
	public class GridCheckBoxColumnEditor : GridBoolColumnEditor
	{
		// Token: 0x170037D1 RID: 14289
		// (get) Token: 0x0600ACBE RID: 44222 RVA: 0x0025206C File Offset: 0x0025026C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CheckBox CheckBoxControl
		{
			get
			{
				this.EnsureControlsCreated();
				return this.checkBox;
			}
		}

		// Token: 0x170037D2 RID: 14290
		// (get) Token: 0x0600ACBF RID: 44223 RVA: 0x0025207A File Offset: 0x0025027A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style CheckBoxStyle
		{
			get
			{
				if (this._checkBoxStyle == null)
				{
					this._checkBoxStyle = new Style(this.ViewState);
				}
				return this._checkBoxStyle;
			}
		}

		// Token: 0x170037D3 RID: 14291
		// (get) Token: 0x0600ACC0 RID: 44224 RVA: 0x0025209B File Offset: 0x0025029B
		// (set) Token: 0x0600ACC1 RID: 44225 RVA: 0x002520A3 File Offset: 0x002502A3
		[Description("The ToolTip that will be applied to the CheckBox control.")]
		public string ToolTip
		{
			get
			{
				return this.toolTip;
			}
			set
			{
				this.toolTip = value;
			}
		}

		// Token: 0x0600ACC2 RID: 44226 RVA: 0x002520AC File Offset: 0x002502AC
		protected override void AddControlsToContainer()
		{
			this.CheckBoxControl.ApplyStyle(this.CheckBoxStyle);
			this.CheckBoxControl.Enabled = this.IsInEditMode;
			this.ContainerControl.Controls.Add(this.CheckBoxControl);
		}

		// Token: 0x0600ACC3 RID: 44227 RVA: 0x002520E8 File Offset: 0x002502E8
		internal override void CopySettingsFrom(IGridColumnEditor editor)
		{
			base.CopySettingsFrom(editor);
			GridCheckBoxColumnEditor gridCheckBoxColumnEditor = editor as GridCheckBoxColumnEditor;
			if (gridCheckBoxColumnEditor != null)
			{
				this.ToolTip = gridCheckBoxColumnEditor.ToolTip;
				this.CheckBoxStyle.CopyFrom(gridCheckBoxColumnEditor.CheckBoxStyle);
			}
		}

		// Token: 0x0600ACC4 RID: 44228 RVA: 0x00252123 File Offset: 0x00250323
		protected override void LoadControlsFromContainer()
		{
			this.checkBox = (this.ContainerControl.Controls[0] as CheckBox);
		}

		// Token: 0x170037D4 RID: 14292
		// (get) Token: 0x0600ACC5 RID: 44229 RVA: 0x00252141 File Offset: 0x00250341
		public override bool IsInitialized
		{
			get
			{
				return base.IsInitialized && this.checkBox != null;
			}
		}

		// Token: 0x0600ACC6 RID: 44230 RVA: 0x00252159 File Offset: 0x00250359
		protected override void CreateControls()
		{
			this.checkBox = new CheckBox();
			AccessibilityHelper.AddToolTip(this.checkBox, this.ToolTip);
		}

		// Token: 0x170037D5 RID: 14293
		// (get) Token: 0x0600ACC7 RID: 44231 RVA: 0x00252177 File Offset: 0x00250377
		// (set) Token: 0x0600ACC8 RID: 44232 RVA: 0x00252184 File Offset: 0x00250384
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Value
		{
			get
			{
				return this.CheckBoxControl.Checked;
			}
			set
			{
				this.CheckBoxControl.Checked = value;
			}
		}

		// Token: 0x04002DCB RID: 11723
		private CheckBox checkBox;

		// Token: 0x04002DCC RID: 11724
		private Style _checkBoxStyle;

		// Token: 0x04002DCD RID: 11725
		private string toolTip;
	}
}
