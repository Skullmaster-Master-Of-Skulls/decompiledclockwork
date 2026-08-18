using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000439 RID: 1081
	internal static class WebBrowserHelper
	{
		// Token: 0x06004B72 RID: 19314 RVA: 0x0013A643 File Offset: 0x00138843
		internal static int Pix2HM(int pix, int logP)
		{
			return (2540 * pix + (logP >> 1)) / logP;
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x0013A652 File Offset: 0x00138852
		internal static int HM2Pix(int hm, int logP)
		{
			return (logP * hm + 1270) / 2540;
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06004B74 RID: 19316 RVA: 0x0013A664 File Offset: 0x00138864
		internal static int LogPixelsX
		{
			get
			{
				if (WebBrowserHelper.logPixelsX == -1)
				{
					IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
					if (dc != IntPtr.Zero)
					{
						WebBrowserHelper.logPixelsX = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 88);
						UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
					}
				}
				return WebBrowserHelper.logPixelsX;
			}
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x0013A6BB File Offset: 0x001388BB
		internal static void ResetLogPixelsX()
		{
			WebBrowserHelper.logPixelsX = -1;
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06004B76 RID: 19318 RVA: 0x0013A6C4 File Offset: 0x001388C4
		internal static int LogPixelsY
		{
			get
			{
				if (WebBrowserHelper.logPixelsY == -1)
				{
					IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
					if (dc != IntPtr.Zero)
					{
						WebBrowserHelper.logPixelsY = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 90);
						UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
					}
				}
				return WebBrowserHelper.logPixelsY;
			}
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x0013A71B File Offset: 0x0013891B
		internal static void ResetLogPixelsY()
		{
			WebBrowserHelper.logPixelsY = -1;
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x0013A724 File Offset: 0x00138924
		internal static ISelectionService GetSelectionService(Control ctl)
		{
			ISite site = ctl.Site;
			if (site != null)
			{
				object service = site.GetService(typeof(ISelectionService));
				if (service is ISelectionService)
				{
					return (ISelectionService)service;
				}
			}
			return null;
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x0013A75C File Offset: 0x0013895C
		internal static NativeMethods.COMRECT GetClipRect()
		{
			return new NativeMethods.COMRECT(new Rectangle(0, 0, 32000, 32000));
		}

		// Token: 0x04002826 RID: 10278
		internal static readonly int sinkAttached = BitVector32.CreateMask();

		// Token: 0x04002827 RID: 10279
		internal static readonly int manualUpdate = BitVector32.CreateMask(WebBrowserHelper.sinkAttached);

		// Token: 0x04002828 RID: 10280
		internal static readonly int setClientSiteFirst = BitVector32.CreateMask(WebBrowserHelper.manualUpdate);

		// Token: 0x04002829 RID: 10281
		internal static readonly int addedSelectionHandler = BitVector32.CreateMask(WebBrowserHelper.setClientSiteFirst);

		// Token: 0x0400282A RID: 10282
		internal static readonly int siteProcessedInputKey = BitVector32.CreateMask(WebBrowserHelper.addedSelectionHandler);

		// Token: 0x0400282B RID: 10283
		internal static readonly int inTransition = BitVector32.CreateMask(WebBrowserHelper.siteProcessedInputKey);

		// Token: 0x0400282C RID: 10284
		internal static readonly int processingKeyUp = BitVector32.CreateMask(WebBrowserHelper.inTransition);

		// Token: 0x0400282D RID: 10285
		internal static readonly int isMaskEdit = BitVector32.CreateMask(WebBrowserHelper.processingKeyUp);

		// Token: 0x0400282E RID: 10286
		internal static readonly int recomputeContainingControl = BitVector32.CreateMask(WebBrowserHelper.isMaskEdit);

		// Token: 0x0400282F RID: 10287
		private static int logPixelsX = -1;

		// Token: 0x04002830 RID: 10288
		private static int logPixelsY = -1;

		// Token: 0x04002831 RID: 10289
		private const int HMperInch = 2540;

		// Token: 0x04002832 RID: 10290
		private static Guid ifont_Guid = typeof(UnsafeNativeMethods.IFont).GUID;

		// Token: 0x04002833 RID: 10291
		internal static Guid windowsMediaPlayer_Clsid = new Guid("{22d6f312-b0f6-11d0-94ab-0080c74c7e95}");

		// Token: 0x04002834 RID: 10292
		internal static Guid comctlImageCombo_Clsid = new Guid("{a98a24c0-b06f-3684-8c12-c52ae341e0bc}");

		// Token: 0x04002835 RID: 10293
		internal static Guid maskEdit_Clsid = new Guid("{c932ba85-4374-101b-a56c-00aa003668dc}");

		// Token: 0x04002836 RID: 10294
		internal static readonly int REGMSG_MSG = SafeNativeMethods.RegisterWindowMessage(Application.WindowMessagesVersion + "_subclassCheck");

		// Token: 0x04002837 RID: 10295
		internal const int REGMSG_RETVAL = 123;

		// Token: 0x0200082A RID: 2090
		internal enum AXState
		{
			// Token: 0x04004346 RID: 17222
			Passive,
			// Token: 0x04004347 RID: 17223
			Loaded,
			// Token: 0x04004348 RID: 17224
			Running,
			// Token: 0x04004349 RID: 17225
			InPlaceActive = 4,
			// Token: 0x0400434A RID: 17226
			UIActive = 8
		}

		// Token: 0x0200082B RID: 2091
		internal enum AXEditMode
		{
			// Token: 0x0400434C RID: 17228
			None,
			// Token: 0x0400434D RID: 17229
			Object,
			// Token: 0x0400434E RID: 17230
			Host
		}

		// Token: 0x0200082C RID: 2092
		internal enum SelectionStyle
		{
			// Token: 0x04004350 RID: 17232
			NotSelected,
			// Token: 0x04004351 RID: 17233
			Selected,
			// Token: 0x04004352 RID: 17234
			Active
		}
	}
}
