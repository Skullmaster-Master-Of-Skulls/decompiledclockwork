using System;
using log4net.Util.TypeConverters;

namespace log4net.Layout
{
	// Token: 0x020000AE RID: 174
	public class RawLayoutConverter : IConvertFrom
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x0000FF17 File Offset: 0x0000E117
		public bool CanConvertFrom(Type sourceType)
		{
			return typeof(ILayout).IsAssignableFrom(sourceType);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000FF2C File Offset: 0x0000E12C
		public object ConvertFrom(object source)
		{
			ILayout layout = source as ILayout;
			if (layout != null)
			{
				return new Layout2RawLayoutAdapter(layout);
			}
			throw ConversionNotSupportedException.Create(typeof(IRawLayout), source);
		}
	}
}
