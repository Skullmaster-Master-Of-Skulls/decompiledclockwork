using System;

namespace System.Drawing.Text
{
	// Token: 0x02000088 RID: 136
	public sealed class InstalledFontCollection : FontCollection
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x0002210C File Offset: 0x0002030C
		public InstalledFontCollection()
		{
			this.nativeFontCollection = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipNewInstalledFontCollection(out this.nativeFontCollection);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}
	}
}
