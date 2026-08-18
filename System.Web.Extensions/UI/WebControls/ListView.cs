using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web.Resources;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000A8 RID: 168
	[DefaultProperty("SelectedValue")]
	[Designer("System.Web.UI.Design.WebControls.ListViewDesigner, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[ControlValueProperty("SelectedValue")]
	[DefaultEvent("SelectedIndexChanged")]
	[SupportsEventValidation]
	[ToolboxBitmap(typeof(ListView), "ListView.bmp")]
	[DataKeyProperty("SelectedPersistedDataKey")]
	public class ListView : DataBoundControl, INamingContainer, IPageableItemContainer, IPersistedSelector, IDataKeysControl, IDataBoundListControl, IDataBoundControl, IWizardSideBarListControl
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001E6B3 File Offset: 0x0001C8B3
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0001E6C7 File Offset: 0x0001C8C7
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x0001E6CF File Offset: 0x0001C8CF
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListViewDataItem), BindingDirection.TwoWay)]
		[ResourceDescription("ListView_AlternatingItemTemplate")]
		public virtual ITemplate AlternatingItemTemplate
		{
			get
			{
				return this._alternatingItemTemplate;
			}
			set
			{
				this._alternatingItemTemplate = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0001E6D8 File Offset: 0x0001C8D8
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001E6E8 File Offset: 0x0001C8E8
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001E6F0 File Offset: 0x0001C8F0
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0001E6F8 File Offset: 0x0001C8F8
		private IOrderedDictionary BoundFieldValues
		{
			get
			{
				if (this._boundFieldValues == null)
				{
					this._boundFieldValues = new OrderedDictionary();
				}
				return this._boundFieldValues;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001AC0A File Offset: 0x00018E0A
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0001E714 File Offset: 0x0001C914
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x0001E73D File Offset: 0x0001C93D
		[Category("Behavior")]
		[DefaultValue(true)]
		[ResourceDescription("ListView_ConvertEmptyStringToNull")]
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = this.ViewState["ConvertEmptyStringToNull"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ConvertEmptyStringToNull"] = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001E755 File Offset: 0x0001C955
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CssClassProperty]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001E75D File Offset: 0x0001C95D
		private ArrayList DataKeysArrayList
		{
			get
			{
				if (this._dataKeysArrayList == null)
				{
					this._dataKeysArrayList = new ArrayList();
				}
				return this._dataKeysArrayList;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001E778 File Offset: 0x0001C978
		private ArrayList ClientIDRowSuffixArrayList
		{
			get
			{
				if (this._clientIDRowSuffixArrayList == null)
				{
					this._clientIDRowSuffixArrayList = new ArrayList();
				}
				return this._clientIDRowSuffixArrayList;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001E793 File Offset: 0x0001C993
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResourceDescription("ListView_DataKeys")]
		public virtual DataKeyArray DataKeys
		{
			get
			{
				if (this._dataKeyArray == null)
				{
					this._dataKeyArray = new DataKeyArray(this.DataKeysArrayList);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._dataKeyArray).TrackViewState();
					}
				}
				return this._dataKeyArray;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001E7C7 File Offset: 0x0001C9C7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataKeyArray ClientIDRowSuffixDataKeys
		{
			get
			{
				if (this._clientIDRowSuffixArray == null)
				{
					this._clientIDRowSuffixArray = new DataKeyArray(this.ClientIDRowSuffixArrayList);
				}
				return this._clientIDRowSuffixArray;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001E7E8 File Offset: 0x0001C9E8
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0001E816 File Offset: 0x0001CA16
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Category("Data")]
		[ResourceDescription("ListView_DataKeyNames")]
		[TypeConverter(typeof(StringArrayConverter))]
		public virtual string[] DataKeyNames
		{
			get
			{
				object dataKeyNames = this._dataKeyNames;
				if (dataKeyNames != null)
				{
					return (string[])((string[])dataKeyNames).Clone();
				}
				return new string[0];
			}
			set
			{
				if (!DataBoundControlHelper.CompareStringArrays(value, this.DataKeyNamesInternal))
				{
					if (value != null)
					{
						this._dataKeyNames = (string[])value.Clone();
					}
					else
					{
						this._dataKeyNames = null;
					}
					this.ClearDataKeys();
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0001E850 File Offset: 0x0001CA50
		private string[] DataKeyNamesInternal
		{
			get
			{
				object dataKeyNames = this._dataKeyNames;
				if (dataKeyNames != null)
				{
					return (string[])dataKeyNames;
				}
				return new string[0];
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001E874 File Offset: 0x0001CA74
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0001E87C File Offset: 0x0001CA7C
		[Category("Default")]
		[DefaultValue(-1)]
		[ResourceDescription("ListView_EditIndex")]
		public virtual int EditIndex
		{
			get
			{
				return this._editIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this._editIndex)
				{
					if (value == -1)
					{
						this.BoundFieldValues.Clear();
					}
					this._editIndex = value;
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001E8B2 File Offset: 0x0001CAB2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResourceDescription("ListView_EditItem")]
		public virtual ListViewItem EditItem
		{
			get
			{
				if (this._editIndex > -1 && this._editIndex < this.Items.Count)
				{
					return this.Items[this._editIndex];
				}
				return null;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001E8E3 File Offset: 0x0001CAE3
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0001E8EB File Offset: 0x0001CAEB
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListViewDataItem), BindingDirection.TwoWay)]
		[ResourceDescription("ListView_EditItemTemplate")]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this._editItemTemplate;
			}
			set
			{
				this._editItemTemplate = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0001E8F4 File Offset: 0x0001CAF4
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0001E8FC File Offset: 0x0001CAFC
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListView))]
		[ResourceDescription("ListView_EmptyDataTemplate")]
		public virtual ITemplate EmptyDataTemplate
		{
			get
			{
				return this._emptyDataTemplate;
			}
			set
			{
				this._emptyDataTemplate = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001E905 File Offset: 0x0001CB05
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x0001E90D File Offset: 0x0001CB0D
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListViewItem))]
		[ResourceDescription("ListView_EmptyItemTemplate")]
		public virtual ITemplate EmptyItemTemplate
		{
			get
			{
				return this._emptyItemTemplate;
			}
			set
			{
				this._emptyItemTemplate = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0001E918 File Offset: 0x0001CB18
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x0001E941 File Offset: 0x0001CB41
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[ResourceDescription("ListView_EnableModelValidation")]
		public virtual bool EnableModelValidation
		{
			get
			{
				object obj = this.ViewState["EnableModelValidation"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["EnableModelValidation"] = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0001E95C File Offset: 0x0001CB5C
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0001E985 File Offset: 0x0001CB85
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[ResourceDescription("ListView_EnablePersistedSelection")]
		public virtual bool EnablePersistedSelection
		{
			get
			{
				object obj = this.ViewState["EnablePersistedSelection"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnablePersistedSelection"] = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0001E99D File Offset: 0x0001CB9D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001E9A5 File Offset: 0x0001CBA5
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0001E9B0 File Offset: 0x0001CBB0
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
		[DefaultValue("groupPlaceholder")]
		[Category("Behavior")]
		[ResourceDescription("ListView_GroupPlaceholderID")]
		public virtual string GroupPlaceholderID
		{
			get
			{
				object obj = this.ViewState["GroupPlaceholderID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "groupPlaceholder";
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_ContainerNameMustNotBeEmpty, new object[]
					{
						"GroupPlaceholderID"
					}));
				}
				this.ViewState["GroupPlaceholderID"] = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001EA2E File Offset: 0x0001CC2E
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x0001EA36 File Offset: 0x0001CC36
		[Category("Default")]
		[DefaultValue(1)]
		[ResourceDescription("ListView_GroupItemCount")]
		public virtual int GroupItemCount
		{
			get
			{
				return this._groupItemCount;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._groupItemCount = value;
				this.SetRequiresDataBindingIfInitialized();
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0001EA54 File Offset: 0x0001CC54
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0001EA5C File Offset: 0x0001CC5C
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListViewItem))]
		[ResourceDescription("ListView_GroupSeparatorTemplate")]
		public virtual ITemplate GroupSeparatorTemplate
		{
			get
			{
				return this._groupSeparatorTemplate;
			}
			set
			{
				this._groupSeparatorTemplate = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001EA65 File Offset: 0x0001CC65
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0001EA6D File Offset: 0x0001CC6D
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListViewItem))]
		[ResourceDescription("ListView_GroupTemplate")]
		public virtual ITemplate GroupTemplate
		{
			get
			{
				return this._groupTemplate;
			}
			set
			{
				this._groupTemplate = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001EA76 File Offset: 0x0001CC76
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001EA7E File Offset: 0x0001CC7E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResourceDescription("ListView_InsertItem")]
		public virtual ListViewItem InsertItem
		{
			get
			{
				return this._insertItem;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0001EA88 File Offset: 0x0001CC88
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0001EAB1 File Offset: 0x0001CCB1
		[Category("Default")]
		[DefaultValue(InsertItemPosition.None)]
		[ResourceDescription("ListView_InsertItemPosition")]
		public virtual InsertItemPosition InsertItemPosition
		{
			get
			{
				object obj = this.ViewState["InsertItemPosition"];
				if (obj != null)
				{
					return (InsertItemPosition)obj;
				}
				return InsertItemPosition.None;
			}
			set
			{
				if (this.InsertItemPosition != value)
				{
					this.ViewState["InsertItemPosition"] = value;
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0001EAD8 File Offset: 0x0001CCD8
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListViewItem), BindingDirection.TwoWay)]
		[ResourceDescription("ListView_InsertItemTemplate")]
		public virtual ITemplate InsertItemTemplate
		{
			get
			{
				return this._insertItemTemplate;
			}
			set
			{
				this._insertItemTemplate = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0001EAEC File Offset: 0x0001CCEC
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x0001EB1C File Offset: 0x0001CD1C
		[DefaultValue("itemPlaceholder")]
		[Category("Behavior")]
		[ResourceDescription("ListView_ItemPlaceholderID")]
		public virtual string ItemPlaceholderID
		{
			get
			{
				object obj = this.ViewState["ItemPlaceholderID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "itemPlaceholder";
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_ContainerNameMustNotBeEmpty, new object[]
					{
						"ItemPlaceholderID"
					}));
				}
				this.ViewState["ItemPlaceholderID"] = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0001EB6A File Offset: 0x0001CD6A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResourceDescription("ListView_Items")]
		public virtual IList<ListViewDataItem> Items
		{
			get
			{
				if (this._itemList == null)
				{
					this._itemList = new List<ListViewDataItem>();
				}
				return this._itemList;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001EB85 File Offset: 0x0001CD85
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0001EB8D File Offset: 0x0001CD8D
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListViewItem))]
		[ResourceDescription("ListView_ItemSeparatorTemplate")]
		public virtual ITemplate ItemSeparatorTemplate
		{
			get
			{
				return this._itemSeparatorTemplate;
			}
			set
			{
				this._itemSeparatorTemplate = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0001EB96 File Offset: 0x0001CD96
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0001EB9E File Offset: 0x0001CD9E
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListViewDataItem), BindingDirection.TwoWay)]
		[ResourceDescription("ListView_ItemTemplate")]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this._itemTemplate;
			}
			set
			{
				this._itemTemplate = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001EBA7 File Offset: 0x0001CDA7
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0001EBAF File Offset: 0x0001CDAF
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListView))]
		[ResourceDescription("ListView_LayoutTemplate")]
		public virtual ITemplate LayoutTemplate
		{
			get
			{
				return this._layoutTemplate;
			}
			set
			{
				this._layoutTemplate = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001EBB8 File Offset: 0x0001CDB8
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0001EBE8 File Offset: 0x0001CDE8
		[DefaultValue(null)]
		[TypeConverter(typeof(StringArrayConverter))]
		[WebCategory("Data")]
		public virtual string[] ClientIDRowSuffix
		{
			get
			{
				object clientIDRowSuffix = this._clientIDRowSuffix;
				if (clientIDRowSuffix != null)
				{
					return (string[])((string[])clientIDRowSuffix).Clone();
				}
				return new string[0];
			}
			set
			{
				if (!DataBoundControlHelper.CompareStringArrays(value, this.ClientIDRowSuffixInternal))
				{
					if (value != null)
					{
						this._clientIDRowSuffix = (string[])value.Clone();
					}
					else
					{
						this._clientIDRowSuffix = null;
					}
					this._clientIDRowSuffixArrayList = null;
					if (base.Initialized)
					{
						base.RequiresDataBinding = true;
					}
				}
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001EC38 File Offset: 0x0001CE38
		private string[] ClientIDRowSuffixInternal
		{
			get
			{
				object clientIDRowSuffix = this._clientIDRowSuffix;
				if (clientIDRowSuffix != null)
				{
					return (string[])clientIDRowSuffix;
				}
				return new string[0];
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001EC5C File Offset: 0x0001CE5C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DataKey SelectedDataKey
		{
			get
			{
				if (this.DataKeyNamesInternal == null || this.DataKeyNamesInternal.Length == 0)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_DataKeyNamesMustBeSpecified, new object[]
					{
						this.ID
					}));
				}
				DataKeyArray dataKeys = this.DataKeys;
				int selectedIndex = this.SelectedIndex;
				if (dataKeys != null && selectedIndex < dataKeys.Count && selectedIndex > -1)
				{
					return dataKeys[selectedIndex];
				}
				return null;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001ECC5 File Offset: 0x0001CEC5
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0001ECD0 File Offset: 0x0001CED0
		[Category("Default")]
		[DefaultValue(-1)]
		[ResourceDescription("ListView_SelectedIndex")]
		public virtual int SelectedIndex
		{
			get
			{
				return this._selectedIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this._selectedIndex)
				{
					this._selectedIndex = value;
					if (this.EnablePersistedSelection && this.DataKeyNamesInternal.Length != 0)
					{
						this.SelectedPersistedDataKey = this.SelectedDataKey;
					}
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001ED1F File Offset: 0x0001CF1F
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0001ED27 File Offset: 0x0001CF27
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListViewDataItem), BindingDirection.TwoWay)]
		[ResourceDescription("ListView_SelectedItemTemplate")]
		public virtual ITemplate SelectedItemTemplate
		{
			get
			{
				return this._selectedItemTemplate;
			}
			set
			{
				this._selectedItemTemplate = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001ED30 File Offset: 0x0001CF30
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedValue
		{
			get
			{
				DataKey selectedDataKey = this.SelectedDataKey;
				if (selectedDataKey != null)
				{
					return this.SelectedDataKey.Value;
				}
				return null;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001ED54 File Offset: 0x0001CF54
		[Browsable(false)]
		[DefaultValue(SortDirection.Ascending)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[ResourceDescription("ListView_SortDirection")]
		[ResourceCategory("Sorting")]
		public virtual SortDirection SortDirection
		{
			get
			{
				return this.SortDirectionInternal;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0001ED64 File Offset: 0x0001CF64
		private SortDirection SortDirectionInternal
		{
			get
			{
				return this._sortDirection;
			}
			set
			{
				if (value < SortDirection.Ascending || value > SortDirection.Descending)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this._sortDirection != value)
				{
					this._sortDirection = value;
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001ED8F File Offset: 0x0001CF8F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResourceDescription("ListView_SortExpression")]
		[ResourceCategory("Sorting")]
		public virtual string SortExpression
		{
			get
			{
				return this.SortExpressionInternal;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0001ED97 File Offset: 0x0001CF97
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x0001ED9F File Offset: 0x0001CF9F
		private string SortExpressionInternal
		{
			get
			{
				return this._sortExpression;
			}
			set
			{
				if (this._sortExpression != value)
				{
					this._sortExpression = value;
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001EDC4 File Offset: 0x0001CFC4
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToolTip
		{
			get
			{
				return base.ToolTip;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0001EDCC File Offset: 0x0001CFCC
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
		[Browsable(false)]
		public virtual DataKey SelectedPersistedDataKey
		{
			get
			{
				return this._persistedDataKey;
			}
			set
			{
				this._persistedDataKey = value;
				if (base.IsTrackingViewState && this._persistedDataKey != null)
				{
					((IStateManager)this._persistedDataKey).TrackViewState();
				}
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001EDF8 File Offset: 0x0001CFF8
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0001E6BB File Offset: 0x0001C8BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				throw new NotSupportedException(AtlasWeb.ListView_StylePropertiesNotSupported);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060007FB RID: 2043 RVA: 0x0001EE00 File Offset: 0x0001D000
		// (remove) Token: 0x060007FC RID: 2044 RVA: 0x0001EE13 File Offset: 0x0001D013
		[Category("Action")]
		[ResourceDescription("ListView_OnItemDeleted")]
		public event EventHandler<ListViewDeletedEventArgs> ItemDeleted
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemDeleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemDeleted, value);
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060007FD RID: 2045 RVA: 0x0001EE26 File Offset: 0x0001D026
		// (remove) Token: 0x060007FE RID: 2046 RVA: 0x0001EE39 File Offset: 0x0001D039
		[Category("Action")]
		[ResourceDescription("ListView_OnItemInserted")]
		public event EventHandler<ListViewInsertedEventArgs> ItemInserted
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemInserted, value);
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060007FF RID: 2047 RVA: 0x0001EE4C File Offset: 0x0001D04C
		// (remove) Token: 0x06000800 RID: 2048 RVA: 0x0001EE5F File Offset: 0x0001D05F
		[Category("Action")]
		[ResourceDescription("ListView_OnItemUpdated")]
		public event EventHandler<ListViewUpdatedEventArgs> ItemUpdated
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemUpdated, value);
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000801 RID: 2049 RVA: 0x0001EE72 File Offset: 0x0001D072
		// (remove) Token: 0x06000802 RID: 2050 RVA: 0x0001EE85 File Offset: 0x0001D085
		[Category("Action")]
		[ResourceDescription("ListView_OnItemCanceling")]
		public event EventHandler<ListViewCancelEventArgs> ItemCanceling
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemCanceling, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemCanceling, value);
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000803 RID: 2051 RVA: 0x0001EE98 File Offset: 0x0001D098
		// (remove) Token: 0x06000804 RID: 2052 RVA: 0x0001EEAB File Offset: 0x0001D0AB
		[Category("Action")]
		[ResourceDescription("ListView_OnItemCommand")]
		public event EventHandler<ListViewCommandEventArgs> ItemCommand
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemCommand, value);
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000805 RID: 2053 RVA: 0x0001EEBE File Offset: 0x0001D0BE
		// (remove) Token: 0x06000806 RID: 2054 RVA: 0x0001EED1 File Offset: 0x0001D0D1
		[Category("Behavior")]
		[ResourceDescription("ListView_OnItemCreated")]
		public event EventHandler<ListViewItemEventArgs> ItemCreated
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemCreated, value);
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000807 RID: 2055 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
		// (remove) Token: 0x06000808 RID: 2056 RVA: 0x0001EEF7 File Offset: 0x0001D0F7
		[Category("Data")]
		[ResourceDescription("ListView_OnItemDataBound")]
		public event EventHandler<ListViewItemEventArgs> ItemDataBound
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemDataBound, value);
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000809 RID: 2057 RVA: 0x0001EF0A File Offset: 0x0001D10A
		// (remove) Token: 0x0600080A RID: 2058 RVA: 0x0001EF1D File Offset: 0x0001D11D
		[Category("Action")]
		[ResourceDescription("ListView_OnItemDeleting")]
		public event EventHandler<ListViewDeleteEventArgs> ItemDeleting
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemDeleting, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemDeleting, value);
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600080B RID: 2059 RVA: 0x0001EF30 File Offset: 0x0001D130
		// (remove) Token: 0x0600080C RID: 2060 RVA: 0x0001EF43 File Offset: 0x0001D143
		[Category("Action")]
		[ResourceDescription("ListView_OnItemEditing")]
		public event EventHandler<ListViewEditEventArgs> ItemEditing
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemEditing, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemEditing, value);
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600080D RID: 2061 RVA: 0x0001EF56 File Offset: 0x0001D156
		// (remove) Token: 0x0600080E RID: 2062 RVA: 0x0001EF69 File Offset: 0x0001D169
		[Category("Action")]
		[ResourceDescription("ListView_OnItemInserting")]
		public event EventHandler<ListViewInsertEventArgs> ItemInserting
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemInserting, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemInserting, value);
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600080F RID: 2063 RVA: 0x0001EF7C File Offset: 0x0001D17C
		// (remove) Token: 0x06000810 RID: 2064 RVA: 0x0001EF8F File Offset: 0x0001D18F
		[Category("Action")]
		[ResourceDescription("ListView_OnItemUpdating")]
		public event EventHandler<ListViewUpdateEventArgs> ItemUpdating
		{
			add
			{
				base.Events.AddHandler(ListView.EventItemUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventItemUpdating, value);
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000811 RID: 2065 RVA: 0x0001EFA2 File Offset: 0x0001D1A2
		// (remove) Token: 0x06000812 RID: 2066 RVA: 0x0001EFB5 File Offset: 0x0001D1B5
		[Category("Behavior")]
		[ResourceDescription("ListView_OnLayoutCreated")]
		public event EventHandler LayoutCreated
		{
			add
			{
				base.Events.AddHandler(ListView.EventLayoutCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventLayoutCreated, value);
			}
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000813 RID: 2067 RVA: 0x0001EFC8 File Offset: 0x0001D1C8
		// (remove) Token: 0x06000814 RID: 2068 RVA: 0x0001EFDB File Offset: 0x0001D1DB
		[Category("Behavior")]
		[ResourceDescription("ListView_OnPagePropertiesChanged")]
		public event EventHandler PagePropertiesChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EventPagePropertiesChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventPagePropertiesChanged, value);
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000815 RID: 2069 RVA: 0x0001EFEE File Offset: 0x0001D1EE
		// (remove) Token: 0x06000816 RID: 2070 RVA: 0x0001F001 File Offset: 0x0001D201
		[Category("Behavior")]
		[ResourceDescription("ListView_OnPagePropertiesChanging")]
		public event EventHandler<PagePropertiesChangingEventArgs> PagePropertiesChanging
		{
			add
			{
				base.Events.AddHandler(ListView.EventPagePropertiesChanging, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventPagePropertiesChanging, value);
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000817 RID: 2071 RVA: 0x0001F014 File Offset: 0x0001D214
		// (remove) Token: 0x06000818 RID: 2072 RVA: 0x0001F027 File Offset: 0x0001D227
		[Category("Action")]
		[ResourceDescription("ListView_OnSelectedIndexChanged")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EventSelectedIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventSelectedIndexChanged, value);
			}
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000819 RID: 2073 RVA: 0x0001F03A File Offset: 0x0001D23A
		// (remove) Token: 0x0600081A RID: 2074 RVA: 0x0001F04D File Offset: 0x0001D24D
		[Category("Action")]
		[ResourceDescription("ListView_OnSelectedIndexChanging")]
		public event EventHandler<ListViewSelectEventArgs> SelectedIndexChanging
		{
			add
			{
				base.Events.AddHandler(ListView.EventSelectedIndexChanging, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventSelectedIndexChanging, value);
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600081B RID: 2075 RVA: 0x0001F060 File Offset: 0x0001D260
		// (remove) Token: 0x0600081C RID: 2076 RVA: 0x0001F073 File Offset: 0x0001D273
		[Category("Action")]
		[ResourceDescription("ListView_OnSorted")]
		public event EventHandler Sorted
		{
			add
			{
				base.Events.AddHandler(ListView.EventSorted, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventSorted, value);
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600081D RID: 2077 RVA: 0x0001F086 File Offset: 0x0001D286
		// (remove) Token: 0x0600081E RID: 2078 RVA: 0x0001F099 File Offset: 0x0001D299
		[Category("Action")]
		[ResourceDescription("ListView_OnSorting")]
		public event EventHandler<ListViewSortEventArgs> Sorting
		{
			add
			{
				base.Events.AddHandler(ListView.EventSorting, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventSorting, value);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001F0AC File Offset: 0x0001D2AC
		protected override bool IsUsingModelBinders
		{
			get
			{
				return !string.IsNullOrEmpty(this.SelectMethod) || !string.IsNullOrEmpty(this.UpdateMethod) || !string.IsNullOrEmpty(this.DeleteMethod) || !string.IsNullOrEmpty(this.InsertMethod);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001F0E5 File Offset: 0x0001D2E5
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x0001F0F6 File Offset: 0x0001D2F6
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_UpdateMethod")]
		public virtual string UpdateMethod
		{
			get
			{
				return this._updateMethod ?? string.Empty;
			}
			set
			{
				if (!string.Equals(this._updateMethod, value, StringComparison.OrdinalIgnoreCase))
				{
					this._updateMethod = value;
					this.OnDataPropertyChanged();
				}
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001F114 File Offset: 0x0001D314
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x0001F125 File Offset: 0x0001D325
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_DeleteMethod")]
		public virtual string DeleteMethod
		{
			get
			{
				return this._deleteMethod ?? string.Empty;
			}
			set
			{
				if (!string.Equals(this._deleteMethod, value, StringComparison.OrdinalIgnoreCase))
				{
					this._deleteMethod = value;
					this.OnDataPropertyChanged();
				}
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001F143 File Offset: 0x0001D343
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0001F154 File Offset: 0x0001D354
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_InsertMethod")]
		public virtual string InsertMethod
		{
			get
			{
				return this._insertMethod ?? string.Empty;
			}
			set
			{
				if (!string.Equals(this._insertMethod, value, StringComparison.OrdinalIgnoreCase))
				{
					this._insertMethod = value;
					this.OnDataPropertyChanged();
				}
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001F174 File Offset: 0x0001D374
		protected virtual void AddControlToContainer(Control control, Control container, int addLocation)
		{
			if (container is HtmlTable)
			{
				ListViewTableRow listViewTableRow = new ListViewTableRow();
				container.Controls.AddAt(addLocation, listViewTableRow);
				listViewTableRow.Controls.Add(control);
				return;
			}
			if (container is HtmlTableRow)
			{
				ListViewTableCell listViewTableCell = new ListViewTableCell();
				container.Controls.AddAt(addLocation, listViewTableCell);
				listViewTableCell.Controls.Add(control);
				return;
			}
			container.Controls.AddAt(addLocation, control);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
		private void AutoIDControl(Control control)
		{
			string str = "ctrl";
			int autoIDIndex = this._autoIDIndex;
			this._autoIDIndex = autoIDIndex + 1;
			control.ID = str + autoIDIndex.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001F219 File Offset: 0x0001D419
		private void ClearDataKeys()
		{
			this._dataKeysArrayList = null;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001F224 File Offset: 0x0001D424
		protected internal override void CreateChildControls()
		{
			object obj = this.ViewState["_!ItemCount"];
			if (obj == null && base.RequiresDataBinding)
			{
				this.EnsureDataBound();
			}
			if (obj != null && (int)obj != -1)
			{
				object[] dataSource = new object[(int)obj];
				this.CreateChildControls(dataSource, false);
				base.ClearChildViewState();
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001F27C File Offset: 0x0001D47C
		protected virtual int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			this.EnsureLayoutTemplate();
			this.RemoveItems();
			if (dataSource == null && this.InsertItemPosition != InsertItemPosition.None)
			{
				dataSource = new object[0];
			}
			bool flag = this._startRowIndex > 0 || this._maximumRows > 0;
			ListViewPagedDataSource listViewPagedDataSource;
			if (dataBinding)
			{
				DataSourceView data = this.GetData();
				DataSourceSelectArguments selectArguments = base.SelectArguments;
				if (data == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_NullView, new object[]
					{
						this.ID
					}));
				}
				bool flag2 = data.CanPage && flag;
				if (!data.CanPage && flag2 && dataSource != null && !(dataSource is ICollection))
				{
					selectArguments.StartRowIndex = this._startRowIndex;
					selectArguments.MaximumRows = this._maximumRows;
					data.Select(selectArguments, new DataSourceViewSelectCallback(this.SelectCallback));
				}
				if (flag2)
				{
					int totalRowCount;
					if (data.CanRetrieveTotalRowCount)
					{
						totalRowCount = selectArguments.TotalRowCount;
					}
					else
					{
						ICollection collection = dataSource as ICollection;
						if (collection == null)
						{
							throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_NeedICollectionOrTotalRowCount, new object[]
							{
								base.GetType().Name
							}));
						}
						totalRowCount = checked(this._startRowIndex + collection.Count);
					}
					listViewPagedDataSource = this.CreateServerPagedDataSource(totalRowCount);
				}
				else
				{
					listViewPagedDataSource = this.CreatePagedDataSource();
				}
			}
			else
			{
				listViewPagedDataSource = this.CreatePagedDataSource();
			}
			ArrayList dataKeysArrayList = this.DataKeysArrayList;
			ArrayList clientIDRowSuffixArrayList = this.ClientIDRowSuffixArrayList;
			this._dataKeyArray = null;
			this._clientIDRowSuffixArray = null;
			ICollection collection2 = dataSource as ICollection;
			if (dataBinding)
			{
				dataKeysArrayList.Clear();
				clientIDRowSuffixArrayList.Clear();
				if (dataSource != null && collection2 == null && !listViewPagedDataSource.IsServerPagingEnabled && flag)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_Missing_VirtualItemCount, new object[]
					{
						this.ID
					}));
				}
			}
			else if (collection2 == null)
			{
				throw new InvalidOperationException(AtlasWeb.ListView_DataSourceMustBeCollectionWhenNotDataBinding);
			}
			if (dataSource != null)
			{
				listViewPagedDataSource.DataSource = dataSource;
				if (dataBinding && flag)
				{
					dataKeysArrayList.Capacity = listViewPagedDataSource.DataSourceCount;
					clientIDRowSuffixArrayList.Capacity = listViewPagedDataSource.DataSourceCount;
				}
				if (this._groupTemplate != null)
				{
					this._itemList = this.CreateItemsInGroups(listViewPagedDataSource, dataBinding, this.InsertItemPosition, dataKeysArrayList);
					if (dataBinding && this.ClientIDRowSuffixInternal != null && this.ClientIDRowSuffixInternal.Length != 0)
					{
						this.CreateSuffixArrayList(listViewPagedDataSource, clientIDRowSuffixArrayList);
					}
				}
				else
				{
					if (this.GroupItemCount != 1)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_GroupItemCountNoGroupTemplate, new object[]
						{
							this.ID,
							this.GroupPlaceholderID
						}));
					}
					this._itemList = this.CreateItemsWithoutGroups(listViewPagedDataSource, dataBinding, this.InsertItemPosition, dataKeysArrayList);
					if (dataBinding && this.ClientIDRowSuffixInternal != null && this.ClientIDRowSuffixInternal.Length != 0)
					{
						this.CreateSuffixArrayList(listViewPagedDataSource, clientIDRowSuffixArrayList);
					}
				}
				this._totalRowCount = (flag ? listViewPagedDataSource.DataSourceCount : this._itemList.Count);
				this.OnTotalRowCountAvailable(new PageEventArgs(this._startRowIndex, this._maximumRows, this._totalRowCount));
				if (this._itemList.Count == 0 && this.InsertItemPosition == InsertItemPosition.None)
				{
					this.Controls.Clear();
					this.CreateEmptyDataItem();
				}
			}
			else
			{
				this.Controls.Clear();
				this.CreateEmptyDataItem();
			}
			return this._totalRowCount;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001F58E File Offset: 0x0001D78E
		protected override Style CreateControlStyle()
		{
			if (!base.DesignMode)
			{
				throw new NotSupportedException(AtlasWeb.ListView_StyleNotSupported);
			}
			return base.CreateControlStyle();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001F5AC File Offset: 0x0001D7AC
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();
			DataSourceView data = this.GetData();
			bool canPage = data.CanPage;
			string text = this.SortExpressionInternal;
			if (this.SortDirectionInternal == SortDirection.Descending && !string.IsNullOrEmpty(text))
			{
				text += " DESC";
			}
			dataSourceSelectArguments.SortExpression = text;
			if (canPage)
			{
				if (data.CanRetrieveTotalRowCount)
				{
					dataSourceSelectArguments.RetrieveTotalRowCount = true;
					dataSourceSelectArguments.MaximumRows = this._maximumRows;
				}
				else
				{
					dataSourceSelectArguments.MaximumRows = -1;
				}
				dataSourceSelectArguments.StartRowIndex = this._startRowIndex;
			}
			return dataSourceSelectArguments;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001F62C File Offset: 0x0001D82C
		protected virtual void CreateEmptyDataItem()
		{
			if (this._emptyDataTemplate != null)
			{
				this._instantiatedEmptyDataTemplate = true;
				ListViewItem listViewItem = this.CreateItem(ListViewItemType.EmptyItem);
				this.AutoIDControl(listViewItem);
				this.InstantiateEmptyDataTemplate(listViewItem);
				this.OnItemCreated(new ListViewItemEventArgs(listViewItem));
				this.AddControlToContainer(listViewItem, this, 0);
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001F674 File Offset: 0x0001D874
		protected virtual ListViewItem CreateEmptyItem()
		{
			if (this._emptyItemTemplate != null)
			{
				ListViewItem listViewItem = this.CreateItem(ListViewItemType.EmptyItem);
				this.AutoIDControl(listViewItem);
				this.InstantiateEmptyItemTemplate(listViewItem);
				this.OnItemCreated(new ListViewItemEventArgs(listViewItem));
				return listViewItem;
			}
			return null;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001F6B0 File Offset: 0x0001D8B0
		protected virtual ListViewItem CreateInsertItem()
		{
			if (this.InsertItemTemplate == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_InsertTemplateRequired, new object[]
				{
					this.ID
				}));
			}
			ListViewItem listViewItem = this.CreateItem(ListViewItemType.InsertItem);
			this.AutoIDControl(listViewItem);
			this.InstantiateInsertItemTemplate(listViewItem);
			this.OnItemCreated(new ListViewItemEventArgs(listViewItem));
			return listViewItem;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001F70C File Offset: 0x0001D90C
		protected virtual ListViewItem CreateItem(ListViewItemType itemType)
		{
			ListViewItem listViewItem = new ListViewItem(itemType);
			if (itemType == ListViewItemType.InsertItem)
			{
				this._insertItem = listViewItem;
			}
			return listViewItem;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001F72C File Offset: 0x0001D92C
		protected virtual ListViewDataItem CreateDataItem(int dataItemIndex, int displayIndex)
		{
			return new ListViewDataItem(dataItemIndex, displayIndex);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001F738 File Offset: 0x0001D938
		protected virtual IList<ListViewDataItem> CreateItemsWithoutGroups(ListViewPagedDataSource dataSource, bool dataBinding, InsertItemPosition insertPosition, ArrayList keyArray)
		{
			if (this._noGroupsOriginalIndexOfItemPlaceholderInContainer == -1)
			{
				this._noGroupsItemPlaceholderContainer = this.GetPreparedContainerInfo(this, true, out this._noGroupsOriginalIndexOfItemPlaceholderInContainer);
			}
			int num = this._noGroupsOriginalIndexOfItemPlaceholderInContainer;
			List<ListViewDataItem> list = new List<ListViewDataItem>();
			int num2 = 0;
			int num3 = 0;
			if (insertPosition == InsertItemPosition.FirstItem)
			{
				ListViewItem listViewItem = this.CreateInsertItem();
				this.AddControlToContainer(listViewItem, this._noGroupsItemPlaceholderContainer, num);
				listViewItem.DataBind();
				num++;
				num2++;
			}
			this.ResetPersistedSelectedIndex();
			foreach (object obj in dataSource)
			{
				if (num2 != 0 && this._itemSeparatorTemplate != null)
				{
					ListViewContainer listViewContainer = new ListViewContainer();
					this.AutoIDControl(listViewContainer);
					this.InstantiateItemSeparatorTemplate(listViewContainer);
					this.AddControlToContainer(listViewContainer, this._noGroupsItemPlaceholderContainer, num);
					num++;
				}
				ListViewDataItem listViewDataItem = this.CreateDataItem(num3 + dataSource.StartRowIndex, num3);
				this.AutoIDControl(listViewDataItem);
				if (dataBinding)
				{
					listViewDataItem.DataItem = obj;
					OrderedDictionary orderedDictionary = new OrderedDictionary(this.DataKeyNamesInternal.Length);
					foreach (string text in this.DataKeyNamesInternal)
					{
						object propertyValue = DataBinder.GetPropertyValue(obj, text);
						orderedDictionary.Add(text, propertyValue);
					}
					if (keyArray.Count == num3)
					{
						keyArray.Add(new DataKey(orderedDictionary, this.DataKeyNamesInternal));
					}
					else
					{
						keyArray[num3] = new DataKey(orderedDictionary, this.DataKeyNamesInternal);
					}
				}
				if (this.EnablePersistedSelection && num3 < keyArray.Count)
				{
					DataKey currentKey = (DataKey)keyArray[num3];
					this.SetPersistedDataKey(num3, currentKey);
				}
				this.InstantiateItemTemplate(listViewDataItem, num3);
				this.OnItemCreated(new ListViewItemEventArgs(listViewDataItem));
				this.AddControlToContainer(listViewDataItem, this._noGroupsItemPlaceholderContainer, num);
				num++;
				list.Add(listViewDataItem);
				if (dataBinding)
				{
					listViewDataItem.DataBind();
					this.OnItemDataBound(new ListViewItemEventArgs(listViewDataItem));
					listViewDataItem.DataItem = null;
				}
				num3++;
				num2++;
			}
			if (insertPosition == InsertItemPosition.LastItem)
			{
				if (this._itemSeparatorTemplate != null)
				{
					ListViewContainer listViewContainer2 = new ListViewContainer();
					this.AutoIDControl(listViewContainer2);
					this.InstantiateItemSeparatorTemplate(listViewContainer2);
					this.AddControlToContainer(listViewContainer2, this._noGroupsItemPlaceholderContainer, num);
					num++;
				}
				ListViewItem listViewItem2 = this.CreateInsertItem();
				this.AddControlToContainer(listViewItem2, this._noGroupsItemPlaceholderContainer, num);
				listViewItem2.DataBind();
				num++;
				num2++;
			}
			this._noGroupsItemCreatedCount = num - this._noGroupsOriginalIndexOfItemPlaceholderInContainer;
			return list;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001F9C0 File Offset: 0x0001DBC0
		private void ResetPersistedSelectedIndex()
		{
			if (this.EnablePersistedSelection && this._persistedDataKey != null)
			{
				this._selectedIndex = -1;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001F9D9 File Offset: 0x0001DBD9
		private void SetPersistedDataKey(int dataItemIndex, DataKey currentKey)
		{
			if (this._persistedDataKey == null)
			{
				if (this._selectedIndex == dataItemIndex)
				{
					this._persistedDataKey = currentKey;
					return;
				}
			}
			else if (this._persistedDataKey.Equals(currentKey))
			{
				this._selectedIndex = dataItemIndex;
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001FA0C File Offset: 0x0001DC0C
		protected virtual IList<ListViewDataItem> CreateItemsInGroups(ListViewPagedDataSource dataSource, bool dataBinding, InsertItemPosition insertPosition, ArrayList keyArray)
		{
			if (this._groupsOriginalIndexOfGroupPlaceholderInContainer == -1)
			{
				this._groupsGroupPlaceholderContainer = this.GetPreparedContainerInfo(this, false, out this._groupsOriginalIndexOfGroupPlaceholderInContainer);
			}
			int num = this._groupsOriginalIndexOfGroupPlaceholderInContainer;
			this._groupsItemCreatedCount = 0;
			int num2 = 0;
			Control container = null;
			this.ResetPersistedSelectedIndex();
			List<ListViewDataItem> list = new List<ListViewDataItem>();
			int num3 = 0;
			int num4 = 0;
			if (insertPosition == InsertItemPosition.FirstItem)
			{
				ListViewContainer listViewContainer = new ListViewContainer();
				this.AutoIDControl(listViewContainer);
				this.InstantiateGroupTemplate(listViewContainer);
				this.AddControlToContainer(listViewContainer, this._groupsGroupPlaceholderContainer, num);
				num++;
				container = this.GetPreparedContainerInfo(listViewContainer, true, out num2);
				ListViewItem listViewItem = this.CreateInsertItem();
				this.AddControlToContainer(listViewItem, container, num2);
				listViewItem.DataBind();
				num2++;
				num3++;
			}
			foreach (object obj in dataSource)
			{
				if (num3 % this._groupItemCount == 0)
				{
					if (num3 != 0 && this._groupSeparatorTemplate != null)
					{
						ListViewContainer listViewContainer2 = new ListViewContainer();
						this.AutoIDControl(listViewContainer2);
						this.InstantiateGroupSeparatorTemplate(listViewContainer2);
						this.AddControlToContainer(listViewContainer2, this._groupsGroupPlaceholderContainer, num);
						num++;
					}
					ListViewContainer listViewContainer3 = new ListViewContainer();
					this.AutoIDControl(listViewContainer3);
					this.InstantiateGroupTemplate(listViewContainer3);
					this.AddControlToContainer(listViewContainer3, this._groupsGroupPlaceholderContainer, num);
					num++;
					container = this.GetPreparedContainerInfo(listViewContainer3, true, out num2);
				}
				ListViewDataItem listViewDataItem = this.CreateDataItem(num4 + this.StartRowIndex, num4);
				if (dataBinding)
				{
					listViewDataItem.DataItem = obj;
					OrderedDictionary orderedDictionary = new OrderedDictionary(this.DataKeyNamesInternal.Length);
					foreach (string text in this.DataKeyNamesInternal)
					{
						object propertyValue = DataBinder.GetPropertyValue(obj, text);
						orderedDictionary.Add(text, propertyValue);
					}
					if (keyArray.Count == num4)
					{
						keyArray.Add(new DataKey(orderedDictionary, this.DataKeyNamesInternal));
					}
					else
					{
						keyArray[num4] = new DataKey(orderedDictionary, this.DataKeyNamesInternal);
					}
				}
				if (this.EnablePersistedSelection && num4 < keyArray.Count)
				{
					DataKey currentKey = (DataKey)keyArray[num4];
					this.SetPersistedDataKey(num4, currentKey);
				}
				this.InstantiateItemTemplate(listViewDataItem, num4);
				this.OnItemCreated(new ListViewItemEventArgs(listViewDataItem));
				if (num3 % this._groupItemCount != 0 && this._itemSeparatorTemplate != null)
				{
					ListViewContainer listViewContainer4 = new ListViewContainer();
					this.InstantiateItemSeparatorTemplate(listViewContainer4);
					this.AddControlToContainer(listViewContainer4, container, num2);
					num2++;
				}
				this.AddControlToContainer(listViewDataItem, container, num2);
				num2++;
				list.Add(listViewDataItem);
				if (dataBinding)
				{
					listViewDataItem.DataBind();
					this.OnItemDataBound(new ListViewItemEventArgs(listViewDataItem));
					listViewDataItem.DataItem = null;
				}
				num3++;
				num4++;
			}
			if (insertPosition == InsertItemPosition.LastItem)
			{
				if (num3 % this._groupItemCount == 0)
				{
					if (num3 != 0 && this._groupSeparatorTemplate != null)
					{
						ListViewContainer listViewContainer5 = new ListViewContainer();
						this.AutoIDControl(listViewContainer5);
						this.InstantiateGroupSeparatorTemplate(listViewContainer5);
						this.AddControlToContainer(listViewContainer5, this._groupsGroupPlaceholderContainer, num);
						num++;
					}
					ListViewContainer listViewContainer6 = new ListViewContainer();
					this.AutoIDControl(listViewContainer6);
					this.InstantiateGroupTemplate(listViewContainer6);
					this.AddControlToContainer(listViewContainer6, this._groupsGroupPlaceholderContainer, num);
					num++;
					container = this.GetPreparedContainerInfo(listViewContainer6, true, out num2);
				}
				if (num3 % this._groupItemCount != 0 && this._itemSeparatorTemplate != null)
				{
					ListViewContainer listViewContainer7 = new ListViewContainer();
					this.InstantiateItemSeparatorTemplate(listViewContainer7);
					this.AddControlToContainer(listViewContainer7, container, num2);
					num2++;
				}
				ListViewItem listViewItem2 = this.CreateInsertItem();
				this.AddControlToContainer(listViewItem2, container, num2);
				listViewItem2.DataBind();
				num2++;
				num3++;
			}
			if (this._emptyItemTemplate != null)
			{
				while (num3 % this._groupItemCount != 0)
				{
					if (this._itemSeparatorTemplate != null)
					{
						ListViewContainer listViewContainer8 = new ListViewContainer();
						this.InstantiateItemSeparatorTemplate(listViewContainer8);
						this.AddControlToContainer(listViewContainer8, container, num2);
						num2++;
					}
					ListViewItem control = this.CreateEmptyItem();
					this.AddControlToContainer(control, container, num2);
					num2++;
					num3++;
				}
			}
			this._groupsItemCreatedCount = num - this._groupsOriginalIndexOfGroupPlaceholderInContainer;
			return list;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001FE10 File Offset: 0x0001E010
		protected virtual void CreateSuffixArrayList(ListViewPagedDataSource dataSource, ArrayList suffixArray)
		{
			int num = 0;
			foreach (object container in dataSource)
			{
				OrderedDictionary orderedDictionary = new OrderedDictionary(this.ClientIDRowSuffixInternal.Length);
				foreach (string text in this.ClientIDRowSuffixInternal)
				{
					object propertyValue = DataBinder.GetPropertyValue(container, text);
					orderedDictionary.Add(text, propertyValue);
				}
				if (suffixArray.Count == num)
				{
					suffixArray.Add(new DataKey(orderedDictionary, this.ClientIDRowSuffixInternal));
				}
				else
				{
					suffixArray[num] = new DataKey(orderedDictionary, this.ClientIDRowSuffixInternal);
				}
				num++;
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001FEDC File Offset: 0x0001E0DC
		protected virtual void CreateLayoutTemplate()
		{
			this._noGroupsOriginalIndexOfItemPlaceholderInContainer = -1;
			this._noGroupsItemCreatedCount = 0;
			this._noGroupsItemPlaceholderContainer = null;
			this._groupsOriginalIndexOfGroupPlaceholderInContainer = -1;
			this._groupsItemCreatedCount = 0;
			this._groupsGroupPlaceholderContainer = null;
			Control control = new Control();
			if (this._layoutTemplate != null)
			{
				this._layoutTemplate.InstantiateIn(control);
				this.Controls.Add(control);
			}
			this.OnLayoutCreated(new EventArgs());
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001FF44 File Offset: 0x0001E144
		private ListViewPagedDataSource CreatePagedDataSource()
		{
			return new ListViewPagedDataSource
			{
				StartRowIndex = this._startRowIndex,
				MaximumRows = this._maximumRows,
				AllowServerPaging = false,
				TotalRowCount = 0
			};
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001FF80 File Offset: 0x0001E180
		private ListViewPagedDataSource CreateServerPagedDataSource(int totalRowCount)
		{
			return new ListViewPagedDataSource
			{
				StartRowIndex = this._startRowIndex,
				MaximumRows = this._maximumRows,
				AllowServerPaging = true,
				TotalRowCount = totalRowCount
			};
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001FFBA File Offset: 0x0001E1BA
		public virtual void DeleteItem(int itemIndex)
		{
			this.ResetModelValidationGroup(this.EnableModelValidation, string.Empty);
			this.HandleDelete(null, itemIndex);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001FFD5 File Offset: 0x0001E1D5
		protected virtual void EnsureLayoutTemplate()
		{
			if (this.Controls.Count == 0 || this._instantiatedEmptyDataTemplate)
			{
				this.Controls.Clear();
				this.CreateLayoutTemplate();
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00020000 File Offset: 0x0001E200
		public virtual void ExtractItemValues(IOrderedDictionary itemValues, ListViewItem item, bool includePrimaryKey)
		{
			if (itemValues == null)
			{
				throw new ArgumentNullException("itemValues");
			}
			DataBoundControlHelper.ExtractValuesFromBindableControls(itemValues, item);
			IBindableTemplate bindableTemplate = null;
			if (item.ItemType == ListViewItemType.DataItem)
			{
				ListViewDataItem listViewDataItem = item as ListViewDataItem;
				if (listViewDataItem == null)
				{
					throw new InvalidOperationException(AtlasWeb.ListView_ItemsNotDataItems);
				}
				if (listViewDataItem.DisplayIndex == this.EditIndex)
				{
					bindableTemplate = (this.EditItemTemplate as IBindableTemplate);
				}
				else if (listViewDataItem.DisplayIndex == this.SelectedIndex)
				{
					bindableTemplate = (this.SelectedItemTemplate as IBindableTemplate);
				}
				else if (listViewDataItem.DisplayIndex % 2 == 1 && this.AlternatingItemTemplate != null)
				{
					bindableTemplate = (this.AlternatingItemTemplate as IBindableTemplate);
				}
				else
				{
					bindableTemplate = (this.ItemTemplate as IBindableTemplate);
				}
			}
			else if (item.ItemType == ListViewItemType.InsertItem && this.InsertItemTemplate != null)
			{
				bindableTemplate = (this.InsertItemTemplate as IBindableTemplate);
			}
			if (bindableTemplate != null)
			{
				OrderedDictionary orderedDictionary = new OrderedDictionary();
				bool convertEmptyStringToNull = this.ConvertEmptyStringToNull;
				foreach (object obj in bindableTemplate.ExtractValues(item))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					object value = dictionaryEntry.Value;
					if (convertEmptyStringToNull && value is string && ((string)value).Length == 0)
					{
						orderedDictionary[dictionaryEntry.Key] = null;
					}
					else
					{
						orderedDictionary[dictionaryEntry.Key] = value;
					}
				}
				foreach (object obj2 in orderedDictionary)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (!includePrimaryKey)
					{
						object[] dataKeyNamesInternal = this.DataKeyNamesInternal;
						if (Array.IndexOf<object>(dataKeyNamesInternal, dictionaryEntry2.Key) != -1)
						{
							continue;
						}
					}
					itemValues[dictionaryEntry2.Key] = dictionaryEntry2.Value;
				}
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000201DC File Offset: 0x0001E3DC
		protected virtual Control FindPlaceholder(string containerID, Control container)
		{
			return container.FindControl(containerID);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000201E8 File Offset: 0x0001E3E8
		private DataPager FindDataPager(Control control)
		{
			foreach (object obj in control.Controls)
			{
				Control control2 = (Control)obj;
				DataPager dataPager = control2 as DataPager;
				if (dataPager != null)
				{
					return dataPager;
				}
			}
			foreach (object obj2 in control.Controls)
			{
				Control control3 = (Control)obj2;
				if (control3 is IPageableItemContainer)
				{
					return null;
				}
				DataPager dataPager2 = this.FindDataPager(control3);
				if (dataPager2 != null)
				{
					return dataPager2;
				}
			}
			return null;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000202B8 File Offset: 0x0001E4B8
		private int GetItemIndex(ListViewItem item, string commandArgument)
		{
			if (item == null)
			{
				return Convert.ToInt32(commandArgument, CultureInfo.InvariantCulture);
			}
			ListViewDataItem listViewDataItem = item as ListViewDataItem;
			if (listViewDataItem != null)
			{
				return listViewDataItem.DisplayIndex;
			}
			return -1;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000202E8 File Offset: 0x0001E4E8
		private bool TryGetItemIndex(ListViewItem item, string commandArgument, out int itemIndex)
		{
			if (item != null)
			{
				ListViewDataItem listViewDataItem = item as ListViewDataItem;
				itemIndex = ((listViewDataItem != null) ? listViewDataItem.DisplayIndex : -1);
				return true;
			}
			return int.TryParse(commandArgument, NumberStyles.Integer, CultureInfo.InvariantCulture, out itemIndex);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002031C File Offset: 0x0001E51C
		private Control GetPreparedContainerInfo(Control outerContainer, bool isItem, out int placeholderIndex)
		{
			string text = isItem ? this.ItemPlaceholderID : this.GroupPlaceholderID;
			Control control = this.FindPlaceholder(text, outerContainer);
			if (control == null)
			{
				if (this._layoutTemplate == null)
				{
					control = new PlaceHolder();
					control.ID = text;
				}
				if (isItem)
				{
					if (this._layoutTemplate != null || this._groupTemplate != null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_NoItemPlaceholder, new object[]
						{
							this.ID,
							this.ItemPlaceholderID
						}));
					}
				}
				else if (this._layoutTemplate != null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_NoGroupPlaceholder, new object[]
					{
						this.ID,
						this.GroupPlaceholderID
					}));
				}
				this.Controls.Add(control);
			}
			Control parent = control.Parent;
			placeholderIndex = parent.Controls.IndexOf(control);
			parent.Controls.Remove(control);
			return parent;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00020404 File Offset: 0x0001E604
		private void HandleCancel(int itemIndex)
		{
			ListViewCancelMode cancelMode = ListViewCancelMode.CancelingInsert;
			if (itemIndex == this.EditIndex && itemIndex >= 0)
			{
				cancelMode = ListViewCancelMode.CancelingEdit;
			}
			else if (itemIndex != -1)
			{
				throw new InvalidOperationException(AtlasWeb.ListView_InvalidCancel);
			}
			ListViewCancelEventArgs listViewCancelEventArgs = new ListViewCancelEventArgs(itemIndex, cancelMode);
			this.OnItemCanceling(listViewCancelEventArgs);
			if (listViewCancelEventArgs.Cancel)
			{
				return;
			}
			if (base.IsDataBindingAutomatic && listViewCancelEventArgs.CancelMode == ListViewCancelMode.CancelingEdit)
			{
				this.EditIndex = -1;
			}
			base.RequiresDataBinding = true;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0002046C File Offset: 0x0001E66C
		private void HandleDelete(ListViewItem item, int itemIndex)
		{
			ListViewDataItem listViewDataItem = item as ListViewDataItem;
			if (itemIndex < 0 && listViewDataItem == null)
			{
				throw new InvalidOperationException(AtlasWeb.ListView_InvalidDelete);
			}
			DataSourceView dataSourceView = null;
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			if (isDataBindingAutomatic)
			{
				dataSourceView = this.GetData();
				if (dataSourceView == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_NullView, new object[]
					{
						this.ID
					}));
				}
			}
			if (item == null && itemIndex < this.Items.Count)
			{
				item = this.Items[itemIndex];
			}
			ListViewDeleteEventArgs listViewDeleteEventArgs = new ListViewDeleteEventArgs(itemIndex);
			if (item != null)
			{
				this.ExtractItemValues(listViewDeleteEventArgs.Values, item, false);
			}
			if (this.DataKeys.Count > itemIndex)
			{
				foreach (object obj in this.DataKeys[itemIndex].Values)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					listViewDeleteEventArgs.Keys.Add(dictionaryEntry.Key, dictionaryEntry.Value);
					if (listViewDeleteEventArgs.Values.Contains(dictionaryEntry.Key))
					{
						listViewDeleteEventArgs.Values.Remove(dictionaryEntry.Key);
					}
				}
			}
			this.OnItemDeleting(listViewDeleteEventArgs);
			if (listViewDeleteEventArgs.Cancel)
			{
				return;
			}
			this._deletedItemIndex = itemIndex;
			if (isDataBindingAutomatic)
			{
				this._deleteKeys = listViewDeleteEventArgs.Keys;
				this._deleteValues = listViewDeleteEventArgs.Values;
				dataSourceView.Delete(listViewDeleteEventArgs.Keys, listViewDeleteEventArgs.Values, new DataSourceViewOperationCallback(this.HandleDeleteCallback));
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000205FC File Offset: 0x0001E7FC
		private bool HandleDeleteCallback(int affectedRows, Exception ex)
		{
			ListViewDeletedEventArgs listViewDeletedEventArgs = new ListViewDeletedEventArgs(affectedRows, ex);
			listViewDeletedEventArgs.SetKeys(this._deleteKeys);
			listViewDeletedEventArgs.SetValues(this._deleteValues);
			this.OnItemDeleted(listViewDeletedEventArgs);
			this._deleteKeys = null;
			this._deleteValues = null;
			if (ex != null && !listViewDeletedEventArgs.ExceptionHandled && this.PageIsValidAfterModelException())
			{
				return false;
			}
			this.EditIndex = -1;
			if (affectedRows > 0 && this._totalRowCount > 0 && this._deletedItemIndex == this.SelectedIndex && this._deletedItemIndex + this._startRowIndex == this._totalRowCount)
			{
				int selectedIndex = this.SelectedIndex;
				this.SelectedIndex = selectedIndex - 1;
			}
			this._deletedItemIndex = -1;
			base.RequiresDataBinding = true;
			return true;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000206AC File Offset: 0x0001E8AC
		private void HandleEdit(int itemIndex)
		{
			if (itemIndex < 0)
			{
				throw new InvalidOperationException(AtlasWeb.ListView_InvalidEdit);
			}
			ListViewEditEventArgs listViewEditEventArgs = new ListViewEditEventArgs(itemIndex);
			this.OnItemEditing(listViewEditEventArgs);
			if (listViewEditEventArgs.Cancel)
			{
				return;
			}
			this.EditIndex = listViewEditEventArgs.NewEditIndex;
			base.RequiresDataBinding = true;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000206F4 File Offset: 0x0001E8F4
		private bool HandleEvent(EventArgs e, bool causesValidation, string validationGroup)
		{
			bool result = false;
			this.ResetModelValidationGroup(causesValidation, validationGroup);
			ListViewCommandEventArgs listViewCommandEventArgs = e as ListViewCommandEventArgs;
			if (listViewCommandEventArgs != null)
			{
				this.OnItemCommand(listViewCommandEventArgs);
				if (listViewCommandEventArgs.Handled)
				{
					return true;
				}
				result = true;
				string commandName = listViewCommandEventArgs.CommandName;
				int itemIndex;
				if (string.Equals(commandName, "Select", StringComparison.OrdinalIgnoreCase))
				{
					this.HandleSelect(this.GetItemIndex(listViewCommandEventArgs.Item, (string)listViewCommandEventArgs.CommandArgument));
				}
				else if (string.Equals(commandName, "Sort", StringComparison.OrdinalIgnoreCase))
				{
					this.HandleSort((string)listViewCommandEventArgs.CommandArgument);
				}
				else if (string.Equals(commandName, "Edit", StringComparison.OrdinalIgnoreCase))
				{
					this.HandleEdit(this.GetItemIndex(listViewCommandEventArgs.Item, (string)listViewCommandEventArgs.CommandArgument));
				}
				else if (string.Equals(commandName, "Cancel", StringComparison.OrdinalIgnoreCase))
				{
					this.HandleCancel(this.GetItemIndex(listViewCommandEventArgs.Item, (string)listViewCommandEventArgs.CommandArgument));
				}
				else if (string.Equals(commandName, "Update", StringComparison.OrdinalIgnoreCase))
				{
					this.HandleUpdate(listViewCommandEventArgs.Item, this.GetItemIndex(listViewCommandEventArgs.Item, (string)listViewCommandEventArgs.CommandArgument), causesValidation);
				}
				else if (string.Equals(commandName, "Delete", StringComparison.OrdinalIgnoreCase))
				{
					this.HandleDelete(listViewCommandEventArgs.Item, this.GetItemIndex(listViewCommandEventArgs.Item, (string)listViewCommandEventArgs.CommandArgument));
				}
				else if (string.Equals(commandName, "Insert", StringComparison.OrdinalIgnoreCase))
				{
					this.HandleInsert(listViewCommandEventArgs.Item, causesValidation);
				}
				else if (this.TryGetItemIndex(listViewCommandEventArgs.Item, (string)listViewCommandEventArgs.CommandArgument, out itemIndex))
				{
					result = this.HandleCommand(listViewCommandEventArgs.Item, itemIndex, commandName);
				}
			}
			return result;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00020898 File Offset: 0x0001EA98
		private bool HandleCommand(ListViewItem item, int itemIndex, string commandName)
		{
			DataSourceView dataSourceView = null;
			if (!base.IsDataBindingAutomatic)
			{
				return false;
			}
			dataSourceView = this.GetData();
			if (dataSourceView == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_NullView, new object[]
				{
					this.ID
				}));
			}
			if (!dataSourceView.CanExecute(commandName))
			{
				return false;
			}
			ListViewDataItem listViewDataItem = item as ListViewDataItem;
			if (itemIndex < 0 && listViewDataItem == null)
			{
				throw new InvalidOperationException(AtlasWeb.ListView_InvalidCommand);
			}
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			OrderedDictionary orderedDictionary2 = new OrderedDictionary();
			if (item != null)
			{
				this.ExtractItemValues(orderedDictionary, item, false);
			}
			if (this.DataKeys.Count > itemIndex)
			{
				foreach (object obj in this.DataKeys[itemIndex].Values)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					orderedDictionary2.Add(dictionaryEntry.Key, dictionaryEntry.Value);
					if (orderedDictionary.Contains(dictionaryEntry.Key))
					{
						orderedDictionary.Remove(dictionaryEntry.Key);
					}
				}
			}
			dataSourceView.ExecuteCommand(commandName, orderedDictionary2, orderedDictionary, new DataSourceViewOperationCallback(this.HandleCommandCallback));
			return true;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000209C8 File Offset: 0x0001EBC8
		private bool HandleCommandCallback(int affectedRows, Exception ex)
		{
			if (ex != null && this.PageIsValidAfterModelException())
			{
				return false;
			}
			this.EditIndex = -1;
			base.RequiresDataBinding = true;
			return true;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000209E8 File Offset: 0x0001EBE8
		private void HandleInsert(ListViewItem item, bool causesValidation)
		{
			if (item != null && item.ItemType != ListViewItemType.InsertItem)
			{
				throw new InvalidOperationException(AtlasWeb.ListView_InvalidInsert);
			}
			if (causesValidation && this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			if (item == null)
			{
				item = this._insertItem;
			}
			if (item == null)
			{
				throw new InvalidOperationException(AtlasWeb.ListView_NoInsertItem);
			}
			DataSourceView dataSourceView = null;
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			if (isDataBindingAutomatic)
			{
				dataSourceView = this.GetData();
				if (dataSourceView == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_NullView, new object[]
					{
						this.ID
					}));
				}
			}
			ListViewInsertEventArgs listViewInsertEventArgs = new ListViewInsertEventArgs(item);
			this.ExtractItemValues(listViewInsertEventArgs.Values, item, true);
			this.OnItemInserting(listViewInsertEventArgs);
			if (listViewInsertEventArgs.Cancel)
			{
				return;
			}
			if (isDataBindingAutomatic)
			{
				this._insertValues = listViewInsertEventArgs.Values;
				dataSourceView.Insert(listViewInsertEventArgs.Values, new DataSourceViewOperationCallback(this.HandleInsertCallback));
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		private bool HandleInsertCallback(int affectedRows, Exception ex)
		{
			ListViewInsertedEventArgs listViewInsertedEventArgs = new ListViewInsertedEventArgs(affectedRows, ex);
			listViewInsertedEventArgs.SetValues(this._insertValues);
			this.OnItemInserted(listViewInsertedEventArgs);
			this._insertValues = null;
			if (ex != null && !listViewInsertedEventArgs.ExceptionHandled)
			{
				if (this.PageIsValidAfterModelException())
				{
					return false;
				}
				listViewInsertedEventArgs.KeepInInsertMode = true;
			}
			if (this.IsUsingModelBinders && !this.Page.ModelState.IsValid)
			{
				listViewInsertedEventArgs.KeepInInsertMode = true;
			}
			if (!listViewInsertedEventArgs.KeepInInsertMode)
			{
				base.RequiresDataBinding = true;
			}
			return true;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00020B40 File Offset: 0x0001ED40
		private void HandleSelect(int itemIndex)
		{
			if (itemIndex < 0)
			{
				throw new InvalidOperationException(AtlasWeb.ListView_InvalidSelect);
			}
			ListViewSelectEventArgs listViewSelectEventArgs = new ListViewSelectEventArgs(itemIndex);
			this.OnSelectedIndexChanging(listViewSelectEventArgs);
			if (listViewSelectEventArgs.Cancel)
			{
				return;
			}
			this.SelectedIndex = listViewSelectEventArgs.NewSelectedIndex;
			this.OnSelectedIndexChanged(EventArgs.Empty);
			base.RequiresDataBinding = true;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00020B94 File Offset: 0x0001ED94
		private void HandleSort(string sortExpression)
		{
			SortDirection sortDirection = SortDirection.Ascending;
			if (this.SortExpressionInternal == sortExpression && this.SortDirectionInternal == SortDirection.Ascending)
			{
				sortDirection = SortDirection.Descending;
			}
			this.HandleSort(sortExpression, sortDirection);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00020BC4 File Offset: 0x0001EDC4
		private void HandleSort(string sortExpression, SortDirection sortDirection)
		{
			ListViewSortEventArgs listViewSortEventArgs = new ListViewSortEventArgs(sortExpression, sortDirection);
			this.OnSorting(listViewSortEventArgs);
			if (listViewSortEventArgs.Cancel)
			{
				return;
			}
			if (base.IsDataBindingAutomatic)
			{
				this.ClearDataKeys();
				if (this.GetData() == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_NullView, new object[]
					{
						this.ID
					}));
				}
				this.EditIndex = -1;
				this.SortExpressionInternal = listViewSortEventArgs.SortExpression;
				this.SortDirectionInternal = listViewSortEventArgs.SortDirection;
				this._startRowIndex = 0;
			}
			this.OnSorted(EventArgs.Empty);
			base.RequiresDataBinding = true;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00020C60 File Offset: 0x0001EE60
		private void HandleUpdate(ListViewItem item, int itemIndex, bool causesValidation)
		{
			ListViewDataItem listViewDataItem = item as ListViewDataItem;
			if (itemIndex < 0 && listViewDataItem == null)
			{
				throw new InvalidOperationException(AtlasWeb.ListView_InvalidUpdate);
			}
			if (causesValidation && this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			DataSourceView dataSourceView = null;
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			if (isDataBindingAutomatic)
			{
				dataSourceView = this.GetData();
				if (dataSourceView == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_NullView, new object[]
					{
						this.ID
					}));
				}
			}
			ListViewUpdateEventArgs listViewUpdateEventArgs = new ListViewUpdateEventArgs(itemIndex);
			foreach (object obj in this.BoundFieldValues)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				listViewUpdateEventArgs.OldValues.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
			if (this.DataKeys.Count > itemIndex)
			{
				foreach (object obj2 in this.DataKeys[itemIndex].Values)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					listViewUpdateEventArgs.Keys.Add(dictionaryEntry2.Key, dictionaryEntry2.Value);
				}
			}
			if (listViewDataItem == null && this.Items.Count > itemIndex)
			{
				listViewDataItem = this.Items[itemIndex];
			}
			if (listViewDataItem != null)
			{
				this.ExtractItemValues(listViewUpdateEventArgs.NewValues, listViewDataItem, true);
			}
			this.OnItemUpdating(listViewUpdateEventArgs);
			if (listViewUpdateEventArgs.Cancel)
			{
				return;
			}
			if (isDataBindingAutomatic)
			{
				this._updateKeys = listViewUpdateEventArgs.Keys;
				this._updateOldValues = listViewUpdateEventArgs.OldValues;
				this._updateNewValues = listViewUpdateEventArgs.NewValues;
				dataSourceView.Update(listViewUpdateEventArgs.Keys, listViewUpdateEventArgs.NewValues, listViewUpdateEventArgs.OldValues, new DataSourceViewOperationCallback(this.HandleUpdateCallback));
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00020E4C File Offset: 0x0001F04C
		private bool HandleUpdateCallback(int affectedRows, Exception ex)
		{
			ListViewUpdatedEventArgs listViewUpdatedEventArgs = new ListViewUpdatedEventArgs(affectedRows, ex);
			listViewUpdatedEventArgs.SetKeys(this._updateKeys);
			listViewUpdatedEventArgs.SetOldValues(this._updateOldValues);
			listViewUpdatedEventArgs.SetNewValues(this._updateNewValues);
			this.OnItemUpdated(listViewUpdatedEventArgs);
			this._updateKeys = null;
			this._updateOldValues = null;
			this._updateNewValues = null;
			if (ex != null && !listViewUpdatedEventArgs.ExceptionHandled)
			{
				if (this.PageIsValidAfterModelException())
				{
					return false;
				}
				listViewUpdatedEventArgs.KeepInEditMode = true;
			}
			if (this.IsUsingModelBinders && !this.Page.ModelState.IsValid)
			{
				listViewUpdatedEventArgs.KeepInEditMode = true;
			}
			if (!listViewUpdatedEventArgs.KeepInEditMode)
			{
				this.EditIndex = -1;
				base.RequiresDataBinding = true;
			}
			return true;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00020EF5 File Offset: 0x0001F0F5
		public virtual void InsertNewItem(bool causesValidation)
		{
			this.ResetModelValidationGroup(causesValidation, string.Empty);
			this.HandleInsert(null, causesValidation);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00020F0B File Offset: 0x0001F10B
		protected virtual void InstantiateEmptyDataTemplate(Control container)
		{
			if (this._emptyDataTemplate != null)
			{
				this._emptyDataTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00020F21 File Offset: 0x0001F121
		protected virtual void InstantiateEmptyItemTemplate(Control container)
		{
			if (this._emptyItemTemplate != null)
			{
				this._emptyItemTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00020F37 File Offset: 0x0001F137
		protected virtual void InstantiateGroupTemplate(Control container)
		{
			if (this._groupTemplate != null)
			{
				this._groupTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00020F4D File Offset: 0x0001F14D
		protected virtual void InstantiateGroupSeparatorTemplate(Control container)
		{
			if (this._groupSeparatorTemplate != null)
			{
				this._groupSeparatorTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00020F63 File Offset: 0x0001F163
		protected virtual void InstantiateInsertItemTemplate(Control container)
		{
			if (this._insertItemTemplate != null)
			{
				this._insertItemTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00020F79 File Offset: 0x0001F179
		protected virtual void InstantiateItemSeparatorTemplate(Control container)
		{
			if (this._itemSeparatorTemplate != null)
			{
				this._itemSeparatorTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00020F90 File Offset: 0x0001F190
		protected virtual void InstantiateItemTemplate(Control container, int displayIndex)
		{
			ITemplate template = this._itemTemplate;
			if (template == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_ItemTemplateRequired, new object[]
				{
					this.ID
				}));
			}
			if (displayIndex % 2 == 1 && this._alternatingItemTemplate != null)
			{
				template = this._alternatingItemTemplate;
			}
			if (displayIndex == this._selectedIndex && this._selectedItemTemplate != null)
			{
				template = this._selectedItemTemplate;
			}
			if (displayIndex == this._editIndex && this._editItemTemplate != null)
			{
				template = this._editItemTemplate;
			}
			template.InstantiateIn(container);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00021018 File Offset: 0x0001F218
		protected internal override void LoadControlState(object savedState)
		{
			this._startRowIndex = 0;
			this._maximumRows = -1;
			this._editIndex = -1;
			this._selectedIndex = -1;
			this._groupItemCount = 1;
			this._sortExpression = string.Empty;
			this._sortDirection = SortDirection.Ascending;
			this._dataKeyNames = new string[0];
			object[] array = savedState as object[];
			if (array != null)
			{
				base.LoadControlState(array[0]);
				if (array[1] != null)
				{
					this._editIndex = (int)array[1];
				}
				if (array[2] != null)
				{
					this._selectedIndex = (int)array[2];
				}
				if (array[3] != null)
				{
					this._groupItemCount = (int)array[3];
				}
				if (array[4] != null)
				{
					this._sortExpression = (string)array[4];
				}
				if (array[5] != null)
				{
					this._sortDirection = (SortDirection)array[5];
				}
				if (array[6] != null)
				{
					this._dataKeyNames = (string[])array[6];
				}
				if (array[7] != null)
				{
					this.LoadDataKeysState(array[7]);
				}
				if (array[8] != null)
				{
					this._totalRowCount = (int)array[8];
				}
				if (array[9] != null && this._dataKeyNames != null && this._dataKeyNames.Length != 0)
				{
					this._persistedDataKey = new DataKey(new OrderedDictionary(this._dataKeyNames.Length), this._dataKeyNames);
					((IStateManager)this._persistedDataKey).LoadViewState(array[9]);
				}
				if (array[10] != null)
				{
					this._clientIDRowSuffix = (string[])array[10];
				}
				if (array[11] != null)
				{
					this.LoadClientIDRowSuffixDataKeysState(array[11]);
				}
				if (array[12] != null)
				{
					this._startRowIndex = (int)array[12];
				}
				if (array[13] != null)
				{
					this._maximumRows = (int)array[13];
				}
			}
			else
			{
				base.LoadControlState(null);
			}
			if (!base.IsViewStateEnabled)
			{
				this.OnTotalRowCountAvailable(new PageEventArgs(this._startRowIndex, this._maximumRows, this._totalRowCount));
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000211D0 File Offset: 0x0001F3D0
		private void LoadDataKeysState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				string[] dataKeyNamesInternal = this.DataKeyNamesInternal;
				int capacity = dataKeyNamesInternal.Length;
				this.ClearDataKeys();
				for (int i = 0; i < array.Length; i++)
				{
					this.DataKeysArrayList.Add(new DataKey(new OrderedDictionary(capacity), dataKeyNamesInternal));
					((IStateManager)this.DataKeysArrayList[i]).LoadViewState(array[i]);
				}
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00021238 File Offset: 0x0001F438
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				base.LoadViewState(array[0]);
				if (array[1] != null)
				{
					OrderedDictionaryStateHelper.LoadViewState((OrderedDictionary)this.BoundFieldValues, (ArrayList)array[1]);
					return;
				}
			}
			else
			{
				base.LoadViewState(savedState);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00021280 File Offset: 0x0001F480
		private void LoadClientIDRowSuffixDataKeysState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				string[] clientIDRowSuffixInternal = this.ClientIDRowSuffixInternal;
				int capacity = clientIDRowSuffixInternal.Length;
				this._clientIDRowSuffixArrayList = null;
				for (int i = 0; i < array.Length; i++)
				{
					this.ClientIDRowSuffixArrayList.Add(new DataKey(new OrderedDictionary(capacity), clientIDRowSuffixInternal));
					((IStateManager)this.ClientIDRowSuffixArrayList[i]).LoadViewState(array[i]);
				}
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000212E8 File Offset: 0x0001F4E8
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			bool causesValidation = false;
			string validationGroup = string.Empty;
			ListViewCommandEventArgs listViewCommandEventArgs = e as ListViewCommandEventArgs;
			if (listViewCommandEventArgs == null && e is CommandEventArgs)
			{
				listViewCommandEventArgs = new ListViewCommandEventArgs(new ListViewItem(ListViewItemType.EmptyItem), source, (CommandEventArgs)e);
			}
			if (listViewCommandEventArgs != null)
			{
				IButtonControl buttonControl = listViewCommandEventArgs.CommandSource as IButtonControl;
				if (buttonControl != null)
				{
					causesValidation = buttonControl.CausesValidation;
					validationGroup = buttonControl.ValidationGroup;
				}
			}
			return this.HandleEvent(listViewCommandEventArgs, causesValidation, validationGroup);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002134C File Offset: 0x0001F54C
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				if (this.DataKeyNames.Length != 0)
				{
					this.Page.RegisterRequiresViewStateEncryption();
				}
				this.Page.RegisterRequiresControlState(this);
			}
			if (!base.DesignMode && !string.IsNullOrEmpty(this.ItemType))
			{
				DataBoundControlHelper.EnableDynamicData(this, this.ItemType);
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000213AC File Offset: 0x0001F5AC
		protected virtual void OnItemCanceling(ListViewCancelEventArgs e)
		{
			EventHandler<ListViewCancelEventArgs> eventHandler = (EventHandler<ListViewCancelEventArgs>)base.Events[ListView.EventItemCanceling];
			if (eventHandler != null)
			{
				eventHandler(this, e);
				return;
			}
			if (!base.IsDataBindingAutomatic && !e.Cancel)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_UnhandledEvent, new object[]
				{
					this.ID,
					"ItemCanceling"
				}));
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00021418 File Offset: 0x0001F618
		protected virtual void OnItemCommand(ListViewCommandEventArgs e)
		{
			EventHandler<ListViewCommandEventArgs> eventHandler = (EventHandler<ListViewCommandEventArgs>)base.Events[ListView.EventItemCommand];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00021448 File Offset: 0x0001F648
		protected virtual void OnItemCreated(ListViewItemEventArgs e)
		{
			EventHandler<ListViewItemEventArgs> eventHandler = (EventHandler<ListViewItemEventArgs>)base.Events[ListView.EventItemCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00021478 File Offset: 0x0001F678
		protected virtual void OnItemDataBound(ListViewItemEventArgs e)
		{
			EventHandler<ListViewItemEventArgs> eventHandler = (EventHandler<ListViewItemEventArgs>)base.Events[ListView.EventItemDataBound];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			EventHandler<WizardSideBarListControlItemEventArgs> eventHandler2 = (EventHandler<WizardSideBarListControlItemEventArgs>)base.Events[ListView.EventWizardListItemDataBound];
			if (eventHandler2 != null)
			{
				ListViewItem item = e.Item;
				WizardSideBarListControlItemEventArgs e2 = new WizardSideBarListControlItemEventArgs(new WizardSideBarListControlItem(item.DataItem, ListItemType.Item, item.DataItemIndex, item));
				eventHandler2(this, e2);
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000214E8 File Offset: 0x0001F6E8
		protected virtual void OnItemDeleted(ListViewDeletedEventArgs e)
		{
			EventHandler<ListViewDeletedEventArgs> eventHandler = (EventHandler<ListViewDeletedEventArgs>)base.Events[ListView.EventItemDeleted];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00021518 File Offset: 0x0001F718
		protected virtual void OnItemDeleting(ListViewDeleteEventArgs e)
		{
			EventHandler<ListViewDeleteEventArgs> eventHandler = (EventHandler<ListViewDeleteEventArgs>)base.Events[ListView.EventItemDeleting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
				return;
			}
			if (!base.IsDataBindingAutomatic && !e.Cancel)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_UnhandledEvent, new object[]
				{
					this.ID,
					"ItemDeleting"
				}));
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00021584 File Offset: 0x0001F784
		protected virtual void OnItemEditing(ListViewEditEventArgs e)
		{
			EventHandler<ListViewEditEventArgs> eventHandler = (EventHandler<ListViewEditEventArgs>)base.Events[ListView.EventItemEditing];
			if (eventHandler != null)
			{
				eventHandler(this, e);
				return;
			}
			if (!base.IsDataBindingAutomatic && !e.Cancel)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_UnhandledEvent, new object[]
				{
					this.ID,
					"ItemEditing"
				}));
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000215F0 File Offset: 0x0001F7F0
		protected virtual void OnItemInserted(ListViewInsertedEventArgs e)
		{
			EventHandler<ListViewInsertedEventArgs> eventHandler = (EventHandler<ListViewInsertedEventArgs>)base.Events[ListView.EventItemInserted];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00021620 File Offset: 0x0001F820
		protected virtual void OnItemInserting(ListViewInsertEventArgs e)
		{
			EventHandler<ListViewInsertEventArgs> eventHandler = (EventHandler<ListViewInsertEventArgs>)base.Events[ListView.EventItemInserting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
				return;
			}
			if (!base.IsDataBindingAutomatic && !e.Cancel)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_UnhandledEvent, new object[]
				{
					this.ID,
					"ItemInserting"
				}));
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002168C File Offset: 0x0001F88C
		protected virtual void OnItemUpdated(ListViewUpdatedEventArgs e)
		{
			EventHandler<ListViewUpdatedEventArgs> eventHandler = (EventHandler<ListViewUpdatedEventArgs>)base.Events[ListView.EventItemUpdated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000216BC File Offset: 0x0001F8BC
		protected virtual void OnItemUpdating(ListViewUpdateEventArgs e)
		{
			EventHandler<ListViewUpdateEventArgs> eventHandler = (EventHandler<ListViewUpdateEventArgs>)base.Events[ListView.EventItemUpdating];
			if (eventHandler != null)
			{
				eventHandler(this, e);
				return;
			}
			if (!base.IsDataBindingAutomatic && !e.Cancel)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_UnhandledEvent, new object[]
				{
					this.ID,
					"ItemUpdating"
				}));
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00021728 File Offset: 0x0001F928
		protected virtual void OnLayoutCreated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.EventLayoutCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00021758 File Offset: 0x0001F958
		protected virtual void OnPagePropertiesChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.EventPagePropertiesChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00021788 File Offset: 0x0001F988
		protected virtual void OnPagePropertiesChanging(PagePropertiesChangingEventArgs e)
		{
			EventHandler<PagePropertiesChangingEventArgs> eventHandler = (EventHandler<PagePropertiesChangingEventArgs>)base.Events[ListView.EventPagePropertiesChanging];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000217B8 File Offset: 0x0001F9B8
		protected virtual void OnTotalRowCountAvailable(PageEventArgs e)
		{
			EventHandler<PageEventArgs> eventHandler = (EventHandler<PageEventArgs>)base.Events[ListView.EventTotalRowCountAvailable];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000217E8 File Offset: 0x0001F9E8
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.EventSelectedIndexChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00021818 File Offset: 0x0001FA18
		protected virtual void OnSelectedIndexChanging(ListViewSelectEventArgs e)
		{
			EventHandler<ListViewSelectEventArgs> eventHandler = (EventHandler<ListViewSelectEventArgs>)base.Events[ListView.EventSelectedIndexChanging];
			if (eventHandler != null)
			{
				eventHandler(this, e);
				return;
			}
			if (!base.IsDataBindingAutomatic && !e.Cancel)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_UnhandledEvent, new object[]
				{
					this.ID,
					"SelectedIndexChanging"
				}));
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00021884 File Offset: 0x0001FA84
		protected virtual void OnSorted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.EventSorted];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000218B4 File Offset: 0x0001FAB4
		protected virtual void OnSorting(ListViewSortEventArgs e)
		{
			EventHandler<ListViewSortEventArgs> eventHandler = (EventHandler<ListViewSortEventArgs>)base.Events[ListView.EventSorting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
				return;
			}
			if (!base.IsDataBindingAutomatic && !e.Cancel)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_UnhandledEvent, new object[]
				{
					this.ID,
					"Sorting"
				}));
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002191F File Offset: 0x0001FB1F
		private bool PageIsValidAfterModelException()
		{
			if (this._modelValidationGroup == null)
			{
				return true;
			}
			this.Page.Validate(this._modelValidationGroup);
			return this.Page.IsValid;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00021948 File Offset: 0x0001FB48
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			this.TrackViewState();
			int num = this.CreateChildControls(data, true);
			base.ChildControlsCreated = true;
			this.ViewState["_!ItemCount"] = num;
			int editIndex = this.EditIndex;
			if (base.IsDataBindingAutomatic && editIndex != -1 && editIndex < this.Items.Count && base.IsViewStateEnabled)
			{
				this.BoundFieldValues.Clear();
				this.ExtractItemValues(this.BoundFieldValues, this.Items[editIndex], false);
			}
			if (this.EnablePersistedSelection)
			{
				string[] dataKeyNamesInternal = this.DataKeyNamesInternal;
				if (dataKeyNamesInternal == null || dataKeyNamesInternal.Length == 0)
				{
					throw new InvalidOperationException(AtlasWeb.ListView_PersistedSelectionRequiresDataKeysNames);
				}
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000219F8 File Offset: 0x0001FBF8
		protected override void PerformSelect()
		{
			if (this._performingSelect)
			{
				return;
			}
			try
			{
				this._performingSelect = true;
				this.EnsureLayoutTemplate();
				if (base.DesignMode)
				{
					DataPager dataPager = this.FindDataPager(this);
					if (dataPager != null)
					{
						this._maximumRows = dataPager.PageSize;
					}
				}
				base.PerformSelect();
			}
			finally
			{
				this._performingSelect = false;
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00021A5C File Offset: 0x0001FC5C
		protected virtual void RemoveItems()
		{
			if (this._groupTemplate != null)
			{
				if (this._groupsItemCreatedCount > 0)
				{
					for (int i = 0; i < this._groupsItemCreatedCount; i++)
					{
						this._groupsGroupPlaceholderContainer.Controls.RemoveAt(this._groupsOriginalIndexOfGroupPlaceholderInContainer);
					}
					this._groupsItemCreatedCount = 0;
				}
			}
			else if (this._noGroupsItemCreatedCount > 0)
			{
				for (int j = 0; j < this._noGroupsItemCreatedCount; j++)
				{
					this._noGroupsItemPlaceholderContainer.Controls.RemoveAt(this._noGroupsOriginalIndexOfItemPlaceholderInContainer);
				}
				this._noGroupsItemCreatedCount = 0;
			}
			this._autoIDIndex = 0;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00021AE8 File Offset: 0x0001FCE8
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderContents(writer);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00021AF1 File Offset: 0x0001FCF1
		private void ResetModelValidationGroup(bool causesValidation, string validationGroup)
		{
			this._modelValidationGroup = null;
			if (causesValidation)
			{
				this.Page.Validate(validationGroup);
				if (this.EnableModelValidation)
				{
					this._modelValidationGroup = validationGroup;
				}
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00021B18 File Offset: 0x0001FD18
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			if (obj != null || this._startRowIndex > 0 || this._maximumRows != -1 || this._editIndex != -1 || this._selectedIndex != -1 || this._groupItemCount != 1 || (this._sortExpression != null && this._sortExpression.Length != 0) || (this._sortDirection != SortDirection.Ascending || this._totalRowCount != -1 || (this._dataKeyNames != null && this._dataKeyNames.Length != 0)) || (this._dataKeysArrayList != null && this._dataKeysArrayList.Count > 0))
			{
				return new object[]
				{
					obj,
					(this._editIndex == -1) ? null : this._editIndex,
					(this._selectedIndex == -1) ? null : this._selectedIndex,
					(this._groupItemCount == 1) ? null : this._groupItemCount,
					(this._sortExpression == null || this._sortExpression.Length == 0) ? null : this._sortExpression,
					(this._sortDirection == SortDirection.Ascending) ? null : ((int)this._sortDirection),
					(this._dataKeyNames == null || this._dataKeyNames.Length == 0) ? null : this._dataKeyNames,
					this.SaveDataKeysState(),
					(this._totalRowCount == -1) ? null : this._totalRowCount,
					(this._persistedDataKey == null) ? null : ((IStateManager)this._persistedDataKey).SaveViewState(),
					(this._clientIDRowSuffix == null || this._clientIDRowSuffix.Length == 0) ? null : this._clientIDRowSuffix,
					this.SaveClientIDRowSuffixDataKeysState(),
					this._startRowIndex,
					this._maximumRows
				};
			}
			return true;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00021CF0 File Offset: 0x0001FEF0
		private object SaveDataKeysState()
		{
			object obj = new object();
			int num = 0;
			if (this._dataKeysArrayList != null && this._dataKeysArrayList.Count > 0)
			{
				num = this._dataKeysArrayList.Count;
				obj = new object[num];
				for (int i = 0; i < num; i++)
				{
					((object[])obj)[i] = ((IStateManager)this._dataKeysArrayList[i]).SaveViewState();
				}
			}
			if (this._dataKeysArrayList != null && num != 0)
			{
				return obj;
			}
			return null;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00021D68 File Offset: 0x0001FF68
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = (this._boundFieldValues != null) ? OrderedDictionaryStateHelper.SaveViewState(this._boundFieldValues) : null;
			return new object[]
			{
				obj,
				obj2
			};
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00021DA4 File Offset: 0x0001FFA4
		private object SaveClientIDRowSuffixDataKeysState()
		{
			object obj = new object();
			int num = 0;
			if (this._clientIDRowSuffixArrayList != null && this._clientIDRowSuffixArrayList.Count > 0)
			{
				num = this._clientIDRowSuffixArrayList.Count;
				obj = new object[num];
				for (int i = 0; i < num; i++)
				{
					((object[])obj)[i] = ((IStateManager)this._clientIDRowSuffixArrayList[i]).SaveViewState();
				}
			}
			if (this._clientIDRowSuffixArrayList != null && num != 0)
			{
				return obj;
			}
			return null;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00021E1A File Offset: 0x0002001A
		private void SelectCallback(IEnumerable data)
		{
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ListView_DataSourceDoesntSupportPaging, new object[]
			{
				this.DataSourceID
			}));
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00021E3F File Offset: 0x0002003F
		private void SetRequiresDataBindingIfInitialized()
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00021E50 File Offset: 0x00020050
		public virtual void Sort(string sortExpression, SortDirection sortDirection)
		{
			this.HandleSort(sortExpression, sortDirection);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00021E5A File Offset: 0x0002005A
		public virtual void UpdateItem(int itemIndex, bool causesValidation)
		{
			this.ResetModelValidationGroup(causesValidation, string.Empty);
			this.HandleUpdate(null, itemIndex, causesValidation);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00021E74 File Offset: 0x00020074
		internal override void UpdateModelDataSourceProperties(ModelDataSource modelDataSource)
		{
			string dataKeyName = (this.DataKeyNamesInternal.Length != 0) ? this.DataKeyNamesInternal[0] : "";
			modelDataSource.UpdateProperties(this.ItemType, this.SelectMethod, this.UpdateMethod, this.InsertMethod, this.DeleteMethod, dataKeyName);
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00021EBF File Offset: 0x000200BF
		int IPageableItemContainer.StartRowIndex
		{
			get
			{
				return this.StartRowIndex;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00021EC7 File Offset: 0x000200C7
		protected virtual int StartRowIndex
		{
			get
			{
				return this._startRowIndex;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00021ECF File Offset: 0x000200CF
		int IPageableItemContainer.MaximumRows
		{
			get
			{
				return this.MaximumRows;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00021ED7 File Offset: 0x000200D7
		protected virtual int MaximumRows
		{
			get
			{
				return this._maximumRows;
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00021EDF File Offset: 0x000200DF
		void IPageableItemContainer.SetPageProperties(int startRowIndex, int maximumRows, bool databind)
		{
			this.SetPageProperties(startRowIndex, maximumRows, databind);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00021EEA File Offset: 0x000200EA
		public void SelectItem(int rowIndex)
		{
			this.HandleSelect(rowIndex);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00021EF3 File Offset: 0x000200F3
		public void SetEditItem(int rowIndex)
		{
			this.HandleEdit(rowIndex);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00021EFC File Offset: 0x000200FC
		protected virtual void SetPageProperties(int startRowIndex, int maximumRows, bool databind)
		{
			if (maximumRows < 1)
			{
				throw new ArgumentOutOfRangeException("maximumRows");
			}
			if (startRowIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startRowIndex");
			}
			if (this._startRowIndex != startRowIndex || this._maximumRows != maximumRows)
			{
				PagePropertiesChangingEventArgs pagePropertiesChangingEventArgs = new PagePropertiesChangingEventArgs(startRowIndex, maximumRows);
				if (databind)
				{
					this.OnPagePropertiesChanging(pagePropertiesChangingEventArgs);
				}
				this._startRowIndex = pagePropertiesChangingEventArgs.StartRowIndex;
				this._maximumRows = pagePropertiesChangingEventArgs.MaximumRows;
				if (databind)
				{
					this.OnPagePropertiesChanged(EventArgs.Empty);
				}
			}
			if (databind)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000888 RID: 2184 RVA: 0x00021F7B File Offset: 0x0002017B
		// (remove) Token: 0x06000889 RID: 2185 RVA: 0x00021F8E File Offset: 0x0002018E
		event EventHandler<PageEventArgs> IPageableItemContainer.TotalRowCountAvailable
		{
			add
			{
				base.Events.AddHandler(ListView.EventTotalRowCountAvailable, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventTotalRowCountAvailable, value);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00021FA1 File Offset: 0x000201A1
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x00021FA9 File Offset: 0x000201A9
		DataKey IPersistedSelector.DataKey
		{
			get
			{
				return this.SelectedPersistedDataKey;
			}
			set
			{
				this.SelectedPersistedDataKey = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00021FB2 File Offset: 0x000201B2
		DataKeyArray IDataKeysControl.ClientIDRowSuffixDataKeys
		{
			get
			{
				return this.ClientIDRowSuffixDataKeys;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00021FBA File Offset: 0x000201BA
		DataKeyArray IDataBoundListControl.DataKeys
		{
			get
			{
				return this.DataKeys;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x00021FC2 File Offset: 0x000201C2
		DataKey IDataBoundListControl.SelectedDataKey
		{
			get
			{
				return this.SelectedDataKey;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00021FCA File Offset: 0x000201CA
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00021FD2 File Offset: 0x000201D2
		int IDataBoundListControl.SelectedIndex
		{
			get
			{
				return this.SelectedIndex;
			}
			set
			{
				this.SelectedIndex = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00021FDB File Offset: 0x000201DB
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00021FE3 File Offset: 0x000201E3
		string[] IDataBoundListControl.ClientIDRowSuffix
		{
			get
			{
				return this.ClientIDRowSuffix;
			}
			set
			{
				this.ClientIDRowSuffix = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00021FEC File Offset: 0x000201EC
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x00021FF4 File Offset: 0x000201F4
		bool IDataBoundListControl.EnablePersistedSelection
		{
			get
			{
				return this.EnablePersistedSelection;
			}
			set
			{
				this.EnablePersistedSelection = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00021FFD File Offset: 0x000201FD
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00022005 File Offset: 0x00020205
		string IDataBoundControl.DataSourceID
		{
			get
			{
				return this.DataSourceID;
			}
			set
			{
				this.DataSourceID = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0002200E File Offset: 0x0002020E
		IDataSource IDataBoundControl.DataSourceObject
		{
			get
			{
				return base.DataSourceObject;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00022016 File Offset: 0x00020216
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0002201E File Offset: 0x0002021E
		object IDataBoundControl.DataSource
		{
			get
			{
				return this.DataSource;
			}
			set
			{
				this.DataSource = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00022027 File Offset: 0x00020227
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0002202F File Offset: 0x0002022F
		string[] IDataBoundControl.DataKeyNames
		{
			get
			{
				return this.DataKeyNames;
			}
			set
			{
				this.DataKeyNames = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00022038 File Offset: 0x00020238
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x00022040 File Offset: 0x00020240
		string IDataBoundControl.DataMember
		{
			get
			{
				return this.DataMember;
			}
			set
			{
				this.DataMember = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00022049 File Offset: 0x00020249
		IEnumerable IWizardSideBarListControl.Items
		{
			get
			{
				return this.Items;
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600089F RID: 2207 RVA: 0x00022051 File Offset: 0x00020251
		// (remove) Token: 0x060008A0 RID: 2208 RVA: 0x00022065 File Offset: 0x00020265
		event CommandEventHandler IWizardSideBarListControl.ItemCommand
		{
			add
			{
				this.ItemCommand += new EventHandler<ListViewCommandEventArgs>(value.Invoke);
			}
			remove
			{
				this.ItemCommand -= new EventHandler<ListViewCommandEventArgs>(value.Invoke);
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060008A1 RID: 2209 RVA: 0x00022079 File Offset: 0x00020279
		// (remove) Token: 0x060008A2 RID: 2210 RVA: 0x0002208C File Offset: 0x0002028C
		event EventHandler<WizardSideBarListControlItemEventArgs> IWizardSideBarListControl.ItemDataBound
		{
			add
			{
				base.Events.AddHandler(ListView.EventWizardListItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EventWizardListItemDataBound, value);
			}
		}

		// Token: 0x04000289 RID: 649
		internal const string ItemCountViewStateKey = "_!ItemCount";

		// Token: 0x0400028A RID: 650
		private ITemplate _itemTemplate;

		// Token: 0x0400028B RID: 651
		private ITemplate _editItemTemplate;

		// Token: 0x0400028C RID: 652
		private ITemplate _insertItemTemplate;

		// Token: 0x0400028D RID: 653
		private ITemplate _layoutTemplate;

		// Token: 0x0400028E RID: 654
		private ITemplate _selectedItemTemplate;

		// Token: 0x0400028F RID: 655
		private ITemplate _groupTemplate;

		// Token: 0x04000290 RID: 656
		private ITemplate _itemSeparatorTemplate;

		// Token: 0x04000291 RID: 657
		private ITemplate _groupSeparatorTemplate;

		// Token: 0x04000292 RID: 658
		private ITemplate _emptyItemTemplate;

		// Token: 0x04000293 RID: 659
		private ITemplate _emptyDataTemplate;

		// Token: 0x04000294 RID: 660
		private ITemplate _alternatingItemTemplate;

		// Token: 0x04000295 RID: 661
		private static readonly object EventTotalRowCountAvailable = new object();

		// Token: 0x04000296 RID: 662
		private static readonly object EventPagePropertiesChanged = new object();

		// Token: 0x04000297 RID: 663
		private static readonly object EventPagePropertiesChanging = new object();

		// Token: 0x04000298 RID: 664
		private static readonly object EventItemCanceling = new object();

		// Token: 0x04000299 RID: 665
		private static readonly object EventItemCommand = new object();

		// Token: 0x0400029A RID: 666
		private static readonly object EventItemCreated = new object();

		// Token: 0x0400029B RID: 667
		private static readonly object EventItemDataBound = new object();

		// Token: 0x0400029C RID: 668
		private static readonly object EventItemDeleted = new object();

		// Token: 0x0400029D RID: 669
		private static readonly object EventItemDeleting = new object();

		// Token: 0x0400029E RID: 670
		private static readonly object EventItemEditing = new object();

		// Token: 0x0400029F RID: 671
		private static readonly object EventItemInserted = new object();

		// Token: 0x040002A0 RID: 672
		private static readonly object EventItemInserting = new object();

		// Token: 0x040002A1 RID: 673
		private static readonly object EventItemUpdated = new object();

		// Token: 0x040002A2 RID: 674
		private static readonly object EventItemUpdating = new object();

		// Token: 0x040002A3 RID: 675
		private static readonly object EventLayoutCreated = new object();

		// Token: 0x040002A4 RID: 676
		private static readonly object EventSelectedIndexChanging = new object();

		// Token: 0x040002A5 RID: 677
		private static readonly object EventSelectedIndexChanged = new object();

		// Token: 0x040002A6 RID: 678
		private static readonly object EventSorted = new object();

		// Token: 0x040002A7 RID: 679
		private static readonly object EventSorting = new object();

		// Token: 0x040002A8 RID: 680
		private static readonly object EventWizardListItemDataBound = new object();

		// Token: 0x040002A9 RID: 681
		private bool _performingSelect;

		// Token: 0x040002AA RID: 682
		private int _editIndex = -1;

		// Token: 0x040002AB RID: 683
		private int _selectedIndex = -1;

		// Token: 0x040002AC RID: 684
		private int _groupItemCount = 1;

		// Token: 0x040002AD RID: 685
		private string _modelValidationGroup;

		// Token: 0x040002AE RID: 686
		private string _sortExpression = string.Empty;

		// Token: 0x040002AF RID: 687
		private SortDirection _sortDirection;

		// Token: 0x040002B0 RID: 688
		private int _startRowIndex;

		// Token: 0x040002B1 RID: 689
		private int _maximumRows = -1;

		// Token: 0x040002B2 RID: 690
		private int _totalRowCount = -1;

		// Token: 0x040002B3 RID: 691
		private IList<ListViewDataItem> _itemList;

		// Token: 0x040002B4 RID: 692
		private ListViewItem _insertItem;

		// Token: 0x040002B5 RID: 693
		private string[] _dataKeyNames;

		// Token: 0x040002B6 RID: 694
		private string[] _clientIDRowSuffix;

		// Token: 0x040002B7 RID: 695
		private DataKeyArray _dataKeyArray;

		// Token: 0x040002B8 RID: 696
		private ArrayList _dataKeysArrayList;

		// Token: 0x040002B9 RID: 697
		private DataKeyArray _clientIDRowSuffixArray;

		// Token: 0x040002BA RID: 698
		private ArrayList _clientIDRowSuffixArrayList;

		// Token: 0x040002BB RID: 699
		private OrderedDictionary _boundFieldValues;

		// Token: 0x040002BC RID: 700
		private DataKey _persistedDataKey;

		// Token: 0x040002BD RID: 701
		private int _deletedItemIndex;

		// Token: 0x040002BE RID: 702
		private IOrderedDictionary _deleteKeys;

		// Token: 0x040002BF RID: 703
		private IOrderedDictionary _deleteValues;

		// Token: 0x040002C0 RID: 704
		private IOrderedDictionary _insertValues;

		// Token: 0x040002C1 RID: 705
		private IOrderedDictionary _updateKeys;

		// Token: 0x040002C2 RID: 706
		private IOrderedDictionary _updateOldValues;

		// Token: 0x040002C3 RID: 707
		private IOrderedDictionary _updateNewValues;

		// Token: 0x040002C4 RID: 708
		private int _autoIDIndex;

		// Token: 0x040002C5 RID: 709
		private const string _automaticIDPrefix = "ctrl";

		// Token: 0x040002C6 RID: 710
		private bool _instantiatedEmptyDataTemplate;

		// Token: 0x040002C7 RID: 711
		private int _noGroupsOriginalIndexOfItemPlaceholderInContainer = -1;

		// Token: 0x040002C8 RID: 712
		private int _noGroupsItemCreatedCount;

		// Token: 0x040002C9 RID: 713
		private Control _noGroupsItemPlaceholderContainer;

		// Token: 0x040002CA RID: 714
		private int _groupsOriginalIndexOfGroupPlaceholderInContainer = -1;

		// Token: 0x040002CB RID: 715
		private int _groupsItemCreatedCount;

		// Token: 0x040002CC RID: 716
		private Control _groupsGroupPlaceholderContainer;

		// Token: 0x040002CD RID: 717
		private string _updateMethod;

		// Token: 0x040002CE RID: 718
		private string _insertMethod;

		// Token: 0x040002CF RID: 719
		private string _deleteMethod;
	}
}
