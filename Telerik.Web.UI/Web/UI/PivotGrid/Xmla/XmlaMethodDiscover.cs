using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D8F RID: 3471
	internal class XmlaMethodDiscover : XmlaMethodBase
	{
		// Token: 0x06008113 RID: 33043 RVA: 0x001D7C03 File Offset: 0x001D5E03
		public XmlaMethodDiscover(string requestType)
		{
			this.RequestType = requestType;
			this.restrictions = new List<XmlaRestrictionProperty>();
		}

		// Token: 0x170028F3 RID: 10483
		// (get) Token: 0x06008114 RID: 33044 RVA: 0x001D7C1D File Offset: 0x001D5E1D
		// (set) Token: 0x06008115 RID: 33045 RVA: 0x001D7C25 File Offset: 0x001D5E25
		public string RequestType { get; private set; }

		// Token: 0x170028F4 RID: 10484
		// (get) Token: 0x06008116 RID: 33046 RVA: 0x001D7C2E File Offset: 0x001D5E2E
		public IEnumerable<XmlaRestrictionProperty> Restrictions
		{
			get
			{
				return this.restrictions;
			}
		}

		// Token: 0x06008117 RID: 33047 RVA: 0x001D7C36 File Offset: 0x001D5E36
		public void AddRestiction(XmlaRestrictionProperty property)
		{
			this.restrictions.Add(property);
		}

		// Token: 0x06008118 RID: 33048 RVA: 0x001D7C44 File Offset: 0x001D5E44
		public void RemoveRestriction(XmlaRestrictionProperty property)
		{
			this.restrictions.Remove(property);
		}

		// Token: 0x040023A2 RID: 9122
		private IList<XmlaRestrictionProperty> restrictions;
	}
}
