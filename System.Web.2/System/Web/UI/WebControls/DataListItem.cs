using System;
using System.Collections;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003CF RID: 975
	[ToolboxItem(false)]
	public class DataListItem : WebControl, IDataItemContainer, INamingContainer
	{
		// Token: 0x06002F1E RID: 12062 RVA: 0x0009A15C File Offset: 0x0009835C
		public DataListItem(int itemIndex, ListItemType itemType)
		{
			this.itemIndex = itemIndex;
			this.itemType = itemType;
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x0009A172 File Offset: 0x00098372
		// (set) Token: 0x06002F20 RID: 12064 RVA: 0x0009A17A File Offset: 0x0009837A
		public virtual object DataItem
		{
			get
			{
				return this.dataItem;
			}
			set
			{
				this.dataItem = value;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x0009A183 File Offset: 0x00098383
		public virtual int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x0009A18B File Offset: 0x0009838B
		public virtual ListItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x0009A193 File Offset: 0x00098393
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle();
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x0009A19C File Offset: 0x0009839C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				DataListCommandEventArgs args = new DataListCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x0009A1CC File Offset: 0x000983CC
		public virtual void RenderItem(HtmlTextWriter writer, bool extractRows, bool tableLayout)
		{
			HttpContext context = this.Context;
			if (context != null && context.TraceIsEnabled)
			{
				int bufferedLength = context.Response.GetBufferedLength();
				this.RenderItemInternal(writer, extractRows, tableLayout);
				int bufferedLength2 = context.Response.GetBufferedLength();
				context.Trace.AddControlSize(this.UniqueID, bufferedLength2 - bufferedLength);
				return;
			}
			this.RenderItemInternal(writer, extractRows, tableLayout);
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x0009A22C File Offset: 0x0009842C
		private void RenderItemInternal(HtmlTextWriter writer, bool extractRows, bool tableLayout)
		{
			if (!extractRows)
			{
				if (tableLayout)
				{
					this.RenderContents(writer);
					return;
				}
				this.RenderControl(writer);
				return;
			}
			else
			{
				IEnumerator enumerator = this.Controls.GetEnumerator();
				Table table = null;
				bool flag = false;
				while (enumerator.MoveNext())
				{
					flag = true;
					Control control = (Control)enumerator.Current;
					if (control is Table)
					{
						table = (Table)control;
						break;
					}
				}
				if (table != null)
				{
					foreach (object obj in table.Rows)
					{
						TableRow tableRow = (TableRow)obj;
						tableRow.RenderControl(writer);
					}
					return;
				}
				if (flag)
				{
					throw new HttpException(SR.GetString("DataList_TemplateTableNotFound", new object[]
					{
						this.Parent.ID,
						this.itemType.ToString()
					}));
				}
				return;
			}
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x0009A2F5 File Offset: 0x000984F5
		protected internal virtual void SetItemType(ListItemType itemType)
		{
			this.itemType = itemType;
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x0009A2FE File Offset: 0x000984FE
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.DataItem;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x0009A306 File Offset: 0x00098506
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x0009A306 File Offset: 0x00098506
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x04002026 RID: 8230
		private int itemIndex;

		// Token: 0x04002027 RID: 8231
		private ListItemType itemType;

		// Token: 0x04002028 RID: 8232
		private object dataItem;
	}
}
