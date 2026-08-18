using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Diagnostics.Design
{
	// Token: 0x0200020F RID: 527
	internal class VerbConverter : TypeConverter
	{
		// Token: 0x06001384 RID: 4996 RVA: 0x00010631 File Offset: 0x0000E831
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0006FAB4 File Offset: 0x0006DCB4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return ((string)value).Trim();
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0006FAE0 File Offset: 0x0006DCE0
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ProcessStartInfo processStartInfo = (context == null) ? null : (context.Instance as ProcessStartInfo);
			TypeConverter.StandardValuesCollection result;
			if (processStartInfo != null)
			{
				result = new TypeConverter.StandardValuesCollection(processStartInfo.Verbs);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x04000A7E RID: 2686
		private const string DefaultVerb = "VerbEditorDefault";
	}
}
