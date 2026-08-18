using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C5A RID: 3162
	[DataContract]
	public abstract class TotalFormat : SettingsNode
	{
		// Token: 0x06007766 RID: 30566 RVA: 0x001BAB67 File Offset: 0x001B8D67
		internal TotalFormat()
		{
		}

		// Token: 0x06007767 RID: 30567 RVA: 0x001BAB6F File Offset: 0x001B8D6F
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Design choice.")]
		public virtual string GetStringFormat(Type dataType, string stringFormat)
		{
			return stringFormat;
		}
	}
}
