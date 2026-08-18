using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003C6 RID: 966
	public sealed class DataGridPagerStyle : TableItemStyle
	{
		// Token: 0x06002E80 RID: 11904 RVA: 0x00098363 File Offset: 0x00096563
		internal DataGridPagerStyle(DataGrid owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x00098374 File Offset: 0x00096574
		internal bool IsPagerOnBottom
		{
			get
			{
				PagerPosition position = this.Position;
				return position == PagerPosition.Bottom || position == PagerPosition.TopAndBottom;
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x00098394 File Offset: 0x00096594
		internal bool IsPagerOnTop
		{
			get
			{
				PagerPosition position = this.Position;
				return position == PagerPosition.Top || position == PagerPosition.TopAndBottom;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000983B2 File Offset: 0x000965B2
		// (set) Token: 0x06002E84 RID: 11908 RVA: 0x000983D8 File Offset: 0x000965D8
		[WebCategory("Appearance")]
		[DefaultValue(PagerMode.NextPrev)]
		[NotifyParentProperty(true)]
		[WebSysDescription("DataGridPagerStyle_Mode")]
		public PagerMode Mode
		{
			get
			{
				if (base.IsSet(524288))
				{
					return (PagerMode)base.ViewState["Mode"];
				}
				return PagerMode.NextPrev;
			}
			set
			{
				if (value < PagerMode.NextPrev || value > PagerMode.NumericPages)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Mode"] = value;
				this.SetBit(524288);
				this.owner.OnPagerChanged();
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x00098424 File Offset: 0x00096624
		// (set) Token: 0x06002E86 RID: 11910 RVA: 0x0009844E File Offset: 0x0009664E
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("&gt;")]
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_NextPageText")]
		public string NextPageText
		{
			get
			{
				if (base.IsSet(1048576))
				{
					return (string)base.ViewState["NextPageText"];
				}
				return "&gt;";
			}
			set
			{
				base.ViewState["NextPageText"] = value;
				this.SetBit(1048576);
				this.owner.OnPagerChanged();
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x00098477 File Offset: 0x00096677
		// (set) Token: 0x06002E88 RID: 11912 RVA: 0x0009849E File Offset: 0x0009669E
		[WebCategory("Behavior")]
		[DefaultValue(10)]
		[NotifyParentProperty(true)]
		[WebSysDescription("DataGridPagerStyle_PageButtonCount")]
		public int PageButtonCount
		{
			get
			{
				if (base.IsSet(4194304))
				{
					return (int)base.ViewState["PageButtonCount"];
				}
				return 10;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["PageButtonCount"] = value;
				this.SetBit(4194304);
				this.owner.OnPagerChanged();
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x000984DB File Offset: 0x000966DB
		// (set) Token: 0x06002E8A RID: 11914 RVA: 0x00098504 File Offset: 0x00096704
		[WebCategory("Layout")]
		[DefaultValue(PagerPosition.Bottom)]
		[NotifyParentProperty(true)]
		[WebSysDescription("DataGridPagerStyle_Position")]
		public PagerPosition Position
		{
			get
			{
				if (base.IsSet(8388608))
				{
					return (PagerPosition)base.ViewState["Position"];
				}
				return PagerPosition.Bottom;
			}
			set
			{
				if (value < PagerPosition.Bottom || value > PagerPosition.TopAndBottom)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Position"] = value;
				this.SetBit(8388608);
				this.owner.OnPagerChanged();
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x00098550 File Offset: 0x00096750
		// (set) Token: 0x06002E8C RID: 11916 RVA: 0x0009857A File Offset: 0x0009677A
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("&lt;")]
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_PreviousPageText")]
		public string PrevPageText
		{
			get
			{
				if (base.IsSet(2097152))
				{
					return (string)base.ViewState["PrevPageText"];
				}
				return "&lt;";
			}
			set
			{
				base.ViewState["PrevPageText"] = value;
				this.SetBit(2097152);
				this.owner.OnPagerChanged();
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x000985A3 File Offset: 0x000967A3
		// (set) Token: 0x06002E8E RID: 11918 RVA: 0x000985C9 File Offset: 0x000967C9
		[WebCategory("Appearance")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[WebSysDescription("DataGridPagerStyle_Visible")]
		public bool Visible
		{
			get
			{
				return !base.IsSet(16777216) || (bool)base.ViewState["PagerVisible"];
			}
			set
			{
				base.ViewState["PagerVisible"] = value;
				this.SetBit(16777216);
				this.owner.OnPagerChanged();
			}
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000985F8 File Offset: 0x000967F8
		public override void CopyFrom(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				base.CopyFrom(s);
				if (s is DataGridPagerStyle)
				{
					DataGridPagerStyle dataGridPagerStyle = (DataGridPagerStyle)s;
					if (dataGridPagerStyle.IsSet(524288))
					{
						this.Mode = dataGridPagerStyle.Mode;
					}
					if (dataGridPagerStyle.IsSet(1048576))
					{
						this.NextPageText = dataGridPagerStyle.NextPageText;
					}
					if (dataGridPagerStyle.IsSet(2097152))
					{
						this.PrevPageText = dataGridPagerStyle.PrevPageText;
					}
					if (dataGridPagerStyle.IsSet(4194304))
					{
						this.PageButtonCount = dataGridPagerStyle.PageButtonCount;
					}
					if (dataGridPagerStyle.IsSet(8388608))
					{
						this.Position = dataGridPagerStyle.Position;
					}
					if (dataGridPagerStyle.IsSet(16777216))
					{
						this.Visible = dataGridPagerStyle.Visible;
					}
				}
			}
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000986C8 File Offset: 0x000968C8
		public override void MergeWith(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				if (this.IsEmpty)
				{
					this.CopyFrom(s);
					return;
				}
				base.MergeWith(s);
				if (s is DataGridPagerStyle)
				{
					DataGridPagerStyle dataGridPagerStyle = (DataGridPagerStyle)s;
					if (dataGridPagerStyle.IsSet(524288) && !base.IsSet(524288))
					{
						this.Mode = dataGridPagerStyle.Mode;
					}
					if (dataGridPagerStyle.IsSet(1048576) && !base.IsSet(1048576))
					{
						this.NextPageText = dataGridPagerStyle.NextPageText;
					}
					if (dataGridPagerStyle.IsSet(2097152) && !base.IsSet(2097152))
					{
						this.PrevPageText = dataGridPagerStyle.PrevPageText;
					}
					if (dataGridPagerStyle.IsSet(4194304) && !base.IsSet(4194304))
					{
						this.PageButtonCount = dataGridPagerStyle.PageButtonCount;
					}
					if (dataGridPagerStyle.IsSet(8388608) && !base.IsSet(8388608))
					{
						this.Position = dataGridPagerStyle.Position;
					}
					if (dataGridPagerStyle.IsSet(16777216) && !base.IsSet(16777216))
					{
						this.Visible = dataGridPagerStyle.Visible;
					}
				}
			}
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000987F4 File Offset: 0x000969F4
		public override void Reset()
		{
			if (base.IsSet(524288))
			{
				base.ViewState.Remove("Mode");
			}
			if (base.IsSet(1048576))
			{
				base.ViewState.Remove("NextPageText");
			}
			if (base.IsSet(2097152))
			{
				base.ViewState.Remove("PrevPageText");
			}
			if (base.IsSet(4194304))
			{
				base.ViewState.Remove("PageButtonCount");
			}
			if (base.IsSet(8388608))
			{
				base.ViewState.Remove("Position");
			}
			if (base.IsSet(16777216))
			{
				base.ViewState.Remove("PagerVisible");
			}
			base.Reset();
		}

		// Token: 0x04001FF5 RID: 8181
		private const int PROP_MODE = 524288;

		// Token: 0x04001FF6 RID: 8182
		private const int PROP_NEXTPAGETEXT = 1048576;

		// Token: 0x04001FF7 RID: 8183
		private const int PROP_PREVPAGETEXT = 2097152;

		// Token: 0x04001FF8 RID: 8184
		private const int PROP_PAGEBUTTONCOUNT = 4194304;

		// Token: 0x04001FF9 RID: 8185
		private const int PROP_POSITION = 8388608;

		// Token: 0x04001FFA RID: 8186
		private const int PROP_VISIBLE = 16777216;

		// Token: 0x04001FFB RID: 8187
		private DataGrid owner;
	}
}
