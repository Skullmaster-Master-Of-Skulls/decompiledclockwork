using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x0200016D RID: 365
	public class TemplateInfo
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001A9DA File Offset: 0x00018BDA
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x0001A9EB File Offset: 0x00018BEB
		public object FormattedModelValue
		{
			get
			{
				return this._formattedModelValue ?? string.Empty;
			}
			set
			{
				this._formattedModelValue = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001A9F4 File Offset: 0x00018BF4
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x0001AA05 File Offset: 0x00018C05
		public string HtmlFieldPrefix
		{
			get
			{
				return this._htmlFieldPrefix ?? string.Empty;
			}
			set
			{
				this._htmlFieldPrefix = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0001AA0E File Offset: 0x00018C0E
		public int TemplateDepth
		{
			get
			{
				return this.VisitedObjects.Count;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001AA1B File Offset: 0x00018C1B
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x0001AA36 File Offset: 0x00018C36
		internal HashSet<object> VisitedObjects
		{
			get
			{
				if (this._visitedObjects == null)
				{
					this._visitedObjects = new HashSet<object>();
				}
				return this._visitedObjects;
			}
			set
			{
				this._visitedObjects = value;
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001AA3F File Offset: 0x00018C3F
		public string GetFullHtmlFieldId(string partialFieldName)
		{
			return HtmlHelper.GenerateIdFromName(this.GetFullHtmlFieldName(partialFieldName));
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001AA50 File Offset: 0x00018C50
		public string GetFullHtmlFieldName(string partialFieldName)
		{
			if (partialFieldName != null && partialFieldName.StartsWith("[", StringComparison.Ordinal))
			{
				return this.HtmlFieldPrefix + partialFieldName;
			}
			return (this.HtmlFieldPrefix + "." + (partialFieldName ?? string.Empty)).Trim(new char[]
			{
				'.'
			});
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001AAA7 File Offset: 0x00018CA7
		public bool Visited(ModelMetadata metadata)
		{
			return this.VisitedObjects.Contains(metadata.Model ?? metadata.ModelType);
		}

		// Token: 0x04000291 RID: 657
		private string _htmlFieldPrefix;

		// Token: 0x04000292 RID: 658
		private object _formattedModelValue;

		// Token: 0x04000293 RID: 659
		private HashSet<object> _visitedObjects;
	}
}
