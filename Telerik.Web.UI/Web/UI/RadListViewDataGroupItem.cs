using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200199E RID: 6558
	public class RadListViewDataGroupItem : RadListViewItem
	{
		// Token: 0x0600FDAA RID: 64938 RVA: 0x0038FB73 File Offset: 0x0038DD73
		public RadListViewDataGroupItem(RadListView ownerView) : base(RadListViewItemType.DataGroupItem, ownerView)
		{
		}

		// Token: 0x17004C91 RID: 19601
		// (get) Token: 0x0600FDAB RID: 64939 RVA: 0x0038FB7D File Offset: 0x0038DD7D
		// (set) Token: 0x0600FDAC RID: 64940 RVA: 0x0038FB85 File Offset: 0x0038DD85
		public object DataGroupKey { get; set; }

		// Token: 0x17004C92 RID: 19602
		// (get) Token: 0x0600FDAD RID: 64941 RVA: 0x0038FB8E File Offset: 0x0038DD8E
		// (set) Token: 0x0600FDAE RID: 64942 RVA: 0x0038FB96 File Offset: 0x0038DD96
		public string FieldName { get; set; }

		// Token: 0x17004C93 RID: 19603
		// (get) Token: 0x0600FDAF RID: 64943 RVA: 0x0038FB9F File Offset: 0x0038DD9F
		// (set) Token: 0x0600FDB0 RID: 64944 RVA: 0x0038FBBA File Offset: 0x0038DDBA
		[DefaultValue(null)]
		[Browsable(false)]
		public IDictionary AggregatesValues
		{
			get
			{
				if (this.aggregatesValues == null)
				{
					this.aggregatesValues = new ListDictionary();
				}
				return this.aggregatesValues;
			}
			set
			{
				this.aggregatesValues = value;
			}
		}

		// Token: 0x040047FE RID: 18430
		private IDictionary aggregatesValues;
	}
}
