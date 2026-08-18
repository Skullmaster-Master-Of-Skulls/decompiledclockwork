using System;
using System.Globalization;
using System.Security.Cryptography;
using System.ServiceModel.Security;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200084B RID: 2123
	internal static class PipeUri
	{
		// Token: 0x06004F7B RID: 20347 RVA: 0x00122A70 File Offset: 0x00120C70
		public static string BuildSharedMemoryName(Uri uri, HostNameComparisonMode hostNameComparisonMode, bool global)
		{
			string path = PipeUri.GetPath(uri);
			string hostName = null;
			switch (hostNameComparisonMode)
			{
			case HostNameComparisonMode.StrongWildcard:
				hostName = "+";
				break;
			case HostNameComparisonMode.Exact:
				hostName = uri.Host;
				break;
			case HostNameComparisonMode.WeakWildcard:
				hostName = "*";
				break;
			}
			return PipeUri.BuildSharedMemoryName(hostName, path, global);
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x00122ABC File Offset: 0x00120CBC
		internal static string BuildSharedMemoryName(string hostName, string path, bool global, AppContainerInfo appContainerInfo)
		{
			if (appContainerInfo == null)
			{
				return PipeUri.BuildSharedMemoryName(hostName, path, global);
			}
			return string.Format(CultureInfo.InvariantCulture, "Session\\{0}\\{1}\\{2}", new object[]
			{
				appContainerInfo.SessionId,
				appContainerInfo.NamedObjectPath,
				PipeUri.BuildSharedMemoryName(hostName, path, global)
			});
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x00122B0C File Offset: 0x00120D0C
		private static string BuildSharedMemoryName(string hostName, string path, bool global)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Uri.UriSchemeNetPipe);
			stringBuilder.Append("://");
			stringBuilder.Append(hostName.ToUpperInvariant());
			stringBuilder.Append(path);
			string s = stringBuilder.ToString();
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			byte[] inArray;
			string value;
			if (bytes.Length >= 128)
			{
				using (HashAlgorithm hashAlgorithm = PipeUri.GetHashAlgorithm())
				{
					inArray = hashAlgorithm.ComputeHash(bytes);
				}
				value = ":H";
			}
			else
			{
				inArray = bytes;
				value = ":E";
			}
			stringBuilder = new StringBuilder();
			if (global)
			{
				stringBuilder.Append("Global\\");
			}
			else
			{
				stringBuilder.Append("Local\\");
			}
			stringBuilder.Append(Uri.UriSchemeNetPipe);
			stringBuilder.Append(value);
			stringBuilder.Append(Convert.ToBase64String(inArray));
			return stringBuilder.ToString();
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x00122BF4 File Offset: 0x00120DF4
		private static HashAlgorithm GetHashAlgorithm()
		{
			if (!LocalAppContextSwitches.UseSha1InPipeConnectionGetHashAlgorithm)
			{
				if (SecurityUtilsEx.RequiresFipsCompliance)
				{
					return new SHA256CryptoServiceProvider();
				}
				return new SHA256Managed();
			}
			else
			{
				if (SecurityUtilsEx.RequiresFipsCompliance)
				{
					return new SHA1CryptoServiceProvider();
				}
				return new SHA1Managed();
			}
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x00122C24 File Offset: 0x00120E24
		public static string GetPath(Uri uri)
		{
			string text = uri.LocalPath.ToUpperInvariant();
			if (!text.EndsWith("/", StringComparison.Ordinal))
			{
				text += "/";
			}
			return text;
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x00122C58 File Offset: 0x00120E58
		public static string GetParentPath(string path)
		{
			if (path.EndsWith("/", StringComparison.Ordinal))
			{
				path = path.Substring(0, path.Length - 1);
			}
			if (path.Length == 0)
			{
				return path;
			}
			return path.Substring(0, path.LastIndexOf('/') + 1);
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x00122C94 File Offset: 0x00120E94
		public static void Validate(Uri uri)
		{
			if (uri.Scheme != Uri.UriSchemeNetPipe)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("uri", SR.GetString("PipeUriSchemeWrong"));
			}
		}
	}
}
