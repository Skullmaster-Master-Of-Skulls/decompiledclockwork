using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FD RID: 253
	[LayoutRenderer("filesystem-normalize")]
	[AmbientProperty("FSNormalize")]
	[ThreadAgnostic]
	public sealed class FileSystemNormalizeLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x0000FD95 File Offset: 0x0000DF95
		public FileSystemNormalizeLayoutRendererWrapper()
		{
			this.FSNormalize = true;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		[DefaultValue(true)]
		public bool FSNormalize { get; set; }

		// Token: 0x06000720 RID: 1824 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		protected override string Transform(string text)
		{
			if (this.FSNormalize)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				for (int i = 0; i < stringBuilder.Length; i++)
				{
					char c = stringBuilder[i];
					if (!FileSystemNormalizeLayoutRendererWrapper.IsSafeCharacter(c))
					{
						stringBuilder[i] = '_';
					}
				}
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000FE06 File Offset: 0x0000E006
		private static bool IsSafeCharacter(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_' || c == '-' || c == '.' || c == ' ';
		}
	}
}
