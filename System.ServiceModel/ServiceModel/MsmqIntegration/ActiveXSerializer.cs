using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003AC RID: 940
	internal class ActiveXSerializer
	{
		// Token: 0x06002333 RID: 9011 RVA: 0x0008065C File Offset: 0x0007E85C
		private TKind[] TakeLockedBuffer<TKind>(out bool lockHeld, int size)
		{
			lockHeld = false;
			Monitor.Enter(this.bufferLock, ref lockHeld);
			if (typeof(byte) == typeof(TKind))
			{
				if (this.byteBuffer == null || size > this.byteBuffer.Length)
				{
					this.byteBuffer = new byte[size];
				}
				return this.byteBuffer as TKind[];
			}
			if (typeof(char) == typeof(TKind))
			{
				if (this.charBuffer == null || size > this.charBuffer.Length)
				{
					this.charBuffer = new char[size];
				}
				return this.charBuffer as TKind[];
			}
			return null;
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x00080705 File Offset: 0x0007E905
		private void ReleaseLockedBuffer()
		{
			Monitor.Exit(this.bufferLock);
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00080714 File Offset: 0x0007E914
		public object Deserialize(MemoryStream stream, int bodyType)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("stream"));
			}
			byte[] array;
			int num2;
			bool flag;
			switch (bodyType)
			{
			case 1:
				return null;
			case 2:
			{
				array = new byte[2];
				int num = stream.Read(array, 0, 2);
				if (num != 2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return BitConverter.ToInt16(array, 0);
			}
			case 3:
			{
				array = new byte[4];
				int num = stream.Read(array, 0, 4);
				if (num != 4)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return BitConverter.ToInt32(array, 0);
			}
			case 4:
			{
				array = new byte[4];
				int num = stream.Read(array, 0, 4);
				if (num != 4)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return BitConverter.ToSingle(array, 0);
			}
			case 5:
			{
				array = new byte[8];
				int num = stream.Read(array, 0, 8);
				if (num != 8)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return BitConverter.ToDouble(array, 0);
			}
			case 6:
			{
				array = new byte[8];
				int num = stream.Read(array, 0, 8);
				if (num != 8)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return decimal.FromOACurrency(BitConverter.ToInt64(array, 0));
			}
			case 7:
			{
				array = new byte[8];
				int num = stream.Read(array, 0, 8);
				if (num != 8)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return new DateTime(BitConverter.ToInt64(array, 0));
			}
			case 8:
			case 31:
				break;
			case 9:
			case 10:
			case 12:
			case 13:
			case 14:
			case 15:
			case 22:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
				goto IL_48F;
			case 11:
			{
				array = new byte[1];
				int num = stream.Read(array, 0, 1);
				if (num != 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return array[0] > 0;
			}
			case 16:
			case 17:
			{
				array = new byte[1];
				int num = stream.Read(array, 0, 1);
				if (num != 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return array[0];
			}
			case 18:
			{
				array = new byte[2];
				int num = stream.Read(array, 0, 2);
				if (num != 2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return BitConverter.ToUInt16(array, 0);
			}
			case 19:
			{
				array = new byte[4];
				int num = stream.Read(array, 0, 4);
				if (num != 4)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return BitConverter.ToUInt32(array, 0);
			}
			case 20:
			{
				array = new byte[8];
				int num = stream.Read(array, 0, 8);
				if (num != 8)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return BitConverter.ToInt64(array, 0);
			}
			case 21:
			{
				array = new byte[8];
				int num = stream.Read(array, 0, 8);
				if (num != 8)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
				}
				return BitConverter.ToUInt64(array, 0);
			}
			case 30:
				array = stream.ToArray();
				num2 = array.Length;
				flag = false;
				try
				{
					char[] array2 = this.TakeLockedBuffer<char>(out flag, num2);
					Encoding.ASCII.GetChars(array, 0, num2, array2, 0);
					return new string(array2, 0, num2);
				}
				finally
				{
					if (flag)
					{
						this.ReleaseLockedBuffer();
					}
				}
				break;
			default:
				if (bodyType != 72)
				{
					if (bodyType != 4113)
					{
						goto IL_48F;
					}
					goto IL_144;
				}
				else
				{
					array = new byte[16];
					int num = stream.Read(array, 0, 16);
					if (num != 16)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqCannotDeserializeActiveXMessage")));
					}
					return new Guid(array);
				}
				break;
			}
			array = stream.ToArray();
			num2 = array.Length / 2;
			flag = false;
			try
			{
				char[] array3 = this.TakeLockedBuffer<char>(out flag, num2);
				Encoding.Unicode.GetChars(array, 0, num2 * 2, array3, 0);
				return new string(array3, 0, num2);
			}
			finally
			{
				if (flag)
				{
					this.ReleaseLockedBuffer();
				}
			}
			IL_144:
			array = stream.ToArray();
			byte[] array4 = new byte[array.Length];
			Array.Copy(array, array4, array.Length);
			return array4;
			IL_48F:
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("MsmqInvalidTypeDeserialization")));
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00080BE8 File Offset: 0x0007EDE8
		public void Serialize(Stream stream, object obj, ref int bodyType)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("stream"));
			}
			VarEnum varEnum;
			if (obj is string)
			{
				int num = ((string)obj).Length * 2;
				bool flag = false;
				try
				{
					byte[] array = this.TakeLockedBuffer<byte>(out flag, num);
					Encoding.Unicode.GetBytes(((string)obj).ToCharArray(), 0, num / 2, array, 0);
					stream.Write(array, 0, num);
				}
				finally
				{
					if (flag)
					{
						this.ReleaseLockedBuffer();
					}
				}
				varEnum = VarEnum.VT_LPWSTR;
			}
			else if (obj is byte[])
			{
				byte[] array2 = (byte[])obj;
				stream.Write(array2, 0, array2.Length);
				varEnum = (VarEnum)4113;
			}
			else if (obj is char[])
			{
				char[] array3 = (char[])obj;
				int num2 = array3.Length * 2;
				bool flag2 = false;
				try
				{
					byte[] array4 = this.TakeLockedBuffer<byte>(out flag2, num2);
					Encoding.Unicode.GetBytes(array3, 0, num2 / 2, array4, 0);
					stream.Write(array4, 0, num2);
				}
				finally
				{
					if (flag2)
					{
						this.ReleaseLockedBuffer();
					}
				}
				varEnum = VarEnum.VT_LPWSTR;
			}
			else if (obj is byte)
			{
				stream.Write(new byte[]
				{
					(byte)obj
				}, 0, 1);
				varEnum = VarEnum.VT_UI1;
			}
			else if (obj is bool)
			{
				if ((bool)obj)
				{
					stream.Write(new byte[]
					{
						byte.MaxValue
					}, 0, 1);
				}
				else
				{
					stream.Write(new byte[1], 0, 1);
				}
				varEnum = VarEnum.VT_BOOL;
			}
			else if (obj is char)
			{
				byte[] bytes = BitConverter.GetBytes((char)obj);
				stream.Write(bytes, 0, 2);
				varEnum = VarEnum.VT_UI2;
			}
			else if (obj is decimal)
			{
				byte[] bytes2 = BitConverter.GetBytes(decimal.ToOACurrency((decimal)obj));
				stream.Write(bytes2, 0, 8);
				varEnum = VarEnum.VT_CY;
			}
			else if (obj is DateTime)
			{
				byte[] bytes3 = BitConverter.GetBytes(((DateTime)obj).Ticks);
				stream.Write(bytes3, 0, 8);
				varEnum = VarEnum.VT_DATE;
			}
			else if (obj is double)
			{
				byte[] bytes4 = BitConverter.GetBytes((double)obj);
				stream.Write(bytes4, 0, 8);
				varEnum = VarEnum.VT_R8;
			}
			else if (obj is Guid)
			{
				byte[] buffer = ((Guid)obj).ToByteArray();
				stream.Write(buffer, 0, 16);
				varEnum = VarEnum.VT_CLSID;
			}
			else if (obj is short)
			{
				byte[] bytes5 = BitConverter.GetBytes((short)obj);
				stream.Write(bytes5, 0, 2);
				varEnum = VarEnum.VT_I2;
			}
			else if (obj is ushort)
			{
				byte[] bytes6 = BitConverter.GetBytes((ushort)obj);
				stream.Write(bytes6, 0, 2);
				varEnum = VarEnum.VT_UI2;
			}
			else if (obj is int)
			{
				byte[] bytes7 = BitConverter.GetBytes((int)obj);
				stream.Write(bytes7, 0, 4);
				varEnum = VarEnum.VT_I4;
			}
			else if (obj is uint)
			{
				byte[] bytes8 = BitConverter.GetBytes((uint)obj);
				stream.Write(bytes8, 0, 4);
				varEnum = VarEnum.VT_UI4;
			}
			else if (obj is long)
			{
				byte[] bytes9 = BitConverter.GetBytes((long)obj);
				stream.Write(bytes9, 0, 8);
				varEnum = VarEnum.VT_I8;
			}
			else if (obj is ulong)
			{
				byte[] bytes10 = BitConverter.GetBytes((ulong)obj);
				stream.Write(bytes10, 0, 8);
				varEnum = VarEnum.VT_UI8;
			}
			else
			{
				if (!(obj is float))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqInvalidTypeSerialization")));
				}
				byte[] bytes11 = BitConverter.GetBytes((float)obj);
				stream.Write(bytes11, 0, 4);
				varEnum = VarEnum.VT_R4;
			}
			bodyType = (int)varEnum;
		}

		// Token: 0x04001FDE RID: 8158
		private byte[] byteBuffer;

		// Token: 0x04001FDF RID: 8159
		private char[] charBuffer;

		// Token: 0x04001FE0 RID: 8160
		private object bufferLock = new object();
	}
}
