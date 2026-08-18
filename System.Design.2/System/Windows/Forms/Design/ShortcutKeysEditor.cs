using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200032F RID: 815
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ShortcutKeysEditor : UITypeEditor
	{
		// Token: 0x06002031 RID: 8241 RVA: 0x000C3610 File Offset: 0x000C1810
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					if (this.shortcutKeysUI == null)
					{
						this.shortcutKeysUI = new ShortcutKeysEditor.ShortcutKeysUI(this);
						this.shortcutKeysUI.BackColor = SystemColors.Control;
					}
					this.shortcutKeysUI.Start(windowsFormsEditorService, value);
					windowsFormsEditorService.DropDownControl(this.shortcutKeysUI);
					if (this.shortcutKeysUI.Value != null)
					{
						value = this.shortcutKeysUI.Value;
					}
					this.shortcutKeysUI.End();
				}
			}
			return value;
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x040018CA RID: 6346
		private ShortcutKeysEditor.ShortcutKeysUI shortcutKeysUI;

		// Token: 0x0200058D RID: 1421
		private class ShortcutKeysUI : UserControl
		{
			// Token: 0x060032BD RID: 12989 RVA: 0x00112D89 File Offset: 0x00110F89
			public ShortcutKeysUI(ShortcutKeysEditor editor)
			{
				this.editor = editor;
				this.keysConverter = null;
				this.End();
				this.InitializeComponent();
				this.AdjustSize();
			}

			// Token: 0x170009F4 RID: 2548
			// (get) Token: 0x060032BE RID: 12990 RVA: 0x00112DB1 File Offset: 0x00110FB1
			public IWindowsFormsEditorService EditorService
			{
				get
				{
					return this.edSvc;
				}
			}

			// Token: 0x170009F5 RID: 2549
			// (get) Token: 0x060032BF RID: 12991 RVA: 0x00112DB9 File Offset: 0x00110FB9
			private TypeConverter KeysConverter
			{
				get
				{
					if (this.keysConverter == null)
					{
						this.keysConverter = TypeDescriptor.GetConverter(typeof(Keys));
					}
					return this.keysConverter;
				}
			}

			// Token: 0x170009F6 RID: 2550
			// (get) Token: 0x060032C0 RID: 12992 RVA: 0x00112DDE File Offset: 0x00110FDE
			public object Value
			{
				get
				{
					if (((Keys)this.currentValue & Keys.KeyCode) == Keys.None)
					{
						return Keys.None;
					}
					return this.currentValue;
				}
			}

			// Token: 0x060032C1 RID: 12993 RVA: 0x00112E00 File Offset: 0x00111000
			private void btnReset_Click(object sender, EventArgs e)
			{
				this.chkCtrl.Checked = false;
				this.chkAlt.Checked = false;
				this.chkShift.Checked = false;
				this.cmbKey.SelectedIndex = -1;
			}

			// Token: 0x060032C2 RID: 12994 RVA: 0x00112E32 File Offset: 0x00111032
			private void chkModifier_CheckedChanged(object sender, EventArgs e)
			{
				this.UpdateCurrentValue();
			}

			// Token: 0x060032C3 RID: 12995 RVA: 0x00112E32 File Offset: 0x00111032
			private void cmbKey_SelectedIndexChanged(object sender, EventArgs e)
			{
				this.UpdateCurrentValue();
			}

			// Token: 0x060032C4 RID: 12996 RVA: 0x00112E3A File Offset: 0x0011103A
			public void End()
			{
				this.edSvc = null;
				this.originalValue = null;
				this.currentValue = null;
				this.updateCurrentValue = false;
				if (this.unknownKeyCode != Keys.None)
				{
					this.cmbKey.Items.RemoveAt(0);
					this.unknownKeyCode = Keys.None;
				}
			}

			// Token: 0x060032C5 RID: 12997 RVA: 0x00112E78 File Offset: 0x00111078
			private void InitializeComponent()
			{
				ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ShortcutKeysEditor));
				this.tlpOuter = new TableLayoutPanel();
				this.lblModifiers = new Label();
				this.chkCtrl = new CheckBox();
				this.chkAlt = new CheckBox();
				this.chkShift = new CheckBox();
				this.tlpInner = new TableLayoutPanel();
				this.lblKey = new Label();
				this.cmbKey = new ComboBox();
				this.btnReset = new Button();
				this.tlpOuter.SuspendLayout();
				this.tlpInner.SuspendLayout();
				base.SuspendLayout();
				componentResourceManager.ApplyResources(this.tlpOuter, "tlpOuter");
				this.tlpOuter.ColumnCount = 3;
				this.tlpOuter.ColumnStyles.Add(new ColumnStyle());
				this.tlpOuter.ColumnStyles.Add(new ColumnStyle());
				this.tlpOuter.ColumnStyles.Add(new ColumnStyle());
				this.tlpOuter.Controls.Add(this.lblModifiers, 0, 0);
				this.tlpOuter.Controls.Add(this.chkCtrl, 0, 1);
				this.tlpOuter.Controls.Add(this.chkShift, 1, 1);
				this.tlpOuter.Controls.Add(this.chkAlt, 2, 1);
				this.tlpOuter.Name = "tlpOuter";
				this.tlpOuter.RowCount = 2;
				this.tlpOuter.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
				this.tlpOuter.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
				componentResourceManager.ApplyResources(this.lblModifiers, "lblModifiers");
				this.tlpOuter.SetColumnSpan(this.lblModifiers, 3);
				this.lblModifiers.Name = "lblModifiers";
				componentResourceManager.ApplyResources(this.chkCtrl, "chkCtrl");
				this.chkCtrl.Name = "chkCtrl";
				this.chkCtrl.Margin = new Padding(12, 3, 3, 3);
				this.chkCtrl.CheckedChanged += this.chkModifier_CheckedChanged;
				componentResourceManager.ApplyResources(this.chkAlt, "chkAlt");
				this.chkAlt.Name = "chkAlt";
				this.chkAlt.CheckedChanged += this.chkModifier_CheckedChanged;
				componentResourceManager.ApplyResources(this.chkShift, "chkShift");
				this.chkShift.Name = "chkShift";
				this.chkShift.CheckedChanged += this.chkModifier_CheckedChanged;
				componentResourceManager.ApplyResources(this.tlpInner, "tlpInner");
				this.tlpInner.ColumnCount = 2;
				this.tlpInner.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				this.tlpInner.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				this.tlpInner.Controls.Add(this.lblKey, 0, 0);
				this.tlpInner.Controls.Add(this.cmbKey, 0, 1);
				this.tlpInner.Controls.Add(this.btnReset, 1, 1);
				this.tlpInner.Name = "tlpInner";
				this.tlpInner.RowCount = 2;
				this.tlpInner.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
				this.tlpInner.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				componentResourceManager.ApplyResources(this.lblKey, "lblKey");
				this.tlpInner.SetColumnSpan(this.lblKey, 2);
				this.lblKey.Name = "lblKey";
				componentResourceManager.ApplyResources(this.cmbKey, "cmbKey");
				this.cmbKey.DropDownStyle = ComboBoxStyle.DropDownList;
				this.cmbKey.Name = "cmbKey";
				this.cmbKey.Margin = new Padding(12, 4, 3, 3);
				this.cmbKey.Padding = this.cmbKey.Margin;
				foreach (Keys keys in ShortcutKeysEditor.ShortcutKeysUI.validKeys)
				{
					this.cmbKey.Items.Add(this.KeysConverter.ConvertToString(keys));
				}
				this.cmbKey.SelectedIndexChanged += this.cmbKey_SelectedIndexChanged;
				componentResourceManager.ApplyResources(this.btnReset, "btnReset");
				this.btnReset.Name = "btnReset";
				this.btnReset.Click += this.btnReset_Click;
				componentResourceManager.ApplyResources(this, "$this");
				base.Controls.AddRange(new Control[]
				{
					this.tlpInner,
					this.tlpOuter
				});
				base.Name = "ShortcutKeysUI";
				base.Padding = new Padding(4);
				this.tlpOuter.ResumeLayout(false);
				this.tlpOuter.PerformLayout();
				this.tlpInner.ResumeLayout(false);
				this.tlpInner.PerformLayout();
				base.ResumeLayout(false);
				base.PerformLayout();
			}

			// Token: 0x060032C6 RID: 12998 RVA: 0x00113390 File Offset: 0x00111590
			private void AdjustSize()
			{
				ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ShortcutKeysEditor));
				Size size = (Size)componentResourceManager.GetObject("btnReset.Size");
				base.Size = new Size(base.Size.Width + this.btnReset.Size.Width - size.Width, base.Size.Height);
			}

			// Token: 0x060032C7 RID: 12999 RVA: 0x00113404 File Offset: 0x00111604
			private static bool IsValidKey(Keys keyCode)
			{
				foreach (Keys keys in ShortcutKeysEditor.ShortcutKeysUI.validKeys)
				{
					if (keys == keyCode)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060032C8 RID: 13000 RVA: 0x00113430 File Offset: 0x00111630
			protected override void OnGotFocus(EventArgs e)
			{
				base.OnGotFocus(e);
				this.chkCtrl.Focus();
			}

			// Token: 0x060032C9 RID: 13001 RVA: 0x00113448 File Offset: 0x00111648
			protected override bool ProcessDialogKey(Keys keyData)
			{
				Keys keys = keyData & Keys.KeyCode;
				Keys keys2 = keyData & Keys.Modifiers;
				if (keys <= Keys.Escape)
				{
					if (keys != Keys.Tab)
					{
						if (keys == Keys.Escape)
						{
							if (!this.cmbKey.Focused || (keys2 & (Keys.Control | Keys.Alt)) != Keys.None || !this.cmbKey.DroppedDown)
							{
								this.currentValue = this.originalValue;
							}
						}
					}
					else if (keys2 == Keys.Shift && this.chkCtrl.Focused)
					{
						this.btnReset.Focus();
						return true;
					}
				}
				else if (keys != Keys.Left)
				{
					if (keys == Keys.Right)
					{
						if ((keys2 & (Keys.Control | Keys.Alt)) == Keys.None)
						{
							if (this.chkShift.Focused)
							{
								this.cmbKey.Focus();
								return true;
							}
							if (this.btnReset.Focused)
							{
								this.chkCtrl.Focus();
								return true;
							}
						}
					}
				}
				else if ((keys2 & (Keys.Control | Keys.Alt)) == Keys.None && this.chkCtrl.Focused)
				{
					this.btnReset.Focus();
					return true;
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x060032CA RID: 13002 RVA: 0x00113550 File Offset: 0x00111750
			public void Start(IWindowsFormsEditorService edSvc, object value)
			{
				this.edSvc = edSvc;
				this.currentValue = value;
				this.originalValue = value;
				Keys keys = (Keys)value;
				this.chkCtrl.Checked = ((keys & Keys.Control) > Keys.None);
				this.chkAlt.Checked = ((keys & Keys.Alt) > Keys.None);
				this.chkShift.Checked = ((keys & Keys.Shift) > Keys.None);
				Keys keys2 = keys & Keys.KeyCode;
				if (keys2 == Keys.None)
				{
					this.cmbKey.SelectedIndex = -1;
				}
				else if (ShortcutKeysEditor.ShortcutKeysUI.IsValidKey(keys2))
				{
					this.cmbKey.SelectedItem = this.KeysConverter.ConvertToString(keys2);
				}
				else
				{
					this.cmbKey.Items.Insert(0, SR.GetString("ShortcutKeys_InvalidKey"));
					this.cmbKey.SelectedIndex = 0;
					this.unknownKeyCode = keys2;
				}
				this.updateCurrentValue = true;
			}

			// Token: 0x060032CB RID: 13003 RVA: 0x00113630 File Offset: 0x00111830
			private void UpdateCurrentValue()
			{
				if (!this.updateCurrentValue)
				{
					return;
				}
				int selectedIndex = this.cmbKey.SelectedIndex;
				Keys keys = Keys.None;
				if (this.chkCtrl.Checked)
				{
					keys |= Keys.Control;
				}
				if (this.chkAlt.Checked)
				{
					keys |= Keys.Alt;
				}
				if (this.chkShift.Checked)
				{
					keys |= Keys.Shift;
				}
				if (this.unknownKeyCode != Keys.None && selectedIndex == 0)
				{
					keys |= this.unknownKeyCode;
				}
				else if (selectedIndex != -1)
				{
					keys |= ShortcutKeysEditor.ShortcutKeysUI.validKeys[(this.unknownKeyCode == Keys.None) ? selectedIndex : (selectedIndex - 1)];
				}
				this.currentValue = keys;
			}

			// Token: 0x060032CC RID: 13004 RVA: 0x001136D0 File Offset: 0x001118D0
			// Note: this type is marked as 'beforefieldinit'.
			static ShortcutKeysUI()
			{
				Keys[] array = new Keys[94];
				RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.C94097695072DB16C6F481271EC5CF66A7EDE205408CB07A6A1EE0B142DA0CBF).FieldHandle);
				ShortcutKeysEditor.ShortcutKeysUI.validKeys = array;
			}

			// Token: 0x040021DF RID: 8671
			private ShortcutKeysEditor editor;

			// Token: 0x040021E0 RID: 8672
			private IWindowsFormsEditorService edSvc;

			// Token: 0x040021E1 RID: 8673
			private object originalValue;

			// Token: 0x040021E2 RID: 8674
			private object currentValue;

			// Token: 0x040021E3 RID: 8675
			private TypeConverter keysConverter;

			// Token: 0x040021E4 RID: 8676
			private Keys unknownKeyCode;

			// Token: 0x040021E5 RID: 8677
			private bool updateCurrentValue;

			// Token: 0x040021E6 RID: 8678
			private TableLayoutPanel tlpOuter;

			// Token: 0x040021E7 RID: 8679
			private TableLayoutPanel tlpInner;

			// Token: 0x040021E8 RID: 8680
			private Label lblModifiers;

			// Token: 0x040021E9 RID: 8681
			private Label lblKey;

			// Token: 0x040021EA RID: 8682
			private CheckBox chkCtrl;

			// Token: 0x040021EB RID: 8683
			private CheckBox chkAlt;

			// Token: 0x040021EC RID: 8684
			private CheckBox chkShift;

			// Token: 0x040021ED RID: 8685
			private ComboBox cmbKey;

			// Token: 0x040021EE RID: 8686
			private Button btnReset;

			// Token: 0x040021EF RID: 8687
			private static Keys[] validKeys;
		}
	}
}
