using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200003E RID: 62
	public sealed class EnvelopedSignatureWriter : DelegatingXmlDictionaryWriter
	{
		// Token: 0x06000240 RID: 576 RVA: 0x00009A54 File Offset: 0x00007C54
		public EnvelopedSignatureWriter(XmlWriter innerWriter, SigningCredentials signingCredentials, string referenceId, SecurityTokenSerializer securityTokenSerializer)
		{
			if (innerWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerWriter");
			}
			if (signingCredentials == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signingCredentials");
			}
			if (string.IsNullOrEmpty(referenceId))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID0006"), "referenceId"));
			}
			if (securityTokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenSerializer");
			}
			this._dictionaryManager = new DictionaryManager();
			this._innerWriter = innerWriter;
			this._signingCreds = signingCredentials;
			this._referenceId = referenceId;
			this._tokenSerializer = securityTokenSerializer;
			this._signatureFragment = new MemoryStream();
			this._endFragment = new MemoryStream();
			this._writerStream = new MemoryStream();
			XmlDictionaryWriter innerWriter2 = XmlDictionaryWriter.CreateTextWriter(this._writerStream, Encoding.UTF8, false);
			base.InitializeInnerWriter(innerWriter2);
			this._hashAlgorithm = CryptoHelper.CreateHashAlgorithm(this._signingCreds.DigestAlgorithm);
			this._hashStream = new HashStream(this._hashAlgorithm);
			base.InnerWriter.StartCanonicalization(this._hashStream, false, null);
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				this._preCanonicalTracingStream = new MemoryStream();
				base.InitializeTracingWriter(new XmlTextWriter(this._preCanonicalTracingStream, Encoding.UTF8));
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009B8C File Offset: 0x00007D8C
		private void ComputeSignature()
		{
			PreDigestedSignedInfo preDigestedSignedInfo = new PreDigestedSignedInfo(this._dictionaryManager);
			preDigestedSignedInfo.AddEnvelopedSignatureTransform = true;
			preDigestedSignedInfo.CanonicalizationMethod = XD.ExclusiveC14NDictionary.Namespace.Value;
			preDigestedSignedInfo.SignatureMethod = this._signingCreds.SignatureAlgorithm;
			preDigestedSignedInfo.DigestMethod = this._signingCreds.DigestAlgorithm;
			preDigestedSignedInfo.AddReference(this._referenceId, this._hashStream.FlushHashAndGetValue(this._preCanonicalTracingStream));
			SignedXml signedXml = new SignedXml(preDigestedSignedInfo, this._dictionaryManager, this._tokenSerializer);
			signedXml.ComputeSignature(this._signingCreds.SigningKey);
			signedXml.Signature.KeyIdentifier = this._signingCreds.SigningKeyIdentifier;
			signedXml.WriteTo(base.InnerWriter);
			((IDisposable)this._hashStream).Dispose();
			this._hashStream = null;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009C58 File Offset: 0x00007E58
		private void OnEndRootElement()
		{
			if (!this._hasSignatureBeenMarkedForInsert)
			{
				((IFragmentCapableXmlDictionaryWriter)base.InnerWriter).StartFragment(this._endFragment, false);
				base.WriteEndElement();
				((IFragmentCapableXmlDictionaryWriter)base.InnerWriter).EndFragment();
			}
			else if (this._hasSignatureBeenMarkedForInsert)
			{
				base.WriteEndElement();
				((IFragmentCapableXmlDictionaryWriter)base.InnerWriter).EndFragment();
			}
			base.EndCanonicalization();
			((IFragmentCapableXmlDictionaryWriter)base.InnerWriter).StartFragment(this._signatureFragment, false);
			this.ComputeSignature();
			((IFragmentCapableXmlDictionaryWriter)base.InnerWriter).EndFragment();
			((IFragmentCapableXmlDictionaryWriter)base.InnerWriter).WriteFragment(this._signatureFragment.GetBuffer(), 0, (int)this._signatureFragment.Length);
			((IFragmentCapableXmlDictionaryWriter)base.InnerWriter).WriteFragment(this._endFragment.GetBuffer(), 0, (int)this._endFragment.Length);
			this._signatureFragment.Close();
			this._endFragment.Close();
			this._writerStream.Position = 0L;
			this._hasSignatureBeenMarkedForInsert = false;
			XmlReader xmlReader = XmlDictionaryReader.CreateTextReader(this._writerStream, XmlDictionaryReaderQuotas.Max);
			xmlReader.MoveToContent();
			this._innerWriter.WriteNode(xmlReader, false);
			this._innerWriter.Flush();
			xmlReader.Close();
			base.Close();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009DA4 File Offset: 0x00007FA4
		public void WriteSignature()
		{
			base.Flush();
			if (this._writerStream == null || this._writerStream.Length == 0L)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID6029")));
			}
			if (this._signatureFragment.Length != 0L)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID6030")));
			}
			((IFragmentCapableXmlDictionaryWriter)base.InnerWriter).StartFragment(this._endFragment, false);
			this._hasSignatureBeenMarkedForInsert = true;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009E2B File Offset: 0x0000802B
		public override void WriteEndElement()
		{
			this._elementCount--;
			if (this._elementCount == 0)
			{
				base.Flush();
				this.OnEndRootElement();
				return;
			}
			base.WriteEndElement();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009E56 File Offset: 0x00008056
		public override void WriteFullEndElement()
		{
			this._elementCount--;
			if (this._elementCount == 0)
			{
				base.Flush();
				this.OnEndRootElement();
				return;
			}
			base.WriteFullEndElement();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009E81 File Offset: 0x00008081
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this._elementCount++;
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009E9C File Offset: 0x0000809C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				if (this._hashStream != null)
				{
					this._hashStream.Dispose();
					this._hashStream = null;
				}
				if (this._hashAlgorithm != null)
				{
					((IDisposable)this._hashAlgorithm).Dispose();
					this._hashAlgorithm = null;
				}
				if (this._signatureFragment != null)
				{
					this._signatureFragment.Dispose();
					this._signatureFragment = null;
				}
				if (this._endFragment != null)
				{
					this._endFragment.Dispose();
					this._endFragment = null;
				}
				if (this._writerStream != null)
				{
					this._writerStream.Dispose();
					this._writerStream = null;
				}
				if (this._preCanonicalTracingStream != null)
				{
					this._preCanonicalTracingStream.Dispose();
					this._preCanonicalTracingStream = null;
				}
			}
			this._disposed = true;
		}

		// Token: 0x0400015B RID: 347
		private DictionaryManager _dictionaryManager;

		// Token: 0x0400015C RID: 348
		private XmlWriter _innerWriter;

		// Token: 0x0400015D RID: 349
		private SigningCredentials _signingCreds;

		// Token: 0x0400015E RID: 350
		private string _referenceId;

		// Token: 0x0400015F RID: 351
		private SecurityTokenSerializer _tokenSerializer;

		// Token: 0x04000160 RID: 352
		private HashStream _hashStream;

		// Token: 0x04000161 RID: 353
		private HashAlgorithm _hashAlgorithm;

		// Token: 0x04000162 RID: 354
		private int _elementCount;

		// Token: 0x04000163 RID: 355
		private MemoryStream _signatureFragment;

		// Token: 0x04000164 RID: 356
		private MemoryStream _endFragment;

		// Token: 0x04000165 RID: 357
		private bool _hasSignatureBeenMarkedForInsert;

		// Token: 0x04000166 RID: 358
		private MemoryStream _writerStream;

		// Token: 0x04000167 RID: 359
		private MemoryStream _preCanonicalTracingStream;

		// Token: 0x04000168 RID: 360
		private bool _disposed;
	}
}
