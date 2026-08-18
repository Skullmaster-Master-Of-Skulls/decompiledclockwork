using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008CB RID: 2251
	internal struct AsnReaderOptions
	{
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06005227 RID: 21031 RVA: 0x00126FEB File Offset: 0x00125FEB
		// (set) Token: 0x06005228 RID: 21032 RVA: 0x00127001 File Offset: 0x00126001
		public int UtcTimeTwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == 0)
				{
					return 2049;
				}
				return (int)this._twoDigitYearMax;
			}
			set
			{
				if (value < 1 || value > 9999)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._twoDigitYearMax = (ushort)value;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06005229 RID: 21033 RVA: 0x00127022 File Offset: 0x00126022
		// (set) Token: 0x0600522A RID: 21034 RVA: 0x0012702A File Offset: 0x0012602A
		public bool SkipSetSortOrderVerification
		{
			get
			{
				return this._skipSetSortOrderVerification;
			}
			set
			{
				this._skipSetSortOrderVerification = value;
			}
		}

		// Token: 0x04002A52 RID: 10834
		private const int DefaultTwoDigitMax = 2049;

		// Token: 0x04002A53 RID: 10835
		private ushort _twoDigitYearMax;

		// Token: 0x04002A54 RID: 10836
		private bool _skipSetSortOrderVerification;
	}
}
