using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Resources.Tools;
using System.Web.UI;
using System.Web.Util;
using System.Xml;
using System.Xml.Schema;

namespace System.Web.Compilation
{
	// Token: 0x020007FC RID: 2044
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Resources)]
	internal abstract class BaseResourcesBuildProvider : BuildProvider
	{
		// Token: 0x0600618F RID: 24975 RVA: 0x00151F69 File Offset: 0x00150169
		internal void DontGenerateStronglyTypedClass()
		{
			this._dontGenerateStronglyTypedClass = true;
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x00151F74 File Offset: 0x00150174
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			this._cultureName = base.GetCultureName();
			if (!this._dontGenerateStronglyTypedClass)
			{
				this._ns = Util.GetNamespaceAndTypeNameFromVirtualPath(base.VirtualPathObject, (this._cultureName == null) ? 1 : 2, out this._typeName);
				if (this._ns.Length == 0)
				{
					this._ns = "Resources";
				}
				else
				{
					this._ns = "Resources." + this._ns;
				}
			}
			using (Stream stream = base.OpenStream())
			{
				IResourceReader resourceReader = this.GetResourceReader(stream);
				try
				{
					this.GenerateResourceFile(assemblyBuilder, resourceReader);
				}
				catch (ArgumentException ex)
				{
					if (ex.InnerException != null && (ex.InnerException is XmlException || ex.InnerException is XmlSchemaException))
					{
						throw ex.InnerException;
					}
					throw;
				}
				if (this._cultureName == null && !this._dontGenerateStronglyTypedClass)
				{
					this.GenerateStronglyTypedClass(assemblyBuilder, resourceReader);
				}
			}
		}

		// Token: 0x06006191 RID: 24977
		protected abstract IResourceReader GetResourceReader(Stream inputStream);

		// Token: 0x06006192 RID: 24978 RVA: 0x00152070 File Offset: 0x00150270
		private void GenerateResourceFile(AssemblyBuilder assemblyBuilder, IResourceReader reader)
		{
			string text;
			if (this._ns == null)
			{
				text = UrlPath.GetFileNameWithoutExtension(base.VirtualPath) + ".resources";
			}
			else if (this._cultureName == null)
			{
				text = this._ns + "." + this._typeName + ".resources";
			}
			else
			{
				text = string.Concat(new string[]
				{
					this._ns,
					".",
					this._typeName,
					".",
					this._cultureName,
					".resources"
				});
			}
			text = text.ToLower(CultureInfo.InvariantCulture);
			Stream stream = null;
			try
			{
				try
				{
					try
					{
					}
					finally
					{
						stream = assemblyBuilder.CreateEmbeddedResource(this, text);
					}
				}
				catch (ArgumentException)
				{
					throw new HttpException(SR.GetString("Duplicate_Resource_File", new object[]
					{
						base.VirtualPath
					}));
				}
				using (stream)
				{
					using (ResourceWriter resourceWriter = new ResourceWriter(stream))
					{
						resourceWriter.TypeNameConverter = new Func<Type, string>(TargetFrameworkUtil.TypeNameConverter);
						foreach (object obj in reader)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							resourceWriter.AddResource((string)dictionaryEntry.Key, dictionaryEntry.Value);
						}
					}
				}
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x00152214 File Offset: 0x00150414
		private void GenerateStronglyTypedClass(AssemblyBuilder assemblyBuilder, IResourceReader reader)
		{
			IDictionary resourceList;
			try
			{
				resourceList = this.GetResourceList(reader);
			}
			finally
			{
				if (reader != null)
				{
					reader.Dispose();
				}
			}
			CodeDomProvider codeDomProvider = assemblyBuilder.CodeDomProvider;
			string[] array;
			CodeCompileUnit compileUnit = StronglyTypedResourceBuilder.Create(resourceList, this._typeName, this._ns, codeDomProvider, false, out array);
			assemblyBuilder.AddCodeCompileUnit(this, compileUnit);
		}

		// Token: 0x06006194 RID: 24980 RVA: 0x00152270 File Offset: 0x00150470
		private IDictionary GetResourceList(IResourceReader reader)
		{
			IDictionary dictionary = new Hashtable(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in reader)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				dictionary.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
			return dictionary;
		}

		// Token: 0x040032B9 RID: 12985
		internal const string DefaultResourcesNamespace = "Resources";

		// Token: 0x040032BA RID: 12986
		private string _ns;

		// Token: 0x040032BB RID: 12987
		private string _typeName;

		// Token: 0x040032BC RID: 12988
		private string _cultureName;

		// Token: 0x040032BD RID: 12989
		private bool _dontGenerateStronglyTypedClass;
	}
}
