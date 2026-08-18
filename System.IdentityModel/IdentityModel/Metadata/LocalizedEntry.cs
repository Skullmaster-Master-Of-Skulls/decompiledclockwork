using System;
using System.Globalization;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000F9 RID: 249
	public abstract class LocalizedEntry
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x0001AB41 File Offset: 0x00018D41
		protected LocalizedEntry() : this(null)
		{
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001AB4A File Offset: 0x00018D4A
		protected LocalizedEntry(CultureInfo language)
		{
			this.language = language;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001AB59 File Offset: 0x00018D59
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001AB61 File Offset: 0x00018D61
		public CultureInfo Language
		{
			get
			{
				return this.language;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.language = value;
			}
		}

		// Token: 0x04000A7A RID: 2682
		private CultureInfo language;
	}
}
