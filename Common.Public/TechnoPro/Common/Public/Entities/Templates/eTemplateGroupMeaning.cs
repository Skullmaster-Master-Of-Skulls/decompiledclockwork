using System;

namespace TechnoPro.Common.Public.Entities.Templates
{
	// Token: 0x0200016E RID: 366
	[Serializable]
	public enum eTemplateGroupMeaning
	{
		// Token: 0x040006D7 RID: 1751
		[TemplateGroupMeaning("unknown", "Unknown", eTemplateType.DocumentTemplate)]
		Unknown,
		// Token: 0x040006D8 RID: 1752
		[TemplateGroupMeaning("reminderemails", "Automatic reminder emails", eTemplateType.EmailTemplate)]
		ReminderEmails,
		// Token: 0x040006D9 RID: 1753
		[TemplateGroupMeaning("alternateformatpublisher", "Alternate format publisher emails", eTemplateType.EmailTemplate)]
		AlternateFormatPublisherEmails,
		// Token: 0x040006DA RID: 1754
		[TemplateGroupMeaning("alternateformatvendor", "Alternate format vendor emails", eTemplateType.EmailTemplate)]
		AlternateFormatVendorEmails,
		// Token: 0x040006DB RID: 1755
		[TemplateGroupMeaning("alternateformatrequest", "Alternate format request emails", eTemplateType.EmailTemplate)]
		AlternateFormatRequestEmails,
		// Token: 0x040006DC RID: 1756
		[TemplateGroupMeaning("inventory", "Product templates", eTemplateType.DocumentTemplate)]
		InventoryProduct,
		// Token: 0x040006DD RID: 1757
		[TemplateGroupMeaning("inventoryloan", "Loan templates", eTemplateType.DocumentTemplate)]
		InventoryLoan,
		// Token: 0x040006DE RID: 1758
		[TemplateGroupMeaning("studentdocument", "'Generate document' button", eTemplateType.DocumentTemplate)]
		GenerateDocument,
		// Token: 0x040006DF RID: 1759
		[TemplateGroupMeaning("accommodationsrequest", "Accommodations request templates", eTemplateType.EmailTemplate)]
		AccommodationsRequest,
		// Token: 0x040006E0 RID: 1760
		[TemplateGroupMeaning("serviceproviders", "Service provider / request email templates", eTemplateType.EmailTemplate)]
		ServiceProviderEmails,
		// Token: 0x040006E1 RID: 1761
		[TemplateGroupMeaning("serviceproviderdocument", "Service provider / request document templates", eTemplateType.DocumentTemplate)]
		ServiceProviderDocuments,
		// Token: 0x040006E2 RID: 1762
		[TemplateGroupMeaning("appemails", "Appointment / Workshop emails", eTemplateType.EmailTemplate)]
		AppointmentEmails,
		// Token: 0x040006E3 RID: 1763
		[TemplateGroupMeaning("inventory", "'Email student' button", eTemplateType.EmailTemplate)]
		EmailStudent,
		// Token: 0x040006E4 RID: 1764
		[TemplateGroupMeaning("files", "Dynamic file list control templates", eTemplateType.DocumentTemplate)]
		DynamicFileList,
		// Token: 0x040006E5 RID: 1765
		[TemplateGroupMeaning("accommodations", "Accommodation letter templates", eTemplateType.DocumentTemplate)]
		Accommodations,
		// Token: 0x040006E6 RID: 1766
		[TemplateGroupMeaning("testexamcampus", "Test/Exam Campus Email templates", eTemplateType.EmailTemplate)]
		TestExamCampusEmails
	}
}
