using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000763 RID: 1891
	public sealed class TransformerInfo : ConfigurationElement
	{
		// Token: 0x06005B25 RID: 23333 RVA: 0x0013C908 File Offset: 0x0013AB08
		static TransformerInfo()
		{
			TransformerInfo._properties = new ConfigurationPropertyCollection();
			TransformerInfo._properties.Add(TransformerInfo._propName);
			TransformerInfo._properties.Add(TransformerInfo._propType);
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x00117E9E File Offset: 0x0011609E
		internal TransformerInfo()
		{
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x0013C980 File Offset: 0x0013AB80
		public TransformerInfo(string name, string type) : this()
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x06005B28 RID: 23336 RVA: 0x0013C996 File Offset: 0x0013AB96
		// (set) Token: 0x06005B29 RID: 23337 RVA: 0x0013C9A8 File Offset: 0x0013ABA8
		[ConfigurationProperty("name", IsRequired = true, DefaultValue = "", IsKey = true)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[TransformerInfo._propName];
			}
			set
			{
				base[TransformerInfo._propName] = value;
			}
		}

		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x06005B2A RID: 23338 RVA: 0x0013C9B6 File Offset: 0x0013ABB6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TransformerInfo._properties;
			}
		}

		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x06005B2B RID: 23339 RVA: 0x0013C9BD File Offset: 0x0013ABBD
		// (set) Token: 0x06005B2C RID: 23340 RVA: 0x0013C9CF File Offset: 0x0013ABCF
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Type
		{
			get
			{
				return (string)base[TransformerInfo._propType];
			}
			set
			{
				base[TransformerInfo._propType] = value;
			}
		}

		// Token: 0x06005B2D RID: 23341 RVA: 0x0013C9E0 File Offset: 0x0013ABE0
		public override bool Equals(object o)
		{
			if (o == this)
			{
				return true;
			}
			TransformerInfo transformerInfo = o as TransformerInfo;
			return StringUtil.Equals(this.Name, transformerInfo.Name) && StringUtil.Equals(this.Type, transformerInfo.Type);
		}

		// Token: 0x06005B2E RID: 23342 RVA: 0x0013CA20 File Offset: 0x0013AC20
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.Type.GetHashCode();
		}

		// Token: 0x04003026 RID: 12326
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04003027 RID: 12327
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04003028 RID: 12328
		private static readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);
	}
}
