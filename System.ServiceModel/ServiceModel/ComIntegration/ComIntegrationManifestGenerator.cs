using System;
using System.IO;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001E1 RID: 481
	internal class ComIntegrationManifestGenerator : MarshalByRefObject
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x000371C8 File Offset: 0x000353C8
		internal static void GenerateManifestCollectionFile(Guid[] manifests, string strAssemblyManifestFileName, string assemblyName)
		{
			string str = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";
			string str2 = "<assembly xmlns=\"urn:schemas-microsoft-com:asm.v1\" manifestVersion=\"1.0\">";
			string value = "</assembly>";
			string directoryName = Path.GetDirectoryName(strAssemblyManifestFileName);
			if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.DirectoryNotFound(directoryName));
			}
			Stream stream = null;
			try
			{
				stream = File.Create(strAssemblyManifestFileName);
				ComIntegrationManifestGenerator.WriteUTFChars(stream, str + Environment.NewLine);
				ComIntegrationManifestGenerator.WriteUTFChars(stream, str2 + Environment.NewLine);
				ComIntegrationManifestGenerator.WriteUTFChars(stream, "<assemblyIdentity" + Environment.NewLine, 4);
				ComIntegrationManifestGenerator.WriteUTFChars(stream, "name=\"" + assemblyName + "\"" + Environment.NewLine, 8);
				ComIntegrationManifestGenerator.WriteUTFChars(stream, "version=\"1.0.0.0\"/>" + Environment.NewLine, 8);
				for (int i = 0; i < manifests.Length; i++)
				{
					ComIntegrationManifestGenerator.WriteUTFChars(stream, "<dependency>" + Environment.NewLine, 4);
					ComIntegrationManifestGenerator.WriteUTFChars(stream, "<dependentAssembly>" + Environment.NewLine, 8);
					ComIntegrationManifestGenerator.WriteUTFChars(stream, "<assemblyIdentity" + Environment.NewLine, 12);
					ComIntegrationManifestGenerator.WriteUTFChars(stream, "name=\"" + manifests[i].ToString() + "\"" + Environment.NewLine, 16);
					ComIntegrationManifestGenerator.WriteUTFChars(stream, "version=\"1.0.0.0\"/>" + Environment.NewLine, 16);
					ComIntegrationManifestGenerator.WriteUTFChars(stream, "</dependentAssembly>" + Environment.NewLine, 8);
					ComIntegrationManifestGenerator.WriteUTFChars(stream, "</dependency>" + Environment.NewLine, 4);
				}
				ComIntegrationManifestGenerator.WriteUTFChars(stream, value);
			}
			catch (Exception ex)
			{
				if (ex is NullReferenceException || ex is SEHException)
				{
					throw;
				}
				stream.Close();
				File.Delete(strAssemblyManifestFileName);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ManifestCreationFailed(strAssemblyManifestFileName, ex.Message));
			}
			stream.Close();
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000373C8 File Offset: 0x000355C8
		internal static void GenerateWin32ManifestFile(Type[] aTypes, string strAssemblyManifestFileName, string assemblyName)
		{
			string str = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";
			string str2 = "<assembly xmlns=\"urn:schemas-microsoft-com:asm.v1\" manifestVersion=\"1.0\">";
			string directoryName = Path.GetDirectoryName(strAssemblyManifestFileName);
			if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.DirectoryNotFound(directoryName));
			}
			Stream stream = null;
			try
			{
				stream = File.Create(strAssemblyManifestFileName);
				ComIntegrationManifestGenerator.WriteUTFChars(stream, str + Environment.NewLine);
				ComIntegrationManifestGenerator.WriteUTFChars(stream, str2 + Environment.NewLine);
				ComIntegrationManifestGenerator.WriteUTFChars(stream, "<assemblyIdentity" + Environment.NewLine, 4);
				ComIntegrationManifestGenerator.WriteUTFChars(stream, "name=\"" + assemblyName + "\"" + Environment.NewLine, 8);
				ComIntegrationManifestGenerator.WriteUTFChars(stream, "version=\"1.0.0.0\"/>" + Environment.NewLine, 8);
				ComIntegrationManifestGenerator.AsmCreateWin32ManifestFile(stream, aTypes);
			}
			catch (Exception ex)
			{
				if (ex is NullReferenceException || ex is SEHException)
				{
					throw;
				}
				stream.Close();
				File.Delete(strAssemblyManifestFileName);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ManifestCreationFailed(strAssemblyManifestFileName, ex.Message));
			}
			stream.Close();
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000374D8 File Offset: 0x000356D8
		private static void AsmCreateWin32ManifestFile(Stream s, Type[] aTypes)
		{
			string value = "</assembly>";
			ComIntegrationManifestGenerator.WriteTypes(s, aTypes, 4);
			ComIntegrationManifestGenerator.WriteUTFChars(s, value);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x000374FC File Offset: 0x000356FC
		private static void WriteTypes(Stream s, Type[] aTypes, int offset)
		{
			RegistrationServices registrationServices = new RegistrationServices();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string imageRuntimeVersion = executingAssembly.ImageRuntimeVersion;
			foreach (Type type in aTypes)
			{
				if (!registrationServices.TypeRequiresRegistration(type))
				{
					throw Fx.AssertAndThrow("User defined types must be registrable");
				}
				string str = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpperInvariant() + "}";
				string fullName = type.FullName;
				if (registrationServices.TypeRepresentsComType(type) || type.IsValueType)
				{
					ComIntegrationManifestGenerator.WriteUTFChars(s, "<clrSurrogate" + Environment.NewLine, offset);
					ComIntegrationManifestGenerator.WriteUTFChars(s, "    clsid=\"" + str + "\"" + Environment.NewLine, offset);
					ComIntegrationManifestGenerator.WriteUTFChars(s, "    name=\"" + fullName + "\"" + Environment.NewLine, offset);
					ComIntegrationManifestGenerator.WriteUTFChars(s, "    runtimeVersion=\"" + imageRuntimeVersion + "\">" + Environment.NewLine, offset);
					ComIntegrationManifestGenerator.WriteUTFChars(s, "</clrSurrogate>" + Environment.NewLine, offset);
				}
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00037624 File Offset: 0x00035824
		private static void WriteUTFChars(Stream s, string value, int offset)
		{
			for (int i = 0; i < offset; i++)
			{
				ComIntegrationManifestGenerator.WriteUTFChars(s, " ");
			}
			ComIntegrationManifestGenerator.WriteUTFChars(s, value);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00037650 File Offset: 0x00035850
		private static void WriteUTFChars(Stream s, string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			s.Write(bytes, 0, bytes.Length);
		}
	}
}
