using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200073D RID: 1853
	[ConfigurationCollection(typeof(ProtocolElement))]
	public sealed class ProtocolCollection : ConfigurationElementCollection
	{
		// Token: 0x170019DE RID: 6622
		// (get) Token: 0x06005946 RID: 22854 RVA: 0x001376D7 File Offset: 0x001358D7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProtocolCollection._properties;
			}
		}

		// Token: 0x170019DF RID: 6623
		// (get) Token: 0x06005947 RID: 22855 RVA: 0x001376DE File Offset: 0x001358DE
		public string[] AllKeys
		{
			get
			{
				return (string[])base.BaseGetAllKeys();
			}
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(ProtocolElement protocolElement)
		{
			this.BaseAdd(protocolElement);
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x00124B08 File Offset: 0x00122D08
		public void Remove(ProtocolElement protocolElement)
		{
			base.BaseRemove(this.GetElementKey(protocolElement));
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x170019E0 RID: 6624
		public ProtocolElement this[string name]
		{
			get
			{
				return (ProtocolElement)base.BaseGet(name);
			}
		}

		// Token: 0x170019E1 RID: 6625
		public ProtocolElement this[int index]
		{
			get
			{
				return (ProtocolElement)base.BaseGet(index);
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

		// Token: 0x0600594F RID: 22863 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x00137707 File Offset: 0x00135907
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProtocolElement();
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x00137710 File Offset: 0x00135910
		protected override object GetElementKey(ConfigurationElement element)
		{
			string name = ((ProtocolElement)element).Name;
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(SR.GetString("Config_collection_add_element_without_key"));
			}
			return name;
		}

		// Token: 0x04002F5C RID: 12124
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
