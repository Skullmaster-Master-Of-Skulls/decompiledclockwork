using System;
using System.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200069B RID: 1691
	public sealed class WindowsServiceElement : ConfigurationElement
	{
		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x000F8568 File Offset: 0x000F6768
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("includeWindowsGroups", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("allowAnonymousLogons", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x000F85DE File Offset: 0x000F67DE
		// (set) Token: 0x06004178 RID: 16760 RVA: 0x000F85F0 File Offset: 0x000F67F0
		[ConfigurationProperty("includeWindowsGroups", DefaultValue = true)]
		public bool IncludeWindowsGroups
		{
			get
			{
				return (bool)base["includeWindowsGroups"];
			}
			set
			{
				base["includeWindowsGroups"] = value;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x000F8603 File Offset: 0x000F6803
		// (set) Token: 0x0600417A RID: 16762 RVA: 0x000F8615 File Offset: 0x000F6815
		[ConfigurationProperty("allowAnonymousLogons", DefaultValue = false)]
		public bool AllowAnonymousLogons
		{
			get
			{
				return (bool)base["allowAnonymousLogons"];
			}
			set
			{
				base["allowAnonymousLogons"] = value;
			}
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x000F8628 File Offset: 0x000F6828
		public void Copy(WindowsServiceElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.AllowAnonymousLogons = from.AllowAnonymousLogons;
			this.IncludeWindowsGroups = from.IncludeWindowsGroups;
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000F8682 File Offset: 0x000F6882
		internal void ApplyConfiguration(WindowsServiceCredential windows)
		{
			if (windows == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("windows");
			}
			windows.AllowAnonymousLogons = this.AllowAnonymousLogons;
			windows.IncludeWindowsGroups = this.IncludeWindowsGroups;
		}

		// Token: 0x04002CE9 RID: 11497
		private ConfigurationPropertyCollection properties;
	}
}
