using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000570 RID: 1392
	internal abstract class TransformerConfigurationWizardBase : Wizard, ITransformerConfigurationControl
	{
		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x0600469F RID: 18079
		protected abstract PropertyDescriptorCollection ConsumerSchema { get; }

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x060046A0 RID: 18080 RVA: 0x000E9A43 File Offset: 0x000E7C43
		// (set) Token: 0x060046A1 RID: 18081 RVA: 0x000E9A4B File Offset: 0x000E7C4B
		protected string[] OldConsumerNames
		{
			get
			{
				return this._oldConsumerNames;
			}
			set
			{
				this._oldConsumerNames = value;
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x000E9A54 File Offset: 0x000E7C54
		// (set) Token: 0x060046A3 RID: 18083 RVA: 0x000E9A5C File Offset: 0x000E7C5C
		protected string[] OldProviderNames
		{
			get
			{
				return this._oldProviderNames;
			}
			set
			{
				this._oldProviderNames = value;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x060046A4 RID: 18084
		protected abstract PropertyDescriptorCollection ProviderSchema { get; }

		// Token: 0x14000110 RID: 272
		// (add) Token: 0x060046A5 RID: 18085 RVA: 0x000E9A65 File Offset: 0x000E7C65
		// (remove) Token: 0x060046A6 RID: 18086 RVA: 0x000E9A78 File Offset: 0x000E7C78
		public event EventHandler Cancelled
		{
			add
			{
				base.Events.AddHandler(TransformerConfigurationWizardBase.EventCancelled, value);
			}
			remove
			{
				base.Events.RemoveHandler(TransformerConfigurationWizardBase.EventCancelled, value);
			}
		}

		// Token: 0x14000111 RID: 273
		// (add) Token: 0x060046A7 RID: 18087 RVA: 0x000E9A8B File Offset: 0x000E7C8B
		// (remove) Token: 0x060046A8 RID: 18088 RVA: 0x000E9A9E File Offset: 0x000E7C9E
		public event EventHandler Succeeded
		{
			add
			{
				base.Events.AddHandler(TransformerConfigurationWizardBase.EventSucceeded, value);
			}
			remove
			{
				base.Events.RemoveHandler(TransformerConfigurationWizardBase.EventSucceeded, value);
			}
		}

		// Token: 0x060046A9 RID: 18089
		protected abstract void CreateWizardSteps();

		// Token: 0x060046AA RID: 18090 RVA: 0x000E9AB4 File Offset: 0x000E7CB4
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				this.CreateWizardSteps();
				base.LoadControlState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 3)
			{
				throw new ArgumentException(SR.GetString("Invalid_ControlState"));
			}
			if (array[1] != null)
			{
				this.OldProviderNames = (string[])array[1];
			}
			if (array[2] != null)
			{
				this.OldConsumerNames = (string[])array[2];
			}
			this.CreateWizardSteps();
			base.LoadControlState(array[0]);
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x000E9B24 File Offset: 0x000E7D24
		protected override void OnCancelButtonClick(EventArgs e)
		{
			this.OnCancelled(EventArgs.Empty);
			base.OnCancelButtonClick(e);
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x000E9B38 File Offset: 0x000E7D38
		private void OnCancelled(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TransformerConfigurationWizardBase.EventCancelled];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x000E9B66 File Offset: 0x000E7D66
		protected override void OnFinishButtonClick(WizardNavigationEventArgs e)
		{
			this.OnSucceeded(EventArgs.Empty);
			base.OnFinishButtonClick(e);
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000E9B7C File Offset: 0x000E7D7C
		protected internal override void OnInit(EventArgs e)
		{
			this.DisplayCancelButton = true;
			this.DisplaySideBar = false;
			if (this.Page != null)
			{
				this.Page.RegisterRequiresControlState(this);
				this.Page.PreRenderComplete += this.OnPagePreRenderComplete;
			}
			base.OnInit(e);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000E9BCC File Offset: 0x000E7DCC
		private void OnPagePreRenderComplete(object sender, EventArgs e)
		{
			string[] array = this.ConvertSchemaToArray(this.ProviderSchema);
			string[] array2 = this.ConvertSchemaToArray(this.ConsumerSchema);
			if (this.StringArraysDifferent(array, this.OldProviderNames) || this.StringArraysDifferent(array2, this.OldConsumerNames) || this.WizardSteps.Count == 0)
			{
				this.OldProviderNames = array;
				this.OldConsumerNames = array2;
				this.WizardSteps.Clear();
				base.ClearChildState();
				this.CreateWizardSteps();
				this.ActiveStepIndex = 0;
			}
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000E9C4C File Offset: 0x000E7E4C
		private string[] ConvertSchemaToArray(PropertyDescriptorCollection schema)
		{
			string[] array = null;
			if (schema != null && schema.Count > 0)
			{
				array = new string[schema.Count * 2];
				for (int i = 0; i < schema.Count; i++)
				{
					PropertyDescriptor propertyDescriptor = schema[i];
					if (propertyDescriptor != null)
					{
						array[2 * i] = propertyDescriptor.DisplayName;
						array[2 * i + 1] = propertyDescriptor.Name;
					}
				}
			}
			return array;
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x000E9CAC File Offset: 0x000E7EAC
		private void OnSucceeded(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TransformerConfigurationWizardBase.EventSucceeded];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x000E9CDC File Offset: 0x000E7EDC
		protected internal override object SaveControlState()
		{
			object[] array = new object[]
			{
				base.SaveControlState(),
				this.OldProviderNames,
				this.OldConsumerNames
			};
			for (int i = 0; i < 3; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x000E9D20 File Offset: 0x000E7F20
		private bool StringArraysDifferent(string[] arrA, string[] arrB)
		{
			int num = (arrA == null) ? 0 : arrA.Length;
			int num2 = (arrB == null) ? 0 : arrB.Length;
			if (num != num2)
			{
				return true;
			}
			for (int i = 0; i < num2; i++)
			{
				if (arrA[i] != arrB[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040026B3 RID: 9907
		private string[] _oldProviderNames;

		// Token: 0x040026B4 RID: 9908
		private string[] _oldConsumerNames;

		// Token: 0x040026B5 RID: 9909
		private const int baseIndex = 0;

		// Token: 0x040026B6 RID: 9910
		private const int oldProviderNamesIndex = 1;

		// Token: 0x040026B7 RID: 9911
		private const int oldConsumerNamesIndex = 2;

		// Token: 0x040026B8 RID: 9912
		private const int controlStateArrayLength = 3;

		// Token: 0x040026B9 RID: 9913
		private static readonly object EventCancelled = new object();

		// Token: 0x040026BA RID: 9914
		private static readonly object EventSucceeded = new object();
	}
}
