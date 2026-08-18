using System;
using System.Collections;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200075C RID: 1884
	[ConfigurationCollection(typeof(TagMapInfo))]
	public sealed class TagMapCollection : ConfigurationElementCollection
	{
		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x06005AE0 RID: 23264 RVA: 0x0013BF22 File Offset: 0x0013A122
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagMapCollection._properties;
			}
		}

		// Token: 0x17001A9B RID: 6811
		public TagMapInfo this[int index]
		{
			get
			{
				return (TagMapInfo)base.BaseGet(index);
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

		// Token: 0x06005AE3 RID: 23267 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(TagMapInfo tagMapInformation)
		{
			this.BaseAdd(tagMapInformation);
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x00124B08 File Offset: 0x00122D08
		public void Remove(TagMapInfo tagMapInformation)
		{
			base.BaseRemove(this.GetElementKey(tagMapInformation));
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x0013BF37 File Offset: 0x0013A137
		protected override ConfigurationElement CreateNewElement()
		{
			return new TagMapInfo();
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x0013BF3E File Offset: 0x0013A13E
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TagMapInfo)element).TagType;
		}

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x06005AE8 RID: 23272 RVA: 0x0013BF4C File Offset: 0x0013A14C
		internal Hashtable TagTypeMappingInternal
		{
			get
			{
				if (this._tagMappings == null)
				{
					lock (this)
					{
						if (this._tagMappings == null)
						{
							Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
							foreach (object obj in this)
							{
								TagMapInfo tagMapInfo = (TagMapInfo)obj;
								Type type = ConfigUtil.GetType(tagMapInfo.TagType, "tagType", tagMapInfo);
								Type type2 = ConfigUtil.GetType(tagMapInfo.MappedTagType, "mappedTagType", tagMapInfo);
								if (!type.IsAssignableFrom(type2))
								{
									throw new ConfigurationErrorsException(SR.GetString("Mapped_type_must_inherit", new object[]
									{
										tagMapInfo.MappedTagType,
										tagMapInfo.TagType
									}), tagMapInfo.ElementInformation.Properties["mappedTagType"].Source, tagMapInfo.ElementInformation.Properties["mappedTagType"].LineNumber);
								}
								hashtable[type] = type2;
							}
							this._tagMappings = hashtable;
						}
					}
				}
				return this._tagMappings;
			}
		}

		// Token: 0x0400300B RID: 12299
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x0400300C RID: 12300
		private Hashtable _tagMappings;
	}
}
