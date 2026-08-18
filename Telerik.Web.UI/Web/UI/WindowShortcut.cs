using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B84 RID: 7044
	public class WindowShortcut : StateManager
	{
		// Token: 0x06011123 RID: 69923 RVA: 0x003C3A79 File Offset: 0x003C1C79
		public WindowShortcut()
		{
		}

		// Token: 0x06011124 RID: 69924 RVA: 0x003C3A81 File Offset: 0x003C1C81
		public WindowShortcut(string _commandName, string _shortcut)
		{
			this.CommandName = _commandName;
			this.Shortcut = _shortcut;
		}

		// Token: 0x06011125 RID: 69925 RVA: 0x003C3A98 File Offset: 0x003C1C98
		private string get_name(string commandName)
		{
			commandName = commandName.ToLower();
			string key;
			switch (key = commandName)
			{
			case "togglepin":
				commandName = "togglePin";
				break;
			case "closeall":
				commandName = "closeAll";
				break;
			case "minimizeall":
				commandName = "minimizeAll";
				break;
			case "restoreall":
				commandName = "restoreAll";
				break;
			case "maximizeall":
				commandName = "maximizeAll";
				break;
			case "minimizeactivewindow":
				commandName = "minimizeActiveWindow";
				break;
			case "restoreactivewindow":
				commandName = "restoreActiveWindow";
				break;
			case "closeactivewindow":
				commandName = "closeActiveWindow";
				break;
			}
			return commandName;
		}

		// Token: 0x06011126 RID: 69926 RVA: 0x003C3BAB File Offset: 0x003C1DAB
		public override string ToString()
		{
			return string.Format("['{0}','{1}']", this.get_name(this.CommandName), this.Shortcut);
		}

		// Token: 0x1700536C RID: 21356
		// (get) Token: 0x06011127 RID: 69927 RVA: 0x003C3BC9 File Offset: 0x003C1DC9
		// (set) Token: 0x06011128 RID: 69928 RVA: 0x003C3BE9 File Offset: 0x003C1DE9
		public string CommandName
		{
			get
			{
				return ((string)base.ViewState["CommandName"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["CommandName"] = value;
			}
		}

		// Token: 0x1700536D RID: 21357
		// (get) Token: 0x06011129 RID: 69929 RVA: 0x003C3BFC File Offset: 0x003C1DFC
		// (set) Token: 0x0601112A RID: 69930 RVA: 0x003C3C1C File Offset: 0x003C1E1C
		public string Shortcut
		{
			get
			{
				return ((string)base.ViewState["Shortcut"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Shortcut"] = value;
			}
		}
	}
}
