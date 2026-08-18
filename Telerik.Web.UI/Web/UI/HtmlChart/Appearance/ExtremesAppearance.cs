using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.JavaScriptSerializers;

namespace Telerik.Web.UI.HtmlChart.Appearance
{
	// Token: 0x020004CC RID: 1228
	public class ExtremesAppearance : OutliersAppearance
	{
		// Token: 0x06002C85 RID: 11397 RVA: 0x000924A0 File Offset: 0x000906A0
		public ExtremesAppearance(string prefix, StateBag stateBag) : base(prefix, stateBag)
		{
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x000924AA File Offset: 0x000906AA
		// (set) Token: 0x06002C87 RID: 11399 RVA: 0x000924CB File Offset: 0x000906CB
		[DefaultValue(OutliersMarkersType.Circle)]
		public new OutliersMarkersType MarkersType
		{
			get
			{
				return (OutliersMarkersType)(base.ViewState["MarkersType"] ?? OutliersMarkersType.Circle);
			}
			set
			{
				base.ViewState["MarkersType"] = value;
			}
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000924E4 File Offset: 0x000906E4
		protected override void RegisterConverters()
		{
			base.Serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new ExtremesAppearanceConverter(),
				new BorderAppearanceConverter()
			});
		}
	}
}
