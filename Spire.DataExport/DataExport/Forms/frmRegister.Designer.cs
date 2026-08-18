namespace Spire.DataExport.Forms
{
	// Token: 0x0200019F RID: 415
	public partial class frmRegister : global::System.Windows.Forms.Form
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x00077D60 File Offset: 0x00076D60
		protected override void Dispose(bool disposing)
		{
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_72;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_72;
					case 2:
						if (this.ᜈ != null)
						{
							num = 3;
							continue;
						}
						goto IL_91;
					case 3:
						this.ᜈ.Dispose();
						num = 0;
						continue;
					case 4:
						num = 2;
						continue;
					}
					if (!disposing)
					{
						goto IL_91;
					}
					num = 4;
					break;
				}
			}
			IL_72:
			IL_91:
			base.Dispose(disposing);
		}

		// Token: 0x040008B7 RID: 2231
		private global::System.ComponentModel.Container ᜈ;
	}
}
