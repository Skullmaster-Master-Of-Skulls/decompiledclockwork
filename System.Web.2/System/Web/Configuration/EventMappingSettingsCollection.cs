using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006D6 RID: 1750
	[ConfigurationCollection(typeof(EventMappingSettings))]
	public sealed class EventMappingSettingsCollection : ConfigurationElementCollection
	{
		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x06005429 RID: 21545 RVA: 0x00127284 File Offset: 0x00125484
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return EventMappingSettingsCollection._properties;
			}
		}

		// Token: 0x17001803 RID: 6147
		public EventMappingSettings this[string key]
		{
			get
			{
				return (EventMappingSettings)base.BaseGet(key);
			}
		}

		// Token: 0x17001804 RID: 6148
		public EventMappingSettings this[int index]
		{
			get
			{
				return (EventMappingSettings)base.BaseGet(index);
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

		// Token: 0x0600542E RID: 21550 RVA: 0x001272A7 File Offset: 0x001254A7
		protected override ConfigurationElement CreateNewElement()
		{
			return new EventMappingSettings();
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x001272AE File Offset: 0x001254AE
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((EventMappingSettings)element).Name;
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(EventMappingSettings eventMappingSettings)
		{
			this.BaseAdd(eventMappingSettings);
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x00118E82 File Offset: 0x00117082
		public void Insert(int index, EventMappingSettings eventMappingSettings)
		{
			this.BaseAdd(index, eventMappingSettings);
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x001272BC File Offset: 0x001254BC
		public int IndexOf(string name)
		{
			ConfigurationElement configurationElement = base.BaseGet(name);
			if (configurationElement == null)
			{
				return -1;
			}
			return base.BaseIndexOf(configurationElement);
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x001272DD File Offset: 0x001254DD
		public bool Contains(string name)
		{
			return this.IndexOf(name) != -1;
		}

		// Token: 0x04002C44 RID: 11332
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
