using System;
using System.ComponentModel;
using Spire.DataExport.TypeConverters;

namespace Spire.DataExport.RTF
{
	// Token: 0x02000170 RID: 368
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class RtfItemStyle : RTFStyle
	{
	}
}
