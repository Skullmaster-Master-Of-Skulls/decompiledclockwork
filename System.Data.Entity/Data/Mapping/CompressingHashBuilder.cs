using System;
using System.Globalization;
using System.Security.Cryptography;

namespace System.Data.Mapping
{
	// Token: 0x02000255 RID: 597
	internal class CompressingHashBuilder : StringHashBuilder
	{
		// Token: 0x0600256E RID: 9582 RVA: 0x0008B9E9 File Offset: 0x00089BE9
		internal CompressingHashBuilder(HashAlgorithm hashAlgorithm) : base(hashAlgorithm, 6144)
		{
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x0008B9F7 File Offset: 0x00089BF7
		internal override void Append(string content)
		{
			base.Append(string.Empty.PadLeft(4 * this._indent, ' '));
			base.Append(content);
			this.CompressHash();
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x0008BA20 File Offset: 0x00089C20
		internal override void AppendLine(string content)
		{
			base.Append(string.Empty.PadLeft(4 * this._indent, ' '));
			base.AppendLine(content);
			this.CompressHash();
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x0008BA4C File Offset: 0x00089C4C
		internal void AppendObjectStartDump(object o, int objectIndex)
		{
			base.Append(string.Empty.PadLeft(4 * this._indent, ' '));
			base.Append(o.GetType().ToString());
			base.Append(" Instance#");
			base.AppendLine(objectIndex.ToString(CultureInfo.InvariantCulture));
			this.CompressHash();
			this._indent++;
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x0008BAB5 File Offset: 0x00089CB5
		internal void AppendObjectEndDump()
		{
			this._indent--;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x0008BAC8 File Offset: 0x00089CC8
		private void CompressHash()
		{
			if (base.CharCount >= 2048)
			{
				string s = base.ComputeHash();
				base.Clear();
				base.Append(s);
			}
		}

		// Token: 0x0400111F RID: 4383
		private const int HashCharacterCompressionThreshold = 2048;

		// Token: 0x04001120 RID: 4384
		private const int SpacesPerIndent = 4;

		// Token: 0x04001121 RID: 4385
		private int _indent;
	}
}
