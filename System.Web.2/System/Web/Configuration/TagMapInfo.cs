using System;
using System.Configuration;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x0200075D RID: 1885
	public sealed class TagMapInfo : ConfigurationElement
	{
		// Token: 0x06005AE9 RID: 23273 RVA: 0x0013C0AC File Offset: 0x0013A2AC
		static TagMapInfo()
		{
			TagMapInfo._properties = new ConfigurationPropertyCollection();
			TagMapInfo._properties.Add(TagMapInfo._propTagTypeName);
			TagMapInfo._properties.Add(TagMapInfo._propMappedTagTypeName);
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x00117E9E File Offset: 0x0011609E
		internal TagMapInfo()
		{
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x0013C123 File Offset: 0x0013A323
		public TagMapInfo(string tagTypeName, string mappedTagTypeName) : this()
		{
			this.TagType = tagTypeName;
			this.MappedTagType = mappedTagTypeName;
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x0013C13C File Offset: 0x0013A33C
		public override bool Equals(object o)
		{
			TagMapInfo tagMapInfo = o as TagMapInfo;
			return StringUtil.Equals(this.TagType, tagMapInfo.TagType) && StringUtil.Equals(this.MappedTagType, tagMapInfo.MappedTagType);
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x0013C176 File Offset: 0x0013A376
		public override int GetHashCode()
		{
			return this.TagType.GetHashCode() ^ this.MappedTagType.GetHashCode();
		}

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x0013C18F File Offset: 0x0013A38F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagMapInfo._properties;
			}
		}

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x06005AEF RID: 23279 RVA: 0x0013C196 File Offset: 0x0013A396
		// (set) Token: 0x06005AF0 RID: 23280 RVA: 0x0013C1A8 File Offset: 0x0013A3A8
		[ConfigurationProperty("mappedTagType")]
		[StringValidator(MinLength = 1)]
		public string MappedTagType
		{
			get
			{
				return (string)base[TagMapInfo._propMappedTagTypeName];
			}
			set
			{
				base[TagMapInfo._propMappedTagTypeName] = value;
			}
		}

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x06005AF1 RID: 23281 RVA: 0x0013C1B6 File Offset: 0x0013A3B6
		// (set) Token: 0x06005AF2 RID: 23282 RVA: 0x0013C1C8 File Offset: 0x0013A3C8
		[ConfigurationProperty("tagType", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string TagType
		{
			get
			{
				return (string)base[TagMapInfo._propTagTypeName];
			}
			set
			{
				base[TagMapInfo._propTagTypeName] = value;
			}
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x0013C1D8 File Offset: 0x0013A3D8
		private void Verify()
		{
			if (string.IsNullOrEmpty(this.TagType))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_missing", new object[]
				{
					"tagType"
				}));
			}
			if (string.IsNullOrEmpty(this.MappedTagType))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_missing", new object[]
				{
					"mappedTagType"
				}));
			}
		}

		// Token: 0x06005AF4 RID: 23284 RVA: 0x0013C23B File Offset: 0x0013A43B
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			this.Verify();
			return base.SerializeElement(writer, serializeCollectionKey);
		}

		// Token: 0x0400300D RID: 12301
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x0400300E RID: 12302
		private static readonly ConfigurationProperty _propTagTypeName = new ConfigurationProperty("tagType", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400300F RID: 12303
		private static readonly ConfigurationProperty _propMappedTagTypeName = new ConfigurationProperty("mappedTagType", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
	}
}
