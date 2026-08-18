using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Configuration
{
	// Token: 0x0200006B RID: 107
	[DebuggerDisplay("LocationSectionRecord {ConfigKey}")]
	internal class LocationSectionRecord
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x000144BF File Offset: 0x000126BF
		internal LocationSectionRecord(SectionXmlInfo sectionXmlInfo, List<ConfigurationException> errors)
		{
			this._sectionXmlInfo = sectionXmlInfo;
			this._errors = errors;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x000144D5 File Offset: 0x000126D5
		internal string ConfigKey
		{
			get
			{
				return this._sectionXmlInfo.ConfigKey;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000144E2 File Offset: 0x000126E2
		internal SectionXmlInfo SectionXmlInfo
		{
			get
			{
				return this._sectionXmlInfo;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x000144EA File Offset: 0x000126EA
		internal ICollection<ConfigurationException> Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x000144EA File Offset: 0x000126EA
		internal List<ConfigurationException> ErrorsList
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x000144F2 File Offset: 0x000126F2
		internal bool HasErrors
		{
			get
			{
				return ErrorsHelper.GetHasErrors(this._errors);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000144FF File Offset: 0x000126FF
		internal void AddError(ConfigurationException e)
		{
			ErrorsHelper.AddError(ref this._errors, e);
		}

		// Token: 0x04000297 RID: 663
		private SectionXmlInfo _sectionXmlInfo;

		// Token: 0x04000298 RID: 664
		private List<ConfigurationException> _errors;
	}
}
