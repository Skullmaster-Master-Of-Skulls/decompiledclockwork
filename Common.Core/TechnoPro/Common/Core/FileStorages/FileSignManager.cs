using System;
using ClockWorkLogger;
using TechnoPro.Common.DAO.FileSign.Impl;
using TechnoPro.Common.ICore.FileStorages;

namespace TechnoPro.Common.Core.FileStorages
{
	// Token: 0x020000F0 RID: 240
	public class FileSignManager : IFileSignManager
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x0003BA0F File Offset: 0x00039C0F
		public FileSignManager()
		{
			this.dao = new FileSignDAO();
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0003BA24 File Offset: 0x00039C24
		public byte[] DecryptAndVerify(byte[] EncryptedFile)
		{
			return this.dao.DecryptAndVerify(EncryptedFile);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0003BA44 File Offset: 0x00039C44
		public byte[] EncryptAndSign(byte[] DecryptedFile, string TechnoProPrivateKey, string TechnoProPassword, string ClockWorkPublicKey)
		{
			return this.dao.EncryptAndSign(DecryptedFile, TechnoProPrivateKey, TechnoProPassword, ClockWorkPublicKey);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0003BA66 File Offset: 0x00039C66
		public void DecryptAndVerifyUsingFileSystem(string EncryptedFileName, string OutputDecryptedFileName)
		{
			this.dao.DecryptAndVerifyUsingFileSystem(EncryptedFileName, OutputDecryptedFileName);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0003BA77 File Offset: 0x00039C77
		public void EncryptAndVerifyUsingFileSystem(string ClockWorkPublicKey, string TechnoProPrivateKey, string TechnoProPassword, string DecryptedFileName, string OutputEncryptedFileName)
		{
			this.dao.EncryptAndVerifyUsingFileSystem(ClockWorkPublicKey, TechnoProPrivateKey, TechnoProPassword, DecryptedFileName, OutputEncryptedFileName);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0003BA90 File Offset: 0x00039C90
		public bool VerifySign(byte[] encryptedFile)
		{
			bool result;
			try
			{
				result = (this.dao.DecryptAndVerify(encryptedFile) != null);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("FileSignManager::VerifySign:: {0}", ex.ToString()), ex);
				result = false;
			}
			return result;
		}

		// Token: 0x040001A2 RID: 418
		private IFileSignDAO dao;
	}
}
