using System;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000184 RID: 388
	public abstract class KeyboardNavigationShortcut : StateManager
	{
		// Token: 0x06000D79 RID: 3449 RVA: 0x00031E86 File Offset: 0x00030086
		public KeyboardNavigationShortcut()
		{
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00031E8E File Offset: 0x0003008E
		public KeyboardNavigationShortcut(KeyboardNavigationKey key)
		{
			this.Key = key;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00031E9D File Offset: 0x0003009D
		public KeyboardNavigationShortcut(KeyboardNavigationKey key, KeyboardNavigationModifier modifier)
		{
			this.Key = key;
			this.Modifiers = modifier;
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00031EB4 File Offset: 0x000300B4
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x00031EDE File Offset: 0x000300DE
		public KeyboardNavigationKey Key
		{
			get
			{
				object obj = base.ViewState["Key"];
				if (obj != null)
				{
					return (KeyboardNavigationKey)obj;
				}
				return KeyboardNavigationKey.A;
			}
			set
			{
				base.ViewState["Key"] = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00031EF8 File Offset: 0x000300F8
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x00031F21 File Offset: 0x00030121
		public KeyboardNavigationModifier Modifiers
		{
			get
			{
				object obj = base.ViewState["Modifiers"];
				if (obj != null)
				{
					return (KeyboardNavigationModifier)obj;
				}
				return KeyboardNavigationModifier.None;
			}
			set
			{
				base.ViewState["Modifiers"] = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00031F3C File Offset: 0x0003013C
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x00031F65 File Offset: 0x00030165
		[ScriptIgnore]
		public bool Enabled
		{
			get
			{
				object obj = base.ViewState["Enabled"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x06000D82 RID: 3458
		internal abstract string GetName();
	}
}
