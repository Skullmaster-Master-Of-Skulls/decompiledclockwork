using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200002B RID: 43
	internal class CipherDataElement
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006184 File Offset: 0x00004384
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000061F0 File Offset: 0x000043F0
		public byte[] CipherValue
		{
			get
			{
				if (this._iv != null)
				{
					byte[] dst = new byte[this._iv.Length + this._cipherText.Length];
					Buffer.BlockCopy(this._iv, 0, dst, 0, this._iv.Length);
					Buffer.BlockCopy(this._cipherText, 0, dst, this._iv.Length, this._cipherText.Length);
					this._iv = null;
				}
				return this._cipherText;
			}
			set
			{
				this._cipherText = value;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000061FC File Offset: 0x000043FC
		public void ReadXml(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.MoveToContent();
			if (!reader.IsStartElement("CipherData", "http://www.w3.org/2001/04/xmlenc#"))
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4188"));
			}
			reader.ReadStartElement("CipherData", "http://www.w3.org/2001/04/xmlenc#");
			reader.ReadStartElement("CipherValue", "http://www.w3.org/2001/04/xmlenc#");
			this._cipherText = reader.ReadContentAsBase64();
			this._iv = null;
			reader.MoveToContent();
			reader.ReadEndElement();
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006293 File Offset: 0x00004493
		public void SetCipherValueFragments(byte[] iv, byte[] cipherText)
		{
			this._iv = iv;
			this._cipherText = cipherText;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000062A4 File Offset: 0x000044A4
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("xenc", "CipherData", "http://www.w3.org/2001/04/xmlenc#");
			writer.WriteStartElement("xenc", "CipherValue", "http://www.w3.org/2001/04/xmlenc#");
			if (this._iv != null)
			{
				writer.WriteBase64(this._iv, 0, this._iv.Length);
			}
			writer.WriteBase64(this._cipherText, 0, this._cipherText.Length);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x040000EB RID: 235
		private byte[] _iv;

		// Token: 0x040000EC RID: 236
		private byte[] _cipherText;
	}
}
