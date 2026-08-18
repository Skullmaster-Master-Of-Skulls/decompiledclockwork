using System;

namespace Telerik.Charting
{
	// Token: 0x02001784 RID: 6020
	public class CustomFiguresCollection : ChartingStateManagedCollection<CustomFigure>
	{
		// Token: 0x0600EACA RID: 60106 RVA: 0x003579CC File Offset: 0x00355BCC
		public CustomFigure GetFigure(int index)
		{
			return base.List[index];
		}

		// Token: 0x0600EACB RID: 60107 RVA: 0x003579DC File Offset: 0x00355BDC
		public CustomFigure GetFigure(string name)
		{
			int num = this.IndexOf(name);
			if (num != -1)
			{
				return this.GetFigure(num);
			}
			throw new ChartException(string.Format("Error while getting custom figure. There is no figure with name '{0}'", name));
		}

		// Token: 0x0600EACC RID: 60108 RVA: 0x00357A10 File Offset: 0x00355C10
		public override void Add(CustomFigure figure)
		{
			if (string.IsNullOrEmpty(figure.Name) && string.IsNullOrEmpty(figure.Description))
			{
				return;
			}
			if (string.IsNullOrEmpty(figure.Name))
			{
				figure.Name = "Figure" + base.List.Count;
			}
			base.Add(figure);
		}

		// Token: 0x0600EACD RID: 60109 RVA: 0x00357A6C File Offset: 0x00355C6C
		public override void AddRange(CustomFigure[] figure)
		{
			foreach (CustomFigure customFigure in figure)
			{
				if (!string.IsNullOrEmpty(customFigure.Name) || !string.IsNullOrEmpty(customFigure.Description))
				{
					base.Add(customFigure);
				}
			}
		}

		// Token: 0x0600EACE RID: 60110 RVA: 0x00357AB0 File Offset: 0x00355CB0
		public bool Contains(string figureName)
		{
			bool result = false;
			foreach (CustomFigure customFigure in base.List)
			{
				if (string.Compare(figureName, customFigure.Name, true) == 0)
				{
					return true;
				}
			}
			return result;
		}

		// Token: 0x0600EACF RID: 60111 RVA: 0x00357B10 File Offset: 0x00355D10
		public int IndexOf(string figureName)
		{
			int num = 0;
			foreach (CustomFigure customFigure in base.List)
			{
				if (string.Compare(figureName, customFigure.Name, true) == 0)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x0600EAD0 RID: 60112 RVA: 0x00357B74 File Offset: 0x00355D74
		public void Remove(string figureName)
		{
			int num = 0;
			foreach (CustomFigure customFigure in base.List)
			{
				if (string.Compare(figureName, customFigure.Name, true) == 0)
				{
					base.List.RemoveAt(num);
					break;
				}
				num++;
			}
		}
	}
}
