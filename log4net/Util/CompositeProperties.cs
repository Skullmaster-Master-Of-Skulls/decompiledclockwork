using System;
using System.Collections;

namespace log4net.Util
{
	// Token: 0x020000F0 RID: 240
	public sealed class CompositeProperties
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x0001591D File Offset: 0x00013B1D
		internal CompositeProperties()
		{
		}

		// Token: 0x1700015E RID: 350
		public object this[string key]
		{
			get
			{
				if (this.m_flattened != null)
				{
					return this.m_flattened[key];
				}
				foreach (object obj in this.m_nestedProperties)
				{
					ReadOnlyPropertiesDictionary readOnlyPropertiesDictionary = (ReadOnlyPropertiesDictionary)obj;
					if (readOnlyPropertiesDictionary.Contains(key))
					{
						return readOnlyPropertiesDictionary[key];
					}
				}
				return null;
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000159B0 File Offset: 0x00013BB0
		public void Add(ReadOnlyPropertiesDictionary properties)
		{
			this.m_flattened = null;
			this.m_nestedProperties.Add(properties);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000159C8 File Offset: 0x00013BC8
		public PropertiesDictionary Flatten()
		{
			if (this.m_flattened == null)
			{
				this.m_flattened = new PropertiesDictionary();
				int num = this.m_nestedProperties.Count;
				while (--num >= 0)
				{
					ReadOnlyPropertiesDictionary readOnlyPropertiesDictionary = (ReadOnlyPropertiesDictionary)this.m_nestedProperties[num];
					foreach (object obj in ((IEnumerable)readOnlyPropertiesDictionary))
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						this.m_flattened[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
			return this.m_flattened;
		}

		// Token: 0x0400029D RID: 669
		private PropertiesDictionary m_flattened;

		// Token: 0x0400029E RID: 670
		private ArrayList m_nestedProperties = new ArrayList();
	}
}
