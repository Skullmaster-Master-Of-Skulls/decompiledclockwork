using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.NodeBuilders
{
	// Token: 0x02000D05 RID: 3333
	internal static class NodeBuilderHelper
	{
		// Token: 0x06007C4A RID: 31818 RVA: 0x001C9504 File Offset: 0x001C7704
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)", Justification = "Will fix soon.")]
		public static void SubmitTraceInformation(SchemaValidationResult validationResult, string elementType)
		{
			string errorsText = validationResult.GetErrorsText();
			PivotTrace.WriteTraceForDataProvider(string.Format("OLAP error: '{0}' for {1} item", errorsText, elementType));
		}
	}
}
