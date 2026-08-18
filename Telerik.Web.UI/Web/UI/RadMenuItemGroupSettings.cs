using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020011BD RID: 4541
	[XmlRoot("Group")]
	public class RadMenuItemGroupSettings : ObjectWithState
	{
		// Token: 0x0600BAD2 RID: 47826 RVA: 0x00298FFC File Offset: 0x002971FC
		public RadMenuItemGroupSettings() : base(string.Empty, new StateBag())
		{
		}

		// Token: 0x0600BAD3 RID: 47827 RVA: 0x0029900E File Offset: 0x0029720E
		public RadMenuItemGroupSettings(StateBag viewState) : base("Group", viewState)
		{
		}

		// Token: 0x0600BAD4 RID: 47828 RVA: 0x0029901C File Offset: 0x0029721C
		public RadMenuItemGroupSettings(StateBag viewState, RadMenuItem owner) : base("Group", viewState)
		{
			this._owner = owner;
		}

		// Token: 0x17003C44 RID: 15428
		// (get) Token: 0x0600BAD5 RID: 47829 RVA: 0x00299031 File Offset: 0x00297231
		protected RadMenuItemGroupSettings DefaultSettings
		{
			get
			{
				if (this._owner != null && this._owner.Menu != null)
				{
					return this._owner.Menu.DefaultGroupSettings;
				}
				return null;
			}
		}

		// Token: 0x17003C45 RID: 15429
		// (get) Token: 0x0600BAD6 RID: 47830 RVA: 0x0029905A File Offset: 0x0029725A
		internal ItemFlow FlowResolved
		{
			get
			{
				if (this.DefaultSettings == null)
				{
					return this.Flow;
				}
				if (!this.IsFlowSet)
				{
					return this.DefaultSettings.Flow;
				}
				return this.Flow;
			}
		}

		// Token: 0x17003C46 RID: 15430
		// (get) Token: 0x0600BAD7 RID: 47831 RVA: 0x00299085 File Offset: 0x00297285
		internal bool IsFlowSet
		{
			get
			{
				return base.ViewState["Flow"] != null;
			}
		}

		// Token: 0x17003C47 RID: 15431
		// (get) Token: 0x0600BAD8 RID: 47832 RVA: 0x0029909D File Offset: 0x0029729D
		internal ExpandDirection ExpandDirectionResolved
		{
			get
			{
				if (this.DefaultSettings == null)
				{
					return this.ExpandDirection;
				}
				if (!this.IsExpandDirectionSet)
				{
					return this.DefaultSettings.ExpandDirection;
				}
				return this.ExpandDirection;
			}
		}

		// Token: 0x17003C48 RID: 15432
		// (get) Token: 0x0600BAD9 RID: 47833 RVA: 0x002990C8 File Offset: 0x002972C8
		internal bool IsExpandDirectionSet
		{
			get
			{
				return base.ViewState["ExpandDirection"] != null;
			}
		}

		// Token: 0x17003C49 RID: 15433
		// (get) Token: 0x0600BADA RID: 47834 RVA: 0x002990E0 File Offset: 0x002972E0
		internal int OffsetXResolved
		{
			get
			{
				if (this.DefaultSettings == null)
				{
					return this.OffsetX;
				}
				if (!this.IsOffsetXSet)
				{
					return this.DefaultSettings.OffsetX;
				}
				return this.OffsetX;
			}
		}

		// Token: 0x17003C4A RID: 15434
		// (get) Token: 0x0600BADB RID: 47835 RVA: 0x0029910B File Offset: 0x0029730B
		internal bool IsOffsetXSet
		{
			get
			{
				return base.ViewState["OffsetX"] != null;
			}
		}

		// Token: 0x17003C4B RID: 15435
		// (get) Token: 0x0600BADC RID: 47836 RVA: 0x00299123 File Offset: 0x00297323
		internal int OffsetYResolved
		{
			get
			{
				if (this.DefaultSettings == null)
				{
					return this.OffsetY;
				}
				if (!this.IsOffsetYSet)
				{
					return this.DefaultSettings.OffsetY;
				}
				return this.OffsetY;
			}
		}

		// Token: 0x17003C4C RID: 15436
		// (get) Token: 0x0600BADD RID: 47837 RVA: 0x0029914E File Offset: 0x0029734E
		internal bool IsOffsetYSet
		{
			get
			{
				return base.ViewState["OffsetY"] != null;
			}
		}

		// Token: 0x17003C4D RID: 15437
		// (get) Token: 0x0600BADE RID: 47838 RVA: 0x00299166 File Offset: 0x00297366
		internal Unit WidthResolved
		{
			get
			{
				if (this.DefaultSettings == null)
				{
					return this.Width;
				}
				if (!this.IsWidthSet)
				{
					return this.DefaultSettings.Width;
				}
				return this.Width;
			}
		}

		// Token: 0x17003C4E RID: 15438
		// (get) Token: 0x0600BADF RID: 47839 RVA: 0x00299191 File Offset: 0x00297391
		internal bool IsWidthSet
		{
			get
			{
				return base.ViewState["Width"] != null;
			}
		}

		// Token: 0x17003C4F RID: 15439
		// (get) Token: 0x0600BAE0 RID: 47840 RVA: 0x002991A9 File Offset: 0x002973A9
		internal Unit HeightResolved
		{
			get
			{
				if (this.DefaultSettings == null)
				{
					return this.Height;
				}
				if (!this.IsHeightSet)
				{
					return this.DefaultSettings.Height;
				}
				return this.Height;
			}
		}

		// Token: 0x17003C50 RID: 15440
		// (get) Token: 0x0600BAE1 RID: 47841 RVA: 0x002991D4 File Offset: 0x002973D4
		internal bool IsHeightSet
		{
			get
			{
				return base.ViewState["Height"] != null;
			}
		}

		// Token: 0x17003C51 RID: 15441
		// (get) Token: 0x0600BAE2 RID: 47842 RVA: 0x002991EC File Offset: 0x002973EC
		internal int RepeatColumnsResolved
		{
			get
			{
				if (this.DefaultSettings == null)
				{
					return this.RepeatColumns;
				}
				if (!this.IsRepeatColumnsSet)
				{
					return this.DefaultSettings.RepeatColumns;
				}
				return this.RepeatColumns;
			}
		}

		// Token: 0x17003C52 RID: 15442
		// (get) Token: 0x0600BAE3 RID: 47843 RVA: 0x00299217 File Offset: 0x00297417
		internal bool IsRepeatColumnsSet
		{
			get
			{
				return base.ViewState["RepeatColumns"] != null;
			}
		}

		// Token: 0x17003C53 RID: 15443
		// (get) Token: 0x0600BAE4 RID: 47844 RVA: 0x0029922F File Offset: 0x0029742F
		internal MenuRepeatDirection RepeatDirectionResolved
		{
			get
			{
				if (this.DefaultSettings == null)
				{
					return this.RepeatDirection;
				}
				if (!this.IsRepeatDirectionSet)
				{
					return this.DefaultSettings.RepeatDirection;
				}
				return this.RepeatDirection;
			}
		}

		// Token: 0x17003C54 RID: 15444
		// (get) Token: 0x0600BAE5 RID: 47845 RVA: 0x0029925A File Offset: 0x0029745A
		internal bool IsRepeatDirectionSet
		{
			get
			{
				return base.ViewState["RepeatDirection"] != null;
			}
		}

		// Token: 0x17003C55 RID: 15445
		// (get) Token: 0x0600BAE6 RID: 47846 RVA: 0x00299272 File Offset: 0x00297472
		// (set) Token: 0x0600BAE7 RID: 47847 RVA: 0x00299293 File Offset: 0x00297493
		[DefaultValue(ItemFlow.Vertical)]
		[Description("Orientation of child items")]
		public ItemFlow Flow
		{
			get
			{
				return (ItemFlow)(base.ViewState["Flow"] ?? ItemFlow.Vertical);
			}
			set
			{
				base.ViewState["Flow"] = value;
			}
		}

		// Token: 0x17003C56 RID: 15446
		// (get) Token: 0x0600BAE8 RID: 47848 RVA: 0x002992AB File Offset: 0x002974AB
		// (set) Token: 0x0600BAE9 RID: 47849 RVA: 0x002992CC File Offset: 0x002974CC
		[Description("Direction in which the child items expands")]
		[DefaultValue(ExpandDirection.Auto)]
		[NotifyParentProperty(true)]
		public ExpandDirection ExpandDirection
		{
			get
			{
				return (ExpandDirection)(base.ViewState["ExpandDirection"] ?? ExpandDirection.Auto);
			}
			set
			{
				base.ViewState["ExpandDirection"] = value;
			}
		}

		// Token: 0x17003C57 RID: 15447
		// (get) Token: 0x0600BAEA RID: 47850 RVA: 0x002992E4 File Offset: 0x002974E4
		// (set) Token: 0x0600BAEB RID: 47851 RVA: 0x00299305 File Offset: 0x00297505
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Description("Offset along x-axis from child items normal expand positions")]
		public int OffsetX
		{
			get
			{
				return (int)(base.ViewState["OffsetX"] ?? 0);
			}
			set
			{
				base.ViewState["OffsetX"] = value;
			}
		}

		// Token: 0x17003C58 RID: 15448
		// (get) Token: 0x0600BAEC RID: 47852 RVA: 0x0029931D File Offset: 0x0029751D
		// (set) Token: 0x0600BAED RID: 47853 RVA: 0x0029933E File Offset: 0x0029753E
		[DefaultValue(0)]
		[Description("Offset along x-axis from child items normal expand positions")]
		[NotifyParentProperty(true)]
		public int OffsetY
		{
			get
			{
				return (int)(base.ViewState["OffsetY"] ?? 0);
			}
			set
			{
				base.ViewState["OffsetY"] = value;
			}
		}

		// Token: 0x17003C59 RID: 15449
		// (get) Token: 0x0600BAEE RID: 47854 RVA: 0x00299356 File Offset: 0x00297556
		// (set) Token: 0x0600BAEF RID: 47855 RVA: 0x0029937B File Offset: 0x0029757B
		[DefaultValue(typeof(Unit), "")]
		[Description("Width of child items")]
		[NotifyParentProperty(true)]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x17003C5A RID: 15450
		// (get) Token: 0x0600BAF0 RID: 47856 RVA: 0x002993B0 File Offset: 0x002975B0
		// (set) Token: 0x0600BAF1 RID: 47857 RVA: 0x002993D5 File Offset: 0x002975D5
		[DefaultValue(typeof(Unit), "")]
		[Description("Height ot child items")]
		[NotifyParentProperty(true)]
		public Unit Height
		{
			get
			{
				return (Unit)(base.ViewState["Height"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x17003C5B RID: 15451
		// (get) Token: 0x0600BAF2 RID: 47858 RVA: 0x0029940A File Offset: 0x0029760A
		// (set) Token: 0x0600BAF3 RID: 47859 RVA: 0x0029942B File Offset: 0x0029762B
		[NotifyParentProperty(true)]
		[Description("The number of columns to display in this item group")]
		[DefaultValue(1)]
		public int RepeatColumns
		{
			get
			{
				return (int)(base.ViewState["RepeatColumns"] ?? 1);
			}
			set
			{
				base.ViewState["RepeatColumns"] = Math.Max(value, 1);
			}
		}

		// Token: 0x17003C5C RID: 15452
		// (get) Token: 0x0600BAF4 RID: 47860 RVA: 0x00299449 File Offset: 0x00297649
		// (set) Token: 0x0600BAF5 RID: 47861 RVA: 0x0029946A File Offset: 0x0029766A
		[NotifyParentProperty(true)]
		[Description("Whether the columns are repeated vertically or horizontally")]
		[DefaultValue(MenuRepeatDirection.Vertical)]
		public MenuRepeatDirection RepeatDirection
		{
			get
			{
				return (MenuRepeatDirection)(base.ViewState["RepeatDirection"] ?? MenuRepeatDirection.Vertical);
			}
			set
			{
				base.ViewState["RepeatDirection"] = value;
			}
		}

		// Token: 0x0600BAF6 RID: 47862 RVA: 0x00299484 File Offset: 0x00297684
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.ExpandDirection,
				",",
				this.Flow,
				",",
				this.OffsetX,
				",",
				this.OffsetY,
				",",
				this.Width,
				",",
				this.Height
			});
		}

		// Token: 0x0600BAF7 RID: 47863 RVA: 0x0029951D File Offset: 0x0029771D
		internal bool ShouldSerialize()
		{
			return this.ToString() != "Auto,Vertical,0,0,,";
		}

		// Token: 0x0600BAF8 RID: 47864 RVA: 0x0029952F File Offset: 0x0029772F
		internal void SerializeTo(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
		}

		// Token: 0x0400314E RID: 12622
		private readonly RadMenuItem _owner;
	}
}
