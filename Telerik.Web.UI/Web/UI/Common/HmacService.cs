using System;
using System.Configuration;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace Telerik.Web.UI.Common
{
	// Token: 0x02000093 RID: 147
	internal class HmacService : IHmacService
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0000DC78 File Offset: 0x0000BE78
		private string GetHmacKey()
		{
			string text = ConfigurationManager.AppSettings.Get(HmacService.customHashKey);
			if (text != null)
			{
				return text;
			}
			return this.TryGetMachineKey();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000DCA0 File Offset: 0x0000BEA0
		private string TryGetMachineKey()
		{
			string validationKey;
			try
			{
				MachineKeySection machineKeySection = (MachineKeySection)ConfigurationManager.GetSection("system.web/machineKey");
				validationKey = machineKeySection.ValidationKey;
			}
			catch (SecurityException)
			{
				string message = "MachineKey section could not be read, most likely due to lack of permissions because the application's Trust level is not Full.\r\n\t\t\t\t\t\t\t\t\t\tTo avoid this error, set the three Telerik-specific encryption keys in your appSettings section: \r\n\t\t\t\t\t\t\t\t\t\tTelerik.AsyncUpload.ConfigurationEncryptionKey,\r\n\t\t\t\t\t\t\t\t\t\tTelerik.Upload.ConfigurationHashKey, and\r\n\t\t\t\t\t\t\t\t\t\tTelerik.Web.UI.DialogParametersEncryptionKey.\r\n\t\t\t\t\t\t\t\t\t\tYou can read more at http://docs.telerik.com/devtools/aspnet-ajax/general-information/web-config-settings-overview#mandatory-additions-to-the-webconfig";
				throw new Exception(message);
			}
			return validationKey;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		public int GetHmacLength()
		{
			return 44;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000DCEC File Offset: 0x0000BEEC
		public static IHmacService GetService()
		{
			if (HmacService.service == null)
			{
				lock (HmacService.serviceLock)
				{
					if (HmacService.service == null)
					{
						HmacService.service = new HmacService();
					}
				}
			}
			return HmacService.service;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000DD60 File Offset: 0x0000BF60
		public string HMAC256(string input)
		{
			return HmacService.exceptionThrower.ThrowIfFails<string>(() => this.ComputeHash(input));
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000DD98 File Offset: 0x0000BF98
		private string ComputeHash(string input)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(this.GetHmacKey());
			string result;
			using (HMACSHA256 hmacsha = new HMACSHA256(bytes))
			{
				byte[] bytes2 = Encoding.UTF8.GetBytes(input);
				byte[] inArray = hmacsha.ComputeHash(bytes2);
				result = Convert.ToBase64String(inArray);
			}
			return result;
		}

		// Token: 0x040000C1 RID: 193
		private const int HMAC_LENGTH = 44;

		// Token: 0x040000C2 RID: 194
		private static readonly string customHashKey = "Telerik.Upload.ConfigurationHashKey";

		// Token: 0x040000C3 RID: 195
		private static readonly object serviceLock = new object();

		// Token: 0x040000C4 RID: 196
		private static IHmacService service;

		// Token: 0x040000C5 RID: 197
		private static readonly ICryptoExceptionThrower exceptionThrower = new CryptoExceptionThrower();
	}
}
