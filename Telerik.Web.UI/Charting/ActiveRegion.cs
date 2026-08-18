using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Web.UI.Design;

namespace Telerik.Charting
{
	// Token: 0x020016D8 RID: 5848
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ActiveRegion : StateManagedObject
	{
		// Token: 0x1700453B RID: 17723
		// (get) Token: 0x0600E1CC RID: 57804 RVA: 0x00323233 File Offset: 0x00321433
		// (set) Token: 0x0600E1CD RID: 57805 RVA: 0x0032323B File Offset: 0x0032143B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Parent
		{
			get
			{
				return this.activeRegionParent;
			}
			set
			{
				this.activeRegionParent = value;
			}
		}

		// Token: 0x1700453C RID: 17724
		// (get) Token: 0x0600E1CE RID: 57806 RVA: 0x00323244 File Offset: 0x00321444
		// (set) Token: 0x0600E1CF RID: 57807 RVA: 0x00323252 File Offset: 0x00321452
		[Browsable(false)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GraphicsPath Region
		{
			get
			{
				return this.activeRegionList[0];
			}
			set
			{
				this.activeRegionList[0] = value;
			}
		}

		// Token: 0x1700453D RID: 17725
		// (get) Token: 0x0600E1D0 RID: 57808 RVA: 0x00323261 File Offset: 0x00321461
		// (set) Token: 0x0600E1D1 RID: 57809 RVA: 0x00323281 File Offset: 0x00321481
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Url")]
		public string Url
		{
			get
			{
				return (string)(base.ViewState["Url"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Url"] = value;
			}
		}

		// Token: 0x1700453E RID: 17726
		// (get) Token: 0x0600E1D2 RID: 57810 RVA: 0x00323294 File Offset: 0x00321494
		// (set) Token: 0x0600E1D3 RID: 57811 RVA: 0x003232B4 File Offset: 0x003214B4
		[Description("Tooltip message")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string Tooltip
		{
			get
			{
				return (string)(base.ViewState["Tooltip"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Tooltip"] = value;
			}
		}

		// Token: 0x1700453F RID: 17727
		// (get) Token: 0x0600E1D4 RID: 57812 RVA: 0x003232C7 File Offset: 0x003214C7
		// (set) Token: 0x0600E1D5 RID: 57813 RVA: 0x003232E7 File Offset: 0x003214E7
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Attributes")]
		public string Attributes
		{
			get
			{
				return (string)(base.ViewState["Attributes"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Attributes"] = value;
			}
		}

		// Token: 0x0600E1D6 RID: 57814 RVA: 0x003232FA File Offset: 0x003214FA
		public ActiveRegion()
		{
			this.activeRegionList = new List<GraphicsPath>();
			this.activeRegionList.Add(null);
		}

		// Token: 0x0600E1D7 RID: 57815 RVA: 0x00323319 File Offset: 0x00321519
		public ActiveRegion(object parent) : this()
		{
			this.activeRegionParent = parent;
		}

		// Token: 0x140001BD RID: 445
		// (add) Token: 0x0600E1D8 RID: 57816 RVA: 0x00323328 File Offset: 0x00321528
		// (remove) Token: 0x0600E1D9 RID: 57817 RVA: 0x00323360 File Offset: 0x00321560
		public event RegionClickEventHandler Click;

		// Token: 0x0600E1DA RID: 57818 RVA: 0x00323398 File Offset: 0x00321598
		public bool CheckPoint(PointF point, bool onclick)
		{
			if (this.activeRegionList[0] != null)
			{
				bool flag = this.Region.IsVisible(point);
				if (flag && this.Click != null && onclick)
				{
					this.Click(this.Parent, new RegionClickEventArgs((IActiveRegion)this.Parent));
				}
				return flag;
			}
			return false;
		}

		// Token: 0x0600E1DB RID: 57819 RVA: 0x003233F2 File Offset: 0x003215F2
		public bool CheckPoint(PointF point)
		{
			return this.CheckPoint(point, false);
		}

		// Token: 0x0600E1DC RID: 57820 RVA: 0x003233FC File Offset: 0x003215FC
		public void GoToUrl()
		{
			if (!string.IsNullOrEmpty(this.Url))
			{
				Process process = new Process();
				try
				{
					process.StartInfo.FileName = this.Url;
					process.StartInfo.UseShellExecute = true;
					process.StartInfo.Verb = "open";
					process.StartInfo.CreateNoWindow = true;
					process.Start();
				}
				catch (Exception inner)
				{
					throw new ChartException("Unable to open Url", inner);
				}
			}
		}

		// Token: 0x0600E1DD RID: 57821 RVA: 0x0032347C File Offset: 0x0032167C
		public bool IsEmpty()
		{
			return string.IsNullOrEmpty(this.Attributes) && string.IsNullOrEmpty(this.Tooltip) && string.IsNullOrEmpty(this.Url);
		}

		// Token: 0x0600E1DE RID: 57822 RVA: 0x003234A8 File Offset: 0x003216A8
		internal static List<IActiveRegion> GetActiveRegions(PointF point, IContainer container)
		{
			Chart chart = container as Chart;
			List<IActiveRegion> list = new List<IActiveRegion>();
			if (container != null)
			{
				if (chart != null)
				{
					ActiveRegion activeRegion = new ActiveRegion();
					foreach (ChartSeries chartSeries in chart.Series)
					{
						foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
						{
							if (string.Compare(chartSeriesItem.ActiveRegion.Attributes, activeRegion.Attributes, true) == 0 && string.Compare(chartSeriesItem.ActiveRegion.Tooltip, activeRegion.Tooltip, true) == 0 && string.Compare(chartSeriesItem.ActiveRegion.Url, activeRegion.Url, true) == 0 && chartSeries.IsActiveRegionSet)
							{
								chartSeriesItem.ActiveRegion.Url = chartSeries.FormatValues(chartSeries.ActiveRegionUrl, chartSeriesItem);
								chartSeriesItem.ActiveRegion.Tooltip = chartSeries.FormatValues(chartSeries.ActiveRegionToolTip, chartSeriesItem);
								chartSeriesItem.ActiveRegion.Attributes = chartSeries.FormatValues(chartSeries.ActiveRegionAttributes, chartSeriesItem);
							}
							if (chartSeriesItem.ActiveRegion.CheckPoint(point))
							{
								list.Add(chartSeriesItem);
							}
						}
					}
				}
				foreach (IOrdering ordering in container.OrderList)
				{
					IContainer container2 = ordering as IContainer;
					if (container2 != null)
					{
						List<IActiveRegion> activeRegions = ActiveRegion.GetActiveRegions(point, container2);
						list.InsertRange(0, activeRegions);
					}
					ChartAxis chartAxis = ordering as ChartAxis;
					IActiveRegion activeRegion2;
					if (chartAxis != null)
					{
						foreach (ChartAxisItem chartAxisItem in chartAxis.Items)
						{
							activeRegion2 = chartAxisItem;
							if (activeRegion2 != null && activeRegion2.ActiveRegion.CheckPoint(point))
							{
								list.Add(activeRegion2);
							}
						}
						activeRegion2 = chartAxis.AxisLabel;
						if (activeRegion2 != null && activeRegion2.ActiveRegion.CheckPoint(point))
						{
							list.Add(activeRegion2);
						}
					}
					activeRegion2 = (ordering as IActiveRegion);
					if (activeRegion2 != null && activeRegion2.ActiveRegion.CheckPoint(point))
					{
						list.Add(activeRegion2);
					}
				}
			}
			return list;
		}

		// Token: 0x0600E1DF RID: 57823 RVA: 0x0032375C File Offset: 0x0032195C
		internal bool HasClickEvent()
		{
			return this.Click != null;
		}

		// Token: 0x0600E1E0 RID: 57824 RVA: 0x0032376A File Offset: 0x0032196A
		internal void OnClick()
		{
			this.OnClick(this.Parent);
		}

		// Token: 0x0600E1E1 RID: 57825 RVA: 0x00323778 File Offset: 0x00321978
		internal void OnClick(object sender)
		{
			if (this.Click != null)
			{
				this.Click(sender, new RegionClickEventArgs((IActiveRegion)this.Parent));
			}
		}

		// Token: 0x04004170 RID: 16752
		private object activeRegionParent;

		// Token: 0x04004171 RID: 16753
		internal List<GraphicsPath> activeRegionList;
	}
}
