using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Web.Caching;
using System.Web.Util;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000521 RID: 1313
	[DefaultProperty("DocumentSource")]
	[PersistChildren(false, true)]
	[ControlBuilder(typeof(XmlBuilder))]
	[Designer("System.Web.UI.Design.WebControls.XmlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class Xml : Control
	{
		// Token: 0x0600425F RID: 16991 RVA: 0x000D8AF0 File Offset: 0x000D6CF0
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		static Xml()
		{
			XmlTextReader stylesheet = new XmlTextReader(new StringReader("<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'><xsl:template match=\"/\"> <xsl:copy-of select=\".\"/> </xsl:template> </xsl:stylesheet>"));
			Xml._identityTransform = new XslTransform();
			Xml._identityTransform.Load(stylesheet, null, null);
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x000610CF File Offset: 0x0005F2CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06004261 RID: 16993 RVA: 0x000610DF File Offset: 0x0005F2DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x000D8B24 File Offset: 0x000D6D24
		// (set) Token: 0x06004263 RID: 16995 RVA: 0x000D8B3A File Offset: 0x000D6D3A
		[Browsable(false)]
		[WebSysDescription("Xml_Document")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("The recommended alternative is the XPathNavigator property. Create a System.Xml.XPath.XPathDocument and call CreateNavigator() to create an XPathNavigator. http://go.microsoft.com/fwlink/?linkid=14202")]
		public XmlDocument Document
		{
			get
			{
				if (this._document == null)
				{
					this.LoadXmlDocument();
				}
				return this._document;
			}
			set
			{
				this.DocumentSource = null;
				this._xpathDocument = null;
				this._documentContent = null;
				this._document = value;
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x000D8B58 File Offset: 0x000D6D58
		// (set) Token: 0x06004265 RID: 16997 RVA: 0x000D8B6E File Offset: 0x000D6D6E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Xml_DocumentContent")]
		public string DocumentContent
		{
			get
			{
				if (this._documentContent == null)
				{
					return string.Empty;
				}
				return this._documentContent;
			}
			set
			{
				this._document = null;
				this._xpathDocument = null;
				this._xpathNavigator = null;
				this._documentContent = value;
				if (base.DesignMode)
				{
					this.ViewState["OriginalContent"] = null;
				}
			}
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06004266 RID: 16998 RVA: 0x000D8BA5 File Offset: 0x000D6DA5
		// (set) Token: 0x06004267 RID: 16999 RVA: 0x000D8BBB File Offset: 0x000D6DBB
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.XmlUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("Xml_DocumentSource")]
		public string DocumentSource
		{
			get
			{
				if (this._documentSource != null)
				{
					return this._documentSource;
				}
				return string.Empty;
			}
			set
			{
				this._document = null;
				this._xpathDocument = null;
				this._documentContent = null;
				this._xpathNavigator = null;
				this._documentSource = value;
			}
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06004268 RID: 17000 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06004269 RID: 17001 RVA: 0x000610E7 File Offset: 0x0005F2E7
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x0600426A RID: 17002 RVA: 0x00028752 File Offset: 0x00026952
		// (set) Token: 0x0600426B RID: 17003 RVA: 0x000610E7 File Offset: 0x0005F2E7
		[Browsable(false)]
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x0600426C RID: 17004 RVA: 0x000D8BE0 File Offset: 0x000D6DE0
		// (set) Token: 0x0600426D RID: 17005 RVA: 0x000D8BED File Offset: 0x000D6DED
		[Browsable(false)]
		[WebSysDescription("Xml_Transform")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public XslTransform Transform
		{
			get
			{
				return XmlUtils.GetXslTransform(this._transform);
			}
			set
			{
				if (XmlUtils.GetXslTransform(value) != null)
				{
					this.TransformSource = null;
					this._transform = value;
				}
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x0600426E RID: 17006 RVA: 0x000D8C05 File Offset: 0x000D6E05
		// (set) Token: 0x0600426F RID: 17007 RVA: 0x000D8C0D File Offset: 0x000D6E0D
		[Browsable(false)]
		[WebSysDescription("Xml_TransformArgumentList")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public XsltArgumentList TransformArgumentList
		{
			get
			{
				return this._transformArgumentList;
			}
			set
			{
				this._transformArgumentList = value;
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06004270 RID: 17008 RVA: 0x000D8C16 File Offset: 0x000D6E16
		// (set) Token: 0x06004271 RID: 17009 RVA: 0x000D8C2C File Offset: 0x000D6E2C
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.XslUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("Xml_TransformSource")]
		public string TransformSource
		{
			get
			{
				if (this._transformSource != null)
				{
					return this._transformSource;
				}
				return string.Empty;
			}
			set
			{
				this._transform = null;
				this._transformSource = value;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06004272 RID: 17010 RVA: 0x000D8C3C File Offset: 0x000D6E3C
		// (set) Token: 0x06004273 RID: 17011 RVA: 0x000D8C44 File Offset: 0x000D6E44
		[Browsable(false)]
		[WebSysDescription("Xml_XPathNavigator")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public XPathNavigator XPathNavigator
		{
			get
			{
				return this._xpathNavigator;
			}
			set
			{
				this.DocumentSource = null;
				this._xpathDocument = null;
				this._documentContent = null;
				this._document = null;
				this._xpathNavigator = value;
			}
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x000D8C6C File Offset: 0x000D6E6C
		protected override void AddParsedSubObject(object obj)
		{
			if (!(obj is LiteralControl))
			{
				throw new HttpException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
				{
					"Xml",
					obj.GetType().Name.ToString(CultureInfo.InvariantCulture)
				}));
			}
			string text = ((LiteralControl)obj).Text;
			int startIndex = Util.FirstNonWhiteSpaceIndex(text);
			this.DocumentContent = text.Substring(startIndex);
			if (base.DesignMode)
			{
				this.ViewState["OriginalContent"] = text;
				return;
			}
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x00061160 File Offset: 0x0005F360
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			return base.FindControl(id);
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x00061169 File Offset: 0x0005F369
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000D8CF4 File Offset: 0x000D6EF4
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			IDictionary dictionary = new HybridDictionary();
			dictionary["OriginalContent"] = this.ViewState["OriginalContent"];
			return dictionary;
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x0006118E File Offset: 0x0005F38E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return base.HasControls();
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x000D8D24 File Offset: 0x000D6F24
		private void LoadTransformFromSource()
		{
			if (this._transform != null)
			{
				return;
			}
			if (string.IsNullOrEmpty(this._transformSource) || this._transformSource.Trim().Length == 0)
			{
				return;
			}
			VirtualPath virtualPath;
			string text;
			base.ResolvePhysicalOrVirtualPath(this._transformSource, out virtualPath, out text);
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			string key = "p" + ((text != null) ? text : virtualPath.VirtualPathString);
			object obj = internalCache.Get(key);
			if (obj == null)
			{
				CacheDependency cacheDependency;
				using (Stream stream = base.OpenFileAndGetDependency(virtualPath, text, out cacheDependency))
				{
					if (text == null)
					{
						text = virtualPath.MapPath();
					}
					XmlReader xmlReader = XmlUtils.CreateXmlReader(stream, text);
					this._transform = XmlUtils.CreateXslTransform(xmlReader);
					if (this._transform == null)
					{
						this._compiledTransform = XmlUtils.CreateXslCompiledTransform(xmlReader);
					}
				}
				if (cacheDependency == null)
				{
					return;
				}
				using (cacheDependency)
				{
					internalCache.Insert(key, (this._compiledTransform == null) ? this._transform : this._compiledTransform, new CacheInsertOptions
					{
						Dependencies = cacheDependency
					});
					return;
				}
			}
			this._compiledTransform = (obj as XslCompiledTransform);
			if (this._compiledTransform == null)
			{
				this._transform = (XslTransform)obj;
			}
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x000D8E6C File Offset: 0x000D706C
		private void LoadXmlDocument()
		{
			if (!string.IsNullOrEmpty(this._documentContent))
			{
				this._document = XmlUtils.CreateXmlDocumentFromContent(this._documentContent);
				return;
			}
			if (string.IsNullOrEmpty(this._documentSource))
			{
				return;
			}
			string text = base.MapPathSecure(this._documentSource);
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			string key = "q" + text;
			this._document = (XmlDocument)internalCache.Get(key);
			if (this._document == null)
			{
				CacheDependency dependencies;
				using (Stream stream = base.OpenFileAndGetDependency(null, text, out dependencies))
				{
					this._document = new XmlDocument();
					this._document.Load(XmlUtils.CreateXmlReader(stream, text));
					internalCache.Insert(key, this._document, new CacheInsertOptions
					{
						Dependencies = dependencies
					});
				}
			}
			XmlDocument document = this._document;
			lock (document)
			{
				this._document = (XmlDocument)this._document.CloneNode(true);
			}
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x000D8F8C File Offset: 0x000D718C
		private void LoadXPathDocument()
		{
			if (!string.IsNullOrEmpty(this._documentContent))
			{
				this._xpathDocument = XmlUtils.CreateXPathDocumentFromContent(this._documentContent);
				return;
			}
			if (string.IsNullOrEmpty(this._documentSource))
			{
				return;
			}
			VirtualPath virtualPath;
			string text;
			base.ResolvePhysicalOrVirtualPath(this._documentSource, out virtualPath, out text);
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			string key = "p" + ((text != null) ? text : virtualPath.VirtualPathString);
			this._xpathDocument = (XPathDocument)internalCache.Get(key);
			if (this._xpathDocument == null)
			{
				CacheDependency cacheDependency;
				using (Stream stream = base.OpenFileAndGetDependency(virtualPath, text, out cacheDependency))
				{
					if (text == null)
					{
						text = virtualPath.MapPath();
					}
					this._xpathDocument = new XPathDocument(XmlUtils.CreateXmlReader(stream, text));
				}
				if (cacheDependency != null)
				{
					using (cacheDependency)
					{
						internalCache.Insert(key, this._xpathDocument, new CacheInsertOptions
						{
							Dependencies = cacheDependency
						});
					}
				}
			}
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x000D9098 File Offset: 0x000D7298
		protected internal override void Render(HtmlTextWriter output)
		{
			if (this._document == null && this._xpathNavigator == null)
			{
				this.LoadXPathDocument();
			}
			this.LoadTransformFromSource();
			if (this._document == null && this._xpathDocument == null && this._xpathNavigator == null)
			{
				return;
			}
			if (this._transform == null)
			{
				this._transform = Xml._identityTransform;
			}
			XmlUrlResolver resolver = null;
			if (HttpRuntime.HasUnmanagedPermission())
			{
				resolver = new XmlUrlResolver();
			}
			IXPathNavigable input;
			if (this._document != null)
			{
				input = this._document;
			}
			else if (this._xpathNavigator != null)
			{
				input = this._xpathNavigator;
			}
			else
			{
				input = this._xpathDocument;
			}
			if (this._compiledTransform != null)
			{
				XmlWriter results = XmlWriter.Create(output);
				this._compiledTransform.Transform(input, this._transformArgumentList, results, null);
				return;
			}
			this._transform.Transform(input, this._transformArgumentList, output, resolver);
		}

		// Token: 0x04002567 RID: 9575
		private XPathNavigator _xpathNavigator;

		// Token: 0x04002568 RID: 9576
		private XmlDocument _document;

		// Token: 0x04002569 RID: 9577
		private XPathDocument _xpathDocument;

		// Token: 0x0400256A RID: 9578
		private XslTransform _transform;

		// Token: 0x0400256B RID: 9579
		private XslCompiledTransform _compiledTransform;

		// Token: 0x0400256C RID: 9580
		private XsltArgumentList _transformArgumentList;

		// Token: 0x0400256D RID: 9581
		private string _documentContent;

		// Token: 0x0400256E RID: 9582
		private string _documentSource;

		// Token: 0x0400256F RID: 9583
		private string _transformSource;

		// Token: 0x04002570 RID: 9584
		private const string identityXslStr = "<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'><xsl:template match=\"/\"> <xsl:copy-of select=\".\"/> </xsl:template> </xsl:stylesheet>";

		// Token: 0x04002571 RID: 9585
		private static XslTransform _identityTransform;
	}
}
