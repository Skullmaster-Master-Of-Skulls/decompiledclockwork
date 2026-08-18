using System;
using System.Collections;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002D9 RID: 729
	public sealed class PageParser : TemplateControlParser
	{
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x0006EB6E File Offset: 0x0006CD6E
		internal int TransactionMode
		{
			get
			{
				return this._transactionMode;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x0006EB76 File Offset: 0x0006CD76
		internal TraceMode TraceMode
		{
			get
			{
				return this._traceMode;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x0006EB7E File Offset: 0x0006CD7E
		internal TraceEnable TraceEnabled
		{
			get
			{
				return this._traceEnabled;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x0006EB86 File Offset: 0x0006CD86
		internal bool FRequiresSessionState
		{
			get
			{
				return this.flags[1048576];
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x0006EB98 File Offset: 0x0006CD98
		internal bool FReadOnlySessionState
		{
			get
			{
				return this.flags[2097152];
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x0006EBAA File Offset: 0x0006CDAA
		internal string StyleSheetTheme
		{
			get
			{
				return this._styleSheetTheme;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x0006EBB2 File Offset: 0x0006CDB2
		internal bool AspCompatMode
		{
			get
			{
				return this.flags[64];
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x0006EBC1 File Offset: 0x0006CDC1
		internal bool AsyncMode
		{
			get
			{
				return this.flags[8388608];
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x0006EBD3 File Offset: 0x0006CDD3
		internal bool ValidateRequest
		{
			get
			{
				return this.flags[4194304];
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x0006EBE5 File Offset: 0x0006CDE5
		internal Type PreviousPageType
		{
			get
			{
				return this._previousPageType;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x0006EBED File Offset: 0x0006CDED
		internal Type MasterPageType
		{
			get
			{
				return this._masterPageType;
			}
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0006EBF8 File Offset: 0x0006CDF8
		public PageParser()
		{
			this.flags[524288] = true;
			this.flags[1048576] = true;
			this.flags[4194304] = true;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0006EC4C File Offset: 0x0006CE4C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static IHttpHandler GetCompiledPageInstance(string virtualPath, string inputFile, HttpContext context)
		{
			if (!string.IsNullOrEmpty(inputFile))
			{
				inputFile = Path.GetFullPath(inputFile);
			}
			return PageParser.GetCompiledPageInstance(VirtualPath.Create(virtualPath), inputFile, context);
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0006EC6C File Offset: 0x0006CE6C
		private static IHttpHandler GetCompiledPageInstance(VirtualPath virtualPath, string inputFile, HttpContext context)
		{
			if (context != null)
			{
				virtualPath = context.Request.FilePathObject.Combine(virtualPath);
			}
			object obj = null;
			IHttpHandler result;
			try
			{
				try
				{
					if (inputFile != null)
					{
						obj = HostingEnvironment.AddVirtualPathToFileMapping(virtualPath, inputFile);
					}
					BuildResultCompiledType buildResultCompiledType = (BuildResultCompiledType)BuildManager.GetVPathBuildResult(context, virtualPath, false, true, true, true);
					result = (IHttpHandler)HttpRuntime.CreatePublicInstance(buildResultCompiledType.ResultType);
				}
				finally
				{
					if (obj != null)
					{
						HostingEnvironment.ClearVirtualPathToFileMapping(obj);
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0006ECEC File Offset: 0x0006CEEC
		internal override Type DefaultBaseType
		{
			get
			{
				return typeof(Page);
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x0006ECF8 File Offset: 0x0006CEF8
		internal override Type DefaultFileLevelBuilderType
		{
			get
			{
				return typeof(FileLevelPageControlBuilder);
			}
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0006ED04 File Offset: 0x0006CF04
		internal override RootBuilder CreateDefaultFileLevelBuilder()
		{
			return new FileLevelPageControlBuilder();
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0006ED0C File Offset: 0x0006CF0C
		private void EnsureMasterPageFileFromConfigApplied()
		{
			if (this._mainDirectiveMasterPageSet)
			{
				return;
			}
			if (this._configMasterPageFile != null)
			{
				int lineNumber = this._lineNumber;
				this._lineNumber = this._mainDirectiveLineNumber;
				try
				{
					if (this._configMasterPageFile.Length > 0)
					{
						Type referencedType = base.GetReferencedType(this._configMasterPageFile);
						if (!typeof(MasterPage).IsAssignableFrom(referencedType))
						{
							base.ProcessError(SR.GetString("Invalid_master_base", new object[]
							{
								this._configMasterPageFile
							}));
						}
					}
					if (((FileLevelPageControlBuilder)base.RootBuilder).ContentBuilderEntries != null)
					{
						base.RootBuilder.SetControlType(base.BaseType);
						base.RootBuilder.PreprocessAttribute(string.Empty, "MasterPageFile", this._configMasterPageFile, true, 0, 0);
					}
				}
				finally
				{
					this._lineNumber = lineNumber;
				}
			}
			this._mainDirectiveMasterPageSet = true;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0006EDF0 File Offset: 0x0006CFF0
		internal override void HandlePostParse()
		{
			base.HandlePostParse();
			this.EnsureMasterPageFileFromConfigApplied();
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0006EE00 File Offset: 0x0006D000
		internal override void ProcessConfigSettings()
		{
			base.ProcessConfigSettings();
			if (base.PagesConfig != null)
			{
				if (!base.PagesConfig.Buffer)
				{
					this._mainDirectiveConfigSettings["buffer"] = Util.GetStringFromBool(base.PagesConfig.Buffer);
				}
				if (!base.PagesConfig.EnableViewStateMac)
				{
					this._mainDirectiveConfigSettings["enableviewstatemac"] = Util.GetStringFromBool(base.PagesConfig.EnableViewStateMac);
				}
				if (!base.PagesConfig.EnableEventValidation)
				{
					this._mainDirectiveConfigSettings["enableEventValidation"] = Util.GetStringFromBool(base.PagesConfig.EnableEventValidation);
				}
				if (base.PagesConfig.SmartNavigation)
				{
					this._mainDirectiveConfigSettings["smartnavigation"] = Util.GetStringFromBool(base.PagesConfig.SmartNavigation);
				}
				if (base.PagesConfig.ThemeInternal != null && base.PagesConfig.Theme.Length != 0)
				{
					this._mainDirectiveConfigSettings["theme"] = base.PagesConfig.Theme;
				}
				if (base.PagesConfig.StyleSheetThemeInternal != null && base.PagesConfig.StyleSheetThemeInternal.Length != 0)
				{
					this._mainDirectiveConfigSettings["stylesheettheme"] = base.PagesConfig.StyleSheetThemeInternal;
				}
				if (base.PagesConfig.MasterPageFileInternal != null && base.PagesConfig.MasterPageFileInternal.Length != 0)
				{
					this._configMasterPageFile = base.PagesConfig.MasterPageFileInternal;
				}
				if (base.PagesConfig.ViewStateEncryptionMode != ViewStateEncryptionMode.Auto)
				{
					this._mainDirectiveConfigSettings["viewStateEncryptionMode"] = Enum.Format(typeof(ViewStateEncryptionMode), base.PagesConfig.ViewStateEncryptionMode, "G");
				}
				if (base.PagesConfig.MaintainScrollPositionOnPostBack)
				{
					this._mainDirectiveConfigSettings["maintainScrollPositionOnPostBack"] = Util.GetStringFromBool(base.PagesConfig.MaintainScrollPositionOnPostBack);
				}
				if (base.PagesConfig.MaxPageStateFieldLength != Page.DefaultMaxPageStateFieldLength)
				{
					this._mainDirectiveConfigSettings["maxPageStateFieldLength"] = base.PagesConfig.MaxPageStateFieldLength;
				}
				this.flags[1048576] = (base.PagesConfig.EnableSessionState == PagesEnableSessionState.True || base.PagesConfig.EnableSessionState == PagesEnableSessionState.ReadOnly);
				this.flags[2097152] = (base.PagesConfig.EnableSessionState == PagesEnableSessionState.ReadOnly);
				this.flags[4194304] = base.PagesConfig.ValidateRequest;
				this.flags[64] = HttpRuntime.ApartmentThreading;
			}
			this.ApplyBaseType();
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0006F098 File Offset: 0x0006D298
		private void ApplyBaseType()
		{
			if (PageParser.DefaultPageBaseType != null)
			{
				base.BaseType = PageParser.DefaultPageBaseType;
				return;
			}
			if (base.PagesConfig != null && base.PagesConfig.PageBaseTypeInternal != null)
			{
				base.BaseType = base.PagesConfig.PageBaseTypeInternal;
			}
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0006F0EC File Offset: 0x0006D2EC
		internal override void ProcessDirective(string directiveName, IDictionary directive)
		{
			if (StringUtil.EqualsIgnoreCase(directiveName, "previousPageType"))
			{
				if (this._previousPageType != null)
				{
					base.ProcessError(SR.GetString("Only_one_directive_allowed", new object[]
					{
						directiveName
					}));
					return;
				}
				this._previousPageType = base.GetDirectiveType(directive, directiveName);
				Util.CheckAssignableType(typeof(Page), this._previousPageType);
				return;
			}
			else
			{
				if (!StringUtil.EqualsIgnoreCase(directiveName, "masterType"))
				{
					base.ProcessDirective(directiveName, directive);
					return;
				}
				if (this._masterPageType != null)
				{
					base.ProcessError(SR.GetString("Only_one_directive_allowed", new object[]
					{
						directiveName
					}));
					return;
				}
				this._masterPageType = base.GetDirectiveType(directive, directiveName);
				Util.CheckAssignableType(typeof(MasterPage), this._masterPageType);
				return;
			}
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0006F1B5 File Offset: 0x0006D3B5
		internal override void ProcessMainDirective(IDictionary mainDirective)
		{
			this._mainDirectiveLineNumber = this._lineNumber;
			base.ProcessMainDirective(mainDirective);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0006F1CC File Offset: 0x0006D3CC
		internal override bool ProcessMainDirectiveAttribute(string deviceName, string name, string value, IDictionary parseData)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2060239653U)
			{
				if (num <= 1500367399U)
				{
					if (num <= 213248501U)
					{
						if (num != 112055478U)
						{
							if (num == 213248501U)
							{
								if (name == "tracemode")
								{
									object enumAttribute = Util.GetEnumAttribute(name, value, typeof(PageParser.TraceModeInternal));
									this._traceMode = (TraceMode)enumAttribute;
									goto IL_722;
								}
							}
						}
						else if (name == "stylesheettheme")
						{
							base.ValidateBuiltInAttribute(deviceName, name, value);
							Util.CheckThemeAttribute(value);
							this._styleSheetTheme = value;
							return true;
						}
					}
					else if (num != 550861556U)
					{
						if (num != 1352603410U)
						{
							if (num == 1500367399U)
							{
								if (name == "enablesessionstate")
								{
									this.flags[1048576] = true;
									this.flags[2097152] = false;
									if (Util.IsFalseString(value))
									{
										this.flags[1048576] = false;
									}
									else if (StringUtil.EqualsIgnoreCase(value, "readonly"))
									{
										this.flags[2097152] = true;
									}
									else if (!Util.IsTrueString(value))
									{
										base.ProcessError(SR.GetString("Enablesessionstate_must_be_true_false_or_readonly"));
									}
									if (this.flags[1048576])
									{
										base.OnFoundAttributeRequiringCompilation(name);
										goto IL_722;
									}
									goto IL_722;
								}
							}
						}
						else if (name == "contenttype")
						{
							Util.GetNonEmptyAttribute(name, value);
							return false;
						}
					}
					else if (name == "validaterequest")
					{
						this.flags[4194304] = Util.GetBooleanAttribute(name, value);
						goto IL_722;
					}
				}
				else if (num <= 1674686130U)
				{
					if (num != 1648016945U)
					{
						if (num == 1674686130U)
						{
							if (name == "errorpage")
							{
								this._errorPage = Util.GetNonEmptyAttribute(name, value);
								return false;
							}
						}
					}
					else if (name == "aspcompat")
					{
						base.OnFoundAttributeRequiringCompilation(name);
						this.flags[64] = Util.GetBooleanAttribute(name, value);
						if (this.flags[64] && !HttpRuntime.HasUnmanagedPermission())
						{
							throw new HttpException(SR.GetString("Insufficient_trust_for_attribute", new object[]
							{
								"AspCompat"
							}));
						}
						goto IL_722;
					}
				}
				else if (num != 1801295439U)
				{
					if (num != 1963546403U)
					{
						if (num == 2060239653U)
						{
							if (name == "lcid")
							{
								if (base.IsExpressionBuilderValue(value))
								{
									return false;
								}
								this._lcid = Util.GetNonNegativeIntegerAttribute(name, value);
								try
								{
									HttpServerUtility.CreateReadOnlyCultureInfo(this._lcid);
								}
								catch
								{
									base.ProcessError(SR.GetString("Invalid_attribute_value", new object[]
									{
										this._lcid.ToString(CultureInfo.InvariantCulture),
										"lcid"
									}));
								}
								return false;
							}
						}
					}
					else if (name == "transaction")
					{
						base.OnFoundAttributeRequiringCompilation(name);
						this.ParseTransactionAttribute(name, value);
						goto IL_722;
					}
				}
				else if (name == "responseencoding")
				{
					if (base.IsExpressionBuilderValue(value))
					{
						return false;
					}
					this._responseEncoding = Util.GetNonEmptyAttribute(name, value);
					Encoding.GetEncoding(this._responseEncoding);
					return false;
				}
			}
			else if (num <= 3146036504U)
			{
				if (num <= 2573005743U)
				{
					if (num != 2168288686U)
					{
						if (num == 2573005743U)
						{
							if (name == "codepage")
							{
								if (base.IsExpressionBuilderValue(value))
								{
									return false;
								}
								this._codePage = Util.GetNonNegativeIntegerAttribute(name, value);
								Encoding.GetEncoding(this._codePage);
								return false;
							}
						}
					}
					else if (name == "trace")
					{
						bool booleanAttribute = Util.GetBooleanAttribute(name, value);
						if (booleanAttribute)
						{
							this._traceEnabled = TraceEnable.Enable;
							goto IL_722;
						}
						this._traceEnabled = TraceEnable.Disable;
						goto IL_722;
					}
				}
				else if (num != 2717370895U)
				{
					if (num != 2834462706U)
					{
						if (num == 3146036504U)
						{
							if (name == "smartnavigation")
							{
								base.ValidateBuiltInAttribute(deviceName, name, value);
								bool booleanAttribute2 = Util.GetBooleanAttribute(name, value);
								return !booleanAttribute2;
							}
						}
					}
					else if (name == "theme")
					{
						if (base.IsExpressionBuilderValue(value))
						{
							return false;
						}
						Util.CheckThemeAttribute(value);
						return false;
					}
				}
				else if (name == "async")
				{
					base.OnFoundAttributeRequiringCompilation(name);
					this.flags[8388608] = Util.GetBooleanAttribute(name, value);
					if (!HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
					{
						throw new HttpException(SR.GetString("Insufficient_trust_for_attribute", new object[]
						{
							"async"
						}));
					}
					goto IL_722;
				}
			}
			else if (num <= 3303907537U)
			{
				if (num != 3168428190U)
				{
					if (num == 3303907537U)
					{
						if (name == "culture")
						{
							this._culture = Util.GetNonEmptyAttribute(name, value);
							if (!HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
							{
								throw new HttpException(SR.GetString("Insufficient_trust_for_attribute", new object[]
								{
									"culture"
								}));
							}
							if (StringUtil.EqualsIgnoreCase(value, HttpApplication.AutoCulture))
							{
								return false;
							}
							CultureInfo cultureInfo;
							try
							{
								if (StringUtil.StringStartsWithIgnoreCase(value, HttpApplication.AutoCulture))
								{
									this._culture = this._culture.Substring(5);
								}
								cultureInfo = HttpServerUtility.CreateReadOnlyCultureInfo(this._culture);
							}
							catch
							{
								base.ProcessError(SR.GetString("Invalid_attribute_value", new object[]
								{
									this._culture,
									"culture"
								}));
								return false;
							}
							if (cultureInfo.IsNeutralCulture)
							{
								base.ProcessError(SR.GetString("Invalid_culture_attribute", new object[]
								{
									Util.GetSpecificCulturesFormattedList(cultureInfo)
								}));
							}
							return false;
						}
					}
				}
				else if (name == "masterpagefile")
				{
					if (base.IsExpressionBuilderValue(value))
					{
						return false;
					}
					if (value.Length > 0)
					{
						Type referencedType = base.GetReferencedType(value);
						if (!typeof(MasterPage).IsAssignableFrom(referencedType))
						{
							base.ProcessError(SR.GetString("Invalid_master_base", new object[]
							{
								value
							}));
						}
						if (deviceName.Length > 0)
						{
							this.EnsureMasterPageFileFromConfigApplied();
						}
					}
					this._mainDirectiveMasterPageSet = true;
					return false;
				}
			}
			else if (num != 3479137834U)
			{
				if (num != 3616602127U)
				{
					if (num == 4174725811U)
					{
						if (name == "uiculture")
						{
							Util.GetNonEmptyAttribute(name, value);
							return false;
						}
					}
				}
				else if (name == "clienttarget")
				{
					if (base.IsExpressionBuilderValue(value))
					{
						return false;
					}
					HttpCapabilitiesDefaultProvider.GetUserAgentFromClientTarget(base.CurrentVirtualPath, value);
					return false;
				}
			}
			else if (name == "maintainscrollpositiononpostback")
			{
				bool booleanAttribute3 = Util.GetBooleanAttribute(name, value);
				return !booleanAttribute3;
			}
			return base.ProcessMainDirectiveAttribute(deviceName, name, value, parseData);
			IL_722:
			base.ValidateBuiltInAttribute(deviceName, name, value);
			return true;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0006F924 File Offset: 0x0006DB24
		internal override void ProcessUnknownMainDirectiveAttribute(string filter, string attribName, string value)
		{
			if (attribName == "asynctimeout")
			{
				int nonNegativeIntegerAttribute = Util.GetNonNegativeIntegerAttribute(attribName, value);
				value = new TimeSpan(0, 0, nonNegativeIntegerAttribute).ToString();
			}
			base.ProcessUnknownMainDirectiveAttribute(filter, attribName, value);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0006F968 File Offset: 0x0006DB68
		internal override void PostProcessMainDirectiveAttributes(IDictionary parseData)
		{
			if (!this.flags[524288] && this._errorPage != null)
			{
				base.ProcessError(SR.GetString("Error_page_not_supported_when_buffering_off"));
				return;
			}
			if (this._culture != null && this._lcid > 0)
			{
				base.ProcessError(SR.GetString("Attributes_mutually_exclusive", new object[]
				{
					"Culture",
					"LCID"
				}));
				return;
			}
			if (this._responseEncoding != null && this._codePage > 0)
			{
				base.ProcessError(SR.GetString("Attributes_mutually_exclusive", new object[]
				{
					"ResponseEncoding",
					"CodePage"
				}));
				return;
			}
			if (this.AsyncMode && this.AspCompatMode)
			{
				base.ProcessError(SR.GetString("Async_and_aspcompat"));
				return;
			}
			if (this.AsyncMode && this._transactionMode != 0)
			{
				base.ProcessError(SR.GetString("Async_and_transaction"));
				return;
			}
			base.PostProcessMainDirectiveAttributes(parseData);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0006FA5C File Offset: 0x0006DC5C
		private void ParseTransactionAttribute(string name, string value)
		{
			object enumAttribute = Util.GetEnumAttribute(name, value, typeof(TransactionOption));
			if (enumAttribute != null)
			{
				this._transactionMode = (int)enumAttribute;
				if (this._transactionMode != 0)
				{
					if (!HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
					{
						throw new HttpException(SR.GetString("Insufficient_trust_for_attribute", new object[]
						{
							"transaction"
						}));
					}
					base.AddAssemblyDependency(typeof(TransactionOption).Assembly);
				}
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x00054F3D File Offset: 0x0005313D
		internal override string DefaultDirectiveName
		{
			get
			{
				return "page";
			}
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0006FAD4 File Offset: 0x0006DCD4
		internal override void ProcessOutputCacheDirective(string directiveName, IDictionary directive)
		{
			bool noStore = false;
			string andRemoveNonEmptyAttribute = Util.GetAndRemoveNonEmptyAttribute(directive, "varybycontentencoding");
			if (andRemoveNonEmptyAttribute != null)
			{
				base.OutputCacheParameters.VaryByContentEncoding = andRemoveNonEmptyAttribute;
			}
			string andRemoveNonEmptyAttribute2 = Util.GetAndRemoveNonEmptyAttribute(directive, "varybyheader");
			if (andRemoveNonEmptyAttribute2 != null)
			{
				base.OutputCacheParameters.VaryByHeader = andRemoveNonEmptyAttribute2;
			}
			object andRemoveEnumAttribute = Util.GetAndRemoveEnumAttribute(directive, typeof(OutputCacheLocation), "location");
			if (andRemoveEnumAttribute != null)
			{
				this._outputCacheLocation = (OutputCacheLocation)andRemoveEnumAttribute;
				base.OutputCacheParameters.Location = this._outputCacheLocation;
			}
			string andRemoveNonEmptyAttribute3 = Util.GetAndRemoveNonEmptyAttribute(directive, "sqldependency");
			if (andRemoveNonEmptyAttribute3 != null)
			{
				base.OutputCacheParameters.SqlDependency = andRemoveNonEmptyAttribute3;
				SqlCacheDependency.ValidateOutputCacheDependencyString(andRemoveNonEmptyAttribute3, true);
			}
			if (Util.GetAndRemoveBooleanAttribute(directive, "nostore", ref noStore))
			{
				base.OutputCacheParameters.NoStore = noStore;
			}
			base.ProcessOutputCacheDirective(directiveName, directive);
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x0006FB97 File Offset: 0x0006DD97
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x0006FB9E File Offset: 0x0006DD9E
		public static Type DefaultPageBaseType
		{
			get
			{
				return PageParser.s_defaultPageBaseType;
			}
			set
			{
				if (value != null && !typeof(Page).IsAssignableFrom(value))
				{
					throw ExceptionUtil.PropertyInvalid("DefaultPageBaseType");
				}
				BuildManager.ThrowIfPreAppStartNotRunning();
				PageParser.s_defaultPageBaseType = value;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x0006FBD1 File Offset: 0x0006DDD1
		// (set) Token: 0x06002207 RID: 8711 RVA: 0x0006FBD8 File Offset: 0x0006DDD8
		public static Type DefaultUserControlBaseType
		{
			get
			{
				return PageParser.s_defaultUserContorlBaseType;
			}
			set
			{
				if (value != null && !typeof(UserControl).IsAssignableFrom(value))
				{
					throw ExceptionUtil.PropertyInvalid("DefaultUserControlBaseType");
				}
				BuildManager.ThrowIfPreAppStartNotRunning();
				PageParser.s_defaultUserContorlBaseType = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x0006FC0B File Offset: 0x0006DE0B
		// (set) Token: 0x06002209 RID: 8713 RVA: 0x0006FC12 File Offset: 0x0006DE12
		public static Type DefaultApplicationBaseType
		{
			get
			{
				return PageParser.s_defaultApplicationBaseType;
			}
			set
			{
				if (value != null && !typeof(HttpApplication).IsAssignableFrom(value))
				{
					throw ExceptionUtil.PropertyInvalid("DefaultApplicationBaseType");
				}
				BuildManager.ThrowIfPreAppStartNotRunning();
				PageParser.s_defaultApplicationBaseType = value;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x0006FC45 File Offset: 0x0006DE45
		// (set) Token: 0x0600220B RID: 8715 RVA: 0x0006FC4C File Offset: 0x0006DE4C
		public static Type DefaultPageParserFilterType
		{
			get
			{
				return PageParser.s_defaultPageParserFilterType;
			}
			set
			{
				if (value != null && !typeof(PageParserFilter).IsAssignableFrom(value))
				{
					throw ExceptionUtil.PropertyInvalid("DefaultPageParserFilterType");
				}
				BuildManager.ThrowIfPreAppStartNotRunning();
				PageParser.s_defaultPageParserFilterType = value;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x0006FC7F File Offset: 0x0006DE7F
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x0006FC86 File Offset: 0x0006DE86
		public static bool EnableLongStringsAsResources
		{
			get
			{
				return PageParser.s_enableLongStringsAsResources;
			}
			set
			{
				BuildManager.ThrowIfPreAppStartNotRunning();
				PageParser.s_enableLongStringsAsResources = value;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x0006FC93 File Offset: 0x0006DE93
		internal override bool FDurationRequiredOnOutputCache
		{
			get
			{
				return this._outputCacheLocation != OutputCacheLocation.None;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x0006FC93 File Offset: 0x0006DE93
		internal override bool FVaryByParamsRequiredOnOutputCache
		{
			get
			{
				return this._outputCacheLocation != OutputCacheLocation.None;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x0006FCA1 File Offset: 0x0006DEA1
		internal override string UnknownOutputCacheAttributeError
		{
			get
			{
				return "Attr_not_supported_in_pagedirective";
			}
		}

		// Token: 0x04001BFE RID: 7166
		private int _transactionMode;

		// Token: 0x04001BFF RID: 7167
		private TraceMode _traceMode = TraceMode.Default;

		// Token: 0x04001C00 RID: 7168
		private TraceEnable _traceEnabled;

		// Token: 0x04001C01 RID: 7169
		private int _codePage;

		// Token: 0x04001C02 RID: 7170
		private string _responseEncoding;

		// Token: 0x04001C03 RID: 7171
		private int _lcid;

		// Token: 0x04001C04 RID: 7172
		private string _culture;

		// Token: 0x04001C05 RID: 7173
		private int _mainDirectiveLineNumber = 1;

		// Token: 0x04001C06 RID: 7174
		private bool _mainDirectiveMasterPageSet;

		// Token: 0x04001C07 RID: 7175
		private OutputCacheLocation _outputCacheLocation;

		// Token: 0x04001C08 RID: 7176
		private string _errorPage;

		// Token: 0x04001C09 RID: 7177
		private string _styleSheetTheme;

		// Token: 0x04001C0A RID: 7178
		private Type _previousPageType;

		// Token: 0x04001C0B RID: 7179
		private Type _masterPageType;

		// Token: 0x04001C0C RID: 7180
		private string _configMasterPageFile;

		// Token: 0x04001C0D RID: 7181
		private static object s_lock = new object();

		// Token: 0x04001C0E RID: 7182
		internal const string defaultDirectiveName = "page";

		// Token: 0x04001C0F RID: 7183
		private static Type s_defaultPageBaseType;

		// Token: 0x04001C10 RID: 7184
		private static Type s_defaultUserContorlBaseType;

		// Token: 0x04001C11 RID: 7185
		private static Type s_defaultApplicationBaseType;

		// Token: 0x04001C12 RID: 7186
		private static Type s_defaultPageParserFilterType;

		// Token: 0x04001C13 RID: 7187
		private static bool s_enableLongStringsAsResources = true;

		// Token: 0x0200097F RID: 2431
		private enum TraceModeInternal
		{
			// Token: 0x040038B6 RID: 14518
			SortByTime,
			// Token: 0x040038B7 RID: 14519
			SortByCategory
		}
	}
}
