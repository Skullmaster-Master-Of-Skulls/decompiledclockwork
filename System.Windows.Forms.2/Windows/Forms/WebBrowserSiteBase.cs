using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000440 RID: 1088
	public class WebBrowserSiteBase : UnsafeNativeMethods.IOleControlSite, UnsafeNativeMethods.IOleClientSite, UnsafeNativeMethods.IOleInPlaceSite, UnsafeNativeMethods.ISimpleFrameSite, UnsafeNativeMethods.IPropertyNotifySink, IDisposable
	{
		// Token: 0x06004B8F RID: 19343 RVA: 0x0013A8ED File Offset: 0x00138AED
		internal WebBrowserSiteBase(WebBrowserBase h)
		{
			if (h == null)
			{
				throw new ArgumentNullException("h");
			}
			this.host = h;
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x0013A90A File Offset: 0x00138B0A
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x0013A913 File Offset: 0x00138B13
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopEvents();
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06004B92 RID: 19346 RVA: 0x0013A91E File Offset: 0x00138B1E
		internal WebBrowserBase Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.IOleControlSite.OnControlInfoChanged()
		{
			return 0;
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IOleControlSite.LockInPlaceActive(int fLock)
		{
			return -2147467263;
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x0003BF27 File Offset: 0x0003A127
		int UnsafeNativeMethods.IOleControlSite.GetExtendedControl(out object ppDisp)
		{
			ppDisp = null;
			return -2147467263;
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x0013A928 File Offset: 0x00138B28
		int UnsafeNativeMethods.IOleControlSite.TransformCoords(NativeMethods._POINTL pPtlHimetric, NativeMethods.tagPOINTF pPtfContainer, int dwFlags)
		{
			if ((dwFlags & 4) != 0)
			{
				if ((dwFlags & 2) != 0)
				{
					pPtfContainer.x = (float)WebBrowserHelper.HM2Pix(pPtlHimetric.x, WebBrowserHelper.LogPixelsX);
					pPtfContainer.y = (float)WebBrowserHelper.HM2Pix(pPtlHimetric.y, WebBrowserHelper.LogPixelsY);
				}
				else
				{
					if ((dwFlags & 1) == 0)
					{
						return -2147024809;
					}
					pPtfContainer.x = (float)WebBrowserHelper.HM2Pix(pPtlHimetric.x, WebBrowserHelper.LogPixelsX);
					pPtfContainer.y = (float)WebBrowserHelper.HM2Pix(pPtlHimetric.y, WebBrowserHelper.LogPixelsY);
				}
			}
			else
			{
				if ((dwFlags & 8) == 0)
				{
					return -2147024809;
				}
				if ((dwFlags & 2) != 0)
				{
					pPtlHimetric.x = WebBrowserHelper.Pix2HM((int)pPtfContainer.x, WebBrowserHelper.LogPixelsX);
					pPtlHimetric.y = WebBrowserHelper.Pix2HM((int)pPtfContainer.y, WebBrowserHelper.LogPixelsY);
				}
				else
				{
					if ((dwFlags & 1) == 0)
					{
						return -2147024809;
					}
					pPtlHimetric.x = WebBrowserHelper.Pix2HM((int)pPtfContainer.x, WebBrowserHelper.LogPixelsX);
					pPtlHimetric.y = WebBrowserHelper.Pix2HM((int)pPtfContainer.y, WebBrowserHelper.LogPixelsY);
				}
			}
			return 0;
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x0013AA2C File Offset: 0x00138C2C
		int UnsafeNativeMethods.IOleControlSite.TranslateAccelerator(ref NativeMethods.MSG pMsg, int grfModifiers)
		{
			this.Host.SetAXHostState(WebBrowserHelper.siteProcessedInputKey, true);
			Message message = default(Message);
			message.Msg = pMsg.message;
			message.WParam = pMsg.wParam;
			message.LParam = pMsg.lParam;
			message.HWnd = pMsg.hwnd;
			int result;
			try
			{
				result = ((this.Host.PreProcessControlMessage(ref message) == PreProcessControlState.MessageProcessed) ? 0 : 1);
			}
			finally
			{
				this.Host.SetAXHostState(WebBrowserHelper.siteProcessedInputKey, false);
			}
			return result;
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.IOleControlSite.OnFocus(int fGotFocus)
		{
			return 0;
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IOleControlSite.ShowPropertyFrame()
		{
			return -2147467263;
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IOleClientSite.SaveObject()
		{
			return -2147467263;
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0003BE4F File Offset: 0x0003A04F
		int UnsafeNativeMethods.IOleClientSite.GetMoniker(int dwAssign, int dwWhichMoniker, out object moniker)
		{
			moniker = null;
			return -2147467263;
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0013AAC4 File Offset: 0x00138CC4
		int UnsafeNativeMethods.IOleClientSite.GetContainer(out UnsafeNativeMethods.IOleContainer container)
		{
			container = this.Host.GetParentContainer();
			return 0;
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x0013AAD4 File Offset: 0x00138CD4
		int UnsafeNativeMethods.IOleClientSite.ShowObject()
		{
			if (this.Host.ActiveXState >= WebBrowserHelper.AXState.InPlaceActive)
			{
				IntPtr intPtr;
				if (NativeMethods.Succeeded(this.Host.AXInPlaceObject.GetWindow(out intPtr)))
				{
					if (this.Host.GetHandleNoCreate() != intPtr && intPtr != IntPtr.Zero)
					{
						this.Host.AttachWindow(intPtr);
						this.OnActiveXRectChange(new NativeMethods.COMRECT(this.Host.Bounds));
					}
				}
				else if (this.Host.AXInPlaceObject is UnsafeNativeMethods.IOleInPlaceObjectWindowless)
				{
					throw new InvalidOperationException(SR.GetString("AXWindowlessControl"));
				}
			}
			return 0;
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.IOleClientSite.OnShowWindow(int fShow)
		{
			return 0;
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IOleClientSite.RequestNewObjectLayout()
		{
			return -2147467263;
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x0013AB74 File Offset: 0x00138D74
		IntPtr UnsafeNativeMethods.IOleInPlaceSite.GetWindow()
		{
			IntPtr parent;
			try
			{
				parent = UnsafeNativeMethods.GetParent(new HandleRef(this.Host, this.Host.Handle));
			}
			catch (Exception ex)
			{
				throw;
			}
			return parent;
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IOleInPlaceSite.ContextSensitiveHelp(int fEnterMode)
		{
			return -2147467263;
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.IOleInPlaceSite.CanInPlaceActivate()
		{
			return 0;
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x0013ABB4 File Offset: 0x00138DB4
		int UnsafeNativeMethods.IOleInPlaceSite.OnInPlaceActivate()
		{
			this.Host.ActiveXState = WebBrowserHelper.AXState.InPlaceActive;
			this.OnActiveXRectChange(new NativeMethods.COMRECT(this.Host.Bounds));
			return 0;
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0013ABDA File Offset: 0x00138DDA
		int UnsafeNativeMethods.IOleInPlaceSite.OnUIActivate()
		{
			this.Host.ActiveXState = WebBrowserHelper.AXState.UIActive;
			this.Host.GetParentContainer().OnUIActivate(this.Host);
			return 0;
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0013AC00 File Offset: 0x00138E00
		int UnsafeNativeMethods.IOleInPlaceSite.GetWindowContext(out UnsafeNativeMethods.IOleInPlaceFrame ppFrame, out UnsafeNativeMethods.IOleInPlaceUIWindow ppDoc, NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect, NativeMethods.tagOIFI lpFrameInfo)
		{
			ppDoc = null;
			ppFrame = this.Host.GetParentContainer();
			lprcPosRect.left = this.Host.Bounds.X;
			lprcPosRect.top = this.Host.Bounds.Y;
			lprcPosRect.right = this.Host.Bounds.Width + this.Host.Bounds.X;
			lprcPosRect.bottom = this.Host.Bounds.Height + this.Host.Bounds.Y;
			lprcClipRect = WebBrowserHelper.GetClipRect();
			if (lpFrameInfo != null)
			{
				lpFrameInfo.cb = Marshal.SizeOf(typeof(NativeMethods.tagOIFI));
				lpFrameInfo.fMDIApp = false;
				lpFrameInfo.hAccel = IntPtr.Zero;
				lpFrameInfo.cAccelEntries = 0;
				lpFrameInfo.hwndFrame = ((this.Host.ParentInternal == null) ? IntPtr.Zero : this.Host.ParentInternal.Handle);
			}
			return 0;
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x00013062 File Offset: 0x00011262
		int UnsafeNativeMethods.IOleInPlaceSite.Scroll(NativeMethods.tagSIZE scrollExtant)
		{
			return 1;
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0013AD12 File Offset: 0x00138F12
		int UnsafeNativeMethods.IOleInPlaceSite.OnUIDeactivate(int fUndoable)
		{
			this.Host.GetParentContainer().OnUIDeactivate(this.Host);
			if (this.Host.ActiveXState > WebBrowserHelper.AXState.InPlaceActive)
			{
				this.Host.ActiveXState = WebBrowserHelper.AXState.InPlaceActive;
			}
			return 0;
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x0013AD45 File Offset: 0x00138F45
		int UnsafeNativeMethods.IOleInPlaceSite.OnInPlaceDeactivate()
		{
			if (this.Host.ActiveXState == WebBrowserHelper.AXState.UIActive)
			{
				((UnsafeNativeMethods.IOleInPlaceSite)this).OnUIDeactivate(0);
			}
			this.Host.GetParentContainer().OnInPlaceDeactivate(this.Host);
			this.Host.ActiveXState = WebBrowserHelper.AXState.Running;
			return 0;
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.IOleInPlaceSite.DiscardUndoState()
		{
			return 0;
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x0013AD80 File Offset: 0x00138F80
		int UnsafeNativeMethods.IOleInPlaceSite.DeactivateAndUndo()
		{
			return this.Host.AXInPlaceObject.UIDeactivate();
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x0013AD92 File Offset: 0x00138F92
		int UnsafeNativeMethods.IOleInPlaceSite.OnPosRectChange(NativeMethods.COMRECT lprcPosRect)
		{
			return this.OnActiveXRectChange(lprcPosRect);
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.ISimpleFrameSite.PreMessageFilter(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp, ref IntPtr plResult, ref int pdwCookie)
		{
			return 0;
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x00013062 File Offset: 0x00011262
		int UnsafeNativeMethods.ISimpleFrameSite.PostMessageFilter(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp, ref IntPtr plResult, int dwCookie)
		{
			return 1;
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x0013AD9C File Offset: 0x00138F9C
		void UnsafeNativeMethods.IPropertyNotifySink.OnChanged(int dispid)
		{
			if (this.Host.NoComponentChangeEvents != 0)
			{
				return;
			}
			WebBrowserBase webBrowserBase = this.Host;
			int noComponentChangeEvents = webBrowserBase.NoComponentChangeEvents;
			webBrowserBase.NoComponentChangeEvents = noComponentChangeEvents + 1;
			try
			{
				this.OnPropertyChanged(dispid);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				WebBrowserBase webBrowserBase2 = this.Host;
				noComponentChangeEvents = webBrowserBase2.NoComponentChangeEvents;
				webBrowserBase2.NoComponentChangeEvents = noComponentChangeEvents - 1;
			}
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.IPropertyNotifySink.OnRequestEdit(int dispid)
		{
			return 0;
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x0013AE0C File Offset: 0x0013900C
		internal virtual void OnPropertyChanged(int dispid)
		{
			try
			{
				ISite site = this.Host.Site;
				if (site != null)
				{
					IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						try
						{
							componentChangeService.OnComponentChanging(this.Host, null);
						}
						catch (CheckoutException ex)
						{
							if (ex == CheckoutException.Canceled)
							{
								return;
							}
							throw ex;
						}
						componentChangeService.OnComponentChanged(this.Host, null, null, null);
					}
				}
			}
			catch (Exception ex2)
			{
				throw;
			}
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x0013AE90 File Offset: 0x00139090
		internal WebBrowserBase GetAXHost()
		{
			return this.Host;
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x0013AE98 File Offset: 0x00139098
		internal void StartEvents()
		{
			if (this.connectionPoint != null)
			{
				return;
			}
			object activeXInstance = this.Host.activeXInstance;
			if (activeXInstance != null)
			{
				try
				{
					this.connectionPoint = new AxHost.ConnectionPointCookie(activeXInstance, this, typeof(UnsafeNativeMethods.IPropertyNotifySink));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x0013AEF4 File Offset: 0x001390F4
		internal void StopEvents()
		{
			if (this.connectionPoint != null)
			{
				this.connectionPoint.Disconnect();
				this.connectionPoint = null;
			}
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x0013AF10 File Offset: 0x00139110
		private int OnActiveXRectChange(NativeMethods.COMRECT lprcPosRect)
		{
			this.Host.AXInPlaceObject.SetObjectRects(NativeMethods.COMRECT.FromXYWH(0, 0, lprcPosRect.right - lprcPosRect.left, lprcPosRect.bottom - lprcPosRect.top), WebBrowserHelper.GetClipRect());
			this.Host.MakeDirty();
			return 0;
		}

		// Token: 0x0400283D RID: 10301
		private WebBrowserBase host;

		// Token: 0x0400283E RID: 10302
		private AxHost.ConnectionPointCookie connectionPoint;
	}
}
