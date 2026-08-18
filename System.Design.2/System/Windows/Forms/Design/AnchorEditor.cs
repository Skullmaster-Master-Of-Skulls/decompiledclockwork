using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000292 RID: 658
	public sealed class AnchorEditor : UITypeEditor
	{
		// Token: 0x06001909 RID: 6409 RVA: 0x0008C014 File Offset: 0x0008A214
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					if (this.anchorUI == null)
					{
						this.anchorUI = DpiHelper.CreateInstanceInSystemAwareContext<AnchorEditor.AnchorUI>(() => new AnchorEditor.AnchorUI(this));
					}
					this.anchorUI.Start(windowsFormsEditorService, value);
					windowsFormsEditorService.DropDownControl(this.anchorUI);
					value = this.anchorUI.Value;
					this.anchorUI.End();
				}
			}
			return value;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x04001558 RID: 5464
		private AnchorEditor.AnchorUI anchorUI;

		// Token: 0x0200051E RID: 1310
		private class AnchorUI : Control
		{
			// Token: 0x06003002 RID: 12290 RVA: 0x001079B0 File Offset: 0x00105BB0
			public AnchorUI(AnchorEditor editor)
			{
				this.editor = editor;
				this.left = new AnchorEditor.AnchorUI.SpringControl(this);
				this.right = new AnchorEditor.AnchorUI.SpringControl(this);
				this.top = new AnchorEditor.AnchorUI.SpringControl(this);
				this.bottom = new AnchorEditor.AnchorUI.SpringControl(this);
				this.tabOrder = new AnchorEditor.AnchorUI.SpringControl[]
				{
					this.left,
					this.top,
					this.right,
					this.bottom
				};
				this.InitializeComponent();
			}

			// Token: 0x1700095B RID: 2395
			// (get) Token: 0x06003003 RID: 12291 RVA: 0x00107A46 File Offset: 0x00105C46
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x06003004 RID: 12292 RVA: 0x00107A4E File Offset: 0x00105C4E
			public void End()
			{
				this.edSvc = null;
				this.value = null;
			}

			// Token: 0x06003005 RID: 12293 RVA: 0x00107A60 File Offset: 0x00105C60
			public virtual AnchorStyles GetSelectedAnchor()
			{
				AnchorStyles anchorStyles = AnchorStyles.None;
				if (this.left.GetSolid())
				{
					anchorStyles |= AnchorStyles.Left;
				}
				if (this.top.GetSolid())
				{
					anchorStyles |= AnchorStyles.Top;
				}
				if (this.bottom.GetSolid())
				{
					anchorStyles |= AnchorStyles.Bottom;
				}
				if (this.right.GetSolid())
				{
					anchorStyles |= AnchorStyles.Right;
				}
				return anchorStyles;
			}

			// Token: 0x06003006 RID: 12294 RVA: 0x00107AB4 File Offset: 0x00105CB4
			internal virtual void InitializeComponent()
			{
				int width = SystemInformation.Border3DSize.Width;
				int height = SystemInformation.Border3DSize.Height;
				base.SetBounds(0, 0, 90, 90);
				base.AccessibleName = SR.GetString("AnchorEditorAccName");
				this.container.Location = new Point(0, 0);
				this.container.Size = new Size(90, 90);
				this.container.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				this.control.Location = new Point(30, 30);
				this.control.Size = new Size(30, 30);
				this.control.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				this.right.Location = new Point(60, 40);
				this.right.Size = new Size(30 - width, 10);
				this.right.TabIndex = 2;
				this.right.TabStop = true;
				this.right.Anchor = AnchorStyles.Right;
				this.right.AccessibleName = SR.GetString("AnchorEditorRightAccName");
				this.left.Location = new Point(width, 40);
				this.left.Size = new Size(30 - width, 10);
				this.left.TabIndex = 0;
				this.left.TabStop = true;
				this.left.Anchor = AnchorStyles.Left;
				this.left.AccessibleName = SR.GetString("AnchorEditorLeftAccName");
				this.top.Location = new Point(40, height);
				this.top.Size = new Size(10, 30 - height);
				this.top.TabIndex = 1;
				this.top.TabStop = true;
				this.top.Anchor = AnchorStyles.Top;
				this.top.AccessibleName = SR.GetString("AnchorEditorTopAccName");
				this.bottom.Location = new Point(40, 60);
				this.bottom.Size = new Size(10, 30 - height);
				this.bottom.TabIndex = 3;
				this.bottom.TabStop = true;
				this.bottom.Anchor = AnchorStyles.Bottom;
				this.bottom.AccessibleName = SR.GetString("AnchorEditorBottomAccName");
				base.Controls.Clear();
				base.Controls.AddRange(new Control[]
				{
					this.container
				});
				this.container.Controls.Clear();
				this.container.Controls.AddRange(new Control[]
				{
					this.control,
					this.top,
					this.left,
					this.bottom,
					this.right
				});
			}

			// Token: 0x06003007 RID: 12295 RVA: 0x00107D63 File Offset: 0x00105F63
			protected override void OnGotFocus(EventArgs e)
			{
				base.OnGotFocus(e);
				this.top.Focus();
			}

			// Token: 0x06003008 RID: 12296 RVA: 0x00107D78 File Offset: 0x00105F78
			private void SetValue()
			{
				this.value = this.GetSelectedAnchor();
			}

			// Token: 0x06003009 RID: 12297 RVA: 0x00107D8C File Offset: 0x00105F8C
			public void Start(IWindowsFormsEditorService edSvc, object value)
			{
				this.edSvc = edSvc;
				this.value = value;
				if (value is AnchorStyles)
				{
					this.left.SetSolid(((AnchorStyles)value & AnchorStyles.Left) == AnchorStyles.Left);
					this.top.SetSolid(((AnchorStyles)value & AnchorStyles.Top) == AnchorStyles.Top);
					this.bottom.SetSolid(((AnchorStyles)value & AnchorStyles.Bottom) == AnchorStyles.Bottom);
					this.right.SetSolid(((AnchorStyles)value & AnchorStyles.Right) == AnchorStyles.Right);
					this.oldAnchor = (AnchorStyles)value;
					return;
				}
				this.oldAnchor = (AnchorStyles.Top | AnchorStyles.Left);
			}

			// Token: 0x0600300A RID: 12298 RVA: 0x00107E1B File Offset: 0x0010601B
			private void Teardown(bool saveAnchor)
			{
				if (!saveAnchor)
				{
					this.value = this.oldAnchor;
				}
				this.edSvc.CloseDropDown();
			}

			// Token: 0x04002091 RID: 8337
			private AnchorEditor.AnchorUI.ContainerPlaceholder container = new AnchorEditor.AnchorUI.ContainerPlaceholder();

			// Token: 0x04002092 RID: 8338
			private AnchorEditor.AnchorUI.ControlPlaceholder control = new AnchorEditor.AnchorUI.ControlPlaceholder();

			// Token: 0x04002093 RID: 8339
			private IWindowsFormsEditorService edSvc;

			// Token: 0x04002094 RID: 8340
			private AnchorEditor.AnchorUI.SpringControl left;

			// Token: 0x04002095 RID: 8341
			private AnchorEditor.AnchorUI.SpringControl right;

			// Token: 0x04002096 RID: 8342
			private AnchorEditor.AnchorUI.SpringControl top;

			// Token: 0x04002097 RID: 8343
			private AnchorEditor.AnchorUI.SpringControl bottom;

			// Token: 0x04002098 RID: 8344
			private AnchorEditor.AnchorUI.SpringControl[] tabOrder;

			// Token: 0x04002099 RID: 8345
			private AnchorEditor editor;

			// Token: 0x0400209A RID: 8346
			private AnchorStyles oldAnchor;

			// Token: 0x0400209B RID: 8347
			private object value;

			// Token: 0x020005E6 RID: 1510
			private class ContainerPlaceholder : Control
			{
				// Token: 0x060034BE RID: 13502 RVA: 0x0011EA6D File Offset: 0x0011CC6D
				public ContainerPlaceholder()
				{
					this.BackColor = SystemColors.Window;
					this.ForeColor = SystemColors.WindowText;
					base.TabStop = false;
				}

				// Token: 0x060034BF RID: 13503 RVA: 0x0011EA94 File Offset: 0x0011CC94
				protected override void OnPaint(PaintEventArgs e)
				{
					Rectangle clientRectangle = base.ClientRectangle;
					ControlPaint.DrawBorder3D(e.Graphics, clientRectangle, Border3DStyle.Sunken);
				}
			}

			// Token: 0x020005E7 RID: 1511
			private class ControlPlaceholder : Control
			{
				// Token: 0x060034C0 RID: 13504 RVA: 0x0011EAB6 File Offset: 0x0011CCB6
				public ControlPlaceholder()
				{
					this.BackColor = SystemColors.Control;
					base.TabStop = false;
					base.SetStyle(ControlStyles.Selectable, false);
				}

				// Token: 0x060034C1 RID: 13505 RVA: 0x0011EADC File Offset: 0x0011CCDC
				protected override void OnPaint(PaintEventArgs e)
				{
					Rectangle clientRectangle = base.ClientRectangle;
					ControlPaint.DrawButton(e.Graphics, clientRectangle, ButtonState.Normal);
				}
			}

			// Token: 0x020005E8 RID: 1512
			private class SpringControl : Control
			{
				// Token: 0x060034C2 RID: 13506 RVA: 0x0011EAFD File Offset: 0x0011CCFD
				public SpringControl(AnchorEditor.AnchorUI picker)
				{
					if (picker == null)
					{
						throw new ArgumentException();
					}
					this.picker = picker;
					base.TabStop = true;
				}

				// Token: 0x060034C3 RID: 13507 RVA: 0x0011EB1C File Offset: 0x0011CD1C
				protected override AccessibleObject CreateAccessibilityInstance()
				{
					return new AnchorEditor.AnchorUI.SpringControl.SpringControlAccessibleObject(this);
				}

				// Token: 0x060034C4 RID: 13508 RVA: 0x0011EB24 File Offset: 0x0011CD24
				public virtual bool GetSolid()
				{
					return this.solid;
				}

				// Token: 0x060034C5 RID: 13509 RVA: 0x0011EB2C File Offset: 0x0011CD2C
				protected override void OnGotFocus(EventArgs e)
				{
					if (!this.focused)
					{
						this.focused = true;
						base.Invalidate();
					}
					base.OnGotFocus(e);
				}

				// Token: 0x060034C6 RID: 13510 RVA: 0x0011EB4A File Offset: 0x0011CD4A
				protected override void OnLostFocus(EventArgs e)
				{
					if (this.focused)
					{
						this.focused = false;
						base.Invalidate();
					}
					base.OnLostFocus(e);
				}

				// Token: 0x060034C7 RID: 13511 RVA: 0x0011EB68 File Offset: 0x0011CD68
				protected override void OnMouseDown(MouseEventArgs e)
				{
					this.SetSolid(!this.solid);
					base.Focus();
				}

				// Token: 0x060034C8 RID: 13512 RVA: 0x0011EB80 File Offset: 0x0011CD80
				protected override void OnPaint(PaintEventArgs e)
				{
					Rectangle clientRectangle = base.ClientRectangle;
					if (this.solid)
					{
						e.Graphics.FillRectangle(SystemBrushes.ControlDark, clientRectangle);
						e.Graphics.DrawRectangle(SystemPens.WindowFrame, clientRectangle.X, clientRectangle.Y, clientRectangle.Width - 1, clientRectangle.Height - 1);
					}
					else
					{
						ControlPaint.DrawFocusRectangle(e.Graphics, clientRectangle);
					}
					if (this.focused)
					{
						clientRectangle.Inflate(-2, -2);
						ControlPaint.DrawFocusRectangle(e.Graphics, clientRectangle);
					}
				}

				// Token: 0x060034C9 RID: 13513 RVA: 0x0011EC0A File Offset: 0x0011CE0A
				protected override bool ProcessDialogChar(char charCode)
				{
					if (charCode == ' ')
					{
						this.SetSolid(!this.solid);
						return true;
					}
					return base.ProcessDialogChar(charCode);
				}

				// Token: 0x060034CA RID: 13514 RVA: 0x0011EC2C File Offset: 0x0011CE2C
				protected override bool ProcessDialogKey(Keys keyData)
				{
					if ((keyData & Keys.KeyCode) == Keys.Return && (keyData & (Keys.Control | Keys.Alt)) == Keys.None)
					{
						this.picker.Teardown(true);
						return true;
					}
					if ((keyData & Keys.KeyCode) == Keys.Escape && (keyData & (Keys.Control | Keys.Alt)) == Keys.None)
					{
						this.picker.Teardown(false);
						return true;
					}
					if ((keyData & Keys.KeyCode) == Keys.Tab && (keyData & (Keys.Control | Keys.Alt)) == Keys.None)
					{
						for (int i = 0; i < this.picker.tabOrder.Length; i++)
						{
							if (this.picker.tabOrder[i] == this)
							{
								i += (((keyData & Keys.Shift) == Keys.None) ? 1 : -1);
								i = ((i < 0) ? (i + this.picker.tabOrder.Length) : (i % this.picker.tabOrder.Length));
								this.picker.tabOrder[i].Focus();
								break;
							}
						}
						return true;
					}
					return base.ProcessDialogKey(keyData);
				}

				// Token: 0x060034CB RID: 13515 RVA: 0x0011ED0C File Offset: 0x0011CF0C
				public virtual void SetSolid(bool value)
				{
					if (this.solid != value)
					{
						this.solid = value;
						this.picker.SetValue();
						base.Invalidate();
					}
				}

				// Token: 0x04002331 RID: 9009
				internal bool solid;

				// Token: 0x04002332 RID: 9010
				internal bool focused;

				// Token: 0x04002333 RID: 9011
				private AnchorEditor.AnchorUI picker;

				// Token: 0x020005FC RID: 1532
				private class SpringControlAccessibleObject : Control.ControlAccessibleObject
				{
					// Token: 0x06003503 RID: 13571 RVA: 0x0011F032 File Offset: 0x0011D232
					public SpringControlAccessibleObject(AnchorEditor.AnchorUI.SpringControl owner) : base(owner)
					{
					}

					// Token: 0x17000A39 RID: 2617
					// (get) Token: 0x06003504 RID: 13572 RVA: 0x0011F8F4 File Offset: 0x0011DAF4
					public override AccessibleStates State
					{
						get
						{
							AccessibleStates accessibleStates = base.State;
							if (((AnchorEditor.AnchorUI.SpringControl)base.Owner).GetSolid())
							{
								accessibleStates |= AccessibleStates.Selected;
							}
							return accessibleStates;
						}
					}
				}
			}
		}
	}
}
