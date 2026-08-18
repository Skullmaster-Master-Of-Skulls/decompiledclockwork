using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x020001CF RID: 463
	[ConfigurationCollection(typeof(SchemaImporterExtensionElement))]
	public sealed class SchemaImporterExtensionElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000676 RID: 1654
		public SchemaImporterExtensionElement this[int index]
		{
			get
			{
				return (SchemaImporterExtensionElement)base.BaseGet(index);
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

		// Token: 0x17000677 RID: 1655
		public SchemaImporterExtensionElement this[string name]
		{
			get
			{
				return (SchemaImporterExtensionElement)base.BaseGet(name);
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

		// Token: 0x06001F61 RID: 8033 RVA: 0x000AA42E File Offset: 0x000A862E
		public void Add(SchemaImporterExtensionElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x000AA437 File Offset: 0x000A8637
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x000AA43F File Offset: 0x000A863F
		protected override ConfigurationElement CreateNewElement()
		{
			return new SchemaImporterExtensionElement();
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000AA446 File Offset: 0x000A8646
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SchemaImporterExtensionElement)element).Key;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000AA453 File Offset: 0x000A8653
		public int IndexOf(SchemaImporterExtensionElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x000AA45C File Offset: 0x000A865C
		public void Remove(SchemaImporterExtensionElement element)
		{
			base.BaseRemove(element.Key);
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x000AA46A File Offset: 0x000A866A
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000AA473 File Offset: 0x000A8673
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
