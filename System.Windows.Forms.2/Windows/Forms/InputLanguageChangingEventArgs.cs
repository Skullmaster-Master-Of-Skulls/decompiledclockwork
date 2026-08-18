using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x0200029D RID: 669
	public class InputLanguageChangingEventArgs : CancelEventArgs
	{
		// Token: 0x06002A0E RID: 10766 RVA: 0x000BF530 File Offset: 0x000BD730
		public InputLanguageChangingEventArgs(CultureInfo culture, bool sysCharSet)
		{
			this.inputLanguage = InputLanguage.FromCulture(culture);
			this.culture = culture;
			this.sysCharSet = sysCharSet;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000BF552 File Offset: 0x000BD752
		public InputLanguageChangingEventArgs(InputLanguage inputLanguage, bool sysCharSet)
		{
			if (inputLanguage == null)
			{
				throw new ArgumentNullException("inputLanguage");
			}
			this.inputLanguage = inputLanguage;
			this.culture = inputLanguage.Culture;
			this.sysCharSet = sysCharSet;
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x000BF582 File Offset: 0x000BD782
		public InputLanguage InputLanguage
		{
			get
			{
				return this.inputLanguage;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x000BF58A File Offset: 0x000BD78A
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x000BF592 File Offset: 0x000BD792
		public bool SysCharSet
		{
			get
			{
				return this.sysCharSet;
			}
		}

		// Token: 0x04001120 RID: 4384
		private readonly InputLanguage inputLanguage;

		// Token: 0x04001121 RID: 4385
		private readonly CultureInfo culture;

		// Token: 0x04001122 RID: 4386
		private readonly bool sysCharSet;
	}
}
