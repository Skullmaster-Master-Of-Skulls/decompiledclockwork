using System;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000EF RID: 239
	public class EncryptionMethod
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x0001A7EF File Offset: 0x000189EF
		public EncryptionMethod(Uri algorithm)
		{
			if (algorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("algorithm");
			}
			this._algorithm = algorithm;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001A817 File Offset: 0x00018A17
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0001A81F File Offset: 0x00018A1F
		public Uri Algorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._algorithm = value;
			}
		}

		// Token: 0x04000A61 RID: 2657
		private Uri _algorithm;
	}
}
