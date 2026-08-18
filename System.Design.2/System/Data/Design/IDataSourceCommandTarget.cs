using System;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x02000247 RID: 583
	internal interface IDataSourceCommandTarget
	{
		// Token: 0x060016A0 RID: 5792
		bool CanAddChildOfType(Type childType);

		// Token: 0x060016A1 RID: 5793
		void AddChild(object child, bool fixName);

		// Token: 0x060016A2 RID: 5794
		bool CanInsertChildOfType(Type childType, object refChild);

		// Token: 0x060016A3 RID: 5795
		void InsertChild(object child, object refChild);

		// Token: 0x060016A4 RID: 5796
		bool CanRemoveChildren(ICollection children);

		// Token: 0x060016A5 RID: 5797
		void RemoveChildren(ICollection children);

		// Token: 0x060016A6 RID: 5798
		int IndexOf(object child);

		// Token: 0x060016A7 RID: 5799
		object GetObject(int index, bool getSiblingIfOutOfRange);
	}
}
