using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AutoComboBox.MyControls
{
	// Token: 0x020000F0 RID: 240
	public class MyLayoutPanel : Panel
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x0004A904 File Offset: 0x00049904
		private void PseudoDisabled_Dispose(bool disposing)
		{
			this.alreadyDisabledControls.Clear();
			this.RemoveEnabledChangedHandlers(this);
			if (this.layoutPanel != null)
			{
				this.layoutPanel.ControlAdded -= this.layoutPanel_ControlAdded;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0004A950 File Offset: 0x00049950
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x0004A968 File Offset: 0x00049968
		public new bool Enabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.isEnabled = value;
				this.SetControlsEnabledDisabled(this);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0004A97C File Offset: 0x0004997C
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x0004A994 File Offset: 0x00049994
		public bool Enabled2
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.isEnabled = value;
				this.SetControlsEnabledDisabled(this);
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0004A9A8 File Offset: 0x000499A8
		private void PseudoDisabled_OnControlAdded(ControlEventArgs e)
		{
			if (e.Control is MyTextBox)
			{
				MyTextBox myTextBox = (MyTextBox)e.Control;
				if (myTextBox.ReadOnly)
				{
					this.alreadyDisabledControls.Add(myTextBox);
				}
				myTextBox.ReadOnlyChanged += this.mtb_ReadOnlyChanged;
			}
			else
			{
				if (!e.Control.Enabled)
				{
					this.alreadyDisabledControls.Add(e.Control);
				}
				e.Control.EnabledChanged += this.Control_EnabledChanged;
			}
			this.SetControlEnabledDisabled(e.Control);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0004AA50 File Offset: 0x00049A50
		private void SetControlEnabledDisabled(Control c)
		{
			if (this.isEnabled)
			{
				if (!this.alreadyDisabledControls.Contains(c))
				{
					this.SetControlEnabledDisabled(c, true);
				}
			}
			else
			{
				this.SetControlEnabledDisabled(c, false);
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0004AA94 File Offset: 0x00049A94
		private void SetControlEnabledDisabled(Control c, bool enabledVal)
		{
			this.ignoreControlEnabledUpdate = true;
			if (c is MyTextBox)
			{
				MyTextBox myTextBox = (MyTextBox)c;
				myTextBox.ReadOnly = !enabledVal;
			}
			else
			{
				c.Enabled = enabledVal;
			}
			this.ignoreControlEnabledUpdate = false;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0004AAE0 File Offset: 0x00049AE0
		private void SetControlsEnabledDisabled(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control controlEnabledDisabled = (Control)obj;
				this.SetControlEnabledDisabled(controlEnabledDisabled);
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0004AB48 File Offset: 0x00049B48
		private void RemoveEnabledChangedHandlers(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is MyTextBox)
				{
					MyTextBox myTextBox = (MyTextBox)control;
					myTextBox.ReadOnlyChanged -= this.mtb_ReadOnlyChanged;
				}
				else
				{
					control.EnabledChanged -= this.Control_EnabledChanged;
				}
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0004ABF0 File Offset: 0x00049BF0
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			if (this.alreadyDisabledControls.Contains(e.Control))
			{
				this.alreadyDisabledControls.Remove(e.Control);
			}
			if (e.Control is MyTextBox)
			{
				MyTextBox myTextBox = (MyTextBox)e.Control;
				myTextBox.ReadOnlyChanged -= this.mtb_ReadOnlyChanged;
			}
			else
			{
				e.Control.EnabledChanged -= this.Control_EnabledChanged;
			}
			base.OnControlRemoved(e);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0004AC80 File Offset: 0x00049C80
		private void Control_EnabledChanged(object sender, EventArgs e)
		{
			if (!this.ignoreControlEnabledUpdate)
			{
				Control control = (Control)sender;
				if (control.Enabled)
				{
					if (this.alreadyDisabledControls.Contains(control))
					{
						this.alreadyDisabledControls.Remove(control);
					}
				}
				else
				{
					this.alreadyDisabledControls.Add(control);
				}
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0004ACE4 File Offset: 0x00049CE4
		private void mtb_ReadOnlyChanged(object sender, EventArgs e)
		{
			if (!this.ignoreControlEnabledUpdate)
			{
				MyTextBox myTextBox = (MyTextBox)sender;
				if (!myTextBox.ReadOnly)
				{
					if (this.alreadyDisabledControls.Contains(myTextBox))
					{
						this.alreadyDisabledControls.Remove(myTextBox);
					}
				}
				else
				{
					this.alreadyDisabledControls.Add(myTextBox);
				}
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0004AD48 File Offset: 0x00049D48
		public void ConvertToLayoutPanel(int numRows, int numCols, string colWidthsRowHeightsDef)
		{
			this.layoutPanel = new TableLayoutPanel();
			this.layoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.layoutPanel.AutoSize = true;
			if (numRows > 0)
			{
				this.layoutPanel.RowCount = numRows;
				for (int i = 0; i < numRows; i++)
				{
					RowStyle rowStyle = new RowStyle(SizeType.AutoSize);
					this.layoutPanel.RowStyles.Add(rowStyle);
				}
			}
			string[] array = colWidthsRowHeightsDef.Split(new char[]
			{
				','
			});
			float[] array2 = new float[numCols];
			float num = (float)(100.0 / Convert.ToDouble(numCols));
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = ((i < array.Length && array[i].Length > 0) ? float.Parse(array[i]) : num);
			}
			this.layoutPanel.ColumnCount = numCols;
			this.layoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			for (int i = 0; i < numCols; i++)
			{
				ColumnStyle columnStyle = new ColumnStyle(SizeType.Percent, array2[i]);
				this.layoutPanel.ColumnStyles.Add(columnStyle);
			}
			base.Controls.Add(this.layoutPanel);
			this.layoutPanel.Dock = DockStyle.Top;
			this.layoutPanel.BringToFront();
			this.layoutPanel.ControlAdded += this.layoutPanel_ControlAdded;
			this.isLayoutPanel = true;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0004AECC File Offset: 0x00049ECC
		private void layoutPanel_ControlAdded(object sender, ControlEventArgs e)
		{
			if (e.Control is CheckBox)
			{
				CheckBox checkBox = (CheckBox)e.Control;
				checkBox.Height = MyLayoutPanel.GetCheckboxHeight(checkBox, checkBox.Width);
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0004AF10 File Offset: 0x00049F10
		private static int GetCheckboxHeight(CheckBox chk, int availableWidth)
		{
			Graphics g = chk.CreateGraphics();
			CheckBoxState state;
			if (chk.Enabled)
			{
				switch (chk.CheckState)
				{
				case CheckState.Unchecked:
					if (chk.Focused)
					{
						state = CheckBoxState.UncheckedHot;
					}
					else
					{
						state = CheckBoxState.UncheckedNormal;
					}
					break;
				case CheckState.Checked:
					if (chk.Focused)
					{
						state = CheckBoxState.CheckedHot;
					}
					else
					{
						state = CheckBoxState.CheckedNormal;
					}
					break;
				case CheckState.Indeterminate:
					if (chk.Focused)
					{
						state = CheckBoxState.MixedHot;
					}
					else
					{
						state = CheckBoxState.MixedNormal;
					}
					break;
				default:
					state = CheckBoxState.UncheckedNormal;
					break;
				}
			}
			else
			{
				switch (chk.CheckState)
				{
				case CheckState.Unchecked:
					state = CheckBoxState.UncheckedDisabled;
					break;
				case CheckState.Checked:
					state = CheckBoxState.CheckedDisabled;
					break;
				case CheckState.Indeterminate:
					state = CheckBoxState.MixedDisabled;
					break;
				default:
					state = CheckBoxState.UncheckedNormal;
					break;
				}
			}
			Size glyphSize = CheckBoxRenderer.GetGlyphSize(g, state);
			glyphSize.Width += Convert.ToInt32(glyphSize.Width / 2);
			Size clientSize = chk.ClientSize;
			clientSize.Width = availableWidth;
			clientSize.Width -= glyphSize.Width;
			return TextRenderer.MeasureText(chk.Text, chk.Font, clientSize, TextFormatFlags.WordBreak).Height + chk.Margin.Top + chk.Margin.Bottom;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0004B064 File Offset: 0x0004A064
		public bool IsLayoutPanel
		{
			get
			{
				return this.isLayoutPanel;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0004B07C File Offset: 0x0004A07C
		public int RealHeight
		{
			get
			{
				int num = 10;
				if (this.layoutPanel != null)
				{
					foreach (object obj in this.layoutPanel.Controls)
					{
						Control control = (Control)obj;
						int num2 = control.Top + control.Height;
						num2 += SystemInformation.Border3DSize.Height;
						if (num2 > num)
						{
							num = num2;
						}
					}
				}
				return num + SystemInformation.Border3DSize.Height * 2;
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0004B148 File Offset: 0x0004A148
		public void ConvertToLayoutPanel(int numCols, string colWidthsRowHeightsDef)
		{
			this.ConvertToLayoutPanel(0, numCols, colWidthsRowHeightsDef);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0004B158 File Offset: 0x0004A158
		protected override void OnControlAdded(ControlEventArgs e)
		{
			this.PseudoDisabled_OnControlAdded(e);
			base.OnControlAdded(e);
			if (this.isLayoutPanel)
			{
				base.Controls.Remove(e.Control);
				this.layoutPanel.Controls.Add(e.Control);
				if (e.Control is Label)
				{
					e.Control.Dock = DockStyle.Fill;
				}
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0004B1D4 File Offset: 0x0004A1D4
		protected override void Dispose(bool disposing)
		{
			this.PseudoDisabled_Dispose(disposing);
			if (this.layoutPanel != null)
			{
				try
				{
					if (base.Controls != null && base.Controls.Contains(this.layoutPanel))
					{
						base.Controls.Remove(this.layoutPanel);
					}
					this.layoutPanel.Dispose();
				}
				catch
				{
				}
				this.layoutPanel = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040006DD RID: 1757
		private bool isEnabled = true;

		// Token: 0x040006DE RID: 1758
		private ArrayList alreadyDisabledControls = new ArrayList();

		// Token: 0x040006DF RID: 1759
		private bool ignoreControlEnabledUpdate = false;

		// Token: 0x040006E0 RID: 1760
		private int collapsible;

		// Token: 0x040006E1 RID: 1761
		private int originalHeight;

		// Token: 0x040006E2 RID: 1762
		private bool isLayoutPanel = false;

		// Token: 0x040006E3 RID: 1763
		private TableLayoutPanel layoutPanel = null;
	}
}
