using System;
using System.Security.Cryptography;

namespace System.Web.Security.Cryptography
{
	// Token: 0x02000600 RID: 1536
	internal sealed class DataProtectorCryptoService : ICryptoService
	{
		// Token: 0x06004D9C RID: 19868 RVA: 0x0010D787 File Offset: 0x0010B987
		public DataProtectorCryptoService(IDataProtectorFactory dataProtectorFactory, Purpose purpose)
		{
			this._dataProtectorFactory = dataProtectorFactory;
			this._purpose = purpose;
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x0010D7A0 File Offset: 0x0010B9A0
		private byte[] PerformOperation(byte[] data, bool protect)
		{
			byte[] result;
			using (new ApplicationImpersonationContext())
			{
				DataProtector dataProtector = null;
				try
				{
					dataProtector = this._dataProtectorFactory.GetDataProtector(this._purpose);
					result = (protect ? dataProtector.Protect(data) : dataProtector.Unprotect(data));
				}
				finally
				{
					IDisposable disposable = dataProtector as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x0010D818 File Offset: 0x0010BA18
		public byte[] Protect(byte[] clearData)
		{
			return this.PerformOperation(clearData, true);
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x0010D822 File Offset: 0x0010BA22
		public byte[] Unprotect(byte[] protectedData)
		{
			return this.PerformOperation(protectedData, false);
		}

		// Token: 0x04002962 RID: 10594
		private readonly IDataProtectorFactory _dataProtectorFactory;

		// Token: 0x04002963 RID: 10595
		private readonly Purpose _purpose;
	}
}
