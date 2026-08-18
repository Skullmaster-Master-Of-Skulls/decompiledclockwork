using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000696 RID: 1686
	public sealed class TransactedBatchingElement : BehaviorExtensionElement
	{
		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x000F7D0C File Offset: 0x000F5F0C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("maxBatchSize", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x000F7D62 File Offset: 0x000F5F62
		// (set) Token: 0x0600414A RID: 16714 RVA: 0x000F7D74 File Offset: 0x000F5F74
		[ConfigurationProperty("maxBatchSize", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxBatchSize
		{
			get
			{
				return (int)base["maxBatchSize"];
			}
			set
			{
				base["maxBatchSize"] = value;
			}
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x000F7D88 File Offset: 0x000F5F88
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			TransactedBatchingElement transactedBatchingElement = from as TransactedBatchingElement;
			this.MaxBatchSize = transactedBatchingElement.MaxBatchSize;
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x000F7DAF File Offset: 0x000F5FAF
		protected internal override object CreateBehavior()
		{
			return new TransactedBatchingBehavior(this.MaxBatchSize);
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x000F7DBC File Offset: 0x000F5FBC
		public override Type BehaviorType
		{
			get
			{
				return typeof(TransactedBatchingBehavior);
			}
		}

		// Token: 0x04002CE4 RID: 11492
		private ConfigurationPropertyCollection properties;
	}
}
