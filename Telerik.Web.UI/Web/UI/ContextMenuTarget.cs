using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B35 RID: 6965
	public abstract class ContextMenuTarget : StateManager
	{
		// Token: 0x17005226 RID: 21030
		// (get) Token: 0x06010DA0 RID: 69024
		internal abstract ContextMenuTargetType Type { get; }

		// Token: 0x06010DA1 RID: 69025 RVA: 0x003BD6AC File Offset: 0x003BB8AC
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			if (obj == null)
			{
				obj = new object[]
				{
					obj
				};
			}
			return obj;
		}

		// Token: 0x06010DA2 RID: 69026 RVA: 0x003BD6D4 File Offset: 0x003BB8D4
		protected override void LoadViewState(object savedState)
		{
			if (!(savedState is object[]))
			{
				base.LoadViewState(savedState);
			}
		}

		// Token: 0x17005227 RID: 21031
		// (get) Token: 0x06010DA3 RID: 69027 RVA: 0x003BD6F2 File Offset: 0x003BB8F2
		// (set) Token: 0x06010DA4 RID: 69028 RVA: 0x003BD6FA File Offset: 0x003BB8FA
		internal RadContextMenu Owner { get; set; }
	}
}
