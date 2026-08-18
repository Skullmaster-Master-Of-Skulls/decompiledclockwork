using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200002A RID: 42
	public abstract class BuildOperation
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x0000345E File Offset: 0x0000165E
		protected BuildOperation(Type typeBeingConstructed)
		{
			this.TypeBeingConstructed = typeBeingConstructed;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000346D File Offset: 0x0000166D
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003475 File Offset: 0x00001675
		public Type TypeBeingConstructed { get; private set; }
	}
}
