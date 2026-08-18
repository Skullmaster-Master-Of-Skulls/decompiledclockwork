using System;

namespace log4net.Util
{
	// Token: 0x02000118 RID: 280
	public sealed class ThreadContextProperties : ContextPropertiesBase
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x00019979 File Offset: 0x00017B79
		internal ThreadContextProperties()
		{
		}

		// Token: 0x170001C2 RID: 450
		public override object this[string key]
		{
			get
			{
				if (ThreadContextProperties._dictionary != null)
				{
					return ThreadContextProperties._dictionary[key];
				}
				return null;
			}
			set
			{
				this.GetProperties(true)[key] = value;
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000199A7 File Offset: 0x00017BA7
		public void Remove(string key)
		{
			if (ThreadContextProperties._dictionary != null)
			{
				ThreadContextProperties._dictionary.Remove(key);
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000199BB File Offset: 0x00017BBB
		public string[] GetKeys()
		{
			if (ThreadContextProperties._dictionary != null)
			{
				return ThreadContextProperties._dictionary.GetKeys();
			}
			return null;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000199D0 File Offset: 0x00017BD0
		public void Clear()
		{
			if (ThreadContextProperties._dictionary != null)
			{
				ThreadContextProperties._dictionary.Clear();
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000199E3 File Offset: 0x00017BE3
		internal PropertiesDictionary GetProperties(bool create)
		{
			if (ThreadContextProperties._dictionary == null && create)
			{
				ThreadContextProperties._dictionary = new PropertiesDictionary();
			}
			return ThreadContextProperties._dictionary;
		}

		// Token: 0x040002FE RID: 766
		[ThreadStatic]
		private static PropertiesDictionary _dictionary;
	}
}
