using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020011A6 RID: 4518
	[ToolboxItem(false)]
	public class GridContextMenu : RadContextMenu
	{
		// Token: 0x17003BF6 RID: 15350
		// (get) Token: 0x0600B9A7 RID: 47527 RVA: 0x002926C4 File Offset: 0x002908C4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public new RadMenuItemCollection Items
		{
			get
			{
				return base.Items;
			}
		}

		// Token: 0x17003BF7 RID: 15351
		// (get) Token: 0x0600B9A8 RID: 47528 RVA: 0x002926CC File Offset: 0x002908CC
		// (set) Token: 0x0600B9A9 RID: 47529 RVA: 0x002926D4 File Offset: 0x002908D4
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x17003BF8 RID: 15352
		// (get) Token: 0x0600B9AA RID: 47530 RVA: 0x002926DD File Offset: 0x002908DD
		// (set) Token: 0x0600B9AB RID: 47531 RVA: 0x002926E5 File Offset: 0x002908E5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new bool ClickToOpen
		{
			get
			{
				return base.ClickToOpen;
			}
			set
			{
				base.ClickToOpen = value;
			}
		}

		// Token: 0x17003BF9 RID: 15353
		// (get) Token: 0x0600B9AC RID: 47532 RVA: 0x002926EE File Offset: 0x002908EE
		// (set) Token: 0x0600B9AD RID: 47533 RVA: 0x002926F6 File Offset: 0x002908F6
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		// Token: 0x17003BFA RID: 15354
		// (get) Token: 0x0600B9AE RID: 47534 RVA: 0x002926FF File Offset: 0x002908FF
		// (set) Token: 0x0600B9AF RID: 47535 RVA: 0x00292707 File Offset: 0x00290907
		[DefaultValue(false)]
		public override bool EnableImageSprites
		{
			get
			{
				return base.EnableImageSprites;
			}
			set
			{
				base.EnableImageSprites = value;
			}
		}
	}
}
