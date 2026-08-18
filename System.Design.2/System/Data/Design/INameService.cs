using System;

namespace System.Data.Design
{
	// Token: 0x02000254 RID: 596
	internal interface INameService
	{
		// Token: 0x060016E5 RID: 5861
		string CreateUniqueName(INamedObjectCollection container, Type type);

		// Token: 0x060016E6 RID: 5862
		string CreateUniqueName(INamedObjectCollection container, string proposed);

		// Token: 0x060016E7 RID: 5863
		string CreateUniqueName(INamedObjectCollection container, string proposedNameRoot, int startSuffix);

		// Token: 0x060016E8 RID: 5864
		void ValidateName(string name);

		// Token: 0x060016E9 RID: 5865
		void ValidateUniqueName(INamedObjectCollection container, string name);

		// Token: 0x060016EA RID: 5866
		void ValidateUniqueName(INamedObjectCollection container, INamedObject namedObject, string proposedName);
	}
}
