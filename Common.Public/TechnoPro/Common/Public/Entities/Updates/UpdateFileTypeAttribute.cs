using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000140 RID: 320
	public class UpdateFileTypeAttribute : Attribute
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00010868 File Offset: 0x0000EA68
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00010870 File Offset: 0x0000EA70
		public string Title { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00010879 File Offset: 0x0000EA79
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00010881 File Offset: 0x0000EA81
		public string Extension { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001088A File Offset: 0x0000EA8A
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00010892 File Offset: 0x0000EA92
		public string Description { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0001089B File Offset: 0x0000EA9B
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x000108A3 File Offset: 0x0000EAA3
		public bool AddSizeVersion { get; set; }

		// Token: 0x060007A4 RID: 1956 RVA: 0x000108AC File Offset: 0x0000EAAC
		public UpdateFileTypeAttribute(string title, string extension, bool addSizeVersion)
		{
			this.Title = title;
			this.Extension = extension;
			this.AddSizeVersion = addSizeVersion;
		}
	}
}
