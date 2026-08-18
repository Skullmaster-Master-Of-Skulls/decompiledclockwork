using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000348 RID: 840
	internal class TextBoxActionList : DesignerActionList
	{
		// Token: 0x06002146 RID: 8518 RVA: 0x000CB4F7 File Offset: 0x000C96F7
		public TextBoxActionList(TextBoxDesigner designer) : base(designer.Component)
		{
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x000CB505 File Offset: 0x000C9705
		// (set) Token: 0x06002148 RID: 8520 RVA: 0x000CB517 File Offset: 0x000C9717
		public bool Multiline
		{
			get
			{
				return ((TextBox)base.Component).Multiline;
			}
			set
			{
				TypeDescriptor.GetProperties(base.Component)["Multiline"].SetValue(base.Component, value);
			}
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000CB540 File Offset: 0x000C9740
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			return new DesignerActionItemCollection
			{
				new DesignerActionPropertyItem("Multiline", SR.GetString("MultiLineDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("MultiLineDescription"))
			};
		}
	}
}
