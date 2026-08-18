using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Resources;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000019 RID: 25
	internal abstract class MapFileLoader
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00003D04 File Offset: 0x00001F04
		public void SaveMapFile(MapFile mapFile)
		{
			this.SaveExternalFiles(mapFile);
			using (TextWriter mapFileWriter = this.GetMapFileWriter())
			{
				this.GetMapFileSerializer().Serialize(mapFileWriter, this.Unwrap(mapFile));
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003D50 File Offset: 0x00001F50
		public MapFile LoadMapFile()
		{
			MapFile mapFile = null;
			using (TextReader mapFileReader = this.GetMapFileReader())
			{
				List<ProxyGenerationError> proxyGenerationErrors = new List<ProxyGenerationError>();
				ValidationEventHandler value = delegate(object sender, ValidationEventArgs e)
				{
					bool flag = e.Severity == XmlSeverityType.Error;
					proxyGenerationErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.LoadMetadata, this.MapFileName, e.Exception, !flag));
					if (flag)
					{
						throw e.Exception;
					}
				};
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
				{
					Schemas = this.GetMapFileSchemaSet(),
					ValidationType = ValidationType.Schema,
					ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
				};
				using (XmlReader xmlReader = XmlReader.Create(mapFileReader, xmlReaderSettings, string.Empty))
				{
					try
					{
						xmlReaderSettings.ValidationEventHandler += value;
						mapFile = this.ReadMapFile(xmlReader);
						this.SetMapFileLoadErrors(mapFile, proxyGenerationErrors);
					}
					finally
					{
						xmlReaderSettings.ValidationEventHandler -= value;
					}
				}
			}
			if (mapFile != null)
			{
				this.LoadExternalFiles(mapFile);
			}
			return mapFile;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003E30 File Offset: 0x00002030
		public void LoadMetadataFile(MetadataFile metadataFile)
		{
			try
			{
				metadataFile.CleanUpContent();
				metadataFile.LoadContent(this.ReadMetadataFile(metadataFile.FileName));
			}
			catch (Exception errorInLoading)
			{
				metadataFile.ErrorInLoading = errorInLoading;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003E74 File Offset: 0x00002074
		public void LoadExtensionFile(ExtensionFile extensionFile)
		{
			try
			{
				extensionFile.CleanUpContent();
				extensionFile.ContentBuffer = this.ReadExtensionFile(extensionFile.FileName);
			}
			catch (Exception errorInLoading)
			{
				extensionFile.ErrorInLoading = errorInLoading;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E5 RID: 229
		protected abstract string MapFileName { get; }

		// Token: 0x060000E6 RID: 230
		protected abstract MapFile Wrap(object mapFileImpl);

		// Token: 0x060000E7 RID: 231
		protected abstract object Unwrap(MapFile mapFile);

		// Token: 0x060000E8 RID: 232
		protected abstract XmlSchemaSet GetMapFileSchemaSet();

		// Token: 0x060000E9 RID: 233
		protected abstract XmlSerializer GetMapFileSerializer();

		// Token: 0x060000EA RID: 234 RVA: 0x00002058 File Offset: 0x00000258
		protected virtual TextReader GetMapFileReader()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002058 File Offset: 0x00000258
		protected virtual TextWriter GetMapFileWriter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002058 File Offset: 0x00000258
		protected virtual byte[] ReadMetadataFile(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002058 File Offset: 0x00000258
		protected virtual void WriteMetadataFile(MetadataFile file)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002058 File Offset: 0x00000258
		protected virtual byte[] ReadExtensionFile(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002058 File Offset: 0x00000258
		protected virtual void WriteExtensionFile(ExtensionFile file)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003EB8 File Offset: 0x000020B8
		private MapFile ReadMapFile(XmlReader reader)
		{
			MapFile result;
			try
			{
				result = this.Wrap(this.GetMapFileSerializer().Deserialize(reader));
			}
			catch (InvalidOperationException ex)
			{
				XmlException ex2 = ex.InnerException as XmlException;
				if (ex2 != null)
				{
					throw ex2;
				}
				XmlSchemaException ex3 = ex.InnerException as XmlSchemaException;
				if (ex3 == null)
				{
					throw;
				}
				if (ex3.LineNumber > 0)
				{
					throw new XmlSchemaException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_AppendLinePosition, new object[]
					{
						ex3.Message,
						ex3.LineNumber,
						ex3.LinePosition
					}), ex3, ex3.LineNumber, ex3.LinePosition);
				}
				throw ex3;
			}
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003F68 File Offset: 0x00002168
		private void SaveExternalFiles(MapFile mapFile)
		{
			foreach (MetadataFile metadataFile in mapFile.MetadataList)
			{
				if (metadataFile.ErrorInLoading == null)
				{
					this.WriteMetadataFile(metadataFile);
				}
			}
			foreach (ExtensionFile extensionFile in mapFile.Extensions)
			{
				if (extensionFile.ErrorInLoading == null)
				{
					this.WriteExtensionFile(extensionFile);
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004010 File Offset: 0x00002210
		private void LoadExternalFiles(MapFile mapFile)
		{
			this.ValidateMapFile(mapFile);
			foreach (MetadataFile metadataFile in mapFile.MetadataList)
			{
				metadataFile.IsExistingFile = true;
				this.LoadMetadataFile(metadataFile);
			}
			foreach (ExtensionFile extensionFile in mapFile.Extensions)
			{
				extensionFile.IsExistingFile = true;
				this.LoadExtensionFile(extensionFile);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000040BC File Offset: 0x000022BC
		private void ValidateMapFile(MapFile mapFile)
		{
			IEnumerable<string> first = from p in mapFile.MetadataList
			select p.FileName into p
			where !string.IsNullOrEmpty(p)
			select p;
			IEnumerable<string> second = from p in mapFile.Extensions
			select p.FileName into p
			where !string.IsNullOrEmpty(p)
			select p;
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in first.Concat(second))
			{
				if (hashSet.Contains(text))
				{
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_TwoExternalFilesWithSameName, new object[]
					{
						text
					}));
				}
				hashSet.Add(text);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000041E4 File Offset: 0x000023E4
		private void SetMapFileLoadErrors(MapFile mapFile, IEnumerable<ProxyGenerationError> proxyGenerationErrors)
		{
			mapFile.LoadErrors = proxyGenerationErrors;
		}
	}
}
