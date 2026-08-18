using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005E5 RID: 1509
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Method)]
	[Serializable]
	public class CompilationRelaxationsAttribute : Attribute
	{
		// Token: 0x060037E4 RID: 14308 RVA: 0x000BBC4D File Offset: 0x000BAC4D
		public CompilationRelaxationsAttribute(int relaxations)
		{
			this.m_relaxations = relaxations;
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000BBC5C File Offset: 0x000BAC5C
		public CompilationRelaxationsAttribute(CompilationRelaxations relaxations)
		{
			this.m_relaxations = (int)relaxations;
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060037E6 RID: 14310 RVA: 0x000BBC6B File Offset: 0x000BAC6B
		public int CompilationRelaxations
		{
			get
			{
				return this.m_relaxations;
			}
		}

		// Token: 0x04001CEB RID: 7403
		private int m_relaxations;
	}
}
