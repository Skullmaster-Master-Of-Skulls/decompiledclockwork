using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002DB RID: 731
	public sealed class DockEditor : UITypeEditor
	{
		// Token: 0x06001D1E RID: 7454 RVA: 0x000AFAC0 File Offset: 0x000ADCC0
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					if (this.dockUI == null)
					{
						this.dockUI = DpiHelper.CreateInstanceInSystemAwareContext<DockEditor.DockUI>(() => new DockEditor.DockUI(this));
					}
					this.dockUI.Start(windowsFormsEditorService, value);
					windowsFormsEditorService.DropDownControl(this.dockUI);
					value = this.dockUI.Value;
					this.dockUI.End();
				}
			}
			return value;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x0400174F RID: 5967
		private DockEditor.DockUI dockUI;

		// Token: 0x0200056C RID: 1388
		private class DockUI : Control
		{
			// Token: 0x060031CA RID: 12746 RVA: 0x0010E638 File Offset: 0x0010C838
			public DockUI(DockEditor editor)
			{
				this.editor = editor;
				this.upDownOrder = new CheckBox[]
				{
					this.top,
					this.fill,
					this.bottom,
					this.none
				};
				this.leftRightOrder = new CheckBox[]
				{
					this.left,
					this.fill,
					this.right
				};
				this.tabOrder = new CheckBox[]
				{
					this.top,
					this.left,
					this.fill,
					this.right,
					this.bottom,
					this.none
				};
				if (!DockEditor.DockUI.isScalingInitialized)
				{
					if (DpiHelper.IsScalingRequired)
					{
						DockEditor.DockUI.noneHeight = DpiHelper.LogicalToDeviceUnitsY(24);
						DockEditor.DockUI.noneWidth = DpiHelper.LogicalToDeviceUnitsX(90);
						DockEditor.DockUI.controlHeight = DpiHelper.LogicalToDeviceUnitsY(116);
						DockEditor.DockUI.controlWidth = DpiHelper.LogicalToDeviceUnitsX(94);
						DockEditor.DockUI.offset2Y = DpiHelper.LogicalToDeviceUnitsY(2);
						DockEditor.DockUI.offset2X = DpiHelper.LogicalToDeviceUnitsX(2);
						DockEditor.DockUI.noneY = DpiHelper.LogicalToDeviceUnitsY(94);
						DockEditor.DockUI.buttonSize = DpiHelper.LogicalToDeviceUnits(DockEditor.DockUI.buttonSizeDefault, 0);
						DockEditor.DockUI.containerSize = DpiHelper.LogicalToDeviceUnits(DockEditor.DockUI.containerSizeDefault, 0);
					}
					DockEditor.DockUI.isScalingInitialized = true;
				}
				this.InitializeComponent();
			}

			// Token: 0x170009AA RID: 2474
			// (get) Token: 0x060031CB RID: 12747 RVA: 0x0010E7C4 File Offset: 0x0010C9C4
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x060031CC RID: 12748 RVA: 0x0010E7CC File Offset: 0x0010C9CC
			public void End()
			{
				this.edSvc = null;
				this.value = null;
			}

			// Token: 0x060031CD RID: 12749 RVA: 0x0010E7DC File Offset: 0x0010C9DC
			public virtual DockStyle GetDock(CheckBox btn)
			{
				if (this.top == btn)
				{
					return DockStyle.Top;
				}
				if (this.left == btn)
				{
					return DockStyle.Left;
				}
				if (this.bottom == btn)
				{
					return DockStyle.Bottom;
				}
				if (this.right == btn)
				{
					return DockStyle.Right;
				}
				if (this.fill == btn)
				{
					return DockStyle.Fill;
				}
				return DockStyle.None;
			}

			// Token: 0x060031CE RID: 12750 RVA: 0x0010E818 File Offset: 0x0010CA18
			private void InitializeComponent()
			{
				base.SetBounds(0, 0, DockEditor.DockUI.controlWidth, DockEditor.DockUI.controlHeight);
				this.BackColor = SystemColors.Control;
				this.ForeColor = SystemColors.ControlText;
				base.AccessibleName = SR.GetString("DockEditorAccName");
				this.none.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				this.none.Location = new Point(DockEditor.DockUI.offset2X, DockEditor.DockUI.noneY);
				this.none.Size = new Size(DockEditor.DockUI.noneWidth, DockEditor.DockUI.noneHeight);
				this.none.Text = DockStyle.None.ToString();
				this.none.TabIndex = 0;
				this.none.TabStop = true;
				this.none.Appearance = Appearance.Button;
				this.none.Click += this.OnClick;
				this.none.KeyDown += this.OnKeyDown;
				this.none.AccessibleName = SR.GetString("DockEditorNoneAccName");
				this.container.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				this.container.Location = new Point(DockEditor.DockUI.offset2X, DockEditor.DockUI.offset2Y);
				this.container.Size = DockEditor.DockUI.containerSize;
				this.none.Dock = DockStyle.Bottom;
				this.container.Dock = DockStyle.Fill;
				this.right.Dock = DockStyle.Right;
				this.right.Size = DockEditor.DockUI.buttonSize;
				this.right.TabIndex = 4;
				this.right.TabStop = true;
				this.right.Text = " ";
				this.right.Appearance = Appearance.Button;
				this.right.Click += this.OnClick;
				this.right.KeyDown += this.OnKeyDown;
				this.right.AccessibleName = SR.GetString("DockEditorRightAccName");
				this.left.Dock = DockStyle.Left;
				this.left.Size = DockEditor.DockUI.buttonSize;
				this.left.TabIndex = 2;
				this.left.TabStop = true;
				this.left.Text = " ";
				this.left.Appearance = Appearance.Button;
				this.left.Click += this.OnClick;
				this.left.KeyDown += this.OnKeyDown;
				this.left.AccessibleName = SR.GetString("DockEditorLeftAccName");
				this.top.Dock = DockStyle.Top;
				this.top.Size = DockEditor.DockUI.buttonSize;
				this.top.TabIndex = 1;
				this.top.TabStop = true;
				this.top.Text = " ";
				this.top.Appearance = Appearance.Button;
				this.top.Click += this.OnClick;
				this.top.KeyDown += this.OnKeyDown;
				this.top.AccessibleName = SR.GetString("DockEditorTopAccName");
				this.bottom.Dock = DockStyle.Bottom;
				this.bottom.Size = DockEditor.DockUI.buttonSize;
				this.bottom.TabIndex = 5;
				this.bottom.TabStop = true;
				this.bottom.Text = " ";
				this.bottom.Appearance = Appearance.Button;
				this.bottom.Click += this.OnClick;
				this.bottom.KeyDown += this.OnKeyDown;
				this.bottom.AccessibleName = SR.GetString("DockEditorBottomAccName");
				this.fill.Dock = DockStyle.Fill;
				this.fill.Size = DockEditor.DockUI.buttonSize;
				this.fill.TabIndex = 3;
				this.fill.TabStop = true;
				this.fill.Text = " ";
				this.fill.Appearance = Appearance.Button;
				this.fill.Click += this.OnClick;
				this.fill.KeyDown += this.OnKeyDown;
				this.fill.AccessibleName = SR.GetString("DockEditorFillAccName");
				base.Controls.Clear();
				base.Controls.AddRange(new Control[]
				{
					this.container,
					this.none
				});
				this.container.Controls.Clear();
				this.container.Controls.AddRange(new Control[]
				{
					this.fill,
					this.left,
					this.right,
					this.top,
					this.bottom
				});
			}

			// Token: 0x060031CF RID: 12751 RVA: 0x0010ECD0 File Offset: 0x0010CED0
			private void OnClick(object sender, EventArgs eventargs)
			{
				DockStyle dock = this.GetDock((CheckBox)sender);
				if (dock >= DockStyle.None)
				{
					this.value = dock;
				}
				this.Teardown();
			}

			// Token: 0x060031D0 RID: 12752 RVA: 0x0010ED00 File Offset: 0x0010CF00
			protected override void OnGotFocus(EventArgs e)
			{
				base.OnGotFocus(e);
				for (int i = 0; i < this.tabOrder.Length; i++)
				{
					if (this.tabOrder[i].Checked)
					{
						this.tabOrder[i].Focus();
						return;
					}
				}
			}

			// Token: 0x060031D1 RID: 12753 RVA: 0x0010ED48 File Offset: 0x0010CF48
			private void OnKeyDown(object sender, KeyEventArgs e)
			{
				Keys keyCode = e.KeyCode;
				Control control = null;
				if (keyCode != Keys.Tab)
				{
					if (keyCode == Keys.Return)
					{
						base.InvokeOnClick((CheckBox)sender, EventArgs.Empty);
						return;
					}
					switch (keyCode)
					{
					case Keys.Left:
					case Keys.Right:
					{
						int num = this.leftRightOrder.Length - 1;
						int i = 0;
						while (i <= num)
						{
							if (this.leftRightOrder[i] == sender)
							{
								if (keyCode == Keys.Left)
								{
									control = this.leftRightOrder[Math.Max(i - 1, 0)];
									break;
								}
								control = this.leftRightOrder[Math.Min(i + 1, num)];
								break;
							}
							else
							{
								i++;
							}
						}
						break;
					}
					case Keys.Up:
					case Keys.Down:
					{
						if (sender == this.left || sender == this.right)
						{
							sender = this.fill;
						}
						int num = this.upDownOrder.Length - 1;
						int j = 0;
						while (j <= num)
						{
							if (this.upDownOrder[j] == sender)
							{
								if (keyCode == Keys.Up)
								{
									control = this.upDownOrder[Math.Max(j - 1, 0)];
									break;
								}
								control = this.upDownOrder[Math.Min(j + 1, num)];
								break;
							}
							else
							{
								j++;
							}
						}
						break;
					}
					default:
						return;
					}
				}
				else
				{
					for (int k = 0; k < this.tabOrder.Length; k++)
					{
						if (this.tabOrder[k] == sender)
						{
							k += (((e.Modifiers & Keys.Shift) == Keys.None) ? 1 : -1);
							k = ((k < 0) ? (k + this.tabOrder.Length) : (k % this.tabOrder.Length));
							control = this.tabOrder[k];
							break;
						}
					}
				}
				e.Handled = true;
				if (control != null && control != sender)
				{
					control.Focus();
				}
			}

			// Token: 0x060031D2 RID: 12754 RVA: 0x0010EEE4 File Offset: 0x0010D0E4
			public void Start(IWindowsFormsEditorService edSvc, object value)
			{
				this.edSvc = edSvc;
				this.value = value;
				if (value is DockStyle)
				{
					DockStyle dockStyle = (DockStyle)value;
					this.none.Checked = false;
					this.top.Checked = false;
					this.left.Checked = false;
					this.right.Checked = false;
					this.bottom.Checked = false;
					this.fill.Checked = false;
					switch (dockStyle)
					{
					case DockStyle.None:
						this.none.Checked = true;
						return;
					case DockStyle.Top:
						this.top.Checked = true;
						return;
					case DockStyle.Bottom:
						this.bottom.Checked = true;
						return;
					case DockStyle.Left:
						this.left.Checked = true;
						return;
					case DockStyle.Right:
						this.right.Checked = true;
						return;
					case DockStyle.Fill:
						this.fill.Checked = true;
						break;
					default:
						return;
					}
				}
			}

			// Token: 0x060031D3 RID: 12755 RVA: 0x0010EFC5 File Offset: 0x0010D1C5
			private void Teardown()
			{
				this.edSvc.CloseDropDown();
			}

			// Token: 0x0400213E RID: 8510
			private DockEditor.DockUI.ContainerPlaceholder container = new DockEditor.DockUI.ContainerPlaceholder();

			// Token: 0x0400213F RID: 8511
			private CheckBox fill = new DockEditor.DockUI.DockEditorCheckBox();

			// Token: 0x04002140 RID: 8512
			private CheckBox left = new DockEditor.DockUI.DockEditorCheckBox();

			// Token: 0x04002141 RID: 8513
			private CheckBox right = new DockEditor.DockUI.DockEditorCheckBox();

			// Token: 0x04002142 RID: 8514
			private CheckBox top = new DockEditor.DockUI.DockEditorCheckBox();

			// Token: 0x04002143 RID: 8515
			private CheckBox bottom = new DockEditor.DockUI.DockEditorCheckBox();

			// Token: 0x04002144 RID: 8516
			private CheckBox none = new DockEditor.DockUI.DockEditorCheckBox();

			// Token: 0x04002145 RID: 8517
			private CheckBox[] upDownOrder;

			// Token: 0x04002146 RID: 8518
			private CheckBox[] leftRightOrder;

			// Token: 0x04002147 RID: 8519
			private CheckBox[] tabOrder;

			// Token: 0x04002148 RID: 8520
			private DockEditor editor;

			// Token: 0x04002149 RID: 8521
			private object value;

			// Token: 0x0400214A RID: 8522
			private IWindowsFormsEditorService edSvc;

			// Token: 0x0400214B RID: 8523
			private static bool isScalingInitialized = false;

			// Token: 0x0400214C RID: 8524
			private const int NONE_HEIGHT = 24;

			// Token: 0x0400214D RID: 8525
			private const int NONE_WIDTH = 90;

			// Token: 0x0400214E RID: 8526
			private static readonly Size buttonSizeDefault = new Size(20, 20);

			// Token: 0x0400214F RID: 8527
			private static readonly Size containerSizeDefault = new Size(90, 90);

			// Token: 0x04002150 RID: 8528
			private const int CONTROL_WIDTH = 94;

			// Token: 0x04002151 RID: 8529
			private const int CONTROL_HEIGHT = 116;

			// Token: 0x04002152 RID: 8530
			private const int OFFSET2X = 2;

			// Token: 0x04002153 RID: 8531
			private const int OFFSET2Y = 2;

			// Token: 0x04002154 RID: 8532
			private const int NONE_Y = 94;

			// Token: 0x04002155 RID: 8533
			private static int noneHeight = 24;

			// Token: 0x04002156 RID: 8534
			private static int noneWidth = 90;

			// Token: 0x04002157 RID: 8535
			private static Size buttonSize = DockEditor.DockUI.buttonSizeDefault;

			// Token: 0x04002158 RID: 8536
			private static Size containerSize = DockEditor.DockUI.containerSizeDefault;

			// Token: 0x04002159 RID: 8537
			private static int controlWidth = 94;

			// Token: 0x0400215A RID: 8538
			private static int controlHeight = 116;

			// Token: 0x0400215B RID: 8539
			private static int offset2X = 2;

			// Token: 0x0400215C RID: 8540
			private static int offset2Y = 2;

			// Token: 0x0400215D RID: 8541
			private static int noneY = 94;

			// Token: 0x020005EF RID: 1519
			private class DockEditorCheckBox : CheckBox
			{
				// Token: 0x17000A36 RID: 2614
				// (get) Token: 0x060034E1 RID: 13537 RVA: 0x00003B0F File Offset: 0x00001D0F
				protected override bool ShowFocusCues
				{
					get
					{
						return true;
					}
				}

				// Token: 0x060034E2 RID: 13538 RVA: 0x0011F0C0 File Offset: 0x0011D2C0
				protected override bool IsInputKey(Keys keyData)
				{
					return keyData == Keys.Return || keyData - Keys.Left <= 3 || base.IsInputKey(keyData);
				}
			}

			// Token: 0x020005F0 RID: 1520
			private class ContainerPlaceholder : Control
			{
				// Token: 0x060034E4 RID: 13540 RVA: 0x0011F0DF File Offset: 0x0011D2DF
				public ContainerPlaceholder()
				{
					this.BackColor = SystemColors.Control;
					base.TabStop = false;
				}

				// Token: 0x060034E5 RID: 13541 RVA: 0x0011F0FC File Offset: 0x0011D2FC
				protected override void OnPaint(PaintEventArgs e)
				{
					Rectangle clientRectangle = base.ClientRectangle;
					ControlPaint.DrawButton(e.Graphics, clientRectangle, ButtonState.Pushed);
				}
			}
		}
	}
}
