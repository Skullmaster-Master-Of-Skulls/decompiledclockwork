using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200056A RID: 1386
	[WebPartTransformer(typeof(IWebPartRow), typeof(IWebPartParameters))]
	public sealed class RowToParametersTransformer : WebPartTransformer, IWebPartParameters
	{
		// Token: 0x0600464F RID: 17999 RVA: 0x000E789A File Offset: 0x000E5A9A
		public override Control CreateConfigurationControl()
		{
			return new RowToParametersTransformer.RowToParametersConfigurationWizard(this);
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x000E78A2 File Offset: 0x000E5AA2
		// (set) Token: 0x06004651 RID: 18001 RVA: 0x000E78C3 File Offset: 0x000E5AC3
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] ConsumerFieldNames
		{
			get
			{
				if (this._consumerFieldNames == null)
				{
					return new string[0];
				}
				return (string[])this._consumerFieldNames.Clone();
			}
			set
			{
				this._consumerFieldNames = ((value != null) ? ((string[])value.Clone()) : null);
			}
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06004652 RID: 18002 RVA: 0x000E78DC File Offset: 0x000E5ADC
		private PropertyDescriptorCollection ConsumerSchema
		{
			get
			{
				return this._consumerSchema;
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06004653 RID: 18003 RVA: 0x000E78E4 File Offset: 0x000E5AE4
		// (set) Token: 0x06004654 RID: 18004 RVA: 0x000E7905 File Offset: 0x000E5B05
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] ProviderFieldNames
		{
			get
			{
				if (this._providerFieldNames == null)
				{
					return new string[0];
				}
				return (string[])this._providerFieldNames.Clone();
			}
			set
			{
				this._providerFieldNames = ((value != null) ? ((string[])value.Clone()) : null);
			}
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x06004655 RID: 18005 RVA: 0x000E791E File Offset: 0x000E5B1E
		private PropertyDescriptorCollection ProviderSchema
		{
			get
			{
				if (this._provider == null)
				{
					return null;
				}
				return this._provider.Schema;
			}
		}

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06004656 RID: 18006 RVA: 0x000E7938 File Offset: 0x000E5B38
		private PropertyDescriptorCollection SelectedProviderSchema
		{
			get
			{
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				PropertyDescriptorCollection providerSchema = this.ProviderSchema;
				if (providerSchema != null && this._providerFieldNames != null && this._providerFieldNames.Length != 0)
				{
					foreach (string name in this._providerFieldNames)
					{
						PropertyDescriptor propertyDescriptor = providerSchema.Find(name, true);
						if (propertyDescriptor == null)
						{
							return new PropertyDescriptorCollection(null);
						}
						propertyDescriptorCollection.Add(propertyDescriptor);
					}
				}
				return propertyDescriptorCollection;
			}
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x000E79A4 File Offset: 0x000E5BA4
		private void CheckFieldNamesLength()
		{
			int num = (this._consumerFieldNames != null) ? this._consumerFieldNames.Length : 0;
			int num2 = (this._providerFieldNames != null) ? this._providerFieldNames.Length : 0;
			if (num != num2)
			{
				throw new InvalidOperationException(SR.GetString("RowToParametersTransformer_DifferentFieldNamesLength"));
			}
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x000E79F0 File Offset: 0x000E5BF0
		private void GetRowData(object rowData)
		{
			IDictionary dictionary = null;
			if (rowData != null)
			{
				PropertyDescriptorCollection schema = ((IWebPartParameters)this).Schema;
				dictionary = new HybridDictionary(schema.Count);
				if (schema.Count > 0)
				{
					PropertyDescriptorCollection selectedProviderSchema = this.SelectedProviderSchema;
					if (selectedProviderSchema != null && selectedProviderSchema.Count > 0 && selectedProviderSchema.Count == schema.Count)
					{
						for (int i = 0; i < selectedProviderSchema.Count; i++)
						{
							PropertyDescriptor propertyDescriptor = selectedProviderSchema[i];
							PropertyDescriptor propertyDescriptor2 = schema[i];
							dictionary[propertyDescriptor2.Name] = propertyDescriptor.GetValue(rowData);
						}
					}
				}
			}
			this._callback(dictionary);
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x000E7A84 File Offset: 0x000E5C84
		protected internal override void LoadConfigurationState(object savedState)
		{
			if (savedState != null)
			{
				string[] array = (string[])savedState;
				int num = array.Length;
				if (num % 2 != 0)
				{
					throw new InvalidOperationException(SR.GetString("RowToParametersTransformer_DifferentFieldNamesLength"));
				}
				int num2 = num / 2;
				this._consumerFieldNames = new string[num2];
				this._providerFieldNames = new string[num2];
				for (int i = 0; i < num2; i++)
				{
					this._consumerFieldNames[i] = array[2 * i];
					this._providerFieldNames[i] = array[2 * i + 1];
				}
			}
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x000E7AF8 File Offset: 0x000E5CF8
		protected internal override object SaveConfigurationState()
		{
			this.CheckFieldNamesLength();
			int num = (this._consumerFieldNames != null) ? this._consumerFieldNames.Length : 0;
			if (num > 0)
			{
				string[] array = new string[num * 2];
				for (int i = 0; i < num; i++)
				{
					array[2 * i] = this._consumerFieldNames[i];
					array[2 * i + 1] = this._providerFieldNames[i];
				}
				return array;
			}
			return null;
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x000E7B57 File Offset: 0x000E5D57
		public override object Transform(object providerData)
		{
			this._provider = (IWebPartRow)providerData;
			return this;
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x000E7B68 File Offset: 0x000E5D68
		void IWebPartParameters.GetParametersData(ParametersCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.CheckFieldNamesLength();
			if (this._provider != null)
			{
				this._callback = callback;
				this._provider.GetRowData(new RowCallback(this.GetRowData));
				return;
			}
			callback(null);
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x0600465D RID: 18013 RVA: 0x000E7BB8 File Offset: 0x000E5DB8
		PropertyDescriptorCollection IWebPartParameters.Schema
		{
			get
			{
				this.CheckFieldNamesLength();
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				if (this._consumerSchema != null && this._consumerFieldNames != null && this._consumerFieldNames.Length != 0)
				{
					foreach (string name in this._consumerFieldNames)
					{
						PropertyDescriptor propertyDescriptor = this._consumerSchema.Find(name, true);
						if (propertyDescriptor == null)
						{
							return new PropertyDescriptorCollection(null);
						}
						propertyDescriptorCollection.Add(propertyDescriptor);
					}
				}
				return propertyDescriptorCollection;
			}
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x000E7C28 File Offset: 0x000E5E28
		void IWebPartParameters.SetConsumerSchema(PropertyDescriptorCollection schema)
		{
			this._consumerSchema = schema;
		}

		// Token: 0x04002699 RID: 9881
		private IWebPartRow _provider;

		// Token: 0x0400269A RID: 9882
		private string[] _consumerFieldNames;

		// Token: 0x0400269B RID: 9883
		private string[] _providerFieldNames;

		// Token: 0x0400269C RID: 9884
		private PropertyDescriptorCollection _consumerSchema;

		// Token: 0x0400269D RID: 9885
		private ParametersCallback _callback;

		// Token: 0x020009F2 RID: 2546
		private sealed class RowToParametersConfigurationWizard : TransformerConfigurationWizardBase
		{
			// Token: 0x06006D2D RID: 27949 RVA: 0x00186B3B File Offset: 0x00184D3B
			public RowToParametersConfigurationWizard(RowToParametersTransformer owner)
			{
				this._owner = owner;
			}

			// Token: 0x17001E10 RID: 7696
			// (get) Token: 0x06006D2E RID: 27950 RVA: 0x00186B4A File Offset: 0x00184D4A
			protected override PropertyDescriptorCollection ConsumerSchema
			{
				get
				{
					return this._owner.ConsumerSchema;
				}
			}

			// Token: 0x17001E11 RID: 7697
			// (get) Token: 0x06006D2F RID: 27951 RVA: 0x00186B57 File Offset: 0x00184D57
			protected override PropertyDescriptorCollection ProviderSchema
			{
				get
				{
					return this._owner.ProviderSchema;
				}
			}

			// Token: 0x06006D30 RID: 27952 RVA: 0x00186B64 File Offset: 0x00184D64
			protected override void CreateWizardSteps()
			{
				int num = (base.OldProviderNames != null) ? base.OldProviderNames.Length : 0;
				if (num > 0)
				{
					this._consumerFieldNames = new DropDownList[num / 2];
					ListItem[] array = null;
					int num2 = (base.OldConsumerNames != null) ? base.OldConsumerNames.Length : 0;
					if (num2 > 0)
					{
						array = new ListItem[num2 / 2];
						for (int i = 0; i < num2 / 2; i++)
						{
							array[i] = new ListItem(base.OldConsumerNames[2 * i], base.OldConsumerNames[2 * i + 1]);
						}
					}
					for (int j = 0; j < num / 2; j++)
					{
						WizardStep wizardStep = new WizardStep();
						wizardStep.Controls.Add(new LiteralControl(SR.GetString("RowToParametersTransformer_ProviderFieldName") + " "));
						Label label = new Label();
						label.Text = HttpUtility.HtmlEncode(base.OldProviderNames[2 * j]);
						label.Font.Bold = true;
						wizardStep.Controls.Add(label);
						wizardStep.Controls.Add(new LiteralControl("<br />"));
						DropDownList dropDownList = new DropDownList();
						dropDownList.ID = "ConsumerFieldName" + j.ToString();
						if (array != null)
						{
							dropDownList.Items.Add(new ListItem());
							string[] providerFieldNames = this._owner._providerFieldNames;
							string[] consumerFieldNames = this._owner._consumerFieldNames;
							string b = base.OldProviderNames[2 * j + 1];
							string b2 = null;
							if (providerFieldNames != null)
							{
								for (int k = 0; k < providerFieldNames.Length; k++)
								{
									if (string.Equals(providerFieldNames[k], b, StringComparison.OrdinalIgnoreCase) && consumerFieldNames != null && consumerFieldNames.Length > k)
									{
										b2 = consumerFieldNames[k];
										break;
									}
								}
							}
							foreach (ListItem listItem in array)
							{
								ListItem listItem2 = new ListItem(listItem.Text, listItem.Value);
								if (string.Equals(listItem2.Value, b2, StringComparison.OrdinalIgnoreCase))
								{
									listItem2.Selected = true;
								}
								dropDownList.Items.Add(listItem2);
							}
						}
						else
						{
							dropDownList.Items.Add(new ListItem(SR.GetString("RowToParametersTransformer_NoConsumerSchema")));
							dropDownList.Enabled = false;
						}
						this._consumerFieldNames[j] = dropDownList;
						Label label2 = new Label();
						label2.Text = SR.GetString("RowToParametersTransformer_ConsumerFieldName");
						label2.AssociatedControlID = dropDownList.ID;
						wizardStep.Controls.Add(label2);
						wizardStep.Controls.Add(new LiteralControl(" "));
						wizardStep.Controls.Add(dropDownList);
						this.WizardSteps.Add(wizardStep);
					}
					return;
				}
				WizardStep wizardStep2 = new WizardStep();
				wizardStep2.Controls.Add(new LiteralControl(SR.GetString("RowToParametersTransformer_NoProviderSchema")));
				this.WizardSteps.Add(wizardStep2);
			}

			// Token: 0x06006D31 RID: 27953 RVA: 0x00186E38 File Offset: 0x00185038
			protected override void OnFinishButtonClick(WizardNavigationEventArgs e)
			{
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				int num = (base.OldProviderNames != null) ? base.OldProviderNames.Length : 0;
				if (num > 0)
				{
					for (int i = 0; i < this._consumerFieldNames.Length; i++)
					{
						DropDownList dropDownList = this._consumerFieldNames[i];
						if (dropDownList.Enabled)
						{
							string selectedValue = dropDownList.SelectedValue;
							if (!string.IsNullOrEmpty(selectedValue))
							{
								arrayList.Add(base.OldProviderNames[2 * i + 1]);
								arrayList2.Add(selectedValue);
							}
						}
					}
				}
				this._owner.ConsumerFieldNames = (string[])arrayList2.ToArray(typeof(string));
				this._owner.ProviderFieldNames = (string[])arrayList.ToArray(typeof(string));
				base.OnFinishButtonClick(e);
			}

			// Token: 0x04003A27 RID: 14887
			private DropDownList[] _consumerFieldNames;

			// Token: 0x04003A28 RID: 14888
			private RowToParametersTransformer _owner;

			// Token: 0x04003A29 RID: 14889
			private const string consumerFieldNameID = "ConsumerFieldName";
		}
	}
}
