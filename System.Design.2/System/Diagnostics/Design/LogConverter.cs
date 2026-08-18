using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Diagnostics.Design
{
	// Token: 0x02000211 RID: 529
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class LogConverter : TypeConverter
	{
		// Token: 0x0600138C RID: 5004 RVA: 0x00010631 File Offset: 0x0000E831
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0006FB30 File Offset: 0x0006DD30
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return ((string)value).Trim();
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0006FB5C File Offset: 0x0006DD5C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			EventLog eventLog = (context == null) ? null : (context.Instance as EventLog);
			string text = ".";
			if (eventLog != null)
			{
				text = eventLog.MachineName;
			}
			if (this.values == null || text != this.oldMachineName)
			{
				try
				{
					EventLog[] eventLogs = EventLog.GetEventLogs(text);
					object[] array = new object[eventLogs.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = eventLogs[i].Log;
					}
					this.values = new TypeConverter.StandardValuesCollection(array);
					this.oldMachineName = text;
				}
				catch (Exception)
				{
				}
			}
			return this.values;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04000A7F RID: 2687
		private TypeConverter.StandardValuesCollection values;

		// Token: 0x04000A80 RID: 2688
		private string oldMachineName;
	}
}
