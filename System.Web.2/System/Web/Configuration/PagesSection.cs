using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x02000724 RID: 1828
	public sealed class PagesSection : ConfigurationSection
	{
		// Token: 0x060057F4 RID: 22516 RVA: 0x00133C1C File Offset: 0x00131E1C
		static PagesSection()
		{
			PagesSection._properties = new ConfigurationPropertyCollection();
			PagesSection._properties.Add(PagesSection._propBuffer);
			PagesSection._properties.Add(PagesSection._propControlRenderingCompatibilityVersion);
			PagesSection._properties.Add(PagesSection._propEnableSessionState);
			PagesSection._properties.Add(PagesSection._propEnableViewState);
			PagesSection._properties.Add(PagesSection._propEnableViewStateMac);
			PagesSection._properties.Add(PagesSection._propEnableEventValidation);
			PagesSection._properties.Add(PagesSection._propSmartNavigation);
			PagesSection._properties.Add(PagesSection._propAutoEventWireup);
			PagesSection._properties.Add(PagesSection._propPageBaseType);
			PagesSection._properties.Add(PagesSection._propUserControlBaseType);
			PagesSection._properties.Add(PagesSection._propValidateRequest);
			PagesSection._properties.Add(PagesSection._propMasterPageFile);
			PagesSection._properties.Add(PagesSection._propTheme);
			PagesSection._properties.Add(PagesSection._propStyleSheetTheme);
			PagesSection._properties.Add(PagesSection._propNamespaces);
			PagesSection._properties.Add(PagesSection._propControls);
			PagesSection._properties.Add(PagesSection._propTagMapping);
			PagesSection._properties.Add(PagesSection._propMaxPageStateFieldLength);
			PagesSection._properties.Add(PagesSection._propCompilationMode);
			PagesSection._properties.Add(PagesSection._propPageParserFilterType);
			PagesSection._properties.Add(PagesSection._propViewStateEncryptionMode);
			PagesSection._properties.Add(PagesSection._propMaintainScrollPosition);
			PagesSection._properties.Add(PagesSection._propAsyncTimeout);
			PagesSection._properties.Add(PagesSection._propRenderAllHiddenFieldsAtTopOfForm);
			PagesSection._properties.Add(PagesSection._propClientIDMode);
			PagesSection._properties.Add(PagesSection._propIgnoreDeviceFilters);
		}

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x060057F6 RID: 22518 RVA: 0x00134118 File Offset: 0x00132318
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PagesSection._properties;
			}
		}

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x060057F7 RID: 22519 RVA: 0x0013411F File Offset: 0x0013231F
		// (set) Token: 0x060057F8 RID: 22520 RVA: 0x00134131 File Offset: 0x00132331
		[ConfigurationProperty("buffer", DefaultValue = true)]
		public bool Buffer
		{
			get
			{
				return (bool)base[PagesSection._propBuffer];
			}
			set
			{
				base[PagesSection._propBuffer] = value;
			}
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x060057F9 RID: 22521 RVA: 0x00134144 File Offset: 0x00132344
		// (set) Token: 0x060057FA RID: 22522 RVA: 0x001341CC File Offset: 0x001323CC
		[ConfigurationProperty("enableSessionState", DefaultValue = "true")]
		public PagesEnableSessionState EnableSessionState
		{
			get
			{
				string a = (string)base[PagesSection._propEnableSessionState];
				PagesEnableSessionState result;
				if (!(a == "true"))
				{
					if (!(a == "false"))
					{
						if (!(a == "ReadOnly"))
						{
							string name = PagesSection._propEnableSessionState.Name;
							string text = "true, false, ReadOnly";
							throw new ConfigurationErrorsException(SR.GetString("Invalid_enum_attribute", new object[]
							{
								name,
								text
							}));
						}
						result = PagesEnableSessionState.ReadOnly;
					}
					else
					{
						result = PagesEnableSessionState.False;
					}
				}
				else
				{
					result = PagesEnableSessionState.True;
				}
				return result;
			}
			set
			{
				string value2;
				switch (value)
				{
				case PagesEnableSessionState.False:
					value2 = "false";
					break;
				case PagesEnableSessionState.ReadOnly:
					value2 = "ReadOnly";
					break;
				case PagesEnableSessionState.True:
					value2 = "true";
					break;
				default:
					value2 = "true";
					break;
				}
				base[PagesSection._propEnableSessionState] = value2;
			}
		}

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x060057FB RID: 22523 RVA: 0x0013421D File Offset: 0x0013241D
		// (set) Token: 0x060057FC RID: 22524 RVA: 0x0013422F File Offset: 0x0013242F
		[ConfigurationProperty("enableViewState", DefaultValue = true)]
		public bool EnableViewState
		{
			get
			{
				return (bool)base[PagesSection._propEnableViewState];
			}
			set
			{
				base[PagesSection._propEnableViewState] = value;
			}
		}

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x060057FD RID: 22525 RVA: 0x00134242 File Offset: 0x00132442
		// (set) Token: 0x060057FE RID: 22526 RVA: 0x00134254 File Offset: 0x00132454
		[ConfigurationProperty("enableViewStateMac", DefaultValue = true)]
		public bool EnableViewStateMac
		{
			get
			{
				return (bool)base[PagesSection._propEnableViewStateMac];
			}
			set
			{
				base[PagesSection._propEnableViewStateMac] = value;
			}
		}

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x060057FF RID: 22527 RVA: 0x00134267 File Offset: 0x00132467
		// (set) Token: 0x06005800 RID: 22528 RVA: 0x00134279 File Offset: 0x00132479
		[ConfigurationProperty("enableEventValidation", DefaultValue = true)]
		public bool EnableEventValidation
		{
			get
			{
				return (bool)base[PagesSection._propEnableEventValidation];
			}
			set
			{
				base[PagesSection._propEnableEventValidation] = value;
			}
		}

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x06005801 RID: 22529 RVA: 0x0013428C File Offset: 0x0013248C
		// (set) Token: 0x06005802 RID: 22530 RVA: 0x0013429E File Offset: 0x0013249E
		[ConfigurationProperty("smartNavigation", DefaultValue = false)]
		public bool SmartNavigation
		{
			get
			{
				return (bool)base[PagesSection._propSmartNavigation];
			}
			set
			{
				base[PagesSection._propSmartNavigation] = value;
			}
		}

		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x06005803 RID: 22531 RVA: 0x001342B1 File Offset: 0x001324B1
		// (set) Token: 0x06005804 RID: 22532 RVA: 0x001342C3 File Offset: 0x001324C3
		[ConfigurationProperty("autoEventWireup", DefaultValue = true)]
		public bool AutoEventWireup
		{
			get
			{
				return (bool)base[PagesSection._propAutoEventWireup];
			}
			set
			{
				base[PagesSection._propAutoEventWireup] = value;
			}
		}

		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x06005805 RID: 22533 RVA: 0x001342D6 File Offset: 0x001324D6
		// (set) Token: 0x06005806 RID: 22534 RVA: 0x001342E8 File Offset: 0x001324E8
		[ConfigurationProperty("maintainScrollPositionOnPostBack", DefaultValue = false)]
		public bool MaintainScrollPositionOnPostBack
		{
			get
			{
				return (bool)base[PagesSection._propMaintainScrollPosition];
			}
			set
			{
				base[PagesSection._propMaintainScrollPosition] = value;
			}
		}

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x06005807 RID: 22535 RVA: 0x001342FB File Offset: 0x001324FB
		// (set) Token: 0x06005808 RID: 22536 RVA: 0x0013430D File Offset: 0x0013250D
		[ConfigurationProperty("pageBaseType", DefaultValue = "System.Web.UI.Page")]
		public string PageBaseType
		{
			get
			{
				return (string)base[PagesSection._propPageBaseType];
			}
			set
			{
				base[PagesSection._propPageBaseType] = value;
			}
		}

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x06005809 RID: 22537 RVA: 0x0013431B File Offset: 0x0013251B
		// (set) Token: 0x0600580A RID: 22538 RVA: 0x0013432D File Offset: 0x0013252D
		[ConfigurationProperty("userControlBaseType", DefaultValue = "System.Web.UI.UserControl")]
		public string UserControlBaseType
		{
			get
			{
				return (string)base[PagesSection._propUserControlBaseType];
			}
			set
			{
				base[PagesSection._propUserControlBaseType] = value;
			}
		}

		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x0600580B RID: 22539 RVA: 0x0013433C File Offset: 0x0013253C
		internal Type PageBaseTypeInternal
		{
			get
			{
				if (this._pageBaseType == null && base.ElementInformation.Properties[PagesSection._propPageBaseType.Name].ValueOrigin != PropertyValueOrigin.Default)
				{
					lock (this)
					{
						if (this._pageBaseType == null)
						{
							Type type = ConfigUtil.GetType(this.PageBaseType, "pageBaseType", this);
							ConfigUtil.CheckBaseType(typeof(Page), type, "pageBaseType", this);
							this._pageBaseType = type;
						}
					}
				}
				return this._pageBaseType;
			}
		}

		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x0600580C RID: 22540 RVA: 0x001343E4 File Offset: 0x001325E4
		internal Type UserControlBaseTypeInternal
		{
			get
			{
				if (this._userControlBaseType == null && base.ElementInformation.Properties[PagesSection._propUserControlBaseType.Name].ValueOrigin != PropertyValueOrigin.Default)
				{
					lock (this)
					{
						if (this._userControlBaseType == null)
						{
							Type type = ConfigUtil.GetType(this.UserControlBaseType, "userControlBaseType", this);
							ConfigUtil.CheckBaseType(typeof(UserControl), type, "userControlBaseType", this);
							this._userControlBaseType = type;
						}
					}
				}
				return this._userControlBaseType;
			}
		}

		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x0600580D RID: 22541 RVA: 0x0013448C File Offset: 0x0013268C
		// (set) Token: 0x0600580E RID: 22542 RVA: 0x0013449E File Offset: 0x0013269E
		[ConfigurationProperty("pageParserFilterType", DefaultValue = "")]
		public string PageParserFilterType
		{
			get
			{
				return (string)base[PagesSection._propPageParserFilterType];
			}
			set
			{
				base[PagesSection._propPageParserFilterType] = value;
			}
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x0600580F RID: 22543 RVA: 0x001344AC File Offset: 0x001326AC
		internal Type PageParserFilterTypeInternal
		{
			get
			{
				if (PageParser.DefaultPageParserFilterType != null)
				{
					return PageParser.DefaultPageParserFilterType;
				}
				if (this._pageParserFilterType == null && !string.IsNullOrEmpty(this.PageParserFilterType))
				{
					Type type = ConfigUtil.GetType(this.PageParserFilterType, "pageParserFilterType", this);
					ConfigUtil.CheckBaseType(typeof(PageParserFilter), type, "pageParserFilterType", this);
					this._pageParserFilterType = type;
				}
				return this._pageParserFilterType;
			}
		}

		// Token: 0x06005810 RID: 22544 RVA: 0x0013451C File Offset: 0x0013271C
		internal PageParserFilter CreateControlTypeFilter()
		{
			Type pageParserFilterTypeInternal = this.PageParserFilterTypeInternal;
			if (pageParserFilterTypeInternal == null)
			{
				return null;
			}
			return (PageParserFilter)HttpRuntime.CreateNonPublicInstance(pageParserFilterTypeInternal);
		}

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x06005811 RID: 22545 RVA: 0x00134546 File Offset: 0x00132746
		// (set) Token: 0x06005812 RID: 22546 RVA: 0x00134558 File Offset: 0x00132758
		[ConfigurationProperty("validateRequest", DefaultValue = true)]
		public bool ValidateRequest
		{
			get
			{
				return (bool)base[PagesSection._propValidateRequest];
			}
			set
			{
				base[PagesSection._propValidateRequest] = value;
			}
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06005813 RID: 22547 RVA: 0x0013456B File Offset: 0x0013276B
		// (set) Token: 0x06005814 RID: 22548 RVA: 0x0013457D File Offset: 0x0013277D
		[ConfigurationProperty("masterPageFile", DefaultValue = "")]
		public string MasterPageFile
		{
			get
			{
				return (string)base[PagesSection._propMasterPageFile];
			}
			set
			{
				base[PagesSection._propMasterPageFile] = value;
			}
		}

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06005815 RID: 22549 RVA: 0x0013458C File Offset: 0x0013278C
		internal string MasterPageFileInternal
		{
			get
			{
				if (this._masterPageFile == null)
				{
					string text = this.MasterPageFile;
					if (!string.IsNullOrEmpty(text))
					{
						if (UrlPath.IsAbsolutePhysicalPath(text))
						{
							throw new ConfigurationErrorsException(SR.GetString("Physical_path_not_allowed", new object[]
							{
								text
							}), base.ElementInformation.Properties["masterPageFile"].Source, base.ElementInformation.Properties["masterPageFile"].LineNumber);
						}
						VirtualPath virtualPath;
						try
						{
							virtualPath = VirtualPath.CreateNonRelative(text);
						}
						catch (Exception ex)
						{
							throw new ConfigurationErrorsException(ex.Message, ex, base.ElementInformation.Properties["masterPageFile"].Source, base.ElementInformation.Properties["masterPageFile"].LineNumber);
						}
						if (!Util.VirtualFileExistsWithAssert(virtualPath))
						{
							throw new ConfigurationErrorsException(SR.GetString("FileName_does_not_exist", new object[]
							{
								text
							}), base.ElementInformation.Properties["masterPageFile"].Source, base.ElementInformation.Properties["masterPageFile"].LineNumber);
						}
						string extension = UrlPath.GetExtension(text);
						Type buildProviderTypeFromExtension = CompilationUtil.GetBuildProviderTypeFromExtension(this._virtualPath, extension, BuildProviderAppliesTo.Web, false);
						if (!typeof(MasterPageBuildProvider).IsAssignableFrom(buildProviderTypeFromExtension))
						{
							throw new ConfigurationErrorsException(SR.GetString("Bad_masterPage_ext"), base.ElementInformation.Properties["masterPageFile"].Source, base.ElementInformation.Properties["masterPageFile"].LineNumber);
						}
						text = virtualPath.AppRelativeVirtualPathString;
					}
					else
					{
						text = string.Empty;
					}
					this._masterPageFile = text;
				}
				return this._masterPageFile;
			}
		}

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06005816 RID: 22550 RVA: 0x0013474C File Offset: 0x0013294C
		// (set) Token: 0x06005817 RID: 22551 RVA: 0x0013475E File Offset: 0x0013295E
		[ConfigurationProperty("theme", DefaultValue = "")]
		public string Theme
		{
			get
			{
				return (string)base[PagesSection._propTheme];
			}
			set
			{
				base[PagesSection._propTheme] = value;
			}
		}

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06005818 RID: 22552 RVA: 0x0013476C File Offset: 0x0013296C
		internal string ThemeInternal
		{
			get
			{
				string theme = this.Theme;
				if (!this._themeChecked)
				{
					if (!string.IsNullOrEmpty(theme) && !Util.ThemeExists(theme))
					{
						throw new ConfigurationErrorsException(SR.GetString("Page_theme_not_found", new object[]
						{
							theme
						}), base.ElementInformation.Properties["theme"].Source, base.ElementInformation.Properties["theme"].LineNumber);
					}
					this._themeChecked = true;
				}
				return theme;
			}
		}

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06005819 RID: 22553 RVA: 0x001347EE File Offset: 0x001329EE
		// (set) Token: 0x0600581A RID: 22554 RVA: 0x00134800 File Offset: 0x00132A00
		[ConfigurationProperty("styleSheetTheme", DefaultValue = "")]
		public string StyleSheetTheme
		{
			get
			{
				return (string)base[PagesSection._propStyleSheetTheme];
			}
			set
			{
				base[PagesSection._propStyleSheetTheme] = value;
			}
		}

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x0600581B RID: 22555 RVA: 0x00134810 File Offset: 0x00132A10
		internal string StyleSheetThemeInternal
		{
			get
			{
				string styleSheetTheme = this.StyleSheetTheme;
				if (!this._styleSheetThemeChecked)
				{
					if (!string.IsNullOrEmpty(styleSheetTheme) && !Util.ThemeExists(styleSheetTheme))
					{
						throw new ConfigurationErrorsException(SR.GetString("Page_theme_not_found", new object[]
						{
							styleSheetTheme
						}), base.ElementInformation.Properties["styleSheetTheme"].Source, base.ElementInformation.Properties["styleSheetTheme"].LineNumber);
					}
					this._styleSheetThemeChecked = true;
				}
				return styleSheetTheme;
			}
		}

		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x0600581C RID: 22556 RVA: 0x00134892 File Offset: 0x00132A92
		[ConfigurationProperty("namespaces")]
		public NamespaceCollection Namespaces
		{
			get
			{
				return (NamespaceCollection)base[PagesSection._propNamespaces];
			}
		}

		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x0600581D RID: 22557 RVA: 0x001348A4 File Offset: 0x00132AA4
		[ConfigurationProperty("controls")]
		public TagPrefixCollection Controls
		{
			get
			{
				return (TagPrefixCollection)base[PagesSection._propControls];
			}
		}

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x0600581E RID: 22558 RVA: 0x001348B6 File Offset: 0x00132AB6
		// (set) Token: 0x0600581F RID: 22559 RVA: 0x001348C8 File Offset: 0x00132AC8
		[ConfigurationProperty("maxPageStateFieldLength", DefaultValue = -1)]
		public int MaxPageStateFieldLength
		{
			get
			{
				return (int)base[PagesSection._propMaxPageStateFieldLength];
			}
			set
			{
				base[PagesSection._propMaxPageStateFieldLength] = value;
			}
		}

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x06005820 RID: 22560 RVA: 0x001348DB File Offset: 0x00132ADB
		[ConfigurationProperty("tagMapping")]
		public TagMapCollection TagMapping
		{
			get
			{
				return (TagMapCollection)base[PagesSection._propTagMapping];
			}
		}

		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x06005821 RID: 22561 RVA: 0x001348ED File Offset: 0x00132AED
		// (set) Token: 0x06005822 RID: 22562 RVA: 0x001348FF File Offset: 0x00132AFF
		[ConfigurationProperty("compilationMode", DefaultValue = CompilationMode.Always)]
		public CompilationMode CompilationMode
		{
			get
			{
				return (CompilationMode)base[PagesSection._propCompilationMode];
			}
			set
			{
				base[PagesSection._propCompilationMode] = value;
			}
		}

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x06005823 RID: 22563 RVA: 0x00134912 File Offset: 0x00132B12
		// (set) Token: 0x06005824 RID: 22564 RVA: 0x00134924 File Offset: 0x00132B24
		[ConfigurationProperty("viewStateEncryptionMode", DefaultValue = ViewStateEncryptionMode.Auto)]
		public ViewStateEncryptionMode ViewStateEncryptionMode
		{
			get
			{
				return (ViewStateEncryptionMode)base[PagesSection._propViewStateEncryptionMode];
			}
			set
			{
				base[PagesSection._propViewStateEncryptionMode] = value;
			}
		}

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x06005825 RID: 22565 RVA: 0x00134937 File Offset: 0x00132B37
		// (set) Token: 0x06005826 RID: 22566 RVA: 0x00134949 File Offset: 0x00132B49
		[ConfigurationProperty("asyncTimeout", DefaultValue = "00:00:45")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		public TimeSpan AsyncTimeout
		{
			get
			{
				return (TimeSpan)base[PagesSection._propAsyncTimeout];
			}
			set
			{
				base[PagesSection._propAsyncTimeout] = value;
			}
		}

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x06005827 RID: 22567 RVA: 0x0013495C File Offset: 0x00132B5C
		// (set) Token: 0x06005828 RID: 22568 RVA: 0x0013496E File Offset: 0x00132B6E
		[ConfigurationProperty("renderAllHiddenFieldsAtTopOfForm", DefaultValue = true)]
		public bool RenderAllHiddenFieldsAtTopOfForm
		{
			get
			{
				return (bool)base[PagesSection._propRenderAllHiddenFieldsAtTopOfForm];
			}
			set
			{
				base[PagesSection._propRenderAllHiddenFieldsAtTopOfForm] = value;
			}
		}

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x06005829 RID: 22569 RVA: 0x00134981 File Offset: 0x00132B81
		// (set) Token: 0x0600582A RID: 22570 RVA: 0x001349B6 File Offset: 0x00132BB6
		[ConfigurationProperty("clientIDMode", DefaultValue = ClientIDMode.Predictable)]
		public ClientIDMode ClientIDMode
		{
			get
			{
				if (this._clientIDMode == null)
				{
					this._clientIDMode = new ClientIDMode?((ClientIDMode)base[PagesSection._propClientIDMode]);
				}
				return this._clientIDMode.Value;
			}
			set
			{
				base[PagesSection._propClientIDMode] = value;
				this._clientIDMode = new ClientIDMode?(value);
			}
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x0600582B RID: 22571 RVA: 0x001349D5 File Offset: 0x00132BD5
		// (set) Token: 0x0600582C RID: 22572 RVA: 0x00134A01 File Offset: 0x00132C01
		[ConfigurationProperty("controlRenderingCompatibilityVersion", DefaultValue = "4.0")]
		[ConfigurationValidator(typeof(VersionValidator))]
		[TypeConverter(typeof(VersionConverter))]
		public Version ControlRenderingCompatibilityVersion
		{
			get
			{
				if (this._controlRenderingCompatibilityVersion == null)
				{
					this._controlRenderingCompatibilityVersion = (Version)base[PagesSection._propControlRenderingCompatibilityVersion];
				}
				return this._controlRenderingCompatibilityVersion;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base[PagesSection._propControlRenderingCompatibilityVersion] = value;
				this._controlRenderingCompatibilityVersion = value;
			}
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x0600582D RID: 22573 RVA: 0x00134A2A File Offset: 0x00132C2A
		[ConfigurationProperty("ignoreDeviceFilters")]
		public IgnoreDeviceFilterElementCollection IgnoreDeviceFilters
		{
			get
			{
				return (IgnoreDeviceFilterElementCollection)base[PagesSection._propIgnoreDeviceFilters];
			}
		}

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x0600582E RID: 22574 RVA: 0x00134A3C File Offset: 0x00132C3C
		internal TagNamespaceRegisterEntryTable TagNamespaceRegisterEntriesInternal
		{
			get
			{
				if (this._tagNamespaceRegisterEntries == null)
				{
					lock (this)
					{
						if (this._tagNamespaceRegisterEntries == null)
						{
							this.FillInRegisterEntries();
						}
					}
				}
				return this._tagNamespaceRegisterEntries;
			}
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x00134A90 File Offset: 0x00132C90
		internal void FillInRegisterEntries()
		{
			TagNamespaceRegisterEntryTable tagNamespaceRegisterEntryTable = new TagNamespaceRegisterEntryTable();
			foreach (object obj in PagesSection.DefaultTagNamespaceRegisterEntries)
			{
				TagNamespaceRegisterEntry tagNamespaceRegisterEntry = (TagNamespaceRegisterEntry)obj;
				tagNamespaceRegisterEntryTable[tagNamespaceRegisterEntry.TagPrefix] = new ArrayList(new object[]
				{
					tagNamespaceRegisterEntry
				});
			}
			Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			foreach (object obj2 in this.Controls)
			{
				TagPrefixInfo tagPrefixInfo = (TagPrefixInfo)obj2;
				if (!string.IsNullOrEmpty(tagPrefixInfo.TagName))
				{
					UserControlRegisterEntry userControlRegisterEntry = new UserControlRegisterEntry(tagPrefixInfo.TagPrefix, tagPrefixInfo.TagName);
					userControlRegisterEntry.ComesFromConfig = true;
					try
					{
						userControlRegisterEntry.UserControlSource = VirtualPath.CreateNonRelative(tagPrefixInfo.Source);
					}
					catch (Exception ex)
					{
						throw new ConfigurationErrorsException(ex.Message, ex, tagPrefixInfo.ElementInformation.Properties["src"].Source, tagPrefixInfo.ElementInformation.Properties["src"].LineNumber);
					}
					hashtable[userControlRegisterEntry.Key] = userControlRegisterEntry;
				}
				else if (!string.IsNullOrEmpty(tagPrefixInfo.Namespace))
				{
					TagNamespaceRegisterEntry value = new TagNamespaceRegisterEntry(tagPrefixInfo.TagPrefix, tagPrefixInfo.Namespace, tagPrefixInfo.Assembly);
					ArrayList arrayList = (ArrayList)tagNamespaceRegisterEntryTable[tagPrefixInfo.TagPrefix];
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						tagNamespaceRegisterEntryTable[tagPrefixInfo.TagPrefix] = arrayList;
					}
					arrayList.Add(value);
				}
			}
			this._tagNamespaceRegisterEntries = tagNamespaceRegisterEntryTable;
			this._userControlRegisterEntries = hashtable;
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x06005830 RID: 22576 RVA: 0x00134CA0 File Offset: 0x00132EA0
		internal static ICollection DefaultTagNamespaceRegisterEntries
		{
			get
			{
				TagNamespaceRegisterEntry tagNamespaceRegisterEntry = new TagNamespaceRegisterEntry("asp", "System.Web.UI.WebControls", "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				TagNamespaceRegisterEntry tagNamespaceRegisterEntry2 = new TagNamespaceRegisterEntry("mobile", "System.Web.UI.MobileControls", "System.Web.Mobile, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				return new TagNamespaceRegisterEntry[]
				{
					tagNamespaceRegisterEntry,
					tagNamespaceRegisterEntry2
				};
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x06005831 RID: 22577 RVA: 0x00134CE8 File Offset: 0x00132EE8
		internal Hashtable UserControlRegisterEntriesInternal
		{
			get
			{
				if (this._userControlRegisterEntries == null)
				{
					lock (this)
					{
						if (this._userControlRegisterEntries == null)
						{
							this.FillInRegisterEntries();
						}
					}
				}
				return this._userControlRegisterEntries;
			}
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x00134D3C File Offset: 0x00132F3C
		protected override void DeserializeSection(XmlReader reader)
		{
			base.DeserializeSection(reader);
			WebContext webContext = base.EvaluationContext.HostingContext as WebContext;
			if (webContext != null)
			{
				this._virtualPath = VirtualPath.CreateNonRelativeTrailingSlashAllowNull(webContext.Path);
			}
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x00134D75 File Offset: 0x00132F75
		protected override void SetReadOnly()
		{
			ConfigUtil.SetFX45DefaultValue(this, PagesSection._propControlRenderingCompatibilityVersion, VersionUtil.Framework45);
			base.SetReadOnly();
		}

		// Token: 0x04002EBD RID: 11965
		private static readonly Version _controlRenderingDefaultVersion = VersionUtil.FrameworkDefault;

		// Token: 0x04002EBE RID: 11966
		private static readonly Version _controlRenderingMinimumVersion = VersionUtil.Framework35;

		// Token: 0x04002EBF RID: 11967
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002EC0 RID: 11968
		private static readonly ConfigurationProperty _propBuffer = new ConfigurationProperty("buffer", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002EC1 RID: 11969
		private static readonly ConfigurationProperty _propControlRenderingCompatibilityVersion = new ConfigurationProperty("controlRenderingCompatibilityVersion", typeof(Version), PagesSection._controlRenderingDefaultVersion, StdValidatorsAndConverters.VersionConverter, new VersionValidator(PagesSection._controlRenderingMinimumVersion), ConfigurationPropertyOptions.None);

		// Token: 0x04002EC2 RID: 11970
		private static readonly ConfigurationProperty _propEnableSessionState = new ConfigurationProperty("enableSessionState", typeof(string), "true", ConfigurationPropertyOptions.None);

		// Token: 0x04002EC3 RID: 11971
		private static readonly ConfigurationProperty _propEnableViewState = new ConfigurationProperty("enableViewState", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002EC4 RID: 11972
		private static readonly ConfigurationProperty _propEnableViewStateMac = new ConfigurationProperty("enableViewStateMac", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002EC5 RID: 11973
		private static readonly ConfigurationProperty _propEnableEventValidation = new ConfigurationProperty("enableEventValidation", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002EC6 RID: 11974
		private static readonly ConfigurationProperty _propSmartNavigation = new ConfigurationProperty("smartNavigation", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002EC7 RID: 11975
		private static readonly ConfigurationProperty _propAutoEventWireup = new ConfigurationProperty("autoEventWireup", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002EC8 RID: 11976
		private static readonly ConfigurationProperty _propPageBaseType = new ConfigurationProperty("pageBaseType", typeof(string), "System.Web.UI.Page", ConfigurationPropertyOptions.None);

		// Token: 0x04002EC9 RID: 11977
		private static readonly ConfigurationProperty _propUserControlBaseType = new ConfigurationProperty("userControlBaseType", typeof(string), "System.Web.UI.UserControl", ConfigurationPropertyOptions.None);

		// Token: 0x04002ECA RID: 11978
		private static readonly ConfigurationProperty _propValidateRequest = new ConfigurationProperty("validateRequest", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002ECB RID: 11979
		private static readonly ConfigurationProperty _propMasterPageFile = new ConfigurationProperty("masterPageFile", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002ECC RID: 11980
		private static readonly ConfigurationProperty _propTheme = new ConfigurationProperty("theme", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002ECD RID: 11981
		private static readonly ConfigurationProperty _propNamespaces = new ConfigurationProperty("namespaces", typeof(NamespaceCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002ECE RID: 11982
		private static readonly ConfigurationProperty _propControls = new ConfigurationProperty("controls", typeof(TagPrefixCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002ECF RID: 11983
		private static readonly ConfigurationProperty _propTagMapping = new ConfigurationProperty("tagMapping", typeof(TagMapCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002ED0 RID: 11984
		private static readonly ConfigurationProperty _propMaxPageStateFieldLength = new ConfigurationProperty("maxPageStateFieldLength", typeof(int), Page.DefaultMaxPageStateFieldLength, ConfigurationPropertyOptions.None);

		// Token: 0x04002ED1 RID: 11985
		private static readonly ConfigurationProperty _propCompilationMode = new ConfigurationProperty("compilationMode", typeof(CompilationMode), CompilationMode.Always, ConfigurationPropertyOptions.None);

		// Token: 0x04002ED2 RID: 11986
		private static readonly ConfigurationProperty _propStyleSheetTheme = new ConfigurationProperty("styleSheetTheme", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002ED3 RID: 11987
		private static readonly ConfigurationProperty _propPageParserFilterType = new ConfigurationProperty("pageParserFilterType", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002ED4 RID: 11988
		private static readonly ConfigurationProperty _propViewStateEncryptionMode = new ConfigurationProperty("viewStateEncryptionMode", typeof(ViewStateEncryptionMode), ViewStateEncryptionMode.Auto, ConfigurationPropertyOptions.None);

		// Token: 0x04002ED5 RID: 11989
		private static readonly ConfigurationProperty _propMaintainScrollPosition = new ConfigurationProperty("maintainScrollPositionOnPostBack", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002ED6 RID: 11990
		private static readonly ConfigurationProperty _propAsyncTimeout = new ConfigurationProperty("asyncTimeout", typeof(TimeSpan), TimeSpan.FromSeconds((double)Page.DefaultAsyncTimeoutSeconds), StdValidatorsAndConverters.TimeSpanSecondsConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002ED7 RID: 11991
		private static readonly ConfigurationProperty _propRenderAllHiddenFieldsAtTopOfForm = new ConfigurationProperty("renderAllHiddenFieldsAtTopOfForm", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002ED8 RID: 11992
		private static readonly ConfigurationProperty _propClientIDMode = new ConfigurationProperty("clientIDMode", typeof(ClientIDMode), ClientIDMode.Predictable, ConfigurationPropertyOptions.None);

		// Token: 0x04002ED9 RID: 11993
		private static readonly ConfigurationProperty _propIgnoreDeviceFilters = new ConfigurationProperty("ignoreDeviceFilters", typeof(IgnoreDeviceFilterElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002EDA RID: 11994
		private VirtualPath _virtualPath;

		// Token: 0x04002EDB RID: 11995
		private string _masterPageFile;

		// Token: 0x04002EDC RID: 11996
		private Type _pageBaseType;

		// Token: 0x04002EDD RID: 11997
		private Type _userControlBaseType;

		// Token: 0x04002EDE RID: 11998
		private Type _pageParserFilterType;

		// Token: 0x04002EDF RID: 11999
		private bool _themeChecked;

		// Token: 0x04002EE0 RID: 12000
		private bool _styleSheetThemeChecked;

		// Token: 0x04002EE1 RID: 12001
		private ClientIDMode? _clientIDMode;

		// Token: 0x04002EE2 RID: 12002
		private Version _controlRenderingCompatibilityVersion;

		// Token: 0x04002EE3 RID: 12003
		private TagNamespaceRegisterEntryTable _tagNamespaceRegisterEntries;

		// Token: 0x04002EE4 RID: 12004
		private Hashtable _userControlRegisterEntries;
	}
}
