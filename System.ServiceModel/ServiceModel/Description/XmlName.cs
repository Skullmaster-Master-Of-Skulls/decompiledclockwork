using System;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x02000422 RID: 1058
	internal class XmlName
	{
		// Token: 0x06002892 RID: 10386 RVA: 0x00098180 File Offset: 0x00096380
		internal XmlName(string name) : this(name, false)
		{
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x0009818A File Offset: 0x0009638A
		internal XmlName(string name, bool isEncoded)
		{
			if (isEncoded)
			{
				XmlName.ValidateEncodedName(name, true);
				this.encoded = name;
				return;
			}
			this.decoded = name;
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x000981AB File Offset: 0x000963AB
		internal string EncodedName
		{
			get
			{
				if (this.encoded == null)
				{
					this.encoded = NamingHelper.XmlName(this.decoded);
				}
				return this.encoded;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x000981CC File Offset: 0x000963CC
		internal string DecodedName
		{
			get
			{
				if (this.decoded == null)
				{
					this.decoded = NamingHelper.CodeName(this.encoded);
				}
				return this.decoded;
			}
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000981F0 File Offset: 0x000963F0
		private static void ValidateEncodedName(string name, bool allowNull)
		{
			if (allowNull && name == null)
			{
				return;
			}
			try
			{
				XmlConvert.VerifyNCName(name);
			}
			catch (XmlException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(ex.Message, "name"));
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x0009823C File Offset: 0x0009643C
		private bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.encoded) && string.IsNullOrEmpty(this.decoded);
			}
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x00098258 File Offset: 0x00096458
		internal static bool IsNullOrEmpty(XmlName xmlName)
		{
			return xmlName == null || xmlName.IsEmpty;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x0009826B File Offset: 0x0009646B
		private bool Matches(XmlName xmlName)
		{
			return string.Equals(this.EncodedName, xmlName.EncodedName, StringComparison.Ordinal);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x00098280 File Offset: 0x00096480
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			XmlName xmlName = obj as XmlName;
			return !(xmlName == null) && this.Matches(xmlName);
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000982B1 File Offset: 0x000964B1
		public override int GetHashCode()
		{
			if (string.IsNullOrEmpty(this.EncodedName))
			{
				return 0;
			}
			return this.EncodedName.GetHashCode();
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000982CD File Offset: 0x000964CD
		public override string ToString()
		{
			if (this.encoded == null && this.decoded == null)
			{
				return null;
			}
			if (this.encoded != null)
			{
				return this.encoded;
			}
			return this.decoded;
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000982F6 File Offset: 0x000964F6
		public static bool operator ==(XmlName a, XmlName b)
		{
			if (a == null)
			{
				return b == null;
			}
			return a.Equals(b);
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x00098307 File Offset: 0x00096507
		public static bool operator !=(XmlName a, XmlName b)
		{
			return !(a == b);
		}

		// Token: 0x0400224A RID: 8778
		private string decoded;

		// Token: 0x0400224B RID: 8779
		private string encoded;
	}
}
