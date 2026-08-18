using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B7E RID: 7038
	internal class ProgressPanel : Panel, INamingContainer
	{
		// Token: 0x060110C6 RID: 69830 RVA: 0x003C2F94 File Offset: 0x003C1194
		public ProgressPanel(ProgressIndicators progressIndicators, bool displayCancelButton, ProgressAreaStrings localization)
		{
			this._progressIndicators = progressIndicators;
			this._displayCancelButton = displayCancelButton;
			this._localization = localization;
		}

		// Token: 0x17005341 RID: 21313
		// (get) Token: 0x060110C7 RID: 69831 RVA: 0x003C2FB1 File Offset: 0x003C11B1
		public ProgressAreaStrings Localization
		{
			get
			{
				return this._localization;
			}
		}

		// Token: 0x17005342 RID: 21314
		// (get) Token: 0x060110C8 RID: 69832 RVA: 0x003C2FB9 File Offset: 0x003C11B9
		public bool DisplayTotalProgressBar
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.TotalProgressBar);
			}
		}

		// Token: 0x17005343 RID: 21315
		// (get) Token: 0x060110C9 RID: 69833 RVA: 0x003C2FC6 File Offset: 0x003C11C6
		public bool DisplayTotalProgress
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.TotalProgress);
			}
		}

		// Token: 0x17005344 RID: 21316
		// (get) Token: 0x060110CA RID: 69834 RVA: 0x003C2FD3 File Offset: 0x003C11D3
		public bool DisplayTotalProgressPercent
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.TotalProgressPercent);
			}
		}

		// Token: 0x17005345 RID: 21317
		// (get) Token: 0x060110CB RID: 69835 RVA: 0x003C2FE0 File Offset: 0x003C11E0
		public bool DisplayRequestSize
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.RequestSize);
			}
		}

		// Token: 0x17005346 RID: 21318
		// (get) Token: 0x060110CC RID: 69836 RVA: 0x003C2FED File Offset: 0x003C11ED
		public bool DisplayFilesCountBar
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.FilesCountBar);
			}
		}

		// Token: 0x17005347 RID: 21319
		// (get) Token: 0x060110CD RID: 69837 RVA: 0x003C2FFB File Offset: 0x003C11FB
		public bool DisplayFilesCount
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.FilesCount);
			}
		}

		// Token: 0x17005348 RID: 21320
		// (get) Token: 0x060110CE RID: 69838 RVA: 0x003C3009 File Offset: 0x003C1209
		public bool DisplayFilesCountPercent
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.FilesCountPercent);
			}
		}

		// Token: 0x17005349 RID: 21321
		// (get) Token: 0x060110CF RID: 69839 RVA: 0x003C3017 File Offset: 0x003C1217
		public bool DisplaySelectedFilesCount
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.SelectedFilesCount);
			}
		}

		// Token: 0x1700534A RID: 21322
		// (get) Token: 0x060110D0 RID: 69840 RVA: 0x003C3028 File Offset: 0x003C1228
		public bool DisplayCurrentFileName
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.CurrentFileName);
			}
		}

		// Token: 0x1700534B RID: 21323
		// (get) Token: 0x060110D1 RID: 69841 RVA: 0x003C3039 File Offset: 0x003C1239
		public bool DisplayTimeElapsed
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.TimeElapsed);
			}
		}

		// Token: 0x1700534C RID: 21324
		// (get) Token: 0x060110D2 RID: 69842 RVA: 0x003C304A File Offset: 0x003C124A
		public bool DisplayTimeEstimated
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.TimeEstimated);
			}
		}

		// Token: 0x1700534D RID: 21325
		// (get) Token: 0x060110D3 RID: 69843 RVA: 0x003C305B File Offset: 0x003C125B
		public bool DisplayTransferSpeed
		{
			get
			{
				return ProgressIndicators.None < (this._progressIndicators & ProgressIndicators.TransferSpeed);
			}
		}

		// Token: 0x1700534E RID: 21326
		// (get) Token: 0x060110D4 RID: 69844 RVA: 0x003C306C File Offset: 0x003C126C
		public bool DisplayCancelButton
		{
			get
			{
				return this._displayCancelButton;
			}
		}

		// Token: 0x1700534F RID: 21327
		// (get) Token: 0x060110D5 RID: 69845 RVA: 0x003C3074 File Offset: 0x003C1274
		// (set) Token: 0x060110D6 RID: 69846 RVA: 0x003C3095 File Offset: 0x003C1295
		public HtmlGenericControl TotalProgressBar
		{
			get
			{
				if (this._totalProgressBar == null)
				{
					return this.FindControl(RadProgressArea.PrimaryProgressBarElement) as HtmlGenericControl;
				}
				return this._totalProgressBar;
			}
			set
			{
				this._totalProgressBar = value;
			}
		}

		// Token: 0x17005350 RID: 21328
		// (get) Token: 0x060110D7 RID: 69847 RVA: 0x003C309E File Offset: 0x003C129E
		// (set) Token: 0x060110D8 RID: 69848 RVA: 0x003C30BF File Offset: 0x003C12BF
		public HtmlGenericControl TotalProgress
		{
			get
			{
				if (this._totalProgress == null)
				{
					return this.FindControl(RadProgressArea.PrimaryValueName) as HtmlGenericControl;
				}
				return this._totalProgress;
			}
			set
			{
				this._totalProgress = value;
			}
		}

		// Token: 0x17005351 RID: 21329
		// (get) Token: 0x060110D9 RID: 69849 RVA: 0x003C30C8 File Offset: 0x003C12C8
		// (set) Token: 0x060110DA RID: 69850 RVA: 0x003C30E9 File Offset: 0x003C12E9
		public HtmlGenericControl TotalProgressPercent
		{
			get
			{
				if (this._totalProgressPercent == null)
				{
					return this.FindControl(RadProgressArea.PrimaryPercentName) as HtmlGenericControl;
				}
				return this._totalProgressPercent;
			}
			set
			{
				this._totalProgressPercent = value;
			}
		}

		// Token: 0x17005352 RID: 21330
		// (get) Token: 0x060110DB RID: 69851 RVA: 0x003C30F2 File Offset: 0x003C12F2
		// (set) Token: 0x060110DC RID: 69852 RVA: 0x003C3113 File Offset: 0x003C1313
		public HtmlGenericControl RequestSize
		{
			get
			{
				if (this._requestSize == null)
				{
					return this.FindControl(RadProgressArea.PrimaryTotalName) as HtmlGenericControl;
				}
				return this._requestSize;
			}
			set
			{
				this._requestSize = value;
			}
		}

		// Token: 0x17005353 RID: 21331
		// (get) Token: 0x060110DD RID: 69853 RVA: 0x003C311C File Offset: 0x003C131C
		// (set) Token: 0x060110DE RID: 69854 RVA: 0x003C313D File Offset: 0x003C133D
		public HtmlGenericControl FilesCountBar
		{
			get
			{
				if (this._filesCountBar == null)
				{
					return this.FindControl(RadProgressArea.SecondaryProgressBarElement) as HtmlGenericControl;
				}
				return this._filesCountBar;
			}
			set
			{
				this._filesCountBar = value;
			}
		}

		// Token: 0x17005354 RID: 21332
		// (get) Token: 0x060110DF RID: 69855 RVA: 0x003C3146 File Offset: 0x003C1346
		// (set) Token: 0x060110E0 RID: 69856 RVA: 0x003C3167 File Offset: 0x003C1367
		public HtmlGenericControl FilesCount
		{
			get
			{
				if (this._filesCount == null)
				{
					return this.FindControl(RadProgressArea.SecondaryValueName) as HtmlGenericControl;
				}
				return this._filesCount;
			}
			set
			{
				this._filesCount = value;
			}
		}

		// Token: 0x17005355 RID: 21333
		// (get) Token: 0x060110E1 RID: 69857 RVA: 0x003C3170 File Offset: 0x003C1370
		// (set) Token: 0x060110E2 RID: 69858 RVA: 0x003C3191 File Offset: 0x003C1391
		public HtmlGenericControl FilesCountPercent
		{
			get
			{
				if (this._filesCountPercent == null)
				{
					return this.FindControl(RadProgressArea.SecondaryPercentName) as HtmlGenericControl;
				}
				return this._filesCountPercent;
			}
			set
			{
				this._filesCountPercent = value;
			}
		}

		// Token: 0x17005356 RID: 21334
		// (get) Token: 0x060110E3 RID: 69859 RVA: 0x003C319A File Offset: 0x003C139A
		// (set) Token: 0x060110E4 RID: 69860 RVA: 0x003C31BB File Offset: 0x003C13BB
		public HtmlGenericControl SelectedFilesCount
		{
			get
			{
				if (this._selectedFilesCount == null)
				{
					return this.FindControl(RadProgressArea.SecondaryTotalName) as HtmlGenericControl;
				}
				return this._selectedFilesCount;
			}
			set
			{
				this._selectedFilesCount = value;
			}
		}

		// Token: 0x17005357 RID: 21335
		// (get) Token: 0x060110E5 RID: 69861 RVA: 0x003C31C4 File Offset: 0x003C13C4
		// (set) Token: 0x060110E6 RID: 69862 RVA: 0x003C31E5 File Offset: 0x003C13E5
		public HtmlGenericControl CurrentFileName
		{
			get
			{
				if (this._currentFileName == null)
				{
					return this.FindControl(RadProgressArea.CurrentOperationName) as HtmlGenericControl;
				}
				return this._currentFileName;
			}
			set
			{
				this._currentFileName = value;
			}
		}

		// Token: 0x17005358 RID: 21336
		// (get) Token: 0x060110E7 RID: 69863 RVA: 0x003C31EE File Offset: 0x003C13EE
		// (set) Token: 0x060110E8 RID: 69864 RVA: 0x003C320F File Offset: 0x003C140F
		public HtmlGenericControl TimeElapsed
		{
			get
			{
				if (this._timeElapsed == null)
				{
					return this.FindControl(RadProgressArea.TimeElapsedName) as HtmlGenericControl;
				}
				return this._timeElapsed;
			}
			set
			{
				this._timeElapsed = value;
			}
		}

		// Token: 0x17005359 RID: 21337
		// (get) Token: 0x060110E9 RID: 69865 RVA: 0x003C3218 File Offset: 0x003C1418
		// (set) Token: 0x060110EA RID: 69866 RVA: 0x003C3239 File Offset: 0x003C1439
		public HtmlGenericControl TimeEstimated
		{
			get
			{
				if (this._timeEstimated == null)
				{
					return this.FindControl(RadProgressArea.TimeEstimatedName) as HtmlGenericControl;
				}
				return this._timeEstimated;
			}
			set
			{
				this._timeEstimated = value;
			}
		}

		// Token: 0x1700535A RID: 21338
		// (get) Token: 0x060110EB RID: 69867 RVA: 0x003C3242 File Offset: 0x003C1442
		// (set) Token: 0x060110EC RID: 69868 RVA: 0x003C3263 File Offset: 0x003C1463
		public HtmlGenericControl TransferSpeed
		{
			get
			{
				if (this._transferSpeed == null)
				{
					return this.FindControl(RadProgressArea.SpeedName) as HtmlGenericControl;
				}
				return this._transferSpeed;
			}
			set
			{
				this._transferSpeed = value;
			}
		}

		// Token: 0x1700535B RID: 21339
		// (get) Token: 0x060110ED RID: 69869 RVA: 0x003C326C File Offset: 0x003C146C
		// (set) Token: 0x060110EE RID: 69870 RVA: 0x003C328D File Offset: 0x003C148D
		public HtmlInputButton CancelButton
		{
			get
			{
				if (this._cancelButton == null)
				{
					return this.FindControl(RadProgressArea.CancelButtonName) as HtmlInputButton;
				}
				return this._cancelButton;
			}
			set
			{
				this._cancelButton = value;
			}
		}

		// Token: 0x04004C53 RID: 19539
		private HtmlGenericControl _totalProgressBar;

		// Token: 0x04004C54 RID: 19540
		private HtmlGenericControl _totalProgress;

		// Token: 0x04004C55 RID: 19541
		private HtmlGenericControl _totalProgressPercent;

		// Token: 0x04004C56 RID: 19542
		private HtmlGenericControl _requestSize;

		// Token: 0x04004C57 RID: 19543
		private HtmlGenericControl _filesCountBar;

		// Token: 0x04004C58 RID: 19544
		private HtmlGenericControl _filesCount;

		// Token: 0x04004C59 RID: 19545
		private HtmlGenericControl _filesCountPercent;

		// Token: 0x04004C5A RID: 19546
		private HtmlGenericControl _selectedFilesCount;

		// Token: 0x04004C5B RID: 19547
		private HtmlGenericControl _currentFileName;

		// Token: 0x04004C5C RID: 19548
		private HtmlGenericControl _timeElapsed;

		// Token: 0x04004C5D RID: 19549
		private HtmlGenericControl _timeEstimated;

		// Token: 0x04004C5E RID: 19550
		private HtmlGenericControl _transferSpeed;

		// Token: 0x04004C5F RID: 19551
		private HtmlInputButton _cancelButton;

		// Token: 0x04004C60 RID: 19552
		private ProgressIndicators _progressIndicators;

		// Token: 0x04004C61 RID: 19553
		private bool _displayCancelButton;

		// Token: 0x04004C62 RID: 19554
		private ProgressAreaStrings _localization;
	}
}
