using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017EA RID: 6122
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class StyleAxisItemText : StyleTextBlock
	{
		// Token: 0x0600EE68 RID: 61032 RVA: 0x00364EC3 File Offset: 0x003630C3
		public StyleAxisItemText()
		{
			this.styleTextBlockTextProperties = new TextPropertiesAxisItem();
			base.MaxLength = DefaultValues.DEFAULT_MAX_ITEM_TEXT_LENGTH;
		}

		// Token: 0x0600EE69 RID: 61033 RVA: 0x00364EE1 File Offset: 0x003630E1
		internal override void Reset()
		{
			base.Reset();
			base.MaxLength = DefaultValues.DEFAULT_MAX_ITEM_TEXT_LENGTH;
			this.styleTextBlockTextProperties = new TextPropertiesAxisItem();
		}

		// Token: 0x0600EE6A RID: 61034 RVA: 0x00364EFF File Offset: 0x003630FF
		protected override bool ShouldSerializeMaxLength()
		{
			return base.MaxLength != DefaultValues.DEFAULT_MAX_ITEM_TEXT_LENGTH;
		}

		// Token: 0x0600EE6B RID: 61035 RVA: 0x00364F11 File Offset: 0x00363111
		protected override void ResetMaxLength()
		{
			base.MaxLength = DefaultValues.DEFAULT_MAX_ITEM_TEXT_LENGTH;
		}
	}
}
