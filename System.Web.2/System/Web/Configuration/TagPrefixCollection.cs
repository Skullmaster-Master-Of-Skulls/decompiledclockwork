using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200075E RID: 1886
	[ConfigurationCollection(typeof(TagPrefixInfo), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class TagPrefixCollection : ConfigurationElementCollection
	{
		// Token: 0x06005AF6 RID: 23286 RVA: 0x001240D1 File Offset: 0x001222D1
		public TagPrefixCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x06005AF7 RID: 23287 RVA: 0x0013C257 File Offset: 0x0013A457
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagPrefixCollection._properties;
			}
		}

		// Token: 0x17001AA1 RID: 6817
		public TagPrefixInfo this[int index]
		{
			get
			{
				return (TagPrefixInfo)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x06005AFA RID: 23290 RVA: 0x00007722 File Offset: 0x00005922
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x06005AFB RID: 23291 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005AFC RID: 23292 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(TagPrefixInfo tagPrefixInformation)
		{
			this.BaseAdd(tagPrefixInformation);
		}

		// Token: 0x06005AFD RID: 23293 RVA: 0x00124B08 File Offset: 0x00122D08
		public void Remove(TagPrefixInfo tagPrefixInformation)
		{
			base.BaseRemove(this.GetElementKey(tagPrefixInformation));
		}

		// Token: 0x06005AFE RID: 23294 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005AFF RID: 23295 RVA: 0x0013C26C File Offset: 0x0013A46C
		protected override ConfigurationElement CreateNewElement()
		{
			return new TagPrefixInfo();
		}

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x06005B00 RID: 23296 RVA: 0x00124DA4 File Offset: 0x00122FA4
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}

		// Token: 0x06005B01 RID: 23297 RVA: 0x0013C274 File Offset: 0x0013A474
		protected override object GetElementKey(ConfigurationElement element)
		{
			TagPrefixInfo tagPrefixInfo = (TagPrefixInfo)element;
			if (string.IsNullOrEmpty(tagPrefixInfo.TagName))
			{
				return string.Concat(new string[]
				{
					tagPrefixInfo.TagPrefix,
					":",
					tagPrefixInfo.Namespace,
					":",
					string.IsNullOrEmpty(tagPrefixInfo.Assembly) ? string.Empty : tagPrefixInfo.Assembly
				});
			}
			return tagPrefixInfo.TagPrefix + ":" + tagPrefixInfo.TagName;
		}

		// Token: 0x04003010 RID: 12304
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
