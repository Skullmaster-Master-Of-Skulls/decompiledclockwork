using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020008C6 RID: 2246
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class SpreadsheetStrings : LocalizationStrings
	{
		// Token: 0x06005342 RID: 21314 RVA: 0x00101B9F File Offset: 0x000FFD9F
		internal SpreadsheetStrings(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x06005343 RID: 21315 RVA: 0x00101BA8 File Offset: 0x000FFDA8
		// (set) Token: 0x06005344 RID: 21316 RVA: 0x00101BB5 File Offset: 0x000FFDB5
		[DefaultValue("Cut")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("ContextMenu")]
		public string ContextMenuCut
		{
			get
			{
				return this.GetString("ContextMenuCut");
			}
			set
			{
				this.SetString("ContextMenuCut", value);
			}
		}

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x06005345 RID: 21317 RVA: 0x00101BC3 File Offset: 0x000FFDC3
		// (set) Token: 0x06005346 RID: 21318 RVA: 0x00101BD0 File Offset: 0x000FFDD0
		[NotifyParentProperty(true)]
		[DefaultValue("Copy")]
		[ScriptIgnore]
		[Category("ContextMenu")]
		[Localizable(true)]
		public string ContextMenuCopy
		{
			get
			{
				return this.GetString("ContextMenuCopy");
			}
			set
			{
				this.SetString("ContextMenuCopy", value);
			}
		}

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x06005347 RID: 21319 RVA: 0x00101BDE File Offset: 0x000FFDDE
		// (set) Token: 0x06005348 RID: 21320 RVA: 0x00101BEB File Offset: 0x000FFDEB
		[DefaultValue("Paste")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("ContextMenu")]
		[Localizable(true)]
		public string ContextMenuPaste
		{
			get
			{
				return this.GetString("ContextMenuPaste");
			}
			set
			{
				this.SetString("ContextMenuPaste", value);
			}
		}

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x06005349 RID: 21321 RVA: 0x00101BF9 File Offset: 0x000FFDF9
		// (set) Token: 0x0600534A RID: 21322 RVA: 0x00101C06 File Offset: 0x000FFE06
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Localizable(true)]
		[Category("ContextMenu")]
		[DefaultValue("Hide")]
		public string ContextMenuHideRow
		{
			get
			{
				return this.GetString("ContextMenuHideRow");
			}
			set
			{
				this.SetString("ContextMenuHideRow", value);
			}
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x0600534B RID: 21323 RVA: 0x00101C14 File Offset: 0x000FFE14
		// (set) Token: 0x0600534C RID: 21324 RVA: 0x00101C21 File Offset: 0x000FFE21
		[DefaultValue("Unhide")]
		[ScriptIgnore]
		[Category("ContextMenu")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ContextMenuUnhideRow
		{
			get
			{
				return this.GetString("ContextMenuUnhideRow");
			}
			set
			{
				this.SetString("ContextMenuUnhideRow", value);
			}
		}

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x0600534D RID: 21325 RVA: 0x00101C2F File Offset: 0x000FFE2F
		// (set) Token: 0x0600534E RID: 21326 RVA: 0x00101C3C File Offset: 0x000FFE3C
		[DefaultValue("Delete")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("ContextMenu")]
		public string ContextMenuDeleteRow
		{
			get
			{
				return this.GetString("ContextMenuDeleteRow");
			}
			set
			{
				this.SetString("ContextMenuDeleteRow", value);
			}
		}

		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x0600534F RID: 21327 RVA: 0x00101C4A File Offset: 0x000FFE4A
		// (set) Token: 0x06005350 RID: 21328 RVA: 0x00101C57 File Offset: 0x000FFE57
		[Category("ContextMenu")]
		[Localizable(true)]
		[DefaultValue("Hide")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string ContextMenuHideColumn
		{
			get
			{
				return this.GetString("ContextMenuHideColumn");
			}
			set
			{
				this.SetString("ContextMenuHideColumn", value);
			}
		}

		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x06005351 RID: 21329 RVA: 0x00101C65 File Offset: 0x000FFE65
		// (set) Token: 0x06005352 RID: 21330 RVA: 0x00101C72 File Offset: 0x000FFE72
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("ContextMenu")]
		[DefaultValue("Hide")]
		[ScriptIgnore]
		public string ContextMenuUnideColumn
		{
			get
			{
				return this.GetString("ContextMenuUnideColumn");
			}
			set
			{
				this.SetString("ContextMenuUnideColumn", value);
			}
		}

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x06005353 RID: 21331 RVA: 0x00101C80 File Offset: 0x000FFE80
		// (set) Token: 0x06005354 RID: 21332 RVA: 0x00101C8D File Offset: 0x000FFE8D
		[DefaultValue("Delete")]
		[NotifyParentProperty(true)]
		[Category("ContextMenu")]
		[ScriptIgnore]
		[Localizable(true)]
		public string ContextMenuDeleteColumn
		{
			get
			{
				return this.GetString("ContextMenuDeleteColumn");
			}
			set
			{
				this.SetString("ContextMenuDeleteColumn", value);
			}
		}

		// Token: 0x17001B50 RID: 6992
		// (get) Token: 0x06005355 RID: 21333 RVA: 0x00101C9B File Offset: 0x000FFE9B
		// (set) Token: 0x06005356 RID: 21334 RVA: 0x00101CA8 File Offset: 0x000FFEA8
		[DefaultValue("Filter By Condition")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		public string FilterMenuFilterByCondition
		{
			get
			{
				return this.GetString("FilterMenuFilterByCondition");
			}
			set
			{
				this.SetString("FilterMenuFilterByCondition", value);
			}
		}

		// Token: 0x17001B51 RID: 6993
		// (get) Token: 0x06005357 RID: 21335 RVA: 0x00101CB6 File Offset: 0x000FFEB6
		// (set) Token: 0x06005358 RID: 21336 RVA: 0x00101CC3 File Offset: 0x000FFEC3
		[DefaultValue("Filter By Value")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		[Category("FilterMenu")]
		public string FilterMenuFilterByValue
		{
			get
			{
				return this.GetString("FilterMenuFilterByValue");
			}
			set
			{
				this.SetString("FilterMenuFilterByValue", value);
			}
		}

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x06005359 RID: 21337 RVA: 0x00101CD1 File Offset: 0x000FFED1
		// (set) Token: 0x0600535A RID: 21338 RVA: 0x00101CDE File Offset: 0x000FFEDE
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Localizable(true)]
		[Category("FilterMenu")]
		[DefaultValue("Sort Ascending")]
		public string FilterMenuSortAscending
		{
			get
			{
				return this.GetString("FilterMenuSortAscending");
			}
			set
			{
				this.SetString("FilterMenuSortAscending", value);
			}
		}

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x0600535B RID: 21339 RVA: 0x00101CEC File Offset: 0x000FFEEC
		// (set) Token: 0x0600535C RID: 21340 RVA: 0x00101CF9 File Offset: 0x000FFEF9
		[DefaultValue("Sort Descending")]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		[Localizable(true)]
		[ScriptIgnore]
		public string FilterMenuSortDescending
		{
			get
			{
				return this.GetString("FilterMenuSortDescending");
			}
			set
			{
				this.SetString("FilterMenuSortDescending", value);
			}
		}

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x0600535D RID: 21341 RVA: 0x00101D07 File Offset: 0x000FFF07
		// (set) Token: 0x0600535E RID: 21342 RVA: 0x00101D14 File Offset: 0x000FFF14
		[Category("FilterMenu")]
		[DefaultValue("Clear Sorting")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string FilterMenuClearSorting
		{
			get
			{
				return this.GetString("FilterMenuClearSorting");
			}
			set
			{
				this.SetString("FilterMenuClearSorting", value);
			}
		}

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x0600535F RID: 21343 RVA: 0x00101D22 File Offset: 0x000FFF22
		// (set) Token: 0x06005360 RID: 21344 RVA: 0x00101D2F File Offset: 0x000FFF2F
		[Localizable(true)]
		[DefaultValue("Apply")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		public string FilterMenuApply
		{
			get
			{
				return this.GetString("FilterMenuApply");
			}
			set
			{
				this.SetString("FilterMenuApply", value);
			}
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x06005361 RID: 21345 RVA: 0x00101D3D File Offset: 0x000FFF3D
		// (set) Token: 0x06005362 RID: 21346 RVA: 0x00101D4A File Offset: 0x000FFF4A
		[Localizable(true)]
		[DefaultValue("Clear")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		public string FilterMenuClear
		{
			get
			{
				return this.GetString("FilterMenuClear");
			}
			set
			{
				this.SetString("FilterMenuClear", value);
			}
		}

		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x06005363 RID: 21347 RVA: 0x00101D58 File Offset: 0x000FFF58
		// (set) Token: 0x06005364 RID: 21348 RVA: 0x00101D65 File Offset: 0x000FFF65
		[ScriptIgnore]
		[DefaultValue("None")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		public string FilterMenuNone
		{
			get
			{
				return this.GetString("FilterMenuNone");
			}
			set
			{
				this.SetString("FilterMenuNone", value);
			}
		}

		// Token: 0x17001B58 RID: 7000
		// (get) Token: 0x06005365 RID: 21349 RVA: 0x00101D73 File Offset: 0x000FFF73
		// (set) Token: 0x06005366 RID: 21350 RVA: 0x00101D80 File Offset: 0x000FFF80
		[ScriptIgnore]
		[Category("FilterMenu")]
		[DefaultValue("Text contains")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string FilterMenuTextContains
		{
			get
			{
				return this.GetString("FilterMenuTextContains");
			}
			set
			{
				this.SetString("FilterMenuTextContains", value);
			}
		}

		// Token: 0x17001B59 RID: 7001
		// (get) Token: 0x06005367 RID: 21351 RVA: 0x00101D8E File Offset: 0x000FFF8E
		// (set) Token: 0x06005368 RID: 21352 RVA: 0x00101D9B File Offset: 0x000FFF9B
		[DefaultValue("Text does not contain")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		public string FilterMenuTextDoesNotContain
		{
			get
			{
				return this.GetString("FilterMenuTextDoesNotContain");
			}
			set
			{
				this.SetString("FilterMenuTextDoesNotContain", value);
			}
		}

		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x06005369 RID: 21353 RVA: 0x00101DA9 File Offset: 0x000FFFA9
		// (set) Token: 0x0600536A RID: 21354 RVA: 0x00101DB6 File Offset: 0x000FFFB6
		[DefaultValue("Text starts with")]
		[Category("FilterMenu")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string FilterMenuTextStartsWith
		{
			get
			{
				return this.GetString("FilterMenuTextStartsWith");
			}
			set
			{
				this.SetString("FilterMenuTextStartsWith", value);
			}
		}

		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x0600536B RID: 21355 RVA: 0x00101DC4 File Offset: 0x000FFFC4
		// (set) Token: 0x0600536C RID: 21356 RVA: 0x00101DD1 File Offset: 0x000FFFD1
		[NotifyParentProperty(true)]
		[DefaultValue("Text ends with")]
		[Localizable(true)]
		[ScriptIgnore]
		[Category("FilterMenu")]
		public string FilterMenuTextEndsWith
		{
			get
			{
				return this.GetString("FilterMenuTextEndsWith");
			}
			set
			{
				this.SetString("FilterMenuTextEndsWith", value);
			}
		}

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x0600536D RID: 21357 RVA: 0x00101DDF File Offset: 0x000FFFDF
		// (set) Token: 0x0600536E RID: 21358 RVA: 0x00101DEC File Offset: 0x000FFFEC
		[DefaultValue("Date is")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		public string FilterMenuDateIs
		{
			get
			{
				return this.GetString("FilterMenuDateIs");
			}
			set
			{
				this.SetString("FilterMenuDateIs", value);
			}
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x0600536F RID: 21359 RVA: 0x00101DFA File Offset: 0x000FFFFA
		// (set) Token: 0x06005370 RID: 21360 RVA: 0x00101E07 File Offset: 0x00100007
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Date is not")]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		public string FilterMenuDateIsNot
		{
			get
			{
				return this.GetString("FilterMenuDateIsNot");
			}
			set
			{
				this.SetString("FilterMenuDateIsNot", value);
			}
		}

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x06005371 RID: 21361 RVA: 0x00101E15 File Offset: 0x00100015
		// (set) Token: 0x06005372 RID: 21362 RVA: 0x00101E22 File Offset: 0x00100022
		[Localizable(true)]
		[DefaultValue("Date is before")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		public string FilterMenuDateIsBefore
		{
			get
			{
				return this.GetString("FilterMenuDateIsBefore");
			}
			set
			{
				this.SetString("FilterMenuDateIsBefore", value);
			}
		}

		// Token: 0x17001B5F RID: 7007
		// (get) Token: 0x06005373 RID: 21363 RVA: 0x00101E30 File Offset: 0x00100030
		// (set) Token: 0x06005374 RID: 21364 RVA: 0x00101E3D File Offset: 0x0010003D
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		[DefaultValue("Date is after")]
		public string FilterMenuDateIsAfter
		{
			get
			{
				return this.GetString("FilterMenuDateIsAfter");
			}
			set
			{
				this.SetString("FilterMenuDateIsAfter", value);
			}
		}

		// Token: 0x17001B60 RID: 7008
		// (get) Token: 0x06005375 RID: 21365 RVA: 0x00101E4B File Offset: 0x0010004B
		// (set) Token: 0x06005376 RID: 21366 RVA: 0x00101E58 File Offset: 0x00100058
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		[DefaultValue("Is equal to")]
		public string FilterMenuIsEqualTo
		{
			get
			{
				return this.GetString("FilterMenuIsEqualTo");
			}
			set
			{
				this.SetString("FilterMenuIsEqualTo", value);
			}
		}

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x06005377 RID: 21367 RVA: 0x00101E66 File Offset: 0x00100066
		// (set) Token: 0x06005378 RID: 21368 RVA: 0x00101E73 File Offset: 0x00100073
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		[DefaultValue("Is not equal to")]
		public string FilterMenuIsNotEqualTo
		{
			get
			{
				return this.GetString("FilterMenuIsNotEqualTo");
			}
			set
			{
				this.SetString("FilterMenuIsNotEqualTo", value);
			}
		}

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x06005379 RID: 21369 RVA: 0x00101E81 File Offset: 0x00100081
		// (set) Token: 0x0600537A RID: 21370 RVA: 0x00101E8E File Offset: 0x0010008E
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Is greater than or equal to")]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		public string FilterMenuIsGreaterThanOrEqualTo
		{
			get
			{
				return this.GetString("FilterMenuIsGreaterThanOrEqualTo");
			}
			set
			{
				this.SetString("FilterMenuIsGreaterThanOrEqualTo", value);
			}
		}

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x0600537B RID: 21371 RVA: 0x00101E9C File Offset: 0x0010009C
		// (set) Token: 0x0600537C RID: 21372 RVA: 0x00101EA9 File Offset: 0x001000A9
		[Localizable(true)]
		[DefaultValue("Is greater than")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		public string FilterMenuIsGreaterThan
		{
			get
			{
				return this.GetString("FilterMenuIsGreaterThan");
			}
			set
			{
				this.SetString("FilterMenuIsGreaterThan", value);
			}
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x0600537D RID: 21373 RVA: 0x00101EB7 File Offset: 0x001000B7
		// (set) Token: 0x0600537E RID: 21374 RVA: 0x00101EC4 File Offset: 0x001000C4
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		[DefaultValue("Is less than or equal to")]
		public string FilterMenuIsLessThanOrEqualTo
		{
			get
			{
				return this.GetString("FilterMenuIsLessThanOrEqualTo");
			}
			set
			{
				this.SetString("FilterMenuIsLessThanOrEqualTo", value);
			}
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x0600537F RID: 21375 RVA: 0x00101ED2 File Offset: 0x001000D2
		// (set) Token: 0x06005380 RID: 21376 RVA: 0x00101EDF File Offset: 0x001000DF
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("FilterMenu")]
		[DefaultValue("Is less than")]
		public string FilterMenuIsLessThan
		{
			get
			{
				return this.GetString("FilterMenuIsLessThan");
			}
			set
			{
				this.SetString("FilterMenuIsLessThan", value);
			}
		}

		// Token: 0x17001B66 RID: 7014
		// (get) Token: 0x06005381 RID: 21377 RVA: 0x00101EED File Offset: 0x001000ED
		// (set) Token: 0x06005382 RID: 21378 RVA: 0x00101EFA File Offset: 0x001000FA
		[Localizable(true)]
		[Category("CustomFormat")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Save")]
		public string CustomFormatSave
		{
			get
			{
				return this.GetString("CustomFormatSave");
			}
			set
			{
				this.SetString("CustomFormatSave", value);
			}
		}

		// Token: 0x17001B67 RID: 7015
		// (get) Token: 0x06005383 RID: 21379 RVA: 0x00101F08 File Offset: 0x00100108
		// (set) Token: 0x06005384 RID: 21380 RVA: 0x00101F15 File Offset: 0x00100115
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("CustomFormat")]
		[DefaultValue("Cancel")]
		[ScriptIgnore]
		public string CustomFormatCancel
		{
			get
			{
				return this.GetString("CustomFormatCancel");
			}
			set
			{
				this.SetString("CustomFormatCancel", value);
			}
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x06005385 RID: 21381 RVA: 0x00101F23 File Offset: 0x00100123
		// (set) Token: 0x06005386 RID: 21382 RVA: 0x00101F30 File Offset: 0x00100130
		[Category("CustomFormat")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[DefaultValue("Number")]
		public string CustomFormatNumber
		{
			get
			{
				return this.GetString("CustomFormatNumber");
			}
			set
			{
				this.SetString("CustomFormatNumber", value);
			}
		}

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x06005387 RID: 21383 RVA: 0x00101F3E File Offset: 0x0010013E
		// (set) Token: 0x06005388 RID: 21384 RVA: 0x00101F4B File Offset: 0x0010014B
		[ScriptIgnore]
		[Category("CustomFormat")]
		[DefaultValue("Currency")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string CustomFormatCurrency
		{
			get
			{
				return this.GetString("CustomFormatCurrency");
			}
			set
			{
				this.SetString("CustomFormatCurrency", value);
			}
		}

		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x06005389 RID: 21385 RVA: 0x00101F59 File Offset: 0x00100159
		// (set) Token: 0x0600538A RID: 21386 RVA: 0x00101F66 File Offset: 0x00100166
		[DefaultValue("Date and Time")]
		[ScriptIgnore]
		[Category("CustomFormat")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string CustomFormatDateTime
		{
			get
			{
				return this.GetString("CustomFormatDateTime");
			}
			set
			{
				this.SetString("CustomFormatDateTime", value);
			}
		}

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x0600538B RID: 21387 RVA: 0x00101F74 File Offset: 0x00100174
		// (set) Token: 0x0600538C RID: 21388 RVA: 0x00101F81 File Offset: 0x00100181
		[DefaultValue("Any Value")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Category("Validation")]
		[Localizable(true)]
		public string ValidationAny
		{
			get
			{
				return this.GetString("ValidationAny");
			}
			set
			{
				this.SetString("ValidationAny", value);
			}
		}

		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x00101F8F File Offset: 0x0010018F
		// (set) Token: 0x0600538E RID: 21390 RVA: 0x00101F9C File Offset: 0x0010019C
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("Number")]
		[Localizable(true)]
		[ScriptIgnore]
		public string ValidationNumber
		{
			get
			{
				return this.GetString("ValidationNumber");
			}
			set
			{
				this.SetString("ValidationNumber", value);
			}
		}

		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x0600538F RID: 21391 RVA: 0x00101FAA File Offset: 0x001001AA
		// (set) Token: 0x06005390 RID: 21392 RVA: 0x00101FB7 File Offset: 0x001001B7
		[Category("Validation")]
		[DefaultValue("Text")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string ValidationText
		{
			get
			{
				return this.GetString("ValidationText");
			}
			set
			{
				this.SetString("ValidationText", value);
			}
		}

		// Token: 0x17001B6E RID: 7022
		// (get) Token: 0x06005391 RID: 21393 RVA: 0x00101FC5 File Offset: 0x001001C5
		// (set) Token: 0x06005392 RID: 21394 RVA: 0x00101FD2 File Offset: 0x001001D2
		[Category("Validation")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Date")]
		public string ValidationDate
		{
			get
			{
				return this.GetString("ValidationDate");
			}
			set
			{
				this.SetString("ValidationDate", value);
			}
		}

		// Token: 0x17001B6F RID: 7023
		// (get) Token: 0x06005393 RID: 21395 RVA: 0x00101FE0 File Offset: 0x001001E0
		// (set) Token: 0x06005394 RID: 21396 RVA: 0x00101FED File Offset: 0x001001ED
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("Custom Formula")]
		public string ValidationCustomFormula
		{
			get
			{
				return this.GetString("ValidationCustomFormula");
			}
			set
			{
				this.SetString("ValidationCustomFormula", value);
			}
		}

		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x06005395 RID: 21397 RVA: 0x00101FFB File Offset: 0x001001FB
		// (set) Token: 0x06005396 RID: 21398 RVA: 0x00102008 File Offset: 0x00100208
		[ScriptIgnore]
		[DefaultValue("List")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		public string ValidationList
		{
			get
			{
				return this.GetString("ValidationList");
			}
			set
			{
				this.SetString("ValidationList", value);
			}
		}

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x06005397 RID: 21399 RVA: 0x00102016 File Offset: 0x00100216
		// (set) Token: 0x06005398 RID: 21400 RVA: 0x00102023 File Offset: 0x00100223
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("Criteria")]
		public string ValidationCriteria
		{
			get
			{
				return this.GetString("ValidationCriteria");
			}
			set
			{
				this.SetString("ValidationCriteria", value);
			}
		}

		// Token: 0x17001B72 RID: 7026
		// (get) Token: 0x06005399 RID: 21401 RVA: 0x00102031 File Offset: 0x00100231
		// (set) Token: 0x0600539A RID: 21402 RVA: 0x0010203E File Offset: 0x0010023E
		[Category("Validation")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Ignore Blank")]
		[ScriptIgnore]
		public string ValidationIgnoreBlank
		{
			get
			{
				return this.GetString("ValidationIgnoreBlank");
			}
			set
			{
				this.SetString("ValidationIgnoreBlank", value);
			}
		}

		// Token: 0x17001B73 RID: 7027
		// (get) Token: 0x0600539B RID: 21403 RVA: 0x0010204C File Offset: 0x0010024C
		// (set) Token: 0x0600539C RID: 21404 RVA: 0x00102059 File Offset: 0x00100259
		[DefaultValue("Display button to show list")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Validation")]
		[ScriptIgnore]
		public string ValidationShowListButton
		{
			get
			{
				return this.GetString("ValidationShowListButton");
			}
			set
			{
				this.SetString("ValidationShowListButton", value);
			}
		}

		// Token: 0x17001B74 RID: 7028
		// (get) Token: 0x0600539D RID: 21405 RVA: 0x00102067 File Offset: 0x00100267
		// (set) Token: 0x0600539E RID: 21406 RVA: 0x00102074 File Offset: 0x00100274
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("Display button to show calendar")]
		public string ValidationShowCalendarButton
		{
			get
			{
				return this.GetString("ValidationShowCalendarButton");
			}
			set
			{
				this.SetString("ValidationShowCalendarButton", value);
			}
		}

		// Token: 0x17001B75 RID: 7029
		// (get) Token: 0x0600539F RID: 21407 RVA: 0x00102082 File Offset: 0x00100282
		// (set) Token: 0x060053A0 RID: 21408 RVA: 0x0010208F File Offset: 0x0010028F
		[ScriptIgnore]
		[DefaultValue("greater than")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		public string ValidationGreaterThan
		{
			get
			{
				return this.GetString("ValidationGreaterThan");
			}
			set
			{
				this.SetString("ValidationGreaterThan", value);
			}
		}

		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x060053A1 RID: 21409 RVA: 0x0010209D File Offset: 0x0010029D
		// (set) Token: 0x060053A2 RID: 21410 RVA: 0x001020AA File Offset: 0x001002AA
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("less than")]
		public string ValidationLessThan
		{
			get
			{
				return this.GetString("ValidationLessThan");
			}
			set
			{
				this.SetString("ValidationLessThan", value);
			}
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x060053A3 RID: 21411 RVA: 0x001020B8 File Offset: 0x001002B8
		// (set) Token: 0x060053A4 RID: 21412 RVA: 0x001020C5 File Offset: 0x001002C5
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("between")]
		[ScriptIgnore]
		public string ValidationBetween
		{
			get
			{
				return this.GetString("ValidationBetween");
			}
			set
			{
				this.SetString("ValidationBetween", value);
			}
		}

		// Token: 0x17001B78 RID: 7032
		// (get) Token: 0x060053A5 RID: 21413 RVA: 0x001020D3 File Offset: 0x001002D3
		// (set) Token: 0x060053A6 RID: 21414 RVA: 0x001020E0 File Offset: 0x001002E0
		[DefaultValue("not between")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Validation")]
		[ScriptIgnore]
		public string ValidationNotBetween
		{
			get
			{
				return this.GetString("ValidationNotBetween");
			}
			set
			{
				this.SetString("ValidationNotBetween", value);
			}
		}

		// Token: 0x17001B79 RID: 7033
		// (get) Token: 0x060053A7 RID: 21415 RVA: 0x001020EE File Offset: 0x001002EE
		// (set) Token: 0x060053A8 RID: 21416 RVA: 0x001020FB File Offset: 0x001002FB
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("equal to")]
		public string ValidationEqualTo
		{
			get
			{
				return this.GetString("ValidationEqualTo");
			}
			set
			{
				this.SetString("ValidationEqualTo", value);
			}
		}

		// Token: 0x17001B7A RID: 7034
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x00102109 File Offset: 0x00100309
		// (set) Token: 0x060053AA RID: 21418 RVA: 0x00102116 File Offset: 0x00100316
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		[Category("Validation")]
		[DefaultValue("not equal to")]
		public string ValidationNotEqualTo
		{
			get
			{
				return this.GetString("ValidationNotEqualTo");
			}
			set
			{
				this.SetString("ValidationNotEqualTo", value);
			}
		}

		// Token: 0x17001B7B RID: 7035
		// (get) Token: 0x060053AB RID: 21419 RVA: 0x00102124 File Offset: 0x00100324
		// (set) Token: 0x060053AC RID: 21420 RVA: 0x00102131 File Offset: 0x00100331
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("greater than or equal to")]
		[Localizable(true)]
		public string ValidationGreaterThanOrEqualTo
		{
			get
			{
				return this.GetString("ValidationGreaterThanOrEqualTo");
			}
			set
			{
				this.SetString("ValidationGreaterThanOrEqualTo", value);
			}
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x060053AD RID: 21421 RVA: 0x0010213F File Offset: 0x0010033F
		// (set) Token: 0x060053AE RID: 21422 RVA: 0x0010214C File Offset: 0x0010034C
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("less than or equal to")]
		[Category("Validation")]
		public string ValidationLessThanOrEqualTo
		{
			get
			{
				return this.GetString("ValidationLessThanOrEqualTo");
			}
			set
			{
				this.SetString("ValidationLessThanOrEqualTo", value);
			}
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x060053AF RID: 21423 RVA: 0x0010215A File Offset: 0x0010035A
		// (set) Token: 0x060053B0 RID: 21424 RVA: 0x00102167 File Offset: 0x00100367
		[DefaultValue("Data")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Category("Validation")]
		[Localizable(true)]
		public string ValidationData
		{
			get
			{
				return this.GetString("ValidationData");
			}
			set
			{
				this.SetString("ValidationData", value);
			}
		}

		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x060053B1 RID: 21425 RVA: 0x00102175 File Offset: 0x00100375
		// (set) Token: 0x060053B2 RID: 21426 RVA: 0x00102182 File Offset: 0x00100382
		[DefaultValue("Min")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		public string ValidationMin
		{
			get
			{
				return this.GetString("ValidationMin");
			}
			set
			{
				this.SetString("ValidationMin", value);
			}
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x060053B3 RID: 21427 RVA: 0x00102190 File Offset: 0x00100390
		// (set) Token: 0x060053B4 RID: 21428 RVA: 0x0010219D File Offset: 0x0010039D
		[ScriptIgnore]
		[DefaultValue("Max")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		public string ValidationMax
		{
			get
			{
				return this.GetString("ValidationMax");
			}
			set
			{
				this.SetString("ValidationMax", value);
			}
		}

		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x060053B5 RID: 21429 RVA: 0x001021AB File Offset: 0x001003AB
		// (set) Token: 0x060053B6 RID: 21430 RVA: 0x001021B8 File Offset: 0x001003B8
		[DefaultValue("Value")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		public string ValidationValue
		{
			get
			{
				return this.GetString("ValidationValue");
			}
			set
			{
				this.SetString("ValidationValue", value);
			}
		}

		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x060053B7 RID: 21431 RVA: 0x001021C6 File Offset: 0x001003C6
		// (set) Token: 0x060053B8 RID: 21432 RVA: 0x001021D3 File Offset: 0x001003D3
		[Localizable(true)]
		[DefaultValue("Reject input")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		public string ValidationReject
		{
			get
			{
				return this.GetString("ValidationReject");
			}
			set
			{
				this.SetString("ValidationReject", value);
			}
		}

		// Token: 0x17001B82 RID: 7042
		// (get) Token: 0x060053B9 RID: 21433 RVA: 0x001021E1 File Offset: 0x001003E1
		// (set) Token: 0x060053BA RID: 21434 RVA: 0x001021EE File Offset: 0x001003EE
		[Localizable(true)]
		[Category("Validation")]
		[DefaultValue("Show warning")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string ValidationWarning
		{
			get
			{
				return this.GetString("ValidationWarning");
			}
			set
			{
				this.SetString("ValidationWarning", value);
			}
		}

		// Token: 0x17001B83 RID: 7043
		// (get) Token: 0x060053BB RID: 21435 RVA: 0x001021FC File Offset: 0x001003FC
		// (set) Token: 0x060053BC RID: 21436 RVA: 0x00102209 File Offset: 0x00100409
		[DefaultValue("On invalid data")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[Localizable(true)]
		public string ValidationOnInvalidData
		{
			get
			{
				return this.GetString("ValidationOnInvalidData");
			}
			set
			{
				this.SetString("ValidationOnInvalidData", value);
			}
		}

		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x060053BD RID: 21437 RVA: 0x00102217 File Offset: 0x00100417
		// (set) Token: 0x060053BE RID: 21438 RVA: 0x00102224 File Offset: 0x00100424
		[Category("Validation")]
		[DefaultValue("Hint Message")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ValidationHintMessage
		{
			get
			{
				return this.GetString("ValidationHintMessage");
			}
			set
			{
				this.SetString("ValidationHintMessage", value);
			}
		}

		// Token: 0x17001B85 RID: 7045
		// (get) Token: 0x060053BF RID: 21439 RVA: 0x00102232 File Offset: 0x00100432
		// (set) Token: 0x060053C0 RID: 21440 RVA: 0x0010223F File Offset: 0x0010043F
		[ScriptIgnore]
		[ClientPropertyName("validationHintEmptyMessage")]
		[Category("Validation")]
		[Localizable(true)]
		[DefaultValue("Enter a value that satisfies the formula:")]
		[NotifyParentProperty(true)]
		public string ValidationHintEmptyMessage
		{
			get
			{
				return this.GetString("ValidationHintEmptyMessage");
			}
			set
			{
				this.SetString("ValidationHintEmptyMessage", value);
			}
		}

		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x060053C1 RID: 21441 RVA: 0x0010224D File Offset: 0x0010044D
		// (set) Token: 0x060053C2 RID: 21442 RVA: 0x0010225A File Offset: 0x0010045A
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Save")]
		[Category("Validation")]
		[ScriptIgnore]
		public string ValidationSave
		{
			get
			{
				return this.GetString("ValidationSave");
			}
			set
			{
				this.SetString("ValidationSave", value);
			}
		}

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x060053C3 RID: 21443 RVA: 0x00102268 File Offset: 0x00100468
		// (set) Token: 0x060053C4 RID: 21444 RVA: 0x00102275 File Offset: 0x00100475
		[Category("Validation")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Cancel")]
		public string ValidationCancel
		{
			get
			{
				return this.GetString("ValidationCancel");
			}
			set
			{
				this.SetString("ValidationCancel", value);
			}
		}

		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x060053C5 RID: 21445 RVA: 0x00102283 File Offset: 0x00100483
		// (set) Token: 0x060053C6 RID: 21446 RVA: 0x00102290 File Offset: 0x00100490
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("Remove validation")]
		[ScriptIgnore]
		[Localizable(true)]
		public string ValidationRemove
		{
			get
			{
				return this.GetString("ValidationRemove");
			}
			set
			{
				this.SetString("ValidationRemove", value);
			}
		}

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x060053C7 RID: 21447 RVA: 0x0010229E File Offset: 0x0010049E
		// (set) Token: 0x060053C8 RID: 21448 RVA: 0x001022AB File Offset: 0x001004AB
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Validation")]
		[DefaultValue("Min number is required")]
		public string ValidationNumberMinRequired
		{
			get
			{
				return this.GetString("ValidationNumberMinRequired");
			}
			set
			{
				this.SetString("ValidationNumberMinRequired", value);
			}
		}

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x060053C9 RID: 21449 RVA: 0x001022B9 File Offset: 0x001004B9
		// (set) Token: 0x060053CA RID: 21450 RVA: 0x001022C6 File Offset: 0x001004C6
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("Max number is required")]
		[ScriptIgnore]
		[Localizable(true)]
		public string ValidationNumberMaxRequired
		{
			get
			{
				return this.GetString("ValidationNumberMaxRequired");
			}
			set
			{
				this.SetString("ValidationNumberMaxRequired", value);
			}
		}

		// Token: 0x17001B8B RID: 7051
		// (get) Token: 0x060053CB RID: 21451 RVA: 0x001022D4 File Offset: 0x001004D4
		// (set) Token: 0x060053CC RID: 21452 RVA: 0x001022E1 File Offset: 0x001004E1
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Value is required")]
		[ScriptIgnore]
		[Category("Validation")]
		public string ValidationTextValueRequired
		{
			get
			{
				return this.GetString("ValidationTextValueRequired");
			}
			set
			{
				this.SetString("ValidationTextValueRequired", value);
			}
		}

		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x060053CD RID: 21453 RVA: 0x001022EF File Offset: 0x001004EF
		// (set) Token: 0x060053CE RID: 21454 RVA: 0x001022FC File Offset: 0x001004FC
		[Category("Validation")]
		[DefaultValue("Min date is required")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string ValidationDateMinRequired
		{
			get
			{
				return this.GetString("ValidationDateMinRequired");
			}
			set
			{
				this.SetString("ValidationDateMinRequired", value);
			}
		}

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x060053CF RID: 21455 RVA: 0x0010230A File Offset: 0x0010050A
		// (set) Token: 0x060053D0 RID: 21456 RVA: 0x00102317 File Offset: 0x00100517
		[NotifyParentProperty(true)]
		[DefaultValue("Max date is required")]
		[ScriptIgnore]
		[Localizable(true)]
		[Category("Validation")]
		public string ValidationDateMaxRequired
		{
			get
			{
				return this.GetString("ValidationDateMaxRequired");
			}
			set
			{
				this.SetString("ValidationDateMaxRequired", value);
			}
		}

		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x060053D1 RID: 21457 RVA: 0x00102325 File Offset: 0x00100525
		// (set) Token: 0x060053D2 RID: 21458 RVA: 0x00102332 File Offset: 0x00100532
		[Localizable(true)]
		[Category("Validation")]
		[DefaultValue("Value is required")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string ValidationCustomValueRequired
		{
			get
			{
				return this.GetString("ValidationCustomValueRequired");
			}
			set
			{
				this.SetString("ValidationCustomValueRequired", value);
			}
		}

		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x060053D3 RID: 21459 RVA: 0x00102340 File Offset: 0x00100540
		// (set) Token: 0x060053D4 RID: 21460 RVA: 0x0010234D File Offset: 0x0010054D
		[Category("Hyperlink")]
		[Localizable(true)]
		[DefaultValue("OK")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string HyperlinkSave
		{
			get
			{
				return this.GetString("HyperlinkSave");
			}
			set
			{
				this.SetString("HyperlinkSave", value);
			}
		}

		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x060053D5 RID: 21461 RVA: 0x0010235B File Offset: 0x0010055B
		// (set) Token: 0x060053D6 RID: 21462 RVA: 0x00102368 File Offset: 0x00100568
		[ScriptIgnore]
		[Category("Hyperlink")]
		[DefaultValue("Cancel")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string HyperlinkCancel
		{
			get
			{
				return this.GetString("HyperlinkCancel");
			}
			set
			{
				this.SetString("HyperlinkCancel", value);
			}
		}

		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x060053D7 RID: 21463 RVA: 0x00102376 File Offset: 0x00100576
		// (set) Token: 0x060053D8 RID: 21464 RVA: 0x00102383 File Offset: 0x00100583
		[DefaultValue("Remove link")]
		[Category("Hyperlink")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string HyperlinkRemove
		{
			get
			{
				return this.GetString("HyperlinkRemove");
			}
			set
			{
				this.SetString("HyperlinkRemove", value);
			}
		}

		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x00102391 File Offset: 0x00100591
		// (set) Token: 0x060053DA RID: 21466 RVA: 0x0010239E File Offset: 0x0010059E
		[ClientPropertyName("hyperlinkTitle")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Hyperlink")]
		[DefaultValue("Hyperlink")]
		public string HyperlinkTitle
		{
			get
			{
				return this.GetString("HyperlinkTitle");
			}
			set
			{
				this.SetString("HyperlinkTitle", value);
			}
		}

		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x060053DB RID: 21467 RVA: 0x001023AC File Offset: 0x001005AC
		// (set) Token: 0x060053DC RID: 21468 RVA: 0x001023B9 File Offset: 0x001005B9
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Address")]
		[NotifyParentProperty(true)]
		[Category("Hyperlink")]
		public string HyperlinkUrl
		{
			get
			{
				return this.GetString("HyperlinkUrl");
			}
			set
			{
				this.SetString("HyperlinkUrl", value);
			}
		}

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x060053DD RID: 21469 RVA: 0x001023C7 File Offset: 0x001005C7
		// (set) Token: 0x060053DE RID: 21470 RVA: 0x001023D4 File Offset: 0x001005D4
		[Localizable(true)]
		[DefaultValue("Home")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarHome
		{
			get
			{
				return this.GetString("ToolBarHome");
			}
			set
			{
				this.SetString("ToolBarHome", value);
			}
		}

		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x060053DF RID: 21471 RVA: 0x001023E2 File Offset: 0x001005E2
		// (set) Token: 0x060053E0 RID: 21472 RVA: 0x001023EF File Offset: 0x001005EF
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Insert")]
		public string ToolBarInsert
		{
			get
			{
				return this.GetString("ToolBarInsert");
			}
			set
			{
				this.SetString("ToolBarInsert", value);
			}
		}

		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x060053E1 RID: 21473 RVA: 0x001023FD File Offset: 0x001005FD
		// (set) Token: 0x060053E2 RID: 21474 RVA: 0x0010240A File Offset: 0x0010060A
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Data")]
		public string ToolBarData
		{
			get
			{
				return this.GetString("ToolBarData");
			}
			set
			{
				this.SetString("ToolBarData", value);
			}
		}

		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x060053E3 RID: 21475 RVA: 0x00102418 File Offset: 0x00100618
		// (set) Token: 0x060053E4 RID: 21476 RVA: 0x00102425 File Offset: 0x00100625
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		[Category("Toolbar")]
		[DefaultValue("Save")]
		public string ToolBarSave
		{
			get
			{
				return this.GetString("ToolBarSave");
			}
			set
			{
				this.SetString("ToolBarSave", value);
			}
		}

		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x060053E5 RID: 21477 RVA: 0x00102433 File Offset: 0x00100633
		// (set) Token: 0x060053E6 RID: 21478 RVA: 0x00102440 File Offset: 0x00100640
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Undo")]
		[ScriptIgnore]
		[Category("Toolbar")]
		public string ToolBarUndo
		{
			get
			{
				return this.GetString("ToolBarUndo");
			}
			set
			{
				this.SetString("ToolBarUndo", value);
			}
		}

		// Token: 0x17001B99 RID: 7065
		// (get) Token: 0x060053E7 RID: 21479 RVA: 0x0010244E File Offset: 0x0010064E
		// (set) Token: 0x060053E8 RID: 21480 RVA: 0x0010245B File Offset: 0x0010065B
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Redo")]
		public string ToolBarRedo
		{
			get
			{
				return this.GetString("ToolBarRedo");
			}
			set
			{
				this.SetString("ToolBarRedo", value);
			}
		}

		// Token: 0x17001B9A RID: 7066
		// (get) Token: 0x060053E9 RID: 21481 RVA: 0x00102469 File Offset: 0x00100669
		// (set) Token: 0x060053EA RID: 21482 RVA: 0x00102476 File Offset: 0x00100676
		[Category("Toolbar")]
		[DefaultValue("Cut")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string ToolBarCut
		{
			get
			{
				return this.GetString("ToolBarCut");
			}
			set
			{
				this.SetString("ToolBarCut", value);
			}
		}

		// Token: 0x17001B9B RID: 7067
		// (get) Token: 0x060053EB RID: 21483 RVA: 0x00102484 File Offset: 0x00100684
		// (set) Token: 0x060053EC RID: 21484 RVA: 0x00102491 File Offset: 0x00100691
		[DefaultValue("Copy")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarCopy
		{
			get
			{
				return this.GetString("ToolBarCopy");
			}
			set
			{
				this.SetString("ToolBarCopy", value);
			}
		}

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x060053ED RID: 21485 RVA: 0x0010249F File Offset: 0x0010069F
		// (set) Token: 0x060053EE RID: 21486 RVA: 0x001024AC File Offset: 0x001006AC
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Paste")]
		public string ToolBarPaste
		{
			get
			{
				return this.GetString("ToolBarPaste");
			}
			set
			{
				this.SetString("ToolBarPaste", value);
			}
		}

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x060053EF RID: 21487 RVA: 0x001024BA File Offset: 0x001006BA
		// (set) Token: 0x060053F0 RID: 21488 RVA: 0x001024C7 File Offset: 0x001006C7
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Bold")]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarBold
		{
			get
			{
				return this.GetString("ToolBarBold");
			}
			set
			{
				this.SetString("ToolBarBold", value);
			}
		}

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x060053F1 RID: 21489 RVA: 0x001024D5 File Offset: 0x001006D5
		// (set) Token: 0x060053F2 RID: 21490 RVA: 0x001024E2 File Offset: 0x001006E2
		[Localizable(true)]
		[DefaultValue("Italic")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarItalic
		{
			get
			{
				return this.GetString("ToolBarItalic");
			}
			set
			{
				this.SetString("ToolBarItalic", value);
			}
		}

		// Token: 0x17001B9F RID: 7071
		// (get) Token: 0x060053F3 RID: 21491 RVA: 0x001024F0 File Offset: 0x001006F0
		// (set) Token: 0x060053F4 RID: 21492 RVA: 0x001024FD File Offset: 0x001006FD
		[ScriptIgnore]
		[DefaultValue("Underline")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarUnderline
		{
			get
			{
				return this.GetString("ToolBarUnderline");
			}
			set
			{
				this.SetString("ToolBarUnderline", value);
			}
		}

		// Token: 0x17001BA0 RID: 7072
		// (get) Token: 0x060053F5 RID: 21493 RVA: 0x0010250B File Offset: 0x0010070B
		// (set) Token: 0x060053F6 RID: 21494 RVA: 0x00102518 File Offset: 0x00100718
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Link")]
		public string ToolBarHyperlink
		{
			get
			{
				return this.GetString("ToolBarHyperlink");
			}
			set
			{
				this.SetString("ToolBarHyperlink", value);
			}
		}

		// Token: 0x17001BA1 RID: 7073
		// (get) Token: 0x060053F7 RID: 21495 RVA: 0x00102526 File Offset: 0x00100726
		// (set) Token: 0x060053F8 RID: 21496 RVA: 0x00102533 File Offset: 0x00100733
		[DefaultValue("All borders")]
		[Category("Toolbar")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string ToolBarBordersAll
		{
			get
			{
				return this.GetString("ToolBarBordersAll");
			}
			set
			{
				this.SetString("ToolBarBordersAll", value);
			}
		}

		// Token: 0x17001BA2 RID: 7074
		// (get) Token: 0x060053F9 RID: 21497 RVA: 0x00102541 File Offset: 0x00100741
		// (set) Token: 0x060053FA RID: 21498 RVA: 0x0010254E File Offset: 0x0010074E
		[DefaultValue("Inside borders")]
		[ScriptIgnore]
		[Category("Toolbar")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ToolBarBordersInside
		{
			get
			{
				return this.GetString("ToolBarBordersInside");
			}
			set
			{
				this.SetString("ToolBarBordersInside", value);
			}
		}

		// Token: 0x17001BA3 RID: 7075
		// (get) Token: 0x060053FB RID: 21499 RVA: 0x0010255C File Offset: 0x0010075C
		// (set) Token: 0x060053FC RID: 21500 RVA: 0x00102569 File Offset: 0x00100769
		[NotifyParentProperty(true)]
		[DefaultValue("Inside horizontal borders")]
		[Localizable(true)]
		[ScriptIgnore]
		[Category("Toolbar")]
		public string ToolBarBordersInsideHorizontal
		{
			get
			{
				return this.GetString("ToolBarBordersInsideHorizontal");
			}
			set
			{
				this.SetString("ToolBarBordersInsideHorizontal", value);
			}
		}

		// Token: 0x17001BA4 RID: 7076
		// (get) Token: 0x060053FD RID: 21501 RVA: 0x00102577 File Offset: 0x00100777
		// (set) Token: 0x060053FE RID: 21502 RVA: 0x00102584 File Offset: 0x00100784
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Inside vertical borders")]
		[ScriptIgnore]
		public string ToolBarBordersInsideVertical
		{
			get
			{
				return this.GetString("ToolBarBordersInsideVertical");
			}
			set
			{
				this.SetString("ToolBarBordersInsideVertical", value);
			}
		}

		// Token: 0x17001BA5 RID: 7077
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x00102592 File Offset: 0x00100792
		// (set) Token: 0x06005400 RID: 21504 RVA: 0x0010259F File Offset: 0x0010079F
		[Category("Toolbar")]
		[Localizable(true)]
		[ScriptIgnore]
		[DefaultValue("Outside borders")]
		[NotifyParentProperty(true)]
		public string ToolBarBordersOutside
		{
			get
			{
				return this.GetString("ToolBarBordersOutside");
			}
			set
			{
				this.SetString("ToolBarBordersOutside", value);
			}
		}

		// Token: 0x17001BA6 RID: 7078
		// (get) Token: 0x06005401 RID: 21505 RVA: 0x001025AD File Offset: 0x001007AD
		// (set) Token: 0x06005402 RID: 21506 RVA: 0x001025BA File Offset: 0x001007BA
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Left border")]
		public string ToolBarBordersLeft
		{
			get
			{
				return this.GetString("ToolBarBordersLeft");
			}
			set
			{
				this.SetString("ToolBarBordersLeft", value);
			}
		}

		// Token: 0x17001BA7 RID: 7079
		// (get) Token: 0x06005403 RID: 21507 RVA: 0x001025C8 File Offset: 0x001007C8
		// (set) Token: 0x06005404 RID: 21508 RVA: 0x001025D5 File Offset: 0x001007D5
		[Category("Toolbar")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Top border")]
		public string ToolBarBordersTop
		{
			get
			{
				return this.GetString("ToolBarBordersTop");
			}
			set
			{
				this.SetString("ToolBarBordersTop", value);
			}
		}

		// Token: 0x17001BA8 RID: 7080
		// (get) Token: 0x06005405 RID: 21509 RVA: 0x001025E3 File Offset: 0x001007E3
		// (set) Token: 0x06005406 RID: 21510 RVA: 0x001025F0 File Offset: 0x001007F0
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Right border")]
		public string ToolBarBordersRight
		{
			get
			{
				return this.GetString("ToolBarBordersRight");
			}
			set
			{
				this.SetString("ToolBarBordersRight", value);
			}
		}

		// Token: 0x17001BA9 RID: 7081
		// (get) Token: 0x06005407 RID: 21511 RVA: 0x001025FE File Offset: 0x001007FE
		// (set) Token: 0x06005408 RID: 21512 RVA: 0x0010260B File Offset: 0x0010080B
		[Localizable(true)]
		[ScriptIgnore]
		[Category("Toolbar")]
		[DefaultValue("Bottom border")]
		[NotifyParentProperty(true)]
		public string ToolBarBordersBottom
		{
			get
			{
				return this.GetString("ToolBarBordersBottom");
			}
			set
			{
				this.SetString("ToolBarBordersBottom", value);
			}
		}

		// Token: 0x17001BAA RID: 7082
		// (get) Token: 0x06005409 RID: 21513 RVA: 0x00102619 File Offset: 0x00100819
		// (set) Token: 0x0600540A RID: 21514 RVA: 0x00102626 File Offset: 0x00100826
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("No border")]
		[Localizable(true)]
		public string ToolBarBordersNo
		{
			get
			{
				return this.GetString("ToolBarBordersNo");
			}
			set
			{
				this.SetString("ToolBarBordersNo", value);
			}
		}

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x0600540B RID: 21515 RVA: 0x00102634 File Offset: 0x00100834
		// (set) Token: 0x0600540C RID: 21516 RVA: 0x00102641 File Offset: 0x00100841
		[Category("Toolbar")]
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Border Color")]
		[NotifyParentProperty(true)]
		public string ToolBarBorderColor
		{
			get
			{
				return this.GetString("ToolBarBorderColor");
			}
			set
			{
				this.SetString("ToolBarBorderColor", value);
			}
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x0600540D RID: 21517 RVA: 0x0010264F File Offset: 0x0010084F
		// (set) Token: 0x0600540E RID: 21518 RVA: 0x0010265C File Offset: 0x0010085C
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Horizontal alignment")]
		[Localizable(true)]
		public string ToolBarHorizontalAlignment
		{
			get
			{
				return this.GetString("ToolBarHorizontalAlignment");
			}
			set
			{
				this.SetString("ToolBarHorizontalAlignment", value);
			}
		}

		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x0600540F RID: 21519 RVA: 0x0010266A File Offset: 0x0010086A
		// (set) Token: 0x06005410 RID: 21520 RVA: 0x00102677 File Offset: 0x00100877
		[ScriptIgnore]
		[Localizable(true)]
		[Category("Toolbar")]
		[DefaultValue("Align Left")]
		[NotifyParentProperty(true)]
		public string ToolBarAlignLeft
		{
			get
			{
				return this.GetString("ToolBarAlignLeft");
			}
			set
			{
				this.SetString("ToolBarAlignLeft", value);
			}
		}

		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x06005411 RID: 21521 RVA: 0x00102685 File Offset: 0x00100885
		// (set) Token: 0x06005412 RID: 21522 RVA: 0x00102692 File Offset: 0x00100892
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Align Center")]
		[Localizable(true)]
		public string ToolBarAlignCenter
		{
			get
			{
				return this.GetString("ToolBarAlignCenter");
			}
			set
			{
				this.SetString("ToolBarAlignCenter", value);
			}
		}

		// Token: 0x17001BAF RID: 7087
		// (get) Token: 0x06005413 RID: 21523 RVA: 0x001026A0 File Offset: 0x001008A0
		// (set) Token: 0x06005414 RID: 21524 RVA: 0x001026AD File Offset: 0x001008AD
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Align Right")]
		[Localizable(true)]
		public string ToolBarAlignRight
		{
			get
			{
				return this.GetString("ToolBarAlignRight");
			}
			set
			{
				this.SetString("ToolBarAlignRight", value);
			}
		}

		// Token: 0x17001BB0 RID: 7088
		// (get) Token: 0x06005415 RID: 21525 RVA: 0x001026BB File Offset: 0x001008BB
		// (set) Token: 0x06005416 RID: 21526 RVA: 0x001026C8 File Offset: 0x001008C8
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Justify")]
		[Localizable(true)]
		public string ToolBarAlignJustify
		{
			get
			{
				return this.GetString("ToolBarAlignJustify");
			}
			set
			{
				this.SetString("ToolBarAlignJustify", value);
			}
		}

		// Token: 0x17001BB1 RID: 7089
		// (get) Token: 0x06005417 RID: 21527 RVA: 0x001026D6 File Offset: 0x001008D6
		// (set) Token: 0x06005418 RID: 21528 RVA: 0x001026E3 File Offset: 0x001008E3
		[ScriptIgnore]
		[Localizable(true)]
		[Category("Toolbar")]
		[DefaultValue("Vertical alignment")]
		[NotifyParentProperty(true)]
		public string ToolBarVerticalAlignment
		{
			get
			{
				return this.GetString("ToolBarVerticalAlignment");
			}
			set
			{
				this.SetString("ToolBarVerticalAlignment", value);
			}
		}

		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x06005419 RID: 21529 RVA: 0x001026F1 File Offset: 0x001008F1
		// (set) Token: 0x0600541A RID: 21530 RVA: 0x001026FE File Offset: 0x001008FE
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Align Top")]
		[Localizable(true)]
		public string ToolBarAlignTop
		{
			get
			{
				return this.GetString("ToolBarAlignTop");
			}
			set
			{
				this.SetString("ToolBarAlignTop", value);
			}
		}

		// Token: 0x17001BB3 RID: 7091
		// (get) Token: 0x0600541B RID: 21531 RVA: 0x0010270C File Offset: 0x0010090C
		// (set) Token: 0x0600541C RID: 21532 RVA: 0x00102719 File Offset: 0x00100919
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Toolbar")]
		[DefaultValue("Align Middle")]
		[ScriptIgnore]
		public string ToolBarAlignMiddle
		{
			get
			{
				return this.GetString("ToolBarAlignMiddle");
			}
			set
			{
				this.SetString("ToolBarAlignMiddle", value);
			}
		}

		// Token: 0x17001BB4 RID: 7092
		// (get) Token: 0x0600541D RID: 21533 RVA: 0x00102727 File Offset: 0x00100927
		// (set) Token: 0x0600541E RID: 21534 RVA: 0x00102734 File Offset: 0x00100934
		[DefaultValue("Align Bottom")]
		[ScriptIgnore]
		[Category("Toolbar")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ToolBarAlignBottom
		{
			get
			{
				return this.GetString("ToolBarAlignBottom");
			}
			set
			{
				this.SetString("ToolBarAlignBottom", value);
			}
		}

		// Token: 0x17001BB5 RID: 7093
		// (get) Token: 0x0600541F RID: 21535 RVA: 0x00102742 File Offset: 0x00100942
		// (set) Token: 0x06005420 RID: 21536 RVA: 0x0010274F File Offset: 0x0010094F
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[Localizable(true)]
		[DefaultValue("Wrap text")]
		public string ToolBarTextWrap
		{
			get
			{
				return this.GetString("ToolBarTextWrap");
			}
			set
			{
				this.SetString("ToolBarTextWrap", value);
			}
		}

		// Token: 0x17001BB6 RID: 7094
		// (get) Token: 0x06005421 RID: 21537 RVA: 0x0010275D File Offset: 0x0010095D
		// (set) Token: 0x06005422 RID: 21538 RVA: 0x0010276A File Offset: 0x0010096A
		[DefaultValue("Format")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarFormat
		{
			get
			{
				return this.GetString("ToolBarFormat");
			}
			set
			{
				this.SetString("ToolBarFormat", value);
			}
		}

		// Token: 0x17001BB7 RID: 7095
		// (get) Token: 0x06005423 RID: 21539 RVA: 0x00102778 File Offset: 0x00100978
		// (set) Token: 0x06005424 RID: 21540 RVA: 0x00102785 File Offset: 0x00100985
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Automatic")]
		[ScriptIgnore]
		[Localizable(true)]
		public string ToolBarFormatAutomatic
		{
			get
			{
				return this.GetString("ToolBarFormatAutomatic");
			}
			set
			{
				this.SetString("ToolBarFormatAutomatic", value);
			}
		}

		// Token: 0x17001BB8 RID: 7096
		// (get) Token: 0x06005425 RID: 21541 RVA: 0x00102793 File Offset: 0x00100993
		// (set) Token: 0x06005426 RID: 21542 RVA: 0x001027A0 File Offset: 0x001009A0
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Number")]
		[Category("Toolbar")]
		[ScriptIgnore]
		public string ToolBarFormatNumber
		{
			get
			{
				return this.GetString("ToolBarFormatNumber");
			}
			set
			{
				this.SetString("ToolBarFormatNumber", value);
			}
		}

		// Token: 0x17001BB9 RID: 7097
		// (get) Token: 0x06005427 RID: 21543 RVA: 0x001027AE File Offset: 0x001009AE
		// (set) Token: 0x06005428 RID: 21544 RVA: 0x001027BB File Offset: 0x001009BB
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Percent")]
		[ScriptIgnore]
		[Localizable(true)]
		public string ToolBarFormatPercent
		{
			get
			{
				return this.GetString("ToolBarFormatPercent");
			}
			set
			{
				this.SetString("ToolBarFormatPercent", value);
			}
		}

		// Token: 0x17001BBA RID: 7098
		// (get) Token: 0x06005429 RID: 21545 RVA: 0x001027C9 File Offset: 0x001009C9
		// (set) Token: 0x0600542A RID: 21546 RVA: 0x001027D6 File Offset: 0x001009D6
		[DefaultValue("Financial")]
		[Category("Toolbar")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ToolBarFormatFinancial
		{
			get
			{
				return this.GetString("ToolBarFormatFinancial");
			}
			set
			{
				this.SetString("ToolBarFormatFinancial", value);
			}
		}

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x001027E4 File Offset: 0x001009E4
		// (set) Token: 0x0600542C RID: 21548 RVA: 0x001027F1 File Offset: 0x001009F1
		[Category("Toolbar")]
		[ScriptIgnore]
		[DefaultValue("Currency")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ToolBarFormatCurrency
		{
			get
			{
				return this.GetString("ToolBarFormatCurrency");
			}
			set
			{
				this.SetString("ToolBarFormatCurrency", value);
			}
		}

		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x0600542D RID: 21549 RVA: 0x001027FF File Offset: 0x001009FF
		// (set) Token: 0x0600542E RID: 21550 RVA: 0x0010280C File Offset: 0x00100A0C
		[Category("Toolbar")]
		[ScriptIgnore]
		[DefaultValue("Date")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ToolBarFormatDate
		{
			get
			{
				return this.GetString("ToolBarFormatDate");
			}
			set
			{
				this.SetString("ToolBarFormatDate", value);
			}
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x0600542F RID: 21551 RVA: 0x0010281A File Offset: 0x00100A1A
		// (set) Token: 0x06005430 RID: 21552 RVA: 0x00102827 File Offset: 0x00100A27
		[NotifyParentProperty(true)]
		[DefaultValue("Time")]
		[ScriptIgnore]
		[Localizable(true)]
		[Category("Toolbar")]
		public string ToolBarFormatTime
		{
			get
			{
				return this.GetString("ToolBarFormatTime");
			}
			set
			{
				this.SetString("ToolBarFormatTime", value);
			}
		}

		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x06005431 RID: 21553 RVA: 0x00102835 File Offset: 0x00100A35
		// (set) Token: 0x06005432 RID: 21554 RVA: 0x00102842 File Offset: 0x00100A42
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("DateTime")]
		public string ToolBarFormatDateTime
		{
			get
			{
				return this.GetString("ToolBarFormatDateTime");
			}
			set
			{
				this.SetString("ToolBarFormatDateTime", value);
			}
		}

		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x06005433 RID: 21555 RVA: 0x00102850 File Offset: 0x00100A50
		// (set) Token: 0x06005434 RID: 21556 RVA: 0x0010285D File Offset: 0x00100A5D
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Duration")]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarFormatDuration
		{
			get
			{
				return this.GetString("ToolBarFormatDuration");
			}
			set
			{
				this.SetString("ToolBarFormatDuration", value);
			}
		}

		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x06005435 RID: 21557 RVA: 0x0010286B File Offset: 0x00100A6B
		// (set) Token: 0x06005436 RID: 21558 RVA: 0x00102878 File Offset: 0x00100A78
		[Localizable(true)]
		[DefaultValue("More formats...")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarMoreFormats
		{
			get
			{
				return this.GetString("ToolBarMoreFormats");
			}
			set
			{
				this.SetString("ToolBarMoreFormats", value);
			}
		}

		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x00102886 File Offset: 0x00100A86
		// (set) Token: 0x06005438 RID: 21560 RVA: 0x00102893 File Offset: 0x00100A93
		[DefaultValue("Increase decimal")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarFormatIncreaseDecimal
		{
			get
			{
				return this.GetString("ToolBarFormatIncreaseDecimal");
			}
			set
			{
				this.SetString("ToolBarFormatIncreaseDecimal", value);
			}
		}

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x06005439 RID: 21561 RVA: 0x001028A1 File Offset: 0x00100AA1
		// (set) Token: 0x0600543A RID: 21562 RVA: 0x001028AE File Offset: 0x00100AAE
		[Category("Toolbar")]
		[DefaultValue("Decrease decimal")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string ToolBarFormatDecreaseDecimal
		{
			get
			{
				return this.GetString("ToolBarFormatDecreaseDecimal");
			}
			set
			{
				this.SetString("ToolBarFormatDecreaseDecimal", value);
			}
		}

		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x0600543B RID: 21563 RVA: 0x001028BC File Offset: 0x00100ABC
		// (set) Token: 0x0600543C RID: 21564 RVA: 0x001028C9 File Offset: 0x00100AC9
		[DefaultValue("Freeze panes")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarFreezePanes
		{
			get
			{
				return this.GetString("ToolBarFreezePanes");
			}
			set
			{
				this.SetString("ToolBarFreezePanes", value);
			}
		}

		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x0600543D RID: 21565 RVA: 0x001028D7 File Offset: 0x00100AD7
		// (set) Token: 0x0600543E RID: 21566 RVA: 0x001028E4 File Offset: 0x00100AE4
		[ScriptIgnore]
		[Category("Toolbar")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Freeze rows")]
		public string ToolBarFreezeRows
		{
			get
			{
				return this.GetString("ToolBarFreezeRows");
			}
			set
			{
				this.SetString("ToolBarFreezeRows", value);
			}
		}

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x0600543F RID: 21567 RVA: 0x001028F2 File Offset: 0x00100AF2
		// (set) Token: 0x06005440 RID: 21568 RVA: 0x001028FF File Offset: 0x00100AFF
		[Category("Toolbar")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Freeze columns")]
		[ScriptIgnore]
		public string ToolBarFreezeColumns
		{
			get
			{
				return this.GetString("ToolBarFreezeColumns");
			}
			set
			{
				this.SetString("ToolBarFreezeColumns", value);
			}
		}

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x06005441 RID: 21569 RVA: 0x0010290D File Offset: 0x00100B0D
		// (set) Token: 0x06005442 RID: 21570 RVA: 0x0010291A File Offset: 0x00100B1A
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Unfreeze panes")]
		[Localizable(true)]
		public string ToolBarUnfreeze
		{
			get
			{
				return this.GetString("ToolBarUnfreeze");
			}
			set
			{
				this.SetString("ToolBarUnfreeze", value);
			}
		}

		// Token: 0x17001BC7 RID: 7111
		// (get) Token: 0x06005443 RID: 21571 RVA: 0x00102928 File Offset: 0x00100B28
		// (set) Token: 0x06005444 RID: 21572 RVA: 0x00102935 File Offset: 0x00100B35
		[ScriptIgnore]
		[Category("Toolbar")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Font Size")]
		public string ToolBarFontSize
		{
			get
			{
				return this.GetString("ToolBarFontSize");
			}
			set
			{
				this.SetString("ToolBarFontSize", value);
			}
		}

		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x06005445 RID: 21573 RVA: 0x00102943 File Offset: 0x00100B43
		// (set) Token: 0x06005446 RID: 21574 RVA: 0x00102950 File Offset: 0x00100B50
		[DefaultValue("Font Family")]
		[Localizable(true)]
		[Category("Toolbar")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string ToolBarFontFamily
		{
			get
			{
				return this.GetString("ToolBarFontFamily");
			}
			set
			{
				this.SetString("ToolBarFontFamily", value);
			}
		}

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x06005447 RID: 21575 RVA: 0x0010295E File Offset: 0x00100B5E
		// (set) Token: 0x06005448 RID: 21576 RVA: 0x0010296B File Offset: 0x00100B6B
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Background Color")]
		[Localizable(true)]
		[ScriptIgnore]
		public string ToolBarBackgroundColor
		{
			get
			{
				return this.GetString("ToolBarBackgroundColor");
			}
			set
			{
				this.SetString("ToolBarBackgroundColor", value);
			}
		}

		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x06005449 RID: 21577 RVA: 0x00102979 File Offset: 0x00100B79
		// (set) Token: 0x0600544A RID: 21578 RVA: 0x00102986 File Offset: 0x00100B86
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Text Color")]
		[ScriptIgnore]
		[Category("Toolbar")]
		public string ToolBarTextColor
		{
			get
			{
				return this.GetString("ToolBarTextColor");
			}
			set
			{
				this.SetString("ToolBarTextColor", value);
			}
		}

		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x0600544B RID: 21579 RVA: 0x00102994 File Offset: 0x00100B94
		// (set) Token: 0x0600544C RID: 21580 RVA: 0x001029A1 File Offset: 0x00100BA1
		[Category("Toolbar")]
		[DefaultValue("Merge Cells")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ToolBarMergeCells
		{
			get
			{
				return this.GetString("ToolBarMergeCells");
			}
			set
			{
				this.SetString("ToolBarMergeCells", value);
			}
		}

		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x0600544D RID: 21581 RVA: 0x001029AF File Offset: 0x00100BAF
		// (set) Token: 0x0600544E RID: 21582 RVA: 0x001029BC File Offset: 0x00100BBC
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Merge Horizontally")]
		[Localizable(true)]
		[Category("Toolbar")]
		public string ToolBarMergeHorizontally
		{
			get
			{
				return this.GetString("ToolBarMergeHorizontally");
			}
			set
			{
				this.SetString("ToolBarMergeHorizontally", value);
			}
		}

		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x0600544F RID: 21583 RVA: 0x001029CA File Offset: 0x00100BCA
		// (set) Token: 0x06005450 RID: 21584 RVA: 0x001029D7 File Offset: 0x00100BD7
		[NotifyParentProperty(true)]
		[DefaultValue("Merge Vertically")]
		[Localizable(true)]
		[ScriptIgnore]
		[Category("Toolbar")]
		public string ToolBarMergeVertically
		{
			get
			{
				return this.GetString("ToolBarMergeVertically");
			}
			set
			{
				this.SetString("ToolBarMergeVertically", value);
			}
		}

		// Token: 0x17001BCE RID: 7118
		// (get) Token: 0x06005451 RID: 21585 RVA: 0x001029E5 File Offset: 0x00100BE5
		// (set) Token: 0x06005452 RID: 21586 RVA: 0x001029F2 File Offset: 0x00100BF2
		[ScriptIgnore]
		[DefaultValue("Unmerge")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarUnmerge
		{
			get
			{
				return this.GetString("ToolBarUnmerge");
			}
			set
			{
				this.SetString("ToolBarUnmerge", value);
			}
		}

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x06005453 RID: 21587 RVA: 0x00102A00 File Offset: 0x00100C00
		// (set) Token: 0x06005454 RID: 21588 RVA: 0x00102A0D File Offset: 0x00100C0D
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Insert cells")]
		public string ToolBarInsertCells
		{
			get
			{
				return this.GetString("ToolBarInsertCells");
			}
			set
			{
				this.SetString("ToolBarInsertCells", value);
			}
		}

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x06005455 RID: 21589 RVA: 0x00102A1B File Offset: 0x00100C1B
		// (set) Token: 0x06005456 RID: 21590 RVA: 0x00102A28 File Offset: 0x00100C28
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Add column left")]
		public string ToolBarAddColumnLeft
		{
			get
			{
				return this.GetString("ToolBarAddColumnLeft");
			}
			set
			{
				this.SetString("ToolBarAddColumnLeft", value);
			}
		}

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x06005457 RID: 21591 RVA: 0x00102A36 File Offset: 0x00100C36
		// (set) Token: 0x06005458 RID: 21592 RVA: 0x00102A43 File Offset: 0x00100C43
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Insert Comment")]
		public string InsertComment
		{
			get
			{
				return this.GetString("InsertComment");
			}
			set
			{
				this.SetString("InsertComment", value);
			}
		}

		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x06005459 RID: 21593 RVA: 0x00102A51 File Offset: 0x00100C51
		// (set) Token: 0x0600545A RID: 21594 RVA: 0x00102A5E File Offset: 0x00100C5E
		[Localizable(true)]
		[Category("Toolbar")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Add column right")]
		public string ToolBarAddColumnRight
		{
			get
			{
				return this.GetString("ToolBarAddColumnRight");
			}
			set
			{
				this.SetString("ToolBarAddColumnRight", value);
			}
		}

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x0600545B RID: 21595 RVA: 0x00102A6C File Offset: 0x00100C6C
		// (set) Token: 0x0600545C RID: 21596 RVA: 0x00102A79 File Offset: 0x00100C79
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Add row above")]
		[Localizable(true)]
		public string ToolBarAddRowAbove
		{
			get
			{
				return this.GetString("ToolBarAddRowAbove");
			}
			set
			{
				this.SetString("ToolBarAddRowAbove", value);
			}
		}

		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x0600545D RID: 21597 RVA: 0x00102A87 File Offset: 0x00100C87
		// (set) Token: 0x0600545E RID: 21598 RVA: 0x00102A94 File Offset: 0x00100C94
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Add row below")]
		[ScriptIgnore]
		[Category("Toolbar")]
		public string ToolBarAddRowBelow
		{
			get
			{
				return this.GetString("ToolBarAddRowBelow");
			}
			set
			{
				this.SetString("ToolBarAddRowBelow", value);
			}
		}

		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x0600545F RID: 21599 RVA: 0x00102AA2 File Offset: 0x00100CA2
		// (set) Token: 0x06005460 RID: 21600 RVA: 0x00102AAF File Offset: 0x00100CAF
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Delete cells")]
		public string ToolBarDeleteCells
		{
			get
			{
				return this.GetString("ToolBarDeleteCells");
			}
			set
			{
				this.SetString("ToolBarDeleteCells", value);
			}
		}

		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x06005461 RID: 21601 RVA: 0x00102ABD File Offset: 0x00100CBD
		// (set) Token: 0x06005462 RID: 21602 RVA: 0x00102ACA File Offset: 0x00100CCA
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Category("Toolbar")]
		[DefaultValue("Delete row")]
		public string ToolBarDeleteRow
		{
			get
			{
				return this.GetString("ToolBarDeleteRow");
			}
			set
			{
				this.SetString("ToolBarDeleteRow", value);
			}
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x06005463 RID: 21603 RVA: 0x00102AD8 File Offset: 0x00100CD8
		// (set) Token: 0x06005464 RID: 21604 RVA: 0x00102AE5 File Offset: 0x00100CE5
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Delete column")]
		[Category("Toolbar")]
		[Localizable(true)]
		public string ToolBarDeleteColumn
		{
			get
			{
				return this.GetString("ToolBarDeleteColumn");
			}
			set
			{
				this.SetString("ToolBarDeleteColumn", value);
			}
		}

		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x06005465 RID: 21605 RVA: 0x00102AF3 File Offset: 0x00100CF3
		// (set) Token: 0x06005466 RID: 21606 RVA: 0x00102B00 File Offset: 0x00100D00
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Toolbar")]
		[DefaultValue("Sort")]
		public string ToolBarSort
		{
			get
			{
				return this.GetString("ToolBarSort");
			}
			set
			{
				this.SetString("ToolBarSort", value);
			}
		}

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x06005467 RID: 21607 RVA: 0x00102B0E File Offset: 0x00100D0E
		// (set) Token: 0x06005468 RID: 21608 RVA: 0x00102B1B File Offset: 0x00100D1B
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[DefaultValue("Sort range A to Z")]
		[ScriptIgnore]
		public string ToolBarSortAscending
		{
			get
			{
				return this.GetString("ToolBarSortAscending");
			}
			set
			{
				this.SetString("ToolBarSortAscending", value);
			}
		}

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x06005469 RID: 21609 RVA: 0x00102B29 File Offset: 0x00100D29
		// (set) Token: 0x0600546A RID: 21610 RVA: 0x00102B36 File Offset: 0x00100D36
		[DefaultValue("Sort range Z to A")]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		[Localizable(true)]
		[ScriptIgnore]
		public string ToolBarSortDescending
		{
			get
			{
				return this.GetString("ToolBarSortDescending");
			}
			set
			{
				this.SetString("ToolBarSortDescending", value);
			}
		}

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x0600546B RID: 21611 RVA: 0x00102B44 File Offset: 0x00100D44
		// (set) Token: 0x0600546C RID: 21612 RVA: 0x00102B51 File Offset: 0x00100D51
		[DefaultValue("Filter")]
		[ScriptIgnore]
		[Category("Toolbar")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ToolBarFilter
		{
			get
			{
				return this.GetString("ToolBarFilter");
			}
			set
			{
				this.SetString("ToolBarFilter", value);
			}
		}

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x0600546D RID: 21613 RVA: 0x00102B5F File Offset: 0x00100D5F
		// (set) Token: 0x0600546E RID: 21614 RVA: 0x00102B6C File Offset: 0x00100D6C
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Data Validation")]
		[ScriptIgnore]
		[Category("Toolbar")]
		public string ToolBarValidation
		{
			get
			{
				return this.GetString("ToolBarValidation");
			}
			set
			{
				this.SetString("ToolBarValidation", value);
			}
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x00102B7A File Offset: 0x00100D7A
		// (set) Token: 0x06005470 RID: 21616 RVA: 0x00102B87 File Offset: 0x00100D87
		[ScriptIgnore]
		[DefaultValue("Toggle gridlines")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Toolbar")]
		public string ToolBarGridLines
		{
			get
			{
				return this.GetString("ToolBarGridLines");
			}
			set
			{
				this.SetString("ToolBarGridLines", value);
			}
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x06005471 RID: 21617 RVA: 0x00102B95 File Offset: 0x00100D95
		// (set) Token: 0x06005472 RID: 21618 RVA: 0x00102BA2 File Offset: 0x00100DA2
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("MessageDialog")]
		[ClientPropertyName("useKeyboardText")]
		[DefaultValue("These actions cannot be invoked through the menu. Please use the keyboard shortcuts instead")]
		[ScriptIgnore]
		public string MessageDialogUseKeyboardText
		{
			get
			{
				return this.GetString("MessageDialogUseKeyboardText");
			}
			set
			{
				this.SetString("MessageDialogUseKeyboardText", value);
			}
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x06005473 RID: 21619 RVA: 0x00102BB0 File Offset: 0x00100DB0
		// (set) Token: 0x06005474 RID: 21620 RVA: 0x00102BBD File Offset: 0x00100DBD
		[ClientPropertyName("useKeyboardTitle")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("MessageDialog")]
		[DefaultValue("Copying and pasting")]
		public string MessageDialogUseKeyboardTitle
		{
			get
			{
				return this.GetString("MessageDialogUseKeyboardTitle");
			}
			set
			{
				this.SetString("MessageDialogUseKeyboardTitle", value);
			}
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x06005475 RID: 21621 RVA: 0x00102BCB File Offset: 0x00100DCB
		// (set) Token: 0x06005476 RID: 21622 RVA: 0x00102BD8 File Offset: 0x00100DD8
		[Localizable(true)]
		[ClientPropertyName("useKeyboardOK")]
		[DefaultValue("OK")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("MessageDialog")]
		public string MessageDialogUseKeyboardOK
		{
			get
			{
				return this.GetString("MessageDialogUseKeyboardOK");
			}
			set
			{
				this.SetString("MessageDialogUseKeyboardOK", value);
			}
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x06005477 RID: 21623 RVA: 0x00102BE6 File Offset: 0x00100DE6
		// (set) Token: 0x06005478 RID: 21624 RVA: 0x00102BF3 File Offset: 0x00100DF3
		[DefaultValue("Data Validation")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("MessageDialog")]
		[ClientPropertyName("validationTitle")]
		public string ValidationDialogTitle
		{
			get
			{
				return this.GetString("ValidationDialogTitle");
			}
			set
			{
				this.SetString("ValidationDialogTitle", value);
			}
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x06005479 RID: 21625 RVA: 0x00102C01 File Offset: 0x00100E01
		// (set) Token: 0x0600547A RID: 21626 RVA: 0x00102C0E File Offset: 0x00100E0E
		[ClientPropertyName("formatTitle")]
		[DefaultValue("Format")]
		[Category("MessageDialog")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string FormatDialogTitle
		{
			get
			{
				return this.GetString("FormatDialogTitle");
			}
			set
			{
				this.SetString("FormatDialogTitle", value);
			}
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x0600547B RID: 21627 RVA: 0x00102C1C File Offset: 0x00100E1C
		// (set) Token: 0x0600547C RID: 21628 RVA: 0x00102C29 File Offset: 0x00100E29
		[NotifyParentProperty(true)]
		[DefaultValue("Cannot change part of a merged cell.")]
		[ScriptIgnore]
		[Localizable(true)]
		[Category("MessageDialog")]
		[ClientPropertyName("modifyMerged")]
		public string MessageDialogModifyMerged
		{
			get
			{
				return this.GetString("MessageDialogModifyMerged");
			}
			set
			{
				this.SetString("MessageDialogModifyMerged", value);
			}
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x0600547D RID: 21629 RVA: 0x00102C37 File Offset: 0x00100E37
		// (set) Token: 0x0600547E RID: 21630 RVA: 0x00102C44 File Offset: 0x00100E44
		[DefaultValue("Destination range contains disabled cells.")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("MessageDialog")]
		[ClientPropertyName("rangeDisabled")]
		public string MessageDialogRangeDisabled
		{
			get
			{
				return this.GetString("MessageDialogRangeDisabled");
			}
			set
			{
				this.SetString("MessageDialogRangeDisabled", value);
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x0600547F RID: 21631 RVA: 0x00102C52 File Offset: 0x00100E52
		// (set) Token: 0x06005480 RID: 21632 RVA: 0x00102C5F File Offset: 0x00100E5F
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("MessageDialog")]
		[ClientPropertyName("overflow")]
		[DefaultValue("Cannot paste, because the copy area and the paste area are not the same size and shape.")]
		public string MessageDialogOverflow
		{
			get
			{
				return this.GetString("MessageDialogOverflow");
			}
			set
			{
				this.SetString("MessageDialogOverflow", value);
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x06005481 RID: 21633 RVA: 0x00102C6D File Offset: 0x00100E6D
		// (set) Token: 0x06005482 RID: 21634 RVA: 0x00102C7A File Offset: 0x00100E7A
		[Localizable(true)]
		[Category("MessageDialog")]
		[NotifyParentProperty(true)]
		[ClientPropertyName("unsupportedSelection")]
		[DefaultValue("That action cannot be performed on multiple selection.")]
		[ScriptIgnore]
		public string MessageDialogUnsupportedSelection
		{
			get
			{
				return this.GetString("MessageDialogUnsupportedSelection");
			}
			set
			{
				this.SetString("MessageDialogUnsupportedSelection", value);
			}
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x06005483 RID: 21635 RVA: 0x00102C88 File Offset: 0x00100E88
		// (set) Token: 0x06005484 RID: 21636 RVA: 0x00102C95 File Offset: 0x00100E95
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("MessageDialog")]
		[ClientPropertyName("incompatibleRanges")]
		[Localizable(true)]
		[DefaultValue("Incompatible ranges")]
		public string MessageDialogIncompatibleRanges
		{
			get
			{
				return this.GetString("MessageDialogIncompatibleRanges");
			}
			set
			{
				this.SetString("MessageDialogIncompatibleRanges", value);
			}
		}

		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x06005485 RID: 21637 RVA: 0x00102CA3 File Offset: 0x00100EA3
		// (set) Token: 0x06005486 RID: 21638 RVA: 0x00102CB0 File Offset: 0x00100EB0
		[DefaultValue("Cannot determine fill direction")]
		[ClientPropertyName("noFillDirection")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("MessageDialog")]
		public string MessageDialogNoFillDirection
		{
			get
			{
				return this.GetString("MessageDialogNoFillDirection");
			}
			set
			{
				this.SetString("MessageDialogNoFillDirection", value);
			}
		}

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x06005487 RID: 21639 RVA: 0x00102CBE File Offset: 0x00100EBE
		// (set) Token: 0x06005488 RID: 21640 RVA: 0x00102CCB File Offset: 0x00100ECB
		[DefaultValue("Duplicate sheet name")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("MessageDialog")]
		[ClientPropertyName("duplicateSheetName")]
		public string MessageDialogDuplicateSheetName
		{
			get
			{
				return this.GetString("MessageDialogDuplicateSheetName");
			}
			set
			{
				this.SetString("MessageDialogDuplicateSheetName", value);
			}
		}

		// Token: 0x17001BEA RID: 7146
		// (get) Token: 0x06005489 RID: 21641 RVA: 0x00102CD9 File Offset: 0x00100ED9
		// (set) Token: 0x0600548A RID: 21642 RVA: 0x00102CE6 File Offset: 0x00100EE6
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("ConfirmationDialog")]
		[ClientPropertyName("ok")]
		[DefaultValue("OK")]
		public string ConfirmationDialogOK
		{
			get
			{
				return this.GetString("ConfirmationDialogOK");
			}
			set
			{
				this.SetString("ConfirmationDialogOK", value);
			}
		}

		// Token: 0x17001BEB RID: 7147
		// (get) Token: 0x0600548B RID: 21643 RVA: 0x00102CF4 File Offset: 0x00100EF4
		// (set) Token: 0x0600548C RID: 21644 RVA: 0x00102D01 File Offset: 0x00100F01
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("ConfirmationDialog")]
		[ClientPropertyName("cancel")]
		[DefaultValue("Cancel")]
		public string ConfirmationDialogCancel
		{
			get
			{
				return this.GetString("ConfirmationDialogCancel");
			}
			set
			{
				this.SetString("ConfirmationDialogCancel", value);
			}
		}

		// Token: 0x17001BEC RID: 7148
		// (get) Token: 0x0600548D RID: 21645 RVA: 0x00102D0F File Offset: 0x00100F0F
		// (set) Token: 0x0600548E RID: 21646 RVA: 0x00102D1C File Offset: 0x00100F1C
		[NotifyParentProperty(true)]
		[DefaultValue("Sheet remove")]
		[ClientPropertyName("title")]
		[Localizable(true)]
		[Category("ConfirmationDialog")]
		[ScriptIgnore]
		public string ConfirmationDialogTitle
		{
			get
			{
				return this.GetString("ConfirmationDialogTitle");
			}
			set
			{
				this.SetString("ConfirmationDialogTitle", value);
			}
		}

		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x0600548F RID: 21647 RVA: 0x00102D2A File Offset: 0x00100F2A
		// (set) Token: 0x06005490 RID: 21648 RVA: 0x00102D37 File Offset: 0x00100F37
		[Localizable(true)]
		[Category("ConfirmationDialog")]
		[NotifyParentProperty(true)]
		[DefaultValue("Are you sure you want to remove this sheet?")]
		[ScriptIgnore]
		[ClientPropertyName("text")]
		public string ConfirmationDialogText
		{
			get
			{
				return this.GetString("ConfirmationDialogText");
			}
			set
			{
				this.SetString("ConfirmationDialogText", value);
			}
		}
	}
}
