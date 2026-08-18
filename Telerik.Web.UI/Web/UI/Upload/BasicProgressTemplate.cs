using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02000982 RID: 2434
	internal class BasicProgressTemplate : ITemplate
	{
		// Token: 0x06005C8B RID: 23691 RVA: 0x0011A739 File Offset: 0x00118939
		public BasicProgressTemplate(RadProgressArea progressArea)
		{
			this._progressArea = progressArea;
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x0011A748 File Offset: 0x00118948
		void ITemplate.InstantiateIn(Control container)
		{
			Control control = container;
			if (control.GetType().Name == "RadProgressArea")
			{
				control = ((RadProgressArea)control)._progressPanel;
			}
			this.CreateControls((ProgressPanel)control);
			this.LayoutControls((ProgressPanel)control);
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x0011A794 File Offset: 0x00118994
		private void CreateControls(ProgressPanel panel)
		{
			panel.TotalProgressBar = (panel.DisplayTotalProgressBar ? this.CreateProgressBarArea(RadProgressArea.PrimaryProgressBarElement, RadProgressArea.PrimaryProgressElement, 35) : null);
			panel.TotalProgress = (panel.DisplayTotalProgress ? this.CreateSpan(RadProgressArea.PrimaryValueName, "5.23 MB") : null);
			panel.TotalProgressPercent = (panel.DisplayTotalProgressPercent ? this.CreateSpan(RadProgressArea.PrimaryPercentName, "35") : null);
			panel.RequestSize = (panel.DisplayRequestSize ? this.CreateSpan(RadProgressArea.PrimaryTotalName, "14.94MB") : null);
			panel.FilesCountBar = (panel.DisplayFilesCountBar ? this.CreateProgressBarArea(RadProgressArea.SecondaryProgressBarElement, RadProgressArea.SecondaryProgressElement, 60) : null);
			panel.FilesCount = (panel.DisplayFilesCount ? this.CreateSpan(RadProgressArea.SecondaryValueName, "3") : null);
			panel.FilesCountPercent = (panel.DisplayFilesCountPercent ? this.CreateSpan(RadProgressArea.SecondaryPercentName, "60") : null);
			panel.SelectedFilesCount = (panel.DisplaySelectedFilesCount ? this.CreateSpan(RadProgressArea.SecondaryTotalName, "5") : null);
			panel.CurrentFileName = (panel.DisplayCurrentFileName ? this.CreateSpan(RadProgressArea.CurrentOperationName, "C:\\DummyFile.txt") : null);
			panel.TimeElapsed = (panel.DisplayTimeElapsed ? this.CreateSpan(RadProgressArea.TimeElapsedName, "00:00:05s") : null);
			panel.TimeEstimated = (panel.DisplayTimeEstimated ? this.CreateSpan(RadProgressArea.TimeEstimatedName, "00:00:14s") : null);
			panel.TransferSpeed = (panel.DisplayTransferSpeed ? this.CreateSpan(RadProgressArea.SpeedName, "1.09MB/s") : null);
			panel.CancelButton = (panel.DisplayCancelButton ? this.CreateButton(RadProgressArea.CancelButtonName, panel.Localization.Cancel) : null);
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x0011A958 File Offset: 0x00118B58
		internal virtual void LayoutControls(ProgressPanel panel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005C8F RID: 23695 RVA: 0x0011A95F File Offset: 0x00118B5F
		internal void LayoutHeader(HtmlGenericControl list, ProgressPanel panel)
		{
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x0011A961 File Offset: 0x00118B61
		internal virtual HtmlGenericControl CreateProgressBarArea(string outerDivId, string innerDivID, int designTimePercents)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x0011A968 File Offset: 0x00118B68
		internal void LayoutTotalProgressData(HtmlGenericControl list, ProgressPanel panel)
		{
			ArrayList arrayList = new ArrayList();
			if (panel.TotalProgressBar != null)
			{
				arrayList.Add(panel.TotalProgressBar);
			}
			bool flag = panel.TotalProgressPercent != null;
			if (flag)
			{
				arrayList.Add(new LiteralControl(panel.Localization.Uploaded));
				arrayList.Add(new LiteralControl(" "));
				arrayList.Add(panel.TotalProgressPercent);
				arrayList.Add(new LiteralControl("% "));
			}
			if (panel.TotalProgress != null)
			{
				if (flag)
				{
					arrayList.Add(new LiteralControl("("));
				}
				arrayList.Add(panel.TotalProgress);
				arrayList.Add(new LiteralControl(" "));
				if (flag)
				{
					arrayList.Add(new LiteralControl(")"));
				}
				arrayList.Add(new LiteralControl(" "));
			}
			if (panel.RequestSize != null)
			{
				arrayList.Add(new LiteralControl(panel.Localization.Total));
				arrayList.Add(new LiteralControl(" "));
				arrayList.Add(panel.RequestSize);
			}
			this.AddListItemControl(list, "ruFilePortion", (Control[])arrayList.ToArray(typeof(Control)));
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x0011AAA4 File Offset: 0x00118CA4
		internal void LayoutFileCountData(HtmlGenericControl list, ProgressPanel panel)
		{
			ArrayList arrayList = new ArrayList();
			if (panel.FilesCountBar != null)
			{
				arrayList.Add(panel.FilesCountBar);
			}
			bool flag = panel.FilesCountPercent != null;
			if (flag)
			{
				arrayList.Add(new LiteralControl(panel.Localization.UploadedFiles));
				arrayList.Add(new LiteralControl(" "));
				arrayList.Add(panel.FilesCountPercent);
				arrayList.Add(new LiteralControl("% "));
			}
			if (panel.FilesCount != null)
			{
				if (flag)
				{
					arrayList.Add(new LiteralControl("("));
				}
				arrayList.Add(panel.FilesCount);
				if (flag)
				{
					arrayList.Add(new LiteralControl(")"));
				}
				arrayList.Add(new LiteralControl(" "));
			}
			if (panel.SelectedFilesCount != null)
			{
				arrayList.Add(new LiteralControl(panel.Localization.TotalFiles));
				arrayList.Add(new LiteralControl(" "));
				arrayList.Add(panel.SelectedFilesCount);
			}
			this.AddListItemControl(list, "ruFileCount", (Control[])arrayList.ToArray(typeof(Control)));
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x0011ABD0 File Offset: 0x00118DD0
		internal void LayoutTimeData(HtmlGenericControl list, ProgressPanel panel)
		{
			ArrayList arrayList = new ArrayList();
			if (panel.TimeElapsed != null)
			{
				arrayList.Add(new LiteralControl(panel.Localization.ElapsedTime));
				arrayList.Add(new LiteralControl(" "));
				arrayList.Add(panel.TimeElapsed);
				arrayList.Add(new LiteralControl("&nbsp;"));
			}
			if (panel.TimeEstimated != null)
			{
				arrayList.Add(new LiteralControl(panel.Localization.EstimatedTime));
				arrayList.Add(new LiteralControl(" "));
				arrayList.Add(panel.TimeEstimated);
				arrayList.Add(new LiteralControl("&nbsp;"));
			}
			if (panel.TransferSpeed != null)
			{
				arrayList.Add(new LiteralControl(panel.Localization.TransferSpeed));
				arrayList.Add(new LiteralControl(" "));
				arrayList.Add(panel.TransferSpeed);
			}
			this.AddListItemControl(list, "ruTimeSpeed", (Control[])arrayList.ToArray(typeof(Control)));
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x0011ACDD File Offset: 0x00118EDD
		internal virtual HtmlInputButton CreateButton(string id, string text)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x0011ACE4 File Offset: 0x00118EE4
		internal virtual void AddListItemControl(HtmlGenericControl list, string className, params Control[] controls)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x0011ACEC File Offset: 0x00118EEC
		internal HtmlGenericControl CreateSpan(string id, string designTimeText)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			htmlGenericControl.ID = id;
			if (this._progressArea.isInDesignMode)
			{
				htmlGenericControl.InnerHtml = designTimeText;
			}
			return htmlGenericControl;
		}

		// Token: 0x04001646 RID: 5702
		internal RadProgressArea _progressArea;
	}
}
