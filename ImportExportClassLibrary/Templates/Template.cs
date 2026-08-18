using System;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ImportExportClassLibrary.Templates
{
	// Token: 0x0200002F RID: 47
	public class Template
	{
		// Token: 0x06000177 RID: 375 RVA: 0x0000C838 File Offset: 0x0000B838
		public Template(NameObjectPairCollection args, string templateFilename, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			this.args = args;
			this.da = da;
			this.tripleDES = tripleDES;
			this.templateFilename = templateFilename;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000C85D File Offset: 0x0000B85D
		public string MailMerge()
		{
			return "";
		}

		// Token: 0x040000C4 RID: 196
		private int personId;

		// Token: 0x040000C5 RID: 197
		private int notetakerId;

		// Token: 0x040000C6 RID: 198
		private int luCourseId;

		// Token: 0x040000C7 RID: 199
		private int courseId;

		// Token: 0x040000C8 RID: 200
		private int appointmentId;

		// Token: 0x040000C9 RID: 201
		private UnivDataAdapter da;

		// Token: 0x040000CA RID: 202
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x040000CB RID: 203
		private string completedDocumentFilename;

		// Token: 0x040000CC RID: 204
		private string templateFilename;

		// Token: 0x040000CD RID: 205
		private NameObjectPairCollection args;
	}
}
