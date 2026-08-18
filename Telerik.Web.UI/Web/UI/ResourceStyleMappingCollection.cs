using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.Web.UI
{
	// Token: 0x02001A1D RID: 6685
	public class ResourceStyleMappingCollection : StronglyTypedStateManagedCollection<ResourceStyleMapping>
	{
		// Token: 0x0601039B RID: 66459 RVA: 0x003A07C7 File Offset: 0x0039E9C7
		protected override void SetDirtyObject(object o)
		{
			((ResourceStyleMapping)o).SetDirty();
		}

		// Token: 0x0601039C RID: 66460 RVA: 0x003A07D4 File Offset: 0x0039E9D4
		internal IList<string> GetMatchingClasses(Resource res)
		{
			List<string> list = new List<string>();
			foreach (ResourceStyleMapping resourceStyleMapping in this.GetMatchingStyleMappings(res))
			{
				if (!string.IsNullOrEmpty(resourceStyleMapping.ApplyCssClass))
				{
					list.Add(resourceStyleMapping.ApplyCssClass);
				}
			}
			return list;
		}

		// Token: 0x0601039D RID: 66461 RVA: 0x003A0844 File Offset: 0x0039EA44
		internal Color GetMatchingBackColor(Resource res)
		{
			foreach (ResourceStyleMapping resourceStyleMapping in this.GetMatchingStyleMappings(res))
			{
				if (!(resourceStyleMapping.BackColor == Color.Empty))
				{
					return resourceStyleMapping.BackColor;
				}
			}
			return Color.Empty;
		}

		// Token: 0x0601039E RID: 66462 RVA: 0x003A08B4 File Offset: 0x0039EAB4
		internal Color GetMatchingBorderColor(Resource res)
		{
			foreach (ResourceStyleMapping resourceStyleMapping in this.GetMatchingStyleMappings(res))
			{
				if (!(resourceStyleMapping.BorderColor == Color.Empty))
				{
					return resourceStyleMapping.BorderColor;
				}
			}
			return Color.Empty;
		}

		// Token: 0x0601039F RID: 66463 RVA: 0x003A0924 File Offset: 0x0039EB24
		private List<ResourceStyleMapping> GetMatchingStyleMappings(Resource res)
		{
			List<ResourceStyleMapping> list = new List<ResourceStyleMapping>();
			foreach (object obj in this)
			{
				ResourceStyleMapping resourceStyleMapping = (ResourceStyleMapping)obj;
				if ((!string.IsNullOrEmpty(resourceStyleMapping.Type) || !string.IsNullOrEmpty(resourceStyleMapping.Key) || !string.IsNullOrEmpty(resourceStyleMapping.Text)) && (string.IsNullOrEmpty(resourceStyleMapping.Type) || !(resourceStyleMapping.Type != res.Type)) && (string.IsNullOrEmpty(resourceStyleMapping.Key) || !(resourceStyleMapping.Key != res.Key.ToString())) && (string.IsNullOrEmpty(resourceStyleMapping.Text) || !(resourceStyleMapping.Text != res.Text)))
				{
					list.Add(resourceStyleMapping);
				}
			}
			return list;
		}
	}
}
