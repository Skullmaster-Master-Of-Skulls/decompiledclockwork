using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200067D RID: 1661
	public sealed class ModelValidatedEventArgs : EventArgs
	{
		// Token: 0x060050B1 RID: 20657 RVA: 0x001164E4 File Offset: 0x001146E4
		public ModelValidatedEventArgs(ModelBindingExecutionContext modelBindingExecutionContext, ModelValidationNode parentNode)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			this.ModelBindingExecutionContext = modelBindingExecutionContext;
			this.ParentNode = parentNode;
		}

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x060050B2 RID: 20658 RVA: 0x00116508 File Offset: 0x00114708
		// (set) Token: 0x060050B3 RID: 20659 RVA: 0x00116510 File Offset: 0x00114710
		public ModelBindingExecutionContext ModelBindingExecutionContext { get; private set; }

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x060050B4 RID: 20660 RVA: 0x00116519 File Offset: 0x00114719
		// (set) Token: 0x060050B5 RID: 20661 RVA: 0x00116521 File Offset: 0x00114721
		public ModelValidationNode ParentNode { get; private set; }
	}
}
