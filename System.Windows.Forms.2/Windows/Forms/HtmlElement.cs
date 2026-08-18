using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x0200027B RID: 635
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class HtmlElement
	{
		// Token: 0x06002888 RID: 10376 RVA: 0x000BBBE3 File Offset: 0x000B9DE3
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		internal HtmlElement(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLElement element)
		{
			this.htmlElement = element;
			this.shimManager = shimManager;
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x000BBBFC File Offset: 0x000B9DFC
		public HtmlElementCollection All
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection ihtmlelementCollection = this.NativeHtmlElement.GetAll() as UnsafeNativeMethods.IHTMLElementCollection;
				if (ihtmlelementCollection == null)
				{
					return new HtmlElementCollection(this.shimManager);
				}
				return new HtmlElementCollection(this.shimManager, ihtmlelementCollection);
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x0600288A RID: 10378 RVA: 0x000BBC38 File Offset: 0x000B9E38
		public HtmlElementCollection Children
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection ihtmlelementCollection = this.NativeHtmlElement.GetChildren() as UnsafeNativeMethods.IHTMLElementCollection;
				if (ihtmlelementCollection == null)
				{
					return new HtmlElementCollection(this.shimManager);
				}
				return new HtmlElementCollection(this.shimManager, ihtmlelementCollection);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x000BBC71 File Offset: 0x000B9E71
		public bool CanHaveChildren
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).CanHaveChildren();
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x000BBC84 File Offset: 0x000B9E84
		public Rectangle ClientRectangle
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement2 ihtmlelement = (UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement;
				return new Rectangle(ihtmlelement.ClientLeft(), ihtmlelement.ClientTop(), ihtmlelement.ClientWidth(), ihtmlelement.ClientHeight());
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x000BBCBC File Offset: 0x000B9EBC
		public HtmlDocument Document
		{
			get
			{
				UnsafeNativeMethods.IHTMLDocument ihtmldocument = this.NativeHtmlElement.GetDocument() as UnsafeNativeMethods.IHTMLDocument;
				if (ihtmldocument == null)
				{
					return null;
				}
				return new HtmlDocument(this.shimManager, ihtmldocument);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x000BBCEB File Offset: 0x000B9EEB
		// (set) Token: 0x0600288F RID: 10383 RVA: 0x000BBD00 File Offset: 0x000B9F00
		public bool Enabled
		{
			get
			{
				return !((UnsafeNativeMethods.IHTMLElement3)this.NativeHtmlElement).GetDisabled();
			}
			set
			{
				((UnsafeNativeMethods.IHTMLElement3)this.NativeHtmlElement).SetDisabled(!value);
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x000BBD18 File Offset: 0x000B9F18
		private HtmlElement.HtmlElementShim ElementShim
		{
			get
			{
				if (this.ShimManager != null)
				{
					HtmlElement.HtmlElementShim elementShim = this.ShimManager.GetElementShim(this);
					if (elementShim == null)
					{
						this.shimManager.AddElementShim(this);
						elementShim = this.ShimManager.GetElementShim(this);
					}
					return elementShim;
				}
				return null;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x000BBD5C File Offset: 0x000B9F5C
		public HtmlElement FirstChild
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement ihtmlelement = null;
				UnsafeNativeMethods.IHTMLDOMNode ihtmldomnode = this.NativeHtmlElement as UnsafeNativeMethods.IHTMLDOMNode;
				if (ihtmldomnode != null)
				{
					ihtmlelement = (ihtmldomnode.FirstChild() as UnsafeNativeMethods.IHTMLElement);
				}
				if (ihtmlelement == null)
				{
					return null;
				}
				return new HtmlElement(this.shimManager, ihtmlelement);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002892 RID: 10386 RVA: 0x000BBD97 File Offset: 0x000B9F97
		// (set) Token: 0x06002893 RID: 10387 RVA: 0x000BBDA4 File Offset: 0x000B9FA4
		public string Id
		{
			get
			{
				return this.NativeHtmlElement.GetId();
			}
			set
			{
				this.NativeHtmlElement.SetId(value);
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x000BBDB2 File Offset: 0x000B9FB2
		// (set) Token: 0x06002895 RID: 10389 RVA: 0x000BBDC0 File Offset: 0x000B9FC0
		public string InnerHtml
		{
			get
			{
				return this.NativeHtmlElement.GetInnerHTML();
			}
			set
			{
				try
				{
					this.NativeHtmlElement.SetInnerHTML(value);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2146827688)
					{
						throw new NotSupportedException(SR.GetString("HtmlElementPropertyNotSupported"));
					}
					throw;
				}
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x000BBE0C File Offset: 0x000BA00C
		// (set) Token: 0x06002897 RID: 10391 RVA: 0x000BBE1C File Offset: 0x000BA01C
		public string InnerText
		{
			get
			{
				return this.NativeHtmlElement.GetInnerText();
			}
			set
			{
				try
				{
					this.NativeHtmlElement.SetInnerText(value);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2146827688)
					{
						throw new NotSupportedException(SR.GetString("HtmlElementPropertyNotSupported"));
					}
					throw;
				}
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x000BBE68 File Offset: 0x000BA068
		// (set) Token: 0x06002899 RID: 10393 RVA: 0x000BBE75 File Offset: 0x000BA075
		public string Name
		{
			get
			{
				return this.GetAttribute("Name");
			}
			set
			{
				this.SetAttribute("Name", value);
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x000BBE83 File Offset: 0x000BA083
		private UnsafeNativeMethods.IHTMLElement NativeHtmlElement
		{
			get
			{
				return this.htmlElement;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x000BBE8C File Offset: 0x000BA08C
		public HtmlElement NextSibling
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement ihtmlelement = null;
				UnsafeNativeMethods.IHTMLDOMNode ihtmldomnode = this.NativeHtmlElement as UnsafeNativeMethods.IHTMLDOMNode;
				if (ihtmldomnode != null)
				{
					ihtmlelement = (ihtmldomnode.NextSibling() as UnsafeNativeMethods.IHTMLElement);
				}
				if (ihtmlelement == null)
				{
					return null;
				}
				return new HtmlElement(this.shimManager, ihtmlelement);
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x000BBEC7 File Offset: 0x000BA0C7
		public Rectangle OffsetRectangle
		{
			get
			{
				return new Rectangle(this.NativeHtmlElement.GetOffsetLeft(), this.NativeHtmlElement.GetOffsetTop(), this.NativeHtmlElement.GetOffsetWidth(), this.NativeHtmlElement.GetOffsetHeight());
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x000BBEFC File Offset: 0x000BA0FC
		public HtmlElement OffsetParent
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement offsetParent = this.NativeHtmlElement.GetOffsetParent();
				if (offsetParent == null)
				{
					return null;
				}
				return new HtmlElement(this.shimManager, offsetParent);
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x000BBF26 File Offset: 0x000BA126
		// (set) Token: 0x0600289F RID: 10399 RVA: 0x000BBF34 File Offset: 0x000BA134
		public string OuterHtml
		{
			get
			{
				return this.NativeHtmlElement.GetOuterHTML();
			}
			set
			{
				try
				{
					this.NativeHtmlElement.SetOuterHTML(value);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2146827688)
					{
						throw new NotSupportedException(SR.GetString("HtmlElementPropertyNotSupported"));
					}
					throw;
				}
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060028A0 RID: 10400 RVA: 0x000BBF80 File Offset: 0x000BA180
		// (set) Token: 0x060028A1 RID: 10401 RVA: 0x000BBF90 File Offset: 0x000BA190
		public string OuterText
		{
			get
			{
				return this.NativeHtmlElement.GetOuterText();
			}
			set
			{
				try
				{
					this.NativeHtmlElement.SetOuterText(value);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2146827688)
					{
						throw new NotSupportedException(SR.GetString("HtmlElementPropertyNotSupported"));
					}
					throw;
				}
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x000BBFDC File Offset: 0x000BA1DC
		public HtmlElement Parent
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement parentElement = this.NativeHtmlElement.GetParentElement();
				if (parentElement == null)
				{
					return null;
				}
				return new HtmlElement(this.shimManager, parentElement);
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x000BC008 File Offset: 0x000BA208
		public Rectangle ScrollRectangle
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement2 ihtmlelement = (UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement;
				return new Rectangle(ihtmlelement.GetScrollLeft(), ihtmlelement.GetScrollTop(), ihtmlelement.GetScrollWidth(), ihtmlelement.GetScrollHeight());
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x000BC03E File Offset: 0x000BA23E
		// (set) Token: 0x060028A5 RID: 10405 RVA: 0x000BC050 File Offset: 0x000BA250
		public int ScrollLeft
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).GetScrollLeft();
			}
			set
			{
				((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).SetScrollLeft(value);
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x000BC063 File Offset: 0x000BA263
		// (set) Token: 0x060028A7 RID: 10407 RVA: 0x000BC075 File Offset: 0x000BA275
		public int ScrollTop
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).GetScrollTop();
			}
			set
			{
				((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).SetScrollTop(value);
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x000BC088 File Offset: 0x000BA288
		private HtmlShimManager ShimManager
		{
			get
			{
				return this.shimManager;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x000BC090 File Offset: 0x000BA290
		// (set) Token: 0x060028AA RID: 10410 RVA: 0x000BC0A2 File Offset: 0x000BA2A2
		public string Style
		{
			get
			{
				return this.NativeHtmlElement.GetStyle().GetCssText();
			}
			set
			{
				this.NativeHtmlElement.GetStyle().SetCssText(value);
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x000BC0B5 File Offset: 0x000BA2B5
		public string TagName
		{
			get
			{
				return this.NativeHtmlElement.GetTagName();
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x000BC0C2 File Offset: 0x000BA2C2
		// (set) Token: 0x060028AD RID: 10413 RVA: 0x000BC0D4 File Offset: 0x000BA2D4
		public short TabIndex
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).GetTabIndex();
			}
			set
			{
				((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).SetTabIndex((int)value);
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000BC0E7 File Offset: 0x000BA2E7
		public object DomElement
		{
			get
			{
				return this.NativeHtmlElement;
			}
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000BC0EF File Offset: 0x000BA2EF
		public HtmlElement AppendChild(HtmlElement newElement)
		{
			return this.InsertAdjacentElement(HtmlElementInsertionOrientation.BeforeEnd, newElement);
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000BC0F9 File Offset: 0x000BA2F9
		public void AttachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.ElementShim.AttachEventHandler(eventName, eventHandler);
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000BC108 File Offset: 0x000BA308
		public void DetachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.ElementShim.DetachEventHandler(eventName, eventHandler);
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000BC118 File Offset: 0x000BA318
		public void Focus()
		{
			try
			{
				((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).Focus();
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2146826178)
				{
					throw new NotSupportedException(SR.GetString("HtmlElementMethodNotSupported"));
				}
				throw;
			}
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000BC168 File Offset: 0x000BA368
		public string GetAttribute(string attributeName)
		{
			object attribute = this.NativeHtmlElement.GetAttribute(attributeName, 0);
			if (attribute != null)
			{
				return attribute.ToString();
			}
			return "";
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000BC194 File Offset: 0x000BA394
		public HtmlElementCollection GetElementsByTagName(string tagName)
		{
			UnsafeNativeMethods.IHTMLElementCollection elementsByTagName = ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).GetElementsByTagName(tagName);
			if (elementsByTagName == null)
			{
				return new HtmlElementCollection(this.shimManager);
			}
			return new HtmlElementCollection(this.shimManager, elementsByTagName);
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000BC1D0 File Offset: 0x000BA3D0
		public HtmlElement InsertAdjacentElement(HtmlElementInsertionOrientation orient, HtmlElement newElement)
		{
			UnsafeNativeMethods.IHTMLElement ihtmlelement = ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).InsertAdjacentElement(orient.ToString(), (UnsafeNativeMethods.IHTMLElement)newElement.DomElement);
			if (ihtmlelement == null)
			{
				return null;
			}
			return new HtmlElement(this.shimManager, ihtmlelement);
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000BC217 File Offset: 0x000BA417
		public object InvokeMember(string methodName)
		{
			return this.InvokeMember(methodName, null);
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000BC224 File Offset: 0x000BA424
		public object InvokeMember(string methodName, params object[] parameter)
		{
			object result = null;
			NativeMethods.tagDISPPARAMS tagDISPPARAMS = new NativeMethods.tagDISPPARAMS();
			tagDISPPARAMS.rgvarg = IntPtr.Zero;
			try
			{
				UnsafeNativeMethods.IDispatch dispatch = this.NativeHtmlElement as UnsafeNativeMethods.IDispatch;
				if (dispatch != null)
				{
					Guid empty = Guid.Empty;
					string[] rgszNames = new string[]
					{
						methodName
					};
					int[] array = new int[]
					{
						-1
					};
					int idsOfNames = dispatch.GetIDsOfNames(ref empty, rgszNames, 1, SafeNativeMethods.GetThreadLCID(), array);
					if (NativeMethods.Succeeded(idsOfNames) && array[0] != -1)
					{
						if (parameter != null)
						{
							Array.Reverse(parameter);
						}
						tagDISPPARAMS.rgvarg = ((parameter == null) ? IntPtr.Zero : HtmlDocument.ArrayToVARIANTVector(parameter));
						tagDISPPARAMS.cArgs = ((parameter == null) ? 0 : parameter.Length);
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
					HtmlDocument.FreeVARIANTVector(tagDISPPARAMS.rgvarg, parameter.Length);
				}
			}
			return result;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000BC358 File Offset: 0x000BA558
		public void RemoveFocus()
		{
			((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).Blur();
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000BC36A File Offset: 0x000BA56A
		public void RaiseEvent(string eventName)
		{
			((UnsafeNativeMethods.IHTMLElement3)this.NativeHtmlElement).FireEvent(eventName, IntPtr.Zero);
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000BC383 File Offset: 0x000BA583
		public void ScrollIntoView(bool alignWithTop)
		{
			this.NativeHtmlElement.ScrollIntoView(alignWithTop);
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000BC398 File Offset: 0x000BA598
		public void SetAttribute(string attributeName, string value)
		{
			try
			{
				this.NativeHtmlElement.SetAttribute(attributeName, value, 0);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147352567)
				{
					throw new NotSupportedException(SR.GetString("HtmlElementAttributeNotSupported"));
				}
				throw;
			}
		}

		// Token: 0x140001CF RID: 463
		// (add) Token: 0x060028BC RID: 10428 RVA: 0x000BC3E8 File Offset: 0x000BA5E8
		// (remove) Token: 0x060028BD RID: 10429 RVA: 0x000BC3FB File Offset: 0x000BA5FB
		public event HtmlElementEventHandler Click
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventClick, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventClick, value);
			}
		}

		// Token: 0x140001D0 RID: 464
		// (add) Token: 0x060028BE RID: 10430 RVA: 0x000BC40E File Offset: 0x000BA60E
		// (remove) Token: 0x060028BF RID: 10431 RVA: 0x000BC421 File Offset: 0x000BA621
		public event HtmlElementEventHandler DoubleClick
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventDoubleClick, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventDoubleClick, value);
			}
		}

		// Token: 0x140001D1 RID: 465
		// (add) Token: 0x060028C0 RID: 10432 RVA: 0x000BC434 File Offset: 0x000BA634
		// (remove) Token: 0x060028C1 RID: 10433 RVA: 0x000BC447 File Offset: 0x000BA647
		public event HtmlElementEventHandler Drag
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventDrag, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventDrag, value);
			}
		}

		// Token: 0x140001D2 RID: 466
		// (add) Token: 0x060028C2 RID: 10434 RVA: 0x000BC45A File Offset: 0x000BA65A
		// (remove) Token: 0x060028C3 RID: 10435 RVA: 0x000BC46D File Offset: 0x000BA66D
		public event HtmlElementEventHandler DragEnd
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventDragEnd, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventDragEnd, value);
			}
		}

		// Token: 0x140001D3 RID: 467
		// (add) Token: 0x060028C4 RID: 10436 RVA: 0x000BC480 File Offset: 0x000BA680
		// (remove) Token: 0x060028C5 RID: 10437 RVA: 0x000BC493 File Offset: 0x000BA693
		public event HtmlElementEventHandler DragLeave
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventDragLeave, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventDragLeave, value);
			}
		}

		// Token: 0x140001D4 RID: 468
		// (add) Token: 0x060028C6 RID: 10438 RVA: 0x000BC4A6 File Offset: 0x000BA6A6
		// (remove) Token: 0x060028C7 RID: 10439 RVA: 0x000BC4B9 File Offset: 0x000BA6B9
		public event HtmlElementEventHandler DragOver
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventDragOver, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventDragOver, value);
			}
		}

		// Token: 0x140001D5 RID: 469
		// (add) Token: 0x060028C8 RID: 10440 RVA: 0x000BC4CC File Offset: 0x000BA6CC
		// (remove) Token: 0x060028C9 RID: 10441 RVA: 0x000BC4DF File Offset: 0x000BA6DF
		public event HtmlElementEventHandler Focusing
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventFocusing, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventFocusing, value);
			}
		}

		// Token: 0x140001D6 RID: 470
		// (add) Token: 0x060028CA RID: 10442 RVA: 0x000BC4F2 File Offset: 0x000BA6F2
		// (remove) Token: 0x060028CB RID: 10443 RVA: 0x000BC505 File Offset: 0x000BA705
		public event HtmlElementEventHandler GotFocus
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventGotFocus, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventGotFocus, value);
			}
		}

		// Token: 0x140001D7 RID: 471
		// (add) Token: 0x060028CC RID: 10444 RVA: 0x000BC518 File Offset: 0x000BA718
		// (remove) Token: 0x060028CD RID: 10445 RVA: 0x000BC52B File Offset: 0x000BA72B
		public event HtmlElementEventHandler LosingFocus
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventLosingFocus, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventLosingFocus, value);
			}
		}

		// Token: 0x140001D8 RID: 472
		// (add) Token: 0x060028CE RID: 10446 RVA: 0x000BC53E File Offset: 0x000BA73E
		// (remove) Token: 0x060028CF RID: 10447 RVA: 0x000BC551 File Offset: 0x000BA751
		public event HtmlElementEventHandler LostFocus
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventLostFocus, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventLostFocus, value);
			}
		}

		// Token: 0x140001D9 RID: 473
		// (add) Token: 0x060028D0 RID: 10448 RVA: 0x000BC564 File Offset: 0x000BA764
		// (remove) Token: 0x060028D1 RID: 10449 RVA: 0x000BC577 File Offset: 0x000BA777
		public event HtmlElementEventHandler KeyDown
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventKeyDown, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventKeyDown, value);
			}
		}

		// Token: 0x140001DA RID: 474
		// (add) Token: 0x060028D2 RID: 10450 RVA: 0x000BC58A File Offset: 0x000BA78A
		// (remove) Token: 0x060028D3 RID: 10451 RVA: 0x000BC59D File Offset: 0x000BA79D
		public event HtmlElementEventHandler KeyPress
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventKeyPress, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventKeyPress, value);
			}
		}

		// Token: 0x140001DB RID: 475
		// (add) Token: 0x060028D4 RID: 10452 RVA: 0x000BC5B0 File Offset: 0x000BA7B0
		// (remove) Token: 0x060028D5 RID: 10453 RVA: 0x000BC5C3 File Offset: 0x000BA7C3
		public event HtmlElementEventHandler KeyUp
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventKeyUp, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventKeyUp, value);
			}
		}

		// Token: 0x140001DC RID: 476
		// (add) Token: 0x060028D6 RID: 10454 RVA: 0x000BC5D6 File Offset: 0x000BA7D6
		// (remove) Token: 0x060028D7 RID: 10455 RVA: 0x000BC5E9 File Offset: 0x000BA7E9
		public event HtmlElementEventHandler MouseMove
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseMove, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseMove, value);
			}
		}

		// Token: 0x140001DD RID: 477
		// (add) Token: 0x060028D8 RID: 10456 RVA: 0x000BC5FC File Offset: 0x000BA7FC
		// (remove) Token: 0x060028D9 RID: 10457 RVA: 0x000BC60F File Offset: 0x000BA80F
		public event HtmlElementEventHandler MouseDown
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseDown, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseDown, value);
			}
		}

		// Token: 0x140001DE RID: 478
		// (add) Token: 0x060028DA RID: 10458 RVA: 0x000BC622 File Offset: 0x000BA822
		// (remove) Token: 0x060028DB RID: 10459 RVA: 0x000BC635 File Offset: 0x000BA835
		public event HtmlElementEventHandler MouseOver
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseOver, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseOver, value);
			}
		}

		// Token: 0x140001DF RID: 479
		// (add) Token: 0x060028DC RID: 10460 RVA: 0x000BC648 File Offset: 0x000BA848
		// (remove) Token: 0x060028DD RID: 10461 RVA: 0x000BC65B File Offset: 0x000BA85B
		public event HtmlElementEventHandler MouseUp
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseUp, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseUp, value);
			}
		}

		// Token: 0x140001E0 RID: 480
		// (add) Token: 0x060028DE RID: 10462 RVA: 0x000BC66E File Offset: 0x000BA86E
		// (remove) Token: 0x060028DF RID: 10463 RVA: 0x000BC681 File Offset: 0x000BA881
		public event HtmlElementEventHandler MouseEnter
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseEnter, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseEnter, value);
			}
		}

		// Token: 0x140001E1 RID: 481
		// (add) Token: 0x060028E0 RID: 10464 RVA: 0x000BC694 File Offset: 0x000BA894
		// (remove) Token: 0x060028E1 RID: 10465 RVA: 0x000BC6A7 File Offset: 0x000BA8A7
		public event HtmlElementEventHandler MouseLeave
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseLeave, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseLeave, value);
			}
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000BC6BC File Offset: 0x000BA8BC
		public static bool operator ==(HtmlElement left, HtmlElement right)
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
				intPtr = Marshal.GetIUnknownForObject(left.NativeHtmlElement);
				intPtr2 = Marshal.GetIUnknownForObject(right.NativeHtmlElement);
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

		// Token: 0x060028E3 RID: 10467 RVA: 0x000BC744 File Offset: 0x000BA944
		public static bool operator !=(HtmlElement left, HtmlElement right)
		{
			return !(left == right);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000BC750 File Offset: 0x000BA950
		public override int GetHashCode()
		{
			if (this.htmlElement != null)
			{
				return this.htmlElement.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000BC767 File Offset: 0x000BA967
		public override bool Equals(object obj)
		{
			return this == obj as HtmlElement;
		}

		// Token: 0x040010B5 RID: 4277
		internal static readonly object EventClick = new object();

		// Token: 0x040010B6 RID: 4278
		internal static readonly object EventDoubleClick = new object();

		// Token: 0x040010B7 RID: 4279
		internal static readonly object EventDrag = new object();

		// Token: 0x040010B8 RID: 4280
		internal static readonly object EventDragEnd = new object();

		// Token: 0x040010B9 RID: 4281
		internal static readonly object EventDragLeave = new object();

		// Token: 0x040010BA RID: 4282
		internal static readonly object EventDragOver = new object();

		// Token: 0x040010BB RID: 4283
		internal static readonly object EventFocusing = new object();

		// Token: 0x040010BC RID: 4284
		internal static readonly object EventGotFocus = new object();

		// Token: 0x040010BD RID: 4285
		internal static readonly object EventLosingFocus = new object();

		// Token: 0x040010BE RID: 4286
		internal static readonly object EventLostFocus = new object();

		// Token: 0x040010BF RID: 4287
		internal static readonly object EventKeyDown = new object();

		// Token: 0x040010C0 RID: 4288
		internal static readonly object EventKeyPress = new object();

		// Token: 0x040010C1 RID: 4289
		internal static readonly object EventKeyUp = new object();

		// Token: 0x040010C2 RID: 4290
		internal static readonly object EventMouseDown = new object();

		// Token: 0x040010C3 RID: 4291
		internal static readonly object EventMouseEnter = new object();

		// Token: 0x040010C4 RID: 4292
		internal static readonly object EventMouseLeave = new object();

		// Token: 0x040010C5 RID: 4293
		internal static readonly object EventMouseMove = new object();

		// Token: 0x040010C6 RID: 4294
		internal static readonly object EventMouseOver = new object();

		// Token: 0x040010C7 RID: 4295
		internal static readonly object EventMouseUp = new object();

		// Token: 0x040010C8 RID: 4296
		private UnsafeNativeMethods.IHTMLElement htmlElement;

		// Token: 0x040010C9 RID: 4297
		private HtmlShimManager shimManager;

		// Token: 0x020006A8 RID: 1704
		[ClassInterface(ClassInterfaceType.None)]
		private class HTMLElementEvents2 : StandardOleMarshalObject, UnsafeNativeMethods.DHTMLElementEvents2, UnsafeNativeMethods.DHTMLAnchorEvents2, UnsafeNativeMethods.DHTMLAreaEvents2, UnsafeNativeMethods.DHTMLButtonElementEvents2, UnsafeNativeMethods.DHTMLControlElementEvents2, UnsafeNativeMethods.DHTMLFormElementEvents2, UnsafeNativeMethods.DHTMLFrameSiteEvents2, UnsafeNativeMethods.DHTMLImgEvents2, UnsafeNativeMethods.DHTMLInputFileElementEvents2, UnsafeNativeMethods.DHTMLInputImageEvents2, UnsafeNativeMethods.DHTMLInputTextElementEvents2, UnsafeNativeMethods.DHTMLLabelEvents2, UnsafeNativeMethods.DHTMLLinkElementEvents2, UnsafeNativeMethods.DHTMLMapEvents2, UnsafeNativeMethods.DHTMLMarqueeElementEvents2, UnsafeNativeMethods.DHTMLOptionButtonElementEvents2, UnsafeNativeMethods.DHTMLSelectElementEvents2, UnsafeNativeMethods.DHTMLStyleElementEvents2, UnsafeNativeMethods.DHTMLTableEvents2, UnsafeNativeMethods.DHTMLTextContainerEvents2, UnsafeNativeMethods.DHTMLScriptEvents2
		{
			// Token: 0x06006828 RID: 26664 RVA: 0x00184785 File Offset: 0x00182985
			public HTMLElementEvents2(HtmlElement htmlElement)
			{
				this.parent = htmlElement;
			}

			// Token: 0x06006829 RID: 26665 RVA: 0x00184794 File Offset: 0x00182994
			private void FireEvent(object key, EventArgs e)
			{
				if (this.parent != null)
				{
					this.parent.ElementShim.FireEvent(key, e);
				}
			}

			// Token: 0x0600682A RID: 26666 RVA: 0x001847B8 File Offset: 0x001829B8
			public bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventClick, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600682B RID: 26667 RVA: 0x001847EC File Offset: 0x001829EC
			public bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventDoubleClick, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600682C RID: 26668 RVA: 0x00184820 File Offset: 0x00182A20
			public bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventKeyPress, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600682D RID: 26669 RVA: 0x00184854 File Offset: 0x00182A54
			public void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventKeyDown, e);
			}

			// Token: 0x0600682E RID: 26670 RVA: 0x00184880 File Offset: 0x00182A80
			public void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventKeyUp, e);
			}

			// Token: 0x0600682F RID: 26671 RVA: 0x001848AC File Offset: 0x00182AAC
			public void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseOver, e);
			}

			// Token: 0x06006830 RID: 26672 RVA: 0x001848D8 File Offset: 0x00182AD8
			public void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseMove, e);
			}

			// Token: 0x06006831 RID: 26673 RVA: 0x00184904 File Offset: 0x00182B04
			public void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseDown, e);
			}

			// Token: 0x06006832 RID: 26674 RVA: 0x00184930 File Offset: 0x00182B30
			public void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseUp, e);
			}

			// Token: 0x06006833 RID: 26675 RVA: 0x0018495C File Offset: 0x00182B5C
			public void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseEnter, e);
			}

			// Token: 0x06006834 RID: 26676 RVA: 0x00184988 File Offset: 0x00182B88
			public void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseLeave, e);
			}

			// Token: 0x06006835 RID: 26677 RVA: 0x001849B4 File Offset: 0x00182BB4
			public bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006836 RID: 26678 RVA: 0x001849DC File Offset: 0x00182BDC
			public void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventGotFocus, e);
			}

			// Token: 0x06006837 RID: 26679 RVA: 0x00184A08 File Offset: 0x00182C08
			public bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventDrag, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006838 RID: 26680 RVA: 0x00184A3C File Offset: 0x00182C3C
			public void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventDragEnd, e);
			}

			// Token: 0x06006839 RID: 26681 RVA: 0x00184A68 File Offset: 0x00182C68
			public void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventDragLeave, e);
			}

			// Token: 0x0600683A RID: 26682 RVA: 0x00184A94 File Offset: 0x00182C94
			public bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventDragOver, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600683B RID: 26683 RVA: 0x00184AC8 File Offset: 0x00182CC8
			public void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventFocusing, e);
			}

			// Token: 0x0600683C RID: 26684 RVA: 0x00184AF4 File Offset: 0x00182CF4
			public void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventLosingFocus, e);
			}

			// Token: 0x0600683D RID: 26685 RVA: 0x00184B20 File Offset: 0x00182D20
			public void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventLostFocus, e);
			}

			// Token: 0x0600683E RID: 26686 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600683F RID: 26687 RVA: 0x00184B4C File Offset: 0x00182D4C
			public bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006840 RID: 26688 RVA: 0x00184B74 File Offset: 0x00182D74
			public bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006841 RID: 26689 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006842 RID: 26690 RVA: 0x00184B9C File Offset: 0x00182D9C
			public bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006843 RID: 26691 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006844 RID: 26692 RVA: 0x00184BC4 File Offset: 0x00182DC4
			public bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006845 RID: 26693 RVA: 0x00184BEC File Offset: 0x00182DEC
			public bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006846 RID: 26694 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006847 RID: 26695 RVA: 0x00184C14 File Offset: 0x00182E14
			public bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006848 RID: 26696 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006849 RID: 26697 RVA: 0x000072B6 File Offset: 0x000054B6
			public void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600684A RID: 26698 RVA: 0x000072B6 File Offset: 0x000054B6
			public void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600684B RID: 26699 RVA: 0x000072B6 File Offset: 0x000054B6
			public void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600684C RID: 26700 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600684D RID: 26701 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600684E RID: 26702 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600684F RID: 26703 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006850 RID: 26704 RVA: 0x00184C3C File Offset: 0x00182E3C
			public bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006851 RID: 26705 RVA: 0x00184C64 File Offset: 0x00182E64
			public bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006852 RID: 26706 RVA: 0x00184C8C File Offset: 0x00182E8C
			public bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006853 RID: 26707 RVA: 0x00184CB4 File Offset: 0x00182EB4
			public bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006854 RID: 26708 RVA: 0x00184CDC File Offset: 0x00182EDC
			public bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006855 RID: 26709 RVA: 0x00184D04 File Offset: 0x00182F04
			public bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006856 RID: 26710 RVA: 0x00184D2C File Offset: 0x00182F2C
			public bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006857 RID: 26711 RVA: 0x00184D54 File Offset: 0x00182F54
			public bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006858 RID: 26712 RVA: 0x00184D7C File Offset: 0x00182F7C
			public bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006859 RID: 26713 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600685A RID: 26714 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600685B RID: 26715 RVA: 0x000072B6 File Offset: 0x000054B6
			public void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600685C RID: 26716 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600685D RID: 26717 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600685E RID: 26718 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600685F RID: 26719 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006860 RID: 26720 RVA: 0x000072B6 File Offset: 0x000054B6
			public void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006861 RID: 26721 RVA: 0x00184DA4 File Offset: 0x00182FA4
			public bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006862 RID: 26722 RVA: 0x00184DCC File Offset: 0x00182FCC
			public bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006863 RID: 26723 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006864 RID: 26724 RVA: 0x00184DF4 File Offset: 0x00182FF4
			public bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006865 RID: 26725 RVA: 0x00184E1C File Offset: 0x0018301C
			public bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006866 RID: 26726 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006867 RID: 26727 RVA: 0x00184E44 File Offset: 0x00183044
			public bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006868 RID: 26728 RVA: 0x00184E6C File Offset: 0x0018306C
			public bool onchange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006869 RID: 26729 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onselect(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600686A RID: 26730 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onload(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600686B RID: 26731 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onerror(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600686C RID: 26732 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onabort(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600686D RID: 26733 RVA: 0x00184E94 File Offset: 0x00183094
			public bool onsubmit(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600686E RID: 26734 RVA: 0x00184EBC File Offset: 0x001830BC
			public bool onreset(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600686F RID: 26735 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onchange_void(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006870 RID: 26736 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onbounce(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006871 RID: 26737 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onfinish(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006872 RID: 26738 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onstart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x04003AEC RID: 15084
			private HtmlElement parent;
		}

		// Token: 0x020006A9 RID: 1705
		internal class HtmlElementShim : HtmlShim
		{
			// Token: 0x06006873 RID: 26739 RVA: 0x00184EE4 File Offset: 0x001830E4
			public HtmlElementShim(HtmlElement element)
			{
				this.htmlElement = element;
				if (this.htmlElement != null)
				{
					HtmlDocument document = this.htmlElement.Document;
					if (document != null)
					{
						HtmlWindow window = document.Window;
						if (window != null)
						{
							this.associatedWindow = window.NativeHtmlWindow;
						}
					}
				}
			}

			// Token: 0x1700168F RID: 5775
			// (get) Token: 0x06006874 RID: 26740 RVA: 0x00184F3D File Offset: 0x0018313D
			public UnsafeNativeMethods.IHTMLElement NativeHtmlElement
			{
				get
				{
					return this.htmlElement.NativeHtmlElement;
				}
			}

			// Token: 0x17001690 RID: 5776
			// (get) Token: 0x06006875 RID: 26741 RVA: 0x00184F4A File Offset: 0x0018314A
			internal HtmlElement Element
			{
				get
				{
					return this.htmlElement;
				}
			}

			// Token: 0x17001691 RID: 5777
			// (get) Token: 0x06006876 RID: 26742 RVA: 0x00184F52 File Offset: 0x00183152
			public override UnsafeNativeMethods.IHTMLWindow2 AssociatedWindow
			{
				get
				{
					return this.associatedWindow;
				}
			}

			// Token: 0x06006877 RID: 26743 RVA: 0x00184F5C File Offset: 0x0018315C
			public override void AttachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy pdisp = base.AddEventProxy(eventName, eventHandler);
				bool flag = ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).AttachEvent(eventName, pdisp);
			}

			// Token: 0x06006878 RID: 26744 RVA: 0x00184F88 File Offset: 0x00183188
			public override void ConnectToEvents()
			{
				if (this.cookie == null || !this.cookie.Connected)
				{
					int num = 0;
					while (num < HtmlElement.HtmlElementShim.dispInterfaceTypes.Length && this.cookie == null)
					{
						this.cookie = new AxHost.ConnectionPointCookie(this.NativeHtmlElement, new HtmlElement.HTMLElementEvents2(this.htmlElement), HtmlElement.HtmlElementShim.dispInterfaceTypes[num], false);
						if (!this.cookie.Connected)
						{
							this.cookie = null;
						}
						num++;
					}
				}
			}

			// Token: 0x06006879 RID: 26745 RVA: 0x00184FFC File Offset: 0x001831FC
			public override void DetachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy htmlToClrEventProxy = base.RemoveEventProxy(eventHandler);
				if (htmlToClrEventProxy != null)
				{
					((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).DetachEvent(eventName, htmlToClrEventProxy);
				}
			}

			// Token: 0x0600687A RID: 26746 RVA: 0x00185026 File Offset: 0x00183226
			public override void DisconnectFromEvents()
			{
				if (this.cookie != null)
				{
					this.cookie.Disconnect();
					this.cookie = null;
				}
			}

			// Token: 0x0600687B RID: 26747 RVA: 0x00185042 File Offset: 0x00183242
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (this.htmlElement != null)
				{
					Marshal.FinalReleaseComObject(this.htmlElement.NativeHtmlElement);
				}
				this.htmlElement = null;
			}

			// Token: 0x0600687C RID: 26748 RVA: 0x00184F4A File Offset: 0x0018314A
			protected override object GetEventSender()
			{
				return this.htmlElement;
			}

			// Token: 0x04003AED RID: 15085
			private static Type[] dispInterfaceTypes = new Type[]
			{
				typeof(UnsafeNativeMethods.DHTMLElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLAnchorEvents2),
				typeof(UnsafeNativeMethods.DHTMLAreaEvents2),
				typeof(UnsafeNativeMethods.DHTMLButtonElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLControlElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLFormElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLFrameSiteEvents2),
				typeof(UnsafeNativeMethods.DHTMLImgEvents2),
				typeof(UnsafeNativeMethods.DHTMLInputFileElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLInputImageEvents2),
				typeof(UnsafeNativeMethods.DHTMLInputTextElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLLabelEvents2),
				typeof(UnsafeNativeMethods.DHTMLLinkElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLMapEvents2),
				typeof(UnsafeNativeMethods.DHTMLMarqueeElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLOptionButtonElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLSelectElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLStyleElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLTableEvents2),
				typeof(UnsafeNativeMethods.DHTMLTextContainerEvents2),
				typeof(UnsafeNativeMethods.DHTMLScriptEvents2)
			};

			// Token: 0x04003AEE RID: 15086
			private AxHost.ConnectionPointCookie cookie;

			// Token: 0x04003AEF RID: 15087
			private HtmlElement htmlElement;

			// Token: 0x04003AF0 RID: 15088
			private UnsafeNativeMethods.IHTMLWindow2 associatedWindow;
		}
	}
}
