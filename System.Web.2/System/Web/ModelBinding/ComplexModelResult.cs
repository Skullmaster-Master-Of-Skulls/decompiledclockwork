using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000638 RID: 1592
	public sealed class ComplexModelResult
	{
		// Token: 0x06004F02 RID: 20226 RVA: 0x00112E0F File Offset: 0x0011100F
		public ComplexModelResult(object model, ModelValidationNode validationNode)
		{
			if (validationNode == null)
			{
				throw new ArgumentNullException("validationNode");
			}
			this.Model = model;
			this.ValidationNode = validationNode;
		}

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x06004F03 RID: 20227 RVA: 0x00112E33 File Offset: 0x00111033
		// (set) Token: 0x06004F04 RID: 20228 RVA: 0x00112E3B File Offset: 0x0011103B
		public object Model { get; private set; }

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x00112E44 File Offset: 0x00111044
		// (set) Token: 0x06004F06 RID: 20230 RVA: 0x00112E4C File Offset: 0x0011104C
		public ModelValidationNode ValidationNode { get; private set; }
	}
}
