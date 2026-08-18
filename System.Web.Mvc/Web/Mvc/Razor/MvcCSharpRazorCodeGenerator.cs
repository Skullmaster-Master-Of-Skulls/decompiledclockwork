using System;
using System.CodeDom;
using System.Web.Razor;
using System.Web.Razor.Generator;

namespace System.Web.Mvc.Razor
{
	// Token: 0x02000095 RID: 149
	internal class MvcCSharpRazorCodeGenerator : CSharpRazorCodeGenerator
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		public MvcCSharpRazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host) : base(className, rootNamespaceName, sourceFileName, host)
		{
			MvcWebPageRazorHost mvcWebPageRazorHost = host as MvcWebPageRazorHost;
			if (mvcWebPageRazorHost != null && !mvcWebPageRazorHost.IsSpecialPage)
			{
				this.SetBaseType("dynamic");
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		private void SetBaseType(string modelTypeName)
		{
			CodeTypeReference value = new CodeTypeReference(base.Context.Host.DefaultBaseClass + "<" + modelTypeName + ">");
			base.Context.GeneratedClass.BaseTypes.Clear();
			base.Context.GeneratedClass.BaseTypes.Add(value);
		}

		// Token: 0x04000129 RID: 297
		private const string DefaultModelTypeName = "dynamic";
	}
}
