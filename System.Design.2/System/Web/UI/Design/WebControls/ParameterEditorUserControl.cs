using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000F7 RID: 247
	public class ParameterEditorUserControl : UserControl
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x00030606 File Offset: 0x0002E806
		public ParameterEditorUserControl(IServiceProvider serviceProvider) : this(serviceProvider, null, null)
		{
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00030611 File Offset: 0x0002E811
		internal ParameterEditorUserControl(IServiceProvider serviceProvider, Control control) : this(serviceProvider, control, null)
		{
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0003061C File Offset: 0x0002E81C
		internal ParameterEditorUserControl(IServiceProvider serviceProvider, Control control, TypeDescriptionProvider provider)
		{
			this._serviceProvider = serviceProvider;
			this._control = control;
			this._provider = provider;
			this.InitializeComponent();
			this.InitializeUI();
			this.InitializeParameterEditors();
			this._parameterTypes = this.CreateParameterList();
			foreach (object obj in this._parameterTypes)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this._parameterTypeComboBox.Items.Add(dictionaryEntry.Value);
			}
			this._parameterTypeComboBox.InvalidateDropDownWidth();
			this.UpdateUI(false);
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x000306D4 File Offset: 0x0002E8D4
		public bool ParametersConfigured
		{
			get
			{
				foreach (object obj in this._parametersListView.Items)
				{
					ParameterEditorUserControl.ParameterListViewItem parameterListViewItem = (ParameterEditorUserControl.ParameterListViewItem)obj;
					if (parameterListViewItem != null && !parameterListViewItem.IsConfigured)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000890 RID: 2192 RVA: 0x00030740 File Offset: 0x0002E940
		// (remove) Token: 0x06000891 RID: 2193 RVA: 0x00030753 File Offset: 0x0002E953
		public event EventHandler ParametersChanged
		{
			add
			{
				base.Events.AddHandler(ParameterEditorUserControl.EventParametersChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ParameterEditorUserControl.EventParametersChanged, value);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00030768 File Offset: 0x0002E968
		public TypeDescriptionProvider TypeDescriptionProvider
		{
			get
			{
				if (this._provider != null)
				{
					return this._provider;
				}
				if (this._control != null)
				{
					return TypeDescriptor.GetProvider(this._control);
				}
				if (this._serviceProvider != null)
				{
					TypeDescriptionProviderService typeDescriptionProviderService = this._serviceProvider.GetService(typeof(TypeDescriptionProviderService)) as TypeDescriptionProviderService;
					if (typeDescriptionProviderService != null)
					{
						return typeDescriptionProviderService.GetProvider(null);
					}
				}
				return null;
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000307C8 File Offset: 0x0002E9C8
		internal ListDictionary CreateParameterList()
		{
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(typeof(Parameter), "None");
			listDictionary.Add(typeof(CookieParameter), "Cookie");
			listDictionary.Add(typeof(ControlParameter), "Control");
			listDictionary.Add(typeof(FormParameter), "Form");
			listDictionary.Add(typeof(ProfileParameter), "Profile");
			listDictionary.Add(typeof(QueryStringParameter), "QueryString");
			listDictionary.Add(typeof(SessionParameter), "Session");
			TypeDescriptionProvider typeDescriptionProvider = this.TypeDescriptionProvider;
			if (typeDescriptionProvider == null || typeDescriptionProvider.IsSupportedType(typeof(RouteParameter)))
			{
				listDictionary.Add(typeof(RouteParameter), "RouteData");
			}
			return listDictionary;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000308A0 File Offset: 0x0002EAA0
		private void InitializeComponent()
		{
			this._addButtonPanel = new System.Windows.Forms.Panel();
			this._addParameterButton = new System.Windows.Forms.Button();
			this._parametersLabel = new System.Windows.Forms.Label();
			this._sourceLabel = new System.Windows.Forms.Label();
			this._parametersListView = new ListView();
			this._nameColumnHeader = new ColumnHeader("");
			this._valueColumnHeader = new ColumnHeader("");
			this._parameterTypeComboBox = new AutoSizeComboBox();
			this._moveUpButton = new System.Windows.Forms.Button();
			this._moveDownButton = new System.Windows.Forms.Button();
			this._deleteParameterButton = new System.Windows.Forms.Button();
			this._editorPanel = new System.Windows.Forms.Panel();
			this._addButtonPanel.SuspendLayout();
			base.SuspendLayout();
			this._parametersLabel.Location = new Point(0, 0);
			this._parametersLabel.Name = "_parametersLabel";
			this._parametersLabel.Size = new Size(252, 16);
			this._parametersLabel.TabIndex = 10;
			this._parametersListView.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this._parametersListView.Columns.AddRange(new ColumnHeader[]
			{
				this._nameColumnHeader,
				this._valueColumnHeader
			});
			this._parametersListView.FullRowSelect = true;
			this._parametersListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this._parametersListView.HideSelection = false;
			this._parametersListView.LabelEdit = true;
			this._parametersListView.Location = new Point(0, 18);
			this._parametersListView.MultiSelect = false;
			this._parametersListView.Name = "_parametersListView";
			this._parametersListView.Size = new Size(252, 224);
			this._parametersListView.TabIndex = 20;
			this._parametersListView.View = System.Windows.Forms.View.Details;
			this._parametersListView.SelectedIndexChanged += this.OnParametersListViewSelectedIndexChanged;
			this._parametersListView.AfterLabelEdit += this.OnParametersListViewAfterLabelEdit;
			this._nameColumnHeader.Width = 85;
			this._valueColumnHeader.Width = 134;
			this._addButtonPanel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this._addButtonPanel.Controls.Add(this._addParameterButton);
			this._addButtonPanel.Location = new Point(0, 248);
			this._addButtonPanel.Name = "_addButtonPanel";
			this._addButtonPanel.Size = new Size(252, 30);
			this._addButtonPanel.TabIndex = 30;
			this._addParameterButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this._addParameterButton.AutoSize = true;
			this._addParameterButton.Location = new Point(124, 0);
			this._addParameterButton.Name = "_addParameterButton";
			this._addParameterButton.Size = new Size(128, 23);
			this._addParameterButton.TabIndex = 10;
			this._addParameterButton.Click += this.OnAddParameterButtonClick;
			this._moveUpButton.Location = new Point(258, 18);
			this._moveUpButton.Name = "_moveUpButton";
			this._moveUpButton.Size = new Size(26, 23);
			this._moveUpButton.TabIndex = 40;
			this._moveUpButton.Click += this.OnMoveUpButtonClick;
			this._moveDownButton.Location = new Point(258, 42);
			this._moveDownButton.Name = "_moveDownButton";
			this._moveDownButton.Size = new Size(26, 23);
			this._moveDownButton.TabIndex = 50;
			this._moveDownButton.Click += this.OnMoveDownButtonClick;
			this._deleteParameterButton.Location = new Point(258, 71);
			this._deleteParameterButton.Name = "_deleteParameterButton";
			this._deleteParameterButton.Size = new Size(26, 23);
			this._deleteParameterButton.TabIndex = 60;
			this._deleteParameterButton.Click += this.OnDeleteParameterButtonClick;
			this._sourceLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._sourceLabel.Location = new Point(292, 0);
			this._sourceLabel.Name = "_sourceLabel";
			this._sourceLabel.Size = new Size(300, 16);
			this._sourceLabel.TabIndex = 70;
			this._parameterTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this._parameterTypeComboBox.Location = new Point(292, 18);
			this._parameterTypeComboBox.Name = "_parameterTypeComboBox";
			this._parameterTypeComboBox.Size = new Size(163, 21);
			this._parameterTypeComboBox.TabIndex = 80;
			this._parameterTypeComboBox.SelectedIndexChanged += this.OnParameterTypeComboBoxSelectedIndexChanged;
			this._editorPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._editorPanel.Location = new Point(292, 47);
			this._editorPanel.Name = "_editorPanel";
			this._editorPanel.Size = new Size(308, 235);
			this._editorPanel.TabIndex = 90;
			base.Controls.Add(this._editorPanel);
			base.Controls.Add(this._addButtonPanel);
			base.Controls.Add(this._deleteParameterButton);
			base.Controls.Add(this._moveDownButton);
			base.Controls.Add(this._moveUpButton);
			base.Controls.Add(this._parameterTypeComboBox);
			base.Controls.Add(this._parametersListView);
			base.Controls.Add(this._sourceLabel);
			base.Controls.Add(this._parametersLabel);
			this.MinimumSize = new Size(460, 126);
			base.Name = "ParameterEditorUserControl";
			base.Size = new Size(600, 280);
			this._addButtonPanel.ResumeLayout(false);
			this._addButtonPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00030EAC File Offset: 0x0002F0AC
		private void InitializeParameterEditors()
		{
			this._advancedParameterEditor = new ParameterEditorUserControl.AdvancedParameterEditor(this._serviceProvider, this._control);
			this._advancedParameterEditor.RequestModeChange += this.ToggleAdvancedMode;
			this._advancedParameterEditor.ParameterChanged += this.OnParametersChanged;
			this._advancedParameterEditor.Visible = false;
			this._editorPanel.Controls.Add(this._advancedParameterEditor);
			this._staticParameterEditor = new ParameterEditorUserControl.StaticParameterEditor(this._serviceProvider);
			this._staticParameterEditor.RequestModeChange += this.ToggleAdvancedMode;
			this._staticParameterEditor.ParameterChanged += this.OnParametersChanged;
			this._staticParameterEditor.Visible = false;
			this._editorPanel.Controls.Add(this._staticParameterEditor);
			this._controlParameterEditor = new ParameterEditorUserControl.ControlParameterEditor(this._serviceProvider, this._control);
			this._controlParameterEditor.RequestModeChange += this.ToggleAdvancedMode;
			this._controlParameterEditor.ParameterChanged += this.OnParametersChanged;
			this._controlParameterEditor.Visible = false;
			this._editorPanel.Controls.Add(this._controlParameterEditor);
			this._formParameterEditor = new ParameterEditorUserControl.FormParameterEditor(this._serviceProvider);
			this._formParameterEditor.RequestModeChange += this.ToggleAdvancedMode;
			this._formParameterEditor.ParameterChanged += this.OnParametersChanged;
			this._formParameterEditor.Visible = false;
			this._editorPanel.Controls.Add(this._formParameterEditor);
			this._queryStringParameterEditor = new ParameterEditorUserControl.QueryStringParameterEditor(this._serviceProvider);
			this._queryStringParameterEditor.RequestModeChange += this.ToggleAdvancedMode;
			this._queryStringParameterEditor.ParameterChanged += this.OnParametersChanged;
			this._queryStringParameterEditor.Visible = false;
			this._editorPanel.Controls.Add(this._queryStringParameterEditor);
			this._routeParameterEditor = new ParameterEditorUserControl.RouteParameterEditor(this._serviceProvider);
			this._routeParameterEditor.RequestModeChange += this.ToggleAdvancedMode;
			this._routeParameterEditor.ParameterChanged += this.OnParametersChanged;
			this._routeParameterEditor.Visible = false;
			this._editorPanel.Controls.Add(this._routeParameterEditor);
			this._cookieParameterEditor = new ParameterEditorUserControl.CookieParameterEditor(this._serviceProvider);
			this._cookieParameterEditor.RequestModeChange += this.ToggleAdvancedMode;
			this._cookieParameterEditor.ParameterChanged += this.OnParametersChanged;
			this._cookieParameterEditor.Visible = false;
			this._editorPanel.Controls.Add(this._cookieParameterEditor);
			this._sessionParameterEditor = new ParameterEditorUserControl.SessionParameterEditor(this._serviceProvider);
			this._sessionParameterEditor.RequestModeChange += this.ToggleAdvancedMode;
			this._sessionParameterEditor.ParameterChanged += this.OnParametersChanged;
			this._sessionParameterEditor.Visible = false;
			this._editorPanel.Controls.Add(this._sessionParameterEditor);
			this._profileParameterEditor = new ParameterEditorUserControl.ProfileParameterEditor(this._serviceProvider);
			this._profileParameterEditor.RequestModeChange += this.ToggleAdvancedMode;
			this._profileParameterEditor.ParameterChanged += this.OnParametersChanged;
			this._profileParameterEditor.Visible = false;
			this._editorPanel.Controls.Add(this._profileParameterEditor);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00031238 File Offset: 0x0002F438
		private void InitializeUI()
		{
			this._parametersLabel.Text = SR.GetString("ParameterEditorUserControl_ParametersLabel");
			this._nameColumnHeader.Text = SR.GetString("ParameterEditorUserControl_ParameterNameColumnHeader");
			this._valueColumnHeader.Text = SR.GetString("ParameterEditorUserControl_ParameterValueColumnHeader");
			this._addParameterButton.Text = SR.GetString("ParameterEditorUserControl_AddButton");
			this._sourceLabel.Text = SR.GetString("ParameterEditorUserControl_SourceLabel");
			Icon icon = BitmapSelector.CreateIcon(typeof(ParameterEditorUserControl), "SortUp.ico");
			Bitmap bitmap = icon.ToBitmap();
			bitmap.MakeTransparent();
			this._moveUpButton.Image = bitmap;
			Icon icon2 = BitmapSelector.CreateIcon(typeof(ParameterEditorUserControl), "SortDown.ico");
			Bitmap bitmap2 = icon2.ToBitmap();
			bitmap2.MakeTransparent();
			this._moveDownButton.Image = bitmap2;
			Icon icon3 = BitmapSelector.CreateIcon(typeof(ParameterEditorUserControl), "Delete.ico");
			Bitmap bitmap3 = icon3.ToBitmap();
			bitmap3.MakeTransparent();
			this._deleteParameterButton.Image = bitmap3;
			this._moveUpButton.AccessibleName = SR.GetString("ParameterEditorUserControl_MoveParameterUp");
			this._moveDownButton.AccessibleName = SR.GetString("ParameterEditorUserControl_MoveParameterDown");
			this._deleteParameterButton.AccessibleName = SR.GetString("ParameterEditorUserControl_DeleteParameter");
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0003137C File Offset: 0x0002F57C
		private void AddParameter(Parameter parameter)
		{
			try
			{
				this.IgnoreParameterChanges(true);
				ParameterEditorUserControl.ParameterListViewItem parameterListViewItem = new ParameterEditorUserControl.ParameterListViewItem(parameter);
				this._parametersListView.BeginUpdate();
				try
				{
					this._parametersListView.Items.Add(parameterListViewItem);
					parameterListViewItem.Selected = true;
					parameterListViewItem.Focused = true;
					parameterListViewItem.EnsureVisible();
					this._parametersListView.Focus();
				}
				finally
				{
					this._parametersListView.EndUpdate();
				}
				parameterListViewItem.Refresh();
				parameterListViewItem.BeginEdit();
			}
			finally
			{
				this.IgnoreParameterChanges(false);
			}
			this.OnParametersChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00031420 File Offset: 0x0002F620
		public void AddParameters(Parameter[] parameters)
		{
			try
			{
				this.IgnoreParameterChanges(true);
				this._parametersListView.BeginUpdate();
				ArrayList arrayList = new ArrayList();
				try
				{
					foreach (Parameter parameter in parameters)
					{
						ParameterEditorUserControl.ParameterListViewItem value = new ParameterEditorUserControl.ParameterListViewItem(parameter);
						this._parametersListView.Items.Add(value);
						arrayList.Add(value);
					}
					if (this._parametersListView.Items.Count > 0)
					{
						this._parametersListView.Items[0].Selected = true;
						this._parametersListView.Items[0].Focused = true;
						this._parametersListView.Items[0].EnsureVisible();
					}
					this._parametersListView.Focus();
				}
				finally
				{
					this._parametersListView.EndUpdate();
				}
				foreach (object obj in arrayList)
				{
					ParameterEditorUserControl.ParameterListViewItem parameterListViewItem = (ParameterEditorUserControl.ParameterListViewItem)obj;
					parameterListViewItem.Refresh();
				}
			}
			finally
			{
				this.IgnoreParameterChanges(false);
			}
			this.OnParametersChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00031590 File Offset: 0x0002F790
		public void ClearParameters()
		{
			try
			{
				this.IgnoreParameterChanges(true);
				this._parametersListView.Items.Clear();
				this.UpdateUI(false);
			}
			finally
			{
				this.IgnoreParameterChanges(false);
			}
			this.OnParametersChanged(this, EventArgs.Empty);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x000315E4 File Offset: 0x0002F7E4
		internal static string GetControlDefaultValuePropertyName(string controlID, IServiceProvider serviceProvider, Control control)
		{
			Control control2 = ControlHelper.FindControl(serviceProvider, control, controlID);
			if (control2 != null)
			{
				return ParameterEditorUserControl.GetDefaultValuePropertyName(control2);
			}
			return string.Empty;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0003160C File Offset: 0x0002F80C
		private static string GetDefaultValuePropertyName(Control control)
		{
			ControlValuePropertyAttribute controlValuePropertyAttribute = (ControlValuePropertyAttribute)TypeDescriptor.GetAttributes(control)[typeof(ControlValuePropertyAttribute)];
			if (controlValuePropertyAttribute != null && !string.IsNullOrEmpty(controlValuePropertyAttribute.Name))
			{
				return controlValuePropertyAttribute.Name;
			}
			return string.Empty;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00031650 File Offset: 0x0002F850
		internal static string GetParameterExpression(IServiceProvider serviceProvider, Parameter p, Control control, out bool isHelperText)
		{
			if (p.GetType() == typeof(ControlParameter))
			{
				ControlParameter controlParameter = (ControlParameter)p;
				if (controlParameter.ControlID.Length == 0)
				{
					isHelperText = true;
					return SR.GetString("ParameterEditorUserControl_ControlParameterExpressionUnknown");
				}
				string text = controlParameter.PropertyName;
				if (text.Length == 0)
				{
					text = ParameterEditorUserControl.GetControlDefaultValuePropertyName(controlParameter.ControlID, serviceProvider, control);
				}
				if (text.Length > 0)
				{
					isHelperText = false;
					return controlParameter.ControlID + "." + text;
				}
				isHelperText = true;
				return SR.GetString("ParameterEditorUserControl_ControlParameterExpressionUnknown");
			}
			else if (p.GetType() == typeof(FormParameter))
			{
				FormParameter formParameter = (FormParameter)p;
				if (formParameter.FormField.Length > 0)
				{
					isHelperText = false;
					return string.Format(CultureInfo.InvariantCulture, "Request.Form(\"{0}\")", new object[]
					{
						formParameter.FormField
					});
				}
				isHelperText = true;
				return SR.GetString("ParameterEditorUserControl_FormParameterExpressionUnknown");
			}
			else if (p.GetType() == typeof(QueryStringParameter))
			{
				QueryStringParameter queryStringParameter = (QueryStringParameter)p;
				if (queryStringParameter.QueryStringField.Length > 0)
				{
					isHelperText = false;
					return string.Format(CultureInfo.InvariantCulture, "Request.QueryString(\"{0}\")", new object[]
					{
						queryStringParameter.QueryStringField
					});
				}
				isHelperText = true;
				return SR.GetString("ParameterEditorUserControl_QueryStringParameterExpressionUnknown");
			}
			else if (p.GetType() == typeof(RouteParameter))
			{
				RouteParameter routeParameter = (RouteParameter)p;
				if (routeParameter.RouteKey.Length > 0)
				{
					isHelperText = false;
					return string.Format(CultureInfo.InvariantCulture, "Page.RouteData(\"{0}\")", new object[]
					{
						routeParameter.RouteKey
					});
				}
				isHelperText = true;
				return SR.GetString("ParameterEditorUserControl_RouteParameterExpressionUnknown");
			}
			else if (p.GetType() == typeof(CookieParameter))
			{
				CookieParameter cookieParameter = (CookieParameter)p;
				if (cookieParameter.CookieName.Length > 0)
				{
					isHelperText = false;
					return string.Format(CultureInfo.InvariantCulture, "Request.Cookies(\"{0}\").Value", new object[]
					{
						cookieParameter.CookieName
					});
				}
				isHelperText = true;
				return SR.GetString("ParameterEditorUserControl_CookieParameterExpressionUnknown");
			}
			else if (p.GetType() == typeof(SessionParameter))
			{
				SessionParameter sessionParameter = (SessionParameter)p;
				if (sessionParameter.SessionField.Length > 0)
				{
					isHelperText = false;
					return string.Format(CultureInfo.InvariantCulture, "Session(\"{0}\")", new object[]
					{
						sessionParameter.SessionField
					});
				}
				isHelperText = true;
				return SR.GetString("ParameterEditorUserControl_SessionParameterExpressionUnknown");
			}
			else if (p.GetType() == typeof(ProfileParameter))
			{
				ProfileParameter profileParameter = (ProfileParameter)p;
				if (profileParameter.PropertyName.Length > 0)
				{
					isHelperText = false;
					return string.Format(CultureInfo.InvariantCulture, "Profile(\"{0}\")", new object[]
					{
						profileParameter.PropertyName
					});
				}
				isHelperText = true;
				return SR.GetString("ParameterEditorUserControl_ProfileParameterExpressionUnknown");
			}
			else
			{
				if (!(p.GetType() == typeof(Parameter)))
				{
					isHelperText = true;
					return p.GetType().Name;
				}
				if (p.DefaultValue == null)
				{
					isHelperText = false;
					return string.Empty;
				}
				isHelperText = false;
				return p.DefaultValue;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00031958 File Offset: 0x0002FB58
		public Parameter[] GetParameters()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._parametersListView.Items)
			{
				ParameterEditorUserControl.ParameterListViewItem parameterListViewItem = (ParameterEditorUserControl.ParameterListViewItem)obj;
				if (parameterListViewItem.Parameter != null)
				{
					arrayList.Add(parameterListViewItem.Parameter);
				}
			}
			return (Parameter[])arrayList.ToArray(typeof(Parameter));
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000319E0 File Offset: 0x0002FBE0
		private void IgnoreParameterChanges(bool ignoreChanges)
		{
			this._ignoreParameterChangesCount += (ignoreChanges ? 1 : -1);
			if (this._ignoreParameterChangesCount == 0)
			{
				this.UpdateUI(false);
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00031A05 File Offset: 0x0002FC05
		private void OnAddParameterButtonClick(object sender, EventArgs e)
		{
			this.AddParameter(new Parameter("newparameter"));
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00031A18 File Offset: 0x0002FC18
		private void OnDeleteParameterButtonClick(object sender, EventArgs e)
		{
			try
			{
				this.IgnoreParameterChanges(true);
				if (this._parametersListView.SelectedItems.Count == 0)
				{
					return;
				}
				int num = this._parametersListView.SelectedIndices[0];
				this._parametersListView.BeginUpdate();
				try
				{
					this._parametersListView.Items.RemoveAt(num);
					if (num < this._parametersListView.Items.Count)
					{
						this._parametersListView.Items[num].Selected = true;
						this._parametersListView.Items[num].Focused = true;
						this._parametersListView.Items[num].EnsureVisible();
						this._parametersListView.Focus();
					}
					else if (this._parametersListView.Items.Count > 0)
					{
						num = this._parametersListView.Items.Count - 1;
						this._parametersListView.Items[num].Selected = true;
						this._parametersListView.Items[num].Focused = true;
						this._parametersListView.Items[num].EnsureVisible();
						this._parametersListView.Focus();
					}
				}
				finally
				{
					this._parametersListView.EndUpdate();
				}
				this.UpdateUI(false);
			}
			finally
			{
				this.IgnoreParameterChanges(false);
			}
			this.OnParametersChanged(this, EventArgs.Empty);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00031BB0 File Offset: 0x0002FDB0
		private void OnMoveDownButtonClick(object sender, EventArgs e)
		{
			try
			{
				this.IgnoreParameterChanges(true);
				if (this._parametersListView.SelectedItems.Count == 0)
				{
					return;
				}
				int num = this._parametersListView.SelectedIndices[0];
				if (num == this._parametersListView.Items.Count - 1)
				{
					return;
				}
				this._parametersListView.BeginUpdate();
				try
				{
					ListViewItem listViewItem = this._parametersListView.Items[num];
					listViewItem.Remove();
					this._parametersListView.Items.Insert(num + 1, listViewItem);
					listViewItem.Selected = true;
					listViewItem.Focused = true;
					listViewItem.EnsureVisible();
					this._parametersListView.Focus();
				}
				finally
				{
					this._parametersListView.EndUpdate();
				}
			}
			finally
			{
				this.IgnoreParameterChanges(false);
			}
			this.OnParametersChanged(this, EventArgs.Empty);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00031C9C File Offset: 0x0002FE9C
		private void OnMoveUpButtonClick(object sender, EventArgs e)
		{
			try
			{
				this.IgnoreParameterChanges(true);
				if (this._parametersListView.SelectedItems.Count == 0)
				{
					return;
				}
				int num = this._parametersListView.SelectedIndices[0];
				if (num == 0)
				{
					return;
				}
				this._parametersListView.BeginUpdate();
				try
				{
					ListViewItem listViewItem = this._parametersListView.Items[num];
					listViewItem.Remove();
					this._parametersListView.Items.Insert(num - 1, listViewItem);
					listViewItem.Selected = true;
					listViewItem.Focused = true;
					listViewItem.EnsureVisible();
					this._parametersListView.Focus();
				}
				finally
				{
					this._parametersListView.EndUpdate();
				}
			}
			finally
			{
				this.IgnoreParameterChanges(false);
			}
			this.OnParametersChanged(this, EventArgs.Empty);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00031D74 File Offset: 0x0002FF74
		protected virtual void OnParametersChanged(object sender, EventArgs e)
		{
			if (this._ignoreParameterChangesCount > 0)
			{
				return;
			}
			EventHandler eventHandler = base.Events[ParameterEditorUserControl.EventParametersChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00031DB0 File Offset: 0x0002FFB0
		private void OnParametersListViewAfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (e.Label == null || e.Label.Trim().Length == 0)
			{
				e.CancelEdit = true;
				return;
			}
			ParameterEditorUserControl.ParameterListViewItem parameterListViewItem = (ParameterEditorUserControl.ParameterListViewItem)this._parametersListView.Items[e.Item];
			parameterListViewItem.ParameterName = e.Label;
			this.UpdateUI(false);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00031E0E File Offset: 0x0003000E
		private void OnParametersListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateUI(false);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00031E18 File Offset: 0x00030018
		private void OnParameterTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.IgnoreParameterChanges(true);
				if (this._parametersListView.SelectedItems.Count == 0)
				{
					return;
				}
				ParameterEditorUserControl.ParameterListViewItem parameterListViewItem = (ParameterEditorUserControl.ParameterListViewItem)this._parametersListView.SelectedItems[0];
				string b = (string)this._parameterTypeComboBox.SelectedItem;
				Type type = null;
				foreach (object obj in this._parameterTypes)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if ((string)dictionaryEntry.Value == b)
					{
						type = (Type)dictionaryEntry.Key;
					}
				}
				if (type != null && (parameterListViewItem.Parameter == null || parameterListViewItem.Parameter.GetType() != type))
				{
					parameterListViewItem.Parameter = (Parameter)Activator.CreateInstance(type);
					parameterListViewItem.Refresh();
				}
				this.SetActiveEditParameterItem(parameterListViewItem, false);
			}
			finally
			{
				this.IgnoreParameterChanges(false);
			}
			this.OnParametersChanged(this, EventArgs.Empty);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00031F3C File Offset: 0x0003013C
		private void SetActiveEditParameterItem(ParameterEditorUserControl.ParameterListViewItem parameterItem, bool allowFocusChange)
		{
			if (parameterItem == null)
			{
				if (this._parameterEditor != null)
				{
					this._parameterEditor.Visible = false;
					this._parameterEditor = null;
					return;
				}
			}
			else
			{
				ParameterEditorUserControl.ParameterEditor parameterEditor = null;
				if (this._inAdvancedMode)
				{
					parameterEditor = this._advancedParameterEditor;
				}
				else if (parameterItem.Parameter != null)
				{
					if (parameterItem.Parameter.GetType() == typeof(Parameter))
					{
						parameterEditor = this._staticParameterEditor;
					}
					else if (parameterItem.Parameter.GetType() == typeof(ControlParameter))
					{
						parameterEditor = this._controlParameterEditor;
					}
					else if (parameterItem.Parameter.GetType() == typeof(FormParameter))
					{
						parameterEditor = this._formParameterEditor;
					}
					else if (parameterItem.Parameter.GetType() == typeof(QueryStringParameter))
					{
						parameterEditor = this._queryStringParameterEditor;
					}
					else if (parameterItem.Parameter.GetType() == typeof(CookieParameter))
					{
						parameterEditor = this._cookieParameterEditor;
					}
					else if (parameterItem.Parameter.GetType() == typeof(SessionParameter))
					{
						parameterEditor = this._sessionParameterEditor;
					}
					else if (parameterItem.Parameter.GetType() == typeof(ProfileParameter))
					{
						parameterEditor = this._profileParameterEditor;
					}
					else if (parameterItem.Parameter.GetType() == typeof(RouteParameter))
					{
						parameterEditor = this._routeParameterEditor;
					}
				}
				if (this._parameterEditor != parameterEditor)
				{
					if (this._parameterEditor != null)
					{
						this._parameterEditor.Visible = false;
					}
					this._parameterEditor = parameterEditor;
				}
				if (this._parameterEditor != null)
				{
					this._parameterEditor.InitializeParameter(parameterItem);
					this._parameterEditor.Visible = true;
					if (allowFocusChange)
					{
						this._parameterEditor.SetDefaultFocus();
					}
				}
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00032110 File Offset: 0x00030310
		public void SetAllowCollectionChanges(bool allowChanges)
		{
			this._moveUpButton.Visible = allowChanges;
			this._moveDownButton.Visible = allowChanges;
			this._deleteParameterButton.Visible = allowChanges;
			this._addParameterButton.Visible = allowChanges;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00032142 File Offset: 0x00030342
		private void ToggleAdvancedMode(object sender, EventArgs e)
		{
			this._inAdvancedMode = !this._inAdvancedMode;
			this.UpdateUI(true);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0003215C File Offset: 0x0003035C
		private void UpdateUI(bool allowFocusChange)
		{
			if (this._parametersListView.SelectedItems.Count > 0)
			{
				ParameterEditorUserControl.ParameterListViewItem parameterListViewItem = (ParameterEditorUserControl.ParameterListViewItem)this._parametersListView.SelectedItems[0];
				this._deleteParameterButton.Enabled = true;
				this._moveUpButton.Enabled = (this._parametersListView.SelectedIndices[0] > 0);
				this._moveDownButton.Enabled = (this._parametersListView.SelectedIndices[0] < this._parametersListView.Items.Count - 1);
				this._sourceLabel.Enabled = true;
				this._parameterTypeComboBox.Enabled = true;
				this._editorPanel.Enabled = true;
				if (parameterListViewItem.Parameter == null)
				{
					this._parameterTypeComboBox.SelectedIndex = -1;
				}
				else
				{
					Type type = parameterListViewItem.Parameter.GetType();
					object obj = this._parameterTypes[type];
					if (obj != null)
					{
						this._parameterTypeComboBox.SelectedItem = obj;
					}
					else
					{
						this._parameterTypeComboBox.SelectedIndex = -1;
					}
				}
				this.SetActiveEditParameterItem(parameterListViewItem, allowFocusChange);
				return;
			}
			this._deleteParameterButton.Enabled = false;
			this._moveUpButton.Enabled = false;
			this._moveDownButton.Enabled = false;
			this._sourceLabel.Enabled = false;
			this._parameterTypeComboBox.Enabled = false;
			this._parameterTypeComboBox.SelectedIndex = -1;
			this._editorPanel.Enabled = false;
			this.SetActiveEditParameterItem(null, false);
		}

		// Token: 0x040004F8 RID: 1272
		private static readonly object EventParametersChanged = new object();

		// Token: 0x040004F9 RID: 1273
		private System.Windows.Forms.Label _parametersLabel;

		// Token: 0x040004FA RID: 1274
		private ListView _parametersListView;

		// Token: 0x040004FB RID: 1275
		private AutoSizeComboBox _parameterTypeComboBox;

		// Token: 0x040004FC RID: 1276
		private ColumnHeader _nameColumnHeader;

		// Token: 0x040004FD RID: 1277
		private ColumnHeader _valueColumnHeader;

		// Token: 0x040004FE RID: 1278
		private System.Windows.Forms.Button _moveUpButton;

		// Token: 0x040004FF RID: 1279
		private System.Windows.Forms.Button _moveDownButton;

		// Token: 0x04000500 RID: 1280
		private System.Windows.Forms.Button _deleteParameterButton;

		// Token: 0x04000501 RID: 1281
		private System.Windows.Forms.Button _addParameterButton;

		// Token: 0x04000502 RID: 1282
		private System.Windows.Forms.Panel _addButtonPanel;

		// Token: 0x04000503 RID: 1283
		private System.Windows.Forms.Label _sourceLabel;

		// Token: 0x04000504 RID: 1284
		private System.Windows.Forms.Panel _editorPanel;

		// Token: 0x04000505 RID: 1285
		private ListDictionary _parameterTypes;

		// Token: 0x04000506 RID: 1286
		private IServiceProvider _serviceProvider;

		// Token: 0x04000507 RID: 1287
		private ParameterEditorUserControl.ParameterEditor _parameterEditor;

		// Token: 0x04000508 RID: 1288
		private bool _inAdvancedMode;

		// Token: 0x04000509 RID: 1289
		private int _ignoreParameterChangesCount;

		// Token: 0x0400050A RID: 1290
		private ParameterEditorUserControl.AdvancedParameterEditor _advancedParameterEditor;

		// Token: 0x0400050B RID: 1291
		private ParameterEditorUserControl.ControlParameterEditor _controlParameterEditor;

		// Token: 0x0400050C RID: 1292
		private ParameterEditorUserControl.CookieParameterEditor _cookieParameterEditor;

		// Token: 0x0400050D RID: 1293
		private ParameterEditorUserControl.FormParameterEditor _formParameterEditor;

		// Token: 0x0400050E RID: 1294
		private ParameterEditorUserControl.QueryStringParameterEditor _queryStringParameterEditor;

		// Token: 0x0400050F RID: 1295
		private ParameterEditorUserControl.SessionParameterEditor _sessionParameterEditor;

		// Token: 0x04000510 RID: 1296
		private ParameterEditorUserControl.StaticParameterEditor _staticParameterEditor;

		// Token: 0x04000511 RID: 1297
		private ParameterEditorUserControl.ProfileParameterEditor _profileParameterEditor;

		// Token: 0x04000512 RID: 1298
		private ParameterEditorUserControl.RouteParameterEditor _routeParameterEditor;

		// Token: 0x04000513 RID: 1299
		private TypeDescriptionProvider _provider;

		// Token: 0x04000514 RID: 1300
		private Control _control;

		// Token: 0x02000417 RID: 1047
		internal sealed class ControlItem
		{
			// Token: 0x06002819 RID: 10265 RVA: 0x000F502A File Offset: 0x000F322A
			public ControlItem(string controlID, string propertyName)
			{
				this._controlID = controlID;
				this._propertyName = propertyName;
			}

			// Token: 0x17000867 RID: 2151
			// (get) Token: 0x0600281A RID: 10266 RVA: 0x000F5040 File Offset: 0x000F3240
			public string ControlID
			{
				get
				{
					return this._controlID;
				}
			}

			// Token: 0x17000868 RID: 2152
			// (get) Token: 0x0600281B RID: 10267 RVA: 0x000F5048 File Offset: 0x000F3248
			public string PropertyName
			{
				get
				{
					return this._propertyName;
				}
			}

			// Token: 0x0600281C RID: 10268 RVA: 0x000F5050 File Offset: 0x000F3250
			private static bool IsValidComponent(IComponent component)
			{
				Control control = component as Control;
				return control != null && !string.IsNullOrEmpty(control.ID);
			}

			// Token: 0x0600281D RID: 10269 RVA: 0x000F507C File Offset: 0x000F327C
			public static ParameterEditorUserControl.ControlItem[] GetControlItems(IDesignerHost host, Control control)
			{
				IList<IComponent> allComponents = ControlHelper.GetAllComponents(control, new ControlHelper.IsValidComponentDelegate(ParameterEditorUserControl.ControlItem.IsValidComponent));
				List<ParameterEditorUserControl.ControlItem> list = new List<ParameterEditorUserControl.ControlItem>();
				foreach (IComponent component in allComponents)
				{
					Control control2 = (Control)component;
					string defaultValuePropertyName = ParameterEditorUserControl.GetDefaultValuePropertyName(control2);
					if (!string.IsNullOrEmpty(defaultValuePropertyName))
					{
						list.Add(new ParameterEditorUserControl.ControlItem(control2.ID, defaultValuePropertyName));
					}
				}
				return list.ToArray();
			}

			// Token: 0x0600281E RID: 10270 RVA: 0x000F5040 File Offset: 0x000F3240
			public override string ToString()
			{
				return this._controlID;
			}

			// Token: 0x04001C90 RID: 7312
			private string _controlID;

			// Token: 0x04001C91 RID: 7313
			private string _propertyName;
		}

		// Token: 0x02000418 RID: 1048
		private class ParameterListViewItem : ListViewItem
		{
			// Token: 0x0600281F RID: 10271 RVA: 0x000F5108 File Offset: 0x000F3308
			public ParameterListViewItem(Parameter parameter)
			{
				this._parameter = parameter;
				this._isConfigured = true;
			}

			// Token: 0x17000869 RID: 2153
			// (get) Token: 0x06002820 RID: 10272 RVA: 0x000F511E File Offset: 0x000F331E
			// (set) Token: 0x06002821 RID: 10273 RVA: 0x000F512B File Offset: 0x000F332B
			public DbType DbType
			{
				get
				{
					return this._parameter.DbType;
				}
				set
				{
					this._parameter.DbType = value;
				}
			}

			// Token: 0x1700086A RID: 2154
			// (get) Token: 0x06002822 RID: 10274 RVA: 0x000F5139 File Offset: 0x000F3339
			public bool IsConfigured
			{
				get
				{
					return this._isConfigured;
				}
			}

			// Token: 0x1700086B RID: 2155
			// (get) Token: 0x06002823 RID: 10275 RVA: 0x000F5141 File Offset: 0x000F3341
			// (set) Token: 0x06002824 RID: 10276 RVA: 0x000F514E File Offset: 0x000F334E
			public string ParameterName
			{
				get
				{
					return this._parameter.Name;
				}
				set
				{
					this._parameter.Name = value;
				}
			}

			// Token: 0x1700086C RID: 2156
			// (get) Token: 0x06002825 RID: 10277 RVA: 0x000F515C File Offset: 0x000F335C
			// (set) Token: 0x06002826 RID: 10278 RVA: 0x000F5169 File Offset: 0x000F3369
			public TypeCode ParameterType
			{
				get
				{
					return this._parameter.Type;
				}
				set
				{
					this._parameter.Type = value;
				}
			}

			// Token: 0x1700086D RID: 2157
			// (get) Token: 0x06002827 RID: 10279 RVA: 0x000F5177 File Offset: 0x000F3377
			// (set) Token: 0x06002828 RID: 10280 RVA: 0x000F5180 File Offset: 0x000F3380
			public Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
				set
				{
					string defaultValue = this._parameter.DefaultValue;
					ParameterDirection direction = this._parameter.Direction;
					string name = this._parameter.Name;
					bool convertEmptyStringToNull = this._parameter.ConvertEmptyStringToNull;
					int size = this._parameter.Size;
					TypeCode type = this._parameter.Type;
					DbType dbType = this._parameter.DbType;
					this._parameter = value;
					this._parameter.DefaultValue = defaultValue;
					this._parameter.Direction = direction;
					this._parameter.Name = name;
					this._parameter.ConvertEmptyStringToNull = convertEmptyStringToNull;
					this._parameter.Size = size;
					this._parameter.Type = type;
					this._parameter.DbType = dbType;
				}
			}

			// Token: 0x06002829 RID: 10281 RVA: 0x000F5244 File Offset: 0x000F3444
			public void Refresh()
			{
				base.SubItems.Clear();
				base.Text = this.ParameterName;
				base.UseItemStyleForSubItems = false;
				ListView listView = base.ListView;
				IServiceProvider serviceProvider = null;
				Control control = null;
				if (listView != null)
				{
					ParameterEditorUserControl parameterEditorUserControl = (ParameterEditorUserControl)listView.Parent;
					serviceProvider = parameterEditorUserControl._serviceProvider;
					control = parameterEditorUserControl._control;
				}
				bool flag;
				string parameterExpression = ParameterEditorUserControl.GetParameterExpression(serviceProvider, this._parameter, control, out flag);
				this._isConfigured = !flag;
				ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem();
				listViewSubItem.Text = parameterExpression;
				if (flag)
				{
					listViewSubItem.ForeColor = SystemColors.GrayText;
				}
				base.SubItems.Add(listViewSubItem);
			}

			// Token: 0x04001C92 RID: 7314
			private Parameter _parameter;

			// Token: 0x04001C93 RID: 7315
			private bool _isConfigured;
		}

		// Token: 0x02000419 RID: 1049
		private class PropertyGridSite : ISite, IServiceProvider
		{
			// Token: 0x0600282A RID: 10282 RVA: 0x000F52E2 File Offset: 0x000F34E2
			public PropertyGridSite(IServiceProvider sp, IComponent comp)
			{
				this._sp = sp;
				this._comp = comp;
			}

			// Token: 0x1700086E RID: 2158
			// (get) Token: 0x0600282B RID: 10283 RVA: 0x000F52F8 File Offset: 0x000F34F8
			public IComponent Component
			{
				get
				{
					return this._comp;
				}
			}

			// Token: 0x1700086F RID: 2159
			// (get) Token: 0x0600282C RID: 10284 RVA: 0x00003598 File Offset: 0x00001798
			public IContainer Container
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000870 RID: 2160
			// (get) Token: 0x0600282D RID: 10285 RVA: 0x0000445B File Offset: 0x0000265B
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000871 RID: 2161
			// (get) Token: 0x0600282E RID: 10286 RVA: 0x00003598 File Offset: 0x00001798
			// (set) Token: 0x0600282F RID: 10287 RVA: 0x00003937 File Offset: 0x00001B37
			public string Name
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x06002830 RID: 10288 RVA: 0x000F5300 File Offset: 0x000F3500
			public object GetService(Type t)
			{
				if (!this._inGetService && this._sp != null)
				{
					try
					{
						this._inGetService = true;
						return this._sp.GetService(t);
					}
					finally
					{
						this._inGetService = false;
					}
				}
				return null;
			}

			// Token: 0x04001C94 RID: 7316
			private IServiceProvider _sp;

			// Token: 0x04001C95 RID: 7317
			private IComponent _comp;

			// Token: 0x04001C96 RID: 7318
			private bool _inGetService;
		}

		// Token: 0x0200041A RID: 1050
		private abstract class ParameterEditor : System.Windows.Forms.Panel
		{
			// Token: 0x06002831 RID: 10289 RVA: 0x000F5350 File Offset: 0x000F3550
			protected ParameterEditor(IServiceProvider serviceProvider)
			{
				this._serviceProvider = serviceProvider;
			}

			// Token: 0x17000872 RID: 2162
			// (get) Token: 0x06002832 RID: 10290 RVA: 0x000F535F File Offset: 0x000F355F
			protected ParameterEditorUserControl.ParameterListViewItem ParameterItem
			{
				get
				{
					return this._parameterItem;
				}
			}

			// Token: 0x17000873 RID: 2163
			// (get) Token: 0x06002833 RID: 10291 RVA: 0x000F5367 File Offset: 0x000F3567
			protected IServiceProvider ServiceProvider
			{
				get
				{
					return this._serviceProvider;
				}
			}

			// Token: 0x14000067 RID: 103
			// (add) Token: 0x06002834 RID: 10292 RVA: 0x000F536F File Offset: 0x000F356F
			// (remove) Token: 0x06002835 RID: 10293 RVA: 0x000F5382 File Offset: 0x000F3582
			public event EventHandler ParameterChanged
			{
				add
				{
					base.Events.AddHandler(ParameterEditorUserControl.ParameterEditor.EventParameterChanged, value);
				}
				remove
				{
					base.Events.RemoveHandler(ParameterEditorUserControl.ParameterEditor.EventParameterChanged, value);
				}
			}

			// Token: 0x14000068 RID: 104
			// (add) Token: 0x06002836 RID: 10294 RVA: 0x000F5395 File Offset: 0x000F3595
			// (remove) Token: 0x06002837 RID: 10295 RVA: 0x000F53A8 File Offset: 0x000F35A8
			public event EventHandler RequestModeChange
			{
				add
				{
					base.Events.AddHandler(ParameterEditorUserControl.ParameterEditor.EventRequestModeChange, value);
				}
				remove
				{
					base.Events.RemoveHandler(ParameterEditorUserControl.ParameterEditor.EventRequestModeChange, value);
				}
			}

			// Token: 0x06002838 RID: 10296 RVA: 0x000F53BB File Offset: 0x000F35BB
			public virtual void InitializeParameter(ParameterEditorUserControl.ParameterListViewItem parameterItem)
			{
				this._parameterItem = parameterItem;
			}

			// Token: 0x06002839 RID: 10297 RVA: 0x000F53C4 File Offset: 0x000F35C4
			protected void OnParameterChanged()
			{
				this.ParameterItem.Refresh();
				EventHandler eventHandler = base.Events[ParameterEditorUserControl.ParameterEditor.EventParameterChanged] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}

			// Token: 0x0600283A RID: 10298 RVA: 0x000F5404 File Offset: 0x000F3604
			protected void OnRequestModeChange()
			{
				EventHandler eventHandler = base.Events[ParameterEditorUserControl.ParameterEditor.EventRequestModeChange] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}

			// Token: 0x0600283B RID: 10299 RVA: 0x00003937 File Offset: 0x00001B37
			public virtual void SetDefaultFocus()
			{
			}

			// Token: 0x04001C97 RID: 7319
			private static readonly object EventParameterChanged = new object();

			// Token: 0x04001C98 RID: 7320
			private static readonly object EventRequestModeChange = new object();

			// Token: 0x04001C99 RID: 7321
			private IServiceProvider _serviceProvider;

			// Token: 0x04001C9A RID: 7322
			private ParameterEditorUserControl.ParameterListViewItem _parameterItem;
		}

		// Token: 0x0200041B RID: 1051
		private sealed class AdvancedParameterEditor : ParameterEditorUserControl.ParameterEditor
		{
			// Token: 0x0600283D RID: 10301 RVA: 0x000F544C File Offset: 0x000F364C
			public AdvancedParameterEditor(IServiceProvider serviceProvider, Control control) : base(serviceProvider)
			{
				this._control = control;
				base.SuspendLayout();
				base.Size = new Size(400, 400);
				this._advancedlabel = new System.Windows.Forms.Label();
				this._parameterPropertyGrid = new VsPropertyGrid(base.ServiceProvider);
				this._hideAdvancedLinkLabel = new LinkLabel();
				this._advancedlabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._advancedlabel.Location = new Point(0, 0);
				this._advancedlabel.Size = new Size(400, 16);
				this._advancedlabel.TabIndex = 10;
				this._advancedlabel.Text = SR.GetString("ParameterEditorUserControl_AdvancedProperties");
				this._parameterPropertyGrid.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				this._parameterPropertyGrid.CommandsVisibleIfAvailable = true;
				this._parameterPropertyGrid.LargeButtons = false;
				this._parameterPropertyGrid.LineColor = SystemColors.ScrollBar;
				this._parameterPropertyGrid.Location = new Point(0, 18);
				this._parameterPropertyGrid.PropertySort = PropertySort.Alphabetical;
				this._parameterPropertyGrid.Site = new ParameterEditorUserControl.PropertyGridSite(base.ServiceProvider, this._parameterPropertyGrid);
				this._parameterPropertyGrid.Size = new Size(400, 356);
				this._parameterPropertyGrid.TabIndex = 20;
				this._parameterPropertyGrid.ToolbarVisible = false;
				this._parameterPropertyGrid.ViewBackColor = SystemColors.Window;
				this._parameterPropertyGrid.ViewForeColor = SystemColors.WindowText;
				this._parameterPropertyGrid.PropertyValueChanged += this.OnParameterPropertyGridPropertyValueChanged;
				this._hideAdvancedLinkLabel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				this._hideAdvancedLinkLabel.Location = new Point(0, 384);
				this._hideAdvancedLinkLabel.Size = new Size(400, 16);
				this._hideAdvancedLinkLabel.TabIndex = 30;
				this._hideAdvancedLinkLabel.TabStop = true;
				this._hideAdvancedLinkLabel.Text = SR.GetString("ParameterEditorUserControl_HideAdvancedPropertiesLabel");
				this._hideAdvancedLinkLabel.Links.Add(new LinkLabel.Link(0, this._hideAdvancedLinkLabel.Text.Length));
				this._hideAdvancedLinkLabel.LinkClicked += this.OnHideAdvancedLinkLabelLinkClicked;
				base.Controls.Add(this._advancedlabel);
				base.Controls.Add(this._parameterPropertyGrid);
				base.Controls.Add(this._hideAdvancedLinkLabel);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x0600283E RID: 10302 RVA: 0x000F56C1 File Offset: 0x000F38C1
			public override void InitializeParameter(ParameterEditorUserControl.ParameterListViewItem parameterItem)
			{
				base.InitializeParameter(parameterItem);
				this._parameterPropertyGrid.SelectedObject = base.ParameterItem.Parameter;
			}

			// Token: 0x0600283F RID: 10303 RVA: 0x000F56E0 File Offset: 0x000F38E0
			private void OnHideAdvancedLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				base.OnRequestModeChange();
			}

			// Token: 0x06002840 RID: 10304 RVA: 0x000F56E8 File Offset: 0x000F38E8
			private void OnParameterPropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
			{
				if (e.ChangedItem.PropertyDescriptor.Name == "ControlID")
				{
					ControlParameter controlParameter = base.ParameterItem.Parameter as ControlParameter;
					if (controlParameter != null && controlParameter.PropertyName.Length == 0 && controlParameter.ControlID != (string)e.OldValue)
					{
						controlParameter.PropertyName = ParameterEditorUserControl.GetControlDefaultValuePropertyName(controlParameter.ControlID, base.ServiceProvider, this._control);
					}
				}
				base.OnParameterChanged();
			}

			// Token: 0x06002841 RID: 10305 RVA: 0x000F576D File Offset: 0x000F396D
			public override void SetDefaultFocus()
			{
				this._parameterPropertyGrid.Focus();
			}

			// Token: 0x04001C9B RID: 7323
			private System.Windows.Forms.Label _advancedlabel;

			// Token: 0x04001C9C RID: 7324
			private PropertyGrid _parameterPropertyGrid;

			// Token: 0x04001C9D RID: 7325
			private LinkLabel _hideAdvancedLinkLabel;

			// Token: 0x04001C9E RID: 7326
			private Control _control;
		}

		// Token: 0x0200041C RID: 1052
		private sealed class ControlParameterEditor : ParameterEditorUserControl.ParameterEditor
		{
			// Token: 0x06002842 RID: 10306 RVA: 0x000F577C File Offset: 0x000F397C
			public ControlParameterEditor(IServiceProvider serviceProvider, Control control) : base(serviceProvider)
			{
				this._control = control;
				base.SuspendLayout();
				base.Size = new Size(400, 400);
				this._controlIDLabel = new System.Windows.Forms.Label();
				this._controlIDComboBox = new AutoSizeComboBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._showAdvancedLinkLabel = new LinkLabel();
				this._controlIDLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._controlIDLabel.Location = new Point(0, 0);
				this._controlIDLabel.Size = new Size(400, 16);
				this._controlIDLabel.TabIndex = 10;
				this._controlIDLabel.Text = SR.GetString("ParameterEditorUserControl_ControlParameterControlID");
				this._controlIDComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._controlIDComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				this._controlIDComboBox.Location = new Point(0, 18);
				this._controlIDComboBox.Size = new Size(400, 21);
				this._controlIDComboBox.Sorted = true;
				this._controlIDComboBox.TabIndex = 20;
				this._controlIDComboBox.SelectedIndexChanged += this.OnControlIDComboBoxSelectedIndexChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 45);
				this._defaultValueLabel.Size = new Size(400, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("ParameterEditorUserControl_ParameterDefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 63);
				this._defaultValueTextBox.Size = new Size(400, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				this._showAdvancedLinkLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._showAdvancedLinkLabel.Location = new Point(0, 87);
				this._showAdvancedLinkLabel.Size = new Size(400, 16);
				this._showAdvancedLinkLabel.TabIndex = 50;
				this._showAdvancedLinkLabel.TabStop = true;
				this._showAdvancedLinkLabel.Text = SR.GetString("ParameterEditorUserControl_ShowAdvancedProperties");
				this._showAdvancedLinkLabel.Links.Add(new LinkLabel.Link(0, this._showAdvancedLinkLabel.Text.Length));
				this._showAdvancedLinkLabel.LinkClicked += this.OnShowAdvancedLinkLabelLinkClicked;
				base.Controls.Add(this._controlIDLabel);
				base.Controls.Add(this._controlIDComboBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				base.Controls.Add(this._showAdvancedLinkLabel);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x06002843 RID: 10307 RVA: 0x000F5A70 File Offset: 0x000F3C70
			public override void InitializeParameter(ParameterEditorUserControl.ParameterListViewItem parameterItem)
			{
				base.InitializeParameter(parameterItem);
				string controlID = ((ControlParameter)base.ParameterItem.Parameter).ControlID;
				string propertyName = ((ControlParameter)base.ParameterItem.Parameter).PropertyName;
				this._controlIDComboBox.Items.Clear();
				ParameterEditorUserControl.ControlItem controlItem = null;
				if (base.ServiceProvider != null)
				{
					IDesignerHost designerHost = (IDesignerHost)base.ServiceProvider.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						ParameterEditorUserControl.ControlItem[] controlItems = ParameterEditorUserControl.ControlItem.GetControlItems(designerHost, this._control);
						foreach (ParameterEditorUserControl.ControlItem controlItem2 in controlItems)
						{
							this._controlIDComboBox.Items.Add(controlItem2);
							if (controlItem2.ControlID == controlID && controlItem2.PropertyName == propertyName)
							{
								controlItem = controlItem2;
							}
						}
					}
				}
				if (controlItem == null && controlID.Length > 0)
				{
					ParameterEditorUserControl.ControlItem controlItem3 = new ParameterEditorUserControl.ControlItem(controlID, propertyName);
					this._controlIDComboBox.Items.Insert(0, controlItem3);
					controlItem = controlItem3;
				}
				this._controlIDComboBox.InvalidateDropDownWidth();
				this._controlIDComboBox.SelectedItem = controlItem;
				this._defaultValueTextBox.Text = base.ParameterItem.Parameter.DefaultValue;
			}

			// Token: 0x06002844 RID: 10308 RVA: 0x000F56E0 File Offset: 0x000F38E0
			private void OnShowAdvancedLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				base.OnRequestModeChange();
			}

			// Token: 0x06002845 RID: 10309 RVA: 0x000F5BA8 File Offset: 0x000F3DA8
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				if (base.ParameterItem.Parameter.DefaultValue != this._defaultValueTextBox.Text)
				{
					base.ParameterItem.Parameter.DefaultValue = this._defaultValueTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x06002846 RID: 10310 RVA: 0x000F5BF8 File Offset: 0x000F3DF8
			private void OnControlIDComboBoxSelectedIndexChanged(object s, EventArgs e)
			{
				ParameterEditorUserControl.ControlItem controlItem = this._controlIDComboBox.SelectedItem as ParameterEditorUserControl.ControlItem;
				ControlParameter controlParameter = (ControlParameter)base.ParameterItem.Parameter;
				if (controlItem == null)
				{
					controlParameter.ControlID = string.Empty;
					controlParameter.PropertyName = string.Empty;
				}
				else
				{
					controlParameter.ControlID = controlItem.ControlID;
					controlParameter.PropertyName = controlItem.PropertyName;
				}
				base.OnParameterChanged();
			}

			// Token: 0x06002847 RID: 10311 RVA: 0x000F5C60 File Offset: 0x000F3E60
			public override void SetDefaultFocus()
			{
				this._controlIDComboBox.Focus();
			}

			// Token: 0x04001C9F RID: 7327
			private System.Windows.Forms.Label _controlIDLabel;

			// Token: 0x04001CA0 RID: 7328
			private AutoSizeComboBox _controlIDComboBox;

			// Token: 0x04001CA1 RID: 7329
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CA2 RID: 7330
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CA3 RID: 7331
			private LinkLabel _showAdvancedLinkLabel;

			// Token: 0x04001CA4 RID: 7332
			private Control _control;
		}

		// Token: 0x0200041D RID: 1053
		private sealed class CookieParameterEditor : ParameterEditorUserControl.ParameterEditor
		{
			// Token: 0x06002848 RID: 10312 RVA: 0x000F5C70 File Offset: 0x000F3E70
			public CookieParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(400, 400);
				this._cookieNameLabel = new System.Windows.Forms.Label();
				this._cookieNameTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._showAdvancedLinkLabel = new LinkLabel();
				this._cookieNameLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._cookieNameLabel.Location = new Point(0, 0);
				this._cookieNameLabel.Size = new Size(400, 16);
				this._cookieNameLabel.TabIndex = 10;
				this._cookieNameLabel.Text = SR.GetString("ParameterEditorUserControl_CookieParameterCookieName");
				this._cookieNameTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._cookieNameTextBox.Location = new Point(0, 18);
				this._cookieNameTextBox.Size = new Size(400, 20);
				this._cookieNameTextBox.TabIndex = 20;
				this._cookieNameTextBox.TextChanged += this.OnCookieNameTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 44);
				this._defaultValueLabel.Size = new Size(400, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("ParameterEditorUserControl_ParameterDefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 62);
				this._defaultValueTextBox.Size = new Size(400, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				this._showAdvancedLinkLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._showAdvancedLinkLabel.Location = new Point(0, 86);
				this._showAdvancedLinkLabel.Size = new Size(400, 16);
				this._showAdvancedLinkLabel.TabIndex = 50;
				this._showAdvancedLinkLabel.TabStop = true;
				this._showAdvancedLinkLabel.Text = SR.GetString("ParameterEditorUserControl_ShowAdvancedProperties");
				this._showAdvancedLinkLabel.Links.Add(new LinkLabel.Link(0, this._showAdvancedLinkLabel.Text.Length));
				this._showAdvancedLinkLabel.LinkClicked += this.OnShowAdvancedLinkLabelLinkClicked;
				base.Controls.Add(this._cookieNameLabel);
				base.Controls.Add(this._cookieNameTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				base.Controls.Add(this._showAdvancedLinkLabel);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x06002849 RID: 10313 RVA: 0x000F5F44 File Offset: 0x000F4144
			public override void InitializeParameter(ParameterEditorUserControl.ParameterListViewItem parameterItem)
			{
				base.InitializeParameter(parameterItem);
				this._defaultValueTextBox.Text = base.ParameterItem.Parameter.DefaultValue;
				this._cookieNameTextBox.Text = ((CookieParameter)base.ParameterItem.Parameter).CookieName;
			}

			// Token: 0x0600284A RID: 10314 RVA: 0x000F56E0 File Offset: 0x000F38E0
			private void OnShowAdvancedLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				base.OnRequestModeChange();
			}

			// Token: 0x0600284B RID: 10315 RVA: 0x000F5F94 File Offset: 0x000F4194
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				if (base.ParameterItem.Parameter.DefaultValue != this._defaultValueTextBox.Text)
				{
					base.ParameterItem.Parameter.DefaultValue = this._defaultValueTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x0600284C RID: 10316 RVA: 0x000F5FE4 File Offset: 0x000F41E4
			private void OnCookieNameTextBoxTextChanged(object s, EventArgs e)
			{
				if (((CookieParameter)base.ParameterItem.Parameter).CookieName != this._cookieNameTextBox.Text)
				{
					((CookieParameter)base.ParameterItem.Parameter).CookieName = this._cookieNameTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x0600284D RID: 10317 RVA: 0x000F603E File Offset: 0x000F423E
			public override void SetDefaultFocus()
			{
				this._cookieNameTextBox.Focus();
			}

			// Token: 0x04001CA5 RID: 7333
			private System.Windows.Forms.Label _cookieNameLabel;

			// Token: 0x04001CA6 RID: 7334
			private System.Windows.Forms.TextBox _cookieNameTextBox;

			// Token: 0x04001CA7 RID: 7335
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CA8 RID: 7336
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CA9 RID: 7337
			private LinkLabel _showAdvancedLinkLabel;
		}

		// Token: 0x0200041E RID: 1054
		private sealed class FormParameterEditor : ParameterEditorUserControl.ParameterEditor
		{
			// Token: 0x0600284E RID: 10318 RVA: 0x000F604C File Offset: 0x000F424C
			public FormParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(400, 400);
				this._formFieldLabel = new System.Windows.Forms.Label();
				this._formFieldTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._showAdvancedLinkLabel = new LinkLabel();
				this._formFieldLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._formFieldLabel.Location = new Point(0, 0);
				this._formFieldLabel.Size = new Size(400, 16);
				this._formFieldLabel.TabIndex = 10;
				this._formFieldLabel.Text = SR.GetString("ParameterEditorUserControl_FormParameterFormField");
				this._formFieldTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._formFieldTextBox.Location = new Point(0, 18);
				this._formFieldTextBox.Size = new Size(400, 20);
				this._formFieldTextBox.TabIndex = 20;
				this._formFieldTextBox.TextChanged += this.OnFormFieldTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 44);
				this._defaultValueLabel.Size = new Size(400, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("ParameterEditorUserControl_ParameterDefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 62);
				this._defaultValueTextBox.Size = new Size(400, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				this._showAdvancedLinkLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._showAdvancedLinkLabel.Location = new Point(0, 86);
				this._showAdvancedLinkLabel.Size = new Size(400, 16);
				this._showAdvancedLinkLabel.TabIndex = 50;
				this._showAdvancedLinkLabel.TabStop = true;
				this._showAdvancedLinkLabel.Text = SR.GetString("ParameterEditorUserControl_ShowAdvancedProperties");
				this._showAdvancedLinkLabel.Links.Add(new LinkLabel.Link(0, this._showAdvancedLinkLabel.Text.Length));
				this._showAdvancedLinkLabel.LinkClicked += this.OnShowAdvancedLinkLabelLinkClicked;
				base.Controls.Add(this._formFieldLabel);
				base.Controls.Add(this._formFieldTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				base.Controls.Add(this._showAdvancedLinkLabel);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x0600284F RID: 10319 RVA: 0x000F6320 File Offset: 0x000F4520
			public override void InitializeParameter(ParameterEditorUserControl.ParameterListViewItem parameterItem)
			{
				base.InitializeParameter(parameterItem);
				this._defaultValueTextBox.Text = base.ParameterItem.Parameter.DefaultValue;
				this._formFieldTextBox.Text = ((FormParameter)base.ParameterItem.Parameter).FormField;
			}

			// Token: 0x06002850 RID: 10320 RVA: 0x000F56E0 File Offset: 0x000F38E0
			private void OnShowAdvancedLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				base.OnRequestModeChange();
			}

			// Token: 0x06002851 RID: 10321 RVA: 0x000F6370 File Offset: 0x000F4570
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				if (base.ParameterItem.Parameter.DefaultValue != this._defaultValueTextBox.Text)
				{
					base.ParameterItem.Parameter.DefaultValue = this._defaultValueTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x06002852 RID: 10322 RVA: 0x000F63C0 File Offset: 0x000F45C0
			private void OnFormFieldTextBoxTextChanged(object s, EventArgs e)
			{
				if (((FormParameter)base.ParameterItem.Parameter).FormField != this._formFieldTextBox.Text)
				{
					((FormParameter)base.ParameterItem.Parameter).FormField = this._formFieldTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x06002853 RID: 10323 RVA: 0x000F641A File Offset: 0x000F461A
			public override void SetDefaultFocus()
			{
				this._formFieldTextBox.Focus();
			}

			// Token: 0x04001CAA RID: 7338
			private System.Windows.Forms.Label _formFieldLabel;

			// Token: 0x04001CAB RID: 7339
			private System.Windows.Forms.TextBox _formFieldTextBox;

			// Token: 0x04001CAC RID: 7340
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CAD RID: 7341
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CAE RID: 7342
			private LinkLabel _showAdvancedLinkLabel;
		}

		// Token: 0x0200041F RID: 1055
		private sealed class SessionParameterEditor : ParameterEditorUserControl.ParameterEditor
		{
			// Token: 0x06002854 RID: 10324 RVA: 0x000F6428 File Offset: 0x000F4628
			public SessionParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(400, 400);
				this._sessionFieldLabel = new System.Windows.Forms.Label();
				this._sessionFieldTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._showAdvancedLinkLabel = new LinkLabel();
				this._sessionFieldLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._sessionFieldLabel.Location = new Point(0, 0);
				this._sessionFieldLabel.Size = new Size(400, 16);
				this._sessionFieldLabel.TabIndex = 10;
				this._sessionFieldLabel.Text = SR.GetString("ParameterEditorUserControl_SessionParameterSessionField");
				this._sessionFieldTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._sessionFieldTextBox.Location = new Point(0, 18);
				this._sessionFieldTextBox.Size = new Size(400, 20);
				this._sessionFieldTextBox.TabIndex = 20;
				this._sessionFieldTextBox.TextChanged += this.OnSessionFieldTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 44);
				this._defaultValueLabel.Size = new Size(400, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("ParameterEditorUserControl_ParameterDefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 62);
				this._defaultValueTextBox.Size = new Size(400, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				this._showAdvancedLinkLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._showAdvancedLinkLabel.Location = new Point(0, 86);
				this._showAdvancedLinkLabel.Size = new Size(400, 16);
				this._showAdvancedLinkLabel.TabIndex = 50;
				this._showAdvancedLinkLabel.TabStop = true;
				this._showAdvancedLinkLabel.Text = SR.GetString("ParameterEditorUserControl_ShowAdvancedProperties");
				this._showAdvancedLinkLabel.Links.Add(new LinkLabel.Link(0, this._showAdvancedLinkLabel.Text.Length));
				this._showAdvancedLinkLabel.LinkClicked += this.OnShowAdvancedLinkLabelLinkClicked;
				base.Controls.Add(this._sessionFieldLabel);
				base.Controls.Add(this._sessionFieldTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				base.Controls.Add(this._showAdvancedLinkLabel);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x06002855 RID: 10325 RVA: 0x000F66FC File Offset: 0x000F48FC
			public override void InitializeParameter(ParameterEditorUserControl.ParameterListViewItem parameterItem)
			{
				base.InitializeParameter(parameterItem);
				this._defaultValueTextBox.Text = base.ParameterItem.Parameter.DefaultValue;
				this._sessionFieldTextBox.Text = ((SessionParameter)base.ParameterItem.Parameter).SessionField;
			}

			// Token: 0x06002856 RID: 10326 RVA: 0x000F56E0 File Offset: 0x000F38E0
			private void OnShowAdvancedLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				base.OnRequestModeChange();
			}

			// Token: 0x06002857 RID: 10327 RVA: 0x000F674C File Offset: 0x000F494C
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				if (base.ParameterItem.Parameter.DefaultValue != this._defaultValueTextBox.Text)
				{
					base.ParameterItem.Parameter.DefaultValue = this._defaultValueTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x06002858 RID: 10328 RVA: 0x000F679C File Offset: 0x000F499C
			private void OnSessionFieldTextBoxTextChanged(object s, EventArgs e)
			{
				if (((SessionParameter)base.ParameterItem.Parameter).SessionField != this._sessionFieldTextBox.Text)
				{
					((SessionParameter)base.ParameterItem.Parameter).SessionField = this._sessionFieldTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x06002859 RID: 10329 RVA: 0x000F67F6 File Offset: 0x000F49F6
			public override void SetDefaultFocus()
			{
				this._sessionFieldTextBox.Focus();
			}

			// Token: 0x04001CAF RID: 7343
			private System.Windows.Forms.Label _sessionFieldLabel;

			// Token: 0x04001CB0 RID: 7344
			private System.Windows.Forms.TextBox _sessionFieldTextBox;

			// Token: 0x04001CB1 RID: 7345
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CB2 RID: 7346
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CB3 RID: 7347
			private LinkLabel _showAdvancedLinkLabel;
		}

		// Token: 0x02000420 RID: 1056
		private sealed class StaticParameterEditor : ParameterEditorUserControl.ParameterEditor
		{
			// Token: 0x0600285A RID: 10330 RVA: 0x000F6804 File Offset: 0x000F4A04
			public StaticParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(400, 400);
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._showAdvancedLinkLabel = new LinkLabel();
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 0);
				this._defaultValueLabel.Size = new Size(400, 16);
				this._defaultValueLabel.TabIndex = 10;
				this._defaultValueLabel.Text = SR.GetString("ParameterEditorUserControl_ParameterDefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 18);
				this._defaultValueTextBox.Size = new Size(400, 20);
				this._defaultValueTextBox.TabIndex = 20;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				this._showAdvancedLinkLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._showAdvancedLinkLabel.Location = new Point(0, 42);
				this._showAdvancedLinkLabel.Size = new Size(400, 16);
				this._showAdvancedLinkLabel.TabIndex = 30;
				this._showAdvancedLinkLabel.TabStop = true;
				this._showAdvancedLinkLabel.Text = SR.GetString("ParameterEditorUserControl_ShowAdvancedProperties");
				this._showAdvancedLinkLabel.Links.Add(new LinkLabel.Link(0, this._showAdvancedLinkLabel.Text.Length));
				this._showAdvancedLinkLabel.LinkClicked += this.OnShowAdvancedLinkLabelLinkClicked;
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				base.Controls.Add(this._showAdvancedLinkLabel);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x0600285B RID: 10331 RVA: 0x000F69EA File Offset: 0x000F4BEA
			public override void InitializeParameter(ParameterEditorUserControl.ParameterListViewItem parameterItem)
			{
				base.InitializeParameter(parameterItem);
				this._defaultValueTextBox.Text = base.ParameterItem.Parameter.DefaultValue;
			}

			// Token: 0x0600285C RID: 10332 RVA: 0x000F56E0 File Offset: 0x000F38E0
			private void OnShowAdvancedLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				base.OnRequestModeChange();
			}

			// Token: 0x0600285D RID: 10333 RVA: 0x000F6A10 File Offset: 0x000F4C10
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				if (base.ParameterItem.Parameter.DefaultValue != this._defaultValueTextBox.Text)
				{
					base.ParameterItem.Parameter.DefaultValue = this._defaultValueTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x0600285E RID: 10334 RVA: 0x000F6A60 File Offset: 0x000F4C60
			public override void SetDefaultFocus()
			{
				this._defaultValueTextBox.Focus();
			}

			// Token: 0x04001CB4 RID: 7348
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CB5 RID: 7349
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CB6 RID: 7350
			private LinkLabel _showAdvancedLinkLabel;
		}

		// Token: 0x02000421 RID: 1057
		private sealed class QueryStringParameterEditor : ParameterEditorUserControl.ParameterEditor
		{
			// Token: 0x0600285F RID: 10335 RVA: 0x000F6A70 File Offset: 0x000F4C70
			public QueryStringParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(400, 400);
				this._queryStringFieldLabel = new System.Windows.Forms.Label();
				this._queryStringFieldTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._showAdvancedLinkLabel = new LinkLabel();
				this._queryStringFieldLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._queryStringFieldLabel.Location = new Point(0, 0);
				this._queryStringFieldLabel.Size = new Size(400, 16);
				this._queryStringFieldLabel.TabIndex = 10;
				this._queryStringFieldLabel.Text = SR.GetString("ParameterEditorUserControl_QueryStringParameterQueryStringField");
				this._queryStringFieldTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._queryStringFieldTextBox.Location = new Point(0, 18);
				this._queryStringFieldTextBox.Size = new Size(400, 20);
				this._queryStringFieldTextBox.TabIndex = 20;
				this._queryStringFieldTextBox.TextChanged += this.OnQueryStringFieldTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 44);
				this._defaultValueLabel.Size = new Size(400, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("ParameterEditorUserControl_ParameterDefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 62);
				this._defaultValueTextBox.Size = new Size(400, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				this._showAdvancedLinkLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._showAdvancedLinkLabel.Location = new Point(0, 86);
				this._showAdvancedLinkLabel.Size = new Size(400, 16);
				this._showAdvancedLinkLabel.TabIndex = 50;
				this._showAdvancedLinkLabel.TabStop = true;
				this._showAdvancedLinkLabel.Text = SR.GetString("ParameterEditorUserControl_ShowAdvancedProperties");
				this._showAdvancedLinkLabel.Links.Add(new LinkLabel.Link(0, this._showAdvancedLinkLabel.Text.Length));
				this._showAdvancedLinkLabel.LinkClicked += this.OnShowAdvancedLinkLabelLinkClicked;
				base.Controls.Add(this._queryStringFieldLabel);
				base.Controls.Add(this._queryStringFieldTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				base.Controls.Add(this._showAdvancedLinkLabel);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x06002860 RID: 10336 RVA: 0x000F6D44 File Offset: 0x000F4F44
			public override void InitializeParameter(ParameterEditorUserControl.ParameterListViewItem parameterItem)
			{
				base.InitializeParameter(parameterItem);
				this._defaultValueTextBox.Text = base.ParameterItem.Parameter.DefaultValue;
				this._queryStringFieldTextBox.Text = ((QueryStringParameter)base.ParameterItem.Parameter).QueryStringField;
			}

			// Token: 0x06002861 RID: 10337 RVA: 0x000F56E0 File Offset: 0x000F38E0
			private void OnShowAdvancedLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				base.OnRequestModeChange();
			}

			// Token: 0x06002862 RID: 10338 RVA: 0x000F6D94 File Offset: 0x000F4F94
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				if (base.ParameterItem.Parameter.DefaultValue != this._defaultValueTextBox.Text)
				{
					base.ParameterItem.Parameter.DefaultValue = this._defaultValueTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x06002863 RID: 10339 RVA: 0x000F6DE4 File Offset: 0x000F4FE4
			private void OnQueryStringFieldTextBoxTextChanged(object s, EventArgs e)
			{
				if (((QueryStringParameter)base.ParameterItem.Parameter).QueryStringField != this._queryStringFieldTextBox.Text)
				{
					((QueryStringParameter)base.ParameterItem.Parameter).QueryStringField = this._queryStringFieldTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x06002864 RID: 10340 RVA: 0x000F6E3E File Offset: 0x000F503E
			public override void SetDefaultFocus()
			{
				this._queryStringFieldTextBox.Focus();
			}

			// Token: 0x04001CB7 RID: 7351
			private System.Windows.Forms.Label _queryStringFieldLabel;

			// Token: 0x04001CB8 RID: 7352
			private System.Windows.Forms.TextBox _queryStringFieldTextBox;

			// Token: 0x04001CB9 RID: 7353
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CBA RID: 7354
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CBB RID: 7355
			private LinkLabel _showAdvancedLinkLabel;
		}

		// Token: 0x02000422 RID: 1058
		private sealed class RouteParameterEditor : ParameterEditorUserControl.ParameterEditor
		{
			// Token: 0x06002865 RID: 10341 RVA: 0x000F6E4C File Offset: 0x000F504C
			public RouteParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(400, 400);
				this._routeKeyLabel = new System.Windows.Forms.Label();
				this._routeKeyTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._showAdvancedLinkLabel = new LinkLabel();
				this._routeKeyLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._routeKeyLabel.Location = new Point(0, 0);
				this._routeKeyLabel.Size = new Size(400, 16);
				this._routeKeyLabel.TabIndex = 10;
				this._routeKeyLabel.Text = SR.GetString("ParameterEditorUserControl_RouteParameterRouteKey");
				this._routeKeyTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._routeKeyTextBox.Location = new Point(0, 18);
				this._routeKeyTextBox.Size = new Size(400, 20);
				this._routeKeyTextBox.TabIndex = 20;
				this._routeKeyTextBox.TextChanged += this.OnRouteKeyTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 44);
				this._defaultValueLabel.Size = new Size(400, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("ParameterEditorUserControl_ParameterDefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 62);
				this._defaultValueTextBox.Size = new Size(400, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				this._showAdvancedLinkLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._showAdvancedLinkLabel.Location = new Point(0, 86);
				this._showAdvancedLinkLabel.Size = new Size(400, 16);
				this._showAdvancedLinkLabel.TabIndex = 50;
				this._showAdvancedLinkLabel.TabStop = true;
				this._showAdvancedLinkLabel.Text = SR.GetString("ParameterEditorUserControl_ShowAdvancedProperties");
				this._showAdvancedLinkLabel.Links.Add(new LinkLabel.Link(0, this._showAdvancedLinkLabel.Text.Length));
				this._showAdvancedLinkLabel.LinkClicked += this.OnShowAdvancedLinkLabelLinkClicked;
				base.Controls.Add(this._routeKeyLabel);
				base.Controls.Add(this._routeKeyTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				base.Controls.Add(this._showAdvancedLinkLabel);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x06002866 RID: 10342 RVA: 0x000F7120 File Offset: 0x000F5320
			public override void InitializeParameter(ParameterEditorUserControl.ParameterListViewItem parameterItem)
			{
				base.InitializeParameter(parameterItem);
				this._defaultValueTextBox.Text = base.ParameterItem.Parameter.DefaultValue;
				this._routeKeyTextBox.Text = ((RouteParameter)base.ParameterItem.Parameter).RouteKey;
			}

			// Token: 0x06002867 RID: 10343 RVA: 0x000F56E0 File Offset: 0x000F38E0
			private void OnShowAdvancedLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				base.OnRequestModeChange();
			}

			// Token: 0x06002868 RID: 10344 RVA: 0x000F7170 File Offset: 0x000F5370
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				if (base.ParameterItem.Parameter.DefaultValue != this._defaultValueTextBox.Text)
				{
					base.ParameterItem.Parameter.DefaultValue = this._defaultValueTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x06002869 RID: 10345 RVA: 0x000F71C0 File Offset: 0x000F53C0
			private void OnRouteKeyTextBoxTextChanged(object s, EventArgs e)
			{
				if (((RouteParameter)base.ParameterItem.Parameter).RouteKey != this._routeKeyTextBox.Text)
				{
					((RouteParameter)base.ParameterItem.Parameter).RouteKey = this._routeKeyTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x0600286A RID: 10346 RVA: 0x000F721A File Offset: 0x000F541A
			public override void SetDefaultFocus()
			{
				this._routeKeyTextBox.Focus();
			}

			// Token: 0x04001CBC RID: 7356
			private System.Windows.Forms.Label _routeKeyLabel;

			// Token: 0x04001CBD RID: 7357
			private System.Windows.Forms.TextBox _routeKeyTextBox;

			// Token: 0x04001CBE RID: 7358
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CBF RID: 7359
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CC0 RID: 7360
			private LinkLabel _showAdvancedLinkLabel;
		}

		// Token: 0x02000423 RID: 1059
		private sealed class ProfileParameterEditor : ParameterEditorUserControl.ParameterEditor
		{
			// Token: 0x0600286B RID: 10347 RVA: 0x000F7228 File Offset: 0x000F5428
			public ProfileParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(400, 400);
				this._propertyNameLabel = new System.Windows.Forms.Label();
				this._propertyNameTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._showAdvancedLinkLabel = new LinkLabel();
				this._propertyNameLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._propertyNameLabel.Location = new Point(0, 0);
				this._propertyNameLabel.Size = new Size(400, 16);
				this._propertyNameLabel.TabIndex = 10;
				this._propertyNameLabel.Text = SR.GetString("ParameterEditorUserControl_ProfilePropertyName");
				this._propertyNameTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._propertyNameTextBox.Location = new Point(0, 18);
				this._propertyNameTextBox.Size = new Size(400, 20);
				this._propertyNameTextBox.TabIndex = 20;
				this._propertyNameTextBox.TextChanged += this.OnPropertyNameTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 44);
				this._defaultValueLabel.Size = new Size(400, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("ParameterEditorUserControl_ParameterDefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 62);
				this._defaultValueTextBox.Size = new Size(400, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				this._showAdvancedLinkLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._showAdvancedLinkLabel.Location = new Point(0, 86);
				this._showAdvancedLinkLabel.Size = new Size(400, 16);
				this._showAdvancedLinkLabel.TabIndex = 50;
				this._showAdvancedLinkLabel.TabStop = true;
				this._showAdvancedLinkLabel.Text = SR.GetString("ParameterEditorUserControl_ShowAdvancedProperties");
				this._showAdvancedLinkLabel.Links.Add(new LinkLabel.Link(0, this._showAdvancedLinkLabel.Text.Length));
				this._showAdvancedLinkLabel.LinkClicked += this.OnShowAdvancedLinkLabelLinkClicked;
				base.Controls.Add(this._propertyNameLabel);
				base.Controls.Add(this._propertyNameTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				base.Controls.Add(this._showAdvancedLinkLabel);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x0600286C RID: 10348 RVA: 0x000F74FC File Offset: 0x000F56FC
			public override void InitializeParameter(ParameterEditorUserControl.ParameterListViewItem parameterItem)
			{
				base.InitializeParameter(parameterItem);
				this._defaultValueTextBox.Text = base.ParameterItem.Parameter.DefaultValue;
				this._propertyNameTextBox.Text = ((ProfileParameter)base.ParameterItem.Parameter).PropertyName;
			}

			// Token: 0x0600286D RID: 10349 RVA: 0x000F56E0 File Offset: 0x000F38E0
			private void OnShowAdvancedLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				base.OnRequestModeChange();
			}

			// Token: 0x0600286E RID: 10350 RVA: 0x000F754C File Offset: 0x000F574C
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				if (base.ParameterItem.Parameter.DefaultValue != this._defaultValueTextBox.Text)
				{
					base.ParameterItem.Parameter.DefaultValue = this._defaultValueTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x0600286F RID: 10351 RVA: 0x000F759C File Offset: 0x000F579C
			private void OnPropertyNameTextBoxTextChanged(object s, EventArgs e)
			{
				if (((ProfileParameter)base.ParameterItem.Parameter).PropertyName != this._propertyNameTextBox.Text)
				{
					((ProfileParameter)base.ParameterItem.Parameter).PropertyName = this._propertyNameTextBox.Text;
					base.OnParameterChanged();
				}
			}

			// Token: 0x06002870 RID: 10352 RVA: 0x000F75F6 File Offset: 0x000F57F6
			public override void SetDefaultFocus()
			{
				this._propertyNameTextBox.Focus();
			}

			// Token: 0x04001CC1 RID: 7361
			private System.Windows.Forms.Label _propertyNameLabel;

			// Token: 0x04001CC2 RID: 7362
			private System.Windows.Forms.TextBox _propertyNameTextBox;

			// Token: 0x04001CC3 RID: 7363
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CC4 RID: 7364
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CC5 RID: 7365
			private LinkLabel _showAdvancedLinkLabel;
		}
	}
}
