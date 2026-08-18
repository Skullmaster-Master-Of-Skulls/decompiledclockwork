using System;

namespace System.Configuration
{
	// Token: 0x02000068 RID: 104
	[ConfigurationCollection(typeof(KeyValueConfigurationElement))]
	public class KeyValueConfigurationCollection : ConfigurationElementCollection
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0001422A File Offset: 0x0001242A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return KeyValueConfigurationCollection._properties;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014231 File Offset: 0x00012431
		public KeyValueConfigurationCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
			this.internalAddToEnd = true;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00008751 File Offset: 0x00006951
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000122 RID: 290
		public KeyValueConfigurationElement this[string key]
		{
			get
			{
				return (KeyValueConfigurationElement)base.BaseGet(key);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00014253 File Offset: 0x00012453
		public string[] AllKeys
		{
			get
			{
				return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys());
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00014260 File Offset: 0x00012460
		public void Add(KeyValueConfigurationElement keyValue)
		{
			keyValue.Init();
			KeyValueConfigurationElement keyValueConfigurationElement = (KeyValueConfigurationElement)base.BaseGet(keyValue.Key);
			if (keyValueConfigurationElement == null)
			{
				this.BaseAdd(keyValue);
				return;
			}
			KeyValueConfigurationElement keyValueConfigurationElement2 = keyValueConfigurationElement;
			keyValueConfigurationElement2.Value = keyValueConfigurationElement2.Value + "," + keyValue.Value;
			int index = base.BaseIndexOf(keyValueConfigurationElement);
			base.BaseRemoveAt(index);
			this.BaseAdd(index, keyValueConfigurationElement);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000142C4 File Offset: 0x000124C4
		public void Add(string key, string value)
		{
			KeyValueConfigurationElement keyValue = new KeyValueConfigurationElement(key, value);
			this.Add(keyValue);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00012911 File Offset: 0x00010B11
		public void Remove(string key)
		{
			base.BaseRemove(key);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001292E File Offset: 0x00010B2E
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000142E0 File Offset: 0x000124E0
		protected override ConfigurationElement CreateNewElement()
		{
			return new KeyValueConfigurationElement();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000142E7 File Offset: 0x000124E7
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((KeyValueConfigurationElement)element).Key;
		}

		// Token: 0x0400028F RID: 655
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
