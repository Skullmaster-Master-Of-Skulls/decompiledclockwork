using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration;

namespace System.Web.UI
{
	// Token: 0x020002DA RID: 730
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Medium)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Medium)]
	public abstract class PageParserFilter
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x0006FCBA File Offset: 0x0006DEBA
		protected string VirtualPath
		{
			get
			{
				return this._virtualPath.VirtualPathString;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x0006FCC7 File Offset: 0x0006DEC7
		protected int Line
		{
			get
			{
				return this._parser._lineNumber;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x0006FCD4 File Offset: 0x0006DED4
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x0006FCDC File Offset: 0x0006DEDC
		private protected bool CalledFromParseControl { protected get; private set; }

		// Token: 0x06002216 RID: 8726 RVA: 0x0006FCE8 File Offset: 0x0006DEE8
		internal static PageParserFilter Create(PagesSection pagesConfig, VirtualPath virtualPath, TemplateParser parser)
		{
			PageParserFilter pageParserFilter = pagesConfig.CreateControlTypeFilter();
			if (pageParserFilter != null)
			{
				pageParserFilter.InitializeInternal(virtualPath, parser);
			}
			return pageParserFilter;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0006FD08 File Offset: 0x0006DF08
		internal void InitializeInternal(VirtualPath virtualPath, TemplateParser parser)
		{
			this._parser = parser;
			this._virtualPath = virtualPath;
			this.Initialize();
			this._numberOfControlsAllowed = this.NumberOfControlsAllowed;
			this._dependenciesAllowed = this.TotalNumberOfDependenciesAllowed + 1;
			this._directDependenciesAllowed = this.NumberOfDirectDependenciesAllowed + 1;
			this.CalledFromParseControl = parser.flags[67108864];
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Initialize()
		{
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ParseComplete(ControlBuilder rootBuilder)
		{
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x00036414 File Offset: 0x00034614
		public virtual CompilationMode GetCompilationMode(CompilationMode current)
		{
			return current;
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool AllowCode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x0006FD67 File Offset: 0x0006DF67
		internal bool AllowControlInternal(Type controlType, ControlBuilder builder)
		{
			this.OnControlAdded();
			return this.AllowControl(controlType, builder);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool AllowControl(Type controlType, ControlBuilder builder)
		{
			return false;
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool AllowBaseType(Type baseType)
		{
			return false;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0006FD78 File Offset: 0x0006DF78
		internal bool AllowVirtualReference(CompilationSection compConfig, VirtualPath referenceVirtualPath)
		{
			string extension = referenceVirtualPath.Extension;
			Type buildProviderTypeFromExtension = CompilationUtil.GetBuildProviderTypeFromExtension(compConfig, extension, BuildProviderAppliesTo.Web, false);
			if (buildProviderTypeFromExtension == null)
			{
				return false;
			}
			VirtualReferenceType referenceType;
			if (buildProviderTypeFromExtension == typeof(PageBuildProvider))
			{
				referenceType = VirtualReferenceType.Page;
			}
			else if (buildProviderTypeFromExtension == typeof(UserControlBuildProvider))
			{
				referenceType = VirtualReferenceType.UserControl;
			}
			else if (buildProviderTypeFromExtension == typeof(MasterPageBuildProvider))
			{
				referenceType = VirtualReferenceType.Master;
			}
			else if (buildProviderTypeFromExtension == typeof(SourceFileBuildProvider))
			{
				referenceType = VirtualReferenceType.SourceFile;
			}
			else
			{
				referenceType = VirtualReferenceType.Other;
			}
			return this.AllowVirtualReference(referenceVirtualPath.VirtualPathString, referenceType);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool AllowVirtualReference(string referenceVirtualPath, VirtualReferenceType referenceType)
		{
			return false;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool AllowServerSideInclude(string includeVirtualPath)
		{
			return false;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void PreprocessDirective(string directiveName, IDictionary attributes)
		{
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x00007722 File Offset: 0x00005922
		public virtual int NumberOfControlsAllowed
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x00007722 File Offset: 0x00005922
		public virtual int TotalNumberOfDependenciesAllowed
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x00007722 File Offset: 0x00005922
		public virtual int NumberOfDirectDependenciesAllowed
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0006FE08 File Offset: 0x0006E008
		private void OnControlAdded()
		{
			if (this._numberOfControlsAllowed < 0)
			{
				return;
			}
			this._currentControlCount++;
			if (this._currentControlCount > this._numberOfControlsAllowed)
			{
				throw new HttpException(SR.GetString("Too_many_controls", new object[]
				{
					this._numberOfControlsAllowed.ToString(CultureInfo.CurrentCulture)
				}));
			}
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0006FE64 File Offset: 0x0006E064
		internal void OnDependencyAdded()
		{
			if (this._dependenciesAllowed <= 0)
			{
				return;
			}
			this._currentDependenciesCount++;
			if (this._currentDependenciesCount > this._dependenciesAllowed)
			{
				throw new HttpException(SR.GetString("Too_many_dependencies", new object[]
				{
					this.VirtualPath,
					this._dependenciesAllowed.ToString(CultureInfo.CurrentCulture)
				}));
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0006FECC File Offset: 0x0006E0CC
		internal void OnDirectDependencyAdded()
		{
			if (this._directDependenciesAllowed <= 0)
			{
				return;
			}
			this._currentDirectDependenciesCount++;
			if (this._currentDirectDependenciesCount > this._directDependenciesAllowed)
			{
				throw new HttpException(SR.GetString("Too_many_direct_dependencies", new object[]
				{
					this.VirtualPath,
					this._directDependenciesAllowed.ToString(CultureInfo.CurrentCulture)
				}));
			}
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool ProcessCodeConstruct(CodeConstructType codeType, string code)
		{
			return false;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool ProcessDataBindingAttribute(string controlId, string name, string value)
		{
			return false;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool ProcessEventHookup(string controlId, string eventName, string handlerName)
		{
			return false;
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual Type GetNoCompileUserControlType()
		{
			return null;
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0006FF31 File Offset: 0x0006E131
		protected void AddControl(Type type, IDictionary attributes)
		{
			this._parser.AddControl(type, attributes);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0006FF40 File Offset: 0x0006E140
		protected void SetPageProperty(string filter, string name, string value)
		{
			if (filter == null)
			{
				filter = string.Empty;
			}
			this._parser.RootBuilder.PreprocessAttribute(filter, name, value, true, 0, 0);
		}

		// Token: 0x04001C14 RID: 7188
		private VirtualPath _virtualPath;

		// Token: 0x04001C15 RID: 7189
		private TemplateParser _parser;

		// Token: 0x04001C17 RID: 7191
		private int _numberOfControlsAllowed;

		// Token: 0x04001C18 RID: 7192
		private int _currentControlCount;

		// Token: 0x04001C19 RID: 7193
		private int _dependenciesAllowed;

		// Token: 0x04001C1A RID: 7194
		private int _currentDependenciesCount;

		// Token: 0x04001C1B RID: 7195
		private int _directDependenciesAllowed;

		// Token: 0x04001C1C RID: 7196
		private int _currentDirectDependenciesCount;
	}
}
