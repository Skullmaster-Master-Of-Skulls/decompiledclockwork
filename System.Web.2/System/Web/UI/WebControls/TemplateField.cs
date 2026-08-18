using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F4 RID: 1268
	public class TemplateField : DataControlField
	{
		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06003F20 RID: 16160 RVA: 0x000CACA3 File Offset: 0x000C8EA3
		// (set) Token: 0x06003F21 RID: 16161 RVA: 0x000CACAB File Offset: 0x000C8EAB
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateField_AlternatingItemTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		public virtual ITemplate AlternatingItemTemplate
		{
			get
			{
				return this.alternatingItemTemplate;
			}
			set
			{
				this.alternatingItemTemplate = value;
				this.OnFieldChanged();
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x00086BC8 File Offset: 0x00084DC8
		// (set) Token: 0x06003F23 RID: 16163 RVA: 0x00086BD0 File Offset: 0x00084DD0
		[WebCategory("Behavior")]
		[WebSysDescription("Control_ValidateRequestMode")]
		[DefaultValue(ValidateRequestMode.Inherit)]
		public new ValidateRequestMode ValidateRequestMode
		{
			get
			{
				return base.ValidateRequestMode;
			}
			set
			{
				base.ValidateRequestMode = value;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x000CACBC File Offset: 0x000C8EBC
		// (set) Token: 0x06003F25 RID: 16165 RVA: 0x00086C49 File Offset: 0x00084E49
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("ImageField_ConvertEmptyStringToNull")]
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = base.ViewState["ConvertEmptyStringToNull"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ConvertEmptyStringToNull"] = value;
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x000CACE5 File Offset: 0x000C8EE5
		// (set) Token: 0x06003F27 RID: 16167 RVA: 0x000CACED File Offset: 0x000C8EED
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateField_EditItemTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this.editItemTemplate;
			}
			set
			{
				this.editItemTemplate = value;
				this.OnFieldChanged();
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06003F28 RID: 16168 RVA: 0x000CACFC File Offset: 0x000C8EFC
		// (set) Token: 0x06003F29 RID: 16169 RVA: 0x000CAD04 File Offset: 0x000C8F04
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateField_FooterTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer))]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
				this.OnFieldChanged();
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x000CAD13 File Offset: 0x000C8F13
		// (set) Token: 0x06003F2B RID: 16171 RVA: 0x000CAD1B File Offset: 0x000C8F1B
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateField_HeaderTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer))]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
				this.OnFieldChanged();
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06003F2C RID: 16172 RVA: 0x000CAD2A File Offset: 0x000C8F2A
		// (set) Token: 0x06003F2D RID: 16173 RVA: 0x000CAD32 File Offset: 0x000C8F32
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateField_InsertItemTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		public virtual ITemplate InsertItemTemplate
		{
			get
			{
				return this.insertItemTemplate;
			}
			set
			{
				this.insertItemTemplate = value;
				this.OnFieldChanged();
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06003F2E RID: 16174 RVA: 0x000CAD41 File Offset: 0x000C8F41
		// (set) Token: 0x06003F2F RID: 16175 RVA: 0x000CAD49 File Offset: 0x000C8F49
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateField_ItemTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
				this.OnFieldChanged();
			}
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x000CAD58 File Offset: 0x000C8F58
		protected override void CopyProperties(DataControlField newField)
		{
			((TemplateField)newField).ConvertEmptyStringToNull = this.ConvertEmptyStringToNull;
			((TemplateField)newField).AlternatingItemTemplate = this.AlternatingItemTemplate;
			((TemplateField)newField).ItemTemplate = this.ItemTemplate;
			((TemplateField)newField).FooterTemplate = this.FooterTemplate;
			((TemplateField)newField).EditItemTemplate = this.EditItemTemplate;
			((TemplateField)newField).HeaderTemplate = this.HeaderTemplate;
			((TemplateField)newField).InsertItemTemplate = this.InsertItemTemplate;
			base.CopyProperties(newField);
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x000CADE3 File Offset: 0x000C8FE3
		protected override DataControlField CreateField()
		{
			return new TemplateField();
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x000CADEC File Offset: 0x000C8FEC
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			DataBoundControlHelper.ExtractValuesFromBindableControls(dictionary, cell);
			IBindableTemplate bindableTemplate = this.ItemTemplate as IBindableTemplate;
			if ((rowState & DataControlRowState.Alternate) != DataControlRowState.Normal && this.AlternatingItemTemplate != null)
			{
				bindableTemplate = (this.AlternatingItemTemplate as IBindableTemplate);
			}
			if ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal && this.EditItemTemplate != null)
			{
				bindableTemplate = (this.EditItemTemplate as IBindableTemplate);
			}
			else if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal && this.InsertVisible)
			{
				if (this.InsertItemTemplate != null)
				{
					bindableTemplate = (this.InsertItemTemplate as IBindableTemplate);
				}
				else if (this.EditItemTemplate != null)
				{
					bindableTemplate = (this.EditItemTemplate as IBindableTemplate);
				}
			}
			if (bindableTemplate != null)
			{
				bool convertEmptyStringToNull = this.ConvertEmptyStringToNull;
				foreach (object obj in bindableTemplate.ExtractValues(cell.BindingContainer))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					object value = dictionaryEntry.Value;
					if (convertEmptyStringToNull && value is string && ((string)value).Length == 0)
					{
						dictionary[dictionaryEntry.Key] = null;
					}
					else
					{
						dictionary[dictionaryEntry.Key] = value;
					}
				}
			}
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x000CAF14 File Offset: 0x000C9114
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			ITemplate template = null;
			switch (cellType)
			{
			case DataControlCellType.Header:
				template = this.headerTemplate;
				break;
			case DataControlCellType.Footer:
				template = this.footerTemplate;
				break;
			case DataControlCellType.DataCell:
				template = this.itemTemplate;
				if ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal)
				{
					if (this.editItemTemplate != null)
					{
						template = this.editItemTemplate;
					}
				}
				else if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
				{
					if (this.insertItemTemplate != null)
					{
						template = this.insertItemTemplate;
					}
					else if (this.editItemTemplate != null)
					{
						template = this.editItemTemplate;
					}
				}
				else if ((rowState & DataControlRowState.Alternate) != DataControlRowState.Normal && this.alternatingItemTemplate != null)
				{
					template = this.alternatingItemTemplate;
				}
				break;
			}
			if (template != null)
			{
				cell.Text = string.Empty;
				template.InstantiateIn(cell);
				return;
			}
			if (cellType == DataControlCellType.DataCell)
			{
				cell.Text = "&nbsp;";
			}
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x000CAFD1 File Offset: 0x000C91D1
		public override void ValidateSupportsCallback()
		{
			throw new NotSupportedException(SR.GetString("TemplateField_CallbacksNotSupported", new object[]
			{
				base.Control.ID
			}));
		}

		// Token: 0x04002431 RID: 9265
		private ITemplate headerTemplate;

		// Token: 0x04002432 RID: 9266
		private ITemplate footerTemplate;

		// Token: 0x04002433 RID: 9267
		private ITemplate itemTemplate;

		// Token: 0x04002434 RID: 9268
		private ITemplate editItemTemplate;

		// Token: 0x04002435 RID: 9269
		private ITemplate alternatingItemTemplate;

		// Token: 0x04002436 RID: 9270
		private ITemplate insertItemTemplate;
	}
}
