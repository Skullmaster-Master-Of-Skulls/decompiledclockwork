using System;
using System.Runtime.Versioning;

namespace System.Configuration
{
	// Token: 0x0200003A RID: 58
	public class ConfigurationSectionGroup
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x00011B2C File Offset: 0x0000FD2C
		internal void AttachToConfigurationRecord(MgmtConfigurationRecord configRecord, FactoryRecord factoryRecord)
		{
			this._configRecord = configRecord;
			this._configKey = factoryRecord.ConfigKey;
			this._group = factoryRecord.Group;
			this._name = factoryRecord.Name;
			this._typeName = factoryRecord.FactoryTypeName;
			if (this._typeName != null)
			{
				FactoryRecord factoryRecord2 = null;
				if (!configRecord.Parent.IsRootConfig)
				{
					factoryRecord2 = configRecord.Parent.FindFactoryRecord(factoryRecord.ConfigKey, true);
				}
				this._declarationRequired = (factoryRecord2 == null || factoryRecord2.FactoryTypeName == null);
				this._declared = (configRecord.GetFactoryRecord(factoryRecord.ConfigKey, true) != null);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00011BC5 File Offset: 0x0000FDC5
		internal void RootAttachToConfigurationRecord(MgmtConfigurationRecord configRecord)
		{
			this._configRecord = configRecord;
			this._isRoot = true;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00011BD5 File Offset: 0x0000FDD5
		internal void DetachFromConfigurationRecord()
		{
			if (this._configSections != null)
			{
				this._configSections.DetachFromConfigurationRecord();
			}
			if (this._configSectionGroups != null)
			{
				this._configSectionGroups.DetachFromConfigurationRecord();
			}
			this._configRecord = null;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00011C04 File Offset: 0x0000FE04
		internal bool Attached
		{
			get
			{
				return this._configRecord != null;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00011C10 File Offset: 0x0000FE10
		private FactoryRecord FindParentFactoryRecord(bool permitErrors)
		{
			FactoryRecord result = null;
			if (this._configRecord != null && !this._configRecord.Parent.IsRootConfig)
			{
				result = this._configRecord.Parent.FindFactoryRecord(this._configKey, permitErrors);
			}
			return result;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00011C52 File Offset: 0x0000FE52
		private void VerifyIsAttachedToConfigRecord()
		{
			if (this._configRecord == null)
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsectiongroup_when_not_attached"));
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00011C6C File Offset: 0x0000FE6C
		public bool IsDeclared
		{
			get
			{
				return this._declared;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00011C74 File Offset: 0x0000FE74
		public bool IsDeclarationRequired
		{
			get
			{
				return this._declarationRequired;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00011C7C File Offset: 0x0000FE7C
		public void ForceDeclaration()
		{
			this.ForceDeclaration(true);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00011C88 File Offset: 0x0000FE88
		public void ForceDeclaration(bool force)
		{
			if (this._isRoot)
			{
				throw new InvalidOperationException(SR.GetString("Config_root_section_group_cannot_be_edited"));
			}
			if (this._configRecord != null && this._configRecord.IsLocationConfig)
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsectiongroup_in_location_config"));
			}
			if (force || !this._declarationRequired)
			{
				this._declared = force;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		public string SectionGroupName
		{
			get
			{
				return this._configKey;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00011CEC File Offset: 0x0000FEEC
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00011CFC File Offset: 0x0000FEFC
		public string Type
		{
			get
			{
				return this._typeName;
			}
			set
			{
				if (this._isRoot)
				{
					throw new InvalidOperationException(SR.GetString("Config_root_section_group_cannot_be_edited"));
				}
				string text = value;
				if (string.IsNullOrEmpty(text))
				{
					text = null;
				}
				if (this._configRecord != null)
				{
					if (this._configRecord.IsLocationConfig)
					{
						throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsectiongroup_in_location_config"));
					}
					if (text != null)
					{
						FactoryRecord factoryRecord = this.FindParentFactoryRecord(false);
						if (factoryRecord != null && !factoryRecord.IsEquivalentType(this._configRecord.Host, text))
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
							{
								this._configKey
							}));
						}
					}
				}
				this._typeName = text;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00011D9A File Offset: 0x0000FF9A
		public ConfigurationSectionCollection Sections
		{
			get
			{
				if (this._configSections == null)
				{
					this.VerifyIsAttachedToConfigRecord();
					this._configSections = new ConfigurationSectionCollection(this._configRecord, this);
				}
				return this._configSections;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00011DC2 File Offset: 0x0000FFC2
		public ConfigurationSectionGroupCollection SectionGroups
		{
			get
			{
				if (this._configSectionGroups == null)
				{
					this.VerifyIsAttachedToConfigRecord();
					this._configSectionGroups = new ConfigurationSectionGroupCollection(this._configRecord, this);
				}
				return this._configSectionGroups;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00011DEA File Offset: 0x0000FFEA
		internal bool IsRoot
		{
			get
			{
				return this._isRoot;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000874E File Offset: 0x0000694E
		protected internal virtual bool ShouldSerializeSectionGroupInTargetVersion(FrameworkName targetFramework)
		{
			return true;
		}

		// Token: 0x0400020B RID: 523
		private string _configKey = string.Empty;

		// Token: 0x0400020C RID: 524
		private string _group = string.Empty;

		// Token: 0x0400020D RID: 525
		private string _name = string.Empty;

		// Token: 0x0400020E RID: 526
		private ConfigurationSectionCollection _configSections;

		// Token: 0x0400020F RID: 527
		private ConfigurationSectionGroupCollection _configSectionGroups;

		// Token: 0x04000210 RID: 528
		private MgmtConfigurationRecord _configRecord;

		// Token: 0x04000211 RID: 529
		private string _typeName;

		// Token: 0x04000212 RID: 530
		private bool _declared;

		// Token: 0x04000213 RID: 531
		private bool _declarationRequired;

		// Token: 0x04000214 RID: 532
		private bool _isRoot;
	}
}
