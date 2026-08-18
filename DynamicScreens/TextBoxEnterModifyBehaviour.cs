using System;
using System.ComponentModel;

namespace DynamicScreens
{
	// Token: 0x02000031 RID: 49
	public enum TextBoxEnterModifyBehaviour
	{
		// Token: 0x040001F0 RID: 496
		[Description("All users can enter text and modify text")]
		All_Users_Can_Enter_and_Modify_Text,
		// Token: 0x040001F1 RID: 497
		[Description("All users can enter text, but once text has been added and saved it can only be appended to")]
		All_Users_Can_Enter_But_Only_Appends_Afterwards,
		// Token: 0x040001F2 RID: 498
		[Description("All users can enter text, but once text has been added and saved it can only be appended to; over-riding is allowed but a copy of the original text will be kept")]
		All_Users_Can_Enter_But_Only_Appends_Afterwards_Overridable
	}
}
