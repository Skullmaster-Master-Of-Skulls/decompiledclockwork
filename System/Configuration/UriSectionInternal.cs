using System;
using System.Threading;

namespace System.Configuration
{
	// Token: 0x02000672 RID: 1650
	internal sealed class UriSectionInternal
	{
		// Token: 0x060032FB RID: 13051 RVA: 0x000D7D0A File Offset: 0x000D6D0A
		internal UriSectionInternal(UriSection section)
		{
			this.idn = section.Idn.Enabled;
			this.iriParsing = section.IriParsing.Enabled;
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000D7D34 File Offset: 0x000D6D34
		internal UriIdnScope Idn
		{
			get
			{
				return this.idn;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x000D7D3C File Offset: 0x000D6D3C
		internal bool IriParsing
		{
			get
			{
				return this.iriParsing;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x000D7D44 File Offset: 0x000D6D44
		internal static object ClassSyncObject
		{
			get
			{
				if (UriSectionInternal.classSyncObject == null)
				{
					Interlocked.CompareExchange(ref UriSectionInternal.classSyncObject, new object(), null);
				}
				return UriSectionInternal.classSyncObject;
			}
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000D7D64 File Offset: 0x000D6D64
		internal static UriSectionInternal GetSection()
		{
			UriSectionInternal result;
			lock (UriSectionInternal.ClassSyncObject)
			{
				UriSection uriSection = PrivilegedConfigurationManager.GetSection(CommonConfigurationStrings.UriSectionPath) as UriSection;
				if (uriSection == null)
				{
					result = null;
				}
				else
				{
					result = new UriSectionInternal(uriSection);
				}
			}
			return result;
		}

		// Token: 0x04002F83 RID: 12163
		private bool iriParsing;

		// Token: 0x04002F84 RID: 12164
		private UriIdnScope idn;

		// Token: 0x04002F85 RID: 12165
		private static object classSyncObject;
	}
}
