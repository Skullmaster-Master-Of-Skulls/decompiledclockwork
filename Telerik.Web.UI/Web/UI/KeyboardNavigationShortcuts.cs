using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000189 RID: 393
	public class KeyboardNavigationShortcuts<T> : StronglyTypedStateManagedCollection<T> where T : KeyboardNavigationShortcut, IMarkableStateManager
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x000321C5 File Offset: 0x000303C5
		public KeyboardNavigationShortcuts()
		{
			this.defaultShortcuts = new Dictionary<string, List<KeyboardNavigationShortcut>>();
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x000321D8 File Offset: 0x000303D8
		public List<KeyboardNavigationShortcut> EnabledShortcuts
		{
			get
			{
				List<KeyboardNavigationShortcut> list = new List<KeyboardNavigationShortcut>();
				foreach (object obj in base.List)
				{
					KeyboardNavigationShortcut keyboardNavigationShortcut = (KeyboardNavigationShortcut)obj;
					if (keyboardNavigationShortcut.Enabled)
					{
						list.Add(keyboardNavigationShortcut);
					}
				}
				return list;
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00032240 File Offset: 0x00030440
		internal void AddDefaultShortcuts(params T[] shortcuts)
		{
			this.defaultShortcutsCount = shortcuts.Length;
			foreach (T item in shortcuts)
			{
				this.Add(item);
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00032278 File Offset: 0x00030478
		public override void Add(T item)
		{
			string key = item.GetName();
			KeyboardNavigationCustomShortcut keyboardNavigationCustomShortcut = item as KeyboardNavigationCustomShortcut;
			if (keyboardNavigationCustomShortcut != null && !string.IsNullOrEmpty(keyboardNavigationCustomShortcut.CustomCommand))
			{
				key = keyboardNavigationCustomShortcut.CustomCommand;
			}
			if (base.List.Count >= this.defaultShortcutsCount)
			{
				if (!this.defaultShortcuts.ContainsKey(key))
				{
					goto IL_D8;
				}
				using (List<KeyboardNavigationShortcut>.Enumerator enumerator = this.defaultShortcuts[key].GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyboardNavigationShortcut keyboardNavigationShortcut = enumerator.Current;
						keyboardNavigationShortcut.Enabled = false;
					}
					goto IL_D8;
				}
			}
			if (this.defaultShortcuts.ContainsKey(key))
			{
				this.defaultShortcuts[key].Add(item);
			}
			else
			{
				this.defaultShortcuts.Add(key, new List<KeyboardNavigationShortcut>
				{
					item
				});
			}
			IL_D8:
			base.Add(item);
		}

		// Token: 0x040003E8 RID: 1000
		private int defaultShortcutsCount;

		// Token: 0x040003E9 RID: 1001
		private Dictionary<string, List<KeyboardNavigationShortcut>> defaultShortcuts;
	}
}
