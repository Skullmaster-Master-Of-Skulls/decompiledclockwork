using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200066E RID: 1646
	[ConfigurationCollection(typeof(WebRequestModuleElement))]
	public sealed class WebRequestModuleElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000BF4 RID: 3060
		public WebRequestModuleElement this[int index]
		{
			get
			{
				return (WebRequestModuleElement)base.BaseGet(index);
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

		// Token: 0x17000BF5 RID: 3061
		public WebRequestModuleElement this[string name]
		{
			get
			{
				return (WebRequestModuleElement)base.BaseGet(name);
			}
			set
			{
				if (base.BaseGet(name) != null)
				{
					base.BaseRemove(name);
				}
				this.BaseAdd(value);
			}
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000D792B File Offset: 0x000D692B
		public void Add(WebRequestModuleElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000D7934 File Offset: 0x000D6934
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000D793C File Offset: 0x000D693C
		protected override ConfigurationElement CreateNewElement()
		{
			return new WebRequestModuleElement();
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000D7943 File Offset: 0x000D6943
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((WebRequestModuleElement)element).Key;
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000D795E File Offset: 0x000D695E
		public int IndexOf(WebRequestModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000D7967 File Offset: 0x000D6967
		public void Remove(WebRequestModuleElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000D7983 File Offset: 0x000D6983
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000D798C File Offset: 0x000D698C
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
