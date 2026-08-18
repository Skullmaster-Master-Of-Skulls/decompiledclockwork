using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000310 RID: 784
	public abstract class TemplateControlParser : BaseTemplateParser
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x00076805 File Offset: 0x00074A05
		internal OutputCacheParameters OutputCacheParameters
		{
			get
			{
				return this._outputCacheSettings;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x0007680D File Offset: 0x00074A0D
		internal bool FAutoEventWireup
		{
			get
			{
				return !this.flags[131072];
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x00076822 File Offset: 0x00074A22
		internal override bool RequiresCompilation
		{
			get
			{
				return this.flags[16] || base.CompilationMode == CompilationMode.Always;
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x00076840 File Offset: 0x00074A40
		internal override void ProcessConfigSettings()
		{
			base.ProcessConfigSettings();
			if (base.PagesConfig != null)
			{
				this.flags[131072] = !base.PagesConfig.AutoEventWireup;
				if (!base.PagesConfig.EnableViewState)
				{
					this._mainDirectiveConfigSettings["enableviewstate"] = Util.GetStringFromBool(base.PagesConfig.EnableViewState);
				}
				base.CompilationMode = base.PagesConfig.CompilationMode;
			}
			if (this._pageParserFilter != null)
			{
				base.CompilationMode = this._pageParserFilter.GetCompilationMode(base.CompilationMode);
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000768D8 File Offset: 0x00074AD8
		internal override void ProcessDirective(string directiveName, IDictionary directive)
		{
			if (StringUtil.EqualsIgnoreCase(directiveName, "outputcache"))
			{
				if (this.FInDesigner)
				{
					return;
				}
				if (this._outputCacheSettings == null)
				{
					this._outputCacheSettings = new OutputCacheParameters();
				}
				if (this._outputCacheDirective != null)
				{
					throw new HttpException(SR.GetString("Only_one_directive_allowed", new object[]
					{
						directiveName
					}));
				}
				this.ProcessOutputCacheDirective(directiveName, directive);
				this._outputCacheDirective = directive;
				return;
			}
			else
			{
				if (!StringUtil.EqualsIgnoreCase(directiveName, "reference"))
				{
					base.ProcessDirective(directiveName, directive);
					return;
				}
				if (this.FInDesigner)
				{
					return;
				}
				VirtualPath virtualPath = Util.GetAndRemoveVirtualPathAttribute(directive, "virtualpath");
				bool flag = false;
				bool flag2 = false;
				VirtualPath andRemoveVirtualPathAttribute = Util.GetAndRemoveVirtualPathAttribute(directive, "page");
				if (andRemoveVirtualPathAttribute != null)
				{
					if (virtualPath != null)
					{
						base.ProcessError(SR.GetString("Invalid_reference_directive"));
						return;
					}
					virtualPath = andRemoveVirtualPathAttribute;
					flag = true;
				}
				andRemoveVirtualPathAttribute = Util.GetAndRemoveVirtualPathAttribute(directive, "control");
				if (andRemoveVirtualPathAttribute != null)
				{
					if (virtualPath != null)
					{
						base.ProcessError(SR.GetString("Invalid_reference_directive"));
						return;
					}
					virtualPath = andRemoveVirtualPathAttribute;
					flag2 = true;
				}
				if (virtualPath == null)
				{
					base.ProcessError(SR.GetString("Invalid_reference_directive"));
					return;
				}
				Type referencedType = base.GetReferencedType(virtualPath);
				if (referencedType == null)
				{
					base.ProcessError(SR.GetString("Invalid_reference_directive_attrib", new object[]
					{
						virtualPath
					}));
				}
				if (flag && !typeof(Page).IsAssignableFrom(referencedType))
				{
					base.ProcessError(SR.GetString("Invalid_reference_directive_attrib", new object[]
					{
						virtualPath
					}));
				}
				if (flag2 && !typeof(UserControl).IsAssignableFrom(referencedType))
				{
					base.ProcessError(SR.GetString("Invalid_reference_directive_attrib", new object[]
					{
						virtualPath
					}));
				}
				Util.CheckUnknownDirectiveAttributes(directiveName, directive);
				return;
			}
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00076A88 File Offset: 0x00074C88
		internal override void ProcessMainDirective(IDictionary mainDirective)
		{
			object obj = null;
			try
			{
				obj = Util.GetAndRemoveEnumAttribute(mainDirective, typeof(CompilationMode), "compilationmode");
			}
			catch (Exception ex)
			{
				base.ProcessError(ex.Message);
			}
			if (obj != null)
			{
				base.CompilationMode = (CompilationMode)obj;
				if (this._pageParserFilter != null)
				{
					base.CompilationMode = this._pageParserFilter.GetCompilationMode(base.CompilationMode);
				}
			}
			base.ProcessMainDirective(mainDirective);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00076B04 File Offset: 0x00074D04
		internal override bool ProcessMainDirectiveAttribute(string deviceName, string name, string value, IDictionary parseData)
		{
			if (!(name == "targetschema"))
			{
				if (!(name == "autoeventwireup"))
				{
					if (name == "enabletheming")
					{
						return false;
					}
					if (!(name == "codefilebaseclass"))
					{
						return base.ProcessMainDirectiveAttribute(deviceName, name, value, parseData);
					}
					parseData[name] = Util.GetNonEmptyAttribute(name, value);
				}
				else
				{
					base.OnFoundAttributeRequiringCompilation(name);
					this.flags[131072] = !Util.GetBooleanAttribute(name, value);
				}
			}
			base.ValidateBuiltInAttribute(deviceName, name, value);
			return true;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00076B94 File Offset: 0x00074D94
		internal override void ProcessUnknownMainDirectiveAttribute(string filter, string attribName, string value)
		{
			if (attribName == "id")
			{
				base.ProcessUnknownMainDirectiveAttribute(filter, attribName, value);
				return;
			}
			try
			{
				base.RootBuilder.PreprocessAttribute(filter, attribName, value, true, 0, 0);
			}
			catch (Exception ex)
			{
				base.ProcessError(SR.GetString("Attrib_parse_error", new object[]
				{
					attribName,
					ex.Message
				}));
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00076C04 File Offset: 0x00074E04
		private void AddStaticObjectAssemblyDependencies(HttpStaticObjectsCollection staticObjects)
		{
			if (staticObjects == null || staticObjects.Objects == null)
			{
				return;
			}
			IDictionaryEnumerator enumerator = staticObjects.Objects.GetEnumerator();
			while (enumerator.MoveNext())
			{
				HttpStaticObjectsEntry httpStaticObjectsEntry = (HttpStaticObjectsEntry)enumerator.Value;
				base.AddTypeDependency(httpStaticObjectsEntry.ObjectType);
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00076C4C File Offset: 0x00074E4C
		internal Type GetDirectiveType(IDictionary directive, string directiveName)
		{
			string andRemoveNonEmptyNoSpaceAttribute = Util.GetAndRemoveNonEmptyNoSpaceAttribute(directive, "typeName");
			VirtualPath andRemoveVirtualPathAttribute = Util.GetAndRemoveVirtualPathAttribute(directive, "virtualPath");
			if (andRemoveNonEmptyNoSpaceAttribute == null == (andRemoveVirtualPathAttribute == null))
			{
				throw new HttpException(SR.GetString("Invalid_typeNameOrVirtualPath_directive", new object[]
				{
					directiveName
				}));
			}
			Type type;
			if (andRemoveNonEmptyNoSpaceAttribute != null)
			{
				type = base.GetType(andRemoveNonEmptyNoSpaceAttribute);
				base.AddTypeDependency(type);
			}
			else
			{
				type = base.GetReferencedType(andRemoveVirtualPathAttribute);
			}
			Util.CheckUnknownDirectiveAttributes(directiveName, directive);
			return type;
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x00076CC0 File Offset: 0x00074EC0
		internal override void HandlePostParse()
		{
			base.HandlePostParse();
			if (!this.FInDesigner)
			{
				if (base.ScriptList.Count == 0 && base.BaseType == this.DefaultBaseType && base.CodeFileVirtualPath == null)
				{
					this.flags[131072] = true;
				}
				this._applicationObjects = HttpApplicationFactory.ApplicationState.StaticObjects;
				this.AddStaticObjectAssemblyDependencies(this._applicationObjects);
				this._sessionObjects = HttpApplicationFactory.ApplicationState.SessionStaticObjects;
				this.AddStaticObjectAssemblyDependencies(this._sessionObjects);
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00076D54 File Offset: 0x00074F54
		internal virtual void ProcessOutputCacheDirective(string directiveName, IDictionary directive)
		{
			int duration = 0;
			string text = null;
			bool andRemovePositiveIntegerAttribute = Util.GetAndRemovePositiveIntegerAttribute(directive, "duration", ref duration);
			if (andRemovePositiveIntegerAttribute)
			{
				this.OutputCacheParameters.Duration = duration;
			}
			if (this is PageParser)
			{
				text = Util.GetAndRemoveNonEmptyAttribute(directive, "cacheProfile");
				if (text != null)
				{
					this.OutputCacheParameters.CacheProfile = text;
				}
			}
			if (!andRemovePositiveIntegerAttribute && (text == null || text.Length == 0) && this.FDurationRequiredOnOutputCache)
			{
				throw new HttpException(SR.GetString("Missing_attr", new object[]
				{
					"duration"
				}));
			}
			string andRemoveNonEmptyAttribute = Util.GetAndRemoveNonEmptyAttribute(directive, "varybycustom");
			if (andRemoveNonEmptyAttribute != null)
			{
				this.OutputCacheParameters.VaryByCustom = andRemoveNonEmptyAttribute;
			}
			string andRemoveNonEmptyAttribute2 = Util.GetAndRemoveNonEmptyAttribute(directive, "varybycontrol");
			if (andRemoveNonEmptyAttribute2 != null)
			{
				this.OutputCacheParameters.VaryByControl = andRemoveNonEmptyAttribute2;
			}
			string andRemoveNonEmptyAttribute3 = Util.GetAndRemoveNonEmptyAttribute(directive, "varybyparam");
			if (andRemoveNonEmptyAttribute3 != null)
			{
				this.OutputCacheParameters.VaryByParam = andRemoveNonEmptyAttribute3;
			}
			if (andRemoveNonEmptyAttribute3 == null && andRemoveNonEmptyAttribute2 == null && (text == null || text.Length == 0) && this.FVaryByParamsRequiredOnOutputCache)
			{
				throw new HttpException(SR.GetString("Missing_varybyparam_attr"));
			}
			if (StringUtil.EqualsIgnoreCase(andRemoveNonEmptyAttribute3, "none"))
			{
				this.OutputCacheParameters.VaryByParam = null;
			}
			if (StringUtil.EqualsIgnoreCase(andRemoveNonEmptyAttribute2, "none"))
			{
				this.OutputCacheParameters.VaryByControl = null;
			}
			Util.CheckUnknownDirectiveAttributes(directiveName, directive, this.UnknownOutputCacheAttributeError);
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x000097B7 File Offset: 0x000079B7
		internal virtual bool FDurationRequiredOnOutputCache
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x000097B7 File Offset: 0x000079B7
		internal virtual bool FVaryByParamsRequiredOnOutputCache
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002462 RID: 9314
		internal abstract string UnknownOutputCacheAttributeError { get; }

		// Token: 0x04001D03 RID: 7427
		private IDictionary _outputCacheDirective;

		// Token: 0x04001D04 RID: 7428
		private OutputCacheParameters _outputCacheSettings;
	}
}
