using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web.Caching;
using System.Web.Util;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000374 RID: 884
	[DefaultEvent("AdCreated")]
	[DefaultProperty("AdvertisementFile")]
	[Designer("System.Web.UI.Design.WebControls.AdRotatorDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxData("<{0}:AdRotator runat=\"server\"></{0}:AdRotator>")]
	public class AdRotator : DataBoundControl
	{
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x00083380 File Offset: 0x00081580
		// (set) Token: 0x060028AA RID: 10410 RVA: 0x00083396 File Offset: 0x00081596
		[Bindable(true)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.XmlUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("AdRotator_AdvertisementFile")]
		public string AdvertisementFile
		{
			get
			{
				if (this._advertisementFile != null)
				{
					return this._advertisementFile;
				}
				return string.Empty;
			}
			set
			{
				this._advertisementFile = value;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x000833A0 File Offset: 0x000815A0
		// (set) Token: 0x060028AC RID: 10412 RVA: 0x000833CD File Offset: 0x000815CD
		[WebCategory("Behavior")]
		[DefaultValue("AlternateText")]
		[WebSysDescription("AdRotator_AlternateTextField")]
		public string AlternateTextField
		{
			get
			{
				string text = (string)this.ViewState["AlternateTextField"];
				if (text == null)
				{
					return "AlternateText";
				}
				return text;
			}
			set
			{
				this.ViewState["AlternateTextField"] = value;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x000833E0 File Offset: 0x000815E0
		internal string BaseUrl
		{
			get
			{
				if (this._baseUrl == null)
				{
					string virtualPathString = base.TemplateControlVirtualDirectory.VirtualPathString;
					string text = null;
					if (!string.IsNullOrEmpty(this.AdvertisementFile))
					{
						string path = UrlPath.Combine(virtualPathString, this.AdvertisementFile);
						text = UrlPath.GetDirectory(path);
					}
					this._baseUrl = string.Empty;
					if (text != null)
					{
						this._baseUrl = text;
					}
					if (this._baseUrl.Length == 0)
					{
						this._baseUrl = virtualPathString;
					}
				}
				return this._baseUrl;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x00083455 File Offset: 0x00081655
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x00083460 File Offset: 0x00081660
		// (set) Token: 0x060028B0 RID: 10416 RVA: 0x0008348D File Offset: 0x0008168D
		[WebCategory("Behavior")]
		[DefaultValue("ImageUrl")]
		[WebSysDescription("AdRotator_ImageUrlField")]
		public string ImageUrlField
		{
			get
			{
				string text = (string)this.ViewState["ImageUrlField"];
				if (text == null)
				{
					return "ImageUrl";
				}
				return text;
			}
			set
			{
				this.ViewState["ImageUrlField"] = value;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x000834A0 File Offset: 0x000816A0
		private bool IsTargetSet
		{
			get
			{
				return this.ViewState["Target"] != null;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000834B5 File Offset: 0x000816B5
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x000834BD File Offset: 0x000816BD
		internal bool IsPostCacheAdHelper
		{
			get
			{
				return this._isPostCacheAdHelper;
			}
			set
			{
				this._isPostCacheAdHelper = value;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x000834C8 File Offset: 0x000816C8
		// (set) Token: 0x060028B5 RID: 10421 RVA: 0x000834F5 File Offset: 0x000816F5
		[Bindable(true)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("AdRotator_KeywordFilter")]
		public string KeywordFilter
		{
			get
			{
				string text = (string)this.ViewState["KeywordFilter"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.ViewState.Remove("KeywordFilter");
					return;
				}
				this.ViewState["KeywordFilter"] = value.Trim();
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x00083528 File Offset: 0x00081728
		// (set) Token: 0x060028B7 RID: 10423 RVA: 0x00083555 File Offset: 0x00081755
		[WebCategory("Behavior")]
		[DefaultValue("NavigateUrl")]
		[WebSysDescription("AdRotator_NavigateUrlField")]
		public string NavigateUrlField
		{
			get
			{
				string text = (string)this.ViewState["NavigateUrlField"];
				if (text == null)
				{
					return "NavigateUrl";
				}
				return text;
			}
			set
			{
				this.ViewState["NavigateUrlField"] = value;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x00083568 File Offset: 0x00081768
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x00083570 File Offset: 0x00081770
		private AdCreatedEventArgs SelectedAdArgs
		{
			get
			{
				return this._adCreatedEventArgs;
			}
			set
			{
				this._adCreatedEventArgs = value;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x0008357C File Offset: 0x0008177C
		// (set) Token: 0x060028BB RID: 10427 RVA: 0x000835A9 File Offset: 0x000817A9
		[Bindable(true)]
		[WebCategory("Behavior")]
		[DefaultValue("_top")]
		[WebSysDescription("AdRotator_Target")]
		[TypeConverter(typeof(TargetConverter))]
		public string Target
		{
			get
			{
				string text = (string)this.ViewState["Target"];
				if (text != null)
				{
					return text;
				}
				return "_top";
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x000835BC File Offset: 0x000817BC
		public override string UniqueID
		{
			get
			{
				if (this._uniqueID == null)
				{
					this._uniqueID = base.UniqueID;
				}
				return this._uniqueID;
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x060028BE RID: 10430 RVA: 0x000835D8 File Offset: 0x000817D8
		// (remove) Token: 0x060028BF RID: 10431 RVA: 0x000835EB File Offset: 0x000817EB
		[WebCategory("Action")]
		[WebSysDescription("AdRotator_OnAdCreated")]
		public event AdCreatedEventHandler AdCreated
		{
			add
			{
				base.Events.AddHandler(AdRotator.EventAdCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(AdRotator.EventAdCreated, value);
			}
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x00083600 File Offset: 0x00081800
		private void CheckOnlyOneDataSource()
		{
			int num = (this.AdvertisementFile.Length > 0) ? 1 : 0;
			num += ((this.DataSourceID.Length > 0) ? 1 : 0);
			num += ((this.DataSource != null) ? 1 : 0);
			if (num > 1)
			{
				throw new HttpException(SR.GetString("AdRotator_only_one_datasource", new object[]
				{
					this.ID
				}));
			}
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x00083668 File Offset: 0x00081868
		internal void CopyFrom(AdRotator adRotator)
		{
			this._adRecs = adRotator._adRecs;
			this.AccessKey = adRotator.AccessKey;
			this.AlternateTextField = adRotator.AlternateTextField;
			this.Enabled = adRotator.Enabled;
			this.ImageUrlField = adRotator.ImageUrlField;
			this.NavigateUrlField = adRotator.NavigateUrlField;
			this.TabIndex = adRotator.TabIndex;
			this.Target = adRotator.Target;
			this.ToolTip = adRotator.ToolTip;
			string id = adRotator.ID;
			if (!string.IsNullOrEmpty(id))
			{
				this.ID = adRotator.ClientID;
			}
			this._uniqueID = adRotator.UniqueID;
			this._baseUrl = adRotator.BaseUrl;
			if (adRotator.HasAttributes)
			{
				foreach (object obj in adRotator.Attributes.Keys)
				{
					string key = (string)obj;
					base.Attributes[key] = adRotator.Attributes[key];
				}
			}
			if (adRotator.ControlStyleCreated)
			{
				base.ControlStyle.CopyFrom(adRotator.ControlStyle);
			}
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x00083798 File Offset: 0x00081998
		private ArrayList CreateAutoGeneratedFields(IEnumerable dataSource)
		{
			if (dataSource == null)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (dataSource is ITypedList)
			{
				propertyDescriptorCollection = ((ITypedList)dataSource).GetItemProperties(new PropertyDescriptor[0]);
			}
			if (propertyDescriptorCollection == null)
			{
				IEnumerator enumerator = dataSource.GetEnumerator();
				if (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (this.IsBindableType(obj.GetType()))
					{
						throw new HttpException(SR.GetString("AdRotator_expect_records_with_advertisement_properties", new object[]
						{
							this.ID,
							obj.GetType()
						}));
					}
					propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
				}
			}
			if (propertyDescriptorCollection != null && propertyDescriptorCollection.Count > 0)
			{
				foreach (object obj2 in propertyDescriptorCollection)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
					if (this.IsBindableType(propertyDescriptor.PropertyType))
					{
						arrayList.Add(propertyDescriptor.Name);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x00083894 File Offset: 0x00081A94
		internal bool DoPostCacheSubstitutionAsNeeded(HtmlTextWriter writer)
		{
			if (!this.IsPostCacheAdHelper && this.SelectedAdArgs == null && this.Page.Response.HasCachePolicy && this.Page.Response.Cache.GetCacheability() != (HttpCacheability)6)
			{
				AdPostCacheSubstitution adPostCacheSubstitution = new AdPostCacheSubstitution(this);
				adPostCacheSubstitution.RegisterPostCacheCallBack(this.Context, this.Page, writer);
				return true;
			}
			return false;
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000838F8 File Offset: 0x00081AF8
		private AdCreatedEventArgs GetAdCreatedEventArgs()
		{
			IDictionary adProperties = this.SelectAdFromRecords();
			return new AdCreatedEventArgs(adProperties, this.ImageUrlField, this.NavigateUrlField, this.AlternateTextField);
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x00083928 File Offset: 0x00081B28
		private AdRotator.AdRec[] GetDataSourceData(IEnumerable dataSource)
		{
			ArrayList arrayList = this.CreateAutoGeneratedFields(dataSource);
			ArrayList arrayList2 = new ArrayList();
			IEnumerator enumerator = dataSource.GetEnumerator();
			while (enumerator.MoveNext())
			{
				IDictionary dictionary = null;
				foreach (object obj in arrayList)
				{
					string text = (string)obj;
					if (dictionary == null)
					{
						dictionary = new HybridDictionary();
					}
					dictionary.Add(text, DataBinder.GetPropertyValue(enumerator.Current, text));
				}
				if (dictionary != null)
				{
					arrayList2.Add(dictionary);
				}
			}
			return this.SetAdRecs(arrayList2);
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000839D0 File Offset: 0x00081BD0
		private AdRotator.AdRec[] GetFileData(string fileName)
		{
			VirtualPath virtualPath;
			string text;
			base.ResolvePhysicalOrVirtualPath(fileName, out virtualPath, out text);
			string key = "n" + ((!string.IsNullOrEmpty(text)) ? text : virtualPath.VirtualPathString);
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			AdRotator.AdRec[] array = internalCache.Get(key) as AdRotator.AdRec[];
			if (array == null)
			{
				CacheDependency cacheDependency;
				try
				{
					using (Stream stream = base.OpenFileAndGetDependency(virtualPath, text, out cacheDependency))
					{
						array = this.LoadStream(stream);
					}
				}
				catch (Exception ex)
				{
					if (!string.IsNullOrEmpty(text) && HttpRuntime.HasPathDiscoveryPermission(text))
					{
						throw new HttpException(SR.GetString("AdRotator_cant_open_file", new object[]
						{
							this.ID,
							ex.Message
						}));
					}
					throw new HttpException(SR.GetString("AdRotator_cant_open_file_no_permission", new object[]
					{
						this.ID
					}));
				}
				if (cacheDependency != null)
				{
					using (cacheDependency)
					{
						internalCache.Insert(key, array, new CacheInsertOptions
						{
							Dependencies = cacheDependency
						});
					}
				}
			}
			return array;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x00083AFC File Offset: 0x00081CFC
		private static int GetRandomNumber(int maxValue)
		{
			if (AdRotator._random == null)
			{
				AdRotator._random = new Random();
			}
			return AdRotator._random.Next(maxValue) + 1;
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x00083B1C File Offset: 0x00081D1C
		private AdRotator.AdRec[] GetXmlDataSourceData(XmlDataSource xmlDataSource)
		{
			XmlDocument xmlDocument = xmlDataSource.GetXmlDocument();
			if (xmlDocument == null)
			{
				return null;
			}
			return this.LoadXmlDocument(xmlDocument);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x00083B3C File Offset: 0x00081D3C
		private bool IsBindableType(Type type)
		{
			return type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type == typeof(decimal);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x00083B7C File Offset: 0x00081D7C
		private bool IsOnAdCreatedOverridden()
		{
			bool result = false;
			Type type = base.GetType();
			if (type != AdRotator._adrotatorType)
			{
				MethodInfo method = type.GetMethod("OnAdCreated", BindingFlags.Instance | BindingFlags.NonPublic, null, AdRotator._AdCreatedParameterTypes, null);
				if (method.DeclaringType != AdRotator._adrotatorType)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x00083BCC File Offset: 0x00081DCC
		private AdRotator.AdRec[] LoadFromXmlReader(XmlReader reader)
		{
			ArrayList arrayList = new ArrayList();
			while (reader.Read())
			{
				if (reader.Name == "Advertisements")
				{
					if (reader.Depth != 0)
					{
						return null;
					}
					IL_A7:
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element && reader.Name == "Ad" && reader.Depth == 1)
						{
							IDictionary dictionary = null;
							reader.Read();
							while (reader.NodeType != XmlNodeType.EndElement)
							{
								if (reader.NodeType == XmlNodeType.Element && !reader.IsEmptyElement)
								{
									if (dictionary == null)
									{
										dictionary = new HybridDictionary();
									}
									dictionary.Add(reader.LocalName, reader.ReadString());
								}
								reader.Skip();
							}
							if (dictionary != null)
							{
								arrayList.Add(dictionary);
							}
						}
					}
					return this.SetAdRecs(arrayList);
				}
			}
			goto IL_A7;
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x00083C94 File Offset: 0x00081E94
		private AdRotator.AdRec[] LoadStream(Stream stream)
		{
			AdRotator.AdRec[] array = null;
			try
			{
				XmlReader reader = XmlUtils.CreateXmlReader(stream);
				array = this.LoadFromXmlReader(reader);
			}
			catch (Exception ex)
			{
				throw new HttpException(SR.GetString("AdRotator_parse_error", new object[]
				{
					this.ID,
					ex.Message
				}), ex);
			}
			if (array == null)
			{
				throw new HttpException(SR.GetString("AdRotator_no_advertisements", new object[]
				{
					this.ID,
					this.AdvertisementFile
				}));
			}
			return array;
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x00083D1C File Offset: 0x00081F1C
		private AdRotator.AdRec[] LoadXmlDocument(XmlDocument doc)
		{
			ArrayList arrayList = new ArrayList();
			if (doc.DocumentElement != null && doc.DocumentElement.LocalName == "Advertisements")
			{
				for (XmlNode xmlNode = doc.DocumentElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					IDictionary dictionary = null;
					if (xmlNode.LocalName.Equals("Ad"))
					{
						for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
						{
							if (xmlNode2.NodeType == XmlNodeType.Element)
							{
								if (dictionary == null)
								{
									dictionary = new HybridDictionary();
								}
								dictionary.Add(xmlNode2.LocalName, xmlNode2.InnerText);
							}
						}
					}
					if (dictionary != null)
					{
						arrayList.Add(dictionary);
					}
				}
			}
			return this.SetAdRecs(arrayList);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x00083DCF File Offset: 0x00081FCF
		private bool MatchingAd(AdRotator.AdRec adRec, string keywordFilter)
		{
			return string.Equals(keywordFilter, adRec.keyword, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x00083DE0 File Offset: 0x00081FE0
		protected virtual void OnAdCreated(AdCreatedEventArgs e)
		{
			AdCreatedEventHandler adCreatedEventHandler = (AdCreatedEventHandler)base.Events[AdRotator.EventAdCreated];
			if (adCreatedEventHandler != null)
			{
				adCreatedEventHandler(this, e);
			}
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x00083E0E File Offset: 0x0008200E
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.RequiresDataBinding = true;
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x00083E20 File Offset: 0x00082020
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this._adRecs == null && this.AdvertisementFile.Length > 0)
			{
				this.PerformAdFileBinding();
			}
			if (base.Events[AdRotator.EventAdCreated] != null || this.IsOnAdCreatedOverridden())
			{
				this.SelectedAdArgs = this.GetAdCreatedEventArgs();
				this.OnAdCreated(this.SelectedAdArgs);
			}
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x00083E82 File Offset: 0x00082082
		private void PerformAdFileBinding()
		{
			this.OnDataBinding(EventArgs.Empty);
			this._adRecs = this.GetFileData(this.AdvertisementFile);
			this.OnDataBound(EventArgs.Empty);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x00083EAC File Offset: 0x000820AC
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			if (data != null)
			{
				object dataSource = this.DataSource;
				XmlDataSource xmlDataSource;
				if (dataSource != null)
				{
					xmlDataSource = (dataSource as XmlDataSource);
				}
				else
				{
					xmlDataSource = (this.GetDataSource() as XmlDataSource);
				}
				if (xmlDataSource != null)
				{
					this._adRecs = this.GetXmlDataSourceData(xmlDataSource);
					return;
				}
				this._adRecs = this.GetDataSourceData(data);
			}
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x00083EFB File Offset: 0x000820FB
		protected override void PerformSelect()
		{
			this.CheckOnlyOneDataSource();
			if (this.AdvertisementFile.Length > 0)
			{
				this.PerformAdFileBinding();
				return;
			}
			base.PerformSelect();
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x00083F20 File Offset: 0x00082120
		internal AdCreatedEventArgs PickAd()
		{
			AdCreatedEventArgs adCreatedEventArgs = this.SelectedAdArgs;
			if (adCreatedEventArgs == null)
			{
				adCreatedEventArgs = this.GetAdCreatedEventArgs();
			}
			adCreatedEventArgs.ImageUrl = this.ResolveAdRotatorUrl(this.BaseUrl, adCreatedEventArgs.ImageUrl);
			adCreatedEventArgs.NavigateUrl = this.ResolveAdRotatorUrl(this.BaseUrl, adCreatedEventArgs.NavigateUrl);
			return adCreatedEventArgs;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x00083F70 File Offset: 0x00082170
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode && !this.IsPostCacheAdHelper && this.DoPostCacheSubstitutionAsNeeded(writer))
			{
				return;
			}
			AdCreatedEventArgs adArgs = this.PickAd();
			this.RenderLink(writer, adArgs);
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x00083FA8 File Offset: 0x000821A8
		private void RenderLink(HtmlTextWriter writer, AdCreatedEventArgs adArgs)
		{
			HyperLink hyperLink = new HyperLink();
			hyperLink.NavigateUrl = adArgs.NavigateUrl;
			hyperLink.Target = this.Target;
			if (base.HasAttributes)
			{
				foreach (object obj in base.Attributes.Keys)
				{
					string key = (string)obj;
					hyperLink.Attributes[key] = base.Attributes[key];
				}
			}
			string id = this.ID;
			if (!string.IsNullOrEmpty(id))
			{
				hyperLink.ID = this.ClientID;
			}
			if (!this.Enabled)
			{
				hyperLink.Enabled = false;
			}
			string text = (string)this.ViewState["AccessKey"];
			if (!string.IsNullOrEmpty(text))
			{
				hyperLink.AccessKey = text;
			}
			object obj2 = this.ViewState["TabIndex"];
			if (obj2 != null)
			{
				short num = (short)obj2;
				if (num != 0)
				{
					hyperLink.TabIndex = num;
				}
			}
			hyperLink.RenderBeginTag(writer);
			Image image = new Image();
			if (base.ControlStyleCreated)
			{
				image.ApplyStyle(base.ControlStyle);
			}
			string alternateText = adArgs.AlternateText;
			if (!string.IsNullOrEmpty(alternateText))
			{
				image.AlternateText = alternateText;
			}
			else
			{
				IDictionary adProperties = adArgs.AdProperties;
				string key2 = (this.AlternateTextField.Length != 0) ? this.AlternateTextField : "AlternateText";
				string text2 = (adProperties == null) ? null : ((string)adProperties[key2]);
				if (text2 != null && text2.Length == 0)
				{
					image.GenerateEmptyAlternateText = true;
				}
			}
			image.UrlResolved = true;
			string imageUrl = adArgs.ImageUrl;
			if (!string.IsNullOrEmpty(imageUrl))
			{
				image.ImageUrl = imageUrl;
			}
			if (adArgs.HasWidth)
			{
				image.ControlStyle.Width = adArgs.Width;
			}
			if (adArgs.HasHeight)
			{
				image.ControlStyle.Height = adArgs.Height;
			}
			string text3 = (string)this.ViewState["ToolTip"];
			if (!string.IsNullOrEmpty(text3))
			{
				image.ToolTip = text3;
			}
			image.RenderControl(writer);
			hyperLink.RenderEndTag(writer);
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000841DC File Offset: 0x000823DC
		private string ResolveAdRotatorUrl(string baseUrl, string relativeUrl)
		{
			if (relativeUrl == null || relativeUrl.Length == 0 || !UrlPath.IsRelativeUrl(relativeUrl) || baseUrl == null || baseUrl.Length == 0)
			{
				return relativeUrl;
			}
			return UrlPath.Combine(baseUrl, relativeUrl);
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x00084208 File Offset: 0x00082408
		private IDictionary SelectAdFromRecords()
		{
			if (this._adRecs == null || this._adRecs.Length == 0)
			{
				return null;
			}
			string text = this.KeywordFilter;
			bool flag = string.IsNullOrEmpty(text);
			if (!flag)
			{
				text = text.ToLower(CultureInfo.InvariantCulture);
			}
			int num = 0;
			for (int i = 0; i < this._adRecs.Length; i++)
			{
				if (flag || this.MatchingAd(this._adRecs[i], text))
				{
					num += this._adRecs[i].impressions;
				}
			}
			if (num == 0)
			{
				return null;
			}
			int randomNumber = AdRotator.GetRandomNumber(num);
			int num2 = 0;
			int num3 = -1;
			for (int j = 0; j < this._adRecs.Length; j++)
			{
				if (flag || this.MatchingAd(this._adRecs[j], text))
				{
					num2 += this._adRecs[j].impressions;
					if (randomNumber <= num2)
					{
						num3 = j;
						break;
					}
				}
			}
			return this._adRecs[num3].adProperties;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x00084304 File Offset: 0x00082504
		private AdRotator.AdRec[] SetAdRecs(ArrayList adDicts)
		{
			if (adDicts == null || adDicts.Count == 0)
			{
				return null;
			}
			AdRotator.AdRec[] array = new AdRotator.AdRec[adDicts.Count];
			int num = 0;
			for (int i = 0; i < adDicts.Count; i++)
			{
				if (adDicts[i] != null)
				{
					array[num].Initialize((IDictionary)adDicts[i]);
					num++;
				}
			}
			return array;
		}

		// Token: 0x04001E10 RID: 7696
		private static readonly object EventAdCreated = new object();

		// Token: 0x04001E11 RID: 7697
		private const string XmlDocumentTag = "Advertisements";

		// Token: 0x04001E12 RID: 7698
		private const string XmlDocumentRootXPath = "/Advertisements";

		// Token: 0x04001E13 RID: 7699
		private const string XmlAdTag = "Ad";

		// Token: 0x04001E14 RID: 7700
		private const string KeywordProperty = "Keyword";

		// Token: 0x04001E15 RID: 7701
		private const string ImpressionsProperty = "Impressions";

		// Token: 0x04001E16 RID: 7702
		private static Random _random;

		// Token: 0x04001E17 RID: 7703
		private string _baseUrl;

		// Token: 0x04001E18 RID: 7704
		private string _advertisementFile;

		// Token: 0x04001E19 RID: 7705
		private AdCreatedEventArgs _adCreatedEventArgs;

		// Token: 0x04001E1A RID: 7706
		private AdRotator.AdRec[] _adRecs;

		// Token: 0x04001E1B RID: 7707
		private bool _isPostCacheAdHelper;

		// Token: 0x04001E1C RID: 7708
		private string _uniqueID;

		// Token: 0x04001E1D RID: 7709
		private static readonly Type _adrotatorType = typeof(AdRotator);

		// Token: 0x04001E1E RID: 7710
		private static readonly Type[] _AdCreatedParameterTypes = new Type[]
		{
			typeof(AdCreatedEventArgs)
		};

		// Token: 0x02000992 RID: 2450
		private struct AdRec
		{
			// Token: 0x06006A7D RID: 27261 RVA: 0x0017C210 File Offset: 0x0017A410
			public void Initialize(IDictionary adProperties)
			{
				this.adProperties = adProperties;
				object obj = adProperties["Keyword"];
				if (obj != null && obj is string)
				{
					this.keyword = ((string)obj).Trim();
				}
				else
				{
					this.keyword = string.Empty;
				}
				string text = adProperties["Impressions"] as string;
				if (string.IsNullOrEmpty(text) || !int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out this.impressions))
				{
					this.impressions = 1;
				}
				if (this.impressions < 0)
				{
					this.impressions = 1;
				}
			}

			// Token: 0x040038D6 RID: 14550
			public string keyword;

			// Token: 0x040038D7 RID: 14551
			public int impressions;

			// Token: 0x040038D8 RID: 14552
			public IDictionary adProperties;
		}
	}
}
