using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001786 RID: 6022
	public class FiguresCollection
	{
		// Token: 0x17004722 RID: 18210
		// (get) Token: 0x0600EAD7 RID: 60119 RVA: 0x003583A6 File Offset: 0x003565A6
		public List<string> Figures
		{
			get
			{
				return this.figures;
			}
		}

		// Token: 0x0600EAD8 RID: 60120 RVA: 0x003583AE File Offset: 0x003565AE
		public FiguresCollection()
		{
			this.figures.AddRange(DefaultFigures.FiguresList);
		}

		// Token: 0x0600EAD9 RID: 60121 RVA: 0x003583D4 File Offset: 0x003565D4
		public FiguresCollection(Chart chart) : this()
		{
			foreach (CustomFigure customFigure in chart.CustomFigures)
			{
				this.figures.Add(customFigure.Name);
			}
		}

		// Token: 0x0600EADA RID: 60122 RVA: 0x00358434 File Offset: 0x00356634
		public void Add(List<CustomFigure> list)
		{
			foreach (CustomFigure customFigure in list)
			{
				this.figures.Add(customFigure.Name);
			}
		}

		// Token: 0x0600EADB RID: 60123 RVA: 0x0035848C File Offset: 0x0035668C
		public void Add(string name)
		{
			this.figures.Add(name);
		}

		// Token: 0x0600EADC RID: 60124 RVA: 0x0035849A File Offset: 0x0035669A
		internal static GraphicsPath GetPath(string name)
		{
			return DefaultFigures.GetPath(name);
		}

		// Token: 0x0600EADD RID: 60125 RVA: 0x003584A4 File Offset: 0x003566A4
		internal static GraphicsPath GetPath(string name, Chart chart)
		{
			if (chart.CustomFigures.Contains(name))
			{
				CustomFigure figure = chart.CustomFigures.GetFigure(name);
				return figure.GetPath();
			}
			return null;
		}

		// Token: 0x040043E4 RID: 17380
		private List<string> figures = new List<string>();
	}
}
