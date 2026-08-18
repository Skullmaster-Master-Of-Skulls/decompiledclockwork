using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x020004AC RID: 1196
	[ComVisible(true)]
	[Serializable]
	public sealed class PermissionRequestEvidence : IBuiltInEvidence
	{
		// Token: 0x06002F78 RID: 12152 RVA: 0x000A1268 File Offset: 0x000A0268
		public PermissionRequestEvidence(PermissionSet request, PermissionSet optional, PermissionSet denied)
		{
			if (request == null)
			{
				this.m_request = null;
			}
			else
			{
				this.m_request = request.Copy();
			}
			if (optional == null)
			{
				this.m_optional = null;
			}
			else
			{
				this.m_optional = optional.Copy();
			}
			if (denied == null)
			{
				this.m_denied = null;
				return;
			}
			this.m_denied = denied.Copy();
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x000A12C2 File Offset: 0x000A02C2
		internal PermissionRequestEvidence()
		{
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x000A12CA File Offset: 0x000A02CA
		public PermissionSet RequestedPermissions
		{
			get
			{
				return this.m_request;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x000A12D2 File Offset: 0x000A02D2
		public PermissionSet OptionalPermissions
		{
			get
			{
				return this.m_optional;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x000A12DA File Offset: 0x000A02DA
		public PermissionSet DeniedPermissions
		{
			get
			{
				return this.m_denied;
			}
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000A12E2 File Offset: 0x000A02E2
		public PermissionRequestEvidence Copy()
		{
			return new PermissionRequestEvidence(this.m_request, this.m_optional, this.m_denied);
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000A12FC File Offset: 0x000A02FC
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.PermissionRequestEvidence");
			securityElement.AddAttribute("version", "1");
			if (this.m_request != null)
			{
				SecurityElement securityElement2 = new SecurityElement("Request");
				securityElement2.AddChild(this.m_request.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.m_optional != null)
			{
				SecurityElement securityElement2 = new SecurityElement("Optional");
				securityElement2.AddChild(this.m_optional.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.m_denied != null)
			{
				SecurityElement securityElement2 = new SecurityElement("Denied");
				securityElement2.AddChild(this.m_denied.ToXml());
				securityElement.AddChild(securityElement2);
			}
			return securityElement;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000A13A8 File Offset: 0x000A03A8
		internal void CreateStrings()
		{
			if (this.m_strRequest == null && this.m_request != null)
			{
				this.m_strRequest = this.m_request.ToXml().ToString();
			}
			if (this.m_strOptional == null && this.m_optional != null)
			{
				this.m_strOptional = this.m_optional.ToXml().ToString();
			}
			if (this.m_strDenied == null && this.m_denied != null)
			{
				this.m_strDenied = this.m_denied.ToXml().ToString();
			}
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000A1428 File Offset: 0x000A0428
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			this.CreateStrings();
			int position2 = 0;
			int num = 0;
			int num2 = position + 1;
			buffer[position] = '\a';
			if (verbose)
			{
				position2 = num2;
				num2 += 2;
			}
			if (this.m_strRequest != null)
			{
				int length = this.m_strRequest.Length;
				if (verbose)
				{
					buffer[num2++] = '\0';
					BuiltInEvidenceHelper.CopyIntToCharArray(length, buffer, num2);
					num2 += 2;
					num++;
				}
				this.m_strRequest.CopyTo(0, buffer, num2, length);
				num2 += length;
			}
			if (this.m_strOptional != null)
			{
				int length = this.m_strOptional.Length;
				if (verbose)
				{
					buffer[num2++] = '\u0001';
					BuiltInEvidenceHelper.CopyIntToCharArray(length, buffer, num2);
					num2 += 2;
					num++;
				}
				this.m_strOptional.CopyTo(0, buffer, num2, length);
				num2 += length;
			}
			if (this.m_strDenied != null)
			{
				int length = this.m_strDenied.Length;
				if (verbose)
				{
					buffer[num2++] = '\u0002';
					BuiltInEvidenceHelper.CopyIntToCharArray(length, buffer, num2);
					num2 += 2;
					num++;
				}
				this.m_strDenied.CopyTo(0, buffer, num2, length);
				num2 += length;
			}
			if (verbose)
			{
				BuiltInEvidenceHelper.CopyIntToCharArray(num, buffer, position2);
			}
			return num2;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000A1524 File Offset: 0x000A0524
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			this.CreateStrings();
			int num = 1;
			if (this.m_strRequest != null)
			{
				if (verbose)
				{
					num += 3;
				}
				num += this.m_strRequest.Length;
			}
			if (this.m_strOptional != null)
			{
				if (verbose)
				{
					num += 3;
				}
				num += this.m_strOptional.Length;
			}
			if (this.m_strDenied != null)
			{
				if (verbose)
				{
					num += 3;
				}
				num += this.m_strDenied.Length;
			}
			if (verbose)
			{
				num += 2;
			}
			return num;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000A1598 File Offset: 0x000A0598
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			int intFromCharArray = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position);
			position += 2;
			for (int i = 0; i < intFromCharArray; i++)
			{
				char c = buffer[position++];
				int intFromCharArray2 = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position);
				position += 2;
				string text = new string(buffer, position, intFromCharArray2);
				position += intFromCharArray2;
				Parser parser = new Parser(text);
				PermissionSet permissionSet = new PermissionSet();
				permissionSet.FromXml(parser.GetTopElement());
				switch (c)
				{
				case '\0':
					this.m_strRequest = text;
					this.m_request = permissionSet;
					break;
				case '\u0001':
					this.m_strOptional = text;
					this.m_optional = permissionSet;
					break;
				case '\u0002':
					this.m_strDenied = text;
					this.m_denied = permissionSet;
					break;
				default:
					throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFixup"));
				}
			}
			return position;
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000A1666 File Offset: 0x000A0666
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x04001822 RID: 6178
		private const char idRequest = '\0';

		// Token: 0x04001823 RID: 6179
		private const char idOptional = '\u0001';

		// Token: 0x04001824 RID: 6180
		private const char idDenied = '\u0002';

		// Token: 0x04001825 RID: 6181
		private PermissionSet m_request;

		// Token: 0x04001826 RID: 6182
		private PermissionSet m_optional;

		// Token: 0x04001827 RID: 6183
		private PermissionSet m_denied;

		// Token: 0x04001828 RID: 6184
		private string m_strRequest;

		// Token: 0x04001829 RID: 6185
		private string m_strOptional;

		// Token: 0x0400182A RID: 6186
		private string m_strDenied;
	}
}
