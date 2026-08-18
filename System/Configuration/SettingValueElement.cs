using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000725 RID: 1829
	public sealed class SettingValueElement : ConfigurationElement
	{
		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060037E6 RID: 14310 RVA: 0x000EC60B File Offset: 0x000EB60B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (SettingValueElement._properties == null)
				{
					SettingValueElement._properties = new ConfigurationPropertyCollection();
				}
				return SettingValueElement._properties;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x000EC623 File Offset: 0x000EB623
		// (set) Token: 0x060037E8 RID: 14312 RVA: 0x000EC62B File Offset: 0x000EB62B
		public XmlNode ValueXml
		{
			get
			{
				return this._valueXml;
			}
			set
			{
				this._valueXml = value;
				this.isModified = true;
			}
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000EC63B File Offset: 0x000EB63B
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.ValueXml = SettingValueElement.doc.ReadNode(reader);
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000EC650 File Offset: 0x000EB650
		public override bool Equals(object settingValue)
		{
			SettingValueElement settingValueElement = settingValue as SettingValueElement;
			return settingValueElement != null && object.Equals(settingValueElement.ValueXml, this.ValueXml);
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000EC67A File Offset: 0x000EB67A
		public override int GetHashCode()
		{
			return this.ValueXml.GetHashCode();
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000EC687 File Offset: 0x000EB687
		protected override bool IsModified()
		{
			return this.isModified;
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000EC68F File Offset: 0x000EB68F
		protected override void ResetModified()
		{
			this.isModified = false;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000EC698 File Offset: 0x000EB698
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (this.ValueXml != null)
			{
				if (writer != null)
				{
					this.ValueXml.WriteTo(writer);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000EC6B4 File Offset: 0x000EB6B4
		protected override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			this.ValueXml = ((SettingValueElement)parentElement).ValueXml;
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000EC6CE File Offset: 0x000EB6CE
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			this.ValueXml = ((SettingValueElement)sourceElement).ValueXml;
		}

		// Token: 0x040031F8 RID: 12792
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x040031F9 RID: 12793
		private static XmlDocument doc = new XmlDocument();

		// Token: 0x040031FA RID: 12794
		private XmlNode _valueXml;

		// Token: 0x040031FB RID: 12795
		private bool isModified;
	}
}
