using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200052E RID: 1326
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataControlFieldCell : TableCell
	{
		// Token: 0x06004133 RID: 16691 RVA: 0x0010ED1F File Offset: 0x0010DD1F
		public DataControlFieldCell(DataControlField containingField)
		{
			this._containingField = containingField;
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x0010ED2E File Offset: 0x0010DD2E
		protected DataControlFieldCell(HtmlTextWriterTag tagKey, DataControlField containingField) : base(tagKey)
		{
			this._containingField = containingField;
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06004135 RID: 16693 RVA: 0x0010ED3E File Offset: 0x0010DD3E
		public DataControlField ContainingField
		{
			get
			{
				return this._containingField;
			}
		}

		// Token: 0x040028AC RID: 10412
		private DataControlField _containingField;
	}
}
