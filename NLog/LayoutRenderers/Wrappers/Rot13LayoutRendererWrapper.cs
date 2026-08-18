using System;
using NLog.Config;
using NLog.Layouts;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000106 RID: 262
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[LayoutRenderer("rot13")]
	public sealed class Rot13LayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00010396 File Offset: 0x0000E596
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0001039E File Offset: 0x0000E59E
		public Layout Text
		{
			get
			{
				return base.Inner;
			}
			set
			{
				base.Inner = value;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000103A8 File Offset: 0x0000E5A8
		public static string DecodeRot13(string encodedValue)
		{
			if (encodedValue == null)
			{
				return null;
			}
			char[] array = encodedValue.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Rot13LayoutRendererWrapper.DecodeRot13Char(array[i]);
			}
			return new string(array);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000103E0 File Offset: 0x0000E5E0
		protected override string Transform(string text)
		{
			return Rot13LayoutRendererWrapper.DecodeRot13(text);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000103E8 File Offset: 0x0000E5E8
		private static char DecodeRot13Char(char c)
		{
			if (c >= 'A' && c <= 'M')
			{
				return 'N' + (c - 'A');
			}
			if (c >= 'a' && c <= 'm')
			{
				return 'n' + (c - 'a');
			}
			if (c >= 'N' && c <= 'Z')
			{
				return 'A' + (c - 'N');
			}
			if (c >= 'n' && c <= 'z')
			{
				return 'a' + (c - 'n');
			}
			return c;
		}
	}
}
