using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000FB0 RID: 4016
	public class DockExpandCollapseCommand : DockToggleCommand
	{
		// Token: 0x06009A1E RID: 39454 RVA: 0x00225C1B File Offset: 0x00223E1B
		public DockExpandCollapseCommand() : base("Telerik.Web.UI.DockExpandCollapseCommand", "rdCollapse", "rdExpand", "ExpandCollapse", null, null, false)
		{
		}

		// Token: 0x170030C1 RID: 12481
		// (get) Token: 0x06009A1F RID: 39455 RVA: 0x00225C3A File Offset: 0x00223E3A
		// (set) Token: 0x06009A20 RID: 39456 RVA: 0x00225C4C File Offset: 0x00223E4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[ScriptIgnore]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DockToggleCommandState State
		{
			get
			{
				if (!base.RadDock.Collapsed)
				{
					return DockToggleCommandState.Primary;
				}
				return DockToggleCommandState.Alternate;
			}
			set
			{
				throw new InvalidOperationException("The State property of DockExpandCollapseCommand is read only. Use the Collapsed property of the parent RadDock instead.");
			}
		}

		// Token: 0x170030C2 RID: 12482
		// (get) Token: 0x06009A21 RID: 39457 RVA: 0x00225C58 File Offset: 0x00223E58
		// (set) Token: 0x06009A22 RID: 39458 RVA: 0x00225C74 File Offset: 0x00223E74
		[DefaultValue("Collapse")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override string Text
		{
			get
			{
				if (base.Text != null)
				{
					return base.Text;
				}
				return base.RadDock.CollapseText;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x170030C3 RID: 12483
		// (get) Token: 0x06009A23 RID: 39459 RVA: 0x00225C7D File Offset: 0x00223E7D
		// (set) Token: 0x06009A24 RID: 39460 RVA: 0x00225C99 File Offset: 0x00223E99
		[Browsable(false)]
		[DefaultValue("Expand")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override string AlternateText
		{
			get
			{
				if (base.AlternateText != null)
				{
					return base.AlternateText;
				}
				return base.RadDock.ExpandText;
			}
			set
			{
				base.AlternateText = value;
			}
		}

		// Token: 0x170030C4 RID: 12484
		// (get) Token: 0x06009A25 RID: 39461 RVA: 0x00225CA2 File Offset: 0x00223EA2
		// (set) Token: 0x06009A26 RID: 39462 RVA: 0x00225CAA File Offset: 0x00223EAA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DefaultValue("rdCollapse")]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x170030C5 RID: 12485
		// (get) Token: 0x06009A27 RID: 39463 RVA: 0x00225CB3 File Offset: 0x00223EB3
		// (set) Token: 0x06009A28 RID: 39464 RVA: 0x00225CBB File Offset: 0x00223EBB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue("rdExpand")]
		public override string AlternateCssClass
		{
			get
			{
				return base.AlternateCssClass;
			}
			set
			{
				base.AlternateCssClass = value;
			}
		}

		// Token: 0x170030C6 RID: 12486
		// (get) Token: 0x06009A29 RID: 39465 RVA: 0x00225CC4 File Offset: 0x00223EC4
		// (set) Token: 0x06009A2A RID: 39466 RVA: 0x00225CCC File Offset: 0x00223ECC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue("ExpandCollapse")]
		[Browsable(false)]
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x04002BBD RID: 11197
		internal const string ExpandCollapseCommandName = "ExpandCollapse";
	}
}
