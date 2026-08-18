using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D08 RID: 3336
	[SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Design choice.")]
	[SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly", Justification = "Design choice.")]
	[SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Design choice.")]
	public class OlapCommunicationException : Exception
	{
		// Token: 0x06007C55 RID: 31829 RVA: 0x001C9624 File Offset: 0x001C7824
		public OlapCommunicationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007C56 RID: 31830 RVA: 0x001C962E File Offset: 0x001C782E
		public OlapCommunicationException(string message) : base(message)
		{
		}

		// Token: 0x06007C57 RID: 31831 RVA: 0x001C9637 File Offset: 0x001C7837
		public OlapCommunicationException()
		{
		}
	}
}
