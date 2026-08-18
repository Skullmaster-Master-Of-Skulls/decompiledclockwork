using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000142 RID: 322
	public abstract class DebugInfoGenerator
	{
		// Token: 0x06000A65 RID: 2661 RVA: 0x00025BEB File Offset: 0x00023DEB
		public static DebugInfoGenerator CreatePdbGenerator()
		{
			return new SymbolDocumentGenerator();
		}

		// Token: 0x06000A66 RID: 2662
		public abstract void MarkSequencePoint(LambdaExpression method, int ilOffset, DebugInfoExpression sequencePoint);

		// Token: 0x06000A67 RID: 2663 RVA: 0x00025BF2 File Offset: 0x00023DF2
		internal virtual void MarkSequencePoint(LambdaExpression method, MethodBase methodBase, ILGenerator ilg, DebugInfoExpression sequencePoint)
		{
			this.MarkSequencePoint(method, ilg.ILOffset, sequencePoint);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00025C03 File Offset: 0x00023E03
		internal virtual void SetLocalName(LocalBuilder localBuilder, string name)
		{
		}
	}
}
