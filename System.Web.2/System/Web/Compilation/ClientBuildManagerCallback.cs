using System;
using System.CodeDom.Compiler;
using System.Security.Permissions;

namespace System.Web.Compilation
{
	// Token: 0x0200082E RID: 2094
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class ClientBuildManagerCallback : MarshalByRefObject
	{
		// Token: 0x060063F6 RID: 25590 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ReportCompilerError(CompilerError error)
		{
		}

		// Token: 0x060063F7 RID: 25591 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ReportParseError(ParserError error)
		{
		}

		// Token: 0x060063F8 RID: 25592 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ReportProgress(string message)
		{
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
