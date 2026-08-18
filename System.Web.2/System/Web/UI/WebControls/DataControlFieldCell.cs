using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003B4 RID: 948
	public class DataControlFieldCell : TableCell
	{
		// Token: 0x06002DD0 RID: 11728 RVA: 0x00095CA2 File Offset: 0x00093EA2
		public DataControlFieldCell(DataControlField containingField)
		{
			this._containingField = containingField;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x00095CB1 File Offset: 0x00093EB1
		protected DataControlFieldCell(HtmlTextWriterTag tagKey, DataControlField containingField) : base(tagKey)
		{
			this._containingField = containingField;
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x00095CC1 File Offset: 0x00093EC1
		public DataControlField ContainingField
		{
			get
			{
				return this._containingField;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x00095CC9 File Offset: 0x00093EC9
		// (set) Token: 0x06002DD4 RID: 11732 RVA: 0x00095CD6 File Offset: 0x00093ED6
		public override ValidateRequestMode ValidateRequestMode
		{
			get
			{
				return this._containingField.ValidateRequestMode;
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("DataControlFieldCell_ShouldNotSetValidateRequestMode"));
			}
		}

		// Token: 0x04001FB2 RID: 8114
		private DataControlField _containingField;
	}
}
