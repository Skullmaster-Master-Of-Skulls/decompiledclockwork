using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C0 RID: 192
	[ThreadAgnostic]
	[LayoutRenderer("all-event-properties")]
	public class AllEventPropertiesLayoutRenderer : LayoutRenderer
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x0000CA72 File Offset: 0x0000AC72
		public AllEventPropertiesLayoutRenderer()
		{
			this.Separator = ", ";
			this.Format = "[key]=[value]";
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000CA90 File Offset: 0x0000AC90
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0000CA98 File Offset: 0x0000AC98
		public string Separator { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000CAA1 File Offset: 0x0000ACA1
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0000CAA9 File Offset: 0x0000ACA9
		[DefaultValue(false)]
		public bool IncludeCallerInformation { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000CAB2 File Offset: 0x0000ACB2
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x0000CABA File Offset: 0x0000ACBA
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (!value.Contains("[key]"))
				{
					throw new ArgumentException("Invalid format: [key] placeholder is missing.");
				}
				if (!value.Contains("[value]"))
				{
					throw new ArgumentException("Invalid format: [value] placeholder is missing.");
				}
				this.format = value;
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000CAF4 File Offset: 0x0000ACF4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			bool flag = true;
			foreach (KeyValuePair<object, object> keyValuePair in this.GetProperties(logEvent))
			{
				if (!flag)
				{
					builder.Append(this.Separator);
				}
				flag = false;
				IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
				string newValue = Convert.ToString(keyValuePair.Key, formatProvider);
				string newValue2 = Convert.ToString(keyValuePair.Value, formatProvider);
				string value = this.Format.Replace("[key]", newValue).Replace("[value]", newValue2);
				builder.Append(value);
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		private IDictionary<object, object> GetProperties(LogEventInfo logEvent)
		{
			if (this.IncludeCallerInformation)
			{
				return logEvent.Properties;
			}
			return (from p in logEvent.Properties
			where !AllEventPropertiesLayoutRenderer.CallerInformationAttributeNames.Contains(p.Key)
			select p).ToDictionary((KeyValuePair<object, object> p) => p.Key, (KeyValuePair<object, object> p) => p.Value);
		}

		// Token: 0x0400014A RID: 330
		private string format;

		// Token: 0x0400014B RID: 331
		private static HashSet<string> CallerInformationAttributeNames = new HashSet<string>
		{
			"CallerMemberName",
			"CallerFilePath",
			"CallerLineNumber"
		};
	}
}
