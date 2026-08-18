using System;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200060E RID: 1550
	public interface INameCreationService
	{
		// Token: 0x060038D2 RID: 14546
		string CreateName(IContainer container, Type dataType);

		// Token: 0x060038D3 RID: 14547
		bool IsValidName(string name);

		// Token: 0x060038D4 RID: 14548
		void ValidateName(string name);
	}
}
