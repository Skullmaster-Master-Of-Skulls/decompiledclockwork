using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Security;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000676 RID: 1654
	public abstract class StandardBindingElement : ServiceModelConfigurationElement, IBindingConfigurationElement, IConfigurationContextProviderInternal
	{
		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x000F0A74 File Offset: 0x000EEC74
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("name", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("closeTimeout", typeof(TimeSpan), TimeSpan.Parse("00:01:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("openTimeout", typeof(TimeSpan), TimeSpan.Parse("00:01:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("receiveTimeout", typeof(TimeSpan), TimeSpan.Parse("00:10:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("sendTimeout", typeof(TimeSpan), TimeSpan.Parse("00:01:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x000F0C28 File Offset: 0x000EEE28
		protected StandardBindingElement() : this(null)
		{
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x000F0C31 File Offset: 0x000EEE31
		protected StandardBindingElement(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				this.Name = name;
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06003F6E RID: 16238
		protected abstract Type BindingElementType { get; }

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x000F0C48 File Offset: 0x000EEE48
		// (set) Token: 0x06003F70 RID: 16240 RVA: 0x000F0C5A File Offset: 0x000EEE5A
		[ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 0)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["name"] = value;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06003F71 RID: 16241 RVA: 0x000F0C77 File Offset: 0x000EEE77
		// (set) Token: 0x06003F72 RID: 16242 RVA: 0x000F0C89 File Offset: 0x000EEE89
		[ConfigurationProperty("closeTimeout", DefaultValue = "00:01:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan CloseTimeout
		{
			get
			{
				return (TimeSpan)base["closeTimeout"];
			}
			set
			{
				base["closeTimeout"] = value;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x000F0C9C File Offset: 0x000EEE9C
		// (set) Token: 0x06003F74 RID: 16244 RVA: 0x000F0CAE File Offset: 0x000EEEAE
		[ConfigurationProperty("openTimeout", DefaultValue = "00:01:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan OpenTimeout
		{
			get
			{
				return (TimeSpan)base["openTimeout"];
			}
			set
			{
				base["openTimeout"] = value;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x000F0CC1 File Offset: 0x000EEEC1
		// (set) Token: 0x06003F76 RID: 16246 RVA: 0x000F0CD3 File Offset: 0x000EEED3
		[ConfigurationProperty("receiveTimeout", DefaultValue = "00:10:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan ReceiveTimeout
		{
			get
			{
				return (TimeSpan)base["receiveTimeout"];
			}
			set
			{
				base["receiveTimeout"] = value;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x000F0CE6 File Offset: 0x000EEEE6
		// (set) Token: 0x06003F78 RID: 16248 RVA: 0x000F0CF8 File Offset: 0x000EEEF8
		[ConfigurationProperty("sendTimeout", DefaultValue = "00:01:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan SendTimeout
		{
			get
			{
				return (TimeSpan)base["sendTimeout"];
			}
			set
			{
				base["sendTimeout"] = value;
			}
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x000F0D0C File Offset: 0x000EEF0C
		public void ApplyConfiguration(Binding binding)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (binding.GetType() != this.BindingElementType)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigInvalidTypeForBinding", new object[]
				{
					(this.BindingElementType == null) ? string.Empty : this.BindingElementType.AssemblyQualifiedName,
					binding.GetType().AssemblyQualifiedName
				}));
			}
			binding.CloseTimeout = this.CloseTimeout;
			binding.OpenTimeout = this.OpenTimeout;
			binding.ReceiveTimeout = this.ReceiveTimeout;
			binding.SendTimeout = this.SendTimeout;
			this.OnApplyConfiguration(binding);
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x000F0DC4 File Offset: 0x000EEFC4
		protected internal virtual void InitializeFrom(Binding binding)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (binding.GetType() != this.BindingElementType)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigInvalidTypeForBinding", new object[]
				{
					(this.BindingElementType == null) ? string.Empty : this.BindingElementType.AssemblyQualifiedName,
					binding.GetType().AssemblyQualifiedName
				}));
			}
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("closeTimeout", binding.CloseTimeout);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("openTimeout", binding.OpenTimeout);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("receiveTimeout", binding.ReceiveTimeout);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("sendTimeout", binding.SendTimeout);
		}

		// Token: 0x06003F7B RID: 16251
		protected abstract void OnApplyConfiguration(Binding binding);

		// Token: 0x06003F7C RID: 16252 RVA: 0x000F0E87 File Offset: 0x000EF087
		[SecurityCritical]
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.contextHelper.OnReset(parentElement);
			base.Reset(parentElement);
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x000F0E9C File Offset: 0x000EF09C
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000F0EA4 File Offset: 0x000EF0A4
		[SecurityCritical]
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return this.contextHelper.GetOriginalContext(this);
		}

		// Token: 0x04002CB7 RID: 11447
		private ConfigurationPropertyCollection properties;

		// Token: 0x04002CB8 RID: 11448
		[SecurityCritical]
		private EvaluationContextHelper contextHelper;
	}
}
