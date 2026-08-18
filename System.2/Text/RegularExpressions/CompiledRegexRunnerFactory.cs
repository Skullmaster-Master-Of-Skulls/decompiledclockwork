using System;
using System.Reflection.Emit;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006AC RID: 1708
	internal sealed class CompiledRegexRunnerFactory : RegexRunnerFactory
	{
		// Token: 0x06003FE1 RID: 16353 RVA: 0x0010CC92 File Offset: 0x0010AE92
		internal CompiledRegexRunnerFactory(DynamicMethod go, DynamicMethod firstChar, DynamicMethod trackCount)
		{
			this.goMethod = go;
			this.findFirstCharMethod = firstChar;
			this.initTrackCountMethod = trackCount;
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x0010CCB0 File Offset: 0x0010AEB0
		protected internal override RegexRunner CreateInstance()
		{
			CompiledRegexRunner compiledRegexRunner = new CompiledRegexRunner();
			new ReflectionPermission(PermissionState.Unrestricted).Assert();
			compiledRegexRunner.SetDelegates((NoParamDelegate)this.goMethod.CreateDelegate(typeof(NoParamDelegate)), (FindFirstCharDelegate)this.findFirstCharMethod.CreateDelegate(typeof(FindFirstCharDelegate)), (NoParamDelegate)this.initTrackCountMethod.CreateDelegate(typeof(NoParamDelegate)));
			return compiledRegexRunner;
		}

		// Token: 0x04002E91 RID: 11921
		private DynamicMethod goMethod;

		// Token: 0x04002E92 RID: 11922
		private DynamicMethod findFirstCharMethod;

		// Token: 0x04002E93 RID: 11923
		private DynamicMethod initTrackCountMethod;
	}
}
