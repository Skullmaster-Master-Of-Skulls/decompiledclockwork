using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x0200076A RID: 1898
	[ConfigurationCollection(typeof(UrlMapping))]
	public sealed class UrlMappingCollection : ConfigurationElementCollection
	{
		// Token: 0x06005B79 RID: 23417 RVA: 0x001240D1 File Offset: 0x001222D1
		public UrlMappingCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x06005B7A RID: 23418 RVA: 0x0013D3B9 File Offset: 0x0013B5B9
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UrlMappingCollection._properties;
			}
		}

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x06005B7B RID: 23419 RVA: 0x00124AED File Offset: 0x00122CED
		public string[] AllKeys
		{
			get
			{
				return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys());
			}
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x00124AFA File Offset: 0x00122CFA
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(UrlMapping urlMapping)
		{
			this.BaseAdd(urlMapping);
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x00124B08 File Offset: 0x00122D08
		public void Remove(UrlMapping urlMapping)
		{
			base.BaseRemove(this.GetElementKey(urlMapping));
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x17001AD8 RID: 6872
		public UrlMapping this[string name]
		{
			get
			{
				return (UrlMapping)base.BaseGet(name);
			}
		}

		// Token: 0x17001AD9 RID: 6873
		public UrlMapping this[int index]
		{
			get
			{
				return (UrlMapping)base.BaseGet(index);
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

		// Token: 0x06005B84 RID: 23428 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x0013D3DC File Offset: 0x0013B5DC
		protected override ConfigurationElement CreateNewElement()
		{
			return new UrlMapping();
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x0013D3E3 File Offset: 0x0013B5E3
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((UrlMapping)element).Url;
		}

		// Token: 0x0400303C RID: 12348
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
