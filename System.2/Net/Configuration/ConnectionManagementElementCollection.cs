using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200032C RID: 812
	[ConfigurationCollection(typeof(ConnectionManagementElement))]
	public sealed class ConnectionManagementElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000739 RID: 1849
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

		// Token: 0x1700073A RID: 1850
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

		// Token: 0x06001D28 RID: 7464 RVA: 0x0008AF7B File Offset: 0x0008917B
		public void Add(ConnectionManagementElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0008AF84 File Offset: 0x00089184
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0008AF8C File Offset: 0x0008918C
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionManagementElement();
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0008AF93 File Offset: 0x00089193
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((ConnectionManagementElement)element).Key;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0008AFAE File Offset: 0x000891AE
		public int IndexOf(ConnectionManagementElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0008AFB7 File Offset: 0x000891B7
		public void Remove(ConnectionManagementElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0008AFD3 File Offset: 0x000891D3
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0008AFDC File Offset: 0x000891DC
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
