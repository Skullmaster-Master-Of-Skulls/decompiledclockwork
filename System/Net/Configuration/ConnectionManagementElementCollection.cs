using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200064A RID: 1610
	[ConfigurationCollection(typeof(ConnectionManagementElement))]
	public sealed class ConnectionManagementElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000B70 RID: 2928
		public ConnectionManagementElement this[int index]
		{
			get
			{
				return (ConnectionManagementElement)base.BaseGet(index);
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

		// Token: 0x17000B71 RID: 2929
		public ConnectionManagementElement this[string name]
		{
			get
			{
				return (ConnectionManagementElement)base.BaseGet(name);
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

		// Token: 0x060031E2 RID: 12770 RVA: 0x000D4F13 File Offset: 0x000D3F13
		public void Add(ConnectionManagementElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000D4F1C File Offset: 0x000D3F1C
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000D4F24 File Offset: 0x000D3F24
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionManagementElement();
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000D4F2B File Offset: 0x000D3F2B
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((ConnectionManagementElement)element).Key;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000D4F46 File Offset: 0x000D3F46
		public int IndexOf(ConnectionManagementElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000D4F4F File Offset: 0x000D3F4F
		public void Remove(ConnectionManagementElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000D4F6B File Offset: 0x000D3F6B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000D4F74 File Offset: 0x000D3F74
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
