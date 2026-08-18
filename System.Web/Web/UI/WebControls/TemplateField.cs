using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000662 RID: 1634
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TemplateField : DataControlField
	{
		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06004FD5 RID: 20437 RVA: 0x0014063D File Offset: 0x0013F63D
		// (set) Token: 0x06004FD6 RID: 20438 RVA: 0x00140645 File Offset: 0x0013F645
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[WebSysDescription("TemplateField_AlternatingItemTemplate")]
		[Browsable(false)]
		[DefaultValue(null)]
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

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06004FD7 RID: 20439 RVA: 0x00140654 File Offset: 0x0013F654
		// (set) Token: 0x06004FD8 RID: 20440 RVA: 0x0014067D File Offset: 0x0013F67D
		[WebCategory("Behavior")]
		[WebSysDescription("ImageField_ConvertEmptyStringToNull")]
		[DefaultValue(true)]
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

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x06004FD9 RID: 20441 RVA: 0x00140695 File Offset: 0x0013F695
		// (set) Token: 0x06004FDA RID: 20442 RVA: 0x0014069D File Offset: 0x0013F69D
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[WebSysDescription("TemplateField_EditItemTemplate")]
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

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06004FDB RID: 20443 RVA: 0x001406AC File Offset: 0x0013F6AC
		// (set) Token: 0x06004FDC RID: 20444 RVA: 0x001406B4 File Offset: 0x0013F6B4
		[TemplateContainer(typeof(IDataItemContainer))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateField_FooterTemplate")]
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

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06004FDD RID: 20445 RVA: 0x001406C3 File Offset: 0x0013F6C3
		// (set) Token: 0x06004FDE RID: 20446 RVA: 0x001406CB File Offset: 0x0013F6CB
		[WebSysDescription("TemplateField_HeaderTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer))]
		[DefaultValue(null)]
		[Browsable(false)]
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

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06004FDF RID: 20447 RVA: 0x001406DA File Offset: 0x0013F6DA
		// (set) Token: 0x06004FE0 RID: 20448 RVA: 0x001406E2 File Offset: 0x0013F6E2
		[WebSysDescription("TemplateField_InsertItemTemplate")]
		[Browsable(false)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06004FE1 RID: 20449 RVA: 0x001406F1 File Offset: 0x0013F6F1
		// (set) Token: 0x06004FE2 RID: 20450 RVA: 0x001406F9 File Offset: 0x0013F6F9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[WebSysDescription("TemplateField_ItemTemplate")]
		[Browsable(false)]
		[DefaultValue(null)]
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

		// Token: 0x06004FE3 RID: 20451 RVA: 0x00140708 File Offset: 0x0013F708
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

		// Token: 0x06004FE4 RID: 20452 RVA: 0x00140793 File Offset: 0x0013F793
		protected override DataControlField CreateField()
		{
			return new TemplateField();
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0014079C File Offset: 0x0013F79C
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

		// Token: 0x06004FE6 RID: 20454 RVA: 0x001408C4 File Offset: 0x0013F8C4
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

		// Token: 0x06004FE7 RID: 20455 RVA: 0x00140984 File Offset: 0x0013F984
		public override void ValidateSupportsCallback()
		{
			throw new NotSupportedException(SR.GetString("TemplateField_CallbacksNotSupported", new object[]
			{
				base.Control.ID
			}));
		}

		// Token: 0x04002CFE RID: 11518
		private ITemplate headerTemplate;

		// Token: 0x04002CFF RID: 11519
		private ITemplate footerTemplate;

		// Token: 0x04002D00 RID: 11520
		private ITemplate itemTemplate;

		// Token: 0x04002D01 RID: 11521
		private ITemplate editItemTemplate;

		// Token: 0x04002D02 RID: 11522
		private ITemplate alternatingItemTemplate;

		// Token: 0x04002D03 RID: 11523
		private ITemplate insertItemTemplate;
	}
}
