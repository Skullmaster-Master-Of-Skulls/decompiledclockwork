using System;
using System.Security.Permissions;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020002FC RID: 764
	public class WebServiceParser : SimpleWebHandlerParser
	{
		// Token: 0x06002351 RID: 9041 RVA: 0x0007318C File Offset: 0x0007138C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static Type GetCompiledType(string inputFile, HttpContext context)
		{
			BuildResultCompiledType buildResultCompiledType = (BuildResultCompiledType)BuildManager.GetVPathBuildResult(context, VirtualPath.Create(inputFile));
			return buildResultCompiledType.ResultType;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x00073165 File Offset: 0x00071365
		internal WebServiceParser(string virtualPath) : base(null, virtualPath, null)
		{
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x000731B1 File Offset: 0x000713B1
		protected override string DefaultDirectiveName
		{
			get
			{
				return "webservice";
			}
		}
	}
}
