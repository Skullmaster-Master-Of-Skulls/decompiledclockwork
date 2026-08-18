using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020005F3 RID: 1523
	public interface IReferenceService
	{
		// Token: 0x06003846 RID: 14406
		IComponent GetComponent(object reference);

		// Token: 0x06003847 RID: 14407
		object GetReference(string name);

		// Token: 0x06003848 RID: 14408
		string GetName(object reference);

		// Token: 0x06003849 RID: 14409
		object[] GetReferences();

		// Token: 0x0600384A RID: 14410
		object[] GetReferences(Type baseType);
	}
}
