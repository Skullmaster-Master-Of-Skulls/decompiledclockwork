using System;
using System.Configuration;
using System.Configuration.Internal;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x0200074E RID: 1870
	internal class RuntimeConfigLKG : RuntimeConfig
	{
		// Token: 0x06005A36 RID: 23094 RVA: 0x0013A77C File Offset: 0x0013897C
		internal RuntimeConfigLKG(IInternalConfigRecord configRecord) : base(configRecord, true)
		{
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x0013A788 File Offset: 0x00138988
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		protected override object GetSectionObject(string sectionName)
		{
			if (this._configRecord != null)
			{
				return this._configRecord.GetLkgSection(sectionName);
			}
			object result;
			try
			{
				result = ConfigurationManager.GetSection(sectionName);
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
