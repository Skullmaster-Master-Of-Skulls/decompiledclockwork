using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001228 RID: 4648
	public class TreeListPdfStyle : TableItemStyle
	{
		// Token: 0x17003DD8 RID: 15832
		// (get) Token: 0x0600BFC5 RID: 49093 RVA: 0x002A9154 File Offset: 0x002A7354
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool IsDefault
		{
			get
			{
				return this.IsEmpty;
			}
		}

		// Token: 0x17003DD9 RID: 15833
		// (get) Token: 0x0600BFC6 RID: 49094 RVA: 0x002A915C File Offset: 0x002A735C
		// (set) Token: 0x0600BFC7 RID: 49095 RVA: 0x002A9164 File Offset: 0x002A7364
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Wrap { get; set; }

		// Token: 0x17003DDA RID: 15834
		// (get) Token: 0x0600BFC8 RID: 49096 RVA: 0x002A916D File Offset: 0x002A736D
		// (set) Token: 0x0600BFC9 RID: 49097 RVA: 0x002A9175 File Offset: 0x002A7375
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override VerticalAlign VerticalAlign { get; set; }

		// Token: 0x17003DDB RID: 15835
		// (get) Token: 0x0600BFCA RID: 49098 RVA: 0x002A917E File Offset: 0x002A737E
		// (set) Token: 0x0600BFCB RID: 49099 RVA: 0x002A91AD File Offset: 0x002A73AD
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Description("Determines the line height")]
		public virtual Unit LineHeight
		{
			get
			{
				if (base.ViewState["LineHeight"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["LineHeight"];
			}
			set
			{
				base.ViewState["LineHeight"] = value;
			}
		}

		// Token: 0x0600BFCC RID: 49100 RVA: 0x002A91C8 File Offset: 0x002A73C8
		public override void CopyFrom(Style s)
		{
			TreeListPdfStyle treeListPdfStyle = s as TreeListPdfStyle;
			if (treeListPdfStyle != null && !treeListPdfStyle.LineHeight.IsEmpty)
			{
				this.LineHeight = treeListPdfStyle.LineHeight;
			}
			base.CopyFrom(s);
		}
	}
}
