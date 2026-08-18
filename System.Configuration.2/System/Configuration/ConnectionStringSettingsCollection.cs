using System;

namespace System.Configuration
{
	// Token: 0x0200004B RID: 75
	[ConfigurationCollection(typeof(ConnectionStringSettings))]
	public sealed class ConnectionStringSettingsCollection : ConfigurationElementCollection
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0001287D File Offset: 0x00010A7D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionStringSettingsCollection._properties;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00012884 File Offset: 0x00010A84
		public ConnectionStringSettingsCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x170000E7 RID: 231
		public ConnectionStringSettings this[int index]
		{
			get
			{
				return (ConnectionStringSettings)base.BaseGet(index);
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

		// Token: 0x170000E8 RID: 232
		public ConnectionStringSettings this[string name]
		{
			get
			{
				return (ConnectionStringSettings)base.BaseGet(name);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000128C7 File Offset: 0x00010AC7
		public int IndexOf(ConnectionStringSettings settings)
		{
			return base.BaseIndexOf(settings);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000128D0 File Offset: 0x00010AD0
		protected override void BaseAdd(int index, ConfigurationElement element)
		{
			if (index == -1)
			{
				base.BaseAdd(element, false);
				return;
			}
			base.BaseAdd(index, element);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000128E7 File Offset: 0x00010AE7
		public void Add(ConnectionStringSettings settings)
		{
			this.BaseAdd(settings);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000128F0 File Offset: 0x00010AF0
		public void Remove(ConnectionStringSettings settings)
		{
			if (base.BaseIndexOf(settings) >= 0)
			{
				base.BaseRemove(settings.Key);
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00012908 File Offset: 0x00010B08
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00012911 File Offset: 0x00010B11
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001291A File Offset: 0x00010B1A
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionStringSettings();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00012921 File Offset: 0x00010B21
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ConnectionStringSettings)element).Key;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001292E File Offset: 0x00010B2E
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x04000240 RID: 576
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
