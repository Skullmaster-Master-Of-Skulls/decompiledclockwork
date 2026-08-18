using System;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200038B RID: 907
	public static class ServiceModelSecurityTokenTypes
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0007BB0A File Offset: 0x00079D0A
		public static string Spnego
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/Spnego";
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0007BB11 File Offset: 0x00079D11
		public static string MutualSslnego
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/MutualSslnego";
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0007BB18 File Offset: 0x00079D18
		public static string AnonymousSslnego
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/AnonymousSslnego";
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0007BB1F File Offset: 0x00079D1F
		public static string SecurityContext
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/SecurityContextToken";
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x0007BB26 File Offset: 0x00079D26
		public static string SecureConversation
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/SecureConversation";
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x0007BB2D File Offset: 0x00079D2D
		public static string SspiCredential
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/SspiCredential";
			}
		}

		// Token: 0x04001F55 RID: 8021
		private const string Namespace = "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens";

		// Token: 0x04001F56 RID: 8022
		private const string spnego = "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/Spnego";

		// Token: 0x04001F57 RID: 8023
		private const string mutualSslnego = "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/MutualSslnego";

		// Token: 0x04001F58 RID: 8024
		private const string anonymousSslnego = "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/AnonymousSslnego";

		// Token: 0x04001F59 RID: 8025
		private const string securityContext = "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/SecurityContextToken";

		// Token: 0x04001F5A RID: 8026
		private const string secureConversation = "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/SecureConversation";

		// Token: 0x04001F5B RID: 8027
		private const string sspiCredential = "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/SspiCredential";
	}
}
