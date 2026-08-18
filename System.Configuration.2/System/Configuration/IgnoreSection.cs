using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000062 RID: 98
	public sealed class IgnoreSection : ConfigurationSection
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x00013ED4 File Offset: 0x000120D4
		private static ConfigurationPropertyCollection EnsureStaticPropertyBag()
		{
			if (IgnoreSection.s_properties == null)
			{
				ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection();
				IgnoreSection.s_properties = configurationPropertyCollection;
			}
			return IgnoreSection.s_properties;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00013EFF File Offset: 0x000120FF
		public IgnoreSection()
		{
			IgnoreSection.EnsureStaticPropertyBag();
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00013F18 File Offset: 0x00012118
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IgnoreSection.EnsureStaticPropertyBag();
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00013F1F File Offset: 0x0001211F
		protected internal override bool IsModified()
		{
			return this._isModified;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013F27 File Offset: 0x00012127
		protected internal override void ResetModified()
		{
			this._isModified = false;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00013F30 File Offset: 0x00012130
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			this._rawXml = string.Empty;
			this._isModified = false;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00013F44 File Offset: 0x00012144
		protected internal override void DeserializeSection(XmlReader xmlReader)
		{
			if (!xmlReader.Read() || xmlReader.NodeType != XmlNodeType.Element)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_expected_to_find_element"), xmlReader);
			}
			this._rawXml = xmlReader.ReadOuterXml();
			this._isModified = true;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00013F7B File Offset: 0x0001217B
		protected internal override string SerializeSection(ConfigurationElement parentSection, string name, ConfigurationSaveMode saveMode)
		{
			return this._rawXml;
		}

		// Token: 0x04000282 RID: 642
		private static volatile ConfigurationPropertyCollection s_properties;

		// Token: 0x04000283 RID: 643
		private string _rawXml = string.Empty;

		// Token: 0x04000284 RID: 644
		private bool _isModified;
	}
}
