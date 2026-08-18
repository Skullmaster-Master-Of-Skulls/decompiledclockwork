using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001161 RID: 4449
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridPostBackReferences : ObjectWithState
	{
		// Token: 0x0600B570 RID: 46448 RVA: 0x0027FA2E File Offset: 0x0027DC2E
		public GridPostBackReferences(StateBag OwnerStateBag, RadGrid owner) : base("cs_postback_", OwnerStateBag)
		{
			this.owner = owner;
		}

		// Token: 0x0600B571 RID: 46449 RVA: 0x0027FA43 File Offset: 0x0027DC43
		internal string GetPostBackEventReference(string args)
		{
			return string.Format("{{{0};}}", this.owner.Page.ClientScript.GetPostBackEventReference(this.owner, args));
		}

		// Token: 0x17003AAC RID: 15020
		// (get) Token: 0x0600B572 RID: 46450 RVA: 0x0027FA6B File Offset: 0x0027DC6B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string PostBackColumnsReorder
		{
			get
			{
				return this.GetPostBackEventReference("ColumnsReorder");
			}
		}

		// Token: 0x17003AAD RID: 15021
		// (get) Token: 0x0600B573 RID: 46451 RVA: 0x0027FA78 File Offset: 0x0027DC78
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string PostBackGroupByColumn
		{
			get
			{
				return this.GetPostBackEventReference("GroupByColumn");
			}
		}

		// Token: 0x17003AAE RID: 15022
		// (get) Token: 0x0600B574 RID: 46452 RVA: 0x0027FA85 File Offset: 0x0027DC85
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string PostBackEditRow
		{
			get
			{
				return this.GetPostBackEventReference("EditRow");
			}
		}

		// Token: 0x04002FE4 RID: 12260
		private readonly RadGrid owner;
	}
}
