using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000BD6 RID: 3030
	[ParseChildren(typeof(DataModelField), ChildrenAsProperties = true, DefaultProperty = "Fields")]
	public class DataModel
	{
		// Token: 0x060073A3 RID: 29603 RVA: 0x001B0411 File Offset: 0x001AE611
		public DataModel() : this("")
		{
		}

		// Token: 0x060073A4 RID: 29604 RVA: 0x001B041E File Offset: 0x001AE61E
		public DataModel(string modelName) : this(modelName, "")
		{
		}

		// Token: 0x060073A5 RID: 29605 RVA: 0x001B042C File Offset: 0x001AE62C
		public DataModel(string modelName, string setName)
		{
			this._id = modelName;
			this._set = setName;
			this._pageSize = 0;
			this._pageIndex = 0;
		}

		// Token: 0x170025A8 RID: 9640
		// (get) Token: 0x060073A6 RID: 29606 RVA: 0x001B0450 File Offset: 0x001AE650
		// (set) Token: 0x060073A7 RID: 29607 RVA: 0x001B0458 File Offset: 0x001AE658
		[DefaultValue("")]
		[Description("Gets or sets the name of the collection that holds the model.")]
		[Category("Behavior")]
		public string Set
		{
			get
			{
				return this._set;
			}
			set
			{
				this._set = value;
			}
		}

		// Token: 0x170025A9 RID: 9641
		// (get) Token: 0x060073A8 RID: 29608 RVA: 0x001B0461 File Offset: 0x001AE661
		// (set) Token: 0x060073A9 RID: 29609 RVA: 0x001B0469 File Offset: 0x001AE669
		[Description("Gets or sets the field name for the operation.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string ModelID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x170025AA RID: 9642
		// (get) Token: 0x060073AA RID: 29610 RVA: 0x001B0472 File Offset: 0x001AE672
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Description("Collection of fields for the model.")]
		public List<DataModelField> Fields
		{
			get
			{
				if (this._fields == null)
				{
					this._fields = new List<DataModelField>();
				}
				return this._fields;
			}
		}

		// Token: 0x170025AB RID: 9643
		// (get) Token: 0x060073AB RID: 29611 RVA: 0x001B048D File Offset: 0x001AE68D
		// (set) Token: 0x060073AC RID: 29612 RVA: 0x001B0495 File Offset: 0x001AE695
		[ClientPropertyName("_pageSize")]
		[Category("Behavior")]
		[DefaultValue(0)]
		[Description("Gets or sets the page size when paging is enabled.")]
		[ClientControlProperty]
		public int PageSize
		{
			get
			{
				return this._pageSize;
			}
			set
			{
				this._pageSize = value;
			}
		}

		// Token: 0x170025AC RID: 9644
		// (get) Token: 0x060073AD RID: 29613 RVA: 0x001B049E File Offset: 0x001AE69E
		// (set) Token: 0x060073AE RID: 29614 RVA: 0x001B04A6 File Offset: 0x001AE6A6
		[DefaultValue(0)]
		[ClientControlProperty]
		[ClientPropertyName("_pageIndex")]
		[Description("Gets or sets the queried page index when paging is enabled.")]
		[Category("Behavior")]
		public int PageIndex
		{
			get
			{
				return this._pageIndex;
			}
			set
			{
				this._pageIndex = value;
			}
		}

		// Token: 0x04001F6F RID: 8047
		private string _id;

		// Token: 0x04001F70 RID: 8048
		private string _set;

		// Token: 0x04001F71 RID: 8049
		private int _pageSize;

		// Token: 0x04001F72 RID: 8050
		private int _pageIndex;

		// Token: 0x04001F73 RID: 8051
		private List<DataModelField> _fields;
	}
}
