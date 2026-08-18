using System;
using System.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x0200073E RID: 1854
	public sealed class ProtocolElement : ConfigurationElement
	{
		// Token: 0x06005952 RID: 22866 RVA: 0x00137744 File Offset: 0x00135944
		static ProtocolElement()
		{
			ProtocolElement._properties = new ConfigurationPropertyCollection();
			ProtocolElement._properties.Add(ProtocolElement._propName);
			ProtocolElement._properties.Add(ProtocolElement._propProcessHandlerType);
			ProtocolElement._properties.Add(ProtocolElement._propAppDomainHandlerType);
			ProtocolElement._properties.Add(ProtocolElement._propValidate);
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x0013780B File Offset: 0x00135A0B
		public ProtocolElement(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("name");
			}
			base[ProtocolElement._propName] = name;
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x00117E9E File Offset: 0x0011609E
		public ProtocolElement()
		{
		}

		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x06005955 RID: 22869 RVA: 0x00137832 File Offset: 0x00135A32
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProtocolElement._properties;
			}
		}

		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x06005956 RID: 22870 RVA: 0x00137839 File Offset: 0x00135A39
		// (set) Token: 0x06005957 RID: 22871 RVA: 0x0013784B File Offset: 0x00135A4B
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[ProtocolElement._propName];
			}
			set
			{
				base[ProtocolElement._propName] = value;
			}
		}

		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x06005958 RID: 22872 RVA: 0x00137859 File Offset: 0x00135A59
		// (set) Token: 0x06005959 RID: 22873 RVA: 0x0013786B File Offset: 0x00135A6B
		[ConfigurationProperty("processHandlerType")]
		public string ProcessHandlerType
		{
			get
			{
				return (string)base[ProtocolElement._propProcessHandlerType];
			}
			set
			{
				base[ProtocolElement._propProcessHandlerType] = value;
			}
		}

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x0600595A RID: 22874 RVA: 0x00137879 File Offset: 0x00135A79
		// (set) Token: 0x0600595B RID: 22875 RVA: 0x0013788B File Offset: 0x00135A8B
		[ConfigurationProperty("appDomainHandlerType")]
		public string AppDomainHandlerType
		{
			get
			{
				return (string)base[ProtocolElement._propAppDomainHandlerType];
			}
			set
			{
				base[ProtocolElement._propAppDomainHandlerType] = value;
			}
		}

		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x0600595C RID: 22876 RVA: 0x00137899 File Offset: 0x00135A99
		// (set) Token: 0x0600595D RID: 22877 RVA: 0x001378AB File Offset: 0x00135AAB
		[ConfigurationProperty("validate", DefaultValue = false)]
		public bool Validate
		{
			get
			{
				return (bool)base[ProtocolElement._propValidate];
			}
			set
			{
				base[ProtocolElement._propValidate] = value;
			}
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x001378C0 File Offset: 0x00135AC0
		private void ValidateTypes()
		{
			Type type;
			try
			{
				type = Type.GetType(this.ProcessHandlerType, true);
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(ex.Message, ex, base.ElementInformation.Properties["ProcessHandlerType"].Source, base.ElementInformation.Properties["ProcessHandlerType"].LineNumber);
			}
			ConfigUtil.CheckAssignableType(typeof(ProcessProtocolHandler), type, this, "ProcessHandlerType");
			Type type2;
			try
			{
				type2 = Type.GetType(this.AppDomainHandlerType, true);
			}
			catch (Exception ex2)
			{
				throw new ConfigurationErrorsException(ex2.Message, ex2, base.ElementInformation.Properties["AppDomainHandlerType"].Source, base.ElementInformation.Properties["AppDomainHandlerType"].LineNumber);
			}
			ConfigUtil.CheckAssignableType(typeof(AppDomainProtocolHandler), type2, this, "AppDomainHandlerType");
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x001379B8 File Offset: 0x00135BB8
		protected override void PostDeserialize()
		{
			if (this.Validate)
			{
				this.ValidateTypes();
			}
		}

		// Token: 0x04002F5D RID: 12125
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x04002F5E RID: 12126
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002F5F RID: 12127
		private static readonly ConfigurationProperty _propProcessHandlerType = new ConfigurationProperty("processHandlerType", typeof(string), null);

		// Token: 0x04002F60 RID: 12128
		private static readonly ConfigurationProperty _propAppDomainHandlerType = new ConfigurationProperty("appDomainHandlerType", typeof(string), null);

		// Token: 0x04002F61 RID: 12129
		private static readonly ConfigurationProperty _propValidate = new ConfigurationProperty("validate", typeof(bool), false);
	}
}
