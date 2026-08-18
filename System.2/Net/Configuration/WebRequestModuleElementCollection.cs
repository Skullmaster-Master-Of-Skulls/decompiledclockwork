using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200034B RID: 843
	[ConfigurationCollection(typeof(WebRequestModuleElement))]
	public sealed class WebRequestModuleElementCollection : ConfigurationElementCollection
	{
		// Token: 0x170007D9 RID: 2009
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

		// Token: 0x170007DA RID: 2010
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

		// Token: 0x06001E48 RID: 7752 RVA: 0x0008DDD9 File Offset: 0x0008BFD9
		public void Add(WebRequestModuleElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0008DDE2 File Offset: 0x0008BFE2
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0008DDEA File Offset: 0x0008BFEA
		protected override ConfigurationElement CreateNewElement()
		{
			return new WebRequestModuleElement();
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0008DDF1 File Offset: 0x0008BFF1
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((WebRequestModuleElement)element).Key;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0008DE0C File Offset: 0x0008C00C
		public int IndexOf(WebRequestModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0008DE15 File Offset: 0x0008C015
		public void Remove(WebRequestModuleElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0008DE31 File Offset: 0x0008C031
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0008DE3A File Offset: 0x0008C03A
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
