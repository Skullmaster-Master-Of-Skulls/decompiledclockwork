using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000481 RID: 1153
	[ControlBuilder(typeof(MultiViewControlBuilder))]
	[Designer("System.Web.UI.Design.WebControls.MultiViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("ActiveViewChanged")]
	[ParseChildren(typeof(View))]
	[ToolboxData("<{0}:MultiView runat=\"server\"></{0}:MultiView>")]
	public class MultiView : Control
	{
		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06003922 RID: 14626 RVA: 0x000B9E46 File Offset: 0x000B8046
		// (set) Token: 0x06003923 RID: 14627 RVA: 0x000B9E60 File Offset: 0x000B8060
		[DefaultValue(-1)]
		[WebCategory("Behavior")]
		[WebSysDescription("MultiView_ActiveView")]
		public virtual int ActiveViewIndex
		{
			get
			{
				if (this._cachedActiveViewIndex > -1)
				{
					return this._cachedActiveViewIndex;
				}
				return this._activeViewIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("MultiView_ActiveViewIndex_less_than_minus_one", new object[]
					{
						value
					}));
				}
				if (this.Views.Count == 0 && base.ControlState < ControlState.FrameworkInitialized)
				{
					this._cachedActiveViewIndex = value;
					return;
				}
				if (value >= this.Views.Count)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("MultiView_ActiveViewIndex_equal_or_greater_than_count", new object[]
					{
						value,
						this.Views.Count
					}));
				}
				int num = (this._cachedActiveViewIndex != -1) ? -1 : this._activeViewIndex;
				this._activeViewIndex = value;
				this._cachedActiveViewIndex = -1;
				if (num != value && num != -1 && num < this.Views.Count)
				{
					this.Views[num].Active = false;
					if (this.ShouldTriggerViewEvent)
					{
						this.Views[num].OnDeactivate(EventArgs.Empty);
					}
				}
				if (num != value && this.Views.Count != 0 && value != -1)
				{
					this.Views[value].Active = true;
					if (this.ShouldTriggerViewEvent)
					{
						this.Views[value].OnActivate(EventArgs.Empty);
						this.OnActiveViewChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x00075E05 File Offset: 0x00074005
		// (set) Token: 0x06003925 RID: 14629 RVA: 0x00075E0D File Offset: 0x0007400D
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06003926 RID: 14630 RVA: 0x000B9FAF File Offset: 0x000B81AF
		private bool ShouldTriggerViewEvent
		{
			get
			{
				return this._controlStateApplied || (this.Page != null && !this.Page.IsPostBack);
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x000B9FD3 File Offset: 0x000B81D3
		[Browsable(false)]
		[WebSysDescription("MultiView_Views")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual ViewCollection Views
		{
			get
			{
				return (ViewCollection)this.Controls;
			}
		}

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06003928 RID: 14632 RVA: 0x000B9FE0 File Offset: 0x000B81E0
		// (remove) Token: 0x06003929 RID: 14633 RVA: 0x000B9FF3 File Offset: 0x000B81F3
		[WebCategory("Action")]
		[WebSysDescription("MultiView_ActiveViewChanged")]
		public event EventHandler ActiveViewChanged
		{
			add
			{
				base.Events.AddHandler(MultiView._eventActiveViewChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(MultiView._eventActiveViewChanged, value);
			}
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000BA008 File Offset: 0x000B8208
		protected override void AddParsedSubObject(object obj)
		{
			if (obj is View)
			{
				this.Controls.Add((Control)obj);
				return;
			}
			if (!(obj is LiteralControl))
			{
				throw new HttpException(SR.GetString("MultiView_cannot_have_children_of_type", new object[]
				{
					obj.GetType().Name
				}));
			}
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000BA05B File Offset: 0x000B825B
		protected override ControlCollection CreateControlCollection()
		{
			return new ViewCollection(this);
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000BA064 File Offset: 0x000B8264
		public View GetActiveView()
		{
			int activeViewIndex = this.ActiveViewIndex;
			if (activeViewIndex >= this.Views.Count)
			{
				throw new Exception(SR.GetString("MultiView_ActiveViewIndex_out_of_range"));
			}
			if (activeViewIndex < 0)
			{
				return null;
			}
			View view = this.Views[activeViewIndex];
			if (!view.Active)
			{
				this.UpdateActiveView(activeViewIndex);
			}
			return view;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000BA0B9 File Offset: 0x000B82B9
		internal void IgnoreBubbleEvents()
		{
			this._ignoreBubbleEvents = true;
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000BA0C4 File Offset: 0x000B82C4
		private void UpdateActiveView(int activeViewIndex)
		{
			for (int i = 0; i < this.Views.Count; i++)
			{
				View view = this.Views[i];
				if (i == activeViewIndex)
				{
					view.Active = true;
					if (this.ShouldTriggerViewEvent)
					{
						view.OnActivate(EventArgs.Empty);
					}
				}
				else if (view.Active)
				{
					view.Active = false;
					if (this.ShouldTriggerViewEvent)
					{
						view.OnDeactivate(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000BA138 File Offset: 0x000B8338
		protected internal override void LoadControlState(object state)
		{
			Pair pair = state as Pair;
			if (pair != null)
			{
				base.LoadControlState(pair.First);
				this.ActiveViewIndex = (int)pair.Second;
			}
			this._controlStateApplied = true;
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000BA174 File Offset: 0x000B8374
		protected virtual void OnActiveViewChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MultiView._eventActiveViewChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000BA1A4 File Offset: 0x000B83A4
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (this._ignoreBubbleEvents)
			{
				return false;
			}
			if (e is CommandEventArgs)
			{
				CommandEventArgs commandEventArgs = (CommandEventArgs)e;
				string commandName = commandEventArgs.CommandName;
				if (commandName == MultiView.NextViewCommandName)
				{
					if (this.ActiveViewIndex < this.Views.Count - 1)
					{
						this.ActiveViewIndex++;
					}
					else
					{
						this.ActiveViewIndex = -1;
					}
					return true;
				}
				if (commandName == MultiView.PreviousViewCommandName)
				{
					if (this.ActiveViewIndex > -1)
					{
						this.ActiveViewIndex--;
					}
					return true;
				}
				if (commandName == MultiView.SwitchViewByIDCommandName)
				{
					View view = this.FindControl((string)commandEventArgs.CommandArgument) as View;
					if (view != null && view.Parent == this)
					{
						this.SetActiveView(view);
						return true;
					}
					throw new HttpException(SR.GetString("MultiView_invalid_view_id", new object[]
					{
						this.ID,
						(string)commandEventArgs.CommandArgument,
						MultiView.SwitchViewByIDCommandName
					}));
				}
				else if (commandName == MultiView.SwitchViewByIndexCommandName)
				{
					int activeViewIndex;
					try
					{
						activeViewIndex = int.Parse((string)commandEventArgs.CommandArgument, CultureInfo.InvariantCulture);
					}
					catch (FormatException)
					{
						throw new FormatException(SR.GetString("MultiView_invalid_view_index_format", new object[]
						{
							(string)commandEventArgs.CommandArgument,
							MultiView.SwitchViewByIndexCommandName
						}));
					}
					this.ActiveViewIndex = activeViewIndex;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000BA310 File Offset: 0x000B8510
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.RegisterRequiresControlState(this);
			if (this._cachedActiveViewIndex > -1)
			{
				this.ActiveViewIndex = this._cachedActiveViewIndex;
				this._cachedActiveViewIndex = -1;
				this.GetActiveView();
			}
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000BA348 File Offset: 0x000B8548
		protected internal override void RemovedControl(Control ctl)
		{
			if (((View)ctl).Active && this.ActiveViewIndex < this.Views.Count)
			{
				this.GetActiveView();
			}
			base.RemovedControl(ctl);
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000BA378 File Offset: 0x000B8578
		protected internal override void Render(HtmlTextWriter writer)
		{
			View activeView = this.GetActiveView();
			if (activeView != null)
			{
				activeView.RenderControl(writer);
			}
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000BA398 File Offset: 0x000B8598
		protected internal override object SaveControlState()
		{
			int activeViewIndex = this.ActiveViewIndex;
			object obj = base.SaveControlState();
			if (obj != null || activeViewIndex != -1)
			{
				return new Pair(obj, activeViewIndex);
			}
			return null;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000BA3C8 File Offset: 0x000B85C8
		public void SetActiveView(View view)
		{
			int num = this.Views.IndexOf(view);
			if (num < 0)
			{
				throw new HttpException(SR.GetString("MultiView_view_not_found", new object[]
				{
					(view == null) ? "null" : view.ID,
					this.ID
				}));
			}
			this.ActiveViewIndex = num;
		}

		// Token: 0x040022A9 RID: 8873
		private static readonly object _eventActiveViewChanged = new object();

		// Token: 0x040022AA RID: 8874
		private int _activeViewIndex = -1;

		// Token: 0x040022AB RID: 8875
		private int _cachedActiveViewIndex = -1;

		// Token: 0x040022AC RID: 8876
		private bool _ignoreBubbleEvents;

		// Token: 0x040022AD RID: 8877
		private bool _controlStateApplied;

		// Token: 0x040022AE RID: 8878
		public static readonly string NextViewCommandName = "NextView";

		// Token: 0x040022AF RID: 8879
		public static readonly string PreviousViewCommandName = "PrevView";

		// Token: 0x040022B0 RID: 8880
		public static readonly string SwitchViewByIDCommandName = "SwitchViewByID";

		// Token: 0x040022B1 RID: 8881
		public static readonly string SwitchViewByIndexCommandName = "SwitchViewByIndex";
	}
}
