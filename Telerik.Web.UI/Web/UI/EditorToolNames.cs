using System;
using System.Collections.Generic;
using System.Reflection;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x020012A6 RID: 4774
	internal class EditorToolNames
	{
		// Token: 0x0600C7FC RID: 51196 RVA: 0x002C8EFD File Offset: 0x002C70FD
		public static string GetCommandName(string name)
		{
			if (EditorToolNames.CommandNames.ContainsKey(name))
			{
				return EditorToolNames.CommandNames[name];
			}
			return name;
		}

		// Token: 0x1700409C RID: 16540
		// (get) Token: 0x0600C7FD RID: 51197 RVA: 0x002C8F1C File Offset: 0x002C711C
		private static Dictionary<string, string> CommandNames
		{
			get
			{
				if (EditorToolNames._commandNames == null)
				{
					EditorToolNames._commandNames = new Dictionary<string, string>(new EditorToolNames.IgnoreCaseKeyComparer());
					PropertyInfo[] properties = typeof(ToolsStrings).GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						EditorToolNames._commandNames[propertyInfo.Name.ToLowerInvariant()] = propertyInfo.Name;
					}
				}
				return EditorToolNames._commandNames;
			}
		}

		// Token: 0x040034A6 RID: 13478
		private static Dictionary<string, string> _commandNames;

		// Token: 0x020012A7 RID: 4775
		private class IgnoreCaseKeyComparer : IEqualityComparer<string>
		{
			// Token: 0x0600C7FF RID: 51199 RVA: 0x002C8F8B File Offset: 0x002C718B
			bool IEqualityComparer<string>.Equals(string x, string y)
			{
				return x.Equals(y, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x0600C800 RID: 51200 RVA: 0x002C8F95 File Offset: 0x002C7195
			int IEqualityComparer<string>.GetHashCode(string obj)
			{
				return obj.ToLowerInvariant().GetHashCode();
			}
		}
	}
}
