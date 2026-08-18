using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Security.Permissions;
using System.Web.UI.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x0200021C RID: 540
	[SecurityPermission(SecurityAction.Demand)]
	public class ImageFileNameEditor : UITypeEditor
	{
		// Token: 0x0600100A RID: 4106 RVA: 0x000AD790 File Offset: 0x000AC790
		protected void InitializeDialog(OpenFileDialog openFileDialog)
		{
			int a_ = 12;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			openFileDialog.CheckFileExists = false;
			openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("䨧䜩尫", a_);
			openFileDialog.Filter = HyperlinksCollectionEditor.b("椧䘩䀫อ礯弱唳儵崷䤹᰻ᘽ樿汁♃⭅㡇煉晋恍❏㽑㉓浕牗瑙㥛㍝ٟ奡乣䡥ɧᩩ୫啭婯山ṳٵᵷᵹ䝻命깿뎇ꂉꊋ憐붓늗뒙ﺛ킟馡躣袥\udfa7잩쪫閭骯鲱톳\udbb5\udeb7膹隻邽꪿닁ꏃﷅꛋ뻍뗏뗑ﳕ꫙닛망\udbdf죡쫣臥臧賩郫곭駯蛱駳韵裷觹\udcfb훽⫿Ⰱ昃欅砇⌉瀋␍㸏瀑礓昕搗圙礛樝䄟䐡䴣䨥䴧天ఫحᨯᰱ䌳嬵帷ጹ䀻ᐽ渿㕁⥃⁅㑇ཉ≋♍ㅏ㱑㝓㍕㱗穙ᅛ㭝ᑟ͡ɣཥѧཀྵὫ乭塯塱婳፵ᕷᱹ啻ɽꩿ겁욋\ude8d햏햑뒓\udf95ﮙﮛﮝ肟춣쪥춧誩蒫蒭麯\ud8b1쒳통醷욹隻邽꪿닁ꏃ뫅苇髉觋觍鯑맓럕뿗뿙ﳛ飝觟軡臣웥샧샩싫蓭胯韱鏳\udff5蓷탹틻铽烿朁挃稅堇䐉䬋⸍夏缑甓焕紗㨙㐛㐝ฟ刡䨣䄥ħ嘩ث-䀯就匳䨵缷猹稻ḽि⽁╃ⅅⵇ橉摋摍繏㕑㵓さ煗♙癛灝ݟୡɣ", a_);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x000AD804 File Offset: 0x000AC804
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return true;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x000AD840 File Offset: 0x000AC840
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			int a_ = 13;
			for (;;)
			{
				IL_5D:
				IComponent component = null;
				for (;;)
				{
					int num = 10;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return value;
						case 1:
							if (provider != null)
							{
								num = 6;
								continue;
							}
							return value;
						case 2:
							goto IL_97;
						case 3:
							if (this.ᜃ.ShowDialog() == DialogResult.OK)
							{
								num = 5;
								continue;
							}
							return value;
						case 4:
							if (value is string)
							{
								num = 18;
								continue;
							}
							goto IL_E3;
						case 5:
							value = this.ᜃ.FileName;
							num = 0;
							continue;
						case 6:
							num = 13;
							continue;
						case 7:
							goto IL_30D;
						case 8:
							this.ᜃ = new OpenFileDialog();
							this.InitializeDialog(this.ᜃ);
							num = 16;
							continue;
						case 9:
						{
							string text = (string)value;
							text = UrlBuilder.BuildUrl(component, null, (string)value, HyperlinksCollectionEditor.b("稨个䄬䨮到䜲ᔴ縶吸娺娼娾", a_), HyperlinksCollectionEditor.b("怨䘪䰬䠮吰ጲ猴帶唸帺丼᜾歀浂≄⹆⽈灊杌慎㭐⍒㉔汖獘畚㝜⽞ѠѢ幤䵦䝨४lὮ䩰奲孴vᑸᵺ䙼啾꾀ꂈꞌꆎ朗겖뎘떚욠颢辤覦쎨\udbaa좬좮誰馲鮴햶풸쮺蚼閾듂꣄ꇆ뿎뿐듒꧔", a_), UrlBuilderOptions.None);
							num = 17;
							continue;
						}
						case 10:
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										IDesignerHost designerHost;
										if (designerHost.RootComponent != null)
										{
											num = 8;
											continue;
										}
										goto IL_2F7;
									}
									case 1:
										num = 0;
										continue;
									case 3:
									{
										IDesignerHost designerHost = (IDesignerHost)context.GetService(typeof(IDesignerHost));
										num = 4;
										continue;
									}
									case 4:
									{
										IDesignerHost designerHost;
										if (designerHost != null)
										{
											num = 1;
											continue;
										}
										goto IL_2F7;
									}
									case 5:
									{
										IDesignerHost designerHost;
										component = designerHost.RootComponent;
										this.ᜂ = true;
										num = 9;
										continue;
									}
									case 6:
									{
										IDesignerHost designerHost;
										if (!(designerHost.RootComponent is Form))
										{
											num = 5;
											continue;
										}
										goto IL_2F7;
									}
									case 7:
										goto IL_302;
									case 8:
										num = 6;
										continue;
									case 9:
										goto IL_2F7;
									}
									if (context != null)
									{
										num = 3;
										continue;
									}
									IL_2F7:
									num = 7;
								}
								IL_302:
								goto IL_C2;
							}
							catch (Exception)
							{
								goto IL_C2;
							}
							goto IL_30D;
							IL_C2:
							num = 1;
							continue;
						case 11:
							if (this.ᜂ)
							{
								num = 9;
								continue;
							}
							num = 14;
							continue;
						case 12:
							goto IL_E3;
						case 13:
							if ((IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService)) != null)
							{
								num = 7;
								continue;
							}
							return value;
						case 14:
							if (true)
							{
							}
							if (this.ᜃ == null)
							{
								num = 8;
								continue;
							}
							goto IL_6C;
						case 15:
						{
							string text;
							value = text;
							num = 2;
							continue;
						}
						case 16:
							goto IL_6C;
						case 17:
						{
							string text;
							if (text != null)
							{
								num = 15;
								continue;
							}
							return value;
						}
						case 18:
							this.ᜃ.FileName = (string)value;
							num = 12;
							continue;
						}
						goto IL_5D;
						IL_6C:
						num = 4;
						continue;
						IL_E3:
						num = 3;
						continue;
						IL_30D:
						num = 11;
					}
					IL_97:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					goto Block_2;
				}
			}
			Block_2:
			if (false)
			{
			}
			return value;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x000ADB94 File Offset: 0x000ACB94
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x000ADBD0 File Offset: 0x000ACBD0
		public override void PaintValue(PaintValueEventArgs e)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_23:
					Image image = null;
					for (;;)
					{
						IL_25:
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (File.Exists(e.Value.ToString()))
								{
									num = 2;
									continue;
								}
								return;
							case 1:
								try
								{
									if (true)
									{
									}
									int width = image.Width;
									int height = image.Height;
									e.Graphics.DrawImage(image, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, null);
									return;
								}
								finally
								{
									image.Dispose();
								}
								goto IL_F1;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_25;
								default:
									if (false)
									{
									}
									goto IL_F1;
								}
								break;
							}
							goto IL_23;
							IL_F1:
							image = Image.FromFile(e.Value.ToString());
							num = 1;
						}
					}
				}
				return;
			}
		}

		// Token: 0x04000B9F RID: 2975
		private const string ᜀ = "Select Image";

		// Token: 0x04000BA0 RID: 2976
		private const string ᜁ = "Image Files(*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png|";

		// Token: 0x04000BA1 RID: 2977
		private byte \u2593\u0094\u008E\u00A0;

		// Token: 0x04000BA2 RID: 2978
		private bool ᜂ;

		// Token: 0x04000BA3 RID: 2979
		private OpenFileDialog ᜃ;
	}
}
