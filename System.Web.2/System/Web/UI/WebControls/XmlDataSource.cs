using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Util;
using System.Xml;
using System.Xml.Xsl;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000522 RID: 1314
	[DefaultEvent("Transforming")]
	[DefaultProperty("DataFile")]
	[Designer("System.Web.UI.Design.WebControls.XmlDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(XmlDataSource))]
	[WebSysDescription("XmlDataSource_Description")]
	[WebSysDisplayName("XmlDataSource_DisplayName")]
	public class XmlDataSource : HierarchicalDataSourceControl, IDataSource, IListSource
	{
		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x0600427F RID: 17023 RVA: 0x000D915F File Offset: 0x000D735F
		private DataSourceCache Cache
		{
			get
			{
				if (this._cache == null)
				{
					this._cache = new DataSourceCache();
					this._cache.Enabled = true;
				}
				return this._cache;
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x000D9186 File Offset: 0x000D7386
		// (set) Token: 0x06004281 RID: 17025 RVA: 0x000D9193 File Offset: 0x000D7393
		[DefaultValue(0)]
		[TypeConverter(typeof(DataSourceCacheDurationConverter))]
		[WebCategory("Cache")]
		[WebSysDescription("DataSourceCache_Duration")]
		public virtual int CacheDuration
		{
			get
			{
				return this.Cache.Duration;
			}
			set
			{
				this.Cache.Duration = value;
			}
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06004282 RID: 17026 RVA: 0x000D91A1 File Offset: 0x000D73A1
		// (set) Token: 0x06004283 RID: 17027 RVA: 0x000D91AE File Offset: 0x000D73AE
		[DefaultValue(DataSourceCacheExpiry.Absolute)]
		[WebCategory("Cache")]
		[WebSysDescription("DataSourceCache_ExpirationPolicy")]
		public virtual DataSourceCacheExpiry CacheExpirationPolicy
		{
			get
			{
				return this.Cache.ExpirationPolicy;
			}
			set
			{
				this.Cache.ExpirationPolicy = value;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x000D91BC File Offset: 0x000D73BC
		// (set) Token: 0x06004285 RID: 17029 RVA: 0x000D91C9 File Offset: 0x000D73C9
		[DefaultValue("")]
		[WebCategory("Cache")]
		[WebSysDescription("DataSourceCache_KeyDependency")]
		public virtual string CacheKeyDependency
		{
			get
			{
				return this.Cache.KeyDependency;
			}
			set
			{
				this.Cache.KeyDependency = value;
			}
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06004286 RID: 17030 RVA: 0x000D91D7 File Offset: 0x000D73D7
		// (set) Token: 0x06004287 RID: 17031 RVA: 0x000D91F7 File Offset: 0x000D73F7
		[DefaultValue("")]
		[WebCategory("Cache")]
		[WebSysDescription("XmlDataSource_CacheKeyContext")]
		public virtual string CacheKeyContext
		{
			get
			{
				return ((string)this.ViewState["CacheKeyContext "]) ?? string.Empty;
			}
			set
			{
				this.ViewState["CacheKeyContext "] = value;
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x000D920A File Offset: 0x000D740A
		// (set) Token: 0x06004289 RID: 17033 RVA: 0x000D9220 File Offset: 0x000D7420
		[DefaultValue("")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter("System.ComponentModel.MultilineStringConverter,System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[WebCategory("Data")]
		[WebSysDescription("XmlDataSource_Data")]
		public virtual string Data
		{
			get
			{
				if (this._data == null)
				{
					return string.Empty;
				}
				return this._data;
			}
			set
			{
				if (value != null)
				{
					value = value.Trim();
				}
				if (this.Data != value)
				{
					if (this._disallowChanges)
					{
						throw new InvalidOperationException(SR.GetString("XmlDataSource_CannotChangeWhileLoading", new object[]
						{
							"Data",
							this.ID
						}));
					}
					this._data = value;
					this._xmlDocument = null;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x0600428A RID: 17034 RVA: 0x000D928E File Offset: 0x000D748E
		// (set) Token: 0x0600428B RID: 17035 RVA: 0x000D92A4 File Offset: 0x000D74A4
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.XmlDataFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Data")]
		[WebSysDescription("XmlDataSource_DataFile")]
		public virtual string DataFile
		{
			get
			{
				if (this._dataFile == null)
				{
					return string.Empty;
				}
				return this._dataFile;
			}
			set
			{
				if (this.DataFile != value)
				{
					if (this._disallowChanges)
					{
						throw new InvalidOperationException(SR.GetString("XmlDataSource_CannotChangeWhileLoading", new object[]
						{
							"DataFile",
							this.ID
						}));
					}
					this._dataFile = value;
					this._xmlDocument = null;
					this._writeableDataFile = null;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x0600428C RID: 17036 RVA: 0x000D930E File Offset: 0x000D750E
		// (set) Token: 0x0600428D RID: 17037 RVA: 0x000D931B File Offset: 0x000D751B
		[DefaultValue(true)]
		[WebCategory("Cache")]
		[WebSysDescription("DataSourceCache_Enabled")]
		public virtual bool EnableCaching
		{
			get
			{
				return this.Cache.Enabled;
			}
			set
			{
				this.Cache.Enabled = value;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x000D9329 File Offset: 0x000D7529
		internal bool IsModifiable
		{
			get
			{
				return string.IsNullOrEmpty(this.TransformFile) && string.IsNullOrEmpty(this.Transform) && !string.IsNullOrEmpty(this.WriteableDataFile);
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x0600428F RID: 17039 RVA: 0x000D9355 File Offset: 0x000D7555
		// (set) Token: 0x06004290 RID: 17040 RVA: 0x000D936C File Offset: 0x000D756C
		[DefaultValue("")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter("System.ComponentModel.MultilineStringConverter,System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[WebCategory("Data")]
		[WebSysDescription("XmlDataSource_Transform")]
		public virtual string Transform
		{
			get
			{
				if (this._transform == null)
				{
					return string.Empty;
				}
				return this._transform;
			}
			set
			{
				if (value != null)
				{
					value = value.Trim();
				}
				if (this.Transform != value)
				{
					if (this._disallowChanges)
					{
						throw new InvalidOperationException(SR.GetString("XmlDataSource_CannotChangeWhileLoading", new object[]
						{
							"Transform",
							this.ID
						}));
					}
					this._transform = value;
					this._xmlDocument = null;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x000D93DA File Offset: 0x000D75DA
		// (set) Token: 0x06004292 RID: 17042 RVA: 0x000D93E2 File Offset: 0x000D75E2
		[Browsable(false)]
		public virtual XsltArgumentList TransformArgumentList
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

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06004293 RID: 17043 RVA: 0x000D93EB File Offset: 0x000D75EB
		// (set) Token: 0x06004294 RID: 17044 RVA: 0x000D9404 File Offset: 0x000D7604
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.XslTransformFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Data")]
		[WebSysDescription("XmlDataSource_TransformFile")]
		public virtual string TransformFile
		{
			get
			{
				if (this._transformFile == null)
				{
					return string.Empty;
				}
				return this._transformFile;
			}
			set
			{
				if (this.TransformFile != value)
				{
					if (this._disallowChanges)
					{
						throw new InvalidOperationException(SR.GetString("XmlDataSource_CannotChangeWhileLoading", new object[]
						{
							"TransformFile",
							this.ID
						}));
					}
					this._transformFile = value;
					this._xmlDocument = null;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x000D9467 File Offset: 0x000D7667
		private string WriteableDataFile
		{
			get
			{
				if (this._writeableDataFile == null)
				{
					this._writeableDataFile = this.GetWriteableDataFile();
				}
				return this._writeableDataFile;
			}
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x000D9483 File Offset: 0x000D7683
		// (set) Token: 0x06004297 RID: 17047 RVA: 0x000D949C File Offset: 0x000D769C
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("XmlDataSource_XPath")]
		public virtual string XPath
		{
			get
			{
				if (this._xPath == null)
				{
					return string.Empty;
				}
				return this._xPath;
			}
			set
			{
				if (this.XPath != value)
				{
					if (this._disallowChanges)
					{
						throw new InvalidOperationException(SR.GetString("XmlDataSource_CannotChangeWhileLoading", new object[]
						{
							"XPath",
							this.ID
						}));
					}
					this._xPath = value;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1400010C RID: 268
		// (add) Token: 0x06004298 RID: 17048 RVA: 0x000D94F8 File Offset: 0x000D76F8
		// (remove) Token: 0x06004299 RID: 17049 RVA: 0x000D950B File Offset: 0x000D770B
		[WebCategory("Data")]
		[WebSysDescription("XmlDataSource_Transforming")]
		public event EventHandler Transforming
		{
			add
			{
				base.Events.AddHandler(XmlDataSource.EventTransforming, value);
			}
			remove
			{
				base.Events.RemoveHandler(XmlDataSource.EventTransforming, value);
			}
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x000D9520 File Offset: 0x000D7720
		internal string CreateCacheKey()
		{
			StringBuilder stringBuilder = new StringBuilder("u", 1024);
			stringBuilder.Append(base.GetType().GetHashCode().ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(this.CacheDuration.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(':');
			stringBuilder.Append(((int)this.CacheExpirationPolicy).ToString(CultureInfo.InvariantCulture));
			bool flag = false;
			if (!string.IsNullOrEmpty(this.CacheKeyContext))
			{
				stringBuilder.Append(':');
				stringBuilder.Append(this.CacheKeyContext);
			}
			if (this.DataFile.Length > 0)
			{
				stringBuilder.Append(':');
				stringBuilder.Append(this.DataFile);
			}
			else if (this.Data.Length > 0)
			{
				flag = true;
			}
			if (this.TransformFile.Length > 0)
			{
				stringBuilder.Append(':');
				stringBuilder.Append(this.TransformFile);
			}
			else if (this.Transform.Length > 0)
			{
				flag = true;
			}
			if (flag)
			{
				if (this.Page != null)
				{
					stringBuilder.Append(':');
					stringBuilder.Append(this.Page.GetType().AssemblyQualifiedName);
				}
				stringBuilder.Append(':');
				string uniqueID = this.UniqueID;
				if (string.IsNullOrEmpty(uniqueID))
				{
					throw new InvalidOperationException(SR.GetString("XmlDataSource_NeedUniqueIDForCache"));
				}
				stringBuilder.Append(uniqueID);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x000D968B File Offset: 0x000D788B
		protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
		{
			return new XmlHierarchicalDataSourceView(this, viewPath);
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x000D9694 File Offset: 0x000D7894
		private XmlReader GetReader(string path, string content, out CacheDependency cacheDependency)
		{
			if (path.Length != 0)
			{
				Uri uri;
				bool flag = Uri.TryCreate(path, UriKind.Absolute, out uri);
				if (flag && uri.Scheme == Uri.UriSchemeHttp)
				{
					if (!HttpRuntime.HasWebPermission(uri))
					{
						throw new InvalidOperationException(SR.GetString("XmlDataSource_NoWebPermission", new object[]
						{
							uri.PathAndQuery,
							this.ID
						}));
					}
					cacheDependency = null;
					return XmlUtils.CreateXmlReader(path);
				}
				else
				{
					VirtualPath virtualPath;
					string physicalPath;
					base.ResolvePhysicalOrVirtualPath(path, out virtualPath, out physicalPath);
					if (virtualPath != null && base.DesignMode)
					{
						throw new NotSupportedException(SR.GetString("XmlDataSource_DesignTimeRelativePathsNotSupported", new object[]
						{
							this.ID
						}));
					}
					Stream datastream = base.OpenFileAndGetDependency(virtualPath, physicalPath, out cacheDependency);
					return XmlUtils.CreateXmlReader(datastream);
				}
			}
			else
			{
				cacheDependency = null;
				content = content.Trim();
				if (content.Length == 0)
				{
					return null;
				}
				return XmlUtils.CreateXmlReader(new StringReader(content));
			}
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x000D9774 File Offset: 0x000D7974
		private string GetWriteableDataFile()
		{
			if (this.DataFile.Length == 0)
			{
				return null;
			}
			Uri uri;
			bool flag = Uri.TryCreate(this.DataFile, UriKind.Absolute, out uri);
			if (flag && uri.Scheme == Uri.UriSchemeHttp)
			{
				return null;
			}
			if (HostingEnvironment.UsingMapPathBasedVirtualPathProvider)
			{
				VirtualPath virtualPath;
				string text;
				base.ResolvePhysicalOrVirtualPath(this.DataFile, out virtualPath, out text);
				if (text == null)
				{
					text = virtualPath.MapPathInternal(base.TemplateControlVirtualDirectory, true);
				}
				return text;
			}
			return null;
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x000D97E4 File Offset: 0x000D79E4
		public XmlDocument GetXmlDocument()
		{
			string text = null;
			if (!this._cacheLookupDone && this.Cache.Enabled)
			{
				text = this.CreateCacheKey();
				this._xmlDocument = (this.Cache.LoadDataFromCache(text) as XmlDocument);
				this._cacheLookupDone = true;
			}
			if (this._xmlDocument == null)
			{
				this._xmlDocument = new XmlDocument();
				CacheDependency cacheDependency;
				CacheDependency cacheDependency2;
				this.PopulateXmlDocument(this._xmlDocument, out cacheDependency, out cacheDependency2);
				if (text != null)
				{
					CacheDependency dependency;
					if (cacheDependency != null)
					{
						if (cacheDependency2 != null)
						{
							AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
							aggregateCacheDependency.Add(new CacheDependency[]
							{
								cacheDependency,
								cacheDependency2
							});
							dependency = aggregateCacheDependency;
						}
						else
						{
							dependency = cacheDependency;
						}
					}
					else
					{
						dependency = cacheDependency2;
					}
					this.Cache.SaveDataToCache(text, this._xmlDocument, dependency);
				}
			}
			return this._xmlDocument;
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x000D989C File Offset: 0x000D7A9C
		private void PopulateXmlDocument(XmlDocument document, out CacheDependency dataCacheDependency, out CacheDependency transformCacheDependency)
		{
			XmlReader xmlReader = null;
			XmlReader xmlReader2 = null;
			XmlReader xmlReader3 = null;
			try
			{
				this._disallowChanges = true;
				xmlReader = this.GetReader(this.TransformFile, this.Transform, out transformCacheDependency);
				if (xmlReader != null)
				{
					xmlReader3 = this.GetReader(this.DataFile, this.Data, out dataCacheDependency);
					XslTransform xslTransform = XmlUtils.CreateXslTransform(xmlReader, null);
					if (xslTransform != null)
					{
						this.OnTransforming(EventArgs.Empty);
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(xmlReader3);
						xmlReader2 = xslTransform.Transform(xmlDocument, this._transformArgumentList, null);
						document.Load(xmlReader2);
						return;
					}
					XslCompiledTransform xslCompiledTransform = XmlUtils.CreateXslCompiledTransform(xmlReader);
					this.OnTransforming(EventArgs.Empty);
					using (MemoryStream memoryStream = new MemoryStream())
					{
						XmlWriter results = XmlWriter.Create(memoryStream);
						xslCompiledTransform.Transform(xmlReader3, this._transformArgumentList, results, null);
						document.Load(XmlUtils.CreateXmlReader(memoryStream));
						return;
					}
				}
				xmlReader2 = this.GetReader(this.DataFile, this.Data, out dataCacheDependency);
				document.Load(xmlReader2);
			}
			finally
			{
				this._disallowChanges = false;
				if (xmlReader2 != null)
				{
					xmlReader2.Close();
				}
				if (xmlReader3 != null)
				{
					xmlReader3.Close();
				}
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x000D99D0 File Offset: 0x000D7BD0
		protected virtual void OnTransforming(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[XmlDataSource.EventTransforming];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x000D9A00 File Offset: 0x000D7C00
		public void Save()
		{
			if (!this.IsModifiable)
			{
				throw new InvalidOperationException(SR.GetString("XmlDataSource_SaveNotAllowed", new object[]
				{
					this.ID
				}));
			}
			string writeableDataFile = this.WriteableDataFile;
			HttpRuntime.CheckFilePermission(writeableDataFile, true);
			this.GetXmlDocument().Save(writeableDataFile);
		}

		// Token: 0x1400010D RID: 269
		// (add) Token: 0x060042A2 RID: 17058 RVA: 0x000C4EE3 File Offset: 0x000C30E3
		// (remove) Token: 0x060042A3 RID: 17059 RVA: 0x000C4EEC File Offset: 0x000C30EC
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
				((IHierarchicalDataSource)this).DataSourceChanged += value;
			}
			remove
			{
				((IHierarchicalDataSource)this).DataSourceChanged -= value;
			}
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x000D9A4E File Offset: 0x000D7C4E
		DataSourceView IDataSource.GetView(string viewName)
		{
			if (viewName.Length == 0)
			{
				viewName = "DefaultView";
			}
			return new XmlDataSourceView(this, viewName);
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x000D9A66 File Offset: 0x000D7C66
		ICollection IDataSource.GetViewNames()
		{
			if (this._viewNames == null)
			{
				this._viewNames = new string[]
				{
					"DefaultView"
				};
			}
			return this._viewNames;
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x00061246 File Offset: 0x0005F446
		bool IListSource.ContainsListCollection
		{
			get
			{
				return !base.DesignMode && ListSourceHelper.ContainsListCollection(this);
			}
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x00061258 File Offset: 0x0005F458
		IList IListSource.GetList()
		{
			if (base.DesignMode)
			{
				return null;
			}
			return ListSourceHelper.GetList(this);
		}

		// Token: 0x04002572 RID: 9586
		private static readonly object EventTransforming = new object();

		// Token: 0x04002573 RID: 9587
		private const string DefaultViewName = "DefaultView";

		// Token: 0x04002574 RID: 9588
		private DataSourceCache _cache;

		// Token: 0x04002575 RID: 9589
		private bool _cacheLookupDone;

		// Token: 0x04002576 RID: 9590
		private bool _disallowChanges;

		// Token: 0x04002577 RID: 9591
		private XsltArgumentList _transformArgumentList;

		// Token: 0x04002578 RID: 9592
		private ICollection _viewNames;

		// Token: 0x04002579 RID: 9593
		private XmlDocument _xmlDocument;

		// Token: 0x0400257A RID: 9594
		private string _writeableDataFile;

		// Token: 0x0400257B RID: 9595
		private string _data;

		// Token: 0x0400257C RID: 9596
		private string _dataFile;

		// Token: 0x0400257D RID: 9597
		private string _transform;

		// Token: 0x0400257E RID: 9598
		private string _transformFile;

		// Token: 0x0400257F RID: 9599
		private string _xPath;
	}
}
