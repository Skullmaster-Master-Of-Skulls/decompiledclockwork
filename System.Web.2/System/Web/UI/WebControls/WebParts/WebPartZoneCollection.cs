using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005BA RID: 1466
	public sealed class WebPartZoneCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06004A61 RID: 19041 RVA: 0x000DCEF2 File Offset: 0x000DB0F2
		public WebPartZoneCollection()
		{
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x000F7358 File Offset: 0x000F5558
		public WebPartZoneCollection(ICollection webPartZones)
		{
			if (webPartZones == null)
			{
				throw new ArgumentNullException("webPartZones");
			}
			foreach (object obj in webPartZones)
			{
				if (obj == null)
				{
					throw new ArgumentException(SR.GetString("Collection_CantAddNull"), "webPartZones");
				}
				if (!(obj is WebPartZone))
				{
					throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
					{
						"WebPartZone"
					}), "webPartZones");
				}
				base.InnerList.Add(obj);
			}
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x000DCF98 File Offset: 0x000DB198
		internal int Add(WebPartZoneBase value)
		{
			return base.InnerList.Add(value);
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(WebPartZoneBase value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(WebPartZoneBase value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x170015E9 RID: 5609
		public WebPartZoneBase this[int index]
		{
			get
			{
				return (WebPartZoneBase)base.InnerList[index];
			}
		}

		// Token: 0x170015EA RID: 5610
		public WebPartZoneBase this[string id]
		{
			get
			{
				WebPartZoneBase result = null;
				foreach (object obj in base.InnerList)
				{
					WebPartZoneBase webPartZoneBase = (WebPartZoneBase)obj;
					if (string.Equals(webPartZoneBase.ID, id, StringComparison.OrdinalIgnoreCase))
					{
						result = webPartZoneBase;
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x000DCFA6 File Offset: 0x000DB1A6
		public void CopyTo(WebPartZoneBase[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
