using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017EC RID: 6124
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleTextBlockError : StyleTextBlock
	{
		// Token: 0x0600EE6E RID: 61038 RVA: 0x00364F44 File Offset: 0x00363144
		public StyleTextBlockError()
		{
			this.styleTextBlockTextProperties = new TextPropertiesError();
		}

		// Token: 0x0600EE6F RID: 61039 RVA: 0x00364F57 File Offset: 0x00363157
		internal override void Reset()
		{
			base.Reset();
			this.styleTextBlockTextProperties = new TextPropertiesError();
		}
	}
}
