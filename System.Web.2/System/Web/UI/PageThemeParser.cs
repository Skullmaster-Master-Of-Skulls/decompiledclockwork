using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002E1 RID: 737
	internal class PageThemeParser : BaseTemplateParser
	{
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x000702B1 File Offset: 0x0006E4B1
		internal VirtualPath VirtualDirPath
		{
			get
			{
				return this._virtualDirPath;
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000702B9 File Offset: 0x0006E4B9
		internal PageThemeParser(VirtualPath virtualDirPath, IList skinFileList, IList cssFileList)
		{
			this._virtualDirPath = virtualDirPath;
			this._skinFileList = skinFileList;
			this._cssFileList = cssFileList;
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x000702D6 File Offset: 0x0006E4D6
		internal ICollection CssFileList
		{
			get
			{
				return this._cssFileList;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x000702DE File Offset: 0x0006E4DE
		internal override Type DefaultBaseType
		{
			get
			{
				return typeof(PageTheme);
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x000702EA File Offset: 0x0006E4EA
		internal override string DefaultDirectiveName
		{
			get
			{
				return "skin";
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x00007722 File Offset: 0x00005922
		internal override bool IsCodeAllowed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x000702F1 File Offset: 0x0006E4F1
		// (set) Token: 0x06002257 RID: 8791 RVA: 0x000702F9 File Offset: 0x0006E4F9
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

		// Token: 0x06002258 RID: 8792 RVA: 0x00070302 File Offset: 0x0006E502
		internal override RootBuilder CreateDefaultFileLevelBuilder()
		{
			return new FileLevelPageThemeBuilder();
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x0007030C File Offset: 0x0006E50C
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

		// Token: 0x0600225A RID: 8794 RVA: 0x0007037C File Offset: 0x0006E57C
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

		// Token: 0x0600225B RID: 8795 RVA: 0x0007040C File Offset: 0x0006E60C
		internal override bool ProcessMainDirectiveAttribute(string deviceName, string name, string value, IDictionary parseData)
		{
			if (name == "classname" || name == "compilationmode" || name == "inherits")
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

		// Token: 0x04001C33 RID: 7219
		internal const string defaultDirectiveName = "skin";

		// Token: 0x04001C34 RID: 7220
		private bool _mainDirectiveProcessed;

		// Token: 0x04001C35 RID: 7221
		private IList _skinFileList;

		// Token: 0x04001C36 RID: 7222
		private IList _cssFileList;

		// Token: 0x04001C37 RID: 7223
		private ControlBuilder _currentSkinBuilder;

		// Token: 0x04001C38 RID: 7224
		private VirtualPath _virtualDirPath;
	}
}
