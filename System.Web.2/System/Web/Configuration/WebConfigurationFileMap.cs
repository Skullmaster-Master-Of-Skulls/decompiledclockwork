using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000773 RID: 1907
	public sealed class WebConfigurationFileMap : ConfigurationFileMap
	{
		// Token: 0x06005BC3 RID: 23491 RVA: 0x0013DCDE File Offset: 0x0013BEDE
		public WebConfigurationFileMap()
		{
			this._site = string.Empty;
			this._virtualDirectoryMapping = new VirtualDirectoryMappingCollection();
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x0013DCFC File Offset: 0x0013BEFC
		private WebConfigurationFileMap(string machineConfigFileName, string site, VirtualDirectoryMappingCollection VirtualDirectoryMapping) : base(machineConfigFileName)
		{
			this._site = site;
			this._virtualDirectoryMapping = VirtualDirectoryMapping;
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x0013DD13 File Offset: 0x0013BF13
		public WebConfigurationFileMap(string machineConfigFileName) : base(machineConfigFileName)
		{
			this._site = string.Empty;
			this._virtualDirectoryMapping = new VirtualDirectoryMappingCollection();
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x0013DD34 File Offset: 0x0013BF34
		public override object Clone()
		{
			VirtualDirectoryMappingCollection virtualDirectoryMapping = this._virtualDirectoryMapping.Clone();
			return new WebConfigurationFileMap(base.MachineConfigFilename, this._site, virtualDirectoryMapping);
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x06005BC7 RID: 23495 RVA: 0x0013DD5F File Offset: 0x0013BF5F
		// (set) Token: 0x06005BC8 RID: 23496 RVA: 0x0013DD67 File Offset: 0x0013BF67
		internal string Site
		{
			get
			{
				return this._site;
			}
			set
			{
				if (!WebConfigurationHost.IsValidSiteArgument(value))
				{
					throw ExceptionUtil.PropertyInvalid("Site");
				}
				this._site = value;
			}
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x06005BC9 RID: 23497 RVA: 0x0013DD83 File Offset: 0x0013BF83
		public VirtualDirectoryMappingCollection VirtualDirectories
		{
			get
			{
				return this._virtualDirectoryMapping;
			}
		}

		// Token: 0x04003050 RID: 12368
		private string _site;

		// Token: 0x04003051 RID: 12369
		private VirtualDirectoryMappingCollection _virtualDirectoryMapping;
	}
}
