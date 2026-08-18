using System;
using System.Collections.Generic;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters.SeriesItems;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters.Bullet
{
	// Token: 0x020003B9 RID: 953
	public class BulletSeriesItemConverter : SeriesItemBaseConverter
	{
		// Token: 0x06002329 RID: 9001 RVA: 0x00075C74 File Offset: 0x00073E74
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			base.PopulateProperties(state, obj);
			BulletSeriesItem bulletSeriesItem = (BulletSeriesItem)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "current", bulletSeriesItem.Current, null);
			ExplicitJavaScriptConverter.AddProperty(state, "target", bulletSeriesItem.Target, null);
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x00075CC0 File Offset: 0x00073EC0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(BulletSeriesItem)
				};
			}
		}
	}
}
