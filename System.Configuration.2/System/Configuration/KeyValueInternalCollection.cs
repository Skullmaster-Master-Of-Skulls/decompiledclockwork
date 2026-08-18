using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x0200006A RID: 106
	internal class KeyValueInternalCollection : NameValueCollection
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x000143F4 File Offset: 0x000125F4
		public KeyValueInternalCollection(AppSettingsSection root)
		{
			this._root = root;
			foreach (object obj in this._root.Settings)
			{
				KeyValueConfigurationElement keyValueConfigurationElement = (KeyValueConfigurationElement)obj;
				base.Add(keyValueConfigurationElement.Key, keyValueConfigurationElement.Value);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001446C File Offset: 0x0001266C
		public override void Add(string key, string value)
		{
			this._root.Settings.Add(new KeyValueConfigurationElement(key, value));
			base.Add(key, value);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001448D File Offset: 0x0001268D
		public override void Clear()
		{
			this._root.Settings.Clear();
			base.Clear();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000144A5 File Offset: 0x000126A5
		public override void Remove(string key)
		{
			this._root.Settings.Remove(key);
			base.Remove(key);
		}

		// Token: 0x04000296 RID: 662
		private AppSettingsSection _root;
	}
}
