using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Internal
{
	// Token: 0x020006C7 RID: 1735
	internal class ModelHashCalculator
	{
		// Token: 0x060044DD RID: 17629 RVA: 0x00144F04 File Offset: 0x00143104
		public virtual string Calculate(DbCompiledModel compiledModel)
		{
			DbProviderInfo providerInfo = compiledModel.ProviderInfo;
			DbModelBuilder dbModelBuilder = compiledModel.CachedModelBuilder.Clone();
			EdmMetadataContext.ConfigureEdmMetadata(dbModelBuilder.ModelConfiguration);
			EdmModel database = dbModelBuilder.Build(providerInfo).DatabaseMapping.Database;
			database.SchemaVersion = 2.0;
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				Indent = true
			}))
			{
				new SsdlSerializer().Serialize(database, providerInfo.ProviderInvariantName, providerInfo.ProviderManifestToken, xmlWriter, true);
			}
			return ModelHashCalculator.ComputeSha256Hash(stringBuilder.ToString());
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x00144FB4 File Offset: 0x001431B4
		private static string ComputeSha256Hash(string input)
		{
			byte[] array = ModelHashCalculator.GetSha256HashAlgorithm().ComputeHash(Encoding.ASCII.GetBytes(input));
			StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x0014501C File Offset: 0x0014321C
		private static SHA256 GetSha256HashAlgorithm()
		{
			SHA256 result;
			try
			{
				result = new SHA256CryptoServiceProvider();
			}
			catch (PlatformNotSupportedException)
			{
				result = new SHA256Managed();
			}
			return result;
		}
	}
}
