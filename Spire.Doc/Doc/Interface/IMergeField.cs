using System;

namespace Spire.Doc.Interface
{
	// Token: 0x0200050C RID: 1292
	public interface IMergeField : IField
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06004269 RID: 17001
		// (set) Token: 0x0600426A RID: 17002
		string FieldName { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600426B RID: 17003
		// (set) Token: 0x0600426C RID: 17004
		string TextBefore { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600426D RID: 17005
		// (set) Token: 0x0600426E RID: 17006
		string TextAfter { get; set; }
	}
}
