using System;
using System.Drawing;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms
{
	// Token: 0x02000450 RID: 1104
	internal sealed class WindowsGraphicsWrapper : IDisposable
	{
		// Token: 0x06004D5B RID: 19803 RVA: 0x0013FBB8 File Offset: 0x0013DDB8
		public WindowsGraphicsWrapper(IDeviceContext idc, TextFormatFlags flags)
		{
			if (idc is Graphics)
			{
				ApplyGraphicsProperties applyGraphicsProperties = ApplyGraphicsProperties.None;
				if ((flags & TextFormatFlags.PreserveGraphicsClipping) != TextFormatFlags.Default)
				{
					applyGraphicsProperties |= ApplyGraphicsProperties.Clipping;
				}
				if ((flags & TextFormatFlags.PreserveGraphicsTranslateTransform) != TextFormatFlags.Default)
				{
					applyGraphicsProperties |= ApplyGraphicsProperties.TranslateTransform;
				}
				if (applyGraphicsProperties != ApplyGraphicsProperties.None)
				{
					this.wg = WindowsGraphics.FromGraphics(idc as Graphics, applyGraphicsProperties);
				}
			}
			else
			{
				this.wg = (idc as WindowsGraphics);
				if (this.wg != null)
				{
					this.idc = idc;
				}
			}
			if (this.wg == null)
			{
				this.idc = idc;
				this.wg = WindowsGraphics.FromHdc(idc.GetHdc());
			}
			if ((flags & TextFormatFlags.LeftAndRightPadding) != TextFormatFlags.Default)
			{
				this.wg.TextPadding = TextPaddingOptions.LeftAndRightPadding;
				return;
			}
			if ((flags & TextFormatFlags.NoPadding) != TextFormatFlags.Default)
			{
				this.wg.TextPadding = TextPaddingOptions.NoPadding;
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06004D5C RID: 19804 RVA: 0x0013FC6C File Offset: 0x0013DE6C
		public WindowsGraphics WindowsGraphics
		{
			get
			{
				return this.wg;
			}
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x0013FC74 File Offset: 0x0013DE74
		~WindowsGraphicsWrapper()
		{
			this.Dispose(false);
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x0013FCA4 File Offset: 0x0013DEA4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x0013FCB4 File Offset: 0x0013DEB4
		public void Dispose(bool disposing)
		{
			if (this.wg != null)
			{
				if (this.wg != this.idc)
				{
					this.wg.Dispose();
					if (this.idc != null)
					{
						this.idc.ReleaseHdc();
					}
				}
				this.idc = null;
				this.wg = null;
			}
		}

		// Token: 0x040028D7 RID: 10455
		private IDeviceContext idc;

		// Token: 0x040028D8 RID: 10456
		private WindowsGraphics wg;
	}
}
