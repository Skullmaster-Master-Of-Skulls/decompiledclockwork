using System;
using System.Configuration;
using System.Drawing.Configuration;
using System.IO;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x020000FE RID: 254
	internal static class BitmapSelector
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000CBB5 File Offset: 0x0000ADB5
		internal static string Suffix
		{
			get
			{
				if (BitmapSelector._suffix == null)
				{
					BitmapSelector._suffix = string.Empty;
					SystemDrawingSection systemDrawingSection = ConfigurationManager.GetSection("system.drawing") as SystemDrawingSection;
					if (systemDrawingSection != null)
					{
						string bitmapSuffix = systemDrawingSection.BitmapSuffix;
						if (bitmapSuffix != null && bitmapSuffix != null)
						{
							BitmapSelector._suffix = bitmapSuffix;
						}
					}
				}
				return BitmapSelector._suffix;
			}
			set
			{
				BitmapSelector._suffix = value;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		internal static string AppendSuffix(string filePath)
		{
			string result;
			try
			{
				result = Path.ChangeExtension(filePath, BitmapSelector.Suffix + Path.GetExtension(filePath));
			}
			catch (ArgumentException)
			{
				result = filePath;
			}
			return result;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		public static string GetFileName(string originalPath)
		{
			if (BitmapSelector.Suffix == string.Empty)
			{
				return originalPath;
			}
			string text = BitmapSelector.AppendSuffix(originalPath);
			if (!File.Exists(text))
			{
				return originalPath;
			}
			return text;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000CC30 File Offset: 0x0000AE30
		private static Stream GetResourceStreamHelper(Assembly assembly, Type type, string name)
		{
			Stream result = null;
			try
			{
				result = assembly.GetManifestResourceStream(type, name);
			}
			catch (FileNotFoundException)
			{
			}
			return result;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000CC60 File Offset: 0x0000AE60
		private static bool DoesAssemblyHaveCustomAttribute(Assembly assembly, string typeName)
		{
			return BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, assembly.GetType(typeName));
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000CC70 File Offset: 0x0000AE70
		private static bool DoesAssemblyHaveCustomAttribute(Assembly assembly, Type attrType)
		{
			if (attrType != null)
			{
				object[] customAttributes = assembly.GetCustomAttributes(attrType, false);
				if (customAttributes.Length != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000CC96 File Offset: 0x0000AE96
		internal static bool SatelliteAssemblyOptIn(Assembly assembly)
		{
			return BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, typeof(BitmapSuffixInSatelliteAssemblyAttribute)) || BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, "System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute");
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000CCB7 File Offset: 0x0000AEB7
		internal static bool SameAssemblyOptIn(Assembly assembly)
		{
			return BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, typeof(BitmapSuffixInSameAssemblyAttribute)) || BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, "System.Drawing.BitmapSuffixInSameAssemblyAttribute");
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		public static Stream GetResourceStream(Assembly assembly, Type type, string originalName)
		{
			if (BitmapSelector.Suffix != string.Empty)
			{
				try
				{
					if (BitmapSelector.SameAssemblyOptIn(assembly))
					{
						string name = BitmapSelector.AppendSuffix(originalName);
						Stream resourceStreamHelper = BitmapSelector.GetResourceStreamHelper(assembly, type, name);
						if (resourceStreamHelper != null)
						{
							return resourceStreamHelper;
						}
					}
				}
				catch
				{
				}
				try
				{
					if (BitmapSelector.SatelliteAssemblyOptIn(assembly))
					{
						AssemblyName name2 = assembly.GetName();
						AssemblyName assemblyName = name2;
						assemblyName.Name += BitmapSelector.Suffix;
						name2.ProcessorArchitecture = ProcessorArchitecture.None;
						Assembly assembly2 = Assembly.Load(name2);
						if (assembly2 != null)
						{
							Stream resourceStreamHelper2 = BitmapSelector.GetResourceStreamHelper(assembly2, type, originalName);
							if (resourceStreamHelper2 != null)
							{
								return resourceStreamHelper2;
							}
						}
					}
				}
				catch
				{
				}
			}
			return assembly.GetManifestResourceStream(type, originalName);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000CD98 File Offset: 0x0000AF98
		public static Stream GetResourceStream(Type type, string originalName)
		{
			return BitmapSelector.GetResourceStream(type.Module.Assembly, type, originalName);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		public static Icon CreateIcon(Type type, string originalName)
		{
			return new Icon(BitmapSelector.GetResourceStream(type, originalName));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000CDBA File Offset: 0x0000AFBA
		public static Bitmap CreateBitmap(Type type, string originalName)
		{
			return new Bitmap(BitmapSelector.GetResourceStream(type, originalName));
		}

		// Token: 0x04000440 RID: 1088
		private static string _suffix;
	}
}
