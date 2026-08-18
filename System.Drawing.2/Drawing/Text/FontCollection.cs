using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Text
{
	// Token: 0x02000085 RID: 133
	public abstract class FontCollection : IDisposable
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x00022029 File Offset: 0x00020229
		internal FontCollection()
		{
			this.nativeFontCollection = IntPtr.Zero;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002203C File Offset: 0x0002023C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00015259 File Offset: 0x00013459
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0002204C File Offset: 0x0002024C
		public FontFamily[] Families
		{
			get
			{
				int num = 0;
				int num2 = SafeNativeMethods.Gdip.GdipGetFontCollectionFamilyCount(new HandleRef(this, this.nativeFontCollection), out num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				IntPtr[] array = new IntPtr[num];
				int num3 = 0;
				num2 = SafeNativeMethods.Gdip.GdipGetFontCollectionFamilyList(new HandleRef(this, this.nativeFontCollection), num, array, out num3);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				FontFamily[] array2 = new FontFamily[num3];
				for (int i = 0; i < num3; i++)
				{
					IntPtr family;
					SafeNativeMethods.Gdip.GdipCloneFontFamily(new HandleRef(null, array[i]), out family);
					array2[i] = new FontFamily(family);
				}
				return array2;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000220DC File Offset: 0x000202DC
		~FontCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x04000720 RID: 1824
		internal IntPtr nativeFontCollection;
	}
}
