using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Collections.Immutable;

namespace System
{
	// Token: 0x02000005 RID: 5
	internal static class SR
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020DF File Offset: 0x000002DF
		private static ResourceManager ResourceManager
		{
			get
			{
				if (System.SR.s_resourceManager == null)
				{
					System.SR.s_resourceManager = new ResourceManager(System.SR.ResourceType);
				}
				return System.SR.s_resourceManager;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020FC File Offset: 0x000002FC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002100 File Offset: 0x00000300
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text, StringComparison.Ordinal))
			{
				return defaultString;
			}
			return text;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.SR.UsingResourceKeys())
			{
				return resourceFormat + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002167 File Offset: 0x00000367
		internal static string Format(string resourceFormat, object p1)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, new object[]
			{
				p1
			});
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002199 File Offset: 0x00000399
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2
				});
			}
			return string.Format(resourceFormat, new object[]
			{
				p1,
				p2
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021D4 File Offset: 0x000003D4
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2,
					p3
				});
			}
			return string.Format(resourceFormat, new object[]
			{
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002221 File Offset: 0x00000421
		internal static string ArrayInitializedStateNotEqual
		{
			get
			{
				return System.SR.GetResourceString("ArrayInitializedStateNotEqual", null);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000222E File Offset: 0x0000042E
		internal static string ArrayLengthsNotEqual
		{
			get
			{
				return System.SR.GetResourceString("ArrayLengthsNotEqual", null);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000223B File Offset: 0x0000043B
		internal static string CannotFindOldValue
		{
			get
			{
				return System.SR.GetResourceString("CannotFindOldValue", null);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002248 File Offset: 0x00000448
		internal static string CapacityMustBeGreaterThanOrEqualToCount
		{
			get
			{
				return System.SR.GetResourceString("CapacityMustBeGreaterThanOrEqualToCount", null);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002255 File Offset: 0x00000455
		internal static string CapacityMustEqualCountOnMove
		{
			get
			{
				return System.SR.GetResourceString("CapacityMustEqualCountOnMove", null);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002262 File Offset: 0x00000462
		internal static string CollectionModifiedDuringEnumeration
		{
			get
			{
				return System.SR.GetResourceString("CollectionModifiedDuringEnumeration", null);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000226F File Offset: 0x0000046F
		internal static string DuplicateKey
		{
			get
			{
				return System.SR.GetResourceString("DuplicateKey", null);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000227C File Offset: 0x0000047C
		internal static string InvalidEmptyOperation
		{
			get
			{
				return System.SR.GetResourceString("InvalidEmptyOperation", null);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002289 File Offset: 0x00000489
		internal static string InvalidOperationOnDefaultArray
		{
			get
			{
				return System.SR.GetResourceString("InvalidOperationOnDefaultArray", null);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002296 File Offset: 0x00000496
		internal static Type ResourceType
		{
			get
			{
				return typeof(FxResources.System.Collections.Immutable.SR);
			}
		}

		// Token: 0x04000001 RID: 1
		private static ResourceManager s_resourceManager;

		// Token: 0x04000002 RID: 2
		private const string s_resourcesName = "FxResources.System.Collections.Immutable.SR";
	}
}
