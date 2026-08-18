using System;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x020000A3 RID: 163
	internal sealed class TypeNamePatternConverter : NamedPatternConverter
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x0000F59F File Offset: 0x0000D79F
		protected override string GetFullyQualifiedName(LoggingEvent loggingEvent)
		{
			return loggingEvent.LocationInformation.ClassName;
		}
	}
}
