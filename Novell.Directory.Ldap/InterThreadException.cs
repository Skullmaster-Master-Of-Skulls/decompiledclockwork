using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000018 RID: 24
	public class InterThreadException : LdapException
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005830 File Offset: 0x00004830
		internal virtual int MessageID
		{
			get
			{
				int result;
				if (this.request == null)
				{
					result = -1;
				}
				else
				{
					result = this.request.MessageID;
				}
				return result;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005858 File Offset: 0x00004858
		internal virtual int ReplyType
		{
			get
			{
				int result;
				if (this.request == null)
				{
					result = -1;
				}
				else
				{
					int messageType = this.request.MessageType;
					int num = -1;
					int num2 = messageType;
					switch (num2)
					{
					case 0:
						num = 1;
						break;
					case 1:
					case 4:
					case 5:
					case 7:
					case 9:
					case 11:
					case 13:
					case 15:
						break;
					case 2:
						num = -1;
						break;
					case 3:
						num = 5;
						break;
					case 6:
						num = 7;
						break;
					case 8:
						num = 9;
						break;
					case 10:
						num = 11;
						break;
					case 12:
						num = 13;
						break;
					case 14:
						num = 15;
						break;
					case 16:
						num = -1;
						break;
					default:
						if (num2 == 23)
						{
							num = 24;
						}
						break;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005908 File Offset: 0x00004908
		internal InterThreadException(string message, object[] arguments, int resultCode, Exception rootException, Message request) : base(message, arguments, resultCode, null, rootException)
		{
			this.request = request;
		}

		// Token: 0x0400008B RID: 139
		private Message request;
	}
}
