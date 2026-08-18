using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000562 RID: 1378
	public sealed class PropertyGridEditorPart : EditorPart
	{
		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x000D9E7A File Offset: 0x000D807A
		// (set) Token: 0x060045F4 RID: 17908 RVA: 0x000D9E82 File Offset: 0x000D8082
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string DefaultButton
		{
			get
			{
				return base.DefaultButton;
			}
			set
			{
				base.DefaultButton = value;
			}
		}

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x000E6894 File Offset: 0x000E4A94
		public override bool Display
		{
			get
			{
				if (!base.Display)
				{
					return false;
				}
				object editableObject = this.GetEditableObject();
				return editableObject != null && this.GetEditableProperties(editableObject, false).Count > 0;
			}
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x000E68C8 File Offset: 0x000E4AC8
		private ArrayList EditorControls
		{
			get
			{
				if (this._editorControls == null)
				{
					this._editorControls = new ArrayList();
				}
				return this._editorControls;
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x000E68E4 File Offset: 0x000E4AE4
		private bool HasError
		{
			get
			{
				foreach (string text in this._errorMessages)
				{
					if (text != null)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x000E6910 File Offset: 0x000E4B10
		// (set) Token: 0x060045F9 RID: 17913 RVA: 0x000D9EF2 File Offset: 0x000D80F2
		[WebSysDefaultValue("PropertyGridEditorPart_PartTitle")]
		public override string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text == null)
				{
					return SR.GetString("PropertyGridEditorPart_PartTitle");
				}
				return text;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x000E6944 File Offset: 0x000E4B44
		public override bool ApplyChanges()
		{
			object editableObject = this.GetEditableObject();
			if (editableObject == null)
			{
				return true;
			}
			this.EnsureChildControls();
			if (this.Controls.Count == 0)
			{
				return true;
			}
			PropertyDescriptorCollection editableProperties = this.GetEditableProperties(editableObject, true);
			for (int i = 0; i < editableProperties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor = editableProperties[i];
				Control editorControl = (Control)this.EditorControls[i];
				try
				{
					object editorControlValue = this.GetEditorControlValue(editorControl, propertyDescriptor);
					if (propertyDescriptor.Attributes.Matches(PropertyGridEditorPart.urlPropertyAttribute) && CrossSiteScriptingValidation.IsDangerousUrl(editorControlValue.ToString()))
					{
						this._errorMessages[i] = SR.GetString("EditorPart_ErrorBadUrl");
					}
					else
					{
						try
						{
							propertyDescriptor.SetValue(editableObject, editorControlValue);
						}
						catch (Exception ex)
						{
							this._errorMessages[i] = base.CreateErrorMessage(ex.Message);
						}
					}
				}
				catch
				{
					if (this.Context != null && this.Context.IsCustomErrorEnabled)
					{
						this._errorMessages[i] = SR.GetString("EditorPart_ErrorConvertingProperty");
					}
					else
					{
						this._errorMessages[i] = SR.GetString("EditorPart_ErrorConvertingPropertyWithType", new object[]
						{
							propertyDescriptor.PropertyType.FullName
						});
					}
				}
			}
			return !this.HasError;
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x000E6A94 File Offset: 0x000E4C94
		private bool CanEditProperty(PropertyDescriptor property)
		{
			if (property.IsReadOnly)
			{
				return false;
			}
			if (base.WebPartManager != null && base.WebPartManager.Personalization != null && base.WebPartManager.Personalization.Scope == PersonalizationScope.User)
			{
				AttributeCollection attributes = property.Attributes;
				if (attributes.Contains(PersonalizableAttribute.SharedPersonalizable))
				{
					return false;
				}
			}
			return Util.CanConvertToFrom(property.Converter, typeof(string));
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x000E6B00 File Offset: 0x000E4D00
		protected internal override void CreateChildControls()
		{
			ControlCollection controls = this.Controls;
			controls.Clear();
			this.EditorControls.Clear();
			object editableObject = this.GetEditableObject();
			if (editableObject != null)
			{
				foreach (object obj in this.GetEditableProperties(editableObject, true))
				{
					PropertyDescriptor pd = (PropertyDescriptor)obj;
					Control control = this.CreateEditorControl(pd);
					this.EditorControls.Add(control);
					this.Controls.Add(control);
				}
				this._errorMessages = new string[this.EditorControls.Count];
			}
			foreach (object obj2 in controls)
			{
				Control control2 = (Control)obj2;
				control2.EnableViewState = false;
			}
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x000E6C00 File Offset: 0x000E4E00
		private Control CreateEditorControl(PropertyDescriptor pd)
		{
			Type propertyType = pd.PropertyType;
			if (propertyType == typeof(bool))
			{
				return new CheckBox();
			}
			if (typeof(Enum).IsAssignableFrom(propertyType))
			{
				DropDownList dropDownList = new DropDownList();
				ICollection standardValues = pd.Converter.GetStandardValues();
				foreach (object value in standardValues)
				{
					string text = pd.Converter.ConvertToString(value);
					dropDownList.Items.Add(new ListItem(text));
				}
				return dropDownList;
			}
			return new TextBox
			{
				Columns = 30
			};
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x000E6CC4 File Offset: 0x000E4EC4
		private string GetDescription(PropertyDescriptor pd)
		{
			WebDescriptionAttribute webDescriptionAttribute = (WebDescriptionAttribute)pd.Attributes[typeof(WebDescriptionAttribute)];
			if (webDescriptionAttribute != null)
			{
				return webDescriptionAttribute.Description;
			}
			return null;
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x000E6CF8 File Offset: 0x000E4EF8
		private string GetDisplayName(PropertyDescriptor pd)
		{
			WebDisplayNameAttribute webDisplayNameAttribute = (WebDisplayNameAttribute)pd.Attributes[typeof(WebDisplayNameAttribute)];
			if (webDisplayNameAttribute != null && !string.IsNullOrEmpty(webDisplayNameAttribute.DisplayName))
			{
				return webDisplayNameAttribute.DisplayName;
			}
			return pd.Name;
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x000E6D40 File Offset: 0x000E4F40
		private object GetEditableObject()
		{
			if (base.DesignMode)
			{
				return PropertyGridEditorPart.designModeWebPart;
			}
			WebPart webPartToEdit = base.WebPartToEdit;
			IWebEditable webEditable = webPartToEdit;
			if (webEditable != null)
			{
				return webEditable.WebBrowsableObject;
			}
			return webPartToEdit;
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x000E6D70 File Offset: 0x000E4F70
		private PropertyDescriptorCollection GetEditableProperties(object editableObject, bool sort)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(editableObject, PropertyGridEditorPart.FilterAttributes);
			if (sort)
			{
				propertyDescriptorCollection = propertyDescriptorCollection.Sort();
			}
			PropertyDescriptorCollection propertyDescriptorCollection2 = new PropertyDescriptorCollection(null);
			foreach (object obj in propertyDescriptorCollection)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (this.CanEditProperty(propertyDescriptor))
				{
					propertyDescriptorCollection2.Add(propertyDescriptor);
				}
			}
			return propertyDescriptorCollection2;
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x000E6DF0 File Offset: 0x000E4FF0
		private object GetEditorControlValue(Control editorControl, PropertyDescriptor pd)
		{
			CheckBox checkBox = editorControl as CheckBox;
			if (checkBox != null)
			{
				return checkBox.Checked;
			}
			DropDownList dropDownList = editorControl as DropDownList;
			if (dropDownList != null)
			{
				string selectedValue = dropDownList.SelectedValue;
				return pd.Converter.ConvertFromString(selectedValue);
			}
			TextBox textBox = (TextBox)editorControl;
			return pd.Converter.ConvertFromString(textBox.Text);
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x000E6E49 File Offset: 0x000E5049
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Display && this.Visible && !this.HasError)
			{
				this.SyncChanges();
			}
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x000E6E70 File Offset: 0x000E5070
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.EnsureChildControls();
			string[] array = null;
			string[] array2 = null;
			object editableObject = this.GetEditableObject();
			if (editableObject != null)
			{
				PropertyDescriptorCollection editableProperties = this.GetEditableProperties(editableObject, true);
				array = new string[editableProperties.Count];
				array2 = new string[editableProperties.Count];
				for (int i = 0; i < editableProperties.Count; i++)
				{
					array[i] = this.GetDisplayName(editableProperties[i]);
					array2[i] = this.GetDescription(editableProperties[i]);
				}
			}
			if (array != null)
			{
				WebControl[] propertyEditors = (WebControl[])this.EditorControls.ToArray(typeof(WebControl));
				base.RenderPropertyEditors(writer, array, array2, propertyEditors, this._errorMessages);
			}
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x000E6F30 File Offset: 0x000E5130
		public override void SyncChanges()
		{
			object editableObject = this.GetEditableObject();
			if (editableObject != null)
			{
				this.EnsureChildControls();
				int num = 0;
				foreach (object obj in this.GetEditableProperties(editableObject, true))
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					if (this.CanEditProperty(propertyDescriptor))
					{
						Control control = (Control)this.EditorControls[num];
						this.SyncChanges(control, propertyDescriptor, editableObject);
						num++;
					}
				}
			}
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x000E6FC4 File Offset: 0x000E51C4
		private void SyncChanges(Control control, PropertyDescriptor pd, object instance)
		{
			Type propertyType = pd.PropertyType;
			if (propertyType == typeof(bool))
			{
				CheckBox checkBox = (CheckBox)control;
				checkBox.Checked = (bool)pd.GetValue(instance);
				return;
			}
			if (typeof(Enum).IsAssignableFrom(propertyType))
			{
				DropDownList dropDownList = (DropDownList)control;
				dropDownList.SelectedValue = pd.Converter.ConvertToString(pd.GetValue(instance));
				return;
			}
			TextBox textBox = (TextBox)control;
			textBox.Text = pd.Converter.ConvertToString(pd.GetValue(instance));
		}

		// Token: 0x04002688 RID: 9864
		private ArrayList _editorControls;

		// Token: 0x04002689 RID: 9865
		private string[] _errorMessages;

		// Token: 0x0400268A RID: 9866
		private static readonly Attribute[] FilterAttributes = new Attribute[]
		{
			WebBrowsableAttribute.Yes
		};

		// Token: 0x0400268B RID: 9867
		private static readonly WebPart designModeWebPart = new PropertyGridEditorPart.DesignModeWebPart();

		// Token: 0x0400268C RID: 9868
		private static readonly UrlPropertyAttribute urlPropertyAttribute = new UrlPropertyAttribute();

		// Token: 0x0400268D RID: 9869
		private const int TextBoxColumns = 30;

		// Token: 0x020009F0 RID: 2544
		private sealed class DesignModeWebPart : WebPart
		{
			// Token: 0x17001E0B RID: 7691
			// (get) Token: 0x06006D21 RID: 27937 RVA: 0x00007722 File Offset: 0x00005922
			// (set) Token: 0x06006D22 RID: 27938 RVA: 0x00006164 File Offset: 0x00004364
			[WebBrowsable]
			[PropertyGridEditorPart.DesignModeWebPart.WebSysWebDisplayNameAttribute("PropertyGridEditorPart_DesignModeWebPart_BoolProperty")]
			public bool BoolProperty
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17001E0C RID: 7692
			// (get) Token: 0x06006D23 RID: 27939 RVA: 0x00007722 File Offset: 0x00005922
			// (set) Token: 0x06006D24 RID: 27940 RVA: 0x00006164 File Offset: 0x00004364
			[WebBrowsable]
			[PropertyGridEditorPart.DesignModeWebPart.WebSysWebDisplayNameAttribute("PropertyGridEditorPart_DesignModeWebPart_EnumProperty")]
			public PropertyGridEditorPart.DesignModeWebPart.SampleEnum EnumProperty
			{
				get
				{
					return PropertyGridEditorPart.DesignModeWebPart.SampleEnum.EnumValue;
				}
				set
				{
				}
			}

			// Token: 0x17001E0D RID: 7693
			// (get) Token: 0x06006D25 RID: 27941 RVA: 0x00028752 File Offset: 0x00026952
			// (set) Token: 0x06006D26 RID: 27942 RVA: 0x00006164 File Offset: 0x00004364
			[WebBrowsable]
			[PropertyGridEditorPart.DesignModeWebPart.WebSysWebDisplayNameAttribute("PropertyGridEditorPart_DesignModeWebPart_StringProperty")]
			public string StringProperty
			{
				get
				{
					return string.Empty;
				}
				set
				{
				}
			}

			// Token: 0x02000A94 RID: 2708
			public enum SampleEnum
			{
				// Token: 0x04003C01 RID: 15361
				EnumValue
			}

			// Token: 0x02000A95 RID: 2709
			private sealed class WebSysWebDisplayNameAttribute : WebDisplayNameAttribute
			{
				// Token: 0x06006F65 RID: 28517 RVA: 0x0018CA5D File Offset: 0x0018AC5D
				internal WebSysWebDisplayNameAttribute(string DisplayName) : base(DisplayName)
				{
				}

				// Token: 0x17001E51 RID: 7761
				// (get) Token: 0x06006F66 RID: 28518 RVA: 0x0018CA66 File Offset: 0x0018AC66
				public override string DisplayName
				{
					get
					{
						if (!this.replaced)
						{
							this.replaced = true;
							base.DisplayNameValue = SR.GetString(base.DisplayName);
						}
						return base.DisplayName;
					}
				}

				// Token: 0x17001E52 RID: 7762
				// (get) Token: 0x06006F67 RID: 28519 RVA: 0x0018CA8E File Offset: 0x0018AC8E
				public override object TypeId
				{
					get
					{
						return typeof(WebDisplayNameAttribute);
					}
				}

				// Token: 0x04003C02 RID: 15362
				private bool replaced;
			}
		}
	}
}
