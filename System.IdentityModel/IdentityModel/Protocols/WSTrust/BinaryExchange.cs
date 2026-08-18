using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001F0 RID: 496
	public class BinaryExchange
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x00046EBC File Offset: 0x000450BC
		public BinaryExchange(byte[] binaryData, Uri valueType) : this(binaryData, valueType, new Uri("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"))
		{
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00046ED0 File Offset: 0x000450D0
		public BinaryExchange(byte[] binaryData, Uri valueType, Uri encodingType)
		{
			if (binaryData == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binaryData");
			}
			if (valueType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("valueType");
			}
			if (encodingType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encodingType");
			}
			if (!valueType.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("valueType", SR.GetString("ID0013"));
			}
			if (!encodingType.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("encodingType", SR.GetString("ID0013"));
			}
			this._binaryData = binaryData;
			this._valueType = valueType;
			this._encodingType = encodingType;
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00046F81 File Offset: 0x00045181
		public byte[] BinaryData
		{
			get
			{
				return this._binaryData;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00046F89 File Offset: 0x00045189
		public Uri ValueType
		{
			get
			{
				return this._valueType;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00046F91 File Offset: 0x00045191
		public Uri EncodingType
		{
			get
			{
				return this._encodingType;
			}
		}

		// Token: 0x04000E64 RID: 3684
		private byte[] _binaryData;

		// Token: 0x04000E65 RID: 3685
		private Uri _valueType;

		// Token: 0x04000E66 RID: 3686
		private Uri _encodingType;
	}
}
