using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005EE RID: 1518
	[ControlBuilder(typeof(MultiViewControlBuilder))]
	[ToolboxData("<{0}:MultiView runat=\"server\"></{0}:MultiView>")]
	[Designer("System.Web.UI.Design.WebControls.MultiViewDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("ActiveViewChanged")]
	[ParseChildren(typeof(View))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class MultiView : Control
	{
		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06004B17 RID: 19223 RVA: 0x001327D1 File Offset: 0x001317D1
		// (set) Token: 0x06004B18 RID: 19224 RVA: 0x001327EC File Offset: 0x001317EC
		[WebSysDescription("MultiView_ActiveView")]
		[WebCategory("Behavior")]
		[DefaultValue(-1)]
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

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x0013293F File Offset: 0x0013193F
		// (set) Token: 0x06004B1A RID: 19226 RVA: 0x00132947 File Offset: 0x00131947
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

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06004B1B RID: 19227 RVA: 0x00132950 File Offset: 0x00131950
		private bool ShouldTriggerViewEvent
		{
			get
			{
				return this._controlStateApplied || (this.Page != null && !this.Page.IsPostBack);
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06004B1C RID: 19228 RVA: 0x00132974 File Offset: 0x00131974
		[WebSysDescription("MultiView_Views")]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual ViewCollection Views
		{
			get
			{
				return (ViewCollection)this.Controls;
			}
		}

		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06004B1D RID: 19229 RVA: 0x00132981 File Offset: 0x00131981
		// (remove) Token: 0x06004B1E RID: 19230 RVA: 0x00132994 File Offset: 0x00131994
		[WebSysDescription("MultiView_ActiveViewChanged")]
		[WebCategory("Action")]
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

		// Token: 0x06004B1F RID: 19231 RVA: 0x001329A8 File Offset: 0x001319A8
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

		// Token: 0x06004B20 RID: 19232 RVA: 0x001329FD File Offset: 0x001319FD
		protected override ControlCollection CreateControlCollection()
		{
			return new ViewCollection(this);
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x00132A08 File Offset: 0x00131A08
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

		// Token: 0x06004B22 RID: 19234 RVA: 0x00132A5D File Offset: 0x00131A5D
		internal void IgnoreBubbleEvents()
		{
			this._ignoreBubbleEvents = true;
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x00132A68 File Offset: 0x00131A68
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

		// Token: 0x06004B24 RID: 19236 RVA: 0x00132ADC File Offset: 0x00131ADC
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

		// Token: 0x06004B25 RID: 19237 RVA: 0x00132B18 File Offset: 0x00131B18
		protected virtual void OnActiveViewChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MultiView._eventActiveViewChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x00132B48 File Offset: 0x00131B48
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

		// Token: 0x06004B27 RID: 19239 RVA: 0x00132CC0 File Offset: 0x00131CC0
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

		// Token: 0x06004B28 RID: 19240 RVA: 0x00132CF8 File Offset: 0x00131CF8
		protected internal override void RemovedControl(Control ctl)
		{
			if (((View)ctl).Active && this.ActiveViewIndex < this.Views.Count)
			{
				this.GetActiveView();
			}
			base.RemovedControl(ctl);
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x00132D28 File Offset: 0x00131D28
		protected internal override void Render(HtmlTextWriter writer)
		{
			View activeView = this.GetActiveView();
			if (activeView != null)
			{
				activeView.RenderControl(writer);
			}
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x00132D48 File Offset: 0x00131D48
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

		// Token: 0x06004B2B RID: 19243 RVA: 0x00132D78 File Offset: 0x00131D78
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

		// Token: 0x04002B9A RID: 11162
		private static readonly object _eventActiveViewChanged = new object();

		// Token: 0x04002B9B RID: 11163
		private int _activeViewIndex = -1;

		// Token: 0x04002B9C RID: 11164
		private int _cachedActiveViewIndex = -1;

		// Token: 0x04002B9D RID: 11165
		private bool _ignoreBubbleEvents;

		// Token: 0x04002B9E RID: 11166
		private bool _controlStateApplied;

		// Token: 0x04002B9F RID: 11167
		public static readonly string NextViewCommandName = "NextView";

		// Token: 0x04002BA0 RID: 11168
		public static readonly string PreviousViewCommandName = "PrevView";

		// Token: 0x04002BA1 RID: 11169
		public static readonly string SwitchViewByIDCommandName = "SwitchViewByID";

		// Token: 0x04002BA2 RID: 11170
		public static readonly string SwitchViewByIndexCommandName = "SwitchViewByIndex";
	}
}
