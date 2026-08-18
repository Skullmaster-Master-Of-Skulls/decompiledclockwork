using System;
using System.Text.RegularExpressions;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.RegularExpressions;

namespace System.Web.UI
{
	// Token: 0x02000240 RID: 576
	public class BaseParser
	{
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x0005431F File Offset: 0x0005251F
		internal VirtualPath BaseVirtualDir
		{
			get
			{
				return this._baseVirtualDir;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x00054327 File Offset: 0x00052527
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x0005432F File Offset: 0x0005252F
		internal VirtualPath CurrentVirtualPath
		{
			get
			{
				return this._currentVirtualPath;
			}
			set
			{
				this._currentVirtualPath = value;
				if (value == null)
				{
					return;
				}
				this._baseVirtualDir = value.Parent;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0005434E File Offset: 0x0005254E
		internal string CurrentVirtualPathString
		{
			get
			{
				return VirtualPath.GetVirtualPathString(this.CurrentVirtualPath);
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0005435B File Offset: 0x0005255B
		internal VirtualPath ResolveVirtualPath(VirtualPath virtualPath)
		{
			return VirtualPathProvider.CombineVirtualPathsInternal(this.CurrentVirtualPath, virtualPath);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00054369 File Offset: 0x00052569
		private bool IsVersion40OrAbove()
		{
			if (HostingEnvironment.IsHosted)
			{
				return MultiTargetingUtil.IsTargetFramework40OrAbove;
			}
			return TargetFrameworkUtil.IsSupportedType(typeof(TagRegex35));
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00054387 File Offset: 0x00052587
		internal Regex TagRegex
		{
			get
			{
				if (this._tagRegex == null)
				{
					this._tagRegex = (this.IsVersion40OrAbove() ? BaseParser.tagRegex40 : BaseParser.tagRegex35);
				}
				return this._tagRegex;
			}
		}

		// Token: 0x04001861 RID: 6241
		private VirtualPath _baseVirtualDir;

		// Token: 0x04001862 RID: 6242
		private VirtualPath _currentVirtualPath;

		// Token: 0x04001863 RID: 6243
		private Regex _tagRegex;

		// Token: 0x04001864 RID: 6244
		private static readonly Regex tagRegex35 = new TagRegex35();

		// Token: 0x04001865 RID: 6245
		private static readonly Regex tagRegex40 = new TagRegex();

		// Token: 0x04001866 RID: 6246
		internal static readonly Regex directiveRegex = new DirectiveRegex();

		// Token: 0x04001867 RID: 6247
		internal static readonly Regex endtagRegex = new EndTagRegex();

		// Token: 0x04001868 RID: 6248
		internal static readonly Regex aspCodeRegex = new AspCodeRegex();

		// Token: 0x04001869 RID: 6249
		internal static readonly Regex aspExprRegex = new AspExprRegex();

		// Token: 0x0400186A RID: 6250
		internal static readonly Regex aspEncodedExprRegex = new AspEncodedExprRegex();

		// Token: 0x0400186B RID: 6251
		internal static readonly Regex databindExprRegex = new DatabindExprRegex();

		// Token: 0x0400186C RID: 6252
		internal static readonly Regex commentRegex = new CommentRegex();

		// Token: 0x0400186D RID: 6253
		internal static readonly Regex includeRegex = new IncludeRegex();

		// Token: 0x0400186E RID: 6254
		internal static readonly Regex textRegex = new TextRegex();

		// Token: 0x0400186F RID: 6255
		internal static readonly Regex gtRegex = new GTRegex();

		// Token: 0x04001870 RID: 6256
		internal static readonly Regex ltRegex = new LTRegex();

		// Token: 0x04001871 RID: 6257
		internal static readonly Regex serverTagsRegex = new ServerTagsRegex();

		// Token: 0x04001872 RID: 6258
		internal static readonly Regex runatServerRegex = new RunatServerRegex();
	}
}
