using System;

namespace System.Web.UI
{
	// Token: 0x0200023D RID: 573
	internal sealed class ApplicationFileParser : TemplateParser
	{
		// Token: 0x06001AC5 RID: 6853 RVA: 0x00053FE4 File Offset: 0x000521E4
		internal ApplicationFileParser()
		{
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x00053FEC File Offset: 0x000521EC
		internal override Type DefaultBaseType
		{
			get
			{
				return PageParser.DefaultApplicationBaseType ?? typeof(HttpApplication);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool FApplicationFile
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x00054001 File Offset: 0x00052201
		internal override string DefaultDirectiveName
		{
			get
			{
				return "application";
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00054008 File Offset: 0x00052208
		internal override void CheckObjectTagScope(ref ObjectTagScope scope)
		{
			if (scope == ObjectTagScope.Default)
			{
				scope = ObjectTagScope.AppInstance;
			}
			if (scope == ObjectTagScope.Page)
			{
				throw new HttpException(SR.GetString("Page_scope_in_global_asax"));
			}
		}

		// Token: 0x0400185E RID: 6238
		internal const string defaultDirectiveName = "application";
	}
}
