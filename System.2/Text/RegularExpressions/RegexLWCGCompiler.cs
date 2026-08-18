using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000694 RID: 1684
	internal class RegexLWCGCompiler : RegexCompiler
	{
		// Token: 0x06003EB1 RID: 16049 RVA: 0x00104EA7 File Offset: 0x001030A7
		internal RegexLWCGCompiler()
		{
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x00104EB0 File Offset: 0x001030B0
		internal RegexRunnerFactory FactoryInstanceFromCode(RegexCode code, RegexOptions options)
		{
			this._code = code;
			this._codes = code._codes;
			this._strings = code._strings;
			this._fcPrefix = code._fcPrefix;
			this._bmPrefix = code._bmPrefix;
			this._anchors = code._anchors;
			this._trackcount = code._trackcount;
			this._options = options;
			string str = Interlocked.Increment(ref RegexLWCGCompiler._regexCount).ToString(CultureInfo.InvariantCulture);
			DynamicMethod go = this.DefineDynamicMethod("Go" + str, null, typeof(CompiledRegexRunner));
			base.GenerateGo();
			DynamicMethod firstChar = this.DefineDynamicMethod("FindFirstChar" + str, typeof(bool), typeof(CompiledRegexRunner));
			base.GenerateFindFirstChar();
			DynamicMethod trackCount = this.DefineDynamicMethod("InitTrackCount" + str, null, typeof(CompiledRegexRunner));
			base.GenerateInitTrackCount();
			return new CompiledRegexRunnerFactory(go, firstChar, trackCount);
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x00104FA8 File Offset: 0x001031A8
		internal DynamicMethod DefineDynamicMethod(string methname, Type returntype, Type hostType)
		{
			MethodAttributes attributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static;
			CallingConventions callingConvention = CallingConventions.Standard;
			DynamicMethod dynamicMethod = new DynamicMethod(methname, attributes, callingConvention, returntype, RegexLWCGCompiler._paramTypes, hostType, false);
			this._ilg = dynamicMethod.GetILGenerator();
			return dynamicMethod;
		}

		// Token: 0x04002DC5 RID: 11717
		private static int _regexCount = 0;

		// Token: 0x04002DC6 RID: 11718
		private static Type[] _paramTypes = new Type[]
		{
			typeof(RegexRunner)
		};
	}
}
