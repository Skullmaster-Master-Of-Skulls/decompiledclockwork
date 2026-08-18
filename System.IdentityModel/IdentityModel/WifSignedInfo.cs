using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000082 RID: 130
	internal sealed class WifSignedInfo : StandardSignedInfo, IDisposable
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00011105 File Offset: 0x0000F305
		public WifSignedInfo(DictionaryManager dictionaryManager) : base(dictionaryManager)
		{
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001111C File Offset: 0x0000F31C
		~WifSignedInfo()
		{
			this.Dispose(false);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001114C File Offset: 0x0000F34C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001115B File Offset: 0x0000F35B
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing && this._bufferedStream != null)
			{
				this._bufferedStream.Close();
				this._bufferedStream = null;
			}
			this._disposed = true;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001118C File Offset: 0x0000F38C
		protected override void ComputeHash(HashStream hashStream)
		{
			if (base.SendSide)
			{
				using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(Stream.Null, Encoding.UTF8, false))
				{
					xmlDictionaryWriter.StartCanonicalization(hashStream, false, null);
					this.WriteTo(xmlDictionaryWriter, base.DictionaryManager);
					xmlDictionaryWriter.EndCanonicalization();
					return;
				}
			}
			if (base.CanonicalStream != null)
			{
				base.CanonicalStream.WriteTo(hashStream);
				return;
			}
			this._bufferedStream.Position = 0L;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(this._bufferedStream, XmlDictionaryReaderQuotas.Max))
			{
				xmlDictionaryReader.MoveToContent();
				using (XmlDictionaryWriter xmlDictionaryWriter2 = XmlDictionaryWriter.CreateTextWriter(Stream.Null, Encoding.UTF8, false))
				{
					xmlDictionaryWriter2.WriteStartElement("a", this._defaultNamespace);
					string[] inclusivePrefixes = base.GetInclusivePrefixes();
					for (int i = 0; i < inclusivePrefixes.Length; i++)
					{
						string namespaceForInclusivePrefix = this.GetNamespaceForInclusivePrefix(inclusivePrefixes[i]);
						if (namespaceForInclusivePrefix != null)
						{
							xmlDictionaryWriter2.WriteXmlnsAttribute(inclusivePrefixes[i], namespaceForInclusivePrefix);
						}
					}
					xmlDictionaryWriter2.StartCanonicalization(hashStream, false, inclusivePrefixes);
					xmlDictionaryWriter2.WriteNode(xmlDictionaryReader, false);
					xmlDictionaryWriter2.EndCanonicalization();
					xmlDictionaryWriter2.WriteEndElement();
				}
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000112CC File Offset: 0x0000F4CC
		public override void ReadFrom(XmlDictionaryReader reader, TransformFactory transformFactory, DictionaryManager dictionaryManager)
		{
			reader.MoveToStartElement("SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
			base.SendSide = false;
			this._defaultNamespace = reader.LookupNamespace(string.Empty);
			this._bufferedStream = new MemoryStream();
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.Encoding = Encoding.UTF8;
			xmlWriterSettings.NewLineHandling = NewLineHandling.None;
			using (XmlWriter xmlWriter = XmlWriter.Create(this._bufferedStream, xmlWriterSettings))
			{
				xmlWriter.WriteNode(reader, true);
				xmlWriter.Flush();
			}
			this._bufferedStream.Position = 0L;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(this._bufferedStream, XmlDictionaryReaderQuotas.Max))
			{
				base.CanonicalStream = new MemoryStream();
				xmlDictionaryReader.StartCanonicalization(base.CanonicalStream, false, null);
				xmlDictionaryReader.MoveToStartElement("SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
				base.Prefix = xmlDictionaryReader.Prefix;
				base.Id = xmlDictionaryReader.GetAttribute("Id", null);
				xmlDictionaryReader.Read();
				base.ReadCanonicalizationMethod(xmlDictionaryReader, base.DictionaryManager);
				base.ReadSignatureMethod(xmlDictionaryReader, base.DictionaryManager);
				while (xmlDictionaryReader.IsStartElement("Reference", "http://www.w3.org/2000/09/xmldsig#"))
				{
					Reference reference = new Reference(base.DictionaryManager);
					reference.ReadFrom(xmlDictionaryReader, transformFactory, base.DictionaryManager);
					base.AddReference(reference);
				}
				xmlDictionaryReader.ReadEndElement();
				xmlDictionaryReader.EndCanonicalization();
			}
			string[] inclusivePrefixes = base.GetInclusivePrefixes();
			if (inclusivePrefixes != null)
			{
				base.CanonicalStream = null;
				base.Context = new Dictionary<string, string>(inclusivePrefixes.Length);
				for (int i = 0; i < inclusivePrefixes.Length; i++)
				{
					base.Context.Add(inclusivePrefixes[i], reader.LookupNamespace(inclusivePrefixes[i]));
				}
			}
		}

		// Token: 0x040003B4 RID: 948
		private MemoryStream _bufferedStream;

		// Token: 0x040003B5 RID: 949
		private string _defaultNamespace = string.Empty;

		// Token: 0x040003B6 RID: 950
		private bool _disposed;
	}
}
