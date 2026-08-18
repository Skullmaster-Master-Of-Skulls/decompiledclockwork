using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Spire.DataExport.Common
{
	// Token: 0x0200016A RID: 362
	public abstract class MemoryExport : ExportBase
	{
		// Token: 0x06000984 RID: 2436
		protected abstract void ShowResult();

		// Token: 0x06000985 RID: 2437 RVA: 0x000609C4 File Offset: 0x0005F9C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void SaveToFile()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.SaveToFile();
					if (true)
					{
					}
					int num = 1;
					for (;;)
					{
						MemoryStream memoryStream;
						switch (num)
						{
						case 0:
							try
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
								{
									if (false)
									{
									}
									StreamWriter streamWriter = new StreamWriter(memoryStream, base.CurrentEncoding);
									try
									{
										this.SaveToMemoryStream(memoryStream, streamWriter);
										memoryStream.Position = 0L;
										byte[] array = new byte[memoryStream.Length];
										memoryStream.Read(array, 0, array.Length);
										string @string = base.CurrentEncoding.GetString(array);
										Clipboard.SetDataObject(@string, true);
									}
									finally
									{
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												((IDisposable)streamWriter).Dispose();
												num = 2;
												continue;
											case 2:
												goto IL_10A;
											}
											if (streamWriter == null)
											{
												break;
											}
											num = 0;
										}
										IL_10A:;
									}
									break;
								}
								}
								goto IL_187;
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_14A;
									case 1:
										((IDisposable)memoryStream).Dispose();
										num = 0;
										continue;
									}
									if (memoryStream == null)
									{
										break;
									}
									num = 1;
								}
								IL_14A:;
							}
							goto IL_14D;
						case 1:
							if (!this.m_exportIfEmpty)
							{
								num = 3;
								continue;
							}
							goto IL_14D;
						case 2:
							if (base.\u1733())
							{
								num = 4;
								continue;
							}
							goto IL_14D;
						case 3:
							num = 2;
							continue;
						case 4:
							return;
						}
						break;
						IL_14D:
						memoryStream = new MemoryStream();
						num = 0;
					}
				}
				return;
				IL_187:
				this.ShowResult();
				return;
			}
		}
	}
}
