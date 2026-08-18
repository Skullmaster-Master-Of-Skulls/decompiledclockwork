using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200002A RID: 42
	internal sealed class CanonicalizationDriver
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005F63 File Offset: 0x00004163
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00005F6B File Offset: 0x0000416B
		public bool CloseReadersAfterProcessing
		{
			get
			{
				return this.closeReadersAfterProcessing;
			}
			set
			{
				this.closeReadersAfterProcessing = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005F74 File Offset: 0x00004174
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00005F7C File Offset: 0x0000417C
		public bool IncludeComments
		{
			get
			{
				return this.includeComments;
			}
			set
			{
				this.includeComments = value;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005F85 File Offset: 0x00004185
		public string[] GetInclusivePrefixes()
		{
			return this.inclusivePrefixes;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005F8D File Offset: 0x0000418D
		public void Reset()
		{
			this.reader = null;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005F96 File Offset: 0x00004196
		public void SetInclusivePrefixes(string[] inclusivePrefixes)
		{
			this.inclusivePrefixes = inclusivePrefixes;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005F9F File Offset: 0x0000419F
		public void SetInput(Stream stream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			this.reader = XmlReader.Create(stream);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005FC0 File Offset: 0x000041C0
		public void SetInput(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			this.reader = reader;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005FDC File Offset: 0x000041DC
		public byte[] GetBytes()
		{
			return this.GetMemoryStream().ToArray();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005FEC File Offset: 0x000041EC
		public MemoryStream GetMemoryStream()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.WriteTo(memoryStream);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return memoryStream;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006011 File Offset: 0x00004211
		public void WriteTo(HashAlgorithm hashAlgorithm)
		{
			this.WriteTo(new HashStream(hashAlgorithm));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006020 File Offset: 0x00004220
		public void WriteTo(Stream canonicalStream)
		{
			if (this.reader != null)
			{
				XmlDictionaryReader xmlDictionaryReader = this.reader as XmlDictionaryReader;
				if (xmlDictionaryReader != null && xmlDictionaryReader.CanCanonicalize)
				{
					xmlDictionaryReader.MoveToContent();
					xmlDictionaryReader.StartCanonicalization(canonicalStream, this.includeComments, this.inclusivePrefixes);
					xmlDictionaryReader.Skip();
					xmlDictionaryReader.EndCanonicalization();
				}
				else
				{
					XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(Stream.Null);
					if (this.inclusivePrefixes != null)
					{
						xmlDictionaryWriter.WriteStartElement("a", this.reader.LookupNamespace(string.Empty));
						for (int i = 0; i < this.inclusivePrefixes.Length; i++)
						{
							string text = this.reader.LookupNamespace(this.inclusivePrefixes[i]);
							if (text != null)
							{
								xmlDictionaryWriter.WriteXmlnsAttribute(this.inclusivePrefixes[i], text);
							}
						}
					}
					xmlDictionaryWriter.StartCanonicalization(canonicalStream, this.includeComments, this.inclusivePrefixes);
					if (this.reader is WrappedReader)
					{
						((WrappedReader)this.reader).XmlTokens.GetWriter().WriteTo(xmlDictionaryWriter, new DictionaryManager());
					}
					else
					{
						xmlDictionaryWriter.WriteNode(this.reader, false);
					}
					xmlDictionaryWriter.Flush();
					xmlDictionaryWriter.EndCanonicalization();
					if (this.inclusivePrefixes != null)
					{
						xmlDictionaryWriter.WriteEndElement();
					}
					xmlDictionaryWriter.Close();
				}
				if (this.closeReadersAfterProcessing)
				{
					this.reader.Close();
				}
				this.reader = null;
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoInputIsSetForCanonicalization")));
		}

		// Token: 0x040000E7 RID: 231
		private bool closeReadersAfterProcessing;

		// Token: 0x040000E8 RID: 232
		private XmlReader reader;

		// Token: 0x040000E9 RID: 233
		private string[] inclusivePrefixes;

		// Token: 0x040000EA RID: 234
		private bool includeComments;
	}
}
