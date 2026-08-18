using System;
using System.Data.Common;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data.OracleClient
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public sealed class OracleException : DbException
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x00069194 File Offset: 0x00068594
		private OracleException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
			this._code = (int)si.GetValue("code", typeof(int));
			base.HResult = -2146232008;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000691D4 File Offset: 0x000685D4
		private OracleException(string message, int code) : base(message)
		{
			this._code = code;
			base.HResult = -2146232008;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00069204 File Offset: 0x00068604
		public int Code
		{
			get
			{
				return this._code;
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00069224 File Offset: 0x00068624
		private static bool ConnectionIsBroken(int code)
		{
			bool result;
			if (12500 <= code && code <= 12699)
			{
				result = true;
			}
			else
			{
				if (code <= 1012)
				{
					if (code <= 24)
					{
						switch (code)
						{
						case 18:
						case 19:
							break;
						default:
							if (code != 24)
							{
								goto IL_A5;
							}
							break;
						}
					}
					else if (code != 28 && code != 436 && code != 1012)
					{
						goto IL_A5;
					}
				}
				else if (code <= 1075)
				{
					switch (code)
					{
					case 1033:
					case 1034:
						break;
					default:
						if (code != 1075)
						{
							goto IL_A5;
						}
						break;
					}
				}
				else if (code != 2392 && code != 2399)
				{
					switch (code)
					{
					case 3113:
					case 3114:
						break;
					default:
						goto IL_A5;
					}
				}
				return true;
				IL_A5:
				result = false;
			}
			return result;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000692E4 File Offset: 0x000686E4
		internal static OracleException CreateException(OciErrorHandle errorHandle, int rc)
		{
			OracleException result;
			using (NativeBuffer nativeBuffer = new NativeBuffer_Exception(1000))
			{
				int num2;
				string text;
				if (errorHandle != null)
				{
					int recordno = 1;
					int num = TracedNativeMethods.OCIErrorGet(errorHandle, recordno, out num2, nativeBuffer);
					if (num == 0)
					{
						text = errorHandle.PtrToString(nativeBuffer);
						if (num2 != 0 && text.StartsWith("ORA-00000", StringComparison.Ordinal) && TracedNativeMethods.oermsg((short)num2, nativeBuffer) == 0)
						{
							text = errorHandle.PtrToString(nativeBuffer);
						}
					}
					else
					{
						text = Res.GetString("ADP_NoMessageAvailable", new object[]
						{
							rc,
							num
						});
						num2 = 0;
					}
					if (OracleException.ConnectionIsBroken(num2))
					{
						errorHandle.ConnectionIsBroken = true;
					}
				}
				else
				{
					text = Res.GetString("ADP_NoMessageAvailable", new object[]
					{
						rc,
						-1
					});
					num2 = 0;
				}
				result = new OracleException(text, num2);
			}
			return result;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000693E4 File Offset: 0x000687E4
		internal static OracleException CreateException(int rc, OracleInternalConnection internalConnection)
		{
			OracleException result;
			using (NativeBuffer nativeBuffer = new NativeBuffer_Exception(1000))
			{
				int length = nativeBuffer.Length;
				int code = 0;
				int num = TracedNativeMethods.OraMTSOCIErrGet(ref code, nativeBuffer, ref length);
				string message;
				if (1 == num)
				{
					message = nativeBuffer.PtrToStringAnsi(0, length);
				}
				else
				{
					message = Res.GetString("ADP_NoMessageAvailable", new object[]
					{
						rc,
						num
					});
					code = 0;
				}
				if (OracleException.ConnectionIsBroken(code))
				{
					internalConnection.DoomThisConnection();
				}
				result = new OracleException(message, code);
			}
			return result;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00069494 File Offset: 0x00068894
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal static void Check(OciErrorHandle errorHandle, int rc)
		{
			OCI.RETURNCODE returncode = (OCI.RETURNCODE)rc;
			switch (returncode)
			{
			case OCI.RETURNCODE.OCI_INVALID_HANDLE:
				throw ADP.InvalidOperation(Res.GetString("ADP_InternalError", new object[]
				{
					rc
				}));
			case OCI.RETURNCODE.OCI_ERROR:
				break;
			default:
				if (returncode != OCI.RETURNCODE.OCI_NO_DATA)
				{
					if (rc < 0 || rc == 99 || rc == 1)
					{
						throw ADP.Simple(Res.GetString("ADP_UnexpectedReturnCode", new object[]
						{
							rc.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return;
				}
				break;
			}
			throw ADP.OracleError(errorHandle, rc);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00069524 File Offset: 0x00068924
		internal static void Check(int rc, OracleInternalConnection internalConnection)
		{
			if (rc != 0)
			{
				throw ADP.OracleError(rc, internalConnection);
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00069544 File Offset: 0x00068944
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (si == null)
			{
				throw new ArgumentNullException("si");
			}
			si.AddValue("code", this._code, typeof(int));
			base.GetObjectData(si, context);
		}

		// Token: 0x0400042B RID: 1067
		private int _code;
	}
}
