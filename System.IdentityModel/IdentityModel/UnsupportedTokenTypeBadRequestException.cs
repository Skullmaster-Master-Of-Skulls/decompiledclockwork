using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.IdentityModel
{
	// Token: 0x020000B3 RID: 179
	[Serializable]
	public class UnsupportedTokenTypeBadRequestException : BadRequestException
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x000147F8 File Offset: 0x000129F8
		public UnsupportedTokenTypeBadRequestException()
		{
			this._tokenType = string.Empty;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001480B File Offset: 0x00012A0B
		public UnsupportedTokenTypeBadRequestException(string tokenType) : base(SR.GetString("ID2014", new object[]
		{
			tokenType
		}))
		{
			this._tokenType = tokenType;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001482E File Offset: 0x00012A2E
		public UnsupportedTokenTypeBadRequestException(string message, Exception exception) : base(message, exception)
		{
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00014838 File Offset: 0x00012A38
		protected UnsupportedTokenTypeBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("info");
			}
			this._tokenType = (info.GetValue("TokenType", typeof(string)) as string);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00014875 File Offset: 0x00012A75
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("info");
			}
			info.AddValue("TokenType", this.TokenType);
			base.GetObjectData(info, context);
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000148A3 File Offset: 0x00012AA3
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000148AB File Offset: 0x00012AAB
		public string TokenType
		{
			get
			{
				return this._tokenType;
			}
			set
			{
				this._tokenType = value;
			}
		}

		// Token: 0x040004CA RID: 1226
		private const string TokenTypeProperty = "TokenType";

		// Token: 0x040004CB RID: 1227
		private string _tokenType;
	}
}
