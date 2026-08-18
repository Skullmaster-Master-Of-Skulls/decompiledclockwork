using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200013D RID: 317
	internal class EncryptedPassword : IDisposable
	{
		// Token: 0x06000CA9 RID: 3241 RVA: 0x00084250 File Offset: 0x00083250
		public EncryptedPassword(string password)
		{
			OpsCon.Encrypt(out this.m_encryptedPwd, out this.m_encryptedPwdLen, password, password.Length);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00084271 File Offset: 0x00083271
		public EncryptedPassword(IntPtr encryptedPwd, int encryptedPwdLen)
		{
			this.m_encryptedPwd = encryptedPwd;
			this.m_encryptedPwdLen = encryptedPwdLen;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00084288 File Offset: 0x00083288
		public string Password
		{
			get
			{
				int length = 0;
				OpsCon.Decrypt(out this.m_decryptPwdBuffer, out length, this.m_encryptedPwd, this.m_encryptedPwdLen);
				string result = Marshal.PtrToStringAuto(this.m_decryptPwdBuffer);
				OpsCon.ClearDecryptBuff(ref this.m_decryptPwdBuffer, length);
				return result;
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000842CD File Offset: 0x000832CD
		public void Dispose()
		{
			Marshal.FreeCoTaskMem(this.m_encryptedPwd);
			this.m_encryptedPwd = IntPtr.Zero;
			this.m_encryptedPwdLen = 0;
			Marshal.FreeCoTaskMem(this.m_decryptPwdBuffer);
			this.m_decryptPwdBuffer = IntPtr.Zero;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00084308 File Offset: 0x00083308
		~EncryptedPassword()
		{
			this.Dispose();
		}

		// Token: 0x040009FA RID: 2554
		public IntPtr m_encryptedPwd;

		// Token: 0x040009FB RID: 2555
		public int m_encryptedPwdLen;

		// Token: 0x040009FC RID: 2556
		public IntPtr m_decryptPwdBuffer;
	}
}
