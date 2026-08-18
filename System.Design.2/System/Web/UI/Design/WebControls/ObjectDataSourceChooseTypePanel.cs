using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Text;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000ED RID: 237
	internal sealed class ObjectDataSourceChooseTypePanel : WizardPanel
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x0002D754 File Offset: 0x0002B954
		public ObjectDataSourceChooseTypePanel(ObjectDataSourceDesigner objectDataSourceDesigner)
		{
			this._objectDataSourceDesigner = objectDataSourceDesigner;
			this._objectDataSource = (ObjectDataSource)this._objectDataSourceDesigner.Component;
			this.InitializeComponent();
			this.InitializeUI();
			ITypeDiscoveryService typeDiscoveryService = null;
			if (this._objectDataSource.Site != null)
			{
				typeDiscoveryService = (ITypeDiscoveryService)this._objectDataSource.Site.GetService(typeof(ITypeDiscoveryService));
			}
			this._discoveryServiceMode = (typeDiscoveryService != null);
			if (this._discoveryServiceMode)
			{
				this._typeNameTextBox.Visible = false;
				this._exampleLabel.Visible = false;
				Cursor value = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					ICollection collection = DesignerUtils.FilterGenericTypes(typeDiscoveryService.GetTypes(typeof(object), true));
					this._typeNameComboBox.BeginUpdate();
					if (collection != null)
					{
						StringCollection stringCollection = new StringCollection();
						stringCollection.Add("My.MyApplication");
						stringCollection.Add("My.MyComputer");
						stringCollection.Add("My.MyProject");
						stringCollection.Add("My.MyUser");
						this._typeItems = new List<ObjectDataSourceChooseTypePanel.TypeItem>(collection.Count);
						bool @checked = false;
						foreach (object obj in collection)
						{
							Type type = (Type)obj;
							if (!type.IsEnum && !type.IsInterface)
							{
								object[] customAttributes = type.GetCustomAttributes(typeof(DataObjectAttribute), true);
								if (customAttributes.Length != 0 && ((DataObjectAttribute)customAttributes[0]).IsDataObject)
								{
									this._typeItems.Add(new ObjectDataSourceChooseTypePanel.TypeItem(type, true));
									@checked = true;
								}
								else if (!stringCollection.Contains(type.FullName))
								{
									this._typeItems.Add(new ObjectDataSourceChooseTypePanel.TypeItem(type, false));
								}
							}
						}
						object showOnlyDataComponentsState = this._objectDataSourceDesigner.ShowOnlyDataComponentsState;
						if (showOnlyDataComponentsState == null)
						{
							this._filterCheckBox.Checked = @checked;
						}
						else
						{
							this._filterCheckBox.Checked = (bool)showOnlyDataComponentsState;
						}
						this.UpdateTypeList();
					}
					goto IL_224;
				}
				finally
				{
					this._typeNameComboBox.EndUpdate();
					Cursor.Current = value;
				}
			}
			this._typeNameComboBox.Visible = false;
			this._filterCheckBox.Visible = false;
			IL_224:
			this.TypeName = this._objectDataSource.TypeName;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0002D9CC File Offset: 0x0002BBCC
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0002D9F0 File Offset: 0x0002BBF0
		private string TypeName
		{
			get
			{
				ObjectDataSourceChooseTypePanel.TypeItem selectedTypeItem = this.SelectedTypeItem;
				if (selectedTypeItem != null)
				{
					return selectedTypeItem.TypeName;
				}
				return string.Empty;
			}
			set
			{
				if (this._discoveryServiceMode)
				{
					foreach (object obj in this._typeNameComboBox.Items)
					{
						ObjectDataSourceChooseTypePanel.TypeItem typeItem = (ObjectDataSourceChooseTypePanel.TypeItem)obj;
						if (string.Compare(typeItem.TypeName, value, StringComparison.OrdinalIgnoreCase) == 0)
						{
							this._typeNameComboBox.SelectedItem = typeItem;
							break;
						}
					}
					if (this._typeNameComboBox.SelectedItem == null && value.Length > 0)
					{
						ObjectDataSourceChooseTypePanel.TypeItem typeItem2 = new ObjectDataSourceChooseTypePanel.TypeItem(value, true);
						this._typeItems.Add(typeItem2);
						this.UpdateTypeList();
						this._typeNameComboBox.SelectedItem = typeItem2;
						return;
					}
				}
				else
				{
					this._typeNameTextBox.Text = value;
				}
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0002DAB8 File Offset: 0x0002BCB8
		private ObjectDataSourceChooseTypePanel.TypeItem SelectedTypeItem
		{
			get
			{
				if (this._discoveryServiceMode)
				{
					return this._typeNameComboBox.SelectedItem as ObjectDataSourceChooseTypePanel.TypeItem;
				}
				return new ObjectDataSourceChooseTypePanel.TypeItem(this._typeNameTextBox.Text, false);
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002DAE4 File Offset: 0x0002BCE4
		private void InitializeComponent()
		{
			this._helpLabel = new System.Windows.Forms.Label();
			this._nameLabel = new System.Windows.Forms.Label();
			this._exampleLabel = new System.Windows.Forms.Label();
			this._typeNameTextBox = new System.Windows.Forms.TextBox();
			this._typeNameComboBox = new AutoSizeComboBox();
			this._filterCheckBox = new System.Windows.Forms.CheckBox();
			base.SuspendLayout();
			this._helpLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._helpLabel.Location = new Point(0, 0);
			this._helpLabel.Name = "_helpLabel";
			this._helpLabel.Size = new Size(544, 60);
			this._helpLabel.TabIndex = 10;
			this._nameLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._nameLabel.Location = new Point(0, 68);
			this._nameLabel.Name = "_nameLabel";
			this._nameLabel.Size = new Size(544, 16);
			this._nameLabel.TabIndex = 20;
			this._typeNameTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._typeNameTextBox.Location = new Point(0, 86);
			this._typeNameTextBox.Name = "_typeNameTextBox";
			this._typeNameTextBox.Size = new Size(300, 20);
			this._typeNameTextBox.TabIndex = 30;
			this._typeNameTextBox.TextChanged += this.OnTypeNameTextBoxTextChanged;
			this._typeNameComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._typeNameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this._typeNameComboBox.Location = new Point(0, 86);
			this._typeNameComboBox.Name = "_typeNameComboBox";
			this._typeNameComboBox.Size = new Size(300, 21);
			this._typeNameComboBox.Sorted = true;
			this._typeNameComboBox.TabIndex = 30;
			this._typeNameComboBox.SelectedIndexChanged += this.OnTypeNameComboBoxSelectedIndexChanged;
			this._filterCheckBox.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this._filterCheckBox.Location = new Point(306, 86);
			this._filterCheckBox.Name = "_filterCheckBox";
			this._filterCheckBox.Size = new Size(200, 18);
			this._filterCheckBox.TabIndex = 50;
			this._filterCheckBox.CheckedChanged += this.OnFilterCheckBoxCheckedChanged;
			this._exampleLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._exampleLabel.ForeColor = SystemColors.GrayText;
			this._exampleLabel.Location = new Point(0, 122);
			this._exampleLabel.Name = "_exampleLabel";
			this._exampleLabel.Size = new Size(544, 16);
			this._exampleLabel.TabIndex = 60;
			base.Controls.Add(this._filterCheckBox);
			base.Controls.Add(this._typeNameComboBox);
			base.Controls.Add(this._typeNameTextBox);
			base.Controls.Add(this._exampleLabel);
			base.Controls.Add(this._nameLabel);
			base.Controls.Add(this._helpLabel);
			base.Name = "ObjectDataSourceChooseTypePanel";
			base.Size = new Size(544, 274);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002DE34 File Offset: 0x0002C034
		private void InitializeUI()
		{
			base.Caption = SR.GetString("ObjectDataSourceChooseTypePanel_PanelCaption");
			this._helpLabel.Text = SR.GetString("ObjectDataSourceChooseTypePanel_HelpLabel");
			this._nameLabel.Text = SR.GetString("ObjectDataSourceChooseTypePanel_NameLabel");
			this._exampleLabel.Text = SR.GetString("ObjectDataSourceChooseTypePanel_ExampleLabel");
			this._filterCheckBox.Text = SR.GetString("ObjectDataSourceChooseTypePanel_FilterCheckBox");
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002DEA8 File Offset: 0x0002C0A8
		protected internal override void OnComplete()
		{
			if (this._objectDataSource.TypeName != this.TypeName)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._objectDataSource)["TypeName"];
				propertyDescriptor.SetValue(this._objectDataSource, this.TypeName);
			}
			if (this.SelectedTypeItem != null && this.SelectedTypeItem.Filtered)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._objectDataSource)["OldValuesParameterFormatString"];
				propertyDescriptor.SetValue(this._objectDataSource, "original_{0}");
			}
			this._objectDataSourceDesigner.ShowOnlyDataComponentsState = this._filterCheckBox.Checked;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002DF4C File Offset: 0x0002C14C
		private void OnFilterCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.UpdateTypeList();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002DF54 File Offset: 0x0002C154
		public override bool OnNext()
		{
			ObjectDataSourceChooseTypePanel.TypeItem selectedTypeItem = this.SelectedTypeItem;
			Type type = selectedTypeItem.Type;
			if (type == null)
			{
				ITypeResolutionService typeResolutionService = (ITypeResolutionService)base.ServiceProvider.GetService(typeof(ITypeResolutionService));
				if (typeResolutionService == null)
				{
					return false;
				}
				try
				{
					type = typeResolutionService.GetType(selectedTypeItem.TypeName, true, true);
				}
				catch (Exception ex)
				{
					UIServiceHelper.ShowError(base.ServiceProvider, ex, SR.GetString("ObjectDataSourceDesigner_CannotGetType", new object[]
					{
						selectedTypeItem.TypeName
					}));
					return false;
				}
			}
			if (type == null)
			{
				return false;
			}
			if (type != this._previousSelectedType)
			{
				ObjectDataSourceChooseMethodsPanel objectDataSourceChooseMethodsPanel = base.NextPanel as ObjectDataSourceChooseMethodsPanel;
				objectDataSourceChooseMethodsPanel.SetType(type);
				this._previousSelectedType = type;
			}
			return true;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00003937 File Offset: 0x00001B37
		public override void OnPrevious()
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002E020 File Offset: 0x0002C220
		private void OnTypeNameComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateEnabledState();
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002E020 File Offset: 0x0002C220
		private void OnTypeNameTextBoxTextChanged(object sender, EventArgs e)
		{
			this.UpdateEnabledState();
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002E028 File Offset: 0x0002C228
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				this.UpdateEnabledState();
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0002E040 File Offset: 0x0002C240
		private void UpdateEnabledState()
		{
			if (base.ParentWizard != null)
			{
				base.ParentWizard.FinishButton.Enabled = false;
				if (this._discoveryServiceMode)
				{
					base.ParentWizard.NextButton.Enabled = (this._typeNameComboBox.SelectedItem != null);
					return;
				}
				base.ParentWizard.NextButton.Enabled = (this._typeNameTextBox.Text.Length > 0);
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002E0B0 File Offset: 0x0002C2B0
		private void UpdateTypeList()
		{
			object selectedItem = this._typeNameComboBox.SelectedItem;
			try
			{
				this._typeNameComboBox.BeginUpdate();
				this._typeNameComboBox.Items.Clear();
				bool @checked = this._filterCheckBox.Checked;
				foreach (ObjectDataSourceChooseTypePanel.TypeItem typeItem in this._typeItems)
				{
					if (@checked)
					{
						if (typeItem.Filtered)
						{
							this._typeNameComboBox.Items.Add(typeItem);
						}
					}
					else
					{
						this._typeNameComboBox.Items.Add(typeItem);
					}
				}
			}
			finally
			{
				this._typeNameComboBox.EndUpdate();
			}
			this._typeNameComboBox.SelectedItem = selectedItem;
			this.UpdateEnabledState();
			this._typeNameComboBox.InvalidateDropDownWidth();
		}

		// Token: 0x040004CC RID: 1228
		private const string CompareAllValuesFormatString = "original_{0}";

		// Token: 0x040004CD RID: 1229
		private System.Windows.Forms.TextBox _typeNameTextBox;

		// Token: 0x040004CE RID: 1230
		private System.Windows.Forms.CheckBox _filterCheckBox;

		// Token: 0x040004CF RID: 1231
		private System.Windows.Forms.Label _helpLabel;

		// Token: 0x040004D0 RID: 1232
		private System.Windows.Forms.Label _nameLabel;

		// Token: 0x040004D1 RID: 1233
		private System.Windows.Forms.Label _exampleLabel;

		// Token: 0x040004D2 RID: 1234
		private AutoSizeComboBox _typeNameComboBox;

		// Token: 0x040004D3 RID: 1235
		private ObjectDataSource _objectDataSource;

		// Token: 0x040004D4 RID: 1236
		private ObjectDataSourceDesigner _objectDataSourceDesigner;

		// Token: 0x040004D5 RID: 1237
		private Type _previousSelectedType;

		// Token: 0x040004D6 RID: 1238
		private bool _discoveryServiceMode;

		// Token: 0x040004D7 RID: 1239
		private List<ObjectDataSourceChooseTypePanel.TypeItem> _typeItems;

		// Token: 0x02000414 RID: 1044
		private sealed class TypeItem
		{
			// Token: 0x0600280E RID: 10254 RVA: 0x000F4F0B File Offset: 0x000F310B
			public TypeItem(string typeName, bool filtered)
			{
				this._typeName = typeName;
				this._prettyTypeName = this._typeName;
				this._type = null;
				this._filtered = filtered;
			}

			// Token: 0x0600280F RID: 10255 RVA: 0x000F4F34 File Offset: 0x000F3134
			public TypeItem(Type type, bool filtered)
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				ObjectDataSourceMethodEditor.AppendTypeName(type, true, stringBuilder);
				this._prettyTypeName = stringBuilder.ToString();
				this._typeName = type.FullName;
				this._type = type;
				this._filtered = filtered;
			}

			// Token: 0x17000863 RID: 2147
			// (get) Token: 0x06002810 RID: 10256 RVA: 0x000F4F7D File Offset: 0x000F317D
			public bool Filtered
			{
				get
				{
					return this._filtered;
				}
			}

			// Token: 0x17000864 RID: 2148
			// (get) Token: 0x06002811 RID: 10257 RVA: 0x000F4F85 File Offset: 0x000F3185
			public string TypeName
			{
				get
				{
					return this._typeName;
				}
			}

			// Token: 0x17000865 RID: 2149
			// (get) Token: 0x06002812 RID: 10258 RVA: 0x000F4F8D File Offset: 0x000F318D
			public Type Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x06002813 RID: 10259 RVA: 0x000F4F95 File Offset: 0x000F3195
			public override string ToString()
			{
				return this._prettyTypeName;
			}

			// Token: 0x04001C8A RID: 7306
			private string _prettyTypeName;

			// Token: 0x04001C8B RID: 7307
			private string _typeName;

			// Token: 0x04001C8C RID: 7308
			private Type _type;

			// Token: 0x04001C8D RID: 7309
			private bool _filtered;
		}
	}
}
