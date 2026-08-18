using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000185 RID: 389
	public class KeyboardNavigationCustomShortcut : KeyboardNavigationShortcut
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x00031F7D File Offset: 0x0003017D
		public KeyboardNavigationCustomShortcut()
		{
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00031F85 File Offset: 0x00030185
		public KeyboardNavigationCustomShortcut(KeyboardNavigationKey key) : base(key)
		{
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00031F8E File Offset: 0x0003018E
		public KeyboardNavigationCustomShortcut(KeyboardNavigationKey key, KeyboardNavigationModifier modifier) : base(key, modifier)
		{
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00031F98 File Offset: 0x00030198
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00031FB8 File Offset: 0x000301B8
		public string CustomCommand
		{
			get
			{
				return (base.ViewState["CustomCommand"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["CustomCommand"] = value;
			}
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00031FCB File Offset: 0x000301CB
		internal override string GetName()
		{
			return this.CustomCommand;
		}
	}
}
