using System;
using System.Configuration;
using System.Drawing.Configuration;
using System.IO;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x02000399 RID: 921
	internal static class BitmapSelector
	{
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x000EB340 File Offset: 0x000E9540
		// (set) Token: 0x06002580 RID: 9600 RVA: 0x000EB389 File Offset: 0x000E9589
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

		// Token: 0x06002581 RID: 9601 RVA: 0x000EB394 File Offset: 0x000E9594
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

		// Token: 0x06002582 RID: 9602 RVA: 0x000EB3D0 File Offset: 0x000E95D0
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

		// Token: 0x06002583 RID: 9603 RVA: 0x000EB404 File Offset: 0x000E9604
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

		// Token: 0x06002584 RID: 9604 RVA: 0x000EB434 File Offset: 0x000E9634
		private static bool DoesAssemblyHaveCustomAttribute(Assembly assembly, string typeName)
		{
			return BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, assembly.GetType(typeName));
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000EB444 File Offset: 0x000E9644
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

		// Token: 0x06002586 RID: 9606 RVA: 0x000EB46A File Offset: 0x000E966A
		internal static bool SatelliteAssemblyOptIn(Assembly assembly)
		{
			return BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, typeof(BitmapSuffixInSatelliteAssemblyAttribute)) || BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, "System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute");
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000EB48B File Offset: 0x000E968B
		internal static bool SameAssemblyOptIn(Assembly assembly)
		{
			return BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, typeof(BitmapSuffixInSameAssemblyAttribute)) || BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, "System.Drawing.BitmapSuffixInSameAssemblyAttribute");
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000EB4AC File Offset: 0x000E96AC
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

		// Token: 0x06002589 RID: 9609 RVA: 0x000EB56C File Offset: 0x000E976C
		public static Stream GetResourceStream(Type type, string originalName)
		{
			return BitmapSelector.GetResourceStream(type.Module.Assembly, type, originalName);
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000EB580 File Offset: 0x000E9780
		public static Icon CreateIcon(Type type, string originalName)
		{
			return new Icon(BitmapSelector.GetResourceStream(type, originalName));
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000EB58E File Offset: 0x000E978E
		public static Bitmap CreateBitmap(Type type, string originalName)
		{
			return new Bitmap(BitmapSelector.GetResourceStream(type, originalName));
		}

		// Token: 0x04001B5B RID: 7003
		private static string _suffix;
	}
}
