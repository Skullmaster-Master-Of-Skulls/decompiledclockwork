using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006FA RID: 1786
	[ConfigurationCollection(typeof(HttpHandlerAction), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMapAlternate)]
	public sealed class HttpHandlerActionCollection : ConfigurationElementCollection
	{
		// Token: 0x06005639 RID: 22073 RVA: 0x001240D1 File Offset: 0x001222D1
		public HttpHandlerActionCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x0600563A RID: 22074 RVA: 0x0012E484 File Offset: 0x0012C684
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpHandlerActionCollection._properties;
			}
		}

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x0600563B RID: 22075 RVA: 0x0012E48B File Offset: 0x0012C68B
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMapAlternate;
			}
		}

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x0600563C RID: 22076 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170018E9 RID: 6377
		public HttpHandlerAction this[int index]
		{
			get
			{
				return (HttpHandlerAction)base.BaseGet(index);
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

		// Token: 0x0600563F RID: 22079 RVA: 0x0012E49C File Offset: 0x0012C69C
		public int IndexOf(HttpHandlerAction action)
		{
			return base.BaseIndexOf(action);
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x00126C26 File Offset: 0x00124E26
		public void Add(HttpHandlerAction httpHandlerAction)
		{
			base.BaseAdd(httpHandlerAction, false);
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x0012E4A5 File Offset: 0x0012C6A5
		public void Remove(HttpHandlerAction action)
		{
			base.BaseRemove(action.Key);
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x0012E4B3 File Offset: 0x0012C6B3
		public void Remove(string verb, string path)
		{
			base.BaseRemove("verb=" + verb + " | path=" + path);
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x0012E4CC File Offset: 0x0012C6CC
		protected override ConfigurationElement CreateNewElement()
		{
			return new HttpHandlerAction();
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x0012E4D3 File Offset: 0x0012C6D3
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((HttpHandlerAction)element).Key;
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x04002DD2 RID: 11730
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
