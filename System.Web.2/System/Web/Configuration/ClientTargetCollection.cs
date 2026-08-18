using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006BA RID: 1722
	[ConfigurationCollection(typeof(ClientTarget))]
	public sealed class ClientTargetCollection : ConfigurationElementCollection
	{
		// Token: 0x06005338 RID: 21304 RVA: 0x001240D1 File Offset: 0x001222D1
		public ClientTargetCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06005339 RID: 21305 RVA: 0x00124AE6 File Offset: 0x00122CE6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientTargetCollection._properties;
			}
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x0600533A RID: 21306 RVA: 0x00124AED File Offset: 0x00122CED
		public string[] AllKeys
		{
			get
			{
				return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys());
			}
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x00124AFA File Offset: 0x00122CFA
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(ClientTarget clientTarget)
		{
			this.BaseAdd(clientTarget);
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x00124B08 File Offset: 0x00122D08
		public void Remove(ClientTarget clientTarget)
		{
			base.BaseRemove(this.GetElementKey(clientTarget));
		}

		// Token: 0x0600533F RID: 21311 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x170017B3 RID: 6067
		public ClientTarget this[string name]
		{
			get
			{
				return (ClientTarget)base.BaseGet(name);
			}
		}

		// Token: 0x170017B4 RID: 6068
		public ClientTarget this[int index]
		{
			get
			{
				return (ClientTarget)base.BaseGet(index);
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

		// Token: 0x06005343 RID: 21315 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x00124B33 File Offset: 0x00122D33
		protected override ConfigurationElement CreateNewElement()
		{
			return new ClientTarget();
		}

		// Token: 0x06005345 RID: 21317 RVA: 0x00124B3A File Offset: 0x00122D3A
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ClientTarget)element).Alias;
		}

		// Token: 0x04002BAA RID: 11178
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
