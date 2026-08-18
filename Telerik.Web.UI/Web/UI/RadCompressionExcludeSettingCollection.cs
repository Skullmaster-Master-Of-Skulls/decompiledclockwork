using System;
using System.Collections;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x02001833 RID: 6195
	[ConfigurationCollection(typeof(RadCompressionExcludeSetting))]
	public class RadCompressionExcludeSettingCollection : ConfigurationElementCollection
	{
		// Token: 0x0600F0D3 RID: 61651 RVA: 0x0036BB3C File Offset: 0x00369D3C
		public RadCompressionExcludeSettingCollection() : this(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x0600F0D4 RID: 61652 RVA: 0x0036BB49 File Offset: 0x00369D49
		public RadCompressionExcludeSettingCollection(IComparer comparer) : base(comparer)
		{
			RadCompressionExcludeSettingCollection._properties = new ConfigurationPropertyCollection();
		}

		// Token: 0x0600F0D5 RID: 61653 RVA: 0x0036BB5C File Offset: 0x00369D5C
		protected override ConfigurationElement CreateNewElement()
		{
			return new RadCompressionExcludeSetting();
		}

		// Token: 0x0600F0D6 RID: 61654 RVA: 0x0036BB63 File Offset: 0x00369D63
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((RadCompressionExcludeSetting)element).HandlerPath;
		}

		// Token: 0x0600F0D7 RID: 61655 RVA: 0x0036BB70 File Offset: 0x00369D70
		public void Add(RadCompressionExcludeSetting excludeSetting)
		{
			if (excludeSetting != null)
			{
				this.BaseAdd(excludeSetting);
			}
		}

		// Token: 0x0600F0D8 RID: 61656 RVA: 0x0036BB7C File Offset: 0x00369D7C
		public void Remove(RadCompressionExcludeSetting excludeSetting)
		{
			if (excludeSetting != null)
			{
				base.BaseRemove(excludeSetting);
			}
		}

		// Token: 0x0600F0D9 RID: 61657 RVA: 0x0036BB88 File Offset: 0x00369D88
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x170048CA RID: 18634
		public RadCompressionExcludeSetting this[int index]
		{
			get
			{
				return (RadCompressionExcludeSetting)base.BaseGet(index);
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

		// Token: 0x170048CB RID: 18635
		public RadCompressionExcludeSetting this[string key]
		{
			get
			{
				return (RadCompressionExcludeSetting)base.BaseGet(key);
			}
		}

		// Token: 0x170048CC RID: 18636
		// (get) Token: 0x0600F0DD RID: 61661 RVA: 0x0036BBC6 File Offset: 0x00369DC6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RadCompressionExcludeSettingCollection._properties;
			}
		}

		// Token: 0x04004557 RID: 17751
		private static ConfigurationPropertyCollection _properties;
	}
}
