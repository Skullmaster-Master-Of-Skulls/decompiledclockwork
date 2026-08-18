using System;
using System.Configuration;
using System.Security.Cryptography;

namespace System.Web.Security.Cryptography
{
	// Token: 0x02000605 RID: 1541
	internal sealed class HomogenizingCryptoServiceWrapper : ICryptoService
	{
		// Token: 0x06004DA8 RID: 19880 RVA: 0x0010D93B File Offset: 0x0010BB3B
		public HomogenizingCryptoServiceWrapper(ICryptoService wrapped)
		{
			this.WrappedCryptoService = wrapped;
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x0010D94A File Offset: 0x0010BB4A
		// (set) Token: 0x06004DAA RID: 19882 RVA: 0x0010D952 File Offset: 0x0010BB52
		internal ICryptoService WrappedCryptoService { get; private set; }

		// Token: 0x06004DAB RID: 19883 RVA: 0x0010D95C File Offset: 0x0010BB5C
		private static byte[] HomogenizeErrors(Func<byte[], byte[]> func, byte[] input)
		{
			byte[] array = null;
			bool flag = false;
			byte[] result;
			try
			{
				array = func(input);
				result = array;
			}
			catch (ConfigurationException)
			{
				flag = true;
				throw;
			}
			finally
			{
				if (array == null && !flag)
				{
					throw new CryptographicException();
				}
			}
			return result;
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x0010D9A8 File Offset: 0x0010BBA8
		public byte[] Protect(byte[] clearData)
		{
			return HomogenizingCryptoServiceWrapper.HomogenizeErrors(new Func<byte[], byte[]>(this.WrappedCryptoService.Protect), clearData);
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x0010D9C2 File Offset: 0x0010BBC2
		public byte[] Unprotect(byte[] protectedData)
		{
			return HomogenizingCryptoServiceWrapper.HomogenizeErrors(new Func<byte[], byte[]>(this.WrappedCryptoService.Unprotect), protectedData);
		}
	}
}
