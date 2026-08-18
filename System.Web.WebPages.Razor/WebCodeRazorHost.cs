using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;

namespace System.Web.WebPages.Razor
{
	// Token: 0x0200000F RID: 15
	public class WebCodeRazorHost : WebPageRazorHost
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000034AF File Offset: 0x000016AF
		public WebCodeRazorHost(string virtualPath) : base(virtualPath)
		{
			this.DefaultBaseClass = WebCodeRazorHost._helperPageBaseType;
			this.DefaultNamespace = WebCodeRazorHost.DetermineNamespace(virtualPath);
			base.DefaultDebugCompilation = false;
			this.StaticHelpers = true;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000034DD File Offset: 0x000016DD
		public WebCodeRazorHost(string virtualPath, string physicalPath) : base(virtualPath, physicalPath)
		{
			this.DefaultBaseClass = WebCodeRazorHost._helperPageBaseType;
			this.DefaultNamespace = WebCodeRazorHost.DetermineNamespace(virtualPath);
			base.DefaultDebugCompilation = false;
			this.StaticHelpers = true;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003520 File Offset: 0x00001720
		public override void PostProcessGeneratedCode(CodeGeneratorContext context)
		{
			base.PostProcessGeneratedCode(context);
			context.GeneratedClass.Members.Remove(context.TargetMethod);
			CodeMemberProperty codeMemberProperty = (from p in context.GeneratedClass.Members.OfType<CodeMemberProperty>()
			where "ApplicationInstance".Equals(p.Name)
			select p).SingleOrDefault<CodeMemberProperty>();
			if (codeMemberProperty != null)
			{
				codeMemberProperty.Attributes |= MemberAttributes.Static;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003593 File Offset: 0x00001793
		protected override string GetClassName(string virtualPath)
		{
			return ParserHelpers.SanitizeClassName(Path.GetFileNameWithoutExtension(virtualPath));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000035A0 File Offset: 0x000017A0
		private static string DetermineNamespace(string virtualPath)
		{
			virtualPath = virtualPath.Replace(Path.DirectorySeparatorChar, '/');
			virtualPath = WebCodeRazorHost.GetDirectory(virtualPath);
			int num = virtualPath.IndexOf("App_Code", StringComparison.OrdinalIgnoreCase);
			if (num != -1)
			{
				virtualPath = virtualPath.Substring(num + "App_Code".Length);
			}
			IEnumerable<string> enumerable = virtualPath.Split(new char[]
			{
				'/'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (!enumerable.Any<string>())
			{
				return "ASP";
			}
			return "ASP." + string.Join(".", enumerable);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003624 File Offset: 0x00001824
		private static string GetDirectory(string virtualPath)
		{
			int num = virtualPath.LastIndexOf('/');
			if (num != -1)
			{
				return virtualPath.Substring(0, num);
			}
			return string.Empty;
		}

		// Token: 0x04000041 RID: 65
		private const string AppCodeDir = "App_Code";

		// Token: 0x04000042 RID: 66
		private const string HttpContextAccessorName = "Context";

		// Token: 0x04000043 RID: 67
		private static readonly string _helperPageBaseType = typeof(HelperPage).FullName;
	}
}
