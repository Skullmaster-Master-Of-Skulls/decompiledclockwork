using System;
using System.Collections.Specialized;

namespace System.Web.UI
{
	// Token: 0x0200024D RID: 589
	public sealed class CompiledBindableTemplateBuilder : IBindableTemplate, ITemplate
	{
		// Token: 0x06001B1B RID: 6939 RVA: 0x0005526B File Offset: 0x0005346B
		public CompiledBindableTemplateBuilder(BuildTemplateMethod buildTemplateMethod, ExtractTemplateValuesMethod extractTemplateValuesMethod)
		{
			this._buildTemplateMethod = buildTemplateMethod;
			this._extractTemplateValuesMethod = extractTemplateValuesMethod;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00055281 File Offset: 0x00053481
		public IOrderedDictionary ExtractValues(Control container)
		{
			if (this._extractTemplateValuesMethod != null)
			{
				return this._extractTemplateValuesMethod(container);
			}
			return new OrderedDictionary();
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0005529D File Offset: 0x0005349D
		public void InstantiateIn(Control container)
		{
			this._buildTemplateMethod(container);
		}

		// Token: 0x04001881 RID: 6273
		private BuildTemplateMethod _buildTemplateMethod;

		// Token: 0x04001882 RID: 6274
		private ExtractTemplateValuesMethod _extractTemplateValuesMethod;
	}
}
