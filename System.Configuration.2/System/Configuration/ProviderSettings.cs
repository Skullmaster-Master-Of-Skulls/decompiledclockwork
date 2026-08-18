using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000080 RID: 128
	public sealed class ProviderSettings : ConfigurationElement
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x000198F4 File Offset: 0x00017AF4
		public ProviderSettings()
		{
			this._properties = new ConfigurationPropertyCollection();
			this._properties.Add(this._propName);
			this._properties.Add(this._propType);
			this._PropertyNameCollection = null;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001997E File Offset: 0x00017B7E
		public ProviderSettings(string name, string type) : this()
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00019994 File Offset: 0x00017B94
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				this.UpdatePropertyCollection();
				return this._properties;
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000199A4 File Offset: 0x00017BA4
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			ProviderSettings providerSettings = parentElement as ProviderSettings;
			if (providerSettings != null)
			{
				providerSettings.UpdatePropertyCollection();
			}
			ProviderSettings providerSettings2 = sourceElement as ProviderSettings;
			if (providerSettings2 != null)
			{
				providerSettings2.UpdatePropertyCollection();
			}
			base.Unmerge(sourceElement, parentElement, saveMode);
			this.UpdatePropertyCollection();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000199E4 File Offset: 0x00017BE4
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			ProviderSettings providerSettings = parentElement as ProviderSettings;
			if (providerSettings != null)
			{
				providerSettings.UpdatePropertyCollection();
			}
			base.Reset(parentElement);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00019A0C File Offset: 0x00017C0C
		internal bool UpdatePropertyCollection()
		{
			bool result = false;
			ArrayList arrayList = null;
			if (this._PropertyNameCollection != null)
			{
				foreach (object obj in this._properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					if (configurationProperty.Name != "name" && configurationProperty.Name != "type" && this._PropertyNameCollection.Get(configurationProperty.Name) == null)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						if ((base.Values.GetConfigValue(configurationProperty.Name).ValueFlags & ConfigurationValueFlags.Locked) == ConfigurationValueFlags.Default)
						{
							arrayList.Add(configurationProperty.Name);
							result = true;
						}
					}
				}
				if (arrayList != null)
				{
					foreach (object obj2 in arrayList)
					{
						string name = (string)obj2;
						this._properties.Remove(name);
					}
				}
				foreach (object obj3 in this._PropertyNameCollection)
				{
					string text = (string)obj3;
					string text2 = this._PropertyNameCollection[text];
					string property = this.GetProperty(text);
					if (property == null || text2 != property)
					{
						this.SetProperty(text, text2);
						result = true;
					}
				}
			}
			this._PropertyNameCollection = null;
			return result;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00019BB4 File Offset: 0x00017DB4
		protected internal override bool IsModified()
		{
			return this.UpdatePropertyCollection() || base.IsModified();
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00019BC6 File Offset: 0x00017DC6
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00019BD9 File Offset: 0x00017DD9
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[this._propName];
			}
			set
			{
				base[this._propName] = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00019BE8 File Offset: 0x00017DE8
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00019BFB File Offset: 0x00017DFB
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get
			{
				return (string)base[this._propType];
			}
			set
			{
				base[this._propType] = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00019C0C File Offset: 0x00017E0C
		public NameValueCollection Parameters
		{
			get
			{
				if (this._PropertyNameCollection == null)
				{
					lock (this)
					{
						if (this._PropertyNameCollection == null)
						{
							this._PropertyNameCollection = new NameValueCollection(StringComparer.Ordinal);
							foreach (object obj in this._properties)
							{
								ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
								if (configurationProperty.Name != "name" && configurationProperty.Name != "type")
								{
									this._PropertyNameCollection.Add(configurationProperty.Name, (string)base[configurationProperty]);
								}
							}
						}
					}
				}
				return this._PropertyNameCollection;
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00019CFC File Offset: 0x00017EFC
		private string GetProperty(string PropName)
		{
			if (this._properties.Contains(PropName))
			{
				ConfigurationProperty configurationProperty = this._properties[PropName];
				if (configurationProperty != null)
				{
					return (string)base[configurationProperty];
				}
			}
			return null;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00019D38 File Offset: 0x00017F38
		private bool SetProperty(string PropName, string value)
		{
			ConfigurationProperty configurationProperty;
			if (this._properties.Contains(PropName))
			{
				configurationProperty = this._properties[PropName];
			}
			else
			{
				configurationProperty = new ConfigurationProperty(PropName, typeof(string), null);
				this._properties.Add(configurationProperty);
			}
			if (configurationProperty != null)
			{
				base[configurationProperty] = value;
				return true;
			}
			return false;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00019D90 File Offset: 0x00017F90
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			ConfigurationProperty configurationProperty = new ConfigurationProperty(name, typeof(string), value);
			this._properties.Add(configurationProperty);
			base[configurationProperty] = value;
			this.Parameters[name] = value;
			return true;
		}

		// Token: 0x040002D7 RID: 727
		private readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, ConfigurationProperty.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040002D8 RID: 728
		private readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x040002D9 RID: 729
		private ConfigurationPropertyCollection _properties;

		// Token: 0x040002DA RID: 730
		private NameValueCollection _PropertyNameCollection;
	}
}
