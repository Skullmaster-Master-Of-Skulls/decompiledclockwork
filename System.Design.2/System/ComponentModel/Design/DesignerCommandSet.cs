using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020001AE RID: 430
	public class DesignerCommandSet
	{
		// Token: 0x06000FC5 RID: 4037 RVA: 0x00003598 File Offset: 0x00001798
		public virtual ICollection GetCommands(string name)
		{
			return null;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00059D68 File Offset: 0x00057F68
		public DesignerVerbCollection Verbs
		{
			get
			{
				return (DesignerVerbCollection)this.GetCommands("Verbs");
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00059D7A File Offset: 0x00057F7A
		public DesignerActionListCollection ActionLists
		{
			get
			{
				return (DesignerActionListCollection)this.GetCommands("ActionLists");
			}
		}
	}
}
