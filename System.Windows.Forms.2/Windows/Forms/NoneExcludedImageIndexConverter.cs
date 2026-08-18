using System;

namespace System.Windows.Forms
{
	// Token: 0x0200030C RID: 780
	internal sealed class NoneExcludedImageIndexConverter : ImageIndexConverter
	{
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected override bool IncludeNoneAsStandardValue
		{
			get
			{
				return false;
			}
		}
	}
}
