using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	// Token: 0x0200044B RID: 1099
	[ConfigurationCollection(typeof(ServiceNameElement))]
	public sealed class ServiceNameElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000A02 RID: 2562
		public ServiceNameElement this[int index]
		{
			get
			{
				return (ServiceNameElement)base.BaseGet(index);
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

		// Token: 0x17000A03 RID: 2563
		public ServiceNameElement this[string name]
		{
			get
			{
				return (ServiceNameElement)base.BaseGet(name);
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

		// Token: 0x060028B7 RID: 10423 RVA: 0x000BAD1A File Offset: 0x000B8F1A
		public void Add(ServiceNameElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000BAD23 File Offset: 0x000B8F23
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000BAD2B File Offset: 0x000B8F2B
		protected override ConfigurationElement CreateNewElement()
		{
			return new ServiceNameElement();
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000BAD32 File Offset: 0x000B8F32
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((ServiceNameElement)element).Key;
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000BAD4D File Offset: 0x000B8F4D
		public int IndexOf(ServiceNameElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000BAD56 File Offset: 0x000B8F56
		public void Remove(ServiceNameElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000BAD72 File Offset: 0x000B8F72
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000BAD7B File Offset: 0x000B8F7B
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
