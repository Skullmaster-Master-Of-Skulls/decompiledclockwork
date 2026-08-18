using System;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
	// Token: 0x0200004A RID: 74
	[__DynamicallyInvokable]
	public class ViaHeaderValue : ICloneable
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		[__DynamicallyInvokable]
		public string ProtocolName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.protocolName;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000EB34 File Offset: 0x0000CD34
		[__DynamicallyInvokable]
		public string ProtocolVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.protocolVersion;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		[__DynamicallyInvokable]
		public string ReceivedBy
		{
			[__DynamicallyInvokable]
			get
			{
				return this.receivedBy;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000EB44 File Offset: 0x0000CD44
		[__DynamicallyInvokable]
		public string Comment
		{
			[__DynamicallyInvokable]
			get
			{
				return this.comment;
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		[__DynamicallyInvokable]
		public ViaHeaderValue(string protocolVersion, string receivedBy) : this(protocolVersion, receivedBy, null, null)
		{
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000EB58 File Offset: 0x0000CD58
		[__DynamicallyInvokable]
		public ViaHeaderValue(string protocolVersion, string receivedBy, string protocolName) : this(protocolVersion, receivedBy, protocolName, null)
		{
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000EB64 File Offset: 0x0000CD64
		[__DynamicallyInvokable]
		public ViaHeaderValue(string protocolVersion, string receivedBy, string protocolName, string comment)
		{
			HeaderUtilities.CheckValidToken(protocolVersion, "protocolVersion");
			ViaHeaderValue.CheckReceivedBy(receivedBy);
			if (!string.IsNullOrEmpty(protocolName))
			{
				HeaderUtilities.CheckValidToken(protocolName, "protocolName");
				this.protocolName = protocolName;
			}
			if (!string.IsNullOrEmpty(comment))
			{
				HeaderUtilities.CheckValidComment(comment, "comment");
				this.comment = comment;
			}
			this.protocolVersion = protocolVersion;
			this.receivedBy = receivedBy;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000EBCD File Offset: 0x0000CDCD
		private ViaHeaderValue()
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000EBD5 File Offset: 0x0000CDD5
		private ViaHeaderValue(ViaHeaderValue source)
		{
			this.protocolName = source.protocolName;
			this.protocolVersion = source.protocolVersion;
			this.receivedBy = source.receivedBy;
			this.comment = source.comment;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000EC10 File Offset: 0x0000CE10
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(this.protocolName))
			{
				stringBuilder.Append(this.protocolName);
				stringBuilder.Append('/');
			}
			stringBuilder.Append(this.protocolVersion);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.receivedBy);
			if (!string.IsNullOrEmpty(this.comment))
			{
				stringBuilder.Append(' ');
				stringBuilder.Append(this.comment);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000EC94 File Offset: 0x0000CE94
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			ViaHeaderValue viaHeaderValue = obj as ViaHeaderValue;
			return viaHeaderValue != null && (string.Compare(this.protocolVersion, viaHeaderValue.protocolVersion, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.receivedBy, viaHeaderValue.receivedBy, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.protocolName, viaHeaderValue.protocolName, StringComparison.OrdinalIgnoreCase) == 0) && string.CompareOrdinal(this.comment, viaHeaderValue.comment) == 0;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000ED00 File Offset: 0x0000CF00
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = this.protocolVersion.ToLowerInvariant().GetHashCode() ^ this.receivedBy.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(this.protocolName))
			{
				num ^= this.protocolName.ToLowerInvariant().GetHashCode();
			}
			if (!string.IsNullOrEmpty(this.comment))
			{
				num ^= this.comment.GetHashCode();
			}
			return num;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		[__DynamicallyInvokable]
		public static ViaHeaderValue Parse(string input)
		{
			int num = 0;
			return (ViaHeaderValue)GenericHeaderParser.SingleValueViaParser.ParseValue(input, null, ref num);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000ED90 File Offset: 0x0000CF90
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out ViaHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.SingleValueViaParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (ViaHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		internal static int GetViaLength(string input, int startIndex, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			string text = null;
			string text2 = null;
			int num = ViaHeaderValue.GetProtocolEndIndex(input, startIndex, out text, out text2);
			if (num == startIndex || num == input.Length)
			{
				return 0;
			}
			string text3 = null;
			int hostLength = HttpRuleParser.GetHostLength(input, num, true, out text3);
			if (hostLength == 0)
			{
				return 0;
			}
			num += hostLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			string text4 = null;
			if (num < input.Length && input[num] == '(')
			{
				int num2 = 0;
				if (HttpRuleParser.GetCommentLength(input, num, out num2) != HttpParseResult.Parsed)
				{
					return 0;
				}
				text4 = input.Substring(num, num2);
				num += num2;
				num += HttpRuleParser.GetWhitespaceLength(input, num);
			}
			parsedValue = new ViaHeaderValue
			{
				protocolVersion = text2,
				protocolName = text,
				receivedBy = text3,
				comment = text4
			};
			return num - startIndex;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000EE98 File Offset: 0x0000D098
		private static int GetProtocolEndIndex(string input, int startIndex, out string protocolName, out string protocolVersion)
		{
			protocolName = null;
			protocolVersion = null;
			int tokenLength = HttpRuleParser.GetTokenLength(input, startIndex);
			if (tokenLength == 0)
			{
				return 0;
			}
			int num = startIndex + tokenLength;
			int whitespaceLength = HttpRuleParser.GetWhitespaceLength(input, num);
			num += whitespaceLength;
			if (num == input.Length)
			{
				return 0;
			}
			if (input[num] == '/')
			{
				protocolName = input.Substring(startIndex, tokenLength);
				num++;
				num += HttpRuleParser.GetWhitespaceLength(input, num);
				tokenLength = HttpRuleParser.GetTokenLength(input, num);
				if (tokenLength == 0)
				{
					return 0;
				}
				protocolVersion = input.Substring(num, tokenLength);
				num += tokenLength;
				whitespaceLength = HttpRuleParser.GetWhitespaceLength(input, num);
				num += whitespaceLength;
			}
			else
			{
				protocolVersion = input.Substring(startIndex, tokenLength);
			}
			if (whitespaceLength == 0)
			{
				return 0;
			}
			return num;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000EF31 File Offset: 0x0000D131
		object ICloneable.Clone()
		{
			return new ViaHeaderValue(this);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000EF3C File Offset: 0x0000D13C
		private static void CheckReceivedBy(string receivedBy)
		{
			if (string.IsNullOrEmpty(receivedBy))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, "receivedBy");
			}
			string text = null;
			if (HttpRuleParser.GetHostLength(receivedBy, 0, true, out text) != receivedBy.Length)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					receivedBy
				}));
			}
		}

		// Token: 0x04000184 RID: 388
		private string protocolName;

		// Token: 0x04000185 RID: 389
		private string protocolVersion;

		// Token: 0x04000186 RID: 390
		private string receivedBy;

		// Token: 0x04000187 RID: 391
		private string comment;
	}
}
