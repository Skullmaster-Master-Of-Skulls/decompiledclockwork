using System;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x0200010A RID: 266
	internal class SpecialColumnTypeAttribute : Attribute
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0004872A File Offset: 0x0004692A
		public SpecialColumnTypeAttribute()
		{
			this.AllowedExternalColumnNames = new string[0];
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00048741 File Offset: 0x00046941
		public SpecialColumnTypeAttribute(params string[] allowedExternalColumnNames)
		{
			this.AllowedExternalColumnNames = allowedExternalColumnNames;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00048753 File Offset: 0x00046953
		public SpecialColumnTypeAttribute(bool isStudentSpecificData, bool isRequired, params string[] allowedExternalColumnNames)
		{
			this.AllowedExternalColumnNames = allowedExternalColumnNames;
			this.IsStudentSpecificData = isStudentSpecificData;
			this.IsRequired = isRequired;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00048775 File Offset: 0x00046975
		public SpecialColumnTypeAttribute(bool isRequired, params string[] allowedExternalColumnNames)
		{
			this.AllowedExternalColumnNames = allowedExternalColumnNames;
			this.IsRequired = isRequired;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0004878F File Offset: 0x0004698F
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00048797 File Offset: 0x00046997
		public string[] AllowedExternalColumnNames { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x000487A0 File Offset: 0x000469A0
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x000487A8 File Offset: 0x000469A8
		public bool IsRequired { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x000487B1 File Offset: 0x000469B1
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x000487B9 File Offset: 0x000469B9
		public bool IsStudentSpecificData { get; set; }
	}
}
