using System;
using System.Net;
using System.Net.Mail;

namespace NLog.Internal
{
	// Token: 0x02000098 RID: 152
	internal class MySmtpClient : SmtpClient, ISmtpClient, IDisposable
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x0000A9D3 File Offset: 0x00008BD3
		SmtpDeliveryMethod ISmtpClient.get_DeliveryMethod()
		{
			return base.DeliveryMethod;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000A9DB File Offset: 0x00008BDB
		void ISmtpClient.set_DeliveryMethod(SmtpDeliveryMethod A_1)
		{
			base.DeliveryMethod = A_1;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		string ISmtpClient.get_Host()
		{
			return base.Host;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000A9EC File Offset: 0x00008BEC
		void ISmtpClient.set_Host(string A_1)
		{
			base.Host = A_1;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000A9F5 File Offset: 0x00008BF5
		int ISmtpClient.get_Port()
		{
			return base.Port;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000A9FD File Offset: 0x00008BFD
		void ISmtpClient.set_Port(int A_1)
		{
			base.Port = A_1;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000AA06 File Offset: 0x00008C06
		int ISmtpClient.get_Timeout()
		{
			return base.Timeout;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000AA0E File Offset: 0x00008C0E
		void ISmtpClient.set_Timeout(int A_1)
		{
			base.Timeout = A_1;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000AA17 File Offset: 0x00008C17
		ICredentialsByHost ISmtpClient.get_Credentials()
		{
			return base.Credentials;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000AA1F File Offset: 0x00008C1F
		void ISmtpClient.set_Credentials(ICredentialsByHost A_1)
		{
			base.Credentials = A_1;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000AA28 File Offset: 0x00008C28
		bool ISmtpClient.get_EnableSsl()
		{
			return base.EnableSsl;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000AA30 File Offset: 0x00008C30
		void ISmtpClient.set_EnableSsl(bool A_1)
		{
			base.EnableSsl = A_1;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000AA39 File Offset: 0x00008C39
		void ISmtpClient.Send(MailMessage A_1)
		{
			base.Send(A_1);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000AA42 File Offset: 0x00008C42
		string ISmtpClient.get_PickupDirectoryLocation()
		{
			return base.PickupDirectoryLocation;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000AA4A File Offset: 0x00008C4A
		void ISmtpClient.set_PickupDirectoryLocation(string A_1)
		{
			base.PickupDirectoryLocation = A_1;
		}
	}
}
