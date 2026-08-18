using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005B7 RID: 1463
	public class Tooltip : StateManager, IDefaultCheck
	{
		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000AD212 File Offset: 0x000AB412
		// (set) Token: 0x06003430 RID: 13360 RVA: 0x000AD233 File Offset: 0x000AB433
		[DefaultValue(true)]
		public bool AutoHide
		{
			get
			{
				return (bool)(base.ViewState["AutoHide"] ?? true);
			}
			set
			{
				base.ViewState["AutoHide"] = value;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x000AD24B File Offset: 0x000AB44B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Animation AnimationSettings
		{
			get
			{
				if (this._animation == null)
				{
					this._animation = new Animation();
				}
				return this._animation;
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x000AD266 File Offset: 0x000AB466
		// (set) Token: 0x06003433 RID: 13363 RVA: 0x000AD286 File Offset: 0x000AB486
		[DefaultValue("")]
		public string Content
		{
			get
			{
				return (string)(base.ViewState["Content"] ?? "");
			}
			set
			{
				base.ViewState["Content"] = value;
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000AD299 File Offset: 0x000AB499
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Content ContentSettings
		{
			get
			{
				if (this._content == null)
				{
					this._content = new Content();
				}
				return this._content;
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000AD2B4 File Offset: 0x000AB4B4
		// (set) Token: 0x06003436 RID: 13366 RVA: 0x000AD2D4 File Offset: 0x000AB4D4
		[DefaultValue("")]
		public string Template
		{
			get
			{
				return (string)(base.ViewState["Template"] ?? "");
			}
			set
			{
				base.ViewState["Template"] = value;
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000AD2E7 File Offset: 0x000AB4E7
		// (set) Token: 0x06003438 RID: 13368 RVA: 0x000AD308 File Offset: 0x000AB508
		[DefaultValue(true)]
		public bool Callout
		{
			get
			{
				return (bool)(base.ViewState["Callout"] ?? true);
			}
			set
			{
				base.ViewState["Callout"] = value;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x000AD320 File Offset: 0x000AB520
		// (set) Token: 0x0600343A RID: 13370 RVA: 0x000AD341 File Offset: 0x000AB541
		[DefaultValue(false)]
		public bool Iframe
		{
			get
			{
				return (bool)(base.ViewState["Iframe"] ?? false);
			}
			set
			{
				base.ViewState["Iframe"] = value;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000AD359 File Offset: 0x000AB559
		// (set) Token: 0x0600343C RID: 13372 RVA: 0x000AD382 File Offset: 0x000AB582
		[DefaultValue(0.0)]
		public double Height
		{
			get
			{
				return (double)(base.ViewState["Height"] ?? 0.0);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000AD39A File Offset: 0x000AB59A
		// (set) Token: 0x0600343E RID: 13374 RVA: 0x000AD3C3 File Offset: 0x000AB5C3
		[DefaultValue(0.0)]
		public double Width
		{
			get
			{
				return (double)(base.ViewState["Width"] ?? 0.0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x000AD3DB File Offset: 0x000AB5DB
		// (set) Token: 0x06003440 RID: 13376 RVA: 0x000AD3FC File Offset: 0x000AB5FC
		[DefaultValue(TooltipPosition.Top)]
		public TooltipPosition Position
		{
			get
			{
				return (TooltipPosition)(base.ViewState["Position"] ?? TooltipPosition.Top);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06003441 RID: 13377 RVA: 0x000AD414 File Offset: 0x000AB614
		// (set) Token: 0x06003442 RID: 13378 RVA: 0x000AD43D File Offset: 0x000AB63D
		[DefaultValue(100.0)]
		public double ShowAfter
		{
			get
			{
				return (double)(base.ViewState["ShowAfter"] ?? 100.0);
			}
			set
			{
				base.ViewState["ShowAfter"] = value;
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x000AD455 File Offset: 0x000AB655
		// (set) Token: 0x06003444 RID: 13380 RVA: 0x000AD475 File Offset: 0x000AB675
		[DefaultValue("mouseenter")]
		public string ShowOn
		{
			get
			{
				return (string)(base.ViewState["ShowOn"] ?? "mouseenter");
			}
			set
			{
				base.ViewState["ShowOn"] = value;
			}
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000AD488 File Offset: 0x000AB688
		internal override void SetDirty()
		{
			base.SetDirty();
			this.AnimationSettings.SetDirty();
			this.ContentSettings.SetDirty();
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000AD4A8 File Offset: 0x000AB6A8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.AnimationSettings).LoadViewState(array[num++]);
			((IStateManager)this.ContentSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000AD4F0 File Offset: 0x000AB6F0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.AnimationSettings).SaveViewState(),
				((IStateManager)this.ContentSettings).SaveViewState()
			};
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000AD52C File Offset: 0x000AB72C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.AnimationSettings).TrackViewState();
			((IStateManager)this.ContentSettings).TrackViewState();
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000AD54C File Offset: 0x000AB74C
		public bool IsDefault
		{
			get
			{
				return this.AutoHide && this.AnimationSettings.IsDefault && this.Content == "" && this.ContentSettings.IsDefault && this.Template == "" && this.Callout && !this.Iframe && this.Height == 0.0 && this.Width == 0.0 && this.Position == TooltipPosition.Top && this.ShowAfter == 100.0 && this.ShowOn == "mouseenter";
			}
		}

		// Token: 0x04000E2E RID: 3630
		private Animation _animation;

		// Token: 0x04000E2F RID: 3631
		private Content _content;
	}
}
