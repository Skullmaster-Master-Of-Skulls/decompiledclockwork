using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000018 RID: 24
	internal class DataModelValidator
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000C1 RID: 193 RVA: 0x00004E5C File Offset: 0x0000305C
		// (remove) Token: 0x060000C2 RID: 194 RVA: 0x00004E94 File Offset: 0x00003094
		public event EventHandler<DataModelErrorEventArgs> OnError;

		// Token: 0x060000C3 RID: 195 RVA: 0x00004ECC File Offset: 0x000030CC
		public void Validate(EdmModel model, bool validateSyntax)
		{
			EdmModelValidationContext edmModelValidationContext = new EdmModelValidationContext(model, validateSyntax);
			edmModelValidationContext.OnError += this.OnError;
			EdmModelValidationVisitor edmModelValidationVisitor = new EdmModelValidationVisitor(edmModelValidationContext, EdmModelRuleSet.CreateEdmModelRuleSet(model.SchemaVersion, validateSyntax));
			edmModelValidationVisitor.Visit(model);
		}
	}
}
