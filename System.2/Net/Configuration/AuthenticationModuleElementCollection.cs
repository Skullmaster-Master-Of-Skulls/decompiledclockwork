using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000325 RID: 805
	[ConfigurationCollection(typeof(AuthenticationModuleElement))]
	public sealed class AuthenticationModuleElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000722 RID: 1826
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

		// Token: 0x17000723 RID: 1827
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

		// Token: 0x06001CED RID: 7405 RVA: 0x0008A8E6 File Offset: 0x00088AE6
		public void Add(AuthenticationModuleElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0008A8EF File Offset: 0x00088AEF
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0008A8F7 File Offset: 0x00088AF7
		protected override ConfigurationElement CreateNewElement()
		{
			return new AuthenticationModuleElement();
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0008A8FE File Offset: 0x00088AFE
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((AuthenticationModuleElement)element).Key;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0008A919 File Offset: 0x00088B19
		public int IndexOf(AuthenticationModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0008A922 File Offset: 0x00088B22
		public void Remove(AuthenticationModuleElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0008A93E File Offset: 0x00088B3E
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0008A947 File Offset: 0x00088B47
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
