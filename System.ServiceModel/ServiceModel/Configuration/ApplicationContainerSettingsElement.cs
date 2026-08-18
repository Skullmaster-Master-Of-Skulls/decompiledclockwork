using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005DF RID: 1503
	public sealed class ApplicationContainerSettingsElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003A3E RID: 14910 RVA: 0x000E0634 File Offset: 0x000DE834
		// (set) Token: 0x06003A3F RID: 14911 RVA: 0x000E0646 File Offset: 0x000DE846
		[ConfigurationProperty("packageFullName", DefaultValue = null)]
		[StringValidator(MinLength = 0)]
		public string PackageFullName
		{
			get
			{
				return (string)base["packageFullName"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["packageFullName"] = value;
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x000E0663 File Offset: 0x000DE863
		// (set) Token: 0x06003A41 RID: 14913 RVA: 0x000E0675 File Offset: 0x000DE875
		[ConfigurationProperty("sessionId", DefaultValue = "CurrentSession")]
		[TypeConverter(typeof(SessionIdTypeConvertor))]
		[ApplicationContainerSettingsElement.SessionIdTypeValidatorAttribute]
		public int SessionId
		{
			get
			{
				return (int)base["sessionId"];
			}
			set
			{
				base["sessionId"] = value;
			}
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000E0688 File Offset: 0x000DE888
		internal void ApplyConfiguration(ApplicationContainerSettings settings)
		{
			if (settings == null)
			{
				throw FxTrace.Exception.ArgumentNull("settings");
			}
			settings.PackageFullName = this.PackageFullName;
			settings.SessionId = this.SessionId;
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000E06B5 File Offset: 0x000DE8B5
		internal void InitializeFrom(ApplicationContainerSettings settings)
		{
			if (settings == null)
			{
				throw FxTrace.Exception.ArgumentNull("settings");
			}
			base.SetPropertyValueIfNotDefaultValue<string>("packageFullName", settings.PackageFullName);
			base.SetPropertyValueIfNotDefaultValue<int>("sessionId", settings.SessionId);
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000E06EC File Offset: 0x000DE8EC
		internal void CopyFrom(ApplicationContainerSettingsElement source)
		{
			if (source == null)
			{
				throw FxTrace.Exception.ArgumentNull("source");
			}
			this.PackageFullName = source.PackageFullName;
			this.SessionId = source.SessionId;
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x000E071C File Offset: 0x000DE91C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("packageFullName", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("sessionId", typeof(int), "CurrentSession", new SessionIdTypeConvertor(), new ApplicationContainerSettingsElement.SessionIdTypeValidator(), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A56 RID: 10838
		private ConfigurationPropertyCollection properties;

		// Token: 0x02000CC0 RID: 3264
		private class SessionIdTypeValidator : IntegerValidator
		{
			// Token: 0x06007987 RID: 31111 RVA: 0x001C5756 File Offset: 0x001C3956
			public SessionIdTypeValidator() : base(1, int.MaxValue)
			{
			}

			// Token: 0x06007988 RID: 31112 RVA: 0x001C5764 File Offset: 0x001C3964
			public override void Validate(object value)
			{
				int num = (int)value;
				if (num == -1 || num == 0)
				{
					return;
				}
				try
				{
					base.Validate(value);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					throw FxTrace.Exception.AsError(new InvalidEnumArgumentException(SR.GetString("SessionValueInvalid", new object[]
					{
						value
					})));
				}
			}
		}

		// Token: 0x02000CC1 RID: 3265
		[AttributeUsage(AttributeTargets.Property)]
		private sealed class SessionIdTypeValidatorAttribute : ConfigurationValidatorAttribute
		{
			// Token: 0x17001B96 RID: 7062
			// (get) Token: 0x06007989 RID: 31113 RVA: 0x001C57CC File Offset: 0x001C39CC
			public override ConfigurationValidatorBase ValidatorInstance
			{
				get
				{
					return new ApplicationContainerSettingsElement.SessionIdTypeValidator();
				}
			}
		}
	}
}
