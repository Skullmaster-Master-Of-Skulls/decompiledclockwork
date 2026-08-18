using System;
using System.ComponentModel;

namespace DynamicScreens
{
	// Token: 0x02000030 RID: 48
	public enum DropListBehaviour
	{
		// Token: 0x040001EC RID: 492
		[Description("Can only choose from the list")]
		Can_Only_Choose_From_the_List,
		// Token: 0x040001ED RID: 493
		[Description("Can enter items not in the list")]
		Can_Enter_Items_Not_in_the_List,
		// Token: 0x040001EE RID: 494
		[Description("Encrypted and can enter items not in the list")]
		Encrypted_and_Can_Enter_Items_Not_in_the_List = -1
	}
}
