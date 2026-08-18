using System;
using System.Globalization;
using System.IO;
using System.Runtime.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Diagnostics
{
	// Token: 0x020001E6 RID: 486
	internal class DigestTraceRecord : TraceRecord
	{
		// Token: 0x0600104E RID: 4174 RVA: 0x00046259 File Offset: 0x00044459
		internal DigestTraceRecord(string traceName, MemoryStream logStream, HashAlgorithm hash)
		{
			if (string.IsNullOrEmpty(traceName))
			{
				this._traceName = "Empty";
			}
			else
			{
				this._traceName = traceName;
			}
			this._logStream = logStream;
			this._hash = hash;
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0004628B File Offset: 0x0004448B
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/" + this._traceName + "TraceRecord";
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000462A4 File Offset: 0x000444A4
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			byte[] buffer = this._logStream.GetBuffer();
			string @string = Encoding.UTF8.GetString(buffer, 0, (int)this._logStream.Length);
			writer.WriteElementString("CanonicalElementStringLength", @string.Length.ToString(CultureInfo.InvariantCulture));
			writer.WriteComment("CanonicalElementString:" + @string);
			writer.WriteElementString("CanonicalOctetsLength", buffer.Length.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("CanonicalOctets", Convert.ToBase64String(buffer));
			writer.WriteElementString("CanonicalOctetsHashLength", this._hash.Hash.Length.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("CanonicalOctetsHash", Convert.ToBase64String(this._hash.Hash));
			if (this._hash is KeyedHashAlgorithm)
			{
				KeyedHashAlgorithm keyedHashAlgorithm = this._hash as KeyedHashAlgorithm;
				byte[] key = keyedHashAlgorithm.Key;
				writer.WriteStartElement("Key");
				writer.WriteElementString("Length", key.Length.ToString(CultureInfo.InvariantCulture));
				writer.WriteElementString("FirstByte", key[0].ToString(CultureInfo.InvariantCulture));
				writer.WriteElementString("LastByte", key[key.Length - 1].ToString(CultureInfo.InvariantCulture));
				writer.WriteEndElement();
			}
		}

		// Token: 0x04000E2F RID: 3631
		private MemoryStream _logStream;

		// Token: 0x04000E30 RID: 3632
		private HashAlgorithm _hash;

		// Token: 0x04000E31 RID: 3633
		private string _traceName;

		// Token: 0x04000E32 RID: 3634
		private const string Empty = "Empty";

		// Token: 0x04000E33 RID: 3635
		private const string CanonicalElementString = "CanonicalElementString";

		// Token: 0x04000E34 RID: 3636
		private const string CanonicalElementStringLength = "CanonicalElementStringLength";

		// Token: 0x04000E35 RID: 3637
		private const string CanonicalOctets = "CanonicalOctets";

		// Token: 0x04000E36 RID: 3638
		private const string CanonicalOctetsLength = "CanonicalOctetsLength";

		// Token: 0x04000E37 RID: 3639
		private const string CanonicalOctetsHash = "CanonicalOctetsHash";

		// Token: 0x04000E38 RID: 3640
		private const string CanonicalOctetsHashLength = "CanonicalOctetsHashLength";

		// Token: 0x04000E39 RID: 3641
		private const string Key = "Key";

		// Token: 0x04000E3A RID: 3642
		private const string Length = "Length";

		// Token: 0x04000E3B RID: 3643
		private const string FirstByte = "FirstByte";

		// Token: 0x04000E3C RID: 3644
		private const string LastByte = "LastByte";
	}
}
