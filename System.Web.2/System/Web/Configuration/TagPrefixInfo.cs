using System;
using System.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x0200075F RID: 1887
	public sealed class TagPrefixInfo : ConfigurationElement
	{
		// Token: 0x06005B02 RID: 23298 RVA: 0x0013C2F8 File Offset: 0x0013A4F8
		static TagPrefixInfo()
		{
			TagPrefixInfo._properties = new ConfigurationPropertyCollection();
			TagPrefixInfo._properties.Add(TagPrefixInfo._propTagPrefix);
			TagPrefixInfo._properties.Add(TagPrefixInfo._propTagName);
			TagPrefixInfo._properties.Add(TagPrefixInfo._propNamespace);
			TagPrefixInfo._properties.Add(TagPrefixInfo._propAssembly);
			TagPrefixInfo._properties.Add(TagPrefixInfo._propSource);
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x00117E9E File Offset: 0x0011609E
		internal TagPrefixInfo()
		{
		}

		// Token: 0x06005B04 RID: 23300 RVA: 0x0013C429 File Offset: 0x0013A629
		public TagPrefixInfo(string tagPrefix, string nameSpace, string assembly, string tagName, string source) : this()
		{
			this.TagPrefix = tagPrefix;
			this.Namespace = nameSpace;
			this.Assembly = assembly;
			this.TagName = tagName;
			this.Source = source;
		}

		// Token: 0x06005B05 RID: 23301 RVA: 0x0013C458 File Offset: 0x0013A658
		public override bool Equals(object prefix)
		{
			TagPrefixInfo tagPrefixInfo = prefix as TagPrefixInfo;
			return StringUtil.Equals(this.TagPrefix, tagPrefixInfo.TagPrefix) && StringUtil.Equals(this.TagName, tagPrefixInfo.TagName) && StringUtil.Equals(this.Namespace, tagPrefixInfo.Namespace) && StringUtil.Equals(this.Assembly, tagPrefixInfo.Assembly) && StringUtil.Equals(this.Source, tagPrefixInfo.Source);
		}

		// Token: 0x06005B06 RID: 23302 RVA: 0x0013C4CB File Offset: 0x0013A6CB
		public override int GetHashCode()
		{
			return this.TagPrefix.GetHashCode() ^ this.TagName.GetHashCode() ^ this.Namespace.GetHashCode() ^ this.Assembly.GetHashCode() ^ this.Source.GetHashCode();
		}

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x06005B07 RID: 23303 RVA: 0x0013C508 File Offset: 0x0013A708
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagPrefixInfo._properties;
			}
		}

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x06005B08 RID: 23304 RVA: 0x0013C50F File Offset: 0x0013A70F
		// (set) Token: 0x06005B09 RID: 23305 RVA: 0x0013C521 File Offset: 0x0013A721
		[ConfigurationProperty("tagPrefix", IsRequired = true, DefaultValue = "/")]
		[StringValidator(MinLength = 1)]
		public string TagPrefix
		{
			get
			{
				return (string)base[TagPrefixInfo._propTagPrefix];
			}
			set
			{
				base[TagPrefixInfo._propTagPrefix] = value;
			}
		}

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x06005B0A RID: 23306 RVA: 0x0013C52F File Offset: 0x0013A72F
		// (set) Token: 0x06005B0B RID: 23307 RVA: 0x0013C541 File Offset: 0x0013A741
		[ConfigurationProperty("tagName")]
		public string TagName
		{
			get
			{
				return (string)base[TagPrefixInfo._propTagName];
			}
			set
			{
				base[TagPrefixInfo._propTagName] = value;
			}
		}

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x06005B0C RID: 23308 RVA: 0x0013C54F File Offset: 0x0013A74F
		// (set) Token: 0x06005B0D RID: 23309 RVA: 0x0013C561 File Offset: 0x0013A761
		[ConfigurationProperty("namespace")]
		public string Namespace
		{
			get
			{
				return (string)base[TagPrefixInfo._propNamespace];
			}
			set
			{
				base[TagPrefixInfo._propNamespace] = value;
			}
		}

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x06005B0E RID: 23310 RVA: 0x0013C56F File Offset: 0x0013A76F
		// (set) Token: 0x06005B0F RID: 23311 RVA: 0x0013C581 File Offset: 0x0013A781
		[ConfigurationProperty("assembly")]
		public string Assembly
		{
			get
			{
				return (string)base[TagPrefixInfo._propAssembly];
			}
			set
			{
				base[TagPrefixInfo._propAssembly] = value;
			}
		}

		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x06005B10 RID: 23312 RVA: 0x0013C58F File Offset: 0x0013A78F
		// (set) Token: 0x06005B11 RID: 23313 RVA: 0x0013C5A1 File Offset: 0x0013A7A1
		[ConfigurationProperty("src")]
		public string Source
		{
			get
			{
				return (string)base[TagPrefixInfo._propSource];
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					base[TagPrefixInfo._propSource] = value;
					return;
				}
				base[TagPrefixInfo._propSource] = null;
			}
		}

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x06005B12 RID: 23314 RVA: 0x0013C5C4 File Offset: 0x0013A7C4
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return TagPrefixInfo.s_elemProperty;
			}
		}

		// Token: 0x06005B13 RID: 23315 RVA: 0x0013C5CC File Offset: 0x0013A7CC
		private static void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("control");
			}
			TagPrefixInfo tagPrefixInfo = (TagPrefixInfo)value;
			if (Util.ContainsWhiteSpace(tagPrefixInfo.TagPrefix))
			{
				throw new ConfigurationErrorsException(SR.GetString("Space_attribute", new object[]
				{
					"tagPrefix"
				}));
			}
			bool flag = false;
			if (!string.IsNullOrEmpty(tagPrefixInfo.Namespace))
			{
				if (!string.IsNullOrEmpty(tagPrefixInfo.TagName) || !string.IsNullOrEmpty(tagPrefixInfo.Source))
				{
					flag = true;
				}
			}
			else if (!string.IsNullOrEmpty(tagPrefixInfo.TagName))
			{
				if (!string.IsNullOrEmpty(tagPrefixInfo.Namespace) || !string.IsNullOrEmpty(tagPrefixInfo.Assembly) || string.IsNullOrEmpty(tagPrefixInfo.Source))
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_tagprefix_entry"));
			}
		}

		// Token: 0x04003011 RID: 12305
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(TagPrefixInfo), new ValidatorCallback(TagPrefixInfo.Validate)));

		// Token: 0x04003012 RID: 12306
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04003013 RID: 12307
		private static readonly ConfigurationProperty _propTagPrefix = new ConfigurationProperty("tagPrefix", typeof(string), "/", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04003014 RID: 12308
		private static readonly ConfigurationProperty _propTagName = new ConfigurationProperty("tagName", typeof(string), string.Empty, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x04003015 RID: 12309
		private static readonly ConfigurationProperty _propNamespace = new ConfigurationProperty("namespace", typeof(string), string.Empty, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x04003016 RID: 12310
		private static readonly ConfigurationProperty _propAssembly = new ConfigurationProperty("assembly", typeof(string), string.Empty, null, null, ConfigurationPropertyOptions.IsAssemblyStringTransformationRequired);

		// Token: 0x04003017 RID: 12311
		private static readonly ConfigurationProperty _propSource = new ConfigurationProperty("src", typeof(string), string.Empty, null, null, ConfigurationPropertyOptions.None);
	}
}
