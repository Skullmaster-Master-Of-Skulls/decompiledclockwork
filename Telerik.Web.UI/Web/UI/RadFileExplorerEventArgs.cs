using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001852 RID: 6226
	public class RadFileExplorerEventArgs : EventArgs
	{
		// Token: 0x0600F25E RID: 62046 RVA: 0x0037418F File Offset: 0x0037238F
		public RadFileExplorerEventArgs(string commandName, string virtualPath, string newVirtualPath)
		{
			this._path = virtualPath;
			this._newPath = newVirtualPath;
			this._command = commandName;
		}

		// Token: 0x17004929 RID: 18729
		// (get) Token: 0x0600F25F RID: 62047 RVA: 0x003741AC File Offset: 0x003723AC
		// (set) Token: 0x0600F260 RID: 62048 RVA: 0x003741B4 File Offset: 0x003723B4
		public string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x1700492A RID: 18730
		// (get) Token: 0x0600F261 RID: 62049 RVA: 0x003741BD File Offset: 0x003723BD
		// (set) Token: 0x0600F262 RID: 62050 RVA: 0x003741C5 File Offset: 0x003723C5
		public string NewPath
		{
			get
			{
				return this._newPath;
			}
			set
			{
				this._newPath = value;
			}
		}

		// Token: 0x1700492B RID: 18731
		// (get) Token: 0x0600F263 RID: 62051 RVA: 0x003741CE File Offset: 0x003723CE
		public string Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x1700492C RID: 18732
		// (get) Token: 0x0600F264 RID: 62052 RVA: 0x003741D6 File Offset: 0x003723D6
		// (set) Token: 0x0600F265 RID: 62053 RVA: 0x003741DE File Offset: 0x003723DE
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x040045C1 RID: 17857
		private string _path;

		// Token: 0x040045C2 RID: 17858
		private string _newPath;

		// Token: 0x040045C3 RID: 17859
		private readonly string _command;

		// Token: 0x040045C4 RID: 17860
		private bool _cancel;
	}
}
