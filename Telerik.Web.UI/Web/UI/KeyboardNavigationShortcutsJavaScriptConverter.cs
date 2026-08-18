using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000188 RID: 392
	internal class KeyboardNavigationShortcutsJavaScriptConverter<T> : JavaScriptConverter where T : KeyboardNavigationShortcut, IMarkableStateManager
	{
		// Token: 0x06000D89 RID: 3465 RVA: 0x00031FD3 File Offset: 0x000301D3
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00031FDC File Offset: 0x000301DC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object>();
			KeyboardNavigationShortcuts<T> keyboardNavigationShortcuts = obj as KeyboardNavigationShortcuts<T>;
			new List<T>();
			foreach (object obj2 in keyboardNavigationShortcuts)
			{
				KeyboardNavigationShortcut keyboardNavigationShortcut = (KeyboardNavigationShortcut)obj2;
				if (keyboardNavigationShortcut.Enabled)
				{
					string key = keyboardNavigationShortcut.GetName();
					KeyboardNavigationCustomShortcut keyboardNavigationCustomShortcut = keyboardNavigationShortcut as KeyboardNavigationCustomShortcut;
					if (keyboardNavigationCustomShortcut != null && !string.IsNullOrEmpty(keyboardNavigationCustomShortcut.CustomCommand))
					{
						key = keyboardNavigationCustomShortcut.CustomCommand;
					}
					if (sortedDictionary.ContainsKey(key))
					{
						if (sortedDictionary[key] is KeyboardNavigationShortcut)
						{
							sortedDictionary[key] = new List<KeyboardNavigationShortcut>
							{
								sortedDictionary[key] as KeyboardNavigationShortcut
							};
						}
						(sortedDictionary[key] as List<KeyboardNavigationShortcut>).Add(keyboardNavigationShortcut);
					}
					else
					{
						sortedDictionary.Add(key, keyboardNavigationShortcut);
					}
				}
			}
			return sortedDictionary;
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x000321A0 File Offset: 0x000303A0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(KeyboardNavigationShortcuts<T>);
				yield break;
			}
		}
	}
}
