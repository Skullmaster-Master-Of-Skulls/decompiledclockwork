using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000766 RID: 1894
	[ConfigurationCollection(typeof(TrustLevel), AddItemName = "trustLevel", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class TrustLevelCollection : ConfigurationElementCollection
	{
		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x06005B47 RID: 23367 RVA: 0x0013CF9C File Offset: 0x0013B19C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TrustLevelCollection._properties;
			}
		}

		// Token: 0x17001ABF RID: 6847
		public TrustLevel this[int index]
		{
			get
			{
				return (TrustLevel)base.BaseGet(index);
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

		// Token: 0x17001AC0 RID: 6848
		public TrustLevel this[string key]
		{
			get
			{
				return (TrustLevel)base.BaseGet(key);
			}
		}

		// Token: 0x06005B4B RID: 23371 RVA: 0x0013CFBF File Offset: 0x0013B1BF
		protected override ConfigurationElement CreateNewElement()
		{
			return new TrustLevel();
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x0013CFC6 File Offset: 0x0013B1C6
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TrustLevel)element).Name;
		}

		// Token: 0x17001AC1 RID: 6849
		// (get) Token: 0x06005B4D RID: 23373 RVA: 0x0013CFD3 File Offset: 0x0013B1D3
		protected override string ElementName
		{
			get
			{
				return "trustLevel";
			}
		}

		// Token: 0x17001AC2 RID: 6850
		// (get) Token: 0x06005B4E RID: 23374 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x00007722 File Offset: 0x00005922
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x0013CFDC File Offset: 0x0013B1DC
		protected override bool IsElementName(string elementname)
		{
			bool result = false;
			if (elementname == "trustLevel")
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(TrustLevel trustLevel)
		{
			this.BaseAdd(trustLevel);
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x0013CFA3 File Offset: 0x0013B1A3
		public TrustLevel Get(int index)
		{
			return (TrustLevel)base.BaseGet(index);
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x00124B08 File Offset: 0x00122D08
		public void Remove(TrustLevel trustLevel)
		{
			base.BaseRemove(this.GetElementKey(trustLevel));
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x00118E82 File Offset: 0x00117082
		public void Set(int index, TrustLevel trustLevel)
		{
			this.BaseAdd(index, trustLevel);
		}

		// Token: 0x04003030 RID: 12336
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
