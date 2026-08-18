using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AutoComboBox.MyControls;

namespace AutoComboBox
{
	// Token: 0x020000E0 RID: 224
	public class MyCheckBox : CheckBox, MyDynamicControl
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x000434AC File Offset: 0x000424AC
		public object ReportObject
		{
			get
			{
				return base.Checked;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000434CC File Offset: 0x000424CC
		public new string ToString()
		{
			return base.Checked ? "1" : "0";
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000434F2 File Offset: 0x000424F2
		public void FromString(string s)
		{
			base.Checked = (s.CompareTo("1") == 0);
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0004350C File Offset: 0x0004250C
		public bool FilledIn
		{
			get
			{
				return base.Checked;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00043524 File Offset: 0x00042524
		public MyCheckBox SyncedCheckbox
		{
			set
			{
				this.syncedCheckbox = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00043530 File Offset: 0x00042530
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00043548 File Offset: 0x00042548
		public int AutoCheckThisBoxWhenOtherControlModified_cid
		{
			get
			{
				return this.autoCheckThisBoxWhenOtherControlModified_cid;
			}
			set
			{
				this.autoCheckThisBoxWhenOtherControlModified_cid = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00043554 File Offset: 0x00042554
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x0004356C File Offset: 0x0004256C
		public CheckBoxAutoResizeMode AutoResizeMode
		{
			get
			{
				return this.autoResizeMode;
			}
			set
			{
				this.autoResizeMode = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00043578 File Offset: 0x00042578
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00043590 File Offset: 0x00042590
		public Control SetEnabledControl
		{
			get
			{
				return this.setEnabledControl;
			}
			set
			{
				this.setEnabledControl = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0004359C File Offset: 0x0004259C
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x000435B4 File Offset: 0x000425B4
		public int SetEnabledControlId
		{
			get
			{
				return this.setEnabledControlId;
			}
			set
			{
				this.setEnabledControlId = value;
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000435C0 File Offset: 0x000425C0
		private Control FindControl(Control topControl, int cid)
		{
			if (topControl != null)
			{
				foreach (object obj in topControl.Controls)
				{
					Control control = (Control)obj;
					if (control.Tag != null && control.Tag is DataRow)
					{
						DataRow dataRow = (DataRow)control.Tag;
						if (dataRow.Table.Columns.Contains("controlid"))
						{
							int num = (int)dataRow["controlid"];
							if (num == cid)
							{
								return control;
							}
						}
					}
					if (control.Controls.Count > 0)
					{
						Control control2 = this.FindControl(control, cid);
						if (control2 != null)
						{
							return control2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000436EC File Offset: 0x000426EC
		protected override void OnCheckedChanged(EventArgs e)
		{
			base.OnCheckedChanged(e);
			if (base.Checked && this.syncedCheckbox != null)
			{
				this.syncedCheckbox.Checked = false;
			}
			if (this.setEnabledControl == null && this.setEnabledControlId > 0)
			{
				Control topLevelControl = base.TopLevelControl;
				this.setEnabledControl = this.FindControl(topLevelControl, this.setEnabledControlId);
			}
			if (this.setEnabledControl != null)
			{
				this.SetEnabledControlEnabled(base.Checked);
			}
			if (this.autoCheckThisBoxWhenOtherControlModified_cid > 0 && !base.Checked)
			{
				try
				{
					Control parent = base.Parent;
					foreach (object obj in parent.Controls)
					{
						Control control = (Control)obj;
						if (control.Tag is DataRow)
						{
							DataRow dataRow = (DataRow)control.Tag;
							int num = (int)dataRow[0];
							if (num == this.autoCheckThisBoxWhenOtherControlModified_cid)
							{
								if (control is AutoComboBox)
								{
									AutoComboBox autoComboBox = (AutoComboBox)control;
									if (autoComboBox.SelectedIndex >= 0)
									{
										autoComboBox.SelectedIndex = -1;
									}
								}
								else if (control is TextBox)
								{
									TextBox textBox = (TextBox)control;
									if (textBox.Text.Length > 0)
									{
										textBox.Text = "";
									}
								}
								break;
							}
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x000438FC File Offset: 0x000428FC
		public void SetEnabledControlEnabled(bool enabled)
		{
			if (this.setEnabledControl != null)
			{
				this.setEnabledControl.Enabled = enabled;
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00043924 File Offset: 0x00042924
		protected override void Dispose(bool disposing)
		{
			this.syncedCheckbox = null;
			this.setEnabledControl = null;
			base.Dispose(disposing);
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00043940 File Offset: 0x00042940
		public Size CheckBoxBoxSize
		{
			get
			{
				if (this.checkboxBoxSize == Size.Empty)
				{
					Graphics g = base.CreateGraphics();
					CheckBoxState state;
					if (base.Enabled)
					{
						switch (base.CheckState)
						{
						case CheckState.Unchecked:
							if (this.Focused)
							{
								state = CheckBoxState.UncheckedHot;
							}
							else
							{
								state = CheckBoxState.UncheckedNormal;
							}
							break;
						case CheckState.Checked:
							if (this.Focused)
							{
								state = CheckBoxState.CheckedHot;
							}
							else
							{
								state = CheckBoxState.CheckedNormal;
							}
							break;
						case CheckState.Indeterminate:
							if (this.Focused)
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
						switch (base.CheckState)
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
					this.checkboxBoxSize = CheckBoxRenderer.GetGlyphSize(g, state);
					this.checkboxBoxSize.Width = this.checkboxBoxSize.Width + Convert.ToInt32(this.checkboxBoxSize.Width / 2);
				}
				return this.checkboxBoxSize;
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00043A50 File Offset: 0x00042A50
		public int MeasureHeightAutoSize(string text)
		{
			Size clientSize = base.ClientSize;
			clientSize.Width -= this.CheckBoxBoxSize.Width;
			return TextRenderer.MeasureText(text, this.Font, clientSize, TextFormatFlags.WordBreak).Height + base.Margin.Top + base.Margin.Bottom;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00043AC0 File Offset: 0x00042AC0
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			if (this.autoResizeMode == CheckBoxAutoResizeMode.AutoResizeHeightToFit)
			{
				this.AutoResizeHeight();
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00043AF0 File Offset: 0x00042AF0
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (this.autoResizeMode == CheckBoxAutoResizeMode.AutoResizeHeightToFit)
			{
				this.AutoResizeHeight();
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00043B20 File Offset: 0x00042B20
		private void AutoResizeHeight()
		{
			if (this.Text.CompareTo(this.lastAutoResize_text) != 0 || this.lastAutoResize_width != base.Width)
			{
				this.lastAutoResize_text = this.Text;
				this.lastAutoResize_width = base.Width;
				int num = this.MeasureHeightAutoSize(this.Text);
				if (num > 0)
				{
					base.Height = num;
				}
			}
		}

		// Token: 0x04000648 RID: 1608
		private CheckBoxAutoResizeMode autoResizeMode = CheckBoxAutoResizeMode.none;

		// Token: 0x04000649 RID: 1609
		private Control setEnabledControl = null;

		// Token: 0x0400064A RID: 1610
		private int setEnabledControlId = -1;

		// Token: 0x0400064B RID: 1611
		private MyCheckBox syncedCheckbox = null;

		// Token: 0x0400064C RID: 1612
		private int autoCheckThisBoxWhenOtherControlModified_cid = 0;

		// Token: 0x0400064D RID: 1613
		private Size checkboxBoxSize = Size.Empty;

		// Token: 0x0400064E RID: 1614
		private string lastAutoResize_text = "";

		// Token: 0x0400064F RID: 1615
		private int lastAutoResize_width = 0;
	}
}
