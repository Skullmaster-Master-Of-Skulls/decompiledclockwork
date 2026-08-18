using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C4 RID: 196
	[LayoutRenderer("asp-request")]
	public class AspRequestValueLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		[DefaultParameter]
		public string Item { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000CDC1 File Offset: 0x0000AFC1
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0000CDC9 File Offset: 0x0000AFC9
		public string QueryString { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000CDD2 File Offset: 0x0000AFD2
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0000CDDA File Offset: 0x0000AFDA
		public string Form { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000CDE3 File Offset: 0x0000AFE3
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0000CDEB File Offset: 0x0000AFEB
		public string Cookie { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		public string ServerVariable { get; set; }

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000CE08 File Offset: 0x0000B008
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			AspHelper.IRequest requestObject = AspHelper.GetRequestObject();
			if (requestObject != null)
			{
				if (this.QueryString != null)
				{
					builder.Append(AspRequestValueLayoutRenderer.GetItem(requestObject.GetQueryString(), this.QueryString));
				}
				else if (this.Form != null)
				{
					builder.Append(AspRequestValueLayoutRenderer.GetItem(requestObject.GetForm(), this.Form));
				}
				else if (this.Cookie != null)
				{
					object item = requestObject.GetCookies().GetItem(this.Cookie);
					builder.Append(Convert.ToString(AspHelper.GetComDefaultProperty(item), CultureInfo.InvariantCulture));
				}
				else if (this.ServerVariable != null)
				{
					builder.Append(AspRequestValueLayoutRenderer.GetItem(requestObject.GetServerVariables(), this.ServerVariable));
				}
				else if (this.Item != null)
				{
					AspHelper.IDispatch item2 = requestObject.GetItem(this.Item);
					AspHelper.IStringList stringList = item2 as AspHelper.IStringList;
					if (stringList != null)
					{
						if (stringList.GetCount() > 0)
						{
							builder.Append(stringList.GetItem(1));
						}
						Marshal.ReleaseComObject(stringList);
					}
				}
				Marshal.ReleaseComObject(requestObject);
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000CF0C File Offset: 0x0000B10C
		private static string GetItem(AspHelper.IRequestDictionary dict, string key)
		{
			object value = null;
			object item = dict.GetItem(key);
			AspHelper.IStringList stringList = item as AspHelper.IStringList;
			if (stringList != null)
			{
				if (stringList.GetCount() > 0)
				{
					value = stringList.GetItem(1);
				}
				Marshal.ReleaseComObject(stringList);
				return Convert.ToString(value, CultureInfo.InvariantCulture);
			}
			return item.GetType().ToString();
		}
	}
}
