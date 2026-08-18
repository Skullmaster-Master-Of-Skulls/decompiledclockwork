using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001044 RID: 4164
	public class DockPinUnpinCommand : DockToggleCommand
	{
		// Token: 0x0600A3B7 RID: 41911 RVA: 0x00246B0E File Offset: 0x00244D0E
		public DockPinUnpinCommand() : base("Telerik.Web.UI.DockPinUnpinCommand", "rdUnpin", "rdPin", "PinUnpin", null, null, false)
		{
		}

		// Token: 0x170033A9 RID: 13225
		// (get) Token: 0x0600A3B8 RID: 41912 RVA: 0x00246B2D File Offset: 0x00244D2D
		// (set) Token: 0x0600A3B9 RID: 41913 RVA: 0x00246B3F File Offset: 0x00244D3F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ScriptIgnore]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override DockToggleCommandState State
		{
			get
			{
				if (!base.RadDock.Pinned)
				{
					return DockToggleCommandState.Primary;
				}
				return DockToggleCommandState.Alternate;
			}
			set
			{
				throw new InvalidOperationException("The State property of DockPinUnpinCommand is read only. Use the Pinned property of the parent RadDock instead.");
			}
		}

		// Token: 0x170033AA RID: 13226
		// (get) Token: 0x0600A3BA RID: 41914 RVA: 0x00246B4B File Offset: 0x00244D4B
		// (set) Token: 0x0600A3BB RID: 41915 RVA: 0x00246B67 File Offset: 0x00244D67
		[DefaultValue("Pin")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				if (base.Text != null)
				{
					return base.Text;
				}
				return base.RadDock.PinText;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x170033AB RID: 13227
		// (get) Token: 0x0600A3BC RID: 41916 RVA: 0x00246B70 File Offset: 0x00244D70
		// (set) Token: 0x0600A3BD RID: 41917 RVA: 0x00246B8C File Offset: 0x00244D8C
		[DefaultValue("Unpin")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string AlternateText
		{
			get
			{
				if (base.AlternateText != null)
				{
					return base.AlternateText;
				}
				return base.RadDock.UnpinText;
			}
			set
			{
				base.AlternateText = value;
			}
		}

		// Token: 0x170033AC RID: 13228
		// (get) Token: 0x0600A3BE RID: 41918 RVA: 0x00246B95 File Offset: 0x00244D95
		// (set) Token: 0x0600A3BF RID: 41919 RVA: 0x00246B9D File Offset: 0x00244D9D
		[Browsable(false)]
		[DefaultValue("rdUnpin")]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x170033AD RID: 13229
		// (get) Token: 0x0600A3C0 RID: 41920 RVA: 0x00246BA6 File Offset: 0x00244DA6
		// (set) Token: 0x0600A3C1 RID: 41921 RVA: 0x00246BAE File Offset: 0x00244DAE
		[DefaultValue("rdPin")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x170033AE RID: 13230
		// (get) Token: 0x0600A3C2 RID: 41922 RVA: 0x00246BB7 File Offset: 0x00244DB7
		// (set) Token: 0x0600A3C3 RID: 41923 RVA: 0x00246BBF File Offset: 0x00244DBF
		[Browsable(false)]
		[DefaultValue("PinUnpin")]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x04002D95 RID: 11669
		internal const string PinUnpinCommandName = "PinUnpin";
	}
}
