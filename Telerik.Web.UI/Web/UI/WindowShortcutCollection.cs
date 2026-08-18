using System;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02001B85 RID: 7045
	public class WindowShortcutCollection : StronglyTypedStateManagedCollection<WindowShortcut>
	{
		// Token: 0x0601112B RID: 69931 RVA: 0x003C3C30 File Offset: 0x003C1E30
		public void Add(string commandName, string shortcut)
		{
			WindowShortcut item = new WindowShortcut(commandName, shortcut);
			base.Add(item);
		}

		// Token: 0x0601112C RID: 69932 RVA: 0x003C3C4C File Offset: 0x003C1E4C
		protected override void SetDirtyObject(object o)
		{
			if (o is WindowShortcut)
			{
				((StateManager)o).SetDirty();
			}
		}

		// Token: 0x0601112D RID: 69933 RVA: 0x003C3C64 File Offset: 0x003C1E64
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			bool flag = false;
			for (int i = 0; i < base.Count; i++)
			{
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this[i].ToString());
				flag = true;
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0601112E RID: 69934 RVA: 0x003C3CCC File Offset: 0x003C1ECC
		internal bool commandShortcutExists(string commandName)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (this[i].CommandName.ToLower() == commandName.ToLower())
				{
					return true;
				}
			}
			return false;
		}
	}
}
