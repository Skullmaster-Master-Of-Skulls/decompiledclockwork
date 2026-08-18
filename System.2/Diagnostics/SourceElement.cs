using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x020004A2 RID: 1186
	internal class SourceElement : ConfigurationElement
	{
		// Token: 0x06002BFA RID: 11258 RVA: 0x000C6D50 File Offset: 0x000C4F50
		static SourceElement()
		{
			SourceElement._properties = new ConfigurationPropertyCollection();
			SourceElement._properties.Add(SourceElement._propName);
			SourceElement._properties.Add(SourceElement._propSwitchName);
			SourceElement._properties.Add(SourceElement._propSwitchValue);
			SourceElement._properties.Add(SourceElement._propSwitchType);
			SourceElement._properties.Add(SourceElement._propListeners);
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000C6E41 File Offset: 0x000C5041
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

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x000C6E61 File Offset: 0x000C5061
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[SourceElement._propListeners];
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000C6E73 File Offset: 0x000C5073
		[ConfigurationProperty("name", IsRequired = true, DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base[SourceElement._propName];
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000C6E85 File Offset: 0x000C5085
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SourceElement._properties;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000C6E8C File Offset: 0x000C508C
		[ConfigurationProperty("switchName")]
		public string SwitchName
		{
			get
			{
				return (string)base[SourceElement._propSwitchName];
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000C6E9E File Offset: 0x000C509E
		[ConfigurationProperty("switchValue")]
		public string SwitchValue
		{
			get
			{
				return (string)base[SourceElement._propSwitchValue];
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000C6EB0 File Offset: 0x000C50B0
		[ConfigurationProperty("switchType")]
		public string SwitchType
		{
			get
			{
				return (string)base[SourceElement._propSwitchType];
			}
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000C6EC4 File Offset: 0x000C50C4
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
			if (!string.IsNullOrEmpty(this.SwitchName) && !string.IsNullOrEmpty(this.SwitchValue))
			{
				throw new ConfigurationErrorsException(SR.GetString("Only_specify_one", new object[]
				{
					this.Name
				}));
			}
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000C6F12 File Offset: 0x000C5112
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000C6F24 File Offset: 0x000C5124
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

		// Token: 0x06002C05 RID: 11269 RVA: 0x000C6F78 File Offset: 0x000C5178
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000C6FB0 File Offset: 0x000C51B0
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SourceElement sourceElement2 = sourceElement as SourceElement;
			if (sourceElement2 != null && sourceElement2._attributes != null)
			{
				this._attributes = sourceElement2._attributes;
			}
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000C6FE4 File Offset: 0x000C51E4
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				SourceElement._properties.Clear();
				SourceElement._properties.Add(SourceElement._propName);
				SourceElement._properties.Add(SourceElement._propSwitchName);
				SourceElement._properties.Add(SourceElement._propSwitchValue);
				SourceElement._properties.Add(SourceElement._propSwitchType);
				SourceElement._properties.Add(SourceElement._propListeners);
			}
		}

		// Token: 0x040026A3 RID: 9891
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x040026A4 RID: 9892
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040026A5 RID: 9893
		private static readonly ConfigurationProperty _propSwitchName = new ConfigurationProperty("switchName", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040026A6 RID: 9894
		private static readonly ConfigurationProperty _propSwitchValue = new ConfigurationProperty("switchValue", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040026A7 RID: 9895
		private static readonly ConfigurationProperty _propSwitchType = new ConfigurationProperty("switchType", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040026A8 RID: 9896
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026A9 RID: 9897
		private Hashtable _attributes;
	}
}
