using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200044B RID: 1099
	internal class PageThemeParser : BaseTemplateParser
	{
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000E3322 File Offset: 0x000E2322
		internal VirtualPath VirtualDirPath
		{
			get
			{
				return this._virtualDirPath;
			}
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000E332A File Offset: 0x000E232A
		internal PageThemeParser(VirtualPath virtualDirPath, IList skinFileList, IList cssFileList)
		{
			this._virtualDirPath = virtualDirPath;
			this._skinFileList = skinFileList;
			this._cssFileList = cssFileList;
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000E3347 File Offset: 0x000E2347
		internal ICollection CssFileList
		{
			get
			{
				return this._cssFileList;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003457 RID: 13399 RVA: 0x000E334F File Offset: 0x000E234F
		internal override Type DefaultBaseType
		{
			get
			{
				return typeof(PageTheme);
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000E335B File Offset: 0x000E235B
		internal override string DefaultDirectiveName
		{
			get
			{
				return "skin";
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06003459 RID: 13401 RVA: 0x000E3362 File Offset: 0x000E2362
		internal override bool IsCodeAllowed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000E3365 File Offset: 0x000E2365
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x000E336D File Offset: 0x000E236D
		internal ControlBuilder CurrentSkinBuilder
		{
			get
			{
				return this._currentSkinBuilder;
			}
			set
			{
				this._currentSkinBuilder = value;
			}
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000E3376 File Offset: 0x000E2376
		internal override RootBuilder CreateDefaultFileLevelBuilder()
		{
			return new FileLevelPageThemeBuilder();
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x000E3380 File Offset: 0x000E2380
		internal override void ParseInternal()
		{
			if (this._skinFileList != null)
			{
				foreach (object obj in this._skinFileList)
				{
					string virtualPath = (string)obj;
					base.ParseFile(null, virtualPath);
				}
			}
			base.AddSourceDependency(this._virtualDirPath);
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x000E33F0 File Offset: 0x000E23F0
		internal override void ProcessDirective(string directiveName, IDictionary directive)
		{
			if (directiveName == null || directiveName.Length == 0 || StringUtil.EqualsIgnoreCase(directiveName, this.DefaultDirectiveName))
			{
				if (this._mainDirectiveProcessed)
				{
					base.ProcessError(SR.GetString("Only_one_directive_allowed", new object[]
					{
						this.DefaultDirectiveName
					}));
					return;
				}
				this.ProcessMainDirective(directive);
				this._mainDirectiveProcessed = true;
				return;
			}
			else
			{
				if (StringUtil.EqualsIgnoreCase(directiveName, "register"))
				{
					base.ProcessDirective(directiveName, directive);
					return;
				}
				base.ProcessError(SR.GetString("Unknown_directive", new object[]
				{
					directiveName
				}));
				return;
			}
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000E3484 File Offset: 0x000E2484
		internal override bool ProcessMainDirectiveAttribute(string deviceName, string name, string value, IDictionary parseData)
		{
			if (name != null && (name == "classname" || name == "compilationmode" || name == "inherits"))
			{
				base.ProcessError(SR.GetString("Attr_not_supported_in_directive", new object[]
				{
					name,
					this.DefaultDirectiveName
				}));
				return false;
			}
			return base.ProcessMainDirectiveAttribute(deviceName, name, value, parseData);
		}

		// Token: 0x040024B0 RID: 9392
		internal const string defaultDirectiveName = "skin";

		// Token: 0x040024B1 RID: 9393
		private bool _mainDirectiveProcessed;

		// Token: 0x040024B2 RID: 9394
		private IList _skinFileList;

		// Token: 0x040024B3 RID: 9395
		private IList _cssFileList;

		// Token: 0x040024B4 RID: 9396
		private ControlBuilder _currentSkinBuilder;

		// Token: 0x040024B5 RID: 9397
		private VirtualPath _virtualDirPath;
	}
}
