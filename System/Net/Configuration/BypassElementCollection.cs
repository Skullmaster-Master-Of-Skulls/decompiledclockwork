using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000646 RID: 1606
	[ConfigurationCollection(typeof(BypassElement))]
	public sealed class BypassElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000B61 RID: 2913
		public BypassElement this[int index]
		{
			get
			{
				return (BypassElement)base.BaseGet(index);
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

		// Token: 0x17000B62 RID: 2914
		public BypassElement this[string name]
		{
			get
			{
				return (BypassElement)base.BaseGet(name);
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

		// Token: 0x060031C0 RID: 12736 RVA: 0x000D4C40 File Offset: 0x000D3C40
		public void Add(BypassElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000D4C49 File Offset: 0x000D3C49
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000D4C51 File Offset: 0x000D3C51
		protected override ConfigurationElement CreateNewElement()
		{
			return new BypassElement();
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000D4C58 File Offset: 0x000D3C58
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((BypassElement)element).Key;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000D4C73 File Offset: 0x000D3C73
		public int IndexOf(BypassElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000D4C7C File Offset: 0x000D3C7C
		public void Remove(BypassElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000D4C98 File Offset: 0x000D3C98
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000D4CA1 File Offset: 0x000D3CA1
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000D4CAA File Offset: 0x000D3CAA
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}
	}
}
