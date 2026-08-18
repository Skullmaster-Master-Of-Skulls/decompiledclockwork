using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000462 RID: 1122
	public sealed class X500DistinguishedName : AsnEncodedData
	{
		// Token: 0x060029A9 RID: 10665 RVA: 0x000BCE67 File Offset: 0x000BB067
		internal X500DistinguishedName(CAPIBase.CRYPTOAPI_BLOB encodedDistinguishedNameBlob) : base(new Oid(), encodedDistinguishedNameBlob)
		{
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000BCE75 File Offset: 0x000BB075
		public X500DistinguishedName(byte[] encodedDistinguishedName) : base(new Oid(), encodedDistinguishedName)
		{
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000BCE83 File Offset: 0x000BB083
		public X500DistinguishedName(AsnEncodedData encodedDistinguishedName) : base(encodedDistinguishedName)
		{
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000BCE8C File Offset: 0x000BB08C
		public X500DistinguishedName(X500DistinguishedName distinguishedName) : base(distinguishedName)
		{
			this.m_distinguishedName = distinguishedName.Name;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000BCEA1 File Offset: 0x000BB0A1
		public X500DistinguishedName(string distinguishedName) : this(distinguishedName, X500DistinguishedNameFlags.Reversed)
		{
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000BCEAB File Offset: 0x000BB0AB
		public X500DistinguishedName(string distinguishedName, X500DistinguishedNameFlags flag) : base(new Oid(), X500DistinguishedName.Encode(distinguishedName, flag))
		{
			this.m_distinguishedName = distinguishedName;
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x000BCEC6 File Offset: 0x000BB0C6
		public string Name
		{
			get
			{
				if (this.m_distinguishedName == null)
				{
					this.m_distinguishedName = this.Decode(X500DistinguishedNameFlags.Reversed);
				}
				return this.m_distinguishedName;
			}
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000BCEE4 File Offset: 0x000BB0E4
		public unsafe string Decode(X500DistinguishedNameFlags flag)
		{
			uint dwStrType = 3U | X500DistinguishedName.MapNameToStrFlag(flag);
			byte[] rawData = this.m_rawData;
			byte[] array;
			byte* value;
			if ((array = rawData) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB;
			IntPtr pName = new IntPtr((void*)(&cryptoapi_BLOB));
			cryptoapi_BLOB.cbData = (uint)rawData.Length;
			cryptoapi_BLOB.pbData = new IntPtr((void*)value);
			uint num = CAPISafe.CertNameToStrW(65537U, pName, dwStrType, SafeLocalAllocHandle.InvalidHandle, 0U);
			if (num == 0U)
			{
				throw new CryptographicException(-2146762476);
			}
			string result;
			using (SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)(2U * num)))))
			{
				if (CAPISafe.CertNameToStrW(65537U, pName, dwStrType, safeLocalAllocHandle, num) == 0U)
				{
					throw new CryptographicException(-2146762476);
				}
				result = Marshal.PtrToStringUni(safeLocalAllocHandle.DangerousGetHandle());
			}
			return result;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000BCFC0 File Offset: 0x000BB1C0
		public override string Format(bool multiLine)
		{
			if (this.m_rawData == null || this.m_rawData.Length == 0)
			{
				return string.Empty;
			}
			return CAPI.CryptFormatObject(1U, multiLine ? 1U : 0U, new IntPtr(7L), this.m_rawData);
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000BCFF4 File Offset: 0x000BB1F4
		private unsafe static byte[] Encode(string distinguishedName, X500DistinguishedNameFlags flag)
		{
			if (distinguishedName == null)
			{
				throw new ArgumentNullException("distinguishedName");
			}
			uint num = 0U;
			uint dwStrType = 3U | X500DistinguishedName.MapNameToStrFlag(flag);
			if (!CAPISafe.CertStrToNameW(65537U, distinguishedName, dwStrType, IntPtr.Zero, IntPtr.Zero, ref num, IntPtr.Zero))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			byte[] array = new byte[num];
			byte[] array2;
			byte* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array2[0];
			}
			if (!CAPISafe.CertStrToNameW(65537U, distinguishedName, dwStrType, IntPtr.Zero, new IntPtr((void*)value), ref num, IntPtr.Zero))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			array2 = null;
			return array;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000BD098 File Offset: 0x000BB298
		private static uint MapNameToStrFlag(X500DistinguishedNameFlags flag)
		{
			uint num = 29169U;
			if ((flag & (X500DistinguishedNameFlags)(~(X500DistinguishedNameFlags)num)) != X500DistinguishedNameFlags.None)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
				{
					"flag"
				}));
			}
			uint num2 = 0U;
			if (flag != X500DistinguishedNameFlags.None)
			{
				if ((flag & X500DistinguishedNameFlags.Reversed) == X500DistinguishedNameFlags.Reversed)
				{
					num2 |= 33554432U;
				}
				if ((flag & X500DistinguishedNameFlags.UseSemicolons) == X500DistinguishedNameFlags.UseSemicolons)
				{
					num2 |= 1073741824U;
				}
				else if ((flag & X500DistinguishedNameFlags.UseCommas) == X500DistinguishedNameFlags.UseCommas)
				{
					num2 |= 67108864U;
				}
				else if ((flag & X500DistinguishedNameFlags.UseNewLines) == X500DistinguishedNameFlags.UseNewLines)
				{
					num2 |= 134217728U;
				}
				if ((flag & X500DistinguishedNameFlags.DoNotUsePlusSign) == X500DistinguishedNameFlags.DoNotUsePlusSign)
				{
					num2 |= 536870912U;
				}
				if ((flag & X500DistinguishedNameFlags.DoNotUseQuotes) == X500DistinguishedNameFlags.DoNotUseQuotes)
				{
					num2 |= 268435456U;
				}
				if ((flag & X500DistinguishedNameFlags.ForceUTF8Encoding) == X500DistinguishedNameFlags.ForceUTF8Encoding)
				{
					num2 |= 524288U;
				}
				if ((flag & X500DistinguishedNameFlags.UseUTF8Encoding) == X500DistinguishedNameFlags.UseUTF8Encoding)
				{
					num2 |= 262144U;
				}
				else if ((flag & X500DistinguishedNameFlags.UseT61Encoding) == X500DistinguishedNameFlags.UseT61Encoding)
				{
					num2 |= 131072U;
				}
			}
			return num2;
		}

		// Token: 0x040025AF RID: 9647
		private string m_distinguishedName;
	}
}
