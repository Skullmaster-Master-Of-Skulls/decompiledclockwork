using System;

namespace System.Configuration
{
	// Token: 0x02000071 RID: 113
	[ConfigurationCollection(typeof(NameValueConfigurationElement))]
	public sealed class NameValueConfigurationCollection : ConfigurationElementCollection
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00018A59 File Offset: 0x00016C59
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NameValueConfigurationCollection._properties;
			}
		}

		// Token: 0x17000143 RID: 323
		public NameValueConfigurationElement this[string name]
		{
			get
			{
				return (NameValueConfigurationElement)base.BaseGet(name);
			}
			set
			{
				int index = -1;
				NameValueConfigurationElement nameValueConfigurationElement = (NameValueConfigurationElement)base.BaseGet(name);
				if (nameValueConfigurationElement != null)
				{
					index = base.BaseIndexOf(nameValueConfigurationElement);
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00014253 File Offset: 0x00012453
		public string[] AllKeys
		{
			get
			{
				return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys());
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000128E7 File Offset: 0x00010AE7
		public void Add(NameValueConfigurationElement nameValue)
		{
			this.BaseAdd(nameValue);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00018AAE File Offset: 0x00016CAE
		public void Remove(NameValueConfigurationElement nameValue)
		{
			if (base.BaseIndexOf(nameValue) >= 0)
			{
				base.BaseRemove(nameValue.Name);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00012911 File Offset: 0x00010B11
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001292E File Offset: 0x00010B2E
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00018AC6 File Offset: 0x00016CC6
		protected override ConfigurationElement CreateNewElement()
		{
			return new NameValueConfigurationElement();
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00018ACD File Offset: 0x00016CCD
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((NameValueConfigurationElement)element).Name;
		}

		// Token: 0x040002B1 RID: 689
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
