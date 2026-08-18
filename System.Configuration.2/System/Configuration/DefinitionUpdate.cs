using System;

namespace System.Configuration
{
	// Token: 0x02000054 RID: 84
	internal class DefinitionUpdate : Update
	{
		// Token: 0x06000357 RID: 855 RVA: 0x00012AE3 File Offset: 0x00010CE3
		internal DefinitionUpdate(string configKey, bool moved, string updatedXml, SectionRecord sectionRecord) : base(configKey, moved, updatedXml)
		{
			this._sectionRecord = sectionRecord;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00012AF6 File Offset: 0x00010CF6
		internal SectionRecord SectionRecord
		{
			get
			{
				return this._sectionRecord;
			}
		}

		// Token: 0x04000254 RID: 596
		private SectionRecord _sectionRecord;
	}
}
