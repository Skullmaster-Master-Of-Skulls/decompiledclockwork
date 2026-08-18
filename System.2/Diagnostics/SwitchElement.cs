using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x020004A9 RID: 1193
	internal class SwitchElement : ConfigurationElement
	{
		// Token: 0x06002C35 RID: 11317 RVA: 0x000C7834 File Offset: 0x000C5A34
		static SwitchElement()
		{
			SwitchElement._properties = new ConfigurationPropertyCollection();
			SwitchElement._properties.Add(SwitchElement._propName);
			SwitchElement._properties.Add(SwitchElement._propValue);
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x000C78A3 File Offset: 0x000C5AA3
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x000C78C3 File Offset: 0x000C5AC3
		[ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[SwitchElement._propName];
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x000C78D5 File Offset: 0x000C5AD5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SwitchElement._properties;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x000C78DC File Offset: 0x000C5ADC
		[ConfigurationProperty("value", IsRequired = true)]
		public string Value
		{
			get
			{
				return (string)base[SwitchElement._propValue];
			}
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000C78EE File Offset: 0x000C5AEE
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000C7900 File Offset: 0x000C5B00
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string localName = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(localName, text);
					}
				}
			}
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000C7954 File Offset: 0x000C5B54
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000C798C File Offset: 0x000C5B8C
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SwitchElement switchElement = sourceElement as SwitchElement;
			if (switchElement != null && switchElement._attributes != null)
			{
				this._attributes = switchElement._attributes;
			}
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000C79C0 File Offset: 0x000C5BC0
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				SwitchElement._properties.Clear();
				SwitchElement._properties.Add(SwitchElement._propName);
				SwitchElement._properties.Add(SwitchElement._propValue);
			}
		}

		// Token: 0x040026C2 RID: 9922
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x040026C3 RID: 9923
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040026C4 RID: 9924
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040026C5 RID: 9925
		private Hashtable _attributes;
	}
}
