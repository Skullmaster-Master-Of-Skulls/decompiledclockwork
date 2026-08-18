using System;
using System.ComponentModel;

namespace System.Web.ModelBinding
{
	// Token: 0x0200067E RID: 1662
	public sealed class ModelValidatingEventArgs : CancelEventArgs
	{
		// Token: 0x060050B6 RID: 20662 RVA: 0x0011652A File Offset: 0x0011472A
		public ModelValidatingEventArgs(ModelBindingExecutionContext modelBindingExecutionContext, ModelValidationNode parentNode)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			this.ModelBindingExecutionContext = modelBindingExecutionContext;
			this.ParentNode = parentNode;
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x060050B7 RID: 20663 RVA: 0x0011654E File Offset: 0x0011474E
		// (set) Token: 0x060050B8 RID: 20664 RVA: 0x00116556 File Offset: 0x00114756
		public ModelBindingExecutionContext ModelBindingExecutionContext { get; private set; }

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x060050B9 RID: 20665 RVA: 0x0011655F File Offset: 0x0011475F
		// (set) Token: 0x060050BA RID: 20666 RVA: 0x00116567 File Offset: 0x00114767
		public ModelValidationNode ParentNode { get; private set; }
	}
}
