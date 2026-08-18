using System;

namespace System.Web.UI
{
	// Token: 0x0200030E RID: 782
	public sealed class CompiledTemplateBuilder : ITemplate
	{
		// Token: 0x06002411 RID: 9233 RVA: 0x00075C1C File Offset: 0x00073E1C
		public CompiledTemplateBuilder(BuildTemplateMethod buildTemplateMethod)
		{
			this._buildTemplateMethod = buildTemplateMethod;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00075C2B File Offset: 0x00073E2B
		public void InstantiateIn(Control container)
		{
			this._buildTemplateMethod(container);
		}

		// Token: 0x04001CE5 RID: 7397
		private BuildTemplateMethod _buildTemplateMethod;
	}
}
