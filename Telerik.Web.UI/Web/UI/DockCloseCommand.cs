using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000FAB RID: 4011
	public class DockCloseCommand : DockCommand
	{
		// Token: 0x06009A03 RID: 39427 RVA: 0x00225A54 File Offset: 0x00223C54
		public DockCloseCommand() : base("Telerik.Web.UI.DockCloseCommand", "rdClose", "Close", null, false)
		{
		}

		// Token: 0x170030BA RID: 12474
		// (get) Token: 0x06009A04 RID: 39428 RVA: 0x00225A6D File Offset: 0x00223C6D
		// (set) Token: 0x06009A05 RID: 39429 RVA: 0x00225A89 File Offset: 0x00223C89
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DefaultValue("Close")]
		public override string Text
		{
			get
			{
				if (base.Text != null)
				{
					return base.Text;
				}
				return base.RadDock.CloseText;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x170030BB RID: 12475
		// (get) Token: 0x06009A06 RID: 39430 RVA: 0x00225A92 File Offset: 0x00223C92
		// (set) Token: 0x06009A07 RID: 39431 RVA: 0x00225A9A File Offset: 0x00223C9A
		[DefaultValue("rdClose")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
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

		// Token: 0x170030BC RID: 12476
		// (get) Token: 0x06009A08 RID: 39432 RVA: 0x00225AA3 File Offset: 0x00223CA3
		// (set) Token: 0x06009A09 RID: 39433 RVA: 0x00225AAB File Offset: 0x00223CAB
		[Browsable(false)]
		[DefaultValue("Close")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
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

		// Token: 0x04002BB7 RID: 11191
		internal const string CloseCommandName = "Close";
	}
}
