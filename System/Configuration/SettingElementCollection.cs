using System;

namespace System.Configuration
{
	// Token: 0x02000723 RID: 1827
	public sealed class SettingElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x000EC434 File Offset: 0x000EB434
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060037D1 RID: 14289 RVA: 0x000EC437 File Offset: 0x000EB437
		protected override string ElementName
		{
			get
			{
				return "setting";
			}
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000EC43E File Offset: 0x000EB43E
		protected override ConfigurationElement CreateNewElement()
		{
			return new SettingElement();
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000EC445 File Offset: 0x000EB445
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SettingElement)element).Key;
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000EC452 File Offset: 0x000EB452
		public SettingElement Get(string elementKey)
		{
			return (SettingElement)base.BaseGet(elementKey);
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000EC460 File Offset: 0x000EB460
		public void Add(SettingElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000EC469 File Offset: 0x000EB469
		public void Remove(SettingElement element)
		{
			base.BaseRemove(this.GetElementKey(element));
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000EC478 File Offset: 0x000EB478
		public void Clear()
		{
			base.BaseClear();
		}
	}
}
