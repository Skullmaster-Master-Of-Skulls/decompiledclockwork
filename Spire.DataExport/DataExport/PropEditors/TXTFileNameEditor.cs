using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.TXT;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000218 RID: 536
	public class TXTFileNameEditor : UITypeEditor
	{
		// Token: 0x06000FFF RID: 4095 RVA: 0x000ACBF0 File Offset: 0x000ABBF0
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return UITypeEditorEditStyle.Modal;
				case 2:
					num = 3;
					continue;
				case 3:
					if (context.Instance != null)
					{
						num = 0;
						continue;
					}
					goto IL_5B;
				}
				if (true)
				{
				}
				if (context != null)
				{
					num = 2;
					continue;
				}
				IL_5B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_71;
				}
			}
			return UITypeEditorEditStyle.Modal;
			IL_71:
			if (false)
			{
			}
			return base.GetEditStyle(context);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000ACC7C File Offset: 0x000ABC7C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			int a_ = 16;
			for (;;)
			{
				TXTExport txtexport = null;
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_91;
					case 1:
						if (txtexport != null)
						{
							num = 7;
							continue;
						}
						return value;
					case 2:
						try
						{
							for (;;)
							{
								OpenFileDialog openFileDialog;
								openFileDialog.CheckFileExists = false;
								TextExportType exportType = txtexport.ExportType;
								num = 10;
								for (;;)
								{
									switch (num)
									{
									case 0:
										openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("堫嘭䐯", a_);
										openFileDialog.Filter = HyperlinksCollectionEditor.b("砫䬭䠯䘱ᐳ倵儷嘹夻䴽怿橁湃桅㱇㉉㡋杍ⱏ硑穓≕⁗⹙", a_);
										num = 7;
										continue;
									case 1:
										value = openFileDialog.FileName;
										num = 3;
										continue;
									case 2:
										if (openFileDialog.ShowDialog() == DialogResult.OK)
										{
											num = 1;
											continue;
										}
										goto IL_2C0;
									case 3:
										goto IL_2C0;
									case 4:
										num = 0;
										continue;
									case 5:
										goto IL_1EA;
									case 6:
										goto IL_1EA;
									case 7:
										goto IL_1EA;
									case 8:
										goto IL_1EA;
									case 9:
										goto IL_1EA;
									case 10:
										switch (exportType)
										{
										case TextExportType.TXT:
											openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("堫嘭䐯", a_);
											openFileDialog.Filter = HyperlinksCollectionEditor.b("砫䬭䠯䘱ᐳ倵儷嘹夻䴽怿橁湃桅㱇㉉㡋杍ⱏ硑穓≕⁗⹙", a_);
											num = 6;
											continue;
										case TextExportType.CSV:
											openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("伫崭䘯", a_);
											openFileDialog.Filter = HyperlinksCollectionEditor.b("漫紭是ሱ刳張吷弹伻ḽ栿桁橃╅㭇㱉敋㉍穏籑㝓╕⹗", a_);
											num = 8;
											continue;
										case TextExportType.DIF:
											openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("䠫䜭嘯", a_);
											openFileDialog.Filter = HyperlinksCollectionEditor.b("栫札瘯ሱ刳張吷弹伻ḽ栿桁橃≅ⅇⱉ敋㉍穏籑こ㽕㹗", a_);
											num = 9;
											continue;
										case TextExportType.SYLK:
											openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("弫䈭嬯", a_);
											openFileDialog.Filter = HyperlinksCollectionEditor.b("缫眭簯礱ᐳ倵儷嘹夻䴽怿橁湃桅㭇♉❋杍ⱏ硑穓╕㑗ㅙ", a_);
											num = 5;
											continue;
										default:
											num = 4;
											continue;
										}
										break;
									case 11:
										goto IL_2CB;
									}
									break;
									IL_1EA:
									num = 2;
									continue;
									IL_2C0:
									num = 11;
								}
							}
							IL_2CB:
							return value;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								OpenFileDialog openFileDialog;
								switch (num)
								{
								case 0:
									((IDisposable)openFileDialog).Dispose();
									num = 1;
									continue;
								case 1:
									goto IL_304;
								}
								if (openFileDialog == null)
								{
									break;
								}
								num = 0;
							}
							IL_304:;
						}
						goto IL_307;
					case 3:
						num = 5;
						continue;
					case 4:
						if (context.Instance != null)
						{
							num = 3;
							continue;
						}
						goto IL_91;
					case 5:
						if (context.Instance is TXTExport)
						{
							num = 6;
							continue;
						}
						goto IL_91;
					case 6:
						goto IL_307;
					case 7:
					{
						OpenFileDialog openFileDialog = new OpenFileDialog();
						num = 2;
						continue;
					}
					case 8:
						if (true)
						{
						}
						num = 4;
						continue;
					case 9:
						if (context == null)
						{
							goto IL_91;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					}
					break;
					IL_91:
					num = 1;
					continue;
					IL_307:
					txtexport = (context.Instance as TXTExport);
					num = 0;
				}
			}
			return value;
		}
	}
}
