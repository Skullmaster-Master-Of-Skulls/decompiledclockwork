using System;
using a.k;
using MailBee.Mime;

namespace MailBee.BounceMail
{
	// Token: 0x02000079 RID: 121
	public class DeliveryStatusParser
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0000A5AC File Offset: 0x000095AC
		public DeliveryStatusParser(string databaseLocations, bool allowFailedDatabases)
		{
			if (databaseLocations == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (databaseLocations == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			this.a = new c(databaseLocations.Split(new char[]
			{
				';'
			}), allowFailedDatabases);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000A5FC File Offset: 0x000095FC
		public DeliveryStatusParser(byte[] xmlDatabase)
		{
			if (xmlDatabase == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.a = new c(xmlDatabase);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000A61C File Offset: 0x0000961C
		public Result Process(MailMessage message)
		{
			if (message == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (this.a == null)
			{
				throw new MailBeeInvalidStateException(11);
			}
			Result result = new Result(message, this.a, false, -1);
			if (result.Recipients.Count == 0)
			{
				return null;
			}
			return result;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000A664 File Offset: 0x00009664
		public Result ProcessWithTimeout(MailMessage message, int timeout)
		{
			if (message == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (timeout < 0)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
			if (this.a == null)
			{
				throw new MailBeeInvalidStateException(11);
			}
			Result result = new Result(message, this.a, true, timeout);
			if (result.Recipients.Count == 0)
			{
				return null;
			}
			return result;
		}

		// Token: 0x040001B5 RID: 437
		private c a;
	}
}
