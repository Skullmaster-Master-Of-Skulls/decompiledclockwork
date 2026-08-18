using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200081F RID: 2079
	internal class BuildResultCodeCompileUnit : BuildResult
	{
		// Token: 0x0600636A RID: 25450 RVA: 0x0015BC54 File Offset: 0x00159E54
		internal BuildResultCodeCompileUnit()
		{
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x0015C4BC File Offset: 0x0015A6BC
		internal BuildResultCodeCompileUnit(Type codeDomProviderType, CodeCompileUnit codeCompileUnit, CompilerParameters compilerParameters, IDictionary linePragmasTable)
		{
			this._codeDomProviderType = codeDomProviderType;
			this._codeCompileUnit = codeCompileUnit;
			this._compilerParameters = compilerParameters;
			this._linePragmasTable = linePragmasTable;
		}

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x0600636C RID: 25452 RVA: 0x0015C4E1 File Offset: 0x0015A6E1
		internal Type CodeDomProviderType
		{
			get
			{
				return this._codeDomProviderType;
			}
		}

		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x0600636D RID: 25453 RVA: 0x0015C4E9 File Offset: 0x0015A6E9
		internal CodeCompileUnit CodeCompileUnit
		{
			get
			{
				return this._codeCompileUnit;
			}
		}

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x0600636E RID: 25454 RVA: 0x0015C4F1 File Offset: 0x0015A6F1
		internal CompilerParameters CompilerParameters
		{
			get
			{
				return this._compilerParameters;
			}
		}

		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x0600636F RID: 25455 RVA: 0x0015C4F9 File Offset: 0x0015A6F9
		internal IDictionary LinePragmasTable
		{
			get
			{
				return this._linePragmasTable;
			}
		}

		// Token: 0x17001C2C RID: 7212
		// (get) Token: 0x06006370 RID: 25456 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool CacheToDisk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x0015C501 File Offset: 0x0015A701
		internal override BuildResultTypeCode GetCode()
		{
			return BuildResultTypeCode.BuildResultCodeCompileUnit;
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x0015C504 File Offset: 0x0015A704
		private string GetPreservationFileName()
		{
			return this._cacheKey + ".ccu";
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x0015C518 File Offset: 0x0015A718
		protected override void ComputeHashCode(HashCodeCombiner hashCodeCombiner)
		{
			base.ComputeHashCode(hashCodeCombiner);
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(base.VirtualPath);
			hashCodeCombiner.AddObject(compilationConfig.RecompilationHash);
			PagesSection pagesConfig = MTConfigUtil.GetPagesConfig(base.VirtualPath);
			hashCodeCombiner.AddObject(Util.GetRecompilationHash(pagesConfig));
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x0015C55C File Offset: 0x0015A75C
		internal override void GetPreservedAttributes(PreservationFileReader pfr)
		{
			base.GetPreservedAttributes(pfr);
			string text = pfr.GetAttribute("CCUpreservationFileName");
			text = Path.Combine(HttpRuntime.CodegenDirInternal, text);
			using (FileStream fileStream = File.Open(text, FileMode.Open))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				this._codeCompileUnit = (binaryFormatter.Deserialize(fileStream) as CodeCompileUnit);
				this._codeDomProviderType = (Type)binaryFormatter.Deserialize(fileStream);
				this._compilerParameters = (CompilerParameters)binaryFormatter.Deserialize(fileStream);
				this._linePragmasTable = (binaryFormatter.Deserialize(fileStream) as IDictionary);
			}
		}

		// Token: 0x06006375 RID: 25461 RVA: 0x0015C5FC File Offset: 0x0015A7FC
		internal void SetCacheKey(string cacheKey)
		{
			this._cacheKey = cacheKey;
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x0015C608 File Offset: 0x0015A808
		internal override void SetPreservedAttributes(PreservationFileWriter pfw)
		{
			base.SetPreservedAttributes(pfw);
			string text = this.GetPreservationFileName();
			pfw.SetAttribute("CCUpreservationFileName", text);
			text = Path.Combine(HttpRuntime.CodegenDirInternal, text);
			using (FileStream fileStream = File.Open(text, FileMode.Create))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (this._codeCompileUnit != null)
				{
					binaryFormatter.Serialize(fileStream, this._codeCompileUnit);
				}
				else
				{
					binaryFormatter.Serialize(fileStream, new object());
				}
				binaryFormatter.Serialize(fileStream, this._codeDomProviderType);
				binaryFormatter.Serialize(fileStream, this._compilerParameters);
				if (this._linePragmasTable != null)
				{
					binaryFormatter.Serialize(fileStream, this._linePragmasTable);
				}
				else
				{
					binaryFormatter.Serialize(fileStream, new object());
				}
			}
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x0015C6C8 File Offset: 0x0015A8C8
		internal override void RemoveOutOfDateResources(PreservationFileReader pfr)
		{
			string text = pfr.GetAttribute("CCUpreservationFileName");
			text = Path.Combine(HttpRuntime.CodegenDirInternal, text);
			File.Delete(text);
		}

		// Token: 0x04003385 RID: 13189
		private Type _codeDomProviderType;

		// Token: 0x04003386 RID: 13190
		private CodeCompileUnit _codeCompileUnit;

		// Token: 0x04003387 RID: 13191
		private CompilerParameters _compilerParameters;

		// Token: 0x04003388 RID: 13192
		private IDictionary _linePragmasTable;

		// Token: 0x04003389 RID: 13193
		private string _cacheKey;

		// Token: 0x0400338A RID: 13194
		private const string fileNameAttribute = "CCUpreservationFileName";
	}
}
