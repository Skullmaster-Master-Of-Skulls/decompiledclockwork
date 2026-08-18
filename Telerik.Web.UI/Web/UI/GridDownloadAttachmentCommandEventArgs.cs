using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010E4 RID: 4324
	public class GridDownloadAttachmentCommandEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B0F1 RID: 45297 RVA: 0x002652FE File Offset: 0x002634FE
		public GridDownloadAttachmentCommandEventArgs(GridItem item, object commandSource, object argument, GridAttachmentColumn column) : base(item, commandSource, "DownloadAttachment", argument)
		{
			this._column = column;
		}

		// Token: 0x1700394C RID: 14668
		// (get) Token: 0x0600B0F2 RID: 45298 RVA: 0x00265316 File Offset: 0x00263516
		public GridAttachmentColumn AttachmentColumn
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x1700394D RID: 14669
		// (get) Token: 0x0600B0F3 RID: 45299 RVA: 0x0026531E File Offset: 0x0026351E
		// (set) Token: 0x0600B0F4 RID: 45300 RVA: 0x0026533A File Offset: 0x0026353A
		public string FileName
		{
			get
			{
				return (string)((IDictionary)base.CommandArgument)["FileName"];
			}
			set
			{
				((IDictionary)base.CommandArgument)["FileName"] = value;
			}
		}

		// Token: 0x1700394E RID: 14670
		// (get) Token: 0x0600B0F5 RID: 45301 RVA: 0x00265352 File Offset: 0x00263552
		public IDictionary AttachmentKeyValues
		{
			get
			{
				return (IDictionary)base.CommandArgument;
			}
		}

		// Token: 0x0600B0F6 RID: 45302 RVA: 0x0026535F File Offset: 0x0026355F
		public override void ExecuteCommand(object source)
		{
			this.AttachmentColumn.StreamDownloadAttachment((Control)base.CommandSource, this.FileName, this.AttachmentKeyValues);
		}

		// Token: 0x04002E7C RID: 11900
		private GridAttachmentColumn _column;
	}
}
