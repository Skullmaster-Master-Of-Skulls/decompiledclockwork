using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200070A RID: 1802
	[ConfigurationCollection(typeof(IgnoreDeviceFilterElement), AddItemName = "filter", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class IgnoreDeviceFilterElementCollection : ConfigurationElementCollection
	{
		// Token: 0x060056F4 RID: 22260 RVA: 0x001240D1 File Offset: 0x001222D1
		public IgnoreDeviceFilterElementCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x060056F5 RID: 22261 RVA: 0x001301E3 File Offset: 0x0012E3E3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IgnoreDeviceFilterElementCollection._properties;
			}
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(IgnoreDeviceFilterElement deviceFilter)
		{
			this.BaseAdd(deviceFilter);
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x00124B08 File Offset: 0x00122D08
		public void Remove(IgnoreDeviceFilterElement deviceFilter)
		{
			base.BaseRemove(this.GetElementKey(deviceFilter));
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x17001925 RID: 6437
		public IgnoreDeviceFilterElement this[string name]
		{
			get
			{
				return (IgnoreDeviceFilterElement)base.BaseGet(name);
			}
		}

		// Token: 0x17001926 RID: 6438
		public IgnoreDeviceFilterElement this[int index]
		{
			get
			{
				return (IgnoreDeviceFilterElement)base.BaseGet(index);
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

		// Token: 0x060056FD RID: 22269 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x00130206 File Offset: 0x0012E406
		protected override ConfigurationElement CreateNewElement()
		{
			return new IgnoreDeviceFilterElement();
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x0013020D File Offset: 0x0012E40D
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((IgnoreDeviceFilterElement)element).Name;
		}

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x06005700 RID: 22272 RVA: 0x0013021A File Offset: 0x0012E41A
		protected override string ElementName
		{
			get
			{
				return "filter";
			}
		}

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06005701 RID: 22273 RVA: 0x00007722 File Offset: 0x00005922
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x04002E34 RID: 11828
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
