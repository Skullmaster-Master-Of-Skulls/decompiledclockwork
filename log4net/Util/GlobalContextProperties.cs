using System;

namespace log4net.Util
{
	// Token: 0x020000FA RID: 250
	public sealed class GlobalContextProperties : ContextPropertiesBase
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x000162F7 File Offset: 0x000144F7
		internal GlobalContextProperties()
		{
		}

		// Token: 0x1700017D RID: 381
		public override object this[string key]
		{
			get
			{
				return this.m_readOnlyProperties[key];
			}
			set
			{
				lock (this.m_syncRoot)
				{
					PropertiesDictionary propertiesDictionary = new PropertiesDictionary(this.m_readOnlyProperties);
					propertiesDictionary[key] = value;
					this.m_readOnlyProperties = new ReadOnlyPropertiesDictionary(propertiesDictionary);
				}
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00016388 File Offset: 0x00014588
		public void Remove(string key)
		{
			lock (this.m_syncRoot)
			{
				if (this.m_readOnlyProperties.Contains(key))
				{
					PropertiesDictionary propertiesDictionary = new PropertiesDictionary(this.m_readOnlyProperties);
					propertiesDictionary.Remove(key);
					this.m_readOnlyProperties = new ReadOnlyPropertiesDictionary(propertiesDictionary);
				}
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000163F8 File Offset: 0x000145F8
		public void Clear()
		{
			lock (this.m_syncRoot)
			{
				this.m_readOnlyProperties = new ReadOnlyPropertiesDictionary();
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00016440 File Offset: 0x00014640
		internal ReadOnlyPropertiesDictionary GetReadOnlyProperties()
		{
			return this.m_readOnlyProperties;
		}

		// Token: 0x040002B0 RID: 688
		private volatile ReadOnlyPropertiesDictionary m_readOnlyProperties = new ReadOnlyPropertiesDictionary();

		// Token: 0x040002B1 RID: 689
		private readonly object m_syncRoot = new object();
	}
}
