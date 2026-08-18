using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006AC RID: 1708
	[ConfigurationCollection(typeof(BufferModeSettings))]
	public sealed class BufferModesCollection : ConfigurationElementCollection
	{
		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x060052CC RID: 21196 RVA: 0x00123A21 File Offset: 0x00121C21
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BufferModesCollection._properties;
			}
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(BufferModeSettings bufferModeSettings)
		{
			this.BaseAdd(bufferModeSettings);
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string s)
		{
			base.BaseRemove(s);
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x00123A28 File Offset: 0x00121C28
		protected override ConfigurationElement CreateNewElement()
		{
			return new BufferModeSettings();
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x00123A2F File Offset: 0x00121C2F
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((BufferModeSettings)element).Name;
		}

		// Token: 0x1700178E RID: 6030
		public BufferModeSettings this[string key]
		{
			get
			{
				return (BufferModeSettings)base.BaseGet(key);
			}
		}

		// Token: 0x1700178F RID: 6031
		public BufferModeSettings this[int index]
		{
			get
			{
				return (BufferModeSettings)base.BaseGet(index);
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

		// Token: 0x04002B6F RID: 11119
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
