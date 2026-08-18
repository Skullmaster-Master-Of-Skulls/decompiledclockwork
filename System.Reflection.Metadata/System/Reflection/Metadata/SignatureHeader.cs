using System;
using System.Text;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AB RID: 171
	public struct SignatureHeader : IEquatable<SignatureHeader>
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x0000FF70 File Offset: 0x0000E170
		public SignatureHeader(byte rawValue)
		{
			this._rawValue = rawValue;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000FF79 File Offset: 0x0000E179
		public SignatureHeader(SignatureKind kind, SignatureCallingConvention convention, SignatureAttributes attributes)
		{
			this = new SignatureHeader((byte)(kind | (SignatureKind)convention | (SignatureKind)attributes));
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0000FF87 File Offset: 0x0000E187
		public byte RawValue
		{
			get
			{
				return this._rawValue;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0000FF90 File Offset: 0x0000E190
		public SignatureCallingConvention CallingConvention
		{
			get
			{
				int num = (int)(this._rawValue & 15);
				if (num > 5)
				{
					return SignatureCallingConvention.Default;
				}
				return (SignatureCallingConvention)num;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		public SignatureKind Kind
		{
			get
			{
				int num = (int)(this._rawValue & 15);
				if (num <= 5)
				{
					return SignatureKind.Method;
				}
				return (SignatureKind)num;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0000FFCF File Offset: 0x0000E1CF
		public SignatureAttributes Attributes
		{
			get
			{
				return (SignatureAttributes)((int)this._rawValue & -16);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0000FFDB File Offset: 0x0000E1DB
		public bool HasExplicitThis
		{
			get
			{
				return (this._rawValue & 64) > 0;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0000FFE9 File Offset: 0x0000E1E9
		public bool IsInstance
		{
			get
			{
				return (this._rawValue & 32) > 0;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0000FFF7 File Offset: 0x0000E1F7
		public bool IsGeneric
		{
			get
			{
				return (this._rawValue & 16) > 0;
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00010005 File Offset: 0x0000E205
		public override bool Equals(object obj)
		{
			return obj is SignatureHeader && this.Equals((SignatureHeader)obj);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001001D File Offset: 0x0000E21D
		public bool Equals(SignatureHeader other)
		{
			return this._rawValue == other._rawValue;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000FF87 File Offset: 0x0000E187
		public override int GetHashCode()
		{
			return (int)this._rawValue;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001001D File Offset: 0x0000E21D
		public static bool operator ==(SignatureHeader left, SignatureHeader right)
		{
			return left._rawValue == right._rawValue;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001002D File Offset: 0x0000E22D
		public static bool operator !=(SignatureHeader left, SignatureHeader right)
		{
			return left._rawValue != right._rawValue;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00010040 File Offset: 0x0000E240
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Kind.ToString());
			if (this.Kind == SignatureKind.Method)
			{
				stringBuilder.Append(',');
				stringBuilder.Append(this.CallingConvention.ToString());
			}
			if (this.Attributes != SignatureAttributes.None)
			{
				stringBuilder.Append(',');
				stringBuilder.Append(this.Attributes.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000442 RID: 1090
		private byte _rawValue;

		// Token: 0x04000443 RID: 1091
		public const byte CallingConventionOrKindMask = 15;

		// Token: 0x04000444 RID: 1092
		private const byte maxCallingConvention = 5;
	}
}
