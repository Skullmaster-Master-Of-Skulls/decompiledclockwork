using System;
using System.IO;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000E1 RID: 225
	internal sealed class PropertyPatternConverter : PatternConverter
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x00014C9C File Offset: 0x00012E9C
		protected override void Convert(TextWriter writer, object state)
		{
			CompositeProperties compositeProperties = new CompositeProperties();
			PropertiesDictionary properties = LogicalThreadContext.Properties.GetProperties(false);
			if (properties != null)
			{
				compositeProperties.Add(properties);
			}
			PropertiesDictionary properties2 = ThreadContext.Properties.GetProperties(false);
			if (properties2 != null)
			{
				compositeProperties.Add(properties2);
			}
			compositeProperties.Add(GlobalContext.Properties.GetReadOnlyProperties());
			if (this.Option != null)
			{
				PatternConverter.WriteObject(writer, null, compositeProperties[this.Option]);
				return;
			}
			PatternConverter.WriteDictionary(writer, null, compositeProperties.Flatten());
		}
	}
}
