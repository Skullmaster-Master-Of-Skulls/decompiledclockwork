using System;
using System.ComponentModel;

namespace Telerik.Licensing
{
	// Token: 0x0200041C RID: 1052
	internal class LicenseContextData : ILicenseContextData
	{
		// Token: 0x060025F1 RID: 9713 RVA: 0x0007CFF2 File Offset: 0x0007B1F2
		public LicenseContextData(LicenseContext context, Type type, bool allowExceptions)
		{
			this.Context = context;
			this.Type = type;
			this.AllowExceptions = allowExceptions;
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x0007D00F File Offset: 0x0007B20F
		// (set) Token: 0x060025F3 RID: 9715 RVA: 0x0007D017 File Offset: 0x0007B217
		public LicenseContext Context { get; set; }

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x0007D020 File Offset: 0x0007B220
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x0007D028 File Offset: 0x0007B228
		public Type Type { get; set; }

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x0007D031 File Offset: 0x0007B231
		// (set) Token: 0x060025F7 RID: 9719 RVA: 0x0007D039 File Offset: 0x0007B239
		public bool AllowExceptions { get; set; }
	}
}
