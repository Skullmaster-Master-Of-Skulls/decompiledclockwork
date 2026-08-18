using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal.Fakeables;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C2 RID: 194
	[ThreadAgnostic]
	[LayoutRenderer("appdomain")]
	public class AppDomainLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x0000CC96 File Offset: 0x0000AE96
		public AppDomainLayoutRenderer() : this(AppDomainWrapper.CurrentDomain)
		{
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000CCA3 File Offset: 0x0000AEA3
		public AppDomainLayoutRenderer(IAppDomain currentDomain)
		{
			this._currentDomain = currentDomain;
			this.Format = "Long";
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000CCBD File Offset: 0x0000AEBD
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000CCC5 File Offset: 0x0000AEC5
		[DefaultValue("Long")]
		[DefaultParameter]
		public string Format { get; set; }

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string formattingString = AppDomainLayoutRenderer.GetFormattingString(this.Format);
			builder.Append(string.Format(formattingString, this._currentDomain.Id, this._currentDomain.FriendlyName));
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000CD14 File Offset: 0x0000AF14
		private static string GetFormattingString(string format)
		{
			string result;
			if (format.Equals("Long", StringComparison.OrdinalIgnoreCase))
			{
				result = "{0:0000}:{1}";
			}
			else if (format.Equals("Short", StringComparison.OrdinalIgnoreCase))
			{
				result = "{0:00}";
			}
			else
			{
				result = format;
			}
			return result;
		}

		// Token: 0x04000151 RID: 337
		private const string ShortFormat = "{0:00}";

		// Token: 0x04000152 RID: 338
		private const string LongFormat = "{0:0000}:{1}";

		// Token: 0x04000153 RID: 339
		private const string LongFormatCode = "Long";

		// Token: 0x04000154 RID: 340
		private const string ShortFormatCode = "Short";

		// Token: 0x04000155 RID: 341
		private readonly IAppDomain _currentDomain;
	}
}
