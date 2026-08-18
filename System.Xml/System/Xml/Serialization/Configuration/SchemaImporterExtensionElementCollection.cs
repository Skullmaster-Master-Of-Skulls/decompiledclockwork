using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x02000353 RID: 851
	[ConfigurationCollection(typeof(SchemaImporterExtensionElement))]
	public sealed class SchemaImporterExtensionElementCollection : ConfigurationElementCollection
	{
		// Token: 0x170009C1 RID: 2497
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

		// Token: 0x170009C2 RID: 2498
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

		// Token: 0x06002934 RID: 10548 RVA: 0x000D356F File Offset: 0x000D256F
		public void Add(SchemaImporterExtensionElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000D3578 File Offset: 0x000D2578
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000D3580 File Offset: 0x000D2580
		protected override ConfigurationElement CreateNewElement()
		{
			return new SchemaImporterExtensionElement();
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000D3587 File Offset: 0x000D2587
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SchemaImporterExtensionElement)element).Key;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000D3594 File Offset: 0x000D2594
		public int IndexOf(SchemaImporterExtensionElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000D359D File Offset: 0x000D259D
		public void Remove(SchemaImporterExtensionElement element)
		{
			base.BaseRemove(element.Key);
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000D35AB File Offset: 0x000D25AB
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000D35B4 File Offset: 0x000D25B4
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
