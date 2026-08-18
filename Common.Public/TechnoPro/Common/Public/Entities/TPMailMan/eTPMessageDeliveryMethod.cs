using System;

namespace TechnoPro.Common.Public.Entities.TPMailMan
{
	// Token: 0x02000164 RID: 356
	[Serializable]
	public enum eTPMessageDeliveryMethod
	{
		// Token: 0x04000698 RID: 1688
		Unknown,
		// Token: 0x04000699 RID: 1689
		PlainText,
		// Token: 0x0400069A RID: 1690
		Html,
		// Token: 0x0400069B RID: 1691
		HtmlAndPlainText = 4
	}
}
