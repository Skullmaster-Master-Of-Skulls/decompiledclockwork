using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x02000205 RID: 517
	public interface IDesignerDataSchema
	{
		// Token: 0x0600135E RID: 4958
		ICollection GetSchemaItems(DesignerDataSchemaClass schemaClass);

		// Token: 0x0600135F RID: 4959
		bool SupportsSchemaClass(DesignerDataSchemaClass schemaClass);
	}
}
