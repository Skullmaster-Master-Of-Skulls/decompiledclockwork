using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000052 RID: 82
	public sealed class DefaultSection : ConfigurationSection
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00012A2C File Offset: 0x00010C2C
		private static ConfigurationPropertyCollection EnsureStaticPropertyBag()
		{
			if (DefaultSection.s_properties == null)
			{
				ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection();
				DefaultSection.s_properties = configurationPropertyCollection;
			}
			return DefaultSection.s_properties;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00012A57 File Offset: 0x00010C57
		public DefaultSection()
		{
			DefaultSection.EnsureStaticPropertyBag();
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00012A70 File Offset: 0x00010C70
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DefaultSection.EnsureStaticPropertyBag();
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00012A77 File Offset: 0x00010C77
		protected internal override bool IsModified()
		{
			return this._isModified;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00012A7F File Offset: 0x00010C7F
		protected internal override void ResetModified()
		{
			this._isModified = false;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00012A88 File Offset: 0x00010C88
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			this._rawXml = string.Empty;
			this._isModified = false;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00012A9C File Offset: 0x00010C9C
		protected internal override void DeserializeSection(XmlReader xmlReader)
		{
			if (!xmlReader.Read() || xmlReader.NodeType != XmlNodeType.Element)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_expected_to_find_element"), xmlReader);
			}
			this._rawXml = xmlReader.ReadOuterXml();
			this._isModified = true;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00012AD3 File Offset: 0x00010CD3
		protected internal override string SerializeSection(ConfigurationElement parentSection, string name, ConfigurationSaveMode saveMode)
		{
			return this._rawXml;
		}

		// Token: 0x04000251 RID: 593
		private static volatile ConfigurationPropertyCollection s_properties;

		// Token: 0x04000252 RID: 594
		private string _rawXml = string.Empty;

		// Token: 0x04000253 RID: 595
		private bool _isModified;
	}
}
