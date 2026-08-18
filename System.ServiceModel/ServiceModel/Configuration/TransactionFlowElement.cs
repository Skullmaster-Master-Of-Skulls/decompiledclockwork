using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006B4 RID: 1716
	public class TransactionFlowElement : BindingElementExtensionElement
	{
		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x000FBA30 File Offset: 0x000F9C30
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("transactionProtocol", typeof(TransactionProtocol), "OleTransactions", new TransactionProtocolConverter(), null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("allowWildcardAction", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x000FBAAC File Offset: 0x000F9CAC
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			TransactionFlowBindingElement transactionFlowBindingElement = (TransactionFlowBindingElement)bindingElement;
			transactionFlowBindingElement.Transactions = true;
			transactionFlowBindingElement.TransactionProtocol = this.TransactionProtocol;
			transactionFlowBindingElement.AllowWildcardAction = this.AllowWildcardAction;
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x000FBAE6 File Offset: 0x000F9CE6
		// (set) Token: 0x06004282 RID: 17026 RVA: 0x000FBAF8 File Offset: 0x000F9CF8
		[ConfigurationProperty("transactionProtocol", DefaultValue = "OleTransactions")]
		[TypeConverter(typeof(TransactionProtocolConverter))]
		public TransactionProtocol TransactionProtocol
		{
			get
			{
				return (TransactionProtocol)base["transactionProtocol"];
			}
			set
			{
				base["transactionProtocol"] = value;
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x000FBB06 File Offset: 0x000F9D06
		// (set) Token: 0x06004284 RID: 17028 RVA: 0x000FBB18 File Offset: 0x000F9D18
		[ConfigurationProperty("allowWildcardAction", DefaultValue = false)]
		public bool AllowWildcardAction
		{
			get
			{
				return (bool)base["allowWildcardAction"];
			}
			set
			{
				base["allowWildcardAction"] = value;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06004285 RID: 17029 RVA: 0x000FBB2B File Offset: 0x000F9D2B
		public override Type BindingElementType
		{
			get
			{
				return typeof(TransactionFlowBindingElement);
			}
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x000FBB38 File Offset: 0x000F9D38
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			TransactionFlowElement transactionFlowElement = (TransactionFlowElement)from;
			this.TransactionProtocol = transactionFlowElement.TransactionProtocol;
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x000FBB5F File Offset: 0x000F9D5F
		protected internal override BindingElement CreateBindingElement()
		{
			return new TransactionFlowBindingElement(true, this.TransactionProtocol)
			{
				AllowWildcardAction = this.AllowWildcardAction
			};
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x000FBB7C File Offset: 0x000F9D7C
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			TransactionFlowBindingElement transactionFlowBindingElement = (TransactionFlowBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<TransactionProtocol>("transactionProtocol", transactionFlowBindingElement.TransactionProtocol);
		}

		// Token: 0x04002D03 RID: 11523
		private ConfigurationPropertyCollection properties;
	}
}
