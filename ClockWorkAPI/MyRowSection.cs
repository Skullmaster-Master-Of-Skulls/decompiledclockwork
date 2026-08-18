using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using BinaryComponents.SuperList;
using BinaryComponents.SuperList.Sections;

namespace ClockWorkAPI
{
	// Token: 0x020000A7 RID: 167
	public class MyRowSection : RowSection
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x00031C8C File Offset: 0x00030C8C
		public MyRowSection(ListControl listControl, RowIdentifier rowIdentifier, HeaderSection headerSection, int position) : base(listControl, rowIdentifier, headerSection, position)
		{
			this._position = position;
			this.highlightColIndices = null;
			this.dataSource = null;
			this.yeses = new string[]
			{
				"y",
				"yes",
				"1",
				"t",
				"true"
			};
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00031D34 File Offset: 0x00030D34
		public override void PaintBackground(Section.GraphicsSettings gs, Rectangle clipRect)
		{
			if (base.Item is ReportListItem)
			{
				ReportListItem rli = (ReportListItem)base.Item;
				int num = base.IsSelected ? -1 : this.ShouldRowBeHighlighted(rli);
				if (num >= 0)
				{
					if (num >= this.colours.Length)
					{
						num = 0;
					}
					using (SolidBrush solidBrush = new SolidBrush(this.colours[num]))
					{
						gs.Graphics.FillRectangle(solidBrush, base.Rectangle);
					}
					return;
				}
			}
			base.PaintBackground(gs, clipRect);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00031DEC File Offset: 0x00030DEC
		private int ShouldRowBeHighlighted(ReportListItem rli)
		{
			if (this.highlightColIndices == null || this.dataSource != rli.Dr.Table)
			{
				this.dataSource = rli.Dr.Table;
				this.highlightColIndices = new List<int>();
				for (int i = 0; i < this.dataSource.Columns.Count; i++)
				{
					string text = this.dataSource.Columns[i].ColumnName.ToLower();
					if (text.IndexOf(ReportListItem.colNameSubstringIndicator) >= 0)
					{
						this.highlightColIndices.Add(i);
					}
				}
			}
			if (this.highlightColIndices != null && this.highlightColIndices.Count > 0)
			{
				DataRow dr = rli.Dr;
				for (int i = 0; i < this.highlightColIndices.Count; i++)
				{
					int columnIndex = this.highlightColIndices[i];
					if (dr[columnIndex] != DBNull.Value)
					{
						bool flag;
						if (dr[columnIndex] is bool)
						{
							flag = Convert.ToBoolean(dr[columnIndex]);
						}
						else
						{
							flag = (Array.IndexOf<string>(this.yeses, dr[columnIndex].ToString().ToLower()) >= 0);
						}
						if (flag)
						{
							return i;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x04000427 RID: 1063
		private string[] yeses;

		// Token: 0x04000428 RID: 1064
		private Color[] colours = new Color[]
		{
			Color.LightGreen,
			Color.MistyRose,
			Color.LightBlue
		};

		// Token: 0x04000429 RID: 1065
		private readonly int _position;

		// Token: 0x0400042A RID: 1066
		private List<int> highlightColIndices;

		// Token: 0x0400042B RID: 1067
		private DataTable dataSource;
	}
}
