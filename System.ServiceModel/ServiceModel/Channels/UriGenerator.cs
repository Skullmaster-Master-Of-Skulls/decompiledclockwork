using System;
using System.Globalization;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008C5 RID: 2245
	internal class UriGenerator
	{
		// Token: 0x060055B7 RID: 21943 RVA: 0x00139984 File Offset: 0x00137B84
		public UriGenerator() : this("uuid")
		{
		}

		// Token: 0x060055B8 RID: 21944 RVA: 0x00139991 File Offset: 0x00137B91
		public UriGenerator(string scheme) : this(scheme, ";")
		{
		}

		// Token: 0x060055B9 RID: 21945 RVA: 0x001399A0 File Offset: 0x00137BA0
		public UriGenerator(string scheme, string delimiter)
		{
			if (scheme == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("scheme"));
			}
			if (scheme.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("UriGeneratorSchemeMustNotBeEmpty"), "scheme"));
			}
			this.prefix = string.Concat(new string[]
			{
				scheme,
				":",
				Guid.NewGuid().ToString(),
				delimiter,
				"id="
			});
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x00139A34 File Offset: 0x00137C34
		public string Next()
		{
			long num = Interlocked.Increment(ref this.id);
			return this.prefix + num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x040034FD RID: 13565
		private long id;

		// Token: 0x040034FE RID: 13566
		private string prefix;
	}
}
