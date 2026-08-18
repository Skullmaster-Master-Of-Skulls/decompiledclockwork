using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017EE RID: 6126
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class StyleTextBlockAxisLabel : StyleTextBlock
	{
		// Token: 0x0600EE74 RID: 61044 RVA: 0x00364FAB File Offset: 0x003631AB
		public StyleTextBlockAxisLabel()
		{
			this.styleTextBlockTextProperties = new TextPropertiesAxisLabel();
		}

		// Token: 0x0600EE75 RID: 61045 RVA: 0x00364FBE File Offset: 0x003631BE
		internal override void Reset()
		{
			base.Reset();
			this.styleTextBlockTextProperties = new TextPropertiesAxisLabel();
		}
	}
}
