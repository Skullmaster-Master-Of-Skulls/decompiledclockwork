using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006F7 RID: 1783
	[WebPartTransformer(typeof(IWebPartRow), typeof(IWebPartParameters))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RowToParametersTransformer : WebPartTransformer, IWebPartParameters
	{
		// Token: 0x0600572B RID: 22315 RVA: 0x0015EFE3 File Offset: 0x0015DFE3
		public override Control CreateConfigurationControl()
		{
			return new RowToParametersTransformer.RowToParametersConfigurationWizard(this);
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x0600572C RID: 22316 RVA: 0x0015EFEB File Offset: 0x0015DFEB
		// (set) Token: 0x0600572D RID: 22317 RVA: 0x0015F00C File Offset: 0x0015E00C
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

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x0600572E RID: 22318 RVA: 0x0015F025 File Offset: 0x0015E025
		private PropertyDescriptorCollection ConsumerSchema
		{
			get
			{
				return this._consumerSchema;
			}
		}

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x0600572F RID: 22319 RVA: 0x0015F02D File Offset: 0x0015E02D
		// (set) Token: 0x06005730 RID: 22320 RVA: 0x0015F04E File Offset: 0x0015E04E
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

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06005731 RID: 22321 RVA: 0x0015F067 File Offset: 0x0015E067
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

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06005732 RID: 22322 RVA: 0x0015F080 File Offset: 0x0015E080
		private PropertyDescriptorCollection SelectedProviderSchema
		{
			get
			{
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				PropertyDescriptorCollection providerSchema = this.ProviderSchema;
				if (providerSchema != null && this._providerFieldNames != null && this._providerFieldNames.Length > 0)
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

		// Token: 0x06005733 RID: 22323 RVA: 0x0015F0F4 File Offset: 0x0015E0F4
		private void CheckFieldNamesLength()
		{
			int num = (this._consumerFieldNames != null) ? this._consumerFieldNames.Length : 0;
			int num2 = (this._providerFieldNames != null) ? this._providerFieldNames.Length : 0;
			if (num != num2)
			{
				throw new InvalidOperationException(SR.GetString("RowToParametersTransformer_DifferentFieldNamesLength"));
			}
		}

		// Token: 0x06005734 RID: 22324 RVA: 0x0015F140 File Offset: 0x0015E140
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

		// Token: 0x06005735 RID: 22325 RVA: 0x0015F1D4 File Offset: 0x0015E1D4
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

		// Token: 0x06005736 RID: 22326 RVA: 0x0015F248 File Offset: 0x0015E248
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

		// Token: 0x06005737 RID: 22327 RVA: 0x0015F2A7 File Offset: 0x0015E2A7
		public override object Transform(object providerData)
		{
			this._provider = (IWebPartRow)providerData;
			return this;
		}

		// Token: 0x06005738 RID: 22328 RVA: 0x0015F2B8 File Offset: 0x0015E2B8
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

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06005739 RID: 22329 RVA: 0x0015F308 File Offset: 0x0015E308
		PropertyDescriptorCollection IWebPartParameters.Schema
		{
			get
			{
				this.CheckFieldNamesLength();
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				if (this._consumerSchema != null && this._consumerFieldNames != null && this._consumerFieldNames.Length > 0)
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

		// Token: 0x0600573A RID: 22330 RVA: 0x0015F383 File Offset: 0x0015E383
		void IWebPartParameters.SetConsumerSchema(PropertyDescriptorCollection schema)
		{
			this._consumerSchema = schema;
		}

		// Token: 0x04002F89 RID: 12169
		private IWebPartRow _provider;

		// Token: 0x04002F8A RID: 12170
		private string[] _consumerFieldNames;

		// Token: 0x04002F8B RID: 12171
		private string[] _providerFieldNames;

		// Token: 0x04002F8C RID: 12172
		private PropertyDescriptorCollection _consumerSchema;

		// Token: 0x04002F8D RID: 12173
		private ParametersCallback _callback;

		// Token: 0x020006F8 RID: 1784
		private sealed class RowToParametersConfigurationWizard : TransformerConfigurationWizardBase
		{
			// Token: 0x0600573C RID: 22332 RVA: 0x0015F394 File Offset: 0x0015E394
			public RowToParametersConfigurationWizard(RowToParametersTransformer owner)
			{
				this._owner = owner;
			}

			// Token: 0x17001685 RID: 5765
			// (get) Token: 0x0600573D RID: 22333 RVA: 0x0015F3A3 File Offset: 0x0015E3A3
			protected override PropertyDescriptorCollection ConsumerSchema
			{
				get
				{
					return this._owner.ConsumerSchema;
				}
			}

			// Token: 0x17001686 RID: 5766
			// (get) Token: 0x0600573E RID: 22334 RVA: 0x0015F3B0 File Offset: 0x0015E3B0
			protected override PropertyDescriptorCollection ProviderSchema
			{
				get
				{
					return this._owner.ProviderSchema;
				}
			}

			// Token: 0x0600573F RID: 22335 RVA: 0x0015F3C0 File Offset: 0x0015E3C0
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
						dropDownList.ID = "ConsumerFieldName" + j;
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

			// Token: 0x06005740 RID: 22336 RVA: 0x0015F694 File Offset: 0x0015E694
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

			// Token: 0x04002F8E RID: 12174
			private const string consumerFieldNameID = "ConsumerFieldName";

			// Token: 0x04002F8F RID: 12175
			private DropDownList[] _consumerFieldNames;

			// Token: 0x04002F90 RID: 12176
			private RowToParametersTransformer _owner;
		}
	}
}
