using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000261 RID: 609
	public class Pannable : StateManager, IDefaultCheck
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0004AD2A File Offset: 0x00048F2A
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x0004AD4B File Offset: 0x00048F4B
		[DefaultValue(ModifierKey.Ctrl)]
		public ModifierKey Key
		{
			get
			{
				return (ModifierKey)(base.ViewState["Key"] ?? ModifierKey.Ctrl);
			}
			set
			{
				base.ViewState["Key"] = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x0004AD63 File Offset: 0x00048F63
		public bool IsDefault
		{
			get
			{
				return this.Key == ModifierKey.Ctrl;
			}
		}
	}
}
