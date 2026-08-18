using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000646 RID: 1606
	public abstract class MsmqElementBase : TransportElement
	{
		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x000EBF7F File Offset: 0x000EA17F
		// (set) Token: 0x06003DD4 RID: 15828 RVA: 0x000EBF91 File Offset: 0x000EA191
		[ConfigurationProperty("customDeadLetterQueue", DefaultValue = null)]
		public Uri CustomDeadLetterQueue
		{
			get
			{
				return (Uri)base["customDeadLetterQueue"];
			}
			set
			{
				base["customDeadLetterQueue"] = value;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x000EBF9F File Offset: 0x000EA19F
		// (set) Token: 0x06003DD6 RID: 15830 RVA: 0x000EBFB1 File Offset: 0x000EA1B1
		[ConfigurationProperty("deadLetterQueue", DefaultValue = DeadLetterQueue.System)]
		[ServiceModelEnumValidator(typeof(DeadLetterQueueHelper))]
		public DeadLetterQueue DeadLetterQueue
		{
			get
			{
				return (DeadLetterQueue)base["deadLetterQueue"];
			}
			set
			{
				base["deadLetterQueue"] = value;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x000EBFC4 File Offset: 0x000EA1C4
		// (set) Token: 0x06003DD8 RID: 15832 RVA: 0x000EBFD6 File Offset: 0x000EA1D6
		[ConfigurationProperty("durable", DefaultValue = true)]
		public bool Durable
		{
			get
			{
				return (bool)base["durable"];
			}
			set
			{
				base["durable"] = value;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x000EBFE9 File Offset: 0x000EA1E9
		// (set) Token: 0x06003DDA RID: 15834 RVA: 0x000EBFFB File Offset: 0x000EA1FB
		[ConfigurationProperty("exactlyOnce", DefaultValue = true)]
		public bool ExactlyOnce
		{
			get
			{
				return (bool)base["exactlyOnce"];
			}
			set
			{
				base["exactlyOnce"] = value;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x000EC00E File Offset: 0x000EA20E
		// (set) Token: 0x06003DDC RID: 15836 RVA: 0x000EC020 File Offset: 0x000EA220
		[ConfigurationProperty("maxRetryCycles", DefaultValue = 2)]
		[IntegerValidator(MinValue = 0)]
		public int MaxRetryCycles
		{
			get
			{
				return (int)base["maxRetryCycles"];
			}
			set
			{
				base["maxRetryCycles"] = value;
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x000EC033 File Offset: 0x000EA233
		// (set) Token: 0x06003DDE RID: 15838 RVA: 0x000EC045 File Offset: 0x000EA245
		[ConfigurationProperty("receiveContextEnabled", DefaultValue = true)]
		public bool ReceiveContextEnabled
		{
			get
			{
				return (bool)base["receiveContextEnabled"];
			}
			set
			{
				base["receiveContextEnabled"] = value;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x000EC058 File Offset: 0x000EA258
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x000EC06A File Offset: 0x000EA26A
		[ConfigurationProperty("receiveErrorHandling", DefaultValue = ReceiveErrorHandling.Fault)]
		[ServiceModelEnumValidator(typeof(ReceiveErrorHandlingHelper))]
		public ReceiveErrorHandling ReceiveErrorHandling
		{
			get
			{
				return (ReceiveErrorHandling)base["receiveErrorHandling"];
			}
			set
			{
				base["receiveErrorHandling"] = value;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06003DE1 RID: 15841 RVA: 0x000EC07D File Offset: 0x000EA27D
		// (set) Token: 0x06003DE2 RID: 15842 RVA: 0x000EC08F File Offset: 0x000EA28F
		[ConfigurationProperty("receiveRetryCount", DefaultValue = 5)]
		[IntegerValidator(MinValue = 0)]
		public int ReceiveRetryCount
		{
			get
			{
				return (int)base["receiveRetryCount"];
			}
			set
			{
				base["receiveRetryCount"] = value;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06003DE3 RID: 15843 RVA: 0x000EC0A2 File Offset: 0x000EA2A2
		// (set) Token: 0x06003DE4 RID: 15844 RVA: 0x000EC0B4 File Offset: 0x000EA2B4
		[ConfigurationProperty("retryCycleDelay", DefaultValue = "00:30:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan RetryCycleDelay
		{
			get
			{
				return (TimeSpan)base["retryCycleDelay"];
			}
			set
			{
				base["retryCycleDelay"] = value;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x000EC0C7 File Offset: 0x000EA2C7
		[ConfigurationProperty("msmqTransportSecurity")]
		public MsmqTransportSecurityElement MsmqTransportSecurity
		{
			get
			{
				return (MsmqTransportSecurityElement)base["msmqTransportSecurity"];
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06003DE6 RID: 15846 RVA: 0x000EC0D9 File Offset: 0x000EA2D9
		// (set) Token: 0x06003DE7 RID: 15847 RVA: 0x000EC0EB File Offset: 0x000EA2EB
		[ConfigurationProperty("timeToLive", DefaultValue = "1.00:00:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan TimeToLive
		{
			get
			{
				return (TimeSpan)base["timeToLive"];
			}
			set
			{
				base["timeToLive"] = value;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x000EC0FE File Offset: 0x000EA2FE
		// (set) Token: 0x06003DE9 RID: 15849 RVA: 0x000EC110 File Offset: 0x000EA310
		[ConfigurationProperty("useSourceJournal", DefaultValue = false)]
		public bool UseSourceJournal
		{
			get
			{
				return (bool)base["useSourceJournal"];
			}
			set
			{
				base["useSourceJournal"] = value;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x000EC123 File Offset: 0x000EA323
		// (set) Token: 0x06003DEB RID: 15851 RVA: 0x000EC135 File Offset: 0x000EA335
		[ConfigurationProperty("useMsmqTracing", DefaultValue = false)]
		public bool UseMsmqTracing
		{
			get
			{
				return (bool)base["useMsmqTracing"];
			}
			set
			{
				base["useMsmqTracing"] = value;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000EC148 File Offset: 0x000EA348
		// (set) Token: 0x06003DED RID: 15853 RVA: 0x000EC15A File Offset: 0x000EA35A
		[ConfigurationProperty("validityDuration", DefaultValue = "00:05:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan ValidityDuration
		{
			get
			{
				return (TimeSpan)base["validityDuration"];
			}
			set
			{
				base["validityDuration"] = value;
			}
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000EC170 File Offset: 0x000EA370
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			MsmqBindingElementBase msmqBindingElementBase = bindingElement as MsmqBindingElementBase;
			if (msmqBindingElementBase != null)
			{
				if (null != this.CustomDeadLetterQueue)
				{
					msmqBindingElementBase.CustomDeadLetterQueue = this.CustomDeadLetterQueue;
				}
				msmqBindingElementBase.DeadLetterQueue = this.DeadLetterQueue;
				msmqBindingElementBase.Durable = this.Durable;
				msmqBindingElementBase.ExactlyOnce = this.ExactlyOnce;
				msmqBindingElementBase.MaxRetryCycles = this.MaxRetryCycles;
				msmqBindingElementBase.ReceiveContextEnabled = this.ReceiveContextEnabled;
				msmqBindingElementBase.ReceiveErrorHandling = this.ReceiveErrorHandling;
				msmqBindingElementBase.ReceiveRetryCount = this.ReceiveRetryCount;
				msmqBindingElementBase.RetryCycleDelay = this.RetryCycleDelay;
				msmqBindingElementBase.TimeToLive = this.TimeToLive;
				msmqBindingElementBase.UseSourceJournal = this.UseSourceJournal;
				msmqBindingElementBase.UseMsmqTracing = this.UseMsmqTracing;
				msmqBindingElementBase.ValidityDuration = this.ValidityDuration;
				this.MsmqTransportSecurity.ApplyConfiguration(msmqBindingElementBase.MsmqTransportSecurity);
			}
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x000EC24C File Offset: 0x000EA44C
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			MsmqElementBase msmqElementBase = from as MsmqElementBase;
			if (msmqElementBase != null)
			{
				this.CustomDeadLetterQueue = msmqElementBase.CustomDeadLetterQueue;
				this.DeadLetterQueue = msmqElementBase.DeadLetterQueue;
				this.Durable = msmqElementBase.Durable;
				this.ExactlyOnce = msmqElementBase.ExactlyOnce;
				this.MaxRetryCycles = msmqElementBase.MaxRetryCycles;
				this.ReceiveContextEnabled = msmqElementBase.ReceiveContextEnabled;
				this.ReceiveErrorHandling = msmqElementBase.ReceiveErrorHandling;
				this.ReceiveRetryCount = msmqElementBase.ReceiveRetryCount;
				this.RetryCycleDelay = msmqElementBase.RetryCycleDelay;
				this.TimeToLive = msmqElementBase.TimeToLive;
				this.UseSourceJournal = msmqElementBase.UseSourceJournal;
				this.UseMsmqTracing = msmqElementBase.UseMsmqTracing;
				this.ValidityDuration = msmqElementBase.ValidityDuration;
				this.MsmqTransportSecurity.MsmqAuthenticationMode = msmqElementBase.MsmqTransportSecurity.MsmqAuthenticationMode;
				this.MsmqTransportSecurity.MsmqProtectionLevel = msmqElementBase.MsmqTransportSecurity.MsmqProtectionLevel;
				this.MsmqTransportSecurity.MsmqEncryptionAlgorithm = msmqElementBase.MsmqTransportSecurity.MsmqEncryptionAlgorithm;
				this.MsmqTransportSecurity.MsmqSecureHashAlgorithm = msmqElementBase.MsmqTransportSecurity.MsmqSecureHashAlgorithm;
			}
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x000EC364 File Offset: 0x000EA564
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			MsmqBindingElementBase msmqBindingElementBase = bindingElement as MsmqBindingElementBase;
			if (msmqBindingElementBase != null)
			{
				base.SetPropertyValueIfNotDefaultValue<Uri>("customDeadLetterQueue", msmqBindingElementBase.CustomDeadLetterQueue);
				base.SetPropertyValueIfNotDefaultValue<DeadLetterQueue>("deadLetterQueue", msmqBindingElementBase.DeadLetterQueue);
				base.SetPropertyValueIfNotDefaultValue<bool>("durable", msmqBindingElementBase.Durable);
				base.SetPropertyValueIfNotDefaultValue<bool>("exactlyOnce", msmqBindingElementBase.ExactlyOnce);
				base.SetPropertyValueIfNotDefaultValue<int>("maxRetryCycles", msmqBindingElementBase.MaxRetryCycles);
				base.SetPropertyValueIfNotDefaultValue<ReceiveErrorHandling>("receiveErrorHandling", msmqBindingElementBase.ReceiveErrorHandling);
				base.SetPropertyValueIfNotDefaultValue<int>("receiveRetryCount", msmqBindingElementBase.ReceiveRetryCount);
				base.SetPropertyValueIfNotDefaultValue<TimeSpan>("retryCycleDelay", msmqBindingElementBase.RetryCycleDelay);
				base.SetPropertyValueIfNotDefaultValue<TimeSpan>("timeToLive", msmqBindingElementBase.TimeToLive);
				base.SetPropertyValueIfNotDefaultValue<bool>("useSourceJournal", msmqBindingElementBase.UseSourceJournal);
				base.SetPropertyValueIfNotDefaultValue<bool>("receiveContextEnabled", msmqBindingElementBase.ReceiveContextEnabled);
				base.SetPropertyValueIfNotDefaultValue<bool>("useMsmqTracing", msmqBindingElementBase.UseMsmqTracing);
				if (msmqBindingElementBase.ValidityDuration != MsmqDefaults.ValidityDuration)
				{
					this.ValidityDuration = msmqBindingElementBase.ValidityDuration;
				}
				this.MsmqTransportSecurity.InitializeFrom(msmqBindingElementBase.MsmqTransportSecurity);
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x000EC480 File Offset: 0x000EA680
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("customDeadLetterQueue", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("deadLetterQueue", typeof(DeadLetterQueue), DeadLetterQueue.System, null, new ServiceModelEnumValidator(typeof(DeadLetterQueueHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("durable", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("exactlyOnce", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxRetryCycles", typeof(int), 2, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("receiveContextEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("receiveErrorHandling", typeof(ReceiveErrorHandling), ReceiveErrorHandling.Fault, null, new ServiceModelEnumValidator(typeof(ReceiveErrorHandlingHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("receiveRetryCount", typeof(int), 5, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("retryCycleDelay", typeof(TimeSpan), TimeSpan.Parse("00:30:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("msmqTransportSecurity", typeof(MsmqTransportSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("timeToLive", typeof(TimeSpan), TimeSpan.Parse("1.00:00:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("useSourceJournal", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("useMsmqTracing", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("validityDuration", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C98 RID: 11416
		private ConfigurationPropertyCollection properties;
	}
}
