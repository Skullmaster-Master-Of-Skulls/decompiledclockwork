using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Web.Compilation
{
	// Token: 0x02000856 RID: 2134
	internal class PreservationFileWriter
	{
		// Token: 0x06006523 RID: 25891 RVA: 0x00163B4C File Offset: 0x00161D4C
		internal PreservationFileWriter(bool precompilationMode)
		{
			this._precompilationMode = precompilationMode;
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x00163B5C File Offset: 0x00161D5C
		internal void SaveBuildResultToFile(string preservationFile, BuildResult result, long hashCode)
		{
			this._writer = new XmlTextWriter(preservationFile, Encoding.UTF8);
			try
			{
				this._writer.Formatting = Formatting.Indented;
				this._writer.Indentation = 4;
				this._writer.WriteStartDocument();
				this._writer.WriteStartElement("preserve");
				this.SetAttribute("resultType", ((int)result.GetCode()).ToString(CultureInfo.InvariantCulture));
				if (result.VirtualPath != null)
				{
					this.SetAttribute("virtualPath", result.VirtualPath.VirtualPathString);
				}
				this.SetAttribute("hash", result.ComputeHashCode(hashCode).ToString("x", CultureInfo.InvariantCulture));
				string virtualPathDependenciesHash = result.VirtualPathDependenciesHash;
				if (virtualPathDependenciesHash != null)
				{
					this.SetAttribute("filehash", virtualPathDependenciesHash);
				}
				result.SetPreservedAttributes(this);
				this.SaveDependencies(result.VirtualPathDependencies);
				this._writer.WriteEndElement();
				this._writer.WriteEndDocument();
				this._writer.Close();
			}
			catch
			{
				this._writer.Close();
				File.Delete(preservationFile);
				throw;
			}
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x00163C84 File Offset: 0x00161E84
		private void SaveDependencies(ICollection dependencies)
		{
			if (dependencies != null)
			{
				this._writer.WriteStartElement("filedeps");
				foreach (object obj in dependencies)
				{
					string value = (string)obj;
					this._writer.WriteStartElement("filedep");
					this._writer.WriteAttributeString("name", value);
					this._writer.WriteEndElement();
				}
				this._writer.WriteEndElement();
			}
		}

		// Token: 0x06006526 RID: 25894 RVA: 0x00163D1C File Offset: 0x00161F1C
		internal void SetAttribute(string name, string value)
		{
			this._writer.WriteAttributeString(name, value);
		}

		// Token: 0x0400342B RID: 13355
		private XmlTextWriter _writer;

		// Token: 0x0400342C RID: 13356
		private bool _precompilationMode;

		// Token: 0x0400342D RID: 13357
		internal const string fileDependenciesTagName = "filedeps";

		// Token: 0x0400342E RID: 13358
		internal const string fileDependencyTagName = "filedep";

		// Token: 0x0400342F RID: 13359
		internal const string buildResultDependenciesTagName = "builddeps";

		// Token: 0x04003430 RID: 13360
		internal const string buildResultDependencyTagName = "builddep";
	}
}
