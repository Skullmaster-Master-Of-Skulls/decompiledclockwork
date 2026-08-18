using System;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x0200029B RID: 667
	public class InputLanguageChangedEventArgs : EventArgs
	{
		// Token: 0x06002A05 RID: 10757 RVA: 0x000BF4D4 File Offset: 0x000BD6D4
		public InputLanguageChangedEventArgs(CultureInfo culture, byte charSet)
		{
			this.inputLanguage = InputLanguage.FromCulture(culture);
			this.culture = culture;
			this.charSet = charSet;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000BF4F6 File Offset: 0x000BD6F6
		public InputLanguageChangedEventArgs(InputLanguage inputLanguage, byte charSet)
		{
			this.inputLanguage = inputLanguage;
			this.culture = inputLanguage.Culture;
			this.charSet = charSet;
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002A07 RID: 10759 RVA: 0x000BF518 File Offset: 0x000BD718
		public InputLanguage InputLanguage
		{
			get
			{
				return this.inputLanguage;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x000BF520 File Offset: 0x000BD720
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x000BF528 File Offset: 0x000BD728
		public byte CharSet
		{
			get
			{
				return this.charSet;
			}
		}

		// Token: 0x0400111D RID: 4381
		private readonly InputLanguage inputLanguage;

		// Token: 0x0400111E RID: 4382
		private readonly CultureInfo culture;

		// Token: 0x0400111F RID: 4383
		private readonly byte charSet;
	}
}
