using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000431 RID: 1073
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Url")]
	[DefaultEvent("DocumentCompleted")]
	[Docking(DockingBehavior.AutoDock)]
	[SRDescription("DescriptionWebBrowser")]
	[Designer("System.Windows.Forms.Design.WebBrowserDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class WebBrowser : WebBrowserBase
	{
		// Token: 0x06004A27 RID: 18983 RVA: 0x0013798C File Offset: 0x00135B8C
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public WebBrowser() : base("8856f961-340a-11d0-a96b-00c04fd705a2")
		{
			this.CheckIfCreatedInIE();
			this.webBrowserState = new BitVector32(37);
			this.AllowNavigation = true;
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06004A28 RID: 18984 RVA: 0x001379BE File Offset: 0x00135BBE
		// (set) Token: 0x06004A29 RID: 18985 RVA: 0x001379CD File Offset: 0x00135BCD
		[SRDescription("WebBrowserAllowNavigationDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public bool AllowNavigation
		{
			get
			{
				return this.webBrowserState[64];
			}
			set
			{
				this.webBrowserState[64] = value;
				if (this.webBrowserEvent != null)
				{
					this.webBrowserEvent.AllowNavigation = value;
				}
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06004A2A RID: 18986 RVA: 0x001379F1 File Offset: 0x00135BF1
		// (set) Token: 0x06004A2B RID: 18987 RVA: 0x001379FE File Offset: 0x00135BFE
		[SRDescription("WebBrowserAllowWebBrowserDropDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public bool AllowWebBrowserDrop
		{
			get
			{
				return this.AxIWebBrowser2.RegisterAsDropTarget;
			}
			set
			{
				if (value != this.AllowWebBrowserDrop)
				{
					this.AxIWebBrowser2.RegisterAsDropTarget = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06004A2C RID: 18988 RVA: 0x00137A1B File Offset: 0x00135C1B
		// (set) Token: 0x06004A2D RID: 18989 RVA: 0x00137A28 File Offset: 0x00135C28
		[SRDescription("WebBrowserScriptErrorsSuppressedDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		public bool ScriptErrorsSuppressed
		{
			get
			{
				return this.AxIWebBrowser2.Silent;
			}
			set
			{
				if (value != this.ScriptErrorsSuppressed)
				{
					this.AxIWebBrowser2.Silent = value;
				}
			}
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06004A2E RID: 18990 RVA: 0x00137A3F File Offset: 0x00135C3F
		// (set) Token: 0x06004A2F RID: 18991 RVA: 0x00137A4D File Offset: 0x00135C4D
		[SRDescription("WebBrowserWebBrowserShortcutsEnabledDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public bool WebBrowserShortcutsEnabled
		{
			get
			{
				return this.webBrowserState[1];
			}
			set
			{
				this.webBrowserState[1] = value;
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06004A30 RID: 18992 RVA: 0x00137A5C File Offset: 0x00135C5C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanGoBack
		{
			get
			{
				return this.CanGoBackInternal;
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06004A31 RID: 18993 RVA: 0x00137A64 File Offset: 0x00135C64
		// (set) Token: 0x06004A32 RID: 18994 RVA: 0x00137A72 File Offset: 0x00135C72
		internal bool CanGoBackInternal
		{
			get
			{
				return this.webBrowserState[8];
			}
			set
			{
				if (value != this.CanGoBackInternal)
				{
					this.webBrowserState[8] = value;
					this.OnCanGoBackChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06004A33 RID: 18995 RVA: 0x00137A95 File Offset: 0x00135C95
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanGoForward
		{
			get
			{
				return this.CanGoForwardInternal;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x00137A9D File Offset: 0x00135C9D
		// (set) Token: 0x06004A35 RID: 18997 RVA: 0x00137AAC File Offset: 0x00135CAC
		internal bool CanGoForwardInternal
		{
			get
			{
				return this.webBrowserState[16];
			}
			set
			{
				if (value != this.CanGoForwardInternal)
				{
					this.webBrowserState[16] = value;
					this.OnCanGoForwardChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06004A36 RID: 18998 RVA: 0x00137AD0 File Offset: 0x00135CD0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HtmlDocument Document
		{
			get
			{
				object document = this.AxIWebBrowser2.Document;
				if (document != null)
				{
					UnsafeNativeMethods.IHTMLDocument2 ihtmldocument = null;
					try
					{
						ihtmldocument = (document as UnsafeNativeMethods.IHTMLDocument2);
					}
					catch (InvalidCastException)
					{
					}
					if (ihtmldocument != null)
					{
						UnsafeNativeMethods.IHTMLLocation location = ihtmldocument.GetLocation();
						if (location != null)
						{
							string href = location.GetHref();
							if (!string.IsNullOrEmpty(href))
							{
								Uri url = new Uri(href);
								WebBrowser.EnsureUrlConnectPermission(url);
								return new HtmlDocument(this.ShimManager, ihtmldocument as UnsafeNativeMethods.IHTMLDocument);
							}
						}
					}
				}
				return null;
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x00137B48 File Offset: 0x00135D48
		// (set) Token: 0x06004A38 RID: 19000 RVA: 0x00137BA4 File Offset: 0x00135DA4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Stream DocumentStream
		{
			get
			{
				HtmlDocument document = this.Document;
				if (document == null)
				{
					return null;
				}
				UnsafeNativeMethods.IPersistStreamInit persistStreamInit = document.DomDocument as UnsafeNativeMethods.IPersistStreamInit;
				if (persistStreamInit == null)
				{
					return null;
				}
				MemoryStream memoryStream = new MemoryStream();
				UnsafeNativeMethods.IStream pstm = new UnsafeNativeMethods.ComStreamFromDataStream(memoryStream);
				persistStreamInit.Save(pstm, false);
				return new MemoryStream(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, false);
			}
			set
			{
				this.documentStreamToSetOnLoad = value;
				try
				{
					this.webBrowserState[2] = true;
					this.Url = new Uri("about:blank");
				}
				finally
				{
					this.webBrowserState[2] = false;
				}
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06004A39 RID: 19001 RVA: 0x00137BF8 File Offset: 0x00135DF8
		// (set) Token: 0x06004A3A RID: 19002 RVA: 0x00137C2C File Offset: 0x00135E2C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DocumentText
		{
			get
			{
				Stream documentStream = this.DocumentStream;
				if (documentStream == null)
				{
					return "";
				}
				StreamReader streamReader = new StreamReader(documentStream);
				documentStream.Position = 0L;
				return streamReader.ReadToEnd();
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				MemoryStream memoryStream = new MemoryStream(value.Length);
				StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
				streamWriter.Write(value);
				streamWriter.Flush();
				memoryStream.Position = 0L;
				this.DocumentStream = memoryStream;
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06004A3B RID: 19003 RVA: 0x00137C78 File Offset: 0x00135E78
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DocumentTitle
		{
			get
			{
				HtmlDocument document = this.Document;
				string result;
				if (document == null)
				{
					result = this.AxIWebBrowser2.LocationName;
				}
				else
				{
					UnsafeNativeMethods.IHTMLDocument2 ihtmldocument = document.DomDocument as UnsafeNativeMethods.IHTMLDocument2;
					try
					{
						result = ihtmldocument.GetTitle();
					}
					catch (COMException)
					{
						result = "";
					}
				}
				return result;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06004A3C RID: 19004 RVA: 0x00137CD4 File Offset: 0x00135ED4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DocumentType
		{
			get
			{
				string result = "";
				HtmlDocument document = this.Document;
				if (document != null)
				{
					UnsafeNativeMethods.IHTMLDocument2 ihtmldocument = document.DomDocument as UnsafeNativeMethods.IHTMLDocument2;
					try
					{
						result = ihtmldocument.GetMimeType();
					}
					catch (COMException)
					{
						result = "";
					}
				}
				return result;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06004A3D RID: 19005 RVA: 0x00137D28 File Offset: 0x00135F28
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebBrowserEncryptionLevel EncryptionLevel
		{
			get
			{
				if (this.Document == null)
				{
					this.encryptionLevel = WebBrowserEncryptionLevel.Unknown;
				}
				return this.encryptionLevel;
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x00137D45 File Offset: 0x00135F45
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsBusy
		{
			get
			{
				return !(this.Document == null) && this.AxIWebBrowser2.Busy;
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06004A3F RID: 19007 RVA: 0x00137D62 File Offset: 0x00135F62
		[SRDescription("WebBrowserIsOfflineDescr")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsOffline
		{
			get
			{
				return this.AxIWebBrowser2.Offline;
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06004A40 RID: 19008 RVA: 0x00137D6F File Offset: 0x00135F6F
		// (set) Token: 0x06004A41 RID: 19009 RVA: 0x00137D7D File Offset: 0x00135F7D
		[SRDescription("WebBrowserIsWebBrowserContextMenuEnabledDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public bool IsWebBrowserContextMenuEnabled
		{
			get
			{
				return this.webBrowserState[4];
			}
			set
			{
				this.webBrowserState[4] = value;
			}
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06004A42 RID: 19010 RVA: 0x00137D8C File Offset: 0x00135F8C
		// (set) Token: 0x06004A43 RID: 19011 RVA: 0x00137D94 File Offset: 0x00135F94
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object ObjectForScripting
		{
			get
			{
				return this.objectForScripting;
			}
			set
			{
				if (value != null)
				{
					Type type = value.GetType();
					if (!Marshal.IsTypeVisibleFromCom(type))
					{
						throw new ArgumentException(SR.GetString("WebBrowserObjectForScriptingComVisibleOnly"));
					}
				}
				this.objectForScripting = value;
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06004A44 RID: 19012 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x06004A45 RID: 19013 RVA: 0x0001365E File Offset: 0x0001185E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		// Token: 0x140003BA RID: 954
		// (add) Token: 0x06004A46 RID: 19014 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x06004A47 RID: 19015 RVA: 0x00013670 File Offset: 0x00011870
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnPaddingChangedDescr")]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x00137DCA File Offset: 0x00135FCA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebBrowserReadyState ReadyState
		{
			get
			{
				if (this.Document == null)
				{
					return WebBrowserReadyState.Uninitialized;
				}
				return this.AxIWebBrowser2.ReadyState;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x00137DE7 File Offset: 0x00135FE7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string StatusText
		{
			get
			{
				if (this.Document == null)
				{
					this.statusText = "";
				}
				return this.statusText;
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x00137E08 File Offset: 0x00136008
		// (set) Token: 0x06004A4B RID: 19019 RVA: 0x00137E4C File Offset: 0x0013604C
		[SRDescription("WebBrowserUrlDescr")]
		[Bindable(true)]
		[SRCategory("CatBehavior")]
		[TypeConverter(typeof(WebBrowserUriTypeConverter))]
		[DefaultValue(null)]
		public Uri Url
		{
			get
			{
				string locationURL = this.AxIWebBrowser2.LocationURL;
				if (string.IsNullOrEmpty(locationURL))
				{
					return null;
				}
				Uri result;
				try
				{
					result = new Uri(locationURL);
				}
				catch (UriFormatException)
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (value != null && value.ToString() == "")
				{
					value = null;
				}
				this.PerformNavigateHelper(this.ReadyNavigateToUrl(value), false, null, null, null);
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06004A4C RID: 19020 RVA: 0x00137E80 File Offset: 0x00136080
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Version Version
		{
			get
			{
				string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "mshtml.dll");
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(fileName);
				return new Version(versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart, versionInfo.FilePrivatePart);
			}
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x00137EC4 File Offset: 0x001360C4
		public bool GoBack()
		{
			bool result = true;
			try
			{
				this.AxIWebBrowser2.GoBack();
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x00137F00 File Offset: 0x00136100
		public bool GoForward()
		{
			bool result = true;
			try
			{
				this.AxIWebBrowser2.GoForward();
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x00137F3C File Offset: 0x0013613C
		public void GoHome()
		{
			this.AxIWebBrowser2.GoHome();
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x00137F49 File Offset: 0x00136149
		public void GoSearch()
		{
			this.AxIWebBrowser2.GoSearch();
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x00137F56 File Offset: 0x00136156
		public void Navigate(Uri url)
		{
			this.Url = url;
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x00137F5F File Offset: 0x0013615F
		public void Navigate(string urlString)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(urlString), false, null, null, null);
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x00137F72 File Offset: 0x00136172
		public void Navigate(Uri url, string targetFrameName)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(url), false, targetFrameName, null, null);
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x00137F85 File Offset: 0x00136185
		public void Navigate(string urlString, string targetFrameName)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(urlString), false, targetFrameName, null, null);
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x00137F98 File Offset: 0x00136198
		public void Navigate(Uri url, bool newWindow)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(url), newWindow, null, null, null);
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x00137FAB File Offset: 0x001361AB
		public void Navigate(string urlString, bool newWindow)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(urlString), newWindow, null, null, null);
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x00137FBE File Offset: 0x001361BE
		public void Navigate(Uri url, string targetFrameName, byte[] postData, string additionalHeaders)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(url), false, targetFrameName, postData, additionalHeaders);
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x00137FD2 File Offset: 0x001361D2
		public void Navigate(string urlString, string targetFrameName, byte[] postData, string additionalHeaders)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(urlString), false, targetFrameName, postData, additionalHeaders);
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x00137FE8 File Offset: 0x001361E8
		public void Print()
		{
			IntSecurity.DefaultPrinting.Demand();
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_PRINT, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x00138034 File Offset: 0x00136234
		public override void Refresh()
		{
			try
			{
				if (this.ShouldSerializeDocumentText())
				{
					string documentText = this.DocumentText;
					this.AxIWebBrowser2.Refresh();
					this.DocumentText = documentText;
				}
				else
				{
					this.AxIWebBrowser2.Refresh();
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x00138090 File Offset: 0x00136290
		public void Refresh(WebBrowserRefreshOption opt)
		{
			object obj = opt;
			try
			{
				if (this.ShouldSerializeDocumentText())
				{
					string documentText = this.DocumentText;
					this.AxIWebBrowser2.Refresh2(ref obj);
					this.DocumentText = documentText;
				}
				else
				{
					this.AxIWebBrowser2.Refresh2(ref obj);
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06004A5C RID: 19036 RVA: 0x001380F8 File Offset: 0x001362F8
		// (set) Token: 0x06004A5D RID: 19037 RVA: 0x00138107 File Offset: 0x00136307
		[SRDescription("WebBrowserScrollBarsEnabledDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public bool ScrollBarsEnabled
		{
			get
			{
				return this.webBrowserState[32];
			}
			set
			{
				if (value != this.webBrowserState[32])
				{
					this.webBrowserState[32] = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x00138130 File Offset: 0x00136330
		public void ShowPageSetupDialog()
		{
			IntSecurity.SafePrinting.Demand();
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_PAGESETUP, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x0013817C File Offset: 0x0013637C
		public void ShowPrintDialog()
		{
			IntSecurity.SafePrinting.Demand();
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_PRINT, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x001381C8 File Offset: 0x001363C8
		public void ShowPrintPreviewDialog()
		{
			IntSecurity.SafePrinting.Demand();
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_PRINTPREVIEW, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x00138214 File Offset: 0x00136414
		public void ShowPropertiesDialog()
		{
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_PROPERTIES, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x00138258 File Offset: 0x00136458
		public void ShowSaveAsDialog()
		{
			IntSecurity.FileDialogSaveFile.Demand();
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_SAVEAS, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x001382A4 File Offset: 0x001364A4
		public void Stop()
		{
			try
			{
				this.AxIWebBrowser2.Stop();
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x140003BB RID: 955
		// (add) Token: 0x06004A64 RID: 19044 RVA: 0x001382DC File Offset: 0x001364DC
		// (remove) Token: 0x06004A65 RID: 19045 RVA: 0x00138314 File Offset: 0x00136514
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("WebBrowserCanGoBackChangedDescr")]
		public event EventHandler CanGoBackChanged;

		// Token: 0x140003BC RID: 956
		// (add) Token: 0x06004A66 RID: 19046 RVA: 0x0013834C File Offset: 0x0013654C
		// (remove) Token: 0x06004A67 RID: 19047 RVA: 0x00138384 File Offset: 0x00136584
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("WebBrowserCanGoForwardChangedDescr")]
		public event EventHandler CanGoForwardChanged;

		// Token: 0x140003BD RID: 957
		// (add) Token: 0x06004A68 RID: 19048 RVA: 0x001383BC File Offset: 0x001365BC
		// (remove) Token: 0x06004A69 RID: 19049 RVA: 0x001383F4 File Offset: 0x001365F4
		[SRCategory("CatBehavior")]
		[SRDescription("WebBrowserDocumentCompletedDescr")]
		public event WebBrowserDocumentCompletedEventHandler DocumentCompleted;

		// Token: 0x140003BE RID: 958
		// (add) Token: 0x06004A6A RID: 19050 RVA: 0x0013842C File Offset: 0x0013662C
		// (remove) Token: 0x06004A6B RID: 19051 RVA: 0x00138464 File Offset: 0x00136664
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("WebBrowserDocumentTitleChangedDescr")]
		public event EventHandler DocumentTitleChanged;

		// Token: 0x140003BF RID: 959
		// (add) Token: 0x06004A6C RID: 19052 RVA: 0x0013849C File Offset: 0x0013669C
		// (remove) Token: 0x06004A6D RID: 19053 RVA: 0x001384D4 File Offset: 0x001366D4
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("WebBrowserEncryptionLevelChangedDescr")]
		public event EventHandler EncryptionLevelChanged;

		// Token: 0x140003C0 RID: 960
		// (add) Token: 0x06004A6E RID: 19054 RVA: 0x0013850C File Offset: 0x0013670C
		// (remove) Token: 0x06004A6F RID: 19055 RVA: 0x00138544 File Offset: 0x00136744
		[SRCategory("CatBehavior")]
		[SRDescription("WebBrowserFileDownloadDescr")]
		public event EventHandler FileDownload;

		// Token: 0x140003C1 RID: 961
		// (add) Token: 0x06004A70 RID: 19056 RVA: 0x0013857C File Offset: 0x0013677C
		// (remove) Token: 0x06004A71 RID: 19057 RVA: 0x001385B4 File Offset: 0x001367B4
		[SRCategory("CatAction")]
		[SRDescription("WebBrowserNavigatedDescr")]
		public event WebBrowserNavigatedEventHandler Navigated;

		// Token: 0x140003C2 RID: 962
		// (add) Token: 0x06004A72 RID: 19058 RVA: 0x001385EC File Offset: 0x001367EC
		// (remove) Token: 0x06004A73 RID: 19059 RVA: 0x00138624 File Offset: 0x00136824
		[SRCategory("CatAction")]
		[SRDescription("WebBrowserNavigatingDescr")]
		public event WebBrowserNavigatingEventHandler Navigating;

		// Token: 0x140003C3 RID: 963
		// (add) Token: 0x06004A74 RID: 19060 RVA: 0x0013865C File Offset: 0x0013685C
		// (remove) Token: 0x06004A75 RID: 19061 RVA: 0x00138694 File Offset: 0x00136894
		[SRCategory("CatAction")]
		[SRDescription("WebBrowserNewWindowDescr")]
		public event CancelEventHandler NewWindow;

		// Token: 0x140003C4 RID: 964
		// (add) Token: 0x06004A76 RID: 19062 RVA: 0x001386CC File Offset: 0x001368CC
		// (remove) Token: 0x06004A77 RID: 19063 RVA: 0x00138704 File Offset: 0x00136904
		[SRCategory("CatAction")]
		[SRDescription("WebBrowserProgressChangedDescr")]
		public event WebBrowserProgressChangedEventHandler ProgressChanged;

		// Token: 0x140003C5 RID: 965
		// (add) Token: 0x06004A78 RID: 19064 RVA: 0x0013873C File Offset: 0x0013693C
		// (remove) Token: 0x06004A79 RID: 19065 RVA: 0x00138774 File Offset: 0x00136974
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("WebBrowserStatusTextChangedDescr")]
		public event EventHandler StatusTextChanged;

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x001387AC File Offset: 0x001369AC
		public override bool Focused
		{
			get
			{
				if (base.Focused)
				{
					return true;
				}
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				return focus != IntPtr.Zero && SafeNativeMethods.IsChild(new HandleRef(this, base.Handle), new HandleRef(null, focus));
			}
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x001387F0 File Offset: 0x001369F0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.htmlShimManager != null)
				{
					this.htmlShimManager.Dispose();
				}
				this.DetachSink();
				base.ActiveXSite.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06004A7C RID: 19068 RVA: 0x00138820 File Offset: 0x00136A20
		protected override Size DefaultSize
		{
			get
			{
				return new Size(250, 250);
			}
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x00138831 File Offset: 0x00136A31
		protected override void AttachInterfaces(object nativeActiveXObject)
		{
			this.axIWebBrowser2 = (UnsafeNativeMethods.IWebBrowser2)nativeActiveXObject;
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x0013883F File Offset: 0x00136A3F
		protected override void DetachInterfaces()
		{
			this.axIWebBrowser2 = null;
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x00138848 File Offset: 0x00136A48
		protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
		{
			return new WebBrowser.WebBrowserSite(this);
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00138850 File Offset: 0x00136A50
		protected override void CreateSink()
		{
			object activeXInstance = this.activeXInstance;
			if (activeXInstance != null)
			{
				this.webBrowserEvent = new WebBrowser.WebBrowserEvent(this);
				this.webBrowserEvent.AllowNavigation = this.AllowNavigation;
				this.cookie = new AxHost.ConnectionPointCookie(activeXInstance, this.webBrowserEvent, typeof(UnsafeNativeMethods.DWebBrowserEvents2));
			}
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x001388A0 File Offset: 0x00136AA0
		protected override void DetachSink()
		{
			if (this.cookie != null)
			{
				this.cookie.Disconnect();
				this.cookie = null;
			}
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x001388BC File Offset: 0x00136ABC
		internal override void OnTopMostActiveXParentChanged(EventArgs e)
		{
			if (base.TopMostParent.IsIEParent)
			{
				WebBrowser.createdInIE = true;
				this.CheckIfCreatedInIE();
				return;
			}
			WebBrowser.createdInIE = false;
			base.OnTopMostActiveXParentChanged(e);
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x001388E5 File Offset: 0x00136AE5
		protected virtual void OnCanGoBackChanged(EventArgs e)
		{
			if (this.CanGoBackChanged != null)
			{
				this.CanGoBackChanged(this, e);
			}
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x001388FC File Offset: 0x00136AFC
		protected virtual void OnCanGoForwardChanged(EventArgs e)
		{
			if (this.CanGoForwardChanged != null)
			{
				this.CanGoForwardChanged(this, e);
			}
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x00138913 File Offset: 0x00136B13
		protected virtual void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			this.AxIWebBrowser2.RegisterAsDropTarget = this.AllowWebBrowserDrop;
			if (this.DocumentCompleted != null)
			{
				this.DocumentCompleted(this, e);
			}
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x0013893B File Offset: 0x00136B3B
		protected virtual void OnDocumentTitleChanged(EventArgs e)
		{
			if (this.DocumentTitleChanged != null)
			{
				this.DocumentTitleChanged(this, e);
			}
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x00138952 File Offset: 0x00136B52
		protected virtual void OnEncryptionLevelChanged(EventArgs e)
		{
			if (this.EncryptionLevelChanged != null)
			{
				this.EncryptionLevelChanged(this, e);
			}
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x00138969 File Offset: 0x00136B69
		protected virtual void OnFileDownload(EventArgs e)
		{
			if (this.FileDownload != null)
			{
				this.FileDownload(this, e);
			}
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x00138980 File Offset: 0x00136B80
		protected virtual void OnNavigated(WebBrowserNavigatedEventArgs e)
		{
			if (this.Navigated != null)
			{
				this.Navigated(this, e);
			}
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x00138997 File Offset: 0x00136B97
		protected virtual void OnNavigating(WebBrowserNavigatingEventArgs e)
		{
			if (this.Navigating != null)
			{
				this.Navigating(this, e);
			}
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x001389AE File Offset: 0x00136BAE
		protected virtual void OnNewWindow(CancelEventArgs e)
		{
			if (this.NewWindow != null)
			{
				this.NewWindow(this, e);
			}
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x001389C5 File Offset: 0x00136BC5
		protected virtual void OnProgressChanged(WebBrowserProgressChangedEventArgs e)
		{
			if (this.ProgressChanged != null)
			{
				this.ProgressChanged(this, e);
			}
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x001389DC File Offset: 0x00136BDC
		protected virtual void OnStatusTextChanged(EventArgs e)
		{
			if (this.StatusTextChanged != null)
			{
				this.StatusTextChanged(this, e);
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06004A8E RID: 19086 RVA: 0x001389F3 File Offset: 0x00136BF3
		internal HtmlShimManager ShimManager
		{
			get
			{
				if (this.htmlShimManager == null)
				{
					this.htmlShimManager = new HtmlShimManager();
				}
				return this.htmlShimManager;
			}
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x00138A0E File Offset: 0x00136C0E
		private void CheckIfCreatedInIE()
		{
			if (!WebBrowser.createdInIE)
			{
				return;
			}
			if (this.ParentInternal != null)
			{
				this.ParentInternal.Controls.Remove(this);
				base.Dispose();
				return;
			}
			base.Dispose();
			throw new NotSupportedException(SR.GetString("WebBrowserInIENotSupported"));
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x00138A50 File Offset: 0x00136C50
		internal static void EnsureUrlConnectPermission(Uri url)
		{
			WebPermission webPermission = new WebPermission(NetworkAccess.Connect, url.AbsoluteUri);
			webPermission.Demand();
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x00138A71 File Offset: 0x00136C71
		private string ReadyNavigateToUrl(string urlString)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				urlString = "about:blank";
			}
			if (!this.webBrowserState[2])
			{
				this.documentStreamToSetOnLoad = null;
			}
			return urlString;
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x00138A98 File Offset: 0x00136C98
		private string ReadyNavigateToUrl(Uri url)
		{
			string urlString;
			if (url == null)
			{
				urlString = "about:blank";
			}
			else
			{
				if (!url.IsAbsoluteUri)
				{
					throw new ArgumentException(SR.GetString("WebBrowserNavigateAbsoluteUri", new object[]
					{
						"uri"
					}));
				}
				urlString = (url.IsFile ? url.OriginalString : url.AbsoluteUri);
			}
			return this.ReadyNavigateToUrl(urlString);
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x00138AFC File Offset: 0x00136CFC
		private void PerformNavigateHelper(string urlString, bool newWindow, string targetFrameName, byte[] postData, string headers)
		{
			object obj = urlString;
			object obj2 = newWindow ? 1 : 0;
			object obj3 = targetFrameName;
			object obj4 = postData;
			object obj5 = headers;
			this.PerformNavigate2(ref obj, ref obj2, ref obj3, ref obj4, ref obj5);
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x00138B34 File Offset: 0x00136D34
		private void PerformNavigate2(ref object URL, ref object flags, ref object targetFrameName, ref object postData, ref object headers)
		{
			try
			{
				this.AxIWebBrowser2.Navigate2(ref URL, ref flags, ref targetFrameName, ref postData, ref headers);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode != -2147023673)
				{
					throw;
				}
			}
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x00138B78 File Offset: 0x00136D78
		private bool ShouldSerializeDocumentText()
		{
			return this.IsValidUrl;
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06004A96 RID: 19094 RVA: 0x00138B80 File Offset: 0x00136D80
		private bool IsValidUrl
		{
			get
			{
				return this.Url == null || this.Url.AbsoluteUri == "about:blank";
			}
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x00138BA7 File Offset: 0x00136DA7
		private bool ShouldSerializeUrl()
		{
			return !this.ShouldSerializeDocumentText();
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x00138BB4 File Offset: 0x00136DB4
		private bool ShowContextMenu(int x, int y)
		{
			ContextMenuStrip contextMenuStrip = this.ContextMenuStrip;
			ContextMenu contextMenu = (contextMenuStrip != null) ? null : this.ContextMenu;
			if (contextMenuStrip == null && contextMenu == null)
			{
				return false;
			}
			bool isKeyboardActivated = false;
			Point point;
			if (x == -1)
			{
				isKeyboardActivated = true;
				point = new Point(base.Width / 2, base.Height / 2);
			}
			else
			{
				point = base.PointToClientInternal(new Point(x, y));
			}
			if (base.ClientRectangle.Contains(point))
			{
				if (contextMenuStrip != null)
				{
					contextMenuStrip.ShowInternal(this, point, isKeyboardActivated);
				}
				else if (contextMenu != null)
				{
					contextMenu.Show(this, point);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x00138C3C File Offset: 0x00136E3C
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 123)
			{
				int x = NativeMethods.Util.SignedLOWORD(m.LParam);
				int y = NativeMethods.Util.SignedHIWORD(m.LParam);
				if (!this.ShowContextMenu(x, y))
				{
					this.DefWndProc(ref m);
					return;
				}
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06004A9A RID: 19098 RVA: 0x00138C88 File Offset: 0x00136E88
		private UnsafeNativeMethods.IWebBrowser2 AxIWebBrowser2
		{
			get
			{
				if (this.axIWebBrowser2 == null)
				{
					if (base.IsDisposed)
					{
						throw new ObjectDisposedException(base.GetType().Name);
					}
					base.TransitionUpTo(WebBrowserHelper.AXState.InPlaceActive);
				}
				if (this.axIWebBrowser2 == null)
				{
					throw new InvalidOperationException(SR.GetString("WebBrowserNoCastToIWebBrowser2"));
				}
				return this.axIWebBrowser2;
			}
		}

		// Token: 0x040027D9 RID: 10201
		private static bool createdInIE;

		// Token: 0x040027DA RID: 10202
		private UnsafeNativeMethods.IWebBrowser2 axIWebBrowser2;

		// Token: 0x040027DB RID: 10203
		private AxHost.ConnectionPointCookie cookie;

		// Token: 0x040027DC RID: 10204
		private Stream documentStreamToSetOnLoad;

		// Token: 0x040027DD RID: 10205
		private WebBrowserEncryptionLevel encryptionLevel;

		// Token: 0x040027DE RID: 10206
		private object objectForScripting;

		// Token: 0x040027DF RID: 10207
		private WebBrowser.WebBrowserEvent webBrowserEvent;

		// Token: 0x040027E0 RID: 10208
		internal string statusText = "";

		// Token: 0x040027E1 RID: 10209
		private const int WEBBROWSERSTATE_webBrowserShortcutsEnabled = 1;

		// Token: 0x040027E2 RID: 10210
		private const int WEBBROWSERSTATE_documentStreamJustSet = 2;

		// Token: 0x040027E3 RID: 10211
		private const int WEBBROWSERSTATE_isWebBrowserContextMenuEnabled = 4;

		// Token: 0x040027E4 RID: 10212
		private const int WEBBROWSERSTATE_canGoBack = 8;

		// Token: 0x040027E5 RID: 10213
		private const int WEBBROWSERSTATE_canGoForward = 16;

		// Token: 0x040027E6 RID: 10214
		private const int WEBBROWSERSTATE_scrollbarsEnabled = 32;

		// Token: 0x040027E7 RID: 10215
		private const int WEBBROWSERSTATE_allowNavigation = 64;

		// Token: 0x040027E8 RID: 10216
		private BitVector32 webBrowserState;

		// Token: 0x040027F4 RID: 10228
		private HtmlShimManager htmlShimManager;

		// Token: 0x02000827 RID: 2087
		[ComVisible(false)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected class WebBrowserSite : WebBrowserSiteBase, UnsafeNativeMethods.IDocHostUIHandler
		{
			// Token: 0x0600700D RID: 28685 RVA: 0x0019B4CE File Offset: 0x001996CE
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public WebBrowserSite(WebBrowser host) : base(host)
			{
			}

			// Token: 0x0600700E RID: 28686 RVA: 0x0019B4D8 File Offset: 0x001996D8
			int UnsafeNativeMethods.IDocHostUIHandler.ShowContextMenu(int dwID, NativeMethods.POINT pt, object pcmdtReserved, object pdispReserved)
			{
				WebBrowser webBrowser = (WebBrowser)base.Host;
				if (webBrowser.IsWebBrowserContextMenuEnabled)
				{
					return 1;
				}
				if (pt.x == 0 && pt.y == 0)
				{
					pt.x = -1;
					pt.y = -1;
				}
				webBrowser.ShowContextMenu(pt.x, pt.y);
				return 0;
			}

			// Token: 0x0600700F RID: 28687 RVA: 0x0019B530 File Offset: 0x00199730
			int UnsafeNativeMethods.IDocHostUIHandler.GetHostInfo(NativeMethods.DOCHOSTUIINFO info)
			{
				WebBrowser webBrowser = (WebBrowser)base.Host;
				info.dwDoubleClick = 0;
				info.dwFlags = 2097168;
				if (webBrowser.ScrollBarsEnabled)
				{
					info.dwFlags |= 128;
				}
				else
				{
					info.dwFlags |= 8;
				}
				if (Application.RenderWithVisualStyles)
				{
					info.dwFlags |= 262144;
				}
				else
				{
					info.dwFlags |= 524288;
				}
				return 0;
			}

			// Token: 0x06007010 RID: 28688 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IDocHostUIHandler.EnableModeless(bool fEnable)
			{
				return -2147467263;
			}

			// Token: 0x06007011 RID: 28689 RVA: 0x00013062 File Offset: 0x00011262
			int UnsafeNativeMethods.IDocHostUIHandler.ShowUI(int dwID, UnsafeNativeMethods.IOleInPlaceActiveObject activeObject, NativeMethods.IOleCommandTarget commandTarget, UnsafeNativeMethods.IOleInPlaceFrame frame, UnsafeNativeMethods.IOleInPlaceUIWindow doc)
			{
				return 1;
			}

			// Token: 0x06007012 RID: 28690 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IDocHostUIHandler.HideUI()
			{
				return -2147467263;
			}

			// Token: 0x06007013 RID: 28691 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IDocHostUIHandler.UpdateUI()
			{
				return -2147467263;
			}

			// Token: 0x06007014 RID: 28692 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IDocHostUIHandler.OnDocWindowActivate(bool fActivate)
			{
				return -2147467263;
			}

			// Token: 0x06007015 RID: 28693 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IDocHostUIHandler.OnFrameWindowActivate(bool fActivate)
			{
				return -2147467263;
			}

			// Token: 0x06007016 RID: 28694 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IDocHostUIHandler.ResizeBorder(NativeMethods.COMRECT rect, UnsafeNativeMethods.IOleInPlaceUIWindow doc, bool fFrameWindow)
			{
				return -2147467263;
			}

			// Token: 0x06007017 RID: 28695 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IDocHostUIHandler.GetOptionKeyPath(string[] pbstrKey, int dw)
			{
				return -2147467263;
			}

			// Token: 0x06007018 RID: 28696 RVA: 0x00016313 File Offset: 0x00014513
			int UnsafeNativeMethods.IDocHostUIHandler.GetDropTarget(UnsafeNativeMethods.IOleDropTarget pDropTarget, out UnsafeNativeMethods.IOleDropTarget ppDropTarget)
			{
				ppDropTarget = null;
				return -2147467263;
			}

			// Token: 0x06007019 RID: 28697 RVA: 0x0019B5B4 File Offset: 0x001997B4
			int UnsafeNativeMethods.IDocHostUIHandler.GetExternal(out object ppDispatch)
			{
				WebBrowser webBrowser = (WebBrowser)base.Host;
				ppDispatch = webBrowser.ObjectForScripting;
				return 0;
			}

			// Token: 0x0600701A RID: 28698 RVA: 0x0019B5D8 File Offset: 0x001997D8
			int UnsafeNativeMethods.IDocHostUIHandler.TranslateAccelerator(ref NativeMethods.MSG msg, ref Guid group, int nCmdID)
			{
				WebBrowser webBrowser = (WebBrowser)base.Host;
				if (webBrowser.WebBrowserShortcutsEnabled)
				{
					return 1;
				}
				int num = (int)msg.wParam | (int)Control.ModifierKeys;
				if (msg.message != 258 && Enum.IsDefined(typeof(Shortcut), (Shortcut)num))
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x0600701B RID: 28699 RVA: 0x0019B634 File Offset: 0x00199834
			int UnsafeNativeMethods.IDocHostUIHandler.TranslateUrl(int dwTranslate, string strUrlIn, out string pstrUrlOut)
			{
				pstrUrlOut = null;
				return 1;
			}

			// Token: 0x0600701C RID: 28700 RVA: 0x0019B63A File Offset: 0x0019983A
			int UnsafeNativeMethods.IDocHostUIHandler.FilterDataObject(IDataObject pDO, out IDataObject ppDORet)
			{
				ppDORet = null;
				return 1;
			}

			// Token: 0x0600701D RID: 28701 RVA: 0x0019B640 File Offset: 0x00199840
			internal override void OnPropertyChanged(int dispid)
			{
				if (dispid != -525)
				{
					base.OnPropertyChanged(dispid);
				}
			}
		}

		// Token: 0x02000828 RID: 2088
		[ClassInterface(ClassInterfaceType.None)]
		private class WebBrowserEvent : StandardOleMarshalObject, UnsafeNativeMethods.DWebBrowserEvents2
		{
			// Token: 0x0600701E RID: 28702 RVA: 0x0019B651 File Offset: 0x00199851
			public WebBrowserEvent(WebBrowser parent)
			{
				this.parent = parent;
			}

			// Token: 0x1700187E RID: 6270
			// (get) Token: 0x0600701F RID: 28703 RVA: 0x0019B660 File Offset: 0x00199860
			// (set) Token: 0x06007020 RID: 28704 RVA: 0x0019B668 File Offset: 0x00199868
			public bool AllowNavigation
			{
				get
				{
					return this.allowNavigation;
				}
				set
				{
					this.allowNavigation = value;
				}
			}

			// Token: 0x06007021 RID: 28705 RVA: 0x0019B671 File Offset: 0x00199871
			public void CommandStateChange(long command, bool enable)
			{
				if (command == 2L)
				{
					this.parent.CanGoBackInternal = enable;
					return;
				}
				if (command == 1L)
				{
					this.parent.CanGoForwardInternal = enable;
				}
			}

			// Token: 0x06007022 RID: 28706 RVA: 0x0019B698 File Offset: 0x00199898
			public void BeforeNavigate2(object pDisp, ref object urlObject, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
			{
				if (this.AllowNavigation || !this.haveNavigated)
				{
					if (targetFrameName == null)
					{
						targetFrameName = "";
					}
					if (headers == null)
					{
						headers = "";
					}
					string uriString = (urlObject == null) ? "" : ((string)urlObject);
					WebBrowserNavigatingEventArgs webBrowserNavigatingEventArgs = new WebBrowserNavigatingEventArgs(new Uri(uriString), (targetFrameName == null) ? "" : ((string)targetFrameName));
					this.parent.OnNavigating(webBrowserNavigatingEventArgs);
					cancel = webBrowserNavigatingEventArgs.Cancel;
					return;
				}
				cancel = true;
			}

			// Token: 0x06007023 RID: 28707 RVA: 0x0019B71C File Offset: 0x0019991C
			public void DocumentComplete(object pDisp, ref object urlObject)
			{
				this.haveNavigated = true;
				if (this.parent.documentStreamToSetOnLoad != null && (string)urlObject == "about:blank")
				{
					HtmlDocument document = this.parent.Document;
					if (document != null)
					{
						UnsafeNativeMethods.IPersistStreamInit persistStreamInit = document.DomDocument as UnsafeNativeMethods.IPersistStreamInit;
						UnsafeNativeMethods.IStream pstm = new UnsafeNativeMethods.ComStreamFromDataStream(this.parent.documentStreamToSetOnLoad);
						persistStreamInit.Load(pstm);
						document.Encoding = "unicode";
					}
					this.parent.documentStreamToSetOnLoad = null;
					return;
				}
				string uriString = (urlObject == null) ? "" : urlObject.ToString();
				WebBrowserDocumentCompletedEventArgs e = new WebBrowserDocumentCompletedEventArgs(new Uri(uriString));
				this.parent.OnDocumentCompleted(e);
			}

			// Token: 0x06007024 RID: 28708 RVA: 0x0019B7CE File Offset: 0x001999CE
			public void TitleChange(string text)
			{
				this.parent.OnDocumentTitleChanged(EventArgs.Empty);
			}

			// Token: 0x06007025 RID: 28709 RVA: 0x0019B7E0 File Offset: 0x001999E0
			public void SetSecureLockIcon(int secureLockIcon)
			{
				this.parent.encryptionLevel = (WebBrowserEncryptionLevel)secureLockIcon;
				this.parent.OnEncryptionLevelChanged(EventArgs.Empty);
			}

			// Token: 0x06007026 RID: 28710 RVA: 0x0019B800 File Offset: 0x00199A00
			public void NavigateComplete2(object pDisp, ref object urlObject)
			{
				string uriString = (urlObject == null) ? "" : ((string)urlObject);
				WebBrowserNavigatedEventArgs e = new WebBrowserNavigatedEventArgs(new Uri(uriString));
				this.parent.OnNavigated(e);
			}

			// Token: 0x06007027 RID: 28711 RVA: 0x0019B838 File Offset: 0x00199A38
			public void NewWindow2(ref object ppDisp, ref bool cancel)
			{
				CancelEventArgs cancelEventArgs = new CancelEventArgs();
				this.parent.OnNewWindow(cancelEventArgs);
				cancel = cancelEventArgs.Cancel;
			}

			// Token: 0x06007028 RID: 28712 RVA: 0x0019B860 File Offset: 0x00199A60
			public void ProgressChange(int progress, int progressMax)
			{
				WebBrowserProgressChangedEventArgs e = new WebBrowserProgressChangedEventArgs((long)progress, (long)progressMax);
				this.parent.OnProgressChanged(e);
			}

			// Token: 0x06007029 RID: 28713 RVA: 0x0019B883 File Offset: 0x00199A83
			public void StatusTextChange(string text)
			{
				this.parent.statusText = ((text == null) ? "" : text);
				this.parent.OnStatusTextChanged(EventArgs.Empty);
			}

			// Token: 0x0600702A RID: 28714 RVA: 0x0019B8AB File Offset: 0x00199AAB
			public void DownloadBegin()
			{
				this.parent.OnFileDownload(EventArgs.Empty);
			}

			// Token: 0x0600702B RID: 28715 RVA: 0x000072B6 File Offset: 0x000054B6
			public void FileDownload(ref bool cancel)
			{
			}

			// Token: 0x0600702C RID: 28716 RVA: 0x000072B6 File Offset: 0x000054B6
			public void PrivacyImpactedStateChange(bool bImpacted)
			{
			}

			// Token: 0x0600702D RID: 28717 RVA: 0x000072B6 File Offset: 0x000054B6
			public void UpdatePageStatus(object pDisp, ref object nPage, ref object fDone)
			{
			}

			// Token: 0x0600702E RID: 28718 RVA: 0x000072B6 File Offset: 0x000054B6
			public void PrintTemplateTeardown(object pDisp)
			{
			}

			// Token: 0x0600702F RID: 28719 RVA: 0x000072B6 File Offset: 0x000054B6
			public void PrintTemplateInstantiation(object pDisp)
			{
			}

			// Token: 0x06007030 RID: 28720 RVA: 0x000072B6 File Offset: 0x000054B6
			public void NavigateError(object pDisp, ref object url, ref object frame, ref object statusCode, ref bool cancel)
			{
			}

			// Token: 0x06007031 RID: 28721 RVA: 0x000072B6 File Offset: 0x000054B6
			public void ClientToHostWindow(ref long cX, ref long cY)
			{
			}

			// Token: 0x06007032 RID: 28722 RVA: 0x000072B6 File Offset: 0x000054B6
			public void WindowClosing(bool isChildWindow, ref bool cancel)
			{
			}

			// Token: 0x06007033 RID: 28723 RVA: 0x000072B6 File Offset: 0x000054B6
			public void WindowSetHeight(int height)
			{
			}

			// Token: 0x06007034 RID: 28724 RVA: 0x000072B6 File Offset: 0x000054B6
			public void WindowSetWidth(int width)
			{
			}

			// Token: 0x06007035 RID: 28725 RVA: 0x000072B6 File Offset: 0x000054B6
			public void WindowSetTop(int top)
			{
			}

			// Token: 0x06007036 RID: 28726 RVA: 0x000072B6 File Offset: 0x000054B6
			public void WindowSetLeft(int left)
			{
			}

			// Token: 0x06007037 RID: 28727 RVA: 0x000072B6 File Offset: 0x000054B6
			public void WindowSetResizable(bool resizable)
			{
			}

			// Token: 0x06007038 RID: 28728 RVA: 0x000072B6 File Offset: 0x000054B6
			public void OnTheaterMode(bool theaterMode)
			{
			}

			// Token: 0x06007039 RID: 28729 RVA: 0x000072B6 File Offset: 0x000054B6
			public void OnFullScreen(bool fullScreen)
			{
			}

			// Token: 0x0600703A RID: 28730 RVA: 0x000072B6 File Offset: 0x000054B6
			public void OnStatusBar(bool statusBar)
			{
			}

			// Token: 0x0600703B RID: 28731 RVA: 0x000072B6 File Offset: 0x000054B6
			public void OnMenuBar(bool menuBar)
			{
			}

			// Token: 0x0600703C RID: 28732 RVA: 0x000072B6 File Offset: 0x000054B6
			public void OnToolBar(bool toolBar)
			{
			}

			// Token: 0x0600703D RID: 28733 RVA: 0x000072B6 File Offset: 0x000054B6
			public void OnVisible(bool visible)
			{
			}

			// Token: 0x0600703E RID: 28734 RVA: 0x000072B6 File Offset: 0x000054B6
			public void OnQuit()
			{
			}

			// Token: 0x0600703F RID: 28735 RVA: 0x000072B6 File Offset: 0x000054B6
			public void PropertyChange(string szProperty)
			{
			}

			// Token: 0x06007040 RID: 28736 RVA: 0x000072B6 File Offset: 0x000054B6
			public void DownloadComplete()
			{
			}

			// Token: 0x04004341 RID: 17217
			private WebBrowser parent;

			// Token: 0x04004342 RID: 17218
			private bool allowNavigation;

			// Token: 0x04004343 RID: 17219
			private bool haveNavigated;
		}
	}
}
