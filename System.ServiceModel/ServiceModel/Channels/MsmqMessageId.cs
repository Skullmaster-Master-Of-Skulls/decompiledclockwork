using System;
using System.Globalization;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000900 RID: 2304
	internal static class MsmqMessageId
	{
		// Token: 0x060057EC RID: 22508 RVA: 0x00143460 File Offset: 0x00141660
		public static string ToString(byte[] messageId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = new byte[16];
			Array.Copy(messageId, array, 16);
			int value = BitConverter.ToInt32(messageId, 16);
			stringBuilder.Append(new Guid(array).ToString());
			stringBuilder.Append("\\");
			stringBuilder.Append(value);
			return stringBuilder.ToString();
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x001434C4 File Offset: 0x001416C4
		public static byte[] FromString(string messageId)
		{
			string[] array = messageId.Split(new char[]
			{
				'\\'
			});
			if (array.Length != 2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MsmqInvalidMessageId", new object[]
				{
					messageId
				}), "messageId"));
			}
			Guid guid;
			if (!DiagnosticUtility.Utility.TryCreateGuid(array[0], out guid))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MsmqInvalidMessageId", new object[]
				{
					messageId
				}), "messageId"));
			}
			int value;
			try
			{
				value = Convert.ToInt32(array[1], CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MsmqInvalidMessageId", new object[]
				{
					messageId
				}), "messageId"));
			}
			byte[] array2 = new byte[20];
			Array.Copy(guid.ToByteArray(), array2, 16);
			Array.Copy(BitConverter.GetBytes(value), 0, array2, 16, 4);
			return array2;
		}

		// Token: 0x0400360A RID: 13834
		private const int guidSize = 16;
	}
}
