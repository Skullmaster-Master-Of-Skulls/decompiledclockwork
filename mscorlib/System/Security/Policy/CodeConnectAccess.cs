using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020004AA RID: 1194
	[ComVisible(true)]
	[Serializable]
	public class CodeConnectAccess
	{
		// Token: 0x06002F48 RID: 12104 RVA: 0x0009FFE4 File Offset: 0x0009EFE4
		public CodeConnectAccess(string allowScheme, int allowPort)
		{
			if (!CodeConnectAccess.IsValidScheme(allowScheme))
			{
				throw new ArgumentOutOfRangeException("allowScheme");
			}
			this.SetCodeConnectAccess(allowScheme.ToLower(CultureInfo.InvariantCulture), allowPort);
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000A0014 File Offset: 0x0009F014
		public static CodeConnectAccess CreateOriginSchemeAccess(int allowPort)
		{
			CodeConnectAccess codeConnectAccess = new CodeConnectAccess();
			codeConnectAccess.SetCodeConnectAccess(CodeConnectAccess.OriginScheme, allowPort);
			return codeConnectAccess;
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000A0034 File Offset: 0x0009F034
		public static CodeConnectAccess CreateAnySchemeAccess(int allowPort)
		{
			CodeConnectAccess codeConnectAccess = new CodeConnectAccess();
			codeConnectAccess.SetCodeConnectAccess(CodeConnectAccess.AnyScheme, allowPort);
			return codeConnectAccess;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000A0054 File Offset: 0x0009F054
		private CodeConnectAccess()
		{
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000A005C File Offset: 0x0009F05C
		private void SetCodeConnectAccess(string lowerCaseScheme, int allowPort)
		{
			this._LowerCaseScheme = lowerCaseScheme;
			if (allowPort == CodeConnectAccess.DefaultPort)
			{
				this._LowerCasePort = "$default";
			}
			else if (allowPort == CodeConnectAccess.OriginPort)
			{
				this._LowerCasePort = "$origin";
			}
			else
			{
				if (allowPort < 0 || allowPort > 65535)
				{
					throw new ArgumentOutOfRangeException("allowPort");
				}
				this._LowerCasePort = allowPort.ToString(CultureInfo.InvariantCulture);
			}
			this._IntPort = allowPort;
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x000A00CA File Offset: 0x0009F0CA
		public string Scheme
		{
			get
			{
				return this._LowerCaseScheme;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002F4E RID: 12110 RVA: 0x000A00D2 File Offset: 0x0009F0D2
		public int Port
		{
			get
			{
				return this._IntPort;
			}
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000A00DC File Offset: 0x0009F0DC
		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			CodeConnectAccess codeConnectAccess = o as CodeConnectAccess;
			return codeConnectAccess != null && this.Scheme == codeConnectAccess.Scheme && this.Port == codeConnectAccess.Port;
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000A0120 File Offset: 0x0009F120
		public override int GetHashCode()
		{
			return this.Scheme.GetHashCode() + this.Port.GetHashCode();
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000A0148 File Offset: 0x0009F148
		internal CodeConnectAccess(string allowScheme, string allowPort)
		{
			if (allowScheme == null || allowScheme.Length == 0)
			{
				throw new ArgumentNullException("allowScheme");
			}
			if (allowPort == null || allowPort.Length == 0)
			{
				throw new ArgumentNullException("allowPort");
			}
			this._LowerCaseScheme = allowScheme.ToLower(CultureInfo.InvariantCulture);
			if (this._LowerCaseScheme == CodeConnectAccess.OriginScheme)
			{
				this._LowerCaseScheme = CodeConnectAccess.OriginScheme;
			}
			else if (this._LowerCaseScheme == CodeConnectAccess.AnyScheme)
			{
				this._LowerCaseScheme = CodeConnectAccess.AnyScheme;
			}
			else if (!CodeConnectAccess.IsValidScheme(this._LowerCaseScheme))
			{
				throw new ArgumentOutOfRangeException("allowScheme");
			}
			this._LowerCasePort = allowPort.ToLower(CultureInfo.InvariantCulture);
			if (this._LowerCasePort == "$default")
			{
				this._IntPort = CodeConnectAccess.DefaultPort;
				return;
			}
			if (this._LowerCasePort == "$origin")
			{
				this._IntPort = CodeConnectAccess.OriginPort;
				return;
			}
			this._IntPort = int.Parse(allowPort, CultureInfo.InvariantCulture);
			if (this._IntPort < 0 || this._IntPort > 65535)
			{
				throw new ArgumentOutOfRangeException("allowPort");
			}
			this._LowerCasePort = this._IntPort.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x000A0283 File Offset: 0x0009F283
		internal bool IsOriginScheme
		{
			get
			{
				return this._LowerCaseScheme == CodeConnectAccess.OriginScheme;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x000A0292 File Offset: 0x0009F292
		internal bool IsAnyScheme
		{
			get
			{
				return this._LowerCaseScheme == CodeConnectAccess.AnyScheme;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x000A02A1 File Offset: 0x0009F2A1
		internal bool IsDefaultPort
		{
			get
			{
				return this.Port == CodeConnectAccess.DefaultPort;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002F55 RID: 12117 RVA: 0x000A02B0 File Offset: 0x0009F2B0
		internal bool IsOriginPort
		{
			get
			{
				return this.Port == CodeConnectAccess.OriginPort;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x000A02BF File Offset: 0x0009F2BF
		internal string StrPort
		{
			get
			{
				return this._LowerCasePort;
			}
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000A02C8 File Offset: 0x0009F2C8
		internal static bool IsValidScheme(string scheme)
		{
			if (scheme == null || scheme.Length == 0 || !CodeConnectAccess.IsAsciiLetter(scheme[0]))
			{
				return false;
			}
			for (int i = scheme.Length - 1; i > 0; i--)
			{
				if (!CodeConnectAccess.IsAsciiLetterOrDigit(scheme[i]) && scheme[i] != '+' && scheme[i] != '-' && scheme[i] != '.')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000A0335 File Offset: 0x0009F335
		private static bool IsAsciiLetterOrDigit(char character)
		{
			return CodeConnectAccess.IsAsciiLetter(character) || (character >= '0' && character <= '9');
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000A0350 File Offset: 0x0009F350
		private static bool IsAsciiLetter(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z');
		}

		// Token: 0x04001810 RID: 6160
		private const string DefaultStr = "$default";

		// Token: 0x04001811 RID: 6161
		private const string OriginStr = "$origin";

		// Token: 0x04001812 RID: 6162
		internal const int NoPort = -1;

		// Token: 0x04001813 RID: 6163
		internal const int AnyPort = -2;

		// Token: 0x04001814 RID: 6164
		private string _LowerCaseScheme;

		// Token: 0x04001815 RID: 6165
		private string _LowerCasePort;

		// Token: 0x04001816 RID: 6166
		private int _IntPort;

		// Token: 0x04001817 RID: 6167
		public static readonly int DefaultPort = -3;

		// Token: 0x04001818 RID: 6168
		public static readonly int OriginPort = -4;

		// Token: 0x04001819 RID: 6169
		public static readonly string OriginScheme = "$origin";

		// Token: 0x0400181A RID: 6170
		public static readonly string AnyScheme = "*";
	}
}
