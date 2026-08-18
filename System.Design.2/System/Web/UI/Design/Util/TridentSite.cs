using System;
using System.Design;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x02000163 RID: 355
	[ClassInterface(ClassInterfaceType.None)]
	internal class TridentSite : NativeMethods.IOleClientSite, NativeMethods.IOleDocumentSite, NativeMethods.IOleInPlaceSite, NativeMethods.IOleInPlaceFrame, NativeMethods.IDocHostUIHandler
	{
		// Token: 0x06000C72 RID: 3186 RVA: 0x000513BD File Offset: 0x0004F5BD
		public TridentSite(Control parent)
		{
			this.parentControl = parent;
			this.resizeHandler = new EventHandler(this.OnParentResize);
			this.parentControl.Resize += this.resizeHandler;
			this.CreateDocument();
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000513F6 File Offset: 0x0004F5F6
		public NativeMethods.IHTMLDocument2 GetDocument()
		{
			return this.tridentDocument;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000513FE File Offset: 0x0004F5FE
		public void Activate()
		{
			this.ActivateDocument();
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00051408 File Offset: 0x0004F608
		protected virtual void OnParentResize(object src, EventArgs e)
		{
			if (this.tridentView != null)
			{
				NativeMethods.COMRECT rect = new NativeMethods.COMRECT();
				NativeMethods.GetClientRect(this.parentControl.Handle, rect);
				this.tridentView.SetRect(rect);
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void SaveObject()
		{
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00051441 File Offset: 0x0004F641
		public virtual object GetMoniker(int dwAssign, int dwWhichMoniker)
		{
			throw new COMException(string.Empty, -2147467263);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00051452 File Offset: 0x0004F652
		public virtual int GetContainer(out NativeMethods.IOleContainer ppContainer)
		{
			ppContainer = null;
			return -2147467262;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void ShowObject()
		{
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnShowWindow(int fShow)
		{
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void RequestNewObjectLayout()
		{
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0005145C File Offset: 0x0004F65C
		public virtual int ActivateMe(NativeMethods.IOleDocumentView pViewToActivate)
		{
			if (pViewToActivate == null)
			{
				return -2147024809;
			}
			NativeMethods.COMRECT rect = new NativeMethods.COMRECT();
			NativeMethods.GetClientRect(this.parentControl.Handle, rect);
			this.tridentView = pViewToActivate;
			this.tridentView.SetInPlaceSite(this);
			this.tridentView.UIActivate(1);
			this.tridentView.SetRect(rect);
			this.tridentView.Show(1);
			return 0;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x000514C2 File Offset: 0x0004F6C2
		public virtual IntPtr GetWindow()
		{
			return this.parentControl.Handle;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00051441 File Offset: 0x0004F641
		public virtual void ContextSensitiveHelp(int fEnterMode)
		{
			throw new COMException(string.Empty, -2147467263);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual int CanInPlaceActivate()
		{
			return 0;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnInPlaceActivate()
		{
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnUIActivate()
		{
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000514D0 File Offset: 0x0004F6D0
		public virtual void GetWindowContext(out NativeMethods.IOleInPlaceFrame ppFrame, out NativeMethods.IOleInPlaceUIWindow ppDoc, NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect, NativeMethods.tagOIFI lpFrameInfo)
		{
			ppFrame = this;
			ppDoc = null;
			NativeMethods.GetClientRect(this.parentControl.Handle, lprcPosRect);
			NativeMethods.GetClientRect(this.parentControl.Handle, lprcClipRect);
			lpFrameInfo.cb = Marshal.SizeOf(typeof(NativeMethods.tagOIFI));
			lpFrameInfo.fMDIApp = 0;
			lpFrameInfo.hwndFrame = this.parentControl.Handle;
			lpFrameInfo.hAccel = IntPtr.Zero;
			lpFrameInfo.cAccelEntries = 0;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0005154C File Offset: 0x0004F74C
		public virtual int Scroll(NativeMethods.tagSIZE scrollExtant)
		{
			return -2147467263;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnUIDeactivate(int fUndoable)
		{
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnInPlaceDeactivate()
		{
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00051553 File Offset: 0x0004F753
		public virtual void DiscardUndoState()
		{
			throw new COMException("Not implemented", -2147467263);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void DeactivateAndUndo()
		{
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual int OnPosRectChange(NativeMethods.COMRECT lprcPosRect)
		{
			return 0;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00051441 File Offset: 0x0004F641
		public virtual void GetBorder(NativeMethods.COMRECT lprectBorder)
		{
			throw new COMException(string.Empty, -2147467263);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00051441 File Offset: 0x0004F641
		public virtual void RequestBorderSpace(NativeMethods.COMRECT pborderwidths)
		{
			throw new COMException(string.Empty, -2147467263);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00051441 File Offset: 0x0004F641
		public virtual void SetBorderSpace(NativeMethods.COMRECT pborderwidths)
		{
			throw new COMException(string.Empty, -2147467263);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void SetActiveObject(NativeMethods.IOleInPlaceActiveObject pActiveObject, string pszObjName)
		{
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00051441 File Offset: 0x0004F641
		public virtual void InsertMenus(IntPtr hmenuShared, object lpMenuWidths)
		{
			throw new COMException(string.Empty, -2147467263);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00051441 File Offset: 0x0004F641
		public virtual void SetMenu(IntPtr hmenuShared, IntPtr holemenu, IntPtr hwndActiveObject)
		{
			throw new COMException(string.Empty, -2147467263);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00051441 File Offset: 0x0004F641
		public virtual void RemoveMenus(IntPtr hmenuShared)
		{
			throw new COMException(string.Empty, -2147467263);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void SetStatusText(string pszStatusText)
		{
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void EnableModeless(int fEnable)
		{
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00003B0F File Offset: 0x00001D0F
		public virtual int TranslateAccelerator(ref NativeMethods.MSG lpmsg, short wID)
		{
			return 1;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual int ShowContextMenu(int dwID, NativeMethods.POINT pt, object pcmdtReserved, object pdispReserved)
		{
			return 0;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00051564 File Offset: 0x0004F764
		public virtual int GetHostInfo(NativeMethods.DOCHOSTUIINFO info)
		{
			info.dwDoubleClick = 0;
			info.dwFlags = 149;
			return 0;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual int EnableModeless(bool fEnable)
		{
			return 0;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual int ShowUI(int dwID, NativeMethods.IOleInPlaceActiveObject activeObject, NativeMethods.IOleCommandTarget commandTarget, NativeMethods.IOleInPlaceFrame frame, NativeMethods.IOleInPlaceUIWindow doc)
		{
			return 0;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual int HideUI()
		{
			return 0;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual int UpdateUI()
		{
			return 0;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0005154C File Offset: 0x0004F74C
		public virtual int OnDocWindowActivate(bool fActivate)
		{
			return -2147467263;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0005154C File Offset: 0x0004F74C
		public virtual int OnFrameWindowActivate(bool fActivate)
		{
			return -2147467263;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0005154C File Offset: 0x0004F74C
		public virtual int ResizeBorder(NativeMethods.COMRECT rect, NativeMethods.IOleInPlaceUIWindow doc, bool fFrameWindow)
		{
			return -2147467263;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00051579 File Offset: 0x0004F779
		public virtual int GetOptionKeyPath(string[] pbstrKey, int dw)
		{
			pbstrKey[0] = null;
			return 0;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00051580 File Offset: 0x0004F780
		public virtual int GetDropTarget(NativeMethods.IOleDropTarget pDropTarget, out NativeMethods.IOleDropTarget ppDropTarget)
		{
			ppDropTarget = null;
			return 1;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00051586 File Offset: 0x0004F786
		public virtual int GetExternal(out object ppDispatch)
		{
			ppDispatch = null;
			return 0;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual int TranslateAccelerator(ref NativeMethods.MSG msg, ref Guid group, int nCmdID)
		{
			return 0;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0005158C File Offset: 0x0004F78C
		public virtual int TranslateUrl(int dwTranslate, string strUrlIn, out string pstrUrlOut)
		{
			pstrUrlOut = null;
			return -2147467263;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00051596 File Offset: 0x0004F796
		public virtual int FilterDataObject(System.Runtime.InteropServices.ComTypes.IDataObject pDO, out System.Runtime.InteropServices.ComTypes.IDataObject ppDORet)
		{
			ppDORet = null;
			return 0;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0005159C File Offset: 0x0004F79C
		protected void CreateDocument()
		{
			try
			{
				this.tridentDocument = (NativeMethods.IHTMLDocument2)new NativeMethods.HTMLDocument();
				this.tridentOleObject = (NativeMethods.IOleObject)this.tridentDocument;
				this.tridentOleObject.SetClientSite(this);
				NativeMethods.IPersistStreamInit persistStreamInit = (NativeMethods.IPersistStreamInit)this.tridentDocument;
				persistStreamInit.InitNew();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00051600 File Offset: 0x0004F800
		protected void ActivateDocument()
		{
			try
			{
				NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
				NativeMethods.GetClientRect(this.parentControl.Handle, comrect);
				this.tridentOleObject.DoVerb(-4, IntPtr.Zero, this, 0, this.parentControl.Handle, comrect);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x040007A6 RID: 1958
		protected Control parentControl;

		// Token: 0x040007A7 RID: 1959
		protected NativeMethods.IOleDocumentView tridentView;

		// Token: 0x040007A8 RID: 1960
		protected NativeMethods.IOleObject tridentOleObject;

		// Token: 0x040007A9 RID: 1961
		protected NativeMethods.IHTMLDocument2 tridentDocument;

		// Token: 0x040007AA RID: 1962
		protected EventHandler resizeHandler;
	}
}
