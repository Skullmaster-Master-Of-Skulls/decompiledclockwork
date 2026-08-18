using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar.Collections;

namespace Telerik.Web.UI.Calendar.View
{
	// Token: 0x02001018 RID: 4120
	public abstract class CalendarView : IClientData
	{
		// Token: 0x0600A21B RID: 41499 RVA: 0x00240BA0 File Offset: 0x0023EDA0
		internal CalendarView(RadCalendar parent) : this(parent, null)
		{
		}

		// Token: 0x0600A21C RID: 41500 RVA: 0x00240BAC File Offset: 0x0023EDAC
		internal CalendarView(RadCalendar parent, CalendarView parentView)
		{
			this._ParentCalendar = parent;
			this._ParentView = parentView;
		}

		// Token: 0x17003346 RID: 13126
		// (get) Token: 0x0600A21D RID: 41501 RVA: 0x00240CBC File Offset: 0x0023EEBC
		// (set) Token: 0x0600A21E RID: 41502 RVA: 0x00240CC4 File Offset: 0x0023EEC4
		public RadCalendar ParentCalendar
		{
			get
			{
				return this._ParentCalendar;
			}
			set
			{
				this._ParentCalendar = value;
			}
		}

		// Token: 0x17003347 RID: 13127
		// (get) Token: 0x0600A21F RID: 41503 RVA: 0x00240CCD File Offset: 0x0023EECD
		// (set) Token: 0x0600A220 RID: 41504 RVA: 0x00240CD5 File Offset: 0x0023EED5
		public CalendarView Parent
		{
			get
			{
				return this._ParentView;
			}
			set
			{
				this._ParentView = value;
			}
		}

		// Token: 0x17003348 RID: 13128
		// (get) Token: 0x0600A221 RID: 41505 RVA: 0x00240CDE File Offset: 0x0023EEDE
		// (set) Token: 0x0600A222 RID: 41506 RVA: 0x00240D04 File Offset: 0x0023EF04
		public string ID
		{
			get
			{
				if (this.IsTopView)
				{
					return this.ParentCalendar.ClientID + "_Top";
				}
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		// Token: 0x17003349 RID: 13129
		// (get) Token: 0x0600A223 RID: 41507 RVA: 0x00240D0D File Offset: 0x0023EF0D
		public CalendarViewCollection ChildViews
		{
			get
			{
				if (this._ChildViews == null)
				{
					this._ChildViews = new CalendarViewCollection();
				}
				return this._ChildViews;
			}
		}

		// Token: 0x1700334A RID: 13130
		// (get) Token: 0x0600A224 RID: 41508 RVA: 0x00240D28 File Offset: 0x0023EF28
		public Orientation Orientation
		{
			get
			{
				return this._Orientation;
			}
		}

		// Token: 0x1700334B RID: 13131
		// (get) Token: 0x0600A225 RID: 41509 RVA: 0x00240D30 File Offset: 0x0023EF30
		public string TitleContent
		{
			get
			{
				return this.GetTitleContent();
			}
		}

		// Token: 0x0600A226 RID: 41510
		internal abstract string GetTitleContent();

		// Token: 0x0600A227 RID: 41511
		internal abstract Table GetCalendarViewStructure();

		// Token: 0x0600A228 RID: 41512
		public abstract DateTime GetEffectiveVisibleDate();

		// Token: 0x1700334C RID: 13132
		// (get) Token: 0x0600A229 RID: 41513 RVA: 0x00240D38 File Offset: 0x0023EF38
		// (set) Token: 0x0600A22A RID: 41514 RVA: 0x00240D40 File Offset: 0x0023EF40
		public bool IsHidden
		{
			get
			{
				return this._IsHidden;
			}
			set
			{
				this._IsHidden = value;
			}
		}

		// Token: 0x1700334D RID: 13133
		// (get) Token: 0x0600A22B RID: 41515 RVA: 0x00240D49 File Offset: 0x0023EF49
		public PresentationType PresentationType
		{
			get
			{
				return this._PresentationType;
			}
		}

		// Token: 0x1700334E RID: 13134
		// (get) Token: 0x0600A22C RID: 41516 RVA: 0x00240D51 File Offset: 0x0023EF51
		// (set) Token: 0x0600A22D RID: 41517 RVA: 0x00240D63 File Offset: 0x0023EF63
		public bool ShowCalendarViewHeader
		{
			get
			{
				return !this.IsTopView && this._ShowCalendarViewHeader;
			}
			set
			{
				this._ShowCalendarViewHeader = value;
			}
		}

		// Token: 0x1700334F RID: 13135
		// (get) Token: 0x0600A22E RID: 41518 RVA: 0x00240D6C File Offset: 0x0023EF6C
		// (set) Token: 0x0600A22F RID: 41519 RVA: 0x00240D74 File Offset: 0x0023EF74
		public bool ShowColumnHeaders
		{
			get
			{
				return this.showColumnHeaders;
			}
			set
			{
				this.showColumnHeaders = value;
			}
		}

		// Token: 0x17003350 RID: 13136
		// (get) Token: 0x0600A230 RID: 41520 RVA: 0x00240D7D File Offset: 0x0023EF7D
		// (set) Token: 0x0600A231 RID: 41521 RVA: 0x00240D85 File Offset: 0x0023EF85
		public bool ShowRowHeaders
		{
			get
			{
				return this.showRowHeaders;
			}
			set
			{
				this.showRowHeaders = value;
			}
		}

		// Token: 0x17003351 RID: 13137
		// (get) Token: 0x0600A232 RID: 41522 RVA: 0x00240D8E File Offset: 0x0023EF8E
		// (set) Token: 0x0600A233 RID: 41523 RVA: 0x00240D96 File Offset: 0x0023EF96
		public bool UseRowHeadersAsSelectors
		{
			get
			{
				return this.useRowHeadersAsSelectors;
			}
			set
			{
				this.useRowHeadersAsSelectors = value;
			}
		}

		// Token: 0x17003352 RID: 13138
		// (get) Token: 0x0600A234 RID: 41524 RVA: 0x00240D9F File Offset: 0x0023EF9F
		// (set) Token: 0x0600A235 RID: 41525 RVA: 0x00240DA7 File Offset: 0x0023EFA7
		public bool UseColumnHeadersAsSelectors
		{
			get
			{
				return this.useColumnHeadersAsSelectors;
			}
			set
			{
				this.useColumnHeadersAsSelectors = value;
			}
		}

		// Token: 0x17003353 RID: 13139
		// (get) Token: 0x0600A236 RID: 41526 RVA: 0x00240DB0 File Offset: 0x0023EFB0
		// (set) Token: 0x0600A237 RID: 41527 RVA: 0x00240DB8 File Offset: 0x0023EFB8
		public bool EnableViewSelector
		{
			get
			{
				return this._EnableViewSelector;
			}
			set
			{
				this._EnableViewSelector = value;
			}
		}

		// Token: 0x17003354 RID: 13140
		// (get) Token: 0x0600A238 RID: 41528 RVA: 0x00240DC1 File Offset: 0x0023EFC1
		// (set) Token: 0x0600A239 RID: 41529 RVA: 0x00240DC9 File Offset: 0x0023EFC9
		public bool EnableMultiView
		{
			get
			{
				return this._EnableMultiView;
			}
			set
			{
				this._EnableMultiView = value;
			}
		}

		// Token: 0x17003355 RID: 13141
		// (get) Token: 0x0600A23A RID: 41530 RVA: 0x00240DD2 File Offset: 0x0023EFD2
		// (set) Token: 0x0600A23B RID: 41531 RVA: 0x00240DDA File Offset: 0x0023EFDA
		public bool EnableMultiSelect
		{
			get
			{
				return this._EnableMultiSelect;
			}
			set
			{
				this._EnableMultiSelect = value;
			}
		}

		// Token: 0x17003356 RID: 13142
		// (get) Token: 0x0600A23C RID: 41532 RVA: 0x00240DE3 File Offset: 0x0023EFE3
		public bool IsSingleView
		{
			get
			{
				return this.MultiViewColumns * this.MultiViewRows <= 1;
			}
		}

		// Token: 0x17003357 RID: 13143
		// (get) Token: 0x0600A23D RID: 41533 RVA: 0x00240DF8 File Offset: 0x0023EFF8
		public bool IsTopView
		{
			get
			{
				return this.Equals(this.ParentCalendar.CalendarView);
			}
		}

		// Token: 0x17003358 RID: 13144
		// (get) Token: 0x0600A23E RID: 41534 RVA: 0x00240E0B File Offset: 0x0023F00B
		// (set) Token: 0x0600A23F RID: 41535 RVA: 0x00240E13 File Offset: 0x0023F013
		public bool IsInitialized
		{
			get
			{
				return this._IsInitialized;
			}
			set
			{
				this._IsInitialized = value;
			}
		}

		// Token: 0x17003359 RID: 13145
		// (get) Token: 0x0600A240 RID: 41536 RVA: 0x00240E1C File Offset: 0x0023F01C
		public int MultiViewRows
		{
			get
			{
				if (this.IsTopView)
				{
					if (this._MultiViewRows > 1)
					{
						return this._MultiViewRows;
					}
					if (this.ParentCalendar.MultiViewRows > 1)
					{
						return this.ParentCalendar.MultiViewRows;
					}
				}
				return this._MultiViewRows;
			}
		}

		// Token: 0x1700335A RID: 13146
		// (get) Token: 0x0600A241 RID: 41537 RVA: 0x00240E56 File Offset: 0x0023F056
		public int MultiViewColumns
		{
			get
			{
				if (this.IsTopView)
				{
					if (this._MultiViewColumns > 1)
					{
						return this._MultiViewColumns;
					}
					if (this.ParentCalendar.MultiViewColumns > 1)
					{
						return this.ParentCalendar.MultiViewColumns;
					}
				}
				return this._MultiViewColumns;
			}
		}

		// Token: 0x1700335B RID: 13147
		// (get) Token: 0x0600A242 RID: 41538 RVA: 0x00240E90 File Offset: 0x0023F090
		public int DefaultRow
		{
			get
			{
				if (this.IsSingleView)
				{
					return 0;
				}
				return this._DefaultRow;
			}
		}

		// Token: 0x1700335C RID: 13148
		// (get) Token: 0x0600A243 RID: 41539 RVA: 0x00240EA2 File Offset: 0x0023F0A2
		public int DefaultColumn
		{
			get
			{
				if (this.IsSingleView)
				{
					return 0;
				}
				return this._DefaultColumn;
			}
		}

		// Token: 0x1700335D RID: 13149
		// (get) Token: 0x0600A244 RID: 41540 RVA: 0x00240EB4 File Offset: 0x0023F0B4
		// (set) Token: 0x0600A245 RID: 41541 RVA: 0x00240F0D File Offset: 0x0023F10D
		public Unit SingleViewWidth
		{
			get
			{
				if (this._SingleViewWidth != Unit.Empty)
				{
					return this._SingleViewWidth;
				}
				if (this.ParentCalendar.SingleViewWidth != new Unit("0px"))
				{
					return this.ParentCalendar.SingleViewWidth;
				}
				return this.ParentCalendar.Width;
			}
			set
			{
				this._SingleViewWidth = value;
			}
		}

		// Token: 0x1700335E RID: 13150
		// (get) Token: 0x0600A246 RID: 41542 RVA: 0x00240F18 File Offset: 0x0023F118
		// (set) Token: 0x0600A247 RID: 41543 RVA: 0x00240F71 File Offset: 0x0023F171
		public Unit SingleViewHeight
		{
			get
			{
				if (this._SingleViewHeight != Unit.Empty)
				{
					return this._SingleViewHeight;
				}
				if (this.ParentCalendar.SingleViewHeight != new Unit("0px"))
				{
					return this.ParentCalendar.SingleViewHeight;
				}
				return this.ParentCalendar.Height;
			}
			set
			{
				this._SingleViewHeight = value;
			}
		}

		// Token: 0x1700335F RID: 13151
		// (get) Token: 0x0600A248 RID: 41544 RVA: 0x00240F7A File Offset: 0x0023F17A
		// (set) Token: 0x0600A249 RID: 41545 RVA: 0x00240F96 File Offset: 0x0023F196
		public TableItemStyle HeaderSettings
		{
			get
			{
				if (this._HeaderSettings != null)
				{
					return this._HeaderSettings;
				}
				return this.ParentCalendar.TitleStyle;
			}
			set
			{
				this._HeaderSettings = value;
			}
		}

		// Token: 0x17003360 RID: 13152
		// (get) Token: 0x0600A24A RID: 41546 RVA: 0x00240F9F File Offset: 0x0023F19F
		// (set) Token: 0x0600A24B RID: 41547 RVA: 0x00240FBB File Offset: 0x0023F1BB
		public TableItemStyle ViewSettings
		{
			get
			{
				if (this._ViewSettings != null)
				{
					return this._ViewSettings;
				}
				return this.ParentCalendar.CalendarTableStyle;
			}
			set
			{
				this._ViewSettings = value;
			}
		}

		// Token: 0x17003361 RID: 13153
		// (get) Token: 0x0600A24C RID: 41548 RVA: 0x00240FC4 File Offset: 0x0023F1C4
		// (set) Token: 0x0600A24D RID: 41549 RVA: 0x00240FE1 File Offset: 0x0023F1E1
		public HorizontalAlign TitleAlign
		{
			get
			{
				if (this._TitleAlign != HorizontalAlign.Center)
				{
					return this._TitleAlign;
				}
				return this.ParentCalendar.TitleAlign;
			}
			set
			{
				this._TitleAlign = value;
			}
		}

		// Token: 0x17003362 RID: 13154
		// (get) Token: 0x0600A24E RID: 41550 RVA: 0x00240FEA File Offset: 0x0023F1EA
		// (set) Token: 0x0600A24F RID: 41551 RVA: 0x00240FF2 File Offset: 0x0023F1F2
		public DateTime CurrentViewBeginDate
		{
			get
			{
				return this._CurrentViewBeginDate;
			}
			set
			{
				this._CurrentViewBeginDate = value;
			}
		}

		// Token: 0x17003363 RID: 13155
		// (get) Token: 0x0600A250 RID: 41552 RVA: 0x00240FFB File Offset: 0x0023F1FB
		// (set) Token: 0x0600A251 RID: 41553 RVA: 0x00241003 File Offset: 0x0023F203
		public DateTime CurrentViewEndDate
		{
			get
			{
				return this._CurrentViewEndDate;
			}
			set
			{
				this._CurrentViewEndDate = value;
			}
		}

		// Token: 0x17003364 RID: 13156
		// (get) Token: 0x0600A252 RID: 41554 RVA: 0x0024100C File Offset: 0x0023F20C
		public CalendarView PreviousView
		{
			get
			{
				return this.GetPreviousView();
			}
		}

		// Token: 0x17003365 RID: 13157
		// (get) Token: 0x0600A253 RID: 41555 RVA: 0x00241014 File Offset: 0x0023F214
		public CalendarView NextView
		{
			get
			{
				return this.GetNextView();
			}
		}

		// Token: 0x17003366 RID: 13158
		// (get) Token: 0x0600A254 RID: 41556 RVA: 0x0024101C File Offset: 0x0023F21C
		// (set) Token: 0x0600A255 RID: 41557 RVA: 0x00241038 File Offset: 0x0023F238
		[Localizable(true)]
		public string Title
		{
			get
			{
				if (string.IsNullOrEmpty(this.title))
				{
					return this.GetTitleContent();
				}
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x17003367 RID: 13159
		// (get) Token: 0x0600A256 RID: 41558 RVA: 0x00241041 File Offset: 0x0023F241
		// (set) Token: 0x0600A257 RID: 41559 RVA: 0x00241049 File Offset: 0x0023F249
		[Localizable(true)]
		public string ConditionsErrorMessage
		{
			get
			{
				return this._ConditionsErrorMessage;
			}
			set
			{
				this._ConditionsErrorMessage = value;
			}
		}

		// Token: 0x0600A258 RID: 41560 RVA: 0x00241052 File Offset: 0x0023F252
		public virtual void EnsureRenderSettings()
		{
		}

		// Token: 0x0600A259 RID: 41561 RVA: 0x00241054 File Offset: 0x0023F254
		internal virtual void Render(HtmlTextWriter writer)
		{
			this.UpdateDefaultCalendar();
		}

		// Token: 0x0600A25A RID: 41562 RVA: 0x0024105C File Offset: 0x0023F25C
		internal virtual void RenderHeader(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600A25B RID: 41563 RVA: 0x0024105E File Offset: 0x0023F25E
		internal virtual void RenderBody(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600A25C RID: 41564 RVA: 0x00241060 File Offset: 0x0023F260
		internal virtual void Initialize()
		{
			if (this.IsTopView && this.IsSingleView)
			{
				this.ShowColumnHeaders = this.ParentCalendar.ShowColumnHeaders;
				this.ShowRowHeaders = this.ParentCalendar.ShowRowHeaders;
				this.EnableViewSelector = this.ParentCalendar.EnableViewSelector;
			}
			else
			{
				this.ShowCalendarViewHeader = true;
			}
			this.SetOrientation(this.ParentCalendar.Orientation);
			this.IsInitialized = true;
		}

		// Token: 0x0600A25D RID: 41565 RVA: 0x002410D4 File Offset: 0x0023F2D4
		internal virtual void UpdateDefaultCalendar()
		{
			if (this.IsTopView)
			{
				if (this.ParentCalendar.DateTimeFormat.Calendar != null)
				{
					this.DefaultCalendar = this.ParentCalendar.DateTimeFormat.Calendar;
					return;
				}
				this.DefaultCalendar = DateTimeFormatInfo.CurrentInfo.Calendar;
			}
		}

		// Token: 0x0600A25E RID: 41566 RVA: 0x00241122 File Offset: 0x0023F322
		internal virtual void CreateChildren()
		{
		}

		// Token: 0x0600A25F RID: 41567 RVA: 0x00241124 File Offset: 0x0023F324
		public virtual CalendarView CreateCalendarView()
		{
			return null;
		}

		// Token: 0x0600A260 RID: 41568 RVA: 0x00241127 File Offset: 0x0023F327
		internal virtual CalendarView GetPreviousView()
		{
			return null;
		}

		// Token: 0x0600A261 RID: 41569 RVA: 0x0024112A File Offset: 0x0023F32A
		internal virtual CalendarView GetPreviousView(int months)
		{
			return null;
		}

		// Token: 0x0600A262 RID: 41570 RVA: 0x0024112D File Offset: 0x0023F32D
		internal virtual CalendarView GetNextView()
		{
			return null;
		}

		// Token: 0x0600A263 RID: 41571 RVA: 0x00241130 File Offset: 0x0023F330
		internal virtual CalendarView GetNextView(int months)
		{
			return null;
		}

		// Token: 0x0600A264 RID: 41572 RVA: 0x00241133 File Offset: 0x0023F333
		internal virtual void ProcessCalendarDays(TableCell processedCell, DateTime processedDate)
		{
		}

		// Token: 0x0600A265 RID: 41573 RVA: 0x00241135 File Offset: 0x0023F335
		internal void SetRows(int value)
		{
			this._MultiViewRows = value;
		}

		// Token: 0x0600A266 RID: 41574 RVA: 0x0024113E File Offset: 0x0023F33E
		internal void SetDefaultRow(int value)
		{
			this._DefaultRow = value;
		}

		// Token: 0x0600A267 RID: 41575 RVA: 0x00241147 File Offset: 0x0023F347
		internal void SetColumns(int value)
		{
			this._MultiViewColumns = value;
		}

		// Token: 0x0600A268 RID: 41576 RVA: 0x00241150 File Offset: 0x0023F350
		internal void SetDefaultColumn(int value)
		{
			this._DefaultColumn = value;
		}

		// Token: 0x0600A269 RID: 41577 RVA: 0x00241159 File Offset: 0x0023F359
		internal void SetOrientation(Orientation type)
		{
			this._Orientation = type;
		}

		// Token: 0x0600A26A RID: 41578 RVA: 0x00241162 File Offset: 0x0023F362
		internal void SetPresentationType(PresentationType type)
		{
			this._PresentationType = type;
		}

		// Token: 0x0600A26B RID: 41579 RVA: 0x0024116B File Offset: 0x0023F36B
		internal void SetRange(DateTime beginDate, DateTime endDate)
		{
			this._CurrentViewBeginDate = beginDate;
			this._CurrentViewEndDate = endDate;
		}

		// Token: 0x0600A26C RID: 41580 RVA: 0x0024117B File Offset: 0x0023F37B
		internal void SetSingleViewRows(int value)
		{
			this._SingleViewRows = value;
		}

		// Token: 0x0600A26D RID: 41581 RVA: 0x00241184 File Offset: 0x0023F384
		internal void SetSingleViewColumns(int value)
		{
			this._SingleViewColumns = value;
		}

		// Token: 0x17003368 RID: 13160
		// (get) Token: 0x0600A26E RID: 41582 RVA: 0x0024118D File Offset: 0x0023F38D
		public int SingleViewRows
		{
			get
			{
				if (this._SingleViewRows != 6 && this._SingleViewRows != 0)
				{
					return this._SingleViewRows;
				}
				if (this.ParentCalendar.SingleViewRows != 0)
				{
					return this.ParentCalendar.SingleViewRows;
				}
				return this._SingleViewRows;
			}
		}

		// Token: 0x17003369 RID: 13161
		// (get) Token: 0x0600A26F RID: 41583 RVA: 0x002411C6 File Offset: 0x0023F3C6
		public int SingleViewColumns
		{
			get
			{
				if (this._SingleViewColumns != 7 && this._SingleViewColumns != 0)
				{
					return this._SingleViewColumns;
				}
				if (this.ParentCalendar.SingleViewColumns != 0)
				{
					return this.ParentCalendar.SingleViewColumns;
				}
				return this._SingleViewColumns;
			}
		}

		// Token: 0x1700336A RID: 13162
		// (get) Token: 0x0600A270 RID: 41584 RVA: 0x002411FF File Offset: 0x0023F3FF
		// (set) Token: 0x0600A271 RID: 41585 RVA: 0x00241220 File Offset: 0x0023F420
		[Localizable(true)]
		public string RowSelectorText
		{
			get
			{
				if (!string.IsNullOrEmpty(this._RowHeaderText))
				{
					return this._RowHeaderText;
				}
				return this.ParentCalendar.RowHeaderText;
			}
			set
			{
				this._RowHeaderText = value;
			}
		}

		// Token: 0x1700336B RID: 13163
		// (get) Token: 0x0600A272 RID: 41586 RVA: 0x00241229 File Offset: 0x0023F429
		// (set) Token: 0x0600A273 RID: 41587 RVA: 0x0024124A File Offset: 0x0023F44A
		[Localizable(true)]
		public string RowHeaderImage
		{
			get
			{
				if (!string.IsNullOrEmpty(this._RowHeaderImage))
				{
					return this._RowHeaderImage;
				}
				return this.ParentCalendar.RowHeaderImage;
			}
			set
			{
				this._RowHeaderImage = value;
			}
		}

		// Token: 0x1700336C RID: 13164
		// (get) Token: 0x0600A274 RID: 41588 RVA: 0x00241253 File Offset: 0x0023F453
		// (set) Token: 0x0600A275 RID: 41589 RVA: 0x00241274 File Offset: 0x0023F474
		[Localizable(true)]
		public string ColumnHeaderText
		{
			get
			{
				if (!string.IsNullOrEmpty(this._ColumnHeaderText))
				{
					return this._ColumnHeaderText;
				}
				return this.ParentCalendar.ColumnHeaderText;
			}
			set
			{
				this._ColumnHeaderText = value;
			}
		}

		// Token: 0x1700336D RID: 13165
		// (get) Token: 0x0600A276 RID: 41590 RVA: 0x0024127D File Offset: 0x0023F47D
		// (set) Token: 0x0600A277 RID: 41591 RVA: 0x0024129E File Offset: 0x0023F49E
		[Localizable(true)]
		public string ColumnHeaderImage
		{
			get
			{
				if (!string.IsNullOrEmpty(this._ColumnHeaderImage))
				{
					return this._ColumnHeaderImage;
				}
				return this.ParentCalendar.ColumnHeaderImage;
			}
			set
			{
				this._ColumnHeaderImage = value;
			}
		}

		// Token: 0x1700336E RID: 13166
		// (get) Token: 0x0600A278 RID: 41592 RVA: 0x002412A7 File Offset: 0x0023F4A7
		// (set) Token: 0x0600A279 RID: 41593 RVA: 0x002412C8 File Offset: 0x0023F4C8
		[Localizable(true)]
		public string ViewSelectorText
		{
			get
			{
				if (!string.IsNullOrEmpty(this._ViewSelectorText))
				{
					return this._ViewSelectorText;
				}
				return this.ParentCalendar.ViewSelectorText;
			}
			set
			{
				this._ViewSelectorText = value;
			}
		}

		// Token: 0x1700336F RID: 13167
		// (get) Token: 0x0600A27A RID: 41594 RVA: 0x002412D1 File Offset: 0x0023F4D1
		// (set) Token: 0x0600A27B RID: 41595 RVA: 0x002412F2 File Offset: 0x0023F4F2
		[Localizable(true)]
		public string ViewSelectorImage
		{
			get
			{
				if (!string.IsNullOrEmpty(this._ViewSelectorImage))
				{
					return this._ViewSelectorImage;
				}
				return this.ParentCalendar.ViewSelectorImage;
			}
			set
			{
				this._ViewSelectorImage = value;
			}
		}

		// Token: 0x17003370 RID: 13168
		// (get) Token: 0x0600A27C RID: 41596 RVA: 0x002412FB File Offset: 0x0023F4FB
		// (set) Token: 0x0600A27D RID: 41597 RVA: 0x00241303 File Offset: 0x0023F503
		public DateTime ViewStartDate
		{
			get
			{
				return this._ViewStartDate;
			}
			set
			{
				this._ViewStartDate = value;
			}
		}

		// Token: 0x17003371 RID: 13169
		// (get) Token: 0x0600A27E RID: 41598 RVA: 0x0024130C File Offset: 0x0023F50C
		// (set) Token: 0x0600A27F RID: 41599 RVA: 0x00241314 File Offset: 0x0023F514
		public DateTime ViewEndDate
		{
			get
			{
				return this._ViewEndDate;
			}
			set
			{
				this._ViewEndDate = value;
			}
		}

		// Token: 0x17003372 RID: 13170
		// (get) Token: 0x0600A280 RID: 41600 RVA: 0x0024131D File Offset: 0x0023F51D
		internal DateTime ViewCacheStartDate
		{
			get
			{
				return this._ViewCacheStartDate;
			}
		}

		// Token: 0x17003373 RID: 13171
		// (get) Token: 0x0600A281 RID: 41601 RVA: 0x00241325 File Offset: 0x0023F525
		internal DateTime ViewCacheEndDate
		{
			get
			{
				return this._ViewCacheEndDate;
			}
		}

		// Token: 0x17003374 RID: 13172
		// (get) Token: 0x0600A282 RID: 41602 RVA: 0x00241330 File Offset: 0x0023F530
		internal int MonthsInView
		{
			get
			{
				int result;
				if (this.IsSingleView)
				{
					result = 1;
				}
				else
				{
					result = this.MultiViewColumns * this.MultiViewRows;
				}
				return result;
			}
		}

		// Token: 0x0600A283 RID: 41603 RVA: 0x0024135A File Offset: 0x0023F55A
		protected virtual void SetViewDateRange()
		{
		}

		// Token: 0x0600A284 RID: 41604 RVA: 0x0024135C File Offset: 0x0023F55C
		internal void GetViewRowsAndColumns(out int rows, out int columns)
		{
			int num = 0;
			int num2 = 0;
			this.GetContentOffset(out num, out num2);
			int num3 = 0;
			int num4 = 0;
			this.GetContentRowsAndColumns(out num3, out num4);
			rows = num3 + num;
			columns = num4 + num2;
		}

		// Token: 0x0600A285 RID: 41605 RVA: 0x0024138F File Offset: 0x0023F58F
		internal void GetContentRowsAndColumns(out int rows, out int columns)
		{
			if (this.IsSingleView)
			{
				rows = this.SingleViewRows;
				columns = this.SingleViewColumns;
				return;
			}
			rows = this.MultiViewRows;
			columns = this.MultiViewColumns;
		}

		// Token: 0x0600A286 RID: 41606 RVA: 0x002413BC File Offset: 0x0023F5BC
		internal void GetContentOffset(out int xShift, out int yShift)
		{
			int num = 0;
			int num2 = 0;
			if (this.EnableViewSelector)
			{
				num2++;
				num++;
			}
			else
			{
				if (this.ShowColumnHeaders)
				{
					num++;
				}
				if (this.ShowRowHeaders)
				{
					num2++;
				}
			}
			xShift = num;
			yShift = num2;
		}

		// Token: 0x0600A287 RID: 41607 RVA: 0x002413FD File Offset: 0x0023F5FD
		ArrayList IClientData.GetClientData()
		{
			return this.GetClientData();
		}

		// Token: 0x0600A288 RID: 41608 RVA: 0x00241408 File Offset: 0x0023F608
		private ArrayList GetClientData()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Pair(this.ID, this.GetViewClientData(this as MonthView)));
			for (int i = 0; i < this.ChildViews.Count; i++)
			{
				arrayList.Add(new Pair(this.ChildViews[i].ID, this.GetViewClientData(this.ChildViews[i] as MonthView)));
			}
			return arrayList;
		}

		// Token: 0x0600A289 RID: 41609 RVA: 0x00241490 File Offset: 0x0023F690
		private string GetViewClientData(MonthView view)
		{
			string arg = view.MonthStartDate.ToString("[yyyy,M,d]");
			return string.Format("[{0}, {1}]", arg, view.MonthsInView);
		}

		// Token: 0x04002D19 RID: 11545
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		protected System.Globalization.Calendar DefaultCalendar;

		// Token: 0x04002D1A RID: 11546
		private RadCalendar _ParentCalendar;

		// Token: 0x04002D1B RID: 11547
		private CalendarView _ParentView;

		// Token: 0x04002D1C RID: 11548
		private string _ID;

		// Token: 0x04002D1D RID: 11549
		private CalendarViewCollection _ChildViews;

		// Token: 0x04002D1E RID: 11550
		private Orientation _Orientation = Orientation.RenderInRows;

		// Token: 0x04002D1F RID: 11551
		private string _TitleContent = string.Empty;

		// Token: 0x04002D20 RID: 11552
		private bool _IsHidden;

		// Token: 0x04002D21 RID: 11553
		private PresentationType _PresentationType = PresentationType.Interactive;

		// Token: 0x04002D22 RID: 11554
		private bool _ShowCalendarViewHeader;

		// Token: 0x04002D23 RID: 11555
		private bool showColumnHeaders = true;

		// Token: 0x04002D24 RID: 11556
		private bool showRowHeaders = true;

		// Token: 0x04002D25 RID: 11557
		private bool useRowHeadersAsSelectors;

		// Token: 0x04002D26 RID: 11558
		private bool useColumnHeadersAsSelectors;

		// Token: 0x04002D27 RID: 11559
		private bool _EnableViewSelector;

		// Token: 0x04002D28 RID: 11560
		private bool _EnableMultiView;

		// Token: 0x04002D29 RID: 11561
		private bool _EnableMultiSelect;

		// Token: 0x04002D2A RID: 11562
		private bool _IsInitialized;

		// Token: 0x04002D2B RID: 11563
		private int _MultiViewRows = 1;

		// Token: 0x04002D2C RID: 11564
		private int _MultiViewColumns = 1;

		// Token: 0x04002D2D RID: 11565
		private int _DefaultRow;

		// Token: 0x04002D2E RID: 11566
		private int _DefaultColumn;

		// Token: 0x04002D2F RID: 11567
		private Unit _SingleViewWidth = Unit.Empty;

		// Token: 0x04002D30 RID: 11568
		private Unit _SingleViewHeight = Unit.Empty;

		// Token: 0x04002D31 RID: 11569
		private TableItemStyle _HeaderSettings;

		// Token: 0x04002D32 RID: 11570
		private TableItemStyle _ViewSettings;

		// Token: 0x04002D33 RID: 11571
		private HorizontalAlign _TitleAlign = HorizontalAlign.Center;

		// Token: 0x04002D34 RID: 11572
		private DateTime _CurrentViewBeginDate = DateTime.MinValue;

		// Token: 0x04002D35 RID: 11573
		private DateTime _CurrentViewEndDate = DateTime.MinValue;

		// Token: 0x04002D36 RID: 11574
		private string title;

		// Token: 0x04002D37 RID: 11575
		private string _ConditionsErrorMessage = string.Empty;

		// Token: 0x04002D38 RID: 11576
		private int _SingleViewRows = 6;

		// Token: 0x04002D39 RID: 11577
		private int _SingleViewColumns = 7;

		// Token: 0x04002D3A RID: 11578
		private string _RowHeaderText = string.Empty;

		// Token: 0x04002D3B RID: 11579
		private string _RowHeaderImage = string.Empty;

		// Token: 0x04002D3C RID: 11580
		private string _ColumnHeaderText = string.Empty;

		// Token: 0x04002D3D RID: 11581
		private string _ColumnHeaderImage = string.Empty;

		// Token: 0x04002D3E RID: 11582
		private string _ViewSelectorText = string.Empty;

		// Token: 0x04002D3F RID: 11583
		private string _ViewSelectorImage = string.Empty;

		// Token: 0x04002D40 RID: 11584
		private DateTime _ViewStartDate = DateTime.MinValue;

		// Token: 0x04002D41 RID: 11585
		private DateTime _ViewEndDate = DateTime.MinValue;

		// Token: 0x04002D42 RID: 11586
		private DateTime _ViewCacheStartDate = DateTime.MinValue;

		// Token: 0x04002D43 RID: 11587
		private DateTime _ViewCacheEndDate = DateTime.MinValue;
	}
}
