using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000B85 RID: 2949
	public abstract class FillStyleBase : ObjectWithState
	{
		// Token: 0x06006F73 RID: 28531 RVA: 0x001A0840 File Offset: 0x0019EA40
		public FillStyleBase(string prefix, StateBag OwnerStateBag) : base("fsb" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17002486 RID: 9350
		// (get) Token: 0x06006F74 RID: 28532 RVA: 0x001A0854 File Offset: 0x0019EA54
		// (set) Token: 0x06006F75 RID: 28533 RVA: 0x001A0879 File Offset: 0x0019EA79
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(ColorConverter))]
		public Color BackgroundColor
		{
			get
			{
				return (Color)(base.ViewState["BackgroundColor"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["BackgroundColor"] = value;
			}
		}

		// Token: 0x06006F76 RID: 28534
		internal abstract string Serialize();
	}
}
