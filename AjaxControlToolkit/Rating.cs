using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000163 RID: 355
	[NonVisualControl]
	[ToolboxData("<{0}:Rating runat=\"server\"></{0}:Rating>")]
	[Designer(typeof(RatingExtenderDesigner))]
	[ToolboxBitmap(typeof(Accessor), "Rating.bmp")]
	public class Rating : Panel, ICallbackEventHandler, IPostBackEventHandler
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x000183A3 File Offset: 0x000165A3
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x000183B6 File Offset: 0x000165B6
		[Description("True to cause a postback on rating change")]
		[Category("Behavior")]
		[ClientPropertyName("autoPostBack")]
		[DefaultValue(false)]
		public bool AutoPostBack
		{
			get
			{
				this.EnsureChildControls();
				return this._extender.AutoPostBack;
			}
			set
			{
				this.EnsureChildControls();
				this._extender.AutoPostBack = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x000183CA File Offset: 0x000165CA
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x000183DD File Offset: 0x000165DD
		[DefaultValue(3)]
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Rating")]
		[Bindable(true, BindingDirection.TwoWay)]
		public int CurrentRating
		{
			get
			{
				this.EnsureChildControls();
				return this._extender.Rating;
			}
			set
			{
				if (value <= this.MaxRating)
				{
					this.EnsureChildControls();
					this._extender.Rating = value;
					return;
				}
				throw new ArgumentOutOfRangeException("CurrentRating", "CurrentRating must be greater than MaxRating");
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001840A File Offset: 0x0001660A
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0001841D File Offset: 0x0001661D
		[Bindable(true, BindingDirection.TwoWay)]
		[ClientPropertyName("maxRating")]
		[Browsable(true)]
		[Category("Behavior")]
		[Description("MaxRating")]
		[DefaultValue(5)]
		public int MaxRating
		{
			get
			{
				this.EnsureChildControls();
				return this._extender.MaxRating;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("MaxRating", "MaxRating must be greater than zero");
				}
				this.EnsureChildControls();
				this._extender.MaxRating = value;
				if (this.CurrentRating > value)
				{
					this.CurrentRating = this.MaxRating;
					return;
				}
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0001845B File Offset: 0x0001665B
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x0001846E File Offset: 0x0001666E
		[Category("Behavior")]
		[Browsable(true)]
		[Description("BehaviorID")]
		[DefaultValue("")]
		public string BehaviorID
		{
			get
			{
				this.EnsureChildControls();
				return this._extender.BehaviorID;
			}
			set
			{
				this.EnsureChildControls();
				this._extender.BehaviorID = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00018482 File Offset: 0x00016682
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00018495 File Offset: 0x00016695
		[Browsable(true)]
		[ClientPropertyName("readOnly")]
		[Category("Behavior")]
		[Description("ReadOnly")]
		[DefaultValue(false)]
		[Bindable(true, BindingDirection.TwoWay)]
		public bool ReadOnly
		{
			get
			{
				this.EnsureChildControls();
				return this._extender.ReadOnly;
			}
			set
			{
				this.EnsureChildControls();
				this._extender.ReadOnly = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x000184A9 File Offset: 0x000166A9
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x000184BC File Offset: 0x000166BC
		[Browsable(true)]
		[ClientPropertyName("tag")]
		[Category("Behavior")]
		[Description("Tag")]
		[DefaultValue("")]
		[Bindable(true, BindingDirection.TwoWay)]
		public string Tag
		{
			get
			{
				this.EnsureChildControls();
				return this._extender.Tag;
			}
			set
			{
				this.EnsureChildControls();
				this._extender.Tag = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000184D0 File Offset: 0x000166D0
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x000184E3 File Offset: 0x000166E3
		[Category("Behavior")]
		[Browsable(true)]
		[Themeable(true)]
		[ClientPropertyName("starCssClass")]
		[Description("StarCssClass")]
		[DefaultValue("")]
		public string StarCssClass
		{
			get
			{
				this.EnsureChildControls();
				return this._extender.StarCssClass;
			}
			set
			{
				this.EnsureChildControls();
				this._extender.StarCssClass = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x000184F7 File Offset: 0x000166F7
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0001850A File Offset: 0x0001670A
		[ClientPropertyName("filledStarCssClass")]
		[Browsable(true)]
		[Themeable(true)]
		[Category("Behavior")]
		[Description("FilledStarCssClass")]
		[DefaultValue("")]
		public string FilledStarCssClass
		{
			get
			{
				this.EnsureChildControls();
				return this._extender.FilledStarCssClass;
			}
			set
			{
				this.EnsureChildControls();
				this._extender.FilledStarCssClass = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0001851E File Offset: 0x0001671E
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x00018531 File Offset: 0x00016731
		[Description("EmptyStarCssClass")]
		[ClientPropertyName("emptyStarCssClass")]
		[Browsable(true)]
		[Themeable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		public string EmptyStarCssClass
		{
			get
			{
				this.EnsureChildControls();
				return this._extender.EmptyStarCssClass;
			}
			set
			{
				this.EnsureChildControls();
				this._extender.EmptyStarCssClass = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00018545 File Offset: 0x00016745
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x00018558 File Offset: 0x00016758
		[Themeable(true)]
		[Browsable(true)]
		[ClientPropertyName("waitingStarCssClass")]
		[Category("Behavior")]
		[Description("WaitingStarCssClass")]
		[DefaultValue("")]
		public string WaitingStarCssClass
		{
			get
			{
				this.EnsureChildControls();
				return this._extender.WaitingStarCssClass;
			}
			set
			{
				this.EnsureChildControls();
				this._extender.WaitingStarCssClass = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0001856C File Offset: 0x0001676C
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x00018574 File Offset: 0x00016774
		[DefaultValue(Orientation.Horizontal)]
		[Browsable(true)]
		[Themeable(true)]
		[Category("Appearance")]
		[Description("Rating Align")]
		public Orientation RatingAlign
		{
			get
			{
				return this._align;
			}
			set
			{
				this._align = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001857D File Offset: 0x0001677D
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0001858B File Offset: 0x0001678B
		[Description("Rating Direction")]
		[Category("Appearance")]
		[ClientPropertyName("ratingDirection")]
		[Themeable(true)]
		[Browsable(true)]
		[DefaultValue(RatingDirection.LeftToRightTopToBottom)]
		public RatingDirection RatingDirection
		{
			get
			{
				this.EnsureChildControls();
				return this._direction;
			}
			set
			{
				this.EnsureChildControls();
				this._direction = value;
				this._extender.RatingDirection = (int)value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x000185A6 File Offset: 0x000167A6
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x000185AE File Offset: 0x000167AE
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
				this.EnsureChildControls();
				this._extender.ID = value + "_RatingExtender";
				this._extender.TargetControlID = value;
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000185DF File Offset: 0x000167DF
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this._extender = new RatingExtender();
			if (!base.DesignMode)
			{
				this.Controls.Add(this._extender);
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001860C File Offset: 0x0001680C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			int currentRating = this.CurrentRating;
			int maxRating = this.MaxRating;
			writer.AddAttribute("href", "javascript:void(0)");
			writer.AddAttribute("style", "text-decoration:none");
			writer.AddAttribute("id", this.ClientID + "_A");
			writer.AddAttribute("title", currentRating.ToString(CultureInfo.CurrentCulture));
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			for (int i = 1; i < this.MaxRating + 1; i++)
			{
				writer.AddAttribute("id", this.ClientID + "_Star_" + i.ToString(CultureInfo.InvariantCulture));
				if (this._align == Orientation.Horizontal)
				{
					writer.AddStyleAttribute("float", "left");
				}
				if (this._direction == RatingDirection.LeftToRightTopToBottom)
				{
					if (i <= currentRating)
					{
						writer.AddAttribute("class", this.StarCssClass + " " + this.FilledStarCssClass);
					}
					else
					{
						writer.AddAttribute("class", this.StarCssClass + " " + this.EmptyStarCssClass);
					}
				}
				else if (i <= maxRating - currentRating)
				{
					writer.AddAttribute("class", this.StarCssClass + " " + this.EmptyStarCssClass);
				}
				else
				{
					writer.AddAttribute("class", this.StarCssClass + " " + this.FilledStarCssClass);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001879C File Offset: 0x0001699C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ClientScriptManager clientScript = this.Page.ClientScript;
			clientScript.GetCallbackEventReference(this, string.Empty, string.Empty, string.Empty);
			this.EnsureChildControls();
			this._extender.CallbackID = this.UniqueID;
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000980 RID: 2432 RVA: 0x000187EA File Offset: 0x000169EA
		// (remove) Token: 0x06000981 RID: 2433 RVA: 0x000187FD File Offset: 0x000169FD
		public event RatingEventHandler Changed
		{
			add
			{
				base.Events.AddHandler(Rating.EventChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(Rating.EventChange, value);
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00018810 File Offset: 0x00016A10
		protected virtual void OnChanged(RatingEventArgs e)
		{
			RatingEventHandler ratingEventHandler = (RatingEventHandler)base.Events[Rating.EventChange];
			if (ratingEventHandler != null)
			{
				ratingEventHandler(this, e);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000983 RID: 2435 RVA: 0x0001883E File Offset: 0x00016A3E
		// (remove) Token: 0x06000984 RID: 2436 RVA: 0x00018851 File Offset: 0x00016A51
		public event RatingEventHandler Click
		{
			add
			{
				base.Events.AddHandler(Rating.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Rating.EventClick, value);
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00018864 File Offset: 0x00016A64
		protected virtual void OnClick(RatingEventArgs e)
		{
			RatingEventHandler ratingEventHandler = (RatingEventHandler)base.Events[Rating.EventClick];
			if (ratingEventHandler != null)
			{
				ratingEventHandler(this, e);
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00018892 File Offset: 0x00016A92
		public string GetCallbackResult()
		{
			return this._returnFromEvent;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001889C File Offset: 0x00016A9C
		public void RaiseCallbackEvent(string eventArgument)
		{
			RatingEventArgs ratingEventArgs = new RatingEventArgs(eventArgument);
			this.OnClick(ratingEventArgs);
			int num = Convert.ToInt32(ratingEventArgs.Value.Replace(";", ""));
			if (num != this.CurrentRating)
			{
				this.OnChanged(ratingEventArgs);
			}
			this._returnFromEvent = ratingEventArgs.CallbackResult;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000188F0 File Offset: 0x00016AF0
		public void RaisePostBackEvent(string eventArgument)
		{
			RatingEventArgs ratingEventArgs = new RatingEventArgs(eventArgument);
			this.OnClick(ratingEventArgs);
			int num = Convert.ToInt32(ratingEventArgs.Value.Replace(";", ""));
			if (num != this.CurrentRating)
			{
				this.OnChanged(ratingEventArgs);
			}
		}

		// Token: 0x040003B8 RID: 952
		private static readonly object EventChange = new object();

		// Token: 0x040003B9 RID: 953
		private static readonly object EventClick = new object();

		// Token: 0x040003BA RID: 954
		private RatingExtender _extender;

		// Token: 0x040003BB RID: 955
		private string _returnFromEvent;

		// Token: 0x040003BC RID: 956
		private Orientation _align;

		// Token: 0x040003BD RID: 957
		private RatingDirection _direction;
	}
}
