using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace TechnoPro.Common.UI.Web.ClockWork.Controls
{
	// Token: 0x02000010 RID: 16
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlWebGrid runat=server></{0}:CtrlWebGrid>")]
	public class CtrlWebGrid : WebControl, INamingContainer
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00004650 File Offset: 0x00002850
		public override void Dispose()
		{
			bool flag = this.grid != null;
			if (flag)
			{
				this.grid.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060000DB RID: 219 RVA: 0x00004680 File Offset: 0x00002880
		// (remove) Token: 0x060000DC RID: 220 RVA: 0x000046B8 File Offset: 0x000028B8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event GridItemEventHandler OnItemCreated;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060000DD RID: 221 RVA: 0x000046F0 File Offset: 0x000028F0
		// (remove) Token: 0x060000DE RID: 222 RVA: 0x00004728 File Offset: 0x00002928
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event GridNeedDataSourceEventHandler OnNeedDataSource;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060000DF RID: 223 RVA: 0x00004760 File Offset: 0x00002960
		// (remove) Token: 0x060000E0 RID: 224 RVA: 0x00004798 File Offset: 0x00002998
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event GridCommandEventHandler OnItemCommand;

		// Token: 0x17000042 RID: 66
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000047CD File Offset: 0x000029CD
		public object DataSource
		{
			set
			{
				this.grid.DataSource = value;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000047DD File Offset: 0x000029DD
		public void AddBoundColumn(string UniqueName, string HeaderText, string SortExpression, string DataField)
		{
			this.AddBoundColumn(UniqueName, HeaderText, SortExpression, DataField, HorizontalAlign.Left);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000047F0 File Offset: 0x000029F0
		public void AddBoundColumn(string UniqueName, string HeaderText, string SortExpression, string DataField, HorizontalAlign halign)
		{
			GridBoundColumn gridBoundColumn = new GridBoundColumn
			{
				UniqueName = UniqueName,
				HeaderText = HeaderText,
				SortExpression = SortExpression,
				DataField = DataField
			};
			gridBoundColumn.HeaderStyle.HorizontalAlign = halign;
			gridBoundColumn.ItemStyle.HorizontalAlign = halign;
			this.grid.Columns.Add(gridBoundColumn);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004854 File Offset: 0x00002A54
		private void FireOnItemCreated(GridItemEventArgs e)
		{
			bool flag = this.OnItemCreated != null;
			if (flag)
			{
				this.OnItemCreated(this, e);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004880 File Offset: 0x00002A80
		private void FireOnNeedDataSource(GridNeedDataSourceEventArgs e)
		{
			bool flag = this.OnNeedDataSource != null;
			if (flag)
			{
				this.OnNeedDataSource(this, e);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000048AC File Offset: 0x00002AAC
		private void FireOnItemCommand(GridCommandEventArgs e)
		{
			bool flag = this.OnItemCommand != null;
			if (flag)
			{
				this.OnItemCommand(this, e);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002619 File Offset: 0x00000819
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag("div");
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000048D5 File Offset: 0x00002AD5
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000048E6 File Offset: 0x00002AE6
		protected override void RenderContents(HtmlTextWriter output)
		{
			this.grid.RenderControl(output);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000048F6 File Offset: 0x00002AF6
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004908 File Offset: 0x00002B08
		private void InitializeControls()
		{
			this.grid.ID = "grid_" + this.ID;
			this.grid.AllowPaging = true;
			this.grid.AllowSorting = true;
			this.grid.AutoGenerateColumns = false;
			this.grid.GridLines = GridLines.None;
			this.grid.Skin = "Office2007";
			this.grid.AlternatingItemStyle.BackColor = ColorTranslator.FromHtml("#EEEEEE");
			this.grid.ItemStyle.Height = new Unit(50.0, UnitType.Pixel);
			this.grid.MasterTableView.Font.Size = FontUnit.Medium;
			this.grid.MasterTableView.NoMasterRecordsText = "No items to display.";
			this.grid.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = new Unit(20.0, UnitType.Pixel);
			this.grid.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = new Unit(20.0, UnitType.Pixel);
			this.grid.ClientSettings.EnableRowHoverStyle = true;
			this.grid.FilterMenu.EnableTheming = true;
			this.grid.FilterMenu.CollapseAnimation.Type = AnimationType.OutQuint;
			this.grid.FilterMenu.CollapseAnimation.Duration = 200;
			this.grid.ItemCreated += this.grid_ItemCreated;
			this.grid.NeedDataSource += this.grid_NeedDataSource;
			this.grid.ItemCommand += this.grid_ItemCommand;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004AD6 File Offset: 0x00002CD6
		private void grid_ItemCommand(object sender, GridCommandEventArgs e)
		{
			this.FireOnItemCommand(e);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004AE1 File Offset: 0x00002CE1
		private void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			this.FireOnNeedDataSource(e);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004AEC File Offset: 0x00002CEC
		private void grid_ItemCreated(object sender, GridItemEventArgs e)
		{
			this.FireOnItemCreated(e);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004AF7 File Offset: 0x00002CF7
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.grid);
		}

		// Token: 0x04000059 RID: 89
		private RadGrid grid = new RadGrid();
	}
}
