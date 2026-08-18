using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000642 RID: 1602
	[ConfigurationCollection(typeof(AuthenticationModuleElement))]
	public sealed class AuthenticationModuleElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000B58 RID: 2904
		public AuthenticationModuleElement this[int index]
		{
			get
			{
				return (AuthenticationModuleElement)base.BaseGet(index);
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

		// Token: 0x17000B59 RID: 2905
		public AuthenticationModuleElement this[string name]
		{
			get
			{
				return (AuthenticationModuleElement)base.BaseGet(name);
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

		// Token: 0x060031A4 RID: 12708 RVA: 0x000D47C4 File Offset: 0x000D37C4
		public void Add(AuthenticationModuleElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000D47CD File Offset: 0x000D37CD
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000D47D5 File Offset: 0x000D37D5
		protected override ConfigurationElement CreateNewElement()
		{
			return new AuthenticationModuleElement();
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000D47DC File Offset: 0x000D37DC
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((AuthenticationModuleElement)element).Key;
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000D47F7 File Offset: 0x000D37F7
		public int IndexOf(AuthenticationModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000D4800 File Offset: 0x000D3800
		public void Remove(AuthenticationModuleElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000D481C File Offset: 0x000D381C
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000D4825 File Offset: 0x000D3825
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
