using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x0200027A RID: 634
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class HtmlDocument
	{
		// Token: 0x0600283D RID: 10301 RVA: 0x000BAF60 File Offset: 0x000B9160
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		internal HtmlDocument(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLDocument doc)
		{
			this.htmlDocument2 = (UnsafeNativeMethods.IHTMLDocument2)doc;
			this.shimManager = shimManager;
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x000BAF7B File Offset: 0x000B917B
		internal UnsafeNativeMethods.IHTMLDocument2 NativeHtmlDocument2
		{
			get
			{
				return this.htmlDocument2;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x000BAF84 File Offset: 0x000B9184
		private HtmlDocument.HtmlDocumentShim DocumentShim
		{
			get
			{
				if (this.ShimManager != null)
				{
					HtmlDocument.HtmlDocumentShim documentShim = this.ShimManager.GetDocumentShim(this);
					if (documentShim == null)
					{
						this.shimManager.AddDocumentShim(this);
						documentShim = this.ShimManager.GetDocumentShim(this);
					}
					return documentShim;
				}
				return null;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x000BAFC5 File Offset: 0x000B91C5
		private HtmlShimManager ShimManager
		{
			get
			{
				return this.shimManager;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x000BAFD0 File Offset: 0x000B91D0
		public HtmlElement ActiveElement
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement activeElement = this.NativeHtmlDocument2.GetActiveElement();
				if (activeElement == null)
				{
					return null;
				}
				return new HtmlElement(this.ShimManager, activeElement);
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000BAFFC File Offset: 0x000B91FC
		public HtmlElement Body
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement body = this.NativeHtmlDocument2.GetBody();
				if (body == null)
				{
					return null;
				}
				return new HtmlElement(this.ShimManager, body);
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x000BB026 File Offset: 0x000B9226
		// (set) Token: 0x06002844 RID: 10308 RVA: 0x000BB034 File Offset: 0x000B9234
		public string Domain
		{
			get
			{
				return this.NativeHtmlDocument2.GetDomain();
			}
			set
			{
				try
				{
					this.NativeHtmlDocument2.SetDomain(value);
				}
				catch (ArgumentException)
				{
					throw new ArgumentException(SR.GetString("HtmlDocumentInvalidDomain"));
				}
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x000BB070 File Offset: 0x000B9270
		// (set) Token: 0x06002846 RID: 10310 RVA: 0x000BB07D File Offset: 0x000B927D
		public string Title
		{
			get
			{
				return this.NativeHtmlDocument2.GetTitle();
			}
			set
			{
				this.NativeHtmlDocument2.SetTitle(value);
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x000BB08C File Offset: 0x000B928C
		public Uri Url
		{
			get
			{
				UnsafeNativeMethods.IHTMLLocation location = this.NativeHtmlDocument2.GetLocation();
				string text = (location == null) ? "" : location.GetHref();
				if (!string.IsNullOrEmpty(text))
				{
					return new Uri(text);
				}
				return null;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x000BB0C8 File Offset: 0x000B92C8
		public HtmlWindow Window
		{
			get
			{
				UnsafeNativeMethods.IHTMLWindow2 parentWindow = this.NativeHtmlDocument2.GetParentWindow();
				if (parentWindow == null)
				{
					return null;
				}
				return new HtmlWindow(this.ShimManager, parentWindow);
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x000BB0F4 File Offset: 0x000B92F4
		// (set) Token: 0x0600284A RID: 10314 RVA: 0x000BB13C File Offset: 0x000B933C
		public Color BackColor
		{
			get
			{
				Color result = Color.Empty;
				try
				{
					result = this.ColorFromObject(this.NativeHtmlDocument2.GetBgColor());
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return result;
			}
			set
			{
				int num = (int)value.R << 16 | (int)value.G << 8 | (int)value.B;
				this.NativeHtmlDocument2.SetBgColor(num);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x000BB178 File Offset: 0x000B9378
		// (set) Token: 0x0600284C RID: 10316 RVA: 0x000BB1C0 File Offset: 0x000B93C0
		public Color ForeColor
		{
			get
			{
				Color result = Color.Empty;
				try
				{
					result = this.ColorFromObject(this.NativeHtmlDocument2.GetFgColor());
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return result;
			}
			set
			{
				int num = (int)value.R << 16 | (int)value.G << 8 | (int)value.B;
				this.NativeHtmlDocument2.SetFgColor(num);
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x000BB1FC File Offset: 0x000B93FC
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x000BB244 File Offset: 0x000B9444
		public Color LinkColor
		{
			get
			{
				Color result = Color.Empty;
				try
				{
					result = this.ColorFromObject(this.NativeHtmlDocument2.GetLinkColor());
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return result;
			}
			set
			{
				int num = (int)value.R << 16 | (int)value.G << 8 | (int)value.B;
				this.NativeHtmlDocument2.SetLinkColor(num);
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000BB280 File Offset: 0x000B9480
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x000BB2C8 File Offset: 0x000B94C8
		public Color ActiveLinkColor
		{
			get
			{
				Color result = Color.Empty;
				try
				{
					result = this.ColorFromObject(this.NativeHtmlDocument2.GetAlinkColor());
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return result;
			}
			set
			{
				int num = (int)value.R << 16 | (int)value.G << 8 | (int)value.B;
				this.NativeHtmlDocument2.SetAlinkColor(num);
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x000BB304 File Offset: 0x000B9504
		// (set) Token: 0x06002852 RID: 10322 RVA: 0x000BB34C File Offset: 0x000B954C
		public Color VisitedLinkColor
		{
			get
			{
				Color result = Color.Empty;
				try
				{
					result = this.ColorFromObject(this.NativeHtmlDocument2.GetVlinkColor());
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return result;
			}
			set
			{
				int num = (int)value.R << 16 | (int)value.G << 8 | (int)value.B;
				this.NativeHtmlDocument2.SetVlinkColor(num);
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x000BB387 File Offset: 0x000B9587
		public bool Focused
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLDocument4)this.NativeHtmlDocument2).HasFocus();
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x000BB399 File Offset: 0x000B9599
		public object DomDocument
		{
			get
			{
				return this.NativeHtmlDocument2;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x000BB3A1 File Offset: 0x000B95A1
		// (set) Token: 0x06002856 RID: 10326 RVA: 0x000BB3AE File Offset: 0x000B95AE
		public string Cookie
		{
			get
			{
				return this.NativeHtmlDocument2.GetCookie();
			}
			set
			{
				this.NativeHtmlDocument2.SetCookie(value);
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x000BB3BC File Offset: 0x000B95BC
		// (set) Token: 0x06002858 RID: 10328 RVA: 0x000BB3D8 File Offset: 0x000B95D8
		public bool RightToLeft
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).GetDir() == "rtl";
			}
			set
			{
				((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).SetDir(value ? "rtl" : "ltr");
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x000BB3F9 File Offset: 0x000B95F9
		// (set) Token: 0x0600285A RID: 10330 RVA: 0x000BB406 File Offset: 0x000B9606
		public string Encoding
		{
			get
			{
				return this.NativeHtmlDocument2.GetCharset();
			}
			set
			{
				this.NativeHtmlDocument2.SetCharset(value);
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x000BB414 File Offset: 0x000B9614
		public string DefaultEncoding
		{
			get
			{
				return this.NativeHtmlDocument2.GetDefaultCharset();
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x000BB424 File Offset: 0x000B9624
		public HtmlElementCollection All
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection all = this.NativeHtmlDocument2.GetAll();
				if (all == null)
				{
					return new HtmlElementCollection(this.ShimManager);
				}
				return new HtmlElementCollection(this.ShimManager, all);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x000BB458 File Offset: 0x000B9658
		public HtmlElementCollection Links
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection links = this.NativeHtmlDocument2.GetLinks();
				if (links == null)
				{
					return new HtmlElementCollection(this.ShimManager);
				}
				return new HtmlElementCollection(this.ShimManager, links);
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600285E RID: 10334 RVA: 0x000BB48C File Offset: 0x000B968C
		public HtmlElementCollection Images
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection images = this.NativeHtmlDocument2.GetImages();
				if (images == null)
				{
					return new HtmlElementCollection(this.ShimManager);
				}
				return new HtmlElementCollection(this.ShimManager, images);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x000BB4C0 File Offset: 0x000B96C0
		public HtmlElementCollection Forms
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection forms = this.NativeHtmlDocument2.GetForms();
				if (forms == null)
				{
					return new HtmlElementCollection(this.ShimManager);
				}
				return new HtmlElementCollection(this.ShimManager, forms);
			}
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000BB4F4 File Offset: 0x000B96F4
		public void Write(string text)
		{
			object[] psarray = new object[]
			{
				text
			};
			this.NativeHtmlDocument2.Write(psarray);
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000BB519 File Offset: 0x000B9719
		public void ExecCommand(string command, bool showUI, object value)
		{
			this.NativeHtmlDocument2.ExecCommand(command, showUI, value);
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000BB52A File Offset: 0x000B972A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Focus()
		{
			((UnsafeNativeMethods.IHTMLDocument4)this.NativeHtmlDocument2).Focus();
			((UnsafeNativeMethods.IHTMLDocument4)this.NativeHtmlDocument2).Focus();
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000BB54C File Offset: 0x000B974C
		public HtmlElement GetElementById(string id)
		{
			UnsafeNativeMethods.IHTMLElement elementById = ((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).GetElementById(id);
			if (elementById == null)
			{
				return null;
			}
			return new HtmlElement(this.ShimManager, elementById);
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000BB57C File Offset: 0x000B977C
		public HtmlElement GetElementFromPoint(Point point)
		{
			UnsafeNativeMethods.IHTMLElement ihtmlelement = this.NativeHtmlDocument2.ElementFromPoint(point.X, point.Y);
			if (ihtmlelement == null)
			{
				return null;
			}
			return new HtmlElement(this.ShimManager, ihtmlelement);
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000BB5B4 File Offset: 0x000B97B4
		public HtmlElementCollection GetElementsByTagName(string tagName)
		{
			UnsafeNativeMethods.IHTMLElementCollection elementsByTagName = ((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).GetElementsByTagName(tagName);
			if (elementsByTagName == null)
			{
				return new HtmlElementCollection(this.ShimManager);
			}
			return new HtmlElementCollection(this.ShimManager, elementsByTagName);
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000BB5F0 File Offset: 0x000B97F0
		public HtmlDocument OpenNew(bool replaceInHistory)
		{
			object name = replaceInHistory ? "replace" : "";
			object obj = null;
			object obj2 = this.NativeHtmlDocument2.Open("text/html", name, obj, obj);
			UnsafeNativeMethods.IHTMLDocument ihtmldocument = obj2 as UnsafeNativeMethods.IHTMLDocument;
			if (ihtmldocument == null)
			{
				return null;
			}
			return new HtmlDocument(this.ShimManager, ihtmldocument);
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000BB63C File Offset: 0x000B983C
		public HtmlElement CreateElement(string elementTag)
		{
			UnsafeNativeMethods.IHTMLElement ihtmlelement = this.NativeHtmlDocument2.CreateElement(elementTag);
			if (ihtmlelement == null)
			{
				return null;
			}
			return new HtmlElement(this.ShimManager, ihtmlelement);
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000BB668 File Offset: 0x000B9868
		public object InvokeScript(string scriptName, object[] args)
		{
			object result = null;
			NativeMethods.tagDISPPARAMS tagDISPPARAMS = new NativeMethods.tagDISPPARAMS();
			tagDISPPARAMS.rgvarg = IntPtr.Zero;
			try
			{
				UnsafeNativeMethods.IDispatch dispatch = this.NativeHtmlDocument2.GetScript() as UnsafeNativeMethods.IDispatch;
				if (dispatch != null)
				{
					Guid empty = Guid.Empty;
					string[] rgszNames = new string[]
					{
						scriptName
					};
					int[] array = new int[]
					{
						-1
					};
					int idsOfNames = dispatch.GetIDsOfNames(ref empty, rgszNames, 1, SafeNativeMethods.GetThreadLCID(), array);
					if (NativeMethods.Succeeded(idsOfNames) && array[0] != -1)
					{
						if (args != null)
						{
							Array.Reverse(args);
						}
						tagDISPPARAMS.rgvarg = ((args == null) ? IntPtr.Zero : HtmlDocument.ArrayToVARIANTVector(args));
						tagDISPPARAMS.cArgs = ((args == null) ? 0 : args.Length);
						tagDISPPARAMS.rgdispidNamedArgs = IntPtr.Zero;
						tagDISPPARAMS.cNamedArgs = 0;
						object[] array2 = new object[1];
						if (dispatch.Invoke(array[0], ref empty, SafeNativeMethods.GetThreadLCID(), 1, tagDISPPARAMS, array2, new NativeMethods.tagEXCEPINFO(), null) == 0)
						{
							result = array2[0];
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
			finally
			{
				if (tagDISPPARAMS.rgvarg != IntPtr.Zero)
				{
					HtmlDocument.FreeVARIANTVector(tagDISPPARAMS.rgvarg, args.Length);
				}
			}
			return result;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000BB7A0 File Offset: 0x000B99A0
		public object InvokeScript(string scriptName)
		{
			return this.InvokeScript(scriptName, null);
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000BB7AC File Offset: 0x000B99AC
		public void AttachEventHandler(string eventName, EventHandler eventHandler)
		{
			HtmlDocument.HtmlDocumentShim documentShim = this.DocumentShim;
			if (documentShim != null)
			{
				documentShim.AttachEventHandler(eventName, eventHandler);
			}
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000BB7CC File Offset: 0x000B99CC
		public void DetachEventHandler(string eventName, EventHandler eventHandler)
		{
			HtmlDocument.HtmlDocumentShim documentShim = this.DocumentShim;
			if (documentShim != null)
			{
				documentShim.DetachEventHandler(eventName, eventHandler);
			}
		}

		// Token: 0x140001C5 RID: 453
		// (add) Token: 0x0600286C RID: 10348 RVA: 0x000BB7EB File Offset: 0x000B99EB
		// (remove) Token: 0x0600286D RID: 10349 RVA: 0x000BB7FE File Offset: 0x000B99FE
		public event HtmlElementEventHandler Click
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventClick, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventClick, value);
			}
		}

		// Token: 0x140001C6 RID: 454
		// (add) Token: 0x0600286E RID: 10350 RVA: 0x000BB811 File Offset: 0x000B9A11
		// (remove) Token: 0x0600286F RID: 10351 RVA: 0x000BB824 File Offset: 0x000B9A24
		public event HtmlElementEventHandler ContextMenuShowing
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventContextMenuShowing, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventContextMenuShowing, value);
			}
		}

		// Token: 0x140001C7 RID: 455
		// (add) Token: 0x06002870 RID: 10352 RVA: 0x000BB837 File Offset: 0x000B9A37
		// (remove) Token: 0x06002871 RID: 10353 RVA: 0x000BB84A File Offset: 0x000B9A4A
		public event HtmlElementEventHandler Focusing
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventFocusing, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventFocusing, value);
			}
		}

		// Token: 0x140001C8 RID: 456
		// (add) Token: 0x06002872 RID: 10354 RVA: 0x000BB85D File Offset: 0x000B9A5D
		// (remove) Token: 0x06002873 RID: 10355 RVA: 0x000BB870 File Offset: 0x000B9A70
		public event HtmlElementEventHandler LosingFocus
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventLosingFocus, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventLosingFocus, value);
			}
		}

		// Token: 0x140001C9 RID: 457
		// (add) Token: 0x06002874 RID: 10356 RVA: 0x000BB883 File Offset: 0x000B9A83
		// (remove) Token: 0x06002875 RID: 10357 RVA: 0x000BB896 File Offset: 0x000B9A96
		public event HtmlElementEventHandler MouseDown
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventMouseDown, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventMouseDown, value);
			}
		}

		// Token: 0x140001CA RID: 458
		// (add) Token: 0x06002876 RID: 10358 RVA: 0x000BB8A9 File Offset: 0x000B9AA9
		// (remove) Token: 0x06002877 RID: 10359 RVA: 0x000BB8BC File Offset: 0x000B9ABC
		public event HtmlElementEventHandler MouseLeave
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventMouseLeave, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventMouseLeave, value);
			}
		}

		// Token: 0x140001CB RID: 459
		// (add) Token: 0x06002878 RID: 10360 RVA: 0x000BB8CF File Offset: 0x000B9ACF
		// (remove) Token: 0x06002879 RID: 10361 RVA: 0x000BB8E2 File Offset: 0x000B9AE2
		public event HtmlElementEventHandler MouseMove
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventMouseMove, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventMouseMove, value);
			}
		}

		// Token: 0x140001CC RID: 460
		// (add) Token: 0x0600287A RID: 10362 RVA: 0x000BB8F5 File Offset: 0x000B9AF5
		// (remove) Token: 0x0600287B RID: 10363 RVA: 0x000BB908 File Offset: 0x000B9B08
		public event HtmlElementEventHandler MouseOver
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventMouseOver, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventMouseOver, value);
			}
		}

		// Token: 0x140001CD RID: 461
		// (add) Token: 0x0600287C RID: 10364 RVA: 0x000BB91B File Offset: 0x000B9B1B
		// (remove) Token: 0x0600287D RID: 10365 RVA: 0x000BB92E File Offset: 0x000B9B2E
		public event HtmlElementEventHandler MouseUp
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventMouseUp, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventMouseUp, value);
			}
		}

		// Token: 0x140001CE RID: 462
		// (add) Token: 0x0600287E RID: 10366 RVA: 0x000BB941 File Offset: 0x000B9B41
		// (remove) Token: 0x0600287F RID: 10367 RVA: 0x000BB954 File Offset: 0x000B9B54
		public event HtmlElementEventHandler Stop
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventStop, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventStop, value);
			}
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000BB968 File Offset: 0x000B9B68
		internal unsafe static IntPtr ArrayToVARIANTVector(object[] args)
		{
			int num = args.Length;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num * HtmlDocument.VariantSize);
			byte* ptr = (byte*)((void*)intPtr);
			for (int i = 0; i < num; i++)
			{
				Marshal.GetNativeVariantForObject(args[i], (IntPtr)((void*)(ptr + HtmlDocument.VariantSize * i)));
			}
			return intPtr;
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000BB9B0 File Offset: 0x000B9BB0
		internal unsafe static void FreeVARIANTVector(IntPtr mem, int len)
		{
			byte* ptr = (byte*)((void*)mem);
			for (int i = 0; i < len; i++)
			{
				SafeNativeMethods.VariantClear(new HandleRef(null, (IntPtr)((void*)(ptr + HtmlDocument.VariantSize * i))));
			}
			Marshal.FreeCoTaskMem(mem);
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000BB9F0 File Offset: 0x000B9BF0
		private Color ColorFromObject(object oColor)
		{
			try
			{
				if (oColor is string)
				{
					string text = oColor as string;
					int num = text.IndexOf('#');
					if (num >= 0)
					{
						string s = text.Substring(num + 1);
						return Color.FromArgb(255, Color.FromArgb(int.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture)));
					}
					return Color.FromName(text);
				}
				else if (oColor is int)
				{
					return Color.FromArgb(255, Color.FromArgb((int)oColor));
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
			return Color.Empty;
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000BBA98 File Offset: 0x000B9C98
		public static bool operator ==(HtmlDocument left, HtmlDocument right)
		{
			if (left == null != (right == null))
			{
				return false;
			}
			if (left == null)
			{
				return true;
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			bool result;
			try
			{
				intPtr = Marshal.GetIUnknownForObject(left.NativeHtmlDocument2);
				intPtr2 = Marshal.GetIUnknownForObject(right.NativeHtmlDocument2);
				result = (intPtr == intPtr2);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.Release(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.Release(intPtr2);
				}
			}
			return result;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000BBB20 File Offset: 0x000B9D20
		public static bool operator !=(HtmlDocument left, HtmlDocument right)
		{
			return !(left == right);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000BBB2C File Offset: 0x000B9D2C
		public override int GetHashCode()
		{
			if (this.htmlDocument2 != null)
			{
				return this.htmlDocument2.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000BBB43 File Offset: 0x000B9D43
		public override bool Equals(object obj)
		{
			return this == (HtmlDocument)obj;
		}

		// Token: 0x040010A8 RID: 4264
		internal static object EventClick = new object();

		// Token: 0x040010A9 RID: 4265
		internal static object EventContextMenuShowing = new object();

		// Token: 0x040010AA RID: 4266
		internal static object EventFocusing = new object();

		// Token: 0x040010AB RID: 4267
		internal static object EventLosingFocus = new object();

		// Token: 0x040010AC RID: 4268
		internal static object EventMouseDown = new object();

		// Token: 0x040010AD RID: 4269
		internal static object EventMouseLeave = new object();

		// Token: 0x040010AE RID: 4270
		internal static object EventMouseMove = new object();

		// Token: 0x040010AF RID: 4271
		internal static object EventMouseOver = new object();

		// Token: 0x040010B0 RID: 4272
		internal static object EventMouseUp = new object();

		// Token: 0x040010B1 RID: 4273
		internal static object EventStop = new object();

		// Token: 0x040010B2 RID: 4274
		private UnsafeNativeMethods.IHTMLDocument2 htmlDocument2;

		// Token: 0x040010B3 RID: 4275
		private HtmlShimManager shimManager;

		// Token: 0x040010B4 RID: 4276
		private static readonly int VariantSize = (int)Marshal.OffsetOf(typeof(HtmlDocument.FindSizeOfVariant), "b");

		// Token: 0x020006A5 RID: 1701
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct FindSizeOfVariant
		{
			// Token: 0x04003AE6 RID: 15078
			[MarshalAs(UnmanagedType.Struct)]
			public object var;

			// Token: 0x04003AE7 RID: 15079
			public byte b;
		}

		// Token: 0x020006A6 RID: 1702
		internal class HtmlDocumentShim : HtmlShim
		{
			// Token: 0x060067F6 RID: 26614 RVA: 0x00184240 File Offset: 0x00182440
			internal HtmlDocumentShim(HtmlDocument htmlDocument)
			{
				this.htmlDocument = htmlDocument;
				if (this.htmlDocument != null)
				{
					HtmlWindow window = htmlDocument.Window;
					if (window != null)
					{
						this.associatedWindow = window.NativeHtmlWindow;
					}
				}
			}

			// Token: 0x1700168C RID: 5772
			// (get) Token: 0x060067F7 RID: 26615 RVA: 0x00184284 File Offset: 0x00182484
			public override UnsafeNativeMethods.IHTMLWindow2 AssociatedWindow
			{
				get
				{
					return this.associatedWindow;
				}
			}

			// Token: 0x1700168D RID: 5773
			// (get) Token: 0x060067F8 RID: 26616 RVA: 0x0018428C File Offset: 0x0018248C
			public UnsafeNativeMethods.IHTMLDocument2 NativeHtmlDocument2
			{
				get
				{
					return this.htmlDocument.NativeHtmlDocument2;
				}
			}

			// Token: 0x1700168E RID: 5774
			// (get) Token: 0x060067F9 RID: 26617 RVA: 0x00184299 File Offset: 0x00182499
			internal HtmlDocument Document
			{
				get
				{
					return this.htmlDocument;
				}
			}

			// Token: 0x060067FA RID: 26618 RVA: 0x001842A4 File Offset: 0x001824A4
			public override void AttachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy pdisp = base.AddEventProxy(eventName, eventHandler);
				bool flag = ((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).AttachEvent(eventName, pdisp);
			}

			// Token: 0x060067FB RID: 26619 RVA: 0x001842D0 File Offset: 0x001824D0
			public override void DetachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy htmlToClrEventProxy = base.RemoveEventProxy(eventHandler);
				if (htmlToClrEventProxy != null)
				{
					((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).DetachEvent(eventName, htmlToClrEventProxy);
				}
			}

			// Token: 0x060067FC RID: 26620 RVA: 0x001842FC File Offset: 0x001824FC
			public override void ConnectToEvents()
			{
				if (this.cookie == null || !this.cookie.Connected)
				{
					this.cookie = new AxHost.ConnectionPointCookie(this.NativeHtmlDocument2, new HtmlDocument.HTMLDocumentEvents2(this.htmlDocument), typeof(UnsafeNativeMethods.DHTMLDocumentEvents2), false);
					if (!this.cookie.Connected)
					{
						this.cookie = null;
					}
				}
			}

			// Token: 0x060067FD RID: 26621 RVA: 0x00184359 File Offset: 0x00182559
			public override void DisconnectFromEvents()
			{
				if (this.cookie != null)
				{
					this.cookie.Disconnect();
					this.cookie = null;
				}
			}

			// Token: 0x060067FE RID: 26622 RVA: 0x00184375 File Offset: 0x00182575
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (disposing)
				{
					if (this.htmlDocument != null)
					{
						Marshal.FinalReleaseComObject(this.htmlDocument.NativeHtmlDocument2);
					}
					this.htmlDocument = null;
				}
			}

			// Token: 0x060067FF RID: 26623 RVA: 0x00184299 File Offset: 0x00182499
			protected override object GetEventSender()
			{
				return this.htmlDocument;
			}

			// Token: 0x04003AE8 RID: 15080
			private AxHost.ConnectionPointCookie cookie;

			// Token: 0x04003AE9 RID: 15081
			private HtmlDocument htmlDocument;

			// Token: 0x04003AEA RID: 15082
			private UnsafeNativeMethods.IHTMLWindow2 associatedWindow;
		}

		// Token: 0x020006A7 RID: 1703
		[ClassInterface(ClassInterfaceType.None)]
		private class HTMLDocumentEvents2 : StandardOleMarshalObject, UnsafeNativeMethods.DHTMLDocumentEvents2
		{
			// Token: 0x06006800 RID: 26624 RVA: 0x001843A7 File Offset: 0x001825A7
			public HTMLDocumentEvents2(HtmlDocument htmlDocument)
			{
				this.parent = htmlDocument;
			}

			// Token: 0x06006801 RID: 26625 RVA: 0x001843B6 File Offset: 0x001825B6
			private void FireEvent(object key, EventArgs e)
			{
				if (this.parent != null)
				{
					this.parent.DocumentShim.FireEvent(key, e);
				}
			}

			// Token: 0x06006802 RID: 26626 RVA: 0x001843D8 File Offset: 0x001825D8
			public bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventClick, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006803 RID: 26627 RVA: 0x0018440C File Offset: 0x0018260C
			public bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventContextMenuShowing, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006804 RID: 26628 RVA: 0x00184440 File Offset: 0x00182640
			public void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventFocusing, e);
			}

			// Token: 0x06006805 RID: 26629 RVA: 0x0018446C File Offset: 0x0018266C
			public void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventLosingFocus, e);
			}

			// Token: 0x06006806 RID: 26630 RVA: 0x00184498 File Offset: 0x00182698
			public void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventMouseMove, e);
			}

			// Token: 0x06006807 RID: 26631 RVA: 0x001844C4 File Offset: 0x001826C4
			public void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventMouseDown, e);
			}

			// Token: 0x06006808 RID: 26632 RVA: 0x001844F0 File Offset: 0x001826F0
			public void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventMouseLeave, e);
			}

			// Token: 0x06006809 RID: 26633 RVA: 0x0018451C File Offset: 0x0018271C
			public void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventMouseOver, e);
			}

			// Token: 0x0600680A RID: 26634 RVA: 0x00184548 File Offset: 0x00182748
			public void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventMouseUp, e);
			}

			// Token: 0x0600680B RID: 26635 RVA: 0x00184574 File Offset: 0x00182774
			public bool onstop(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventStop, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600680C RID: 26636 RVA: 0x001845A8 File Offset: 0x001827A8
			public bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600680D RID: 26637 RVA: 0x001845D0 File Offset: 0x001827D0
			public bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600680E RID: 26638 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600680F RID: 26639 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006810 RID: 26640 RVA: 0x001845F8 File Offset: 0x001827F8
			public bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006811 RID: 26641 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006812 RID: 26642 RVA: 0x00184620 File Offset: 0x00182820
			public bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006813 RID: 26643 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006814 RID: 26644 RVA: 0x00184648 File Offset: 0x00182848
			public bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006815 RID: 26645 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006816 RID: 26646 RVA: 0x00184670 File Offset: 0x00182870
			public bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006817 RID: 26647 RVA: 0x00184698 File Offset: 0x00182898
			public bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006818 RID: 26648 RVA: 0x001846C0 File Offset: 0x001828C0
			public bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006819 RID: 26649 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600681A RID: 26650 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600681B RID: 26651 RVA: 0x000072B6 File Offset: 0x000054B6
			public void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600681C RID: 26652 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600681D RID: 26653 RVA: 0x000072B6 File Offset: 0x000054B6
			public void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600681E RID: 26654 RVA: 0x000072B6 File Offset: 0x000054B6
			public void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600681F RID: 26655 RVA: 0x000072B6 File Offset: 0x000054B6
			public void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006820 RID: 26656 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onbeforeeditfocus(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006821 RID: 26657 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onselectionchange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006822 RID: 26658 RVA: 0x001846E8 File Offset: 0x001828E8
			public bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006823 RID: 26659 RVA: 0x00184710 File Offset: 0x00182910
			public bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006824 RID: 26660 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006825 RID: 26661 RVA: 0x000072B6 File Offset: 0x000054B6
			public void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006826 RID: 26662 RVA: 0x00184738 File Offset: 0x00182938
			public bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006827 RID: 26663 RVA: 0x00184760 File Offset: 0x00182960
			public bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x04003AEB RID: 15083
			private HtmlDocument parent;
		}
	}
}
