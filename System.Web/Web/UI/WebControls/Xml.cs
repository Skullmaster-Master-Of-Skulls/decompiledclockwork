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
	// Token: 0x0200068D RID: 1677
	[Designer("System.Web.UI.Design.WebControls.XmlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("DocumentSource")]
	[PersistChildren(false, true)]
	[ControlBuilder(typeof(XmlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Xml : Control
	{
		// Token: 0x06005213 RID: 21011 RVA: 0x0014BB00 File Offset: 0x0014AB00
		static Xml()
		{
			XmlTextReader stylesheet = new XmlTextReader(new StringReader("<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'><xsl:template match=\"/\"> <xsl:copy-of select=\".\"/> </xsl:template> </xsl:stylesheet>"));
			Xml._identityTransform = new XslTransform();
			Xml._identityTransform.Load(stylesheet, null, null);
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06005214 RID: 21012 RVA: 0x0014BB34 File Offset: 0x0014AB34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06005215 RID: 21013 RVA: 0x0014BB3C File Offset: 0x0014AB3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x06005216 RID: 21014 RVA: 0x0014BB44 File Offset: 0x0014AB44
		// (set) Token: 0x06005217 RID: 21015 RVA: 0x0014BB5A File Offset: 0x0014AB5A
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

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06005218 RID: 21016 RVA: 0x0014BB78 File Offset: 0x0014AB78
		// (set) Token: 0x06005219 RID: 21017 RVA: 0x0014BB8E File Offset: 0x0014AB8E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x0600521A RID: 21018 RVA: 0x0014BBC5 File Offset: 0x0014ABC5
		// (set) Token: 0x0600521B RID: 21019 RVA: 0x0014BBDB File Offset: 0x0014ABDB
		[UrlProperty]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.XmlUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x0600521C RID: 21020 RVA: 0x0014BC00 File Offset: 0x0014AC00
		// (set) Token: 0x0600521D RID: 21021 RVA: 0x0014BC04 File Offset: 0x0014AC04
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
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

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x0600521E RID: 21022 RVA: 0x0014BC36 File Offset: 0x0014AC36
		// (set) Token: 0x0600521F RID: 21023 RVA: 0x0014BC40 File Offset: 0x0014AC40
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DefaultValue("")]
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

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06005220 RID: 21024 RVA: 0x0014BC72 File Offset: 0x0014AC72
		// (set) Token: 0x06005221 RID: 21025 RVA: 0x0014BC83 File Offset: 0x0014AC83
		[WebSysDescription("Xml_Transform")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public XslTransform Transform
		{
			get
			{
				if (!AppSettings.RestrictXmlControls)
				{
					return this._transform;
				}
				return null;
			}
			set
			{
				if (!AppSettings.RestrictXmlControls)
				{
					this.TransformSource = null;
					this._transform = value;
				}
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06005222 RID: 21026 RVA: 0x0014BC9A File Offset: 0x0014AC9A
		// (set) Token: 0x06005223 RID: 21027 RVA: 0x0014BCA2 File Offset: 0x0014ACA2
		[WebSysDescription("Xml_TransformArgumentList")]
		[Browsable(false)]
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

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06005224 RID: 21028 RVA: 0x0014BCAB File Offset: 0x0014ACAB
		// (set) Token: 0x06005225 RID: 21029 RVA: 0x0014BCC1 File Offset: 0x0014ACC1
		[WebSysDescription("Xml_TransformSource")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.XslUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06005226 RID: 21030 RVA: 0x0014BCD1 File Offset: 0x0014ACD1
		// (set) Token: 0x06005227 RID: 21031 RVA: 0x0014BCD9 File Offset: 0x0014ACD9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Xml_XPathNavigator")]
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

		// Token: 0x06005228 RID: 21032 RVA: 0x0014BD00 File Offset: 0x0014AD00
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

		// Token: 0x06005229 RID: 21033 RVA: 0x0014BD87 File Offset: 0x0014AD87
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x0014BD8F File Offset: 0x0014AD8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			return base.FindControl(id);
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0014BD98 File Offset: 0x0014AD98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0014BDCC File Offset: 0x0014ADCC
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			IDictionary dictionary = new HybridDictionary();
			dictionary["OriginalContent"] = this.ViewState["OriginalContent"];
			return dictionary;
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x0014BDFB File Offset: 0x0014ADFB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return base.HasControls();
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0014BE04 File Offset: 0x0014AE04
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
			CacheInternal cacheInternal = HttpRuntime.CacheInternal;
			string key = "p" + ((text != null) ? text : virtualPath.VirtualPathString);
			object obj = cacheInternal.Get(key);
			if (obj == null)
			{
				CacheDependency cacheDependency;
				using (Stream stream = base.OpenFileAndGetDependency(virtualPath, text, out cacheDependency))
				{
					if (text == null)
					{
						text = virtualPath.MapPath();
					}
					XmlTextReader xmlTextReader = new XmlTextReader(text, stream);
					if (AppSettings.RestrictXmlControls)
					{
						xmlTextReader.ProhibitDtd = true;
						this._compiledTransform = new XslCompiledTransform();
						this._compiledTransform.Load(xmlTextReader, null, null);
					}
					else
					{
						this._transform = new XslTransform();
						this._transform.Load(xmlTextReader);
					}
				}
				if (cacheDependency == null)
				{
					return;
				}
				using (cacheDependency)
				{
					cacheInternal.UtcInsert(key, AppSettings.RestrictXmlControls ? this._compiledTransform : this._transform, cacheDependency);
					return;
				}
			}
			if (AppSettings.RestrictXmlControls)
			{
				this._compiledTransform = (XslCompiledTransform)obj;
				return;
			}
			this._transform = (XslTransform)obj;
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x0014BF5C File Offset: 0x0014AF5C
		private void LoadXmlDocument()
		{
			if (!string.IsNullOrEmpty(this._documentContent))
			{
				this._document = new XmlDocument();
				this._document.LoadXml(this._documentContent);
				return;
			}
			if (string.IsNullOrEmpty(this._documentSource))
			{
				return;
			}
			string text = base.MapPathSecure(this._documentSource);
			CacheInternal cacheInternal = HttpRuntime.CacheInternal;
			string key = "q" + text;
			this._document = (XmlDocument)cacheInternal.Get(key);
			if (this._document == null)
			{
				CacheDependency dependencies;
				using (Stream stream = base.OpenFileAndGetDependency(null, text, out dependencies))
				{
					XmlTextReader reader;
					if (AppSettings.RestrictXmlControls)
					{
						reader = new NoEntitiesXmlReader(text, stream);
					}
					else
					{
						reader = new XmlTextReader(text, stream);
					}
					this._document = new XmlDocument();
					this._document.Load(reader);
					cacheInternal.UtcInsert(key, this._document, dependencies);
				}
			}
			lock (this._document)
			{
				this._document = (XmlDocument)this._document.CloneNode(true);
			}
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x0014C084 File Offset: 0x0014B084
		private void LoadXPathDocument()
		{
			if (!string.IsNullOrEmpty(this._documentContent))
			{
				StringReader textReader = new StringReader(this._documentContent);
				this._xpathDocument = new XPathDocument(textReader);
				return;
			}
			if (string.IsNullOrEmpty(this._documentSource))
			{
				return;
			}
			VirtualPath virtualPath;
			string text;
			base.ResolvePhysicalOrVirtualPath(this._documentSource, out virtualPath, out text);
			CacheInternal cacheInternal = HttpRuntime.CacheInternal;
			string key = "p" + ((text != null) ? text : virtualPath.VirtualPathString);
			this._xpathDocument = (XPathDocument)cacheInternal.Get(key);
			if (this._xpathDocument == null)
			{
				CacheDependency cacheDependency;
				using (Stream stream = base.OpenFileAndGetDependency(virtualPath, text, out cacheDependency))
				{
					if (text == null)
					{
						text = virtualPath.MapPath();
					}
					XmlTextReader reader;
					if (AppSettings.RestrictXmlControls)
					{
						reader = new NoEntitiesXmlReader(text, stream);
					}
					else
					{
						reader = new XmlTextReader(text, stream);
					}
					this._xpathDocument = new XPathDocument(reader);
				}
				if (cacheDependency != null)
				{
					using (cacheDependency)
					{
						cacheInternal.UtcInsert(key, this._xpathDocument, cacheDependency);
					}
				}
			}
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x0014C1A0 File Offset: 0x0014B1A0
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
			if (AppSettings.RestrictXmlControls && this._compiledTransform != null)
			{
				this._compiledTransform.Transform(input, this._transformArgumentList, output);
				return;
			}
			this._transform.Transform(input, this._transformArgumentList, output, resolver);
		}

		// Token: 0x04002DDD RID: 11741
		private const string identityXslStr = "<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'><xsl:template match=\"/\"> <xsl:copy-of select=\".\"/> </xsl:template> </xsl:stylesheet>";

		// Token: 0x04002DDE RID: 11742
		private XPathNavigator _xpathNavigator;

		// Token: 0x04002DDF RID: 11743
		private XmlDocument _document;

		// Token: 0x04002DE0 RID: 11744
		private XPathDocument _xpathDocument;

		// Token: 0x04002DE1 RID: 11745
		private XslTransform _transform;

		// Token: 0x04002DE2 RID: 11746
		private XslCompiledTransform _compiledTransform;

		// Token: 0x04002DE3 RID: 11747
		private XsltArgumentList _transformArgumentList;

		// Token: 0x04002DE4 RID: 11748
		private string _documentContent;

		// Token: 0x04002DE5 RID: 11749
		private string _documentSource;

		// Token: 0x04002DE6 RID: 11750
		private string _transformSource;

		// Token: 0x04002DE7 RID: 11751
		private static XslTransform _identityTransform;
	}
}
