using System;
using System.Collections.Generic;

namespace Microsoft.Owin.Security.OAuth.Messages
{
	// Token: 0x02000005 RID: 5
	public class AuthorizeEndpointRequest
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		public AuthorizeEndpointRequest(IReadableStringCollection parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.Scope = new List<string>();
			foreach (KeyValuePair<string, string[]> keyValuePair in parameters)
			{
				this.AddParameter(keyValuePair.Key, parameters.Get(keyValuePair.Key));
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021BC File Offset: 0x000003BC
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021C4 File Offset: 0x000003C4
		public string ResponseType { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021CD File Offset: 0x000003CD
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000021D5 File Offset: 0x000003D5
		public string ResponseMode { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021DE File Offset: 0x000003DE
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021E6 File Offset: 0x000003E6
		public string ClientId { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021EF File Offset: 0x000003EF
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021F7 File Offset: 0x000003F7
		public string RedirectUri { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002200 File Offset: 0x00000400
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002208 File Offset: 0x00000408
		public IList<string> Scope { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002211 File Offset: 0x00000411
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002219 File Offset: 0x00000419
		public string State { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002222 File Offset: 0x00000422
		public bool IsAuthorizationCodeGrantType
		{
			get
			{
				return this.ContainsGrantType("code");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000222F File Offset: 0x0000042F
		public bool IsImplicitGrantType
		{
			get
			{
				return this.ContainsGrantType("token");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000223C File Offset: 0x0000043C
		public bool IsFormPostResponseMode
		{
			get
			{
				return string.Equals(this.ResponseMode, "form_post", StringComparison.Ordinal);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002250 File Offset: 0x00000450
		public bool ContainsGrantType(string responseType)
		{
			string[] array = this.ResponseType.Split(new char[]
			{
				' '
			});
			foreach (string a in array)
			{
				if (string.Equals(a, responseType, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022A4 File Offset: 0x000004A4
		private void AddParameter(string name, string value)
		{
			if (string.Equals(name, "response_type", StringComparison.Ordinal))
			{
				this.ResponseType = value;
				return;
			}
			if (string.Equals(name, "client_id", StringComparison.Ordinal))
			{
				this.ClientId = value;
				return;
			}
			if (string.Equals(name, "redirect_uri", StringComparison.Ordinal))
			{
				this.RedirectUri = value;
				return;
			}
			if (string.Equals(name, "scope", StringComparison.Ordinal))
			{
				this.Scope = value.Split(new char[]
				{
					' '
				});
				return;
			}
			if (string.Equals(name, "state", StringComparison.Ordinal))
			{
				this.State = value;
				return;
			}
			if (string.Equals(name, "response_mode", StringComparison.Ordinal))
			{
				this.ResponseMode = value;
			}
		}
	}
}
