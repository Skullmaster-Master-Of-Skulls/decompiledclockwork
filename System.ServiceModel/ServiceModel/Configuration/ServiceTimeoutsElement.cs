using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006BB RID: 1723
	public sealed class ServiceTimeoutsElement : BehaviorExtensionElement
	{
		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x000FCBF8 File Offset: 0x000FADF8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("transactionTimeout", typeof(TimeSpan), TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x000FCC7F File Offset: 0x000FAE7F
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x000FCC91 File Offset: 0x000FAE91
		[ConfigurationProperty("transactionTimeout", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan TransactionTimeout
		{
			get
			{
				return (TimeSpan)base["transactionTimeout"];
			}
			set
			{
				base["transactionTimeout"] = value;
			}
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x000FCCA4 File Offset: 0x000FAEA4
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ServiceTimeoutsElement serviceTimeoutsElement = (ServiceTimeoutsElement)from;
			this.TransactionTimeout = serviceTimeoutsElement.TransactionTimeout;
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x000FCCCB File Offset: 0x000FAECB
		protected internal override object CreateBehavior()
		{
			return new ServiceTimeoutsBehavior(this.TransactionTimeout);
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x000FCCD8 File Offset: 0x000FAED8
		public override Type BehaviorType
		{
			get
			{
				return typeof(ServiceTimeoutsBehavior);
			}
		}

		// Token: 0x04002D0B RID: 11531
		private ConfigurationPropertyCollection properties;
	}
}
