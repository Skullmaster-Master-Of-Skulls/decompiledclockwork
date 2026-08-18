using System;

namespace MailBee.Mime
{
	// Token: 0x0200054C RID: 1356
	[Flags]
	public enum MailMergeTargets
	{
		// Token: 0x04001EBF RID: 7871
		None = 0,
		// Token: 0x04001EC0 RID: 7872
		BodyPlainText = 1,
		// Token: 0x04001EC1 RID: 7873
		BodyHtmlText = 2,
		// Token: 0x04001EC2 RID: 7874
		From = 4,
		// Token: 0x04001EC3 RID: 7875
		ReplyTo = 8,
		// Token: 0x04001EC4 RID: 7876
		Recipients = 16,
		// Token: 0x04001EC5 RID: 7877
		Subject = 32,
		// Token: 0x04001EC6 RID: 7878
		Other = 64,
		// Token: 0x04001EC7 RID: 7879
		All = 127
	}
}
