using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000744 RID: 1860
	public class XmlaQueryProperty : IXmlaMethodProperty
	{
		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x000CF42A File Offset: 0x000CD62A
		// (set) Token: 0x06004205 RID: 16901 RVA: 0x000CF432 File Offset: 0x000CD632
		public string Name { get; set; }

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x000CF43B File Offset: 0x000CD63B
		// (set) Token: 0x06004207 RID: 16903 RVA: 0x000CF443 File Offset: 0x000CD643
		public string Value { get; set; }

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06004208 RID: 16904 RVA: 0x000CF44C File Offset: 0x000CD64C
		string IXmlaMethodProperty.Name
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06004209 RID: 16905 RVA: 0x000CF454 File Offset: 0x000CD654
		object IXmlaMethodProperty.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x000CF45C File Offset: 0x000CD65C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} => {1}", new object[]
			{
				this.Name,
				this.Value
			});
		}
	}
}
