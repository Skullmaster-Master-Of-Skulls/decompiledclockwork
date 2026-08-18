using System;

namespace System.Windows.Forms.Automation
{
	// Token: 0x020004F4 RID: 1268
	internal abstract class UiaTextProvider2 : UiaTextProvider, UnsafeNativeMethods.UiaCore.ITextProvider2, UnsafeNativeMethods.UiaCore.ITextProvider
	{
		// Token: 0x0600526F RID: 21103
		public abstract UnsafeNativeMethods.UiaCore.ITextRangeProvider GetCaretRange(out UnsafeNativeMethods.BOOL isActive);

		// Token: 0x06005270 RID: 21104
		public abstract UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement);
	}
}
