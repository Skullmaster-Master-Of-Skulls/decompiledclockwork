namespace Spire.DataExport.Forms
{
	// Token: 0x0200019E RID: 414
	public partial class DataExportColumnsEditor : global::System.Windows.Forms.Form
	{
		// Token: 0x06000B47 RID: 2887 RVA: 0x00075654 File Offset: 0x00074654
		protected override void Dispose(bool disposing)
		{
			for (;;)
			{
				IL_00:
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (this.\u170D != null)
						{
							num = 3;
							continue;
						}
						goto IL_8E;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 3:
						this.\u170D.Dispose();
						num = 4;
						continue;
					case 4:
						goto IL_72;
					}
					if (!disposing)
					{
						goto IL_8E;
					}
					num = 0;
				}
			}
			IL_72:
			IL_8E:
			base.Dispose(disposing);
		}

		// Token: 0x0400089D RID: 2205
		private global::System.ComponentModel.IContainer \u170D;
	}
}
