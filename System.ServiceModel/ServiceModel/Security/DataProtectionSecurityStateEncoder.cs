using System;
using System.Security.Cryptography;
using System.Text;

namespace System.ServiceModel.Security
{
	// Token: 0x020002F7 RID: 759
	public class DataProtectionSecurityStateEncoder : SecurityStateEncoder
	{
		// Token: 0x0600198A RID: 6538 RVA: 0x0005F6B8 File Offset: 0x0005D8B8
		public DataProtectionSecurityStateEncoder() : this(true)
		{
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0005F6C1 File Offset: 0x0005D8C1
		public DataProtectionSecurityStateEncoder(bool useCurrentUserProtectionScope) : this(useCurrentUserProtectionScope, null)
		{
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0005F6CB File Offset: 0x0005D8CB
		public DataProtectionSecurityStateEncoder(bool useCurrentUserProtectionScope, byte[] entropy)
		{
			this.useCurrentUserProtectionScope = useCurrentUserProtectionScope;
			if (entropy == null)
			{
				this.entropy = null;
				return;
			}
			this.entropy = DiagnosticUtility.Utility.AllocateByteArray(entropy.Length);
			Buffer.BlockCopy(entropy, 0, this.entropy, 0, entropy.Length);
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x0005F709 File Offset: 0x0005D909
		public bool UseCurrentUserProtectionScope
		{
			get
			{
				return this.useCurrentUserProtectionScope;
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0005F714 File Offset: 0x0005D914
		public byte[] GetEntropy()
		{
			byte[] array = null;
			if (this.entropy != null)
			{
				array = DiagnosticUtility.Utility.AllocateByteArray(this.entropy.Length);
				Buffer.BlockCopy(this.entropy, 0, array, 0, this.entropy.Length);
			}
			return array;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0005F758 File Offset: 0x0005D958
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.GetType().ToString());
			stringBuilder.AppendFormat("{0}  UseCurrentUserProtectionScope={1}", Environment.NewLine, this.useCurrentUserProtectionScope);
			stringBuilder.AppendFormat("{0}  Entropy Length={1}", Environment.NewLine, (this.entropy == null) ? 0 : this.entropy.Length);
			return stringBuilder.ToString();
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0005F7C8 File Offset: 0x0005D9C8
		protected internal override byte[] DecodeSecurityState(byte[] data)
		{
			byte[] result;
			try
			{
				result = ProtectedData.Unprotect(data, this.entropy, this.useCurrentUserProtectionScope ? DataProtectionScope.CurrentUser : DataProtectionScope.LocalMachine);
			}
			catch (CryptographicException inner)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("SecurityStateEncoderDecodingFailure"), inner));
			}
			return result;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0005F820 File Offset: 0x0005DA20
		protected internal override byte[] EncodeSecurityState(byte[] data)
		{
			byte[] result;
			try
			{
				result = ProtectedData.Protect(data, this.entropy, this.useCurrentUserProtectionScope ? DataProtectionScope.CurrentUser : DataProtectionScope.LocalMachine);
			}
			catch (CryptographicException inner)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("SecurityStateEncoderEncodingFailure"), inner));
			}
			return result;
		}

		// Token: 0x04001CA4 RID: 7332
		private byte[] entropy;

		// Token: 0x04001CA5 RID: 7333
		private bool useCurrentUserProtectionScope;
	}
}
