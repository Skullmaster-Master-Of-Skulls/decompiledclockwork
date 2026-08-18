using System;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000038 RID: 56
	public abstract class ConfigurationSection : ConfigurationElement
	{
		// Token: 0x06000295 RID: 661 RVA: 0x000116EF File Offset: 0x0000F8EF
		protected ConfigurationSection()
		{
			this._section = new SectionInformation(this);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00011703 File Offset: 0x0000F903
		public SectionInformation SectionInformation
		{
			get
			{
				return this._section;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000101B8 File Offset: 0x0000E3B8
		protected internal virtual object GetRuntimeObject()
		{
			return this;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0001170B File Offset: 0x0000F90B
		protected internal override bool IsModified()
		{
			return this.SectionInformation.IsModifiedFlags() || base.IsModified();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00011722 File Offset: 0x0000F922
		protected internal override void ResetModified()
		{
			this.SectionInformation.ResetModifiedFlags();
			base.ResetModified();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00011735 File Offset: 0x0000F935
		protected internal virtual void DeserializeSection(XmlReader reader)
		{
			if (!reader.Read() || reader.NodeType != XmlNodeType.Element)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_expected_to_find_element"), reader);
			}
			this.DeserializeElement(reader, false);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00011764 File Offset: 0x0000F964
		protected internal virtual string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
		{
			if (base.CurrentConfiguration != null && base.CurrentConfiguration.TargetFramework != null && !this.ShouldSerializeSectionInTargetVersion(base.CurrentConfiguration.TargetFramework))
			{
				return string.Empty;
			}
			ConfigurationElement.ValidateElement(this, null, true);
			ConfigurationElement configurationElement = base.CreateElement(base.GetType());
			configurationElement.Unmerge(this, parentElement, saveMode);
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.Indentation = 4;
			xmlTextWriter.IndentChar = ' ';
			configurationElement.DataToWriteInternal = (saveMode != ConfigurationSaveMode.Minimal);
			if (base.CurrentConfiguration != null && base.CurrentConfiguration.TargetFramework != null)
			{
				this._configRecord.SectionsStack.Push(this);
			}
			configurationElement.SerializeToXmlElement(xmlTextWriter, name);
			if (base.CurrentConfiguration != null && base.CurrentConfiguration.TargetFramework != null)
			{
				this._configRecord.SectionsStack.Pop();
			}
			xmlTextWriter.Flush();
			return stringWriter.ToString();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000874E File Offset: 0x0000694E
		protected internal virtual bool ShouldSerializePropertyInTargetVersion(ConfigurationProperty property, string propertyName, FrameworkName targetFramework, ConfigurationElement parentConfigurationElement)
		{
			return true;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000874E File Offset: 0x0000694E
		protected internal virtual bool ShouldSerializeElementInTargetVersion(ConfigurationElement element, string elementName, FrameworkName targetFramework)
		{
			return true;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000874E File Offset: 0x0000694E
		protected internal virtual bool ShouldSerializeSectionInTargetVersion(FrameworkName targetFramework)
		{
			return true;
		}

		// Token: 0x04000208 RID: 520
		private SectionInformation _section;
	}
}
