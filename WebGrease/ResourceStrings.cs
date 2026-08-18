using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace WebGrease
{
	// Token: 0x020001B9 RID: 441
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	internal class ResourceStrings
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x00081848 File Offset: 0x0007FA48
		internal ResourceStrings()
		{
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00081850 File Offset: 0x0007FA50
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(ResourceStrings.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("WebGrease.ResourceStrings", typeof(ResourceStrings).Assembly);
					ResourceStrings.resourceMan = resourceManager;
				}
				return ResourceStrings.resourceMan;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0008188F File Offset: 0x0007FA8F
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x00081896 File Offset: 0x0007FA96
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return ResourceStrings.resourceCulture;
			}
			set
			{
				ResourceStrings.resourceCulture = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0008189E File Offset: 0x0007FA9E
		internal static string BundlingFiles
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("BundlingFiles", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x000818B4 File Offset: 0x0007FAB4
		internal static string ConfigurationFileParseError
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("ConfigurationFileParseError", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x000818CA File Offset: 0x0007FACA
		internal static string DuplicateFoundFormat
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("DuplicateFoundFormat", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x000818E0 File Offset: 0x0007FAE0
		internal static string ErrorsInFileFormat
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("ErrorsInFileFormat", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x000818F6 File Offset: 0x0007FAF6
		internal static string FileHasheActivityCouldNotLocateDirectory
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("FileHasheActivityCouldNotLocateDirectory", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x0008190C File Offset: 0x0007FB0C
		internal static string FileHasherActivityErrorOccurred
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("FileHasherActivityErrorOccurred", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00081922 File Offset: 0x0007FB22
		internal static string GeneralErrorMessage
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("GeneralErrorMessage", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x00081938 File Offset: 0x0007FB38
		internal static string InvalidBundlingOutputFile
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("InvalidBundlingOutputFile", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0008194E File Offset: 0x0007FB4E
		internal static string MinifyingCssFilesAndSpritingBackgroundImages
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("MinifyingCssFilesAndSpritingBackgroundImages", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x00081964 File Offset: 0x0007FB64
		internal static string MoreThan256Colours
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("MoreThan256Colours", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x0008197A File Offset: 0x0007FB7A
		internal static string MultipleSwitches
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("MultipleSwitches", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00081990 File Offset: 0x0007FB90
		internal static string NoFilesProcessed
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("NoFilesProcessed", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x000819A6 File Offset: 0x0007FBA6
		internal static string OverrideFileLoadErrorMessage
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("OverrideFileLoadErrorMessage", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x000819BC File Offset: 0x0007FBBC
		internal static string PreprocessingCouldNotFindThePluginPath
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("PreprocessingCouldNotFindThePluginPath", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x000819D2 File Offset: 0x0007FBD2
		internal static string PreprocessingEngineFound
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("PreprocessingEngineFound", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x000819E8 File Offset: 0x0007FBE8
		internal static string PreprocessingInitializeEnd
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("PreprocessingInitializeEnd", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x000819FE File Offset: 0x0007FBFE
		internal static string PreprocessingInitializeStart
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("PreprocessingInitializeStart", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00081A14 File Offset: 0x0007FC14
		internal static string PreprocessingLoadingError
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("PreprocessingLoadingError", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00081A2A File Offset: 0x0007FC2A
		internal static string PreprocessingPluginPath
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("PreprocessingPluginPath", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00081A40 File Offset: 0x0007FC40
		internal static string ResolvingTokensAndPerformingLocalization
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("ResolvingTokensAndPerformingLocalization", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00081A56 File Offset: 0x0007FC56
		internal static string ResourcePivotActivityDuplicateKeysError
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("ResourcePivotActivityDuplicateKeysError", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00081A6C File Offset: 0x0007FC6C
		internal static string ResourcePivotActivityError
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("ResourcePivotActivityError", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00081A82 File Offset: 0x0007FC82
		internal static string ResourceResolverDuplicateKeyExceptionMessage
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("ResourceResolverDuplicateKeyExceptionMessage", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x00081A98 File Offset: 0x0007FC98
		internal static string SafeLockFailedMessage
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("SafeLockFailedMessage", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00081AAE File Offset: 0x0007FCAE
		internal static string SemiTransparencyFound
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("SemiTransparencyFound", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x00081AC4 File Offset: 0x0007FCC4
		internal static string ThereWereErrorsWhileApplyingCssresources
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("ThereWereErrorsWhileApplyingCssresources", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00081ADA File Offset: 0x0007FCDA
		internal static string ThereWereErrorsWhileBundlingFiles
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("ThereWereErrorsWhileBundlingFiles", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x00081AF0 File Offset: 0x0007FCF0
		internal static string ThereWereErrorsWhileMinifyingTheCssFiles
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("ThereWereErrorsWhileMinifyingTheCssFiles", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x00081B06 File Offset: 0x0007FD06
		internal static string Usage
		{
			get
			{
				return ResourceStrings.ResourceManager.GetString("Usage", ResourceStrings.resourceCulture);
			}
		}

		// Token: 0x04000BD1 RID: 3025
		private static ResourceManager resourceMan;

		// Token: 0x04000BD2 RID: 3026
		private static CultureInfo resourceCulture;
	}
}
