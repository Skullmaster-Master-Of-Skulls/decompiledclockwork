using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000569 RID: 1385
	[WebPartTransformer(typeof(IWebPartRow), typeof(IWebPartField))]
	public sealed class RowToFieldTransformer : WebPartTransformer, IWebPartField
	{
		// Token: 0x06004644 RID: 17988 RVA: 0x000E7798 File Offset: 0x000E5998
		public override Control CreateConfigurationControl()
		{
			return new RowToFieldTransformer.RowToFieldConfigurationWizard(this);
		}

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x000E77A0 File Offset: 0x000E59A0
		// (set) Token: 0x06004646 RID: 17990 RVA: 0x000E77B6 File Offset: 0x000E59B6
		public string FieldName
		{
			get
			{
				if (this._fieldName == null)
				{
					return string.Empty;
				}
				return this._fieldName;
			}
			set
			{
				this._fieldName = value;
			}
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06004647 RID: 17991 RVA: 0x000E77BF File Offset: 0x000E59BF
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

		// Token: 0x06004648 RID: 17992 RVA: 0x000E77D8 File Offset: 0x000E59D8
		private void GetRowData(object rowData)
		{
			object fieldValue = null;
			if (rowData != null)
			{
				PropertyDescriptor schema = ((IWebPartField)this).Schema;
				if (schema != null)
				{
					fieldValue = schema.GetValue(rowData);
				}
			}
			this._callback(fieldValue);
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x000E7808 File Offset: 0x000E5A08
		protected internal override void LoadConfigurationState(object savedState)
		{
			this._fieldName = (string)savedState;
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000E7816 File Offset: 0x000E5A16
		protected internal override object SaveConfigurationState()
		{
			return this._fieldName;
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x000E781E File Offset: 0x000E5A1E
		public override object Transform(object providerData)
		{
			this._provider = (IWebPartRow)providerData;
			return this;
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x000E782D File Offset: 0x000E5A2D
		void IWebPartField.GetFieldValue(FieldCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (this._provider != null)
			{
				this._callback = callback;
				this._provider.GetRowData(new RowCallback(this.GetRowData));
				return;
			}
			callback(null);
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x0600464D RID: 17997 RVA: 0x000E786C File Offset: 0x000E5A6C
		PropertyDescriptor IWebPartField.Schema
		{
			get
			{
				PropertyDescriptorCollection providerSchema = this.ProviderSchema;
				if (providerSchema == null)
				{
					return null;
				}
				return providerSchema.Find(this.FieldName, true);
			}
		}

		// Token: 0x04002696 RID: 9878
		private IWebPartRow _provider;

		// Token: 0x04002697 RID: 9879
		private string _fieldName;

		// Token: 0x04002698 RID: 9880
		private FieldCallback _callback;

		// Token: 0x020009F1 RID: 2545
		private sealed class RowToFieldConfigurationWizard : TransformerConfigurationWizardBase
		{
			// Token: 0x06006D28 RID: 27944 RVA: 0x001869B5 File Offset: 0x00184BB5
			public RowToFieldConfigurationWizard(RowToFieldTransformer owner)
			{
				this._owner = owner;
			}

			// Token: 0x17001E0E RID: 7694
			// (get) Token: 0x06006D29 RID: 27945 RVA: 0x0000298D File Offset: 0x00000B8D
			protected override PropertyDescriptorCollection ConsumerSchema
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001E0F RID: 7695
			// (get) Token: 0x06006D2A RID: 27946 RVA: 0x001869C4 File Offset: 0x00184BC4
			protected override PropertyDescriptorCollection ProviderSchema
			{
				get
				{
					return this._owner.ProviderSchema;
				}
			}

			// Token: 0x06006D2B RID: 27947 RVA: 0x001869D4 File Offset: 0x00184BD4
			protected override void CreateWizardSteps()
			{
				WizardStep wizardStep = new WizardStep();
				this._fieldName = new DropDownList();
				this._fieldName.ID = "FieldName";
				if (base.OldProviderNames != null)
				{
					for (int i = 0; i < base.OldProviderNames.Length / 2; i++)
					{
						ListItem listItem = new ListItem(base.OldProviderNames[2 * i], base.OldProviderNames[2 * i + 1]);
						if (string.Equals(listItem.Value, this._owner.FieldName, StringComparison.OrdinalIgnoreCase))
						{
							listItem.Selected = true;
						}
						this._fieldName.Items.Add(listItem);
					}
				}
				else
				{
					this._fieldName.Items.Add(new ListItem(SR.GetString("RowToFieldTransformer_NoProviderSchema")));
					this._fieldName.Enabled = false;
				}
				Label label = new Label();
				label.Text = SR.GetString("RowToFieldTransformer_FieldName");
				label.AssociatedControlID = this._fieldName.ID;
				wizardStep.Controls.Add(label);
				wizardStep.Controls.Add(new LiteralControl(" "));
				wizardStep.Controls.Add(this._fieldName);
				this.WizardSteps.Add(wizardStep);
			}

			// Token: 0x06006D2C RID: 27948 RVA: 0x00186B00 File Offset: 0x00184D00
			protected override void OnFinishButtonClick(WizardNavigationEventArgs e)
			{
				string fieldName = null;
				if (this._fieldName.Enabled)
				{
					fieldName = this._fieldName.SelectedValue;
				}
				this._owner.FieldName = fieldName;
				base.OnFinishButtonClick(e);
			}

			// Token: 0x04003A24 RID: 14884
			private DropDownList _fieldName;

			// Token: 0x04003A25 RID: 14885
			private RowToFieldTransformer _owner;

			// Token: 0x04003A26 RID: 14886
			private const string fieldNameID = "FieldName";
		}
	}
}
