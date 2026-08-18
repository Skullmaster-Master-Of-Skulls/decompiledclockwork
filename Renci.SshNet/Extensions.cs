using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet
{
	// Token: 0x0200000A RID: 10
	internal static class Extensions
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002B23 File Offset: 0x00000D23
		public static bool IsNullOrWhiteSpace(this string value)
		{
			return string.IsNullOrEmpty(value) || value.All(new Func<char, bool>(char.IsWhiteSpace));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B41 File Offset: 0x00000D41
		internal static byte[] ToArray(this GlobalRequestName globalRequestName)
		{
			if (globalRequestName == GlobalRequestName.TcpIpForward)
			{
				return SshData.Ascii.GetBytes("tcpip-forward");
			}
			if (globalRequestName != GlobalRequestName.CancelTcpIpForward)
			{
				throw new NotSupportedException(string.Format("Global request name '{0}' is not supported.", globalRequestName));
			}
			return SshData.Ascii.GetBytes("cancel-tcpip-forward");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B81 File Offset: 0x00000D81
		internal static byte[] ToArray(this ServiceName serviceName)
		{
			if (serviceName == ServiceName.UserAuthentication)
			{
				return SshData.Ascii.GetBytes("ssh-userauth");
			}
			if (serviceName != ServiceName.Connection)
			{
				throw new NotSupportedException(string.Format("Service name '{0}' is not supported.", serviceName));
			}
			return SshData.Ascii.GetBytes("ssh-connection");
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002BC4 File Offset: 0x00000DC4
		internal static ServiceName ToServiceName(this byte[] data)
		{
			string @string = SshData.Ascii.GetString(data, 0, data.Length);
			if (@string == "ssh-userauth")
			{
				return ServiceName.UserAuthentication;
			}
			if (!(@string == "ssh-connection"))
			{
				throw new NotSupportedException(string.Format("Service name '{0}' is not supported.", @string));
			}
			return ServiceName.Connection;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C14 File Offset: 0x00000E14
		internal static GlobalRequestName ToGlobalRequestName(this byte[] data)
		{
			string @string = SshData.Ascii.GetString(data, 0, data.Length);
			if (@string == "tcpip-forward")
			{
				return GlobalRequestName.TcpIpForward;
			}
			if (!(@string == "cancel-tcpip-forward"))
			{
				throw new NotSupportedException(string.Format("Global request name '{0}' is not supported.", @string));
			}
			return GlobalRequestName.CancelTcpIpForward;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C64 File Offset: 0x00000E64
		internal static BigInteger ToBigInteger(this byte[] data)
		{
			byte[] array = new byte[data.Length];
			Buffer.BlockCopy(data, 0, array, 0, data.Length);
			return new BigInteger(array.Reverse<byte>());
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C91 File Offset: 0x00000E91
		internal static T[] Reverse<T>(this T[] array)
		{
			Array.Reverse(array);
			return array;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C9C File Offset: 0x00000E9C
		internal static void DebugPrint(this IEnumerable<byte> bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "0x{0:x2}, ", new object[]
				{
					b
				});
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D04 File Offset: 0x00000F04
		internal static T CreateInstance<T>(this Type type) where T : class
		{
			if (type == null)
			{
				return default(T);
			}
			return Activator.CreateInstance(type) as T;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D34 File Offset: 0x00000F34
		internal static byte[] GetBytes(this ushort value)
		{
			return new byte[]
			{
				(byte)(value >> 8),
				(byte)(value & 255)
			};
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D50 File Offset: 0x00000F50
		internal static byte[] GetBytes(this uint value)
		{
			byte[] array = new byte[4];
			value.Write(array, 0);
			return array;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D6D File Offset: 0x00000F6D
		internal static void Write(this uint value, byte[] buffer, int offset)
		{
			buffer[offset++] = (byte)(value >> 24);
			buffer[offset++] = (byte)(value >> 16);
			buffer[offset++] = (byte)(value >> 8);
			buffer[offset] = (byte)(value & 255U);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DA0 File Offset: 0x00000FA0
		internal static byte[] GetBytes(this ulong value)
		{
			return new byte[]
			{
				(byte)(value >> 56),
				(byte)(value >> 48),
				(byte)(value >> 40),
				(byte)(value >> 32),
				(byte)(value >> 24),
				(byte)(value >> 16),
				(byte)(value >> 8),
				(byte)(value & 255UL)
			};
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002DF8 File Offset: 0x00000FF8
		internal static byte[] GetBytes(this long value)
		{
			return new byte[]
			{
				(byte)(value >> 56),
				(byte)(value >> 48),
				(byte)(value >> 40),
				(byte)(value >> 32),
				(byte)(value >> 24),
				(byte)(value >> 16),
				(byte)(value >> 8),
				(byte)(value & 255L)
			};
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E4E File Offset: 0x0000104E
		internal static void ValidatePort(this uint value, string argument)
		{
			if (value > 65535U)
			{
				throw new ArgumentOutOfRangeException(argument, string.Format(CultureInfo.InvariantCulture, "Specified value cannot be greater than {0}.", new object[]
				{
					65535
				}));
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E84 File Offset: 0x00001084
		internal static void ValidatePort(this int value, string argument)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(argument, string.Format(CultureInfo.InvariantCulture, "Specified value cannot be less than {0}.", new object[]
				{
					0
				}));
			}
			if (value > 65535)
			{
				throw new ArgumentOutOfRangeException(argument, string.Format(CultureInfo.InvariantCulture, "Specified value cannot be greater than {0}.", new object[]
				{
					65535
				}));
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002EEC File Offset: 0x000010EC
		public static byte[] Take(this byte[] value, int offset, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (count == 0)
			{
				return Array<byte>.Empty;
			}
			if (offset == 0 && value.Length == count)
			{
				return value;
			}
			byte[] array = new byte[count];
			Buffer.BlockCopy(value, offset, array, 0, count);
			return array;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F30 File Offset: 0x00001130
		public static byte[] Take(this byte[] value, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (count == 0)
			{
				return Array<byte>.Empty;
			}
			if (value.Length == count)
			{
				return value;
			}
			byte[] array = new byte[count];
			Buffer.BlockCopy(value, 0, array, 0, count);
			return array;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F70 File Offset: 0x00001170
		public static bool IsEqualTo(this byte[] left, byte[] right)
		{
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			if (right == null)
			{
				throw new ArgumentNullException("right");
			}
			if (left == right)
			{
				return true;
			}
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002FC4 File Offset: 0x000011C4
		public static byte[] TrimLeadingZeros(this byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int i = 0;
			while (i < value.Length)
			{
				if (value[i] != 0)
				{
					if (i == 0)
					{
						return value;
					}
					int num = value.Length - i;
					byte[] array = new byte[num];
					Buffer.BlockCopy(value, i, array, 0, num);
					return array;
				}
				else
				{
					i++;
				}
			}
			return value;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003014 File Offset: 0x00001214
		public static byte[] Concat(this byte[] first, byte[] second)
		{
			if (first == null || first.Length == 0)
			{
				return second;
			}
			if (second == null || second.Length == 0)
			{
				return first;
			}
			byte[] array = new byte[first.Length + second.Length];
			Buffer.BlockCopy(first, 0, array, 0, first.Length);
			Buffer.BlockCopy(second, 0, array, first.Length, second.Length);
			return array;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000305B File Offset: 0x0000125B
		internal static bool CanRead(this Socket socket)
		{
			return SocketAbstraction.CanRead(socket);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003063 File Offset: 0x00001263
		internal static bool CanWrite(this Socket socket)
		{
			return SocketAbstraction.CanWrite(socket);
		}
	}
}
