using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020010A0 RID: 4256
	public abstract class GridDropDownColumnEditor : GridColumnEditorBase
	{
		// Token: 0x170037D8 RID: 14296
		// (get) Token: 0x0600ACD6 RID: 44246
		// (set) Token: 0x0600ACD7 RID: 44247
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract int SelectedIndex { get; set; }

		// Token: 0x170037D9 RID: 14297
		// (get) Token: 0x0600ACD8 RID: 44248
		// (set) Token: 0x0600ACD9 RID: 44249
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public abstract string SelectedValue { get; set; }

		// Token: 0x170037DA RID: 14298
		// (get) Token: 0x0600ACDA RID: 44250
		// (set) Token: 0x0600ACDB RID: 44251
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public abstract string SelectedText { get; set; }

		// Token: 0x0600ACDC RID: 44252
		public new abstract void DataBind();

		// Token: 0x170037DB RID: 14299
		// (get) Token: 0x0600ACDD RID: 44253 RVA: 0x002521E0 File Offset: 0x002503E0
		// (set) Token: 0x0600ACDE RID: 44254 RVA: 0x002521E8 File Offset: 0x002503E8
		public virtual string DataMember
		{
			get
			{
				return this._dataMember;
			}
			set
			{
				this._dataMember = value;
			}
		}

		// Token: 0x170037DC RID: 14300
		// (get) Token: 0x0600ACDF RID: 44255 RVA: 0x002521F1 File Offset: 0x002503F1
		// (set) Token: 0x0600ACE0 RID: 44256 RVA: 0x002521F9 File Offset: 0x002503F9
		public virtual object DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				this._dataSource = value;
			}
		}

		// Token: 0x170037DD RID: 14301
		// (get) Token: 0x0600ACE1 RID: 44257 RVA: 0x00252202 File Offset: 0x00250402
		// (set) Token: 0x0600ACE2 RID: 44258 RVA: 0x0025220A File Offset: 0x0025040A
		public virtual string DataTextField
		{
			get
			{
				return this._dataTextField;
			}
			set
			{
				this._dataTextField = value;
			}
		}

		// Token: 0x170037DE RID: 14302
		// (get) Token: 0x0600ACE3 RID: 44259 RVA: 0x00252213 File Offset: 0x00250413
		// (set) Token: 0x0600ACE4 RID: 44260 RVA: 0x0025221B File Offset: 0x0025041B
		public virtual string DataTextFormatString
		{
			get
			{
				return this._dataTextFormatString;
			}
			set
			{
				this._dataTextFormatString = value;
			}
		}

		// Token: 0x170037DF RID: 14303
		// (get) Token: 0x0600ACE5 RID: 44261 RVA: 0x00252224 File Offset: 0x00250424
		// (set) Token: 0x0600ACE6 RID: 44262 RVA: 0x0025222C File Offset: 0x0025042C
		public virtual string DataValueField
		{
			get
			{
				return this._dataValueField;
			}
			set
			{
				this._dataValueField = value;
			}
		}

		// Token: 0x04002DD0 RID: 11728
		private string _dataMember;

		// Token: 0x04002DD1 RID: 11729
		private object _dataSource;

		// Token: 0x04002DD2 RID: 11730
		private string _dataTextField;

		// Token: 0x04002DD3 RID: 11731
		private string _dataTextFormatString;

		// Token: 0x04002DD4 RID: 11732
		private string _dataValueField;
	}
}
