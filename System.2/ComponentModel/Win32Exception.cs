using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.ComponentModel
{
	// Token: 0x020005BC RID: 1468
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class Win32Exception : ExternalException, ISerializable
	{
		// Token: 0x06003713 RID: 14099 RVA: 0x000EFC5F File Offset: 0x000EDE5F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Win32Exception() : this(Marshal.GetLastWin32Error())
		{
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000EFC6C File Offset: 0x000EDE6C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Win32Exception(int error) : this(error, Win32Exception.GetErrorMessage(error))
		{
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000EFC7B File Offset: 0x000EDE7B
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Win32Exception(int error, string message) : base(message)
		{
			this.nativeErrorCode = error;
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000EFC8B File Offset: 0x000EDE8B
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Win32Exception(string message) : this(Marshal.GetLastWin32Error(), message)
		{
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000EFC99 File Offset: 0x000EDE99
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Win32Exception(string message, Exception innerException) : base(message, innerException)
		{
			this.nativeErrorCode = Marshal.GetLastWin32Error();
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000EFCAE File Offset: 0x000EDEAE
		protected Win32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			IntSecurity.UnmanagedCode.Demand();
			this.nativeErrorCode = info.GetInt32("NativeErrorCode");
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06003719 RID: 14105 RVA: 0x000EFCD3 File Offset: 0x000EDED3
		public int NativeErrorCode
		{
			get
			{
				return this.nativeErrorCode;
			}
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000EFCDC File Offset: 0x000EDEDC
		private static bool TryGetErrorMessage(int error, StringBuilder sb, out string errorMsg)
		{
			errorMsg = "";
			int num = SafeNativeMethods.FormatMessage(12800, IntPtr.Zero, (uint)error, 0, sb, sb.Capacity + 1, null);
			if (num != 0)
			{
				int i;
				for (i = sb.Length; i > 0; i--)
				{
					char c = sb[i - 1];
					if (c > ' ' && c != '.')
					{
						break;
					}
				}
				errorMsg = sb.ToString(0, i);
			}
			else
			{
				if (Marshal.GetLastWin32Error() == 122)
				{
					return false;
				}
				errorMsg = "Unknown error (0x" + Convert.ToString(error, 16) + ")";
			}
			return true;
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000EFD68 File Offset: 0x000EDF68
		private static string GetErrorMessage(int error)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			string result;
			while (!Win32Exception.TryGetErrorMessage(error, stringBuilder, out result))
			{
				stringBuilder.Capacity *= 4;
				if (stringBuilder.Capacity >= 66560)
				{
					return "Unknown error (0x" + Convert.ToString(error, 16) + ")";
				}
			}
			return result;
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000EFDBF File Offset: 0x000EDFBF
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("NativeErrorCode", this.nativeErrorCode);
			base.GetObjectData(info, context);
		}

		// Token: 0x04002AC5 RID: 10949
		private readonly int nativeErrorCode;

		// Token: 0x04002AC6 RID: 10950
		private const int MaxAllowedBufferSize = 66560;
	}
}
