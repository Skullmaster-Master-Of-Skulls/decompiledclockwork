namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000E6 RID: 230
	internal partial class MenuBindingsEditorForm : global::System.Web.UI.Design.Util.DesignerForm
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x0002A514 File Offset: 0x00028714
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0002A54C File Offset: 0x0002874C
		private void InitializeComponent()
		{
			this._schemaLabel = new global::System.Windows.Forms.Label();
			this._bindingsLabel = new global::System.Windows.Forms.Label();
			this._bindingsListView = new global::System.Windows.Forms.ListBox();
			this._addBindingButton = new global::System.Windows.Forms.Button();
			this._propertiesLabel = new global::System.Windows.Forms.Label();
			this._cancelButton = new global::System.Windows.Forms.Button();
			this._propertyGrid = new global::System.Windows.Forms.Design.VsPropertyGrid(base.ServiceProvider);
			this._schemaTreeView = new global::System.Windows.Forms.TreeView();
			this._moveBindingUpButton = new global::System.Windows.Forms.Button();
			this._moveBindingDownButton = new global::System.Windows.Forms.Button();
			this._deleteBindingButton = new global::System.Windows.Forms.Button();
			this._okButton = new global::System.Windows.Forms.Button();
			this._applyButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this._schemaLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._schemaLabel.Location = new global::System.Drawing.Point(12, 12);
			this._schemaLabel.Name = "_schemaLabel";
			this._schemaLabel.Size = new global::System.Drawing.Size(196, 14);
			this._schemaLabel.TabIndex = 10;
			this._bindingsLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._bindingsLabel.Location = new global::System.Drawing.Point(12, 186);
			this._bindingsLabel.Name = "_bindingsLabel";
			this._bindingsLabel.Size = new global::System.Drawing.Size(196, 14);
			this._bindingsLabel.TabIndex = 25;
			this._bindingsListView.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._bindingsListView.Location = new global::System.Drawing.Point(12, 202);
			this._bindingsListView.Name = "_bindingsListView";
			this._bindingsListView.Size = new global::System.Drawing.Size(164, 135);
			this._bindingsListView.TabIndex = 30;
			this._bindingsListView.SelectedIndexChanged += new global::System.EventHandler(this.OnBindingsListViewSelectedIndexChanged);
			this._bindingsListView.GotFocus += new global::System.EventHandler(this.OnBindingsListViewGotFocus);
			this._addBindingButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._addBindingButton.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this._addBindingButton.Location = new global::System.Drawing.Point(133, 154);
			this._addBindingButton.Name = "_addBindingButton";
			this._addBindingButton.Size = new global::System.Drawing.Size(75, 23);
			this._addBindingButton.TabIndex = 20;
			this._addBindingButton.Click += new global::System.EventHandler(this.OnAddBindingButtonClick);
			this._propertiesLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this._propertiesLabel.Location = new global::System.Drawing.Point(229, 12);
			this._propertiesLabel.Name = "_propertiesLabel";
			this._propertiesLabel.Size = new global::System.Drawing.Size(266, 14);
			this._propertiesLabel.TabIndex = 50;
			this._cancelButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._cancelButton.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this._cancelButton.Location = new global::System.Drawing.Point(340, 346);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.TabIndex = 65;
			this._cancelButton.Click += new global::System.EventHandler(this.OnCancelButtonClick);
			this._okButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._okButton.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this._okButton.Location = new global::System.Drawing.Point(260, 346);
			this._okButton.Name = "_okButton";
			this._okButton.TabIndex = 60;
			this._okButton.Click += new global::System.EventHandler(this.OnOKButtonClick);
			this._applyButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._applyButton.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this._applyButton.Location = new global::System.Drawing.Point(420, 346);
			this._applyButton.Name = "_applyButton";
			this._applyButton.TabIndex = 60;
			this._applyButton.Click += new global::System.EventHandler(this.OnApplyButtonClick);
			this._applyButton.Enabled = false;
			this._propertyGrid.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._propertyGrid.CommandsVisibleIfAvailable = true;
			this._propertyGrid.Cursor = global::System.Windows.Forms.Cursors.HSplit;
			this._propertyGrid.LargeButtons = false;
			this._propertyGrid.LineColor = global::System.Drawing.SystemColors.ScrollBar;
			this._propertyGrid.Location = new global::System.Drawing.Point(229, 28);
			this._propertyGrid.Name = "_propertyGrid";
			this._propertyGrid.Size = new global::System.Drawing.Size(266, 309);
			this._propertyGrid.TabIndex = 55;
			this._propertyGrid.Text = global::System.Design.SR.GetString("MenuItemCollectionEditor_PropertyGrid");
			this._propertyGrid.ToolbarVisible = true;
			this._propertyGrid.ViewBackColor = global::System.Drawing.SystemColors.Window;
			this._propertyGrid.ViewForeColor = global::System.Drawing.SystemColors.WindowText;
			this._propertyGrid.PropertyValueChanged += new global::System.Windows.Forms.PropertyValueChangedEventHandler(this.OnPropertyGridPropertyValueChanged);
			this._propertyGrid.Site = this._menu.Site;
			this._schemaTreeView.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._schemaTreeView.HideSelection = false;
			this._schemaTreeView.ImageIndex = -1;
			this._schemaTreeView.Location = new global::System.Drawing.Point(12, 28);
			this._schemaTreeView.Name = "_schemaTreeView";
			this._schemaTreeView.SelectedImageIndex = -1;
			this._schemaTreeView.Size = new global::System.Drawing.Size(196, 120);
			this._schemaTreeView.TabIndex = 15;
			this._schemaTreeView.AfterSelect += new global::System.Windows.Forms.TreeViewEventHandler(this.OnSchemaTreeViewAfterSelect);
			this._schemaTreeView.GotFocus += new global::System.EventHandler(this.OnSchemaTreeViewGotFocus);
			this._moveBindingUpButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._moveBindingUpButton.Location = new global::System.Drawing.Point(182, 202);
			this._moveBindingUpButton.Name = "_moveBindingUpButton";
			this._moveBindingUpButton.Size = new global::System.Drawing.Size(26, 23);
			this._moveBindingUpButton.TabIndex = 35;
			this._moveBindingUpButton.Click += new global::System.EventHandler(this.OnMoveBindingUpButtonClick);
			this._moveBindingDownButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._moveBindingDownButton.Location = new global::System.Drawing.Point(182, 226);
			this._moveBindingDownButton.Name = "_moveBindingDownButton";
			this._moveBindingDownButton.Size = new global::System.Drawing.Size(26, 23);
			this._moveBindingDownButton.TabIndex = 40;
			this._moveBindingDownButton.Click += new global::System.EventHandler(this.OnMoveBindingDownButtonClick);
			this._deleteBindingButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._deleteBindingButton.Location = new global::System.Drawing.Point(182, 255);
			this._deleteBindingButton.Name = "_deleteBindingButton";
			this._deleteBindingButton.Size = new global::System.Drawing.Size(26, 23);
			this._deleteBindingButton.TabIndex = 45;
			this._deleteBindingButton.Click += new global::System.EventHandler(this.OnDeleteBindingButtonClick);
			base.AcceptButton = this._okButton;
			base.CancelButton = this._cancelButton;
			base.ClientSize = new global::System.Drawing.Size(507, 381);
			base.Controls.Add(this._deleteBindingButton);
			base.Controls.Add(this._moveBindingDownButton);
			base.Controls.Add(this._moveBindingUpButton);
			base.Controls.Add(this._okButton);
			base.Controls.Add(this._cancelButton);
			base.Controls.Add(this._applyButton);
			base.Controls.Add(this._propertiesLabel);
			base.Controls.Add(this._addBindingButton);
			base.Controls.Add(this._bindingsListView);
			base.Controls.Add(this._bindingsLabel);
			base.Controls.Add(this._schemaTreeView);
			base.Controls.Add(this._schemaLabel);
			base.Controls.Add(this._propertyGrid);
			this.MinimumSize = new global::System.Drawing.Size(507, 381);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "MenuBindingsEditor";
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.InitializeForm();
			base.ResumeLayout(false);
		}

		// Token: 0x04000495 RID: 1173
		private global::System.Windows.Forms.Label _schemaLabel;

		// Token: 0x04000496 RID: 1174
		private global::System.Windows.Forms.Label _bindingsLabel;

		// Token: 0x04000497 RID: 1175
		private global::System.Windows.Forms.ListBox _bindingsListView;

		// Token: 0x04000498 RID: 1176
		private global::System.Windows.Forms.Button _addBindingButton;

		// Token: 0x04000499 RID: 1177
		private global::System.Windows.Forms.Label _propertiesLabel;

		// Token: 0x0400049A RID: 1178
		private global::System.Windows.Forms.Button _cancelButton;

		// Token: 0x0400049B RID: 1179
		private global::System.Windows.Forms.Button _okButton;

		// Token: 0x0400049C RID: 1180
		private global::System.Windows.Forms.Button _applyButton;

		// Token: 0x0400049D RID: 1181
		private global::System.Windows.Forms.PropertyGrid _propertyGrid;

		// Token: 0x0400049E RID: 1182
		private global::System.Windows.Forms.TreeView _schemaTreeView;

		// Token: 0x0400049F RID: 1183
		private global::System.Windows.Forms.Button _moveBindingUpButton;

		// Token: 0x040004A0 RID: 1184
		private global::System.Windows.Forms.Button _deleteBindingButton;

		// Token: 0x040004A1 RID: 1185
		private global::System.Windows.Forms.Button _moveBindingDownButton;

		// Token: 0x040004A2 RID: 1186
		private global::System.ComponentModel.Container components;

		// Token: 0x040004A3 RID: 1187
		private global::System.Web.UI.WebControls.Menu _menu;
	}
}
