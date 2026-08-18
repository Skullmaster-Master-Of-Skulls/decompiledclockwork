using System;
using System.ComponentModel;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x0200107F RID: 4223
	public class EditorSymbol : EditorValueItem
	{
		// Token: 0x0600A9E4 RID: 43492 RVA: 0x0024DE15 File Offset: 0x0024C015
		public EditorSymbol()
		{
		}

		// Token: 0x0600A9E5 RID: 43493 RVA: 0x0024DE1D File Offset: 0x0024C01D
		public EditorSymbol(char value)
		{
			this.Char = value;
		}

		// Token: 0x1700368B RID: 13963
		// (get) Token: 0x0600A9E6 RID: 43494 RVA: 0x0024DE2C File Offset: 0x0024C02C
		// (set) Token: 0x0600A9E7 RID: 43495 RVA: 0x0024DE47 File Offset: 0x0024C047
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Value
		{
			get
			{
				return this.Char.ToString();
			}
			set
			{
				this.Char = ToolsFileLoader.ParseSymbol(value, ' ');
			}
		}

		// Token: 0x1700368C RID: 13964
		// (get) Token: 0x0600A9E8 RID: 43496 RVA: 0x0024DE57 File Offset: 0x0024C057
		// (set) Token: 0x0600A9E9 RID: 43497 RVA: 0x0024DE83 File Offset: 0x0024C083
		[TypeConverter(typeof(CharTypeConverter))]
		public char Char
		{
			get
			{
				if (base.ViewState["Value"] == null)
				{
					return ' ';
				}
				return (char)base.ViewState["Value"];
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}
	}
}
