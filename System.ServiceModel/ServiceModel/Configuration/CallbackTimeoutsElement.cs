using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000603 RID: 1539
	public sealed class CallbackTimeoutsElement : BehaviorExtensionElement
	{
		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000E31A4 File Offset: 0x000E13A4
		// (set) Token: 0x06003B4C RID: 15180 RVA: 0x000E31B6 File Offset: 0x000E13B6
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

		// Token: 0x06003B4D RID: 15181 RVA: 0x000E31CC File Offset: 0x000E13CC
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			CallbackTimeoutsElement callbackTimeoutsElement = (CallbackTimeoutsElement)from;
			this.TransactionTimeout = callbackTimeoutsElement.TransactionTimeout;
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000E31F4 File Offset: 0x000E13F4
		protected internal override object CreateBehavior()
		{
			return new CallbackTimeoutsBehavior
			{
				TransactionTimeout = this.TransactionTimeout
			};
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x000E3214 File Offset: 0x000E1414
		public override Type BehaviorType
		{
			get
			{
				return typeof(CallbackTimeoutsBehavior);
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000E3220 File Offset: 0x000E1420
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

		// Token: 0x04002A83 RID: 10883
		private ConfigurationPropertyCollection properties;
	}
}
