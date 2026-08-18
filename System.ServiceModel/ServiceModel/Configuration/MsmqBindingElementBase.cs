using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000641 RID: 1601
	public abstract class MsmqBindingElementBase : StandardBindingElement
	{
		// Token: 0x06003D97 RID: 15767 RVA: 0x000EB444 File Offset: 0x000E9644
		protected MsmqBindingElementBase(string name) : base(name)
		{
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x000EB44D File Offset: 0x000E964D
		protected MsmqBindingElementBase() : this(null)
		{
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x000EB456 File Offset: 0x000E9656
		// (set) Token: 0x06003D9A RID: 15770 RVA: 0x000EB468 File Offset: 0x000E9668
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

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06003D9B RID: 15771 RVA: 0x000EB476 File Offset: 0x000E9676
		// (set) Token: 0x06003D9C RID: 15772 RVA: 0x000EB488 File Offset: 0x000E9688
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

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06003D9D RID: 15773 RVA: 0x000EB49B File Offset: 0x000E969B
		// (set) Token: 0x06003D9E RID: 15774 RVA: 0x000EB4AD File Offset: 0x000E96AD
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

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06003D9F RID: 15775 RVA: 0x000EB4C0 File Offset: 0x000E96C0
		// (set) Token: 0x06003DA0 RID: 15776 RVA: 0x000EB4D2 File Offset: 0x000E96D2
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

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x000EB4E5 File Offset: 0x000E96E5
		// (set) Token: 0x06003DA2 RID: 15778 RVA: 0x000EB4F7 File Offset: 0x000E96F7
		[ConfigurationProperty("maxReceivedMessageSize", DefaultValue = 65536L)]
		[LongValidator(MinValue = 0L)]
		public long MaxReceivedMessageSize
		{
			get
			{
				return (long)base["maxReceivedMessageSize"];
			}
			set
			{
				base["maxReceivedMessageSize"] = value;
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x000EB50A File Offset: 0x000E970A
		// (set) Token: 0x06003DA4 RID: 15780 RVA: 0x000EB51C File Offset: 0x000E971C
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

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x000EB52F File Offset: 0x000E972F
		// (set) Token: 0x06003DA6 RID: 15782 RVA: 0x000EB541 File Offset: 0x000E9741
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

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x000EB554 File Offset: 0x000E9754
		// (set) Token: 0x06003DA8 RID: 15784 RVA: 0x000EB566 File Offset: 0x000E9766
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

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x000EB579 File Offset: 0x000E9779
		// (set) Token: 0x06003DAA RID: 15786 RVA: 0x000EB58B File Offset: 0x000E978B
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

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06003DAB RID: 15787 RVA: 0x000EB59E File Offset: 0x000E979E
		// (set) Token: 0x06003DAC RID: 15788 RVA: 0x000EB5B0 File Offset: 0x000E97B0
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

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06003DAD RID: 15789 RVA: 0x000EB5C3 File Offset: 0x000E97C3
		// (set) Token: 0x06003DAE RID: 15790 RVA: 0x000EB5D5 File Offset: 0x000E97D5
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

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x000EB5E8 File Offset: 0x000E97E8
		// (set) Token: 0x06003DB0 RID: 15792 RVA: 0x000EB5FA File Offset: 0x000E97FA
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

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06003DB1 RID: 15793 RVA: 0x000EB60D File Offset: 0x000E980D
		// (set) Token: 0x06003DB2 RID: 15794 RVA: 0x000EB61F File Offset: 0x000E981F
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

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x000EB632 File Offset: 0x000E9832
		// (set) Token: 0x06003DB4 RID: 15796 RVA: 0x000EB644 File Offset: 0x000E9844
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

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000EB658 File Offset: 0x000E9858
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			MsmqBindingBase msmqBindingBase = (MsmqBindingBase)binding;
			base.SetPropertyValueIfNotDefaultValue<DeadLetterQueue>("deadLetterQueue", msmqBindingBase.DeadLetterQueue);
			base.SetPropertyValueIfNotDefaultValue<Uri>("customDeadLetterQueue", msmqBindingBase.CustomDeadLetterQueue);
			base.SetPropertyValueIfNotDefaultValue<bool>("durable", msmqBindingBase.Durable);
			base.SetPropertyValueIfNotDefaultValue<bool>("exactlyOnce", msmqBindingBase.ExactlyOnce);
			base.SetPropertyValueIfNotDefaultValue<long>("maxReceivedMessageSize", msmqBindingBase.MaxReceivedMessageSize);
			base.SetPropertyValueIfNotDefaultValue<int>("maxRetryCycles", msmqBindingBase.MaxRetryCycles);
			base.SetPropertyValueIfNotDefaultValue<bool>("receiveContextEnabled", msmqBindingBase.ReceiveContextEnabled);
			base.SetPropertyValueIfNotDefaultValue<ReceiveErrorHandling>("receiveErrorHandling", msmqBindingBase.ReceiveErrorHandling);
			base.SetPropertyValueIfNotDefaultValue<int>("receiveRetryCount", msmqBindingBase.ReceiveRetryCount);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("retryCycleDelay", msmqBindingBase.RetryCycleDelay);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("timeToLive", msmqBindingBase.TimeToLive);
			base.SetPropertyValueIfNotDefaultValue<bool>("useSourceJournal", msmqBindingBase.UseSourceJournal);
			base.SetPropertyValueIfNotDefaultValue<bool>("useMsmqTracing", msmqBindingBase.UseMsmqTracing);
			if (msmqBindingBase.ValidityDuration != MsmqDefaults.ValidityDuration)
			{
				this.ValidityDuration = msmqBindingBase.ValidityDuration;
			}
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x000EB770 File Offset: 0x000E9970
		protected override void OnApplyConfiguration(Binding binding)
		{
			MsmqBindingBase msmqBindingBase = (MsmqBindingBase)binding;
			if (this.CustomDeadLetterQueue != null)
			{
				msmqBindingBase.CustomDeadLetterQueue = this.CustomDeadLetterQueue;
			}
			msmqBindingBase.DeadLetterQueue = this.DeadLetterQueue;
			msmqBindingBase.Durable = this.Durable;
			msmqBindingBase.ExactlyOnce = this.ExactlyOnce;
			msmqBindingBase.MaxReceivedMessageSize = this.MaxReceivedMessageSize;
			msmqBindingBase.MaxRetryCycles = this.MaxRetryCycles;
			msmqBindingBase.ReceiveContextEnabled = this.ReceiveContextEnabled;
			msmqBindingBase.ReceiveErrorHandling = this.ReceiveErrorHandling;
			msmqBindingBase.ReceiveRetryCount = this.ReceiveRetryCount;
			msmqBindingBase.RetryCycleDelay = this.RetryCycleDelay;
			msmqBindingBase.TimeToLive = this.TimeToLive;
			msmqBindingBase.UseSourceJournal = this.UseSourceJournal;
			msmqBindingBase.UseMsmqTracing = this.UseMsmqTracing;
			msmqBindingBase.ValidityDuration = this.ValidityDuration;
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06003DB7 RID: 15799 RVA: 0x000EB83C File Offset: 0x000E9A3C
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
							configurationPropertyCollection.Add(new ConfigurationProperty("maxReceivedMessageSize", typeof(long), 65536L, null, new LongValidator(0L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxRetryCycles", typeof(int), 2, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("receiveContextEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("receiveErrorHandling", typeof(ReceiveErrorHandling), ReceiveErrorHandling.Fault, null, new ServiceModelEnumValidator(typeof(ReceiveErrorHandlingHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("receiveRetryCount", typeof(int), 5, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("retryCycleDelay", typeof(TimeSpan), TimeSpan.Parse("00:30:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
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

		// Token: 0x04002C94 RID: 11412
		private ConfigurationPropertyCollection properties;
	}
}
