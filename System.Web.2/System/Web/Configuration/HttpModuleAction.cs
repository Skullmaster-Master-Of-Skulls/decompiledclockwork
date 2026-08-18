using System;
using System.Configuration;
using System.Web.Configuration.Common;
using System.Web.Security;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006FC RID: 1788
	public sealed class HttpModuleAction : ConfigurationElement
	{
		// Token: 0x0600564D RID: 22093 RVA: 0x0012E60C File Offset: 0x0012C80C
		static HttpModuleAction()
		{
			HttpModuleAction._properties = new ConfigurationPropertyCollection();
			HttpModuleAction._properties.Add(HttpModuleAction._propName);
			HttpModuleAction._properties.Add(HttpModuleAction._propType);
		}

		// Token: 0x0600564E RID: 22094 RVA: 0x00117E9E File Offset: 0x0011609E
		internal HttpModuleAction()
		{
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x0012E6A7 File Offset: 0x0012C8A7
		public HttpModuleAction(string name, string type) : this()
		{
			this.Name = name;
			this.Type = type;
			this._modualEntry = null;
		}

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x06005650 RID: 22096 RVA: 0x0012E6C4 File Offset: 0x0012C8C4
		internal string Key
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06005651 RID: 22097 RVA: 0x0012E6CC File Offset: 0x0012C8CC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpModuleAction._properties;
			}
		}

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06005652 RID: 22098 RVA: 0x0012E6D3 File Offset: 0x0012C8D3
		// (set) Token: 0x06005653 RID: 22099 RVA: 0x0012E6E5 File Offset: 0x0012C8E5
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[HttpModuleAction._propName];
			}
			set
			{
				base[HttpModuleAction._propName] = value;
			}
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06005654 RID: 22100 RVA: 0x0012E6F3 File Offset: 0x0012C8F3
		// (set) Token: 0x06005655 RID: 22101 RVA: 0x0012E705 File Offset: 0x0012C905
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		public string Type
		{
			get
			{
				return (string)base[HttpModuleAction._propType];
			}
			set
			{
				base[HttpModuleAction._propType] = value;
			}
		}

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06005656 RID: 22102 RVA: 0x0012E713 File Offset: 0x0012C913
		internal string FileName
		{
			get
			{
				return base.ElementInformation.Properties["name"].Source;
			}
		}

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x06005657 RID: 22103 RVA: 0x0012E72F File Offset: 0x0012C92F
		internal int LineNumber
		{
			get
			{
				return base.ElementInformation.Properties["name"].LineNumber;
			}
		}

		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x06005658 RID: 22104 RVA: 0x0012E74C File Offset: 0x0012C94C
		internal ModulesEntry Entry
		{
			get
			{
				ModulesEntry modualEntry;
				try
				{
					if (this._modualEntry == null)
					{
						this._modualEntry = new ModulesEntry(this.Name, this.Type, HttpModuleAction._propType.Name, this);
					}
					modualEntry = this._modualEntry;
				}
				catch (Exception ex)
				{
					throw new ConfigurationErrorsException(ex.Message, base.ElementInformation.Properties[HttpModuleAction._propType.Name].Source, base.ElementInformation.Properties[HttpModuleAction._propType.Name].LineNumber);
				}
				return modualEntry;
			}
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x0012E7E8 File Offset: 0x0012C9E8
		internal static bool IsSpecialModule(string className)
		{
			return ModulesEntry.IsTypeMatch(typeof(DefaultAuthenticationModule), className);
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x0012E7FA File Offset: 0x0012C9FA
		internal static bool IsSpecialModuleName(string name)
		{
			return StringUtil.EqualsIgnoreCase(name, "DefaultAuthentication");
		}

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x0600565B RID: 22107 RVA: 0x0012E807 File Offset: 0x0012CA07
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return HttpModuleAction.s_elemProperty;
			}
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x0012E810 File Offset: 0x0012CA10
		private static void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("httpModule");
			}
			HttpModuleAction httpModuleAction = (HttpModuleAction)value;
			if (HttpModuleAction.IsSpecialModule(httpModuleAction.Type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Special_module_cannot_be_added_manually", new object[]
				{
					httpModuleAction.Type
				}), httpModuleAction.ElementInformation.Properties["type"].Source, httpModuleAction.ElementInformation.Properties["type"].LineNumber);
			}
			if (HttpModuleAction.IsSpecialModuleName(httpModuleAction.Name))
			{
				throw new ConfigurationErrorsException(SR.GetString("Special_module_cannot_be_added_manually", new object[]
				{
					httpModuleAction.Name
				}), httpModuleAction.ElementInformation.Properties["name"].Source, httpModuleAction.ElementInformation.Properties["name"].LineNumber);
			}
		}

		// Token: 0x04002DD6 RID: 11734
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(HttpModuleAction), new ValidatorCallback(HttpModuleAction.Validate)));

		// Token: 0x04002DD7 RID: 11735
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002DD8 RID: 11736
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002DD9 RID: 11737
		private static readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04002DDA RID: 11738
		private ModulesEntry _modualEntry;
	}
}
