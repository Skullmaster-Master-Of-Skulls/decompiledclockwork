using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.Output
{
	// Token: 0x020002C9 RID: 713
	public class AllowedExtensionGroupAttribute : Attribute
	{
		// Token: 0x060015A5 RID: 5541 RVA: 0x0001B082 File Offset: 0x00019282
		public AllowedExtensionGroupAttribute()
		{
			this.AllowedExtensions = new string[0];
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0001B099 File Offset: 0x00019299
		public AllowedExtensionGroupAttribute(string mergedDocumentImplementationDll, string mergedDocumentImplementationClass, string clockWorkLicenseKey, params string[] allowedExtensions)
		{
			this.MergedDocumentImplementationDll = mergedDocumentImplementationDll;
			this.MergedDocumentImplementationClass = mergedDocumentImplementationClass;
			this.ClockWorkLicenseKey = clockWorkLicenseKey;
			this.AllowedExtensions = allowedExtensions;
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0001B0C4 File Offset: 0x000192C4
		// (set) Token: 0x060015A8 RID: 5544 RVA: 0x0001B0CC File Offset: 0x000192CC
		public string[] AllowedExtensions { get; set; }

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0001B0D5 File Offset: 0x000192D5
		// (set) Token: 0x060015AA RID: 5546 RVA: 0x0001B0DD File Offset: 0x000192DD
		public string MergedDocumentImplementationDll { get; set; }

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0001B0E6 File Offset: 0x000192E6
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x0001B0EE File Offset: 0x000192EE
		public string MergedDocumentImplementationClass { get; set; }

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0001B0F7 File Offset: 0x000192F7
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x0001B0FF File Offset: 0x000192FF
		public string ClockWorkLicenseKey { get; set; }
	}
}
