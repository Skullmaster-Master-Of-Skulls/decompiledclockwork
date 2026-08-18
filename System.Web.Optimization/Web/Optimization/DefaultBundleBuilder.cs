using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Hosting;

namespace System.Web.Optimization
{
	// Token: 0x0200002F RID: 47
	public class DefaultBundleBuilder : IBundleBuilder
	{
		// Token: 0x0600015C RID: 348 RVA: 0x000054C4 File Offset: 0x000036C4
		private static Dictionary<string, string> GetInstrumentedBundlePreamble(string boundaryValue)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["Bundle"] = "System.Web.Optimization.Bundle";
			dictionary["Boundary"] = boundaryValue;
			return dictionary;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000054F4 File Offset: 0x000036F4
		private static string GetBoundaryIdentifier(Bundle bundle)
		{
			Type type;
			if (bundle.Transforms != null && bundle.Transforms.Count > 0)
			{
				type = bundle.Transforms[0].GetType();
			}
			else
			{
				type = typeof(DefaultTransform);
			}
			return Convert.ToBase64String(Encoding.Unicode.GetBytes(type.FullName.GetHashCode().ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000555D File Offset: 0x0000375D
		private static string GetInstrumentedFileHeaderFormat(string boundaryValue)
		{
			return "/* " + boundaryValue + " \"{0}\" */";
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005570 File Offset: 0x00003770
		internal static string ConvertToAppRelativePath(string appPath, string fullName)
		{
			if (string.Equals("/", appPath, StringComparison.OrdinalIgnoreCase))
			{
				return fullName;
			}
			string text;
			if (!string.IsNullOrEmpty(appPath) && fullName.StartsWith(appPath, StringComparison.OrdinalIgnoreCase))
			{
				text = fullName.Replace(appPath, "~/");
			}
			else
			{
				text = fullName;
			}
			return text.Replace('\\', '/');
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000055BC File Offset: 0x000037BC
		private static string GetApplicationPath(VirtualPathProvider vpp)
		{
			if (vpp != null && vpp.DirectoryExists("~"))
			{
				VirtualDirectory directory = vpp.GetDirectory("~");
				if (directory != null)
				{
					return directory.VirtualPath;
				}
			}
			return null;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000055F0 File Offset: 0x000037F0
		private static string GetFileHeader(BundleContext context, VirtualFile file, string fileHeaderFormat)
		{
			if (string.IsNullOrEmpty(fileHeaderFormat))
			{
				return string.Empty;
			}
			string applicationPath = DefaultBundleBuilder.GetApplicationPath(context.VirtualPathProvider);
			return string.Format(CultureInfo.InvariantCulture, fileHeaderFormat, new object[]
			{
				DefaultBundleBuilder.ConvertToAppRelativePath(applicationPath, file.VirtualPath)
			}) + "\r\n";
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005644 File Offset: 0x00003844
		private static string GenerateBundlePreamble(string bundleHash)
		{
			Dictionary<string, string> instrumentedBundlePreamble = DefaultBundleBuilder.GetInstrumentedBundlePreamble(bundleHash);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("/* ");
			foreach (string text in instrumentedBundlePreamble.Keys)
			{
				stringBuilder.Append(text + "=" + instrumentedBundlePreamble[text] + ";");
			}
			stringBuilder.Append(" */");
			return stringBuilder.ToString();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000056DC File Offset: 0x000038DC
		public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
		{
			if (files == null)
			{
				return string.Empty;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (bundle == null)
			{
				throw new ArgumentNullException("bundle");
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			if (context.EnableInstrumentation)
			{
				text = DefaultBundleBuilder.GetBoundaryIdentifier(bundle);
				stringBuilder.AppendLine(DefaultBundleBuilder.GenerateBundlePreamble(text));
			}
			string text2 = null;
			if (!string.IsNullOrEmpty(bundle.ConcatenationToken))
			{
				text2 = bundle.ConcatenationToken;
			}
			else
			{
				foreach (IBundleTransform bundleTransform in bundle.Transforms)
				{
					if (typeof(JsMinify).IsAssignableFrom(bundleTransform.GetType()))
					{
						text2 = ";" + Environment.NewLine;
						break;
					}
				}
			}
			if (text2 == null || context.EnableInstrumentation)
			{
				text2 = Environment.NewLine;
			}
			foreach (BundleFile bundleFile in files)
			{
				if (context.EnableInstrumentation)
				{
					stringBuilder.Append(DefaultBundleBuilder.GetFileHeader(context, bundleFile.VirtualFile, DefaultBundleBuilder.GetInstrumentedFileHeaderFormat(text)));
				}
				stringBuilder.Append(bundleFile.ApplyTransforms());
				stringBuilder.Append(text2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000077 RID: 119
		internal static IBundleBuilder Instance = new DefaultBundleBuilder();
	}
}
