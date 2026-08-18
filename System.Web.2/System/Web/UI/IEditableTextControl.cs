using System;

namespace System.Web.UI
{
	// Token: 0x02000330 RID: 816
	public interface IEditableTextControl : ITextControl
	{
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060025E9 RID: 9705
		// (remove) Token: 0x060025EA RID: 9706
		event EventHandler TextChanged;
	}
}
