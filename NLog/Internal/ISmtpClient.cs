using System;
using System.Net;
using System.Net.Mail;

namespace NLog.Internal
{
	// Token: 0x02000093 RID: 147
	internal interface ISmtpClient : IDisposable
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004B3 RID: 1203
		// (set) Token: 0x060004B4 RID: 1204
		SmtpDeliveryMethod DeliveryMethod { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004B5 RID: 1205
		// (set) Token: 0x060004B6 RID: 1206
		string Host { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004B7 RID: 1207
		// (set) Token: 0x060004B8 RID: 1208
		int Port { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004B9 RID: 1209
		// (set) Token: 0x060004BA RID: 1210
		int Timeout { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060004BB RID: 1211
		// (set) Token: 0x060004BC RID: 1212
		ICredentialsByHost Credentials { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004BD RID: 1213
		// (set) Token: 0x060004BE RID: 1214
		bool EnableSsl { get; set; }

		// Token: 0x060004BF RID: 1215
		void Send(MailMessage msg);

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004C0 RID: 1216
		// (set) Token: 0x060004C1 RID: 1217
		string PickupDirectoryLocation { get; set; }
	}
}
